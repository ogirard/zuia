//-----------------------------------------------------------------------
// <copyright file="UIAUtil.cs" company="Crypto">
//     Copyright (c) Crypto. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Automation;

namespace ZE.UI.WPF.Spy.UIA
{
  public static class UIAUtil
  {
    /// <summary>
    /// Finds a window by title.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <returns></returns>
    public static AutomationElement FindWindowByTitle(string title)
    {
      var condition = new PropertyCondition(AutomationElement.NameProperty, title);
      var element = AutomationElement.RootElement.FindFirst(TreeScope.Children, condition);
      return element;
    }

    /// <summary>
    /// Gets the display label of the given AutomationElement.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    public static string GetDisplayLabel(AutomationElement element)
    {
      if (element == null)
      {
        return "<null>";
      }

      return GetLabel(element) + " [" + GetControlType(element) + "]";
    }

    private static string GetLabel(AutomationElement element)
    {
      if (!string.IsNullOrEmpty(element.Current.Name))
      {
        return element.Current.Name;
      }

      if (element.Current.LabeledBy != null)
      {
        return GetLabel(element.Current.LabeledBy);
      }

      return "<noname>";
    }

    /// <summary>
    /// Gets the FriendlyName of the given AutomationElement.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    public static string GetFriendlyName(AutomationElement element)
    {
      if (!string.IsNullOrEmpty(element.Current.AutomationId))
      {
        return element.Current.AutomationId;
      }

      if (!string.IsNullOrEmpty(element.Current.Name))
      {
        return element.Current.Name;
      }

      return string.Empty;
    }

    private static readonly string[] SkipParts = new[]
      {
        "ZE", "model", "platform", "framework", "utilities", "userinterface", "module", "modules", "test", "tests",
        "wpf", "uiservices", "uiservice", "interfaces", "services", "message", "data", "launcher", 
      };

    /// <summary>
    /// Gets the Identifier of the given AutomationElement.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    public static string GetIdentifier(AutomationElement element)
    {
      var identifier = string.Empty;

      if (string.IsNullOrEmpty(element.Current.AutomationId))
      {
        identifier = CleanIdentifier(element.Current.Name).CapitalizeFirstLetter();
      }
      else
      {
        var identifierParts = element.Current.AutomationId.Split('.');

        foreach (var part in identifierParts)
        {
          if (!SkipParts.Contains(part.ToLowerInvariant()))
          {
            identifier += part;
          }
        }
      }

      var controlType = GetControlType(element);

      if (identifier.EndsWith(controlType, StringComparison.CurrentCultureIgnoreCase))
      {
        identifier = identifier.Substring(0, identifier.Length - controlType.Length);
      }

      identifier += controlType;

      return CleanIdentifier(identifier).CapitalizeFirstLetter();
    }

    private static string CleanIdentifier(string identifier)
    {
      if (string.IsNullOrEmpty(identifier))
      {
        return identifier;
      }

      var cleanIdentifier = string.Empty;

      foreach (var c in identifier)
      {
        if (char.IsLetterOrDigit(c) || c == '_')
        {
          cleanIdentifier += Translate(c);
        }
      }

      return cleanIdentifier;
    }

    private static char Translate(char c)
    {
      switch (c)
      {
        case 'ü':
        case 'û':
        case 'ù':
        case 'ú':
          return 'u';
        case 'ä':
        case 'à':
        case 'á':
        case 'â':
          return 'a';
        case 'ö':
        case 'ô':
          return 'o';
        case 'î':
          return 'i';
        case 'é':
        case 'è':
          return 'e';
      }

      return c;
    }

    /// <summary>
    /// Gets the ControlType of the given AutomationElement.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    public static string GetControlType(AutomationElement element)
    {
      var controlType = element.Current.ControlType.ProgrammaticName;

      if (controlType.Contains("DataGrid"))
      {
        controlType = "Table";
      }

      controlType = controlType.Replace("ControlType.", string.Empty).Trim();
      return MapControlType(controlType);
    }

    /// <summary>
    /// Maps control types to more suitable types (where needed).
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    public static string MapControlType(string controlType)
    {
      switch (controlType.ToLowerInvariant())
      {
        case "tabitem":
          return "TabPage";
        case "tab":
          return "TabList";
      }

      return controlType;
    }

    /// <summary>
    /// Gets the ControlType of the given AutomationElement.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns></returns>
    public static string GetControlName(AutomationElement element)
    {

      var name = element.Current.Name;
      return name;
    }

    /// <summary>
    /// Capitalizes the first letter of the given string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns></returns>
    public static string CapitalizeFirstLetter(this string str)
    {
      if (!string.IsNullOrEmpty(str))
      {
        return str[0].ToString().ToUpper(CultureInfo.CurrentCulture) + str.Substring(1);
      }

      return str;
    }

    public static string SpyVersion
    {
      get
      {
        return "1.0.0.0";
      }
    }
  }
}