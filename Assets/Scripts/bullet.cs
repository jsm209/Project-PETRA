using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    // How long to stay live until killing itself
    public float lifeTime;

    private void Start() {
        Object.Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
