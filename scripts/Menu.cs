using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public int currentPlayer = 1;
    public Rect windowRect = new Rect(10, 10, 120, 100);

    //GUI update
    void OnGUI()
    {
        windowRect = GUI.Window(1, windowRect, windowFunc, "Player selection");
    }
    void windowFunc(int windowID)
    {
        if (GUILayout.Button("Player1"))
        {
            //Switch to player 1
            currentPlayer = 1;
        }
        if (GUILayout.Button("Player2"))
        {
            //Switch to player 2
            currentPlayer = 2;
        }
        GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
}