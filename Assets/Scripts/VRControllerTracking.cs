using UnityEngine;
using UnityEngine.XR;

public class VRControllerTracking : MonoBehaviour 
{
	[SerializeField] private XRNode whichNode_ = XRNode.LeftHand;

	private void Start () 
	{
		if (XRSettings.enabled == false)
		{
			Destroy (this);
		}
	}
	
	private void Update () 
	{
		transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition (whichNode_);
		transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation (whichNode_);
	}
}
