using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script handle the Menu buttons in Menu scene.
/// </summary>

public class MenuHandler : MonoBehaviour
{
    #region SERIALIZE FILEDS

    [SerializeField] GameObject settingPanel;

    [SerializeField] Text scoreText;

    #endregion

    #region PRIVATE FIELDS

   
    GameManager gameManager;

    int score;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        gameManager = GameManager.GetInstance();

        gameManager.SetGameState(GAMESTATE.UI);

        score = PlayerPrefs.GetInt(GameManager.highestScoreKey);

        scoreText.text = "Highest Score :- " + score;
 
    }

    #endregion

    #region BUTTONS ACTIONS

    // Button Action for play
    public void OnClickPlay()
    {
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);

        SceneManager.LoadSceneAsync((int)GAMESTATE.GAMEPLAY );   
    }

    // Button Action for Quit
    public void OnClickQuit()
    {
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);

        Application.Quit();
    }

    #endregion
}
