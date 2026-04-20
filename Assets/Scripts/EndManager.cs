using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MixedSignals
{
public class EndingManager : MonoBehaviour
{
    public TMP_Text alienInfoText;
    public SpriteRenderer alienPortrait;
    public Light spotlight;                 // optional
    public float imageCycleSeconds = 1.2f;

    public string startSceneName = "Main";  // SET THIS in inspector

    void Start()
    {
        // Make sure cursor is free so the player can click Play Again
        if (GameManager.Instance != null)
            GameManager.Instance.cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var alien = GameManager.Instance != null ? GameManager.Instance.trueLove : null;
        if (alien == null)
        {
            alienInfoText.text = "[ERR] No signal. You died alone.";
            return;
        }

        alienInfoText.text =
            $"Your... Perfect Soul-Mate, unique in this universe.\n\n" +
            $"NAME:     {alien.GetFullName()}\n" +
            $"TYPE:     {alien.alienType}\n" +
            $"LIKES:    {alien.likes}\n" +
            $"DISLIKES: {alien.dislikes}\n" +
            $"HASH:     {alien.hashX}:{alien.hashY}";

        alienPortrait.color = alien.color;
        if (spotlight != null) spotlight.color = alien.color;

        StartCoroutine(CyclePortraits(alien));
    }

    IEnumerator CyclePortraits(Alien alien)
    {
        Sprite[] frames = {
            alien.imagePair.curiousImage,
            alien.imagePair.happyImage,
            alien.imagePair.sadImage,
        };
        int i = 0;
        while (true)
        {
            if (alien.imagePair != null && frames[i] != null)
                alienPortrait.sprite = frames[i];
            i = (i + 1) % frames.Length;
            yield return new WaitForSeconds(imageCycleSeconds);
        }
    }

    public void OnPlayAgainClicked()
    {
        // Nuke all DontDestroyOnLoad singletons so fresh ones spawn in the main scene
        if (DatingManager.Instance != null)
            Destroy(DatingManager.Instance.gameObject);
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);
        if (DatingManager.Instance != null)
            Destroy(DatingManager.Instance.gameObject);
        if (ConsoleManager.Instance != null)
            Destroy(ConsoleManager.Instance.gameObject);
        if (PlanetsManager.Instance != null)
            Destroy(PlanetsManager.Instance.gameObject);
        if (SatelliteConsoleManager.Instance != null)
            Destroy(SatelliteConsoleManager.Instance.gameObject);

        SceneManager.LoadScene(startSceneName, LoadSceneMode.Single);
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
}