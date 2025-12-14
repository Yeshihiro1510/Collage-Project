using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}