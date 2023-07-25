using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject objectToSpawn; // Assign the prefab of the object to spawn in the Inspector

    private Rigidbody2D rb;
    private float totalDistanceMoved = 0f;
    private Vector2 lastPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }

    private void Update()
    {
        // Horizontal movement
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveInputX, moveInputY).normalized * moveSpeed;
        rb.velocity = movement;

        // Calculate distance moved in this frame
        float distanceMoved = Vector2.Distance(rb.position, lastPosition);
        totalDistanceMoved += distanceMoved;
        lastPosition = rb.position;

        // Check if the player has moved 1 meter
        if (totalDistanceMoved >= 3f)
        {
            SpawnObject();
            totalDistanceMoved = 0f; // Reset the distance counter
        }
    }

    private void SpawnObject()
    {
        // Spawn the object at the player's position
        Instantiate(objectToSpawn, rb.position, Quaternion.identity);
    }
}
