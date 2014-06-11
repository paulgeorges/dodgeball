using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialColorChanger : MonoBehaviour {

	private Health hp;
	private bool isRed;
	
	public Color color1;
	public Color color2;
	public Color color3;
	public Material[] glowMaterials;
	public GameObject mainMesh;
	public LensFlare flare;

	public Color flashColor;
	public Material[] bodyMaterials;

	void Awake () {
		hp = this.gameObject.GetComponent<Health>();
		isRed = false;

		ResetColor();
		ResetGlowColor();	
	}

	// For the Flashing effect
	private void OnTakeDamage(DamageInfo damageInfo){
		//Debug.Log("Take Damage");
		if(!isRed){
			for(int i = 0; i<bodyMaterials.Length;i++){
				bodyMaterials[i].color = flashColor;
			}
			isRed = true;
			Invoke("ResetColor",0.05f);
		}
	}

	private void OnSelfDead(){
		//Debug.Log("Loose Life");
		ChangeGlowColor();
		Invoke("ResetColor",0.05f);
	}

	private void ChangeGlowColor(){
		//Debug.Log ("Lives:" + hp.lives);
		Color toSwitch = new Color();

		switch (hp.lives){
			case 1:
				toSwitch = color3;
				break;
			case 2:
				toSwitch = color2;
				break;
			default:
				toSwitch = color1;
				break;
		}

		//iTween.ColorTo(mainMesh,iTween.Hash("color",toSwitch,"namedcolorvalue","_GlowColor","time",4.0f));

		// Upgrade Note: Make it animated with iTween, not working on a single material right now
		for(int i = 0; i<glowMaterials.Length;i++){
			glowMaterials[i].SetColor("_GlowColor",toSwitch);
		}
		// Also make the flare tween color
		flare.color = toSwitch;
	}
	
	private void ResetColor(){
		for(int i = 0; i<bodyMaterials.Length;i++){
			bodyMaterials[i].color = Color.black;
		}
		isRed = false;
	}

	private void ResetGlowColor(){
		for(int i = 0; i<glowMaterials.Length;i++){
			glowMaterials[i].SetColor("_GlowColor",color1);
		}
	}

	//Resets materials colors
	void OnApplicationQuit() {
		ResetGlowColor();
		ResetColor();
	} 


}
