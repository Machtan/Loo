using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject customer;
    public float spawn_cooldown = 3;
    public float elapsed = 0;
    public int spawned = 0;
    public int active = 0;
    public int extra_customers = 0; // The amount of customers above full capacity to spawn

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("customer")) {
            if (collider.gameObject.GetComponent<Customer>().finished) {
                GameObject.Destroy(collider.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed >= spawn_cooldown) {
            if (active < GameObject.FindGameObjectsWithTag("stall_clean").Length + extra_customers) {
                GameObject obj = Instantiate(customer);
                obj.transform.position = transform.FindChild("spawn_position").transform.position;
                spawned += 1;
                active += 1;
            }

            elapsed = 0;
        }
	}
}
