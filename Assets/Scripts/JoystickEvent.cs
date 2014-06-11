using UnityEngine;
using System.Collections;

public class JoystickEvent : MonoBehaviour
{
	public float moveX = 0;
	public float moveY = 0;
	public float aimX = 0;
	public float aimY = 0;
	public bool jump = false;
	
	void OnEnable ()
	{
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		EasyButton.On_ButtonDown += On_ButtonDown;
		EasyButton.On_ButtonUp += On_ButtonUp;
	}
	
	void OnDisable ()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
		EasyButton.On_ButtonDown -= On_ButtonDown;
		EasyButton.On_ButtonUp -= On_ButtonUp;
	}
	
	void On_JoystickMoveEnd (MovingJoystick move)
	{
		if (move.joystickName == "MoveJoystick") {
			moveX = 0;
			moveY = 0;
		} else if (move.joystickName == "AimJoystick") {
			aimX = 0;
			aimY = 0;
		}
	}
	
	void On_JoystickMove (MovingJoystick move)
	{
		if (move.joystickName == "MoveJoystick") {
			moveX = move.joystick.JoystickValue.x;
			moveY = move.joystick.JoystickValue.y;
		} else if (move.joystickName == "AimJoystick") {
			aimX = move.joystick.JoystickValue.x;
			aimY = move.joystick.JoystickValue.y;
		}
	}
	
	void On_ButtonDown (string buttonName)
	{
		if (buttonName == "JumpButton")
			jump = true;
	}
	
	void On_ButtonUp (string buttonName)
	{
		if (buttonName == "JumpButton")
			jump = false;
	}
}
