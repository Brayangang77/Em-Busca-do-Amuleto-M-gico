using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            movement.y = 1;

        if (Input.GetKey(KeyCode.S))
            movement.y = -1;

        if (Input.GetKey(KeyCode.A))
            movement.x = -1;

        if (Input.GetKey(KeyCode.D))
            movement.x = 1;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}