using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem Particles = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Biscuit>() != null)
            Particles.Play();
    }
}
