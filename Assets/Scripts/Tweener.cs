using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens = new List<Tween>();
    private PacStudentSoundManager sound;

    void Start()
    {
        sound = GetComponent<PacStudentSoundManager>();
    }

    public bool TweenExists(Transform target)
    {
        bool inList = false;
        foreach (Tween tween in activeTweens)
        {
            if (tween.Target == target)
            {
                inList = true;
                break;
            }
        }

        return inList;
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (!TweenExists(targetObject))
        {
            Tween activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(activeTween);
            sound.PlayStepSound();
            return true;
        }
        return false;
    }
    
    // Update is called once per frame
    void Update()
    {
        for (int i = activeTweens.Count - 1; i >= 0; i--)
        {
            Tween activeTween = activeTweens[i];
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float pastDuration = (Time.time - activeTween.StartTime) / activeTween.Duration; // linear
                // float cubicTime = pastDuration * pastDuration * pastDuration;
                Vector3 pos = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, pastDuration);
                activeTween.Target.position = pos;
            }

            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTweens.RemoveAt(i);
            }
        }
    }
}