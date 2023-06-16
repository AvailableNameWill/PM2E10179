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
using Android.Locations;
using Android.Content;
using PM2E10179.Models;
using System.IO;
using System.Drawing;
using Android.Media;

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

        public siteLocPage(sitios _sitio){
            sitios = _sitio;
            latitude = Convert.ToDouble(sitios.latitud);
            longitude = Convert.ToDouble(sitios.longitud);
        }

        protected async override void OnAppearing(){
            base.OnAppearing();

            try
            {
                var location = await Geolocation.GetLocationAsync();
                var locl = CrossGeolocator.Current;
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
                else
                {
                    await DisplayAlert("Aviso","El GPS esta inactivo","OK");
                }
            }
            catch(Exception ex){
                await DisplayAlert("Aviso", "GPS inactivo", "OK");
            }
        }

        public bool IsLocationServiceEnabled(){
            LocationManager location = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocaleService);
            return location.IsProviderEnabled(LocationManager.GpsProvider);
        }

        private void Button_Clicked(object sender, EventArgs e){
            
        }
    }
}