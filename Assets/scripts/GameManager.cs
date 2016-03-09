using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    protected float middle_x;

	// Use this for initialization
	void Start () {
        middle_x = GameObject.FindGameObjectWithTag("middle").transform.position.x;
	}

    bool player_1_has_won() {
        int clean_stalls_on_left_side = 0;
        foreach (var stall in GameObject.FindGameObjectsWithTag("stall_clean")) {
            if (stall.transform.position.x < middle_x) {
                clean_stalls_on_left_side += 1;
            }
        }
        return clean_stalls_on_left_side != 0;
    }

    bool player_2_has_won() {
        int clean_stalls_on_right_side = 0;
        foreach (var stall in GameObject.FindGameObjectsWithTag("stall_clean")) {
            if (stall.transform.position.x > middle_x) {
                clean_stalls_on_right_side += 1;
            }
        }
        return clean_stalls_on_right_side != 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (player_1_has_won()) {
            Debug.Log("Player 1 wins!!!");
        }
        if (player_2_has_won()) {
            Debug.Log("Player 2 wins!!!");
        }
	}
}
