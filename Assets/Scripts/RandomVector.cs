using UnityEngine;
using System.Collections;

[System.Serializable]
public class RandomVector {
	public Vector3 min;
	public Vector3 max;
	public Vector3 value;

	public RandomVector(){

	}

	public RandomVector(Vector3 min, Vector3 max){
		this.min = min;
		this.max = max;
	}

	public void InitValue(){
		value = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	}
}
