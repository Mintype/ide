using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace java_ide;

public partial class EditorWindow : Window
{
    private string _folderPath;
    public EditorWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public EditorWindow(string folderPath)
    {
        _folderPath = folderPath;
        Console.WriteLine("FolderPath: " + _folderPath);
        InitializeComponent();
    }
}