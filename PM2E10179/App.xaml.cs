using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10179
{
    public partial class App : Application
    {
        static Controllers.db db;
        public static Controllers.db Instancia
        {
            get{
                if (db == null){
                    string dbName = "db.db3";
                    string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string dbFullP = Path.Combine(dbPath, dbName);
                    db = new Controllers.db(dbFullP);
                }

                return db;  
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
