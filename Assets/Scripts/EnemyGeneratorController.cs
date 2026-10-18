using System.Linq;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float generationTime = 1.75f;

    void CreateEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void StartGeneration()
    {
        InvokeRepeating(nameof(CreateEnemy), 0f, generationTime);
    }

    public void Finish()
    {
        CancelInvoke(nameof(CreateEnemy));

        object[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject enemy in enemies.Cast<GameObject>())
        {
            Destroy(enemy);
        }
    }
}
