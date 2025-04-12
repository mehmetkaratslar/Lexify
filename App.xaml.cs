using System.Configuration;
using System.Data;
using System.Windows;
using Lexify.Services;

using Lexify.Services;
using System.Windows;

namespace Lexify
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // DatabaseService'i başlat - ve
            // DatabaseService'i başlat - veritabanı oluşturma ve başlatma işlemlerini gerçekleştirir
            var dbService = new DatabaseService();

            // Ana pencereyi oluştur ve göster
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}