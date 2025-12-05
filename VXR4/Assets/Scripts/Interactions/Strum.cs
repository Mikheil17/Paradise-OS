using UnityEngine;

public class Strumming : MonoBehaviour
{
    public bool plucked = false;
    public AudioSource note;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finger"))
        {
            if (!plucked)
            {
                plucked = true;
                note.Play();
            }
    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("finger"))
        {
            plucked = false;
        }
    }
}