using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float dragMultiplyer = 1.2f;

    public float zoomSpeed = 1.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 5.0f;

    private bool drag = false;
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    private Vector3 dragVelocity;

    private void Start()
    {
        resetCamera = Camera.main.transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            float zoom = Camera.main.orthographicSize - scroll * zoomSpeed;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            Camera.main.orthographicSize = zoom;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drag = true;
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }

        if (drag)
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            Vector3 targetPosition = origin - difference * 0.5f * dragMultiplyer;
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref dragVelocity, 0.1f);
        }

        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = resetCamera;
        }
    }
}
