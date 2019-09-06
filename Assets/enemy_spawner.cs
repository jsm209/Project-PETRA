using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {

    // The object to spawn.
    public GameObject spawn;

    // The spawner for objects.
    public GameObject spawner;

    // Max speed to shoot off spawns.
    public float maxSpeed;

    // How long between each spawn.
    public float interval;

    // How long until spawners multiply.
    public float multiplyInterval;

    // Use this for initialization
    void Start() {
        InvokeRepeating("SpawnEnemy", interval, interval);
        InvokeRepeating("SpawnEnemySpawner", multiplyInterval, multiplyInterval);
    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnEnemy() {
        GameObject enemy = Instantiate(spawn, transform.position, transform.rotation);
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.velocity = Random.onUnitSphere * Random.Range(0.1f, 1f) * maxSpeed;
    }

    void SpawnEnemySpawner() {
        Instantiate(spawner, transform.position, transform.rotation);
    }
}
