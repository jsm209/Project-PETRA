using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y > this.transform.position.y) {
            this.GetComponent<SpriteRenderer>().sortingOrder = 100;
        } else {
            this.GetComponent<SpriteRenderer>().sortingOrder = -100;
        }
	}
}
