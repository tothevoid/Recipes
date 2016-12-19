using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Media.Animation;


namespace Catalog_of_recipes
{

    public partial class MainMenu : Window
    {
       
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Load (object sender, RoutedEventArgs e)
        {
            ViewModelBase.Load();
        }

        private void Close (object sender, EventArgs e)
        {
            ViewModelBase.Save();
        }
    }

}
