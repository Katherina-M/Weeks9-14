using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public SpriteRenderer playerBoarder;


    private void Start()
    {
        if (playerBoarder != null)
        {
            playerBoarder.enabled = false;
        }
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
            Debug.Log("Player border enabled.");
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
    }

}
