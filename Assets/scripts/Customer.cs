using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour {

    public Sprite stand;
    public GameObject destination;
    public float move_speed = 1.0f;
    public float vx = 0.0f;
    public float vy = 0.0f;
    protected SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float delta = Time.deltaTime;
        Vector3 direction = (destination.transform.position - transform.position).normalized;
        vx = direction.x * move_speed;
        vy = direction.y * move_speed;
        transform.position += new Vector3(vx * delta, vy * delta, 0);

        // Dynamic direction setting
        if (Mathf.Abs(vx) > Mathf.Abs(vy)) {
            if (vx > 0) {
                transform.up = Vector3.right;
            } else if (vx < 0){
                transform.up = Vector3.left;
            }
        } else {
            if (vy > 0) {
                transform.up = Vector3.up;
            } else if (vy < 0){
                transform.up = Vector3.down;
            }
        }
	}
}
