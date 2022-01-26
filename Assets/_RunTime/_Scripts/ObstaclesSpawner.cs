using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private Obstacles _firstObstacle;
    [SerializeField] private Obstacles[] _obstaclePrefab;
    [SerializeField] private PlayerController _playerController;
    private ObjectPool<Obstacles> _pool;
    public List<Obstacles> _obstaclesList;
    //
    [SerializeField] private float _minDistanceToConsiderInsideObstacle;
    [SerializeField] private int _initialObstaclesCount, _minObstaclesInFrontOfPlayer;

    private void Awake()
    {
        _pool = new ObjectPool<Obstacles>(() => {
            int rng = Random.Range(0,_obstaclePrefab.Length);
            return Instantiate(_obstaclePrefab[rng]);

        }, Obstacles => {
            Obstacles.gameObject.SetActive(true);

        }, Obstacles => {
            Obstacles.gameObject.SetActive(true);

        }, Obstacles => {
            Destroy(Obstacles.gameObject);

        }, false, 10, 20);
    }
    private void Start()
    {
        _SpawnObstacles(_initialObstaclesCount);
    }
    
    private void Update()
    {
        _UpdateObstacles();
    }

    private void _UpdateObstacles()
    {
        int playerIndex = _GetPlayerIndex();
        
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
        } 
        _obstaclesList.Add(obstacle);
        return obstacle;
    }

    private int _GetPlayerIndex()
    {
        for (int i = 0; i < _obstaclesList.Count; i++)
        {
            Obstacles instanceObstacle = _obstaclesList[i];
            if (_playerController.transform.position.x >= instanceObstacle._startPoint.position.x + _minDistanceToConsiderInsideObstacle
                && _playerController.transform.position.x <= instanceObstacle._endPoint.position.x)
            {
                return i;
            }
        }
        return -1;
    }
}
