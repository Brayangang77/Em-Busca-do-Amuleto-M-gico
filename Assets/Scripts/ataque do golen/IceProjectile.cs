using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public float velocidade = 6f;
    public float dano = 20f;

    Vector2 direcao;

    public void SetDirection(Vector2 novaDirecao)
    {
        direcao = novaDirecao.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            colisao.GetComponent<PlayerHealth>()?.TakeDamage(dano);
            Destroy(gameObject);
        }
    }
}