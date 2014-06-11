using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uSequencer")]
	[Tooltip("Plays a uSequencer sequence from a specified time")]
	public class PlaySequenceFromTime : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(USSequencer))]
		[Tooltip("This is the sequence you would like to Play.")]
		public FsmOwnerDefault sequenceToPlay;
		
		[Tooltip("Event to send when the sequence has started Playing.")]
		public FsmEvent startedPlayback;
		
		[Tooltip("Event to send when the sequence is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
		public FsmEvent finishEvent;
		
		[RequiredField]
		[Tooltip("This is the time you would like the sequence to start at.")]
		public FsmFloat timeToStart;
		
		public override void Reset()
		{
			timeToStart = 0.0f;
		}
		
		public override void OnEnter()
		{
			if(Fsm == null)
				return;
			
			GameObject go = Fsm.GetOwnerDefaultTarget(sequenceToPlay);
			if(!go)
				return;
				
			USSequencer sequence = go.GetComponent<USSequencer>();
			if(!go)
				return;
			
			sequence.PlaybackFinished += OnSequenceFinished;
			sequence.RunningTime = timeToStart.Value;
			sequence.Play();
			
			Fsm.Event(startedPlayback);
		}
		
		private void OnSequenceFinished(USSequencer sequence)
		{
			Fsm.Event(finishEvent);
			Finish();
		}
	}
}