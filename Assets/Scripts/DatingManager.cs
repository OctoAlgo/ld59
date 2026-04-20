using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DatingManager : MonoBehaviour
{
    public static DatingManager Instance;

    public TextMeshProUGUI alienInfo;
    public TextMeshProUGUI alienPrompt;

    public List<GameObject> answerButtons;

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
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init(QuestionAnswerPair question, Alien alienData)
    {

        List<GameObject> tmp = new List<GameObject>(answerButtons);

        alienPrompt.text = question.question;
        alienInfo.text = $"NAME: {alienData.GetFullName()}{Environment.NewLine}LIKES: {alienData.likes}{Environment.NewLine}DISLIKES: {alienData.dislikes}";

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
}
