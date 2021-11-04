using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour //Script operating the camera
{
    public Transform car;
    public Vector3 targetPosition;

    //Set in editor
    public float followDamping; 
    public float cameraHeight; 
    public float cameraForwardSpeed;

    Rigidbody carRb;

    
    void Start()
    {
        carRb = car.GetComponent<Rigidbody> ();
    }

    void FixedUpdate() 
    {
        targetPosition = car.position + Vector3.up * cameraHeight + carRb.velocity * cameraForwardSpeed; //Calculation for camera position
        Vector3 maxAhead = targetPosition - car.position; //Maximum value to get camera ahead of the car
        
        if (maxAhead.x > 10) {
            targetPosition.x = car.position.x + 10;
        }
        else if (maxAhead.x < -10) {
            targetPosition.x = car.position.x - 10;
        }

        if (maxAhead.z > 10) {
            targetPosition.z = car.position.z + 10;
        }
        else if (maxAhead.z < -10) {
            targetPosition.z = car.position.z - 10;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping); //Setting camera position with smooth movement

    }

}
