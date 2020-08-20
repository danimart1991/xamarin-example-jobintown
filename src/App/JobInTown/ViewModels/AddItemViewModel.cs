using System;
using System.Collections.Generic;
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
using JobInTown.Models.Enums;
using Localization.Contracts;
using Models.Mvvm;
using Navigation.Contracts;
using Network.Models.Exceptions.Basic;
using Plugin.FilePicker.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace JobInTown.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IFilePickerService _filePickerService;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IApiClient _apiClient;
        private readonly IUserDialogs _userDialogs;
        private readonly ILocalizationService _localizationService;
        private readonly Geocoder _geocoder;

        private string _title;
        private string _description;
        private List<ContractType> _contracts;
        private ContractType _selectedContract;
        private List<WorkdayType> _workdays;
        private WorkdayType _selectedWorkday;
        private List<CategoryType> _categories;
        private CategoryType _selectedCategory;
        private ImageSource _image;
        private MemoryStream _imageStream;
        private string _location;
        private string _currentLocation;
        private Position _position;
        private FileData _photoFile;

        public AddItemViewModel(
            INavigationService navigationService,
            IFilePickerService filePickerService,
            IAzureStorageService azureStorageService,
            IApiClient apiClient,
            IUserDialogs userDialogs,
            ILocalizationService localizationService)
        {
            _navigationService = navigationService;
            _filePickerService = filePickerService;
            _azureStorageService = azureStorageService;
            _apiClient = apiClient;
            _userDialogs = userDialogs;
            _filePickerService = filePickerService;
            _localizationService = localizationService;

            Categories = Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>().ToList();
            Workdays = Enum.GetValues(typeof(WorkdayType)).Cast<WorkdayType>().ToList();
            Contracts = Enum.GetValues(typeof(ContractType)).Cast<ContractType>().ToList();

            _geocoder = new Geocoder();
            _photoFile = null;
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

        public List<ContractType> Contracts
        {
            get
            {
                return _contracts;
            }

            set
            {
                _contracts = value;
                RaisePropertyChanged(() => Contracts);
            }
        }

        public ContractType SelectedContract
        {
            get
            {
                return _selectedContract;
            }

            set
            {
                _selectedContract = value;
                RaisePropertyChanged(() => SelectedContract);
            }
        }

        public List<WorkdayType> Workdays
        {
            get
            {
                return _workdays;
            }

            set
            {
                _workdays = value;
                RaisePropertyChanged(() => Workdays);
            }
        }

        public WorkdayType SelectedWorkday
        {
            get
            {
                return _selectedWorkday;
            }

            set
            {
                _selectedWorkday = value;
                RaisePropertyChanged(() => SelectedWorkday);
            }
        }

        public List<CategoryType> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }

        public CategoryType SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public ImageSource Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                RaisePropertyChanged(() => Location);
            }
        }

        public ICommand AddItemCommand => new Command(async () => await AddItem());

        public ICommand GetCurrentLocationCommand => new Command(async () => await GetCurrentLocation());

        public ICommand ImageChangeCommand => new Command(async () => await ImageSwitch());

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if (_photoFile == null)
            {
                if (navigationContext is Job job)
                {
                    var galleryItem = Mapper.Map<Job, GalleryItem>(job);

                    Title = galleryItem.Title;
                    Description = galleryItem.Description;
                    SelectedCategory = galleryItem.Category;
                    SelectedWorkday = galleryItem.Workday;
                    SelectedContract = galleryItem.Contract;
                    Location = job.Location;
                    Image = string.IsNullOrEmpty(job?.ImageUrl) ? null : ImageSource.FromUri(new Uri(job.ImageUrl));
                    _position = new Position(job.Latitude, job.Longitude);
                }
            }
        }

        private async Task AddItem()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                if (string.IsNullOrEmpty(Location))
                {
                    throw new InvalidDataException(_localizationService.GetString("AddItemPage_NoTitle_Exception_Message"));
                }

                if (string.IsNullOrEmpty(Description))
                {
                    throw new InvalidDataException(_localizationService.GetString("AddItemPage_NoDescription_Exception_Message"));
                }

                var galleryItem = new GalleryItem()
                {
                    Title = Title,
                    Description = Description.Replace("\r", "\r\n"),
                    Category = SelectedCategory,
                    Workday = SelectedWorkday,
                    Contract = SelectedContract
                };

                var job = Mapper.Map<GalleryItem, Job>(galleryItem);

                try
                {
                    if (string.IsNullOrEmpty(Location))
                    {
                        throw new InvalidDataException(_localizationService.GetString("AddItemPage_NoLocation_Exception_Message"));
                    }
                    else if (Location != _currentLocation)
                    {
                        await UpdatePosition();
                    }

                    job.Location = Location;
                    job.Latitude = _position.Latitude;
                    job.Longitude = _position.Longitude;

                    if (_imageStream != null)
                    {
                        var imageUrl = await _azureStorageService.PushFile(_imageStream);
                        job.ImageUrl = imageUrl;
                    }

                    var jobResult = await _apiClient.PostJobAsync(job);

                    _photoFile = null;

                    await _navigationService.NavigateBackAsync();
                    await _navigationService.NavigateToAsync<ItemDetailViewModel>(jobResult.Id);
                }
                catch (InvalidDataException ex)
                {
                    await _userDialogs.AlertAsync(ex?.Message);
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
                    IsBusy = false;
                }
            }
        }

        private async Task AddImage()
        {
            if (!IsBusy)
            {
                try
                {
                    _photoFile = await _filePickerService.PickImage();

                    IsBusy = true;

                    _imageStream = new MemoryStream(_photoFile.DataArray);
                    Image = ImageSource.FromStream(() => new MemoryStream(_photoFile.DataArray));
                }
                catch (ArgumentException)
                {
                    await _userDialogs.AlertAsync(_localizationService.GetString("AddItemPage_InvalidImage_Exception_Message"));
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
                    IsBusy = false;
                }
            }
        }

        private async Task GetCurrentLocation()
        {
            try
            {
                var canGetLocation = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
                if (!canGetLocation)
                {
                    throw new Exception();
                }

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                Plugin.Geolocator.Abstractions.Position geoPosition = null;

                geoPosition = await locator.GetLastKnownLocationAsync();

                if (geoPosition == null)
                {
                    geoPosition = await locator.GetPositionAsync(TimeSpan.FromMinutes(2));
                    if (geoPosition == null)
                    {
                        throw new Exception();
                    }
                }

                _position = new Position(geoPosition.Latitude, geoPosition.Longitude);

                var addresses = await _geocoder.GetAddressesForPositionAsync(_position);
                if (addresses != null && addresses.Any())
                {
                    Location = _currentLocation = addresses.FirstOrDefault().Replace("\r\n", ", ");
                }
                else
                {
                    throw new Exception(_localizationService.GetString("AddItemPage_AddressError_Exception_Message"));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("AddItemPage_LocationError_Exception_Message"));
#endif
            }
        }

        private async Task UpdatePosition()
        {
            var positions = await _geocoder.GetPositionsForAddressAsync(Location);
            if (positions != null && positions.Any())
            {
                _position = positions.FirstOrDefault();
            }
            else
            {
                throw new InvalidDataException(_localizationService.GetString("AddItemPage_InvalidAdress_Exception_Message"));
            }
        }

        private async Task ImageSwitch()
        {
            if (Image != null)
            {
                if (await _userDialogs.ConfirmAsync(_localizationService.GetString("AddItemPage_RemoveImage_Confirmation_Message")))
                {
                    Image = null;
                }
            }
            else
            {
                await AddImage();
            }
        }
    }
}