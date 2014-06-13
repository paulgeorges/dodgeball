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

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == TagNames.Player && (capturedBy == null || capturedBy.gameObject != collision.gameObject)){
			HandleCollision(collision.gameObject);
		}
	}

	private void HandleCollision(GameObject other){
		if(!isInFlight && !isCaptured){
			isCaptured = true;
			capturedBy = other.GetComponent<Player>();
			isInFlight = false;
			_sphereCollider.isTrigger = true;
			capturedBy.SendMessage(GameMessages.DODGE_BALL_CAPTURED, this);
		}
	}

	private void OnThrowDodgeBall(ThrowDodgeBallProperties throwDodgeBallProperties){
		StartCoroutine(HandleThrowDodgeBall(throwDodgeBallProperties));
	}

	private IEnumerator HandleThrowDodgeBall(ThrowDodgeBallProperties throwDodgeBallProperties){
		isCaptured = false;
		capturedBy = null;
		isInFlight = false;
		transform.parent = null;
		_rigidBody.velocity = throwDodgeBallProperties.velocity;
		yield return new WaitForFixedUpdate();
		_sphereCollider.isTrigger = false;
	}
}
