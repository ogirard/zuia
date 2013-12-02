// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyTestsImprovedFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZE.UIA.WPF.Framework;

namespace MyApp.UITest.UITests
{
  /// <summary>
  /// Collection of UI Tests testing functionality provided in MainWindow
  /// </summary>
  [CodedUITest]
  public class MyTestsImprovedFixture : UITestBaseFixture
  {
    /// <summary>
    /// UITest: Clicks the increase button and asserts that the counter is increased.
    /// </summary>
    [TestMethod]
    [WorkItem(8888)]
    public void ClickIncreaseButtonIncreasesCounter()
    {
      TestContext.LogStart();

      // Startup
      Basic.StartupMyApp();

      // Arrange
      var counterBefore = Maps.MainWindow.UIMyAppMainWindowCounterLabelText.NumberValue;

      Timing.WaitSeconds(2);

      // Act
      Maps.MainWindow.UIMyAppMainWindowIncreaseButton.Click();

      Timing.WaitSeconds(2);

      // Assert
      var counterAfter = Maps.MainWindow.UIMyAppMainWindowCounterLabelText.NumberValue;

      TestContext.Log("Assert that counter has been increased");
      Assert.AreEqual(counterBefore + 1, counterAfter);

      // Shutdown
      Basic.ShutdownMyApp();

      TestContext.LogEnd();
    }

    /// <summary>
    /// UITest: Clicks the increase button N times and asserts that the counter is increased by N.
    /// </summary>
    [TestMethod]
    [DataSource("ClickIncreaseButtonManyTimesDataSource")]
    [WorkItem(8889)]
    public void ClickIncreaseButtonIncreasesCounterManyTimes()
    {
      var numberOfIncreases = TestContext.NumberValue("NumberOfIncreases");

      TestContext.LogStart();

      // Startup
      Basic.StartupMyApp();

      // Arrange
      var counterBefore = Maps.MainWindow.UIMyAppMainWindowCounterLabelText.NumberValue;

      // Act
      TestContext.Log("Click increase button {0} times", numberOfIncreases);
      for (var i = 0; i < numberOfIncreases; i++)
      {
        Maps.MainWindow.UIMyAppMainWindowIncreaseButton.Click();
      }

      // Assert
      var counterAfter = Maps.MainWindow.UIMyAppMainWindowCounterLabelText.NumberValue;

      TestContext.Log("Assert that counter has been increased by {0}", numberOfIncreases);
      Assert.AreEqual(counterBefore + numberOfIncreases, counterAfter);

      // Shutdown
      Basic.ShutdownMyApp();

      TestContext.LogEnd();
    }

    /// <summary>
    /// UITest: Turns off the counter and asserts that the increase button is not clickable.
    /// </summary>
    [TestMethod]
    [WorkItem(9999)]
    public void TurnOffCounterDisablesIncreaseButton()
    {
      TestContext.LogStart();

      // Startup
      Basic.StartupMyApp();

      // Check precondition
      TestContext.Log("Assert that counter is on and increase button is enabled");
      Maps.MainWindow.UIMyAppMainWindowPowerButtonCustom.AssertChecked();
      Maps.MainWindow.UIMyAppMainWindowIncreaseButton.AssertEnabled();

      // Act
      TestContext.Log("Turn off counter");
      Maps.MainWindow.UIMyAppMainWindowPowerButtonCustom.IsChecked = false;

      // Assert
      TestContext.Log("Assert that counter is off and increase button is disabled");
      Maps.MainWindow.UIMyAppMainWindowPowerButtonCustom.AssertNotChecked();
      Maps.MainWindow.UIMyAppMainWindowIncreaseButton.AssertDisabled();

      // Shutdown
      Basic.ShutdownMyApp();

      TestContext.LogEnd();
    }
    /// <summary>
    /// UITest: Clicks the increase button and asserts that the counter is increased (fluid example).
    /// </summary>
    [TestMethod]
    [WorkItem(8890)]
    public void ClickIncreaseButtonIncreasesCounterFluid()
    {
      TestContext.LogStart();

      // Startup
      Basic.StartupMyApp();
      int counterBefore, counterAfter;

      // Act
      Maps.MainWindow.ReadCounter(out counterBefore)
                     .ClickButtonIncrease()
                     .ReadCounter(out counterAfter);

      // Assert
      TestContext.Log("Assert that counter has been increased");
      Assert.AreEqual(counterBefore + 1, counterAfter);

      // Shutdown
      Basic.ShutdownMyApp();

      TestContext.LogEnd();
    }
  }
}
