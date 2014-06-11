using UnityEngine;
using System.Collections;

public class GameMessages  
{
	public const string COMMON_STARTUP_COMPLETE = "OnCommonStartupComplete";
	public const string SEND_FLOW_EVENT = "OnSendFlowEvent";
	public const string FORWARD = "FORWARD";
	public const string BACK = "BACK";
	public const string LOADING_COMPLETE = "OnLoadingComplete";
	public const string START_LEVEL = "OnStartLevel";
	public const string LEVEL_STARTED = "OnLevelStarted";

	public const string SET_GAME_CONTROL_TYPE = "OnSetGameControlType";
	public const string SET_CINEMATIC_CONTROL_TYPE = "OnSetCinematicControlType";
	public const string SET_CONTROL_TYPE = "OnSetControlType";

	public const string CREATE_GAME = "OnCreateGame";
	public const string JOIN_GAME = "OnJoinGame";
	public const string INITIALIZE_SERVER = "OnInitializeServer";
	public const string SERVER_INITIALIZED = "OnServerInitialized";
	public const string UNINITIALIZE_SERVER = "OnUninitializeServer";
	public const string SERVER_UNINITIALIZED = "OnServerUninitialized";
	public const string CONNECT_TO_SERVER_WITH_IP_AND_PORT = "OnConnectToServerWithIpAndPort";
	public const string CONNECT_TO_SERVER_WITH_HOSTDATA = "OnConnectToServerWithHostData";
	public const string CONNECTED_TO_SERVER = "OnConnectedToServer";
	public const string DISCONNECT_FROM_SERVER = "OnDisconnectFromServer";

	public const string ADD_PLAYER = "OnAddPlayer";
	public const string ADD_PLAYER_WITH_AXIS_MAP = "OnAddPlayerWithAxisMap";
	public const string ADD_PLAYER_WITH_OPTIONS = "OnAddPlayerWithOptions";
	public const string PLAYER_ADDED = "OnPlayerAdded";

	public const string GUN_MODEL_LOADED = "OnGunModelLoaded";

	public const string REMOVE_PLAYER = "OnRemovePlayer";
	public const string PLAYER_REMOVED = "OnPlayerRemoved";

	public const string SET_PLAYER = "OnSetPlayer";

	public const string PLAYER_TOOK_DAMAGE = "OnPlayerTookDamage";
	public const string RESET_PLAYER = "OnResetPlayer";
	public const string RESET_LEVEL = "OnResetLevel";
	public const string ADD_ENEMY = "ADD_ENEMY";
	public const string ENEMY_ADDED = "ENEMY_ADDED";
	public const string REASSIGN_PLAYER_AXIS_MAP = "REASSIGN_PLAYER_AXIS_MAP";
	public const string START_MULTIPLAYER_SERVER = "START_MULTIPLAYER_SERVER";
	public const string START_MULTIPLAYER_CLIENT = "START_MULTIPLAYER_CLIENT";
	public const string DEPTH_MODE_CHANGED = "DEPTH_MODE_CHANGED";
	public const string CHECKPOINT_REACHED = "OnCheckpointReached";
	public const string START_SLOW_MOTION = "START_SLOW_MOTION";
	public const string AUTO_TARGET_IGNORE_ENVIRONMENT = "AUTO_TARGET_IGNORE_ENVIRONMENT";
	public const string AUTO_TARGET_DONT_IGNORE_ENVIRONMENT = "AUTO_TARGET_DONT_IGNORE_ENVIRONMENT";
	public const string MODULE_PICKUP = "MODULE_PICKUP";
	public const string AMMO_PICKUP = "AMMO_PICKUP";
	public const string AMMO_QUANTITY_UPDATED = "AMMO_QUANTITY_UPDATED";
	public const string AMMO_CHANGED = "AMMO_CHANGED";
	public const string AMMO_ADDED = "AMMO_ADDED";
	public const string AMMO_DEPLETED = "AMMO_DEPLETED";
	public const string PAUSE_GAME = "PAUSE_GAME";
	public const string RESUME_GAME = "RESUME_GAME";
	public const string AH_ANIMATION_END = "AH_ANIMATION_END";
	public const string AH_ANIMATION_EVENT = "AH_ANIMATION_EVENT";
	public const string DISPLAY_BOSS_UI = "DISPLAY_BOSS_UI";
	public const string HIDE_BOSS_UI = "HIDE_BOSS_UI";

	public const string ROTATE_PLAYER = "OnRotatePlayer";

	public const string ENABLE_SPAWN_POINT = "OnEnableSpawnPoint";
	public const string DISABLE_SPAWN_POINT = "OnDisableSpawnPoint";

	public const string START_PATROL = "OnStartPatrol";
	public const string UPDATE_PATROL_DESTINATION = "OnUpdatePatrolDestination";
	public const string PATROL_WAYPOINT_REACHED = "OnPatrolWaypointReached";
	public const string STOP_PATROL = "OnStopPatrol";
	public const string INVERT_PATROL = "OnInvertPatrol";

	public const string MOVE_ELEVATOR_TO_START = "OnMoveElevatorToStart";
	public const string MOVE_ELEVATOR_TO_END = "OnMoveElevatorToEnd";

	public const string OPEN_SLIDING_DOOR = "OnOpenSlidingDoor";
	public const string CLOSE_SLIDING_DOOR = "OnCloseSlidingDoor";

	public const string TARGET_UPDATED = "OnTargetUpdated";
	public const string TARGET_REMOVED = "OnTargetRemoved";
	public const string START_BEING_TARGETTED = "OnStartBeingTargetted";
	public const string STOP_BEING_TARGETTED = "OnStopBeingTargetted";
	public const string START_SHOOTING_AT_TARGET = "OnStartShootingAtTarget";
	public const string STOP_SHOOTING_AT_TARGET = "OnStopShootingAtTarget";
	public const string TARGET_CLOSEST_PLAYER = "OnTargetClosestPlayer";
	public const string TARGET_NOT_FOUND = "OnTargetNotFound";
	public const string TARGET_LOCKED = "OnTargetLocked";

	public const string START_MOVE_TOWARDS_TARGET = "OnStartMoveTowardsTarget";
	public const string MOVE_TOWARDS_TARGET_NOT_REACHED = "OnMoveTowardsTargetNotReached";
	public const string MOVE_TOWARDS_TARGET_REACHED = "OnMoveTowardsTargetReached";
	public const string STOP_MOVE_TOWARDS_TARGET = "OnStopMoveTowardsTarget";

	public const string START_CHARGE_TOWARDS_TARGET = "OnStartChargeTowardsTarget";
	public const string CHARGE_HIT_TARGET = "OnChargeHitTarget";
	public const string STOP_CHARGE_TOWARDS_TARGET = "OnStopChargeTowardsTarget";

	public const string START_WANDERING = "OnStartWandering";
	public const string STOP_WANDERING = "OnStopWandering";
	
	public const string START_AIMING_AT_TARGET = "OnStartAimingAtTarget";
	public const string STOP_AIMING_AT_TARGET = "OnStopAimingAtTarget";

	public const string START_BURST_FIRE = "OnStartBurstFire";
	public const string STOP_BURST_FIRE = "OnStopBurstFire";

	public const string BEGIN_TRAVERSING_LINK = "OnBeginTraversingLink";
	public const string TRAVERSE_LINK_COMPLETE = "OnTraverseLinkComplete";

	public const string START_DEFLECTING_BULLETS = "OnStartDeflectingBullets";
	public const string STOP_DEFLECTING_BULLETS = "OnStopDeflectingBullets";

	public const string FORCE_INVINCIBLE = "OnForceInvincible";

	public const string START_LOOK_AT = "OnStartLookAt";
	public const string STOP_LOOK_AT = "OnStopLookAt";

	public const string START_MELEE_ATTACK = "OnStartMeleeAttack";
	public const string STOP_MELEE_ATTACK = "OnStopMeleeAttack";
	public const string MELEE_HIT = "OnMeleeHit";

	public const string START_KNOCKBACK = "OnStartKnockback";
	public const string STOP_KNOCKBACK = "OnStopKnockback";

	public const string START_CUTSCENE = "OnStartCutscene";
	public const string END_CUTSCENE = "OnEndCutscene";
	public const string SHAKE_CAMERA = "OnShakeCamera";

	public const string BULLET_HIT = "OnBulletHit";
	public const string SET_FROZEN = "OnSetFrozen";
	public const string SET_BURNING = "OnSetBurning";

	public const string SHOW_PREPARE_FIRE_VISUAL_FEEDBACK = "OnShowPrepareFireVisualFeedback";
	public const string PREPARE_FIRE_VISUAL_FEEDBACK_COMPLETE = "OnPrepareFireVisualFeedbackComplete";
	public const string START_FIRING_WITH_PATTERN = "OnStartFiringWithPattern";
	public const string PREPARE_FIRING = "OnPrepareFiring";
	public const string START_FIRING = "OnStartFiring";
	public const string STOP_FIRING = "OnStopFiring";
	public const string FIRE_PROJECTILE = "OnFireProjectile";
	public const string PROJETILE_FIRED = "OnProjectileFired";

	public const string BULLET_AIM_POINT_UPDATED = "OnBulletAimPointUpdated";

	public const string SET_ARMED = "OnSetArmed";
	public const string PREPARE_MISSILE_TRACK = "OnPrepareMissileTrack";
	public const string START_MISSILE_ON_TRACK = "OnStartMissileOnTrack";
	public const string UPDATE_NODE_POSITION = "OnUpdateNodePosition";
	public const string MISSILE_LANDED = "OnMissileLanded";

	public const string FORCE_UPDATE_DISTANCE_DETECTOR = "OnForceUpdateDistanceDetector";
	public const string GAME_OBJECT_DETECTED_WITHIN_DISTANCE = "OnGameObjectDetectedWithinDistance";
	public const string GAME_OBJECT_NO_LONGER_DETECTED_WITHIN_DISTANCE = "OnGameObjectNoLongerDetectedWithinDistance";

	public const string GAME_OBJECT_IN_CAMERA_FOV = "OnGameObjectInCameraFOV";
	public const string GAME_OBJECT_NO_LONGER_IN_CAMERA_FOV = "OnGameObjectNoLongerInCameraFOV"; 
	public const string CHANGE_CAMERA_OFFSET = "OnChangeCameraOffset";

	public const string LOSE_LIFE = "OnLoseLife";
	public const string TAKE_DAMAGE = "OnTakeDamage";
	public const string RECOVER_HEALTH = "OnRecoverHealth";
	public const string STOP_BEHAVIORS = "OnStopBehaviors";
	public const string SELF_DEAD = "OnSelfDead";
	public const string OTHER_DEAD = "OnOtherDead";
	public const string CREATE_RAGDOLL = "OnCreateRagdoll";

	public const string SWITCH_TEXTURES_FOR_MATERIALS = "OnSwitchTexturesForMaterials";

	public const string START_BOSS = "OnStartBoss";
	public const string BOSS_DEFEATED = "OnBossDefeated";

	public const string WEAPON_KNOCK_BACK = "OnWeaponKnockBack";
	
    public const string PROJECTILE_TRIGGER_HIT = "OnProjectileTriggerHit";

	public const string LAUNCH_SPECIAL_ATTACK = "OnLaunchSpecialAttack";
	public const string SPECIAL_ATTACK_USED = "OnSpecialAttackUsed";
	public const string ADD_BULLET_SHELL_TO_MANAGER = "ADD_BULLET_SHELL_TO_MANAGER";
	
	public const string ADD_AMMO = "AddAmmo";
    public const string MODULE_PICKUP_COMPLETE = "OnModulePickupComplete";
	public const string UPDATE_GUN_STATS = "OnUpdateGunStats";

    public const string CHARACTER_JUMP = "OnJump";
    public const string CHARACTER_LAND = "OnLand";
    public const string CHARACTER_DOUBLE_JUMP = "OnDoubleJump";
    public const string CHARACTER_FALL = "OnFall";
    public const string CHARACTER_AFFECTED_BY_EXTERNAL_VELOCITY = "OnExternalVelocity";
	public const string CHARACTER_MOVE = "OnMove";

	public const string SWITCH_AMMO = "OnSwitchAmmo";

	public const string UPDATE_LASER_VFX = "OnUpdateLaserVFX";
	public const string INIT_MODULE = "OnInitModule";
	public const string LOAD_PLAYER_MODEL = "OnLoadPlayerModel";
	public const string PLAYER_MODEL_LOADED = "OnPlayerModelLoaded";

	public const string START_ATTACK = "OnStartAttack";
	public const string STOP_ATTACK = "OnStopAttack";
	public const string DISABLE_ATTACK = "OnDisableAttack";
	public const string ENABLE_ATTACK = "OnEnableAttack";

	// Player Character
    public const string CHARACTER_MODEL_LOADED = "CHARACTER_MODEL_LOADED";
	public const string PLAYER_CHARACTERS_INITIALIZED = "PLAYER_CHARACTERS_INITIALIZED";
}
