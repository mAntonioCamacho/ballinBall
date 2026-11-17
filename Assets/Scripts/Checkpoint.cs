using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public uint CheckpointID;
    private Vector2 Position;
    public static event Action<Checkpoint> NotifyCheckpointManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Position = this.transform.position;
        NotifyCheckpointManager?.Invoke(this);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    } 

    public Vector2 GetPosition()
    {
        return Position;
    }

    public float GetPositionY()
    {
        return Position.y;
    }
}
