using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Catalog_of_recipes
{
    class ManageIngredienstVm:ViewModelBase
    {
        string _pr;
        string _fat;
        string _ch;
        string _weight;
        string _name;

        public string Name { get { return _name; } set { Set(ref _name, value); } }
        public string Pr { get { return _pr; } set { Set(ref _pr, value.Replace('.', ','));} }
        public string Fat { get { return _fat; } set { Set(ref _fat, value.Replace('.', ',')); } }
        public string Ch { get { return _ch; } set { Set(ref _ch, value.Replace('.', ',')); } }
        public string Weight { get { return _weight; } set { Set(ref _weight, value); } }

        private void Add(object parameter)
        {
            double b = Math.Round(Convert.ToDouble(Pr)*4+Convert.ToDouble(Ch) *4+Convert.ToDouble(Fat) *9,2);
            var a = new Ingredient(Name,Convert.ToDouble(Pr), Convert.ToDouble(Ch), Convert.ToDouble(Fat), b,Convert.ToDouble(Weight));
            Ingredients.Add(a);
            Search.Add(Name);
        }

        private void Remove(object parameter)
        {
            IList list = parameter as IList;
            List<Ingredient> Selected = list.Cast<Ingredient>().ToList();
            foreach (var i in Selected)
            {
                Ingredients.Remove(i);
            }
            Search.Clear();
            foreach (var i in Ingredients )
            {
                Search.Add(i.Name);
            }
        }

        public ICommand Del_ingr
        {
            get { return new CommandBase(Remove); }
        }

        public ICommand Add_ingr
        {
            get { return new CommandBase(Add); }
        }
    }
}
