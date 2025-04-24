using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    [Header("Clips")]
    [Serializefield] private AudioClip _recordedClip;

    [Spave(5f)]
    [SerializeField] private AudioSource _audioSource;

    [Header("Microphone"),Space (5f)]
    [SerializedField] private Microphone _currentMicrophone;
    [SerializedField] private int _selectedMicIndex = 0;
    [SerializedField] private string _microphoneName;

    void OnEnable()
    {
        GetMicrophoneDevices();
    }

    private void GetMicrophoneDevices()
    {
        if(Microphone.devices.Length <= 0)
        {
            Debug.LogError("No microphone devices found.");
            return;
        }

        Debug.Log("Microphone devices found: " + Microphone.devices.Length);
        for (int i = 0; i < Microphone.devices.Length; i++)
            Debug.Log("Microphone " + i + ": " + Microphone.devices[i]);

        _currentMicrophone = Microphone.devices[_selectedMicIndex];
        _micName = _currentMicrophone.name;
    }
}
