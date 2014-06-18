using UnityEngine;
using System.Collections;

public class ThrowDodgeBallProperties{
	public Vector3 velocity;
}

public class DodgeBall : MonoBehaviour {
	private SphereCollider _sphereCollider;
	private Rigidbody _rigidBody;
	private GameObject _dodgeBallManager;

	public bool isCaptured = false;
	public Player capturedBy;
	public bool isInFlight = false;

	private void Awake(){
		_rigidBody = GetComponent<Rigidbody>();
		_sphereCollider = GetComponent<SphereCollider>();
		_dodgeBallManager = GameObject.FindGameObjectWithTag(TagNames.DodgeBallManager);
		transform.parent = _dodgeBallManager.transform;
		Reset();
	}

	public void Reset(){
		_rigidBody.velocity = Vector3.zero;
		_sphereCollider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other) {
		HandleCollision(other.gameObject);
	}

	private void OnCollisionEnter(Collision collision) {
		HandleCollision(collision.gameObject);
	}

	private void OnTriggerExit(Collider other) {
		if(isCaptured && other.gameObject.tag == TagNames.Player && other.gameObject == capturedBy.gameObject){
			_sphereCollider.isTrigger = false;
			capturedBy = null;
			isCaptured = false;
		}
	}
	
	private void HandleCollision(GameObject other){
		if(other.tag == TagNames.Player){
			if(!isCaptured && !isInFlight && !capturedBy){
				HandleCapture(other);
			}else if(isInFlight && other != capturedBy){
				HandleHit(other);
			}
		}else if(other.tag == TagNames.HitZone){
			HandleCapture(other);
		}
	}

	private void HandleHit(GameObject other){
		_rigidBody.velocity = Vector3.zero;
		isCaptured = false;
		capturedBy = null;
		isInFlight = false;
		_sphereCollider.isTrigger = true;
		other.SendMessage(GameMessages.DODGE_BALL_HIT, this);
	}

	private void HandleCapture(GameObject other){
		Player player = null;
		HitZone hitZone = other.GetComponent<HitZone>();
		if(hitZone && hitZone.owner){
			player = hitZone.owner;
		}else{
			player = other.GetComponent<Player>();
		}

		if(player){
			_rigidBody.velocity = Vector3.zero;
			isCaptured = true;
			capturedBy = player;
			isInFlight = false;
			_sphereCollider.isTrigger = true;
			capturedBy.SendMessage(GameMessages.DODGE_BALL_CAPTURED, this);
		}
	}

	private void OnThrowDodgeBall(ThrowDodgeBallProperties throwDodgeBallProperties){
		isInFlight = true;
		transform.parent = _dodgeBallManager.transform;
		_rigidBody.velocity = throwDodgeBallProperties.velocity;
	}
}
