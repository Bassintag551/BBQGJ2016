using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float radius = 1;
	public float strength = 10;

	// Use this for initialization
	void Start () {
		GetComponent<CircleCollider2D>().radius = radius;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Rigidbody2D>()) {
			Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
			Vector3 blow = collider.GetComponent<Transform>().position - transform.position;
			blow.Normalize();

			float percentage = (radius - Mathf.Sqrt(blow.x*blow.x + blow.y*blow.y)) / radius;

			rigidbody.AddForce(blow * strength * percentage);
		}
	}
}
