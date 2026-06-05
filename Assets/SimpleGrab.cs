using UnityEngine;
using System.Collections.Generic;

public class SimpleGrab : MonoBehaviour
{
    public Transform holdPoint;
    public float grabDistance = 3f;

    [Header("Snap Settings")]
    public float snapDetectionRadius = 1.5f;
    public LayerMask snapPointLayer;

    private GameObject grabbedObject;
    private Rigidbody grabbedRb;
    private GrabbableSwitch grabbedSwitch; // referensi ke script snap

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grabbedObject == null)
                TryGrab();
            else
                Drop();
        }

        if (grabbedObject != null)
        {
            grabbedObject.transform.position = holdPoint.position;
            grabbedObject.transform.rotation = holdPoint.rotation;

            // Update highlight snap points setiap frame saat dipegang
            if (grabbedSwitch != null)
                grabbedSwitch.UpdateSnapHighlight();
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(Camera.main.transform.position,
                         Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRb = grabbedObject.GetComponent<Rigidbody>();
                grabbedSwitch = grabbedObject.GetComponent<GrabbableSwitch>();

                if (grabbedRb != null)
                {
                    grabbedRb.useGravity = false;
                    grabbedRb.isKinematic = true;
                }

                // Beritahu switch bahwa dia sedang di-grab
                if (grabbedSwitch != null)
                    grabbedSwitch.OnGrab(snapDetectionRadius, snapPointLayer);
            }
        }
    }

    void Drop()
    {
        // Cek apakah ada snap point terdekat → snap dulu sebelum drop
        bool snapped = false;
        if (grabbedSwitch != null)
            snapped = grabbedSwitch.TrySnapOnRelease();

        // Kalau tidak snap, baru drop normal
        if (!snapped)
        {
            if (grabbedRb != null)
            {
                grabbedRb.useGravity = true;
                grabbedRb.isKinematic = false;
            }
        }

        grabbedObject = null;
        grabbedRb = null;
        grabbedSwitch = null;
    }
}