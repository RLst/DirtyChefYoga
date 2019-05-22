using UnityEngine;

namespace DirtyChefYoga
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        CharacterController controller;
        PlayerInput input;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInput>();
        }

        void Update()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            controller.Move(new Vector3(input.move * speed * Time.deltaTime, 0, 0));
        }

    }
}