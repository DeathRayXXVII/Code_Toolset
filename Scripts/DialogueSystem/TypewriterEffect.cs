using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float textSpeed = 50f;
    public bool IsRunning { get; private set; }
    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f)
    };
    
    private Coroutine typingCoroutine;
    private TMP_Text textLabel;
    private string text;
    
    public void Run(string text, TMP_Text textLabel)
    {
        if (IsRunning)
            StopCoroutine(typingCoroutine);
        this.text = text;
        this.textLabel = textLabel;
        
        typingCoroutine = StartCoroutine(TypeText());
    }
    
    public void Stop()
    {
        if (!IsRunning) return;

        StopCoroutine(typingCoroutine);
        OnTypingCompleted();
    }

    private IEnumerator TypeText()
    {
        IsRunning = true;

        textLabel.maxVisibleCharacters = 0;
        textLabel.text = text;

        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            t += Time.deltaTime * textSpeed;
            int nextCharIndex = Mathf.Clamp(Mathf.FloorToInt(t), 0, text.Length);

            if (nextCharIndex > charIndex)
            {
                for (int i = charIndex; i < nextCharIndex; i++)
                {
                    textLabel.maxVisibleCharacters = i + 1;

                    if (IsPunctuation(text[i], out float waitTime) && i < text.Length - 1 && !IsPunctuation(text[i + 1], out _))
                    {
                        yield return new WaitForSeconds(waitTime);
                    }
                }
                charIndex = nextCharIndex;
            }

            yield return null;
        }

        OnTypingCompleted();
    }
    private void OnTypingCompleted()
    {
        IsRunning = false;
        textLabel.maxVisibleCharacters = text.Length;
    }
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
