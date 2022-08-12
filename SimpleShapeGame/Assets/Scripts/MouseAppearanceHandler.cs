using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAppearanceHandler : MonoBehaviour
{

    public Texture2D mouseTexture; // mouse textures should always be 16x16
    public Vector2 offset;

    void Start()
    {
        Cursor.SetCursor(mouseTexture, offset, CursorMode.Auto);
    }


    void OnMouseExit()
    {
        Cursor.SetCursor(null, offset, CursorMode.Auto);
    }
}
