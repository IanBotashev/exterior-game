using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float shiftMultiplier = 3;
    [SerializeField] float scrollRate;
    [SerializeField] Camera mainCamera;

    [Header("Clamp Zoom Values")]
    [SerializeField] float maxZoom;
    [SerializeField] float minZoom;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();    
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
        UpdateZoom();
    }

    private void UpdatePosition()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        float shiftMultiply = Mathf.Clamp(Convert.ToInt32(Input.GetKey(KeyCode.LeftShift)) * shiftMultiplier, 1, Mathf.Infinity);

        // Create direction to move in
        Vector3 direction = new(horizontal, vertical, 0f);

        // Move the camera in the direction of the axis
        transform.Translate(moveSpeed * shiftMultiply * Time.deltaTime * direction);
    }

    private void UpdateZoom()
    {
        float zoomValueChange = -Input.mouseScrollDelta.y * scrollRate;  // Get by how much we will change the zoom
        float newZoom = mainCamera.orthographicSize + zoomValueChange;  // Get the new zoom value
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);  // Clamp zoom value

        mainCamera.orthographicSize = newZoom;  // Set zoom to the new one.
    }
}
