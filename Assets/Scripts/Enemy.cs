using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Waveconfig waveConfig;
    private List<Transform> waypoints;
    private Vector2 laserMotion = new Vector2(0, 0);
    private Coroutine firingCoroutine;

    [Header("Enemy")]
    [SerializeField] float health = 500f;

    [Header("Explosion Effect")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Laser Movement")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float firingPeriod = 0.5f;

    int waypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = CurrentPosition();
        ResetShotCounter();
        laserMotion.y -= laserSpeed;
    }

    private void Update()
    {
        EnemyWaypoints();
        CountDownAndShoot();
    }

    private void ResetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    void Fire()
    {
         GameObject newLaser = Instantiate(laser, transform.position, Quaternion.identity);
         newLaser.GetComponent<Rigidbody2D>().velocity = laserMotion;
    }

    private void EnemyWaypoints()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector2 targetPosition = CurrentPosition();
            float step = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            if (IsEnemyArrived(targetPosition))
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (!laser) return;
        ProcessHit(laser);
    }

    private void ProcessHit(Laser laser)
    {
        health -= laser.GetDamage();
        laser.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }
    }

    public void SetWaveConfig(Waveconfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private bool IsEnemyArrived(Vector2 targetPosition)
    {
        return (transform.position.x == targetPosition.x) && (transform.position.y == targetPosition.y);
    }

    private Vector2 CurrentPosition()
    {
        return waypoints[waypointIndex].transform.position;
    }
}
