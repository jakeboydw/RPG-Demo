using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float wayPointRadius = 0.1f;
    private int currentWayPointIndex = 0;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (wayPoints.Length == 0)
        {
            return;
        }
        Patrol();
    }

    private void Patrol()
    {
        Transform targetWayPoint = wayPoints[currentWayPointIndex];
        Vector2 direction = targetWayPoint.position - transform.position;
        float distance = direction.magnitude;

        if (distance < wayPointRadius)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;   //switch to next waypoint
            animator.SetBool("isMoving", false);
        }
        else
        {
            Vector2 moveDirection = direction.normalized;
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
            animator.SetBool("isMoving", true);

            //enemy uses MoveTowards method,but Rigidbody.MovePosition() method is better for character
            transform.position = Vector2.MoveTowards(transform.position, targetWayPoint.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement playerController = collision.GetComponent<PlayerMovement>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }
        }
    }
}
