using UnityEngine;

public interface IGraphableFunction
{
   
    string Name();
    float Run(float x, float z);
    Vector2[] GetRecommendedDomain();

}
