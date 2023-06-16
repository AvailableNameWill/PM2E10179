using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Plugin.Media;

namespace PM2E10179.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage{

        Plugin.Media.Abstractions.MediaFile photo = null;
        private double longitud = 0.0;
        private double latitude = 0.0;

        public MainPage(){
            InitializeComponent();
        }

        protected async override void OnAppearing(){
            base.OnAppearing();

            try{
                var conect = Connectivity.NetworkAccess;
                var locl = CrossGeolocator.Current;
                if (conect == NetworkAccess.Internet)
                {
                    if (locl != null)
                    {
                        var posicion = await locl.GetPositionAsync();
                        latitude = posicion.Latitude;
                        longitud = posicion.Longitude;
                        txtLat.Text = Convert.ToString(longitud);
                        txtLon.Text = Convert.ToString(latitude);
                    }
                }
                else
                {
                    txtLat.Text = "No hay conexion a internet";
                    txtLon.Text = "No hay conexion a internet";
                    latitude = 0.0;
                    longitud = 0.0;
                }
            }
            catch (Exception e){
                txtLat.Text = "El GPS esta desactivado";
                txtLon.Text = "El GPS esta desactivado";
                latitude = 0.0;
                longitud = 0.0;
            }
        }


        public byte[] getimgByte(){
            
            if (photo != null){
                using (MemoryStream memory = new MemoryStream()){
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotoByte = memory.ToArray();
                    return fotoByte;
                }
            }

            return null;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e){
            var sitio = new Models.sitios
            {
                foto        = getimgByte(),
                latitud     = Convert.ToString(latitude),
                longitud    = Convert.ToString(longitud),
                description = txtDesc.Text
            };

            if(sitio.foto == null || sitio.longitud.Equals("0.0") || sitio.latitud.Equals("0.0")
            || sitio.description.Equals("")){
                await DisplayAlert("Aviso", "No se permiten campos vacios!!", "OK");
            }else {
                if (await App.Instancia.addSite(sitio) > 0){
                    await DisplayAlert("Aviso", "Sitio Agregado correctamente", "OK");
                }
                else await DisplayAlert("Aviso", "A ocurrido un error", "OK");
            }
        }

        private async void btnList_Clicked(object sender, EventArgs e){
            await Navigation.PushAsync(new Views.siteListPage());
        }

        private void btnExit_Clicked(object sender, EventArgs e){
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void btnTakeF_Clicked(object sender, EventArgs e){
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Exam",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null){
                foto.Source = ImageSource.FromStream(()=> { return photo.GetStream(); });
            }
        }

    }
}