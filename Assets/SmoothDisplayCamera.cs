using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDisplayCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Range(0, 1)]
    [Tooltip("Smaller value means more smoothing")]
    [SerializeField] private float positionDamp = .5f;

    [Range(0, 1)]
    [Tooltip("Smaller value means more smoothing")]
    [SerializeField] private float rotationDamp = .5f;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (target == null) target = FindObjectOfType<AudioListener>().transform;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, positionDamp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamp);
    }
}
