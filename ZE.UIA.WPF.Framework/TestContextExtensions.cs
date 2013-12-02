using System;
using System.Data;
using System.Globalization;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZE.UIA.WPF.Framework
{
  public static class TestContextExtensions
  {
    public const string LogTimestampFormat = "dd.MM.yyyy HH:mm:ss.fff";

    /// <summary>
    /// Logs the start of a test case.
    /// </summary>
    /// <param name="context">The context.</param>
    public static void LogStart(this TestContext context)
    {
      if (context == null)
      {
        return;
      }

      Log(context, "Test {0}.{1} started", context.FullyQualifiedTestClassName, context.TestName);

      string data = null;
      if (context.DataRow != null && context.DataRow.Table.Columns.Count > 0)
      {
        data = "{" + string.Join(", ", context.DataRow.Table.Columns.OfType<DataColumn>().Select(c => c.ColumnName + " : \"" + context.DataRow[c.ColumnName] + "\"")) + "}";
      }

      Log(context, "Test Data: {0}", data ?? "No parameter data");
    }

    /// <summary>
    /// Logs the end of a test case.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="message">The message (optional).</param>
    public static void LogEnd(this TestContext context, string message = null)
    {
      if (context == null)
      {
        return;
      }

      var formattedMessage = string.IsNullOrEmpty(message)
                               ? string.Empty
                               : string.Format(CultureInfo.CurrentCulture, ", {0}", message);

      Log(context, "Test {0}.{1} ended{2}", context.FullyQualifiedTestClassName, context.TestName, formattedMessage);
    }

    /// <summary>
    /// Logs the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="format">The message format.</param>
    /// <param name="args">The format arguments.</param>
    public static void Log(this TestContext context, string format, params object[] args)
    {
      if (context == null)
      {
        return;
      }

      context.WriteLine("{0}: {1}", DateTime.Now.ToString(LogTimestampFormat), string.Format(CultureInfo.InvariantCulture, format, args));
    }

    public static bool HasValue(this TestContext context, string columnName)
    {
      return context != null && context.DataRow != null && !string.IsNullOrEmpty(columnName) && context.DataRow.Table.Columns.Contains(columnName);
    }

    public static string Value(this TestContext testContext, string column)
    {
      return testContext.DataRow[column] as string;
    }

    public static int NumberValue(this TestContext testContext, string column)
    {
      return int.Parse(Value(testContext, column));
    }

    public static bool BoolValue(this TestContext testContext, string column)
    {
      return bool.Parse(Value(testContext, column));
    }
  }
}