using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Empty = 0,
    Wall = 1,
}

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private Grid grid;   
    public HUDManager hudManager;
    public PacStudentSoundManager soundManager;
    
    public TileType CheckTile()
    {
        if (targetTilemap != null)
        {
            Vector3Int cellPos = targetTilemap.WorldToCell(transform.position);
            TileBase tile = targetTilemap.GetTile(cellPos);

            if (tile != null && tile.name.Contains("Wall"))
            {
                return TileType.Wall;
            }
        }
        
        return TileType.Empty;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerPellet"))
        {
            Destroy(other.gameObject);
            soundManager.setGhostState(GhostState.Scared);
            hudManager.UpdateScore(50);
        }
        
        else if (other.CompareTag("cherry_moved"))
        {
            Destroy(other.gameObject);
            soundManager.PlayCherrySound();
            hudManager.UpdateScore(100);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Tilemap tilemap = other.gameObject.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            Vector3Int cellPos = tilemap.WorldToCell(transform.position);
            TileBase tile = tilemap.GetTile(cellPos);

            if (tile != null)
            {
                Debug.Log($"Collided with tile: {tile.name}");
                if (tile.name == "pellet")
                {
                    hudManager.UpdateScore(10);
                    tilemap.SetTile(cellPos, null);
                }
            }
        }
    }
}
