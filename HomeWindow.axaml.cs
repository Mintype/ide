using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;

namespace java_ide;

public partial class HomeWindow : Window
{
    private const string FolderPathsFile = "folderPaths.json"; // File to store the paths
    private FolderPaths _folderPaths;
    
    public HomeWindow()
    {
        InitializeComponent();
        LoadFolderPaths();
        UpdateFolderListBox();
    }

    private void UpdateFolderListBox()
    {
        var folderListBox = this.FindControl<ListBox>("ProjectsList");
            
        // Extract folder names from the paths and bind them to the ListBox
        var folderNames = _folderPaths.Paths.Select(path => Path.GetFileName(path)).ToList();
        folderListBox.ItemsSource = folderNames;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _folderPaths = new FolderPaths();
    }
    
    private void SaveFolderPaths()
    {
        string json = JsonConvert.SerializeObject(_folderPaths);
        File.WriteAllText(FolderPathsFile, json);
    }

    private void LoadFolderPaths()
    {
        if (File.Exists(FolderPathsFile))
        {
            string json = File.ReadAllText(FolderPathsFile);
            _folderPaths = JsonConvert.DeserializeObject<FolderPaths>(json);
        }
    }

    // Open Folder Button click handler
    private async void OpenFolderButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog
        {
            Title = "Select a folder"
        };
            
        string path = await dialog.ShowAsync(this);
            
        if (path != null)
        {
            // Add the new path to the list and save it
            _folderPaths.Paths.Add(path);
            SaveFolderPaths();
            
            // Create and open the editor window with the path
            OpenEditorWindow(path);
        }
        else
        {
            Console.WriteLine("No folder selected.");
        }
    }
    
    private void OpenEditorWindow(String path)
    {
        var editorWindow = new EditorWindow(path);
        editorWindow.Show();  // Show the editor window
        this.Close();  // Close the current window
    }

    private void ProjectsList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var listBox = sender as ListBox;

        // Get the selected folder name
        var selectedFolderName = listBox?.SelectedItem as string;

        if (selectedFolderName == null) return;
        // Find the corresponding full path for the selected folder name
        var path = _folderPaths.Paths.FirstOrDefault(path => Path.GetFileName(path) == selectedFolderName);

        if (path == null) return;
        
        OpenEditorWindow(path);
    }
}