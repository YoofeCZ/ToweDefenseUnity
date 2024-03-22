using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlowTower : UnifiedTurret
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;



    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / sps)
        {
            FreezeEnemies();
            timeUntilFire = 0;
        }
    }

private void FreezeEnemies()
{
    RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

    if (hits.Length > 0)
    {
        float speedModifier = 0.5f - (sps - 1) * 0.03f; // Pøíklad vzorce

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
            if (em != null)
            {
                em.UpdateSpeed(speedModifier); // Použijeme vypoèítaný modifikátor

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }
}


    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

}
