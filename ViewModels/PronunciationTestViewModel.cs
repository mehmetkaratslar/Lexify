using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexify.Helpers;
using Lexify.Services;

namespace Lexify.ViewModels
{
    public class PronunciationTestViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public PronunciationTestViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            // Ses dosyasını çal, doğru yazılışı seç testinin mantığı buraya yazılır
        }
    }
}
