using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shooting : MonoBehaviour {

    public Transform bowTip;
    Transform firePoint;
    Rigidbody2D body;

    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Camera cam;

    Vector2 mousePos;
    float angle;
   
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        firePoint = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        // Make firepoint snap to current position of the bow.
        firePoint.position = bowTip.position;

        // converts pixel coordinates to world units
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Get angle of mouse relative to the firepoint.
        Vector2 lookDir = mousePos - body.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 270f;

        body.rotation = angle; // Actually rotating angle of firepoint itself.

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }

	}

    void Shoot() {

        // Only shoot if player has charge.
        if (GameObject.FindGameObjectsWithTag("Laser").Length < Player.PLAYER_CHARGES) {

            // Creating bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Maintaining a reference to its rigidbody so we can later add a force.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.rotation = angle + 90f;
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse); // Impulse because we want instant force.
        }
    }
}
