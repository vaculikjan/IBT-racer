using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackStart : MonoBehaviour //Track start checks if care moved through all checkpoints
{
    //Set in editor
    public PlayerMovement player;

    GameObject[] checkpoints;
    int passed_count = 0;
    
    void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    void OnTriggerEnter(Collider col)   //When car passes start check if all checkpoints were cleared
                                        //if they were give positive reward else do nothing -- that means
                                        //the car reversed into start line
    {
        if (col.gameObject.CompareTag("Player")) {
            foreach (GameObject cp in checkpoints) {
                if(!cp.GetComponent<Checkpoint>().passed) {
                    passed_count += 1;
                }
            }
            if (passed_count == 0) {
                player.AddReward(2.0f);
                player.EndEpisode();
            }

            else {
                passed_count = 0;
            }
        }

    }
}
