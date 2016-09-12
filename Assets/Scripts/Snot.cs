using UnityEngine;
using System.Collections;

public class Snot : MonoBehaviour {

	public float speed;
	public Vector3 direction;

	// Use this for initialization
	void Start(){

	}
	
	// Update is called once per frame
	void Update() {
		transform.position += speed * direction;
	}

	void SetDirection(Vector3 d) {
		direction = d;
		if (direction == Vector3.right) {
			transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
		} else if (direction == Vector3.up) {
			transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
		} else if (direction == Vector3.left) {
			transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
		} else if (direction == Vector3.down) {
			transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
		} 
	}
}
