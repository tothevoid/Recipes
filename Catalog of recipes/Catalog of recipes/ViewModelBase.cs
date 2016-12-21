using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Catalog_of_recipes
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public static ObservableCollection<Item> Recipes { get; set; }
        //protected static List<Item> Temp { get; set; }

        public static ObservableCollection<Recipe> Recipes { get; set; }
        protected static List<Recipe> Temp { get; set; }

        private static BinaryFormatter formatter = new BinaryFormatter();

        public static void Save()
        {
            using (FileStream fs = new FileStream("Recipes.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Recipes);
            }
        }

        public static void Load()
        {
            using (FileStream fs = new FileStream("Recipes.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    Recipes = (ObservableCollection<Recipe>) formatter.Deserialize(fs);
                }
                catch
                {
                    Recipes = new ObservableCollection<Recipe>();
                }
            }
        }

        public static void Load_Temp()
        {
            using (FileStream fs = new FileStream("Recipes.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    Temp = ((ObservableCollection<Recipe>)formatter.Deserialize(fs)).ToList();
                }
                catch
                {
                    Temp = new List<Recipe>();
                }
            }
        }

        protected void Set<T>(ref T field, T value, [CallerMemberName] string propName = null)
        {
            if (field != null && !field.Equals(value) || value != null && !value.Equals(field))
            {
                field = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
        }
    }


}
