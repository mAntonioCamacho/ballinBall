using System;
using UnityEngine;

public class PlayerDragInput : MonoBehaviour
{
    [SerializeField] private float arrowMaxLength = 1f;
    private Vector3 mouseStartWorldPos;
    private Camera _mainCam;
    public Vector3 FinalDragVector { get; set; } = Vector2.zero;
    public static event Action<Vector3> onDrawArrow;
    public static event Action<bool> onShowArrow;
    
    void Start()
    {
        // make sure the arrow object is turned off and not showing
        onShowArrow?.Invoke(false);
        _mainCam = Camera.main;
    }


    void OnMouseDown()
    {
        mouseStartWorldPos = GetMouseWorldPosition();
        // player pressed mouse button so turn on arrow object
        onShowArrow?.Invoke(true);
    }


    void OnMouseDrag()
    {
        // these values are calculating while the player holds button down and drags the mouse around
        // this is updating the arrow
        Vector3 currentMousePos = GetMouseWorldPosition();
        Vector3 dragVector = currentMousePos - mouseStartWorldPos;
        
        // update the arrow 
        onDrawArrow?.Invoke(dragVector);
        
    }


    void OnMouseUp()
    {
        // these values are used for the launch
        Vector3 currentMousePos = GetMouseWorldPosition();
        // -1 because we want the vector to shoot in the opposite direction of where the player drags the mouse
        Vector3 dragVector = (currentMousePos - mouseStartWorldPos) * -1;
        float currentDistance = dragVector.magnitude;

        float maxAllowedLength = GetMaxArrowLength();
        float finalLength = Mathf.Min(currentDistance, maxAllowedLength);

        if(currentDistance > .001f)
        {
            Vector2 direction = dragVector.normalized;
            FinalDragVector = direction * finalLength;
        }
        else
        {
            FinalDragVector = Vector2.zero;
        }

        onShowArrow?.Invoke(false);
    }


    float GetMaxArrowLength()
    {
        return arrowMaxLength;
    }


    Vector3 GetMouseWorldPosition()
    {
        // screen to world point returns the input of the screen space where the player clicked, but in the form of world space coordinates
        // this allows for small increments of power - the vector that is produced isn't just a massive value.
        // IE where the player clicked on screen to where the player dragged, that "vector" (dragVector) is what is made and used in later calculations
        return _mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
