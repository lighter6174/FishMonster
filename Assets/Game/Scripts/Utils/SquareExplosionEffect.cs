using UnityEngine;

public class SquareExplosionEffect : MonoBehaviour
{
    private void Start()
    {
    }

    public void OnPlayFinished()
    {
        Invoke("DestroySelf", 0.3f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}