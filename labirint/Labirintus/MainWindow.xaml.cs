using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Labirintus
{
    public partial class MainWindow : Window
    {
      
        DispatcherTimer keyTimer { get; set; }
        GameManager gm;
        ProgramSetting setting;
        public MainWindow()
        {
            InitializeComponent();
            #region animation
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = LabelText.ActualWidth;
            doubleAnimation.To = 680;
            doubleAnimation.Duration = TimeSpan.FromSeconds(3.5);
            LabelText.BeginAnimation(Label.WidthProperty, doubleAnimation);


            #endregion

            gm = new GameManager(Environment.CurrentDirectory);
            DataContext = gm;

            Loaded += delegate
            {
                //pauseButton.Focus ();
                gm.IsPaused = false;

                setting = new ProgramSetting(Environment.CurrentDirectory + "\\progsetting.xml");
                Left = setting.Left;
                Top = setting.Top;
                Width = setting.Width;
                Height = setting.Height;
            };

            Closed += delegate
              {
                  setting.Left = Left;
                  setting.Top = Top;
                  setting.Width = Width;
                  setting.Height = Height;
              };
        }
       
        Key currentKey = Key.None;
        private void keyEvent(object sender = null, EventArgs e = null)
        {
            gm.moveKeyEvent(currentKey, keyTimer.Interval.Milliseconds);
        }
        private void Window_KeyDown(Object sender, KeyEventArgs e) //события нажатия клавиш
        {
            if (e.IsRepeat)
                return;

            switch (e.Key)
            {
                case Key.Up:
                case Key.Right:
                case Key.Down:
                case Key.Left:
                    keyTimer?.Stop();
                    currentKey = e.Key;
                    // скорость движения в мс. Здесь 200. Меньше-быстрее
                    keyTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(200), DispatcherPriority.Background, keyEvent, Dispatcher);
                    keyEvent();
                    break;
            }
            gm.keyPress(e.Key);
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            currentKey = Key.None;
            keyTimer?.Stop();
        }
        private void pauseButtonClick(object sender, RoutedEventArgs e)
        {
            gm.IsPaused = false;
        }
       
    }

}





