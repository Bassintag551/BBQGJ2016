using UnityEngine;
using System.Collections;

public class ExplosiveCheese : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Cutout>()) {
			Destroy(gameObject);
		}
	}
}
