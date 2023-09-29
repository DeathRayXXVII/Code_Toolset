//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Scripts/InputSystem/GameInputs.inputactions
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

public partial class @GameInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputs"",
    ""maps"": [
        {
            ""name"": ""KeyActionMap"",
            ""id"": ""7140e64e-f063-4e98-9f3a-756977f9ec65"",
            ""actions"": [
                {
                    ""name"": ""Vertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2a646de4-6bba-4334-bd62-7a25f65b2536"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""f5aff77d-6797-49b8-bcf7-2d4ceae793f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Move"",
                    ""id"": ""c793a26b-f77e-489f-9b4e-397e22aa221b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""53368adb-5936-42a9-8e38-482a4aafeabd"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7d84d0d8-ccb1-4f4a-9478-29bb68ef1282"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b05d6f58-002b-4b4a-a188-ebd24dbe68cd"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aeb8e8bf-9a0c-4d0d-bc29-9b671e46d06f"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""556de521-ffe6-45ae-978c-9b2305796973"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""id"": ""a12c621e-68ae-4afb-9d1c-6b07ef968e4a"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""68e0cd43-6c6c-4d41-9c64-49fed73f2b75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryPositition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9afcbc33-7a21-4d56-bcf1-7eb03c20e802"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72ed46de-07fa-4122-8aa2-ba684b9f6cbc"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6ddbf80-5760-4ffe-aaac-73e5f59f1aa0"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPositition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""mTouch"",
            ""id"": ""435796a4-ac8a-441c-b0f7-ee8a4b45a669"",
            ""actions"": [
                {
                    ""name"": ""Contact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fa41f1f3-e497-4714-a218-80a6e66d9399"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ContactPostion"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3a400464-5e74-41b9-ba36-81a13fa45105"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""aa1065d8-06ad-4c27-a9dc-941a1a7ac7b6"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Contact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1936bfcf-b383-43ed-93e7-842aabde8bd9"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContactPostion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keys"",
            ""bindingGroup"": ""Keys"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // KeyActionMap
        m_KeyActionMap = asset.FindActionMap("KeyActionMap", throwIfNotFound: true);
        m_KeyActionMap_Vertical = m_KeyActionMap.FindAction("Vertical", throwIfNotFound: true);
        m_KeyActionMap_Click = m_KeyActionMap.FindAction("Click", throwIfNotFound: true);
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_PrimaryContact = m_Touch.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Touch_PrimaryPositition = m_Touch.FindAction("PrimaryPositition", throwIfNotFound: true);
        // mTouch
        m_mTouch = asset.FindActionMap("mTouch", throwIfNotFound: true);
        m_mTouch_Contact = m_mTouch.FindAction("Contact", throwIfNotFound: true);
        m_mTouch_ContactPostion = m_mTouch.FindAction("ContactPostion", throwIfNotFound: true);
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

    // KeyActionMap
    private readonly InputActionMap m_KeyActionMap;
    private List<IKeyActionMapActions> m_KeyActionMapActionsCallbackInterfaces = new List<IKeyActionMapActions>();
    private readonly InputAction m_KeyActionMap_Vertical;
    private readonly InputAction m_KeyActionMap_Click;
    public struct KeyActionMapActions
    {
        private @GameInputs m_Wrapper;
        public KeyActionMapActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Vertical => m_Wrapper.m_KeyActionMap_Vertical;
        public InputAction @Click => m_Wrapper.m_KeyActionMap_Click;
        public InputActionMap Get() { return m_Wrapper.m_KeyActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IKeyActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_KeyActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyActionMapActionsCallbackInterfaces.Add(instance);
            @Vertical.started += instance.OnVertical;
            @Vertical.performed += instance.OnVertical;
            @Vertical.canceled += instance.OnVertical;
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(IKeyActionMapActions instance)
        {
            @Vertical.started -= instance.OnVertical;
            @Vertical.performed -= instance.OnVertical;
            @Vertical.canceled -= instance.OnVertical;
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(IKeyActionMapActions instance)
        {
            if (m_Wrapper.m_KeyActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_KeyActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyActionMapActions @KeyActionMap => new KeyActionMapActions(this);

    // Touch
    private readonly InputActionMap m_Touch;
    private List<ITouchActions> m_TouchActionsCallbackInterfaces = new List<ITouchActions>();
    private readonly InputAction m_Touch_PrimaryContact;
    private readonly InputAction m_Touch_PrimaryPositition;
    public struct TouchActions
    {
        private @GameInputs m_Wrapper;
        public TouchActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_Touch_PrimaryContact;
        public InputAction @PrimaryPositition => m_Wrapper.m_Touch_PrimaryPositition;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void AddCallbacks(ITouchActions instance)
        {
            if (instance == null || m_Wrapper.m_TouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TouchActionsCallbackInterfaces.Add(instance);
            @PrimaryContact.started += instance.OnPrimaryContact;
            @PrimaryContact.performed += instance.OnPrimaryContact;
            @PrimaryContact.canceled += instance.OnPrimaryContact;
            @PrimaryPositition.started += instance.OnPrimaryPositition;
            @PrimaryPositition.performed += instance.OnPrimaryPositition;
            @PrimaryPositition.canceled += instance.OnPrimaryPositition;
        }

        private void UnregisterCallbacks(ITouchActions instance)
        {
            @PrimaryContact.started -= instance.OnPrimaryContact;
            @PrimaryContact.performed -= instance.OnPrimaryContact;
            @PrimaryContact.canceled -= instance.OnPrimaryContact;
            @PrimaryPositition.started -= instance.OnPrimaryPositition;
            @PrimaryPositition.performed -= instance.OnPrimaryPositition;
            @PrimaryPositition.canceled -= instance.OnPrimaryPositition;
        }

        public void RemoveCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITouchActions instance)
        {
            foreach (var item in m_Wrapper.m_TouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TouchActions @Touch => new TouchActions(this);

    // mTouch
    private readonly InputActionMap m_mTouch;
    private List<IMTouchActions> m_MTouchActionsCallbackInterfaces = new List<IMTouchActions>();
    private readonly InputAction m_mTouch_Contact;
    private readonly InputAction m_mTouch_ContactPostion;
    public struct MTouchActions
    {
        private @GameInputs m_Wrapper;
        public MTouchActions(@GameInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Contact => m_Wrapper.m_mTouch_Contact;
        public InputAction @ContactPostion => m_Wrapper.m_mTouch_ContactPostion;
        public InputActionMap Get() { return m_Wrapper.m_mTouch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MTouchActions set) { return set.Get(); }
        public void AddCallbacks(IMTouchActions instance)
        {
            if (instance == null || m_Wrapper.m_MTouchActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MTouchActionsCallbackInterfaces.Add(instance);
            @Contact.started += instance.OnContact;
            @Contact.performed += instance.OnContact;
            @Contact.canceled += instance.OnContact;
            @ContactPostion.started += instance.OnContactPostion;
            @ContactPostion.performed += instance.OnContactPostion;
            @ContactPostion.canceled += instance.OnContactPostion;
        }

        private void UnregisterCallbacks(IMTouchActions instance)
        {
            @Contact.started -= instance.OnContact;
            @Contact.performed -= instance.OnContact;
            @Contact.canceled -= instance.OnContact;
            @ContactPostion.started -= instance.OnContactPostion;
            @ContactPostion.performed -= instance.OnContactPostion;
            @ContactPostion.canceled -= instance.OnContactPostion;
        }

        public void RemoveCallbacks(IMTouchActions instance)
        {
            if (m_Wrapper.m_MTouchActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMTouchActions instance)
        {
            foreach (var item in m_Wrapper.m_MTouchActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MTouchActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MTouchActions @mTouch => new MTouchActions(this);
    private int m_KeysSchemeIndex = -1;
    public InputControlScheme KeysScheme
    {
        get
        {
            if (m_KeysSchemeIndex == -1) m_KeysSchemeIndex = asset.FindControlSchemeIndex("Keys");
            return asset.controlSchemes[m_KeysSchemeIndex];
        }
    }
    public interface IKeyActionMapActions
    {
        void OnVertical(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
    public interface ITouchActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPositition(InputAction.CallbackContext context);
    }
    public interface IMTouchActions
    {
        void OnContact(InputAction.CallbackContext context);
        void OnContactPostion(InputAction.CallbackContext context);
    }
}
