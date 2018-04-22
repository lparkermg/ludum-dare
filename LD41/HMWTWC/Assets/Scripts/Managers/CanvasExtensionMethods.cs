using UnityEngine;

public static class CanvasExtensionMethods
{
    public static void Show(this CanvasGroup c)
    {
        c.alpha = 1.0f;
        c.blocksRaycasts = true;
        c.interactable = true;
    }

    public static void Hide(this CanvasGroup c)
    {
        c.alpha = 0.0f;
        c.blocksRaycasts = false;
        c.interactable = false;
    }
}