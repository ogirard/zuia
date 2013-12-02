using System;
using System.Collections.Generic;
using System.Linq;

namespace ZE.UIA.WPF.Framework
{
  public class UIMapRepositoryBase
  {
    private readonly ITestContextProvider _testContextProvider;

    private readonly IDictionary<Type, object> _mapCache = new Dictionary<Type, object>();

    protected UIMapRepositoryBase(ITestContextProvider testContextProvider)
    {
      if (testContextProvider == null)
      {
        throw new ArgumentNullException("testContextProvider");
      }

      _testContextProvider = testContextProvider;
    }

    /// <summary>
    /// Gets the map of given type. Lazy instantiation and caching.
    /// </summary>
    /// <typeparam name="TUIMap">The type of the UI map.</typeparam>
    /// <returns></returns>
    protected TUIMap GetMap<TUIMap>() where TUIMap : UIMapBase
    {
      var key = typeof(TUIMap);
      if (!_mapCache.ContainsKey(key))
      {
        var constructor = key.GetConstructors()
          .FirstOrDefault(c => c.IsPublic && c.GetParameters().Count() == 1 && c.GetParameters().First().ParameterType == typeof(ITestContextProvider));
        if (constructor == null)
        {
          return null;
        }

        _mapCache.Add(key, Activator.CreateInstance(key, _testContextProvider));
      }

      return (TUIMap)_mapCache[key];
    }

    /// <summary>
    /// Resets all created UIMaps.
    /// </summary>
    public void Reset()
    {
      _mapCache.Clear();
    }
  }
}