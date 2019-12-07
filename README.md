# EWU-CSCD371-2019-Fall

## Assignment 9

For this assignment you will be creating a simple shopping list application using WPF. This assignment should be completed using the [MVVM design pattern](https://intellitect.com/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/).

#### Main Window
The main window should have several elements. I recommend using the same elements for creating a new item as well as editing an existing items.

- It should display a list of all of the shopping list items. 
- The each item in the list should display the name of item.
- Selecting an item in the list should allow you to edit it.
- When no item is selected the view elements for editing an item should be hidden. [Value converters](https://www.wpftutorial.net/ValueConverters.html) will be helpful here.
- There should be a button (or similar) that allows for creating a new shopping item.
- The MainWindows (and all view object should ***not*** be unit tested)

#### View Models
You will need to create view model objects to support the above view functionality. The following is required:
- A view model for the Main Windows
- This view model should have a command that is invoked when the user wants to create a new shopping item.
- It should also have an [ObservableCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netcore-3.0) of items.
- It should have a command that is invoked when a new shopping item is created. There is an existing ICommand implementation in the project.
- All wiew models should raise the [INotifyPropertyChanged (INPC)](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=netcore-3.0) as needed.
- All view models should be unit tested.

#### Extra Credit
- Give your app a little bit of flare by adding in some theming. Some popular options are [MaterialDesignInXaml](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) or [MahApps](https://github.com/MahApps/MahApps.Metro). Because we are using .NET Core 3.0, you will need to use their *preview* NuGet packages.
- Rather than implementing INPC on your view models, and using your own commands, use a third party MVVM library. A popular options is [MVVMLight](https://www.nuget.org/packages/MvvmLightLibsStd10/).