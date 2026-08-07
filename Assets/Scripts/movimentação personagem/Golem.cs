using UnityEngine;

public class Golem : MonoBehaviour
{
    public float velocidade = 1f;
    public Transform inimigo;

    public GameObject bolaDeGelo;
    public Transform pontoDisparo;
    public float distanciaAtaque = 5f;
    public float tempoAtaque = 2f;

    Rigidbody2D rb;
    float proximoAtaque;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float direcao = inimigo.position.x > transform.position.x ? 1 : -1;

        transform.localScale = new Vector3(
            -direcao * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );

        if (Vector2.Distance(transform.position, inimigo.position) <= distanciaAtaque)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time > proximoAtaque)
            {
                Atacar(direcao);
                proximoAtaque = Time.time + tempoAtaque;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(
                direcao * velocidade,
                rb.linearVelocity.y
            );
        }
    }

    void Atacar(float direcao)
    {
        GameObject gelo = Instantiate(
            bolaDeGelo,
            pontoDisparo.position,
            Quaternion.identity
        );

        gelo.GetComponent<IceProjectile>()
            .SetDirection(new Vector2(direcao, 0));
    }
}