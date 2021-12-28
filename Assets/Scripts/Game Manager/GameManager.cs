using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [SerializeField] CinemachineVirtualCamera finalCam;
    internal void SetFinalCam(Transform transform)
    {
        finalCam.Priority = 12;

        finalCam.LookAt = transform;
        finalCam.Follow = transform;
    }
}