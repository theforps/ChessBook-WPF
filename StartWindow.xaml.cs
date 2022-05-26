using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RotTsar
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            StatusBarControls.SetValues(this);
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Cross_Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarControls.Exit();
        }

        private void Tutorial_Button_Click(object sender, RoutedEventArgs e)
        {
            TutorialWindow tutorialWindow = new TutorialWindow();
            StartWindow start = new StartWindow();
            tutorialWindow.Show();
            
            Close();
        }

        private void Restore_Down_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarControls.DoMaximize(this);
        }

        private void Minimise_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarControls.Minimize(this);
        }

        private void Fullscreen_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarControls.DoFullScreen(this);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
