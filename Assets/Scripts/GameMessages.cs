using UnityEngine;
using System.Collections;

public class GameMessages  
{
	// character motor related
	// TODO: move outside of GameMessages (pgeorges)
	public const string CHARACTER_FALL = "OnCharacterFall";
	public const string CHARACTER_LAND = "OnCharacterLand";
	public const string CHARACTER_JUMP = "OnCharacterJump";
	public const string CHARACTER_DOUBLE_JUMP = "OnCharacterDoubleJump";
	public const string CHARACTER_AFFECTED_BY_EXTERNAL_VELOCITY = "OnCharacterAffectedByExternalVelocity";
	
	// player related
	public const string ADD_PLAYER = "OnAddPlayer";
	public const string ADD_PLAYER_WITH_AXIS_MAP = "OnAddPlayerWithAxisMap";
	public const string REASSIGN_PLAYER_AXIS_MAP = "OnReassignPlayerAxisMap";
	public const string OTHER_DEAD = "OnOtherDead";
	public const string SELF_DEAD = "OnSelfDead";
	public const string RESET_PLAYER = "OnResetPlayer";
	public const string TAKE_DAMAGE = "OnTakeDamage";
	public const string PROJECTILE_TRIGGER_HIT = "OnProjectileTriggerHit";
	public const string BULLET_HIT = "OnBulletHit";

	// dodge ball related
	public const string DODGE_BALL_CAPTURED = "OnDodgeBallCaptured";
	public const string THROW_DODGE_BALL = "OnThrowDodgeBall";

	// level related
	public const string RESET_LEVEL = "OnResetLevel";

	// flow related
	public const string COMMON_STARTUP_COMPLETE = "OnCommonStartupComplete";
	public const string PAUSE_GAME = "OnPauseGame";
	public const string RESUME_GAME = "OnResumeGame";
	public const string SEND_FLOW_EVENT = "OnSendFlowEvent";
	public const string FORWARD = "OnForward";
	public const string BACK = "OnBack";
	public const string START_SLOW_MOTION = "OnStartSlowMotion";

	// effects related
	public const string SWITCH_TEXTURES_FOR_MATERIALS = "OnSwitchTexturesForMaterials";
}
