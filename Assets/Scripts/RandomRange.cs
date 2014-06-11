using UnityEngine;
using System.Collections;

[System.Serializable]
public class RandomRange {
	public float min;
	public float max;
	public float value;

	public RandomRange(){
		
	}
	
	public RandomRange(float min, float max){
		this.min = min;
		this.max = max;
	}

	public void InitValue(){
		value = Random.Range(min, max);
	}
}
