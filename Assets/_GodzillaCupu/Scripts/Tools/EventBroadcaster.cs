using System;
using System.Collections.Generic;

public static class EventBroadcaster<T>
{
    private static List<Action<T>> _activeObservers = new List<Action<T>>();

    public static List<Action<T>> _pendingObservers = new List<Action<T>>();
    public static int _currentIndex;

    public static void RegisterObserver(Action<T> listener) => _pendingObservers.Add(listener);

    public static void UnregisterObserver(Action<T> listener)
    {
        _activeObservers.Remove(listener);
        _currentIndex--;
    }

    public static void Broadcast(T data)
    {
        if(_activeObservers.Count > 0)
        {
            for (_currentIndex = _activeObservers.Count - 1; _currentIndex >= 0; _currentIndex--)
            {
                var observer = _activeObservers[_currentIndex];
                if (observer == null) continue;
                observer?.Invoke(data);
            }

            _activeObservers.AddRange(_pendingObservers);
            _pendingObservers.Clear();
        }
    }
}
