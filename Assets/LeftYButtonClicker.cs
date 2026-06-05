using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class LeftYButtonClicker : MonoBehaviour
{
    public Transform leftController;
    public float rayDistance = 10f;

    private InputDevice leftHand;
    private bool previousYState = false;

    void Start()
    {
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        if (!leftHand.isValid)
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            return;
        }

        bool yPressed;
        leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out yPressed);

        if (yPressed && !previousYState)
        {
            ClickButton();
        }

        previousYState = yPressed;
    }

    void ClickButton()
    {
        Ray ray = new Ray(leftController.position, leftController.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Button button = hit.collider.GetComponent<Button>();

            if (button != null)
            {
                button.onClick.Invoke();
                Debug.Log("Tombol diklik: " + button.name);
            }
        }
    }
}