using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VirtualContactInformation.Models;

namespace VirtualContactInformation.Repositories
{
    public class JsonContactRepository : IContactRepository
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.json");


        public List<ContactInfo> LoadContacts()
        {
            if (!File.Exists(filePath))
                return new List<ContactInfo>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<ContactInfo>>(json)
                   ?? new List<ContactInfo>();
        }

        public void SaveContacts(List<ContactInfo> contacts)
        {
            var folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }
    }
}
