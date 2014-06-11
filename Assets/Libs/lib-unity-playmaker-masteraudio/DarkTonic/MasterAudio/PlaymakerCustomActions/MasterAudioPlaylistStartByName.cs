using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory(ActionCategory.Audio)]
[Tooltip("Start a Playlist by name in Master Audio")]
public class MasterAudioPlaylistStartByName : FsmStateAction {
    [Tooltip("Name of Playlist Controller to use. Not required if you only have one.")]
	public FsmString playlistControllerName;

	[RequiredField]
    [Tooltip("Name of playlist to start")]
	public FsmString playlistName;

	public override void OnEnter() {
		if (string.IsNullOrEmpty(playlistControllerName.Value)) {
			MasterAudio.ChangePlaylistByName(playlistName.Value, true);
		} else {
			MasterAudio.ChangePlaylistByName(playlistControllerName.Value, playlistName.Value, true);
		}
		
		Finish();
	}
	
	public override void Reset() {
		playlistControllerName = new FsmString(string.Empty);
		playlistName = new FsmString(string.Empty);
	}
}
