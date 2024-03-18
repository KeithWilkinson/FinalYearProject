using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object "GridOrigin" is the point from which the grid is generated from
public class Grid : MonoBehaviour
{
    public static int rows = 200;
    public static int columns = 200;
    public GameObject gridObject;
    public GameObject testBuilding;
    public GameObject testRoad;
    private int[,] cells = new int[rows, columns];
    public Material[] buildingMat;
    public GameObject[] modelArray;
    private int _buildingCount = 0;
    private int _roadCount = 0;

    // Start generation algorithm
    void Start()
    {
        GenerateGrid();      
    }

    // Check for input to re-generate
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ClearGrid();
            GenerateGrid();
        }
    }

    // Generate grid rows and columns
    void GenerateGrid()
    {
        var roadGenRandom = Random.Range(25, 50);
        Vector2 center = new Vector2(rows / 2, columns / 2);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Generates grid
                Vector3 position = new Vector3(j, 0, i);
                Instantiate(gridObject, position, Quaternion.identity, transform);
                cells[i, j] = 0;

                // Handle road generation calculation
                if (i % roadGenRandom == 0 || j % roadGenRandom == 0)
                {
                    GenerateRoad(i, j);
                    cells[i, j] = 1;
                    _roadCount++;
                }
            }
        }
        // Loop to generate buildings
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var number = Random.Range(0, 10);
                if (number % 2 == 0 && cells[i, j] == 0 && NeighboringCellsEmpty(i, j, 5))
                {
                    float distanceFromCenter = Vector2.Distance(new Vector2(i, j), center);
                    float size = Mathf.Lerp(15f, 5f, distanceFromCenter / (Mathf.Max(rows, columns) / 5f));
                    Debug.Log(distanceFromCenter);
                    GenerateBuilding(i, j, size,distanceFromCenter);
                    cells[i, j] = 1;
                    _buildingCount++;
                }
            }
        }

        // Print out building and road counts
        Debug.Log("Buildings generated - " + _buildingCount);
        Debug.Log("Road pieces generated - " + _roadCount);
    }

    // Generate placeholder building
    void GenerateBuilding(int row, int col, float height, float distanceFromCenter)
    {
        Vector3 rotationAngles = new Vector3(0f, Random.Range(0f,10f), 0f);
        Vector3 buildingposition = new Vector3(col, 1, row);
        int randomIndex = Random.Range(0, buildingMat.Length);
        // Inner ring
        if (distanceFromCenter <= 200 / 4)
        {
            Instantiate(modelArray[0], buildingposition, Quaternion.identity, transform);
            modelArray[0].transform.localScale = new Vector3(Random.Range(2f, 4f), height, Random.Range(2f, 4f));
            modelArray[0].GetComponent<Renderer>().material = buildingMat[0];
        }
        // Outer ring
        else if (distanceFromCenter <= 200 / 3)
        {
            Instantiate(modelArray[2], buildingposition, Quaternion.identity, transform);
            modelArray[2].transform.localScale = new Vector3(Random.Range(2f, 4f), height, Random.Range(2f, 4f));
            modelArray[2].GetComponent<Renderer>().material = buildingMat[2];
        }
        // Far ring
        else
        {
            Instantiate(modelArray[1], buildingposition, Quaternion.identity, transform);
            modelArray[1].transform.localScale = new Vector3(Random.Range(2f, 4f), height, Random.Range(2f, 4f));
            modelArray[1].GetComponent<Renderer>().material = buildingMat[1];
        }

    }

    // Generate road at point
    void GenerateRoad(int row, int col)
    {
        Vector3 roadgposition = new Vector3(col, 0, row);
        Instantiate(testRoad, roadgposition, Quaternion.identity, transform);
    }

    // Check if cells around building is empty
    bool NeighboringCellsEmpty(int row, int col,int gapSize)
    {
        for (int i = -gapSize; i <= gapSize; i++)
        {
            for (int j = -gapSize; j <= gapSize; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < columns &&
                    cells[newRow, newCol] == 1)
                {
                    return false; // At least one neighboring cell has a building
                }
            }
        }

        return true;
    }

    // Utility function for clearing the grid 
    void ClearGrid()
    {
        // Reset building and road count
        _buildingCount = 0;
        _roadCount = 0;

        // Destroy all buildings
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Reset the cells array to mark all cells as empty
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cells[i, j] = 0;
            }
        }
    }
}
