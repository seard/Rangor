using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Texture start;
    public Texture exit;

    public float guiPlacementStartX;
    public float guiPlacementStartY;
    public float guiPlacementExitX;
    public float guiPlacementExitY;

    void OnGUI()
    {
        
        if(GUI.Button(new Rect(Screen.width/2 -75 , Screen.height * guiPlacementStartY, 150, 50), start, ""))
        {
            Debug.Log("YAAAAAY");
            Application.LoadLevel("devScene_2");
        }

        if (GUI.Button(new Rect(Screen.width/2 - 75, Screen.height * guiPlacementExitY, 150,50), exit, ""))
        {
            Debug.Log("YAAAAAY");
            Application.Quit();
        }
    }
}
