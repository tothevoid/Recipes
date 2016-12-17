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
            tab2.Focus();
           
        }

        //private void Add_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    var a = Data_manage.Ingredients.Where(x => x.Name.ToLower() == curr_ingr.Text.ToLower()).FirstOrDefault();
        //    if (a == null)
        //        MessageBox.Show("NOT FOUND");
        //    else
        //    {
        //        Data_manage.using_list.Add(a);
        //        curr_ingr.Text = null;
        //        var b = Data_manage.using_list.Select(x => x.Name);
        //        IngrBox.ItemsSource = null;
        //        IngrBox.ItemsSource = b;
        //        Item temp = new Item(null,0,0,0,0);

        //        foreach (var x in Data_manage.using_list )
        //        {
        //            temp.Ch += x.Ch;
        //            temp.Fat += x.Fat;
        //            temp.Cl += x.Cl;
        //            temp.Pr += x.Pr;
        //        }
            
        //        Stats.Text = String.Format("{0} : {1} : {2} : {3}",temp.Pr,temp.Ch,temp.Fat,temp.Cl);

        //    }
        //}

     

        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    List<string>  a = bjd.Text.Split(';').ToList();
        //    if (a == null || a.Count != 3)
        //        MessageBox.Show("Неправильный ввод\nПример: a;b;c;", "Ошибка ввода");
        //}

        
     
        
    }

}
