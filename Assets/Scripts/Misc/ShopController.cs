using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private Text m_Coins;
    #endregion

    #region Private Variables
    private string m_DefaultCoins;
    #endregion

    #region Initialization
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        m_DefaultCoins = m_Coins.text;
    }

    private void Start()
    {
        UpdateCoins();
    }
    #endregion

    #region Play Button Methods
    public void PlayArena()
    {
        SceneManager.LoadScene("Arena");
    }
    #endregion

    #region General Button Methods
    //public void Quit()
    //{
    //    Application.Quit();
    //}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseHealth()
    {
        if (!PlayerPrefs.HasKey("CC") || PlayerPrefs.GetInt("CC") <= 0) return;
        if (!PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetInt("Health", 525);
        }
        else
        {
            int cur = PlayerPrefs.GetInt("Health");
            PlayerPrefs.SetInt("Health", cur+25);
        }
        int coins = PlayerPrefs.GetInt("CC");
        PlayerPrefs.SetInt("CC", coins - 5);
        UpdateCoins();
        Debug.Log(PlayerPrefs.GetInt("Health"));
    }

    public void IncreaseSpeed()
    {
        if (!PlayerPrefs.HasKey("CC") || PlayerPrefs.GetInt("CC") <= 0) return;
        if (!PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.SetFloat("Speed", 105);
        }
        else
        {
            float cur = PlayerPrefs.GetFloat("Speed");
            PlayerPrefs.SetFloat("Speed", cur + 5);
        }
        int coins = PlayerPrefs.GetInt("CC");
        PlayerPrefs.SetInt("CC", coins - 5);
        UpdateCoins();
        Debug.Log(PlayerPrefs.GetFloat("Speed"));
    }
    #endregion

    #region Coin Methods
    private void UpdateCoins()
    {
        if (PlayerPrefs.HasKey("CC"))
        {
            m_Coins.text = m_DefaultCoins.Replace("%S", PlayerPrefs.GetInt("CC").ToString());
        }
        else
        {
            PlayerPrefs.SetInt("CC", 0);
            m_Coins.text = m_DefaultCoins.Replace("%S", "0");
        }
    }

    //public void ReserHighScore()
    //{
    //    PlayerPrefs.SetInt("HS", 0);
    //    UpdateHighScore();
    //}
    #endregion
}
