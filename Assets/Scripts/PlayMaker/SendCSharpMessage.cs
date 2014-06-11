// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Sends a CSharpMessenger message")]
	public class SendCSharpMessage : FsmStateAction
	{
        [RequiredField]
		public FunctionCall functionCall;

		public override void Reset()
		{
			functionCall = null;
		}

		public override void OnEnter()
		{
			DoSendMessage();
			Finish();
		}

		void DoSendMessage()
		{
			switch (functionCall.ParameterType)
			{
				case "None":
					Messenger.Invoke(functionCall.FunctionName);
					break;

				case "bool":
					Messenger<bool>.Invoke(functionCall.FunctionName, functionCall.BoolParameter.Value);
					break;

				case "int":
					Messenger<int>.Invoke(functionCall.FunctionName, functionCall.IntParameter.Value);
					break;

				case "float":
					Messenger<float>.Invoke(functionCall.FunctionName, functionCall.FloatParameter.Value);
					break;

				case "string":
					Messenger<string>.Invoke(functionCall.FunctionName, functionCall.StringParameter.Value);
					break;

                case "Vector2":
					Messenger<Vector2>.Invoke(functionCall.FunctionName, functionCall.Vector2Parameter.Value);
                    break;

				case "Vector3":
					Messenger<Vector3>.Invoke(functionCall.FunctionName, functionCall.Vector3Parameter.Value);
					break;

				case "Rect":
					Messenger<Rect>.Invoke(functionCall.FunctionName, functionCall.RectParamater.Value);
					break;

				case "GameObject":
					Messenger<GameObject>.Invoke(functionCall.FunctionName, functionCall.GameObjectParameter.Value);
					break;

				case "Material":
					Messenger<Material>.Invoke(functionCall.FunctionName, functionCall.MaterialParameter.Value);
					break;

				case "Texture":
					Messenger<Texture>.Invoke(functionCall.FunctionName, functionCall.TextureParameter.Value);
					break;

                case "Color":
					Messenger<Color>.Invoke(functionCall.FunctionName, functionCall.ColorParameter.Value);
                    break;

				case "Quaternion":
					Messenger<Quaternion>.Invoke(functionCall.FunctionName, functionCall.QuaternionParameter.Value);
					break;

				case "Object":
					Messenger<UnityEngine.Object>.Invoke(functionCall.FunctionName, functionCall.ObjectParameter.Value);
					break;
			}
		}
	}
}