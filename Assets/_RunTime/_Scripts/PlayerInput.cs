using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerTap();
    public event PlayerTap OnTap;

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTap?.Invoke();
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                OnTap?.Invoke();
            }
        }
    }
}
