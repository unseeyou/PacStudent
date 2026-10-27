using Unity.VisualScripting;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public float gridSize = 1f;  
    private float moveTime = 5f;
    private Vector2 mapSize = new(28f, 30f);
    
    public Tweener tweener;
    public Transform cherryParent;
    private Transform cherry;

    Vector3 GetRandomGridOutsideBox()
    {
        // 0: Left, 1: Right
        int side = Random.Range(0, 1);
    
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
                ry = Random.Range(boxBottomGrid + 2, boxTopGrid - 3);
                break;
            case 1: 
                rx = Random.Range(boxRightGrid + 1, boxRightGrid + 4);
                ry = Random.Range(boxBottomGrid + 2, boxTopGrid - 3);
                break;
        }

        return new Vector3(rx * gridSize, ry * gridSize, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cherry = Instantiate(cherryParent, GetRandomGridOutsideBox(), Quaternion.identity);
        cherry.gameObject.SetActive(true);

        Vector3 endPos = new Vector3();
        endPos.y = cherry.position.y;
        if (cherry.position.x < 0)
        {
            endPos.x = mapSize.x / 2 + 1;
        }
        else
        {
            endPos.x = -1 * (mapSize.x / 2 + 1);
        }
        
        tweener.AddTween(cherry, cherry.position, endPos, moveTime);
        cherry.tag = "cherry_moved";
    }

    // Update is called once per frame
    void Update()
    {
        if (cherry != null)
        {
            if (!tweener.TweenExists(cherry) && cherry.CompareTag("cherry_moved"))
            {
                cherry.gameObject.SetActive(false);
                Destroy(cherry.gameObject);
                cherry = null;
            }
        }
    }
}
