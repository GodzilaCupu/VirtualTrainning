using System.Collections.Generic;
using UnityEngine;

public class FixedUpdatePublisher : MonoBehaviour
{
    public static List<IFixedUpdateObserver> _observeMethods = new List<IFixedUpdateObserver>();
    public static List<IFixedUpdateObserver> _pendingObserveMethods = new List<IFixedUpdateObserver>();
    public static int _currentIndex;

    private void Update()
    {
        for (_currentIndex = _observeMethods.Count - 1; _currentIndex >= 0; _currentIndex--)
        {
            _observeMethods[_currentIndex].FixedUpdateObserver();
        }

        _observeMethods.AddRange(_pendingObserveMethods);
        _pendingObserveMethods.Clear();
    }

    public static void RegisterObserver(IFixedUpdateObserver methodToObserve)=> _pendingObserveMethods.Add(methodToObserve);

    public static void UnregisterObserver(IFixedUpdateObserver methodToObserve)
    {
        _observeMethods.Remove(methodToObserve);
        _currentIndex--;
    }
}
