// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyImprovedUIMap.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MyApp.UITest.UIMaps.MyImprovedUIMapClasses
{
  public partial class MyImprovedUIMap
  {
    public MyImprovedUIMap ClickButtonIncrease()
    {
      UIMyAppMainWindowIncreaseButton.Click();
      return this;
    }

    public MyImprovedUIMap ReadCounter(out int counter)
    {
      counter = UIMyAppMainWindowCounterLabelText.NumberValue;
      return this;
    }

    public MyImprovedUIMap EnableIncrease()
    {
      UIMyAppMainWindowPowerButtonCustom.IsChecked = true;
      return this;
    }

    public MyImprovedUIMap DisableIncrease()
    {
      UIMyAppMainWindowPowerButtonCustom.IsChecked = false;
      return this;
    }
  }
}

