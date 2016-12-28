using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Catalog_of_recipes
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static BinaryFormatter formatter = new BinaryFormatter();

        public static ObservableCollection<string> Search { get; set; } 
        public static ObservableCollection<Ingredient> Ingredients { get; set; } 
        public static ObservableCollection<Recipe> Recipes { get; set; }
        protected static List<Recipe> Temp { get; set; }


        public static void Save()
        {
            using (FileStream fs = new FileStream("Recipes.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Temp);
            }
        }

        public static void Save_ingrs()
        {
            using (FileStream fs = new FileStream("Ingredients.dat", FileMode.OpenOrCreate))
            {
                List<Ingredient> a = Ingredients.ToList();
                formatter.Serialize(fs, a);
            }
        }

        public static void Load_ingrs()
        {
            using (FileStream fs = new FileStream("Ingredients.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    var data = (List<Ingredient>)formatter.Deserialize(fs);
                    Ingredients = new ObservableCollection<Ingredient>(data);
                    Search = new ObservableCollection<string>(Ingredients.Select(x => x.Name).ToList());
                }
                catch
                {
                    Ingredients = new ObservableCollection<Ingredient>();
                }
            }
        }

        protected static void Load()
        {
            using (FileStream fs = new FileStream("Recipes.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    var data = (List<Recipe>)formatter.Deserialize(fs);
                    Recipes = new ObservableCollection<Recipe>(data);
                    Temp = data;

                }
                catch
                {
                    Recipes = new ObservableCollection<Recipe>();
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
