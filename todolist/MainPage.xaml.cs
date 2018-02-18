using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace todolist
{
    /// <summary>
    /// Page principal de l application c est l accueil
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Constructeur demarrant la connexion SQLite et creant la table Task
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            string DbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            App Data = App.Current as App;
            Data.MyDb = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DbPath);
            Data.MyDb.CreateTable<Task>();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Bienvenue sur to do list!");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        /// <summary>
        /// bouton de navigation vers le reste de l appli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ToDo));
        }
    }
}
