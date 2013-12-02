using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ZE.UI.WPF.Spy.Common
{
  public class TreeViewSelectionBehavior : Behavior<TreeView>
  {
    /// <summary>
    /// The SelectedItem dependency property
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewSelectionBehavior), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the selected tree item.
    /// </summary>
    public object SelectedItem
    {
      get
      {
        return GetValue(SelectedItemProperty);
      }

      set
      {
        SetValue(SelectedItemProperty, value);
      }
    }

    protected override void OnAttached()
    {
      base.OnAttached();

      if (AssociatedObject != null)
      {
        AssociatedObject.SelectedItemChanged += SelectedTreeItemChangedHandler;
      }
    }

    private void SelectedTreeItemChangedHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
      SelectedItem = AssociatedObject.SelectedItem;
    }
  }
}