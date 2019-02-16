using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// holds dataabout the puck. how many points it is worth and who it belongs to. Should have a thing where every point change/knockout updates the score, as collisions are legal.
/// </summary>
public class Puck : MonoBehaviour {
    private int _owner;
    private int _score;
    public Material[] _playerMaterials;
    public bool isActive = false;
    private Rigidbody _rb;

    public delegate void ScoreUpdatedEvent(int owner);
    public ScoreUpdatedEvent updateScore;

    public int Value {
        get { return _score; }
    }

    public void SetValue(int value) {
        _score = value;
        updateScore(_owner);
    }

    public void SetOwner(int owner) {
        _owner = owner;
        GetComponent<Renderer>().material = _playerMaterials[owner - 1];
        _rb = GetComponent<Rigidbody>();
    }

    public void LateUpdate() {
        if (isActive && _rb.velocity.sqrMagnitude == 0f) {
            isActive = false;
            CheckForValid();
            GameController.Instance.ChangeTurn();
        }
    }

    /// <summary>
    /// https://forum.unity.com/threads/how-do-i-find-the-closest-point-on-a-line.340058/
    /// </summary>
    private void CheckForValid() {
        Vector3 originPoint = new Vector3(12.5f, 0.61f, 0f);
        Debug.Log(Vector3.Magnitude(transform.position - originPoint));
        if(Vector3.Magnitude(transform.position - originPoint) < 2.27f) {
            _rb.detectCollisions = false;
        }
    }
}
