using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum Weapon {
    Broom,
    Spray,
    Vacuum,
}

public struct WeaponInfo {
    public float cooldown;
    public float stun_time;
    public List<GameObject> customers_in_range;

    public WeaponInfo(float cooldown, float stun_time) {
        this.cooldown = cooldown;
        this.stun_time = stun_time;
        customers_in_range = new List<GameObject>();
    }
}

public class Player : MonoBehaviour {

    public float move_speed = 1.0f;
    public Dictionary<Weapon, WeaponInfo> weapon_info;
    public float attack_cooldown_remaining = 0.0f;
    public float broom_stun_time = 3;
    public Weapon weapon = Weapon.Broom;
    public GameObject current_stall;
    public GameObject spray;
    public float broom_push_force = 3;
    //public Sprite stand;

    public float vx = 0.0f;
    public float vy = 0.0f;
    protected new SpriteRenderer renderer;
    protected new Rigidbody2D rigidbody;
    protected Animator animator;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        transform.up = Vector3.left;
        weapon_info = new Dictionary<Weapon, WeaponInfo>() {
            { Weapon.Broom, new WeaponInfo(0.5f, 1) },
            { Weapon.Spray, new WeaponInfo(0, 4) },
            { Weapon.Vacuum, new WeaponInfo(1, 0) }
        };
	}
        
    public void remove_customer_from_attack_areas(GameObject customer) {
        foreach (WeaponInfo info in weapon_info.Values) {
            info.customers_in_range.Remove(customer);
        }
    }


	// Update is called once per frame
	void Update () {

        float delta = Time.deltaTime;

        Vector2 new_position = new Vector2(transform.position.x + vx * delta, transform.position.y + vy * delta);
        rigidbody.MovePosition(new_position);

        if (vx != 0 || vy != 0) {
            transform.up = new Vector3(vx, vy, 0);
            if (! animator.GetBool("moving")) {
                //Debug.Log("MOVING!!!!");
                animator.SetBool("moving", true);
            }
        } else {
            animator.SetBool("moving", false);
            //Debug.Log("NOT MOVING!");
        }

        if (attack_cooldown_remaining > 0) {
            attack_cooldown_remaining -= Time.deltaTime;
        }
	}

    public void on_attack() {
        WeaponInfo info = weapon_info[weapon];

        // Prune dead objects
        int i = 0;
        while (i < info.customers_in_range.Count) {
            if (info.customers_in_range[i] == null) {
                info.customers_in_range.RemoveAt(i);
            } else {
                i += 1;
            }
        }

        /*Debug.Log("Customers:");
        foreach (GameObject customer in info.customers_in_range) {
            Debug.Log("- " + customer);
        }*/

        switch (weapon) {
        case Weapon.Broom: {
                foreach (GameObject customer in info.customers_in_range) {
                    customer.GetComponent<Customer>().stun_time_remaining = info.stun_time;
                    customer.GetComponent<Animator>().SetBool("stunned", true);
                    Vector2 push_force = (new Vector2(transform.up.x, transform.up.y)).normalized * broom_push_force;
                    customer.GetComponent<Rigidbody2D>().AddForce(push_force);
                }
                info.customers_in_range.Clear();
                if (current_stall != null) {
                    current_stall.GetComponent<FilthyStall>().Clean();
                    current_stall = null;
                }
                break;
            }
        case Weapon.Spray: {
                GameObject new_spray = Instantiate(spray);
                new_spray.transform.position = transform.FindChild("spray_attack_area").transform.position;
                new_spray.transform.up = transform.up;

                foreach (GameObject customer in info.customers_in_range) {
                    customer.GetComponent<Customer>().stun_time_remaining = info.stun_time;
                    customer.GetComponent<Animator>().SetBool("stunned", true);
                }

                break;
            }
        case Weapon.Vacuum: {
                Debug.Log("Vooooooooooooom");
                if (info.customers_in_range.Count == 0) {
                    Debug.Log("No customer to vacuum :(");
                    return;
                }
                GameObject target = info.customers_in_range[0];
                info.customers_in_range.RemoveAt(0);
                bool is_male = target.GetComponent<Animator>().GetBool("is_male");
                animator.SetBool("target_is_male", is_male);
                Destroy(target);
                break;
            }
        }

        animator.SetBool("attacking", true);
        attack_cooldown_remaining = info.cooldown;
    }

    public void switch_to_weapon(Weapon weapon) {
        Debug.Log("Swap weapon to " + weapon);
        this.weapon = weapon;
        animator.SetInteger("weapon", (int) weapon);
        animator.SetBool("change_weapon", true);
    }
}
