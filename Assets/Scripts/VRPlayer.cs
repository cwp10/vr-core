using UnityEngine;
using System;
using System.Collections;
using UnityEngine.XR;

public enum VRDevice
{
    None,
    OpenVR,
    Oculus,
}

public class VRPlayer : MonoBehaviour
{
    [SerializeField] private VRDevice vrDevice_ = VRDevice.None;
    [SerializeField] private TrackingSpaceType trackingSpaceType_ = TrackingSpaceType.Stationary;
    [SerializeField] private RayPointer[] rayPointers_ = null;
    [SerializeField] private InputDevice leftDevice_ = null;
    [SerializeField] private InputDevice rightDevice_ = null;

    public event Action<bool> leftTriggerDown = null;
    public event Action<bool> rightTriggerDown = null;
    public event Action<RayPointEventArgs> rayPointIn = null;
    public event Action<RayPointEventArgs> rayPointOut = null;

    public void EnableVR()
    {
        StartCoroutine(LoadDevice(vrDevice_.ToString(), true));
    }

    public void DisableVR()
    {
        StartCoroutine(LoadDevice("", false));
    }

    public void TrackingRecenter(float height)
    {
        XRDevice.SetTrackingSpaceType(trackingSpaceType_);

        transform.localPosition = Vector3.zero;

		Debug.Log(XRDevice.GetTrackingSpaceType());

        if (XRDevice.GetTrackingSpaceType() == TrackingSpaceType.RoomScale)
        {
            Debug.Log("Roomscale Tracking");
        }
        else
        {
            transform.localPosition += Vector3.up * height;
            Debug.Log("Stationary Tracking");
        }

        InputTracking.Recenter();
    }

	public void DisablePositionalTracking(bool isDisable)
	{
		InputTracking.disablePositionalTracking = isDisable;
	}

    private void OnEnable()
    {
        leftDevice_.triggerPressed += leftTriggerDown;
        rightDevice_.triggerPressed += rightTriggerDown;

        foreach (RayPointer rayPointer in rayPointers_)
        {
            rayPointer.pointerIn += OnPointerIn;
            rayPointer.pointerOut += OnPointerOut;
        }
    }

    private void OnDisable()
    {
        leftDevice_.triggerPressed -= leftTriggerDown;
        rightDevice_.triggerPressed -= rightTriggerDown;

        foreach (RayPointer rayPointer in rayPointers_)
        {
            rayPointer.pointerIn -= OnPointerIn;
            rayPointer.pointerOut -= OnPointerOut;
        }
    }

    private void OnPointerIn(object sender, RayPointEventArgs e)
    {
        rayPointIn?.Invoke(e);
    }

    private void OnPointerOut(object sender, RayPointEventArgs e)
    {
        rayPointOut?.Invoke(e);
    }

    private IEnumerator LoadDevice(string device, bool enable)
    {
        XRSettings.LoadDeviceByName(device);
        yield return null;
        XRSettings.enabled = true;
    }
}
