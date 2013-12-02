// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyTestsLittleImprovedFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyApp.UITest.UIMaps.MyLittleImprovedUIMapClasses;

using ZE.UIA.WPF.Framework;

namespace MyApp.UITest.UITests
{
  /// <summary>
  /// Collection of UI Tests testing functionality provided in MainWindow
  /// </summary>
  [CodedUITest]
  public class MyTestsLittleImprovedFixture : ITestContextProvider
  {
    [TestMethod]
    public void ClickIncreaseButtonIncreasesCounter()
    {
      // *** assume app is running

      // Arrange
      var counterBefore = MyUIMap.CounterLabel.NumberValue;

      // Act
      MyUIMap.IncreaseButton.Click();

      // Assert
      var counterAfter = MyUIMap.CounterLabel.NumberValue;
      Assert.AreEqual(counterBefore + 1, counterAfter);
    }

    /// <summary>
    /// Gets or sets the test context which provides information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }

    public MyLittleImprovedUIMap MyUIMap
    {
      get
      {
        return _map ?? (_map = new MyLittleImprovedUIMap(this));
      }
    }

    private MyLittleImprovedUIMap _map;
  }
}