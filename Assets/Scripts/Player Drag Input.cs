using System;
using System.Collections.Concurrent;
using UnityEngine;

public class PlayerDragInput : MonoBehaviour
{
    public GameObject arrowObject;

    [SerializeField] private float defaultMaxLength = 1f;

    private Camera mainCamera;
    private Vector3 mouseStartWorldPos;

    public Vector3 FinalDragVector { get; set; } = Vector2.zero;

    void Start()
    {
        mainCamera = Camera.main;

        if(arrowObject != null)
        {
            arrowObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        mouseStartWorldPos = GetMouseWorldPosition();
        if (arrowObject != null)
        {
            arrowObject.SetActive(true);
        }
    }


    void OnMouseDrag()
    {
        if(arrowObject == null) return;
        
        Vector3 currentMousePos = GetMouseWorldPosition();
        Vector3 dragVector = currentMousePos - mouseStartWorldPos;
        float currentDistance = dragVector.magnitude;

        if(currentDistance > .001f)
        {
            float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        }
    }


    void OnMouseUp()
    {
        Vector3 currentMousePos = GetMouseWorldPosition();
        Vector3 dragVector = currentMousePos - mouseStartWorldPos;
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

        if(arrowObject != null)
        {
            arrowObject.SetActive(false);
        }
    }


    float GetMaxArrowLength()
    {
        return defaultMaxLength;
    }


    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
