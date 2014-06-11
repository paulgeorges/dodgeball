using UnityEngine;
using System.Collections;

public static class AHGameObjectExtensions
{
	public static void SetLayerRecursively(this GameObject node,  int layer)
	{
		node.layer = layer;
		foreach (Transform child in node.transform) {
			child.gameObject.SetLayerRecursively(layer);
		}
	}
}
