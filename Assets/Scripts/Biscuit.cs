using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Biscuit : MonoBehaviour
{
    [SerializeField] private BiscuitData LaunchData;

    private Plane mLaunchPlane;
    private Vector3 mLastVel;

    public bool Thrown { get; private set; }

    void Start()
    {
        Thrown = false;
        mLaunchPlane = new Plane(transform.up, transform.position);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnMouseDrag()
    {
        var lastPos = transform.position;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distToPlane;

        mLaunchPlane.Raycast(ray, out distToPlane);
        transform.position = ray.GetPoint(distToPlane);
        mLastVel = transform.position - lastPos;
    }

    private void OnMouseUp()
    {
        Thrown = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = mLastVel * LaunchData.LaunchScale;
    }
}
