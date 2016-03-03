using UnityEngine;
using System.Collections;

public class CleanStall : MonoBehaviour {

    public GameObject filty_stall;
    public float cooldown = 5;
    protected GameObject customer;
    protected float elapsed = 0;
    protected GameObject stall;

	// Use this for initialization
	void Start () {
        stall = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (customer != null) {
            elapsed += Time.deltaTime;
            if (elapsed >= cooldown) {
                GameObject filthy = GameObject.Instantiate(filty_stall);

                filthy.transform.position = stall.transform.position;
                filthy.transform.up = stall.transform.up;

                customer.SetActive(true);

                filthy.GetComponent<FilthyStall>().used_by_female = 
                    ! customer.GetComponent<Animator>().GetBool("is_male");
                
                customer.transform.position = transform.position;
                customer.transform.up = stall.transform.up * -1;
                customer.GetComponent<Customer>().finished = true;
                customer.GetComponent<Customer>().destination = GameObject.FindGameObjectWithTag("door");
                customer = null;

                Destroy(stall);
            }
        }
	}

    void OnTriggerEnter2D (Collider2D collider) {
        //Debug.Log("ENTER!");
        if (collider.gameObject.CompareTag("customer")) {
            // Check that this is the stall the customer wants to enter
            GameObject destination = collider.gameObject.GetComponent<Customer>().destination;
            if (destination == gameObject) {
                Debug.Log("CUSTOMER ENTERS CLEAN STALL!");
                GameObject.FindGameObjectWithTag("door").GetComponent<Door>().active -= 1;
                customer = collider.gameObject;
                customer.SetActive(false);
                elapsed = 0;
                gameObject.tag = "stall_filthy";
            }
        }
    }
}
