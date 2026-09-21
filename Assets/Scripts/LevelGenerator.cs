using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
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

    public Tilemap target;
    public TileBase[] tiles;
    public GameObject powerPellet;
    public GameObject[] currentPowerPellets;
    
    bool IsWall(int id)
    {
        return id is 1 or 2 or 3 or 4 or 7;
    }
    
    float GetTileRotation(int[,] grid, int r, int c, int tileID)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        
        bool top = (r > 0) && IsWall(grid[r - 1, c]);
        bool bottom = (r < rows - 1) && IsWall(grid[r + 1, c]);
        bool left = (c > 0) && IsWall(grid[r, c - 1]);
        bool right = (c < cols - 1) && IsWall(grid[r, c + 1]);
        
        if (tileID == 2 || tileID == 4)
        {
            if (top || bottom) return 90f;
            return 0f;                    
        }
        
        if (tileID == 1 ||  tileID == 3)
        {
            if (top && right)    return 90f;
            if (right && bottom) return 0f;
            if (bottom && left)  return -90;
            if (left && top)     return 180f;
        }

        return 0f;
    }

    TileBase GetTileFromID(int tileID)
    {
        TileBase tileToPlace = null;
        if (tileID > 0)
        {
            switch (tileID)
            {
                case 1:
                    tileToPlace = tiles[2];
                    break;
                case 2:
                    tileToPlace = tiles[3];
                    break;
                case 3:
                    tileToPlace = tiles[0];
                    break;
                case 4:
                    tileToPlace = tiles[1];
                    break;
                case 7:
                    tileToPlace = tiles[4];
                    break;
                case 8:
                    tileToPlace = tiles[5];
                    break;
                case 5:
                    tileToPlace = tiles[7];
                    break;
            }
        }
        return tileToPlace;
    }

    void PlaceTiles(int[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int tileID = grid[r, c];

                TileBase tileToPlace = null;
                if (tileID == 6)
                {
                    int y = rows - 1 - r - Mathf.CeilToInt(rows/2f);
                    int x = c - Mathf.CeilToInt(cols/2f);
                    Vector3Int cellPos = new Vector3Int(x, y, 0);
                    Vector3 worldPos = target.GetCellCenterWorld(cellPos);
                    GameObject p = Instantiate(powerPellet, worldPos, Quaternion.identity);
                    p.GetComponent<Renderer>().sortingOrder = 3;
                }
                else
                {
                    tileToPlace = GetTileFromID(tileID);
                }

                if (tileToPlace == null)
                {
                    // do nothing
                }
                else
                {
                    int yPos = rows - 1 - r - Mathf.CeilToInt(rows/2f);
                    int xPos = c - Mathf.CeilToInt(cols/2f);
                    Vector3Int pos = new Vector3Int(xPos, yPos, 0);
                    target.SetTile(pos, tileToPlace);
                    target.SetTileFlags(pos, TileFlags.None);
                    
                    float rotAngle = GetTileRotation(grid, r, c, tileID);
                    Quaternion rot = Quaternion.Euler(0f, 0f, rotAngle);
                    Matrix4x4 mat = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);
                    target.SetTransformMatrix(pos, mat);
                }
            }
        }

        Debug.Log($"LevelGenerator: Placed tiles successfully! Map size: {rows} x {cols}");
    }
    
    void Start()
    {
        target.ClearAllTiles();
        foreach (GameObject obj in currentPowerPellets)
        {
            obj.SetActive(false); // the apples that are alr there will go poof
        }

        int originalRows = levelMap.GetLength(0); // 15
        int originalCols = levelMap.GetLength(1); // 14
        
        int newRows = (originalRows * 2) - 1; // 29
        int newCols = originalCols * 2;       // 28
        
        int[,] fullMap = new int[newRows, newCols];

        for (int r = 0; r < originalRows; r++)
        {
            for (int c = 0; c < originalCols; c++)
            {
                int tileVal = levelMap[r, c];

                // top right
                fullMap[r, c] = tileVal;
                fullMap[r, newCols - 1 - c] = tileVal;

                // bottom half
                if (r < originalRows - 1)
                {
                    int mirroredRow = newRows - 1 - r;
                    
                    // left
                    fullMap[mirroredRow, c] = tileVal;
                    
                    // right
                    fullMap[mirroredRow, newCols - 1 - c] = tileVal;
                }
            }
        }
        PlaceTiles(fullMap);
    }
}
