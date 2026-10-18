using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, Min(0.1f)] private float generationTime = 1.75f;
    private bool isGenerating = false;

    private readonly List<GameObject> enemies = new();

    void OnDisable()
    {
        StopGeneration();
    }

    void CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemies.Add(enemy);
        enemies.RemoveAll(enemy => enemy == null);
    }

    public void StartGeneration()
    {
        if (isGenerating) return;

        if (enemyPrefab == null || generationTime <= 0f)
        {
            Debug.LogError("Configuración del generador inválida.", this);
            return;
        }

        isGenerating = true;
        InvokeRepeating(nameof(CreateEnemy), 0f, generationTime);
    }

    public void StopGeneration()
    {
        CancelInvoke(nameof(CreateEnemy));
        isGenerating = false;
    }

    public void Finish()
    {
        StopGeneration();

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                Destroy(enemy);

        }

        enemies.Clear();
    }
}
