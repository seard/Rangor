using UnityEngine;
using System.Collections;
using System;
public class ConnectionScript : MonoBehaviour
{
    public bool autoHost;
    public GameObject prefabPlayer1 = null;
    public GameObject prefabPlayer2 = null;
    public Vector3 secondStartPos;
    public Vector3 firstStartPos;
    public string masterServerIp = "127.0.0.1";

    private string maxPlayers = "2", port = "25000", serverName = "Rangor testserver";
    private GUIStyle largeFont = new GUIStyle();
    private string gameName = "Rangor TestServer";
    private Rect windowRect = new Rect(0, 200, 400, 400);

    void Start()
    {
        MasterServer.ipAddress = masterServerIp;
        MasterServer.port = 23466;
        Network.natFacilitatorIP = masterServerIp;
        Network.natFacilitatorPort = 50005;
        largeFont.fontSize = 24;
        largeFont.normal.textColor = Color.white;
    }

    void update()
    {

    }

    private void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (autoHost)
            {
                Network.InitializeSecurity();
                Network.InitializeServer(int.Parse(maxPlayers), int.Parse(port), !Network.HavePublicAddress());
                MasterServer.RegisterHost(gameName, serverName, "Do not join, this is just a test");
                GameObject player1Prefab = Network.Instantiate(prefabPlayer1, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject; //Create player 1
                GameObject player1 = player1Prefab.transform.FindChild("Player").gameObject;
                player1.transform.position = firstStartPos;
            }
            else // manual host
            {
                windowRect = GUI.Window(0, windowRect, windowFunc, "Serverlist");
                GUILayout.Label("Server Name");
                serverName = GUILayout.TextField(serverName);
                GUILayout.Label("Port");
                port = GUILayout.TextField(port);
                GUILayout.Label("Masterserver ip");
                masterServerIp = GUILayout.TextField(masterServerIp);
                MasterServer.ipAddress = masterServerIp;
                if (GUILayout.Button("Create Server"))
                {
                    try
                    {
                        Network.InitializeSecurity();
                        Network.InitializeServer(int.Parse(maxPlayers), int.Parse(port), !Network.HavePublicAddress());
                        MasterServer.RegisterHost(gameName, serverName, "Do not join, this is just a test");
                        GameObject player1Prefab = Network.Instantiate(prefabPlayer1, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject; //Create player 1
                        GameObject player1 = player1Prefab.transform.FindChild("Player").gameObject;
                        player1.transform.position = firstStartPos;
                    }
                    catch (Exception)
                    {
                        print("Please type in numbers for port and max players");
                    }
                }
            }
        }
        else
        {
            if (GUILayout.Button("Disconnect"))
            {
                MasterServer.UnregisterHost();
                Network.Disconnect();
                Application.LoadLevel("MainScene");
            }
        }
    }
    private void windowFunc(int id)
    {
        if (GUILayout.Button("Refresh"))
        {
            MasterServer.RequestHostList(gameName);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Box("Servername");
        GUILayout.EndHorizontal();
        if (MasterServer.PollHostList().Length != 0)
        {
            HostData[] data = MasterServer.PollHostList();
            foreach (HostData c in data)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(c.gameName);
                if (GUILayout.Button("Connect"))
                {
                    Network.Connect(c);
                }
                GUILayout.EndHorizontal();
            }
        }
        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
    //called on client when client connects
    void OnConnectedToServer()
    {
        GameObject player2Prefab = Network.Instantiate(prefabPlayer2, new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;
        GameObject player2 = player2Prefab.transform.FindChild("Player").gameObject;
        player2.transform.position = secondStartPos;
    }
    //called on server when client connects
    void OnPlayerConnected()
    {

    }
    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }

    void OnDestroy()
    {
        Network.Disconnect();
    }
}

