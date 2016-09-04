using UnityEngine;
using System.Collections;

public class speedup : MonoBehaviour {
	public float speedMultiplier = 2f;
	public float duration = 2;

	private float start = 0;
	private PlayerMove playerMove;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<PlayerMove>()) {
			//Disable powerup
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<CircleCollider2D>().enabled = false;

			playerMove = collider.GetComponent<PlayerMove>();
			playerMove.speed *= speedMultiplier;

			start = Time.time;
		}

		if(collider.GetComponent<Cutout>()) {
			Destroy(gameObject);
		}
	}

	void Update() {
		if(start != 0 && Time.time >= start + duration) {
			playerMove.speed /= speedMultiplier;
			Destroy(gameObject);
		}
	}
}
