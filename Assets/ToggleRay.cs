using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleRay : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private XRInteractorLineVisual lineVisual;

    void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();

        // Sembunyikan ray dari awal
        rayInteractor.enabled = false;
        if (lineVisual != null)
            lineVisual.enabled = false;
    }

    void Update()
    {
        InputDevice leftHand = InputDevices
            .GetDeviceAtXRNode(XRNode.LeftHand);

        bool triggerPressed;
        leftHand.TryGetFeatureValue(
            CommonUsages.triggerButton, out triggerPressed);

        // Tampilkan ray hanya saat trigger ditekan
        rayInteractor.enabled = triggerPressed;
        if (lineVisual != null)
            lineVisual.enabled = triggerPressed;
    }
}