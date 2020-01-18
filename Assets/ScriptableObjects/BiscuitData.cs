using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BiscuitData", order = 1)]
public class BiscuitData : ScriptableObject
{
    [SerializeField] private float DragScaleDesktop = 0.01f;
    [SerializeField] private float LaunchScaleDesktop = 20.0f;
    
    [SerializeField] private float DragScaleApp = 0.001f;
    [SerializeField] private float LaunchScaleApp = 34.0f;

    [SerializeField] private bool UseDesktop = true;

    public float DragScale {
        get {
            return UseDesktop ? DragScaleDesktop : DragScaleApp;
        }
    }

    public float LaunchScale {
        get {
            return UseDesktop ? LaunchScaleDesktop : LaunchScaleApp;
        }
    }
}
