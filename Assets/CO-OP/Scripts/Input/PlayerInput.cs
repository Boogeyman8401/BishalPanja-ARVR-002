// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""62db3121-954c-48da-be28-86dbee1efdd1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b15cf74c-db4f-42f4-9a05-f127a1dc263f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRotation"",
                    ""type"": ""Value"",
                    ""id"": ""7c28f433-48fa-481b-a141-8c3159925271"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7f40f8e7-a5d8-462b-8ca5-4902a151e964"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""b4fde541-ccbd-4f4c-b880-96691fa9cfe5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6598e2db-7089-4363-a65b-50dea8f791b9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dae3b331-906e-4aee-89e9-9ffe3301883d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f64f9177-c4c5-4db8-b220-d156f234d381"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f4892f7-04d0-4c33-bee5-1eac6db31251"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fc716de0-7379-4e6c-abbf-4eb568e3d66c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5677d361-0ed5-4d1b-ab84-f7fdf610feda"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98e43843-ef0b-42bf-bcee-ad348e814c8f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f27e8ca1-e9c8-453b-8af5-9f03f5927ed6"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cae6d3ef-1f9f-46ac-b863-43776bf5e3b9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f9b9363-8803-4944-8d4d-40dcc5a14e67"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a233ec2b-48a8-4b5d-a549-66566ea95d23"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0999c1d8-2b06-4ba2-997a-b3f71dd3bfc9"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""0c2c36c8-784f-4d0d-a824-e4fad3a8fabf"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""66a657b7-27f3-4488-b643-ca5b912cc9fe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRotation"",
                    ""type"": ""Value"",
                    ""id"": ""345ddbe4-95ae-424d-84ae-a0d66518d234"",
                    ""expectedControlType"": """",
                    ""processors"": ""StickDeadzone(min=0.3)"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7b9885b-8cb3-4a19-8c89-506c580ed04c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""b51af7dc-427e-4418-b70b-a01ecdbc6b19"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b3cf44b7-ae81-4500-b36e-0d63c491c596"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4832ff33-8472-4569-b2eb-082b2453bc64"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""897d45cf-3534-4e0f-9bf8-bdd649325c61"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2d07f074-8e8d-4cce-a131-5224a14df262"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d97823fe-a438-4548-9f74-d005f08fdb67"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Movement = m_Player1.FindAction("Movement", throwIfNotFound: true);
        m_Player1_MouseRotation = m_Player1.FindAction("MouseRotation", throwIfNotFound: true);
        m_Player1_Shoot = m_Player1.FindAction("Shoot", throwIfNotFound: true);
        m_Player1_Reload = m_Player1.FindAction("Reload", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_Movement = m_Player2.FindAction("Movement", throwIfNotFound: true);
        m_Player2_MouseRotation = m_Player2.FindAction("MouseRotation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Movement;
    private readonly InputAction m_Player1_MouseRotation;
    private readonly InputAction m_Player1_Shoot;
    private readonly InputAction m_Player1_Reload;
    public struct Player1Actions
    {
        private @PlayerInput m_Wrapper;
        public Player1Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player1_Movement;
        public InputAction @MouseRotation => m_Wrapper.m_Player1_MouseRotation;
        public InputAction @Shoot => m_Wrapper.m_Player1_Shoot;
        public InputAction @Reload => m_Wrapper.m_Player1_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @MouseRotation.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMouseRotation;
                @Shoot.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                @Reload.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MouseRotation.started += instance.OnMouseRotation;
                @MouseRotation.performed += instance.OnMouseRotation;
                @MouseRotation.canceled += instance.OnMouseRotation;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_Movement;
    private readonly InputAction m_Player2_MouseRotation;
    public struct Player2Actions
    {
        private @PlayerInput m_Wrapper;
        public Player2Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player2_Movement;
        public InputAction @MouseRotation => m_Wrapper.m_Player2_MouseRotation;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMovement;
                @MouseRotation.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMouseRotation;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MouseRotation.started += instance.OnMouseRotation;
                @MouseRotation.performed += instance.OnMouseRotation;
                @MouseRotation.canceled += instance.OnMouseRotation;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    public interface IPlayer1Actions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseRotation(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseRotation(InputAction.CallbackContext context);
    }
}
