using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingManager : MonoBehaviour
{


    public static SnappingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private GameObject draggingObject;
    private List<Transform> snapPointsOfTheStructure = new List<Transform>();

    [SerializeField]
    private float snapRange = 0.4f;
    [SerializeField]
    private float snapTime = 0.3f;

    private Transform closestSnapPointOnBlock = null;
    private Transform closestSnapPointOnStructure = null;


    public void SetDraggingObject(GameObject block)
    {
        draggingObject = block;
    }

    private void RemoveDraggingObject()
    {
        draggingObject = new GameObject();
    }

    public void AddSnapPoint(Transform x)
    {
        snapPointsOfTheStructure.Add(x);
    }

    private void RemoveSnapPoint(Transform x)
    {
        snapPointsOfTheStructure.Remove(x);
    }

    public void ResetSnappingPoints()
    {
        snapPointsOfTheStructure = new List<Transform>();
    }


    public bool TrySnap()
    {
        float smallestDistance = 10;
        closestSnapPointOnBlock = null;
        closestSnapPointOnStructure = null;

        //itterate through all points on the block and all points on the structure to find if there are 2 that are close
        for (int i = 0; i < draggingObject.transform.childCount; i++)
        {
            Transform anchor = draggingObject.transform.GetChild(i);
            if (snapPointsOfTheStructure.Count != 0)
            {
                foreach (Transform snapPoint in snapPointsOfTheStructure)
                {
                    float distance = Vector3.Distance(snapPoint.position, anchor.position);
                    if (distance < snapRange && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        closestSnapPointOnBlock = anchor;
                        closestSnapPointOnStructure = snapPoint;
                    }
                }
            }
        }

        AddTheNewSnapPointsToStructure(draggingObject);

        if(closestSnapPointOnBlock != null && closestSnapPointOnStructure != null)
        {
            SnapBlock(draggingObject);
            return true;
        }

        else
        {
            return false;
        }
    }


    private void AddTheNewSnapPointsToStructure(GameObject block)
    {
        for (int i = 0; i < block.transform.childCount; i++)
        {
            Transform anchor = draggingObject.transform.GetChild(i);
            AddSnapPoint(anchor);
        }
    }

    private void SnapBlock(GameObject block)
    {

        block.transform.position = Vector3.Lerp(block.transform.position, closestSnapPointOnStructure.position, snapTime);

    }
}
