using UnityEngine;
using System.Collections;

public class DamageInfo
{
	public Vector3 originPoint;
	public Vector3 impactPoint;
	public Vector3 normal;
	public float damageDone;
}

public class Health : MonoBehaviour
{
	#region Members
	// public
	public float healthPoints = 25;
	public float currentHealthPoints = 25;
	public int lives = 1;
	public int currentLives = 1;
	public float postDamageInvincibilityTime = 0;
	public bool postDamageInvincible = false;
	public bool postDeathInvincible = false;
	public bool forceInvincible = false;

	// private
	[SerializeField]
	private bool _hasInfiniteLives = false;

	private float _accumulatedPostDamageInvincibilityTime = 0;
	
	#endregion

	#region Unity Callbacks
	private void Start()
	{
		Reset();
	}

	private void Update()
	{
		if (postDamageInvincible) {
			_accumulatedPostDamageInvincibilityTime += Time.deltaTime;
			if (_accumulatedPostDamageInvincibilityTime >= postDamageInvincibilityTime) {
				postDamageInvincible = false;
			}
		}
	}
	#endregion

	#region Public Methods
	public void TakeDamage(float damageDone)
	{
		if (lives > 0) {
			if (!postDamageInvincible && !forceInvincible && !postDeathInvincible) {
				currentHealthPoints -= damageDone;
				StartCoroutine("DamageFlash");
				
				if (postDamageInvincibilityTime > 0) {
					postDamageInvincible = true;
					_accumulatedPostDamageInvincibilityTime = 0;
				}
				
				if (currentHealthPoints <= 0) {
					OnLoseLife();
				}
			}
		}
	}
	#endregion

	#region Private Methods

	public void Reset()
	{
		currentLives = lives;
		currentHealthPoints = healthPoints;
	}
	#endregion

	#region Message Receivers
	private void OnForceInvincible(bool value)
	{
		forceInvincible = value;
	}
	
	private void OnLoseLife()
	{
		if (!_hasInfiniteLives) {
			lives -= 1;
		}

		if (lives > 0) {
			currentHealthPoints = healthPoints;
		}
		
		Messenger<GameObject>.Invoke(GameMessages.OTHER_DEAD, gameObject);
		BroadcastMessage(GameMessages.SELF_DEAD, SendMessageOptions.DontRequireReceiver);
	}
	
	private void OnTakeDamage(DamageInfo damageInfo)
	{
		TakeDamage(damageInfo.damageDone);
	}
	
	private void OnRecoverHealth(float recoveryPoints)
	{
		currentHealthPoints = Mathf.Min(healthPoints, currentHealthPoints + recoveryPoints);
	}
	#endregion
}
