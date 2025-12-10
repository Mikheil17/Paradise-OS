using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanvasInteractionManager : MonoBehaviour
{
    [Header("Dialogue Audio Source")]
    public AudioSource speakerSource;

    [Header("Song Audio Source")]
    public AudioSource songSource;

    [Header("Intro Dialogue")]
    public DialogueBlock introBlock;

    [Header("Intro Song")]
    public AudioClip introSong;

    [Header("Outro Dialogue")]
    public DialogueBlock outroBlock;

    [Header("Canvases")]
    public GameObject[] canvases;

    [Header("Finish Flags")]
    public bool[] finishes;

    private SaveManager saveManager; 

    private void Awake()
    {
        saveManager = SaveManager.instance;
        StartCoroutine(PlayIntroMusicAndDialogue());
    }

    private IEnumerator PlayIntroMusicAndDialogue()
    {
        // Play intro music
        if (introSong != null && songSource != null)
        {
            songSource.clip = introSong;
            songSource.Play();
        }

        // Wait 1 second after music starts
        yield return new WaitForSeconds(1f);

        // Play intro dialogue
        if (introBlock != null && introBlock.voiceClips != null)
        {
            foreach (AudioClip clip in introBlock.voiceClips)
            {
                if (clip != null && speakerSource != null)
                {
                    speakerSource.clip = clip;
                    speakerSource.Play();
                    yield return new WaitForSeconds(clip.length + 0.5f);
                }
            }
        }

        // Go through canvases, activate each, wait for finishes[index] to be true
        if (canvases != null && finishes != null)
        {
            for (int i = 0; i < canvases.Length && i < finishes.Length; i++)
            {
                if (canvases[i] != null)
                    canvases[i].SetActive(true);

                // Wait until finishes[i] is true
                yield return StartCoroutine(WaitForFinish(i));

                if (canvases[i] != null)
                    canvases[i].SetActive(false);

                if( i < canvases.Length - 1)
                    yield return new WaitForSeconds(2f);
            }
        }

        // After all canvases are done, play outro dialogue
        yield return StartCoroutine(PlayOutroDialogue());
    }

    private IEnumerator PlayOutroDialogue()
    {
        if (outroBlock != null && outroBlock.voiceClips != null)
        {
            foreach (AudioClip clip in outroBlock.voiceClips)
            {
                if (clip != null && speakerSource != null)
                {
                    speakerSource.clip = clip;
                    speakerSource.Play();
                    yield return new WaitForSeconds(clip.length + 0.5f);
                }
            }
        }
        
        // Go to next scene after all outro dialogue has finished
        saveManager.GotoNextScene();
    }

    private IEnumerator WaitForFinish(int index)
    {
        while (!finishes[index])
        {
            yield return null;
        }
    }

    public void setTrue(int index)
    {
        if (index >= 0 && index < finishes.Length)
        {
            finishes[index] = true;
        }
    }
}