using UnityEngine;

public class Function3 : IGraphableFunction
{

    public string Name() {
        return "f(x, y) = 1 / xy";
    }

    public float Run(float x, float z) {
        return 1 / x*z;
    }

    public Vector2[] GetRecommendedDomain() {
        Vector2[] results = new Vector2[3];
        results[0] = new Vector2(-5f, 5f);
        results[1] = new Vector2(-5f, 5f);
        results[2] = new Vector2(-5f, 5f);
        return results;
    }

}
