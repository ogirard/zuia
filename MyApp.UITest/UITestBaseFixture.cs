// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UITestBaseFixture.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyApp.UITest.SharedSteps;

using ZE.UIA.WPF.Framework;

namespace MyApp.UITest
{
  public class UITestBaseFixture : ITestContextProvider
  {
    /// <summary>
    /// This property is set automatically when test case is run
    /// </summary>
    public TestContext TestContext { get; set; }

    private MyAppUIMaps _maps;

    public MyAppUIMaps Maps
    {
      get
      {
        return _maps ?? (_maps = new MyAppUIMaps(this));
      }
    }

    private BasicSharedSteps _basicSharedSteps;

    public BasicSharedSteps Basic
    {
      get
      {
        return _basicSharedSteps ?? (_basicSharedSteps = new BasicSharedSteps(this));
      }
    }

    [TestInitialize]
    public virtual void Initialize()
    {
    }

    [TestCleanup]
    public virtual void CleanUp()
    {
    }
  }
}