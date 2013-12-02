using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using ZE.UI.WPF.Spy.Common;
using ZE.UI.WPF.Spy.UIA;

namespace ZE.UI.WPF.Spy.Generate
{
  public class UpdateUIMapService
  {
    public const string MapIdPrefix = "MAP_";
    public const string WrapperIdPrefix = "UI";
    public const string WrapperFieldIdPrefix = "_ui";

    #region    Templates

    private const string WindowTitle = "$(WindowTitle)";
    private const string Identifier = "$(Identifier)";
    private const string ControlType = "$(ControlType)";
    private const string SearchProperty = "$(SearchProperty)";
    private const string SearchValue = "$(SearchValue)";
    private const string WrapperType = "$(WrapperType)";
    private const string WrapperField = "$(WrapperField)";
    private const string ElementPath = "$(ElementPath)";

    private const string TopLevelWindowNodeTemplate =
                                 "<TopLevelWindow ControlType=\"Window\" Id=\"$(Identifier)\" FriendlyName=\"$(WindowTitle)\" SpecialControlType=\"None\" SessionId=\"6360196\">" +
                                 "  <TechnologyName>UIA</TechnologyName>" +
                                 "  <WindowTitles>" +
                                 "    <WindowTitle>$(WindowTitle)</WindowTitle>" +
                                 "  </WindowTitles>" +
                                 "  <AndCondition>" +
                                 "    <PropertyCondition Name=\"ControlType\">Window</PropertyCondition>" +
                                 "    <PropertyCondition Name=\"Name\">$(WindowTitle)</PropertyCondition>" +
                                 "    <PropertyCondition Name=\"FrameworkId\">WPF</PropertyCondition>" +
                                 "    <PropertyCondition Name=\"ClassName\" Operator=\"Contains\">HwndWrapper</PropertyCondition>" +
                                 "  </AndCondition>" +
                                 "  <SupportLevel>0</SupportLevel>" +
                                 "  <Descendants/>" +
                                 "</TopLevelWindow>";

    private const string UIObjectNodeTemplate =
                                 "<UIObject ControlType=\"$(ControlType)\" Id=\"$(Identifier)\" FriendlyName=\"$(Identifier)\" SpecialControlType=\"None\">" +
                                 "  <TechnologyName>UIA</TechnologyName>" +
                                 "  <WindowTitles>" +
                                 "    <WindowTitle>$(WindowTitle)</WindowTitle>" +
                                 "  </WindowTitles>" +
                                 "  <AndCondition>" +
                                 "    <PropertyCondition Name=\"ControlType\">$(ControlType)</PropertyCondition>" +
                                 "    <PropertyCondition Name=\"$(SearchProperty)\">$(SearchValue)</PropertyCondition>" +
                                 "  </AndCondition>" +
                                 "  <SupportLevel>0</SupportLevel>" +
                                 "  <Descendants />" +
                                 "</UIObject>";

    private static readonly string PropertyTemplate =
                                 "    " + Environment.NewLine +
                                 "    private $(WrapperType) $(WrapperField);" + Environment.NewLine +
                                 "    " + Environment.NewLine +
                                 "    [GeneratedCode(\"UI Spy\", \"1.0.0.0\")]" + Environment.NewLine +
                                 "    public $(WrapperType) $(Identifier)" + Environment.NewLine +
                                 "    {" + Environment.NewLine +
                                 "      get" + Environment.NewLine +
                                 "      {" + Environment.NewLine +
                                 "        if ($(WrapperField) == null)" + Environment.NewLine +
                                 "        {" + Environment.NewLine +
                                 "          $(WrapperField) = new $(WrapperType)($(ElementPath), this);" + Environment.NewLine +
                                 "        }" + Environment.NewLine +
                                 "    " + Environment.NewLine +
                                 "        return $(WrapperField);" + Environment.NewLine +
                                 "      }" + Environment.NewLine +
                                 "    }";

    #endregion Templates

    private readonly IEnumerable<string> _skipControlTypes = new[] { "" };

    private readonly XmlNamespaceManager _nsManager;

    public UpdateUIMapService()
    {
      _nsManager = new XmlNamespaceManager(new NameTable());
      _nsManager.AddNamespace("ut", "http://schemas.microsoft.com/VisualStudio/TeamTest/UITest/2010");
    }

    public void UpdateMap(UIATreeNode rootNode, UIMap selectedUIMap)
    {
      var windowNode = rootNode.GetNode(n => n.IsChecked);
      if (windowNode == null)
      {
        throw new ArgumentException(@"Tree does not contain any checked window node!", "rootNode");
      }

      var idMap = GenerateXml(windowNode, selectedUIMap.Path);
      GenerateProperties(windowNode, selectedUIMap.GeneratedPath, idMap);
    }

    private IDictionary<UIATreeNode, string> GenerateXml(UIATreeNode windowNode, string uiTestFile)
    {
      var idMap = new Dictionary<UIATreeNode, string>();
      var uiTest = XDocument.Load(uiTestFile);
      var topLevelWindows = uiTest.XPathSelectElement("//ut:TopLevelWindows", _nsManager);
      topLevelWindows.RemoveAll();
      AddWindow(windowNode, topLevelWindows, idMap);
      uiTest.SaveAndFixNamespaces(uiTestFile);

      return idMap;
    }

    private void AddWindow(UIATreeNode windowNode, XContainer topLevelWindows, IDictionary<UIATreeNode, string> idMap)
    {
      var topLevelWindow =
        XElement.Parse(
          TopLevelWindowNodeTemplate.Replace(WindowTitle, windowNode.WindowTitle)
                                    .Replace(Identifier, MapIdPrefix + GetUniqueId(windowNode, idMap)));

      topLevelWindows.Add(topLevelWindow);
      var descendants = topLevelWindow.Element("Descendants");
      foreach (var childNode in windowNode.Children)
      {
        AddUIObject(childNode, descendants, idMap);
      }
    }

    private void AddUIObject(UIATreeNode uiObjectNode, XContainer parentNode, IDictionary<UIATreeNode, string> idMap)
    {
      if (_skipControlTypes.Contains(uiObjectNode.ControlType))
      {
        return;
      }

      var automationId = uiObjectNode.Element.Current.AutomationId;
      var name = uiObjectNode.Element.Current.Name;
      var hasAutomationId = automationId != name;

      var uiObject =
        XElement.Parse(
          UIObjectNodeTemplate.Replace(WindowTitle, uiObjectNode.WindowTitle)
                              .Replace(Identifier, MapIdPrefix + GetUniqueId(uiObjectNode, idMap))
                              .Replace(SearchProperty, hasAutomationId ? "AutomationId" : "Name")
                              .Replace(SearchValue, hasAutomationId ? uiObjectNode.Element.Current.AutomationId : uiObjectNode.Element.Current.Name)
                              .Replace(ControlType, UIAUtil.GetControlType(uiObjectNode.Element)));

      parentNode.Add(uiObject);
      var descendants = uiObject.Element("Descendants");
      foreach (var childNode in uiObjectNode.Children)
      {
        AddUIObject(childNode, descendants, idMap);
      }
    }

    private void GenerateProperties(UIATreeNode windowNode, string generatedFile, IDictionary<UIATreeNode, string> idMap)
    {
      var classText = File.ReadAllText(generatedFile);
      var classBegin = classText.IndexOf("public partial class", StringComparison.Ordinal);
      var classSignature = "public partial class " + Path.GetFileNameWithoutExtension(generatedFile).Replace(".Generated", string.Empty) + " : UIMapBase" + Environment.NewLine
                           + "  {" + Environment.NewLine + "  public MyImprovedUIMap(ITestContextProvider testContextProvider) : base(testContextProvider) { }"
                           + Environment.NewLine;

      var properties = new StringBuilder();
      AddProperty(windowNode, string.Empty, properties, idMap);
      var generatedPropertiesClass = classText.Substring(0, classBegin) + classSignature + properties + @"  }" + Environment.NewLine + @"}";
      File.WriteAllText(generatedFile, generatedPropertiesClass);
    }

    private void AddProperty(UIATreeNode currentNode, string elementPath, StringBuilder properties, IDictionary<UIATreeNode, string> idMap)
    {
      if (_skipControlTypes.Contains(currentNode.ControlType))
      {
        return;
      }

      if (!string.IsNullOrEmpty(elementPath))
      {
        elementPath += ".";
      }
      elementPath += MapIdPrefix + idMap[currentNode];

      properties.AppendLine(PropertyTemplate.Replace(WrapperType, currentNode.UIWrapperType.FullName)
                                            .Replace(Identifier, WrapperIdPrefix + idMap[currentNode])
                                            .Replace(WrapperField, WrapperFieldIdPrefix + idMap[currentNode])
                                            .Replace(ElementPath, elementPath));

      foreach (var childNode in currentNode.Children)
      {
        AddProperty(childNode, elementPath, properties, idMap);
      }
    }

    private static string GetUniqueId(UIATreeNode node, IDictionary<UIATreeNode, string> idMap)
    {
      var identifier = node.Identifier;
      var inUse = idMap.Where(p => p.Value.StartsWith(identifier)).Select(p => p.Value.Replace(identifier, string.Empty)).ToList();

      if (inUse.Any())
      {
        // get highest number suffix for given id
        var maxNr = inUse.Max(n => string.IsNullOrEmpty(n) ? 0 : int.Parse(n));
        identifier = node.Identifier + (maxNr + 1);
      }

      idMap.Add(node, identifier);
      return identifier;
    }
  }
}