using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager singleton;

    #region Private Variables
    private int m_CurCoins;
    #endregion

    #region Initialization
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("CC"))
        {
            m_CurCoins = 0;
            PlayerPrefs.SetInt("CC", m_CurCoins);
        }
        else
        {
            m_CurCoins = PlayerPrefs.GetInt("CC");
        }
    }
    #endregion

    #region Score Methods
    public void IncreaseCoins(int amount)
    {
        m_CurCoins += amount;
    }

    //private void UpdateHighScore()
    //{
    //    if (!PlayerPrefs.HasKey("HS"))
    //    {
    //        PlayerPrefs.SetInt("HS", m_CurCoins);
    //        return;
    //    }

    //    int hs = PlayerPrefs.GetInt("HS");
    //    if (hs < m_CurCoins)
    //    {
    //        PlayerPrefs.SetInt("HS", m_CurCoins);
    //    }
    //}
    #endregion

    #region Destruction
    private void OnDisable()
    {
        PlayerPrefs.SetInt("CC", m_CurCoins);
    }
    #endregion
}
