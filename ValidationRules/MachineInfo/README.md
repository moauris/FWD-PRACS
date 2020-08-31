# FWD-PRACS: MachineInfo

[toc]

## Goal

Create a WPF application that contains the below elements:

<span style="background:grey;color:white">  Input Fields:  </span>

- Host Name field
- Domain field
- IPv4 Address field
- IPv6 Address field
- Operating System field
- OS Version field
- OS Architecture field

<span style="background:gray;color:white">  Buttons:  </span>

- Submit - generates a **name-enumerated**, **serialized** MachineInfoEntry.dat **binary** file locally.
- Clear - through a warning, cancel input, clear all the fields.
- Close - through a warning, close the window.

<span style="background:orangered;color:white">  Validation Rules:  </span>

- Host Name: must start with three letters, must be between 8 and 50 digits
- Domain: must start with dot, consists of alphanumeric strings, separated by dots, but not end with dots.
- IPv4 & IPv6: Validate by IP Address parse rules. IPv4 contains 4 bytes, while IPv6, 16.
- Operating System, OS Version, OS Architecture: List box items binding to external xml file, which is exported from database.

## UI Preview



## Technology Points:

### Validation Rules

Validation Rules are objects that checks the input of the target control, check if they are valid. If valid, returns valid enum, if not, returns invalid.

- All Validation Rule classes are created by inheriting from `System.Windows.Controls.ValidationRule` abstract class.
- The implementations is a method called `validate` that returns a `ValidationRule` object.

### Binding Group 

An object that is directly inherited from `System.Windows.DependencyObject` which is used to bind binding objects and validation rules.

- In the outer wrapper (i.e. panels, grid, etc.), handle the event to the event handler `Validation.Error`. The delegate contains a special eventargs called `ValidateionErrorEventArgs`, in its `Action` property, it takes an enum for `ValidationErrorEventAction`. We can actually access the state of when error is added or removed.
- The Binding Group object in the outer wrapper takes several methods for the edit transaction. `BeginEdit()`, `CancelEdit()`, and `CommitEdit()`.

### Binding Validation Rules to Input Control

To bind the validation rules to input control, we need to understand that the input control (such as Textboxes) actually takes in its content other than values (i.e. string, numbers) a set of objects called `Bindings`. 

Bindings can bind the expression of the object, most likely a string, but it also binds `ValidationRules`.

Snippet of the example:

```xaml
<HeaderedContentControl Header="Date Offer Ends">
    <TextBox Name="dateField" Width="150" >
      <TextBox.Text>
        <Binding Path="OfferExpires" StringFormat="d" >
          <Binding.ValidationRules>
            <src:FutureDateRule/>
          </Binding.ValidationRules>
        </Binding>
      </TextBox.Text>
    </TextBox>
  </HeaderedContentControl>
```

### Headered Content Control - Template - Content Presenter

Content presenter dictates how a control is displayed. In this particular example, it helps sorting the control object called Headered Content Control. It acts similar to a label, but it is actually a wrapper that wraps one or a set of controls to a title.

### MVVM pattern

The Model - View - ViewModel design pattern is a pattern that is used to separate the frontend design of an app to it's backend. The idea is that when you have a team which have delegation of front/backend workload, the both teams can work on the same app without having to interfere with each other.

- View: dictates how an UI Looks
- Model: contains objects used in the UI interaction
- ViewModel: The "engine" that helps with the interaction between Model and View. Models feed into the ViewModel, and ViewModel in turn supplies to the View.

### The `IEditableObject` Inheritance

For the object associated with the form, it implements the `System.ComponentModel.IEditableObject` interface.

It has three implementations, `BeginEdit()`, `CancelEdit()`, and `CommitEdit()`.

### How are the Model in the form structured.

Actual data object is a struct named `ItemData`, which has the three fields. It has a static method which returns its own type:

```c#
private struct ItemData
{
    internal string Description;
    internal DateTime OfferExpires;
    internal double Price;

    internal static ItemData NewItem()
    {
        var data = new ItemData
        {
            Description = "New item",
            Price = 0,
            OfferExpires = DateTime.Now + new TimeSpan(7, 0, 0, 0)
        };

        return data;
    }
}
```

<span style="color:darkgray">About Struct: Structs are like classes but they are value types</span>

The main class is this, a class that implements `INotifyPropertyChanged` and `IEditableObject` interfaces.

Two fields exist for this class:

```C#
public class PurchaseItem : INotifyPropertyChanged, IEditableObject
{
    private ItemData _copyData = ItemData.NewItem();
    private ItemData _currentData = ItemData.NewItem();
    //...
    private struct ItemData
    {
        internal string Description;
        internal DateTime OfferExpires;
        internal double Price;

        internal static ItemData NewItem()
        {
            var data = new ItemData
            {
                Description = "New item",
                Price = 0,
                OfferExpires = DateTime.Now + new TimeSpan(7, 0, 0, 0)
            };

            return data;
        }
    }
}
```

Of course it carries with it all the properties of the Struct, but in the property getters and setters, they are manipulating the `_currentData` field.

Now, you might be wondering what is the significance of the copy data and current data. Now, in the item transaction, begin edit, copies the current data to the copy data. Cancel edit simply does the reverse. End edit assign an empty item data to the copy data.

```C#
public class PurchaseItem : INotifyPropertyChanged, IEditableObject
{
    //...
    #region IEditableObject Members

    public void BeginEdit()
    {
        _copyData = _currentData;
    }

    public void CancelEdit()
    {
        _currentData = _copyData;
        NotifyPropertyChanged("");
    }

    public void EndEdit()
    {
        _copyData = ItemData.NewItem();
    }

    #endregion
}
```

## Try Implement

### Create MVVM

There are actually 4 folders: Models, ViewModels, Views (obviously), and **ValidationRules**. In the example, all MVVM objects and the laid out outside, but for a good practice, let's split them into different folders to imitate larger projects.



### Create Models

First of all is the Models, think about the mission that we are trying to achieve, a Machine Info form with 7 input controls, and for OS Info it is a combo box with 3 elements.

 Implement `INotifyPropertyChanged`, and `IEditableObject`. Don't forget to implement the event invoker.

We are using one private struct for holding the data for editable object called `DataItem`.

```c#
class MachineInfo : INotifyPropertyChanged, IEditableObject
    {


        private struct DataItem
        {
            internal string HostName;
            internal string Domain;
            internal string IPv4;
            internal string IPv6;
            internal string OSName;
            internal string OSVersion;
            internal string OSBit;
            internal DataItem NewItem()
            {
                return new DataItem
                {
                    HostName = "",
                    Domain = "",
                    IPv4 = "",
                    IPv6 = "",
                    OSName = "",
                    OSVersion = "",
                    OSBit = ""
                };
            }
        }
	}
```

Now, create two fields, copy item and current item.

Create properties of the Machine Info object, in the setter, on property notify changed.

Now, implement the begin, cancel, and end edit methods.

Done.

### Design Form View







