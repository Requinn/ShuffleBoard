using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    [SerializeField]
    private Text _scoreText;


    public void UpdateTextSolo(int score) {
        _scoreText.text = "\nPlayer 1: " + score;
    }

    public void UpdateTextDoubles(int p1Score, int p2Score) {
        _scoreText.text = "Player 1: " + p1Score + "\n\nPlayer 2: " + p2Score;
    }

    public void StateWinner(int winner) {
        if (winner != 0) {
            _scoreText.text = "\nPlayer " + winner + " wins!";
        }
        else {
            _scoreText.text = "\nDraw!";
        }
    }
}
