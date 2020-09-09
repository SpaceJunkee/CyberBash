using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScreenManager : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("HighScore").GetComponent<Text>().text = $"HighScore\n{PlayerPrefs.GetInt("HighScore")}";
        GameObject.Find("SpecialCurrencyText").GetComponent<Text>().text = $"Gems\n{PlayerPrefs.GetInt("SpecialCurrency")}";
        GameObject.Find("NormalCurrencyText").GetComponent<Text>().text = $"Money\n${PlayerPrefs.GetInt("NormalCurrency")}";
        GameObject.Find("MoneyEarnedText").GetComponent<Text>().text = $"$ {PlayerPrefs.GetInt("MoneyEarned")}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

}
