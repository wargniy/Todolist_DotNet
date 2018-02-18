using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.IO;
using Windows.UI.Xaml.Navigation;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Globalization;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace todolist
{
    /// <summary>
    /// Class pour ajouter une tache et sauvegarde le tout dans une base de donnees
    /// </summary>
    public sealed partial class AddTask : Page
    {
        private string DbPath;
        /// <summary>
        /// Ajoute une tache a la base de donnees db.sqlite
        /// </summary>
        public AddTask()
        {
            this.InitializeComponent();
            DbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ToDo));
        }

        /// <summary>
        ///  creer une tache en affichant la date et l heure sous la forme DD/MM/YYYY HH/MM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButtonClick(object sender, RoutedEventArgs e)
        {
            string DateFormat;

            DateFormat = datePicker.Date.ToString("d") + "  at " + timePicker.Time.ToString("t");
            DateFormat = DateFormat.Substring(0, DateFormat.Length - 3);
            App Data = App.Current as App;
            if (string.IsNullOrWhiteSpace(descBox.Text))
                descBox.Text = "EMPTY";

            Data.MyDb.Insert(new Task() { Title = titleBox.Text, Desc = descBox.Text, DueTime = DateFormat, Done = false });

            Frame.Navigate(typeof(ToDo));
        }
    }
}

