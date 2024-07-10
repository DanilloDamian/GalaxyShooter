using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] Lives;
    public Image LivesImageDisplay;
    public Text ScoreText;
    public int Score;
    public GameObject TitleScreen;

    public void UpdateLives(int currentLives)
    {
        LivesImageDisplay.sprite = Lives[currentLives];
    }

    public void UpdateScore()
    {
        Score += 10;
        ScoreText.text = "Score: " + Score;
    }

    public void StartGame()
    {
        TitleScreen.SetActive(false);
        ScoreText.text = "Score: ";
    }

    public void GameOver()
    {
        TitleScreen.SetActive(true);
    }

}
