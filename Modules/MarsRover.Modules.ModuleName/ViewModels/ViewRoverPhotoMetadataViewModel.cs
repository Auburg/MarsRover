using MarsRover.Core.Models;
using MarsRover.Module.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace MarsRover.Module.ViewModels
{
    public class ViewRoverPhotoMetadataViewModel : BindableBase, INavigationAware
    {
        private PhotoManifest photoManifest;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private SolSelectedChangedEvent solSelectedChangedEvent;

        public ViewRoverPhotoMetadataViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            PhotoCommand = new DelegateCommand(GetPhoto, () => true);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }        

        private int _maxSol;
        public int MaxSol
        {
            get { return _maxSol; }
            set { SetProperty(ref _maxSol, value); }
        }

        private int _solValue;
        public int SolValue
        {
            get { return _solValue; }
            set { SetProperty(ref _solValue, value); }
        }
     
        public PhotoManifest PhotoManifest
        {
            get { return photoManifest; }
            set { SetProperty(ref photoManifest, value); }
        }

        private PhotoMetadata _selectedPhotoMetaData;
        public PhotoMetadata SelectedPhotoMetaData
        {
            get { return _selectedPhotoMetaData; }
            set { SetProperty(ref _selectedPhotoMetaData, value); }
        }

        private int selectedCameraIndex;
        public int SelectedCameraIndex
        {
            get { return selectedCameraIndex; }
            set { SetProperty(ref selectedCameraIndex, value); }
        }

        public DelegateCommand PhotoCommand { get; private set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
            SubscribeToSolChangedEvent();
            UpdateUi(navigationContext);
        }

        private void UpdateUi(NavigationContext navigationContext)
        {
            PhotoManifest = navigationContext.Parameters.GetValue<PhotoManifest>(Constants.RoverMetadataParamKey);
            MaxSol = PhotoManifest.max_sol;
            UpdateSelectedMetaData();
        }

        private void SubscribeToSolChangedEvent()
        {
            solSelectedChangedEvent = eventAggregator.GetEvent<SolSelectedChangedEvent>();
            solSelectedChangedEvent.Subscribe(OnSolChanged);
        }

        private void GetPhoto()
        {            
            eventAggregator.GetEvent<GetPhotosEvent>().Publish(new RetrievePhotosData
            {
                camera = SelectedPhotoMetaData.cameras[SelectedCameraIndex],
                name = PhotoManifest.name,
                sol = SolValue
            });
        }

        private void OnSolChanged(int solValue)
        {
            SolValue = solValue;
            UpdateSelectedMetaData();
        }

        private void UpdateSelectedMetaData()
        {
            if(SolValue>=0 && SolValue < PhotoManifest.photos.Length)
            {
                SelectedPhotoMetaData = PhotoManifest.photos[SolValue];
                SelectedCameraIndex = 0;
            }
        }
    }
}
