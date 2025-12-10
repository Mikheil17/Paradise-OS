using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class ButtonInteractionManager : MonoBehaviour
{
    [Header("Canvas")]
    public CanvasGroup canvasGroup;
    [Header("Button Texts")]
    public TMPro.TextMeshProUGUI redButtonText;
    public TMPro.TextMeshProUGUI blueButtonText;

    [Header("Speaker Audio Source")]
    public AudioSource speakerSource;

    [Header("Intro Dialogue")]
    public DialogueBlock introBlock;

    [Header("WYR Block")]
    public WYRBlock wYRBlock;

    private bool introPlayed = false;
    private bool waitingForButton = false;

    private SaveManager saveManager;

    private void Awake()
    {
        saveManager = SaveManager.instance;

        // Hide canvas at start
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;
            
        StartCoroutine(MainInteractionCoroutine());
    }

    private IEnumerator MainInteractionCoroutine()
    {
        // Play intro dialogue
        if (introBlock != null && introBlock.voiceClips != null && introBlock.voiceClips.Length > 0 && !introPlayed)
        {
            introPlayed = true;
            foreach (var clip in introBlock.voiceClips)
            {
                speakerSource.Stop();
                speakerSource.clip = clip;
                speakerSource.Play();
                yield return new WaitWhile(() => speakerSource.isPlaying);
            }
        }

        // Show the canvas after dialogue is finished
        if (canvasGroup != null)
            canvasGroup.alpha = 1f;

        // Start WYR block loop
        while (wYRBlock != null)
        {
            DisplayWYRBlockChoices();

            waitingForButton = true;

            while (waitingForButton == true)
                yield return null;

            // Clear button texts for 2 seconds before showing next block
            if (blueButtonText != null) blueButtonText.text = "";
            if (redButtonText != null) redButtonText.text = "";
            yield return new WaitForSeconds(2f);

            wYRBlock = wYRBlock.nextBlock;
        }

        saveManager.GotoNextScene();
    }

    private void DisplayWYRBlockChoices()
    {
        if (wYRBlock != null)
        {
            if (blueButtonText != null)
                blueButtonText.text = wYRBlock.blueChoice;

            if (redButtonText != null)
                redButtonText.text = wYRBlock.redChoice;
        }
    }

    public void setFalse()
    {
        waitingForButton = false;
    }
}