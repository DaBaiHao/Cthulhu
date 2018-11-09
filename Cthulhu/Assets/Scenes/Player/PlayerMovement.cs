using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;




[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;


    ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;

    Vector3 currentDestination, clickPoint;

    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;
    // TODO solve fight between serialize and const
    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

     void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;

        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }


    //TODO fix the problem with the increase speed WASD and click


    //// Fixed update is called in sync with physics
    //private void FixedUpdate()
    //{

    //    if (Input.GetKeyDown(KeyCode.G)) { // TODO allow player to map
    //        isInDirectMode = !isInDirectMode; // toggle mode
    //        currentDestination = transform.position; // clear the click target
    //    }


    //    if (isInDirectMode)
    //    {
    //         ProcessDirectMovement();
    //    }
    //    else
    //    {
    //        ProcessMouseMovement(); // mouse movement
    //    }




    //}

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayerNumber:
                // navigate to the enemy
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                // navigate to point on the ground
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("Don't know how to handle mouse click for player movement");
                return;
        }

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

    

    void ProcessDirectMovement()
    {
        // draw movment Gizmos
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;


        thirdPersonCharacter.Move(movement, false, false);
    }
}

