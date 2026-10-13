using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [SerializeField] private float _drawLineLength;
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _drawLineLength);
    }
}
