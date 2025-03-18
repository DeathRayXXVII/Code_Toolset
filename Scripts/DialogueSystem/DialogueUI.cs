using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Scripts.UnityActions;
using UI.DialogueSystem;
using UnityEngine.Events;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameAction action;
    [SerializeField] private GameObject dialogueBox;
    public TMP_Text textLabel;
    [SerializeField] private InputActionReference inputAction;
    [SerializeField, SteppedRange(0, 25, 0.1f)] private float autoCloseDelay = 5f;
    private const float elementDelay = 3f;
    [SerializeField] private bool autoClose;
    [SerializeField] private UnityEvent OnOpenDialogue;
    [SerializeField] private UnityEvent OnTypingFinish;
    [SerializeField] private UnityEvent OnFinalDialogueTypingFinish;
    [SerializeField] private UnityEvent OnWaitingForResponse;
    [SerializeField] private UnityEvent OnWaitingForExternalAction;
    [SerializeField] private UnityEvent OnCloseDialogue;
    public DialogueData dialogueData;
    
    private Coroutine _dialogueCoroutine;
    private Coroutine _waitCoroutine;
    private WaitForFixedUpdate _waitFixedUpdate = new();

    public bool _waitForExternalAction;
    public bool waitForExternalAction
    {
        private get => _waitForExternalAction;
        set => _waitForExternalAction = value;
    }
    
    public bool IsOpen { get; private set;}
    
    public TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    
    private void Start()
    {
        typewriterEffect ??= GetComponent<TypewriterEffect>();
        responseHandler ??= GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }
    
    public void SetAutoClose(bool state) => autoClose = state;
    
    public void ShowDialogue(DialogueData dialogueObj)
    {
        if (dialogueObj.locked) return;
        dialogueObj.Activated();
        
        if (IsOpen && _dialogueCoroutine != null)
        {
            if (dialogueData == dialogueObj) return;
            CloseDialogueBox(dialogueData);
            dialogueData.Activated();
        }
        
        dialogueBox.SetActive(true);
        dialogueObj.FirstDialogueEvent(action);
        
        _dialogueCoroutine ??= StartCoroutine(StepThroughDialogue(dialogueObj));
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents) => responseHandler.AddResponseEvents(responseEvents);
    
    private IEnumerator StepThroughDialogue(DialogueData dialogueObj)
    {
        IsOpen = true;
        dialogueData = dialogueObj;
        
        var formattedDialogueArray = dialogueObj.Dialogue;
        var lastDialogueIndex = dialogueObj.lastDialogueIndex;

        for (int i = 0; i < dialogueObj.length; i++)
        {
            OnOpenDialogue?.Invoke();
            var dialogue = formattedDialogueArray[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            
            if (i == lastDialogueIndex) break;
            yield return null;
            
            _waitCoroutine = StartCoroutine(WaitForDelay(elementDelay));
            yield return new WaitUntil(() => _waitCoroutine == null);
        }
        yield return _waitFixedUpdate;
        
        if (dialogueObj.hasResponses && dialogueObj.Responses.Length > 0)
        {
            OnWaitingForResponse?.Invoke();
            responseHandler.ShowResponses(dialogueObj.Responses);
            yield break;
        }
        
        OnFinalDialogueTypingFinish.Invoke();

        if (autoClose)
        {
            _waitCoroutine = StartCoroutine(WaitForDelay(autoCloseDelay));
            yield return new WaitUntil(() => _waitCoroutine == null);
        }
        else if (waitForExternalAction)
        {
            OnWaitingForExternalAction?.Invoke();
            yield return new WaitUntil(() => waitForExternalAction == false);
        }
        else
        {
            _waitCoroutine = StartCoroutine(WaitForInput());
            yield return new WaitUntil(() => _waitCoroutine == null);
        }
        
        CloseDialogueBox(dialogueObj);
    }
    
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);
        yield return WaitLoop(() => typewriterEffect.IsRunning, () => typewriterEffect.Stop());
        
        OnTypingFinish.Invoke();
    }
    
    private IEnumerator WaitForDelay(float delay)
    {
        float time = Time.time;
        
        yield return WaitLoop(() => Time.time - time < delay, () => { });
        _waitCoroutine = null;
    }
    
    private IEnumerator WaitLoop(System.Func<bool> condition, System.Action actionOnInput, bool breakOnInput = true)
    {
        while (condition())
        {
            yield return null;
            if (!inputAction.action.triggered) continue;
            actionOnInput?.Invoke();
            if (breakOnInput) break;
        }
        yield return _waitFixedUpdate;
    }
    
    private IEnumerator WaitForInput()
    {
        yield return new WaitUntil(() => inputAction.action.triggered);
        _waitCoroutine = null;
    }
    
    private bool _closing, _closingBypass;

    public void CloseDialogueBox()
    {
        if (_closing && !_closingBypass) return;
        _closingBypass = false;
        _closing = true;
        
        if (_dialogueCoroutine != null)
        {
            StopCoroutine(_dialogueCoroutine);
            _dialogueCoroutine = null;
        }
        
        if (_waitCoroutine != null)
        {
            StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
        }
        
        if (dialogueBox == null || !dialogueBox.activeSelf)
        {
            _closing = false;
            return;
        }

        IsOpen = false;
        dialogueBox?.SetActive(false);
        textLabel.text = string.Empty;
        _closing = false;
    }
    
    public void CloseDialogueBox(DialogueData dialogueObj)
    {
        if (_closing) return;
        _closing = true;
        _closingBypass = true;
        CloseDialogueBox();
        dialogueObj?.LastDialogueEvent(action);
    }
    
    private void StopActiveCoroutines()
    {
        if (_dialogueCoroutine != null)
        {
            StopCoroutine(_dialogueCoroutine);
            _dialogueCoroutine = null;
        }
        
        if (_waitCoroutine != null)
        {
            StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
        }
        
        waitForExternalAction = false;
    }
    
    public void OnEnable() => inputAction?.action.Enable();
    public void OnDisable() 
    {
        inputAction?.action.Disable();
        StopActiveCoroutines();
    }

    public void OnDestroy()
    {
        inputAction?.action.Disable();
        StopActiveCoroutines();
    }
}
