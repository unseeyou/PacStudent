using UnityEngine;
using System.Collections.Generic;

public class PacStudentController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    [SerializeField] private Tweener tweener;
    [SerializeField] private CollisionManager collisionManager;
    public Transform backTrack;
    public GameObject countdown;
    
    private Dictionary<KeyCode, Vector2> directions = new Dictionary<KeyCode, Vector2>()
    {
        { KeyCode.A, new Vector2(-1, 0) },
        { KeyCode.D, new Vector2(1, 0) },
        { KeyCode.W, new Vector2(0, 1) },
        { KeyCode.S, new Vector2(0, -1) },
        { KeyCode.End, Vector2.zero },
    };

    private KeyCode placeholder = KeyCode.End;
    private KeyCode lastInput;
    private KeyCode currentInput;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
        lastInput = placeholder;
        currentInput = placeholder;
        backTrack.position = transform.position;
    }

    Vector3 GenerateNextMovement(KeyCode key)
    {
        Vector3 endPos = target.position;
        switch (key)
        {
            case KeyCode.W:
                endPos.y += 1f;
                break;
            case KeyCode.S:
                endPos.y -= 1f;
                break;
            case KeyCode.A:
                endPos.x -= 1f;
                break;
            case KeyCode.D:
                endPos.x += 1f;
                break;
            case KeyCode.End:  // idk how to make it work as const and im too tired :(
                break;
        }
        return endPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown.activeSelf)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
        }

        if (!tweener.TweenExists(target))
        {
            bool walkable = false; // set this flag with a check in the next section
            TileType tile = collisionManager.CheckTile();
            if (tile != TileType.Wall)
            {
                walkable = true;
                backTrack.position = target.position;
            }
            
            if (walkable)
            { 
                tweener.AddTween(target, target.position, GenerateNextMovement(lastInput), 0.2f);  // im pretty sure this is 5 tiles per second
                currentInput = lastInput;
            }
            else
            {
                tile = collisionManager.CheckTile();
                if (tile != TileType.Wall)
                {
                    walkable = true;
                    backTrack.position = target.position;
                    
                }
                if (walkable)
                {
                    tweener.AddTween(target, target.position, GenerateNextMovement(currentInput), 0.2f);
                }
                else
                {
                    target.position = backTrack.position;
                    collisionManager.soundManager.PlayWallBumpSound();
                }
            }
        }
    }
}
