using System.Collections;
using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public float expansionSpeed = 1f;
    public float destroyTime = 5f;
    public float targetScale = 10f;
    public float alphaSpeed = 1f; // You can set this value in the Inspector
    public float spawnInterval = 1f; // You can set this value in the Inspector

    private Material material;
    private float currentAlpha = 1f;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine(ExpandRoutine());
    }

    private IEnumerator ExpandRoutine()
    {
        float currentScale = 0f;

        while (currentScale < targetScale)
        {
            currentScale += expansionSpeed * Time.deltaTime;
            transform.localScale = Vector3.one * currentScale;

            if (currentScale >= targetScale)
            {
                // Calculate the new alpha value when it reaches the targetScale
                currentAlpha -= alphaSpeed * Time.deltaTime;
                currentAlpha = Mathf.Clamp01(currentAlpha); // Clamp the alpha value between 0 and 1
                Color newColor = material.color;
                newColor.a = currentAlpha;
                material.color = newColor;
            }

            yield return null;
        }

        Destroy(gameObject, destroyTime);
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(ExpandRoutine());
    }
}
