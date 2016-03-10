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

public class WeaponInfo {
    public float cooldown;
    public float stun_time;
    public float cooldown_remaining;
    public List<GameObject> customers_in_range;

    public WeaponInfo(float cooldown, float stun_time) {
        this.cooldown = cooldown;
        this.stun_time = stun_time;
        customers_in_range = new List<GameObject>();
        cooldown_remaining = 0;
    }
}

public class Player : MonoBehaviour {

    public float move_speed = 1.0f;
    public Dictionary<Weapon, WeaponInfo> weapon_info;
    public float broom_stun_time = 3;
    public Weapon weapon = Weapon.Broom;
    public GameObject current_stall;
    public GameObject spray;
    public GameObject smack;
    public PlayerUI ui;
    public float broom_push_force = 3;

    public float vx = 0.0f;
    public float vy = 0.0f;
    protected new SpriteRenderer renderer;
    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected VacuumSound vacuum_sound;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        vacuum_sound = transform.FindChild("vacuum_attack_area").gameObject.GetComponent<VacuumSound>();
        transform.up = Vector3.left;
        weapon_info = new Dictionary<Weapon, WeaponInfo>() {
            { Weapon.Broom, new WeaponInfo(0.5f, 1) },
            { Weapon.Spray, new WeaponInfo(0.5f, 3) },
            { Weapon.Vacuum, new WeaponInfo(1.5f, 0) }
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
                GetComponent<AudioSource>().Play();
                //Debug.Log("MOVING!!!!");
                animator.SetBool("moving", true);
            }
        } else {
            animator.SetBool("moving", false);
            GetComponent<AudioSource>().Stop();
            //Debug.Log("NOT MOVING!");
        }

        foreach (Weapon wep in weapon_info.Keys) {
            WeaponInfo info = weapon_info[wep];
            info.cooldown_remaining -= Time.deltaTime;
            if (info.cooldown_remaining < 0) {
                info.cooldown_remaining = 0;
            }
        }
	}

    void attack() {
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
                if (info.customers_in_range.Count != 0) {
                    transform.FindChild("broom_attack_area").GetComponent<AudioSource>().Play();
                }
                foreach (GameObject customer in info.customers_in_range) {
                    customer.GetComponent<Customer>().stun_time_remaining = info.stun_time;
                    customer.GetComponent<Animator>().SetBool("stunned", true);
                    Vector2 push_force = (new Vector2(transform.up.x, transform.up.y)).normalized * broom_push_force;
                    customer.GetComponent<Rigidbody2D>().AddForce(push_force);
                    customer.GetComponent<Customer>().fully_entered = true;
                    Vector3 pos = customer.transform.position;
                    Vector3 dist = pos - transform.position;
                    Vector3 smack_pos = transform.position + dist / 2;
                    GameObject new_smack = Instantiate(smack);
                    new_smack.transform.position = smack_pos;
                    new_smack.transform.up = dist.normalized;
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
                new_spray.transform.parent = transform.FindChild("spray_attack_area").transform;

                foreach (GameObject customer in info.customers_in_range) {
                    customer.GetComponent<Customer>().stun_time_remaining = info.stun_time;
                    customer.GetComponent<Customer>().fully_entered = true;
                    customer.GetComponent<Animator>().SetBool("stunned", true);
                }

                break;
            }
        case Weapon.Vacuum: {
                //Debug.Log("Vooooooooooooom");
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
        Debug.Log("Weapon: " + weapon + " Cooldown: " + info.cooldown);
        weapon_info[weapon].cooldown_remaining = info.cooldown;
        ui.start_cooldown(weapon, info.cooldown);
    }

    public void switch_to_weapon(Weapon weapon) {
        if (this.weapon == weapon) {
            return;
        }
        //Debug.Log("Swap weapon to " + weapon);

        animator.SetInteger("weapon", (int) weapon);
        animator.SetBool("change_weapon", true);
        if (weapon == Weapon.Vacuum) {
            vacuum_sound.start_vacuuming();
        } else if (this.weapon == Weapon.Vacuum) {
            vacuum_sound.stop_vacuuming();
        }
        this.weapon = weapon;
        ui.select_weapon(weapon);
    }

    public void attempt_to_attack() {
        if (weapon_info[weapon].cooldown_remaining <= 0) {
            attack();
        }
    }
}
