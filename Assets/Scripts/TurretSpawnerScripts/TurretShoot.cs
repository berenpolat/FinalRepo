using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    private Transform target;  // Hedefin transformu
    public float rotationSpeed = 1000f;  // Taret d�nme h�z�

    private float bulletspeed = 70f;
    // Ate�leme parametreleri
    public Transform firePoint;  // Mermi at�� noktas�
    public GameObject bulletPrefab;  // Mermi prefab�
    public float fireRate = 0.5f;  // Ate� h�z�
    private float fireCountdown = 0f;  // Ate�e kadar geri say�m

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        // Hedefi belirlemek i�in ba�lang��ta null atanm�� olabilir
        if (target == null)
        {
            Debug.LogError("Taretin bir hedefi olmal�!");
        }
    }

    void Update()
    {
        // Hedefe do�ru d�nme
        LockOnTarget();

        // Ate� etme kontrol�
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget()
    {
        // Hedefe do�ru d�nme
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        // Mermi olu�turma ve ate�leme
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletspeed;

    }

}
