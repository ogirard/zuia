using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UITesting;

namespace ZE.UIA.WPF.Framework
{
  public static class Timing
  {
    public const int FiveSecondsTimeout = 5000;
    public const int TenSecondsTimeout = 10000;
    public const int TwentySecondsTimeout = 20000;
    public const int OneMinuteTimeout = 60000;
    public const int TwoMinutesTimeout = 120000;
    public const int FourMinutesTimeout = 240000;
    public const int FiveMinutesTimeout = 300000;

    public static readonly TimeSpan FiveSeconds = TimeSpan.FromMilliseconds(FiveSecondsTimeout);
    public static readonly TimeSpan TenSeconds = TimeSpan.FromMilliseconds(TenSecondsTimeout);
    public static readonly TimeSpan TwentySeconds = TimeSpan.FromMilliseconds(TwentySecondsTimeout);
    public static readonly TimeSpan OneMinute = TimeSpan.FromMilliseconds(OneMinuteTimeout);
    public static readonly TimeSpan TwoMinutes = TimeSpan.FromMilliseconds(TwoMinutesTimeout);
    public static readonly TimeSpan FourMinutes = TimeSpan.FromMilliseconds(FourMinutesTimeout);
    public static readonly TimeSpan FiveMinutes = TimeSpan.FromMilliseconds(FiveMinutesTimeout);

    public static readonly int DefaultWaitControlTimeout = 2000;
    public static readonly int DefaultMainWindowStartupTimeout = 75000;

    public static readonly int DefaultSearchTimeoutMilliseconds = 120000;

    public static void WaitSeconds(int seconds)
    {
      int waitTime;

      checked
      {
        waitTime = 1000 * seconds;
      }

      Wait(waitTime);
    }

    #region    Waiting

    /// <summary>
    /// Waits the until the given file exists.
    /// </summary>
    /// <param name="fileInfo">The file to check.</param>
    /// <param name="timeoutMilliseconds">The timeout milliseconds (default is 3000ms).</param>
    /// <returns><tt>true</tt> if file exists, <tt>false</tt> if timeout has expired</returns>
    public static bool WaitExists(this FileInfo fileInfo, int timeoutMilliseconds = 3000)
    {
      if (fileInfo == null)
      {
        return false;
      }

      return WaitUntilConditionMet(() => fileInfo.Exists, timeoutMilliseconds);
    }

    /// <summary>
    /// Waits until the process is ready.
    /// </summary>
    /// <param name="process">The process.</param>
    /// <param name="timeoutMilliseconds">The timeout milliseconds.</param>
    /// <returns><tt>true</tt> if ready, <tt>false</tt> if timeout has expired</returns>
    public static bool WaitUntilReady(this Process process, int timeoutMilliseconds = 1000)
    {
      Wait(timeoutMilliseconds);

      return false;
    }

    /// <summary>
    /// Waits the until the process has exited and then closes it.
    /// </summary>
    /// <param name="process">The process.</param>
    /// <returns>
    ///   <tt>true</tt> if exited, <tt>false</tt> if timeout has expired
    /// </returns>
    public static bool WaitUntilExitedAndClose(this Process process)
    {
      var result = WaitUntilConditionMet(() => process.HasExited);
      process.Close();
      return result;
    }

    /// <summary>
    /// Kill the process without crashing if it does not exist anymore
    /// </summary>
    /// <param name="process">The process.</param>
    [DebuggerHidden]
    public static void SafeKill(this Process process)
    {
      try
      {
        process.Kill();
      }
      catch
      {
        // don't care
      }
    }

    /// <summary>
    /// Waits the until condition met or timeout over.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="timeoutMilliseconds">The timeout milliseconds (default is 2000ms, 0 means forever).</param>
    /// <param name="waitIntervalMilliseconds">The wait interval milliseconds (default is 250ms, minimum of 20ms).</param>
    /// <returns><tt>true</tt> if condition is met, <tt>false</tt> if timeout has expired</returns>
    public static bool WaitUntilConditionMet(Func<bool> condition, int timeoutMilliseconds = 2000, int waitIntervalMilliseconds = 250)
    {
      waitIntervalMilliseconds = Math.Max(waitIntervalMilliseconds, 20);
      var stopWatch = new Stopwatch();
      stopWatch.Start();

      do
      {
        if (condition())
        {
          return true;
        }

        Wait(waitIntervalMilliseconds);
      }
      while (stopWatch.ElapsedMilliseconds < timeoutMilliseconds || timeoutMilliseconds == 0);

      stopWatch.Stop();
      return false;
    }

    private static Action<int> _waitAction = timeout => Playback.Wait(timeout);

    /// <summary>
    /// Waits the specified timeout milliseconds.
    /// </summary>
    /// <param name="timeoutMilliseconds">The timeout milliseconds.</param>
    public static void Wait(int timeoutMilliseconds = 2000)
    {
      try
      {
        _waitAction(timeoutMilliseconds);
      }
      catch (FileNotFoundException)
      {
        // replace wait action with normal Thread.Sleep if Playback.Wait is not available
        _waitAction = Thread.Sleep;
        Thread.Sleep(timeoutMilliseconds);
      }
    }

    /// <summary>
    /// Executes the given action without waiting for any controls to exist
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public static TResult NoSearchTime<TResult>(Func<TResult> action)
    {
      Playback.PlaybackSettings.SearchTimeout = 10;
      var result = action();
      Playback.PlaybackSettings.SearchTimeout = DefaultSearchTimeoutMilliseconds;
      return result;
    }

    #endregion Waiting
  }
}