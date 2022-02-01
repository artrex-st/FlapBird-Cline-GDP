using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject _pipes;
    [SerializeField] public Transform _startPoint, _endPoint;
    [SerializeField] private int _minimumYposition = -2;
    [SerializeField] private int _maximumYposition = 3;
    //
    [SerializeField] private AudioSource _obstacleAudioSource;
    [SerializeField] private AudioClip _hitSound;
    public bool IsActiveObstacle => _pipes.activeInHierarchy;

    public void DisablePipe()
    {
        _pipes.SetActive(false);
    }
    public void HitSound()
    {
        _obstacleAudioSource.PlayOneShot(_hitSound);
        Debug.Log("Obstacle");
    }
    private void OnDisable()
    {
        _pipes.SetActive(true);
        _ShamblesPipes();
    }
    private void Update()
    {
        if (GameStateManager.Instance.CurrentGameState.Equals(GameStates.GAME_WAITING))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
    }

    private void _ShamblesPipes()
    {
        int rngY = Random.Range(_minimumYposition,_maximumYposition);
        _pipes.transform.position = new Vector3(_pipes.transform.position.x, rngY, 0);
    }
}
