using UnityEngine;
using System.Collections;

public class ResetPizza : MonoBehaviour {
	private bool activated = false;
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<PlayerMove>()) {
			resetPizza();
		}

		if(collider.GetComponent<Cutout>()) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate() {
		if(!activated && GetComponent<Fall>() && GetComponent<Fall>().ready) {
			GetComponent<CircleCollider2D>().enabled = true;
			activated = true;
			Destroy(gameObject, .5f);
		}
	}

	void resetPizza() {
		GameObject pizza = GameObject.Find("Pizza");
		foreach (Transform child in pizza.transform) {
			Destroy(child.gameObject);
		}
		Destroy(gameObject);
	}
}
