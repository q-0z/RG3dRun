using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button playBtn, optionBtn, exitBtn;
    public GameObject mainMenyPanel;
    private void Start()
    {
        playBtn.onClick.AddListener(OnClickPlayBtn);
        optionBtn.onClick.AddListener(OnClickOptionBtn);
        exitBtn.onClick.AddListener(OnClickExitBtn);

        Time.timeScale = 0;
    }
    

    private void OnClickPlayBtn()
    {
        mainMenyPanel.SetActive(false);
        Time.timeScale = 1;

    }
    private void OnClickExitBtn()
    {


    }
    private void OnClickOptionBtn()
    {
        Application.Quit();
    }
}
