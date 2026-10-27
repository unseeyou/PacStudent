using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PacStudentController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    [SerializeField] 
    private Tweener tweener;

    private KeyCode placeholder = KeyCode.End;
    private KeyCode lastInput;
    private KeyCode currentInput;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
        lastInput = placeholder;
        currentInput = placeholder;
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
            bool walkable = true; // set this flag with a check in the next section
            if (walkable)
            { 
                tweener.AddTween(target, target.position, GenerateNextMovement(lastInput), 0.2f);  // im pretty sure this is 5 tiles per second
                currentInput = lastInput;
            }
            else
            {
                walkable = true; // this would be the second check for the current direction
                if (walkable)
                {
                    tweener.AddTween(target, target.position, GenerateNextMovement(currentInput), 0.2f);
                }
            }
        }
    }
}
