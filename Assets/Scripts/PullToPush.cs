using System;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

/// <summary>
/// Class to handle the pushing of the puck. Hold mouse 1 to pull, then release to push.
/// This goes on the puck. Draw an arrow from the puck, opposite of the direction pulled, size scaling to applied force.
/// </summary>
public class PullToPush : MonoBehaviour {
    //acts on the rigidbody?
	// Use this for initialization
    [SerializeField]
    private float _maximumForce = 50f;
    [SerializeField]
    private float _distanceToForceconversionRate = 0.25f;
    [SerializeField]
    private GameObject _marker;
    private Vector3 _originalMousePosition, _currentMousePosition, _forceDirection, _forceToApply;
    private float distance;

	void Start () {
        _marker.transform.localScale = new Vector3(0, 1.5f, 1f);
        _marker.SetActive(false);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetMouseButtonDown(0)) {
            _originalMousePosition = MousePositionHandler.Instance.GetMousePosition();
        }
        if (Input.GetMouseButton(0)) {
            _currentMousePosition = MousePositionHandler.Instance.GetMousePosition();
            CalculateForce();
            DrawForceLine();
        }
        if (Input.GetMouseButtonUp(0)) {
            ApplyForceToPuck();
        }
    }

    private void CalculateForce() {
        _forceDirection = (_originalMousePosition - _currentMousePosition).normalized;
        distance = Vector3.SqrMagnitude(_originalMousePosition - _currentMousePosition);
        float calculatedForce = (distance/4) * _distanceToForceconversionRate;
        calculatedForce = Mathf.Clamp(calculatedForce, 0, _maximumForce);
        _forceToApply = _forceDirection * calculatedForce;
    }

    private void ApplyForceToPuck() {
        _marker.SetActive(false);
        //distance to force calculations here
        Debug.Log("Force applied " + _forceToApply);
        GetComponent<Rigidbody>().AddForce(_forceToApply / 2, ForceMode.Impulse);
        Timing.RunCoroutine(DelayedSetActive());
        this.enabled = false;
    }

    private IEnumerator<float> DelayedSetActive() {
        yield return Timing.WaitForSeconds(0.1f);
        GetComponent<Puck>().isActive = true;
        yield return 0f;
    }

    float scale, angle;
    private void DrawForceLine() {
        _marker.SetActive(true);
        //scale an arrow texture from 0 to 5? in relation to the force that is to apply
        scale = _forceToApply.sqrMagnitude / 100f;
        //angle of force
        angle = Quaternion.FromToRotation(transform.position, -_forceDirection).eulerAngles.y;
        //Debug.Log(angle);
        _marker.transform.localScale = new Vector3(-scale, 1.5f, 1f);
        _marker.transform.rotation = Quaternion.Euler(0, angle, 0);
        //Debug.DrawRay(transform.position, transform.position * scale * angle, Color.red);
    }
}
