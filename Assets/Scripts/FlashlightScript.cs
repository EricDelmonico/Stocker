using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField] 
    private Light flashlightLight;
    [SerializeField] 
    private Light pointLight;

    // Update is called once per frame
    void Update()
    {
        transform.forward += (Camera.main.transform.forward - transform.forward) * Time.deltaTime * moveSpeed;

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightLight.enabled = !flashlightLight.enabled;
            pointLight.enabled = !pointLight.enabled;
        }
    }
}

public static class EaseLerp
{
    public static Vector3 Lerp(Vector3 start, Vector3 end, float t)
    {
        return new Vector3(
            Mathf.SmoothStep(start.x, end.x, t),
            Mathf.SmoothStep(start.y, end.y, t),
            Mathf.SmoothStep(start.z, end.z, t));
    }
}
