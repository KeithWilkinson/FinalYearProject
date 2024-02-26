using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object "GridOrigin" is the point from which the grid is generated from
public class Grid : MonoBehaviour
{
    public static int rows = 10;
    public static int columns = 10;
    public GameObject gridObject;
    public GameObject testBuilding;
    public GameObject testRoad;
    private int[,] cells = new int[rows, columns];
    private int _verticalPath = 10;
    private int _horizontalPath = 10;
  
    void Start()
    {
        GenerateGrid();
    }

    // Generate grid rows and columns
    void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
              
                Vector3 position = new Vector3(col, 0, row);
                Instantiate(gridObject, position, Quaternion.identity, transform);
                cells[row, col] = 0;
                var number = Random.Range(0,10);
                // Place building (PROTOTYPE FUNCTIONALITY)
                if(row == 5 && col < _verticalPath)
                {
                    //GenerateBuilding(row, col);
                    GenerateRoad(row, col);
                    cells[row, col] = 1;
                }

                if (col == 7 && row < _horizontalPath)
                {
                    //GenerateBuilding(row, col);
                    GenerateRoad(row, col);
                    cells[row, col] = 1;
                }

                if (number % 2 == 0 && cells[row, col] == 0)
                {
                    GenerateBuilding(row, col);
                    cells[row, col] = 1;
                }
            }
        }


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Debug.Log(cells[row, col]);
            }
        }
    }

    // Generate placeholder building
    void GenerateBuilding(int row, int col)
    {
        Vector3 buildingposition = new Vector3(col, 1, row);
        Instantiate(testBuilding, buildingposition, Quaternion.identity, transform);
    }

    void GenerateRoad(int row, int col)
    {
        Vector3 roadgposition = new Vector3(col, 0, row);
        Instantiate(testRoad, roadgposition, Quaternion.identity, transform);
    }
}
