using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using PM2E10179.Models;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using System.IO;

namespace PM2E10179.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class siteLocPage : ContentPage
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public sitios sitios { get; set; }

        public siteLocPage(){
            InitializeComponent();
        }

        public siteLocPage(double _latitude, double _longitude){
            InitializeComponent();
            latitude = _latitude;
            longitude = _longitude;
        }

        public siteLocPage(sitios sitio){
            InitializeComponent();
            sitios = sitio;
            latitude = Convert.ToDouble(sitios.latitud);
            longitude = Convert.ToDouble(sitios.longitud);
        }

        protected async override void OnAppearing(){
            base.OnAppearing();

           try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    var pin = new Pin()
                    {
                        Position = new Position(latitude, longitude),
                        Label = "Ubicacion actual"
                    };
                    mapa.Pins.Add(pin);
                    mapa.IsShowingUser = true;
                    mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMeters(100)));
                }
            }
            catch(Exception ex){
                await DisplayAlert("Aviso", "GPS o Internet desactivados", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e){
            MemoryStream imgMS = new MemoryStream(sitios.foto);
        }
    }
}