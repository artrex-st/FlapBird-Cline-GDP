using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig Configuration", menuName = "Config/GameMode")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField]
    public float ForwardSpeed { get; private set; } = 4;
    
    [field: SerializeField]
    public float GravityScale { get; private set; } = 25;

    [field: SerializeField]
    public float JumpForce { get; private set; } = 10;

    [field: SerializeField]
    public float RotationZSpeed { get; private set; } = 90;
    [field: SerializeField]
    public float FlapRoration { get; private set; } = 30;
    
}
