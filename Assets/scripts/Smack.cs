using UnityEngine;
using System.Collections;

public class Smack : MonoBehaviour {

    public float duration = 0.2f;
    protected float elapsed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed > duration) {
            Destroy(gameObject);
        }
	}
}
