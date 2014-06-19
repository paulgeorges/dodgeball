using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Player> players = new List<Player>();

    private List<PlayerSpawnPoint> _playerSpawnPoints = new List<PlayerSpawnPoint>();
    private bool paused = false;
	private GameObject _playerCreationPoint;
	private static PlayerManager _instance;
	
	public static PlayerManager Instance
	{
		get {
			if (_instance == null) {
				GameObject playerManagerGO = GameObject.FindGameObjectWithTag(TagNames.PlayerManager);
				if (playerManagerGO) {
					_instance = playerManagerGO.GetComponent<PlayerManager>();
				}
			}
			
			return _instance;
		}
	}
	
    #region Component Methods
    private void Awake()
    {
        if (!playerPrefab) {
            playerPrefab = (GameObject)Resources.Load("Player");
        }

		_playerCreationPoint = GameObject.FindGameObjectWithTag(TagNames.PlayerCreationPoint);
    }

	private void Update(){
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			Messenger.Invoke(GameMessages.ADD_PLAYER);
		}
	}

    private void OnEnable()
    {
        Messenger.AddListener(GameMessages.ADD_PLAYER, OnAddPlayer);
        Messenger<AxisMap>.AddListener(GameMessages.ADD_PLAYER_WITH_AXIS_MAP, OnAddPlayerWithAxisMap);
        Messenger<Player>.AddListener(GameMessages.REASSIGN_PLAYER_AXIS_MAP, OnReassignPlayerAxisMap);
        Messenger<Player>.AddListener(GameMessages.RESET_PLAYER, OnResetPlayer);
    }
    
    private void OnDisable()
    {
        Messenger.RemoveListener(GameMessages.ADD_PLAYER, OnAddPlayer);
        Messenger<AxisMap>.RemoveListener(GameMessages.ADD_PLAYER_WITH_AXIS_MAP, OnAddPlayerWithAxisMap);;
        Messenger<Player>.RemoveListener(GameMessages.REASSIGN_PLAYER_AXIS_MAP, OnReassignPlayerAxisMap);
        Messenger<Player>.RemoveListener(GameMessages.RESET_PLAYER, OnResetPlayer);
    }
	
    #endregion

	private List<PlayerSpawnPoint> PlayerSpawnPoints
	{
		get {
			if (_playerSpawnPoints.Count == 0) {
				GameObject[] playerSpawnPointGOs = GameObject.FindGameObjectsWithTag(TagNames.Checkpoint);
				foreach (GameObject spawnPointGO in playerSpawnPointGOs) {
					PlayerSpawnPoint playerSpawnPoint = spawnPointGO.GetComponent<PlayerSpawnPoint>();
					if (playerSpawnPoint) {
						_playerSpawnPoints.Add(playerSpawnPoint);
					}
				}
			}
			
			return _playerSpawnPoints;
		}
	}
	
    private void OnAddPlayer()
    {
        if (InputManager.Instance.IsAxisMapAvailable()) {
			AddPlayerWithAxisMap(InputManager.Instance.GetFirstUnusedAxisMap());
        }
    }
    
    private void OnAddPlayerWithAxisMap(AxisMap axisMapToUse)
    {
        AddPlayerWithAxisMap(axisMapToUse);
    }

    /// <summary>
    /// Adds the player with axis map.
    /// </summary>
    /// <returns>The player id.</returns>
    /// <param name="axisMapToUse">Axis map to use.</param>
    private Player AddPlayerWithAxisMap(AxisMap axisMapToUse)
    {
        GameObject playerObject;
        
        if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected) {
			if(_playerCreationPoint){
				playerObject = (GameObject)Instantiate(playerPrefab, _playerCreationPoint.transform.position, _playerCreationPoint.transform.rotation);
			}else{
				playerObject = (GameObject)Instantiate(playerPrefab, transform.position, transform.rotation);
			} 
        }
        else {
			if(_playerCreationPoint){
				playerObject = (GameObject)uLink.Network.Instantiate(playerPrefab, _playerCreationPoint.transform.position, _playerCreationPoint.transform.rotation, 0);
			}else{
				playerObject = (GameObject)uLink.Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
			}    
        }
        
        if (playerObject != null) {
            Player player = playerObject.GetComponent<Player>();
            player.transform.parent = transform;
            player.SetId(players.Count);
            player.AxisMap = axisMapToUse;
            players.Add(player);
			OnResetPlayer(player);
            return player;
        }

        return null;
    }

    private void OnResetPlayer(Player player)
    {    
        if (player.playerLocation == PlayerLocationEnum.LOCAL) {
			if(PlayerSpawnPoints.Count > 0){
				PlayerSpawnPoint playerSpawnPoint = PlayerSpawnPoints[Random.Range(0, PlayerSpawnPoints.Count)];
				player.transform.position = playerSpawnPoint.transform.position;
				player.transform.rotation = Quaternion.LookRotation(playerSpawnPoint.transform.forward);
			}

            player.Health.Reset();
        }
    }

    private void OnReassignPlayerAxisMap(Player player)
    {
        if (player) {
			player.AxisMap = InputManager.Instance.GetNextUnusedAxisMap(player.AxisMap);
        }
    }
}
