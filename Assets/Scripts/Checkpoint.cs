using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        CheckpointManager.CurrentCheckpoint = this.transform.position;
        this.gameObject.SetActive(false);
    } 
}
