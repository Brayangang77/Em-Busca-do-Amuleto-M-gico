using UnityEngine;

public class Golem : MonoBehaviour
{
    public float velocidade = 1f;
    public Transform inimigo;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float direcao = inimigo.position.x > transform.position.x ? 1 : -1;

        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);

        // vira o Golem sem alterar o tamanho
        float tamanho = Mathf.Abs(transform.localScale.x);

        transform.localScale = new Vector3(
            -direcao * tamanho,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}