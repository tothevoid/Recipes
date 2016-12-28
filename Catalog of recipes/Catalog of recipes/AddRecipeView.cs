using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;

namespace Catalog_of_recipes
{
    internal class AddRecipeView:ViewModelBase
    {
        #region Constructor
        public AddRecipeView()
        {
            Load_ingrs();
            Time = new List<string>() { "Праздничное", "Завтрак", "Обед", "Ужин" };
            UsingIngrs = new ObservableCollection<Ingredient>() {};
            Summary = "0: 0: 0: 0";
            Weight = "100";
            
        }
        #endregion

        #region Fields
        private string _name, _description, _message, _summary, _weight, _selectedTime;
        private int _searchselect;
        private Uri _image; 
        #endregion

        #region Properties
        public Uri Image{get { return _image; } set { Set(ref _image, value); }}
        public string Message { get { return _message; } set { Set(ref _message, value); } }
        public string SelectedTime { get { return _selectedTime; } set { Set(ref _selectedTime, value); } }
        public string Weight { get { return _weight; } set {Set(ref _weight,value); } }
        public string Summary {get { return _summary; } set {Set(ref _summary,value);} }
        public string Name { get { return _name; } set { Set(ref _name, value); } }
        public string Description { get { return _description; } set { Set(ref _description, value); } }
        public List<string> Time { get; set; }
        public ObservableCollection<Ingredient> UsingIngrs { get; set;}
        public int SearchSelect { get { return _searchselect; } set { Set(ref _searchselect,value);} }
       
    
        #endregion

        #region Methods

        private void Del(object parameter)
        {
            IList list = parameter as IList;
            List<Ingredient> Selected = list.Cast<Ingredient>().ToList();
            foreach (var i in Selected)
            {
                UsingIngrs.Remove(i);
            }
            CountSummary();
        }

        private void Add_recipe(object parameter)
        {
            bool isReady = FieldsCheck();
            if (isReady==false)
                return;
            if (Description == null)
                Description = "Отсутствует";
            List<double> props = Summary.Split(':').Select(x => double.Parse(x)).ToList();
            StringBuilder ingr = new StringBuilder();
            foreach (var x in UsingIngrs)
                ingr.Append(x.Name + "/" + x.Weight + "/");
            if (Temp.Count == Recipes.Count || Recipes.Count == 0)
                Recipes.Add(new Recipe { Name = Name, Time = SelectedTime, Description = Description, Pr = props[0], Fat = props[1], Ch = props[2], Cl = props[3], Ingredients = Convert.ToString(ingr) });
            Temp.Add(new Recipe { Name = Name, Time = SelectedTime, Description = Description, Pr = props[0], Fat = props[1], Ch = props[2], Cl = props[3], Ingredients = Convert.ToString(ingr) });
            Message = String.Format("{0} успешно добавлен ",Name);
            if (Image!=null)
            File.Copy(Image.LocalPath, Environment.CurrentDirectory + String.Format(@"\Images\{0}.png",Name));   
            Reset();
        }

        private bool FieldsCheck()
        {
            StringBuilder msg = new StringBuilder();
            if (Recipes.Any(i => Name == i.Name))
            {
                Message = "Такое название уже существует";
                return false;
            }
            if (string.IsNullOrEmpty(Name))
                    msg.Append("Имя, ");
            if (SelectedTime == null)
                    msg.Append("Время, ");
            if (UsingIngrs.Count == 0)
                    msg.Append("Ингредиенты ");
            if (msg.Length == 0)
                    return true;
                msg.Insert(0, "Необходимо заполнить: ");
                Message = Convert.ToString(msg).Substring(0,msg.Length-1);
                return false;
        }

        private void Add(object parameter)
        {
            
            var cur = Ingredients[SearchSelect];
            var dif = Math.Round(Convert.ToDouble(Weight) / cur.Weight,1);
            var newIngr = new Ingredient { Name = cur.Name, Pr = cur.Pr * dif, Ch = cur.Ch * dif, Fat = cur.Fat * dif, Cl = cur.Cl * dif, Weight = Convert.ToDouble(Weight)};
            int index = Check(cur);
            if (index == -1)
                UsingIngrs.Add(newIngr);
            else
            {
                UsingIngrs[index].Ch += newIngr.Ch;
                UsingIngrs[index].Fat += newIngr.Fat;
                UsingIngrs[index].Cl += newIngr.Cl;
                UsingIngrs[index].Pr += newIngr.Pr;
                UsingIngrs[index].Weight += newIngr.Weight;
                var update = UsingIngrs[index];
                UsingIngrs.RemoveAt(index);
                UsingIngrs.Add(update);
            }
            CountSummary();
        }

        
        private int Check(Ingredient temp)
        {
            int index = -1;
            for (int i = 0; i < UsingIngrs.Count; i++)
            {
                if (UsingIngrs[i].Name == temp.Name)
                    index = i;
            }
            return index;
        }

        private void Reset()
        {
            Name = null;
            Description = null;
            Image = null;
            SelectedTime= null;
            Summary = "0: 0: 0: 0";
            UsingIngrs.Clear();
        }

        private void Add_image(object parameter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Image";
            dlg.Filter = "Image (.png)|*.png";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
                Image = new Uri(dlg.FileName);
        }

        private void CountSummary()
        {
            Item temp = new Item { Ch = 0, Cl = 0, Fat = 0, Pr = 0 };
            foreach (var i in UsingIngrs)
            {
                temp.Ch += i.Ch;
                temp.Fat += i.Fat;
                temp.Cl += i.Cl;
                temp.Pr += i.Pr;
            }
            Summary = string.Format("{0} : {1} : {2} : {3}", temp.Pr, temp.Ch, temp.Fat, temp.Cl);
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

        public ICommand AddImage
        {
            get { return new CommandBase(Add_image); }
        }

        public ICommand Del_ingr
        {
            get { return new CommandBase(Del); }
        }
        #endregion

    }

}
