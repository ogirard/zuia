// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonUIWrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace ZE.UIA.WPF.Framework.UIWrapper
{
  [UIWrapperDescription("Button")]
  public class ButtonUIWrapper : UIWrapperBase
  {
    private readonly WpfButton _button;

    public ButtonUIWrapper(WpfButton button, ITestContextProvider testContextProvider)
      : base(button, testContextProvider)
    {
      _button = button;
    }
  }
}