using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

public class AttributeUI : MonoBehaviour
{

	public GlobalData.Attributes attribute;
	[SerializeField] private Image fillImage;
	[SerializeField] private Image bigAmount;
	[SerializeField] private Image littleAmount;
	private Color fillColor;

	public void Initialize()
	{
		SetAmount(0.5f, false);
		ActiveBigAmount(false);
		ActiveLittleAmount(false);

		fillColor = fillImage.color;
	}

	public void SetAmount(float amount, bool animated = true)
	{
		if (fillImage.fillAmount.Equals(amount)) return;
		if (animated)
		{
			StartCoroutine(AnimateAmount(amount));
		}
		else
		{
			fillImage.fillAmount = amount;
		}
	}

	public void ShowModifier(int amount)
	{
		if(amount==0)
		{
			ActiveBigAmount(false);
			ActiveLittleAmount(false);
		}else if (Mathf.Abs(amount) > 1)
		{
			ActiveBigAmount(true);
			ActiveLittleAmount(false);
		}
		else {
			ActiveBigAmount(false);
			ActiveLittleAmount(true);
		}
	}

	private void ActiveBigAmount(bool active)
	{
		bigAmount.gameObject.SetActive(active);
	}

	private void ActiveLittleAmount(bool active)
	{
		littleAmount.gameObject.SetActive(active);
	}

	private void SetFillColor(Color color)
	{
		fillImage.color = color;
	}

	private IEnumerator AnimateAmount(float amount)
	{
		float timer = 0.0f;
		float initialAmount = fillImage.fillAmount;

		if (initialAmount > amount) SetFillColor(Color.red);
		else SetFillColor(Color.green);

		while (timer < 1)
		{
			timer += Time.deltaTime / 0.2f;
			fillImage.fillAmount = Mathf.Lerp(initialAmount, amount, timer);
			yield return null;
		}

		SetFillColor(fillColor);
	}


}
