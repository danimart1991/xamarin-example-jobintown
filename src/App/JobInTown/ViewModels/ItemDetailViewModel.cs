using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using AutoMapper;
using Core.Extensions;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using JobInTown.Models;
using JobInTown.Models.Enums;
using Localization.Contracts;
using Models;
using Models.Mvvm;
using Navigation.Contracts;
using Network.Models.Exceptions.Basic;
using Plugin.Geolocator;
using Settings.Contracts;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static Core.Extensions.PositionExtensions;

namespace JobInTown.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiClient _apiClient;
        private readonly ISettingsService _settingsService;
        private readonly IUserDialogs _userDialogs;
        private readonly ILocalizationService _localizationService;

        private int _id;
        private string _title;
        private string _description;
        private string _fullName;
        private ContractType _contract;
        private WorkdayType _workday;
        private DateTime _postedDate;
        private CategoryType _category;
        private string _imageUrl;
        private bool _isDeleteButtonVisible;
        private Position _mapPosition;
        private Position _currentPosition;
        private string _mapAddress;
        private string _distance;

        private Geocoder _geocoder;

        public ItemDetailViewModel(
            INavigationService navigationService,
            IApiClient apiClient,
            ISettingsService settingsService,
            IUserDialogs userDialogs,
            ILocalizationService localizationService)
        {
            IsBusy = true;

            _navigationService = navigationService;
            _apiClient = apiClient;
            _settingsService = settingsService;
            _userDialogs = userDialogs;
            _localizationService = localizationService;

            _geocoder = new Geocoder();
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public string FullName
        {
            get
            {
                return _fullName;
            }

            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        public ContractType Contract
        {
            get
            {
                return _contract;
            }

            set
            {
                _contract = value;
                RaisePropertyChanged(() => Contract);
            }
        }

        public WorkdayType Workday
        {
            get
            {
                return _workday;
            }

            set
            {
                _workday = value;
                RaisePropertyChanged(() => Workday);
            }
        }

        public DateTime PostedDate
        {
            get
            {
                return _postedDate;
            }

            set
            {
                _postedDate = value;
                RaisePropertyChanged(() => PostedDate);
            }
        }

        public CategoryType Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                _imageUrl = value;
                RaisePropertyChanged(() => ImageUrl);
            }
        }

        public bool IsDeleteButtonVisible
        {
            get
            {
                return _isDeleteButtonVisible;
            }

            set
            {
                _isDeleteButtonVisible = value;
                RaisePropertyChanged(() => IsDeleteButtonVisible);
            }
        }

        public Position MapPosition
        {
            get
            {
                return _mapPosition;
            }

            set
            {
                _mapPosition = value;
                RaisePropertyChanged(() => MapPosition);
            }
        }

        public Position CurrentPosition
        {
            get
            {
                return _currentPosition;
            }

            set
            {
                _currentPosition = value;
                RaisePropertyChanged(() => CurrentPosition);
            }
        }

        public string MapAddress
        {
            get
            {
                return _mapAddress;
            }

            set
            {
                _mapAddress = value;
                RaisePropertyChanged(() => MapAddress);
            }
        }

        public string Distance
        {
            get
            {
                return _distance;
            }

            set
            {
                _distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }

        public ICommand DeleteJobCommand => new Command(async () => await DeleteJob());

        public ICommand ImageZoomCommand => new Command(async () => await ImageZoom());

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var mustGoBack = true;

            ImageUrl = null;

            IsBusy = true;

            if (navigationContext is int jobId)
            {
                try
                {
                    await Task.Delay(1000);

                    var job = await _apiClient.GetJobAsync(jobId);

                    var galleryItem = Mapper.Map<Job, GalleryItem>(job);

                    _id = job.Id;
                    Title = job.Title;
                    Description = job.Description;
                    FullName = !string.IsNullOrEmpty(job.User?.FullName) ? $"{job.User?.FullName}:" : null;
                    Contract = galleryItem.Contract;
                    Workday = galleryItem.Workday;
                    Category = galleryItem.Category;
                    PostedDate = job.PostedDate;
                    ImageUrl = job.ImageUrl;
                    MapAddress = job.Location.Replace("\r\n", ", ");
                    MapPosition = new Position(job.Latitude, job.Longitude);

                    mustGoBack = false;

                    IsBusy = false;

                    IsDeleteButtonVisible = _settingsService.GetValue(GlobalSettings.LogedUserNameKey, string.Empty) == job.User?.Email;

                    await GetDistance();
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

            if (mustGoBack)
            {
                await _navigationService.NavigateBackAsync();
            }
        }

        private async Task DeleteJob()
        {
            if (!IsBusy)
            {
                try
                {
                    if (await _userDialogs.ConfirmAsync(_localizationService.GetString("ItemDetailPage_RemoveJob_Confirmation_Message")))
                    {
                        await _apiClient.DeleteJobAsync(_id);

                        await _navigationService.NavigateBackAsync();
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
        }

        private async Task ImageZoom()
        {
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                await _navigationService.NavigateToAsync<ImageDetailViewModel>(ImageUrl);
            }
        }

        private async Task GetDistance()
        {
            try
            {
                var canGetLocation = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
                if (!canGetLocation)
                {
                    throw new Exception();
                }

                var locator = CrossGeolocator.Current;

                Plugin.Geolocator.Abstractions.Position geoPosition = null;

                geoPosition = await locator.GetLastKnownLocationAsync();

                if (geoPosition == null)
                {
                    geoPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(60));
                    if (geoPosition == null)
                    {
                        throw new Exception();
                    }
                }

                CurrentPosition = new Position(geoPosition.Latitude, geoPosition.Longitude);

                var distance = MapPosition.DistanceTo(CurrentPosition, UnitOfLength.Kilometers);
                if (distance < 1)
                {
                    distance = distance * 100;
                    var distanceMetersText = _localizationService.GetString("ItemDetailPage_Distance_Meters_Text");
                    Distance = string.Format(distanceMetersText, distance);
                }
                else
                {
                    var distanceKilometersText = _localizationService.GetString("ItemDetailPage_Distance_Kilometers_Text");
                    Distance = string.Format(distanceKilometersText, distance);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("ItemDetailPage_LocationError_Exception_Message"));
#endif
            }
        }
    }
}