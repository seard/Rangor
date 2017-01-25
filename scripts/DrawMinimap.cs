using UnityEngine;
using System.Collections;

public class DrawMinimap : MonoBehaviour {
    public RenderTexture MinimapImage;
    public int mapsize = 1;

    void OnGUI ()
    {
        GUI.DrawTexture(new Rect(0, 0, MinimapImage.width, MinimapImage.height), MinimapImage);
    }
}
