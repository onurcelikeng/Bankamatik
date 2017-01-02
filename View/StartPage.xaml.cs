using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Bankamatik.View
{
    public sealed partial class StartPage : Page
    {


        public StartPage()
        {
            this.InitializeComponent();
        }


        private void grid1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Garanti";
            MapPage.type = "garanti";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Yapı Kredi";
            MapPage.type = "yapıkredi";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "İş Bankası";
            MapPage.type = "işbankası";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Vakıf Bank";
            MapPage.type = "vakıfbank";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Akbank";
            MapPage.type = "akbank";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Ziraat Bankası";
            MapPage.type = "ziraatbankası";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid7_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "ING Bank";
            MapPage.type = "ingbank";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Halk Bank";
            MapPage.type = "halkbank";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid9_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "TEB";
            MapPage.type = "teb";
            Frame.Navigate(typeof(View.MapPage));
        }

        private void grid10_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapPage.bankName = "Finans Bank";
            MapPage.type = "finansbank";
            Frame.Navigate(typeof(View.MapPage));
        }

        #region BackRequested Handlers

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }

            else
            {
                Application.Current.Exit();
            }
        }

        #endregion
    }
}
