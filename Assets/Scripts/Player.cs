//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float acceleration = 10f;

    private Vector2 motion = new Vector2(0, 0);
    private readonly string axisHorizontalName = "Horizontal";
    private readonly string axisVerticalName = "Vertical";

    void Update()
    {
        Move();
    }

    private void Move()
    {
        motion.x = GetVelocity(axisHorizontalName) + transform.position.x;
        motion.y = GetVelocity(axisVerticalName) + transform.position.y;
        transform.position = motion;
    }

    private float GetVelocity(string axisName)
    {
        return Input.GetAxis(axisName) * Time.deltaTime * acceleration;
    }
}
