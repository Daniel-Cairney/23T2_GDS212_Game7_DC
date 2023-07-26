using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleDetection : MonoBehaviour
{
    [SerializeField] private Transform ripplePivotPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Ripple Detected");
            collision.GetComponent<EnemyVision>().CheckRipple(ripplePivotPoint);
        }
    }
}
