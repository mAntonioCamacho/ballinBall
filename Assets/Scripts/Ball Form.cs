using UnityEngine;

[CreateAssetMenu(menuName = "Player/BallForm")]
public class BallForm : ScriptableObject
{
    public Sprite sprite;
    public Color spriteColor;
    public PhysicsMaterial2D ballPhysics;
    public float launchPower;
    public float moveSpeed;
    public float playerGravity;
    public bool canBreakObjects;
    
}
