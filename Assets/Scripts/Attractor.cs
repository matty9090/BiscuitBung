using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private AnimationCurve Curve = null;
    [SerializeField] private float RadiusMin = 3.0f;
    [SerializeField] private float RadiusMax = 5.0f;
    [SerializeField] private float Force = 10.0f;

    void Update()
    {
        var biscuit = FindObjectOfType<Biscuit>();

        if (biscuit != null)
        {
            var dist = Vector3.Distance(transform.position, biscuit.transform.position);

            if (dist < RadiusMax && dist > RadiusMin)
            {
                //var scale = Curve.Evaluate(dist / RadiusMax);
                var v = (transform.position - biscuit.transform.position).normalized;
                biscuit.GetComponent<Rigidbody>().AddForce(v * Force);
            }
        }
    }
}
