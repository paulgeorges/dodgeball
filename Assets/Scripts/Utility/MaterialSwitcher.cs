using UnityEngine;
using System.Collections;

public class MaterialSwitcher : MonoBehaviour {
    public string switcherID;
	public Material[] materialsToSwitch;
	public Texture[] textures;

	private void OnEnable(){
		Messenger<string, int, string>.AddListener(GameMessages.SWITCH_TEXTURES_FOR_MATERIALS, OnSwitchTexturesForMaterials);
	}

	private void OnDisable(){
		Messenger<string, int, string>.RemoveListener(GameMessages.SWITCH_TEXTURES_FOR_MATERIALS, OnSwitchTexturesForMaterials);
	}

	public void OnSwitchTexturesForMaterials(string propertyName, int textureIndex, string switcherID){
		if(textures.Length > textureIndex && this.switcherID.Equals(switcherID)) {
			foreach(Material material in materialsToSwitch) {
				material.SetTexture(propertyName, textures[textureIndex]);
			}
		}
	}
}
