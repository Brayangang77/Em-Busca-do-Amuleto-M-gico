using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 20f;

    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            collision.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}