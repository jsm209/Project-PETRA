using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour {

    // Interaction
    public GameObject textObject;
    private float price;
    private TextMesh textArea;
    private bool isInteracting;

    // Wandering
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start() {
        price = 100;
        textArea = textObject.GetComponent<TextMesh>();

        // Set the wander wait time, and pick a random place to walk towards.
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update() {

        Debug.Log(isInteracting);

        // Only when merchant isn't interacting does he move.
        if (!isInteracting) {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
            SpriteFaceTarget(moveSpot);
            if (Vector2.Distance(transform.position, moveSpot.position) < 5f) {
                if (waitTime <= 0) {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
                else {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        isInteracting = true;
        textArea.text = "HIYA PETRA. IT'LL COST YA " + price + 
            " ROCKS\nTO UPGRADE YER GUN.\n" +
            "(PRESS SPACE TO UPGRADE)";
        textObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision) {

        // When the player is interacting, face the player.
        SpriteFaceTarget(collision.gameObject.GetComponent<Transform>());

        // If the player pushes space, attempt to purchase upgrade.
        if (Input.GetKeyDown("space")) {
            if (Player.PLAYER_MONEY >= price) {
                textArea.text = "THANKS PETRA. NEXT TIME\n" +
                    "IT'LL COST YA DOUBLE.";
                Player.PLAYER_MONEY -= price;
                Player.PLAYER_CHARGES++;
                price = price * 2;
            } else {
                textArea.text = "HUH? DON'T PULL ME LEG PETRA, COME BACK\n" +
                    "WHEN YOU ACTUALLY HAVE THE ROCKS.";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isInteracting = false;
        textObject.SetActive(false);
    }

    private void SpriteFaceTarget(Transform target) {
        if (target.position.x > this.transform.position.x) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
