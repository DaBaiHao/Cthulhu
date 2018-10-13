using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;


    [SerializeField] float walkMoveStopRadius = 0.2f;


    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    //TODO fix the problem with the increase speed WASD and click


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.G)) { // TODO allow player to map
            isInDirectMode = !isInDirectMode; // toggle mode

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

        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }





    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            // print("Cursor raycast hit layer :" + cameraRaycaster.layerHit);
            switch (cameraRaycaster.layerHit)
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
            m_Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

