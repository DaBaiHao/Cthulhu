using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;


    [SerializeField] float walkMoveStopRadius = 0.2f;


    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    //TODO fix the problem with the increase speed WASD and click


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.G)) { // TODO allow player to map
            isInDirectMode = !isInDirectMode; // toggle mode
            currentClickTarget = transform.position; // clear the click target
        }


        if (isInDirectMode)
        {
             ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement(); // mouse movement
        }


        

    }



    private void ProcessDirectMovement()
    {
        // print("direction movement");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        print(h+v);
        // calculate camera relative direction to move:

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }





    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            // print("Cursor raycast hit layer :" + cameraRaycaster.layerHit);
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
                    break;
                case Layer.Enemy:
                    print("not moving to enemy");
                    break;
                default:
                    print("unexpect layer found");
                    return;
            }



        }

        var playerToClickPoint = currentClickTarget - transform.position;

        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }


    void OnDrawGizmos()
    {
        //print("Gizmos draw");
        Gizmos.DrawLine(transform.position, currentClickTarget);
    }
}

