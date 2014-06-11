using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the starting and stopping of the slow motion effect.
/// </summary>
public class SlowMotionManager : MonoBehaviour 
{
    #region Members
    private float slowTime;
    private float slowFactor;
    
    private float newTimeScale;  
    private float currentSlowTime;

    // TODO: do we really need to adjust the physics time scale.
    private bool adjustPhysicsTimeScale = false;
    #endregion
    
    #region Private Methods
    private void OnDestroy()
    {
        OnDisable();
    }

    private void OnEnable()
    {
        Messenger<float, float>.AddListener(GameMessages.START_SLOW_MOTION, OnStartSlowMotion);
    }

    private void OnDisable()
    {
        Messenger<float, float>.RemoveListener(GameMessages.START_SLOW_MOTION, OnStartSlowMotion);
    }

    private void Update()  
    {  
        UpdateTimeScale();
    }

    private void OnStartSlowMotion(float slowTime, float slowFactor)
    {
        // only start slow motion if we aren't already in one.
        if (Time.timeScale == 1.0f)  
        {  
            this.slowTime = slowTime;
            this.slowFactor = slowFactor;
            newTimeScale = Time.timeScale / this.slowFactor;

            Time.timeScale = newTimeScale;  

            // According to the Unity docs, we should adjust these as well but doing so actually breaks the rigidbody physics.
            if (adjustPhysicsTimeScale)
            {
                //proportionally reduce the 'fixedDeltaTime', so that the Rigidbody simulation can react correctly  
                Time.fixedDeltaTime = Time.fixedDeltaTime / slowFactor;  
                
                //The maximum amount of time of a single frame  
                Time.maximumDeltaTime = Time.maximumDeltaTime / slowFactor;  
            }
        }
    }

    private void UpdateTimeScale()
    {
        // if the game is running in slow motion
        if (Time.timeScale == newTimeScale)
        {  
            currentSlowTime += (Time.deltaTime * slowFactor);

            if (currentSlowTime >= slowTime)
            {
                Reset();
            }
        }  
    }
    
    private void Reset()
    {
        Time.timeScale = 1.0f;  

        if (adjustPhysicsTimeScale)
        {
            Time.fixedDeltaTime = Time.fixedDeltaTime * slowFactor;  
            Time.maximumDeltaTime = Time.maximumDeltaTime * slowFactor;  
        }

        currentSlowTime = 0.0f;
        slowTime = 0.0f;
        slowFactor = 1.0f;
    }
    #endregion
}