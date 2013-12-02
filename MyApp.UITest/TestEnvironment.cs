// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEnvironment.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace MyApp.UITest
{
  public static class TestEnvironment
  {
    public enum TestVariable
    {
      TargetApplicationFolder,

      TargetApplicationExecutable,

      TestDataFolder
    }

    public enum TestFlag
    {
      Reporting
    }

    private static readonly IDictionary<TestVariable, string> Settings = new Dictionary<TestVariable, string>();
    private static readonly IDictionary<TestFlag, bool> Flags = new Dictionary<TestFlag, bool>();

    static TestEnvironment()
    {
      // init settings
      /*
       * TODO: adapt folder paths here and in the MyApp.UITest\App.config and ZE.UI.WPF.Spy\App.config !!!
       */
      Settings.Add(TestVariable.TargetApplicationFolder, @"O:\Dropbox\Zühlke\UIA\Mettler Toledo\MyApp\MyApp\bin\Debug\");
      Settings.Add(TestVariable.TargetApplicationExecutable, Path.Combine(Settings[TestVariable.TargetApplicationFolder], "MyApp.exe"));
      Settings.Add(TestVariable.TestDataFolder, Path.Combine(Settings[TestVariable.TargetApplicationFolder], @"TestData\"));

      // set flags
      Flags.Add(TestFlag.Reporting, false);
    }

    public static string Get(TestVariable testVariable)
    {
      if (Settings.ContainsKey(testVariable))
      {
        return Settings[testVariable];
      }

      throw new ArgumentException(string.Format("TestVariable {0} not found", testVariable), "testVariable");
    }

    public static bool Get(TestFlag testFlag)
    {
      if (Flags.ContainsKey(testFlag))
      {
        return Flags[testFlag];
      }

      throw new ArgumentException(string.Format("TestFlag {0} not found", testFlag), "testFlag");
    }
  }
}