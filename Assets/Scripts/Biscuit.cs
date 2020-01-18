using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Biscuit : MonoBehaviour
{
    [SerializeField] private BiscuitData LaunchData;

    private Plane mLaunchPlane;
    private Vector3 mLastVel;
    private Vector3 mInitialPosition;

    public bool Thrown { get; private set; }

    void Start()
    {
        Thrown = false;
        mInitialPosition = transform.position;
        mLaunchPlane = new Plane(transform.up, transform.position);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnMouseDrag()
    {
        var lastPos = transform.position;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distToPlane;

        mLaunchPlane.Raycast(ray, out distToPlane);
        var planePoint = ray.GetPoint(distToPlane);

        if (Vector3.Distance(planePoint, mInitialPosition) < LaunchData.MaxLaunchDist)
        {
            transform.position = planePoint;
        }
        else
        {
            transform.position = (planePoint - mInitialPosition).normalized * LaunchData.MaxLaunchDist + mInitialPosition;
        }

        mLastVel = transform.position - lastPos;
    }

    private void OnMouseUp()
    {
        Thrown = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = mLastVel * LaunchData.LaunchScale;
    }
}
