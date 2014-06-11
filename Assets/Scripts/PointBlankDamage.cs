using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointBlankDamage : MonoBehaviour
{
    #region Members
    // public
    public float pointBlankDamage = 1.0f;

    #endregion
	
    #region Private Methods

	private void OnBulletHit(ProjectileHit projectileHitInfo)
	{
		if(projectileHitInfo.hitObject){
			DamageInfo damageInfo = new DamageInfo();
			damageInfo.originPoint = projectileHitInfo.originPoint;
			damageInfo.impactPoint = projectileHitInfo.hitPoint;
			damageInfo.normal = projectileHitInfo.normal;
			damageInfo.damageDone = pointBlankDamage;
			
			projectileHitInfo.hitObject.SendMessage(GameMessages.TAKE_DAMAGE, damageInfo, SendMessageOptions.DontRequireReceiver);
		}
	}

    #endregion
}
