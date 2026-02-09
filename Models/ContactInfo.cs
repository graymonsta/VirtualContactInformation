using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualContactInformation.Models
{
    public partial class ContactInfo : ObservableObject
    {
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string phone = "";

        [ObservableProperty]
        private string email = "";
    }
}
