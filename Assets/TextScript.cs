using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {
	int pos = 0;
	public Text[] textBoxes;

	public void onTextAdded(string msg){
		if (pos == textBoxes.Length)
			pos--;
		Text text = textBoxes [pos];
		text.text = msg;
		print (pos + " :: " + textBoxes.Length);
		pos++;
	}
}
