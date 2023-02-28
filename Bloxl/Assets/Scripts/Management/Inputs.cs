//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Inputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""PlayerBasics"",
            ""id"": ""6bc6add1-e71b-4169-8fa2-872d29273f47"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""ce1745e4-e723-45c7-9473-5f49d443d309"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b507e869-6aa3-474f-8d76-f41685f44bf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fastfall"",
                    ""type"": ""Button"",
                    ""id"": ""f788b402-106c-4a39-94a8-b04b565ecb66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""23b3b979-319f-460f-b0c0-7c3d5cdbb342"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""3fbee54f-d591-438e-9d46-fd993ec82e4b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""97dd107b-f84b-4ef9-bd05-1063d4558d45"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6a4f601a-5e92-492f-8ab3-754079804e86"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""85fdb2e8-f2be-4ae5-a54a-466475b6851d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec7dc047-65e6-45c5-9152-ee0007aa6ab2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fastfall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63a516a0-c027-46c0-8c16-8e4c56648a74"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseMenu"",
            ""id"": ""e6a598f4-9169-4802-bf20-40af8ceb1339"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""7d69916d-ea3b-461d-91d1-4283797e7b00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1e8cf65-6f41-4d0f-b3a5-bb7b1c0bf6b5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerBasics
        m_PlayerBasics = asset.FindActionMap("PlayerBasics", throwIfNotFound: true);
        m_PlayerBasics_Run = m_PlayerBasics.FindAction("Run", throwIfNotFound: true);
        m_PlayerBasics_Jump = m_PlayerBasics.FindAction("Jump", throwIfNotFound: true);
        m_PlayerBasics_Fastfall = m_PlayerBasics.FindAction("Fastfall", throwIfNotFound: true);
        m_PlayerBasics_Attack = m_PlayerBasics.FindAction("Attack", throwIfNotFound: true);
        // PauseMenu
        m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
        m_PauseMenu_Pause = m_PauseMenu.FindAction("Pause", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerBasics
    private readonly InputActionMap m_PlayerBasics;
    private IPlayerBasicsActions m_PlayerBasicsActionsCallbackInterface;
    private readonly InputAction m_PlayerBasics_Run;
    private readonly InputAction m_PlayerBasics_Jump;
    private readonly InputAction m_PlayerBasics_Fastfall;
    private readonly InputAction m_PlayerBasics_Attack;
    public struct PlayerBasicsActions
    {
        private @Inputs m_Wrapper;
        public PlayerBasicsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_PlayerBasics_Run;
        public InputAction @Jump => m_Wrapper.m_PlayerBasics_Jump;
        public InputAction @Fastfall => m_Wrapper.m_PlayerBasics_Fastfall;
        public InputAction @Attack => m_Wrapper.m_PlayerBasics_Attack;
        public InputActionMap Get() { return m_Wrapper.m_PlayerBasics; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerBasicsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerBasicsActions instance)
        {
            if (m_Wrapper.m_PlayerBasicsActionsCallbackInterface != null)
            {
                @Run.started -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnJump;
                @Fastfall.started -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnFastfall;
                @Fastfall.performed -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnFastfall;
                @Fastfall.canceled -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnFastfall;
                @Attack.started -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerBasicsActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_PlayerBasicsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Fastfall.started += instance.OnFastfall;
                @Fastfall.performed += instance.OnFastfall;
                @Fastfall.canceled += instance.OnFastfall;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public PlayerBasicsActions @PlayerBasics => new PlayerBasicsActions(this);

    // PauseMenu
    private readonly InputActionMap m_PauseMenu;
    private IPauseMenuActions m_PauseMenuActionsCallbackInterface;
    private readonly InputAction m_PauseMenu_Pause;
    public struct PauseMenuActions
    {
        private @Inputs m_Wrapper;
        public PauseMenuActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_PauseMenu_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
        public void SetCallbacks(IPauseMenuActions instance)
        {
            if (m_Wrapper.m_PauseMenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PauseMenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PauseMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PauseMenuActions @PauseMenu => new PauseMenuActions(this);
    public interface IPlayerBasicsActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFastfall(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IPauseMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}