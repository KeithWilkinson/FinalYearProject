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
        //_verticalPath = columns;
        //_horizontalPath = rows;
        GenerateGrid();
    }

    // Generate grid rows and columns
    void GenerateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Generates grid
                Vector3 position = new Vector3(j, 0, i);
                Instantiate(gridObject, position, Quaternion.identity, transform);

                cells[i, j] = 0;

                switch (i)
                {
                    case 1:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 10:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 20:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 30:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 40:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 50:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                }

                switch (j)
                {
                    case 1:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 10:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 20:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 30:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 40:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
                    case 50:
                        GenerateRoad(i, j);
                        cells[i, j] = 1;
                        break;
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
                if (number % 2 == 0 && i >= 20 && i <= 30 && j >= 20 && j <= 30 && cells[i, j] == 0)
                {
                    GenerateBuilding(i, j, 15f);
                    cells[i, j] = 1;
                }
                if (number % 2 == 0 && i >= 10 && i <= 40 && j >= 10 && j <= 40 && cells[i, j] == 0)
                {
                    GenerateBuilding(i, j, 10f);
                    cells[i, j] = 1;
                }
                if (number % 2 == 0 && i >= 0 && i <= 50 && j >= 0 && j <= 50 && cells[i, j] == 0)
                {
                    GenerateBuilding(i, j, 5f);
                    cells[i, j] = 1;
                }
                if (number % 2 == 0 && cells[i, j] == 0)
                {                    
                    //GenerateBuilding(i, j, 3f);
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
        testBuilding.transform.localScale = new Vector3(1f,height,1f);
        int randomIndex = Random.Range(0, buildingMat.Length);
        testBuilding.GetComponent<Renderer>().material = buildingMat[randomIndex];
    }

    void GenerateRoad(int row, int col)
    {
        Vector3 roadgposition = new Vector3(col, 0, row);
        Instantiate(testRoad, roadgposition, Quaternion.identity, transform);
    }
}
