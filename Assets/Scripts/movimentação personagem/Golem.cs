using UnityEngine;

public class Golem : MonoBehaviour
{
    public float velocidade = 1f;

    [Header("Referências")]
    public Transform inimigo;
    public GameObject bolaDeGelo, bolaDeGeloGrande;
    public Transform pontoDisparo;
    public GameObject defesaGelo;

    [Header("Posição de Ataque")]
    [Range(0.1f, 0.9f)]
    public float posicaoAtaque = 0.65f;
    public float tolerancia = 0.1f;

    [Header("Ataque")]
    public float intervaloEntreBolas = 0.5f;
    public float intervaloEntreAtaques = 2f;
    public int quantidadeBolas = 3;

    Rigidbody2D rb;
    float proximoDisparo, proximoAtaque;
    int bolasDisparadas;
    bool atacando;
    bool protegido = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (defesaGelo != null)
            defesaGelo.SetActive(true);
    }

    void FixedUpdate()
    {
        if (inimigo == null || Camera.main == null) return;

        float alvo = Camera.main.ViewportToWorldPoint(
            new Vector3(posicaoAtaque, 0.5f, -Camera.main.transform.position.z)
        ).x;

        float distancia = alvo - transform.position.x;

        // Enquanto não chegou ao ponto de ataque, vai da direita para a esquerda
        if (Mathf.Abs(distancia) > tolerancia)
        {
            rb.linearVelocity = new Vector2(
                -velocidade,
                rb.linearVelocity.y
            );

            protegido = true;

            if (defesaGelo != null)
                defesaGelo.SetActive(true);

            VirarParaPlayer();
            return;
        }

        // Chegou ao ponto de ataque
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        protegido = false;

        if (defesaGelo != null)
            defesaGelo.SetActive(false);

        // Continua olhando para o Player
        VirarParaPlayer();

        // Ataque
        if (!atacando && Time.time >= proximoAtaque)
        {
            atacando = true;
            bolasDisparadas = 0;
            proximoDisparo = Time.time;
        }

        if (atacando && Time.time >= proximoDisparo)
        {
            DispararBola();

            if (++bolasDisparadas >= quantidadeBolas)
            {
                atacando = false;
                proximoAtaque = Time.time + intervaloEntreAtaques;
            }
            else
            {
                proximoDisparo = Time.time + intervaloEntreBolas;
            }
        }
    }

    void VirarParaPlayer()
    {
        float direcao = inimigo.position.x > transform.position.x ? 1 : -1;

        transform.localScale = new Vector3(
            -direcao * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    void DispararBola()
    {
        GameObject prefab = Random.value < 0.7f
            ? bolaDeGelo
            : bolaDeGeloGrande;

        GameObject gelo = Instantiate(
            prefab,
            pontoDisparo.position,
            Quaternion.identity
        );

        float angulo = Mathf.Atan2(
            inimigo.position.y - pontoDisparo.position.y,
            inimigo.position.x - pontoDisparo.position.x
        ) * Mathf.Rad2Deg;

        angulo = Mathf.Round(angulo / 45f) * 45f;

        Vector2 direcao = new Vector2(
            Mathf.Cos(angulo * Mathf.Deg2Rad),
            Mathf.Sin(angulo * Mathf.Deg2Rad)
        ).normalized;

        gelo.GetComponent<IceProjectile>()?.SetDirection(direcao);
    }

    public bool EstaProtegido()
    {
        return protegido;
    }
}