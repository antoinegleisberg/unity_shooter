using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemies : MonoBehaviour
{
    public float minX = -10;
    public float maxX = 10;
    public float minY = -6;
    public float maxY = 6;

    public float timeBetweenSpawns = 5;

    public GameObject ennemyPrefab;
    public GameObject ennemyPrefabSpawner;

    private float timeBeforeNextSpawn;

    void Start()
    {
        timeBeforeNextSpawn = timeBetweenSpawns;
    }

    void Update()
    {
        timeBeforeNextSpawn -= Time.deltaTime;
        if (timeBeforeNextSpawn < 0)
        {
            timeBeforeNextSpawn = timeBetweenSpawns;
            StartCoroutine(spawnEnnemy());
        }
    }

    IEnumerator spawnEnnemy()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        GameObject spawner = Instantiate(ennemyPrefabSpawner, new Vector3(x, 0.5f, y), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(spawner);
        Instantiate(ennemyPrefab, new Vector3(x, 1.5f, y), Quaternion.identity);
    }
}
