using System.Collections;
using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public float expansionSpeed = 1f;
    public float destroyTime = 5f;
    public float targetScale = 10f;
    public float alphaSpeed = 1f; // You can set this value in the Inspector
    public float spawnInterval = 1f; // You can set this value in the Inspector

    

    private void Start()
    {
        
        StartCoroutine(ExpandRoutine());
    }

    private IEnumerator ExpandRoutine()
    {
        float currentScale = 0f;

        while (currentScale < targetScale)
        {
            currentScale += expansionSpeed * Time.deltaTime;
            transform.localScale = Vector3.one * currentScale;

           

            yield return null;
        }

        Destroy(gameObject, destroyTime);
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(ExpandRoutine());
    }
}
