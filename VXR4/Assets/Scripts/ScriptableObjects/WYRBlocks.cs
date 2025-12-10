using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// WYR block ScriptableObject
[CreateAssetMenu(menuName = "Custom/WYRBlock")]
public class WYRBlock : ScriptableObject
{
    [Tooltip("Display texts for choices")]
    public string blueChoice;
    public string redChoice;

    public WYRBlock nextBlock;
}