using System.CodeDom.Compiler;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZE.UIA.WPF.Framework;
using ZE.UIA.WPF.Framework.UIWrapper;

namespace MyApp.UITest.UIMaps.MyImprovedUIMapClasses
{
  public partial class MyImprovedUIMap : UIMapBase
  {
  public MyImprovedUIMap(ITestContextProvider testContextProvider) : base(testContextProvider) { }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiMyAppMainWindow;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UIMyAppMainWindow
    {
      get
      {
        if (_uiMyAppMainWindow == null)
        {
          _uiMyAppMainWindow = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow, this);
        }
    
        return _uiMyAppMainWindow;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiTitleBar;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UITitleBar
    {
      get
      {
        if (_uiTitleBar == null)
        {
          _uiTitleBar = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar, this);
        }
    
        return _uiTitleBar;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiSystemMenuBar;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UISystemMenuBar
    {
      get
      {
        if (_uiSystemMenuBar == null)
        {
          _uiSystemMenuBar = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar.MAP_SystemMenuBar, this);
        }
    
        return _uiSystemMenuBar;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiItem1MenuItem;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UIItem1MenuItem
    {
      get
      {
        if (_uiItem1MenuItem == null)
        {
          _uiItem1MenuItem = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar.MAP_SystemMenuBar.MAP_Item1MenuItem, this);
        }
    
        return _uiItem1MenuItem;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper _uiMinimizeButton;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper UIMinimizeButton
    {
      get
      {
        if (_uiMinimizeButton == null)
        {
          _uiMinimizeButton = new ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar.MAP_MinimizeButton, this);
        }
    
        return _uiMinimizeButton;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper _uiMaximizeButton;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper UIMaximizeButton
    {
      get
      {
        if (_uiMaximizeButton == null)
        {
          _uiMaximizeButton = new ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar.MAP_MaximizeButton, this);
        }
    
        return _uiMaximizeButton;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper _uiCloseButton;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper UICloseButton
    {
      get
      {
        if (_uiCloseButton == null)
        {
          _uiCloseButton = new ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper(MAP_MyAppMainWindow.MAP_TitleBar.MAP_CloseButton, this);
        }
    
        return _uiCloseButton;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiMyAppMainWindowTabControlTabList;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UIMyAppMainWindowTabControlTabList
    {
      get
      {
        if (_uiMyAppMainWindowTabControlTabList == null)
        {
          _uiMyAppMainWindowTabControlTabList = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList, this);
        }
    
        return _uiMyAppMainWindowTabControlTabList;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiMyAppMainWindowTabItem1TabPage;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UIMyAppMainWindowTabItem1TabPage
    {
      get
      {
        if (_uiMyAppMainWindowTabItem1TabPage == null)
        {
          _uiMyAppMainWindowTabItem1TabPage = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage, this);
        }
    
        return _uiMyAppMainWindowTabItem1TabPage;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper _uiNEWTABITEMText;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper UINEWTABITEMText
    {
      get
      {
        if (_uiNEWTABITEMText == null)
        {
          _uiNEWTABITEMText = new ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_NEWTABITEMText, this);
        }
    
        return _uiNEWTABITEMText;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.PowerCheckBoxUIWrapper _uiMyAppMainWindowPowerButtonCustom;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.PowerCheckBoxUIWrapper UIMyAppMainWindowPowerButtonCustom
    {
      get
      {
        if (_uiMyAppMainWindowPowerButtonCustom == null)
        {
          _uiMyAppMainWindowPowerButtonCustom = new ZE.UIA.WPF.Framework.UIWrapper.PowerCheckBoxUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_MyAppMainWindowPowerButtonCustom, this);
        }
    
        return _uiMyAppMainWindowPowerButtonCustom;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper _uiMyAppMainWindowIncreaseButton;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper UIMyAppMainWindowIncreaseButton
    {
      get
      {
        if (_uiMyAppMainWindowIncreaseButton == null)
        {
          _uiMyAppMainWindowIncreaseButton = new ZE.UIA.WPF.Framework.UIWrapper.ButtonUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_MyAppMainWindowIncreaseButton, this);
        }
    
        return _uiMyAppMainWindowIncreaseButton;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper _uiIncreaseText;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper UIIncreaseText
    {
      get
      {
        if (_uiIncreaseText == null)
        {
          _uiIncreaseText = new ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_MyAppMainWindowIncreaseButton.MAP_IncreaseText, this);
        }
    
        return _uiIncreaseText;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper _uiMyAppMainWindowCounterLabelText;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper UIMyAppMainWindowCounterLabelText
    {
      get
      {
        if (_uiMyAppMainWindowCounterLabelText == null)
        {
          _uiMyAppMainWindowCounterLabelText = new ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_MyAppMainWindowCounterLabelText, this);
        }
    
        return _uiMyAppMainWindowCounterLabelText;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper _ui0Text;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper UI0Text
    {
      get
      {
        if (_ui0Text == null)
        {
          _ui0Text = new ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem1TabPage.MAP_MyAppMainWindowCounterLabelText.MAP_0Text, this);
        }
    
        return _ui0Text;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper _uiMyAppMainWindowTabItem2TabPage;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper UIMyAppMainWindowTabItem2TabPage
    {
      get
      {
        if (_uiMyAppMainWindowTabItem2TabPage == null)
        {
          _uiMyAppMainWindowTabItem2TabPage = new ZE.UIA.WPF.Framework.UIWrapper.ControlUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem2TabPage, this);
        }
    
        return _uiMyAppMainWindowTabItem2TabPage;
      }
    }
    
    private ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper _uiANOTHERTABITEMText;
    
    [GeneratedCode("UI Spy", "1.0.0.0")]
    public ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper UIANOTHERTABITEMText
    {
      get
      {
        if (_uiANOTHERTABITEMText == null)
        {
          _uiANOTHERTABITEMText = new ZE.UIA.WPF.Framework.UIWrapper.LabelUIWrapper(MAP_MyAppMainWindow.MAP_MyAppMainWindowTabControlTabList.MAP_MyAppMainWindowTabItem2TabPage.MAP_ANOTHERTABITEMText, this);
        }
    
        return _uiANOTHERTABITEMText;
      }
    }
  }
}