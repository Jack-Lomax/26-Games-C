using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Fruit[] fruits;
    [SerializeField] private Fruit[] bugs;

    Fruit spawnedFruit;
    Fruit spawnedBug;

    void Update()
    {
        if(spawnedFruit == null)
            SpawnFruit();
    }

    void Start()
    {
        StartCoroutine(SpawnBug());
    }

    IEnumerator SpawnBug()
    {
        yield return new WaitForSeconds(Random.Range(10, 15));
        if(spawnedBug)
            spawnedBug.Despawn();

        Vector3 spawnPoint = spawnedFruit.transform.position;
        while(Vector3.Distance(spawnedFruit.transform.position, spawnPoint) < 1)
            spawnPoint = GetRandomSpawn();
        spawnedBug = Instantiate(bugs[Random.Range(0, bugs.Length)], spawnPoint, Quaternion.identity);
        StartCoroutine(SpawnBug());
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, fruits.Length);

        spawnedFruit = Instantiate(fruits[index], GetRandomSpawn(), Quaternion.identity);
    }

    Vector3 GetRandomSpawn()
    {
        return new Vector3(Random.Range(-7, 7), 0.25f, Random.Range(-3, 3));
    }

}
