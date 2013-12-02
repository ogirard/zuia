using System.Windows.Automation;

namespace ZE.UI.WPF.Spy.UIA
{
  public class UIAService
  {
    public UIATreeNode CreateTree(string windowTitle)
    {
      var windowElement = UIAUtil.FindWindowByTitle(windowTitle);
      if (windowElement == null)
      {
        return null;
      }

      var root = new UIATreeNode(null, windowElement)
                   {
                     DisplayLabel = UIAUtil.GetDisplayLabel(windowElement),
                     WindowTitle = windowTitle
                   };

      return CreateTree(windowTitle, root);
    }

    private static UIATreeNode CreateTree(string windowTitle, UIATreeNode currentRoot)
    {
      var viewWalker = TreeWalker.ControlViewWalker;

      var currentElement = viewWalker.GetFirstChild(currentRoot.Element);

      while (currentElement != null)
      {
        var childNode = new UIATreeNode(currentRoot, currentElement)
        {
          DisplayLabel = UIAUtil.GetDisplayLabel(currentElement),
          WindowTitle = windowTitle
        };

        currentRoot.AddChild(childNode);

        CreateTree(windowTitle, childNode);
        currentElement = viewWalker.GetNextSibling(currentElement);
      }

      return currentRoot;
    }

    public bool CheckWindowExists(string windowTitle)
    {
      return UIAUtil.FindWindowByTitle(windowTitle) != null;
    }
  }
}