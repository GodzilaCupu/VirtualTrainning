using System.Collections.Generic;
using UnityEngine;

public class UpdatePublisher : MonoBehaviour
{
    [Header("Debugging")]
    public bool TESTINGONLY = false; // Set to true for testing purposes

    public static List<IUpdateObserver> _observeMethods = new List<IUpdateObserver>();
    public static List<IUpdateObserver> _pendingObserveMethods = new List<IUpdateObserver>();
    public static int _currentIndex;

    private void Update()
    {
        for (_currentIndex = _observeMethods.Count -1 ; _currentIndex >= 0 ; _currentIndex--)
        {
            if (TESTINGONLY) DEBUGING();
            _observeMethods[_currentIndex].ObservedUpdate();
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

    public void DEBUGING()
    {
        Debug.Log("Current Index: " + _currentIndex);
        Debug.Log("Observe Methods Count: " + _observeMethods.Count);
        Debug.Log("Pending Observe Methods Count: " + _pendingObserveMethods.Count);
        if (_currentIndex >= 0)
            Debug.Log("Current Class Name: " + _observeMethods[_currentIndex].GetType().Name);

    }
}
