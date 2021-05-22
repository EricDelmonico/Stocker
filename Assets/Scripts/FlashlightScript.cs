using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private Vector3 initialLocalPosition;
    private Vector3 endLocalPosition;
    private float timeOnUpNormal = 0;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        endLocalPosition = initialLocalPosition + new Vector3(0, 1.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 start = Camera.main.transform.position;
        Vector3 end = Camera.main.transform.position + Camera.main.transform.forward * 100;
        if (Physics.Linecast(start, end, out hit, ~(68)))
        {
            bool holdable = false;
            for (int i = 0; i < PlayerInteract.holdableTags.Length; i++)
            {
                if (hit.collider.tag == PlayerInteract.holdableTags[i])
                {
                    transform.forward = hit.transform.position - transform.position;
                    holdable = true;
                    break;
                }
            }

            if (holdable && hit.normal == new Vector3(0, 1, 0))
            {
                timeOnUpNormal += Time.fixedDeltaTime;
                transform.localPosition = EaseLerp.Lerp(initialLocalPosition, endLocalPosition, timeOnUpNormal);
            }
            else if (timeOnUpNormal <= 0)
            {
                timeOnUpNormal = 0;
                transform.localPosition = EaseLerp.Lerp(initialLocalPosition, endLocalPosition, timeOnUpNormal);
            }
            else
            {
                timeOnUpNormal -= Time.fixedDeltaTime;
                transform.localPosition = EaseLerp.Lerp(initialLocalPosition, endLocalPosition, timeOnUpNormal);
            }
            transform.forward = hit.point - transform.position;
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
