using UnityEngine;
using System.Collections;

public enum Side {
    Left,
    Right,
}

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
    public GameObject waypoint;
    public bool fully_entered = false;
    public Side field_side;

    protected new SpriteRenderer renderer;
    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected float middle_x;

	// Use this for initialization
	void Start () {
        middle_x = GameObject.FindGameObjectWithTag("middle").transform.position.x;
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (Random.Range(0, 2) == 0) {
            animator.SetBool("is_male", true);
        } else {
            animator.SetBool("is_male", false);
        }
        animator.SetBool("gender_set", true);

        // Assign the customer a waypoint to go to
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        if (waypoints.Length != 0) {
            int side = Random.Range(0, waypoints.Length);
            waypoint = waypoints[side];
            if (waypoint.transform.position.x < middle_x) {
                field_side = Side.Left;
            } else {
                field_side = Side.Right;
            }
            destination = waypoint;
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

    public void target_nearest_clean_stall() {
        GameObject[] clean_stalls = GameObject.FindGameObjectsWithTag("clean_door");
        if (clean_stalls.Length != 0) {
            bool found = false;
            GameObject closest = null;
            float distance = 0;
            foreach (var stall in clean_stalls) {
                // The stall is on the other side of the field
                if ((field_side == Side.Right == stall.transform.position.x < middle_x) 
                    || (field_side == Side.Left == stall.transform.position.x > middle_x)) {
                    continue;
                }
                float stall_distance = (stall.transform.position - transform.position).magnitude;
                if (! found) {
                    found = true;
                    distance = stall_distance;
                    closest = stall;
                } else if (stall_distance < distance) {
                    distance = stall_distance;
                    closest = stall;
                }
            }
            if (found) {
                stall_destination = closest;
                destination = stall_destination; 
            } else {
                closest = clean_stalls[0];
                distance = (closest.transform.position - transform.position).magnitude;
                foreach (var stall in clean_stalls) {
                    // Don't check the side this time
                    float stall_distance = (stall.transform.position - transform.position).magnitude;
                    if (stall_distance < distance) {
                        distance = stall_distance;
                        closest = stall;
                    }
                }
                stall_destination = closest;
                destination = stall_destination; 
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        // Check if the customer is stunned
        if (stun_time_remaining > 0) {
            stun_time_remaining -= Time.deltaTime;
            if (stun_time_remaining <= 0) {
                GetComponent<Animator>().SetBool("stunned", false);
            }
            return;
        }

        // Set the field side
        if (transform.position.x < middle_x) {
            field_side = Side.Left;
        } else {
            field_side = Side.Right;
        }

        // Check the target
        if ((! finished) && fully_entered) {
            // Ensure that the target stall isn't filthy
            target_nearest_clean_stall();
        }
        if (destination == null) {
            return;
        }

        // Move
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
