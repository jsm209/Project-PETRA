using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nugget : MonoBehaviour {

    private float angle;

	// Use this for initialization
	void Start () {
        angle = Random.Range(0, 360);
        this.transform.Rotate(0f, 0f, angle);
        Object.Destroy(gameObject, 10f);
    }
}
