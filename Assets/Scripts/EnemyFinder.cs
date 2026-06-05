using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        FindClosestEnemy();
    }

    void FindClosestEnemy()
    {
        //No se cambiar el obsoleto por otro parecido y que funcione
        //por eso utilizo el obsoleto
        //EnemyController[] enemigos;
        //enemigos = GameObject.FindAnyObjectByType<EnemyController>();
        EnemyController[] allEnemies = GameObject.FindObjectsOfType<EnemyController>();

        float closestDistance = Mathf.Infinity;
        EnemyController closestEnemy = null;

        foreach (EnemyController enemy in allEnemies)
        {
            float distance = (enemy.transform.position - player.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            player.LookAt(closestEnemy.transform);
        }
    }
}