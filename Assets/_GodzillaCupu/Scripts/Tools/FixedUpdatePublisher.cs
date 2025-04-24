using System.Collections.Generic;
using UnityEngine;

public class FixedUpdatePublisher : MonoBehaviour
{
    [Header("Debugging")]
    public bool TESTINGONLY = false; // Set to true for testing purposes

    public static List<IFixedUpdateObserver> _observeMethods = new List<IFixedUpdateObserver>();
    public static List<IFixedUpdateObserver> _pendingObserveMethods = new List<IFixedUpdateObserver>();
    public static int _currentIndex;

    private void Update()
    {
        if (TESTINGONLY) DEBUGING();
        for (_currentIndex = _observeMethods.Count - 1; _currentIndex >= 0; _currentIndex--)
        {
            _observeMethods[_currentIndex].ObserveFixedUpdate();
            
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

    public void DEBUGING()
    {
        Debug.Log("Current Index: " + _currentIndex);
        Debug.Log("Observe Methods Count: " + _observeMethods.Count);
        Debug.Log("Pending Observe Methods Count: " + _pendingObserveMethods.Count);
        if (_currentIndex >= 0)
        {
            Debug.Log("Current Class Name: " + _observeMethods[_currentIndex].GetType().Name);
            Debug.Log("Current Method Name: " + _observeMethods[_currentIndex].GetType().GetMethod("FixedUpdateObserver").Name);
        }
    }
}
