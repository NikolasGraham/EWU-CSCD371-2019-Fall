namespace ShoppingList
{
    internal class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
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