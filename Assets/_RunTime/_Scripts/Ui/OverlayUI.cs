using UnityEngine;

public class OverlayUI : MonoBehaviour
{
    [SerializeField] protected HudController hudController;
    protected void GetHudController()
    {
        hudController = hudController != null ? hudController : GetComponentInParent<HudController>();
    }
}
