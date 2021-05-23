using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerEyes : FlickerLights
{
    //Needed objects
    public GameObject leftEye;
    public GameObject rightEye;


    // Start is called before the first frame update

    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();       
    }

    protected override IEnumerator Flicker ()
    {
        
        while (playFlicker)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            //Activate and deactives both the lights and the eyes
            spotLight.enabled = !spotLight.enabled;

            activeEyes();
        }

    }

    public void activeEyes()
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
