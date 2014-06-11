using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {
	public float autoDestroyTime = 1;
	public float autoDestroyWhenOffscreenTime = 1;
	public bool offScreen = false;

	private float _totalAccumulatedTime = 0;
	private float _offscreenAccumulatedTime = 0;

	private Camera _camera;

	private void Awake(){
		GameObject cameraGO = GameObject.FindGameObjectWithTag("MainCamera");
		if(cameraGO){
			_camera = cameraGO.camera;
		}
	}

	public void Reset(){
		_totalAccumulatedTime = 0;
		_offscreenAccumulatedTime = 0;
	}

	private void Update(){
		if(_camera){
			Vector3 positionOnScreen = _camera.WorldToScreenPoint(transform.position);

			if(autoDestroyWhenOffscreenTime != -1){
				if(!offScreen && (positionOnScreen.x < 0 || positionOnScreen.x >  Screen.width || positionOnScreen.y < 0 || positionOnScreen.y > Screen.height)){
					_offscreenAccumulatedTime = 0;
					offScreen = true;
				}else if(offScreen && positionOnScreen.x >= 0 && positionOnScreen.x <= Screen.width && positionOnScreen.y >= 0 && positionOnScreen.y <= Screen.height){
					offScreen = false;
				}
				
				if(offScreen){
					_offscreenAccumulatedTime += Time.deltaTime;
					if(_offscreenAccumulatedTime >= autoDestroyWhenOffscreenTime){
						DoDestroy();
					}
				}
			}

			_totalAccumulatedTime += Time.deltaTime;
			
			if(_totalAccumulatedTime >= autoDestroyTime){
				DoDestroy();
			}
		}
	}

	virtual protected void DoDestroy(){
		Destroy(gameObject);
	}
}
