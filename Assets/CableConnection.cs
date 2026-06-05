using UnityEngine;

public class CableConnection : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    [Header("Objek Penghalang")]
    public Transform serverObject;
    public Transform switchObject;

    [Header("Setting Server")]
    public float serverKeluarDariPort = 0.4f;
    public float serverMajuKeSwitch = 0.2f;
    public float serverJarakAman = 0.54f;
    public float serverCableWidth = 0.005f;

    [Header("Setting PC")]
    public float pcKeluarDariPort = -1f;
    public float pcMajuKeSwitch = 0.2f;
    public float pcJarakAman = 0f;
    public float pcCableWidth = 0.003f;

    [Header("Bentuk Kabel")]
    public int segments = 24;

    [Header("Status Kabel")]
    public bool isConnected;

    private LineRenderer lr;

    private float keluarDariPort;
    private float majuKeSwitch;
    private float jarakAman;
    private float cableWidth;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        if (lr == null)
            lr = GetComponent<LineRenderer>();

        ApplyCableType();

        lr.startWidth = cableWidth;
        lr.endWidth = cableWidth;
        lr.numCornerVertices = 16;
        lr.numCapVertices = 16;
        lr.useWorldSpace = true;

        UpdateConnectionStatus();
    }

    private void Update()
    {
        UpdateConnectionStatus();

        if (!isConnected || lr == null)
        {
            if (lr != null)
                lr.positionCount = 0;

            return;
        }

        ApplyCableType();

        lr.startWidth = cableWidth;
        lr.endWidth = cableWidth;
        lr.positionCount = segments;

        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        Vector3 startKeluar = start + startPoint.forward * keluarDariPort;
        Vector3 switchMasuk = end - endPoint.forward * majuKeSwitch;

        float amanY = GetSafeY();

        Vector3 startAman = new Vector3(startKeluar.x, amanY, startKeluar.z);
        Vector3 switchAman = new Vector3(switchMasuk.x, amanY, switchMasuk.z);

        Vector3 control1 = Vector3.Lerp(startKeluar, startAman, 0.5f);
        Vector3 control2 = Vector3.Lerp(switchAman, switchMasuk, 0.5f);

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);

            Vector3 point =
                Mathf.Pow(1 - t, 3) * start +
                3 * Mathf.Pow(1 - t, 2) * t * control1 +
                3 * (1 - t) * t * t * control2 +
                t * t * t * end;

            lr.SetPosition(i, point);
        }
    }

    public void UpdateConnectionStatus()
    {
        isConnected = startPoint != null && endPoint != null;
    }

    public bool IsConnected()
    {
        return isConnected;
    }

    private void ApplyCableType()
    {
        if (startPoint != null && startPoint.CompareTag("PC"))
        {
            keluarDariPort = pcKeluarDariPort;
            majuKeSwitch = pcMajuKeSwitch;
            jarakAman = pcJarakAman;
            cableWidth = pcCableWidth;
        }
        else
        {
            keluarDariPort = serverKeluarDariPort;
            majuKeSwitch = serverMajuKeSwitch;
            jarakAman = serverJarakAman;
            cableWidth = serverCableWidth;
        }
    }

    private float GetSafeY()
    {
        float safeY = Mathf.Min(startPoint.position.y, endPoint.position.y);

        if (serverObject != null)
        {
            Bounds b = GetObjectBounds(serverObject);
            safeY = Mathf.Min(safeY, b.min.y - jarakAman);
        }

        if (switchObject != null)
        {
            Bounds b = GetObjectBounds(switchObject);
            safeY = Mathf.Min(safeY, b.min.y - jarakAman);
        }

        return safeY;
    }

    private Bounds GetObjectBounds(Transform obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
            return new Bounds(obj.position, Vector3.one * 0.1f);

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return bounds;
    }
}