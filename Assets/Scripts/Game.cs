using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject BiscuitType = null;
    [SerializeField] private Transform LaunchPosition = null;

    public UnityEvent SuccessEvent;
    public UnityEvent FailedEvent;
    public UnityEvent ScoreChangedEvent;
    public UnityEvent BestChangedEvent;

    public int Score { get; private set; }
    public int Best { get; private set; }

    void Start()
    {
        Score = 0;
        PrepareLaunch();
    }

    public void OnSuccess()
    {
        ++Score;
        ScoreChangedEvent.Invoke();

        if (Score > Best)
        {
            Best = Score;
            BestChangedEvent.Invoke();
        }

        PrepareLaunch();
    }

    public void OnFailed()
    {
        Score = 0;
        ScoreChangedEvent.Invoke();

        PrepareLaunch();
    }

    void PrepareLaunch()
    {
         Instantiate(BiscuitType, LaunchPosition.position, LaunchPosition.rotation);
    }
}
