using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Sprite[] lives;
    [SerializeField]
    private Image _livesImageDisplay;
    [SerializeField] 
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject titleScreen;
    [FormerlySerializedAs("_score")] [HideInInspector]
    public int score=0;

    
    public void UpdateLives(int currentLives)
    {
        if (currentLives >= 0)
        {
            _livesImageDisplay.sprite = lives[currentLives];
        }
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }
    
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);

    }
}
   