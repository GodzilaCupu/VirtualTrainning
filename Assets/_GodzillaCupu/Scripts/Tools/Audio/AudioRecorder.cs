using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AudioRecorder : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip _recordedClip;
    [SerializeField] private float _recordingDurations = 5f;

    [Space(5f)]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _isRecording = false;

    [Header("Microphone"),Space (5f)]
    [SerializeField] private int _selectedMicIndex = 0;
    [SerializeField] private string _currentMicrophoneName;

    void Awake()
    {
        _audioSource = _audioSource == null ? GetComponent<AudioSource>() : _audioSource;
    }
    
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
        
        _currentMicrophoneName = _selectedMicIndex > Microphone.devices.Length ? Microphone.devices[0] : Microphone.devices[_selectedMicIndex];
    }

    public void StartRecording()
    {
        if (_currentMicrophoneName == null || _currentMicrophoneName == string.Empty)
        {
            Debug.LogError("No microphone selected.");
            return;
        }

        Debug.Log("Recording started on: " + _currentMicrophoneName);
        _recordedClip = Microphone.Start(_currentMicrophoneName, true, (int)_recordingDurations, 44100);
        _isRecording = true;
        StartCoroutine(WaitForRecordingToEnd(_recordingDurations));
    }

    public IEnumerator WaitForRecordingToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopRecording();
    }

    public void StopRecording()
    {
        if(!_isRecording) return;

        Microphone.End(_currentMicrophoneName);
        _isRecording = false;
        Debug.Log("Recording stopped on: " + _currentMicrophoneName);
    }
    
    public void PlayRecordedClip()
    {
        if (_recordedClip == null)
        {
            Debug.LogError("No recorded clip found.");
            return;
        }

        _audioSource.clip = _recordedClip;
        _audioSource.Play();
    }
}