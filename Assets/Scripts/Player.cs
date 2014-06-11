using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AxisEnum
{
    HORIZONTAL,
    HORIZONTAL_DPAD,
    VERTICAL,
    VERTICAL_DPAD,
    AIM_HORIZONTAL,
    AIM_VERTICAL,
    FIRE,
    SECONDARY_FIRE,
    JUMP,
    SWAP_AMMO,
    SPECIAL,
    LOCK,
    MELEE,
    SELECT,
    START
}

public enum PlayerLocationEnum
{
    LOCAL,
    NETWORK
}

public class Player : MonoBehaviour
{
    #region Members
	private const int kInvalidPlayerId = -1;

    [SerializeField]
    private int _playerId = kInvalidPlayerId;

    [SerializeField]
    private int axisId;

    [SerializeField]
    private AxisMap _axisMap;

    private Animator _animator;
    private Health _health;
    private PlayerController _playerController;
    private CharacterMotor _characterMotor;
	private CharacterController _characterController;
    private uLinkNetworkView _uLinkNetworkView;

    // properties
	public PlayerLocationEnum playerLocation;

    public int PlayerId
    {
        get {
            return _playerId;
        }
    }
    
    public AxisMap AxisMap
    {
        get {
            return _axisMap;
        }
        
        set {
            _axisMap = value;
        }
    }
    
    public Health Health
    {
        get {
            if (_health == null) {
                _health = GetComponent<Health>();
            }
            
            return _health;
        }
    }

    public PlayerController PlayerController
    {
        get {
            if (_playerController == null) {
                _playerController = GetComponent<PlayerController>();
            }
            
            return _playerController;
        }
    }
    
    public CharacterMotor CharacterMotor
    {
        get {
            if (_characterMotor == null) {
                _characterMotor = GetComponent<CharacterMotor>();
            }
            
            return _characterMotor;
        }
    }
    
	public uLinkNetworkView ULinkNetworkView
	{
		get {
			if (_uLinkNetworkView == null) {
				_uLinkNetworkView = GetComponent<uLinkNetworkView>();
			}
			
			return _uLinkNetworkView;
		}
	}
	
    private Animator Animator
    {
        get {
            if (_animator == null) {
                _animator = GetComponentInChildren<Animator>();
            }
            
            return _animator;
        }
    }

	private CharacterController CharacterController
	{
		get {
			if (_characterController == null) {
				_characterController = GetComponentInChildren<CharacterController>();
			}
			
			return _characterController;
		}
	}

	#endregion

    #region Component Lifecycle
    private void Awake()
    {

    }

    private void OnEnable()
    {

    }
    
    private void OnDisable()
    {

    }
    #endregion

    #region Public Methods

    public void SetId(int playerId)
    {
        _playerId = playerId;
    }

    public int GetPlayerNumber()
    {
        return _playerId + 1;
    }

	#endregion
}
