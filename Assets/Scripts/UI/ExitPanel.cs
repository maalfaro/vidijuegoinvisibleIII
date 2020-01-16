using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExitPanel : MonoBehaviour
{

	[SerializeField] private Button exitButton;
	[SerializeField] private Button continueButton;

	private void Awake()
	{
		exitButton.onClick.AddListener(Exit);
		continueButton.onClick.AddListener(Continue);
	}

	public void Hide()
    {
		gameObject.SetActive(false);
    }

    public void Show()
    {
		gameObject.SetActive(true);
	}


	private void Exit()
	{
		SoundsManager.Instance.PlaySound("click");
		Application.Quit();
	}

	private void Continue()
	{
		SoundsManager.Instance.PlaySound("click");
		GameManager.Instance.GamePaused = false;
	}

}
