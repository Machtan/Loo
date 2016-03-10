using UnityEngine;
using System.Collections;

public class VacuumSound : MonoBehaviour {

    public AudioClip vacuum_start;
    public AudioClip vacuum_loop;

    protected float start_length;
    protected float start_elapsed = 0;
    protected AudioSource source;
    protected bool is_vacuuming = false;
    protected bool start_finished = false;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        start_length = vacuum_start.length;
	}

    public void start_vacuuming() {
        if (is_vacuuming) {
            return;
        }
        //Debug.Log("Start vacuuming");
        source.loop = false;
        is_vacuuming = true;
        start_finished = false;
        start_elapsed = 0;
        source.clip = vacuum_start;
        source.Play();
    }

    public void stop_vacuuming() {
        if (! is_vacuuming) { 
            return;
        }
        //Debug.Log("Stop vacuuming");
        is_vacuuming = false;
        source.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        if (! is_vacuuming) {
            return;
        }
        if (start_elapsed < start_length) {
            start_elapsed += Time.deltaTime;
        } else if (! start_finished) {
            start_finished = true;
            source.clip = vacuum_loop;
            source.loop = true;
            source.Play();
        }
	}
}
