using UnityEngine;

namespace Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void SetActive(this CanvasGroup canvasGroup, bool state)
        {
            canvasGroup.alpha = state ? 1 : 0;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
    }
}