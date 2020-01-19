using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Level
{
    public Camera Camera;
    public Animator Cutscene;
    public float CutsceneLength;
}

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject BiscuitType = null;
    [SerializeField] private Transform LaunchPosition = null;
    [SerializeField] private List<Level> Levels = null;
    [SerializeField] private AudioSource AchievementSound = null;

    public UnityEvent SuccessEvent;
    public UnityEvent FailedEvent;
    public UnityEvent ScoreChangedEvent;
    public UnityEvent BestChangedEvent;

    public int Score { get; private set; }
    public int Best { get; private set; }

    void Start()
    {
        if (PlayerPrefs.HasKey("Best"))
        {
            Best = PlayerPrefs.GetInt("Best");
            BestChangedEvent.Invoke();
        }

        Score = 0;

        StartCoroutine(PlayCutscene(Levels[0]));
    }

    private IEnumerator PlayCutscene(Level level)
    {
        level.Camera.enabled = false;
        level.Cutscene.Play("Cutscene");
        yield return new WaitForSeconds(level.CutsceneLength);
        level.Cutscene.StopPlayback();
        level.Camera.enabled = true;

        PrepareLaunch();
    }

    public void OnSuccess()
    {
        ++Score;
        ScoreChangedEvent.Invoke();
        AchievementSound.Play();

        if (Score > Best)
        {
            Best = Score;
            BestChangedEvent.Invoke();

            PlayerPrefs.SetInt("Best", Best);
            PlayerPrefs.Save();
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
