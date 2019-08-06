using UnityEngine;
using System;

public class InputDevice : MonoBehaviour
{
    public enum InputName
    {
        None = 0,
        PrimaryIndexTrigger = 1,
        SecondaryIndexTrigger,
        PrimaryHandTrigger,
        SecondaryHandTrigger,
    }

    [SerializeField] private InputName inputTriggerName_ = InputName.None;

    public event Action<bool> triggerPressed = null;

    private void Update()
    {
        if (Input.GetAxisRaw(inputTriggerName_.ToString()) > 0.1f)
        {
            triggerPressed?.Invoke(true);
        }
        else
        {
            triggerPressed?.Invoke(false);
        }
    }
}