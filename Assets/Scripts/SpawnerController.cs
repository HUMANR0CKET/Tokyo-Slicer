using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] items;
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        StartCoroutine(spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Instantiate(items[itemToSpawn()], spawnPosition(), Quaternion.identity, enemies.transform);
        }
    }

    private int itemToSpawn()
    {
        return Random.Range(0, items.Length);
    }

    private Vector3 spawnPosition()
    {
        return new Vector3 (Random.Range(36f, 42f), 4.0f, 0.0f);
    }
}
