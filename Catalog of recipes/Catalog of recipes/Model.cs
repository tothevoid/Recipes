using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Catalog_of_recipes
{
    [Serializable]
     class Item
    {
        public string Name { get; set; }
        public double Pr { get; set; }
        public double Ch { get; set; }
        public double Fat { get; set; }
        public double Cl { get; set; }
        public Item(string Name, double Pr, double Ch, double Fat, double Cl)
        {
            this.Name = Name;
            this.Pr = Pr;
            this.Ch = Ch;
            this.Fat = Fat;
            this.Cl = Cl;
        }
    }

    [Serializable]
    class Recipe : Item
    {
        public string Time { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public Recipe(string Name, string Time, string Description, double Pr, double Ch, double Fat, double Cl, string Ingredients) : base(Name, Pr, Ch, Fat, Cl)
        {
            this.Time = Time;
            this.Description = Description;
            this.Ingredients = Ingredients;
        }
    }

    //[Serializable]
    //class Recipe:ViewModelBase, IDataErrorInfo
    //{
    //    private string _name;
    //    private double _pr;
    //    private double _ch;
    //    private double _fat;
    //    private double _cl;
    //    private string _time;
    //    private string _description;
    //    private string _ingredients;

    //    public string Name { get { return _name; }
    //        set
    //        {
    //            //if (value == "")
    //            //    return;
    //            Set(ref _name, value);
    //        }
    //    }
    //    public string Time { get { return _time; } set { Set(ref _time, value); } }
    //    public string Description { get { return _description; }
    //        set { Set(ref _description, value); } }
    //    public string Ingredients { get { return _ingredients; } set { Set(ref _ingredients, value); } }

    //    public double Pr { get { return _pr; } set { Set(ref _pr, value); } }
    //    public double Ch { get { return _ch; } set { Set(ref _ch, value); } }
    //    public double Fat { get { return _fat; } set { Set(ref _fat, value); } }
    //    public double Cl { get { return _cl; } set { Set(ref _cl, value); } }

    //}

    [Serializable]
    class Ingredient:Item
    {
        public double Weight { get; set; }
        public Ingredient(string Name, double Pr, double Ch, double Fat, double Cl, double Weight):base(Name,Pr,Ch,Fat,Cl)
        {
            this.Weight = Weight;
        }
    }

    class Model
    {
        public List<Ingredient> Load_ingr()
        {
            string[] temp = File.ReadAllLines("Ingredients.txt");
            List<Ingredient> Ingredients = temp.Select(x => x.Split('\t')).Select(x => new Ingredient(x[0], Convert.ToDouble(x[1]), Convert.ToDouble(x[2]), Convert.ToDouble(x[3]), Convert.ToDouble(x[4]), Convert.ToDouble(x[5]))).ToList();
            return Ingredients;
        }
    }
}

