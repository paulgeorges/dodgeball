using UnityEngine;
using System.Collections;

/// <summary>
/// Slow motion trigger.
/// </summary>
public class SlowMotion : MonoBehaviour 
{
    public enum SlowMoTypes
    {
        ON_DEATH
    }

    #region Members
    public SlowMoTypes slowMoType;
    public float slowTime = 3.0f;
    public float slowFactor = 4.0f;
    #endregion

    #region Private Methods
    private void SlowDownTime()
    {
        Messenger<float, float>.Invoke(GameMessages.START_SLOW_MOTION, slowTime, slowFactor);
    }

    private void OnSelfDead()
    {
        if (slowMoType == SlowMoTypes.ON_DEATH)
        {
            SlowDownTime();
        }
    }
    #endregion
}