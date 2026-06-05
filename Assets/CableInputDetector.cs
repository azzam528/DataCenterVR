using UnityEngine;
using UnityEngine.XR;

public class CableInputDetector : MonoBehaviour
{
    private PortActivator currentPort;
    private bool lastButtonState = false;

    private void Update()
    {
        if (currentPort == null) return;

        bool pressed = false;

        // Tombol X di controller kiri Quest asli
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool xPressed))
        {
            pressed = xPressed;
        }

        // Tombol N untuk XR Device Simulator
        if (Input.GetKeyDown(KeyCode.N))
        {
            pressed = true;
        }

        if (pressed && !lastButtonState)
        {
            currentPort.ConnectCable();
        }

        lastButtonState = pressed;
    }

    private void OnTriggerEnter(Collider other)
    {
        PortActivator port = other.GetComponent<PortActivator>();

        if (port != null)
        {
            currentPort = port;
            Debug.Log("Dekat port: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PortActivator port = other.GetComponent<PortActivator>();

        if (port != null && port == currentPort)
        {
            currentPort = null;
            Debug.Log("Keluar dari port: " + other.name);
        }
    }
}