using System.Collections.Generic;
using Scripts.UnityActions;
using UnityEngine;
using UnityEngine.Events;
using ZPTools.Utility;

namespace UI.DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogue/DialogueData")]
    public class DialogueData : ScriptableObject
    {
        public bool playOnlyOncePerGame;
        [SerializeField] private bool _locked;
        
        public void ResetToNewGameValues(int tier = 2)
        {
            if (tier < 2) return;
            _locked = false;
        }

        public bool locked
        {
            get => playOnlyOncePerGame && _locked;
            set
            {
                if (!value) _firstDialogueEventTriggered = _lastDialogueEventTriggered = false;
                _locked = playOnlyOncePerGame && value;
            }
        }
        
        public void SetLocked(bool lockState)
        {
            if (lockState) playOnlyOncePerGame = true;
            locked = lockState;
        }
        
        // Will only be true if playOnlyOncePerGame is true
        public void Activated()
        {
            locked = true;
        }
        
        [Header("Dialogue Data")]
        [SerializeField] private GameAction firstAction, lastAction;
        
        [SerializeField] private StringFactory[] dialogue;
        [SerializeField] private Response[] responses;
        [SerializeField] private UnityEvent firstTrigger, lastTrigger;
        
        
        
        public string[] Dialogue
        {
            get
            {
                string[] dialogueStrings = new string[dialogue.Length];
                for (int i = 0; i < dialogue.Length; i++)
                {
                    dialogue[i].debugContext = this;
                    dialogueStrings[i] = dialogue[i].formattedString;
                }
                return dialogueStrings;
            }
        }
        
        public int length => dialogue.Length;
        public int lastDialogueIndex => length - 1;
        
        public bool hasResponses => responses is { Length: > 0 };
        public Response[] Responses => responses;

        // private void OnEnable()
        // {
        //     if (firstAction != null) firstAction.RaiseEvent += FirstDialogueEvent;
        //     if (lastAction != null) lastAction.RaiseEvent += LastDialogueEvent;
        // }
        //
        // private void OnDisable()
        // {
        //     if (firstAction != null) firstAction.RaiseEvent -= FirstDialogueEvent;
        //     if (lastAction != null) lastAction.RaiseEvent -= LastDialogueEvent;
        // }
        
        
        private bool _firstDialogueEventTriggered;
        public void FirstDialogueEvent() => FirstDialogueEvent(null);
        public void FirstDialogueEvent(GameAction _)
        {
            if (_firstDialogueEventTriggered && locked) return;
            _firstDialogueEventTriggered = true;
            firstTrigger?.Invoke();
        }
        
        private bool _lastDialogueEventTriggered;
        public void LastDialogueEvent() => LastDialogueEvent(null);
        public void LastDialogueEvent(GameAction _)
        {
            if (_lastDialogueEventTriggered && locked) return;
            _lastDialogueEventTriggered = true;
            lastTrigger?.Invoke();
        }
        
        public List<(System.Action, string)> GetButtonActions()
        {
            return new List<(System.Action, string)> { (() => {_locked = !_locked;}, "Toggle Locked State") };
        }
    }
}