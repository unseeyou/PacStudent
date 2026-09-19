using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject backgroundTile;
    [SerializeField] private GameObject fillCube;
    private int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    int LookupMap(int index)
    {
        int row = 0;
        while (index > 13) // im pretty sure this isnt supposed to be 14
        {
            row ++;
            index -= 14;
        }
        return levelMap[row,index];
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int cols = Mathf.RoundToInt(fillCube.transform.localScale.x);
        int rows = Mathf.RoundToInt(fillCube.transform.localScale.y);
        
        Debug.Log(cols + rows);
        
        FillGrid(cols, rows);
    }

    void FillGrid(int cols, int rows)
    {
        Vector3 halfScale = fillCube.transform.localScale * 0.5f;
        Vector3 center = fillCube.transform.position;
        Vector3 topLeft = new Vector3(center.x -  halfScale.x, center.y + halfScale.y, center.z - 1);
        Vector3 bottomRight = new Vector3(center.x + halfScale.x, center.y - halfScale.y, center.z - 1);

        int index = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float tX = cols > 1 ? (float)col / (cols - 1) : 0.5f;  // cool one liner
                float tY = rows > 1 ? (float)row / (rows - 1) : 0.5f;
                float spawnX = Mathf.Lerp(topLeft.x, bottomRight.x, tX);
                float spawnY = Mathf.Lerp(topLeft.y, bottomRight.y, tY);
                
                Vector3 spawnPos = new Vector3(spawnX, spawnY, topLeft.z);
                Instantiate(backgroundTile, spawnPos, Quaternion.identity);

                index++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
