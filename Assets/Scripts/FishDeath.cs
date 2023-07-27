using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ded");
            SceneManager.LoadScene("Death Scene");
        }
    }

}