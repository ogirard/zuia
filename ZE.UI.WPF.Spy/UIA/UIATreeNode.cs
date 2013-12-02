using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Automation;

using ZE.UI.WPF.Spy.Common;
using ZE.UIA.WPF.Framework;

namespace ZE.UI.WPF.Spy.UIA
{
  public class UIATreeNode : NotificationObject
  {
    private readonly UIATreeNode _parent;

    private readonly AutomationElement _automationElement;

    private readonly IList<UIATreeNode> _children;

    public UIATreeNode(UIATreeNode parent, AutomationElement automationElement)
    {
      _parent = parent;
      _automationElement = automationElement;

      _children = new List<UIATreeNode>();
      IsChecked = false;
    }

    public void AddChild(UIATreeNode childNode)
    {
      _children.Add(childNode);
    }

    public UIATreeNode Parent
    {
      get
      {
        return _parent;
      }
    }

    public AutomationElement Element
    {
      get
      {
        return _automationElement;
      }
    }

    public IEnumerable<UIATreeNode> Children
    {
      get
      {
        return _children;
      }
    }

    public string WindowTitle { get; set; }

    public string DisplayLabel { get; set; }

    public string ControlType
    {
      get
      {
        return UIAUtil.GetControlType(_automationElement).ToLower(CultureInfo.CurrentCulture);
      }
    }

    public bool IsWindow
    {
      get
      {
        return ControlType == "window";
      }
    }

    private bool _isChecked;

    /// <summary>
    /// Gets or sets the IsChecked.
    /// </summary>
    public bool IsChecked
    {
      get
      {
        return _isChecked;
      }

      set
      {
        if (_isChecked != value)
        {
          _isChecked = value;
          NotifyChanged();

          if (!value)
          {
            foreach (var childNode in Children)
            {
              childNode.IsChecked = false;
            }
          }
        }
      }
    }

    private bool _isSelected;

    /// <summary>
    /// Gets or sets the IsSelected.
    /// </summary>
    public bool IsSelected
    {
      get
      {
        return _isSelected;
      }

      set
      {
        if (_isSelected != value)
        {
          _isSelected = value;
          NotifyChanged();
        }
      }
    }

    public Type UIWrapperType
    {
      get
      {
        return UIWrapperTypeMap.GetMatchingWrapper(Element);
      }
    }

    public string Identifier
    {
      get
      {
        return UIAUtil.GetIdentifier(Element);
      }
    }

    public bool IsAnyNodeChecked()
    {
      return IsChecked || Children.Any(c => c.IsAnyNodeChecked());
    }

    public UIATreeNode GetNode(Func<UIATreeNode, bool> condition)
    {
      return condition(this) ? this : Children.Select(childNode => childNode.GetNode(condition)).FirstOrDefault(node => node != null);
    }
  }
}