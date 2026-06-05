using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RackHighlightManager : MonoBehaviour
{
    public XRGrabInteractable[] grabObjects;
    public RackSlotHighlight[] slots;

    private void OnEnable()
    {
        foreach (XRGrabInteractable grab in grabObjects)
        {
            grab.selectEntered.AddListener(OnGrabbed);
            grab.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        foreach (XRGrabInteractable grab in grabObjects)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        GameObject grabbedObject =
            args.interactableObject.transform.gameObject;

        foreach (RackSlotHighlight slot in slots)
        {
            slot.ShowGreen(grabbedObject);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        HideAllSlots();
    }

    public void HideAllSlots()
    {
        foreach (RackSlotHighlight slot in slots)
        {
            slot.Hide();
        }
    }
}