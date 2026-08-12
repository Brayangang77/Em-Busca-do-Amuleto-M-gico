using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float velocidade = 6f;
    public float dano = 20f;
    public float tempoVida = 3f;

    [Header("Resistência")]
    public int vida = 1;

    Vector2 direcao;

    void Start()
    {
        Destroy(gameObject, tempoVida);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetDirection(Vector2 novaDirecao)
    {
        direcao = novaDirecao.normalized;
    }

    void Update()
    {
        transform.position +=
            (Vector3)(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            colisao.GetComponent<PlayerHealth>()?.TakeDamage(dano);
            Destroy(gameObject);
            return;
        }

        if (colisao.CompareTag("PlayerFireball"))
        {
            vida--;

            Destroy(colisao.gameObject);

            if (vida <= 0)
                Destroy(gameObject);

            return;
        }

        if (colisao.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}