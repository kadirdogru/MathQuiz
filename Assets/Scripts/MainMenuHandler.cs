using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject lastScoreTextObject; // Son skoru gösterecek obje
    public GameObject highScoreTextObject; // En yüksek skoru gösterecek obje
    public GameObject newScoreTextObject;  // Yeni skoru gösterecek obje

    private TMP_Text lastScoreText;
    private TMP_Text highScoreText;
    private TMP_Text newScoreText;

    public void Start()
    {
        lastScoreText = lastScoreTextObject.GetComponent<TMP_Text>();
        highScoreText = highScoreTextObject.GetComponent<TMP_Text>();
        newScoreText = newScoreTextObject.GetComponent<TMP_Text>();

        int lastScore = PlayerPrefs.GetInt("LastScore");
        int highScore = PlayerPrefs.GetInt("HighScore");

        lastScoreText.text = "" + lastScore;
        highScoreText.text = "" + highScore;

        if (lastScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", lastScore);
            highScoreText.text = "" + lastScore;
        }
        else
        {
            newScoreText.text = "" + lastScore;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit button pressed");
    }
}



