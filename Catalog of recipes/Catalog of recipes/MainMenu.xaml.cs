using System;
using System.Windows;

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
