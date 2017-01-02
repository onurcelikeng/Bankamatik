using Bankamatik.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Phone.UI.Input;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Bankamatik.View
{
    public sealed partial class MapPage : Page
    {
        public static string bankName;
        public static string type;
        Geopoint myLocation;


        public MapPage()
        {
            this.InitializeComponent();
            header.Text = bankName;
            getMyLocation();
        }


        public async void getMyLocation()
        {
            try
            {
                map.MapElements.Clear();
                progress.IsIndeterminate = true;

                var gl = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                var location = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));
                myLocation = location.Coordinate.Point;

                var pin = new MapIcon()
                {
                    Location = location.Coordinate.Point,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icon/pin.png")),
                    NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 },
                };

                map.MapElements.Add(pin);
                await map.TrySetViewAsync(location.Coordinate.Point, 16, 0, 0, MapAnimationKind.Bow);

                ConnectApi(myLocation);

                progress.IsIndeterminate = false;
            }

            catch (Exception)
            {
                progress.IsIndeterminate = false;
                Message("Konum ayarlarınız kapalı.", "Bir hata oluştu :(");
            }
        }

        private async void ConnectApi(Geopoint g)
        {
            try
            {
                progress.IsIndeterminate = true;

                string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + g.Position.Latitude.ToString().Replace(",", ".") + "," + g.Position.Longitude.ToString().Replace(",", ".") + "&radius=10000&types=atm&name=" + type + "&key=AIzaSyDSuWOakAmCKEzeDfPq3fRfuISKu0nhjmU";
                HttpClient client = new HttpClient();
                var data = await client.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<ATM>(data);

                for (int i = 0; i < obj.results.Count; i++)
                {
                    if (obj.results[i] == null)
                    {
                        break;
                    }

                    PinToMap(obj.results[i].geometry, obj.results[i].name);
                }

                progress.IsIndeterminate = false;
            }

            catch (Exception)
            {
                progress.IsIndeterminate = false;

                if (NetworkInformation.GetInternetConnectionProfile() == null)
                {
                    Message("Herhangi bir ağ bağlantısı bulunamadı. Telefon ayarlarınızı kontrol edin ve yeniden deneyin.", "Bir hata oluştu :(");
                }

                else
                {
                    Message("Bağlantı zaman aşımına uğradı. Bağlantınızı kontrol edip tekrar deneyiniz.", "Bir hata oluştu :(");
                }
            }
        }

        private void PinToMap(Model.Geometry g, String name)
        {
            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = g.location.lat;
            pos.Longitude = g.location.lng;
            MapIcon icon = new MapIcon();

            icon.Location = new Geopoint(pos);
            icon.NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 };
            icon.Visible = true;
            icon.Title = name;
            icon.ZIndex = int.MaxValue;
            icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/MapPin/" + selectMapPin().ToString().Trim() + ".png", UriKind.RelativeOrAbsolute));

            map.MapElements.Add(icon);
        }

        private string selectMapPin()
        {
            if (bankName == "Garanti")
            {
                return "garanti";
            }

            if (bankName == "Yapı Kredi")
            {
                return "yapikredi";
            }

            if (bankName == "İş Bankası")
            {
                return "isbankasi";
            }

            if (bankName == "Vakıf Bank")
            {
                return "vakifbank";
            }

            if (bankName == "Akbank")
            {
                return "akbank";
            }

            if (bankName == "Ziraat Bankası")
            {
                return "ziraatbankasi";
            }

            if (bankName == "ING Bank")
            {
                return "ingbank";
            }

            if (bankName == "Halk Bank")
            {
                return "halkbank";
            }

            if (bankName == "TEB")
            {
                return "teb";
            }

            if (bankName == "Finans Bank")
            {
                return "finansbank";
            }

            return null;
        }

        public async void Message(string body, string head)
        {
            MessageDialog dialog = new MessageDialog(body, head);
            dialog.Commands.Add(new UICommand("Kapat"));
            await dialog.ShowAsync();
        }

        private void targetButton_Click(object sender, RoutedEventArgs e)
        {
            getMyLocation();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (myLocation != null)
            {
                ConnectApi(myLocation);
            }
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
