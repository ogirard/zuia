using System;
using System.IO;
using System.Linq;

namespace ZE.UI.WPF.Spy.Generate
{
  public class UIMap
  {
    private readonly string _uiTestProjectFolder;

    public string Path { get; private set; }

    public string Name { get; private set; }

    public string Scope { get; private set; }

    public string DesignerPath
    {
      get
      {
        return Path != null ? Path.Replace(".uitest", ".Designer.cs") : null;
      }
    }

    public string GeneratedPath
    {
      get
      {
        return Path != null ? Path.Replace(".uitest", ".Generated.cs") : null;
      }
    }

    public UIMap(string uiTestProjectFolder, string path)
    {
      _uiTestProjectFolder = uiTestProjectFolder;
      Path = path;
      Name = System.IO.Path.GetFileNameWithoutExtension(Path);
      Scope = path.Replace(uiTestProjectFolder, string.Empty).Split(new[] { System.IO.Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
    }

    public bool CheckExists()
    {
      return File.Exists(Path) && File.Exists(DesignerPath) && File.Exists(GeneratedPath);
    }
  }
}