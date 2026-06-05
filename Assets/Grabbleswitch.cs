using UnityEngine;
using System.Collections.Generic;

public class GrabbableSwitch : MonoBehaviour
{
    private float detectionRadius;
    private LayerMask snapLayer;
    private List<SnapPoint> activeHighlights = new List<SnapPoint>();
    private SnapPoint currentNearest = null;

    public void OnGrab(float radius, LayerMask layer)
    {
        detectionRadius = radius;
        snapLayer = layer;
    }

    // Dipanggil tiap frame dari SimpleGrab saat dipegang
    public void UpdateSnapHighlight()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, snapLayer);

        // Reset semua highlight sebelumnya
        foreach (var sp in activeHighlights)
            sp.SetHighlight(false);
        activeHighlights.Clear();
        currentNearest = null;

        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            SnapPoint sp = hit.GetComponent<SnapPoint>();
            if (sp == null || sp.isOccupied) continue;

            float d = Vector3.Distance(transform.position, sp.transform.position);
            sp.SetHighlight(true, isClose: false); // semua hijau
            activeHighlights.Add(sp);

            if (d < minDist)
            {
                minDist = d;
                currentNearest = sp;
            }
        }

        // Yang paling dekat → kuning
        if (currentNearest != null)
            currentNearest.SetHighlight(true, isClose: true);
    }

    // Dipanggil saat G ditekan lagi (drop)
    // Return true kalau berhasil snap
    public bool TrySnapOnRelease()
    {
        foreach (var sp in activeHighlights)
            sp.SetHighlight(false);
        activeHighlights.Clear();

        if (currentNearest != null)
        {
            currentNearest.OccupySlot(gameObject);
            currentNearest = null;
            return true; // berhasil snap, jangan drop normal
        }

        currentNearest = null;
        return false; // tidak ada snap, drop normal
    }
}