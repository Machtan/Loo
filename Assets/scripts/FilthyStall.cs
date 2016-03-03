using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FilthyStall : MonoBehaviour {

    public List<Sprite> filthy_sprites;
    public Sprite blood;
    public bool used_by_female = false;
    protected new SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        //Debug.Log("Setting sprite...");
        renderer = GetComponent<SpriteRenderer>();
        if (used_by_female) {
            filthy_sprites.Add(blood);
        }
        int index = Random.Range(0, filthy_sprites.Count);
        renderer.sprite = filthy_sprites[index];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
