using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZE.UIA.WPF.Framework;

namespace MyApp.UITest.SharedSteps
{
  public class BasicSharedSteps
  {
    private readonly UITestBaseFixture _fixture;

    private readonly string _processName;

    public TestContext TestContext { get { return _fixture.TestContext; } }

    public MyAppUIMaps Maps { get { return _fixture.Maps; } }

    public BasicSharedSteps(UITestBaseFixture fixture)
    {
      _fixture = fixture;
      _processName = Path.GetFileNameWithoutExtension(TestEnvironment.Get(TestEnvironment.TestVariable.TargetApplicationExecutable));
    }

    public void StartupMyApp()
    {
      // make sure, it's not running already
      KillMyApp();

      // startup my app
      TestContext.Log("Startup MyApp");
      Process.Start(TestEnvironment.Get(TestEnvironment.TestVariable.TargetApplicationExecutable));
      
      Timing.WaitSeconds(2);
      Timing.WaitUntilConditionMet(() => Maps.MainWindow.UIMyAppMainWindow.TestControl.Exists);
    }

    public void ShutdownMyApp()
    {
      TestContext.Log("Shutdown MyApp");
      Maps.MainWindow.UICloseButton.Click();
      Timing.WaitSeconds(2);

      // make sure, it's really shut down
      KillMyApp();
    }

    public void KillMyApp()
    {
      Process.GetProcesses().FirstOrDefault(p => p.ProcessName == _processName).SafeKill();
    }
  }
}