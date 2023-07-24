using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstuctionMask;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVroutine());
    }

    private IEnumerator FOVroutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }

        void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if(rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector2 directionToTarget = (target.position - transform.position).normalized; 

                if(Vector2.Angle(transform.position, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstuctionMask))
                        canSeePlayer = true;
                    else
                        canSeePlayer= false;
                    
                }
                else 
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
            {
                canSeePlayer= false;
            }
        }

        
    }
}
