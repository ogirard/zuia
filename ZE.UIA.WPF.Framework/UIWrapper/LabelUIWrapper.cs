// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelUIWrapper.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace ZE.UIA.WPF.Framework.UIWrapper
{
  [UIWrapperDescription("Text")]
  public class LabelUIWrapper : UIWrapperBase
  {
    private readonly WpfText _label;

    public LabelUIWrapper(WpfText label, ITestContextProvider testContextProvider)
      : base(label, testContextProvider)
    {
      _label = label;
    }

    public string Value
    {
      get
      {
        return _label.DisplayText;
      }
    }

    public int NumberValue
    {
      get
      {
        return int.Parse(Value);
      }
    }
  }
}