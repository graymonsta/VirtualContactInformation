using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualContactInformation.Models;
using VirtualContactInformation.Repositories;

namespace VirtualContactInformation.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private readonly IContactRepository repository;

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string phone = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private ContactInfo? selectedContact;

        public ObservableCollection<ContactInfo> Contacts { get; }

        public ContactViewModel()
        {
            repository = new JsonContactRepository();

            var loaded = repository.LoadContacts();
            Contacts = new ObservableCollection<ContactInfo>(loaded);

            AddCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
            DeleteCommand.NotifyCanExecuteChanged();
        }
        private bool CanAdd() =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(Phone) &&
            !string.IsNullOrWhiteSpace(Email);

        private bool CanDelete() =>
            SelectedContact != null;

        private bool CanUpdate() =>
            SelectedContact != null && CanAdd();

        // COMANDOS
        [RelayCommand(CanExecute = nameof(CanAdd))]
        private void Add()
        {
            var contact = new ContactInfo
            {
                Name = Name,
                Phone = Phone,
                Email = Email
            };

            Contacts.Add(contact);
            Save();
            ClearFields();
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private void Delete()
        {
            if (SelectedContact == null) return;

            Contacts.Remove(SelectedContact);
            SelectedContact = null;

            Save();
            ClearFields();
        }

        [RelayCommand(CanExecute = nameof(CanUpdate))]
        private void Update()
        {
            if (SelectedContact == null) return;

            SelectedContact.Name = Name;
            SelectedContact.Phone = Phone;
            SelectedContact.Email = Email;

            Save();
        }
        partial void OnNameChanged(string value)
        {
            AddCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnPhoneChanged(string value)
        {
            AddCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnEmailChanged(string value)
        {
            AddCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        partial void OnSelectedContactChanged(ContactInfo? value)
        {
            if (value != null)
            {
                Name = value.Name;
                Phone = value.Phone;
                Email = value.Email;
            }

            DeleteCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }

        private void Save()
        {
            repository.SaveContacts(Contacts.ToList());
        }

        private void ClearFields()
        {
            Name = "";
            Phone = "";
            Email = "";

            AddCommand.NotifyCanExecuteChanged();
            UpdateCommand.NotifyCanExecuteChanged();
        }
    }
}