using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = mousePosition - firePoint.position;

            GameObject fireball = Instantiate(
                fireballPrefab,
                firePoint.position,
                Quaternion.identity
            );

            fireball.GetComponent<Fireball>().SetDirection(direction);
        }
    }
}