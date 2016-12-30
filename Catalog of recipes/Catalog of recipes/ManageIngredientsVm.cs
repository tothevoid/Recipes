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
    class ManageIngredientsVm:ViewModelBase
    {
        public ManageIngredientsVm()
        {
            Weight = "100";
        }
        string _pr;
        string _fat;
        string _ch;
        string _weight;
        string _name;
        string _message;

        public string Name { get { return _name; } set { Set(ref _name, value); } }
        public string Pr { get { return _pr; } set { Set(ref _pr, value.Replace('.', ','));} }
        public string Fat { get { return _fat; } set { Set(ref _fat, value.Replace('.', ',')); } }
        public string Ch { get { return _ch; } set { Set(ref _ch, value.Replace('.', ',')); } }
        public string Message { get { return _message; } set { Set(ref _message, value.Replace('.', ',')); } }
        public string Weight { get { return _weight; } set { Set(ref _weight, value); } }

        private void Add(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Pr) || string.IsNullOrWhiteSpace(Fat) || string.IsNullOrWhiteSpace(Ch))
            {
                Message = "Все параметры равны 0";
                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                Message = "Отсутствует имя";
                    return;
            }
            if (WeigtCheck(Weight) == false)
            {
                Message = "Масса введена неправильно";
                return;
            }
            if (Validty())
            {
                Message = "Такой ингредиент уже существует";
                return;
            }
            double cl = Math.Round(Convert.ToDouble(Pr)*4+Convert.ToDouble(Ch) *4+Convert.ToDouble(Fat) *9,2);
            var newIngr = new Ingredient { Name = Name, Pr = Convert.ToDouble(Pr), Ch = Convert.ToDouble(Ch), Fat = Convert.ToDouble(Fat), Cl = cl, Weight = Convert.ToDouble(Weight) };
            Ingredients.Add(newIngr);
            Message = string.Format("Ингредиент {0} добавлен", Name);
            Search.Add(Name);
        }

        private bool Validty()
        {
            foreach (var i in Search)
            {
                if (Name == i)
                    return true;
            }
            return false;
        }

        private void Remove(object parameter)
        {
            IList list = parameter as IList; 
            List<Ingredient> Selected = list.Cast<Ingredient>().ToList();
            foreach (var i in Selected)
            {
                Ingredients.Remove(i);
            }
            foreach (var i in Selected)
            {
                Search.Remove(i.Name);
            }
        }

        public ICommand DelIngr
        {
            get { return new CommandBase(Remove); }
        }

        public ICommand AddIngr
        {
            get { return new CommandBase(Add); }
        }
    }
}
