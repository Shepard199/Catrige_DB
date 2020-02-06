using System;
using System.Windows;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title += UserStatus;


            //MainFrameHeight = Width;
            //MainFrameWidth = Height;

            //GrFrame.Width = GrMain.Width;
            //GrFrame.Height = GrMain.Height;

            //MessageBox.Show("Current Page Size: " + Height + "/" + Width + " Main Frame Size: " +
            //                GrFrame.Width + "/" +
            //                GrFrame.Height);
        }

        public void BtAdd_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("ToRefill.xaml", UriKind.Relative));
        }

        public void BtAddNew_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("AddNewCatriges.xaml", UriKind.Relative));
        }

        public void BtDelete_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("DeleteCatriges.xaml", UriKind.Relative));
        }

        public void BtCheck_Click(object sender, EventArgs e)
        {
        }

        public void BtShowToFilling_Click(object sender, EventArgs e)
        {
            MaiPageFrame.NavigationService.Navigate(new Uri("ShowRefillLog.xaml", UriKind.Relative));
        }

        private void BtSettings_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize == new Size()) return;


            //MainFrameHeight = e.NewSize.Height;
            //MainFrameWidth = e.NewSize.Width;           

            //MainFrameHeight = GrFrame.Width;
            //MainFrameWidth = GrFrame.Height;

            //GrFrame.Width = GrMain.Width;
            //GrFrame.Height = GrMain.Height;

            ////MaiPageFrame.Width = e.NewSize.Height - 6;

            //double prevWindowHeight = e.PreviousSize.Height;
            //double prevWindowWidth = e.PreviousSize.Width;

            //MessageBox.Show(e.NewSize.Width + " / " + e.NewSize.Height);
            //MessageBox.Show("Grid Size is: " + GrMain.Width + " / " + GrMain.Height);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //double dWidth = -1;
            //double dHeight = -1;
            //if (Content is FrameworkElement pnlClient)
            //{
            //    dWidth = pnlClient.ActualWidth;
            //    dHeight = pnlClient.ActualHeight;

            //    MainWindowWidth = pnlClient.ActualWidth;
            //    MainWindowHeight = pnlClient.ActualHeight;
            //}

            ////MessageBox.Show("Windows Size1 is: " + dWidth + " / " + dHeight);
            //MessageBox.Show("Windows Size2 is: " + MainWindowWidth + " / " + MainWindowHeight);
            //MessageBox.Show("Toll Bar Size is: " + TbMainWindow.Width + " / " + TbMainWindow.Height);
            //MessageBox.Show("Grid Size is: " + GrMain.Width + " / " + GrMain.Height);
        }
    }
}