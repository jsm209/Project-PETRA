using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public static float PLAYER_SPEED;

    public static float PLAYER_CURRENT_HEALTH;
    public static float PLAYER_MAX_HEALTH;

    public static float PLAYER_CURRENT_BREATH;
    public static float PLAYER_MAX_BREATH;

    public static float PLAYER_MONEY;

    public static int PLAYER_CHARGES;

    public static bool IS_SUFFOCATING;

    public static int UPGRADES_BOUGHT;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PLAYER_SPEED = 10f;
        PLAYER_MAX_HEALTH = 100f;
        PLAYER_CURRENT_HEALTH = PLAYER_MAX_HEALTH;

        PLAYER_MAX_BREATH = 100f;
        PLAYER_CURRENT_BREATH = PLAYER_MAX_BREATH;

        PLAYER_MONEY = 100;

        PLAYER_CHARGES = 1;

        IS_SUFFOCATING = false;

        UPGRADES_BOUGHT = 0;
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Sets correct animation if there is movement.
        if (horizontal != 0 || vertical != 0) {
            anim.SetBool("isMoving", true);
        } else {
            anim.SetBool("isMoving", false);
        }

        // Flips the character depending on movement.
        if (horizontal > 0) {
            spriteRenderer.flipX = false; // face right
        } else if (horizontal < 0) {
            spriteRenderer.flipX = true; // face left
        }

        LoseBreath(0.08f);
        if (PLAYER_CURRENT_BREATH <= 0) {
            IS_SUFFOCATING = true;
        } else {
            IS_SUFFOCATING = false;
        }
	}

    // FixedUpdate can run once, zero, or several times per frame, depending on game settings.
    private void FixedUpdate() {

        // If we are moving diagonally, limit movement speed.
        if (horizontal != 0 && vertical != 0) {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * PLAYER_SPEED, vertical * PLAYER_SPEED);
    }

    private void LoseBreath(float loss) {
        if (!IS_SUFFOCATING) {
            PLAYER_CURRENT_BREATH -= loss;
        } else {
            PLAYER_CURRENT_HEALTH -= loss;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Air") {
            if (Player.PLAYER_CURRENT_BREATH > PLAYER_MAX_BREATH - 10f) {
                PLAYER_CURRENT_BREATH = 100f;
            } else {
                PLAYER_CURRENT_BREATH += 10f;
            }
        }

        if (collision.gameObject.tag == "Nugget") {
            Destroy(collision.gameObject);
            PLAYER_MONEY++;
            Debug.Log(PLAYER_MONEY);
        }
    }
}
