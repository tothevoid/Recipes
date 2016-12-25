using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_of_recipes
{
    class ManageIngredienstVm:ViewModelBase
    {
        string _pr;
        string _fat;
        string _ch;

        public string Pr { get { return _pr; } set { Set(ref _pr, value);} }
        public string Fat { get { return _fat; } set { Set(ref _fat, value); } }
        public string Ch { get { return _ch; } set { Set(ref _ch, value); } }


    }
}
