using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] GameObject startMenu, gameplayMenu, winMenu, loseMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        StartGame();
    }

    [SerializeField] GameObject scoreMultiplierHolder;
    [SerializeField] float scoreMultDegreeLimit = 35;

    int scoreCount = 1;

    [SerializeField] GameObject fog;
    [SerializeField] float fogDisableTime = 2f;
    internal void DisableFog()
    {
        StartCoroutine(DisableFogAsync());
    }

    IEnumerator DisableFogAsync()
    {
        Color fogColor = fog.GetComponent<Image>().color;

        for (int i = 0; i < 20; i++)
        {
            fogColor.a = 1 - i / 20.0f;

            fog.GetComponent<Image>().color = fogColor;

            yield return new WaitForSeconds(fogDisableTime / 20);
        }

        fog.SetActive(false);
    }

    internal void StartGame()
    {
        startMenu.SetActive(false);
        gameplayMenu.SetActive(true);
    }

    bool isFinished;
    internal bool FinishGame(bool isWinned)
    {
        if (!isFinished)
        {
            gameplayMenu.SetActive(false);

            if (isWinned && !loseMenu.activeInHierarchy)
                StartCoroutine(ShowMenu(winMenu));
            else
                StartCoroutine(ShowMenu(loseMenu));

            isFinished = true;

            return true;
        }

        return false;
    }

    IEnumerator ShowMenu(GameObject gO)
    {
        yield return new WaitForSeconds(1.5f);

        gO.SetActive(true);
    }
}