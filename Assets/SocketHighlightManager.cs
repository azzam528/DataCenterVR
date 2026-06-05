using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class SocketHighlightManager : MonoBehaviour
{
    public List<XRSocketInteractor> sockets;
    public XRGrabInteractable grabbable;

    void Update()
    {
        if (grabbable == null || !grabbable.isSelected)
        {
            foreach (var s in sockets) s.socketActive = false;
            return;
        }

        XRSocketInteractor nearest = null;
        float minDist = float.MaxValue;

        foreach (var s in sockets)
        {
            if (s.hasSelection) continue;
            float d = Vector3.Distance(grabbable.transform.position, s.transform.position);
            if (d < minDist) { minDist = d; nearest = s; }
        }

        foreach (var s in sockets)
        {
            if (s.hasSelection) { s.socketActive = false; continue; }
            s.socketActive = (s == nearest);
        }
    }
}