using UnityEngine;

public class Function2 : IGraphableFunction
{

    public string Name() {
        return "f(x, y) = exp(-x^2 / 2) * exp(-y^2 / 2)";
    }

    public float Run(float x, float z) {
        return Mathf.Exp(-(x*x) / 2f) * Mathf.Exp(-(z*z) / 2f);
    }

    public Vector2[] GetRecommendedDomain() {
        Vector2[] results = new Vector2[3];
        results[0] = new Vector2(-5f, 5f);
        results[1] = new Vector2(-0.1f, 1.1f);
        results[2] = new Vector2(-5f, 5f);
        return results;
    }

}
