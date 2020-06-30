//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float acceleration = 10f;
    [SerializeField] float padding = 0.5f;

    private Vector2 motion = new Vector2(0, 0);

    private Camera mainCamera;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private readonly string axisHorizontalName = "Horizontal";
    private readonly string axisVerticalName = "Vertical";

    private void Start()
    {
        mainCamera = Camera.main;
        SetUpBoundaries();
    }

    private void SetUpBoundaries()
    {
        minX = GetBoundaries().x + padding;
        maxX = GetBoundaries(1).x - padding;

        minY = GetBoundaries().y + padding;
        maxY = GetBoundaries(0, 1).y - padding;
    }

    private Vector3 GetBoundaries(int x = 0, int y = 0, int z = 0)
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(x, y, z));
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float motionX = GetMotion(axisHorizontalName, 0);
        float motionY = GetMotion(axisVerticalName, 1);
        motion.x = Mathf.Clamp(motionX, minX, maxX);
        motion.y = Mathf.Clamp(motionY, minY, maxY);
        transform.position = motion;
    }

    private float GetMotion(string axisName, int vectorIndex)
    {
        return GetVelocity(axisName) + transform.position[vectorIndex];
    }

    private float GetVelocity(string axisName)
    {
        return Input.GetAxis(axisName) * Time.deltaTime * acceleration;
    }
}
