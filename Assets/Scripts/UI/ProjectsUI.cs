using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectsUI : MonoBehaviour
{
	private Text projectsText;

	private void Awake()
	{
		projectsText = GetComponent<Text>();
	}

	private void OnEnable()
	{
		GameManager.OnProjectsChange += SetProjects;
	}

	private void OnDisable()
	{
		GameManager.OnProjectsChange -= SetProjects;
	}

	private void SetProjects(int projects)
	{
		projectsText.text = string.Format("{0} proyectos desarrollados", projects);
	}

}
