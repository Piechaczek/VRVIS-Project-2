using UnityEngine;

public class Function1 : IGraphableFunction
{

    public float Run(float x, float z){
        return Mathf.Sin(x) * Mathf.Cos(z) - Mathf.Sin(x);
    }

}
