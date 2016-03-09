using UnityEngine;
using System.Collections;

public class Player2Input : MonoBehaviour {

    protected Player logic;

    // Use this for initialization
    void Start () {
        logic = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
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
        if (Input.GetKeyDown(KeyCode.G)) {
            if (logic.attack_cooldown_remaining <= 0) {
                logic.on_attack();
                //Debug.Log("ATTACK!");
            }
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
}
