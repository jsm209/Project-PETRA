﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float potential;

    // Determines size
    public float hp = 3;

    // The item to drop upon death.
    public GameObject drop;

    // The target to chase
    private Transform target;

    // Determines red in RGB (less red, faster slime), causes slime to turn green.
    private float speed;

    // Determines green in RGB (less green, stronger slime), causes slime to turn red.
    private float damage;

    Transform trans;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        trans = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();

        potential = 10 * (0.5f * (Player.UPGRADES_BOUGHT + 1));
        hp = Player.UPGRADES_BOUGHT + 1;

        AllocateSkillpoints();
        Sizeshift();
        Colorshift();
    }
	
	// Update is called once per frame
	void Update () {
        float curMoveSpeed = speed;
        if (Vector2.Distance(trans.position, target.position) > 20) {
            curMoveSpeed = speed / 4;
        } else {
            curMoveSpeed = speed;
        }
        trans.position = Vector2.MoveTowards(trans.position, target.position, curMoveSpeed * Time.deltaTime);


    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Laser") {
            TakeDamage();
        }

        if (collision.gameObject.tag == "Player") {
            Debug.Log("DEALT " + damage + " DMG!");
            Player.PLAYER_CURRENT_HEALTH -= damage;


            // Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            // Vector2 away = transform.position - collision.transform.position;
            // rb.AddForce(-away * 1000);

        }
    }

    void AllocateSkillpoints() {
        // allocate skillpoints randomly
        for (int i = 0; i < potential; i++) {
            if (Random.value < 0.5) {
                speed++;
            } else {
                damage++;
            }
        }

        Debug.Log("Speed: " + speed + ", DMG: " + damage);
    }

    void Sizeshift() {
        transform.localScale = new Vector3(hp / 2, hp / 2);
    }

    void Colorshift() {
        float redModifier = speed * 10;
        float greenModifier = damage * 10;

        

        if (redModifier > 255) {
            redModifier = 255;
        }

        if (greenModifier > 255) {
            greenModifier = 255;
        }

        // Debug.Log(redModifier);
        // Debug.Log(greenModifier);

        // Debug.Log((255 - redModifier) / 255);
        // Debug.Log((255 - greenModifier) / 255);

        sprite.color = new Color((255 - redModifier) / 255, (255 - greenModifier) / 255, 255f);
    }

    void TakeDamage() {
        hp--;
        SpawnRocks(Random.Range(1, 4));
        if (hp == 0) {
            Destroy(gameObject);
        } else {
            Sizeshift();
        }
    }

    void SpawnRocks(int n) {
        for (int i = 0; i < n; i++) {
            GameObject rock = Instantiate(drop, transform.position, transform.rotation);
            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            rb.velocity = Random.onUnitSphere * Random.Range(0.1f, 1f) * 10;
        }
    }
}
