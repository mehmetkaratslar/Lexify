using System.Windows;
using Lexify.Services;
using Lexify.ViewModels;

namespace Lexify
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Veritabanı servisini başlat
            var dbService = new DatabaseService();

            // MainViewModel oluştur
            var mainViewModel = new MainViewModel();

            // Ana pencereyi başlat
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}
