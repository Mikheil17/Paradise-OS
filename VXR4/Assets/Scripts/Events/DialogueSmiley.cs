using UnityEngine;
using System.Collections;

public class DialogueSmiley : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public GameObject smileyFace;

    private SaveManager saveManager;

    private void Start()
    {

        saveManager = SaveManager.instance;

        if (audioSource != null && audioClips != null && audioClips.Length > 0)
        {
            StartCoroutine(PlayAudioSequence());
        }
    }

    private IEnumerator PlayAudioSequence()
    {
        foreach (var clip in audioClips)
        {

            // check if we are on second clip to activate smiley face
            if(System.Array.IndexOf(audioClips, clip) == 1)
            {
                if(smileyFace != null)
                {
                    smileyFace.SetActive(true);
                }

                audioSource.clip = clip;
                audioSource.Play();

                yield return new WaitWhile(() => audioSource.isPlaying);
                yield return new WaitForSeconds(2f);

                smileyFace.SetActive(false);
            }
            else
            {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitWhile(() => audioSource.isPlaying);
            }
        }

        saveManager.GotoNextScene();
    }
}
