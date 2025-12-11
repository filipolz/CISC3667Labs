using UnityEngine;

public class PinController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Vector2 moveDirection = Vector2.right; 

    public AudioClip popSound; 

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            if (popSound != null)
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
            }

            BalloonBobbing balloon = other.GetComponent<BalloonBobbing>();
            if (balloon != null)
            {
                balloon.Respawn();
            }

            GameManager.instance.AddPoint();

            Destroy(gameObject);
        }
    }
}