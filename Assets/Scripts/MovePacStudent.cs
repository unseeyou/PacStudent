using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovePacStudent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    private Tweener tweener;
    private KeyCode lastInput = KeyCode.End;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
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
            tweener.AddTween(target, target.position, GenerateNextMovement(lastInput), 0.37f);
        }
    }
}
