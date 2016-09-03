using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public uint joystickId = 1;
	public int speed = 1;

    public float angle { private set; get; }

	private Vector3 movement;
	private int slipEffect = 0;
	private string currentTrigger = "idle";

	private Rigidbody2D rigidbody;
	private Animator animator;

	void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.GetComponent<Slip>()) slipEffect = collider.GetComponent<Slip>().coefficient;
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.GetComponent<Slip>()) {
			slipEffect = 0;
			rigidbody.velocity = new Vector3(0, 0, 0);
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("H" + joystickId.ToString());
		float v = Input.GetAxis("V" + joystickId.ToString());

		bool moveUp 	 = (v > 0) ? true : false;
		bool moveRight = (h > 0) ? true : false;

		if(h == 0 && v == 0) setAnimation("idle");
		else if(moveUp && moveRight) setAnimation("GoRT");
		else if(moveUp && !moveRight) setAnimation("GoLT");
		else if(!moveUp && moveRight) setAnimation("GoRB");
		else if(!moveUp && !moveRight) setAnimation("GoLB");

		movement = new Vector3(h, v, 0);
		movement.Normalize();

		rigidbody.velocity = rigidbody.velocity * .99f;

		if(slipEffect != 0) rigidbody.AddForce(movement * Time.deltaTime * slipEffect);
    angle = Vector3.Angle(Vector3.up, movement);

		transform.Translate(movement * speed * Time.deltaTime);
	}

	void setAnimation(string trigger) {
		if(currentTrigger != trigger) {
			animator.SetTrigger(trigger);
			currentTrigger = trigger;
		}
	}
}
