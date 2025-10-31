using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject pinPrefab;
    public Transform shootPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        if (moveInput.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void Shoot()
    {
        GameObject pin = Instantiate(pinPrefab, shootPoint.position, shootPoint.rotation);
        PinController pinScript = pin.GetComponent<PinController>();

        if (spriteRenderer.flipX == true)
        {
            pinScript.moveDirection = Vector2.left;
        }
        else
        {
            pinScript.moveDirection = Vector2.right;
        }
    }
}