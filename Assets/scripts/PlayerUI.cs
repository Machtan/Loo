using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    public GameObject cooldown_icon;
    protected GameObject broom;
    protected GameObject spray;
    protected GameObject vacuum;
    protected GameObject selection;

	// Use this for initialization
	void Start () {
        broom = transform.Find("broom").gameObject;
        spray = transform.Find("spray").gameObject;
        vacuum = transform.Find("vacuum").gameObject;
        selection = transform.Find("selection").gameObject;
	}

    public void select_weapon(Weapon weapon) {
        if (weapon == Weapon.Broom) {
            selection.transform.position = broom.transform.position;
        } else if (weapon == Weapon.Spray) {
            selection.transform.position = spray.transform.position;
        } else if (weapon == Weapon.Vacuum) {
            selection.transform.position = vacuum.transform.position;
        }
    }

    public void start_cooldown(Weapon weapon, float cooldown) {
        GameObject icon = null;
        if (weapon == Weapon.Broom) {
            icon = broom;
        } else if (weapon == Weapon.Spray) {
            icon = spray;
        } else if (weapon == Weapon.Vacuum) {
            icon = vacuum;
        }
        GameObject new_cooldown = Instantiate(cooldown_icon);
        new_cooldown.transform.position = icon.transform.position;
        new_cooldown.GetComponent<Cooldown>().start_cooldown(cooldown);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
