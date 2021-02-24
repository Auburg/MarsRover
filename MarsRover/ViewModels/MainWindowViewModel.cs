using Prism.Mvvm;

namespace MarsRover.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Mars Rover Photo Viewer";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
