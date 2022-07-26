using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float levelLength;
    public float levelTimer;
    private GameObject enemyHolder;

    public List<HazardSpawnInfo> hazards;
    public List<EnemySpawnInfo> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyHolder = GameObject.FindGameObjectWithTag("EnemyHolder");
        levelTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelTimer += Time.deltaTime;

        for (int i = 0; i < hazards.Count; i++)
        {
            if (levelTimer >= hazards[i].spawnTime)
            {
                // play entrance animation?
                GameObject hazard = Instantiate(hazards[i].prefab, hazards[i].position, Quaternion.identity, enemyHolder.transform);
                DamagerBehavior hazardBehavior = hazard.GetComponent<DamagerBehavior>();
                hazardBehavior.existTime = hazards[i].existTime;
                hazards.RemoveAt(i);
                continue;
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (levelTimer >= enemies[i].spawnTime)
            {
                // play entrance animation?
                GameObject enemy = Instantiate(enemies[i].prefab, enemies[i].position, Quaternion.identity, enemyHolder.transform);
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
                enemyBehavior.existTime = enemies[i].existTime;
                enemyBehavior.health = enemies[i].health;
                enemies.RemoveAt(i);
                continue;
            }
        }
    }
}
