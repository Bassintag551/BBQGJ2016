using UnityEngine;
using System.Collections;

public class ChilyPepper : MonoBehaviour {
	private bool activated = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Cutout>()) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		if(!activated && GetComponent<Fall>() && GetComponent<Fall>().ready) {
			GetComponent<Explosion>().enabled = true;
			activated = true;
			Destroy(gameObject, .1f);
		}
	}
}
