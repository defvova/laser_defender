using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;

    int waypointIndex = 0;

    private void Start()
    {
        transform.position = CurrentPosition();
    }

    private void Update()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Vector2 targetPosition = CurrentPosition();
            float step = moveSpeed * Time.deltaTime;

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

    private bool IsEnemyArrived(Vector2 targetPosition)
    {
        return (transform.position.x == targetPosition.x) && (transform.position.y == targetPosition.y);
    }

    private Vector2 CurrentPosition()
    {
        return waypoints[waypointIndex].transform.position;
    }
}
