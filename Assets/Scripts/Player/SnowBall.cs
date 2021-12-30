using UnityEngine;

public class SnowBall : MonoBehaviour
{
    float distanceBetweenBalls, followSpeed;

    internal float DistanceBetweenBalls {set => distanceBetweenBalls = value;}
    internal float DistanceBetweenBalls1 {set => distanceBetweenBalls = value;}
    internal float FollowSpeed {set => followSpeed = value; }
    internal SnowBall NextBall {set => nextBall = value;}
    internal SnowBall PrevBall {set => prevBall = value;}

    SnowBall nextBall, prevBall;

    private void Update()
    {
        Vector3 displacement = Vector3.zero;

        displacement.x = prevBall.transform.position.x - transform.position.x;
        displacement.x *= Time.deltaTime * followSpeed;
        displacement.z = prevBall.transform.position.z - transform.position.z + distanceBetweenBalls;

        transform.position += displacement;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
