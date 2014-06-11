using UnityEngine;
using System.Collections;

public class PlayMakerHelper : MonoBehaviour {
	private PlayMakerFSM _playMakerFSM;

	private void Awake(){
		_playMakerFSM = GetComponent<PlayMakerFSM>();
	}

	private void OnEnable(){
		Messenger<string>.AddListener(GameMessages.SEND_FLOW_EVENT, OnSendFlowEvent);
		Messenger.AddListener(GameMessages.FORWARD, OnForward);
		Messenger.AddListener(GameMessages.BACK, OnBack);
	}
	
	private void OnDisable(){
		Messenger<string>.RemoveListener(GameMessages.SEND_FLOW_EVENT, OnSendFlowEvent);
		Messenger.RemoveListener(GameMessages.FORWARD, OnForward);
		Messenger.RemoveListener(GameMessages.BACK, OnBack);
	}

	private void OnSendFlowEvent(string eventName){
		_playMakerFSM.SendEvent(eventName);
	}

	private void OnForward(){
		_playMakerFSM.SendEvent(GameMessages.FORWARD);
	}

	private void OnBack(){
		_playMakerFSM.SendEvent(GameMessages.BACK);
	}
}
