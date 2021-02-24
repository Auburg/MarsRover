using MarsRover.Core.Models;
using MarsRover.Module.Events;
using MarsRover.Services.Interfaces;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;

namespace MarsRover.Module.ViewModels
{
    public class ViewPhotosViewModel : ViewModelBase, INavigationAware
    {
        private string message;        
        private GetPhotosEvent getPhotosEvent;
        private readonly IMarsRoverApi marsRoverApi;
        private readonly string apiKey;

        public ViewPhotosViewModel(IEventAggregator eventAggregator, IMarsRoverApi marsRoverApi)
        {            
            getPhotosEvent = eventAggregator.GetEvent<GetPhotosEvent>();
            getPhotosEvent.Subscribe(OnGetPhotos);
            this.marsRoverApi = marsRoverApi;
        }
            
        private IList<Photo> photos;
        public IList<Photo> Photos
        {
            get { return photos; }
            set { SetProperty(ref photos, value); }
        }

        private void OnGetPhotos(RetrievePhotosData retrievePhoto)
        {
            GetRoverData(retrievePhoto);
        }

        public string Info
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private async void GetRoverData(RetrievePhotosData retrievePhoto)
        {
            try
            {
                IsBusy = true;
                var ret = await marsRoverApi.GetRoverPhotosAsync(retrievePhoto.name, retrievePhoto.sol, retrievePhoto.camera);
                Photos = ret.photos;
                Info = Photos.Count == 0 ? "No photos available" : GetMessageData();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private string GetMessageData()
        {
            return $"Camera: {Photos[0].camera.full_name}, Date: {Photos[0].earth_date}";
        }
    }
}
