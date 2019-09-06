using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour {

    // The target to chase
    private Transform target;

    public float speed;

    // How long to stay live until killing itself
    public float lifeTime;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Object.Destroy(gameObject, lifeTime);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
