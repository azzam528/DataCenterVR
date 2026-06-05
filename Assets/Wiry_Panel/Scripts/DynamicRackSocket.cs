using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicRackSocket : MonoBehaviour
{
    public XRSocketInteractor socket;

    public Transform attachPointSwitch;
    public Transform attachPointServer;

    private AudioSource audioSource;
    private bool sudahKepasang = false;

    private void Awake()
    {
        if (socket == null)
            socket = GetComponent<XRSocketInteractor>();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (socket == null) return;

        socket.hoverEntered.AddListener(OnHoverEntered);
        socket.selectEntered.AddListener(OnSelectEntered);
        socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        if (socket == null) return;

        socket.hoverEntered.RemoveListener(OnHoverEntered);
        socket.selectEntered.RemoveListener(OnSelectEntered);
        socket.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (sudahKepasang) return;

        SetAttachPoint(args.interactableObject.transform);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Transform obj = args.interactableObject.transform;

        SetAttachPoint(obj);

        ServerConfigData serverData = obj.GetComponent<ServerConfigData>();
        if (serverData != null)
            serverData.isInstalled = true;

        if (!sudahKepasang)
        {
            if (audioSource != null && audioSource.clip != null)
                audioSource.PlayOneShot(audioSource.clip);

            sudahKepasang = true;
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Transform obj = args.interactableObject.transform;

        ServerConfigData serverData = obj.GetComponent<ServerConfigData>();
        if (serverData != null)
            serverData.isInstalled = false;

        sudahKepasang = false;
    }

    private void SetAttachPoint(Transform obj)
    {
        if (obj.CompareTag("Switch"))
        {
            attachPointSwitch.localPosition = new Vector3(0f, 0f, 0.303f);
            attachPointSwitch.localRotation = Quaternion.Euler(0f, -90f, 90f);
            socket.attachTransform = attachPointSwitch;
        }
        else if (obj.CompareTag("Server"))
        {
            attachPointServer.localPosition = new Vector3(0.408f, 0f, 0.001f);
            attachPointServer.localRotation = Quaternion.Euler(0f, -90f, 90f);
            socket.attachTransform = attachPointServer;
        }
    }
}