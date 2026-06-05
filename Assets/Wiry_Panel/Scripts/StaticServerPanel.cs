using UnityEngine;
using TMPro;

public class StaticServerPanel : MonoBehaviour
{
    [Header("Target Server")]
    public ServerConfigData serverData;

    [Header("Semua Server")]
    public ServerConfigData[] allServers;

    [Header("UI Text")]
    public TMP_Text titleText;
    public TMP_Text powerStatusText;
    public TMP_Text cableStatusText;
    public TMP_Text confirmStatusText;
    public TMP_Text ipText;
    public TMP_Text rackStatusText;
    public TMP_Text readyStatusText;
    public TMP_Text confirmText;

    [Header("IP")]
    public TMP_Dropdown ipDropdown;

    private Color32 greenColor = new Color32(46, 204, 113, 255);
    private Color32 redColor = new Color32(231, 76, 60, 255);
    private Color32 whiteColor = new Color32(230, 240, 255, 255);

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        RefreshUI();
    }

    public void TogglePower()
    {
        if (serverData == null) return;

        serverData.TogglePower();
        RefreshUI();
    }

    public void ConfirmIP()
    {
        if (serverData == null) return;
        if (ipDropdown == null) return;

        string ip = ipDropdown.options[ipDropdown.value].text;

        if (ip == "Pilih IP")
        {
            confirmText.text = "Error: Pilih IP dulu";
            confirmText.color = redColor;
            return;
        }

        ServerConfigData usedByServer = GetServerUsingIP(ip);

        if (usedByServer != null)
        {
            confirmText.text = "Error: IP " + ip + " sudah dipilih oleh " + usedByServer.serverName;
            confirmText.color = redColor;
            return;
        }

        serverData.SetIP(ip);
        serverData.Confirm();

        confirmText.text = "IP berhasil dikonfirmasi";
        confirmText.color = greenColor;

        RefreshUI();
    }

    private ServerConfigData GetServerUsingIP(string ip)
    {
        if (allServers == null) return null;

        foreach (ServerConfigData server in allServers)
        {
            if (server == null) continue;
            if (server == serverData) continue;

            if (server.selectedIP == ip)
            {
                return server;
            }
        }

        return null;
    }

    public void RefreshUI()
    {
        if (serverData == null) return;

        titleText.text = "Konfigurasi " + serverData.serverName;
        titleText.color = whiteColor;

        powerStatusText.text = "Power: " + (serverData.isPowerOn ? "ON" : "OFF");
        powerStatusText.color = serverData.isPowerOn ? greenColor : redColor;

        cableStatusText.text = "Kabel: " + (serverData.IsCableConnected() ? "Connected" : "No");
        cableStatusText.color = serverData.IsCableConnected() ? greenColor : redColor;

        rackStatusText.text = "Rack: " + (serverData.isInstalled ? "Installed" : "Not Installed");
        rackStatusText.color = serverData.isInstalled ? greenColor : redColor;

        confirmStatusText.text = serverData.isConfirmed ? "Confirmed" : "Unconfirmed";
        confirmStatusText.color = serverData.isConfirmed ? greenColor : redColor;

        if (string.IsNullOrEmpty(serverData.selectedIP))
        {
            ipText.text = "IP: Belum Dipilih";
            ipText.color = redColor;
        }
        else
        {
            ipText.text = "IP: " + serverData.selectedIP;
            ipText.color = greenColor;
        }

        readyStatusText.text = "Status: " + (serverData.IsReady() ? "Ready" : "Not Ready");
        readyStatusText.color = serverData.IsReady() ? greenColor : redColor;
    }
}