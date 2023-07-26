using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float radius;
    public float angle;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float enemyRotateSpeed;
    public float rotationDuration = 3f;
    public Transform originPosition;

    private Transform player;
    private bool isChasing = false;
    private bool atOrigin = true;
    private float targetRotation;
    private float rotationTimer;

    private float currentRotation = 0f;
    private bool reachedOrigin = false;

    private bool isMovingToRipple = false;
    private Vector3 rippleLocation;
    


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        bool playerDetected = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector2 direction = (player.position - transform.position).normalized;
                float angleToPlayer = Vector2.Angle(transform.up, direction);

                if (angleToPlayer <= angle / 2f && !ObstructedVision())
                {
                    playerDetected = true;
                    isChasing = true;
                    reachedOrigin = false; // Reset the flag when the enemy starts chasing
                    ChasePlayer();
                    break;
                }
            }
        }

        // If no player is found within the cone or the vision is obstructed, stop chasing and return to origin position
        isChasing = playerDetected;
        if (!isChasing)
        {
            if (isMovingToRipple)
            {
                MoveToRipple();
            }

            else if (!atOrigin)
            {
                RotateInPlace();
            }
            else
            {
                ReturnToOrigin();
                ConstantRotate();
            }
        }
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
        atOrigin = false;

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

        // Check if the enemy has reached the origin point
        if (!reachedOrigin && Vector3.Distance(transform.position, originPosition.position) < 0.1f)
        {
            reachedOrigin = true;
            currentRotation = transform.eulerAngles.z; // Set the initial rotation for constant rotation
        }
    }

    private void RotateInPlace()
    {
        // Rotate the enemy in place for a duration
        rotationTimer += Time.deltaTime;

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0f;
            atOrigin = true;
        }
        else
        {
            // Rotate the enemy towards the target rotation smoothly
            float targetAngle = (transform.eulerAngles.z + enemyRotateSpeed * Time.deltaTime) % 360f;
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);

            // Update the target rotation for the next frame
            targetRotation = targetAngle;
        }
    }

    private void ConstantRotate()
    {
        if (reachedOrigin)
        {
            // Rotate the enemy constantly while at the origin
            currentRotation += enemyRotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
    }

    public void CheckRipple(Transform ripple)
    {
        Debug.Log("Move Towards" + ripple.position);
      
        isMovingToRipple= true;
        rippleLocation = ripple.position;
    }

    public void MoveToRipple()
    {
        Vector2 direction = (rippleLocation - transform.position).normalized;
        Vector2 movement = direction * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movement.x, movement.y, 0f);
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the enemy towards the player smoothly
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (transform.position == rippleLocation)
        {
            isMovingToRipple= false;
        }
    }

}
