using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleport : MonoBehaviour
{
    public GameObject leftTeleportRay;
    public GameObject rightTeleportRay;

    public InputActionProperty leftTeleportActivate;
    public InputActionProperty rightTeleportActivate;


    void Update()
    {
        leftTeleportRay.SetActive(leftTeleportActivate.action.ReadValue<Vector2>() != new Vector2(0,0) && (rightTeleportActivate.action.ReadValue<Vector2>() == new Vector2(0, 0)));
        rightTeleportRay.SetActive(rightTeleportActivate.action.ReadValue<Vector2>() != new Vector2(0, 0) && (leftTeleportActivate.action.ReadValue<Vector2>() == new Vector2(0, 0)));
    }
}
