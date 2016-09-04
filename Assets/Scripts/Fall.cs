using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {
	public float duration = 2;
	public float endScale = .5f;

	public bool ready = false;

	private float t = 0;

	void Start() {
		transform.localScale = new Vector3(0, 0, 0);
		GetComponent<SpriteRenderer>().color = Color.black;
	}

	void FixedUpdate() {
		if(t <= duration) {
			t += Time.deltaTime;

			float currentScale = endScale * t / duration;

			transform.localScale = new Vector3(currentScale, currentScale, 0);
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
			ready = true;
		}
	}

}
