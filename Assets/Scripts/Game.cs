using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityEvent SuccessEvent;
    public int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SuccessEvent = new UnityEvent();
        SuccessEvent.AddListener(OnSuccess);
    }

    void OnSuccess()
    {

    }
}
