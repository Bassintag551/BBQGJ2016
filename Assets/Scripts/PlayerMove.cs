﻿using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public int joystickId = 1;
	public float speed = 1;

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


        if (collider.GetComponent<Cutout>())
        {
            Cutout cutout = collider.GetComponent<Cutout>();
            if(cutout.owner != this || cutout.lived > 1)
            {
                GameManager.Instance.KillPlayer(joystickId - 1);
            }
        }
    }

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.GetComponent<Slip>()) {
			slipEffect = 0;
			rigidbody.velocity = new Vector3(0, 0, 0);
		}
	}

	void FixedUpdate() {
        float h = ControllerManager.Instance.Horizontal[joystickId - 1];
		float v = ControllerManager.Instance.Vertical[joystickId - 1];

        bool moveUp 	 = (v > 0) ? true : false;
		bool moveRight = (h > 0) ? true : false;

		if(h == 0 && v == 0) setAnimation("idle");
		else if(moveUp && moveRight) setAnimation("GoRT");
		else if(moveUp && !moveRight) setAnimation("GoLT");
		else if(!moveUp && moveRight) setAnimation("GoRB");
		else if(!moveUp && !moveRight) setAnimation("GoLB");

		movement = new Vector3(h, v, 0);
		movement.Normalize();

		if(slipEffect != 0) rigidbody.AddForce(movement * Time.deltaTime * slipEffect);
		else rigidbody.velocity *= .9f;

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
