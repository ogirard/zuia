using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using ZE.UI.WPF.Spy.Common;
using ZE.UI.WPF.Spy.Generate;
using ZE.UI.WPF.Spy.Properties;
using ZE.UI.WPF.Spy.UIA;

namespace ZE.UI.WPF.Spy
{
  public class MainViewModel : NotificationObject
  {
    private readonly UIAService _uiaService;

    private readonly UpdateUIMapService _updateUIMapService;

    private readonly string _inspectToolPath;

    public MainViewModel()
    {
      _uiaService = new UIAService();
      _updateUIMapService = new UpdateUIMapService();
      _inspectToolPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools\\inspect.exe");

      BuildTreeCommand = new DelegateCommand(BuildTree, () => !string.IsNullOrEmpty(WindowTitle));
      UpdateUIMapCommand = new DelegateCommand(UpdateUIMap, CanUpdateUIMap);
      OpenInspectCommand = new DelegateCommand(OpenInspect, () => File.Exists(_inspectToolPath));

      RootNode = new ObservableCollection<UIATreeNode>();

      // init defaults
      WindowTitle = Settings.Default.TargetApplicationTitle;
      UITestProjectFolder = Settings.Default.UITestProjectRoot;

      // init available UIMaps
      UpdatableUIMaps =
          Directory.GetFiles(UITestProjectFolder, "*.uitest", SearchOption.AllDirectories)
                   .Select(p => new UIMap(UITestProjectFolder, p))
                   .ToList();
    }

    private void UpdateUIMap()
    {
      if (!CanUpdateUIMap())
      {
        return;
      }

      var rootNode = RootNode.First();
      if (!rootNode.IsAnyNodeChecked())
      {
        MessageBox.Show(
          Application.Current.MainWindow,
          "Choose a window node to be updated",
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Warning);
        return;
      }

      try
      {
        _updateUIMapService.UpdateMap(rootNode, SelectedUIMap);
        MessageBox.Show(
          Application.Current.MainWindow,
          string.Format("UIMap '{0}' successfully updated!", SelectedUIMap.Name),
          "Update Successful",
          MessageBoxButton.OK,
          MessageBoxImage.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show(
          Application.Current.MainWindow,
          string.Format(
            "UIMap '{0}' could not be updated!{1}{2}{1}{3}",
            SelectedUIMap.Name,
            Environment.NewLine,
            ex.Message,
            ex.StackTrace),
          "Update Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error);
      }
    }

    private bool CanUpdateUIMap()
    {
      return RootNode.Any() && SelectedUIMap != null && SelectedUIMap.CheckExists();
    }

    private void OpenInspect()
    {
      try
      {
        Process.Start(_inspectToolPath);
      }
      catch
      {
        MessageBox.Show(
          Application.Current.MainWindow,
          string.Format("Cannot open '{0}'...", _inspectToolPath),
          "Error",
          MessageBoxButton.OK,
          MessageBoxImage.Error);
      }
    }

    private string _windowTitle;

    /// <summary>
    /// Gets or sets the WindowTitle.
    /// </summary>
    public string WindowTitle
    {
      get
      {
        return _windowTitle;
      }

      set
      {
        if (_windowTitle != value)
        {
          _windowTitle = value;
          BuildTreeCommand.RaiseCanExecuteChanged();
          NotifyChanged();
        }
      }
    }

    public string UITestProjectFolder { get; private set; }

    public IEnumerable<UIMap> UpdatableUIMaps { get; private set; }

    private UIMap _selectedUIMap;

    /// <summary>
    /// Gets or sets the SelectedUIMap.
    /// </summary>
    public UIMap SelectedUIMap
    {
      get
      {
        return _selectedUIMap;
      }

      set
      {
        if (_selectedUIMap != value)
        {
          _selectedUIMap = value;
          UpdateUIMapCommand.RaiseCanExecuteChanged();
          NotifyChanged();
        }
      }
    }

    public DelegateCommand BuildTreeCommand { get; set; }

    public DelegateCommand UpdateUIMapCommand { get; set; }

    public DelegateCommand OpenInspectCommand { get; set; }

    public ObservableCollection<UIATreeNode> RootNode { get; private set; }

    private UIATreeNode _selectedNode;

    /// <summary>
    /// Gets or sets the SelectedNode.
    /// </summary>
    public UIATreeNode SelectedNode
    {
      get
      {
        return _selectedNode;
      }

      set
      {
        if (_selectedNode != value)
        {
          _selectedNode = value;
          NotifyChanged();
        }
      }
    }

    private void BuildTree()
    {
      RootNode.Clear();
      RootNode.Add(_uiaService.CreateTree(WindowTitle));
      UpdateUIMapCommand.RaiseCanExecuteChanged();
    }
  }
}