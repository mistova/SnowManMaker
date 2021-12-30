using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [SerializeField] float forwardSpeed, sideSpeed;

    [SerializeField] Vector2 limit;
    [SerializeField] Transform[] wheels;

    bool isMove, isFinished;

    float screenWidth;
    float clickedPositionX;

    Vector3 goingPlace;

    Vector2 speedMultiplier;

    SnowBallManager sbManager;

    private void Start()
    {
        sbManager = GetComponent<SnowBallManager>();

        screenWidth = Screen.width;

        speedMultiplier = Vector2.one;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 displacement = Vector3.forward * forwardSpeed;

        if (isFinished)
        {
            if(transform.position.z < goingPlace.z)
            {
                displacement = (goingPlace - transform.position).normalized * forwardSpeed;
            }
            else
            {
                this.enabled = false;

                if (!Input.GetMouseButton(0))
                {
                    isMove = false;
                }
            }
        }
        else if (isGoingToPlace)
        {
            if (transform.position.z + 1f < goingPlace.z)
            {
                displacement = (goingPlace - transform.position).normalized * forwardSpeed * 0.6f;
            }
            else
            {
                isGoingToPlace = false;
            }
        }
        else if (fixingPosition)
        {
            if (transform.position.z + 0.1f < goingPlace.z)
            {
                displacement = (goingPlace - transform.position).normalized * forwardSpeed * 0.6f;
            }
            else
            {
                fixingPosition = false;
            }
        }
        else
        {
            MouseInput(ref displacement);
        }

        if(wheels.Length > 0)
        {
            SetWheelsRotation(displacement);
        }

        displacement.y = 0;

        displacement *= Time.deltaTime;

        transform.position += displacement;
    }

    private void MouseInput(ref Vector3 displacement)
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedPositionX = Input.mousePosition.x;

            isMove = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
        }
        else if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x;
            float moveScale = (x - clickedPositionX) / screenWidth;
            clickedPositionX += (x - clickedPositionX) / 2;
            displacement.x += sideSpeed * moveScale * 50;

            if (displacement.x > 0 && transform.position.x > limit.y)
            {
                displacement.x = 0;
            }
            else if (displacement.x < 0 && transform.position.x < limit.x)
            {
                displacement.x = 0;
            }
        }

        displacement.x *= speedMultiplier.x;
        displacement.z *= speedMultiplier.y;

        if (!isMove && !isFinished)
        {
            displacement = Vector3.zero;
        }
    }

    private void SetWheelsRotation(Vector3 displacement)
    {
        float rot = Mathf.Atan2(displacement.x, displacement.z) * Mathf.Rad2Deg;

        foreach(Transform wheel in wheels)
        {
            wheel.localRotation = Quaternion.Euler(new Vector3(0, rot, 0));
        }
    }

    bool isGoingToPlace = false;
    internal void GoToPlace(Vector3 position)
    {
        goingPlace = position;

        isGoingToPlace = true;
    }

    bool fixingPosition;
    internal void UnWear(Vector3 goingPlace)
    {
        foreach (Transform wheel in wheels)
        {
            wheel.gameObject.SetActive(true);
        }

        this.goingPlace = new Vector3((limit.x + limit.y) / 2, transform.position.y, goingPlace.z);

        fixingPosition = true;
    }

    [SerializeField] float slowDownTime = 0.3f;
    internal void SlowDown(float slowDownMultiplier)
    {
        StartCoroutine(SlowDownAsync(slowDownMultiplier));
    }

    IEnumerator SlowDownAsync(float slowDownMultiplier)
    {
        forwardSpeed *= slowDownMultiplier;
        sideSpeed *= slowDownMultiplier;
        
        yield return new WaitForSeconds(slowDownTime);

        forwardSpeed /= slowDownMultiplier;
        sideSpeed /= slowDownMultiplier;
    }
}