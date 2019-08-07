using UnityEngine;
using UnityEngine.XR;

public class InputDeviceTracking : MonoBehaviour
{
    [SerializeField] private XRNode whichNode_ = XRNode.LeftHand;

    private void Update()
    {
        if (!XRSettings.enabled) return;

        transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition(whichNode_);
        transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(whichNode_);
    }
}
