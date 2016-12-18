using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Catalog_of_recipes.Annotations;
using Microsoft.Win32;

namespace Catalog_of_recipes
{
    internal class ShowRecipesVm:ViewModelBase
    {
        #region Constructor
        public ShowRecipesVm()
        {
            Recipes = new ObservableCollection<Item>(new Data_manage().Load_rec());
            Temp = new ObservableCollection<Item>(new Data_manage().Load_rec());
            Items = new List<string> { "Название", "Калории", "Белки", "Жиры", "Углеводы" };
           
        }
        #endregion

        #region Fields
        private string _searchquery;
        private double value;
        private int _index;
        
        #endregion

        #region Properties

        public List<string> Items { get; set; }
       

        public int Index { get { return _index; } set { Set(ref _index, value); } }

        public string SearchQuery
        {
            get { return _searchquery; }
            set
            {
                Set(ref _searchquery, value);
                if (SearchQuery != "")
                    Search();
                else
                {
                   Recipes.Clear();
                    foreach (var i in Temp)
                    {
                        Recipes.Add(i);
                    }
                }
            }
        }
        #endregion

        #region Methods
        private bool MyComparer(Item item, string searchString, bool isNum)
        {
            if (isNum)
                value = double.Parse(searchString.Replace(".", ","));

            switch (Index)
            {
                case 0: return item.Name.ToLower().Contains(searchString);
                case 1:
                    return Math.Abs(value - item.Cl) < 1;
                case 2:
                    return Math.Abs(value - item.Pr) < 1;
                case 3:
                    return Math.Abs(value - item.Fat) < 1;
                case 4:
                    return Math.Abs(value - item.Ch) < 1;
            }
            throw new ArgumentException();
        }

        void Search()
        {
            if (Recipes == null)
                return;
            double res = 0;
            bool isNum = Double.TryParse(SearchQuery, out res);
            if (isNum == false && Index != 0)
            {
                Recipes.Clear();
                return;
            }
            Recipes.Clear();
            var temp = (Temp.Where(x => MyComparer(x, SearchQuery.ToLower(), isNum)));
            foreach (var i in temp)
            {
                Recipes.Add(i);
            }
            
        }

        private void Change(object parameter)
        {
            IList list = parameter as IList;
            List<Item> Selected = list.Cast<Item>().ToList();
            Temp = new ObservableCollection<Item>(Temp.Except(Selected));
            foreach (var i in Selected)
            {
                Recipes.Remove(i);
            }
        }
        #endregion

        #region Command

        public ICommand DeleteRecipes
        {
            get { return new CommandBase(Change); }
        }

        #endregion

    }

   
}


