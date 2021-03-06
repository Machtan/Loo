﻿using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("customer")) {
            Customer customer = collider.gameObject.GetComponent<Customer>();
            if (!customer.fully_entered) {
                customer.target_nearest_clean_stall();
                customer.fully_entered = true;
            }
        }
    }
}
