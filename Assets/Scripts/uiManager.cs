using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class uiManager : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public SpriteRenderer playerBoarder;
    public EnemySystem[] enemies;
    public CanvasGroup gameOverCanvas;



    private void Start()
    {
        if (playerBoarder != null)
        {
            playerBoarder.enabled = false;
        }

        HideGameOverScreen();
    }
    public void ApplyInvisibilityEffect()
    {
        //Make the player half transparent
        if (playerSprite != null)
        {
            Color newColor = playerSprite.color;
            newColor.a = 0.5f;
            playerSprite.color = newColor;
            Debug.Log("Player half transparent");
        }
        //Enable yellow boarder
        if (playerBoarder != null)
        {
            playerBoarder.enabled = true;
        }

        //Start countdown Coroutine to reset ability
        StartCoroutine(InvisibilityDurationCoroutine());
    }

    private IEnumerator InvisibilityDurationCoroutine()
    {
        yield return new WaitForSeconds(10f); // 10 sec aility
        ResetPlayerVisuals();

        //Restart Enemy tracing
        foreach (EnemySystem enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.ResumeTrackingPlayer();
            }
        }
    }

    //Listener
    public void OnPlayerBecameInvisible()
    {
        ApplyInvisibilityEffect();
    }

    public void ResetPlayerVisuals()
    {
        //Reset player color
        if (playerSprite != null)
        {
            Color resetColor = playerSprite.color;
            resetColor.a = 1f;
            playerSprite.color = resetColor;
        }

        //Disable Yellow boarder
        if (playerBoarder != null)
        {
            playerBoarder.enabled = false;
        }

        Debug.Log("Invisibility effect reset.");

    }

    public void ShowGameOverScreen()
    {
        //Show game over when player die
        gameOverCanvas.alpha = 1;
        gameOverCanvas.interactable = true;
        gameOverCanvas.blocksRaycasts = true;
    }

    public void HideGameOverScreen()
    {
        //Hide game over screen at the start of the game
        gameOverCanvas.alpha = 0;
        gameOverCanvas.interactable = false;
        gameOverCanvas.blocksRaycasts = false;
    }

    public void RestartGame()
    {
        //Reload game screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
