using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    //Needed objects
    Light spotLight;
    public GameObject leftEye;
    public GameObject rightEye;

    public float maxWaitTime;
    public float minWaitTime;
    float currentTime = 0.0f;
    float endTime;

    public float maxFlickerTime;
    public float minFlickerTime;

    bool playFlicker = true;


    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponent<Light>();
        endTime = Random.Range(minWaitTime, maxWaitTime);

        StartCoroutine(Flicker());

    }

    // Update is called once per frame
    void Update()
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

    IEnumerator Flicker ()
    {
        
        while (playFlicker)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            //Activate and deactives both the lights and the eyes
            spotLight.enabled = !spotLight.enabled;

            activeEyes();
        }

    }

    void activeEyes()
    {
        if (spotLight.enabled == false && playFlicker == false)
        {
            leftEye.SetActive(true);
            rightEye.SetActive(true);
        }
        else
        {
            leftEye.SetActive(false);
            rightEye.SetActive(false);
        }
    }
}
