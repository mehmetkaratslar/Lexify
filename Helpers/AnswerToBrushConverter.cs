using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Lexify.Models;

namespace Lexify.Helpers
{
    public class AnswerToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selected = value as string;
            var question = parameter as TestQuestion;

            if (question == null || string.IsNullOrEmpty(question.SelectedAnswer))
                return new SolidColorBrush(Color.FromRgb(94, 129, 172)); // Normal

            if (selected == question.CorrectAnswer)
                return new SolidColorBrush(Color.FromRgb(163, 190, 140)); // Yeşil

            if (selected == question.SelectedAnswer)
                return new SolidColorBrush(Color.FromRgb(191, 97, 106)); // Kırmızı

            return new SolidColorBrush(Color.FromRgb(94, 129, 172)); // Default
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
