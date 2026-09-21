using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePacStudent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;
    private Tweener tweener;
    private List<string> _directions;
    private int _index = 0;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
        _directions = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            _directions.Add("right");
        }
        for (int i = 0; i < 4; i++)
        {
            _directions.Add("down");
        }
        for (int i = 0; i < 5; i++)
        {
            _directions.Add("left");
        }
        for (int i = 0; i < 4; i++)
        {
            _directions.Add("up");
        }
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
        if (_index > _directions.Count - 1)
        {
            _index = 0;
        }
        bool check = tweener.AddTween(target, target.position, GenerateNextMovement(_directions[_index]), 1f/3f);
        if (check)
        {
            _index++;
        }
    }
}
