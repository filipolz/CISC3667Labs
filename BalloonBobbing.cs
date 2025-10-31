using UnityEngine;

public class BalloonBobbing : MonoBehaviour
{
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;

    private Vector2 startingPosition;

    private Camera mainCamera;
    private float screenMinX, screenMaxX, screenMinY, screenMaxY;

    void Start()
    {
        startingPosition = transform.position;

        mainCamera = Camera.main;

        Vector2 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

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

    public void Respawn()
    {
        float randomX = Random.Range(screenMinX, screenMaxX);
        float randomY = Random.Range(screenMinY, screenMaxY);

        transform.position = new Vector2(randomX, randomY);

        startingPosition = transform.position;
    }
}