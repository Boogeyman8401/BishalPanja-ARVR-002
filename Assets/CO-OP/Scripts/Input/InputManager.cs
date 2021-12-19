using UnityEngine;

namespace Alpha
{
    public class InputManager : MonoBehaviour
    {
        #region Variables

        protected float horizontalInput;
        protected float verticalInput;
        protected float mouseX;
        protected float mouseY;

        
        protected float horizontalRightStick;
        protected float verticalRightStick;


        public bool Shoot { get; private set; }
        public bool ShootTap { get; private set; }
        public bool Reload { get; private set; }


        [SerializeField] private Vector2 horizontalVerticalInput;
        [SerializeField] private Vector2 mouse;
        [SerializeField] private Vector2 rightStick;

        private PlayerInput playerInput;
        #endregion

        #region Properties
        public float HorizontalInput { get { return horizontalInput; } }
        public float VerticalInput { get { return verticalInput; } }
        public float MouseX { get { return mouseX; } }
        public float MouseY { get { return mouseY; } }

        

        public float HorizontalRightStick { get { return horizontalRightStick; } }
        public float VerticalRightStick { get { return verticalRightStick; } }



        #endregion

        #region Builtin Methods
        private void OnEnable()
        {
            playerInput.Player1.Enable();
            playerInput.Player1.Movement.performed += _ => GetPlayerInput();
            playerInput.Player1.Movement.canceled += _ => GetPlayerInput();

            playerInput.Player1.MouseRotation.performed += _ => GetPlayerInput();
            playerInput.Player1.MouseRotation.canceled += _ => GetPlayerInput();

            //playerInput.Player2.Enable();
            //playerInput.Player2.Movement.performed += _ => GetLeftStick();
            //playerInput.Player2.Movement.canceled += _ => GetLeftStick();

            //playerInput.Player2.MouseRotation.performed += _ => GetRightStickInput();
            //playerInput.Player2.MouseRotation.canceled += _ => GetRightStickInput();

            playerInput.Player1.Shoot.performed += ctx => Shoot = true;
            playerInput.Player1.Shoot.canceled += ctx => Shoot = false;
            
            //playerInput.Player1.ShootTap.performed += ctx => ShootTap = true;
            //playerInput.Player1.ShootTap.canceled += ctx => ShootTap = false;

            playerInput.Player1.Reload.performed += ctx => Reload = true;
            playerInput.Player1.Reload.canceled += ctx => Reload = false;





        }

        private void OnDisable()
        {
            playerInput.Player1.Movement.performed -= _ => GetPlayerInput();
            playerInput.Player1.Movement.canceled -= _ => GetPlayerInput();

            playerInput.Player1.MouseRotation.performed -= _ => GetPlayerInput();
            playerInput.Player1.MouseRotation.canceled -= _ => GetPlayerInput();
            playerInput.Player1.Disable();


            //playerInput.Player2.MouseRotation.performed -= _ => GetRightStickInput();
            //playerInput.Player2.MouseRotation.canceled -= _ => GetRightStickInput();
            //playerInput.Player2.Disable();

        }
        private void Awake()
        {
            playerInput = new PlayerInput();
        }
        
        #endregion

        #region Custom Methods
        


        void GetPlayerInput()
        {
            horizontalVerticalInput = playerInput.Player1.Movement.ReadValue<Vector2>();
            mouse = playerInput.Player1.MouseRotation.ReadValue<Vector2>();

            mouseX = mouse.x;
            mouseY = mouse.y;
            horizontalInput = horizontalVerticalInput.x;
            verticalInput = horizontalVerticalInput.y;
            
        }
        void GetLeftStick()
        {
            horizontalVerticalInput = playerInput.Player2.Movement.ReadValue<Vector2>();
            horizontalInput = horizontalVerticalInput.x;
            verticalInput = horizontalVerticalInput.y;
        }
        void GetRightStickInput()
        {
            rightStick = playerInput.Player2.MouseRotation.ReadValue<Vector2>();
            horizontalRightStick = rightStick.x;
            verticalRightStick = rightStick.y;
        }

        #endregion
    }
}