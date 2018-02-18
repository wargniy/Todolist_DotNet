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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace todolist
{
    /// <summary>
    /// Edit une tache deja creee
    /// </summary>
    public sealed partial class Edit : Page
    {
        /// <summary>
        /// Constructeur recuperant le titre et la description de la tache existante
        /// </summary>
        public Edit()
        {
            this.InitializeComponent();
            App Data = App.Current as App;
            TitleBlockContent.Text = Data.MyData.Title;
            DescBlockContent.Text = Data.MyData.Desc;
            
        }
        /// <summary>
        /// Clique de retour a la frame precedente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ToDo));
        }

        /// <summary>
        /// Bouton edit confirmant la modification de la tache actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            string DateFormat;

            DateFormat = datePicker.Date.ToString("d") + "  at " + timePicker.Time.ToString("t");
            DateFormat = DateFormat.Substring(0, DateFormat.Length - 3);
            App Data = App.Current as App;
            if (string.IsNullOrWhiteSpace(TitleBlockContent.Text))
                TitleBlockContent.Text = "EMPTY";

            
            Data.MyData.Title = TitleBlockContent.Text;
            Data.MyData.Desc = DescBlockContent.Text;
            Data.MyData.DueTime = DateFormat;
            Data.MyDb.Update(Data.MyData);
            Frame.Navigate(typeof(ToDo));
        }
    }
}
