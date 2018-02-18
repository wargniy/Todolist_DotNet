using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net.Attributes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace todolist
{
    /// <summary>
    /// Class ToDo prenant en compte les fonctions principales de l application soit :
    /// - Creation de la grille de tache
    /// - Rafraichir la page
    /// - Supprimer une tache
    /// - Valider la tache ou non
    /// - Retour arriere
    /// </summary>
    public sealed partial class ToDo : Page
    {
        public Tasks MyTask;
        public ToDo()
        {
            this.InitializeComponent();
        }

        private void CheckButtonClick(object sender, RoutedEventArgs e)
        {
            App Data = App.Current as App;
            Task query = Data.MyDb.Query<Task>("SELECT * FROM Task WHERE Id = " + MyTask.Id).FirstOrDefault();
            query.Done = !query.Done;
            Data.MyDb.Update(query);
            Frame.Navigate(typeof(ToDo));
        }
        /// <summary>
        /// Gestion des evenements lors d'un clique sur une tache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_OnClick(object sender, PointerRoutedEventArgs e)
        {
            this.MyTask = (Tasks)sender;
            DelButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Visible;
            CheckBoxButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Bouton pour revenir a l accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        /// <summary>
        /// Bouton ajoutant la tache a la grille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTask));
        }
        /// <summary>
        /// Bouton supprimant la tache apres avoir clique dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelButtonClick(object sender, RoutedEventArgs e)
        {
            App Data = App.Current as App;
            Data.MyDb.Query<Task>("DELETE FROM Task WHERE Id = " + MyTask.Id);
            Frame.Navigate(typeof(ToDo));
        }
        /// <summary>
        /// Bouton pour editer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            App Data = App.Current as App;
            Data.MyData = Data.MyDb.Query<Task>("SELECT * FROM Task WHERE Id = " + MyTask.Id).FirstOrDefault();
            Frame.Navigate(typeof(Edit));
        }
        /// <summary>
        /// Rafraichit la grille de tache dans une boucle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTask(object sender, RoutedEventArgs e)
        {
            App Data = App.Current as App;
            SQLite.Net.TableQuery<Task> TaskToDo = Data.MyDb.Table<Task>();
            foreach (var OneTask in TaskToDo)
            {
                Grid AllTask;
                TextBlock Done;
                TextBlock Date;
                MenuToDoList.Children.Add(AllTask = new Tasks
                {
                    Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue),
                    Margin = new Thickness(50, 50, 0, 0),
                    Height = 300,
                    Width = 200,
                    Id = OneTask.Id
                });
                AllTask.Children.Add(new TextBlock
                {
                    Text = OneTask.Title,
                    Foreground = new SolidColorBrush(Windows.UI.Colors.Black),
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.ExtraBlack,
                    TextWrapping = TextWrapping.Wrap,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    MaxHeight = 50,
                    Margin = new Thickness(0, 0, 0, 75)
                });
                AllTask.Children.Add(new TextBlock
                {
                    Text = OneTask.DueTime,
                    Foreground = new SolidColorBrush(Windows.UI.Colors.Purple),
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.ExtraBlack,
                    TextWrapping = TextWrapping.Wrap,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    MaxHeight = 50,
                    Margin = new Thickness(0, 0, 0, 20)
                });
                AllTask.Children.Add(Done = new TextBlock
                {
                    Foreground = new SolidColorBrush(Windows.UI.Colors.DarkBlue),
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 75),
                    Visibility = Visibility.Collapsed
                });
                if (OneTask.Done == true)
                {
                    AllTask.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                }
                AllTask.PointerPressed += new PointerEventHandler(grid_OnClick);
            }
        }
    }
}
