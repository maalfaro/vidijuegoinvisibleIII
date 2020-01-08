using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesUI : MonoBehaviour {

	#region Members

	[SerializeField]
	private GameObject leftChoice;

	[SerializeField]
	private GameObject rightChoice;

	[SerializeField]
	private Text leftText;

	[SerializeField]
	private Text rightText;

	#endregion

	#region Public methods

	public void DisableChoices() {
		leftChoice.SetActive(false);
		rightChoice.SetActive(false);
	}

	public void EnableLeftChoice() {
		leftChoice.SetActive(true);
		rightChoice.SetActive(false);
	}

	public void EnableRightChoice() {
		rightChoice.SetActive(true);
		leftChoice.SetActive(false);
	}

	public void SetChoices(string left, string right) {
		leftText.text = left;
		rightText.text = right;
	}

	#endregion
}
