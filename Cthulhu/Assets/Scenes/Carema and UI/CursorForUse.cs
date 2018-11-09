using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRaycaster))]
public class CursorForUse : MonoBehaviour {
    CameraRaycaster cameraRaycaster;

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Texture2D targetCursor = null;
    [SerializeField] Vector2 cursorHotSpot = new Vector2(0,0);

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

    // Use this for initialization
    void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        //cameraRaycaster.notifyMouseClickObservers += OnLayerChanged; // registering
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // registering
    }
	
	// Update is no no no no called once per frame
	void OnLayerChanged (int newLayer) { // only call when layer changes
         print("cursor for use Delegate reporting for duty");
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                break;

            case enemyLayerNumber:
                Cursor.SetCursor(targetCursor, cursorHotSpot, CursorMode.Auto);
                break;
                /*
            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                break;
                */
            default:
                //Debug.LogError("Don't know what cursor to show");
                Cursor.SetCursor(unknownCursor, cursorHotSpot, CursorMode.Auto);
                return;
        }
        
	}

    // TODO consder de-registering
}
