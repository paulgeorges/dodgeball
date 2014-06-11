using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct InputController
{
	public InputManager.Controllers controller;
	public int index;

	public InputController(InputManager.Controllers controller, int controllerIndex) 
	{
		this.controller = controller;
		this.index = controllerIndex;
	}
}

[System.Serializable]
public class AxisMap : Dictionary<AxisEnum, string>
{
	public string axisPrefix;
	public bool isInUse = false;
	public bool isGamePad = true;
}

/// <summary>
/// Manages input devices.
/// </summary>
public sealed class InputManager : MonoBehaviour 
{
	#region Members
	public enum Controllers
	{
		None, Playstation3, Playstation4, Xbox360
	};
	
	private static string[] _playstation3ControllerNames = new string[] {
		"Sony PLAYSTATION(R)3 Controller"
	};
	
	private static string[] _playstation4ControllerNames = new string[] {
		"Sony Computer Entertainment Wireless Controller"
	};
	
	private static string[] _xbox360ControllerNames = new string[] {
		"360"
	};

	private InputController[] _inputControllers;
	public int nbControllers;
	public List<AxisMap> axisMaps;

	#endregion

	private static InputManager _instance;
	
	public static InputManager Instance
	{
		get {
			if (_instance == null) {
				GameObject inputManagerGO = GameObject.FindGameObjectWithTag(TagNames.InputManager);
				if (inputManagerGO) {
					_instance = inputManagerGO.GetComponent<InputManager>();
				}
			}
			
			return _instance;
		}
	}

	#region Unity Callbacks
	private void Awake()
	{
		RefreshGameControllers();
		CreateAxisMaps();
	}
	
	#endregion

	#region Public Methods
	/// <summary>
	/// Refreshes the list of game controllers currently detected.
	/// </summary>
	public void RefreshGameControllers()
	{
		_inputControllers = null;
		GetActiveGameControllers();
	}

	/// <summary>
	/// Gets the list of active game controllers.
	/// Use <code>RefreshGameControllers</code> to update list.
	/// </summary>
	/// <returns>The active game controllers.</returns>
	public InputController[] GetActiveGameControllers()
	{
		if (_inputControllers == null) {
			string[] controllerNames = Input.GetJoystickNames();
			nbControllers = controllerNames.Length;

			_inputControllers = new InputController[controllerNames.Length];

			if (controllerNames.Length > 0) {
				for (int i = 0; i < controllerNames.Length; ++i) {
					string controllerName = controllerNames[i];
					if (IsPlaystation3Controller(controllerName)) {
						_inputControllers[i] = new InputController(Controllers.Playstation3, i);
					}
					else if (IsPlaystation4Controller(controllerName)) {
						_inputControllers[i] = new InputController(Controllers.Playstation4, i);
					}
					else if (IsXboxController(controllerName)) {
						_inputControllers[i] = new InputController(Controllers.Xbox360, i);
					}
				}
			}
		}

		return _inputControllers;
	}

	public InputController[] GetFilteredGameControllers(InputManager.Controllers filter)
	{
		List<InputController> filteredControllers = new List<InputController>();
		InputController[] controllers = GetActiveGameControllers();
		for (int i = 0; i < controllers.Length; ++i) {
			if (controllers[i].controller == filter) {
				filteredControllers.Add(controllers[i]);
			}
		}

		return filteredControllers.ToArray();
	}

	public bool IsPlaystation3Controller(string controllerName)
	{
		bool isPlaystation3 = false;
		
		for (int i = 0; i < _playstation3ControllerNames.Length; ++i) {
			if (controllerName.ToLower().Contains(_playstation3ControllerNames[i].ToLower())) {
				isPlaystation3 = true;
				break;
			}
		}
		
		return isPlaystation3;
	}

	public bool IsPlaystation4Controller(string controllerName)
	{
		bool isPlaystation4 = false;
		
		for (int i = 0; i < _playstation4ControllerNames.Length; ++i) {
			if (controllerName.ToLower().Contains(_playstation4ControllerNames[i].ToLower())) {
				isPlaystation4 = true;
				break;
			}
		}
		
		return isPlaystation4;
	}
	
	public bool IsXboxController(string controllerName)
	{
		bool isXbox = false;
		
		for (int i = 0; i < _xbox360ControllerNames.Length; ++i) {
			if (controllerName.ToLower().Contains(_xbox360ControllerNames[i].ToLower())) {
				isXbox = true;
				break;
			}
		}
		
		return isXbox;
	}
	
	#endregion

	#region Private Methods
	private void CreateAxisMaps()
	{
		foreach (string joystickName in Input.GetJoystickNames()) {
			Debug.Log(joystickName);
		}
		
		axisMaps = new List<AxisMap>();
		
		List<string> axisMapPrefixes = new List<string>();
		
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
			InputController[] inputControllers = GetActiveGameControllers();
			foreach (InputController inputController in inputControllers) {
				if (inputController.controller == InputManager.Controllers.Playstation3) {
					axisMapPrefixes.Add("MAC:PS3C" + (inputController.index + 1) + ":");
				}
				else if (inputController.controller == InputManager.Controllers.Playstation4) {
					axisMapPrefixes.Add("MAC:PS4C" + (inputController.index + 1) + ":");
				}
			}
			
			axisMapPrefixes.Add("PC-MAC:KM:");
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer) {
			InputController[] inputControllers = InputManager.Instance.GetActiveGameControllers();
			foreach (InputController inputController in inputControllers) {
				if (inputController.controller == InputManager.Controllers.Playstation4) {
					axisMapPrefixes.Add("PC:PS4C" + (inputController.index + 1) + ":");
				}
				else if (inputController.controller == InputManager.Controllers.Xbox360) {
					axisMapPrefixes.Add("PC:XBOX360C" + (inputController.index + 1) + ":");
				}
			}
			
			axisMapPrefixes.Add("PC-MAC:KM:");
		}
		else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			axisMapPrefixes.Add("MOBILE:M1:");
		}
		
		foreach (string axisMapPrefix in axisMapPrefixes) {
			AxisMap axisMap = new AxisMap();
			axisMap.axisPrefix = axisMapPrefix;
			axisMap.isInUse = false;
			axisMap.isGamePad = !axisMapPrefix.Contains("KM");
			axisMap [AxisEnum.HORIZONTAL] = axisMapPrefix + "Horizontal";
			axisMap [AxisEnum.HORIZONTAL_DPAD] = axisMapPrefix + "HorizontalDpad";
			axisMap [AxisEnum.VERTICAL] = axisMapPrefix + "Vertical";
			axisMap [AxisEnum.VERTICAL_DPAD] = axisMapPrefix + "VerticalDpad";
			axisMap [AxisEnum.AIM_HORIZONTAL] = axisMapPrefix + "AimHorizontal";
			axisMap [AxisEnum.AIM_VERTICAL] = axisMapPrefix + "AimVertical";
			axisMap [AxisEnum.JUMP] = axisMapPrefix + "Jump";
			axisMap [AxisEnum.FIRE] = axisMapPrefix + "Fire";
			axisMap [AxisEnum.SECONDARY_FIRE] = axisMapPrefix + "SecondaryFire";
			axisMap [AxisEnum.SWAP_AMMO] = axisMapPrefix + "SwapAmmo";
			axisMap [AxisEnum.SPECIAL] = axisMapPrefix + "Special";
			axisMap [AxisEnum.LOCK] = axisMapPrefix + "Lock";
			axisMap [AxisEnum.MELEE] = axisMapPrefix + "Melee";
			axisMap [AxisEnum.START] = axisMapPrefix + "Start";
			axisMap [AxisEnum.SELECT] = axisMapPrefix + "Select";
			
			axisMaps.Add(axisMap);
		}
	}

	public bool IsAxisMapAvailable()
	{
		foreach (AxisMap axisMap in axisMaps) {
			if (!axisMap.isInUse) {
				return true;
			}
		}
		
		return false;
	}
	
	public AxisMap IsAxisDown(AxisEnum axisEnum)
	{
		foreach (AxisMap axisMap in axisMaps) {
			if (Input.GetButton(axisMap [axisEnum])) {
				return axisMap;
			}
		}
		
		return null;
	}
	
	public AxisMap GetFirstUnusedAxisMap()
	{
		foreach (AxisMap axisMap in axisMaps) {
			if (!axisMap.isInUse) {
				axisMap.isInUse = true;
				return axisMap;
			}
		}
		
		return null;
	}
	
	public AxisMap GetNextUnusedAxisMap(AxisMap currentAxisMap)
	{
		int currentAxisMapIndex = axisMaps.IndexOf(currentAxisMap);
		
		if (currentAxisMapIndex != -1) {
			int axisMapIndexToCheck = currentAxisMapIndex;
			
			axisMapIndexToCheck = currentAxisMapIndex == axisMaps.Count - 1 ? 0 : axisMapIndexToCheck + 1;
			while (axisMapIndexToCheck != currentAxisMapIndex) {
				AxisMap axisMapToCheck = axisMaps [axisMapIndexToCheck];
				if (!axisMapToCheck.isInUse) {
					currentAxisMap.isInUse = false;
					axisMapToCheck.isInUse = true;
					return axisMapToCheck;
				}
				else {
					axisMapIndexToCheck++;
				}
			}
		}
		
		return currentAxisMap;
	}
	#endregion
}
