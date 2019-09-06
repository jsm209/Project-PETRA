using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_breathing : MonoBehaviour {

    Vector3 localScale;

	// Use this for initialization
	void Start () {
        localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        // We use 4 because that's how much I scaled the color bar by originally.
        localScale.x = (Player.PLAYER_CURRENT_BREATH / Player.PLAYER_MAX_BREATH) * 4;
        transform.localScale = localScale;
	}
}
