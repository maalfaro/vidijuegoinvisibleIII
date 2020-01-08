using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour
{

	[SerializeField] private GlobalData.Attributes attribute;
	[SerializeField] private Image fillImage;
	[SerializeField] private Image bigAmount;
	[SerializeField] private Image littleAmount;

	public void Initialize()
	{
		SetAmount(0.5f);
		ActiveBigAmount(false);
		ActiveLittleAmount(false);
	}

	public void SetAmount(float amount)
	{
		fillImage.fillAmount = amount;
	}

	public void ActiveBigAmount(bool active)
	{
		bigAmount.gameObject.SetActive(active);
	}

	public void ActiveLittleAmount(bool active)
	{
		littleAmount.gameObject.SetActive(active);
	}




}
