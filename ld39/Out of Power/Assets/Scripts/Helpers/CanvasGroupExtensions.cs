using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
	public static class CanvasGroupExtensions
	{

		public static void Hide(this CanvasGroup c)
		{
			c.alpha = 0.0f;
			c.interactable = false;
			c.blocksRaycasts = false;
		}

		public static void Show(this CanvasGroup c)
		{
			c.alpha = 1.0f;
			c.interactable = true;
			c.blocksRaycasts = true;
		}
	}
}
