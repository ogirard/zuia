// ----------------------------------------------------------------------------------------------------
// <copyright file="ITestContextProvider.cs" company="Zühlke Engineering AG">
//   Copyright (c) Zühlke Engineering AG. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZE.UIA.WPF.Framework
{
  public interface ITestContextProvider
  {
    TestContext TestContext { get; } 
  }
}