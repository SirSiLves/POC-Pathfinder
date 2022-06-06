// GENERATED AUTOMATICALLY FROM 'Assets/Pathfinder/InputSystem/TouchController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TouchController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchController"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""54602beb-19fe-4a43-b09e-b5c4b476c9b2"",
            ""actions"": [
                {
                    ""name"": ""PrimaryFingerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""1b4058f3-e246-4ba1-800b-fab78783b83a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryTouchContact"",
                    ""type"": ""Button"",
                    ""id"": ""3f9b4d89-d8d9-4a14-8cc8-f90185466d47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PrimaryTouchHold"",
                    ""type"": ""Button"",
                    ""id"": ""f8cb5e17-45d5-4e42-902b-0d8384789a59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.2,pressPoint=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""420a1f4d-ff7a-4d5b-9b8d-001235fc990f"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryFingerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""306fdefd-4ee8-4b40-8913-95cafebcb734"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4e143a7-cf81-47fa-a583-2405cfe7768c"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_PrimaryFingerPosition = m_Touch.FindAction("PrimaryFingerPosition", throwIfNotFound: true);
        m_Touch_PrimaryTouchContact = m_Touch.FindAction("PrimaryTouchContact", throwIfNotFound: true);
        m_Touch_PrimaryTouchHold = m_Touch.FindAction("PrimaryTouchHold", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_PrimaryFingerPosition;
    private readonly InputAction m_Touch_PrimaryTouchContact;
    private readonly InputAction m_Touch_PrimaryTouchHold;
    public struct TouchActions
    {
        private @TouchController m_Wrapper;
        public TouchActions(@TouchController wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryFingerPosition => m_Wrapper.m_Touch_PrimaryFingerPosition;
        public InputAction @PrimaryTouchContact => m_Wrapper.m_Touch_PrimaryTouchContact;
        public InputAction @PrimaryTouchHold => m_Wrapper.m_Touch_PrimaryTouchHold;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @PrimaryFingerPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryFingerPosition;
                @PrimaryFingerPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryFingerPosition;
                @PrimaryFingerPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryFingerPosition;
                @PrimaryTouchContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchContact;
                @PrimaryTouchContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchContact;
                @PrimaryTouchContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchContact;
                @PrimaryTouchHold.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchHold;
                @PrimaryTouchHold.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchHold;
                @PrimaryTouchHold.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnPrimaryTouchHold;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryFingerPosition.started += instance.OnPrimaryFingerPosition;
                @PrimaryFingerPosition.performed += instance.OnPrimaryFingerPosition;
                @PrimaryFingerPosition.canceled += instance.OnPrimaryFingerPosition;
                @PrimaryTouchContact.started += instance.OnPrimaryTouchContact;
                @PrimaryTouchContact.performed += instance.OnPrimaryTouchContact;
                @PrimaryTouchContact.canceled += instance.OnPrimaryTouchContact;
                @PrimaryTouchHold.started += instance.OnPrimaryTouchHold;
                @PrimaryTouchHold.performed += instance.OnPrimaryTouchHold;
                @PrimaryTouchHold.canceled += instance.OnPrimaryTouchHold;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnPrimaryFingerPosition(InputAction.CallbackContext context);
        void OnPrimaryTouchContact(InputAction.CallbackContext context);
        void OnPrimaryTouchHold(InputAction.CallbackContext context);
    }
}
