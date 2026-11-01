using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] private GameObject arrowObject;
    private float maxArrowLength = 1.25f;
    private float arrowGrowthSpeed = 6.4f;
    void Awake()
    {
        PlayerDragInput.onDrawArrow += OnDrawArrowObject;
        PlayerDragInput.onShowArrow += OnShowArrowObject;
    }

    private void OnDrawArrowObject(Vector3 dragVector)
    {
        
        RotateArrow(dragVector);
        StretchArrow(dragVector);
        
    }

    private void OnShowArrowObject(bool isShown)
    {
        if(!isShown) { ResetArrow(); }
        arrowObject.SetActive(isShown);
    }

    private void RotateArrow(Vector3 dragVector)
    {
        // get the angle from the x axis from when player clicked the mouse down to where the player dragged the mouse
        // drag vector = playerDragDirectionPosition - MouseClickDownPosition
        // the angle is from MouseClickDownPosition - our "x axis" 
        //
        //             *       '*' here  is drag position 
        //              \
        //               \    
        //            ----*----   '*' here is our x axis "click down position"
        // we get displacement vector throw it into Atan2 and we get our angle, plus that conversion from radian to degrees
        float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }
    private void StretchArrow(Vector3 dragVector)
    {
        float length = (dragVector.magnitude > maxArrowLength) ? maxArrowLength : dragVector.magnitude;
        Vector3 dragScaleArrow = new Vector3(length * arrowGrowthSpeed, arrowObject.transform.localScale.y, arrowObject.transform.localScale.z);
        // fix the direction of the arrow
        if(dragScaleArrow.x > 0) { dragScaleArrow.x *= -1; }
        

        arrowObject.transform.localScale = Vector3.Lerp(Vector3.zero, dragScaleArrow, 1);
    }

    private void ResetArrow()
    {
        arrowObject.transform.localPosition = Vector3.zero;
        arrowObject.transform.localScale = new Vector3(-3f,3f,0f);
    }
}
