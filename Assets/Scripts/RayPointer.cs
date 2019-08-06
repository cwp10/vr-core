using UnityEngine;

public struct RayPointEventArgs
{
	public float distance;
	public Transform target;
}

public delegate void RayPointerEventHandler(object sender, RayPointEventArgs e);

public class RayPointer : MonoBehaviour 
{
	[SerializeField] private Camera vrCamera_ = null;

	private Vector2 _center = Vector2.zero;
	private Transform _previousTarget = null;

	public event RayPointerEventHandler pointerIn = null;
	public event RayPointerEventHandler pointerOut = null;

	public void OnPointIn(RayPointEventArgs e)
	{
		pointerIn?.Invoke(this, e);
	}

	public void OnPointOut(RayPointEventArgs e)
	{
		pointerOut?.Invoke(this, e);
	}

	private void OnEnable()
	{
		_center = new Vector3(vrCamera_.pixelWidth / 2, vrCamera_.pixelHeight / 2);
	}

	private void Update()
	{
		if (vrCamera_ == null) return;

		Ray ray = vrCamera_.ScreenPointToRay(_center);
		RaycastHit hit;

		bool bHit = Physics.Raycast(ray, out hit);

		if (_previousTarget && _previousTarget != hit.transform)
		{
			RayPointEventArgs argsOut = new RayPointEventArgs();
			argsOut.distance = 0;
			argsOut.target = _previousTarget;
			OnPointOut(argsOut);
			_previousTarget = null;
		}

		if (bHit && _previousTarget != hit.transform)
		{
			RayPointEventArgs argsIn = new RayPointEventArgs();
			argsIn.distance = hit.distance;
			argsIn.target = hit.transform;
			OnPointIn(argsIn);
			_previousTarget = hit.transform;
		}

		if (!bHit)
		{
			_previousTarget = null;
		}
	}
}
