using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "CREDENTIALS", menuName = "Scriptable Objects/CREDENTIALS")]
public class CREDENTIALS : ScriptableObject
{
    [SerializeField] private string _key;
    public string Key
    {
        get
        { 
            return _key == string.Empty || _key == null? 
                null : _key;
        }
        set{_key = value;}
    }

    [SerializeField] private string _targetURL;
    public string URL
    {
        get
        { 
            return _targetURL == string.Empty || _targetURL == null? 
                null : _targetURL;
        }
        set{_targetURL = value;}
    }
}
