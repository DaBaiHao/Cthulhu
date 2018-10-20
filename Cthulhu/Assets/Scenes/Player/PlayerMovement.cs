using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;


    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    //TODO fix the problem with the increase speed WASD and click


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.G)) { // TODO allow player to map
            isInDirectMode = !isInDirectMode; // toggle mode
            currentDestination = transform.position; // clear the click target
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

            clickPoint = cameraRaycaster.hit.point;
            // print("Cursor raycast hit layer :" + cameraRaycaster.layerHit);
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    // currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    print("not moving to enemy");
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                    break;
                default:
                    print("unexpect layer found");
                    return;
            }



        }

        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;

        if (playerToClickPoint.magnitude >= 0)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        // draw movment Gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.DrawSphere(clickPoint, 0.1f);


        // draw attack Gizmos
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

