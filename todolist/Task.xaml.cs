using System;
using SQLite.Net.Attributes;
using Windows.UI.Xaml.Controls;

namespace todolist
{
    /// <summary>
    /// Class Task comprenant les attributs Id, Titre, Description, Temps, Valide
    /// </summary>
    public class Task
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Title { get; set; }
            public string Desc { get; set; }
            public string DueTime { get; set; }
            public bool Done { get; set; }
    }

    public class Tasks : Grid
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string DueTime { get; set; }
        public bool Done { get; set; }
    }
}
