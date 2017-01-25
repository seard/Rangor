using UnityEngine;
using System.Collections;

public class Chat : MonoBehaviour {

    public GUISkin ChatSkin;

    private Rect windowRect = new Rect(200, 200, 300, 450);
    private string messBox = "", messageToSend = "", user = "";

    private void OnGUI()
    {
        GUI.skin = ChatSkin;
        windowRect = GUI.Window(1, windowRect, windowFunc, "Chat");
    }

    private void windowFunc(int id)
    {
        GUILayout.Box(messBox, GUILayout.Height(350));
        GUILayout.BeginHorizontal();
        messageToSend = GUILayout.TextField(messageToSend);
        if (GUILayout.Button("Send", GUILayout.Width(75)))
        {
            GetComponent<NetworkView>().RPC("sendMessage", RPCMode.All, user + ": " + messageToSend + "\n");
            messageToSend = "";
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        
        GUILayout.Label("User:");
        user = GUILayout.TextField(user);

        GUILayout.EndHorizontal();
        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    [RPC]
    private void sendMessage(string mess)
    {
        messBox += mess;
    }
}
