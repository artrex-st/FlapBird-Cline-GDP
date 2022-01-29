using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclesSpawner : MonoBehaviour
{
    public delegate Vector3 TargetPosition();
    public event TargetPosition OnPositionChange;
    public delegate void ObstacleClear();
    public event ObstacleClear OnPassObstacle;

    [SerializeField] private Obstacles[] _obstaclePrefab;
    private ObjectPool<Obstacles> _pool;
    public List<Obstacles> _obstaclesList;
    //
    [SerializeField] private float _minDistanceToConsiderInsideObstacle;
    [SerializeField] private int _minObstaclesInFrontOfPlayer;

    private void Awake()
    {
        _pool = new ObjectPool<Obstacles>(() => {
            int rng = Random.Range(0, _obstaclePrefab.Length);
            return Instantiate(_obstaclePrefab[rng]);

        }, Obstacles => {
            Obstacles.gameObject.SetActive(true);

        }, Obstacles => {
            Obstacles.gameObject.SetActive(false);

        }, Obstacles => {
            Destroy(Obstacles.gameObject);

        }, false, _minObstaclesInFrontOfPlayer, _minObstaclesInFrontOfPlayer);
    }
    private void Start()
    {
        _SpawnObstacles(_minObstaclesInFrontOfPlayer);
    }
    
    private void Update()
    {
        _UpdateObstacles();
    }

    private void _UpdateObstacles()
    {
        int playerIndex = _GetTargetIndex();
        
        if (playerIndex < 0)
        {
            return;
        }
        int obstaclesInFront = _obstaclesList.Count - (playerIndex + 1);
        if (obstaclesInFront < _minObstaclesInFrontOfPlayer)
        {
            _SpawnObstacles(_minObstaclesInFrontOfPlayer - obstaclesInFront);
        }

        for (int i = 0; i < playerIndex; i++)
        {
            if (_obstaclesList[i + 1].IsActiveObstacle)
            {
                OnPassObstacle?.Invoke();
            }
            _pool.Release(_obstaclesList[i]);
        }
        _obstaclesList.RemoveRange(0, playerIndex);
    }

    private void _SpawnObstacles(int obstaclesCount)
    {
        Obstacles previousObstacle = _obstaclesList.Count > 0 ? _obstaclesList[_obstaclesList.Count - 1] : null;
        for (int i = 0; i < obstaclesCount; i++)
        {
            Obstacles obstacle = _pool.Get();
            obstacle.transform.parent = transform;
            previousObstacle = _ReorganizeObstacleSegment(obstacle, previousObstacle);
        }
    }

    private Obstacles _ReorganizeObstacleSegment(Obstacles obstacle, Obstacles previousObstacle)
    {
        if (previousObstacle != null)
        {
            obstacle.transform.position = previousObstacle._endPoint.position + (obstacle.transform.position - obstacle._startPoint.position);
        }
        else
        {
            obstacle.transform.position = Vector3.zero;
            obstacle.DisablePipe();
        }
        if (GameStateManager.Instance.CurrentGameState.Equals(GameStates.GAME_WAITING))
        {
            obstacle.DisablePipe();
        }
        _obstaclesList.Add(obstacle);
        return obstacle;
    }

    private int _GetTargetIndex()
    {
        for (int i = 0; i < _obstaclesList.Count; i++)
        {
            Vector3 targetPosition = (Vector3)OnPositionChange?.Invoke();
            Obstacles instanceObstacle = _obstaclesList[i];
            if (targetPosition.x >= instanceObstacle._startPoint.position.x + _minDistanceToConsiderInsideObstacle
                && targetPosition.x <= instanceObstacle._endPoint.position.x)
            {
                return i;
            }
        }
        return -1;
    }
}
