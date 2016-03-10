using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int player_1_points = 0;
    public int player_2_points = 0;
    public int point_target = 500;

    public float point_cooldown = 2.0f;

    protected float middle_x;
    protected float elapsed = 0;
    protected GameObject player_1_stalls;
    protected GameObject player_2_stalls;

	// Use this for initialization
	void Start () {
        middle_x = GameObject.FindGameObjectWithTag("middle").transform.position.x;
        player_1_stalls = GameObject.Find("player_1_stalls");
        player_2_stalls = GameObject.Find("player_2_stalls");
	}

    void give_points() {
        // Give player 1 points for each clean stall
        for (int i = 0; i < player_1_stalls.transform.childCount; ++i) {
            GameObject stall = player_1_stalls.transform.GetChild(i).gameObject;
            if (stall.CompareTag("stall_clean")) {
                player_1_points += 1;
            }
        }

        // Give player 2 points for each clean stall
        for (int i = 0; i < player_2_stalls.transform.childCount; ++i) {
            GameObject stall = player_2_stalls.transform.GetChild(i).gameObject;
            if (stall.CompareTag("stall_clean")) {
                player_2_points += 1;
            }
        } 

        if (player_1_points > point_target && player_2_points > point_target) {
            if (player_1_points > player_2_points) {
                Debug.Log("Player 1 wins!");
            } else if (player_2_points > player_1_points) {
                Debug.Log("Player 2 wins!");
            } else {
                Debug.Log("Tie!");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        while (elapsed > point_cooldown) {
            give_points();
            elapsed -= point_cooldown;
        }
	}
}
