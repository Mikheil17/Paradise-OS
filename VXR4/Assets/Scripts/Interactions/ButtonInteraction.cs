using UnityEngine;
using TMPro;

public class ButtonInteraction : MonoBehaviour
{
    [Header("General Settings")]
    public ButtonInteractionManager interactionManager;

    [Header("Button Text")]
    public TextMeshProUGUI buttonText;

    public void OnButtonPress()
    {
        if (interactionManager != null)
        {
            interactionManager.PlayButtonDialogue(this);
        }
    }
}