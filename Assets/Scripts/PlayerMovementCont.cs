using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class PlayerMovementCont : Agent //Agent script learning in this environment == The car -- continuouos action space
{
    //Set in editor
    public float speed;
    public float rotationSpeed;
    public CheckpointManagerCont cpm;
    
    float dir = 0; 
    float mag = 0;
    float rotation;
    float offTrack = 1;
    
    Rigidbody rb;
    
    public override void Initialize() {
        rb = GetComponent<Rigidbody> ();
        Physics.IgnoreLayerCollision(0, 9);
	}


    public override void OnEpisodeBegin() { //Whenever new episode begins this is called and sets the initial values for training
        cpm.ResetCheckpoints();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(-980f,0.2f,-819.5f);
        transform.eulerAngles = new Vector3(0,90,0);
    }

    public override void CollectObservations(VectorSensor sensor) { //Observation vector of floats
       sensor.AddObservation(transform.rotation.y);
       sensor.AddObservation(rb.velocity);
       sensor.AddObservation(rb.angularVelocity);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers) { //Whenever Action Vector is issued this is called 

        var continuousActions = actionBuffers.ContinuousActions;
        
        dir = continuousActions[1]; //Rotation direction
        mag = continuousActions[0]; //Movement magnitude

        if (mag > 0) {
            mag = 1;
        }
        
        else if (mag < 0) {
            mag = -0.75f;            
        
        }

        else {
            mag = 0;
        }

        rotation = rotationSpeed;

        if (rb.velocity.magnitude < 3) {
            rotation = rotation * (rb.velocity.magnitude/3);
        }
        else if (rb.velocity.magnitude == 0) {
            rotation = 0;
        }
        
    }

    public override void Heuristic(in ActionBuffers actionsOut) { //For controllng agent directly in Heuristics mode

        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Gas");
        
        continuousActionsOut[1] = Input.GetAxis("Horizontal");

    }

    void FixedUpdate() {

        transform.Rotate(0, dir * rotation, 0);
        rb.AddRelativeForce(Vector3.forward * mag * speed * offTrack);
        AddReward(-0.0001f);
        
        if (transform.position.y > 0.5 || transform.position.y < -0.5) {
            AddReward(-1f);
            EndEpisode();
        }
        
    }

    //Checking for collision with barrier -- ending episode and giving negative reward
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10) {
            AddReward(-1f);
            EndEpisode();
        }
    }
}   