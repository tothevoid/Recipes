using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Catalog_of_recipes
{
    internal class AddRecipeView:ViewModelBase
    {
        #region Constructor
        public AddRecipeView()
        {
            Time = new List<string>() { "Праздничное", "Завтрак", "Обед", "Ужин" };
            Ingredients = new Data_manage().Load_ingr();
            Search = Ingredients.Select(x => x.Name).ToList();
            Using_ingrs = new ObservableCollection<Ingredient>() {};
            Summary = "0: 0: 0: 0";
            Weight = "100";
        }
        #endregion

        #region Fields
        private string _message;
        private readonly List<Ingredient> Ingredients;   
        private string _name, _description;
        private List<string> _search;
        private int _searchselect;
        private string _summary;
        private string _weight;
        private string _selectedTime;
        #endregion

        #region Properties
        public string Message { get { return _message; } set { Set(ref _message, value); } }
        public string SelectedTime { get { return _selectedTime; } set { Set(ref _selectedTime, value); } }
        public string Weight { get { return _weight; } set {Set(ref _weight,value); } }
        public string Summary {get { return _summary; } set {Set(ref _summary,value);} }
        public List<string> Time { get; set; }
        public ObservableCollection<Ingredient> Using_ingrs { get; set; }
        public int SearchSelect { get { return _searchselect; } set { Set(ref _searchselect,value);} }
        public List<string> Search { get { return _search; } set {Set(ref _search,value);} }
        public string Name { get { return _name; } set { Set(ref _name, value); } }
        public string Description { get { return _description; } set { Set(ref _description, value); } }
        #endregion

        #region Methods
        private void Add_recipe(object parameter)
        {
            if (Name=="" || Using_ingrs.Count == 0 || SelectedTime == null)
            {
                Message = "Не все поля заполнены";
                return;
            }
            if (Description == null)
                Description = "Отсутствует";
            List<double> props = Summary.Split(':').Select(x => double.Parse(x)).ToList();
            StringBuilder ingr = new StringBuilder();
            foreach (var x in Using_ingrs)
                ingr.Append(x.Name + "/" + x.Weight + "/");
            if (Temp.Count == Recipes.Count || Recipes.Count == 0)
               Recipes.Add(new Recipe(Name, SelectedTime, Description, props[0], props[1], props[2], props[3], Convert.ToString(ingr)));
            Temp.Add(new Recipe(Name, SelectedTime, Description, props[0], props[1], props[2], props[3], Convert.ToString(ingr)));
            Message = String.Format("Рецепт {0} успешно добавлен ",Name);
           
         
            //List<string> list = Convert.ToString(ingr).Split('/').ToList();
            //list.RemoveAt(list.Count-1);
            //Recipes.Add(new Item(Name, props[0], props[1], props[2], props[3]));
            //Temp.Add(new Item(Name, props[0], props[1], props[2], props[3]));
        }

        private void Add(object parameter)
        {
            var temp = Ingredients[SearchSelect];
            var dif = Convert.ToDouble(Weight)/temp.Weight;
            var temp2 = new Ingredient(temp.Name, temp.Pr*dif,temp.Ch*dif,temp.Fat*dif,temp.Cl*dif,Convert.ToDouble(Weight));
            Using_ingrs.Add(temp2);
            CountSummary();
        }

        private void CountSummary()
        {
            Item temp = new Item(null, 0, 0, 0, 0);
            foreach (var x in Using_ingrs)
            {
                temp.Ch += x.Ch;
                temp.Fat += x.Fat;
                temp.Cl += x.Cl;
                temp.Pr += x.Pr;
            }
            Summary = String.Format("{0} : {1} : {2} : {3}", temp.Pr, temp.Ch, temp.Fat, temp.Cl);
        }
        #endregion

        #region Command
        public ICommand Add_Ingr
        {
            get { return new CommandBase(Add); }
        }

        public ICommand Add_rec
        {
            get { return new CommandBase(Add_recipe); }
        }
        #endregion
    }

}
