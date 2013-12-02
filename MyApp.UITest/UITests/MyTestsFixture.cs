// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyTestsFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyApp.UITest.UIMaps.MyUIMapClasses;

namespace MyApp.UITest.UITests
{
  /// <summary>
  /// Collection of UI Tests testing functionality provided in MainWindow
  /// </summary>
  [CodedUITest]
  public class MyTestsFixture
  {
    [TestMethod]
    public void ClickIncreaseButtonIncreasesCounter()
    {
      // *** assume app is running

      // Arrange
      var counterBefore = int.Parse(MyUIMap.UIMyAppCounterWindow.UITabControl.UITabItem1.UICounterLabel.DisplayText);

      // Act
      Mouse.Click(MyUIMap.UIMyAppCounterWindow.UITabControl.UITabItem1.UIIncreaseButton);

      // Assert
      var counterAfter = int.Parse(MyUIMap.UIMyAppCounterWindow.UITabControl.UITabItem1.UICounterLabel.DisplayText);
      Assert.AreEqual(counterBefore + 1, counterAfter);
    }

    /// <summary>
    /// Gets or sets the test context which provides information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }

    public MyUIMap MyUIMap
    {
      get
      {
        return _map ?? (_map = new MyUIMap());
      }
    }

    private MyUIMap _map;
  }
}