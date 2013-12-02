using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Automation;

namespace ZE.UIA.WPF.Framework.UIWrapper
{
  [UIWrapperDescription("PowerButton")]
  public class PowerCheckBoxUIWrapper : UIWrapperBase
  {
    private readonly WpfCustom _powerCheckBox;

    private TogglePattern TogglePattern
    {
      get
      {
        return GetPattern<TogglePattern>();
      }
    }

    public PowerCheckBoxUIWrapper(WpfCustom powerCheckBox, ITestContextProvider testContextProvider)
      : base(powerCheckBox, testContextProvider)
    {
      _powerCheckBox = powerCheckBox;
    }

    public bool IsChecked
    {
      get
      {
        return TogglePattern.Current.ToggleState == ToggleState.On;
      }

      set
      {
        if (value != IsChecked)
        {
          TogglePattern.Toggle();
        }
      }
    }

    public void AssertChecked()
    {
      Assert.IsTrue(IsChecked, "Assertion failed, '{0}' should be checked", Name);
    }

    public void AssertNotChecked()
    {
      Assert.IsFalse(IsChecked, "Assertion failed, '{0}' should not be checked", Name);
    }
  }
}