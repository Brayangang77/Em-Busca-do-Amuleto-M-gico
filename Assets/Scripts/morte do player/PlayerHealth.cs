using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float vida = 100;

    public void TakeDamage(float dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
