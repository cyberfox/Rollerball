using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public Rigidbody bodyTarget;

	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate () {
		// Early out if we don't have a target
		if (!target) return;

		// Calculate the current rotation angles
		Vector3 targetForward = bodyTarget.velocity.normalized;
		targetForward.y = 0;
//		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
//		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);

		// Always look at the target
		transform.LookAt(target);
	}

	[SerializeField] private float m_SmoothTurnTime = 0.2f; // the smoothing for the camera's rotation
	[SerializeField] private float m_MoveSpeed = 3; // How fast the rig will move to keep up with target's position
	[SerializeField] private float m_TurnSpeed = 1; // How fast the rig will turn to keep up with target's rotation
	private float m_TurnSpeedVelocityChange; // The change in the turn speed velocity
	private float m_CurrentTurnAmount; // How much to turn the camera
	[SerializeField] private float m_TargetVelocityLowerLimit = 4f;// the minimum velocity above which the camera turns towards the object's velocity. Below this we use the object's forward direction.

	void altLateUpdate() {
		float deltaTime = Time.deltaTime;
		Vector3 targetForward;
		if (bodyTarget.velocity.magnitude > m_TargetVelocityLowerLimit) {
			targetForward = bodyTarget.velocity.normalized;
		} else {
			targetForward = bodyTarget.transform.forward;
		}
		targetForward.y = 0;
		var rollRotation = Quaternion.LookRotation(targetForward, Vector3.up);
		// camera position moves towards target position:
		transform.position = Vector3.Lerp(transform.position, bodyTarget.transform.position, deltaTime*m_MoveSpeed);

		float currentHeight = transform.position.y;
		float wantedHeight = target.position.y + height;
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		m_CurrentTurnAmount = Mathf.SmoothDamp(m_CurrentTurnAmount, 1, ref m_TurnSpeedVelocityChange, m_SmoothTurnTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, rollRotation, m_TurnSpeed*m_CurrentTurnAmount*deltaTime);
		transform.position = target.position;
		transform.position -= rollRotation * Vector3.forward * distance;
		transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
	}
}
