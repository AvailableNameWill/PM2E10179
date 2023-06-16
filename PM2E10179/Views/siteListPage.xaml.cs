using PM2E10179.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10179.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class siteListPage : ContentPage
    {
        public int selectedId { get; set; }
        public sitios sitio;

        public siteListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing(){
            base.OnAppearing();
            sitesList.ItemsSource = await App.Instancia.GetAll();
        }

        private void sitesList_SelectionChanged(object sender, SelectionChangedEventArgs e){
            selectedId = (int)(e.CurrentSelection.FirstOrDefault() as sitios)?.id;
            sitio = App.Instancia.getById(selectedId).Result;
        }

        private async void btnDel_Clicked(object sender, EventArgs e){
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (selectedId != 0)
            {
                if (await App.Instancia.deleteSite(sitio) > 0)
                {
                    await DisplayAlert("Aviso", "Sitio eliminado satisfactoriamente", "OK");
                    selectedId = 0;
                    sitio = null;
                    sitesList.ItemsSource = await App.Instancia.GetAll();
                }
                else await DisplayAlert("Aviso", "Ha ocurrido un error", "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e){
            if (selectedId != 0){
                await Navigation.PushAsync(new Views.siteLocPage(sitio));
            }
        }
    }
}