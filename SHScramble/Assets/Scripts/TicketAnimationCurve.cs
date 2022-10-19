using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketAnimationCurve : MonoBehaviour{
	public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	float elapsed = 0f;
	Vector3 startScale;

	void Start(){
		startScale = transform.localScale;
	}

	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.localScale = startScale * curve.Evaluate(elapsed);
		    elapsed += Time.deltaTime;
        }
	}
}
