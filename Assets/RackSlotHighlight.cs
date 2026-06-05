using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RackSlotHighlight : MonoBehaviour
{
    public XRSocketInteractor socket;

    public Transform slotVisual;
    public Renderer slotRenderer;

    public Material greenMaterial;
    public Material blueMaterial;

    private GameObject currentGrabObject;

    private void Awake()
    {
        if (socket == null)
            socket = GetComponent<XRSocketInteractor>();

        Hide();
    }

    private void OnEnable()
    {
        socket.hoverEntered.AddListener(OnHoverEntered);
        socket.hoverExited.AddListener(OnHoverExited);
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        socket.hoverEntered.RemoveListener(OnHoverEntered);
        socket.hoverExited.RemoveListener(OnHoverExited);
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    public void ShowGreen(GameObject grabbedObject)
    {
        currentGrabObject = grabbedObject;

        if (grabbedObject.CompareTag("Switch"))
        {
            slotVisual.localPosition = new Vector3(0f, 0f, 0.303f);
            slotVisual.localRotation = Quaternion.Euler(0f, -90f, 90f);
            slotVisual.localScale = new Vector3(0.6f, 0.1f, 0.245f);
        }
        else if (grabbedObject.CompareTag("Server"))
        {
            slotVisual.localPosition = new Vector3(0f, 0f, 0.048f);
            slotVisual.localRotation = Quaternion.Euler(0f, -90f, 90f);
            slotVisual.localScale = new Vector3(0.6f, 0.2f, 0.8f);
        }

        slotRenderer.material = greenMaterial;
        slotRenderer.enabled = true;
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (currentGrabObject == null) return;

        slotRenderer.material = blueMaterial;
        slotRenderer.enabled = true;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (currentGrabObject != null)
        {
            slotRenderer.material = greenMaterial;
            slotRenderer.enabled = true;
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
    FindObjectOfType<RackHighlightManager>()
        .HideAllSlots();
    }

    public void Hide()
    {
        currentGrabObject = null;

        if (slotRenderer != null)
            slotRenderer.enabled = false;
    }
}