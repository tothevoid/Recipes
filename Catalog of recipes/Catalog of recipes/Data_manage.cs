using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Catalog_of_recipes
{
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

        public List<Item> Load_rec()
        {
            var temp = File.ReadAllLines("Recipes.txt");
            List<Item> Recipes = temp.Select(x => x.Split()).Select(x=>new Item(x[0], Convert.ToDouble(x[1]), Convert.ToDouble(x[2]), Convert.ToDouble(x[3]), Convert.ToDouble(x[4]))).ToList();
            return Recipes;
        }

        public List<Ingredient> Load_ingr()
        {
            var temp = File.ReadAllLines("Ingredients.txt");
            //List<Item>Ingredients = temp.Select(x => x.Split('\t')).Select(x => new Item(x[0], Convert.ToDouble(x[1]), Convert.ToDouble(x[2]), Convert.ToDouble(x[3]), Convert.ToDouble(x[4]))).ToList();
            List<Ingredient> Ingredients = temp.Select(x => x.Split('\t')).Select(x => new Ingredient(x[0], Convert.ToDouble(x[1]), Convert.ToDouble(x[2]), Convert.ToDouble(x[3]), Convert.ToDouble(x[4]), Convert.ToDouble(x[5]))).ToList();
            return Ingredients;
        }

      
    }
}

