using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointsParent;
    public GameObject pointPrefab;
    private Dictionary<IntVector2, GameObject> points = new Dictionary<IntVector2, GameObject>();


    private int pointsPerFrame = 25; // amount of points to calculate per frame
    private int currentPointCount = 0;
    private int gridSize = 100;

    private float domainXMin = -10;
    private float domainXMax = 10;
    private float domainYMin = -10;
    private float domainYMax = 10;
    private float domainZMin = -10;
    private float domainZMax = 10;

    private Vector3 worldSpaceGraphOrigin = new Vector3(-0.5f, 0f, -0.5f);
    private Vector3 worldSpaceGraphSize = new Vector3(1f, 1f, 1f);

    float Function(float x, float z) {
        // HARDCODED function for testing
        return Mathf.Sin(x) * Mathf.Cos(z) - Mathf.Sin(x);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pointsPerFrame; i++)
        {
            if (currentPointCount < gridSize * gridSize) {
                // figure out which point on the grid to draw next
                int gridX = currentPointCount % 100;
                int gridZ = Mathf.FloorToInt(currentPointCount / 100);

                // figure out what coords are that in the function's domain
                Vector2 domainCoords = GridToDomainCoords(gridX, gridZ);
                float x = domainCoords.x;
                float z = domainCoords.y;
                float y = Function(x, z);

                // figure out where to place the game object (relative to parent) and how big it should be
                Vector3 worldCoords = DomainToWorldSpaceCoords(x, y, z);
                Vector3 scale = new Vector3(1f / gridSize, 1f / gridSize, 1f / gridSize);

                // create and place the point
                GameObject newPoint = Instantiate(pointPrefab, pointsParent);
                newPoint.transform.localPosition = worldCoords;
                newPoint.transform.localScale = scale;
                points.Add(new IntVector2(gridX, gridZ), newPoint);

                // update point count
                currentPointCount++;
            }
        }

    }

    private Vector2 GridToDomainCoords(int gridX, int gridZ) {
        float xDelta = (domainXMax - domainXMin) / gridSize;
        float zDelta = (domainZMax - domainZMin) / gridSize;
        return new Vector2(domainXMin + gridX * xDelta, domainZMin + gridZ * zDelta);
    }

    private Vector3 DomainToWorldSpaceCoords(float x, float y, float z){
        float worldX = (x - domainXMin) / (domainXMax - domainXMin) * worldSpaceGraphSize.x + worldSpaceGraphOrigin.x;
        float worldY = (y - domainYMin) / (domainYMax - domainYMin) * worldSpaceGraphSize.y + worldSpaceGraphOrigin.y;
        float worldZ = (z - domainZMin) / (domainZMax - domainZMin) * worldSpaceGraphSize.z + worldSpaceGraphOrigin.z;
        return new Vector3(worldX, worldY, worldZ);
    }

    public void OnTargetFound() {
        // show graph
        pointsParent.gameObject.SetActive(true);
    }

    public void OnTargetLost() {
        // hide graph
        pointsParent.gameObject.SetActive(false);
    }

    // Custom class to be used as key
    private class IntVector2 {

        public int x;
        public int y;

        public IntVector2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;

            IntVector2 cast = obj as IntVector2;
            return cast.x == this.x && cast.y == this.y;
        }

        public override int GetHashCode() {
            return System.HashCode.Combine(x, y);
        }

    }

}
