using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Catalog_of_recipes
{
    [Serializable]
    class Item
    {
        private string _name;
        private double _pr;
        private double _ch;
        private double _fat;
        private double _cl;

        public string Name { get { return _name; }set{ _name = value;}}
        public double Pr { get { return _pr; } set { _pr = value; } }
        public double Ch { get { return _ch; } set { _ch = value; } }
        public double Fat { get { return _fat; } set { _fat = value; } }
        public double Cl { get { return _cl; } set { _cl = value; } }
    }

    [Serializable]
    class Recipe : Item
    {
        private string _time;
        private string _description;
        private string _ingredients;

        public string Time { get { return _time; } set { _time = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Ingredients { get { return _ingredients; } set { _ingredients = value; } }
    }

    [Serializable]
    class Ingredient : Item
    {
        private double _weight;
        public double Weight { get { return _weight; } set { _weight = value; } }

    }
}

