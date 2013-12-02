using System;

namespace ZE.UIA.WPF.Framework
{
  [AttributeUsage(AttributeTargets.Class)]
  public class UIWrapperDescriptionAttribute : Attribute
  {
    public string UIAControlType { get; private set; }

    public bool IsDefault { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UIWrapperDescriptionAttribute"/> class.
    /// </summary>
    /// <param name="uiaControlType">Type of the UIA control (or class name of custom controls) to be wrapped.</param>
    public UIWrapperDescriptionAttribute(string uiaControlType)
    {
      UIAControlType = uiaControlType;
    }
  }
}