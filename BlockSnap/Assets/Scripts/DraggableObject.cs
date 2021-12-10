using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{

    private bool isDragged = false;

    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;

    private Vector3 mOffset;
    private float mZCoord;

    //To keep movement only in 2 dimensions
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown()
    {
        isDragged = true;
        SnappingManager.Instance.SetDraggingObject(gameObject);

        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPosition();
    }


    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mOffset;
    }


    private void OnMouseUp()
    {
        SnappingManager.Instance.TrySnap();
    }
}
