using System.Collections.Generic;
using UnityEngine;

public class UpdatePublisher : MonoBehaviour
{
    public static List<IUpdateObserver> _observeMethods = new List<IUpdateObserver>();
    public static List<IUpdateObserver> _pendingObserveMethods = new List<IUpdateObserver>();
    public static int _currentIndex;

    private void Update()
    {
        for (_currentIndex = _observeMethods.Count -1 ; _currentIndex >= 0 ; _currentIndex--)
        {
            _observeMethods[_currentIndex].UpdateObserver();
        }

        _observeMethods.AddRange(_pendingObserveMethods);
        _pendingObserveMethods.Clear();
    }

    public static void RegisterObserver(IUpdateObserver observer) => _pendingObserveMethods.Add(observer);

    public static void UnregisterObserver(IUpdateObserver observer)
    {
        _observeMethods.Remove(observer);
        _currentIndex--;
    }
}
