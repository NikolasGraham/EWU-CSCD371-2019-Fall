using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ShoppingList
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if(!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Commands
        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SortListCommand { get; }

        // Items
        public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();

        private string _ItemToAdd = "";
        public string ItemToAdd
        {
            get => _ItemToAdd;
            set => SetProperty(ref _ItemToAdd, value);
        }

        private Item _SelectedItem = null;
        public Item SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        public MainWindowViewModel()
        {
            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command(OnDeleteItem);
            SortListCommand = new Command(OnSortList);

            Items.Add(new Item("Apples"));
            Items.Add(new Item("Bananas"));
        }

        private void OnAddItem()
        {
            if(ItemToAdd != "")
            {
                Items.Add(new Item(ItemToAdd));
                ItemToAdd = "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        private void OnDeleteItem()
        {
            Items.Remove(SelectedItem);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }

        private void OnSortList()
        {
            Items = new ObservableCollection<Item>(Items.OrderBy(i => i));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }
    }
}
