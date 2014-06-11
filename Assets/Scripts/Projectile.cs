using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public AnimationCurve speedCurve;
    public bool deflected = false;
    public GameObject owner;
	public LayerMask hitMask;
	public Vector3 originPoint;

	private Transform _bulletManager;
	private bool _collisionComplete = false;
	
	private void Awake()
    {	
        transform.parent = ProjectileManager.Instance.projectileContainer;
	}

    private void OnTriggerEnter(Collider other)
    {
		if((hitMask.value & 1 << other.gameObject.layer) != 0 && other.gameObject != owner && !_collisionComplete) {
			_collisionComplete = true;

			ProjectileHit projectileHit = new ProjectileHit();
			projectileHit.originObject = owner;
			projectileHit.originPoint = originPoint;
			projectileHit.hitObject = other.gameObject;
			projectileHit.hitObjectLayer = other.gameObject.layer;

			ProcessTriggerHit(ref projectileHit);
			ProcessCollision(projectileHit);
		}
    }

    private void OnCollisionEnter(Collision other)
    {
		if((hitMask.value & 1 << other.gameObject.layer) != 0 && other.gameObject != owner && !_collisionComplete) {
			_collisionComplete = true;

			ProjectileHit projectileHit = new ProjectileHit();
			projectileHit.originObject = owner;
			projectileHit.originPoint = originPoint;
			projectileHit.hitObject = other.gameObject;
			projectileHit.hitObjectLayer = other.gameObject.layer;
			projectileHit.normal = other.contacts[0].normal;
			projectileHit.hitPoint = other.contacts[0].point;

			ProcessCollision(projectileHit);
		}
    }

	private void OnRedirectedCollision(ProjectileHit projectileHit){
		if((hitMask.value & 1 << projectileHit.hitObject.layer) != 0 && projectileHit.hitObject != owner && !_collisionComplete) {
			_collisionComplete = true;

			ProcessCollision(projectileHit);
		}
	}
    
	private void ProcessTriggerHit(ref ProjectileHit projectileHit)
    {
		projectileHit.hitPoint = projectileHit.hitObject.collider.ClosestPointOnBounds(transform.position);
		projectileHit.hitObject.SendMessage(GameMessages.PROJECTILE_TRIGGER_HIT, projectileHit, SendMessageOptions.DontRequireReceiver);
	}

	private void ProcessCollision(ProjectileHit projectileHit)
    {
		ProcessBulletHit(projectileHit);
    }

	private void ProcessBulletHit(ProjectileHit projectileHit)
	{
		SendMessage(GameMessages.BULLET_HIT, projectileHit, SendMessageOptions.DontRequireReceiver);
	}
}
