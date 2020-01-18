using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    [SerializeField] private Game Game = null;
    [SerializeField] private ParticleSystem Particles = null;

    void OnTriggerEnter()
    {
        Game.SuccessEvent.Invoke();
        Particles.Play();
    }
}
