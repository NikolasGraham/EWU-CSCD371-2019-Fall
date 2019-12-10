using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShoppingList.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void AddItem_TextEmpty_DoesntAdd()
        {
            MainWindowViewModel VM = new MainWindowViewModel();
            string toAdd = "";
            VM.ItemToAdd = toAdd;

            VM.AddItemCommand.Execute(toAdd);

            Assert.AreEqual(0, VM.Items.Count);
        }

        [TestMethod]
        public void AddItem_TextWhitespace_DoesntAdd()
        {
            MainWindowViewModel VM = new MainWindowViewModel();
            string toAdd = "            ";
            VM.ItemToAdd = toAdd;

            VM.AddItemCommand.Execute(toAdd);

            Assert.AreEqual(0, VM.Items.Count);
        }

        [TestMethod]
        public void AddItem_TextNotEmpty_AddsToList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();
            string toAdd = "Pineapple";
            VM.ItemToAdd = toAdd;

            VM.AddItemCommand.Execute(toAdd);

            Assert.AreEqual(1, VM.Items.Count);
        }

        [TestMethod]
        public void DeleteItem_DeleteSelectedItem()
        {
            MainWindowViewModel VM = new MainWindowViewModel();
            Item item = new Item("Pineapple");
            VM.Items.Add(item);

            VM.SelectedItem = item;

            VM.DeleteItemCommand.Execute(item);

            Assert.AreEqual(0, VM.Items.Count);
        }

        [TestMethod]
        public void SortList_SortsList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();
            VM.Items.Add(new Item("Pineapple"));
            VM.Items.Add(new Item("Cucumbers"));
            VM.Items.Add(new Item("Apples"));
            VM.Items.Add(new Item("Zuccinis"));

            VM.SortListCommand.Execute(VM.Items);

            Item[] expected =
            {
                new Item("Apples"),
                new Item("Cucumbers"),
                new Item("Pineapple"),
                new Item("Zuccinis")
            };

            Item[] actual = new Item[4];
            for(int i=0; i < VM.Items.Count; i++)
            {
                actual[i] = VM.Items[i];
            }

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i].CompareTo(actual[i]) == 0);
            }
        }

        [TestMethod]
        public void SaveList_SavesList_SavesListToSelectedList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();

            VM.Items.Add(new Item("Pineapple"));
            VM.Items.Add(new Item("Cucumbers", true));
            VM.Items.Add(new Item("Apples"));

            VM.CurrentList = "TESTLIST";

            VM.SaveListCommand.Execute(null);

            string[] savedItems = new string[3];
            int counter = 0;

            using(StreamReader SR = new StreamReader("shoppinglistTESTLIST.save"))
            {
                string line;
                while(( line = SR.ReadLine()) != null && line != "")
                {
                    savedItems[counter] = line;
                    counter++;
                }
            }

            string[] expected = { "Pineapple.", "Cucumbers.Checked", "Apples." };

            CollectionAssert.AreEqual(expected, savedItems);

            File.Delete("shoppinglistTESTLIST.save");
        }

        [TestMethod]
        public void LoadList_LoadsSelectedList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();

            VM.Items.Add(new Item("Pineapple"));
            VM.Items.Add(new Item("Cucumbers", true));
            VM.Items.Add(new Item("Apples"));

            VM.CurrentList = "TESTLIST";

            VM.SaveListCommand.Execute(null);

            VM.Items.Clear();

            VM.LoadListCommand.Execute(null);

            Item[] expected =
            {
                new Item("Pineapple"),
                new Item("Cucumbers", true),
                new Item("Apples")
            };

            for(int i=0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i].CompareTo(VM.Items[i]) == 0 && 
                    expected[i].CrossedOff == VM.Items[i].CrossedOff);
            }

            File.Delete("shoppinglistTESTLIST.save");
        }

        [TestMethod]
        public void SelectList_ChangeCurrentList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();

            VM.CurrentList = "1";

            VM.SelectList3Command.Execute(VM.CurrentList);

            Assert.AreEqual("3", VM.CurrentList);
        }

        [TestMethod]
        public void Unselect_UnselectsCurrentItem()
        {
            MainWindowViewModel VM = new MainWindowViewModel();

            Item item = new Item("Pineapple");
            VM.Items.Add(item);

            VM.SelectedItem = item;

            VM.UnselectCommand.Execute(item);

            Assert.AreEqual(null, VM.SelectedItem);
        }

        [TestMethod]
        public void ClearList_ClearsList()
        {
            MainWindowViewModel VM = new MainWindowViewModel();

            VM.Items.Add(new Item("Pineapple"));
            VM.Items.Add(new Item("Cucumbers", true));
            VM.Items.Add(new Item("Apples"));

            VM.ClearList.Execute(VM.Items);

            Assert.AreEqual(0, VM.Items.Count);
        }
    }
}