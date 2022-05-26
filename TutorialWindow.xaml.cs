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
    public partial class TutorialWindow : Window
    {
        public TutorialWindow()
        {   
            InitializeComponent();
            StatusBarControls.SetValues(this);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            StatusBarControls.Exit();
        }

        private void Cross_Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusBarControls.Exit();
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

        private void Figures_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Figures_Button.IsDefault)
            {
                Pawn_Button.Visibility = Visibility.Visible;
                Rook_Button.Visibility = Visibility.Visible;
                Knight_Button.Visibility = Visibility.Visible;
                King_Button.Visibility = Visibility.Visible;
                Queen_Button.Visibility = Visibility.Visible;
                Bishop_Button.Visibility = Visibility.Visible;
                Figures_Button.IsDefault = false;
            }

            else
            {
                Pawn_Button.Visibility = Visibility.Collapsed;
                Rook_Button.Visibility = Visibility.Collapsed;
                Knight_Button.Visibility = Visibility.Collapsed;
                King_Button.Visibility = Visibility.Collapsed;
                Queen_Button.Visibility = Visibility.Collapsed;
                Bishop_Button.Visibility = Visibility.Collapsed;
                Figures_Button.IsDefault = true;
            }

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            Close();
        }

        public static Frame LastFrame = null;

        private void Pawn_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Pawn_Board.Visibility = Visibility.Visible;
            LastFrame = Pawn_Board;
        }

        private void Knight_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Knight_Board.Visibility = Visibility.Visible;
            LastFrame = Knight_Board;
        }

        private void Bishop_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Bishop_Board.Visibility = Visibility.Visible;
            LastFrame = Bishop_Board;
        }

        private void King_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            King_Board.Visibility = Visibility.Visible;
            LastFrame = King_Board;
        }

        private void Queen_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Queen_Board.Visibility = Visibility.Visible;
            LastFrame = Queen_Board;
        }

        private void Rook_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Rook_Board.Visibility = Visibility.Visible;
            LastFrame = Rook_Board;
        }
        /*
        private void Italian_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Italian_Board.Visibility = Visibility.Visible;
            LastFrame = Rook_Board;
        }
        */
        private void Game_Basics_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Game_Basics_Button.IsDefault)
            {
                Check_Button.Visibility = Visibility.Visible;
                Check_And_Mate_Button.Visibility = Visibility.Visible;
                Game_Basics_Button.IsDefault = false;
            }
            else
            {
                Check_Button.Visibility = Visibility.Collapsed;
                Check_And_Mate_Button.Visibility = Visibility.Collapsed;
                Game_Basics_Button.IsDefault = true;
            }
        }
        /*
        private void Debut_Basics_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Debut_Basics_Button.IsDefault)
            {
                Italian_Button.Visibility = Visibility.Visible;
                Debut_Basics_Button.IsDefault = false;
            }

            else
            {
                Italian_Button.Visibility = Visibility.Collapsed;
                Debut_Basics_Button.IsDefault = true;
            }
        }
        */
        private void Check_And_Mate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Checkmate_Board.Visibility = Visibility.Visible;
            LastFrame = Checkmate_Board;
        }

        private void Check_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LastFrame != null) { LastFrame.Visibility = Visibility.Collapsed; LastFrame.NavigationService.Refresh(); }
            Check_Board.Visibility = Visibility.Visible;
            LastFrame = Check_Board;
        }
    }
}
