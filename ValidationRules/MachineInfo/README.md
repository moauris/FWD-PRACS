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

