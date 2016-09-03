using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public uint joystickId = 1;
	public int speed = 1;

    public float angle { private set; get; }

	private Vector3 movement;

	void FixedUpdate() {
		float h = Input.GetAxis("H" + joystickId.ToString());
		float v = Input.GetAxis("V" + joystickId.ToString());

		movement = new Vector3(h, v, 0);
		movement.Normalize();

        angle = Vector3.Angle(Vector3.up, movement);

		transform.Translate(movement * speed * Time.deltaTime);
	}
}
