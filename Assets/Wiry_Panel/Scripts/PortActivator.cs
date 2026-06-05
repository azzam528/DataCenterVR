using UnityEngine;

public class PortActivator : MonoBehaviour
{
    public GameObject cable;

    private bool connected = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ConnectCable()
    {
        // Toggle status connect
        connected = !connected;

        // Cek biar aman kalau cable belum diisi
        if (cable != null)
        {
            cable.SetActive(connected);
        }

        // Bunyi cuma saat connect
        if (connected)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }

    public bool IsConnected()
    {
        return connected;
    }
}