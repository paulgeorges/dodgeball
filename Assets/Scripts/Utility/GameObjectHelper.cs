using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectHelper : MonoBehaviour {
	public static GameObject GetChildWithName (GameObject root, string name)
	{
		Component[] transforms = root.GetComponentsInChildren(typeof(Transform), true);
		
		foreach (Transform transform in transforms) {
			if (transform.gameObject.name == name) {
				return transform.gameObject;
			}
		}
		
		return null;
	}

	public static List<GameObject> GetChildrenWithName (GameObject root, string name)
	{
		List<GameObject> children = new List<GameObject>();

		Component[] transforms = root.GetComponentsInChildren(typeof(Transform), true);
		
		foreach (Transform transform in transforms) {
			if (transform.gameObject.name == name) {
				children.Add(transform.gameObject);
			}
		}
		
		return children;
	}

	public static GameObject GetChildWithPartialName (GameObject root, string name)
	{
		Component[] transforms = root.GetComponentsInChildren(typeof(Transform), true);
		
		foreach (Transform transform in transforms) {
			if (transform.gameObject.name.Contains (name)) {
				return transform.gameObject;
			}
		}
		
		return null;
	}

	public static List<GameObject> GetChildrenWithPartialName (GameObject root, string name)
	{
		List<GameObject> children = new List<GameObject>();

		Component[] transforms = root.GetComponentsInChildren(typeof(Transform), true);
		
		foreach (Transform transform in transforms) {
			if (transform.gameObject.name.Contains (name)) {
				children.Add(transform.gameObject);
			}
		}
		
		return children;
	}
}
