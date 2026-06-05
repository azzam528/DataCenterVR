using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    [HideInInspector] public bool isOccupied = false;

    [Header("Highlight Visual")]
    public GameObject highlightVisual; // assign quad/plane transparan di Inspector

    private Renderer highlightRenderer;
    private Material highlightMat;

    private Color colorAvailable = new Color(0f, 1f, 0.4f, 0.35f);
    private Color colorNear = new Color(1f, 0.9f, 0f, 0.6f);

    void Start()
    {
        if (highlightVisual != null)
        {
            highlightRenderer = highlightVisual.GetComponent<Renderer>();
            // Buat instance material sendiri supaya tidak saling override
            highlightMat = new Material(highlightRenderer.material);
            highlightRenderer.material = highlightMat;
            highlightVisual.SetActive(false);
        }
    }

    public void SetHighlight(bool active, bool isClose = false)
    {
        if (highlightVisual == null) return;

        highlightVisual.SetActive(active);

        if (active && highlightMat != null)
        {
            Color c = isClose ? colorNear : colorAvailable;
            highlightMat.color = c;
            highlightMat.SetColor("_EmissionColor", c * 2f);
        }
    }

    public void OccupySlot(GameObject switchObj)
    {
        isOccupied = true;
        SetHighlight(false);

        switchObj.transform.SetParent(transform);
        switchObj.transform.localPosition = Vector3.zero;
        switchObj.transform.localRotation = Quaternion.identity;

        // Matikan fisika karena sudah tertempel di slot
        Rigidbody rb = switchObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    public void VacateSlot(GameObject switchObj)
    {
        isOccupied = false;
        switchObj.transform.SetParent(null);
    }
}