using UnityEngine;

public class CherryController : MonoBehaviour
{
    public float gridSize = 1f;  
    public float moveTime = 1f;
    public Vector2 mapCentre = Vector2.zero + new Vector2(0f, -1f);
    public Vector2 mapSize = new Vector2(28f, 30f);
    
    public Tweener tweener;
    public Transform cherryParent;
    private Transform cherry;

    Vector3 GetRandomGridOutsideBox()
    {
        // 0: Left, 1: Right, 2: Bottom, 3: Top: 4
        int side = Random.Range(0, 4);
    
        int rx = 0;
        int ry = 0;

        int boxLeftGrid   = -15;
        int boxRightGrid  = 15;
        int boxBottomGrid = -16;
        int boxTopGrid    = 16;

        
        switch (side)
        {
            case 0:
                rx = Random.Range(boxLeftGrid - 3, boxLeftGrid);
                ry = Random.Range(boxBottomGrid - 2, boxTopGrid + 3);
                break;
            case 1: 
                rx = Random.Range(boxRightGrid + 1, boxRightGrid + 4);
                ry = Random.Range(boxBottomGrid - 2, boxTopGrid + 3);
                break;
            case 2: 
                rx = Random.Range(boxLeftGrid, boxRightGrid + 1);
                ry = Random.Range(boxBottomGrid - 3, boxBottomGrid);
                break;
            case 3: 
                rx = Random.Range(boxLeftGrid, boxRightGrid + 1);
                ry = Random.Range(boxTopGrid + 1, boxTopGrid + 4);
                break;
        }

        return new Vector3(rx * gridSize, ry * gridSize, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cherry = Instantiate(cherryParent, GetRandomGridOutsideBox(), Quaternion.identity);
        cherry.gameObject.SetActive(true);
        tweener.AddTween(cherry, cherry.position, GetRandomGridOutsideBox(), moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
