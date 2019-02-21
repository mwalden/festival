using UnityEngine;
using System.Collections;

public class DanceScript : MonoBehaviour {
	[SerializeField]
	private Animator anim;
	Vector3 positionbeforeDancing;
	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void StartDance(){
		positionbeforeDancing = gameObject.transform.localPosition;
		anim.SetTrigger ("dance");
	}
	public void EndDance(){
		positionbeforeDancing = new Vector3 (0, 0, 0);
		anim.SetTrigger ("stopDance");
		gameObject.transform.localPosition = positionbeforeDancing;
	}

}
