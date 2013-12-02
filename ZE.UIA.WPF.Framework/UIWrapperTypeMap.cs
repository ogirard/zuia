// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIWrapperTypeMap.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Automation;

using ZE.UIA.WPF.Framework.UIWrapper;

namespace ZE.UIA.WPF.Framework
{
  /// <summary>
  /// Published UIWrapper types
  /// </summary>
  public static class UIWrapperTypeMap
  {
    private static readonly IDictionary<string, Type> PublishedTypes = new Dictionary<string, Type>();

    private static readonly Type DefaultType;

    static UIWrapperTypeMap()
    {
      // get all UI Wrappers
      foreach (var type in Assembly.GetAssembly(typeof(UIWrapperBase)).GetTypes())
      {
        var uiWrapperAttribute = type.GetCustomAttribute<UIWrapperDescriptionAttribute>();
        if (uiWrapperAttribute != null)
        {
          PublishedTypes.Add(GetKey(uiWrapperAttribute.UIAControlType), type);

          if (uiWrapperAttribute.IsDefault)
          {
            DefaultType = type;
          }
        }
      }
    }

    /// <summary>
    /// Gets the matching <see cref="UIWrapperBase"/> type for the given UIA control type.
    /// </summary>
    /// <param name="automationElement">The uia control.</param>
    /// <returns></returns>
    public static Type GetMatchingWrapper(AutomationElement automationElement)
    {
      if (automationElement == null)
      {
        return null;
      }

      var typeKey = GetKey(automationElement.Current.ControlType.ProgrammaticName);

      if (typeKey == "custom")
      {
        typeKey = GetKey(automationElement.Current.ClassName);
      }

      if (PublishedTypes.ContainsKey(typeKey))
      {
        return PublishedTypes[typeKey];
      }

      return DefaultType;
    }

    private static string GetKey(string uiaControlType)
    {
      if (string.IsNullOrEmpty(uiaControlType))
      {
        throw new ArgumentNullException("uiaControlType");
      }

      if (uiaControlType.Contains("DataGrid"))
      {
        uiaControlType = "Table";
      }

      return uiaControlType.Replace("ControlType.", string.Empty).Trim().ToLowerInvariant();
    }
  }
}