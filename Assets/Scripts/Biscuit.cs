using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Biscuit : MonoBehaviour
{
    [SerializeField] private BiscuitData LaunchData = null;

    private Plane mLaunchPlane;
    private Vector3 mLastVel;
    private Vector3 mInitialPosition;

    public bool Thrown { get; private set; }
    private Game mGame;

    void Start()
    {
        Thrown = false;
        mInitialPosition = transform.position;
        mGame = GameObject.Find("Game").GetComponent<Game>();
        mLaunchPlane = new Plane(transform.up, transform.position);
        GetComponent<Rigidbody>().isKinematic = true;

        Destroy(gameObject, LaunchData.Timeout);
    }

    private void Update()
    {
        if (Thrown && GetComponent<Rigidbody>().velocity.magnitude < 0.02f)
        {
            mGame.FailedEvent.Invoke();
            Destroy(gameObject);
        }
    }

    public void OnMouseDrag()
    {
        if (Thrown)
            return;

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
        GetComponent<Rigidbody>().angularVelocity = Random.rotation.eulerAngles * Random.Range(0.0f, 0.02f);
    }

    private void OnTriggerEnter()
    {
        mGame.SuccessEvent.Invoke();
        Destroy(gameObject);
    }
}
