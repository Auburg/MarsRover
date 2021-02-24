using MarsRover.Core;
using MarsRover.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MarsRover.Module
{
    public class MarsRoverModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MarsRoverModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.RoverContentRegion, typeof(ViewRover));
            _regionManager.RegisterViewWithRegion(RegionNames.PhotosContentRegion, typeof(ViewPhotos));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewRoverPhotoMetadata>();
        }
    }
}