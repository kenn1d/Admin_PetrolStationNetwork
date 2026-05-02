using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolStationNetwork.Models
{
    public partial class Product : ObservableObject
    {
        public int id { get; set; }

        [ObservableProperty]
        private string name;
    }
}
