using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Android.Gradle.Manifest;

public class SpeechToTextHandler : MonoBehaviour
{
    [SerializeField] private CREDENTIALS _data;
    private string _targetURL(CREDENTIALS data)
    {
        if (data == null || data.URL == string.Empty)
        {
            Debug.LogError("Your Credentials is Empty");
            return string.Empty;
        }

        if (data.Key == string.Empty)
        {
            Debug.LogError("Your Key is Empty");
            return string.Empty;
        }

        return data.URL += data.Key;
    }
    public void SendAudioToGoogle(byte[] audioData)
    {
        StartCoroutine(PostRequest(audioData));
    }

    private IEnumerator PostRequest(byte[] audioData)
    {
        string url = _targetURL(_data);
        Debug.Log($"[API] Data target URL {url}");
        string base64Audio = System.Convert.ToBase64String(audioData);

        string json = "{\"config\":{\"encoding\":\"LINEAR16\",\"sampleRateHertz\":16000,\"languageCode\":\"id-ID\"},\"audio\":{\"content\":\"" + base64Audio + "\"}}";
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }
}
