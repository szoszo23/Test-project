using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Histech_test.Model;

namespace Histech_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveAreaWidth = MoveCanvas.Width;
            viewModel.MoveAreaHeight = MoveCanvas.Height;
            viewModel.MoveAreaZHeight = CanvasZ.Height;
        }
        
        //Handling Mouse Left button click on the moving area
        private new void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(MoveCanvas);
            var viewModel = (ViewModel)this.DataContext;
            position.X /= viewModel.ScaleX;
            position.Y /= viewModel.ScaleY;
            bool? xCheck = XBox.IsChecked;
            bool? yCheck = YBox.IsChecked;
            bool? zCheck = ZBox.IsChecked;

            if (xCheck != null && (bool)xCheck && yCheck != null && (bool)yCheck && zCheck != null &&
                (bool)zCheck) // Move all
            {
                viewModel.SetTargetPosition(position.X, position.Y, viewModel.ObjectTable.EngineZ.Position);
            }
            else if (xCheck != null && !(bool)xCheck && yCheck != null && (bool)yCheck && zCheck != null &&
                     (bool)zCheck) // Move on Y and Z
            {
                viewModel.SetTargetPosition(position.Y, viewModel.ObjectTable.EngineZ.Position);
            }
            else if (xCheck != null && (bool)xCheck && yCheck != null && (bool)yCheck && zCheck != null &&
                     !(bool)zCheck) // Move on X and Y
            {
                viewModel.SetTargetPositionZlock(position.X,
                    position.Y);
            }
            else if (xCheck != null && (bool)xCheck && yCheck != null && !(bool)yCheck && zCheck != null &&
                     (bool)zCheck) // Move on X and Z
            {
                viewModel.SetTargetPositionYLock(position.X, viewModel.ObjectTable.EngineZ.Position);
            }
            else if ((xCheck != null && (bool)xCheck && yCheck != null && !(bool)yCheck && zCheck != null &&
                      !(bool)zCheck)) // Move on X
            {
                viewModel.SetTargetPosition(position.X);
            }
            else if ((xCheck != null && !(bool)xCheck && yCheck != null && (bool)yCheck && zCheck != null &&
                      !(bool)zCheck)) // Move on Y
            {
                viewModel.SetTargetPositionOnY(position.Y);
            } else if ((xCheck != null && !(bool)xCheck && yCheck != null && (bool)yCheck && zCheck != null &&
                        !(bool)zCheck)) // Move on Z
            {
                viewModel.SetTargetPositionOnZ(viewModel.ObjectTable.EngineZ.Position);
            }
        }
        
        //Engine speed increase and decrease 
        private void IncreaseXSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.IncreaseSpeedX();
        }
        private void DecreaseXSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.DecreaseSpeedX();
        }
        private void IncreaseYSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.IncreaseSpeedY();
        }
        private void DecreaseYSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.DecreaseSpeedY();
        }
        private void IncreaseZSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.IncreaseSpeedZ();
        }
        private void DecreaseZSpeed(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.DecreaseSpeedZ();
        }
        
        //Handling button on click movement
        private void MoveDownX(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveDownX();
        }

        private void MoveUpX(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveUpX();
        }

        private void MoveUpY(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveUpY();
        }

        private void MoveDownY(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveDownY();
        }
        
        private void MoveUpZ(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveUpZ();
        }
        
        private void MoveDownZ(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)this.DataContext;
            viewModel.MoveDownZ();
        }
    }
}