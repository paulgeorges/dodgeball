using UnityEngine;
using System.Collections;

public class PlayUSequencerOnMessageReceived : MonoBehaviour {
	private USSequencer _uSequencer;

	public string messageToListenFor;

	private void Awake(){
		_uSequencer = GetComponent<USSequencer>();
	}

	private void OnEnable(){
		Messenger.AddListener(messageToListenFor, OnMessageReceived);
	}

	private void OnDisable(){
		Messenger.RemoveListener(messageToListenFor, OnMessageReceived);
	}

	private void OnMessageReceived(){
		if(_uSequencer){
			_uSequencer.Play();
		}
	}
}
