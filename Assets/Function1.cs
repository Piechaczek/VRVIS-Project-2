using UnityEngine;

public class Function1 : IGraphableFunction
{

    public string Name() {
        return "f(x, y) = sin(x)cos(y) - sin(x)";
    }

    public float Run(float x, float z){
        return Mathf.Sin(x) * Mathf.Cos(z) - Mathf.Sin(x);
    }

    public Vector2[] GetRecommendedDomain() {
        Vector2[] results = new Vector2[3];
        results[0] = new Vector2(-10f, 10f);
        results[1] = new Vector2(-10f, 10f);
        results[2] = new Vector2(-10f, 10f);
        return results;
    }

}
