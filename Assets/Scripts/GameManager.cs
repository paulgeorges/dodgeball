using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	#region Members
	// private
	private static bool sIsPaused = false;
    
	// properties
	public static bool IsPaused
	{
		get {
			return sIsPaused;
		}
	}

	#endregion

	#region Unity Callbacks

	#endregion

	#region Public Methods
   	public static void PauseGame()
	{
		if (!sIsPaused) {
			sIsPaused = true;
			Time.timeScale = 0.0f;

			Messenger.Invoke(GameMessages.PAUSE_GAME);
		}
	}

	public static void ResumeGame()
	{
		if (sIsPaused) {
			sIsPaused = false;
			Time.timeScale = 1.0f;

			Messenger.Invoke(GameMessages.RESUME_GAME);
		}
	}
	#endregion
}
