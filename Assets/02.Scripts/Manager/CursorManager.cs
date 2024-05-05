using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    Texture2D hand;
    Texture2D original;

    private void Start()
    {
        original = Resources.Load<Texture2D>("Cursor");
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(original, new Vector2(original.width / 3, 0), CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(original, new Vector2(0, 0), CursorMode.Auto);
    }
}
