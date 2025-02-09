using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damageAmount;
        [SerializeField] private Shooting.BulletType bulletType;

        private BonusToGivePlayer bonusToGivePlayer;
        private EnemyAI enemyAI;
        private Collider[] collidersInsideRadius;
        private float bombEffectArea = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (bulletType == Shooting.BulletType.Mortar && other.gameObject.CompareTag("Floor"))
            {
                collidersInsideRadius = Physics.OverlapSphere(transform.position, bombEffectArea);
                foreach (var collider in collidersInsideRadius)
                {
                    if (collider.gameObject.tag.Equals("Enemy"))
                    {
                        DestroyEnemyOperations(collider.gameObject);
                    }
                }
            }
            else if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyAI>() != null)
            {
                DestroyEnemyOperations(other.gameObject);
            }
        }

        private void DestroyEnemyOperations(GameObject enemy)
        {
            enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.LowerHealth(damageAmount);
            if (!enemyAI.CheckIsDead())
            {
                Destroy(gameObject);
                return;
            }
            bonusToGivePlayer = enemyAI.GetMoneyAndPointInformation();
            GameManager.Instance.CurrentMoney += bonusToGivePlayer.givenMoneyToThePlayer;
            GameManager.Instance.CurrentPoints += bonusToGivePlayer.givenPointsToThePlayer;
            GameManager.Instance.CurrentHappiness += bonusToGivePlayer.givenHappinessToPlayer;
            enemyAI.DestroyEnemy();
            Destroy(gameObject);
        }
    }
}
