using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Start.");
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rb;
		GameObject ball = GameObject.Find("Sphere");
		rb = ball.GetComponent<Rigidbody> ();
		if (Input.GetKey("space")) {
			rb.AddForce((float)0.0f, (float)0.4f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKey("left") || Input.GetKey("a")) {
			rb.AddForce((float)-0.3f, (float)0.0f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKey("right") || Input.GetKey("d")) {
			rb.AddForce((float)0.3f, (float)0.0f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKey("up") || Input.GetKey("w")) {
			rb.AddForce((float)0.0f, (float)0.0f, (float)0.3f, ForceMode.Impulse);
		}
		if (Input.GetKey("down") || Input.GetKey("s")) {
			rb.AddForce((float)0.0f, (float)0.0f, (float)-0.3f, ForceMode.Impulse);
		}
	}
}
