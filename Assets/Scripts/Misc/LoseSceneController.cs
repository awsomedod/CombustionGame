using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseSceneController : MonoBehaviour
{
    //#region Editor Variables
    //[SerializeField]
    //private Text m_Coins;
    //#endregion

    //#region Private Variables
    //private string m_DefaultHighScoreText;
    //#endregion

    #region Initialization
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        //m_DefaultHighScoreText = m_HighScore.text;
    }

    //private void Start()
    //{
    //    UpdateHighScore();
    //}
    #endregion

    //#region Play Button Methods
    //public void PlayArena()
    //{
    //    SceneManager.LoadScene("Arena");
    //}
    //#endregion

    #region General Button Methods
    //public void Quit()
    //{
    //    Application.Quit();
    //}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    //#region High Score Methods
    //private void UpdateHighScore()
    //{
    //    if (PlayerPrefs.HasKey("HS"))
    //    {
    //        m_HighScore.text = m_DefaultHighScoreText.Replace("%S", PlayerPrefs.GetInt("HS").ToString());
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetInt("HS", 0);
    //        m_HighScore.text = m_DefaultHighScoreText.Replace("%S", "0");
    //    }
    //}

    //public void ReserHighScore()
    //{
    //    PlayerPrefs.SetInt("HS", 0);
    //    UpdateHighScore();
    //}
    //#endregion
}
