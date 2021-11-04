using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour //Manages checkpoints when resetting the episode
{
    GameObject[] checkpoints;
    void Start()
    {
       checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    public void ResetCheckpoints() //Gets instances of all checkpoints and resets them to be able to give rewards again
    { 
        foreach (GameObject cp in checkpoints)
        {
            cp.GetComponent<Checkpoint>().passed = false;
        }
    }
}
