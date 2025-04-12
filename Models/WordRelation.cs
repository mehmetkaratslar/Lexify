using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lexify.Services;
using System.Windows;

namespace Lexify
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // DatabaseService'i başlat - veritabanı oluşturma ve başlatma işlemlerini gerçekleştirir
            var dbService = new DatabaseService();

            // Ana pencereyi oluştur ve göster
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}