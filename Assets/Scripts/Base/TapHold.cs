using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHold : MonoBehaviour
{
    private float startTime;
    private Vector3 mouseStartPos;
    private float power;
    private bool isPlayerTouch;
    private GameObject sightObject;
    Rigidbody rb;

    Vector3 grapplePoint;
    SpringJoint joint;
    LineRenderer lr;
    [SerializeField] LayerMask lm;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sightObject = transform.GetChild(0).gameObject;
        lr = sightObject.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            mouseStartPos = Input.mousePosition;
            isPlayerTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPlayerTouch = false;
            if (Time.time - startTime < 0.15f)
            {
                Tap();
            }
            else
            {
                Hold();
            }
        }

        if (Time.time - startTime > 0.15f && isPlayerTouch) PlayerUpdate();
    }

    void Tap()
    {
        rb.AddForce(new Vector3(0, 1 * 500f, 0));
    }

    void Hold()
    {
        StopGrapple();
        StartGrapple();
        sightObject.SetActive(false);
        Time.timeScale = 1f;
    }
    private void LateUpdate()
    {
        DrawRope();
    }

    void PlayerUpdate()
    {
        Time.timeScale = 0.3f;
        sightObject.SetActive(true);
        Vector3 mouseFinishPos = mouseStartPos - Input.mousePosition;
        power = mouseFinishPos.x / 10;
        Quaternion tempSight = sightObject.transform.rotation;
        sightObject.transform.rotation = Quaternion.Euler(tempSight.x, tempSight.y, tempSight.z - (power));
        //Debug.Log(power);
    }

    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(sightObject.transform.position, sightObject.transform.up, out hit, 20f, lm))
        {
            //Debug.Log(hit.transform.position);
            Debug.Log(hit.point);
            Vector3 difTarget = hit.transform.position;
            Vector3 difHit = hit.point;

           
           // Debug.Log(difHit);
            hit.point = difHit;

            grapplePoint = hit.point;
            joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint /2 ;
            joint.minDistance = distanceFromPoint -(distanceFromPoint - 0.01f);

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            

            lr.positionCount = 2;
        }
    }

    private void DrawRope()
    {
        if (!joint) return;
        Vector3 startPoint = sightObject.transform.position;
        startPoint.z = 0;
        Vector3 finishPoint = grapplePoint;
        finishPoint.z = 0;
        lr.SetPosition(0, startPoint);
        lr.SetPosition(1, finishPoint);
    }

    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
