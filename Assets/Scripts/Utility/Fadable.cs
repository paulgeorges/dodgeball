using UnityEngine;
using System.Collections;

public class Fadable : MonoBehaviour {

	public Shader shader1;
	public Shader shader2;

	private void Awake()
	{
		if(shader1 == null){
			shader1 = Shader.Find("Hibernum/BRDF/Add/DNR");
		}
		if(shader2 == null){
			shader2 = Shader.Find("Hibernum/Alpha/DNR");
		}
	}

}
