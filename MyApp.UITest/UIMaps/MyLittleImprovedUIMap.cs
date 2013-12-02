// ----------------------------------------------------------------------------------------------------
// <copyright file="MyLittleImprovedUIMap.cs" company="Zühlke Engineering AG">
//     Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------------------------

using ZE.UIA.WPF.Framework;
using ZE.UIA.WPF.Framework.UIWrapper;

namespace MyApp.UITest.UIMaps.MyLittleImprovedUIMapClasses
{
  public partial class MyLittleImprovedUIMap : UIMapBase
  {
    public MyLittleImprovedUIMap(ITestContextProvider testContextProvider)
      : base(testContextProvider)
    {
    }

    private LabelUIWrapper _counterLabel;

    public LabelUIWrapper CounterLabel
    {
      get
      {
        return _counterLabel ?? (_counterLabel = new LabelUIWrapper(UIMyAppCounterWindow.UITabControl.UITabItem1.UICounterLabel, this));
      }
    }
    private ButtonUIWrapper _increaseButton;

    public ButtonUIWrapper IncreaseButton
    {
      get
      {
        return _increaseButton ?? (_increaseButton = new ButtonUIWrapper(UIMyAppCounterWindow.UITabControl.UITabItem1.UIIncreaseButton, this));
      }
    }
  }
}
