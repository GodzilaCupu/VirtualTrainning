using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : Singletons<AudioRecorder>
{
    public bool TESTINGONLY = false; // Set to true for testing purposes
    public AudioClip recordedClip;
    [SerializeField]
    private string filePath = "/recordedAudio.wav";
    [SerializeField] bool isRecording = false;


    public override void Awake()
    {
        filePath += Application.persistentDataPath;
        CheckMicrophone();
        CheckClipAvailable();
    }

    private void Update() 
    {
        if(!TESTINGONLY) return;

        if(isRecording) StartRecording();
    }

    private void CheckMicrophone()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone found!");
            return;
        }

        foreach (string device in Microphone.devices)
        {
            Debug.Log("Microphone found: " + device);
        }
        Debug.Log("Using default microphone: " + Microphone.devices[0]);
    }

    private void CreateNewClip()
    {
        if(recordedClip == null)
            recordedClip = AudioClip.Create("RecordedAudio", 16000 * 5, 1, 16000, false); // 5 sec clip
        else
            Debug.LogWarning("AudioClip already exists. Please stop recording before creating a new one.");
    }

    private void CheckClipAvailable()
    {
        if (recordedClip == null)
        {
            CreateNewClip();
            return;
        }

        if (recordedClip.length <= 0)
        {
            Debug.LogError("Audio clip is empty. Please record something.");
            return;
        }
    }

    public void StartRecording()
    {
        recordedClip = Microphone.Start(null, false, 5, 16000); // Record for 5 sec
    }

    public void StopRecording()
    {
        Microphone.End(null);
        Save(filePath, recordedClip);
    }

    public void KillMicrophone() => Microphone.End(null);
    public void StopTesting() => TESTINGONLY = false;   

    void Save(string path, AudioClip clip)
    {
        SavWav.SaveWav(filePath ,clip); // Convert AudioClip to WAV
        Debug.Log("Saved to: " + path);
    }
}
