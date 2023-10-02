using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private Vector2[] waypoints;
    private int currentWaypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Vector2[]
        {
             new Vector2(-12.5f, 13.5f),
             new Vector2(-7.5f, 13.5f),
             new Vector2(-7.5f, 9.5f),
             new Vector2(-12.5f, 9.5f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            if (Vector2.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                var t = (Time.time - activeTween.StartTime) / activeTween.Duration;
                activeTween.Target.position = Vector2.Lerp(activeTween.StartPos, activeTween.EndPos,t);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }
    }

    public void AddTween(Transform targetObject, Vector2 startPos, Vector3 endPos, float duration)
    {
        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
    }
}
