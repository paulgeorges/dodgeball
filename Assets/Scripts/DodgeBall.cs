using UnityEngine;
using System.Collections;

public class ThrowDodgeBallProperties{
	public Vector3 velocity;
}

public class DodgeBall : MonoBehaviour {
	private SphereCollider _sphereCollider;
	private Rigidbody _rigidBody;

	public bool isCaptured = false;
	public Player capturedBy;
	public bool isInFlight = false;

	private void Awake(){
		_rigidBody = GetComponent<Rigidbody>();
		_sphereCollider = GetComponent<SphereCollider>();
		Reset();
	}

	public void Reset(){
		_rigidBody.velocity = Vector3.zero;
		_sphereCollider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == TagNames.Player && (capturedBy == null || capturedBy.gameObject != other.gameObject)){
			HandleCollision(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other) {
		if(isCaptured && other.gameObject.tag == TagNames.Player && other.gameObject == capturedBy.gameObject){
			_sphereCollider.isTrigger = false;
			capturedBy = null;
			isCaptured = false;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == TagNames.Player && (capturedBy == null || capturedBy.gameObject != collision.gameObject)){
			HandleCollision(collision.gameObject);
		}
	}

	private void HandleCollision(GameObject other){
		if(!isInFlight && !isCaptured){
			_rigidBody.velocity = Vector3.zero;
			isCaptured = true;
			capturedBy = other.GetComponent<Player>();
			isInFlight = false;
			_sphereCollider.isTrigger = true;
			capturedBy.SendMessage(GameMessages.DODGE_BALL_CAPTURED, this);
		}
	}

	private void OnThrowDodgeBall(ThrowDodgeBallProperties throwDodgeBallProperties){
		isInFlight = false;
		transform.parent = null;
		_rigidBody.velocity = throwDodgeBallProperties.velocity;
	}
}
