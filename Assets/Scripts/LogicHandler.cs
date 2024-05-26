using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicHandler : MonoBehaviour
{
    public GameObject questionGameObject;
    public GameObject scoreGameObject;
    public GameObject timerGameObject;
    public TMP_InputField answerInput;
    public AudioSource correct_answer_1;
    public AudioSource wrong_answer;
    public AudioSource failsound;


    private TMP_Text questionText;
    private TMP_Text scoreText;
    private TMP_Text timerText;

    private int currentScore = 0;
    private int currentAnswer;
    private float questionTimer = 3.0f;
    private float gameTimer = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        questionText = questionGameObject.GetComponent<TMP_Text>();
        scoreText = scoreGameObject.GetComponent<TMP_Text>();
        timerText = timerGameObject.GetComponent<TMP_Text>();

        answerInput.onEndEdit.AddListener(delegate { CheckAnswer(answerInput.text); });

        NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        print(questionTimer);
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = "" + Mathf.CeilToInt(gameTimer).ToString();

            questionTimer -= Time.deltaTime;
            if (questionTimer <= 0)
            {
                currentScore -= 1;
                scoreText.text = "" + currentScore;
                NextQuestion();
                failsound.Play();
            }
        }
        else
        {
            EndGame();
        }
    }

    void NextQuestion()
    {
        questionTimer = 3.0f; // Reset the question timer
        questionText.text = GenerateQuestion();
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    public void CheckAnswer(string userInput)
    {
        if (!string.IsNullOrEmpty(userInput) && int.TryParse(userInput, out int userAnswer))
        {
            if (userAnswer == currentAnswer)
            {
                currentScore += 2;
                scoreText.text = "" + currentScore;
                NextQuestion();
                correct_answer_1.Play();
            }
            else
            {
                currentScore -= 1;
                scoreText.text = "" + currentScore;
                wrong_answer.Play();
            }
        }

        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    void EndGame()
    {
        PlayerPrefs.SetInt("LastScore", currentScore);
        SceneManager.LoadScene("StartMenu");
    }

    string GenerateQuestion()
    {
        int a = Random.Range(1, 20); // First number
        int b = Random.Range(1, 20); // Second number
        int operation = Random.Range(0, 4); // 0 for addition, 1 for subtraction, 2 for multiplication, 3 for division

        string question;
        switch (operation)
        {
            case 0:
                question = $"{a} + {b}";
                currentAnswer = a + b;
                break;
            case 1:
                question = $"{a} - {b}";
                currentAnswer = a - b;
                break;
            case 2:
                question = $"{a} * {b}";
                currentAnswer = a * b;
                break;
            case 3:
                while (b == 0 || a % b != 0) // Ensure integer result
                {
                    b = Random.Range(1, 20);
                }
                question = $"{a} / {b}";
                currentAnswer = a / b;
                break;
            default:
                question = $"{a} + {b}";
                currentAnswer = a + b;
                break;
        }
        return question;
    }
}
