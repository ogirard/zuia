// ----------------------------------------------------------------------------------------------------
// <copyright file="UIMapBase.cs" company="Zühlke Engineering AG">
//   Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZE.UIA.WPF.Framework
{
  public abstract class UIMapBase : ITestContextProvider
  {
    private readonly ITestContextProvider _testContextProvider;

    public TestContext TestContext { get { return _testContextProvider.TestContext; } }

    protected UIMapBase(ITestContextProvider testContextProvider)
    {
      if (testContextProvider == null)
      {
        throw new ArgumentNullException("testContextProvider");
      }

      _testContextProvider = testContextProvider;
    }
  }
}