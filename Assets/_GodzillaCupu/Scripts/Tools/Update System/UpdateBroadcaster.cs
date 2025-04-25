using System.Collections.Generic;
using UnityEngine;

public static class UpdateBroadcaster
{
    #region Update
    [Header("Update")]
    public static List<IUpdateObserver> _updateObservers = new List<IUpdateObserver>();
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
        {
            for (_currentUpdateIndex = _updateObservers.Count - 1; _currentUpdateIndex >= 0; _currentUpdateIndex--)
            {
                var observer = _updateObservers[_currentUpdateIndex];
                if (observer == null) continue;
                observer.OnObservedUpdate();
            }

            _updateObservers.AddRange(_pendingUpdateObservers);
            _pendingUpdateObservers.Clear();
        }
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
        {
            for (_currentFixedUpdateIndex = _fixedUpdateObservers.Count - 1; _currentFixedUpdateIndex >= 0; _currentFixedUpdateIndex--)
            {
                var observer = _fixedUpdateObservers[_currentFixedUpdateIndex];
                if (observer == null) continue;
                observer.OnObservedFixedUpdate();
            }

            _fixedUpdateObservers.AddRange(_pendingFixedUpdateObservers);
            _pendingFixedUpdateObservers.Clear();
        }
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
        {
            for (_currentLateUpdateIndex = _lateUpdateObservers.Count - 1; _currentLateUpdateIndex >= 0; _currentLateUpdateIndex--)
            {
                var observer = _lateUpdateObservers[_currentLateUpdateIndex];
                if (observer == null) continue;
                observer.OnObservedLateUpdate();
            }

            _lateUpdateObservers.AddRange(_pendingLateUpdateObservers);
            _pendingLateUpdateObservers.Clear();
        }
    }
    #endregion
}