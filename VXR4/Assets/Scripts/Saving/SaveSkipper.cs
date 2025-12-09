using UnityEngine;

public class SaveSkipper : MonoBehaviour
{
    private SaveManager saveManager;

    private void Awake()
    {
        saveManager = SaveManager.instance;
        saveManager.GotoNextScene();
    }
}