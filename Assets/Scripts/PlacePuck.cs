using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When turn starts, draw a marker to place a puck. Click to put down.
/// Track whose puck it is we are placing down
/// </summary>
public class PlacePuck : MonoBehaviour {
    private bool _isPlacingPuck = false;
    [SerializeField]
    private Puck _puckPrefab;
    private Puck _puckPlacing;

    public Puck StartPlactingNew() {
        _isPlacingPuck = true;
        _puckPlacing = Instantiate(_puckPrefab, new Vector3(11.5f, 0.61f, 0), Quaternion.identity);
        _puckPlacing.updateScore += GameController.Instance.CalculateScore;
        _puckPlacing.SetOwner(GameController.Instance.GetCurrentPlayer());
        _puckPlacing.GetComponent<Rigidbody>().detectCollisions = false;
        return _puckPlacing;
    }

	// Update is called once per frame
	void Update () {
        if (_isPlacingPuck) {
            //clamp x to 11 and 12
            //clamp z to 2 and -2
            Vector3 mousePos = MousePositionHandler.Instance.GetMousePosition();
            Vector3 clampedMousePosition = new Vector3(Mathf.Clamp(mousePos.x, 11f, 12f), 0.61f, Mathf.Clamp(mousePos.z, -1.7f, 1.7f));
            _puckPlacing.transform.position = clampedMousePosition;
            if (Input.GetMouseButtonDown(0)) {
                _isPlacingPuck = false;
                _puckPlacing.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }
	}
}
