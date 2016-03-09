using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponAreaChecker : MonoBehaviour {

    public Weapon weapon_type;
    protected GameObject parent;
	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject;
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("customer")) {
            List<GameObject> in_range = parent.GetComponent<Player>().weapon_info[weapon_type].customers_in_range;
            if (! in_range.Contains(collider.gameObject)) {
                in_range.Add(collider.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("customer")) {
            List<GameObject> in_range = parent.GetComponent<Player>().weapon_info[weapon_type].customers_in_range;
            if (in_range.Contains(collider.gameObject)) {
                in_range.Remove(collider.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
