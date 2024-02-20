using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object "GridOrigin" is the point from which the grid is generated from
public class Grid : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject gridObject;
    public GameObject testBuilding;

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

                Vector3 buildingposition = new Vector3(col, 1, row);
                Instantiate(testBuilding, buildingposition, Quaternion.identity, transform);
            }
        }
    }
}
