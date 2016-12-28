using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace Catalog_of_recipes
{

    public partial class MainMenu : Window
    {
       
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Close (object sender, EventArgs e)
        {
            ViewModelBase.Save();
            ViewModelBase.Save_ingrs();
        }

       
    }

}
