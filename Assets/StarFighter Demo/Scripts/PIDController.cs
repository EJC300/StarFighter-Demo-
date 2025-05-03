using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PIDController
{
    public float integralGain;
    public float derivativeGain;
    public float proportinalGain;
    private float storedIntegral;
    private float p, i, d;

    public PIDController(float p, float i, float d)
    {
        this.proportinalGain = p;
        this.integralGain = i;
        this.derivativeGain = d;
    }

    private float previousError;


    public float result(float dt, float measuredPoint)
    {
        p = measuredPoint;
        storedIntegral += (p * dt);
        i = Mathf.Clamp(integralGain * storedIntegral, -10f, 10f);
        i += p * dt;
        d = (measuredPoint - previousError) / dt;

        previousError = measuredPoint;

        float value = Mathf.Clamp(p * proportinalGain + i * integralGain + d * derivativeGain, -100, 100);

        return value;
    }

}