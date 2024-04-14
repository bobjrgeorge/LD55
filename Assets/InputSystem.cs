using UnityEngine;
using UnityEngine.Windows;

public class InputSystem : MonoBehaviour
{
    private static InputSystem _instance;
    public static InputSystem Instance { get { return _instance; } }

    private Inputs playermovement;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        playermovement = new Inputs();
    }

    private void OnEnable()
    {
        playermovement.Enable();
    }

    private void OnDisable()
    {
        playermovement.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playermovement.Player.Movement.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playermovement.Player.Jump.triggered;
    }
    public bool PlayerAttacked()
    {
        return playermovement.Player.Attack.triggered;
    }
    public bool PlayerSummonOne()
    {
        return playermovement.Player.SummonOne.triggered;
    }
    public bool PlayerSummonTwo()
    {
        return playermovement.Player.SummonTwo.triggered;
    }

    public bool PlayerInteract()
    {
        return playermovement.Player.Interact.triggered;
    }
}
