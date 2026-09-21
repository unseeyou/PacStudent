using UnityEngine;
using UnityEngine.Serialization;
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
    
    void PlaceTiles(int[,] grid)
    {
        foreach (var tile in tiles)
        {
            Debug.Log($"LevelGenerator: Using saved tile {tile.name}");
        }
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int tileID = grid[r, c];

                TileBase tileToPlace = null;

                // Check if the tileID is within bounds of your assigned array
                if (tileID > 0)
                {
                    switch (tileID)
                    {
                        case 1:
                            tileToPlace = tiles[3];
                            break;
                        case 2:
                            tileToPlace = tiles[2];
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
                        default:
                            tileToPlace = tiles[6];
                            break;
                    }
                }

                // Invert the Y coordinate (`rows - 1 - r`) so row 0 (top of array) 
                // gets placed at the top of the tilemap instead of being flipped upside down.
                int yPos = rows - 1 - r - Mathf.CeilToInt(rows/2f);
                int xPos = c - Mathf.CeilToInt(cols/2f);
                Vector3Int pos = new Vector3Int(xPos, yPos, 0);

                target.SetTile(pos, tileToPlace);
            }
        }

        Debug.Log($"LevelGenerator: Placed tiles successfully! Map size: {rows} x {cols}");
    }
    
    void Start()
    {
        target.ClearAllTiles();

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
