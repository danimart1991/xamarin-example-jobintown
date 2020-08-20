using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AutoMapper;
using AzureStorage.Contracts;
using FilePicker.Contracts;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using JobInTown.Models;
using Localization.Contracts;
using Models.Mvvm;
using Navigation.Contracts;
using Network.Models.Exceptions.Basic;
using Settings.Contracts;
using Xamarin.Forms;

namespace JobInTown.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private readonly IApiClient _apiClient;
        private readonly IUserDialogs _userDialogs;
        private readonly IFilePickerService _filePickerService;
        private readonly IAzureStorageService _azureStorageService;
        private readonly ILocalizationService _localizationService;

        private string _noItemsText;
        private bool _isJobsRefreshing;
        private bool _isProfileRefreshing;
        private ObservableCollection<GalleryItem> _galleryItems;
        private ObservableCollection<GalleryItem> _myJobs;
        private GalleryItem _selectedItem;
        private string _userImageUrl;
        private string _userFullName;
        private string _userEmail;
        private bool _isDistanceOrder;
        private List<GalleryItem> _unorderedGalleryItems;

        public MainViewModel(
            INavigationService navigationService,
            ISettingsService settingsService,
            IApiClient apiClient,
            IUserDialogs userDialogs,
            IFilePickerService filePickerService,
            IAzureStorageService azureStorageService,
            ILocalizationService localizationService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _apiClient = apiClient;
            _userDialogs = userDialogs;
            _filePickerService = filePickerService;
            _azureStorageService = azureStorageService;
            _localizationService = localizationService;
        }

        public string NoItemsText
        {
            get
            {
                return _noItemsText;
            }

            set
            {
                _noItemsText = value;
                RaisePropertyChanged(() => NoItemsText);
            }
        }

        public bool IsJobsRefreshing
        {
            get
            {
                return _isJobsRefreshing;
            }

            set
            {
                _isJobsRefreshing = value;
                RaisePropertyChanged(() => IsJobsRefreshing);
            }
        }

        public bool IsProfileRefreshing
        {
            get
            {
                return _isProfileRefreshing;
            }

            set
            {
                _isProfileRefreshing = value;
                RaisePropertyChanged(() => IsProfileRefreshing);
            }
        }

        public ObservableCollection<GalleryItem> GalleryItems
        {
            get
            {
                return _galleryItems;
            }

            set
            {
                _galleryItems = value;
                RaisePropertyChanged(() => GalleryItems);
            }
        }

        public ObservableCollection<GalleryItem> MyJobs
        {
            get
            {
                return _myJobs;
            }

            set
            {
                _myJobs = value;
                RaisePropertyChanged(() => MyJobs);
            }
        }

        public GalleryItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    _navigationService?.NavigateToAsync<ItemDetailViewModel>(_selectedItem.Id).GetAwaiter();
                }
            }
        }

        public string UserImageUrl
        {
            get
            {
                return _userImageUrl;
            }

            set
            {
                _userImageUrl = value;
                RaisePropertyChanged(() => UserImageUrl);
            }
        }

        public string UserFullName
        {
            get
            {
                return _userFullName;
            }

            set
            {
                _userFullName = value;
                RaisePropertyChanged(() => UserFullName);
            }
        }

        public string UserEmail
        {
            get
            {
                return _userEmail;
            }

            set
            {
                _userEmail = value;
                RaisePropertyChanged(() => UserEmail);
            }
        }

        public ICommand AddItemCommand => new Command(async () => await AddItem());

        public ICommand GetItemsCommand => new Command(async () => await GetItems());

        public ICommand GetMyThingsCommand => new Command(async () => await GetMyThings());

        public ICommand ChangePhotoCommand => new Command(async () => await ChangePhoto());

        public ICommand LogoutCommand => new Command(async () => await Logout());

        public ICommand OrderJobsByDistanceCommand => new Command(() => JobsOrderByDistance());

        public ICommand OrderJobsByDateCommand => new Command(() => JobsOrderByDate());

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            await GetItems();
            await GetMyThings();
        }

        private Task AddItem()
        {
            return _navigationService?.NavigateToAsync<AddItemViewModel>(new Job());
        }

        private async Task GetItems()
        {
            if (!IsJobsRefreshing)
            {
                try
                {
                    IsJobsRefreshing = true;

                    await Task.Delay(1000);

                    var jobs = await _apiClient.GetJobsAsync();

                    _unorderedGalleryItems = Mapper.Map<List<Job>, List<GalleryItem>>(jobs);

                    if (_isDistanceOrder)
                    {
                        JobsOrderByDistance();
                    }
                    else
                    {
                        JobsOrderByDate();
                    }
                }
                catch (NetworkServiceClientErrorException ex)
                {
                    await _userDialogs.AlertAsync(ex?.Message);
                }
                catch (Exception ex)
                {
#if DEBUG
                    await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
                }
                finally
                {
                    IsJobsRefreshing = false;
                }
            }
        }

        private async Task GetMyThings()
        {
            await GetMyJobs();
            await GetMyProfile();
        }

        private async Task GetMyJobs()
        {
            if (!IsProfileRefreshing)
            {
                try
                {
                    IsProfileRefreshing = true;

                    await Task.Delay(1000);

                    var myJobs = await _apiClient.GetMyJobsAsync();

                    var galleryItems = Mapper.Map<List<Job>, List<GalleryItem>>(myJobs);

                    MyJobs = new ObservableCollection<GalleryItem>(galleryItems.OrderByDescending(x => x.PostedDate));
                }
                catch (NetworkServiceClientErrorException ex)
                {
                    await _userDialogs.AlertAsync(ex?.Message);
                }
                catch (Exception ex)
                {
#if DEBUG
                    await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                    await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
                }
                finally
                {
                    if (MyJobs == null || MyJobs.Count == 0)
                    {
                        NoItemsText = _localizationService.GetString("MainPage_MyJobsList_NoItems_Label_Text");
                    }
                    else
                    {
                        NoItemsText = null;
                    }

                    IsProfileRefreshing = false;
                }
            }
        }

        private async Task GetMyProfile()
        {
            try
            {
                var userInfo = await _apiClient.GetUserInfo();

                UserImageUrl = userInfo?.ImageUrl;
                if (string.IsNullOrEmpty(UserImageUrl))
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Windows:
                        case Device.WinPhone:
                            UserImageUrl = "Assets/UserPlaceholder.png";
                            break;
                        default:
                            UserImageUrl = "UserPlaceholder.png";
                            break;
                    }
                }

                UserFullName = userInfo?.FullName ?? "Failed to obtain this info.";
                UserEmail = userInfo?.Email ?? "Failed to obtain this info.";
            }
            catch (NetworkServiceClientErrorException ex)
            {
                await _userDialogs.AlertAsync(ex?.Message);
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
            }
        }

        private void JobsOrderByDate()
        {
            _isDistanceOrder = false;
            GalleryItems = new ObservableCollection<GalleryItem>(_unorderedGalleryItems.OrderByDescending(x => x.PostedDate));
        }

        private void JobsOrderByDistance()
        {
            _isDistanceOrder = true;
            GalleryItems = new ObservableCollection<GalleryItem>(_unorderedGalleryItems.OrderBy(x => x.Title));
        }

        private async Task ChangePhoto()
        {
            try
            {
                var photoFile = await _filePickerService.PickImage();

                var imageStream = new MemoryStream(photoFile.DataArray);
                if (imageStream != null)
                {
                    var imageUrl = await _azureStorageService.PushFile(imageStream);
                    await _apiClient.ChangeImageUrl(imageUrl);
                    UserImageUrl = imageUrl;
                }
            }
            catch (NetworkServiceClientErrorException ex)
            {
                await _userDialogs.AlertAsync(ex?.Message);
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
            }
        }

        private async Task Logout()
        {
            try
            {
                await _apiClient.LogoutAsync();

                _settingsService.RemoveKey(GlobalSettings.AccessTokenKey);

                await _navigationService.NavigateToAsync<LoginViewModel>();
                await _navigationService.RemoveBackStackAsync();
            }
            catch (NetworkServiceClientErrorException ex)
            {
                await _userDialogs.AlertAsync(ex?.Message);
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
            }
        }
    }
}