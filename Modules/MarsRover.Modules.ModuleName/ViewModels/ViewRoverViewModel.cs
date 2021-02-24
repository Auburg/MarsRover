using MarsRover.Core;
using MarsRover.Core.Models;
using MarsRover.Services.Interfaces;
using Prism.Commands;
using Prism.Regions;

namespace MarsRover.Module.ViewModels
{
    public class ViewRoverViewModel : ViewModelBase
    {    
        private string imageUrl;
        private string _selectedName;
        private readonly IMarsRoverApi marsRoverApi;
        private RoverManifest roverManifest;  
        private readonly IRegionManager regionManager;

        public ViewRoverViewModel(IRegionManager regionManager, IMarsRoverApi marsRoverApi) 
        {          
            RoverDataCommand = new DelegateCommand(GetRoverData);
            this.marsRoverApi = marsRoverApi;
            this.regionManager = regionManager;
        }

        private async void GetRoverData()
        {
            try
            {
                IsBusy = true;
                RoverManifest = await marsRoverApi.GetRoverManifestAsync(SelectedRover);

                var p = new NavigationParameters
                {
                    { Constants.RoverMetadataParamKey, RoverManifest.photoManifest }
                };
                regionManager.RequestNavigate(RegionNames.RoverMetadataContentRegion, "ViewRoverPhotoMetadata", p);
            }
            finally
            {
                IsBusy = false;
            }            
        }

        public DelegateCommand RoverDataCommand { get; private set; }

        public string SelectedImageUrl
        {
            get { return imageUrl; }
            set { SetProperty(ref imageUrl, value); }
        }
            
        public string SelectedRover
        {
            get { return _selectedName; }
            set { SetProperty(ref _selectedName, value); }
        }

        public RoverManifest RoverManifest
        {
            get { return roverManifest; }
            set { SetProperty(ref roverManifest, value); }
        }
    }
}
   

