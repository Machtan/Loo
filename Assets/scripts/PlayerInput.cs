using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public bool is_player_1 = true;
    protected Player logic;

	// Use this for initialization
	void Start () {
        Debug.Log("Is player 1: " + is_player_1);
        logic = gameObject.GetComponent<Player>();
	}

    void player_1_update() {
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
        if (Input.GetKeyUp(KeyCode.K)) {
            logic.attempt_to_attack();
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.J)) {
            logic.switch_to_weapon(Weapon.Broom);
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            logic.switch_to_weapon(Weapon.Spray);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            logic.switch_to_weapon(Weapon.Vacuum);
        }
    }

    void player_2_update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            logic.vy = logic.move_speed;
            transform.up = Vector3.up;
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            logic.vy = 0.0f;
        }

        // Down
        if (Input.GetKeyDown(KeyCode.S)) {
            logic.vy = -logic.move_speed;
            transform.up = Vector3.down;
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            logic.vy = 0.0f;
        }
        // Right
        if (Input.GetKeyDown(KeyCode.D)) {
            logic.vx = logic.move_speed;
            transform.up = Vector3.right;
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            logic.vx = 0.0f;
        }

        // Left
        if (Input.GetKeyDown(KeyCode.A)) {
            logic.vx = -logic.move_speed;
            transform.up = Vector3.left;
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            logic.vx = 0.0f;
        }

        // Attack
        if (Input.GetKeyUp(KeyCode.G)) {
            logic.attempt_to_attack();
        }

        // Switch weapon
        if (Input.GetKeyDown(KeyCode.F)) {
            logic.switch_to_weapon(Weapon.Broom);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            logic.switch_to_weapon(Weapon.Spray);
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            logic.switch_to_weapon(Weapon.Vacuum);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (is_player_1) {
            player_1_update();
        } else {
            player_2_update();
        }
	}
}
