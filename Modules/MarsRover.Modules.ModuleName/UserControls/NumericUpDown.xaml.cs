using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MarsRover.Module.UserControls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
            NUDTextBox.Text = StartValue.ToString();         
            MinValue = 0;
            StartValue = 0;
            NUDButtonUP.PreviewMouseLeftButtonUp += NUDButton_PreviewMouseLeftButtonUp;
            NUDButtonDown.PreviewMouseLeftButtonUp += NUDButton_PreviewMouseLeftButtonUp;
        }        

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
        "ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public int StartValue { get; set; }
        
        public int MinValue { get; set; }

        public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register(
        "MaxValue", typeof(int),
        typeof(NumericUpDown)
        );

        public static readonly DependencyProperty NumericValueProperty = DependencyProperty.Register("NumericValue", typeof(int),typeof(NumericUpDown));
    
        public int NumericValue
        {
            get { return (int)GetValue(NumericValueProperty); }
            set { SetValue(NumericValueProperty, value); }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            DoButtonAction(true);
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            DoButtonAction(false);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RaiseChangedEvent();
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
                if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = StartValue.ToString();
            if (number > MaxValue) NUDTextBox.Text = MaxValue.ToString();
            if (number < MinValue) NUDTextBox.Text = MinValue.ToString();
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;
            NumericValue = int.Parse(NUDTextBox.Text);
        }

        private void NUDTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                RaiseChangedEvent();
            }
        }

        void RaiseChangedEvent()
        {           
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ValueChangedEvent);            
            RaiseEvent(newEventArgs);
        }

        private void NUDTextBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                DoButtonAction(true);

            else if (e.Delta < 0)
                DoButtonAction(false);

            RaiseChangedEvent();
        }

        private void DoButtonAction(bool isUp)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;

            if(isUp)
            {
                if (number < MaxValue)
                    NUDTextBox.Text = Convert.ToString(number + 1);
            }
            else
            {
                if (number > MinValue)
                    NUDTextBox.Text = Convert.ToString(number - 1);
            }
        }
    }
}
