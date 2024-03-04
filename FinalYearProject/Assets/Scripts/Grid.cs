using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object "GridOrigin" is the point from which the grid is generated from
public class Grid : MonoBehaviour
{
    public static int rows = 50;
    public static int columns = 50;
    public GameObject gridObject;
    public GameObject testBuilding;
    public GameObject testRoad;
    private int[,] cells = new int[rows, columns];
    private int _verticalPath;
    private int _horizontalPath;
    public Material[] buildingMat;
 
    void Start()
    {
        //DestroyCity();
        GenerateGrid();
    }

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
        var roadGenRandom = Random.Range(7, 25);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Generates grid
                Vector3 position = new Vector3(j, 0, i);
                Instantiate(gridObject, position, Quaternion.identity, transform);

                cells[i, j] = 0;

                if (i % roadGenRandom == 0 || j % roadGenRandom == 0)
                {
                    GenerateRoad(i, j);
                    cells[i, j] = 1;
                }
            }
        }
        // Loop to generate buildings
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var number = Random.Range(0, 10);
                // Generates building
                if (number % 2 == 0 && i >= 20 && i <= 30 && j >= 20 && j <= 30 && cells[i, j] == 0 && NeighboringCellsEmpty(i,j,1))
                {
                    GenerateBuilding(i, j, 15f);
                    cells[i, j] = 1;
                }
                if (number % 2 == 0 && i >= 10 && i <= 40 && j >= 10 && j <= 40 && cells[i, j] == 0 && NeighboringCellsEmpty(i, j, 1))
                {
                    GenerateBuilding(i, j, 10f);
                    cells[i, j] = 1;
                }
                if (number % 2 == 0 && i >= 0 && i <= 50 && j >= 0 && j <= 50 && cells[i, j] == 0 && NeighboringCellsEmpty(i, j, 1))
                {
                    GenerateBuilding(i, j, 5f);
                    cells[i, j] = 1;
                }
            }
        }
    }
    // Generate placeholder building
    void GenerateBuilding(int row, int col, float height)
    {
        Vector3 buildingposition = new Vector3(col, 1, row);
        Instantiate(testBuilding, buildingposition, Quaternion.identity, transform);
        testBuilding.transform.localScale = new Vector3(Random.Range(1f,2f),height,Random.Range(1f,2f));
        int randomIndex = Random.Range(0, buildingMat.Length);
        testBuilding.GetComponent<Renderer>().material = buildingMat[randomIndex];
    }

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
