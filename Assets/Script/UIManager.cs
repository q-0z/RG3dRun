using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]private Button playBtnMainMenu, optionBtnMainMenu, exitBtnMainMenu;
    [SerializeField]private Button pauseBtnPauseMenu,resumeBtnPauseMenu, restartBtnPauseMenu, optionBtnPauseMenu, exitBtnPauseMenu;
    [SerializeField]private GameObject mainMenyPanel,pauseMenuPanel,gameOverPanel;
    [SerializeField]private TMP_Text coinTxt, lifeTxt;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playBtnMainMenu.onClick.AddListener(OnClickPlayBtn);
        optionBtnMainMenu.onClick.AddListener(OnClickOptionBtn);
        exitBtnMainMenu.onClick.AddListener(OnClickExitBtn);

        pauseBtnPauseMenu.onClick.AddListener(OnClickPauseBtn);
        resumeBtnPauseMenu.onClick.AddListener(OnClickResumeBtn);
        restartBtnPauseMenu.onClick.AddListener(OnClickRestartBtn);
        optionBtnPauseMenu.onClick.AddListener(OnClickOptionBtn);
        exitBtnPauseMenu.onClick.AddListener(OnClickExitBtn);

        Time.timeScale = 0;
    }
    
    public void SetLife(int val)
    {
        lifeTxt.SetText("life:    " + val);

    }
    public void SetCoin(int val)
    {
        coinTxt.SetText("coin:  " + val);
    }
    private void OnClickPlayBtn()
    {
        mainMenyPanel.SetActive(false);
        Time.timeScale = 1;

        ObstacleManager.Instance.Init();

    }
    private void OnClickExitBtn()
    {
        Application.Quit();
    }
    private void OnClickResumeBtn()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
    }
    private void OnClickPauseBtn()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
    }
    private void OnClickRestartBtn()
    {
        StartCoroutine(RestartLevel(0));
    }
    private void OnClickOptionBtn()
    {
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        StartCoroutine(RestartLevel(3));
    }

    IEnumerator RestartLevel(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
