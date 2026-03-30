using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsFollow : MonoBehaviour
{
    public Transform targetPos;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = (targetPos.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = targetPos.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceDeg = angleInDegree * rotationAxis;

        rigid.angularVelocity = (rotationDifferenceDeg * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
