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

    [Serializable]
    class Ingredient:Item
    {
        public double Weight { get; set; }
        public Ingredient(string Name, double Pr, double Ch, double Fat, double Cl, double Weight):base(Name,Pr,Ch,Fat,Cl)
        {
            this.Weight = Weight;
        }
    }
}

