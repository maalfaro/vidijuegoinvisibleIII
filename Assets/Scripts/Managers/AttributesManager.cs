using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{

	public float[] AttributeValues
	{
		get;
		private set;
	}

	public void Initialize()
	{
		AttributeValues = new float[System.Enum.GetValues(typeof(GlobalData.Attributes)).Length];
		for (int i = 0; i < AttributeValues.Length; i++)
		{
			AttributeValues[i] = 5;
		}
	}


	public void ChangeAttributeValue(GlobalData.AttributesEffect attributeEffect)
	{
		AttributeValues[(int)attributeEffect.attribute] += attributeEffect.amount;
	}

	public float GetAttributeAmount(GlobalData.Attributes attribute)
	{
		return AttributeValues[(int)attribute]/10f;
	}

}
