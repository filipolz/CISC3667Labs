using UnityEngine;

public class BalloonBobbing : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;

    private Vector2 startingPosition;

    // NEW! Variables for screen boundaries
    private Camera mainCamera;
    private float screenMinX, screenMaxX, screenMinY, screenMaxY;

    void Start()
    {
        startingPosition = transform.position;

        // NEW! Get the camera and calculate screen edges
        mainCamera = Camera.main;

        // Get screen boundaries in world coordinates
        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // Add a little padding so it doesn't spawn right on the edge
        screenMinX = screenBottomLeft.x + 1;
        screenMaxX = screenTopRight.x - 1;
        screenMinY = screenBottomLeft.y + 1;
        screenMaxY = screenTopRight.y - 1;
    }

    void Update()
    {
        float newY = startingPosition.y + (floatHeight * Mathf.Sin(Time.time * floatSpeed));
        transform.position = new Vector2(startingPosition.x, newY);
    }

    // NEW! This function can be called by other scripts (like our Pin)
    public void Respawn()
    {
        // Pick a new random X and Y within the screen
        float randomX = Random.Range(screenMinX, screenMaxX);
        float randomY = Random.Range(screenMinY, screenMaxY);

        // Set the new position
        transform.position = new Vector2(randomX, randomY);

        // We also MUST reset its "startingPosition" for the bobbing
        startingPosition = transform.position;
    }
}