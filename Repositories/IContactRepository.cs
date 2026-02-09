using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualContactInformation.Models;

namespace VirtualContactInformation.Repositories
{
    interface IContactRepository
    {
        List<ContactInfo> LoadContacts();
        void SaveContacts(List<ContactInfo> contacts);
    }
}
