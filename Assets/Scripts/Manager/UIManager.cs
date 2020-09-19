using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// script handle the UI part of the Game play.
/// </summary>

public class UIManager : MonoBehaviour
{
    #region PUBLIC FIELDS

    [Header("Player Health and level up UI")]
    [SerializeField] Image healthImage;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject gameOver;
    [SerializeField] Text message;
    [SerializeField] Image levelUp;
    #endregion
    #region PRIAVTE FIELDS

    Vector3 upPosition = new Vector3(0, 2000, 0);

    Color galaxyColor = new Color(0.34f, 0.34f, 0.34f, 1);

    Vector3 levelUpInitPosition;

    GameManager gameManager;

    #endregion

    private void Start()
    {
        levelUpInitPosition = levelUp.transform.localPosition;

        gameManager = GameManager.GetInstance();

    }

    #region PRIVATE METHODS

  
    #endregion

    #region PUBLIC FIELDS

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    // Set the player health UI
    public void SetPlayerHealth(float damage,float maxHealth)
    {
        healthImage.fillAmount = healthImage.fillAmount - ((float)damage / maxHealth);
    }

    // shows the score
    public void SetScore(int score)
    {
        scoreText.text = "Score:- " + score.ToString();
    }

    // Button action for Restart
    public void RestartGame()
    {
        Time.timeScale = 1;
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);
        SceneManager.LoadScene((int)GAMESTATE.GAMEPLAY);
    }
    
    // Button Action for Menu
    public void BackToMenu()
    {
        Time.timeScale = 1;
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);
        SceneManager.LoadScene((int)GAMESTATE.UI);
    }

    public void LevelUp()
    {
        StartCoroutine(scaleUp());
    }

    #endregion

    #region COROUTINES

    // Scale the level up image to 1.
    IEnumerator scaleUp()
    {
        float duration=1;

        float time=0;

        while(time < duration)
        {
            time += Time.deltaTime;

            levelUp.transform.localScale = Vector3.Lerp(levelUp.transform.localScale, Vector3.one, time * 5);

            yield return null;
        }

        StartCoroutine(MoveLevelUp());
    }

    // Move the level up image to top
    IEnumerator MoveLevelUp()
    {
        float duration = 1.5f;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            levelUp.transform.localPosition = Vector3.Lerp(levelUp.transform.localPosition, upPosition, time * 5);

            yield return null;
        }

        levelUp.transform.localScale = Vector3.zero;

        levelUp.transform.localPosition = levelUpInitPosition;
    }
    #endregion
}
