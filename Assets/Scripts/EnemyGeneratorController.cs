using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController: MonoBehaviour {
    public GameObject enemyPrefab;
    public float generationTime = 1.75f;
    
    void CreateEnemy() {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void StartGeneration() {
        InvokeRepeating("CreateEnemy", 0f, generationTime);
    }

    public void Finish(bool clean = false) {
        CancelInvoke("CreateEnemy");
        
        if (clean) {
            object[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach(GameObject enemy in enemies) {
                Destroy(enemy);
            }

        }
    }
}
