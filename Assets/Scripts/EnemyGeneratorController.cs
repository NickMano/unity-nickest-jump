using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {
    public GameObject enemyPrefab;
    public float generationTime = 1.75f;
    void createEnemy(){
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void startGeneration(){
        InvokeRepeating("createEnemy", 0f, generationTime);
    }

    public void finish(bool clean = false)
    {
        CancelInvoke("createEnemy");
        
        if (clean) {
            object[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach(GameObject enemy in enemies){
                Destroy(enemy);
            }

        }
    }
}
