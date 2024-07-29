//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/SpaceCombatKit/UniversalVehicleCombat/DemoAssets/Input/InputSystem/LoadoutInputAsset.inputactions
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

public partial class @LoadoutInputAsset: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @LoadoutInputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LoadoutInputAsset"",
    ""maps"": [
        {
            ""name"": ""Loadout Controls"",
            ""id"": ""bb585eb7-c211-4d88-bb77-1af054e5dd2c"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""1f743bad-b2bd-4354-ad7b-f5b1dea07abb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""ed27d17a-cbc8-4cbd-8d74-d3009272ad03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""dc94f1fe-9eb2-40ce-a8c4-3937c57a3cae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""da46da41-32a2-45f1-bd19-b5dbda81b41f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""2144511a-23bd-407a-9aa4-07b32d3aa849"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CycleModuleSelection"",
                    ""type"": ""Button"",
                    ""id"": ""ea23b92f-5f58-4494-886e-2df2484d1965"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CycleModuleMountSelection"",
                    ""type"": ""Button"",
                    ""id"": ""2c7f2431-1578-443e-a51f-d847e4b2b34b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CycleVehicleSelection"",
                    ""type"": ""Button"",
                    ""id"": ""d411ba9d-857b-4193-b4fc-90042c9c38c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Delete"",
                    ""type"": ""Button"",
                    ""id"": ""bcad9c50-b188-431f-97a5-aed042301bb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""1b9fc3d6-a1c0-4c09-8e79-e1b58d7925c1"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""716f2bfd-3d39-4b3b-8658-fd0830edb518"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""1b76ac8e-fdb6-401b-a2db-49a7e98d1fd3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.175,y=0.175)"",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a77554bd-70f9-4ac0-a51e-8b2745f546cc"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b66b6c7b-57d2-46b2-8d4f-c9def6b71fa3"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70fda144-60c3-478b-ad7d-264d6dc7b3fc"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a05d7c7-a987-439a-ad11-e08776e9e805"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da3ac354-f388-4530-856c-f7b4ddab1ddf"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9cfee5e-630f-41d4-8f7e-30d387a46811"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""9ca72c70-506f-4c08-9c72-cc44e2ab7708"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3a4b8300-8b8f-4ed6-a933-47da33e440bf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""de5d5dc7-73fd-4cb0-8c9e-fc5bb71d5f0e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""02aa98d3-0000-4b5b-97d4-b24b545fc904"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e156bbdb-8a20-4376-a424-5a3315719531"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""162a0568-e7b5-4c85-a543-fa446e0d90c0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""1c7ed4f0-f287-46c1-b1e0-00ec3aa2c80c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f2fb9ad4-f0e3-4064-ad26-1860fcc65bda"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d6429abc-cb88-48e4-9f6a-25f8ba8798b7"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fdba02d4-5a27-4b93-8f81-797fe628b9e9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""dacdc287-1e16-4caf-9e3a-4b0245591d68"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""46fc4716-87fb-4153-bdf5-afd76d6fa952"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""1183e3aa-c41b-4fe1-8dd7-afc8829d02bb"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f5d3dec1-5dfe-4df0-bf98-bf54f298fe1b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""eb04cfcd-0c87-4ed8-b128-0771a9dfb539"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a6b0618e-4c6a-41ea-a838-f344652ae8da"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2477dcd8-7a18-4906-9894-3cfcf713703c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6a4f8589-7f9f-4265-b1ee-a1d8358cac21"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleVehicleSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8a4fe5ee-3c7e-4a98-a1a7-d74b088e5a30"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0486ca56-d4fb-4ad7-894b-2dcce39aab46"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6daca71c-ce57-4ba2-9780-e11fc8c1ebfd"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""167dddd2-3972-4b1c-ac5a-2a7b0025446f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c81c9e39-2151-4625-b65a-0ff5eb61d24e"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b756c86c-3312-446c-9322-a430ccddeced"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleModuleMountSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""660d39ae-2337-488d-a8ff-c01519900a15"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4354e1b4-3eb0-4264-9b80-6d4b5f27d49e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efd08a95-ebe7-46fd-af0f-88043f82da24"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseKeyboard"",
            ""bindingGroup"": ""MouseKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Loadout Controls
        m_LoadoutControls = asset.FindActionMap("Loadout Controls", throwIfNotFound: true);
        m_LoadoutControls_Look = m_LoadoutControls.FindAction("Look", throwIfNotFound: true);
        m_LoadoutControls_Back = m_LoadoutControls.FindAction("Back", throwIfNotFound: true);
        m_LoadoutControls_Menu = m_LoadoutControls.FindAction("Menu", throwIfNotFound: true);
        m_LoadoutControls_Start = m_LoadoutControls.FindAction("Start", throwIfNotFound: true);
        m_LoadoutControls_Accept = m_LoadoutControls.FindAction("Accept", throwIfNotFound: true);
        m_LoadoutControls_CycleModuleSelection = m_LoadoutControls.FindAction("CycleModuleSelection", throwIfNotFound: true);
        m_LoadoutControls_CycleModuleMountSelection = m_LoadoutControls.FindAction("CycleModuleMountSelection", throwIfNotFound: true);
        m_LoadoutControls_CycleVehicleSelection = m_LoadoutControls.FindAction("CycleVehicleSelection", throwIfNotFound: true);
        m_LoadoutControls_Delete = m_LoadoutControls.FindAction("Delete", throwIfNotFound: true);
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

    // Loadout Controls
    private readonly InputActionMap m_LoadoutControls;
    private List<ILoadoutControlsActions> m_LoadoutControlsActionsCallbackInterfaces = new List<ILoadoutControlsActions>();
    private readonly InputAction m_LoadoutControls_Look;
    private readonly InputAction m_LoadoutControls_Back;
    private readonly InputAction m_LoadoutControls_Menu;
    private readonly InputAction m_LoadoutControls_Start;
    private readonly InputAction m_LoadoutControls_Accept;
    private readonly InputAction m_LoadoutControls_CycleModuleSelection;
    private readonly InputAction m_LoadoutControls_CycleModuleMountSelection;
    private readonly InputAction m_LoadoutControls_CycleVehicleSelection;
    private readonly InputAction m_LoadoutControls_Delete;
    public struct LoadoutControlsActions
    {
        private @LoadoutInputAsset m_Wrapper;
        public LoadoutControlsActions(@LoadoutInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_LoadoutControls_Look;
        public InputAction @Back => m_Wrapper.m_LoadoutControls_Back;
        public InputAction @Menu => m_Wrapper.m_LoadoutControls_Menu;
        public InputAction @Start => m_Wrapper.m_LoadoutControls_Start;
        public InputAction @Accept => m_Wrapper.m_LoadoutControls_Accept;
        public InputAction @CycleModuleSelection => m_Wrapper.m_LoadoutControls_CycleModuleSelection;
        public InputAction @CycleModuleMountSelection => m_Wrapper.m_LoadoutControls_CycleModuleMountSelection;
        public InputAction @CycleVehicleSelection => m_Wrapper.m_LoadoutControls_CycleVehicleSelection;
        public InputAction @Delete => m_Wrapper.m_LoadoutControls_Delete;
        public InputActionMap Get() { return m_Wrapper.m_LoadoutControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LoadoutControlsActions set) { return set.Get(); }
        public void AddCallbacks(ILoadoutControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_LoadoutControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LoadoutControlsActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Back.started += instance.OnBack;
            @Back.performed += instance.OnBack;
            @Back.canceled += instance.OnBack;
            @Menu.started += instance.OnMenu;
            @Menu.performed += instance.OnMenu;
            @Menu.canceled += instance.OnMenu;
            @Start.started += instance.OnStart;
            @Start.performed += instance.OnStart;
            @Start.canceled += instance.OnStart;
            @Accept.started += instance.OnAccept;
            @Accept.performed += instance.OnAccept;
            @Accept.canceled += instance.OnAccept;
            @CycleModuleSelection.started += instance.OnCycleModuleSelection;
            @CycleModuleSelection.performed += instance.OnCycleModuleSelection;
            @CycleModuleSelection.canceled += instance.OnCycleModuleSelection;
            @CycleModuleMountSelection.started += instance.OnCycleModuleMountSelection;
            @CycleModuleMountSelection.performed += instance.OnCycleModuleMountSelection;
            @CycleModuleMountSelection.canceled += instance.OnCycleModuleMountSelection;
            @CycleVehicleSelection.started += instance.OnCycleVehicleSelection;
            @CycleVehicleSelection.performed += instance.OnCycleVehicleSelection;
            @CycleVehicleSelection.canceled += instance.OnCycleVehicleSelection;
            @Delete.started += instance.OnDelete;
            @Delete.performed += instance.OnDelete;
            @Delete.canceled += instance.OnDelete;
        }

        private void UnregisterCallbacks(ILoadoutControlsActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Back.started -= instance.OnBack;
            @Back.performed -= instance.OnBack;
            @Back.canceled -= instance.OnBack;
            @Menu.started -= instance.OnMenu;
            @Menu.performed -= instance.OnMenu;
            @Menu.canceled -= instance.OnMenu;
            @Start.started -= instance.OnStart;
            @Start.performed -= instance.OnStart;
            @Start.canceled -= instance.OnStart;
            @Accept.started -= instance.OnAccept;
            @Accept.performed -= instance.OnAccept;
            @Accept.canceled -= instance.OnAccept;
            @CycleModuleSelection.started -= instance.OnCycleModuleSelection;
            @CycleModuleSelection.performed -= instance.OnCycleModuleSelection;
            @CycleModuleSelection.canceled -= instance.OnCycleModuleSelection;
            @CycleModuleMountSelection.started -= instance.OnCycleModuleMountSelection;
            @CycleModuleMountSelection.performed -= instance.OnCycleModuleMountSelection;
            @CycleModuleMountSelection.canceled -= instance.OnCycleModuleMountSelection;
            @CycleVehicleSelection.started -= instance.OnCycleVehicleSelection;
            @CycleVehicleSelection.performed -= instance.OnCycleVehicleSelection;
            @CycleVehicleSelection.canceled -= instance.OnCycleVehicleSelection;
            @Delete.started -= instance.OnDelete;
            @Delete.performed -= instance.OnDelete;
            @Delete.canceled -= instance.OnDelete;
        }

        public void RemoveCallbacks(ILoadoutControlsActions instance)
        {
            if (m_Wrapper.m_LoadoutControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILoadoutControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_LoadoutControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LoadoutControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LoadoutControlsActions @LoadoutControls => new LoadoutControlsActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("MouseKeyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ILoadoutControlsActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnCycleModuleSelection(InputAction.CallbackContext context);
        void OnCycleModuleMountSelection(InputAction.CallbackContext context);
        void OnCycleVehicleSelection(InputAction.CallbackContext context);
        void OnDelete(InputAction.CallbackContext context);
    }
}
