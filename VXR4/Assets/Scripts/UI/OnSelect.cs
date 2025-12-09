using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelect : MonoBehaviour
{
    public CanvasInteractionManager canvasManager;
    public int canvasIndex;

    public void Select()
    {
        if(canvasManager != null)
            canvasManager.setTrue(canvasIndex);
    }
}