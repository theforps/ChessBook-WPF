using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace RotTsar
{
    internal class StatusBarControls
    {
        public static bool isMax = false, isFullScreen = false;
        static Point old_location, default_location;
        static Size old_size, default_size;

        public static void SetValues(Window window)
        {
            old_size = new Size(window.Width, window.Height);
            old_location = new Point(window.Top, window.Left);

            default_size = new Size(window.Width, window.Height);
            default_location = new Point(window.Top, window.Left);
        }

        public static void DoMaximize(Window window)
        {
            if (!isMax)
            {
                old_size = new Size(window.Width, window.Height);
                old_location = new Point(window.Top, window.Left);
                Maximize(window);
                isMax = true; isFullScreen = false;
            }
            else
            {
                if (old_size.Width >= SystemParameters.WorkArea.Width || old_size.Height >= SystemParameters.WorkArea.Height)
                {
                    window.Top = default_location.Y;
                    window.Left = default_location.X;

                    window.Width = default_size.Width;
                    window.Height = default_size.Height;
                }

                else
                {
                    window.Top = old_location.Y;
                    window.Left = old_location.X;

                    window.Width = old_size.Width;
                    window.Height = old_size.Height;
                }
                
                isMax = false; isFullScreen = false;
            }
        }

        public static void DoFullScreen(Window window)
        {
            if (!isMax)
            {
                old_size = new Size(window.Width, window.Height);
                old_location = new Point(window.Top, window.Left);
                FullScreen(window);
                isMax = false; isFullScreen = true;
            }
            else
            {
                if (old_size.Width >= SystemParameters.WorkArea.Width || old_size.Height >= SystemParameters.WorkArea.Height)
                {
                    window.Top = default_location.Y;
                    window.Left = default_location.X;

                    window.Width = default_size.Width;
                    window.Height = default_size.Height;
                }

                else
                {
                    window.Top = old_location.Y;
                    window.Left = old_location.X;

                    window.Width = old_size.Width;
                    window.Height = old_size.Height;
                }

                isMax = false; isFullScreen = false;
            }
        }

        static void FullScreen(Window window)
        {
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else { window.WindowState = WindowState.Normal; }
        }

        static void Maximize(Window window)
        {
            double x = SystemParameters.WorkArea.Width;
            double y = SystemParameters.WorkArea.Height;
            window.WindowState = WindowState.Normal;

            window.Top = 0;
            window.Left = 0;

            window.Width = x;
            window.Height = y;
        }

        public static void Minimize(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public static void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
