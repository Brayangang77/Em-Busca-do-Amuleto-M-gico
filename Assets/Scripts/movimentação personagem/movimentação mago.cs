using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    Vector2 movimento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movimento.x = Input.GetAxisRaw("Horizontal");
        movimento.y = Input.GetAxisRaw("Vertical");

        if (movimento.x != 0)
    {
    float tamanho = Mathf.Abs(transform.localScale.x);

    transform.localScale = new Vector3(
        movimento.x > 0 ? tamanho : -tamanho,
        transform.localScale.y,
        transform.localScale.z
    );
}
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimento.normalized * speed * Time.fixedDeltaTime);
    }
}