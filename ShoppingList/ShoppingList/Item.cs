using System;

namespace ShoppingList
{
    public class Item : IComparable
    {
        public string Name { get; set; }
        public bool CrossedOff { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public Item(string name, bool crossedOff)
        {
            Name = name;
            CrossedOff = crossedOff;
        }

        public int CompareTo(object obj)
        {
            if(obj is Item)
            {
                return ItemCompareTo((Item)obj);
            }
            return -1;
        }

        public int ItemCompareTo(Item item)
        {
            return Name.CompareTo(item.Name);
        }
    }
}
/*
<TextBlock Grid.Column="1" Grid.Row= "3" Background= "#3C7ECE" Foreground= "white" >
            < TextBlock.Text >
                < Binding Path= "SelectedItem.Name" StringFormat= "Selected: {0}" />
            </ TextBlock.Text >
        </ TextBlock >
*/

//<local:VisibleDetailsConverter x:Key="VisibleDetailsConverter" />

//Visibility="{Binding SelectedItem, Converter={StaticResource VisibleDetailsConverter}}"