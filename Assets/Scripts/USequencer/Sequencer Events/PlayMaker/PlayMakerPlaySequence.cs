using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uSequencer")]
	[Tooltip("Plays a uSequencer sequence")]
	public class PlaySequence : FsmStateAction
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
		[Tooltip("Use this flag if you'd like to start the sequence from the beginning.")]
		public FsmBool startFromBeginning = false;
		
		public override void Reset()
		{
			startFromBeginning = false;
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
			
			if(startFromBeginning.Value)
				sequence.Stop();
			
			sequence.PlaybackFinished += OnSequenceFinished;
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