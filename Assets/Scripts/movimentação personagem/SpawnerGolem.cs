using UnityEngine;

public class SpawnerGolem : MonoBehaviour
{
    public GameObject golemPrefab;
    public Transform jogador;

    public float tempo = 3f;
    public int quantidadeMaxima = 5;

    int quantidadeCriada = 0;

    void Start()
    {
        InvokeRepeating(nameof(CriarGolem), 1f, tempo);
    }

    void CriarGolem()
    {
        
        if (quantidadeCriada >= quantidadeMaxima)
        {
            CancelInvoke(nameof(CriarGolem));
            return;
        }

        GameObject novoGolem = Instantiate(golemPrefab, transform.position, Quaternion.identity);

        novoGolem.GetComponent<Golem>().inimigo = jogador;

        quantidadeCriada++;
    }
}