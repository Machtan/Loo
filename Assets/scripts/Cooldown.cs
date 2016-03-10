using UnityEngine;
using System.Collections;

public class Cooldown : MonoBehaviour {

    public float cooldown;
    public bool cooldown_started = false;
    public float remainder;

	// Use this for initialization
	void Start () {
	
	}

    public void start_cooldown(float cooldown) {
        this.cooldown = cooldown;
        this.cooldown_started = true;
        this.remainder = cooldown;
    }
	
	// Update is called once per frame
	void Update () {
        if (! cooldown_started) {
            return;
        }
        if (remainder <= 0) {
            Destroy(gameObject);
        } else {
            remainder -= Time.deltaTime;
            float scale = remainder / cooldown;
            transform.localScale = new Vector3(32, 32, 1) * scale;
        }
        Debug.Log("Cooldown: " + remainder + " / " + cooldown);
	}
}
