using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject _pipes;
    [SerializeField] public Transform _startPoint, _endPoint;
    [SerializeField] private int _minimumYposition = -2;
    [SerializeField] private int _maximumYposition = 3;

    public void DisablePipe()
    {
        _pipes.SetActive(false);
    }
    private void OnDisable()
    {
        _pipes.SetActive(true);
        _ShamblesPipes();
    }
    
    private void _ShamblesPipes()
    {
        int rngY = Random.Range(_minimumYposition,_maximumYposition);
        _pipes.transform.position = new Vector3(_pipes.transform.position.x, rngY, 0);
    }
}
