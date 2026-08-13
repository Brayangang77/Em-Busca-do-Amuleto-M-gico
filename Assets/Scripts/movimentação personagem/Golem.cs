using UnityEngine;

public class Golem : MonoBehaviour
{
    public float velocidade = 1f;

    [Header("Referencias")]
    public Transform inimigo;
    public GameObject bolaDeGelo;
    public GameObject bolaDeGeloGrande;
    public Transform pontoDisparo;
    public GameObject defesaGelo;

    [Header("Movimento")]
    [Range(0.1f, 0.9f)]
    public float posicaoInicial = 0.1f;
    public float distanciaAtaque = 2f;
    public float tolerancia = 0.1f;

    [Header("Ataque")]
    public float intervaloEntreBolas = 0.5f;
    public float intervaloEntreAtaques = 2f;
    public int quantidadeBolas = 3;

    private Rigidbody2D rb;

    private float proximoDisparo;
    private float proximoAtaque;

    private int bolasDisparadas;

    private bool atacando;
    private bool protegido = true;
    private bool chegouAos10 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (defesaGelo != null)
            defesaGelo.SetActive(true);

        
        proximoDisparo = Time.time;
    }

    void FixedUpdate()
    {
        if (inimigo == null || Camera.main == null)
            return;

        if (!chegouAos10)
        {
            IrAte10Porcento();
            Atacar();
            return;
        }

        if (inimigo.position.x > transform.position.x + distanciaAtaque)
        {
            PerseguirPlayer();
            return;
        }

        PararEAtacar();
    }

    void IrAte10Porcento()
    {
        float pontoDeParada = Camera.main.ViewportToWorldPoint(
            new Vector3(
                posicaoInicial,
                0.5f,
                -Camera.main.transform.position.z
            )
        ).x;

        if (transform.position.x > pontoDeParada + tolerancia)
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

        chegouAos10 = true;
        protegido = false;

        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );

        if (defesaGelo != null)
            defesaGelo.SetActive(false);

        VirarParaPlayer();
    }

    void PerseguirPlayer()
    {
        protegido = false;
        atacando = false;

        rb.linearVelocity = new Vector2(
            velocidade,
            rb.linearVelocity.y
        );

        VirarParaPlayer();
    }

    void PararEAtacar()
    {
        protegido = false;

        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );

        VirarParaPlayer();
        Atacar();
    }

    void Atacar()
    {
        if (!atacando && Time.time >= proximoAtaque)
        {
            atacando = true;
            bolasDisparadas = 0;
            proximoDisparo = Time.time;
        }

        if (!atacando)
            return;

        if (Time.time < proximoDisparo)
            return;

        DispararBola();

        bolasDisparadas++;

        if (bolasDisparadas >= quantidadeBolas)
        {
            atacando = false;
            proximoAtaque = Time.time + intervaloEntreAtaques;
        }
        else
        {
            proximoDisparo = Time.time + intervaloEntreBolas;
        }
    }

    void VirarParaPlayer()
    {
        float direcao;

        if (inimigo.position.x > transform.position.x)
            direcao = 1f;
        else
            direcao = -1f;

        transform.localScale = new Vector3(
            -direcao * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    void DispararBola()
    {
        GameObject prefab;

        if (Random.value < 0.7f)
            prefab = bolaDeGelo;
        else
            prefab = bolaDeGeloGrande;

        if (prefab == null || pontoDisparo == null)
            return;

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

        IceProjectile projetil =
            gelo.GetComponent<IceProjectile>();

        if (projetil != null)
            projetil.SetDirection(direcao);
    }

    public bool EstaProtegido()
    {
        return protegido;
    }
}