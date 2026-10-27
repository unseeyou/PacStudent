using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovePacStudent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    private Tweener tweener;
    private int _index = 0;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
    }

    Vector3 GenerateNextMovement(string direction)
    {
        Vector3 endPos = target.position;
        switch (direction)
        {
            case "up":
                endPos.y += 1f;
                break;
            case "down":
                endPos.y -= 1f;
                break;
            case "left":
                endPos.x -= 1f;
                break;
            case "right":
                endPos.x += 1f;
                break;
        }
        return endPos;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
