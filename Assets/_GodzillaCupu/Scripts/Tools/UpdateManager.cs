using UnityEngine;

[RequireComponent(typeof(UpdatePublisher))]
[RequireComponent(typeof(FixedUpdatePublisher))]
public class UpdateManager : MonoBehaviour
{
    public UpdatePublisher _updatePublisher;
    public FixedUpdatePublisher _fixedUpdatePublisher;

    private void Awake()
    {
        _updatePublisher = _updatePublisher == null ?  GetComponent<UpdatePublisher>() : _updatePublisher;
        _fixedUpdatePublisher = _fixedUpdatePublisher == null ? GetComponent<FixedUpdatePublisher>() : _fixedUpdatePublisher;
    }
}
