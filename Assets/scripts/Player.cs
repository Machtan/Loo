using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float move_speed = 1.0f;
    public Sprite stand;

    protected float vx = 0.0f;
    protected float vy = 0.0f;
    protected new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
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

        float delta = Time.deltaTime;
        transform.position += new Vector3(vx * delta, vy * delta, 0);

        // Dynamic direction setting
        if (Mathf.Abs(vx) > Mathf.Abs(vy)) {
            if (vx > 0) {
                transform.up = Vector3.right;
            } else if (vx < 0){
                transform.up = Vector3.left;
            }
        } else {
            if (vy > 0) {
                transform.up = Vector3.up;
            } else if (vy < 0){
                transform.up = Vector3.down;
            }
        }
	}
}
