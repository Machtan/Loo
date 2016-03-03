using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float move_speed = 1.0f;
    public float broom_attack_cooldown = 0.5f;
    public float attack_cooldown_remaining = 0.0f;
    //public Sprite stand;

    protected float vx = 0.0f;
    protected float vy = 0.0f;
    protected new SpriteRenderer renderer;
    protected Animator animator;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        transform.up = Vector3.left;
	}
        
	
	// Update is called once per frame
	void Update () {
        // Up
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (! Input.GetKey(KeyCode.DownArrow)) {
                vy = move_speed;
                transform.up = Vector3.up;
            } else {
                vy = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            if (! Input.GetKey(KeyCode.DownArrow)) {
                vy = 0.0f;
            } else {
                vy = -move_speed;
                transform.up = Vector3.down;
            }
        }

        // Down
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (! Input.GetKey(KeyCode.UpArrow)) {
                vy = -move_speed;
                transform.up = Vector3.down;
            } else {
                vy = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            if (! Input.GetKey(KeyCode.UpArrow)) {
                vy = 0.0f;
            } else {
                vy = move_speed;
                transform.up = Vector3.up;
            }
        }

        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (! Input.GetKey(KeyCode.LeftArrow)) {
                vx = move_speed;
                transform.up = Vector3.right;
            } else {
                vx = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (! Input.GetKey(KeyCode.LeftArrow)) {
                vx = 0.0f;
            } else {
                vx = -move_speed;
                transform.up = Vector3.left;
            }
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (! Input.GetKey(KeyCode.RightArrow)) {
                vx = -move_speed;
                transform.up = Vector3.left;
            } else {
                vx = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (! Input.GetKey(KeyCode.RightArrow)) {
                vx = 0.0f;
            } else {
                vx = move_speed;
                transform.up = Vector3.right;
            }
        }

        // Attack
        if (attack_cooldown_remaining > 0) {
            attack_cooldown_remaining -= Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (attack_cooldown_remaining <= 0) {
                animator.SetBool("broom_attack", true);
                //Debug.Log("ATTACK!");
            }
        }

        float delta = Time.deltaTime;
        transform.position += new Vector3(vx * delta, vy * delta, 0);

        if (vx != 0 || vy != 0) {
            transform.up = new Vector3(vx, vy, 0);
            if (! animator.GetBool("moving")) {
                //Debug.Log("MOVING!!!!");
                animator.SetBool("moving", true);
            }
        } else {
            animator.SetBool("moving", false);
            //Debug.Log("NOT MOVING!");
        }
	}
}
