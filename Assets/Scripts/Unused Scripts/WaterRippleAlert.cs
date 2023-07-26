using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRippleAlert : MonoBehaviour
{
    public float attractionRadius = 5f;
    public float attractionSpeed = 5f;
    public float rotateSpeed = 5f; // Define the rotate speed here
    public Transform pivotPoint;

    private EnemyPivotMovement enemyPivotMovement;

    private void Start()
    {
        enemyPivotMovement = GetComponent<EnemyPivotMovement>();
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attractionRadius);
        bool attracted = false;

        foreach (var hit in hits)
        {
            
            if (hit.CompareTag("Attract"))
            {
                attracted = true;
                enemyPivotMovement.MoveToPivot(pivotPoint);
                break;
            }
        }

        if (!attracted)
        {
            // Continue normal behavior (e.g., chasing the player)
        }
    }
}
