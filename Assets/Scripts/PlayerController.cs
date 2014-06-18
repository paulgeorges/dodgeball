using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Members
    // public
    [HideInInspector]
    public bool useVirtualGamepad = false;

	public bool isMoving = false;
    public Vector3 moveDirection = Vector3.right;
	public Vector3 aimingDirection = Vector3.right;
	public float horizontalMovement;
	public float verticalMovement;
	public float horizontalAim;
	public float verticalAim;
	public bool horizontalAimApplied;
	public bool verticalAimApplied;
	public float preWaitTime = 0.1f;
	public float captureTime = 0.1f;
	public float postWaitTime = 0.3f;

    // private
	private Player _player;
	private CharacterMotor _characterMotor;
    private CharacterController _characterController;
	private PlayerCharacter _playerCharacter;
	private Health _health;
	private JoystickEvent _joystickEvent;
	private List<Animator> _animators;
	private bool _canUpdatePlayer = true;
	private CenterPoint _centerPoint;
	private HitZone _hitZone;
	private float _idleTime;
	private DodgeBall _dodgeBall;
	private bool _receiving = false;
	private DodgeBallAttachPoint _dodgeBallAttachPoint;

    // properties
	public bool CanUpdatePlayer
	{
		get {
			return _canUpdatePlayer;
		}

		set {
			_canUpdatePlayer = value;
		}
	}

	private Player Player
	{
		get {
			if (_player == null) {
				_player = GetComponent<Player>();
			}

			return _player;
		}
	}

    private Health Health
    {
        get {
            if (_health == null) {
                _health = GetComponent<Health>();
            }

            return _health;
        }
    }

    private List<Animator> Animators
    {
        get {
            if (_animators == null) {
				UpdateAnimatorList();
            }

			return _animators;
        }
    }

    private JoystickEvent JoystickEvent
    {
        get {
            if (_joystickEvent == null) {
                _joystickEvent = GetComponent<JoystickEvent>();
            }

            return _joystickEvent;
        }
    }

    private CharacterMotor CharacterMotor
    {
        get {
            if (_characterMotor == null) {
                _characterMotor = GetComponent<CharacterMotor>();
            }

            return _characterMotor;
        }
    }

    private CharacterController CharacterController
    {
        get {
            if (_characterController == null) {
                _characterController = GetComponent<CharacterController>();
            }

            return _characterController;
        }
    }

	private CenterPoint CenterPoint
	{
		get {
			if (_centerPoint == null) {
				_centerPoint = GetComponentInChildren<CenterPoint>();
			}
			
			return _centerPoint;
		}
	}

	private HitZone HitZone
	{
		get {
			if (_hitZone == null) {
				_hitZone = GetComponentInChildren<HitZone>();
			}
			
			return _hitZone;
		}
	}

	private PlayerCharacter PlayerCharacter
	{
		get {
			if (_playerCharacter == null) {
				_playerCharacter = GetComponentInChildren<PlayerCharacter>();
			}
			
			return _playerCharacter;
		}
	}

	private DodgeBallAttachPoint DodgeBallAttachPoint
	{
		get {
			if (_dodgeBallAttachPoint == null) {
				_dodgeBallAttachPoint = GetComponentInChildren<DodgeBallAttachPoint>();
			}
			
			return _dodgeBallAttachPoint;
		}
	}
	
	#endregion
	
	#region Unity Callbacks
	private void Awake()
    {
        // reset the move direction
        moveDirection = transform.TransformDirection(Vector3.right);

        // check to see whether or not we are on mobile and must use a virtual gamepad
        useVirtualGamepad = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }

    private void Update()
    {
        if (!_canUpdatePlayer) {
            return;
        }

        // if we're not animating death and we're not being knocked back
        if (!Health.postDeathInvincible) {
            HandleInput();
        }
        // if we're either animating dead or getting knocked back (same treatment)
        else {
            ResetPlayer();
        }
    }
    #endregion

    #region Public Methods
	public void UpdateAnimatorList(){
		Animator[] allAnimators = GetComponentsInChildren<Animator>();
		
		if(_animators == null){
			_animators = new List<Animator>();
		}else{
			_animators.Clear();
		}
		
		foreach(Animator animator in allAnimators){
			if(animator.runtimeAnimatorController != null){
				_animators.Add(animator);
			}
		}
	}
    
    #endregion

    #region Private Methods
    private void HandleInput()
    {
		if(Player && Player.AxisMap.axisPrefix != ""){
			HandleAiming();
			HandleAttack();
			HandlePlayerMovement();
		}
	}

    public void ResetPlayer()
    {
        // set everything to default
        isMoving = false;

        CharacterMotor.inputMoveDirection = Vector3.zero;
        AnimatorsSetFloat("Speed", 0);
        AnimatorsSetBool("Aiming", false);
        AnimatorsSetBool("Jump", false);
        AnimatorsSetFloat("AimDirectionHorizontal", 0);
        AnimatorsSetFloat("AimDirectionVertical", 0);
    }

    private void HandleAiming()
    {
        // if playing on a gamepad
        if (Player.AxisMap.isGamePad) {

        }
        // if the axis map is not setup for a gamepad (mouse aiming)
        else {
            if (Camera.main) {
                // update mouse aiming direction
				Vector3 positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
                positionOnScreen.z = 0;
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                aimingDirection = (mousePosition - positionOnScreen).normalized;
            }
        }
    }

	private void HandleAttack(){
		if(Input.GetButtonDown(Player.AxisMap [AxisEnum.FIRE])){
			if (_dodgeBall) {
				ThrowDodgeBallProperties throwDodgeBallProperties = new ThrowDodgeBallProperties();
				throwDodgeBallProperties.velocity = aimingDirection.normalized * 20;
				_dodgeBall.BroadcastMessage(GameMessages.THROW_DODGE_BALL, throwDodgeBallProperties);
				_dodgeBall = null;
			}else if(!_receiving){
				StartCoroutine(HandleReceive());
			}
		}
	}
	
	private IEnumerator HandleReceive(){
		_receiving = true;
		yield return new WaitForSeconds(preWaitTime);
		_hitZone.SetActive(true);
		yield return new WaitForSeconds(captureTime);
		_hitZone.SetActive(false);
		yield return new WaitForSeconds(postWaitTime);
		_receiving = false;
	}

    private void HandlePlayerMovement()
    {
        if (useVirtualGamepad) {
            horizontalMovement = JoystickEvent.moveX;
        }
        else {
            horizontalMovement = Input.GetAxisRaw(Player.AxisMap [AxisEnum.HORIZONTAL]);
            if (Player.AxisMap.isGamePad && horizontalMovement < float.Epsilon && horizontalMovement > -float.Epsilon) {
                horizontalMovement = Input.GetAxisRaw(Player.AxisMap [AxisEnum.HORIZONTAL_DPAD]);
            }
        }

		if (useVirtualGamepad) {
			verticalMovement = JoystickEvent.moveY;
		}
		else {
			verticalMovement = Input.GetAxisRaw(Player.AxisMap [AxisEnum.VERTICAL]);
			if (Player.AxisMap.isGamePad && verticalMovement < float.Epsilon && verticalMovement > -float.Epsilon) {
				verticalMovement = Input.GetAxisRaw(Player.AxisMap [AxisEnum.VERTICAL_DPAD]);
			}
		}

		isMoving = Mathf.Abs(horizontalMovement) >= 0.1f;

		if (isMoving) {
			// move forwards in the direction we're looking
			moveDirection = new Vector3(horizontalMovement, 0, 0);
		}

		horizontalAim = Mathf.RoundToInt(horizontalMovement);
		verticalAim = Mathf.RoundToInt(verticalMovement);

		horizontalAimApplied = Mathf.Abs(horizontalAim) == 1;
		verticalAimApplied = Mathf.Abs(verticalAim) == 1;

		if(!horizontalAimApplied && !verticalAimApplied){
			if(moveDirection.x > 0){
				aimingDirection.x = 1;
				aimingDirection.y = 0;
			}else{
				aimingDirection.x = -1;
				aimingDirection.y = 0;
			}
		}else{
			if (horizontalAimApplied) {
				if(verticalAimApplied){
					aimingDirection.x = horizontalAim / 2;
				}else{
					aimingDirection.x = horizontalAim;
				}
			}else{
				aimingDirection.x = 0;
			}
			
			if (verticalAimApplied) {
				if(horizontalAimApplied){
					aimingDirection.y = verticalAim / 2;
				}else{
					aimingDirection.y = verticalAim;
				}
			}else{
				aimingDirection.y = 0;
			}
		}

		if(HitZone){
			HitZone.transform.rotation = Quaternion.LookRotation(aimingDirection);
			HitZone.transform.localPosition = new Vector3(0, aimingDirection.y, aimingDirection.x);
		}

        // set our speed to the animator
        AnimatorsSetFloat("Speed", Mathf.Abs(horizontalMovement));

		if (isMoving) {
			// calculate actual motion
			Vector3 movement = moveDirection;
			movement *= Time.deltaTime;
			
			// move the motor
			CharacterMotor.inputMoveDirection = moveDirection.normalized;
		}
		else {
			// don't move the motor
			CharacterMotor.inputMoveDirection = Vector3.zero;
		}

        // if grounded, moving and not attacking
        if ((CharacterMotor.IsGrounded() && isMoving) || !CharacterMotor.IsGrounded()) {
            // set idle time
			_idleTime = 0;
		}
		else {
            // increase idle time
			_idleTime += Time.deltaTime;
        }
        
        // input jump to the character motor
        CharacterMotor.inputJump = (useVirtualGamepad && JoystickEvent.jump) || (!useVirtualGamepad && Input.GetButton(Player.AxisMap [AxisEnum.JUMP]));
        
		PlayerCharacter.transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    private IEnumerator ResetAfterDeadAnimation()
    {
        yield return new WaitForSeconds(4f);

        if (Health.lives > 0) {
            Messenger<Player>.Invoke(GameMessages.RESET_PLAYER, Player);
        }
        else {
            Messenger.Invoke(GameMessages.RESET_LEVEL);
        }

        AnimatorsSetBool("Dead", false);
        Health.postDeathInvincible = false;
    }

    #endregion

    #region Message Receivers
	
	private void OnDodgeBallCaptured(DodgeBall dodgeBall){
		if(_receiving){
			StopCoroutine(HandleReceive());
			_receiving = false;
		}

		_dodgeBall = dodgeBall;

		if(DodgeBallAttachPoint){
			_dodgeBall.transform.parent = DodgeBallAttachPoint.transform;
		}else{
			_dodgeBall.transform.parent = transform;
		}

		_dodgeBall.transform.localPosition = Vector3.zero;
	}

	private void OnDodgeBallHit(DodgeBall dodgeBall){
		Messenger<Player>.Invoke(GameMessages.RESET_PLAYER, _player);
	}

    private void OnAnimatorIK(int layerIndex)
    {
        if (!_canUpdatePlayer) {
            return;
        }
    }

    private void OnJump()
    {
        if (!_canUpdatePlayer) {
            return;
        }

        AnimatorsSetBool("Jump", true);
        //MasterAudio.PlaySoundAndForget("BlazeJump");   commented for now because the sfx is annoying
    }
    
    private void OnDoubleJump()
    {
        if (!_canUpdatePlayer) {
            return;
        }

        AnimatorsSetBool("DoubleJump", true);
    }
    
    private void OnFall()
    {
        if (!_canUpdatePlayer) {
            return;
        }
    }
    
    private void OnLand()
    {
        if (!_canUpdatePlayer) {
            return;
        }

        AnimatorsSetBool("Jump", false);
        AnimatorsSetBool("DoubleJump", false);
    }
    
    private void OnSelfDead()
    {
		Health.postDeathInvincible = true;
        AnimatorsSetBool("Dead", true);
        StartCoroutine("ResetAfterDeadAnimation");
    }

    private IEnumerator ResetAnimatorParameter(int parameterId)
    {
        yield return new WaitForEndOfFrame();
        if (Animators != null) {
            AnimatorsSetBool(parameterId, false);
        }
    }

	private void AnimatorsSetFloat(string varName, float value){
		foreach(Animator animator in Animators){
			animator.SetFloat(varName, value);
		}
	}

	private void AnimatorsSetFloat(int parameterId, float value){
		foreach(Animator animator in Animators){
			animator.SetFloat(parameterId, value);
		}
	}

	private void AnimatorsSetInteger(string varName, int value){
		foreach(Animator animator in Animators){
			animator.SetInteger(varName, value);
		}
	}

	private void AnimatorsSetInteger(int parameterId, int value){
		foreach(Animator animator in Animators){
			animator.SetInteger(parameterId, value);
		}
	}

	private void AnimatorsSetBool(string varName, bool value){
		foreach(Animator animator in Animators){
			animator.SetBool(varName, value);
		}	
	}

	private void AnimatorsSetBool(int parameterId, bool value){
		foreach(Animator animator in Animators){
			animator.SetBool(parameterId, value);
		}	
	}

	private float AnimatorsGetFloat(string varName){
		float value = 0;
		if(Animators != null && Animators.Count > 0){
			value = Animators[0].GetFloat(varName);
		}

		return value;
	}
	
	private int AnimatorsGetInteger(string varName){
		int value = 0;
		if(Animators != null && Animators.Count > 0){
			value = Animators[0].GetInteger(varName);
		}
		
		return value;
	}
	
	private bool AnimatorsGetBool(string varName){
		bool value = false;
		if(Animators != null && Animators.Count > 0){
			value = Animators[0].GetBool(varName);
		}
		
		return value;
	}

    #endregion
}