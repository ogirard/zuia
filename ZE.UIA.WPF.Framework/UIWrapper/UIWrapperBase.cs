// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIWrapperBase.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Automation;

using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZE.UIA.WPF.Framework.UIWrapper
{
  public abstract class UIWrapperBase : ITestContextProvider
  {
    private readonly UITestControl _uiTestControl;

    private readonly ITestContextProvider _testContextProvider;

    private readonly IDictionary<Type, BasePattern> _patternCache = new Dictionary<Type, BasePattern>();

    protected UIWrapperBase(UITestControl uiTestControl, ITestContextProvider testContextProvider)
    {
      if (uiTestControl == null)
      {
        throw new ArgumentNullException("uiTestControl");
      }

      if (testContextProvider == null)
      {
        throw new ArgumentNullException("testContextProvider");
      }

      _uiTestControl = uiTestControl;
      _testContextProvider = testContextProvider;
    }

    public void Click(bool autoLog = true)
    {
      if (autoLog)
      {
        // log click action
        TestContext.Log("Click {0} '{1}'.", TestControl.ControlType.FriendlyName, TestControl != null ? TestControl.Name : "n/a");
      }

      // click middle of control
      Mouse.Click(_uiTestControl, new Point(_uiTestControl.Width / 2, _uiTestControl.Height / 2));

    }

    public void Focus()
    {
      _uiTestControl.SetFocus();
    }

    public void TypeText(string text)
    {

    }

    public AutomationElement AutomationElement
    {
      get
      {
        return (AutomationElement)_uiTestControl.NativeElement;
      }
    }

    public UITestControl TestControl
    {
      get
      {
        return _uiTestControl;
      }
    }

    public TestContext TestContext { get { return _testContextProvider.TestContext; } }

    /// <summary>
    /// Gets the given automation pattern or <tt>null</tt> if not available.
    /// </summary>
    /// <typeparam name="TPattern">The type of the pattern.</typeparam>
    /// <returns></returns>
    /// <exception cref="Microsoft.VisualStudio.TestTools.UITest.Extension.UITestException"></exception>
    public TPattern GetPattern<TPattern>() where TPattern : BasePattern
    {
      var patternType = typeof(TPattern);

      if (_patternCache.ContainsKey(patternType))
      {
        return _patternCache[patternType] as TPattern;
      }

      if (AutomationElement == null)
      {
        throw new UITestException(string.Format(CultureInfo.CurrentCulture, "Automation Element of '{0}' is required!", _uiTestControl.Name));
      }

      var patternField = patternType.GetField("Pattern");
      if (patternField != null && patternField.IsStatic && patternField.IsPublic)
      {
        var pattern = patternField.GetValue(null) as AutomationPattern;
        if (pattern != null)
        {
          try
          {
            var typedPattern = AutomationElement.GetCurrentPattern(pattern) as TPattern;
            if (typedPattern != null)
            {
              _patternCache.Add(patternType, typedPattern);
              return typedPattern;
            }
          }
          catch (Exception)
          {
            return null;
          }
        }
      }

      return null;
    }

    public virtual string Name
    {
      get
      {
        return AutomationElement.Current.Name;
      }
    }

    public bool IsVisible
    {
      get
      {
        return !AutomationElement.Current.IsOffscreen;
      }
    }

    public bool IsEnabled
    {
      get
      {
        return AutomationElement.Current.IsEnabled;
      }
    }

    public void AssertVisible()
    {
      Assert.IsTrue(IsVisible, "Assertion failed, '{0}' should be visible", Name);
    }

    public void AssertInvisible()
    {
      Assert.IsFalse(IsVisible, "Assertion failed, '{0}' should not be visible", Name);
    }

    public void AssertEnabled()
    {
      Assert.IsTrue(IsEnabled, "Assertion failed, '{0}' should be enabled", Name);
    }

    public void AssertDisabled()
    {
      Assert.IsFalse(IsEnabled, "Assertion failed, '{0}' should be disabled", Name);
    }
  }
}