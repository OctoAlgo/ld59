using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnscrambleGameManager : MonoBehaviour
{
    public bool isComplete = false;

    List<string> WordList = new List<string>() { "SATELLITE", "ALIEN", "PLANET", "GALAXY", "UNIVERSE", "SIGNAL" };

    string CurrentWord;
    string scrambledWord;

    void ChooseWord()
    {
        CurrentWord = WordList[UnityEngine.Random.Range(0, WordList.Count)];
    }

    void ScrambleWord()
    {
        char[] letters = CurrentWord.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, letters.Length);
            char temp = letters[i];
            letters[i] = letters[randomIndex];
            letters[randomIndex] = temp;
        }
        scrambledWord = new string(letters);
    }

    void DisplayWord()
    {
        transform.Find("Scrambled Text").GetComponent<TextMeshProUGUI>().text = scrambledWord;
    }

    void SetInputField()
    {
        transform.Find("InputField").transform.Find("Text Area").Find("Placeholder").GetComponent<TextMeshProUGUI>().text = new string('_', CurrentWord.Length);
    }

    public void CheckCorrect()
    {
        if (!isComplete)
        {
            string text = transform.Find("InputField").transform.Find("Text Area").Find("Text").GetComponent<TextMeshProUGUI>().text.Trim((char)8203);
            if (text.ToUpper() == CurrentWord)
            {
                isComplete = true;

                transform.Find("InputField").GetComponent<TMP_InputField>().interactable = false;
                transform.Find("InputField").GetComponent<TMP_InputField>().text = "Correct!";
                transform.Find("Scrambled Text").GetComponent<TextMeshProUGUI>().text = CurrentWord;

                GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().stage += 1;
                GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().StatusUpdate();
            }
        }
    }

    void Reset()
    {
        transform.Find("Scrambled Text").GetComponent<TextMeshProUGUI>().text = "";
        transform.Find("InputField").GetComponent<TMP_InputField>().text = "";
        transform.Find("InputField").transform.Find("Text Area").Find("Placeholder").GetComponent<TextMeshProUGUI>().text = "";
        transform.Find("InputField").GetComponent<TMP_InputField>().interactable = true;
        isComplete = false;
    }

    public void LoadGame()
    {
        Reset();
        ChooseWord();
        ScrambleWord();
        DisplayWord();
        SetInputField();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCorrect();
    }
}
