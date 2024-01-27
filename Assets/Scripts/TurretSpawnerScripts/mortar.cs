using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar : MonoBehaviour
{
    public Transform firePoint; // F�rlatma noktas�
    public Rigidbody projectilePrefab; // F�rlat�lacak obje prefab�
    [SerializeField]private float firingAngle; // A�� (derece cinsinden)
    [SerializeField]private float firingSpeed; // H�z
    [SerializeField]private float detectionRange; // Alg�lama mesafesi
    [SerializeField]private float fireRate; // At�� h�z�
    private float nextFireTime = 0f; // Sonraki at�� zaman�
    private Transform currentTarget; // Aktif hedef

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            FindTarget();
            if (currentTarget != null)
            {
                FireAtTarget(currentTarget.position);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= detectionRange && distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = enemy.transform;
            }
        }
    }

    void FireAtTarget(Vector3 targetPosition)
    {
        // Fire point'te bir pozisyon olu�tur
        Vector3 firePointPosition = firePoint.position;

        // Objeyi f�rlatma i�lemi
        Rigidbody projectileInstance = Instantiate(projectilePrefab, firePointPosition, Quaternion.identity);

        // Hedefe do�ru vekt�r� hesapla
        Vector3 targetDir = targetPosition - firePointPosition;

        // Yatay mesafeyi ve y�kseklik fark�n� hesapla
        float horizontalDistance = Vector3.Distance(new Vector3(targetPosition.x, 0f, targetPosition.z), new Vector3(firePointPosition.x, 0f, firePointPosition.z));
        float verticalDistance = targetPosition.y - firePointPosition.y;

        // A��y� dereceden radyana �evir ve atanacak a��y� ayarla
        float angle = firingAngle * Mathf.Deg2Rad;

        // Yatay mesafeyi hesapla
        float distance = Vector3.Distance(firePointPosition, targetPosition);

        // H�z� kullanarak projectileSpeed'i ayarla
        float projectileSpeed = firingSpeed;

        // F�rlatma kuvveti vekt�r�n� hesapla ve uygula
        Vector3 velocity = projectileSpeed * targetDir.normalized;
        projectileInstance.velocity = velocity;
    }
}
