using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catalog_of_recipes
{
    internal class ShowRecipesVm:ViewModelBase
    {
        #region Constructor
        public ShowRecipesVm()
        {
            Load();
        }
        #endregion

        #region Fields
        private string _searchQuery;
        private double _value;
        private int _index;
        private int _currentRecipe = -1;
        private string _currIngrs;
        private string _description;
        private BitmapImage _currImg;
        #endregion

        #region Properties
        public BitmapImage CurrImg { get { return _currImg; } set { Set(ref _currImg,value);} }
        public string CurrIngrs {get { return _currIngrs; } set {Set(ref _currIngrs, value);} }
        public string Description { get { return _description; } set {Set(ref _description,value); } }
        public List<string> Items { get;} = new List<string> { "Название", "Калории", "Белки", "Жиры", "Углеводы" };
        public int Index
        {
            get { return _index; }
            set
            {
                Set(ref _index, value);
                if (string.IsNullOrEmpty(SearchQuery)==false)
                Search(); 
            }
        }

        public int Current_recipe
        {
            get { return _currentRecipe; }
            set
            {
                Set(ref _currentRecipe,value);
                UpdateDetails();
            }
            
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                Set(ref _searchQuery, value);
                if (string.IsNullOrWhiteSpace(SearchQuery)==false)
                    Search();
                else
                {
                   Recipes.Clear();
                   foreach (var i in Temp)
                        Recipes.Add(i);
                }
            }
        }
        #endregion
        
        #region Methods

        void UpdateDetails()
        {
            if (Current_recipe<0)
                return;
            List<string> ingrs = Recipes[Current_recipe].Ingredients.Split('/').ToList();
            ingrs.RemoveAt(ingrs.Count - 1);
            StringBuilder convert = new StringBuilder();
            for (int i = -2; i < ingrs.Count-2; i+=2)
            {
                convert.Append(string.Format("{0} ({1} грамм); ", ingrs[i + 2], ingrs[i + 3]));
            }
            CurrIngrs = Convert.ToString(convert);
            Description = Recipes[Current_recipe].Description;
            GetImg(Recipes[Current_recipe].Name);
        }

        private void GetImg(string Name)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(Environment.CurrentDirectory + String.Format(@"\Images\{0}.png", Name));
                image.EndInit();
                CurrImg = image;
            }
            catch 
            {
                CurrImg = null;
            }
          
        }

        private bool MyComparer(Recipe rec, char fl)
        {
            if (SearchQuery.Length == 1 && (fl == '>' || fl == '<'))
                return false;
            switch (Index)
            {
                case 0: return rec.Name.ToLower().Contains(SearchQuery.ToLower());
                case 1:
                    return Compare(rec.Cl, _value, fl);
                case 2:
                    return Compare(rec.Pr, _value, fl);
                case 3:
                    return Compare(rec.Fat, _value, fl);
                case 4:
                    return Compare(rec.Ch, _value, fl);
            }
            throw new ArgumentException();
        }

        private bool Compare(double entered, double exist, char operand)
        {
            switch (operand)
            {
                case '>':
                    return entered > exist;
                case '<':
                    return entered < exist;
                default:
                    return Math.Abs(exist - entered) < 1;
            }
        }

        private void Search()
        {
            if (Recipes == null)
                return;
            double res = 0;
            bool isNum;
            char fl = SearchQuery.First();
            if (fl == '>' || fl == '<')
                isNum = double.TryParse(SearchQuery.Replace(".", ",").Substring(1), out res);
            else
                isNum = double.TryParse(SearchQuery.Replace(".", ","), out res);
            if (isNum)
                _value = res;
            Recipes.Clear();
            var temp = Temp.Where(x => MyComparer(x,fl));
            foreach (var i in temp)
            {
                Recipes.Add(i);
            }
        }

        private void Remove(object parameter)
        {
            IList list = parameter as IList;
            List<Recipe> Selected = list.Cast<Recipe>().ToList();
            Description = null;
            CurrIngrs = null;
            CurrImg = null;
            Recipes.Clear();
            foreach (var i in Selected)
            {
                File.Delete(Environment.CurrentDirectory + String.Format(@"\Images\{0}.png", i.Name));
                Recipes.Remove(i);
            }
            var edited = Temp.Except(Selected).ToList();
            Temp = edited;
        }

        private void Clear(object parameter)
        {
            SearchQuery = null;
            Index = 0;
            Recipes.Clear();
            foreach (var i in Temp)
            {
                Recipes.Add(i);
            }
        }
        #endregion

        #region Command

        public ICommand DeleteRecipes
        {
            get { return new CommandBase(Remove); }
        }

        public ICommand SearchClear
        {
            get { return new CommandBase(Clear); }
        }
        #endregion

    }

   
}


