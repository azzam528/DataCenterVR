using UnityEngine;

public class RackSlot : MonoBehaviour
{
    public Transform snapPoint;
    private bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        // Kalau slot sudah terisi, abaikan
        if (isOccupied)
            return;

        // Cek apakah object bisa disimpan
        if (other.CompareTag("Grabbable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Matikan physics
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Snap ke posisi slot
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            isOccupied = true;
        }
    }
}