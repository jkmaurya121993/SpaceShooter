using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script handle the Menu buttons in Menu scene.
/// </summary>

public class MenuHandler : MonoBehaviour
{
    #region SERIALIZE FILEDS
    [SerializeField] Text scoreText;
    #endregion

    #region PRIVATE FIELDS
     private GameManager gameManager;
     private int score;
    #endregion

    #region UNITY METHODS

    private void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.SetGameState(GAMESTATE.UI);

        score = PlayerPrefs.GetInt(GameManager.highestScoreKey);

        scoreText.text = "Highest Score :- " + score;
 
    }

    #endregion

    #region BUTTONS ACTIONS
    ///<summary>
    /// Button Action for play
    /// </summary>
    public void OnClickPlayButton()
    {
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);

        SceneManager.LoadSceneAsync((int)GAMESTATE.GAMEPLAY );   
    }
    ///<summary>
    /// Button Action for Quit
    /// </summary>
    public void OnClickQuitButton()
    {
        gameManager.PlayAudio(AUDIOTYPE.BUTTON);

        Application.Quit();
    }

    #endregion
}
