using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStealth : MonoBehaviour
{
    public static bool running = false;
    public static bool walking = false;

    [SerializeField]
    private GameObject playerCam;
    [SerializeField]
    private GameObject detectionSphere;

    [SerializeField]
    private float crouchSpeed;

    private float currentCrouchLerpTime;
    private float CrouchLerpClamped
    {
        get => currentCrouchLerpTime;
        set
        {
            if (value > 1)
            {
                currentCrouchLerpTime = 1;
            }
            else if (value < 0)
            {
                currentCrouchLerpTime = 0;
            }
            else
            {
                currentCrouchLerpTime = value;
            }
        }
    }

    // "ds" for detection sphere
    [SerializeField]
    private Vector3 lowestdsPos;
    private Vector3 initialdsPos;

    [SerializeField]
    private Transform flashlightParent;

    private Vector3 lowestCamPos;
    private Vector3 initialCamPos;

    [SerializeField]
    private FirstPersonController fpc;

    private float initialWalkSpeed;
    private float initialRunSpeed;

    [SerializeField]
    private float crouchSpeedDivideAmount = 2;

    private void Start()
    {
        initialdsPos = detectionSphere.transform.localPosition;

        Vector3 amountToFall = lowestdsPos - initialdsPos;

        initialCamPos = playerCam.transform.localPosition;
        lowestCamPos = initialCamPos + amountToFall;

        initialRunSpeed = fpc.m_RunSpeed;
        initialWalkSpeed = fpc.m_WalkSpeed;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else
        {
            Stand();
        }

        if ((Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) 
            && !Input.GetKey(KeyCode.LeftControl))
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
        }
        else
        {
            running = false;
        }

        detectionSphere.transform.localPosition = EaseLerp.Lerp(initialdsPos, lowestdsPos, CrouchLerpClamped);
        playerCam.transform.localPosition = EaseLerp.Lerp(initialCamPos, lowestCamPos, CrouchLerpClamped);
        flashlightParent.position = Camera.main.transform.position - new Vector3(0, -.2f, 0) + Camera.main.transform.right / 1.6f;
    }

    private void Crouch()
    {
        CrouchLerpClamped += Time.deltaTime * crouchSpeed;
        fpc.m_WalkSpeed = initialWalkSpeed / crouchSpeedDivideAmount;
        fpc.m_RunSpeed = fpc.m_WalkSpeed;
    }

    private void Stand()
    {
        CrouchLerpClamped -= Time.deltaTime * crouchSpeed;
        fpc.m_RunSpeed = initialRunSpeed;
        fpc.m_WalkSpeed = initialWalkSpeed;
    }
}
