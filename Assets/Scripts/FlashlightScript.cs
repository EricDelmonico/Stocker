using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.forward += (Camera.main.transform.forward - transform.forward) * Time.deltaTime * moveSpeed;
        transform.parent.position = Camera.main.transform.position - new Vector3(0, -.2f, 0) + Camera.main.transform.right / 1.6f;
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
