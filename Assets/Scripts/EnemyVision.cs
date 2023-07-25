using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float radius;
    public float angle;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public Transform originPosition; 

    private Transform player;
    private bool isChasing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
      

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector2 direction = (player.position - transform.position).normalized;
                float angleToPlayer = Vector2.Angle(transform.up, direction);

                if (angleToPlayer <= angle / 2f && !ObstructedVision())
                {
                    isChasing = true;
                    ChasePlayer();
                    return;
                }
            }
        }

        // If no player is found within the cone or the vision is obstructed, stop chasing and return to origin position
        isChasing = false;
        ReturnToOrigin();
    }

    private bool ObstructedVision()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radius);

        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            return true; // Obstruction found between the enemy and player
        }

        return false; // No obstruction found
    }

    private void ChasePlayer()
    {
        if (!isChasing) return;

        // Disable the looking around rotation while chasing
        // (Keep the current rotation towards the player)
        isChasing = false;

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0f);

        // Calculate the angle to rotate towards the player
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the enemy towards the player smoothly
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void ReturnToOrigin()
    {
        
        // Enable the looking around rotation while in the origin position
        Vector2 direction = (originPosition.position - transform.position).normalized;
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0f);

        // Calculate the angle to rotate towards the origin position
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the enemy towards the origin position smoothly
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
