// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Color of the screen overlay")]
	public class ColorScreenOverlay : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Color to use for overlay")]
		public FsmColor color;
		
		public override void Reset()
		{
			color = Color.black;
		}
		
		public override void OnGUI()
		{
			var guiColor = GUI.color;
			GUI.color = color.Value;
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), ActionHelpers.WhiteTexture);
			GUI.color = guiColor;
		}
	}
}