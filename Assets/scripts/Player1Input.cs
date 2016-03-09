using UnityEngine;
using System.Collections;

public class Player1Input : MonoBehaviour {

    protected Player logic;

	// Use this for initialization
	void Start () {
        logic = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (! Input.GetKey(KeyCode.DownArrow)) {
                logic.vy = logic.move_speed;
                transform.up = Vector3.up;
            } else {
                logic.vy = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            if (! Input.GetKey(KeyCode.DownArrow)) {
                logic.vy = 0.0f;
            } else {
                logic.vy = -logic.move_speed;
                transform.up = Vector3.down;
            }
        }

        // Down
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (! Input.GetKey(KeyCode.UpArrow)) {
                logic.vy = -logic.move_speed;
                transform.up = Vector3.down;
            } else {
                logic.vy = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            if (! Input.GetKey(KeyCode.UpArrow)) {
                logic.vy = 0.0f;
            } else {
                logic.vy = logic.move_speed;
                transform.up = Vector3.up;
            }
        }

        // Right
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (! Input.GetKey(KeyCode.LeftArrow)) {
                logic.vx = logic.move_speed;
                transform.up = Vector3.right;
            } else {
                logic.vx = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (! Input.GetKey(KeyCode.LeftArrow)) {
                logic.vx = 0.0f;
            } else {
                logic.vx = -logic.move_speed;
                transform.up = Vector3.left;
            }
        }

        // Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (! Input.GetKey(KeyCode.RightArrow)) {
                logic.vx = -logic.move_speed;
                transform.up = Vector3.left;
            } else {
                logic.vx = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (! Input.GetKey(KeyCode.RightArrow)) {
                logic.vx = 0.0f;
            } else {
                logic.vx = logic.move_speed;
                transform.up = Vector3.right;
            }
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (logic.attack_cooldown_remaining <= 0) {
                logic.on_attack();
                //Debug.Log("ATTACK!");
            }
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            logic.switch_to_weapon(Weapon.Broom);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            logic.switch_to_weapon(Weapon.Spray);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            logic.switch_to_weapon(Weapon.Vacuum);
        }
	}
}
