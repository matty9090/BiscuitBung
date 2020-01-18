using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject BiscuitType = null;
    [SerializeField] private Transform LaunchPosition = null;
    private GameObject Biscuit = null;

    public UnityEvent SuccessEvent;
    public UnityEvent FailedEvent;
    public UnityEvent ScoreChangedEvent;
    public UnityEvent BestChangedEvent;

    public int Score { get; private set; }
    public int Best { get; private set; }

    // Start is called before the first frame update
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
        Biscuit = Instantiate(BiscuitType, LaunchPosition.position, LaunchPosition.rotation);
    }
}
