using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Empty = 0,
    Wall = 1,
    Pellet = 2,
    Teleporter = 3
}

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private Grid grid;   
    public HUDManager hudManager;
    public PacStudentSoundManager soundManager;
    public TileType CheckTile(Vector2 facingDirection)
    {
        if (targetTilemap == null || grid == null)
        {
            Debug.LogWarning("Tilemap or Grid reference missing on TileDetector script.");
            return TileType.Empty;
        }
        
        // Vector3 playerPos = transform.position;
        // Vector3Int cellPosition = grid.WorldToCell(playerPos);
        // TileBase tile = targetTilemap.GetTile(cellPosition);
        //
        // if (tile != null)
        // {
        //     if (tile.name == "pellet")
        //     {
        //         return TileType.Pellet;
        //     }
        //     Debug.Log($"Facing tile: {tile.name} at cell position: {cellPosition}");
        //     
        //     return TileType.Wall;
        // }
        //
        // Debug.Log($"Facing an empty tile space at cell position: {cellPosition}");
        return TileType.Empty;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("powerPellet"))
        {
            Destroy(other.gameObject);
            soundManager.setGhostState(GhostState.Scared);
        }
        
        else if (other.CompareTag("cherry_moved"))
        {
            Destroy(other.gameObject);
            soundManager.PlayCherrySound();
            hudManager.UpdateScore(100);
        }
    }
    
}
