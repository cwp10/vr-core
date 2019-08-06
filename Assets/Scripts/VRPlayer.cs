using UnityEngine;
using System;

public class VRPlayer : MonoBehaviour 
{
	[SerializeField] private RayPointer rayPointer_ = null;
	[SerializeField] private InputDevice leftDevice_ = null;
	[SerializeField] private InputDevice rightDevice_ = null;

	public event Action<bool> leftTriggerDown = null;
	public event Action<bool> rightTriggerDown = null;
	public event Action<GameObject> pointIn = null;
	public event Action<GameObject> pointOut = null;

	private void OnEnable()
	{
		leftDevice_.triggerPressed += leftTriggerDown;
		rightDevice_.triggerPressed += rightTriggerDown;
		rayPointer_.pointerIn += OnPointerIn;
		rayPointer_.pointerOut += OnPointerOut;
	}

	private void OnDisable()
	{
		leftDevice_.triggerPressed -= leftTriggerDown;
		rightDevice_.triggerPressed -= rightTriggerDown;
		rayPointer_.pointerIn -= OnPointerIn;
		rayPointer_.pointerOut -= OnPointerOut;
	}

	private void OnPointerIn(object sender, RayPointEventArgs e)
	{
		pointIn?.Invoke(e.target.gameObject);
	}

	private void OnPointerOut(object sender, RayPointEventArgs e)
	{
		pointOut?.Invoke(e.target.gameObject);
	}
}
