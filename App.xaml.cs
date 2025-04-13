using System.Windows;
using Lexify.Services;

namespace Lexify
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Veritabanı servisini başlat
            var dbService = new DatabaseService();

            // Ana pencereyi başlat
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
