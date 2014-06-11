using UnityEngine;
using System.Collections;

public class CommonStartup : MonoBehaviour {
	public bool startMusicMuted = false;
	public bool startAmbientMuted = false;
	public bool startSfxMuted = false;
	public bool startVoiceMuted = false;
	public bool disableMasterAudioLogging = true;

	private void Awake () {
		if(!GameObject.FindGameObjectWithTag("GameManagers")){
			StartCoroutine("DoCommonStartup");
		}else{
			Destroy(gameObject);
		}
	}

	private IEnumerator DoCommonStartup() {
		AsyncOperation async = Application.LoadLevelAdditiveAsync("GameManagers");
		yield return async;

		MasterAudio.Instance.disableLogging = disableMasterAudioLogging;

		// HACK: wait a half second for the sounds from the DynamicSoundGroupCreated to get transfered over to MasterAudio
		yield return new WaitForSeconds(0.5f);

		if(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor){
			if(startMusicMuted){
				MasterAudio.MuteBus("Music");
			}
			
			if(startAmbientMuted){
				MasterAudio.MuteBus("Ambient");
			}
			
			if(startSfxMuted){
				MasterAudio.MuteBus("SFX");
			}

			if(startVoiceMuted){
				MasterAudio.MuteBus("Voice");
			}
		}

		Messenger.Invoke(GameMessages.COMMON_STARTUP_COMPLETE);
		Destroy(gameObject);
	}
}
