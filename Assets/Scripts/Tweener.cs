using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private Vector2[] waypoints;
    private int currentWaypoint = 0;
    public float speed;
    private Animator animator;

    public AudioClip walkSfx;
    private AudioSource audioSource;

    private bool isWalking = false;
    
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
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        AddTween();
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

                if (!isWalking)
                {
                    PlayWalkSfx();
                    isWalking = true;
                }
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                AddTween();
            }
        }
    }

    void AddTween()
    {
        Vector2 startPos = waypoints[currentWaypoint];
        Vector2 endPos = waypoints[(currentWaypoint + 1) % waypoints.Length];
        var duration = Vector2.Distance(startPos, endPos) / speed;

        activeTween = new Tween(transform, startPos, endPos, Time.time, duration);
        
        Vector2 direction = endPos - startPos;
        animator.ResetTrigger("WalkRightAnim");
        animator.ResetTrigger("WalkLeftAnim");
        animator.ResetTrigger("WalkDownAnim");
        animator.ResetTrigger("WalkUpAnim");
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetTrigger(direction.x > 0 ? "WalkRightAnim" : "WalkLeftAnim");
        }
        else
        {
            animator.SetTrigger(direction.y > 0 ? "WalkUpAnim" : "WalkDownAnim");
        }
        
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    public void PlayWalkSfx()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = walkSfx;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}