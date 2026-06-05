using UnityEngine;
using UnityEngine.XR;

public class CableAimConnector : MonoBehaviour
{
    [Header("Ray Setting")]
    public float rayDistance = 5f;
    public LayerMask portLayer;

    [Header("Guide Line")]
    public LineRenderer guideLine;

    private PortActivator targetedPort;

    private void Start()
    {
        // Line renderer aktif terus dari awal
        if (guideLine != null)
            guideLine.enabled = true;
    }

    private void Update()
    {
        UpdateGuideLine();

        // Connect saat tombol X diklik (bukan ditahan)
        if (GetXButtonDown())
        {
            TryConnectCable();
        }
    }

    private bool GetXButtonDown()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        bool pressed = false;

        leftController.TryGetFeatureValue(CommonUsages.primaryButton, out pressed);

        // Untuk test di editor
        if (Input.GetKeyDown(KeyCode.N))
            pressed = true;

        return pressed;
    }

    private void UpdateGuideLine()
    {
        targetedPort = null;

        Ray ray = new Ray(transform.position, transform.forward);
        Vector3 endPoint = transform.position + transform.forward * rayDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, portLayer))
        {
            endPoint = hit.point;

            PortActivator port = hit.collider.GetComponent<PortActivator>();
            if (port != null)
                targetedPort = port;
        }

        if (guideLine != null)
        {
            guideLine.positionCount = 2;
            guideLine.SetPosition(0, transform.position);
            guideLine.SetPosition(1, endPoint);
        }
    }

    private void TryConnectCable()
    {
        if (targetedPort != null)
        {
            targetedPort.ConnectCable();
        }
    }
}