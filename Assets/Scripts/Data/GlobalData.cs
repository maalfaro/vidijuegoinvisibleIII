using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData 
{
   
	public enum Attributes
	{
		Publisher, Community, Team, Money
	}
	

	[System.Serializable]
	public class AttributesEffect
	{
		public Attributes attribute;
		public int amount;
	}


}
