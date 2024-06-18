using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Globalization;
using System.Threading;
using System.Collections;
using Application = System.Windows.Application;


namespace Notatnik_WPF
{
    class SetLanguage
    {
        private static ResourceDictionary dictionary = new ResourceDictionary();
        public static void SwitchDictionarySource(string switchCondition)
        {
            switch (switchCondition)
            {
                case "en-US":
                    dictionary.Source = new Uri("..\\View\\Languages\\StringResource.en-US.xaml",
                                  UriKind.Relative);
                    break;
                case "pl-PL":
                    dictionary.Source = new Uri("..\\View\\Languages\\StringResource.pl-PL.xaml",
                                       UriKind.Relative);
                    break;
                case "es-ES":
                    dictionary.Source = new Uri("..\\View\\Languages\\StringResource.es-ES.xaml",
                                                              UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\View\\Languages\\StringResource.en-US.xaml",
                                      UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        public static void SetLanguageDictionaryAtStart()
        {
            SwitchDictionarySource(Thread.CurrentThread.CurrentCulture.ToString());
        }

        public static void ChangeLanguageDictionary(string lang)
        {
            SwitchDictionarySource(lang);
        }
    }
}
