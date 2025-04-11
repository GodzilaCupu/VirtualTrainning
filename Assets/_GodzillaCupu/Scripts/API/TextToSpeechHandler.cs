using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Android.Gradle.Manifest;
public class TextToSpeechHandler : MonoBehaviour
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

    private AudioSource _source;


    public void Speak(string text)
    {
        StartCoroutine(PostRequestAudio(text));
    }

    private IEnumerator PostRequestAudio(string text)
    {
        string url = _targetURL(_data);
        Debug.Log($"[API] Data target URL {url}");
        //Prepare The Data with JSON Format
        string json = "{\"input\":{\"text\":\"" + text + "\"},\"voice\":{\"languageCode\":\"id-ID\",\"name\":\"id-ID-Wavenet-D\",\"ssmlGender\":\"MALE\"},\"audioConfig\":{\"audioEncoding\":\"MP3\"}}";
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // string jsonResponse = request.downloadHandler.text;
            // string base64Audio = JsonUtility.FromJson<AudioResponse>(jsonResponse).audioContent;
            // byte[] audioBytes = System.Convert.FromBase64String(base64Audio);

            // AudioClip audioClip = WavUtility.ToAudioClip(audioBytes);
            // audioSource.clip = audioClip;
            // audioSource.Play();
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
