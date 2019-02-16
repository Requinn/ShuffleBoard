using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When a puck enters this collider, its point value will change. glow on enter
/// </summary>
public class ScoreZone : MonoBehaviour {
    [SerializeField]
    private int _score = 1;

    /// <summary>
    /// on exit because the puck needs to be fully through the line? or maybe just do a distance check when the puck stops from the edge of the starting side.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) {
        //if other is a puck, adjust its score value
        other.GetComponent<Puck>().SetValue(_score);
    }
}
