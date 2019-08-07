using UnityEngine;

public struct RayPointEventArgs
{
    public RayPointer.Device device;
    public float distance;
    public Transform target;
}

public delegate void RayPointerEventHandler(object sender, RayPointEventArgs e);

public class RayPointer : MonoBehaviour
{
    public enum Device
    {
        None,
        HeadMount,
        LeftController,
        RightController,
    }

    [SerializeField] private Device device_ = Device.None;

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

    private void Update()
    {
        UpdateRaycast();
    }

    private void UpdateRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        bool bHit = Physics.Raycast(ray, out hit);

        if (_previousTarget && _previousTarget != hit.transform)
        {
            RayPointEventArgs argsOut = new RayPointEventArgs();
            argsOut.distance = 0;
            argsOut.device = device_;
            argsOut.target = _previousTarget;
            OnPointOut(argsOut);
            _previousTarget = null;
        }

        if (bHit && _previousTarget != hit.transform)
        {
            RayPointEventArgs argsIn = new RayPointEventArgs();
            argsIn.distance = hit.distance;
            argsIn.device = device_;
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
