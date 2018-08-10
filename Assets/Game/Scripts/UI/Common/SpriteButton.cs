using DG.Tweening;
using UnityEngine;

public class SpriteButton : MonoBehaviour
{
    public string[] eventTypes;
    public float clickScale = 1.2f;
    public float animInterval = 0.3f;

    private void OnMouseDown()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.one * clickScale, animInterval);
    }

    private void OnMouseUp()
    {
        transform.DOScale(Vector3.one, animInterval);
    }

    private void OnMouseUpAsButton()
    {
        if (eventTypes != null)
        {
            foreach (var eventType in eventTypes)
            {
                Game.EventManager.Send(eventType);
            }
        }
    }
}