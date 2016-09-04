using UnityEngine;
using System.Collections;

public class ChilyPepper : MonoBehaviour {
	private bool activated = false;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Cutout>()) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		if(!activated && GetComponent<Fall>() && GetComponent<Fall>().ready) {
			anim.SetTrigger("explode");
			activated = true;
			Destroy(gameObject, .5f);
		}
	}
}
