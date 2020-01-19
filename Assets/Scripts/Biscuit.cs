using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Biscuit : MonoBehaviour
{
    [SerializeField] private BiscuitData LaunchData = null;

    private Plane mLaunchPlane;
    private Vector3 mInitialPosition;
    private List<Vector3> mVels;

    public bool Thrown { get; private set; }
    public bool Success = false;
    private Game mGame = null;

    void Start()
    {
        Thrown = false;
        mInitialPosition = transform.position;
        mVels = new List<Vector3>();
        mGame = GameObject.Find("Game").GetComponent<Game>();
        mLaunchPlane = new Plane(transform.up, transform.position);
        GetComponent<Rigidbody>().isKinematic = true;

        Destroy(gameObject, LaunchData.Timeout);
    }

    private void Update()
    {
        if (Thrown && GetComponent<Rigidbody>().velocity.magnitude < 0.002f)
        {
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

        mVels.Add(transform.position - lastPos);

        if (mVels.Count > LaunchData.NumFramesAvg)
            mVels.RemoveAt(0);
    }

    private void OnMouseUp()
    {
        Vector3 avgVel = Vector3.zero;

        foreach (var vel in mVels)
            avgVel += vel;

        avgVel /= mVels.Count;

        Thrown = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = avgVel * LaunchData.LaunchScale;
        GetComponent<Rigidbody>().angularVelocity = Random.rotation.eulerAngles * Random.Range(0.0f, 0.02f);
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter()
    {
        Success = true;
        mGame.SuccessEvent.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (mGame != null && !Success)
            mGame.FailedEvent.Invoke();
    }
}
