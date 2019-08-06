using UnityEngine;
using UnityEngine.XR;

public class PlayerTrackingAdjustment : MonoBehaviour 
{
	[SerializeField] private TrackingSpaceType trackingSpaceType_ = TrackingSpaceType.RoomScale;
	[SerializeField] private float playerHeight_ = 1.7f;

	void Start () 
	{
		if (XRSettings.enabled == false) 
		{
			Destroy (this);
		}

		XRDevice.SetTrackingSpaceType(trackingSpaceType_);

		if (XRDevice.GetTrackingSpaceType () == TrackingSpaceType.RoomScale) 
		{
			InputTracking.disablePositionalTracking = true;
			Debug.Log ("Roomscale Tracking");
		}
		else 
		{
			Debug.Log ("Stationary Tracking");
			transform.localPosition += Vector3.up * playerHeight_;
			InputTracking.disablePositionalTracking = false;
		}

		InputTracking.Recenter ();
	}
}
