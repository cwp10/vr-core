using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private string inputTriggerName_ = string.Empty;

    public bool IsTriggerPressed { get; private set; }

    private void Update()
    {
        if (Input.GetAxisRaw(inputTriggerName_) > 0.1f)
        {
            IsTriggerPressed = true;
        }
        else
        {
            IsTriggerPressed = false;
        }
    }
}