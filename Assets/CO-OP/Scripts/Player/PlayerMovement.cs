using UnityEngine;

namespace Alpha
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [Header("Movement Properties")]
        public bool player1 = true;
        public float speed = 40f;
        public float NormalAngularDrag = 3;
        public float shootingAngularDrag = 35;
        public float shootDrag = 2f;
        [Range(0, 1)]
        public float multiplier;

        [Range(0, 1)]
        public float animationDampTime;
        private Animator anim;

        [Space]
        [Header("GroundCheck")]
        public LayerMask whatisGround;
        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;



        [Space]
        [Header("Features")]
        public bool useDiagonalRotation = true;
        public bool usePlayerInputToRotate = true;

        [Space]
        [Header("Status")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isMoving = false;

        public Gun gun;

        [SerializeField]private float rotationSpeed = 2;
        [SerializeField] private float turnSpeed=10;

        private Vector3 desiredDirection;
        private Rigidbody rb;
        private InputManager inputManager;
        private Vector2 hvInput;
        private float roty;
        private float mousey;
        private float startDrag;
        public float walkSpeed;
        #endregion

        #region Properties
        public bool IsMoving { get { return isMoving; } }

        #endregion

        #region Builtin Methods
        private void Awake()
        {
            InitalizeOnStart();
        }

        private void Start()
        {
           // Cursor.lockState = CursorLockMode.Locked;
            startDrag = rb.drag;

        }

        private void Update()
        {
            PlayerStatus();
            CheckSurroundings();

            
            GetInput();
            UpdateAnimations();
            if (!gun.IsShooting && usePlayerInputToRotate && (inputManager.MouseX == 0 || inputManager.MouseX == 0))
                RotatePlayer();
            else
                GetRotationToPlayer();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            GetRotationToPlayer();
        }
        #endregion


        #region Custom Methods
        void InitalizeOnStart()
        {
            rb = GetComponent<Rigidbody>();
           
            inputManager = GetComponent<InputManager>();
            if (anim == null)
                anim = GetComponentInChildren<Animator>();
        }
        void ApplyMovement()
        {
            float magnitude = Mathf.Clamp01(desiredDirection.magnitude);

            desiredDirection.Normalize();
            desiredDirection *= walkSpeed * magnitude;

            if (isGrounded)
            {
                rb.AddForce(desiredDirection);
                //rb.AddRelativeForce(desiredDirection);
            }
            else
            {
                if (rb.velocity.magnitude < 0.1f)
                {
                    rb.velocity = Vector3.zero;
                }

            }
        }


        void GetInput()
        {
            desiredDirection = Vector3.zero;

            hvInput.x = inputManager.HorizontalInput;
            hvInput.y = inputManager.VerticalInput;
            desiredDirection = new Vector3(hvInput.x, 0f, hvInput.y);

            if (inputManager.Shoot && isMoving)
            {

                rb.angularDrag = shootingAngularDrag;
                rb.drag = startDrag + shootDrag;
            }
            else
            {
                rb.angularDrag = NormalAngularDrag;
                rb.drag = startDrag;
            }



        }

        void CheckSurroundings()
        {
            RaycastHit hit;
            if (Physics.Raycast(groundCheck.transform.position, Vector3.down, out hit, groundCheckRadius, whatisGround.value))
            {
                isGrounded = true;

            }
            else
            {
                isGrounded = false;
            }
        }


        void UpdateAnimations()
        {
            Vector3 localVelocity = Vector3.zero;
            if (isMoving)
                localVelocity = rb.transform.InverseTransformDirection(rb.velocity);


            anim.SetFloat("Horizontal", Mathf.Clamp(localVelocity.x, -1f, 1f), animationDampTime, Time.deltaTime);
            anim.SetFloat("Vertical", Mathf.Clamp(localVelocity.z, -1f, 1f), animationDampTime, Time.deltaTime);
            anim.SetBool("Reload", gun.Reloading);
            //anim.SetBool("Shooting", gun.IsShooting);
        }

        void RotatePlayer()
        {
            //Quaternion currentRotation = transform.localRotation;
            
            if (desiredDirection != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, turnSpeed * Time.deltaTime);
            }

        }

        void GetRotationToPlayer()
        {
            if (useDiagonalRotation)
            {
                DiagonalRotation();
            }
            else
            {
                RotateToPlayerInputDirection();

            }
        }

        void DiagonalRotation()
        {

            mousey = inputManager.MouseX;
            //mousey = Mathf.Clamp(mousey, -1f, 1f);
            roty += mousey;
            if (mousey != 0)
                transform.localRotation = Quaternion.Euler(0f, roty * rotationSpeed, 0f);
        }

        void RotateToPlayerInputDirection()
        {

            Vector3 currentRotationInput = new Vector3(inputManager.HorizontalRightStick, 0, inputManager.VerticalRightStick);

            if (currentRotationInput != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(currentRotationInput);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, rotationSpeed * multiplier);
            }

        }

        void PlayerStatus()
        {

            if (hvInput.x == 0 && hvInput.y == 0)
            {
                isMoving = false;
                rb.isKinematic = true;

            }
            else
            {
                rb.isKinematic = false;
                isMoving = true;
            }

            if (inputManager.MouseX != 0 && inputManager.MouseY != 0)
                rb.isKinematic = false;

        }
        #endregion
    }
}
