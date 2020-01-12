using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

	[SerializeField] private Text projectsText;

	public void InitPanel()
	{
		Hide();
	}

	public void Show()
	{
		projectsText.text = string.Format("{0} proyectos desarrollados", GameManager.Instance.ProjectsCount);
		gameObject.SetActive(true);
		StartCoroutine(ResetGame());
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	IEnumerator ResetGame()
	{
		yield return new WaitForSeconds(5);
		UIManager.Instance.ShowGameplayPanel();
		GameManager.Instance.ResetGame();
	}
}
