using UnityEngine;

public static class Utilities
{
   public static float Remap(float x , float inputMax,float outputMax,float inputMin , float outputMin,bool clamp)
    {
        if(clamp) x = Mathf.Clamp(x,inputMin,inputMax);
        return  (x - inputMin) * (outputMax - outputMin)/(inputMax - inputMin) + outputMin;
    }
}
