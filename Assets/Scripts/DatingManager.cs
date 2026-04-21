using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MixedSignals
{
    
public class DatingManager : MonoBehaviour
{
    public static DatingManager Instance;

    public int dateLimit;

    public TextMeshProUGUI alienInfo;
    public TextMeshProUGUI alienPrompt;
    public GameObject canvasObject;
    public List<Image> loveMeterHearts;

    public List<GameObject> answerButtons;
    public Image datingImage;
    int currentCorrectAnswer = -1;

    public int currentDatingCycle;
    public Alien currentDatingAlien;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple DatingManager. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Answer(int n)
    {
        float halfHeartStep = 1f / (loveMeterHearts.Count * 2f);

        if(n == currentCorrectAnswer)
        {
            // + 1 social credit
            Debug.Log("Yay!");
            // Love Meter go up
            // Happy png
            currentDatingAlien.loveMeter += halfHeartStep;
            datingImage.sprite = currentDatingAlien.imagePair.happyImage;
        }
        else
        {
            // Answered Wrongly
            Debug.Log("Nay!");
            // Love Meter go down
            // Sad png
            currentDatingAlien.loveMeter -= halfHeartStep;
            if(currentDatingAlien.loveMeter < 0) currentDatingAlien.loveMeter = 0;
            datingImage.sprite = currentDatingAlien.imagePair.sadImage;
        }

        if(currentDatingCycle >= dateLimit)
        {
    RefreshLoveMeter(currentDatingAlien);

        if (currentDatingAlien.loveMeter >= 1f)
            {
                Debug.Log("You win!");
                GameManager.Instance.trueLove = currentDatingAlien;
                Hide();
                GameManager.Instance.UnfreezePlayerInput();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Ending", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        else
            {
                currentDatingAlien.signalCloudy = true;
                Debug.Log("Get ghosted kid");
                Hide();
                GameManager.Instance.UnfreezePlayerInput();
                ConsoleManager.Instance.blockInput = true;
                GameManager.Instance.ExitConsole(ConsoleManager.Instance);
                GameManager.Instance.cursorLocked = false;
                Cursor.lockState = CursorLockMode.None;

                GameManager.Instance.DateEnds(currentDatingAlien);
                
            }
        }
        else
        {
            QuestionAnswerPair nextQuestion;
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                nextQuestion = Names.GetRandomQuestion();
            }
            else
            {
                nextQuestion = Names.GetRandomSpecialQuestion(currentDatingAlien.alienType);
            }

            Debug.Log($"Next Question: {nextQuestion.question}");

            LoadQuestion(nextQuestion, currentDatingAlien);
            SetupAnswerButtons(nextQuestion);
            currentCorrectAnswer = nextQuestion.answers.correctIndex;
            currentDatingCycle++;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(canvasObject.activeInHierarchy)
            {
                GameManager.Instance.cursorLocked = false;
            }
    }

    public void Show()
    {
        canvasObject.SetActive(true);
        GameManager.Instance.cursorLocked = false;
    }

    public void Hide()
    {
        canvasObject.SetActive(false);
        GameManager.Instance.cursorLocked = true;
    }

    public void Init(QuestionAnswerPair question, Alien alienData)
    {
        currentDatingAlien = alienData;
        currentCorrectAnswer = question.answers.correctIndex;
        currentDatingCycle = 1;

        datingImage.sprite = alienData.imagePair.curiousImage;

        LoadQuestion(question, alienData);
        SetupAnswerButtons(question);
        DatingManager.Instance.GetComponent<AudioSource>().Play();
        }

    void SetupAnswerButtons(QuestionAnswerPair question)
    {
        List<GameObject> tmp = new List<GameObject>(answerButtons);

        foreach (var item in tmp)
        {
            item.SetActive(true);
        }

        for (int i = 0; i < question.answers.answers.Count; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.answers.answers[i];
            tmp.Remove(answerButtons[i]);
        }

        foreach (var item in tmp)
        {
            item.SetActive(false);
        }
    }

    void LoadQuestion(QuestionAnswerPair question, Alien alienData)
    {
        alienPrompt.text = question.question;
        alienInfo.text = $"NAME: {alienData.GetFullName()}{Environment.NewLine}LIKES: {alienData.likes}{Environment.NewLine}DISLIKES: {alienData.dislikes}";
        datingImage.color = alienData.color;
        currentCorrectAnswer = question.answers.correctIndex;

        RefreshLoveMeter(alienData);

    }

    void RefreshLoveMeter(Alien alienData)
{
    float stepSize = 1f / loveMeterHearts.Count;
    for (int i = 0; i < loveMeterHearts.Count; i++)
    {
        float sliceStart = i * stepSize;
        float fill = Mathf.Clamp01((alienData.loveMeter - sliceStart) / stepSize);
        loveMeterHearts[i].fillAmount = fill;
    }
}
}

}