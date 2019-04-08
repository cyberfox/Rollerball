using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class Movement : MonoBehaviour
{
	public float bulletZSpeed = 1000f;
	public float bulletYSpeed = 5f;
	public float bulletXSpeed = 0f;

	private int m_CurrentBullet = 0;
	private Vector3 v = new Vector3(0.0f, 0.25f, 1.0f);
	private GameObject[] pews = new GameObject[100];
	private GameObject[] badGuys;
	private StoreTransform[] originalTransforms;

	public class StoreTransform
	{
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 localScale;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Start.");
		badGuys = GameObject.FindGameObjectsWithTag("Bad Guys");
		originalTransforms = new StoreTransform[badGuys.Length+1];
		int i = 0;
		foreach(GameObject badGuy in badGuys)
		{
			StoreTransform saved = new StoreTransform();
			saved.position = badGuy.transform.position;
			saved.rotation = badGuy.transform.rotation;

			originalTransforms[i++] = saved;
		}
		GameObject ball = GameObject.Find("Sphere");
		StoreTransform playerOriginal = new StoreTransform();
		playerOriginal.position = ball.transform.position;
		playerOriginal.rotation = ball.transform.rotation;

		originalTransforms[i] = playerOriginal;
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

		bool resetting = Input.GetKey("r");

		if (Input.GetKey("r") && (Input.GetKey("left shift") || Input.GetKey("right shift")))
		{
			GameObject hsObj = GameObject.Find("HighScore");
			HighScore hs = hsObj.GetComponent<HighScore>();
			hs.ResetScore();
			resetting = true;
		}

		if(resetting) {
			int i = 0;
			foreach (GameObject badGuy in badGuys)
			{
				resetObject(badGuy, i);
				BadGuys bg = badGuy.GetComponent<BadGuys>();
				if (bg != null)
				{
					bg.ResetScored();
				}
				i++;
			}

			resetObject(ball, i);
		}

		if (Input.GetKey("t"))
		{
			if (pews[m_CurrentBullet] == null)
			{
				GameObject pew = GameObject.Find("Pew");
				GameObject o = Object.Instantiate(pew);
				pews[m_CurrentBullet] = o;
			}

			GameObject bullet = pews[m_CurrentBullet];
			bullet.transform.position = ball.transform.position + v;

			Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
			bulletBody.velocity = Vector3.zero;
			bulletBody.rotation = Quaternion.identity;
			bulletBody.angularVelocity = Vector3.zero;
			bulletBody.AddForce(bulletXSpeed, bulletYSpeed, bulletZSpeed);

			m_CurrentBullet++;
			m_CurrentBullet %= 100;
		}
	}

	private void resetObject(GameObject badGuy, int i)
	{
		badGuy.transform.position = originalTransforms[i].position;
		badGuy.transform.rotation = originalTransforms[i].rotation;
		Rigidbody bulletBody = badGuy.GetComponent<Rigidbody>();
		bulletBody.rotation = Quaternion.identity;
		bulletBody.angularVelocity = Vector3.zero;
		bulletBody.velocity = Vector3.zero;
	}
}
