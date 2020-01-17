using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour, IPointerClickHandler
{

	[SerializeField] private Text projectsText;
	[SerializeField] private Text clickText;
	[SerializeField] private Text firedText;
	[SerializeField] private List<Text> scores;

	private bool cickable;

	public void InitPanel()
	{
		Hide();
	}

	public void Show(bool playerWin)
	{
		if (playerWin) ShowWin();
		else ShowGameOver();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	
	private void ShowWin()
	{
		cickable = false;
		clickText.gameObject.SetActive(false);
		projectsText.text = "HAS COMPLETADO TODOS LOS PROYECTOS";
		firedText.text = "ENHORABUENA";
		gameObject.SetActive(true);
		ShowScores();
		StartCoroutine(ShowClickText());
	}

	private void ShowGameOver() {
		cickable = false;
		clickText.gameObject.SetActive(false);
		projectsText.text = string.Format("{0} proyectos desarrollados", GameManager.Instance.ProjectsCount);
		gameObject.SetActive(true);
		firedText.text = "HAS SIDO DESPEDIDO";
		ShowScores();
		StartCoroutine(ShowClickText());
	}

	private void ShowScores()
	{
		for(int i = 0; i < scores.Count; i++)
		{
			scores[i].text = string.Format("{0} proyectos desarrollados", GameManager.Instance.Scores[i]); 
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!cickable) return;

		SoundsManager.Instance.PlaySound("click");

		clickText.gameObject.SetActive(false);
		UIManager.Instance.ShowGameplayPanel();
		GameManager.Instance.ResetGame();
	}

	IEnumerator ShowClickText()
	{
		yield return new WaitForSeconds(5);
		cickable = true;
		clickText.gameObject.SetActive(true);
	}

}
