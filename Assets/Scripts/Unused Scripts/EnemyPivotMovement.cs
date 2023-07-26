using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPivotMovement : MonoBehaviour
{
    public float attractionSpeed = 5f;
    public float rotateSpeed = 5f; // Define the rotate speed here
    private Transform pivotPoint;
    private bool isMovingToPivot = false;

    public void MoveToPivot(Transform targetPivotPoint)
    {
        pivotPoint = targetPivotPoint;
        isMovingToPivot = true;
    }

    private void Update()
    {
        if (isMovingToPivot && pivotPoint != null)
        {
            Vector2 direction = (pivotPoint.position - transform.position).normalized;
            Vector2 movement = direction * attractionSpeed * Time.deltaTime;
            transform.position += new Vector3(movement.x, movement.y, 0f);

            // Calculate the angle to rotate towards the pivot point
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Rotate the enemy towards the pivot point smoothly
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Check if the enemy has reached the pivot point
            if (Vector2.Distance(transform.position, pivotPoint.position) <= 0.1f)
            {
                isMovingToPivot = false;
            }
        }
    }
}
