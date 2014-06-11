using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomUSEditor(typeof(USEnableObjectEvent))]
public class USEnableObjectEventEditor : USEventBaseEditor
{
	public override Rect RenderEvent(Rect myArea)
	{
		USEnableObjectEvent toggleEvent = TargetEvent as USEnableObjectEvent;

		DrawDefaultBox(myArea);

		using(new GUIBeginArea(myArea))
		{
			if (toggleEvent)
			{
				GUILayout.Label(toggleEvent.enable?"Enable : ":"Disable : ", DefaultLabel);
				GUILayout.Label(toggleEvent.AffectedObject.name, DefaultLabel);
			}
		}

		return myArea;
	}
}
