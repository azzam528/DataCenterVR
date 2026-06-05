using UnityEngine;
using TMPro;

public class PingPanel : MonoBehaviour
{
    [Header("Server Data")]
    public ServerConfigData serverA;
    public ServerConfigData serverB;
    public ServerConfigData serverC;

    [Header("Server Status Text")]
    public TMP_Text serverAStatusText;
    public TMP_Text serverBStatusText;
    public TMP_Text serverCStatusText;

    [Header("Result UI")]
    public TMP_Text resultText;

    private Color32 greenColor = new Color32(34, 197, 94, 255);
    private Color32 redColor = new Color32(239, 68, 68, 255);
    private Color32 yellowColor = new Color32(250, 204, 21, 255);

    private void Start()
    {
        RefreshStatus();
    }

    private void Update()
    {
        RefreshStatusOnly();
    }

    public void CheckPing()
    {
        bool allReady =
            serverA != null && serverA.IsReady() &&
            serverB != null && serverB.IsReady() &&
            serverC != null && serverC.IsReady();

        if (allReady)
        {
            resultText.text = "PING BERHASIL\nSemua server online";
            resultText.color = greenColor;
        }
        else
        {
            resultText.text = "PING GAGAL\nCek power, kabel, rack, dan IP";
            resultText.color = redColor;
        }

        RefreshStatusOnly();
    }

    private void RefreshStatus()
    {
        RefreshStatusOnly();

        if (resultText != null)
        {
            resultText.text = "Tekan Check Ping";
            resultText.color = yellowColor;
        }
    }

    private void RefreshStatusOnly()
    {
        SetServerStatus(serverA, serverAStatusText);
        SetServerStatus(serverB, serverBStatusText);
        SetServerStatus(serverC, serverCStatusText);
    }

    private void SetServerStatus(ServerConfigData server, TMP_Text text)
    {
        if (text == null) return;

        if (server == null)
        {
            text.text = "Server belum diisi";
            text.color = yellowColor;
            return;
        }

        if (server.IsReady())
        {
            text.text = server.serverName + " : Ready";
            text.color = greenColor;
        }
        else
        {
            text.text = server.serverName + " : Offline";
            text.color = redColor;
        }
    }
}