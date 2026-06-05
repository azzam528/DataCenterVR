using UnityEngine;

public class ServerConfigData : MonoBehaviour
{
    public string serverName = "Server A";

    public bool isInstalled = false;
    public bool isPowerOn = false;
    public bool isConfirmed = false;

    public string selectedIP = "";

    public PortActivator portActivator;

    public bool IsCableConnected()
    {
        return portActivator != null && portActivator.IsConnected();
    }

    public bool IsReady()
    {
        return isInstalled &&
               isPowerOn &&
               isConfirmed &&
               !string.IsNullOrEmpty(selectedIP) &&
               IsCableConnected();
    }

    public void TogglePower()
    {
        isPowerOn = !isPowerOn;
    }

    public void SetIP(string ip)
    {
        selectedIP = ip;
    }

    public void Confirm()
    {
        isConfirmed = true;
    }
}