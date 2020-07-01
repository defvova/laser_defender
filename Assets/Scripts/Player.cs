//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float acceleration = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float firingPeriod = 0.5f;

    private Vector2 playerMotion = new Vector2(0, 0);
    private Vector2 laserMotion = new Vector2(0, 0);

    private Camera mainCamera;
    private Coroutine firingCoroutine;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private readonly string axisHorizontalName = "Horizontal";
    private readonly string axisVerticalName = "Vertical";

    private void Start()
    {
        mainCamera = Camera.main;
        laserMotion.y = laserSpeed;
        SetUpBoundaries();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = laserMotion;

            yield return new WaitForSeconds(firingPeriod);
        }
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

    private void Move()
    {
        float motionX = GetMotion(axisHorizontalName, 0);
        float motionY = GetMotion(axisVerticalName, 1);
        playerMotion.x = Mathf.Clamp(motionX, minX, maxX);
        playerMotion.y = Mathf.Clamp(motionY, minY, maxY);
        transform.position = playerMotion;
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
