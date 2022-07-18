using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVC;

    private float moveSpeed = 10f;
    private float rotationSpeed = 100f;
    private float zoomAmount = 1f;
    private float zoomSpeed = 5f;

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 trargetFollowOffset;

    private void Start()
    {
        cinemachineTransposer = cinemachineVC.GetCinemachineComponent<CinemachineTransposer>();
        trargetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            inputMoveDirection.z += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            inputMoveDirection.z -= 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            inputMoveDirection.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            inputMoveDirection.x += 1;

        Vector3 moveVect = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVect * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 rotationVect = Vector3.zero;

        if (Input.GetKey(KeyCode.Keypad6))
            rotationVect.y += 1;
        if (Input.GetKey(KeyCode.Keypad4))
            rotationVect.y -= 1;

        transform.eulerAngles += rotationVect * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
            trargetFollowOffset.y -= zoomAmount;
        if (Input.mouseScrollDelta.y < 0)
            trargetFollowOffset.y += zoomAmount;

        trargetFollowOffset.y = Mathf.Clamp(trargetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Slerp(cinemachineTransposer.m_FollowOffset, trargetFollowOffset,
            Time.deltaTime * zoomSpeed);
    }
}