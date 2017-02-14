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
		if (Input.GetKeyDown("space")) {
			rb.AddForce((float)0.0f, (float)5.8f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKeyDown("left")) {
			rb.AddForce((float)-2.0f, (float)0.0f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKeyDown("right")) {
			rb.AddForce((float)2.0f, (float)0.0f, (float)0.0f, ForceMode.Impulse);
		}
		if (Input.GetKeyDown("up")) {
			rb.AddForce((float)0.0f, (float)0.0f, (float)2.0f, ForceMode.Impulse);
		}
		if (Input.GetKeyDown("down")) {
			rb.AddForce((float)0.0f, (float)0.0f, (float)-2.0f, ForceMode.Impulse);
		}
		Debug.Log ("Update.");
	}
}
