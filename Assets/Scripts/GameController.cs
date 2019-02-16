using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles scores and turns and game state.
/// </summary>
public class GameController : MonoBehaviour {
    private int _currentPlayer = 1;
    private int _p1Score = 0, _p2Score = 0, _roundsLeft = 4;
    public int PlayerCount = 2;
    public static GameController Instance;
    [SerializeField]
    private ScoreBoard _board;
    [SerializeField]
    private PlacePuck _puckPlacer;
    private List<Puck> _p1ActivePucks = new List<Puck>();
    private List<Puck> _p2ActivePucks = new List<Puck>();
    [SerializeField]
    private GameObject _resetbutton;

    public int GetCurrentPlayer() {
        return _currentPlayer;
    }

    // Use this for initialization
	void Start () {
        Instance = this;
        ResetGame();
	}

    /// <summary>
    /// calculate score by goignt hrough the list of owned pucks. Done this way because the score can change at any time during gameplay, by collisions etc.
    /// </summary>
    /// <param name="owner"></param>
    public void CalculateScore(int owner) {
        if(owner == 1) {
            _p1Score = 0;
            foreach(Puck p in _p1ActivePucks) {
                _p1Score += p.Value;
            }
        }
        else {
            _p2Score = 0;
            foreach (Puck p in _p2ActivePucks) {
                _p2Score += p.Value;
            }
        }
        UpdateScoreBoard();
    }

    /// <summary>
    /// When the player starts, spawn a puck and add it into the puck list
    /// </summary>
    private void StartTurn() {
        if (_currentPlayer == 1) {
            _p1ActivePucks.Add(_puckPlacer.StartPlactingNew());
        }
        else {
            _p2ActivePucks.Add(_puckPlacer.StartPlactingNew());
        }
    }

    int _turnCount = 0;
    /// <summary>
    /// Changing turns, and also checking if we are out of rounds
    /// </summary>
    public void ChangeTurn() {
        if (PlayerCount == 2) {
            if (_currentPlayer == 1) {
                _currentPlayer = 2;
            }
            else {
                _currentPlayer = 1;
            }
        }

        _turnCount++;
        if(_turnCount > PlayerCount) {
            _turnCount = 0;
            _roundsLeft--;
        }

        if (_roundsLeft == 0) {
            GameOver();
        }
        else {
            StartTurn();
        }
    }

    private void GameOver() {
        _resetbutton.SetActive(true);
        if (_p1Score > _p2Score) {
            _board.StateWinner(1);
        }
        else if(_p2Score > _p1Score) {
            _board.StateWinner(2);
        }
        else {
            _board.StateWinner(0);
        }
    }

    public void ResetGame() {
        _resetbutton.SetActive(false);
        foreach (var p in _p1ActivePucks) {
            Destroy(p.gameObject);
        }
        foreach (var p in _p2ActivePucks) {
            Destroy(p.gameObject);
        }
        _p1ActivePucks.Clear();
        _p2ActivePucks.Clear();

        _p1Score = 0;
        _p2Score = 0;
        UpdateScoreBoard();
        _currentPlayer = 1;
        _roundsLeft = 4;

        StartTurn();
    }

    private void UpdateScoreBoard() {
        if(PlayerCount == 1) {
            _board.UpdateTextSolo(_p1Score);
        }
        else {
            _board.UpdateTextDoubles(_p1Score, _p2Score);
        }
    }
}
