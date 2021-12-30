using System.Collections.Generic;
using UnityEngine;

public class SnowBallManager : MonoBehaviour
{
    [SerializeField] float distanceBetweenBalls = 0.25f, followSpeed = 1f;

    List<SnowBall> snowBalls;

    [SerializeField] SnowBall defaultSnowBall;

    int count = 1;

    private void Start()
    {
        snowBalls.Add(defaultSnowBall);
    }

    internal void Add(SnowBall sb)
    {
        sb.PrevBall = snowBalls[count - 1];
        snowBalls[count - 1].NextBall = sb;

        sb.DistanceBetweenBalls = distanceBetweenBalls;
        sb.FollowSpeed = followSpeed;

        snowBalls.Add(sb);

        count++;
    }

    internal void Remove(SnowBall sb) 
    {

    }
}
