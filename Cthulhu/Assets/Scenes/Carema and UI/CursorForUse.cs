using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorForUse : MonoBehaviour {
    CameraRaycaster cameraRaycaster;

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(96,96);
    // Use this for initialization
    void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();

    }
	
	// Update is called once per frame
	void Update () {
        // print(cameraRaycaster.layerHit);
        switch (cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;

            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, cursorHotSpot, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                break;

            default:
                Debug.LogError("Don't know what cursor to show");
                return;
        }
        
	}
}
