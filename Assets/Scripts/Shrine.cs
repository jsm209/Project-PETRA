using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour {

    public Sprite[] shrine;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = shrine[Random.Range(0, shrine.Length)];
	}
}
