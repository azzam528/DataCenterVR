using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlightController : MonoBehaviour
{
    public GameObject highlightObject;
    public Material greenMaterial;
    public Material blueMaterial;

    XRSocketInteractor socket;
    MeshRenderer meshRenderer;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        meshRenderer = highlightObject.GetComponentInChildren<MeshRenderer>();

        highlightObject.SetActive(false);

        socket.hoverEntered.AddListener((a) =>
        {
            if (!socket.hasSelection)
            {
                meshRenderer.material = greenMaterial;
                highlightObject.SetActive(true);
            }
        });

        socket.hoverExited.AddListener((a) =>
        {
            if (!socket.hasSelection)
            {
                highlightObject.SetActive(false);
            }
        });

        socket.selectEntered.AddListener((a) =>
        {
            meshRenderer.material = blueMaterial;
            highlightObject.SetActive(true);
        });

        socket.selectExited.AddListener((a) =>
        {
            highlightObject.SetActive(false);
        });
    }
}