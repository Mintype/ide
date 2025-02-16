using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace java_ide;

public partial class SecondWindow : Window
{
    private string _folderPath;
    public SecondWindow()
    {
        InitializeComponent();
    }
    
    public SecondWindow(string folderPath)
    {
        _folderPath = folderPath;
        Console.WriteLine("FolderPath: " + _folderPath);
        InitializeComponent();
    }
}