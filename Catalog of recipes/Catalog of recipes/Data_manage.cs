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

    class Recipe:Item
    {
        public string Time { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public Recipe(string Name, string Time, string Description, double Pr, double Ch, double Fat, double Cl, string Ingredients) :base(Name,Pr,Ch,Fat,Cl)
        {
            this.Time = Time;
            this.Description = Description;
            this.Ingredients = Ingredients;
        }

    }

    class Ingredient:Item
    {
        public double Weight { get; set; }
        public Ingredient(string Name, double Pr, double Ch, double Fat, double Cl, double Weight):base(Name,Pr,Ch,Fat,Cl)
        {
            this.Weight = Weight;
        }
    }

    class Data_manage
    {
        public List<Ingredient> Load_ingr()
        {
            var temp = File.ReadAllLines("Ingredients.txt");
            List<Ingredient> Ingredients = temp.Select(x => x.Split('\t')).Select(x => new Ingredient(x[0], Convert.ToDouble(x[1]), Convert.ToDouble(x[2]), Convert.ToDouble(x[3]), Convert.ToDouble(x[4]), Convert.ToDouble(x[5]))).ToList();
            return Ingredients;
        }

      
    }
}

