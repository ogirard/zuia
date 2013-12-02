// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlUIWrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting;

namespace ZE.UIA.WPF.Framework.UIWrapper
{
  [UIWrapperDescription("Default", IsDefault = true)]
  public class ControlUIWrapper : UIWrapperBase
  {
    public ControlUIWrapper(UITestControl uiTestControl, ITestContextProvider testContextProvider)
      : base(uiTestControl, testContextProvider)
    {
    }
  }
}
