using System.Collections.Generic;
using UnityEngine;

public static class UpdateBroadcaster
{
    #region Update
    [Header("Update")]
    public  static List<IUpdateObserver> _updateObservers = new List<IUpdateObserver>();
    public static List<IUpdateObserver> _pendingUpdateObservers = new List<IUpdateObserver>();
    public static int _currentUpdateIndex;

    public static void Register(IUpdateObserver observer) => _pendingUpdateObservers.Add(observer);

    public static void Unregister(IUpdateObserver observer)
    {
        _updateObservers.Remove(observer);
        _currentUpdateIndex--;
    }

    public static void Update()
    {
        if (_updateObservers.Count > 0)
            foreach (var observer in _updateObservers)
                observer.OnObservedUpdate();
    }
    #endregion

    #region Fixed Update
    [Header("Fixed Update")]
    public static List<IFixedUpdateObserver> _fixedUpdateObservers = new List<IFixedUpdateObserver>();
    public static List<IFixedUpdateObserver> _pendingFixedUpdateObservers = new List<IFixedUpdateObserver>();
    public static int _currentFixedUpdateIndex;

    public static void Register(IFixedUpdateObserver observer) => _pendingFixedUpdateObservers.Add(observer);

    public static void Unregister(IFixedUpdateObserver observer)
    {
        _fixedUpdateObservers.Remove(observer);
        _currentFixedUpdateIndex--;
    }

    public static void FixedUpdate()
    {
        if (_fixedUpdateObservers.Count > 0)
            foreach (var observer in _fixedUpdateObservers)
                observer.OnObservedFixedUpdate();
    }
    #endregion

    #region Late Update
    [Header("Late Update")]
    public static List<ILateUpdateObserver> _lateUpdateObservers = new List<ILateUpdateObserver>(); 
    public static List<ILateUpdateObserver> _pendingLateUpdateObservers = new List<ILateUpdateObserver>();
    public static int _currentLateUpdateIndex;

    public static void Register(ILateUpdateObserver observer) => _pendingLateUpdateObservers.Add(observer);

    public static void Unregister(ILateUpdateObserver observer)
    {
        _lateUpdateObservers.Remove(observer);
        _currentLateUpdateIndex--;
    }

    public static void LateUpdate()
    {
        if (_lateUpdateObservers.Count > 0)
            foreach (var observer in _lateUpdateObservers)
                observer.OnObservedLateUpdate();
    }
    #endregion
}