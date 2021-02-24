using MarsRover.Module.Events;
using MarsRover.Module.UserControls;
using Prism.Events;
using System.Windows;
using System.Windows.Controls;

namespace MarsRover.Module.Views
{
    /// <summary>
    /// Interaction logic for ViewRoverPhotos.xaml
    /// </summary>
    public partial class ViewRoverPhotoMetadata : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public ViewRoverPhotoMetadata(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
        }

        private void NumericUpDown_ValueChanged(object sender, RoutedEventArgs e)
        {
            eventAggregator.GetEvent<SolSelectedChangedEvent>().Publish(((NumericUpDown)sender).NumericValue);
        }
    }
}
