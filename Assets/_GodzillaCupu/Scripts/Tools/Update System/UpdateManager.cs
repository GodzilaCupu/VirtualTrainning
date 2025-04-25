using UnityEngine;
public class UpdateManager : MonoBehaviour
{
    private void Update() => UpdateBroadcaster.Update();

    private void FixedUpdate() => UpdateBroadcaster.FixedUpdate();

    private void LateUpdate() => UpdateBroadcaster.LateUpdate();
}
