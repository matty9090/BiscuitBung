using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Biscuit : MonoBehaviour
{
    [SerializeField] private float DragScale = 0.01f;
    [SerializeField] private float ThrowScale = 4.0f;

    private Vector3 mDelta;
    private Vector3 mLastMousePos;
    private Vector3 mLastVel;

    public bool Thrown { get; private set; }

    void Start()
    {
        Thrown = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        mDelta = Input.mousePosition - mLastMousePos;
        mLastMousePos = Input.mousePosition;
    }

    public void OnMouseDrag()
    {
        mLastVel = DragScale * (transform.forward * mDelta.y + transform.right * mDelta.x);
        transform.position += mLastVel;
    }

    private void OnMouseUp()
    {
        Thrown = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = mLastVel * ThrowScale;
    }
}
