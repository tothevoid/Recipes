using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
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
        //private Uri _currentImg;
        private BitmapImage _currentImg;
        private FileStream stream;
        #endregion

        #region Properties
        public BitmapImage Current_img { get { return _currentImg; } set { Set(ref _currentImg,value);} }
        //public Uri Current_img { get { return _currentImg; } set { Set(ref _currentImg, value); } }
        public string Curr_ingrs {get { return _currIngrs; } set {Set(ref _currIngrs, value);} }
        public string Description { get { return _description; }
            set {Set(ref _description,value); } }
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
                if (SearchQuery != "")
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
            Curr_ingrs = Convert.ToString(convert);
            Description = Recipes[Current_recipe].Description;
            GetImg(Recipes[Current_recipe].Name);
        }

        private void GetImg(string Name)
        { 
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(Environment.CurrentDirectory + String.Format(@"\Images\{0}.png", Name));
            image.EndInit();
            Current_img = image;
        }

        private bool MyComparer(Recipe rec, string searchString, bool isNum)
        {
            if (isNum)
                _value = double.Parse(searchString.Replace(".", ","));

            switch (Index)
            {
                case 0: return rec.Name.ToLower().Contains(searchString);
                case 1:
                    return Math.Abs(_value - rec.Cl) < 1;
                case 2:
                    return Math.Abs(_value - rec.Pr) < 1;
                case 3:
                    return Math.Abs(_value - rec.Fat) < 1;
                case 4:
                    return Math.Abs(_value - rec.Ch) < 1;
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
            var temp = Temp.Where(x => MyComparer(x, SearchQuery.ToLower(), isNum));
            foreach (var i in temp)
            {
                Recipes.Add(i);
            }
        }

        private void Remove(object parameter)
        {
            Current_img = null;
            IList list = parameter as IList;
            List<Recipe> Selected = list.Cast<Recipe>().ToList();
            foreach (var i in Selected)
            {
                File.Delete(Environment.CurrentDirectory + String.Format(@"\Images\{0}.png", i.Name));
                Recipes.Remove(i);
            }
            Temp = Temp.Except(Selected).ToList();
        }

        private void Clear(object parameter)
        {
            SearchQuery = "";
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


