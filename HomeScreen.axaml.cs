using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace java_ide;

public partial class HomeScreen : Window
{
    public HomeScreen()
    {
        InitializeComponent();
    }
    
    // Button click handler to open the second window
    private void OpenSecondWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var secondWindow = new SecondWindow();
        secondWindow.Show();  // Show the second window
        this.Close();  // Close the current window
    }
}