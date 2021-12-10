using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{


    [SerializeField]
    private GameObject shapePrefab;
    [SerializeField]
    private GameObject blocksContainer;

    [SerializeField]
    private GameObject cubePrefab;
    [SerializeField]
    private GameObject spherePrefab;
    [SerializeField]
    private GameObject cilinderPrefab;
    [SerializeField]
    private GameObject capsulePrefab;

    private float lastMinuteHardCodedZOffset = 2f;

    private Vector3 mousePositionOnInstantiate;

   //public void InstantiateShape()
   //{
   //    mousePositionOnInstantiate = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   //    mousePositionOnInstantiate.z = lastMinuteHardCodedZOffset;
   //
   //    GameObject shape = Instantiate(shapePrefab, mousePositionOnInstantiate, Quaternion.identity);
   //    shape.transform.parent = blocksContainer.transform;
   //    //pick a random shape
   //    //add it as a component
   //    AddSnappingPointsToShape(shape);
   //} 

    public void InstantiateShape(GameObject prefab)
    {
        mousePositionOnInstantiate = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionOnInstantiate.z = lastMinuteHardCodedZOffset;

        GameObject shape = Instantiate(prefab, mousePositionOnInstantiate, Quaternion.identity);
        shape.transform.parent = blocksContainer.transform;
    }

    private void AddSnappingPointsToShape(GameObject shape)
    {
        Mesh shapeMesh = shape.GetComponent<Mesh>();
        Vector3 shapeCenter = shapeMesh.bounds.center;
        Vector3 shapeSize = shapeMesh.bounds.size;

        //adding half the size in each direction to the center to obtain the snapping points. (It works only for symmetrical shapes)
        Vector3 topSnappingPoint = shapeCenter + Vector3.up * shapeSize.y;
        Vector3 bottomSnappingPoint = shapeCenter + Vector3.up * -shapeSize.y;
        Vector3 rightSnappingPoint = shapeCenter + Vector3.right * shapeSize.x;
        Vector3 leftSnappingPoint = shapeCenter + Vector3.right * -shapeSize.x;
        Vector3 frontSnappingPoint = shapeCenter + Vector3.forward * shapeSize.z;
        Vector3 backSnappingPoint = shapeCenter + Vector3.forward * -shapeSize.z;

        //Instantiate empty game objects at the above Vector3 positions, as childs of the shape. And add the SnappingPoint script component to them

    }





}
