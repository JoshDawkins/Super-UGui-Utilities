# Super UGUI Utilities
 
This project is intended primarily as a portfolio piece demonstrating a bit of my coding philosophy as well as expertise in Unity's UGUI. If it looks useful to you though, it is freely available to use for both personal and commercial projects, subject to the included license, _except_ in projects using the blockchain, NFTs, or generative AI.

A number of utility scripts and components are included that are designed to provide a framework for more easily working with Unity's canvas-based UI system, as well as providing a few basic pieces of functionality pre-built.

## Prerequisites

### Unity Version
- Created and tested in Unity 6
- All scripts should be compatible with at least Unity 2022 and above
- Because the included property drawers are written in UI Toolkit, they may not display properly in Unity 2021 or earlier on components that do not have a custom inspector also written in UI Toolkit, including most of the included components. This is because prior to Unity 2022, the default inspector only supported IMGUI drawers.

### Dependencies
While it is not required by the core systems or utilities, a few of the more specific components depend on the [free version of DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676) for animations. The specific components with this dependency are:
- AdvancedCooldownButton
- AnimationToggleBase
- ColorSwapToggle
- SlideToggle

## Viewing the Samples

Samples of the main features of this project can be found in the `SampleScene` scene, located at `Assets/SuperUGuiUtilities/Samples/Scenes/SampleScene.unity`. All assets that are specifically for the samples are organized into appropriate sub-folders under `Assets/SuperUGuiUtilities/Samples`, including scripts, sprites, and ScriptableObjects for themes. The theme manager ScriptableObjects used by the samples can be found in the `Assets/SuperUGuiUtilities/Samples/Resources` folder.

You can also view the samples running in your browser on [my portfolio website](https://joshdawkins.github.io/projects/super-ugui-utilities.html).

## Key Features and Components

### Super Selectables
Sometimes, you want to give a `Selectable` component, such as a button or toggle, more complex state changes (hover, pressed, disabled, etc.) than the built-in single-graphic Color Tint and Sprite Swap transitions allow. A simple example might be having a button change not only its background color, but also its text color and the sprite for an icon. The typical way of managing this would be to use the Animation transition type in conjunction with an Animator component. This approach is powerful and easy for designers who are used to Unity's animation tools to approach, but it requires some set up, makes behavior less obvious in the inspector, and may not be ideal in cases that require complex logic or many different animations for different elements.

This project provides an alternative approach that may be more comfortable for some cases and teams, in the form of Super Selectables. The primary challenge to keeping these transitions in code and the inspector is simply that the built-in selectables don't publicly expose any way to access their current state or hook into an event for state changes. Super Selectables inherit the built-in selectable components to add this feature through the `ISuperSelectable` interface, while still slotting in seamlessly anywhere the built-in components are already used. So for example, a `SuperButton` component inherits from `Button`, so it can be used by any script requiring a `Button`, while also adding the `CurrentState` property and `OnStateChanged` event through the `ISuperSelectable` interface. Taken together with sub-classes of the `SelectableStateBehavior` type covered in the next section, a code-based approach to selectable transitions that can easily support componentization and reusability across different styles and projects becomes easy.

All of the provided Super Selectable types and their base type are shown in the table below. For convenience, each one adds an option to the context menu of its base type (right-click the component's header in the inspector, or click the gear icon on the component) to convert a base component to the matching Super Selectable type. This will carry over all properties configured on the original component to the new, upgraded component. **However**, it is important to note that **existing inspector references to the component will be lost** and will need to be manually re-serialized, so use this option with caution.
| Base Type | Super Selectable |
| --- | --- |
| Button | SuperButton |
| TMP_Dropdown | SuperDropdown |
| TMP_InputField | SuperInputField |
| Scrollbar | SuperScrollbar |
| Slider | SuperSlider |
| Toggle | SuperToggle |

### Selectable Behaviors
This project establishes a pattern for adding behaviors to selectable UI elements, such as buttons, toggles, dropdowns, and more. This pattern is based on adding a component for each behavior a selectable should trigger, which should be designed to be as configurable and reusable as possible (though of course some behaviors in real production code will likely be unique one-off cases, and this is okay when carefully considered). These components can then be mixed and matched as needed to create more complex behaviors. So for example, a button for casting a spell might have three attached behaviors: one that causes a subtle scaling effect on its icon when hovered or pressed, one that initiates a cooldown when pressed to prevent the button from being pressed again for a certain amount of time, and one that fires an event when pressed telling the player character to cast the spell.

These behavior components are each based on an abstract base class which implements the boilerplate code for attaching and detaching the behavior to an event on an element, and provides an abstract method that concrete sub-classes use to define what happens when the element is triggered. The base class should obtain the element that will trigger the behavior through a serialized field, so that behavior components are not required to be placed on the same GameObject as the component that triggers them, allowing greater organizational flexibility. They should, however, use the OnValidate method and GetComponent to try to automatically serialize a matching component on the current GameObject if the field has not been set, as a convenience when the behavior is being attached to the same GameObject.

The following base classes have been provided already, though others can be easily defined by following the same patterns used in these scripts:
| Class | Triggering Element.Event |
| --- | --- |
| ButtonBehavior | Button.onClick |
| DropdownBehavior | TMP_Dropdown.onValueChanged |
| SelectableStateBehavior | ISuperSelectable.OnStateChanged |
| ToggleBehavior | Toggle.onValueChanged |

The following more specific components, including both specific behaviors and intermediary base classes implementing additional boilerplate logic for categories of behavior, have been provided to get you started:
| Class | Inherits From | Description |
| --- | --- | --- |
| ActivateGameObjectsButton | ButtonBehavior | Clicking the button activates or deactivates the configured GameObject(s). Whether a GameObject is activated or deactivated is configured for each target GameObject. |
| CooldownButton | ButtonBehavior | Clicking the button sets the button's `interactable` property to false, then back to true after a configurable amount of time. |
| AdvancedCooldownButton | ButtonBehavior | Like CooldownButton, but also animates a Slider component's value to visualize the cooldown. Clever layering can allow this to look like the button itself is "refilling." |
| TypeDropdown<T> | DropdownBehavior | Abstract base that tracks a list of typed objects associated with a Dropdown's options, triggering an `onOptionChosen` event that gives the matching typed object to listeners instead of an index. |
| ColorTintAdditionalGraphics | SelectableStateBehavior | Allows multiple Graphics to be color tinted based on the linked Selectable's state in the same way as the default Color Tint transition, each with their own set of colors. |
| SpriteSwapAdditionalGraphics | SelectableStateBehavior | Allows multiple Graphics to have their sprites swapped based on the linked Selectable's state in the same way as the default Sprite Swap transition, each with their own set of sprites. |
| TriggerAdditionalAnimations | SelectableStateBehavior | Allows multiple Animators to be triggered based on the linked Selectable's state in the same way as the default Animation transition, each with their own set of animation triggers. |
| SetCanvasGroupAlphaWithStateBehavior | SelectableStateBehavior | Sets the alpha transparency of any number of CanvasGroup components based on the linked Selectable's state. Each CanvasGroup can have different alpha values. |
| AnimationToggleBase | ToggleBehavior | Abstract base for behaviors that want to trigger a DOTween animation when the linked Toggle is toggled. Inheritors must implement both the DOTween animation and how to "jump" to the end without animating (to support animation durations of 0 or edit-mode toggling in case \[ExecuteAlways\] is applied to the inheritor). |
| ColorSwapToggle | AnimationToggleBase | Animates the color of any number of Graphics between two values when the linked Toggle is toggled. Each target Graphic defines its own pair of colors. |
| SlideToggle | AnimationToggleBase | Animates the anchored position and optionally the anchoring properties of any number of RectTransform components when the linked Toggle is toggled. Each target RectTransform defines its own pair of positions. |
| InteractabilityToggle | ToggleBehavior | Toggles the `interactable` property of any number of Selectable and/or CanvasGroup components when the linked Toggle is toggled. All targets toggle together, either matching or opposite the linked Toggle's `isOn` property depending whether the `invert` setting is enabled. |

### Themes
Included is a framework for creating UI themes, which define a list of styles that can be applied to elements and automatically keep configured components up-to-date with theme styles and even allow swapping themes at runtime. In order to provide maximum flexibility, some code is required to define what your game's UI themes need to support and how they are applied to elements, but the core functionality of managing and enforcing themes is provided. A project can manage just one type of theme, or have multiple theme managers each handling a different type of theme with different purposes. For example, one theme manager might change the overall style or color pallette of the game's UI, while a separate theme manager controls text styling, allowing these to be more easily controlled separately.

There are two types of themes: "full" themes and "simple" themes. The difference is in how many types of components the theme knows how to apply styles to: a simple theme can only apply styles to one component type (such as the text styling theme in our previous example, which would apply only to one type, most likely either `TextMeshProUGUI` or `TMP_Text`), whereas a full theme can apply styles to multiple types of components. Technically, a simple theme can be implemented in the same way as a full theme, which may be desirable if there is an expectation that more types will be added to it later in development. However, we will cover in the section on theme definitions below how a simple theme can be implemented through a provided base class to make development easier.

To implement themes in your game, you will need to define a few different types:

#### A Theme Style ID
Themes generally manage multiple styles, each defining one _conceptual_ type of element (not a programmatic type like `Image`) that each theme variation will provide different values for. So for example, a text-only style might define styles for a main title, a header, and body text; a more complex theme might have many more styles. The first type that needs to be defined for a theme is an enumeration of unique IDs for the styles the theme will manage, something like:
```C#
public enum MyThemeStyleId {
	HeaderContainer,
	BannerImage,
	Icon,
	...
}```

#### One or More Theme Style Classes
For each programmatic component type you will want your theme to be able to apply styles to, you will need to create a theme style class, which defines what properties are configurable for a style applied to a particular component type and how those properties are to be applied to the component. Note that these are plain C# classes, not MonoBehaviours or another Unity type. To define your theme style class(es), walk through the following steps:

1. Define a class that inherits from the `ThemeStyleBase<TId>` class, where `TId` is the theme style ID enum you already defined. For a full theme, consider using an abstract base that defines `TId` and anything else that will be common to all theme style classes for this theme, regardless of the component type they apply to.
```C#
public abstract class MyThemeStyleBase : ThemeStyleBase<MyThemeStyleId> {

}

public class MyImageThemeStyle : MyThemeStyleBase {

}

//Additional sub-classes for other component types```

2. Add the `[Serializable]` attribute from the `System` namespace to your concrete types, so that they can be displayed in the inspector.
```C#
[Serializable]
public class MyImageThemeStyle : MyThemeStyleBase {```

3. Consider how instances of your theme style class will set their `StyleId` property (inherited from `ThemeStyleBase`), which identifies the specific style to which this instance applies. This depends upon how the theme definition class you will be creating later will create instances of these theme style classes; you can handle this in other ways and can come back to this step when you have made those decisions, but here is how you should handle `StyleId` if you intend to follow the recommended steps in this guide:
	- For a simple theme, no special steps are necessary. The `SimpleThemeDefinitionBase` you will use later takes care of this step for you.
	- For a full theme, use a constructor (preferrably starting in your base class) to set the `StyleId`. You should also set the inherited `_preventEditing` field to `true`, which simply makes the `StyleId` read-only in the inspector. This is desireable in this case, since it will be controlled by the constructor already and should not be changed later.
```C#
public abstract class MyThemeStyleBase : ThemeStyleBase<MyThemeStyleId> {
	protected MyThemeStyleBase(MyThemeStyleId id) {
		StyleId = id;
		_preventEditing = true;
	}
}

[Serializable]
public class MyImageThemeStyle : MyThemeStyleBase {
	public MyImageThemeStyle(MyThemeStyleId id) : base(id) { }
}```

4. Add serializable fields and public getters for the properties you want to be configurable for this type of style. You can define your fields and getters separately, or use the combined syntax shown here, this is purely a matter of preference.
```C#
[Serializable]
public class MyImageThemeStyle : MyThemeStyleBase {
	[field: SerializeField]
	public Color Color { get; private set; } = Color.white;
	[field: SerializeField]
	public Sprite Sprite { get; private set; }
	
	public MyImageThemeStyle(MyThemeStyleId id) : base(id) { }
}```

5. For each concrete type, implement the `IProvidesThemeFor<T>` interface and its `ApplyThemeTo(T target)` method, where `T` is the component type to which this style should apply. This method will accept a component and apply the configured properties to it. It may be possible in some cases for a single theme style class to implement this interface for more than one type, but this is generally discouraged.
```C#
public class MyImageThemeStyle : MyThemeStyleBase, IProvidesThemeFor<Image> {
	...
	
	public void ApplyThemeTo(Image target){
		if (target == null) return;
		
		target.sprite = Sprite;
		target.color = Color;
	}
}```

#### A Theme Definition Class
Now that you've defined what styles your theme will manage and how styles will be applied to certain component types, you need to tie these together to define which type of component each style is to be applied to, and how your theme styles are created. This is the responsibility of a theme definition class, which descends from the `ThemeDefinitionBase<TId, TStyle>` type. This is also the `ScriptableObject` type of which you will create an asset for each theme variation.

For a simple theme, this step is extremely straightforward: you need only create an empty class which inherits `SimpleThemeDefinitionBase<TId, TStyle, TTarget>`, where `TId` is your theme style ID enum, `TStyle` is your theme style class, and `TTarget` is the component type which `TStyle` can be applied to through the `IProvidesThemeFor<T>` interface. Then apply the `[CreateAssetMenu]` attribute to this class, so that you can create assets of this `ScriptableObject` type later. `SimpleThemeDefinitionBase` pre-defines all of the necessary features of a theme definition for you in the case of a simple theme.
```C#
[CreateAssetMenu(menuName = "Themes/My Simple Theme")]
public class MySimpleThemeDefinition : SimpleThemeDefinitionBase<MySimpleThemeId, MySimpleThemeStyle, TextMeshProUGUI> { }```

A full theme naturally requires a little more work. Use the following steps to guide you through the process:

1. Create a class that inherits from `ThemeDefinitionBase<TId, TStyle>`, filling in the appropriate type parameters. If you used a base theme style class as recommended, `TStyle` should be that base type; otherwise, you can use a generic type like `ThemeStyleBase<MyThemeStyleId>`. Add the `[CreateAssetMenu]` attribute to the class.
```C#
[CreateAssetMenu(menuName = "Themes/My Theme")]
public class MyThemeDefinition : ThemeDefinitionBase<MyThemeStyleId, MyThemeStyleBase> {

}```

2. The recommended way to both associate each style ID to the correct style type and allow configuration of each style is simply to add a serialized field for each style ID, initializing it with a new instance of the appropriate theme style class.
```C#
[CreateAssetMenu(menuName = "Themes/My Theme")]
public class MyThemeDefinition : ThemeDefinitionBase<MyThemeStyleId, MyThemeStyleBase> {
	[SerializeField]
	private MyRectThemeStyle headerContainerStyle = new(MyThemeStyleId.HeaderContainer);
	[SerializeField]
	private MyImageThemeStyle bannerImageStyle = new(MyThemeStyleId.BannerImage);
	[SerializeField]
	private MyImageThemeStyle iconStyle = new(MyThemeStyleId.Icon);
	...
}```

3. Finally, implement the required `AllStyles` property and `GetStyle(TId styleId)` method from the base class. There are multiple ways to do this and the way you handled the previous step will impact the options available. The recommended implementation for simplicity and maintainability is to use an internal Dictionary, as demonstrated here. Note that because it references other instance fields, we cannot initialize this dictionary with a field initializer; here we have opted to initialize it in the ScriptableObject's OnEnable message. You may also consider lazy-loading or other methods.
```C#
[CreateAssetMenu(menuName = "Themes/My Theme")]
public class MyThemeDefinition : ThemeDefinitionBase<MyThemeStyleId, MyThemeStyleBase> {
	...
	
	private Dictionary<MyThemeStyleId, MyThemeStyleBase> styles;
	
	private void OnEnable() {
		styles = new() {
			{ MyThemeStyleId.HeaderContainer, headerContainerStyle },
			{ MyThemeStyleId.BannerImage, bannerImageStyle },
			{ MyThemeStyleId.Icon, iconStyle },
			...
		};
		
		_readOnlyStyles = styles.Values.ToList().AsReadOnly();
	}
	
	private ReadOnlyCollection<MyThemeStyleBase> _readOnlyStyles;
	public override ReadOnlyCollection<MyThemeStyleBase> AllStyles {
		get => _readOnlyStyles;
	}
	
	public override MyThemeStyleBase GetStyle(MyThemeStyleId styleId)
		=> styles.TryGetValue(styleId, out MyThemeStyleBase style) ? style : null;
}```

Now with your theme definition class created, you can create instances of it by right-clicking in a folder in the project window, and navigating to the Create option and finding it in the menu (in the example, `Create > Themes > My Theme`). You can create any number of variants and configure each one differently through the inspector. But we still need a couple more types to handle managing the currently active theme and applying styles to elements, though fortunately these ones are easy.

#### A Theme Manager Class
The theme manager class is a ScriptableObject type that manages the list of valid theme definition assets and which one is currently active, and acts as the entry point for elements and systems looking to interact with this theming system. To create the type, all you need to do is create a class that inherits from `ThemeManagerBase<TTheme, TId, TStyle, TSelf>` and apply the CreateAssetMenu attribute. Nothing needs to be implemented within the class itself, it simply gives a concrete type for Unity to work with; the base class handles all of the necessary state and logic. The type parameters should be filled as follows:
- `TTheme`: Your theme definition class
- `TId`: Your theme style id enum
- `TStyle`: The base for your theme style classes; a concrete type is preferred, but the generic `ThemeStyleBase<TId>` can be used if needed
- `TSelf`: The manager type itself!
```C#
[CreateAssetMenu(menuName = "Themes/My Theme Manager")]
public class MyThemeManager : ThemeManagerBase<MyThemeDefinition, MyThemeStyleId, MyThemeStyleBase, MyThemeManager> { }```

Note that there should only be one asset created for a particular theme manager class, and it *MUST* be located in a folder named "Resources" anywhere under the Assets folder. You might consider removing the CreateAssetMenu attribute after creating your one instance to prevent any additional instances from being made accidentally. In the asset's inspector, make sure to fill in the list of valid theme definition assets and set the current theme index to a valid index in the list as desired. Remember that changes made to ScriptableObjects like the manager or theme definitions made in the editor's play mode may affect the saved assets in the project, but this will not be the case in a build; you would have to implement a save system to handle this in a build, which is beyond the scope of this document.

The manager follows a typical Singleton pattern, providing access to its instance, both in the editor and at runtime, through a static `Instance` property. So in our example, you could access the manager from code via `MyThemeManager.Instance`. From here, a number of properties and methods are provided for accessing or changing the current theme, managing valid themes (for example, if you wanted to add user-created themes at runtime), and applying styles to elements. It also has an `OnThemeChanged` event, which your code can subscribe to if it wants to be notified when the current theme is changed (this works both at runtime and in editor scripts).

#### One or More Themed Element Components
While you can at this point manage themes and apply them to elements through code, in most cases it would be simpler if the element already knew how to handle its theme. Themed element components exist for that purpose, allowing you to simply add the component and choose an appropriate style, and the component will automatically apply that style to the configured element and enforce it even if the theme variant or the settings for the chosen style are changed, both at runtime and edit-time. These components also have a public `Style` property, which can be used to programmatically change the applied style, allowing them to cover nearly any use-case.

To use themed element components, you simply need to inherit the `ThemedElementBase<TTarget, TManager, TTheme, TId, TStyle>` base class for each type of component your theme can be applied to. For a full theme, consider using an intermediate base class as in the example below, though this is not required. Fill in the type parameters as follows:
- `TTarget`: The type of component to which the style will be applied
- `TManager`: Your theme manager class
- `TTheme`: Your theme definition class
- `TId`: Your theme style id enum
- `TStyle`: The base for your theme style classes; a concrete type is preferred, but the generic `ThemeStyleBase<TId>` can be used if needed
```C#
public class MyThemedElementBase<T> : ThemedElementBase<T, MyThemeManager, MyThemeDefinition, MyThemeStyleId, MyThemeStyleBase>
	where T : Component { }

public class MyThemedImage : MyThemedElementBase<Image> { }

public class MyThemedRect : MyThemedElementBase<RectTransform> { }

...```