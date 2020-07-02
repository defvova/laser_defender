using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Waveconfig waveConfig;
    private List<Transform> waypoints;

    [SerializeField] float health = 500f;

    int waypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = CurrentPosition();
    }

    private void Update()
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
        PlayerLaser playerLaser = collision.gameObject.GetComponent<PlayerLaser>();
        ProcessHit(playerLaser);
    }

    private void ProcessHit(PlayerLaser playerLaser)
    {
        health -= playerLaser.GetDamage();
        playerLaser.Hit();
        if (health <= 0) Destroy(gameObject);
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
