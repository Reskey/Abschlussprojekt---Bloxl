//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerBasic"",
            ""id"": ""c3b26eb9-3a8f-48bd-92c6-bac233ab6f17"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""92ba8f0c-36a8-4f26-bf42-5994b1139a59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cd56905d-45fc-401c-b71e-9a338616f96d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fastfall"",
                    ""type"": ""Button"",
                    ""id"": ""415a3b0c-0288-4720-b8f4-e36afd5d1416"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""1b85136e-cac0-47b3-b9e1-dc7900adb662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""41f12b09-56f5-4d59-9dc1-e8b63427867d"",
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
                    ""id"": ""4022c503-68d7-4f6b-b671-ae51aec7a6e6"",
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
                    ""id"": ""fbbed222-eb1d-4fee-82e0-eec6873123b5"",
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
                    ""id"": ""5b237e6f-004e-4a0e-99bb-97c95ab0e07d"",
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
                    ""id"": ""53e411bb-36b5-4afe-88f7-be2e666e64f7"",
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
                    ""id"": ""13b50adb-36bb-4c92-bb65-66dd021e360c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerBasic
        m_PlayerBasic = asset.FindActionMap("PlayerBasic", throwIfNotFound: true);
        m_PlayerBasic_Run = m_PlayerBasic.FindAction("Run", throwIfNotFound: true);
        m_PlayerBasic_Jump = m_PlayerBasic.FindAction("Jump", throwIfNotFound: true);
        m_PlayerBasic_Fastfall = m_PlayerBasic.FindAction("Fastfall", throwIfNotFound: true);
        m_PlayerBasic_Dash = m_PlayerBasic.FindAction("Dash", throwIfNotFound: true);
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

    // PlayerBasic
    private readonly InputActionMap m_PlayerBasic;
    private IPlayerBasicActions m_PlayerBasicActionsCallbackInterface;
    private readonly InputAction m_PlayerBasic_Run;
    private readonly InputAction m_PlayerBasic_Jump;
    private readonly InputAction m_PlayerBasic_Fastfall;
    private readonly InputAction m_PlayerBasic_Dash;
    public struct PlayerBasicActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerBasicActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_PlayerBasic_Run;
        public InputAction @Jump => m_Wrapper.m_PlayerBasic_Jump;
        public InputAction @Fastfall => m_Wrapper.m_PlayerBasic_Fastfall;
        public InputAction @Dash => m_Wrapper.m_PlayerBasic_Dash;
        public InputActionMap Get() { return m_Wrapper.m_PlayerBasic; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerBasicActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerBasicActions instance)
        {
            if (m_Wrapper.m_PlayerBasicActionsCallbackInterface != null)
            {
                @Run.started -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnJump;
                @Fastfall.started -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnFastfall;
                @Fastfall.performed -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnFastfall;
                @Fastfall.canceled -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnFastfall;
                @Dash.started -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerBasicActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_PlayerBasicActionsCallbackInterface = instance;
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
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public PlayerBasicActions @PlayerBasic => new PlayerBasicActions(this);
    public interface IPlayerBasicActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnFastfall(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
