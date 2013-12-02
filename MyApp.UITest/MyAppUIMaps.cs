// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyAppUIMaps.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MyApp.UITest.UIMaps.MyImprovedUIMapClasses;

using ZE.UIA.WPF.Framework;

namespace MyApp.UITest
{
  public class MyAppUIMaps : UIMapRepositoryBase
  {
    public MyAppUIMaps(ITestContextProvider testContextProvider)
      : base(testContextProvider)
    {
    }

    public MyImprovedUIMap MainWindow
    {
      get
      {
        return GetMap<MyImprovedUIMap>();
      }
    }
  }
}