using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    //Needed objects
    protected Light spotLight;

    public float maxWaitTime;
    public float minWaitTime;
    protected float currentTime = 0.0f;
    protected float endTime;

    public float maxFlickerTime;
    public float minFlickerTime;

    protected bool playFlicker = true;


    // Start is called before the first frame update
    protected void Start()
    {
        spotLight = GetComponent<Light>();
        endTime = Random.Range(minWaitTime, maxWaitTime);

        StartCoroutine(Flicker());

    }

    // Update is called once per frame
    protected virtual void Update()
    {

        currentTime += Time.deltaTime;

        if (currentTime >= endTime)
        {
            currentTime = 0.0f;
            endTime = Random.Range(minWaitTime, maxWaitTime);
            playFlicker = !playFlicker;

            if (playFlicker)
                StartCoroutine(Flicker());
        }





    }

    protected virtual IEnumerator Flicker()
    {

        while (playFlicker)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            //Activate and deactives both the lights and the eyes
            spotLight.enabled = !spotLight.enabled;
        }

    }

}
