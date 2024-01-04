using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public MeshFilter meshHolder;
    public GameObject pointPrefab;

    private int pointsPerFrame = 25; // amount of points to calculate per frame
    private int currentPointCount = 0;
    private int currentTriangleCount = 0;
    private int gridSize = 100;

    private float domainXMin = -10;
    private float domainXMax = 10;
    private float domainYMin = -10;
    private float domainYMax = 10;
    private float domainZMin = -10;
    private float domainZMax = 10;

    private Vector3 worldSpaceGraphOrigin = new Vector3(-0.5f, 0f, -0.5f);
    private Vector3 worldSpaceGraphSize = new Vector3(1f, 1f, 1f);

    private Mesh mesh;
    private Vector3[] meshVertices;
    private int[] meshTriangles;

    void Start() {
        // Set up the empty mesh
        mesh = new Mesh();
        meshHolder.mesh = mesh;

        meshVertices = new Vector3[2 * gridSize * gridSize];
        meshTriangles = new int[(gridSize * gridSize - (gridSize + gridSize - 1)) * 12];
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
    }

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
                Vector3 meshCoords = DomainToWorldSpaceCoords(x, y, z);

                // add vertex and triangles to mesh
                meshVertices[2 * currentPointCount] = meshCoords;     // front-facing vertex
                meshVertices[2 * currentPointCount + 1] = meshCoords; // back-facing vertex
                if (gridX != 0 && gridZ != 0) {
                    // front-facing triangles
                    // first triangle
                    meshTriangles[currentTriangleCount * 3] = 2 * currentPointCount;  
                    meshTriangles[currentTriangleCount * 3 + 1] = 2 * currentPointCount - 2 * 100;  
                    meshTriangles[currentTriangleCount * 3 + 2] = 2 * currentPointCount - 2 * 101;
                    currentTriangleCount++;
                    // second triangle
                    meshTriangles[currentTriangleCount * 3] = 2 * currentPointCount;  
                    meshTriangles[currentTriangleCount * 3 + 1] = 2 * currentPointCount - 2 * 101;  
                    meshTriangles[currentTriangleCount * 3 + 2] = 2 * currentPointCount - 2 * 1;
                    currentTriangleCount++;
                    // back-facing triangles
                    // first triangle
                    meshTriangles[currentTriangleCount * 3] = 2 * currentPointCount + 1;  
                    meshTriangles[currentTriangleCount * 3 + 1] = 2 * currentPointCount - 2 * 101 + 1;  
                    meshTriangles[currentTriangleCount * 3 + 2] = 2 * currentPointCount - 2 * 100 + 1;
                    currentTriangleCount++;
                    // second triangle
                    meshTriangles[currentTriangleCount * 3] = 2 * currentPointCount + 1;  
                    meshTriangles[currentTriangleCount * 3 + 1] = 2 * currentPointCount - 2 * 1 + 1;  
                    meshTriangles[currentTriangleCount * 3 + 2] = 2 * currentPointCount - 2 * 101 + 1;
                    currentTriangleCount++;

                    mesh.vertices = meshVertices;
                    mesh.triangles = meshTriangles;
                    mesh.RecalculateNormals();
                }

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
        meshHolder.gameObject.SetActive(true);
    }

    public void OnTargetLost() {
        // hide graph
        meshHolder.gameObject.SetActive(false);
    }

}
