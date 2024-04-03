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
    [SerializeField] private GameObject[] _smallBuildingArray;
    [SerializeField] private GameObject[] _lowBuildingArray;
    [SerializeField] private GameObject[] _skyscraperArray;
    private int _buildingCount = 0;
    private int _roadCount = 0;

    [SerializeField] private GameObject _testTree;
    [SerializeField] private GameObject[] _roadsideDecorations;

    [SerializeField] private GameObject _testObject;

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
                Vector3 position = new Vector3(j, 0.5f, i);
                Instantiate(gridObject, position, Quaternion.identity, transform);
                cells[i, j] = 0;

                // Handle road generation calculation
                if (i % roadGenRandom == 0 || j % roadGenRandom == 0)
                {
                    GenerateRoad(i, j);
                    cells[i, j] = 1;
                    _roadCount++;
                    GenerateTree(i, j);
                    //if(ran == 4)
                    //{
                    //    Instantiate(_testTree, new Vector3(i + 1, 0, j), Quaternion.identity);
                    //}
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
                    GenerateBuilding(i, j, size,distanceFromCenter, Random.Range(1,3));
                    cells[i, j] = 1;
                    _buildingCount++;
                }
            }
        }
        
        // Print out building and road counts
        Debug.Log("Buildings generated - " + _buildingCount);
        Debug.Log("Road pieces generated - " + _roadCount);
    }

    // Generate building
    void GenerateBuilding(int row, int col, float height, float distanceFromCenter, int index)
    {
        int randomBuildingIndex = Random.Range(0, 6);
        
        print(randomBuildingIndex);
        Vector3 buildingposition = new Vector3(col, 1, row);
        int randomIndex = Random.Range(0, buildingMat.Length);
        float[] rotationOptions = {0f, 90f, 180f, 270f, 360f };
        Vector3 rotationAngles = new Vector3(0f, rotationOptions[Random.Range(0, rotationOptions.Length)], 0);
        var ran = Random.Range(3, 6);
        // Inner ring
        if (distanceFromCenter <= 200 / 4)
        {
            Instantiate(_skyscraperArray[randomBuildingIndex], buildingposition, Quaternion.identity, transform);
            _skyscraperArray[randomBuildingIndex].transform.localScale = new Vector3(ran, ran, ran);
            _skyscraperArray[randomBuildingIndex].GetComponent<Renderer>().material = buildingMat[Random.Range(0, buildingMat.Length)];
        }
        // Outer ring
        else if (distanceFromCenter <= 200 / 3)
        {
            Instantiate(_lowBuildingArray[randomBuildingIndex], buildingposition, Quaternion.identity, transform);
            _lowBuildingArray[randomBuildingIndex].transform.localScale = new Vector3(ran, ran, ran);
            _lowBuildingArray[randomBuildingIndex].GetComponent<Renderer>().material = buildingMat[Random.Range(0, buildingMat.Length)];
        }
        // Far ring
        else
        {
            Instantiate(_smallBuildingArray[randomBuildingIndex], buildingposition, Quaternion.Euler(rotationAngles), transform);
            _smallBuildingArray[randomBuildingIndex].transform.localScale = new Vector3(ran, ran, ran);
            _smallBuildingArray[randomBuildingIndex].GetComponent<Renderer>().material = buildingMat[Random.Range(0, buildingMat.Length)];
        }

    }

    // Generate road at point
    void GenerateRoad(int row, int col)
    {
        Vector3 roadgposition = new Vector3(col, 0.5f, row);
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

    // Function to generate road side decorations
    void GenerateTree(int row, int col)
    {
        var ran = Random.Range(1, 20);
        int ranDecoration = Random.Range(0, _roadsideDecorations.Length);
        GameObject randomObject = _roadsideDecorations[ranDecoration];
        if (ran == 5)
        {
            Instantiate(randomObject, new Vector3(row + 2, 0, col - 2), Quaternion.identity);
        }
    }
}
