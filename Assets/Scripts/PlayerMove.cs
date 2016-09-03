using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public uint joystickId = 1;
	public int speed = 1;

	private Vector3 movement;
	private int slipEffect = 0;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Slip>()) slipEffect = collider.GetComponent<Slip>().coefficient;

	}

	void OnTriggerExit2D() {
		slipEffect = 0;
		GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
	}

	void FixedUpdate() {
		float h = Input.GetAxis("H" + joystickId.ToString());
		float v = Input.GetAxis("V" + joystickId.ToString());

		movement = new Vector3(h, v, 0);
		movement.Normalize();

		if(slipEffect != 0) GetComponent<Rigidbody2D>().AddForce(movement * Time.deltaTime * slipEffect);

		transform.Translate(movement * speed * Time.deltaTime);
	}
}
