using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour {
    public float duration = 0.5f;
    protected float elapsed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (elapsed > duration) {
            Destroy(gameObject);
        } else {
            elapsed += Time.deltaTime;
        }
	}
}
