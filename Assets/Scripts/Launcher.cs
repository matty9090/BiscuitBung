using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject BiscuitType = null;
    private GameObject Biscuit = null;

    void Start()
    {
        Biscuit = Instantiate(BiscuitType, transform.position, transform.rotation);
    }

    void Update()
    {
        if (Biscuit.GetComponent<Biscuit>().Thrown &&
            Biscuit.GetComponent<Rigidbody>().velocity.magnitude <= 0.001f)
        {
            Debug.Log("Spawning");
            Destroy(Biscuit);
            Biscuit = Instantiate(BiscuitType, transform.position, transform.rotation);
        }
    }
}
