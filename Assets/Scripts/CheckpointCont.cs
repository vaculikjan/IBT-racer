using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCont : MonoBehaviour //Code for a single Checkpoint guiding the car -- for continuous action space
{
    //Set in editor
    public PlayerMovementCont player;

    public bool passed = false;
    
    void OnTriggerEnter(Collider col) //When car enters this checkpoint it gets a reward and disables the checkpoint
    {
        if (col.gameObject.CompareTag("Player")) {
            if (!passed) {
                passed = true;
                player.AddReward(0.1f);
            }
        }
    }
}
