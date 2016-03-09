﻿using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

    public Sprite stand;
    public GameObject stall_destination;
    public GameObject destination;
    public float run_speed = 1.0f;
    public float walk_speed = 0.5f;
    public float vx = 0.0f;
    public float vy = 0.0f;
    public bool finished = false;
    public float stun_time_remaining = 0;

    protected new SpriteRenderer renderer;
    protected new Rigidbody2D rigidbody;
    protected Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) {
            animator.SetBool("is_male", true);
        } else {
            animator.SetBool("is_male", false);
        }
        animator.SetBool("gender_set", true);

        GameObject[] destinations = GameObject.FindGameObjectsWithTag("stall_clean");
        if (destinations.Length != 0) {
            stall_destination = destinations[Random.Range(0, destinations.Length)];
            destination = stall_destination;
        }
	}

    void OnDestroy() {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player == null) { 
            return;
        }
        Player script = player.GetComponent<Player>();
        if (script == null) {
            return;
        }
        script.remove_customer_from_attack_areas(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (stun_time_remaining > 0) {
            stun_time_remaining -= Time.deltaTime;
            if (stun_time_remaining <= 0) {
                GetComponent<Animator>().SetBool("stunned", false);
            }
            return;
        }

        if ((! finished) && (stall_destination != null) && (! stall_destination.CompareTag("stall_clean"))) {
            GameObject[] destinations = GameObject.FindGameObjectsWithTag("stall_clean");
            if (destinations.Length != 0) {
                stall_destination = destinations[Random.Range(0, destinations.Length)];
                destination = stall_destination;
            }
        }
        if (destination == null) {
            return;
        }
        
        float delta = Time.deltaTime;
        Vector3 direction = (destination.transform.position - transform.position).normalized;
        if (! finished) {
            vx = direction.x * run_speed;
            vy = direction.y * run_speed;
        } else {
            vx = direction.x * walk_speed;
            vy = direction.y * walk_speed;
        }

        Vector2 new_position = new Vector2(transform.position.x + vx * delta, transform.position.y + vy * delta);
        rigidbody.MovePosition(new_position);
        if (vx != 0 || vy != 0) {
            transform.up = direction;
            animator.SetBool("is_walking", true);
        } else {
            animator.SetBool("is_walking", false);
        }
	}
}
