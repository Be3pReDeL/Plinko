using UnityEngine;

public class SetAutoRotation : MonoBehaviour
{
    private void Start(){
        DeviceOrientationManager.AutoRotateScreen = true;
    }
}
