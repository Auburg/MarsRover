using Prism.Mvvm;

namespace MarsRover.Module.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
    }
}
