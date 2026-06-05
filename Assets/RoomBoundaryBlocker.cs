using UnityEngine;

public class RoomBoundaryBlocker : MonoBehaviour
{
    private Vector3 lastSafePosition;

    private void Start()
    {
        lastSafePosition = transform.position;
    }

    private void Update()
    {
        lastSafePosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.position = lastSafePosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.position = lastSafePosition;
        }
    }
}