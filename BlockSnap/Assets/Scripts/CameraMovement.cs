using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    /// <summary>
    /// Rotation speed in degrees per second
    /// </summary>
    [SerializeField]
    private float rotationSpeed = 60f;
    [SerializeField]
    private float cameraDistanceMax = 30f;
    [SerializeField]
    private float cameraDistanceMin = 5f;

    private float cameraDistance;
    private float horizontalRotation;

    private void Start()
    {
        cameraDistance = camera.transform.position.z;
    }
    private void Update()
    {
        //Compute rotation based on key presses
        float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        //Rotate around the Oy Axis
        camera.transform.RotateAround(Vector3.zero, Vector3.up, horizontalRotation);

        
        //Compute new camera distance
        cameraDistance += Input.GetAxis("Mouse ScrollWheel"); //Mouse functions already include Time.deltaTime, so no need to add it        
        cameraDistance = Mathf.Clamp(cameraDistance, -cameraDistanceMax, -cameraDistanceMin);
        //Debug.Log(cameraDistance.ToString());
        //Modify camera position
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, cameraDistance);
    }
}