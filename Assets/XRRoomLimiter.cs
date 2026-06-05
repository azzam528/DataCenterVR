using UnityEngine;

public class XRRoomLimiter : MonoBehaviour
{
    [Header("Batas Ruangan")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minZ = -8f;
    public float maxZ = 8f;

    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}