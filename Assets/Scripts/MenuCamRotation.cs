using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamRotation : MonoBehaviour
{
    [SerializeField] private float Speed = 1.0f;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0.0f, Speed * Time.deltaTime, 0.0f);
    }
}
