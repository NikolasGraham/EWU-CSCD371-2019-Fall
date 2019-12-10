using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
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
        public ICommand SaveListCommand { get; }
        public ICommand LoadListCommand { get; }
        public ICommand CrossOffCommand { get; }
        public ICommand UnselectCommand { get; }

        public ICommand SelectList1Command { get; }
        public ICommand SelectList2Command { get; }
        public ICommand SelectList3Command { get; }
        public ICommand SelectList4Command { get; }
        public ICommand ClearList { get; }
        public ICommand DeleteList { get; }

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

        public string CurrentList { get; set; } = "NONE";

        public MainWindowViewModel()
        {
            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command(OnDeleteItem);
            SortListCommand = new Command(OnSortList);
            SaveListCommand = new Command(OnSaveList);
            LoadListCommand = new Command(OnLoadList);
            CrossOffCommand = new Command(OnCrossOff);
            UnselectCommand = new Command(OnUnselect);

            SelectList1Command = new Command(SelectListOne);
            SelectList2Command = new Command(SelectListTwo);
            SelectList3Command = new Command(SelectListThree);
            SelectList4Command = new Command(SelectListFour);
            ClearList = new Command(OnClearList);
            DeleteList = new Command(OnDeleteList);
        }

        private void OnAddItem()
        {
            if(!string.IsNullOrWhiteSpace(ItemToAdd))
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

        private void OnSaveList()
        {
            var file = File.Create($"shoppinglist{CurrentList}.save");
            file.Close();
            
            using (StreamWriter SW = new StreamWriter($"shoppinglist{CurrentList}.save"))
            {
                foreach (Item item in Items)
                {
                    if(item.CrossedOff == true)
                    {
                        SW.WriteLine($"{item.Name}.Checked");
                    }
                    else
                    {
                        SW.WriteLine($"{item.Name}.");
                    }
                }
            }
        }

        private void OnLoadList()
        {
            if (File.Exists($"shoppinglist{CurrentList}.save"))
            {
                Items.Clear();
                using (StreamReader SR = new StreamReader($"shoppinglist{CurrentList}.save"))
                {
                    string line;
                    while((line = SR.ReadLine()) != null && line != "")
                    {
                        string[] itemParts = line.Split(".");
                        if(itemParts[1] == "Checked")
                        {
                            Items.Add(new Item(itemParts[0], true));
                        }
                        else
                        {
                            Items.Add(new Item(itemParts[0], false));
                        }
                    }
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
            else //(Items.Count == 0 || !File.Exists($"shoppinglist{CurrentList}.save"))
            {
                MessageBox.Show("List doesn't exist, add items first!");
            }
        }

        private void OnCrossOff()
        {
            int index = Items.IndexOf(SelectedItem);
            if (SelectedItem.CrossedOff)
            {
                Items[index].CrossedOff = false;
            }
            else
            {
                Items[index].CrossedOff = true;
            }
            Item temp = Items[index];
            Items.RemoveAt(index);
            Items.Insert(index, temp);
            SelectedItem = temp;
        }

        private void SelectListOne()
        {
            CurrentList = "1";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentList)));
        }
        private void SelectListTwo()
        {
            CurrentList = "2";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentList)));
        }
        private void SelectListThree()
        {
            CurrentList = "3";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentList)));
        }
        private void SelectListFour()
        {
            CurrentList = "4";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentList)));
        }

        private void OnClearList()
        {
            Items.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }

        private void OnDeleteList()
        {
            File.Delete($"shoppinglist{CurrentList}.save");
        }

        private void OnUnselect()
        {
            SelectedItem = null;
        }
    }
}
