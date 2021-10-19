id: WizardWrx.ConsoleStreams
language: CSharp
name:
  Default: WizardWrx.ConsoleStreams
qualifiedName:
  Default: WizardWrx.ConsoleStreams
type: Assembly
modifiers: {}
items:
- id: WizardWrx.ConsoleStreams
  commentId: N:WizardWrx.ConsoleStreams
  language: CSharp
  name:
    CSharp: WizardWrx.ConsoleStreams
    VB: WizardWrx.ConsoleStreams
  nameWithType:
    CSharp: WizardWrx.ConsoleStreams
    VB: WizardWrx.ConsoleStreams
  qualifiedName:
    CSharp: WizardWrx.ConsoleStreams
    VB: WizardWrx.ConsoleStreams
  type: Namespace
  assemblies:
  - WizardWrx.ConsoleStreams
  modifiers: {}
  items:
  - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
    commentId: T:WizardWrx.ConsoleStreams.DefaultErrorMessageColors
    language: CSharp
    name:
      CSharp: DefaultErrorMessageColors
      VB: DefaultErrorMessageColors
    nameWithType:
      CSharp: DefaultErrorMessageColors
      VB: DefaultErrorMessageColors
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
      VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/DefaultErrorMessageColors.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: DefaultErrorMessageColors
      path: ../ConsoleStreams/DefaultErrorMessageColors.cs
      startLine: 103
    summary: "\nExpose the default fatal and nonfatal exception message colors, which\nare defined in a standard application configuration file that is linked\nto the assembly that defines this class.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class DefaultErrorMessageColors : PropertyDefaults'
        VB: >-
          Public Class DefaultErrorMessageColors

              Inherits PropertyDefaults
    inheritance:
    - System.Object
    - WizardWrx.Core.AssemblyLocatorBase
    - WizardWrx.Core.PropertyDefaults
    inheritedMembers:
    - WizardWrx.Core.PropertyDefaults.ValuesCollection
    - WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate(System.DateTimeKind)
    - WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
    - WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
    - WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
    - WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
    - WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
    - WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
    - WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
    - WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
    - WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
    - WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
    - WizardWrx.Core.AssemblyLocatorBase.DLLSettings
    - WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
    - WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting(System.String)
    - WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration(System.Type)
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor
      commentId: M:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor
      language: CSharp
      name:
        CSharp: DefaultErrorMessageColors()
        VB: DefaultErrorMessageColors()
      nameWithType:
        CSharp: DefaultErrorMessageColors.DefaultErrorMessageColors()
        VB: DefaultErrorMessageColors.DefaultErrorMessageColors()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors()
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors()
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 112
      summary: "\nThe default constructor deviates from the usual pattern by\nexplicitly calling the base constructor overload that accepts a\nreference to the assembly that defines this class, with the\nobjective of linking it to the application configuration file\nthat is linked to this DLL.\n"
      example: []
      syntax:
        content:
          CSharp: public DefaultErrorMessageColors()
          VB: Public Sub New
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor(System.Reflection.Assembly)
      commentId: M:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: DefaultErrorMessageColors(Assembly)
        VB: DefaultErrorMessageColors(Assembly)
      nameWithType:
        CSharp: DefaultErrorMessageColors.DefaultErrorMessageColors(Assembly)
        VB: DefaultErrorMessageColors.DefaultErrorMessageColors(Assembly)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors(System.Reflection.Assembly)
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors(System.Reflection.Assembly)
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 128
      summary: "\nThe overloaded constructor adheres to the usual pattern by\nexplicitly calling the base constructor with a reference to the DLL\nspecified by the caller, with the objective of linking it to the\napplication configuration file that is linked to aNonther DLL.\n"
      example: []
      syntax:
        content:
          CSharp: public DefaultErrorMessageColors(Assembly pasmLinkedAssembly)
          VB: Public Sub New(pasmLinkedAssembly As Assembly)
        parameters:
        - id: pasmLinkedAssembly
          type: System.Reflection.Assembly
          description: "\nSpecify the assembly to which the desired configuration file is\nlinked.\n"
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
      language: CSharp
      name:
        CSharp: FatalExceptionTextColor
        VB: FatalExceptionTextColor
      nameWithType:
        CSharp: DefaultErrorMessageColors.FatalExceptionTextColor
        VB: DefaultErrorMessageColors.FatalExceptionTextColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: FatalExceptionTextColor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 143
      summary: "\nGet or set the default text color for rendering reports of fatal\nexceptions.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor FatalExceptionTextColor { get; set; }
          VB: Public Property FatalExceptionTextColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
      language: CSharp
      name:
        CSharp: FatalExceptionBackgroundColor
        VB: FatalExceptionBackgroundColor
      nameWithType:
        CSharp: DefaultErrorMessageColors.FatalExceptionBackgroundColor
        VB: DefaultErrorMessageColors.FatalExceptionBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: FatalExceptionBackgroundColor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 153
      summary: "\nGet or set the default background color for rendering reports of \nfatal exceptions.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor FatalExceptionBackgroundColor { get; set; }
          VB: Public Property FatalExceptionBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
      language: CSharp
      name:
        CSharp: RecoverableExceptionTextColor
        VB: RecoverableExceptionTextColor
      nameWithType:
        CSharp: DefaultErrorMessageColors.RecoverableExceptionTextColor
        VB: DefaultErrorMessageColors.RecoverableExceptionTextColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RecoverableExceptionTextColor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 163
      summary: "\nGet or set the default text color for rendering reports of\nrecoverable exceptions.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor RecoverableExceptionTextColor { get; set; }
          VB: Public Property RecoverableExceptionTextColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
      language: CSharp
      name:
        CSharp: RecoverableExceptionBackgroundColor
        VB: RecoverableExceptionBackgroundColor
      nameWithType:
        CSharp: DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
        VB: DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RecoverableExceptionBackgroundColor
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 173
      summary: "\nGet or set the default background color for rendering reports of\nrecoverable exceptions.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor RecoverableExceptionBackgroundColor { get; set; }
          VB: Public Property RecoverableExceptionBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
      language: CSharp
      name:
        CSharp: PropsLeftAtDefault
        VB: PropsLeftAtDefault
      nameWithType:
        CSharp: DefaultErrorMessageColors.PropsLeftAtDefault
        VB: DefaultErrorMessageColors.PropsLeftAtDefault
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: PropsLeftAtDefault
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 188
      summary: "\nGet the count of properties that were omitted from the linked \nconfiguration file. When this value is greater than zero, generic\ndictionary MissingConfigSettings, inherited from the\nAssemblyLocatorBase base class, contains one string for each such\nproperty.\n"
      example: []
      syntax:
        content:
          CSharp: public int PropsLeftAtDefault { get; }
          VB: Public ReadOnly Property PropsLeftAtDefault As Integer
        parameters: []
        return:
          type: System.Int32
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault*
      seealso:
      - linkId: WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
        commentId: P:WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
      references:
        WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings: 
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
      commentId: P:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
      language: CSharp
      name:
        CSharp: PropsSetFromConfig
        VB: PropsSetFromConfig
      nameWithType:
        CSharp: DefaultErrorMessageColors.PropsSetFromConfig
        VB: DefaultErrorMessageColors.PropsSetFromConfig
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: PropsSetFromConfig
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 198
      summary: "\nGet the count of properties that were set from the linked\nconfiguration file.\n"
      example: []
      syntax:
        content:
          CSharp: public int PropsSetFromConfig { get; }
          VB: Public ReadOnly Property PropsSetFromConfig As Integer
        parameters: []
        return:
          type: System.Int32
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString
      commentId: M:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: DefaultErrorMessageColors.ToString()
        VB: DefaultErrorMessageColors.ToString()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString()
        VB: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString()
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/DefaultErrorMessageColors.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../ConsoleStreams/DefaultErrorMessageColors.cs
        startLine: 214
      summary: "\nOverride the default ToString method on the base class (object) to\nshow the key properties followed by the fully qualified class name.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nReturn a string similar to the following.\n\nTemplate: {Fatal: Text = FatalExceptionTextColor, FatalExceptionBackgroundColor = BackgroundColor; Recoverable: Text = RecoverableExceptionTextColor, Background = RecoverableExceptionBackgroundColor} WizardWrx.ConsoleStreams.DefaultErrorMessageColors\nTemplate: {{WizardWrx.ConsoleStreams.DefaultErrorMessageColors (Fatal: Text (Foreground) = FatalExceptionTextColor, Background = FatalExceptionBackgroundColor; Recoverable: Text (Foreground) = RecoverableExceptionTextColor, Background = RecoverableExceptionBackgroundColor)}}\n"
      overload: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString*
      overridden: System.Object.ToString
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
  - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColor
    language: CSharp
    name:
      CSharp: ErrorMessagesInColor
      VB: ErrorMessagesInColor
    nameWithType:
      CSharp: ErrorMessagesInColor
      VB: ErrorMessagesInColor
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor
      VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/ErrorMessagesInColor.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ErrorMessagesInColor
      path: ../ConsoleStreams/ErrorMessagesInColor.cs
      startLine: 242
    summary: "\nConsole.Error.Write and Console.Error.WriteLine methods that write in\nliving color.\n"
    remarks: "\nThere are two identical sets of methods.\n\n1) Static methods write text in your choice of foreground and background\ncolors, then revert the console colors to their initial values (that is,\nthe values they had when the program loaded).\n\n2) Instance methods go a step further, by maintaining a record of the\ncurrent colors, so that the colors can progress through a range, without\nreverting to the initial colors.\n\nFor each overload of Console.Error.Write, there are corresponding static\nand instance methods. The only difference in their signatures is that\nthese methods specify a foreground color and a background color,\nfollowed by the same arguments that would apply to the corresponding\noverload of the Console.Error.Write method.\n"
    example: []
    syntax:
      content:
        CSharp: >-
          [TypeConverter(typeof(ErrorMessagesInColorConverter))]

          [SettingsSerializeAs(SettingsSerializeAs.String)]

          public class ErrorMessagesInColor
        VB: >-
          <TypeConverter(GetType(ErrorMessagesInColorConverter))>

          <SettingsSerializeAs(SettingsSerializeAs.String)>

          Public Class ErrorMessagesInColor
    seealso:
    - linkId: WizardWrx.ConsoleStreams.MessageInColor
      commentId: T:WizardWrx.ConsoleStreams.MessageInColor
    inheritance:
    - System.Object
    inheritedMembers:
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    attributes:
    - type: System.ComponentModel.TypeConverterAttribute
      ctor: System.ComponentModel.TypeConverterAttribute.#ctor(System.Type)
      arguments:
      - type: System.Type
        value: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
    - type: System.Configuration.SettingsSerializeAsAttribute
      ctor: System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs)
      arguments:
      - type: System.Configuration.SettingsSerializeAs
        value: 0
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(System.Reflection.Assembly)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: InitializeDefaultPropertiesFromDllConfogurationFile(Assembly)
        VB: InitializeDefaultPropertiesFromDllConfogurationFile(Assembly)
      nameWithType:
        CSharp: ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(Assembly)
        VB: ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(Assembly)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(System.Reflection.Assembly)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile(System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: InitializeDefaultPropertiesFromDllConfogurationFile
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 291
      summary: "\nCall this static method to cause the configuration file linked to\nanother DLL to be the source from which default properties are read.\n\nTo have the intended effect, a call to this method must be the first\nreference to this class.\n"
      example: []
      syntax:
        content:
          CSharp: public static void InitializeDefaultPropertiesFromDllConfogurationFile(Assembly pasmLinkedAssembly)
          VB: Public Shared Sub InitializeDefaultPropertiesFromDllConfogurationFile(pasmLinkedAssembly As Assembly)
        parameters:
        - id: pasmLinkedAssembly
          type: System.Reflection.Assembly
          description: "\nSpecify the assembly with which the desired configuration file is\nlinked.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor
      language: CSharp
      name:
        CSharp: ErrorMessagesInColor()
        VB: ErrorMessagesInColor()
      nameWithType:
        CSharp: ErrorMessagesInColor.ErrorMessagesInColor()
        VB: ErrorMessagesInColor.ErrorMessagesInColor()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor()
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor()
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 367
      summary: "\nConstructing an instance saves the current foreground and background\ncolors into two pairs of read only ConsoleColor properties. Methods\nfacilitate changing one or both colors, while retaining the original\ncolors in the other two ConsoleColor properties, which are never\nchanged after the class instance comes into being.\n"
      remarks: "\nThis method is provided to facilitate construction of a List or\nother sortable collection of ErrorMessagesInColor objects, and \nallows for a future version of this class to be exposed to COM.\n"
      example: []
      syntax:
        content:
          CSharp: public ErrorMessagesInColor()
          VB: Public Sub New
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor(System.ConsoleColor,System.ConsoleColor)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor(System.ConsoleColor,System.ConsoleColor)
      language: CSharp
      name:
        CSharp: ErrorMessagesInColor(ConsoleColor, ConsoleColor)
        VB: ErrorMessagesInColor(ConsoleColor, ConsoleColor)
      nameWithType:
        CSharp: ErrorMessagesInColor.ErrorMessagesInColor(ConsoleColor, ConsoleColor)
        VB: ErrorMessagesInColor.ErrorMessagesInColor(ConsoleColor, ConsoleColor)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor(System.ConsoleColor, System.ConsoleColor)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor(System.ConsoleColor, System.ConsoleColor)
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 390
      summary: "\nThis constructor creates an instance with its foreground and\nbackground colors properties set to the specified ConsoleColor\nvalues, which presumably differ from the initial foreground and\nbackground colors.\n"
      example: []
      syntax:
        content:
          CSharp: public ErrorMessagesInColor(ConsoleColor pclrTextForeColor, ConsoleColor pclrTextBackColor)
          VB: Public Sub New(pclrTextForeColor As ConsoleColor, pclrTextBackColor As ConsoleColor)
        parameters:
        - id: pclrTextForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the text (foreground)\ncolor in generated messages.\n"
        - id: pclrTextBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the background color in\ngenerated messages.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
      language: CSharp
      name:
        CSharp: OriginalBackgroundColor
        VB: OriginalBackgroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.OriginalBackgroundColor
        VB: ErrorMessagesInColor.OriginalBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: OriginalBackgroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 407
      summary: "\nGets the Console.BackgroundColor that was in force when the instance\nwas constructed.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor OriginalBackgroundColor { get; }
          VB: Public ReadOnly Property OriginalBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
      language: CSharp
      name:
        CSharp: OriginalForegroundColor
        VB: OriginalForegroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.OriginalForegroundColor
        VB: ErrorMessagesInColor.OriginalForegroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: OriginalForegroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 417
      summary: "\nGets the Console.ForegroundColor that was in force when the instance\nwas constructed.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor OriginalForegroundColor { get; }
          VB: Public ReadOnly Property OriginalForegroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
      language: CSharp
      name:
        CSharp: MessageBackgroundColor
        VB: MessageBackgroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.MessageBackgroundColor
        VB: ErrorMessagesInColor.MessageBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: MessageBackgroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 427
      summary: "\nGets or sets the Console.BackgroundColor to use for messages\ngenerated by the instance Write and WriteLine methods.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor MessageBackgroundColor { get; set; }
          VB: Public Property MessageBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
      language: CSharp
      name:
        CSharp: MessageForegroundColor
        VB: MessageForegroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.MessageForegroundColor
        VB: ErrorMessagesInColor.MessageForegroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: MessageForegroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 438
      summary: "\nGets or sets the Console.ForegroundColor to use for messages\ngenerated by the instance Write and WriteLine methods.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor MessageForegroundColor { get; set; }
          VB: Public Property MessageForegroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Boolean)
      language: CSharp
      name:
        CSharp: WriteLine(Boolean)
        VB: WriteLine(Boolean)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Boolean)
        VB: ErrorMessagesInColor.WriteLine(Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Boolean)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 453
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(bool value)
          VB: Public Sub WriteLine(value As Boolean)
        parameters:
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char)
      language: CSharp
      name:
        CSharp: WriteLine(Char)
        VB: WriteLine(Char)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Char)
        VB: ErrorMessagesInColor.WriteLine(Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 469
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char value)
          VB: Public Sub WriteLine(value As Char)
        parameters:
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[])
      language: CSharp
      name:
        CSharp: WriteLine(Char[])
        VB: WriteLine(Char())
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Char[])
        VB: ErrorMessagesInColor.WriteLine(Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 485
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char[] buffer)
          VB: Public Sub WriteLine(buffer As Char())
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Decimal)
      language: CSharp
      name:
        CSharp: WriteLine(Decimal)
        VB: WriteLine(Decimal)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Decimal)
        VB: ErrorMessagesInColor.WriteLine(Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Decimal)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 501
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(decimal value)
          VB: Public Sub WriteLine(value As Decimal)
        parameters:
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Double)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Double)
      language: CSharp
      name:
        CSharp: WriteLine(Double)
        VB: WriteLine(Double)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Double)
        VB: ErrorMessagesInColor.WriteLine(Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Double)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 517
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(double value)
          VB: Public Sub WriteLine(value As Double)
        parameters:
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Single)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Single)
      language: CSharp
      name:
        CSharp: WriteLine(Single)
        VB: WriteLine(Single)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Single)
        VB: ErrorMessagesInColor.WriteLine(Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Single)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 533
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(float value)
          VB: Public Sub WriteLine(value As Single)
        parameters:
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int32)
      language: CSharp
      name:
        CSharp: WriteLine(Int32)
        VB: WriteLine(Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Int32)
        VB: ErrorMessagesInColor.WriteLine(Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 549
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(int value)
          VB: Public Sub WriteLine(value As Integer)
        parameters:
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int64)
      language: CSharp
      name:
        CSharp: WriteLine(Int64)
        VB: WriteLine(Int64)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Int64)
        VB: ErrorMessagesInColor.WriteLine(Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 565
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(long value)
          VB: Public Sub WriteLine(value As Long)
        parameters:
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(Object)
        VB: WriteLine(Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Object)
        VB: ErrorMessagesInColor.WriteLine(Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 581
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(object value)
          VB: Public Sub WriteLine(value As Object)
        parameters:
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String)
      language: CSharp
      name:
        CSharp: WriteLine(String)
        VB: WriteLine(String)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String)
        VB: ErrorMessagesInColor.WriteLine(String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 597
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string value)
          VB: Public Sub WriteLine(value As String)
        parameters:
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt32)
      language: CSharp
      name:
        CSharp: WriteLine(UInt32)
        VB: WriteLine(UInt32)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(UInt32)
        VB: ErrorMessagesInColor.WriteLine(UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 614
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(uint value)
          VB: Public Sub WriteLine(value As UInteger)
        parameters:
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt64)
      language: CSharp
      name:
        CSharp: WriteLine(UInt64)
        VB: WriteLine(UInt64)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(UInt64)
        VB: ErrorMessagesInColor.WriteLine(UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 631
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(ulong value)
          VB: Public Sub WriteLine(value As ULong)
        parameters:
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object)
        VB: WriteLine(String, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String, Object)
        VB: ErrorMessagesInColor.WriteLine(String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 653
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0)
          VB: Public Sub WriteLine(format As String, arg0 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substitution token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object[])
      language: CSharp
      name:
        CSharp: WriteLine(String, Object[])
        VB: WriteLine(String, Object())
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String, Object[])
        VB: ErrorMessagesInColor.WriteLine(String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 678
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, params object[] arg)
          VB: Public Sub WriteLine(format As String, ParamArray arg As Object())
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: WriteLine(Char[], Int32, Int32)
        VB: WriteLine(Char(), Int32, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(Char[], Int32, Int32)
        VB: ErrorMessagesInColor.WriteLine(Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 703
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char[] buffer, int index, int count)
          VB: Public Sub WriteLine(buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object)
        VB: WriteLine(String, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String, Object, Object)
        VB: ErrorMessagesInColor.WriteLine(String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 731
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstitution tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object, Object)
        VB: WriteLine(String, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String, Object, Object, Object)
        VB: ErrorMessagesInColor.WriteLine(String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 763
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1, object arg2)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstitution tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object, Object, Object)
        VB: WriteLine(String, Object, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.WriteLine(String, Object, Object, Object, Object)
        VB: ErrorMessagesInColor.WriteLine(String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine(System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 800
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstitution tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Boolean)
      language: CSharp
      name:
        CSharp: Write(Boolean)
        VB: Write(Boolean)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Boolean)
        VB: ErrorMessagesInColor.Write(Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Boolean)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 822
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(bool value)
          VB: Public Sub Write(value As Boolean)
        parameters:
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char)
      language: CSharp
      name:
        CSharp: Write(Char)
        VB: Write(Char)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Char)
        VB: ErrorMessagesInColor.Write(Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 837
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char value)
          VB: Public Sub Write(value As Char)
        parameters:
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[])
      language: CSharp
      name:
        CSharp: Write(Char[])
        VB: Write(Char())
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Char[])
        VB: ErrorMessagesInColor.Write(Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 852
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char[] buffer)
          VB: Public Sub Write(buffer As Char())
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Decimal)
      language: CSharp
      name:
        CSharp: Write(Decimal)
        VB: Write(Decimal)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Decimal)
        VB: ErrorMessagesInColor.Write(Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Decimal)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 867
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(decimal value)
          VB: Public Sub Write(value As Decimal)
        parameters:
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Double)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Double)
      language: CSharp
      name:
        CSharp: Write(Double)
        VB: Write(Double)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Double)
        VB: ErrorMessagesInColor.Write(Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Double)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 882
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(double value)
          VB: Public Sub Write(value As Double)
        parameters:
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Single)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Single)
      language: CSharp
      name:
        CSharp: Write(Single)
        VB: Write(Single)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Single)
        VB: ErrorMessagesInColor.Write(Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Single)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 897
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(float value)
          VB: Public Sub Write(value As Single)
        parameters:
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int32)
      language: CSharp
      name:
        CSharp: Write(Int32)
        VB: Write(Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Int32)
        VB: ErrorMessagesInColor.Write(Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 912
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(int value)
          VB: Public Sub Write(value As Integer)
        parameters:
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int64)
      language: CSharp
      name:
        CSharp: Write(Int64)
        VB: Write(Int64)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Int64)
        VB: ErrorMessagesInColor.Write(Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 927
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(long value)
          VB: Public Sub Write(value As Long)
        parameters:
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Object)
      language: CSharp
      name:
        CSharp: Write(Object)
        VB: Write(Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Object)
        VB: ErrorMessagesInColor.Write(Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 942
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(object value)
          VB: Public Sub Write(value As Object)
        parameters:
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String)
      language: CSharp
      name:
        CSharp: Write(String)
        VB: Write(String)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String)
        VB: ErrorMessagesInColor.Write(String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 957
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string value)
          VB: Public Sub Write(value As String)
        parameters:
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt32)
      language: CSharp
      name:
        CSharp: Write(UInt32)
        VB: Write(UInt32)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(UInt32)
        VB: ErrorMessagesInColor.Write(UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 973
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(uint value)
          VB: Public Sub Write(value As UInteger)
        parameters:
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt64)
      language: CSharp
      name:
        CSharp: Write(UInt64)
        VB: Write(UInt64)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(UInt64)
        VB: ErrorMessagesInColor.Write(UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 989
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(ulong value)
          VB: Public Sub Write(value As ULong)
        parameters:
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object)
        VB: Write(String, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String, Object)
        VB: ErrorMessagesInColor.Write(String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1010
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0)
          VB: Public Sub Write(format As String, arg0 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substitution token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object[])
      language: CSharp
      name:
        CSharp: Write(String, Object[])
        VB: Write(String, Object())
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String, Object[])
        VB: ErrorMessagesInColor.Write(String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1034
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, params object[] arg)
          VB: Public Sub Write(format As String, ParamArray arg As Object())
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: Write(Char[], Int32, Int32)
        VB: Write(Char(), Int32, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(Char[], Int32, Int32)
        VB: ErrorMessagesInColor.Write(Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1058
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char[] buffer, int index, int count)
          VB: Public Sub Write(buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object)
        VB: Write(String, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String, Object, Object)
        VB: ErrorMessagesInColor.Write(String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1085
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstitution tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object, Object)
        VB: Write(String, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String, Object, Object, Object)
        VB: ErrorMessagesInColor.Write(String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1116
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1, object arg2)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstitution tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object, Object, Object)
        VB: Write(String, Object, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.Write(String, Object, Object, Object, Object)
        VB: ErrorMessagesInColor.Write(String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write(System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1152
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstitution tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: ErrorMessagesInColor.ToString()
        VB: ErrorMessagesInColor.ToString()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString()
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString()
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1184
      summary: "\nOverride the default ToString method inherited from the base class\n(object) to display the most significant properties, the text, or\nforeground, and background colors set by the constructor, followed\nby the fully qualified type name.\n"
      remarks: "\nThough this method could have easily been implemented inline, using\nthe shared message template, moving the implementation out of line\naffords the flexibility to rearrange the display consistently, even\nif that requires the properties to be reordered.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nThe return value is a string of the following format.\n\n{Text = ConsoleColorText, Background = BackroundColor} WizardWrx.ConsoleStreams.ErrorMessagesInColor\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString*
      overridden: System.Object.ToString
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
      language: CSharp
      name:
        CSharp: FatalExceptionTextColor
        VB: FatalExceptionTextColor
      nameWithType:
        CSharp: ErrorMessagesInColor.FatalExceptionTextColor
        VB: ErrorMessagesInColor.FatalExceptionTextColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: FatalExceptionTextColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1201
      summary: "\nGet the color to apply to the text of a fatal exception message.\n\nThe return value is a member of the System.ConsoleColors enumeration\nthat is intended to be treated as a foreground (text) color.\n"
      example: []
      syntax:
        content:
          CSharp: public static ConsoleColor FatalExceptionTextColor { get; set; }
          VB: Public Shared Property FatalExceptionTextColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor*
      modifiers:
        CSharp:
        - public
        - static
        - get
        - set
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
      language: CSharp
      name:
        CSharp: FatalExceptionBackgroundColor
        VB: FatalExceptionBackgroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.FatalExceptionBackgroundColor
        VB: ErrorMessagesInColor.FatalExceptionBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: FatalExceptionBackgroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1214
      summary: "\nGet the color to use as the background of a fatal exception message.\n\nThe return value is a member of the System.ConsoleColors enumeration\nthat is intended to be treated as a background color.\n"
      example: []
      syntax:
        content:
          CSharp: public static ConsoleColor FatalExceptionBackgroundColor { get; set; }
          VB: Public Shared Property FatalExceptionBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor*
      modifiers:
        CSharp:
        - public
        - static
        - get
        - set
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
      language: CSharp
      name:
        CSharp: RecoverableExceptionTextColor
        VB: RecoverableExceptionTextColor
      nameWithType:
        CSharp: ErrorMessagesInColor.RecoverableExceptionTextColor
        VB: ErrorMessagesInColor.RecoverableExceptionTextColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RecoverableExceptionTextColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1228
      summary: "\nGet the color to apply to the text of a recoverable exception\nmessage.\n\nThe return value is a member of the System.ConsoleColors enumeration\nthat is intended to be treated as a foreground (text) color.\n"
      example: []
      syntax:
        content:
          CSharp: public static ConsoleColor RecoverableExceptionTextColor { get; set; }
          VB: Public Shared Property RecoverableExceptionTextColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor*
      modifiers:
        CSharp:
        - public
        - static
        - get
        - set
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
      language: CSharp
      name:
        CSharp: RecoverableExceptionBackgroundColor
        VB: RecoverableExceptionBackgroundColor
      nameWithType:
        CSharp: ErrorMessagesInColor.RecoverableExceptionBackgroundColor
        VB: ErrorMessagesInColor.RecoverableExceptionBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RecoverableExceptionBackgroundColor
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1242
      summary: "\nGet the color to use as the background of a recoverable exception\nmessage.\n\nThe return value is a member of the System.ConsoleColors enumeration\nthat is intended to be treated as a background color.\n"
      example: []
      syntax:
        content:
          CSharp: public static ConsoleColor RecoverableExceptionBackgroundColor { get; set; }
          VB: Public Shared Property RecoverableExceptionBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor*
      modifiers:
        CSharp:
        - public
        - static
        - get
        - set
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Boolean)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1263
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, bool value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Boolean)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1287
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Char)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[])
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char[])
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char())
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char[])
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1311
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Decimal)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1335
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, decimal value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Decimal)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Double)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Double)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Double)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Double)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Double)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Double)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1359
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, double value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Double)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Single)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Single)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Single)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Single)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Single)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Single)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1383
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, float value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Single)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1407
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, int value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int64)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1431
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, long value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Long)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Object)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1455
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, object value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1479
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As String)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1504
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, uint value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As UInteger)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1529
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, ulong value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As ULong)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1559
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substitution token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object[])
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object())
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object[])
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1592
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, params object[] arg)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, ParamArray arg As Object())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1625
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer, int index, int count)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1661
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstitution tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1701
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstitution tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: ErrorMessagesInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1746
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstitution tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Boolean)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Boolean)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Boolean)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Boolean)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1776
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, bool value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Boolean)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1799
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Char)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[])
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char[])
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char())
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char[])
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1822
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Decimal)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Decimal)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Decimal)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Decimal)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1845
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, decimal value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Decimal)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Double)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Double)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Double)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Double)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Double)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Double)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1868
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, double value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Double)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Single)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Single)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Single)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Single)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Single)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Single)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1891
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, float value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Single)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Int32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Int32)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1914
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, int value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int64)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Int64)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Int64)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Int64)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1937
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, long value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Long)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Object)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1960
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, object value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 1983
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As String)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, UInt32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, UInt32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt32)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2007
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, uint value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As UInteger)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, UInt64)
        VB: RGBWrite(ConsoleColor, ConsoleColor, UInt64)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt64)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt64)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2031
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, ulong value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As ULong)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2060
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substitution token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object[])
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object())
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object[])
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2092
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, params object[] arg)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, ParamArray arg As Object())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2124
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer, int index, int count)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2159
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstitution tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2198
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstitution tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      nameWithType:
        CSharp: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: ErrorMessagesInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2242
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstitution tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      language: CSharp
      name:
        CSharp: GetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity)
        VB: GetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity)
      nameWithType:
        CSharp: ErrorMessagesInColor.GetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity)
        VB: ErrorMessagesInColor.GetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetDefaultErrorMessageColors
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2272
      summary: "\nReturn a new ErrorMessagesInColor object with its text and\nbackground colors initialized from the specified default color pair.\n"
      example: []
      syntax:
        content:
          CSharp: public static ErrorMessagesInColor GetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity penmErrorSeverity)
          VB: Public Shared Function GetDefaultErrorMessageColors(penmErrorSeverity As ErrorMessagesInColor.ErrorSeverity) As ErrorMessagesInColor
        parameters:
        - id: penmErrorSeverity
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
          description: "\nSpecify a member of the ErrorSeverity enumeration to indicate which\nof the two default color schemes is wanted.\n"
        return:
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor
          description: "\nThe returned ErrorMessagesInColor object is ready for use with the\ninstance Write and WriteLine methods to display error messages of\nthe specified type on the STDERR (Standard Error) console stream.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      language: CSharp
      name:
        CSharp: GetDefaultMessageColors(ErrorMessagesInColor.ErrorSeverity)
        VB: GetDefaultMessageColors(ErrorMessagesInColor.ErrorSeverity)
      nameWithType:
        CSharp: ErrorMessagesInColor.GetDefaultMessageColors(ErrorMessagesInColor.ErrorSeverity)
        VB: ErrorMessagesInColor.GetDefaultMessageColors(ErrorMessagesInColor.ErrorSeverity)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetDefaultMessageColors
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2308
      summary: "\nReturn a new MessageInColor object with its text and background\ncolors initialized from the specified default color pair.\n"
      example: []
      syntax:
        content:
          CSharp: public static MessageInColor GetDefaultMessageColors(ErrorMessagesInColor.ErrorSeverity penmErrorSeverity)
          VB: Public Shared Function GetDefaultMessageColors(penmErrorSeverity As ErrorMessagesInColor.ErrorSeverity) As MessageInColor
        parameters:
        - id: penmErrorSeverity
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
          description: "\nSpecify a member of the ErrorSeverity enumeration to indicate which\nof the two default color schemes is wanted.\n"
        return:
          type: WizardWrx.ConsoleStreams.MessageInColor
          description: "\nThe returned MessageInColor object is ready for use with the\ninstance Write and WriteLine methods of its class to display\nmessages of the specified type on the STDOUT (Standard Output)\nconsole stream.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity,System.ConsoleColor,System.ConsoleColor)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity,System.ConsoleColor,System.ConsoleColor)
      language: CSharp
      name:
        CSharp: SetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity, ConsoleColor, ConsoleColor)
        VB: SetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity, ConsoleColor, ConsoleColor)
      nameWithType:
        CSharp: ErrorMessagesInColor.SetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity, ConsoleColor, ConsoleColor)
        VB: ErrorMessagesInColor.SetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity, ConsoleColor, ConsoleColor)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity, System.ConsoleColor, System.ConsoleColor)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors(WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity, System.ConsoleColor, System.ConsoleColor)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: SetDefaultErrorMessageColors
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2349
      summary: "\nSimultaneously override the default text and background colors that\nare read into four static ConsoleColor properties when the library\ninitializes. This can also be accomplished by setting the text and\nbackground color properties separately, but this accomplishes the\ntask with one method call.\n"
      example: []
      syntax:
        content:
          CSharp: public static void SetDefaultErrorMessageColors(ErrorMessagesInColor.ErrorSeverity penmErrorSeverity, ConsoleColor pclrTextForeColor, ConsoleColor pclrTextBackColor)
          VB: Public Shared Sub SetDefaultErrorMessageColors(penmErrorSeverity As ErrorMessagesInColor.ErrorSeverity, pclrTextForeColor As ConsoleColor, pclrTextBackColor As ConsoleColor)
        parameters:
        - id: penmErrorSeverity
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
          description: "\nSpecify a member of the ErrorSeverity enumeration to indicate which\nof the two default color schemes is wanted.\n"
        - id: pclrTextForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the text (foreground)\ncolor in generated messages.\n"
        - id: pclrTextBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the background color in\ngenerated messages.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    references:
      WizardWrx.ConsoleStreams.MessageInColor: 
  - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
    language: CSharp
    name:
      CSharp: ErrorMessagesInColor.ErrorSeverity
      VB: ErrorMessagesInColor.ErrorSeverity
    nameWithType:
      CSharp: ErrorMessagesInColor.ErrorSeverity
      VB: ErrorMessagesInColor.ErrorSeverity
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
      VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
    type: Enum
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/ErrorMessagesInColor.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ErrorSeverity
      path: ../ConsoleStreams/ErrorMessagesInColor.cs
      startLine: 251
    summary: "\nUse this enumeration to specify the desired color scheme to the\nGetDefaultErrorMessageColors and GetDefaultMessageColors methods.\n"
    example: []
    syntax:
      content:
        CSharp: public enum ErrorSeverity
        VB: Public Enum ErrorSeverity
    extensionMethods:
    - WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Fatal
      commentId: F:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Fatal
      language: CSharp
      name:
        CSharp: Fatal
        VB: Fatal
      nameWithType:
        CSharp: ErrorMessagesInColor.ErrorSeverity.Fatal
        VB: ErrorMessagesInColor.ErrorSeverity.Fatal
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Fatal
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Fatal
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Fatal
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 256
      summary: "\nThe error is fatal, and the program has terminated.\n"
      example: []
      syntax:
        content:
          CSharp: Fatal = 0
          VB: Fatal = 0
        return:
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Recoverable
      commentId: F:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Recoverable
      language: CSharp
      name:
        CSharp: Recoverable
        VB: Recoverable
      nameWithType:
        CSharp: ErrorMessagesInColor.ErrorSeverity.Recoverable
        VB: ErrorMessagesInColor.ErrorSeverity.Recoverable
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Recoverable
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.Recoverable
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Recoverable
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 263
      summary: "\nThe program recovered from the error, and the message is for\nyour information. You can probably prevent it by some corrective\naction.\n"
      example: []
      syntax:
        content:
          CSharp: Recoverable = 1
          VB: Recoverable = 1
        return:
          type: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
    language: CSharp
    name:
      CSharp: ErrorMessagesInColorConverter
      VB: ErrorMessagesInColorConverter
    nameWithType:
      CSharp: ErrorMessagesInColorConverter
      VB: ErrorMessagesInColorConverter
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
      VB: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/ErrorMessagesInColor.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ErrorMessagesInColorConverter
      path: ../ConsoleStreams/ErrorMessagesInColor.cs
      startLine: 2500
    summary: "\nAlthough its scope is public, the only practical use for this class is\nto facilitate storage of default or user specified ErrorMessagesInColor\nvalues in application settings files. That being the case, I put it at\nthe end of the source file that defines that class.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class ErrorMessagesInColorConverter : TypeConverter'
        VB: >-
          Public Class ErrorMessagesInColorConverter

              Inherits TypeConverter
    inheritance:
    - System.Object
    - System.ComponentModel.TypeConverter
    inheritedMembers:
    - System.ComponentModel.TypeConverter.CanConvertFrom(System.Type)
    - System.ComponentModel.TypeConverter.CanConvertTo(System.Type)
    - System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type)
    - System.ComponentModel.TypeConverter.ConvertFrom(System.Object)
    - System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String)
    - System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
    - System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type)
    - System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object)
    - System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
    - System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    - System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)
    - System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)
    - System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)
    - System.ComponentModel.TypeConverter.GetCreateInstanceSupported
    - System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetProperties(System.Object)
    - System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])
    - System.ComponentModel.TypeConverter.GetPropertiesSupported
    - System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValues
    - System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValuesExclusive
    - System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValuesSupported
    - System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.IsValid(System.Object)
    - System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[])
    - System.Object.ToString
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      language: CSharp
      name:
        CSharp: CanConvertFrom(ITypeDescriptorContext, Type)
        VB: CanConvertFrom(ITypeDescriptorContext, Type)
      nameWithType:
        CSharp: ErrorMessagesInColorConverter.CanConvertFrom(ITypeDescriptorContext, Type)
        VB: ErrorMessagesInColorConverter.CanConvertFrom(ITypeDescriptorContext, Type)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Type)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Type)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CanConvertFrom
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2524
      summary: "\nReturn True if inputs of the specified type can be converted.\n"
      remarks: "\nThis method and its companions ConvertFrom and ConvertTo are \ndelegates, which the runtime engine calls as needed. Hence, the\narguments described above as black boxes are required, although this\nimplementation ignores them, since it processes string\nrepresentations of the System.Console.ConsoleColors enumerated type.\n"
      example: []
      syntax:
        content:
          CSharp: public override bool CanConvertFrom(ITypeDescriptorContext pIContext, Type ptypSourceType)
          VB: Public Overrides Function CanConvertFrom(pIContext As ITypeDescriptorContext, ptypSourceType As Type) As Boolean
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: ptypSourceType
          type: System.Type
          description: "\nThis argument specifies the System.Type to be evaluated. Treat it as\na black box.\n"
        return:
          type: System.Boolean
          description: "\nThis method returns TRUE if ptypSourceType is typeof ( string ). Any\nother type returns FALSE.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom*
      overridden: System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      language: CSharp
      name:
        CSharp: ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
        VB: ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
      nameWithType:
        CSharp: ErrorMessagesInColorConverter.ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
        VB: ErrorMessagesInColorConverter.ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ConvertFrom
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2562
      summary: "\nConvert from string (the only supported source type) to\nErrorMessagesInColor.\n"
      remarks: "\nThis method and its companions CanConvertFrom and ConvertTo are \ndelegates, which the runtime engine calls as needed. Hence, the\narguments described above as black boxes are required, although this\nimplementation ignores them, since it processes string\nrepresentations of the System.Console.ConsoleColors enumerated type.\n"
      example: []
      syntax:
        content:
          CSharp: public override object ConvertFrom(ITypeDescriptorContext pIContext, CultureInfo pCulture, object pobjValue)
          VB: Public Overrides Function ConvertFrom(pIContext As ITypeDescriptorContext, pCulture As CultureInfo, pobjValue As Object) As Object
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: pCulture
          type: System.Globalization.CultureInfo
          description: "\nThis argument supplies a reference to the current CultureInfo object\nthat drives many aspects of text and numeric conversions. Treat it as\na black box.\n"
        - id: pobjValue
          type: System.Object
          description: "\nSpecify the source object to be converted. Although the method\nsignature requires this argument to be cast to Object, the only type\nsupported is System.string. In any event, treat it as a black box.\n"
        return:
          type: System.Object
          description: "\nAlthough specified as object to meet the requirements of the base\nclass, the return value is expected to be an ErrorMessagesInColor\nobject.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom*
      overridden: System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      commentId: M:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      language: CSharp
      name:
        CSharp: ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
        VB: ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
      nameWithType:
        CSharp: ErrorMessagesInColorConverter.ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
        VB: ErrorMessagesInColorConverter.ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object, System.Type)
        VB: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object, System.Type)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/ErrorMessagesInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ConvertTo
        path: ../ConsoleStreams/ErrorMessagesInColor.cs
        startLine: 2622
      summary: "\nGiven an ErrorMessagesInColor object, return a string representation\nthat is suitable for storage in a standard application settings file.\n"
      remarks: "\nThis method and its companions CanConvertFrom and ConvertFrom are \ndelegates, which the runtime engine calls as needed. Hence, the\narguments described above as black boxes are required, although this\nimplementation ignores them, since it processes string\nrepresentations of the System.Console.ConsoleColors enumerated type.\n"
      example: []
      syntax:
        content:
          CSharp: public override object ConvertTo(ITypeDescriptorContext pIContext, CultureInfo pCulture, object pobjValue, Type pDestType)
          VB: Public Overrides Function ConvertTo(pIContext As ITypeDescriptorContext, pCulture As CultureInfo, pobjValue As Object, pDestType As Type) As Object
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: pCulture
          type: System.Globalization.CultureInfo
          description: "\nThis argument supplies a reference to the current CultureInfo object\nthat drives many aspects of text and numeric conversions. Treat it as\na black box.\n"
        - id: pobjValue
          type: System.Object
          description: "\nAlthough the method signature calls for an generic System.Object,\nthis argument must actually be an ErrorMessagesInColor object.\n"
        - id: pDestType
          type: System.Type
          description: "\nThe only valid value for this argument is typeof ( string ). The\nspecification type is dictated by the signature of the ConvertTo\nmethod in the base class.\n"
        return:
          type: System.Object
          description: "\nAlthough specified as object to meet the requirements of the base\nclass, the return value is expected to be a System.string.\n"
      overload: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo*
      overridden: System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
  - id: WizardWrx.ConsoleStreams.MessageInColor
    commentId: T:WizardWrx.ConsoleStreams.MessageInColor
    language: CSharp
    name:
      CSharp: MessageInColor
      VB: MessageInColor
    nameWithType:
      CSharp: MessageInColor
      VB: MessageInColor
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.MessageInColor
      VB: WizardWrx.ConsoleStreams.MessageInColor
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/MessageInColor.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: MessageInColor
      path: ../ConsoleStreams/MessageInColor.cs
      startLine: 213
    summary: "\nConsole.Write and Console.WriteLine methods that write in living color.\n"
    remarks: "\nThere are two identical sets of methods.\n\n1) Static methods write text in your choice of foreground and background\ncolors, then revert the console colors to their initial values (that is,\nthe values they had when the program loaded).\n\n2) Instance methods go a step further, by maintaining a record of the\ncurrent colors, so that the colors can progress through a range, without\nreverting to the initial colors.\n\nFor each overload of Console.Write, there are corresponding static and\ninstance methods. The only difference in their signatures is that these\nmethods specify a foreground color and a background color, followed by\nthe same arguments that would apply to the corresponding overload of the\nConsole.Write method.\n"
    example: []
    syntax:
      content:
        CSharp: >-
          [TypeConverter(typeof(MessageInColorConverter))]

          [SettingsSerializeAs(SettingsSerializeAs.String)]

          public class MessageInColor
        VB: >-
          <TypeConverter(GetType(MessageInColorConverter))>

          <SettingsSerializeAs(SettingsSerializeAs.String)>

          Public Class MessageInColor
    seealso:
    - linkId: WizardWrx.ConsoleStreams.ErrorMessagesInColor
      commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColor
    - linkId: WizardWrx.Core.PropertyDefaults
      commentId: T:WizardWrx.Core.PropertyDefaults
    inheritance:
    - System.Object
    inheritedMembers:
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    attributes:
    - type: System.ComponentModel.TypeConverterAttribute
      ctor: System.ComponentModel.TypeConverterAttribute.#ctor(System.Type)
      arguments:
      - type: System.Type
        value: WizardWrx.ConsoleStreams.MessageInColorConverter
    - type: System.Configuration.SettingsSerializeAsAttribute
      ctor: System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs)
      arguments:
      - type: System.Configuration.SettingsSerializeAs
        value: 0
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ConsoleStreams.MessageInColor.#ctor
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.#ctor
      language: CSharp
      name:
        CSharp: MessageInColor()
        VB: MessageInColor()
      nameWithType:
        CSharp: MessageInColor.MessageInColor()
        VB: MessageInColor.MessageInColor()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor()
        VB: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor()
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 242
      summary: "\nConstructing an instance saves the current foreground and background\ncolors into two pairs of read only ConsoleColor properties. Methods\nfacilitate changing one or both colors, while retaining the original\ncolors in the other two ConsoleColor properties, which are never\nchanged after the class instance comes into being.\n"
      remarks: "\nThis method is provided to facilitate construction of a List or\nother sortable collection of MessageInColor objects, and allows for\na future version of this class to be exposed to COM.\n"
      example: []
      syntax:
        content:
          CSharp: public MessageInColor()
          VB: Public Sub New
      overload: WizardWrx.ConsoleStreams.MessageInColor.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.#ctor(System.ConsoleColor,System.ConsoleColor)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.#ctor(System.ConsoleColor,System.ConsoleColor)
      language: CSharp
      name:
        CSharp: MessageInColor(ConsoleColor, ConsoleColor)
        VB: MessageInColor(ConsoleColor, ConsoleColor)
      nameWithType:
        CSharp: MessageInColor.MessageInColor(ConsoleColor, ConsoleColor)
        VB: MessageInColor.MessageInColor(ConsoleColor, ConsoleColor)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor(System.ConsoleColor, System.ConsoleColor)
        VB: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor(System.ConsoleColor, System.ConsoleColor)
      type: Constructor
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 265
      summary: "\nThis constructor creates an instance with its foreground and\nbackground colors properties set to the specified ConsoleColor\nvalues, which presumably differ from the initial foreground and\nbackground colors.\n"
      example: []
      syntax:
        content:
          CSharp: public MessageInColor(ConsoleColor pclrTextForeColor, ConsoleColor pclrTextBackColor)
          VB: Public Sub New(pclrTextForeColor As ConsoleColor, pclrTextBackColor As ConsoleColor)
        parameters:
        - id: pclrTextForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the text (foreground)\ncolor in generated messages.\n"
        - id: pclrTextBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor property to use as the background color in\ngenerated messages.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
      language: CSharp
      name:
        CSharp: OriginalBackgroundColor
        VB: OriginalBackgroundColor
      nameWithType:
        CSharp: MessageInColor.OriginalBackgroundColor
        VB: MessageInColor.OriginalBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
        VB: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: OriginalBackgroundColor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 282
      summary: "\nGets the Console.BackgroundColor that was in force when the instance\nwas constructed.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor OriginalBackgroundColor { get; }
          VB: Public ReadOnly Property OriginalBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
      commentId: P:WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
      language: CSharp
      name:
        CSharp: OriginalForegroundColor
        VB: OriginalForegroundColor
      nameWithType:
        CSharp: MessageInColor.OriginalForegroundColor
        VB: MessageInColor.OriginalForegroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
        VB: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: OriginalForegroundColor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 292
      summary: "\nGets the Console.ForegroundColor that was in force when the instance\nwas constructed.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor OriginalForegroundColor { get; }
          VB: Public ReadOnly Property OriginalForegroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
      commentId: P:WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
      language: CSharp
      name:
        CSharp: MessageBackgroundColor
        VB: MessageBackgroundColor
      nameWithType:
        CSharp: MessageInColor.MessageBackgroundColor
        VB: MessageInColor.MessageBackgroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
        VB: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: MessageBackgroundColor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 302
      summary: "\nGets or sets the Console.BackgroundColor to use for messages\ngenerated by the instance Write and WriteLine methods.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor MessageBackgroundColor { get; set; }
          VB: Public Property MessageBackgroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
      commentId: P:WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
      language: CSharp
      name:
        CSharp: MessageForegroundColor
        VB: MessageForegroundColor
      nameWithType:
        CSharp: MessageInColor.MessageForegroundColor
        VB: MessageInColor.MessageForegroundColor
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
        VB: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
      type: Property
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: MessageForegroundColor
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 313
      summary: "\nGets or sets the Console.ForegroundColor to use for messages\ngenerated by the instance Write and WriteLine methods.\n"
      example: []
      syntax:
        content:
          CSharp: public ConsoleColor MessageForegroundColor { get; set; }
          VB: Public Property MessageForegroundColor As ConsoleColor
        parameters: []
        return:
          type: System.ConsoleColor
      overload: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.ToString
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: MessageInColor.ToString()
        VB: MessageInColor.ToString()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.ToString()
        VB: WizardWrx.ConsoleStreams.MessageInColor.ToString()
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 339
      summary: "\nOverride the default ToString method inherited from the base class\n(object) to display the most significant properties, the text, or\nforeground, and background colors set by the constructor, followed\nby the fully qualified type name.\n"
      remarks: "\nThough this method could have easily been implemented inline, using\nthe shared message template, moving the implementation out of line\naffords the flexibility to rearrange the display consistently, even\nif that requires the properties to be reordered.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nThe return value is a string of the following format.\n\n{Text = ConsoleColorText, Background = BackroundColor} WizardWrx.ConsoleStreams.MessageInColor\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.ToString*
      overridden: System.Object.ToString
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Boolean)
      language: CSharp
      name:
        CSharp: WriteLine(Boolean)
        VB: WriteLine(Boolean)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Boolean)
        VB: MessageInColor.WriteLine(Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Boolean)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 356
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(bool value)
          VB: Public Sub WriteLine(value As Boolean)
        parameters:
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char)
      language: CSharp
      name:
        CSharp: WriteLine(Char)
        VB: WriteLine(Char)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Char)
        VB: MessageInColor.WriteLine(Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 372
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char value)
          VB: Public Sub WriteLine(value As Char)
        parameters:
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[])
      language: CSharp
      name:
        CSharp: WriteLine(Char[])
        VB: WriteLine(Char())
      nameWithType:
        CSharp: MessageInColor.WriteLine(Char[])
        VB: MessageInColor.WriteLine(Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 388
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char[] buffer)
          VB: Public Sub WriteLine(buffer As Char())
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Decimal)
      language: CSharp
      name:
        CSharp: WriteLine(Decimal)
        VB: WriteLine(Decimal)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Decimal)
        VB: MessageInColor.WriteLine(Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Decimal)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 404
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(decimal value)
          VB: Public Sub WriteLine(value As Decimal)
        parameters:
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Double)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Double)
      language: CSharp
      name:
        CSharp: WriteLine(Double)
        VB: WriteLine(Double)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Double)
        VB: MessageInColor.WriteLine(Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Double)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 420
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(double value)
          VB: Public Sub WriteLine(value As Double)
        parameters:
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Single)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Single)
      language: CSharp
      name:
        CSharp: WriteLine(Single)
        VB: WriteLine(Single)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Single)
        VB: MessageInColor.WriteLine(Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Single)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 436
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(float value)
          VB: Public Sub WriteLine(value As Single)
        parameters:
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int32)
      language: CSharp
      name:
        CSharp: WriteLine(Int32)
        VB: WriteLine(Int32)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Int32)
        VB: MessageInColor.WriteLine(Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 452
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(int value)
          VB: Public Sub WriteLine(value As Integer)
        parameters:
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int64)
      language: CSharp
      name:
        CSharp: WriteLine(Int64)
        VB: WriteLine(Int64)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Int64)
        VB: MessageInColor.WriteLine(Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 468
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(long value)
          VB: Public Sub WriteLine(value As Long)
        parameters:
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(Object)
        VB: WriteLine(Object)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Object)
        VB: MessageInColor.WriteLine(Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 484
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(object value)
          VB: Public Sub WriteLine(value As Object)
        parameters:
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String)
      language: CSharp
      name:
        CSharp: WriteLine(String)
        VB: WriteLine(String)
      nameWithType:
        CSharp: MessageInColor.WriteLine(String)
        VB: MessageInColor.WriteLine(String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 500
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string value)
          VB: Public Sub WriteLine(value As String)
        parameters:
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt32)
      language: CSharp
      name:
        CSharp: WriteLine(UInt32)
        VB: WriteLine(UInt32)
      nameWithType:
        CSharp: MessageInColor.WriteLine(UInt32)
        VB: MessageInColor.WriteLine(UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 517
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(uint value)
          VB: Public Sub WriteLine(value As UInteger)
        parameters:
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt64)
      language: CSharp
      name:
        CSharp: WriteLine(UInt64)
        VB: WriteLine(UInt64)
      nameWithType:
        CSharp: MessageInColor.WriteLine(UInt64)
        VB: MessageInColor.WriteLine(UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 534
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(ulong value)
          VB: Public Sub WriteLine(value As ULong)
        parameters:
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object)
        VB: WriteLine(String, Object)
      nameWithType:
        CSharp: MessageInColor.WriteLine(String, Object)
        VB: MessageInColor.WriteLine(String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 556
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0)
          VB: Public Sub WriteLine(format As String, arg0 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substition token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object[])
      language: CSharp
      name:
        CSharp: WriteLine(String, Object[])
        VB: WriteLine(String, Object())
      nameWithType:
        CSharp: MessageInColor.WriteLine(String, Object[])
        VB: MessageInColor.WriteLine(String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 581
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, params object[] arg)
          VB: Public Sub WriteLine(format As String, ParamArray arg As Object())
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: WriteLine(Char[], Int32, Int32)
        VB: WriteLine(Char(), Int32, Int32)
      nameWithType:
        CSharp: MessageInColor.WriteLine(Char[], Int32, Int32)
        VB: MessageInColor.WriteLine(Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 606
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(char[] buffer, int index, int count)
          VB: Public Sub WriteLine(buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object)
        VB: WriteLine(String, Object, Object)
      nameWithType:
        CSharp: MessageInColor.WriteLine(String, Object, Object)
        VB: MessageInColor.WriteLine(String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 634
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstition tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object, Object)
        VB: WriteLine(String, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.WriteLine(String, Object, Object, Object)
        VB: MessageInColor.WriteLine(String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 666
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1, object arg2)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstition tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: WriteLine(String, Object, Object, Object, Object)
        VB: WriteLine(String, Object, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.WriteLine(String, Object, Object, Object, Object)
        VB: MessageInColor.WriteLine(String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.WriteLine(System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: WriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 703
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Sub WriteLine(format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstition tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Boolean)
      language: CSharp
      name:
        CSharp: Write(Boolean)
        VB: Write(Boolean)
      nameWithType:
        CSharp: MessageInColor.Write(Boolean)
        VB: MessageInColor.Write(Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Boolean)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 725
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(bool value)
          VB: Public Sub Write(value As Boolean)
        parameters:
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char)
      language: CSharp
      name:
        CSharp: Write(Char)
        VB: Write(Char)
      nameWithType:
        CSharp: MessageInColor.Write(Char)
        VB: MessageInColor.Write(Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 740
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char value)
          VB: Public Sub Write(value As Char)
        parameters:
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[])
      language: CSharp
      name:
        CSharp: Write(Char[])
        VB: Write(Char())
      nameWithType:
        CSharp: MessageInColor.Write(Char[])
        VB: MessageInColor.Write(Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 755
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char[] buffer)
          VB: Public Sub Write(buffer As Char())
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Decimal)
      language: CSharp
      name:
        CSharp: Write(Decimal)
        VB: Write(Decimal)
      nameWithType:
        CSharp: MessageInColor.Write(Decimal)
        VB: MessageInColor.Write(Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Decimal)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 770
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(decimal value)
          VB: Public Sub Write(value As Decimal)
        parameters:
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Double)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Double)
      language: CSharp
      name:
        CSharp: Write(Double)
        VB: Write(Double)
      nameWithType:
        CSharp: MessageInColor.Write(Double)
        VB: MessageInColor.Write(Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Double)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 785
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(double value)
          VB: Public Sub Write(value As Double)
        parameters:
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Single)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Single)
      language: CSharp
      name:
        CSharp: Write(Single)
        VB: Write(Single)
      nameWithType:
        CSharp: MessageInColor.Write(Single)
        VB: MessageInColor.Write(Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Single)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 800
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(float value)
          VB: Public Sub Write(value As Single)
        parameters:
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int32)
      language: CSharp
      name:
        CSharp: Write(Int32)
        VB: Write(Int32)
      nameWithType:
        CSharp: MessageInColor.Write(Int32)
        VB: MessageInColor.Write(Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 815
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(int value)
          VB: Public Sub Write(value As Integer)
        parameters:
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int64)
      language: CSharp
      name:
        CSharp: Write(Int64)
        VB: Write(Int64)
      nameWithType:
        CSharp: MessageInColor.Write(Int64)
        VB: MessageInColor.Write(Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 830
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(long value)
          VB: Public Sub Write(value As Long)
        parameters:
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Object)
      language: CSharp
      name:
        CSharp: Write(Object)
        VB: Write(Object)
      nameWithType:
        CSharp: MessageInColor.Write(Object)
        VB: MessageInColor.Write(Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 845
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(object value)
          VB: Public Sub Write(value As Object)
        parameters:
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String)
      language: CSharp
      name:
        CSharp: Write(String)
        VB: Write(String)
      nameWithType:
        CSharp: MessageInColor.Write(String)
        VB: MessageInColor.Write(String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 860
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string value)
          VB: Public Sub Write(value As String)
        parameters:
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt32)
      language: CSharp
      name:
        CSharp: Write(UInt32)
        VB: Write(UInt32)
      nameWithType:
        CSharp: MessageInColor.Write(UInt32)
        VB: MessageInColor.Write(UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 876
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(uint value)
          VB: Public Sub Write(value As UInteger)
        parameters:
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt64)
      language: CSharp
      name:
        CSharp: Write(UInt64)
        VB: Write(UInt64)
      nameWithType:
        CSharp: MessageInColor.Write(UInt64)
        VB: MessageInColor.Write(UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 892
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(ulong value)
          VB: Public Sub Write(value As ULong)
        parameters:
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object)
        VB: Write(String, Object)
      nameWithType:
        CSharp: MessageInColor.Write(String, Object)
        VB: MessageInColor.Write(String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 913
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0)
          VB: Public Sub Write(format As String, arg0 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substition token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object[])
      language: CSharp
      name:
        CSharp: Write(String, Object[])
        VB: Write(String, Object())
      nameWithType:
        CSharp: MessageInColor.Write(String, Object[])
        VB: MessageInColor.Write(String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 937
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, params object[] arg)
          VB: Public Sub Write(format As String, ParamArray arg As Object())
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: Write(Char[], Int32, Int32)
        VB: Write(Char(), Int32, Int32)
      nameWithType:
        CSharp: MessageInColor.Write(Char[], Int32, Int32)
        VB: MessageInColor.Write(Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 961
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(char[] buffer, int index, int count)
          VB: Public Sub Write(buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object)
        VB: Write(String, Object, Object)
      nameWithType:
        CSharp: MessageInColor.Write(String, Object, Object)
        VB: MessageInColor.Write(String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 988
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstition tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object, Object)
        VB: Write(String, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.Write(String, Object, Object, Object)
        VB: MessageInColor.Write(String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1019
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1, object arg2)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstition tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.Write(System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: Write(String, Object, Object, Object, Object)
        VB: Write(String, Object, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.Write(String, Object, Object, Object, Object)
        VB: MessageInColor.Write(String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.Write(System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Write
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1055
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public void Write(string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Sub Write(format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstition tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.Write*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear
      language: CSharp
      name:
        CSharp: SafeConsoleClear()
        VB: SafeConsoleClear()
      nameWithType:
        CSharp: MessageInColor.SafeConsoleClear()
        VB: MessageInColor.SafeConsoleClear()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear()
        VB: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear()
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: SafeConsoleClear
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1085
      summary: "\nUse this method as a non-throwing replacement for Console.Clear,\nwhich throws an System.IO.IOException exception if the standard\noutput stream is redirected. This catches that exception and eats it\nunless the user enables logging of all exceptions, typically in a\ntesting scenario.\n"
      remarks: "\nComparing the HResult to a local constant, E_HANDLE, means that the\nerror test works correctly in any locale.\n\nThankfully, Microsoft came to their senses, and made the HResult\nvisible in later frameworks. Meanwhile, I&apos;ve found a workaround\nthat should do the job in code that targets .NET Framework 3.5.\n"
      example: []
      syntax:
        content:
          CSharp: public static void SafeConsoleClear()
          VB: Public Shared Sub SafeConsoleClear
      overload: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Boolean)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1135
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, bool value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Boolean)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1159
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Char)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[])
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char[])
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char())
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char[])
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1183
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Decimal)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1207
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, decimal value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Decimal)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Double)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Double)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Double)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Double)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Double)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Double)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1231
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, double value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Double)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Single)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Single)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Single)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Single)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Single)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Single)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1255
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, float value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Single)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1279
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, int value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Int64)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1303
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, long value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Long)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Object)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1327
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, object value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1351
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As String)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1376
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, uint value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As UInteger)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1401
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, ulong value)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As ULong)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1431
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substition token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object[])
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object())
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object[])
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1464
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, params object[] arg)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, ParamArray arg As Object())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1497
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer, int index, int count)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1533
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstition tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1573
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstition tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: MessageInColor.RGBWriteLine(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWriteLine
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1618
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWriteLine(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Shared Sub RGBWriteLine(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstition tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Boolean)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Boolean)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Boolean)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Boolean)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Boolean)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Boolean)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1648
      summary: "\nWrite the string representation of a bool (Boolean) variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, bool value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Boolean)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Boolean
          description: "\nSpecify the Boolean value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1671
      summary: "\nWrite the string representation of a char (a Unicode character).\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Char)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Char
          description: "\nSpecify the Unicode character to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[])
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char[])
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char())
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char[])
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1694
      summary: "\nWrite the string representation of a character array.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nSpecify the array of Unicode characters to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Decimal)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Decimal)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Decimal)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Decimal)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Decimal)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Decimal)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Decimal)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1717
      summary: "\nWrite the string representation of a decimal variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, decimal value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Decimal)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Decimal
          description: "\nSpecify the decimal value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Double)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Double)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Double)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Double)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Double)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Double)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Double)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Double)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1740
      summary: "\nWrite the string representation of a double precision variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, double value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Double)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Double
          description: "\nSpecify the double precision value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Single)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Single)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Single)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Single)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Single)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Single)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Single)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Single)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1763
      summary: "\nWrite the string representation of a floating point variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, float value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Single)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Single
          description: "\nSpecify the floating point value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Int32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Int32)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Int32)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1786
      summary: "\nWrite the string representation of an integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, int value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int32
          description: "\nSpecify the integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Int64)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Int64)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Int64)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Int64)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Int64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Int64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1809
      summary: "\nWrite the string representation of a long integer variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, long value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Long)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Int64
          description: "\nSpecify the long integer value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Object)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1832
      summary: "\nWrite the string representation of a generic Object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, object value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.Object
          description: "\nSpecify the object value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1855
      summary: "\nWrite a string variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As String)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.String
          description: "\nSpecify the string value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, UInt32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, UInt32)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt32)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1879
      summary: "\nWrite the string representation of a uint (unsigned integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, uint value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As UInteger)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt32
          description: "\nSpecify the uint (unsigned integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.UInt64)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, UInt64)
        VB: RGBWrite(ConsoleColor, ConsoleColor, UInt64)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt64)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, UInt64)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt64)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.UInt64)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1903
      summary: "\nWrite the string representation of a ulong (unsigned long integer)\nvariable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, ulong value)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, value As ULong)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: value
          type: System.UInt64
          description: "\nSpecify the ulong (unsigned long integer) value to display.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1932
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may include at most\none substitution token.\n"
        - id: arg0
          type: System.Object
          description: "\nReplace the substition token with the string representation of this\ngeneric object.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object[])
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object[])
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object())
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object[])
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object())
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object[])
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object())
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1964
      summary: "\nWrite a formatted message that includes the string representation of\nan generic object variable.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, params object[] arg)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, ParamArray arg As Object())
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contains\nsubstitution tokens for each object in an array of generic Object\nvariables.\n"
        - id: arg
          type: System.Object[]
          description: "\nSubstitute elements from this array of generic Object variables into\nthe format string, replacing tokens with the element whose subscript\nis the number within its brackets.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.Char[],System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: RGBWrite(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char[], Int32, Int32)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, Char(), Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char[], System.Int32, System.Int32)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.Char(), System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 1996
      summary: "\nWrite a formatted message that includes a range of characters taken\nfrom an array of Unicode characters.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, char[] buffer, int index, int count)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, buffer As Char(), index As Integer, count As Integer)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: buffer
          type: System.Char[]
          description: "\nExtract characters from this array of Unicode characters.\n"
        - id: index
          type: System.Int32
          description: "\nSubscript of character to substitute for token {0} in format.\n"
        - id: count
          type: System.Int32
          description: "\nNumber of characters from buffer to substitute into string, which\nmust contain at least count - 1 substitution tokens.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2031
      summary: "\nWrite a formatted message that includes up to two substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to two\nsubstition tokens, {0} and {1}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2070
      summary: "\nWrite a formatted message that includes up to three substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 3\nsubstition tokens, {0}, {1}, and {2}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor,System.ConsoleColor,System.String,System.Object,System.Object,System.Object,System.Object)
      language: CSharp
      name:
        CSharp: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      nameWithType:
        CSharp: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
        VB: MessageInColor.RGBWrite(ConsoleColor, ConsoleColor, String, Object, Object, Object, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite(System.ConsoleColor, System.ConsoleColor, System.String, System.Object, System.Object, System.Object, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: RGBWrite
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2114
      summary: "\nWrite a formatted message that includes up to four substitution\ntokens.\n"
      example: []
      syntax:
        content:
          CSharp: public static void RGBWrite(ConsoleColor pclrForeColor, ConsoleColor pclrBackColor, string format, object arg0, object arg1, object arg2, object arg3)
          VB: Public Shared Sub RGBWrite(pclrForeColor As ConsoleColor, pclrBackColor As ConsoleColor, format As String, arg0 As Object, arg1 As Object, arg2 As Object, arg3 As Object)
        parameters:
        - id: pclrForeColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the foreground (text).\n"
        - id: pclrBackColor
          type: System.ConsoleColor
          description: "\nSpecify the ConsoleColor to use for the background.\n"
        - id: format
          type: System.String
          description: "\nUse this string as the message template, which may contain up to 4\nsubstition tokens, {0}, {1}, {2}, and {3}.\n"
        - id: arg0
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {0}.\n"
        - id: arg1
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {1}.\n"
        - id: arg2
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {2}.\n"
        - id: arg3
          type: System.Object
          description: "\nThe default string representation of this generic Object variable is\nsubstituted into format for token {3}.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    references:
      WizardWrx.ConsoleStreams.ErrorMessagesInColor: 
      WizardWrx.Core.PropertyDefaults: 
  - id: WizardWrx.ConsoleStreams.MessageInColorConverter
    commentId: T:WizardWrx.ConsoleStreams.MessageInColorConverter
    language: CSharp
    name:
      CSharp: MessageInColorConverter
      VB: MessageInColorConverter
    nameWithType:
      CSharp: MessageInColorConverter
      VB: MessageInColorConverter
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.MessageInColorConverter
      VB: WizardWrx.ConsoleStreams.MessageInColorConverter
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/MessageInColor.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: MessageInColorConverter
      path: ../ConsoleStreams/MessageInColor.cs
      startLine: 2252
    summary: "\nAlthough its scope is public, the only practical use for this class is\nto facilitate storage of default or user specified MessageInColor values\nin application settings files.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class MessageInColorConverter : TypeConverter'
        VB: >-
          Public Class MessageInColorConverter

              Inherits TypeConverter
    inheritance:
    - System.Object
    - System.ComponentModel.TypeConverter
    inheritedMembers:
    - System.ComponentModel.TypeConverter.CanConvertFrom(System.Type)
    - System.ComponentModel.TypeConverter.CanConvertTo(System.Type)
    - System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type)
    - System.ComponentModel.TypeConverter.ConvertFrom(System.Object)
    - System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String)
    - System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String)
    - System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
    - System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type)
    - System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object)
    - System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
    - System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    - System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)
    - System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)
    - System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)
    - System.ComponentModel.TypeConverter.GetCreateInstanceSupported
    - System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetProperties(System.Object)
    - System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])
    - System.ComponentModel.TypeConverter.GetPropertiesSupported
    - System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValues
    - System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValuesExclusive
    - System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.GetStandardValuesSupported
    - System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)
    - System.ComponentModel.TypeConverter.IsValid(System.Object)
    - System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object)
    - System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[])
    - System.Object.ToString
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      language: CSharp
      name:
        CSharp: CanConvertFrom(ITypeDescriptorContext, Type)
        VB: CanConvertFrom(ITypeDescriptorContext, Type)
      nameWithType:
        CSharp: MessageInColorConverter.CanConvertFrom(ITypeDescriptorContext, Type)
        VB: MessageInColorConverter.CanConvertFrom(ITypeDescriptorContext, Type)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Type)
        VB: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Type)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CanConvertFrom
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2268
      summary: "\nReturn True if inputs of the specified type can be converted.\n"
      example: []
      syntax:
        content:
          CSharp: public override bool CanConvertFrom(ITypeDescriptorContext pIContext, Type ptypSourceType)
          VB: Public Overrides Function CanConvertFrom(pIContext As ITypeDescriptorContext, ptypSourceType As Type) As Boolean
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: ptypSourceType
          type: System.Type
          description: "\nThis argument specifies the System.Type to be evaluated.\n"
        return:
          type: System.Boolean
          description: "\nThis method returns TRUE if ptypSourceType is typeof ( string ). Any\nother type returns FALSE.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom*
      overridden: System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      language: CSharp
      name:
        CSharp: ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
        VB: ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
      nameWithType:
        CSharp: MessageInColorConverter.ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
        VB: MessageInColorConverter.ConvertFrom(ITypeDescriptorContext, CultureInfo, Object)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object)
        VB: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ConvertFrom
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2298
      summary: "\nConvert from string (the only supported source type) to\nMessageInColor.\n"
      example: []
      syntax:
        content:
          CSharp: public override object ConvertFrom(ITypeDescriptorContext pIContext, CultureInfo pCulture, object pobjValue)
          VB: Public Overrides Function ConvertFrom(pIContext As ITypeDescriptorContext, pCulture As CultureInfo, pobjValue As Object) As Object
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: pCulture
          type: System.Globalization.CultureInfo
          description: "\nThis argument supplies a reference to the current CultureInfo object\nthat drives many aspects of text and numeric conversions.\n"
        - id: pobjValue
          type: System.Object
          description: "\nSpecify the source object to be converted. Although the method\nsignature requires this argument to be cast to Object, the only type\nsupported is System.string.\n"
        return:
          type: System.Object
          description: "\nAlthough specified as object to meet the requirements of the base\nclass, the actual return value is expected to be a MessageInColors\nobject.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom*
      overridden: System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      commentId: M:WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      language: CSharp
      name:
        CSharp: ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
        VB: ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
      nameWithType:
        CSharp: MessageInColorConverter.ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
        VB: MessageInColorConverter.ConvertTo(ITypeDescriptorContext, CultureInfo, Object, Type)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object, System.Type)
        VB: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext, System.Globalization.CultureInfo, System.Object, System.Type)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/MessageInColor.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ConvertTo
        path: ../ConsoleStreams/MessageInColor.cs
        startLine: 2344
      summary: "\nGiven a MessageInColors object, return a string representation that\nis suitable for storage in a standard application settings file.\n"
      example: []
      syntax:
        content:
          CSharp: public override object ConvertTo(ITypeDescriptorContext pIContext, CultureInfo pCulture, object pobjValue, Type pDestType)
          VB: Public Overrides Function ConvertTo(pIContext As ITypeDescriptorContext, pCulture As CultureInfo, pobjValue As Object, pDestType As Type) As Object
        parameters:
        - id: pIContext
          type: System.ComponentModel.ITypeDescriptorContext
          description: "\nThis argument provides internal details about the type. Treat it as\na black box.\n"
        - id: pCulture
          type: System.Globalization.CultureInfo
          description: "\nThis argument supplies a reference to the current CultureInfo object\nthat drives many aspects of text and numeric conversions.\n"
        - id: pobjValue
          type: System.Object
          description: "\nAlthough the method signature calls for an generic System.Object,\nthis argument must actually be a MessageInColors object.\n"
        - id: pDestType
          type: System.Type
          description: "\nThe only valid value for this argument is typeof ( string ). The\nspecification type is dictated by the signature of the ConvertTo\nmethod in the base class.\n"
        return:
          type: System.Object
          description: "\nAlthough specified as object to meet the requirements of the base\nclass, the actual return value is expected to be a System.string.\n"
      overload: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo*
      overridden: System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
  - id: WizardWrx.ConsoleStreams.StandardHandleInfo
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo
    language: CSharp
    name:
      CSharp: StandardHandleInfo
      VB: StandardHandleInfo
    nameWithType:
      CSharp: StandardHandleInfo
      VB: StandardHandleInfo
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo
      VB: WizardWrx.ConsoleStreams.StandardHandleInfo
    type: Class
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/StandardHandleInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: StandardHandleInfo
      path: ../ConsoleStreams/StandardHandleInfo.cs
      startLine: 100
    summary: "\nThis static class provides type-safe managed methods to determine the\ntrue redirection state of a standard console handle, including, if\napplicable, learning the name of the file to which it is redirected.\n"
    remarks: "\nThe standard properties on the Process (all versions) and Console\n(version 4.5 and newer) objects report based solely on redirections\nbrought about by the Base Class Library. They completely ignore redirection\nthat happens in the shell, before the process started. Hence, to learn the\ntrue redirection state requires a couple of Platform Invoke calls into the\nWindows API, which this class provides through type-safe wrappers.\n"
    example: []
    syntax:
      content:
        CSharp: public static class StandardHandleInfo
        VB: Public Module StandardHandleInfo
    inheritance:
    - System.Object
    inheritedMembers:
    - System.Object.ToString
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    modifiers:
      CSharp:
      - public
      - static
      - class
      VB:
      - Public
      - Module
    items:
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_HANDLE_VALUE
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_HANDLE_VALUE
      language: CSharp
      name:
        CSharp: INVALID_HANDLE_VALUE
        VB: INVALID_HANDLE_VALUE
      nameWithType:
        CSharp: StandardHandleInfo.INVALID_HANDLE_VALUE
        VB: StandardHandleInfo.INVALID_HANDLE_VALUE
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_HANDLE_VALUE
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_HANDLE_VALUE
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: INVALID_HANDLE_VALUE
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 391
      summary: "\nWhen a routine, such as GetStandardHandle cannot return a usable\nfile handle, this is the return value, cast to IntPtr, as is the\nexpected file handle.\n"
      remarks: "\nWere it not that IntPtr is a structure, INVALID_HANDLE_VALUE would\nbe defined as a public constant.\n"
      example: []
      syntax:
        content:
          CSharp: public static readonly IntPtr INVALID_HANDLE_VALUE
          VB: Public Shared ReadOnly INVALID_HANDLE_VALUE As IntPtr
        return:
          type: System.IntPtr
      modifiers:
        CSharp:
        - public
        - static
        - readonly
        VB:
        - Public
        - Shared
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_MODULE_HANDLE
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_MODULE_HANDLE
      language: CSharp
      name:
        CSharp: INVALID_MODULE_HANDLE
        VB: INVALID_MODULE_HANDLE
      nameWithType:
        CSharp: StandardHandleInfo.INVALID_MODULE_HANDLE
        VB: StandardHandleInfo.INVALID_MODULE_HANDLE
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_MODULE_HANDLE
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.INVALID_MODULE_HANDLE
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: INVALID_MODULE_HANDLE
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 402
      summary: "\nWhen a routine, such as GetModuleHandle cannot return a usable\nmodule handle, this is the return value, cast to IntPtr, as is the\nexpected module handle.\n"
      remarks: "\nWere it not that IntPtr is a structure, INVALID_HANDLE_VALUE would\nbe defined as a public constant.\n"
      example: []
      syntax:
        content:
          CSharp: public static readonly IntPtr INVALID_MODULE_HANDLE
          VB: Public Shared ReadOnly INVALID_MODULE_HANDLE As IntPtr
        return:
          type: System.IntPtr
      modifiers:
        CSharp:
        - public
        - static
        - readonly
        VB:
        - Public
        - Shared
        - ReadOnly
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      commentId: M:WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      language: CSharp
      name:
        CSharp: GetStandardHandleState(StandardHandleInfo.StandardConsoleHandle)
        VB: GetStandardHandleState(StandardHandleInfo.StandardConsoleHandle)
      nameWithType:
        CSharp: StandardHandleInfo.GetStandardHandleState(StandardHandleInfo.StandardConsoleHandle)
        VB: StandardHandleInfo.GetStandardHandleState(StandardHandleInfo.StandardConsoleHandle)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetStandardHandleState
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 535
      summary: "\nGet the redirection state of a specified standard console stream.\n"
      remarks: "\nIn all versions of the framework, the following properties report\ninaccurately on the true state of the console stream.\n\n- System.Diagnostics.Process.StartInfo.RedirectStandardInput ,\n- System.Diagnostics.Process.StartInfo.RedirectStandardOutput ,\n- System.Diagnostics.Process.StartInfo.RedirectStandardError ,\n\nThe following properties, added in version 4.5, also report incorrectly.\n\n- Console.IsErrorRedirected\n- Console.IsInputRedirected\n- Console.IsOutputRedirected\n\nThe above properties report inaccurately because their report is\nbased entirely on changes made by calls to methods on the Console\nclass. They exhibit no knowledge, whatsoever, of what the underlying\noperating system does in response to shell commands to redirect one\nor more of the standard console streams.\n\nOn the other hand, since StandardHandleState gets its information \nfrom GetConsoleMode, a native Windows API routine, its report takes \ninto account console stream redirections that are the result of\nshell commands.\n"
      example: []
      syntax:
        content:
          CSharp: public static StandardHandleInfo.StandardHandleState GetStandardHandleState(StandardHandleInfo.StandardConsoleHandle penmStdHandleID)
          VB: Public Shared Function GetStandardHandleState(penmStdHandleID As StandardHandleInfo.StandardConsoleHandle) As StandardHandleInfo.StandardHandleState
        parameters:
        - id: penmStdHandleID
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
          description: "\nUse a member of the StandardConsoleHandle enumeration to designate\nthe desired standard console stream.\n"
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
          description: "\nThe return value is one of the following.\n\nStandardHandleState.Attached means that the specified stream is\nattached to its console.\n\nStandardHandleState.Redirected means that the specified stream is\nredirected.\n\nCall GetRedirectedFileName to get the fully qualified name of the\nfile to which it is redirected.\n"
      overload: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      commentId: M:WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      language: CSharp
      name:
        CSharp: GetRedirectedFileName(StandardHandleInfo.StandardConsoleHandle)
        VB: GetRedirectedFileName(StandardHandleInfo.StandardConsoleHandle)
      nameWithType:
        CSharp: StandardHandleInfo.GetRedirectedFileName(StandardHandleInfo.StandardConsoleHandle)
        VB: StandardHandleInfo.GetRedirectedFileName(StandardHandleInfo.StandardConsoleHandle)
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName(WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetRedirectedFileName
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 588
      summary: "\nGet the name of the file to which a specified standard handle is\nredirected.\n"
      example: []
      syntax:
        content:
          CSharp: public static string GetRedirectedFileName(StandardHandleInfo.StandardConsoleHandle penmStdHandleID)
          VB: Public Shared Function GetRedirectedFileName(penmStdHandleID As StandardHandleInfo.StandardConsoleHandle) As String
        parameters:
        - id: penmStdHandleID
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
          description: "\nUse a member of the StandardConsoleHandle enumeration to designate\nthe desired standard console stream.\n"
        return:
          type: System.String
          description: "\nIf the file is redirected, the returned string contains the fully\nqualified name of the file to which it is redirected. If the handle\nis attached to its console (that is, not redirected), the return\nvalue is the empty string.\n"
      overload: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode
      commentId: M:WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode
      language: CSharp
      name:
        CSharp: GetWin32StatusCode()
        VB: GetWin32StatusCode()
      nameWithType:
        CSharp: StandardHandleInfo.GetWin32StatusCode()
        VB: StandardHandleInfo.GetWin32StatusCode()
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode()
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode()
      type: Method
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetWin32StatusCode
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 653
      summary: "\nGet the last Win32 status code logged by Marshal.GetLastWin32Error.\n"
      remarks: "\nAs is true of the other methods defined by this class, this method\nis not yet thread-safe.\n"
      example: []
      syntax:
        content:
          CSharp: public static int GetWin32StatusCode()
          VB: Public Shared Function GetWin32StatusCode As Integer
        return:
          type: System.Int32
          description: "\nThe return value is the value returned by the last call to\nMarshal.GetLastWin32Error.\n"
      overload: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
  - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
    language: CSharp
    name:
      CSharp: StandardHandleInfo.ConsoleModes
      VB: StandardHandleInfo.ConsoleModes
    nameWithType:
      CSharp: StandardHandleInfo.ConsoleModes
      VB: StandardHandleInfo.ConsoleModes
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
    type: Enum
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/StandardHandleInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ConsoleModes
      path: ../ConsoleStreams/StandardHandleInfo.cs
      startLine: 128
    summary: "\nUse these flags to test the values returned by GetConsoleMode.\n"
    remarks: "\nIn wincon.h, these are defined as symbolic constants, although there\nis ample justification for rolling them into a bit-mapped\nenumeration. I suspect the reason they aren&apos;t is that the change\nwould break too much legacy code.\n\nFor the present, GetConsoleMode is called, but only for a side\neffect; its return value is discarded.\n\nThough currently unused, the enumeration is reserved for future use.\n\nThe summaries shown here for all but the last member of this\nenumeration are quoted verbatim from the MSDN manual page of the\nSetConsoleMode function cited below.\n"
    example: []
    syntax:
      content:
        CSharp: >-
          [Flags]

          public enum ConsoleModes
        VB: >-
          <Flags>

          Public Enum ConsoleModes
    see:
    - linkType: HRef
      linkId: https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx
      altText: https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx
    extensionMethods:
    - WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    attributes:
    - type: System.FlagsAttribute
      ctor: System.FlagsAttribute.#ctor
      arguments: []
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
      language: CSharp
      name:
        CSharp: ENABLE_PROCESSED_INPUT
        VB: ENABLE_PROCESSED_INPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
        VB: StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_PROCESSED_INPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_PROCESSED_INPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 138
      summary: "\nCharacters written by the WriteFile or WriteConsole function or\nechoed by the ReadFile or ReadConsole function are examined for\nASCII control sequences and the correct action is performed.\nBackspace, tab, bell, carriage return, and line feed characters\nare processed.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_PROCESSED_INPUT = 1
          VB: ENABLE_PROCESSED_INPUT = 1
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
      language: CSharp
      name:
        CSharp: ENABLE_LINE_INPUT
        VB: ENABLE_LINE_INPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
        VB: StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_LINE_INPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_LINE_INPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 145
      summary: "\nThe ReadFile or ReadConsole function returns only when a\ncarriage return character is read. If this mode is disabled, the\nfunctions return when one or more characters are available.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_LINE_INPUT = 2
          VB: ENABLE_LINE_INPUT = 2
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
      language: CSharp
      name:
        CSharp: ENABLE_ECHO_INPUT
        VB: ENABLE_ECHO_INPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
        VB: StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_ECHO_INPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_ECHO_INPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 152
      summary: "\nCharacters read by the ReadFile or ReadConsole function are\nwritten to the active screen buffer as they are read. This mode\ncan be used only if the ENABLE_LINE_INPUT mode is also enabled.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_ECHO_INPUT = 4
          VB: ENABLE_ECHO_INPUT = 4
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
      language: CSharp
      name:
        CSharp: ENABLE_WINDOW_INPUT
        VB: ENABLE_WINDOW_INPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
        VB: StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_WINDOW_INPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_WINDOW_INPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 161
      summary: "\nUser interactions that change the size of the console screen\nbuffer are reported in the console&apos;s input buffer. Information\nabout these events can be read from the input buffer by\napplications using the ReadConsoleInput function, but not by\nthose using ReadFile or ReadConsole.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_WINDOW_INPUT = 8
          VB: ENABLE_WINDOW_INPUT = 8
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
      language: CSharp
      name:
        CSharp: ENABLE_MOUSE_INPUT
        VB: ENABLE_MOUSE_INPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
        VB: StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_MOUSE_INPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_MOUSE_INPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 170
      summary: "\nIf the mouse pointer is within the borders of the console window\nand the window has the keyboard focus, mouse events generated by\nmouse movement and button presses are placed in the input buffer.\nThese events are discarded by ReadFile or ReadConsole, even when\nthis mode is enabled.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_MOUSE_INPUT = 16
          VB: ENABLE_MOUSE_INPUT = 16
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
      language: CSharp
      name:
        CSharp: ENABLE_INSERT_MODE
        VB: ENABLE_INSERT_MODE
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
        VB: StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_INSERT_MODE
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_INSERT_MODE
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 182
      summary: "\nWhen enabled, text entered in a console window will be inserted\nat the current cursor location and all text following that\nlocation will not be overwritten. When disabled, all following\ntext will be overwritten.\n\nTo enable this mode, use ENABLE_INSERT_MODE | ENABLE_EXTENDED_FLAGS.\n\nTo disable this mode, use ENABLE_EXTENDED_FLAGS without this flag.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_INSERT_MODE = 32
          VB: ENABLE_INSERT_MODE = 32
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
      language: CSharp
      name:
        CSharp: ENABLE_QUICK_EDIT_MODE
        VB: ENABLE_QUICK_EDIT_MODE
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
        VB: StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_QUICK_EDIT_MODE
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_QUICK_EDIT_MODE
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 192
      summary: "\nThis flag enables the user to use the mouse to select and edit\ntext.\n\nTo enable this mode, use ENABLE_QUICK_EDIT_MODE | ENABLE_EXTENDED_FLAGS.\n\nTo disable this mode, use ENABLE_EXTENDED_FLAGS without this flag.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_QUICK_EDIT_MODE = 64
          VB: ENABLE_QUICK_EDIT_MODE = 64
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
      language: CSharp
      name:
        CSharp: ENABLE_EXTENDED_FLAGS
        VB: ENABLE_EXTENDED_FLAGS
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
        VB: StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_EXTENDED_FLAGS
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_EXTENDED_FLAGS
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 199
      summary: "\nRequired to enable or disable extended flags.\n\nSee ENABLE_INSERT_MODE and ENABLE_QUICK_EDIT_MODE.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_EXTENDED_FLAGS = 128
          VB: ENABLE_EXTENDED_FLAGS = 128
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
      language: CSharp
      name:
        CSharp: ENABLE_AUTO_POSITION
        VB: ENABLE_AUTO_POSITION
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
        VB: StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.ENABLE_AUTO_POSITION
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_AUTO_POSITION
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 208
      summary: "\nThough defined in WinCon.h, this flag is otherwise undocumented.\n\nMy initial best guess is that it is related to the &quot;Let system\nposition window&quot; check box on the property sheet of a CMD.EXE\nwindow, though I have yet to test this theory.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_AUTO_POSITION = 256
          VB: ENABLE_AUTO_POSITION = 256
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
    language: CSharp
    name:
      CSharp: StandardHandleInfo.ConsoleOutputModes
      VB: StandardHandleInfo.ConsoleOutputModes
    nameWithType:
      CSharp: StandardHandleInfo.ConsoleOutputModes
      VB: StandardHandleInfo.ConsoleOutputModes
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
      VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
    type: Enum
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/StandardHandleInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ConsoleOutputModes
      path: ../ConsoleStreams/StandardHandleInfo.cs
      startLine: 236
    summary: "\nUse these flags with SetConsoleMode to control how long output lines\nare handled.\n"
    remarks: "\nIn wincon.h, these are defined as symbolic constants, although there\nis ample justification for rolling them into a bit-mapped\nenumeration. I suspect the reason they aren&apos;t is that the change\nwould break too much legacy code.\n\nFor the present, SetConsoleMode is not called.\n\nThough currently unused, the enumeration is reserved for future use.\n\nThe summaries shown here for all but the last member of this\nenumeration are quoted verbatim from the MSDN manual page of the\nSetConsoleMode function cited below.\n"
    example: []
    syntax:
      content:
        CSharp: >-
          [Flags]

          public enum ConsoleOutputModes
        VB: >-
          <Flags>

          Public Enum ConsoleOutputModes
    see:
    - linkType: HRef
      linkId: https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx
      altText: https://msdn.microsoft.com/en-us/library/windows/desktop/ms686033(v=vs.85).aspx
    extensionMethods:
    - WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    attributes:
    - type: System.FlagsAttribute
      ctor: System.FlagsAttribute.#ctor
      arguments: []
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
      language: CSharp
      name:
        CSharp: ENABLE_PROCESSED_OUTPUT
        VB: ENABLE_PROCESSED_OUTPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
        VB: StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_PROCESSED_OUTPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_PROCESSED_OUTPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 246
      summary: "\nCharacters written by the WriteFile or WriteConsole function or\nechoed by the ReadFile or ReadConsole function are examined for\nASCII control sequences and the correct action is performed.\nBackspace, tab, bell, carriage return, and line feed characters\nare processed.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_PROCESSED_OUTPUT = 1
          VB: ENABLE_PROCESSED_OUTPUT = 1
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
      language: CSharp
      name:
        CSharp: ENABLE_WRAP_AT_EOL_OUTPUT
        VB: ENABLE_WRAP_AT_EOL_OUTPUT
      nameWithType:
        CSharp: StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
        VB: StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.ENABLE_WRAP_AT_EOL_OUTPUT
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ENABLE_WRAP_AT_EOL_OUTPUT
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 261
      summary: "\nWhen writing with WriteFile or WriteConsole or echoing with\nReadFile or ReadConsole, the cursor moves to the beginning of\nthe next row when it reaches the end of the current row. This\ncauses the rows displayed in the console window to scroll up\nautomatically when the cursor advances beyond the last row in\nthe window. It also causes the contents of the console screen\nbuffer to scroll up (discarding the top row of the console\nscreen buffer) when the cursor advances beyond the last row in\nthe console screen buffer. If this mode is disabled, the last\ncharacter in the row is overwritten with any subsequent\ncharacters.\n"
      example: []
      syntax:
        content:
          CSharp: ENABLE_WRAP_AT_EOL_OUTPUT = 2
          VB: ENABLE_WRAP_AT_EOL_OUTPUT = 2
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
    language: CSharp
    name:
      CSharp: StandardHandleInfo.StandardConsoleHandle
      VB: StandardHandleInfo.StandardConsoleHandle
    nameWithType:
      CSharp: StandardHandleInfo.StandardConsoleHandle
      VB: StandardHandleInfo.StandardConsoleHandle
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
    type: Enum
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/StandardHandleInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: StandardConsoleHandle
      path: ../ConsoleStreams/StandardHandleInfo.cs
      startLine: 296
    summary: "\nThis enumeration defines the three valid values for an enumeration,\nStandardConsoleHandle, the input argument type of methods\nStandardHandleState and GetRedirectedFileName, both public methods,\nand GetStandardHandle, currently private, but under consideration\nfor a promotion.\n\nThe upper and lower values are for efficient boundary testing.\n"
    remarks: "\nAlthough the standard handles have well-known fixed values, each\nOF WHICH HAS a corresponding symbolic constant defined in winbase.h,\nthey are nonstandard handle values (all three less than zero), which\nwould usually be interpreted as invalid handles. These values would\nbe unusual, though perfectly legal, enumeration values.\n\nInstead, I defined this enumeration, and provided a private method,\nGetStandardHandle, that maps the enumeration to the corresponding \nstandard handle constant by way of a simple lookup table that uses\nthe enumeration value, cast to integer, as its index. The returned\nWindows handle value is a 32 or 64 bit positive integer, which that\nfunction passes into GetStdHandle (via Platform Invoke), through\nwhich it returns.\n\nThis is one of two public enumerations that were ported from\nStandardHandleState.H, the C/C++ header that defines the interfaces\nthat this class supersedes.\n"
    example: []
    syntax:
      content:
        CSharp: public enum StandardConsoleHandle
        VB: Public Enum StandardConsoleHandle
    extensionMethods:
    - WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.Undefined
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.Undefined
      language: CSharp
      name:
        CSharp: Undefined
        VB: Undefined
      nameWithType:
        CSharp: StandardHandleInfo.StandardConsoleHandle.Undefined
        VB: StandardHandleInfo.StandardConsoleHandle.Undefined
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.Undefined
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.Undefined
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Undefined
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 302
      summary: "\nThis member is both the uninitialized value of a variable of its\ntype and the lower limit of the range of valid values.\n"
      example: []
      syntax:
        content:
          CSharp: Undefined = 0
          VB: Undefined = 0
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardInput
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardInput
      language: CSharp
      name:
        CSharp: StandardInput
        VB: StandardInput
      nameWithType:
        CSharp: StandardHandleInfo.StandardConsoleHandle.StandardInput
        VB: StandardHandleInfo.StandardConsoleHandle.StandardInput
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardInput
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardInput
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StandardInput
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 309
      summary: "\nThis member signifies a request to perform an operation on the\nStandard Input stream, STDOUT, which corresponds to the Console.In\nand System.Diagnostics.Process.StandardInput properties.\n"
      example: []
      syntax:
        content:
          CSharp: StandardInput = 1
          VB: StandardInput = 1
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardOutput
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardOutput
      language: CSharp
      name:
        CSharp: StandardOutput
        VB: StandardOutput
      nameWithType:
        CSharp: StandardHandleInfo.StandardConsoleHandle.StandardOutput
        VB: StandardHandleInfo.StandardConsoleHandle.StandardOutput
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardOutput
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardOutput
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StandardOutput
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 316
      summary: "\nThis member signifies a request to perform an operation on the\nStandard Output stream, STDIN, which corresponds to the Console.Out\nand System.Diagnostics.Process.StandardOutput properties.\n"
      example: []
      syntax:
        content:
          CSharp: StandardOutput = 2
          VB: StandardOutput = 2
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardError
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardError
      language: CSharp
      name:
        CSharp: StandardError
        VB: StandardError
      nameWithType:
        CSharp: StandardHandleInfo.StandardConsoleHandle.StandardError
        VB: StandardHandleInfo.StandardConsoleHandle.StandardError
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardError
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.StandardError
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StandardError
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 323
      summary: "\nThis member signifies a request to perform an operation on the\nStandard Output stream, STDERR, which corresponds to the Console.Error\nand System.Diagnostics.Process.StandardError properties.\n"
      example: []
      syntax:
        content:
          CSharp: StandardError = 3
          VB: StandardError = 3
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.OutOfRange
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.OutOfRange
      language: CSharp
      name:
        CSharp: OutOfRange
        VB: OutOfRange
      nameWithType:
        CSharp: StandardHandleInfo.StandardConsoleHandle.OutOfRange
        VB: StandardHandleInfo.StandardConsoleHandle.OutOfRange
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.OutOfRange
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.OutOfRange
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: OutOfRange
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 329
      summary: "\nThis member is intended for bounds-checking values that are cast\nto an integral type.\n"
      example: []
      syntax:
        content:
          CSharp: OutOfRange = 4
          VB: OutOfRange = 4
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
    language: CSharp
    name:
      CSharp: StandardHandleInfo.StandardHandleState
      VB: StandardHandleInfo.StandardHandleState
    nameWithType:
      CSharp: StandardHandleInfo.StandardHandleState
      VB: StandardHandleInfo.StandardHandleState
    qualifiedName:
      CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
    type: Enum
    assemblies:
    - WizardWrx.ConsoleStreams
    namespace: WizardWrx.ConsoleStreams
    source:
      remote:
        path: ConsoleStreams/StandardHandleInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: StandardHandleState
      path: ../ConsoleStreams/StandardHandleInfo.cs
      startLine: 347
    summary: "\nThis enumeration defines the two valid states and two defined error\nstates that may be returned by the StandardHandleState method, along\nwith a pair of values that may be returned to indicate one of two\nerror conditions.\n"
    remarks: "\nThis is one of two public enumerations that were ported from\nStandardHandleState.H, the C/C++ header that defines the interfaces\nthat this class supersedes.\n"
    example: []
    syntax:
      content:
        CSharp: public enum StandardHandleState
        VB: Public Enum StandardHandleState
    extensionMethods:
    - WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Undetermined
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Undetermined
      language: CSharp
      name:
        CSharp: Undetermined
        VB: Undetermined
      nameWithType:
        CSharp: StandardHandleInfo.StandardHandleState.Undetermined
        VB: StandardHandleInfo.StandardHandleState.Undetermined
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Undetermined
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Undetermined
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Undetermined
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 353
      summary: "\nThis member is returned when the supplied StandardConsoleHandle\nmember is out of range.\n"
      example: []
      syntax:
        content:
          CSharp: Undetermined = 0
          VB: Undetermined = 0
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Attached
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Attached
      language: CSharp
      name:
        CSharp: Attached
        VB: Attached
      nameWithType:
        CSharp: StandardHandleInfo.StandardHandleState.Attached
        VB: StandardHandleInfo.StandardHandleState.Attached
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Attached
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Attached
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Attached
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 360
      summary: "\nThis member is returned when console stream that corresponds to\nthe specified StandardConsoleHandle member is attached to its\nconsole.\n"
      example: []
      syntax:
        content:
          CSharp: Attached = 1
          VB: Attached = 1
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Redirected
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Redirected
      language: CSharp
      name:
        CSharp: Redirected
        VB: Redirected
      nameWithType:
        CSharp: StandardHandleInfo.StandardHandleState.Redirected
        VB: StandardHandleInfo.StandardHandleState.Redirected
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Redirected
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.Redirected
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Redirected
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 367
      summary: "\nThis member is returned when console stream that corresponds to\nthe specified StandardConsoleHandle member is redirected to a\nfile.\n"
      example: []
      syntax:
        content:
          CSharp: Redirected = 2
          VB: Redirected = 2
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
      commentId: F:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
      language: CSharp
      name:
        CSharp: STATE_SYSTEM_ERROR
        VB: STATE_SYSTEM_ERROR
      nameWithType:
        CSharp: StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
        VB: StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
      qualifiedName:
        CSharp: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
        VB: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.STATE_SYSTEM_ERROR
      type: Field
      assemblies:
      - WizardWrx.ConsoleStreams
      namespace: WizardWrx.ConsoleStreams
      source:
        remote:
          path: ConsoleStreams/StandardHandleInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: STATE_SYSTEM_ERROR
        path: ../ConsoleStreams/StandardHandleInfo.cs
        startLine: 373
      summary: "\nThis member is returned when attempting to get the state of the\nspecified StandardConsoleHandle raised an exception.\n"
      example: []
      syntax:
        content:
          CSharp: STATE_SYSTEM_ERROR = 3
          VB: STATE_SYSTEM_ERROR = 3
        return:
          type: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
references:
  WizardWrx.Core:
    name:
      CSharp:
      - name: WizardWrx.Core
        nameWithType: WizardWrx.Core
        qualifiedName: WizardWrx.Core
      VB:
      - name: WizardWrx.Core
        nameWithType: WizardWrx.Core
        qualifiedName: WizardWrx.Core
    isDefinition: true
    commentId: N:WizardWrx.Core
  WizardWrx.Core.PropertyDefaults:
    name:
      CSharp:
      - id: WizardWrx.Core.PropertyDefaults
        name: PropertyDefaults
        nameWithType: PropertyDefaults
        qualifiedName: WizardWrx.Core.PropertyDefaults
      VB:
      - id: WizardWrx.Core.PropertyDefaults
        name: PropertyDefaults
        nameWithType: PropertyDefaults
        qualifiedName: WizardWrx.Core.PropertyDefaults
    isDefinition: true
    parent: WizardWrx.Core
    commentId: T:WizardWrx.Core.PropertyDefaults
  WizardWrx.Core.PropertyDefaults.ValuesCollection:
    name:
      CSharp:
      - id: WizardWrx.Core.PropertyDefaults.ValuesCollection
        name: ValuesCollection
        nameWithType: PropertyDefaults.ValuesCollection
        qualifiedName: WizardWrx.Core.PropertyDefaults.ValuesCollection
      VB:
      - id: WizardWrx.Core.PropertyDefaults.ValuesCollection
        name: ValuesCollection
        nameWithType: PropertyDefaults.ValuesCollection
        qualifiedName: WizardWrx.Core.PropertyDefaults.ValuesCollection
    isDefinition: true
    parent: WizardWrx.Core.PropertyDefaults
    commentId: P:WizardWrx.Core.PropertyDefaults.ValuesCollection
  WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate(System.DateTimeKind):
    name:
      CSharp:
      - id: WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate(System.DateTimeKind)
        name: GetAssemblyBuildDate
        nameWithType: PropertyDefaults.GetAssemblyBuildDate
        qualifiedName: WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.DateTimeKind
        name: DateTimeKind
        nameWithType: DateTimeKind
        qualifiedName: System.DateTimeKind
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate(System.DateTimeKind)
        name: GetAssemblyBuildDate
        nameWithType: PropertyDefaults.GetAssemblyBuildDate
        qualifiedName: WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.DateTimeKind
        name: DateTimeKind
        nameWithType: DateTimeKind
        qualifiedName: System.DateTimeKind
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.PropertyDefaults
    commentId: M:WizardWrx.Core.PropertyDefaults.GetAssemblyBuildDate(System.DateTimeKind)
  WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString:
    name:
      CSharp:
      - id: WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
        name: GetAssemblyVersionString
        nameWithType: PropertyDefaults.GetAssemblyVersionString
        qualifiedName: WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
        name: GetAssemblyVersionString
        nameWithType: PropertyDefaults.GetAssemblyVersionString
        qualifiedName: WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.PropertyDefaults
    commentId: M:WizardWrx.Core.PropertyDefaults.GetAssemblyVersionString
  WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues:
    name:
      CSharp:
      - id: WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
        name: EnumerateMissingConfigurationValues
        nameWithType: PropertyDefaults.EnumerateMissingConfigurationValues
        qualifiedName: WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
        name: EnumerateMissingConfigurationValues
        nameWithType: PropertyDefaults.EnumerateMissingConfigurationValues
        qualifiedName: WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.PropertyDefaults
    commentId: M:WizardWrx.Core.PropertyDefaults.EnumerateMissingConfigurationValues
  WizardWrx.Core.AssemblyLocatorBase:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase
        name: AssemblyLocatorBase
        nameWithType: AssemblyLocatorBase
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase
        name: AssemblyLocatorBase
        nameWithType: AssemblyLocatorBase
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase
    isDefinition: true
    parent: WizardWrx.Core
    commentId: T:WizardWrx.Core.AssemblyLocatorBase
  WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
        name: ASSEMBLYDATAPATH_TOKEN
        nameWithType: AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
        name: ASSEMBLYDATAPATH_TOKEN
        nameWithType: AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: F:WizardWrx.Core.AssemblyLocatorBase.ASSEMBLYDATAPATH_TOKEN
  WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
        name: _strAssemblyLocation
        nameWithType: AssemblyLocatorBase._strAssemblyLocation
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
        name: _strAssemblyLocation
        nameWithType: AssemblyLocatorBase._strAssemblyLocation
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: F:WizardWrx.Core.AssemblyLocatorBase._strAssemblyLocation
  WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
        name: AssemblyDataPath
        nameWithType: AssemblyLocatorBase.AssemblyDataPath
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
        name: AssemblyDataPath
        nameWithType: AssemblyLocatorBase.AssemblyDataPath
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.AssemblyDataPath
  WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
        name: AssemblyLocation
        nameWithType: AssemblyLocatorBase.AssemblyLocation
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
        name: AssemblyLocation
        nameWithType: AssemblyLocatorBase.AssemblyLocation
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.AssemblyLocation
  WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
        name: RecoveredConfigurationExceptions
        nameWithType: AssemblyLocatorBase.RecoveredConfigurationExceptions
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
        name: RecoveredConfigurationExceptions
        nameWithType: AssemblyLocatorBase.RecoveredConfigurationExceptions
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.RecoveredConfigurationExceptions
  WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
        name: MissingConfigSettings
        nameWithType: AssemblyLocatorBase.MissingConfigSettings
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
        name: MissingConfigSettings
        nameWithType: AssemblyLocatorBase.MissingConfigSettings
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.MissingConfigSettings
  WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
        name: DLLConfiguration
        nameWithType: AssemblyLocatorBase.DLLConfiguration
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
        name: DLLConfiguration
        nameWithType: AssemblyLocatorBase.DLLConfiguration
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.DLLConfiguration
  WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
        name: DLLSettingsSection
        nameWithType: AssemblyLocatorBase.DLLSettingsSection
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
        name: DLLSettingsSection
        nameWithType: AssemblyLocatorBase.DLLSettingsSection
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.DLLSettingsSection
  WizardWrx.Core.AssemblyLocatorBase.DLLSettings:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLSettings
        name: DLLSettings
        nameWithType: AssemblyLocatorBase.DLLSettings
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLSettings
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.DLLSettings
        name: DLLSettings
        nameWithType: AssemblyLocatorBase.DLLSettings
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.DLLSettings
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: P:WizardWrx.Core.AssemblyLocatorBase.DLLSettings
  WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion:
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
        name: GetAssemblyVersion
        nameWithType: AssemblyLocatorBase.GetAssemblyVersion
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
        name: GetAssemblyVersion
        nameWithType: AssemblyLocatorBase.GetAssemblyVersion
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: M:WizardWrx.Core.AssemblyLocatorBase.GetAssemblyVersion
  WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting(System.String):
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting(System.String)
        name: GetDLLSetting
        nameWithType: AssemblyLocatorBase.GetDLLSetting
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting(System.String)
        name: GetDLLSetting
        nameWithType: AssemblyLocatorBase.GetDLLSetting
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: M:WizardWrx.Core.AssemblyLocatorBase.GetDLLSetting(System.String)
  WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration(System.Type):
    name:
      CSharp:
      - id: WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration(System.Type)
        name: SetPropertiesFromDLLConfiguration
        nameWithType: AssemblyLocatorBase.SetPropertiesFromDLLConfiguration
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration(System.Type)
        name: SetPropertiesFromDLLConfiguration
        nameWithType: AssemblyLocatorBase.SetPropertiesFromDLLConfiguration
        qualifiedName: WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: WizardWrx.Core.AssemblyLocatorBase
    commentId: M:WizardWrx.Core.AssemblyLocatorBase.SetPropertiesFromDLLConfiguration(System.Type)
  System:
    name:
      CSharp:
      - name: System
        nameWithType: System
        qualifiedName: System
        isExternal: true
      VB:
      - name: System
        nameWithType: System
        qualifiedName: System
    isDefinition: true
    commentId: N:System
  System.Object:
    name:
      CSharp:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      VB:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Object
  System.Object.Equals(System.Object):
    name:
      CSharp:
      - id: System.Object.Equals(System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.Equals(System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.Equals(System.Object)
  System.Object.Equals(System.Object,System.Object):
    name:
      CSharp:
      - id: System.Object.Equals(System.Object,System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.Equals(System.Object,System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.Equals(System.Object,System.Object)
  System.Object.ReferenceEquals(System.Object,System.Object):
    name:
      CSharp:
      - id: System.Object.ReferenceEquals(System.Object,System.Object)
        name: ReferenceEquals
        nameWithType: Object.ReferenceEquals
        qualifiedName: System.Object.ReferenceEquals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.ReferenceEquals(System.Object,System.Object)
        name: ReferenceEquals
        nameWithType: Object.ReferenceEquals
        qualifiedName: System.Object.ReferenceEquals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.ReferenceEquals(System.Object,System.Object)
  System.Object.GetHashCode:
    name:
      CSharp:
      - id: System.Object.GetHashCode
        name: GetHashCode
        nameWithType: Object.GetHashCode
        qualifiedName: System.Object.GetHashCode
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.GetHashCode
        name: GetHashCode
        nameWithType: Object.GetHashCode
        qualifiedName: System.Object.GetHashCode
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.GetHashCode
  System.Object.GetType:
    name:
      CSharp:
      - id: System.Object.GetType
        name: GetType
        nameWithType: Object.GetType
        qualifiedName: System.Object.GetType
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.GetType
        name: GetType
        nameWithType: Object.GetType
        qualifiedName: System.Object.GetType
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.GetType
  System.Object.MemberwiseClone:
    name:
      CSharp:
      - id: System.Object.MemberwiseClone
        name: MemberwiseClone
        nameWithType: Object.MemberwiseClone
        qualifiedName: System.Object.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.MemberwiseClone
        name: MemberwiseClone
        nameWithType: Object.MemberwiseClone
        qualifiedName: System.Object.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.MemberwiseClone
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor*
        name: DefaultErrorMessageColors
        nameWithType: DefaultErrorMessageColors.DefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor*
        name: DefaultErrorMessageColors
        nameWithType: DefaultErrorMessageColors.DefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.DefaultErrorMessageColors
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.#ctor
  System.Reflection:
    name:
      CSharp:
      - name: System.Reflection
        nameWithType: System.Reflection
        qualifiedName: System.Reflection
        isExternal: true
      VB:
      - name: System.Reflection
        nameWithType: System.Reflection
        qualifiedName: System.Reflection
    isDefinition: true
    commentId: N:System.Reflection
  System.Reflection.Assembly:
    name:
      CSharp:
      - id: System.Reflection.Assembly
        name: Assembly
        nameWithType: Assembly
        qualifiedName: System.Reflection.Assembly
        isExternal: true
      VB:
      - id: System.Reflection.Assembly
        name: Assembly
        nameWithType: Assembly
        qualifiedName: System.Reflection.Assembly
        isExternal: true
    isDefinition: true
    parent: System.Reflection
    commentId: T:System.Reflection.Assembly
  System.ConsoleColor:
    name:
      CSharp:
      - id: System.ConsoleColor
        name: ConsoleColor
        nameWithType: ConsoleColor
        qualifiedName: System.ConsoleColor
        isExternal: true
      VB:
      - id: System.ConsoleColor
        name: ConsoleColor
        nameWithType: ConsoleColor
        qualifiedName: System.ConsoleColor
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.ConsoleColor
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor*
        name: FatalExceptionTextColor
        nameWithType: DefaultErrorMessageColors.FatalExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor*
        name: FatalExceptionTextColor
        nameWithType: DefaultErrorMessageColors.FatalExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionTextColor
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor*
        name: FatalExceptionBackgroundColor
        nameWithType: DefaultErrorMessageColors.FatalExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor*
        name: FatalExceptionBackgroundColor
        nameWithType: DefaultErrorMessageColors.FatalExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.FatalExceptionBackgroundColor
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor*
        name: RecoverableExceptionTextColor
        nameWithType: DefaultErrorMessageColors.RecoverableExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor*
        name: RecoverableExceptionTextColor
        nameWithType: DefaultErrorMessageColors.RecoverableExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionTextColor
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor*
        name: RecoverableExceptionBackgroundColor
        nameWithType: DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor*
        name: RecoverableExceptionBackgroundColor
        nameWithType: DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.RecoverableExceptionBackgroundColor
  System.Int32:
    name:
      CSharp:
      - id: System.Int32
        name: Int32
        nameWithType: Int32
        qualifiedName: System.Int32
        isExternal: true
      VB:
      - id: System.Int32
        name: Int32
        nameWithType: Int32
        qualifiedName: System.Int32
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Int32
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault*
        name: PropsLeftAtDefault
        nameWithType: DefaultErrorMessageColors.PropsLeftAtDefault
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault*
        name: PropsLeftAtDefault
        nameWithType: DefaultErrorMessageColors.PropsLeftAtDefault
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsLeftAtDefault
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig*
        name: PropsSetFromConfig
        nameWithType: DefaultErrorMessageColors.PropsSetFromConfig
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig*
        name: PropsSetFromConfig
        nameWithType: DefaultErrorMessageColors.PropsSetFromConfig
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.PropsSetFromConfig
  System.String:
    name:
      CSharp:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      VB:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.String
  System.Object.ToString:
    name:
      CSharp:
      - id: System.Object.ToString
        name: ToString
        nameWithType: Object.ToString
        qualifiedName: System.Object.ToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.ToString
        name: ToString
        nameWithType: Object.ToString
        qualifiedName: System.Object.ToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.ToString
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString*
        name: ToString
        nameWithType: DefaultErrorMessageColors.ToString
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString*
        name: ToString
        nameWithType: DefaultErrorMessageColors.ToString
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.DefaultErrorMessageColors.ToString
  WizardWrx.ConsoleStreams.DefaultErrorMessageColors:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
        name: DefaultErrorMessageColors
        nameWithType: DefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
      VB:
      - id: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
        name: DefaultErrorMessageColors
        nameWithType: DefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.DefaultErrorMessageColors
    isDefinition: true
    commentId: T:WizardWrx.ConsoleStreams.DefaultErrorMessageColors
  WizardWrx.ConsoleStreams.MessageInColor:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor
        name: MessageInColor
        nameWithType: MessageInColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor
        name: MessageInColor
        nameWithType: MessageInColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.MessageInColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile*
        name: InitializeDefaultPropertiesFromDllConfogurationFile
        nameWithType: ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile*
        name: InitializeDefaultPropertiesFromDllConfogurationFile
        nameWithType: ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.InitializeDefaultPropertiesFromDllConfogurationFile
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor*
        name: ErrorMessagesInColor
        nameWithType: ErrorMessagesInColor.ErrorMessagesInColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor*
        name: ErrorMessagesInColor
        nameWithType: ErrorMessagesInColor.ErrorMessagesInColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorMessagesInColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.#ctor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor*
        name: OriginalBackgroundColor
        nameWithType: ErrorMessagesInColor.OriginalBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor*
        name: OriginalBackgroundColor
        nameWithType: ErrorMessagesInColor.OriginalBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalBackgroundColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor*
        name: OriginalForegroundColor
        nameWithType: ErrorMessagesInColor.OriginalForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor*
        name: OriginalForegroundColor
        nameWithType: ErrorMessagesInColor.OriginalForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.OriginalForegroundColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor*
        name: MessageBackgroundColor
        nameWithType: ErrorMessagesInColor.MessageBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor*
        name: MessageBackgroundColor
        nameWithType: ErrorMessagesInColor.MessageBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageBackgroundColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor*
        name: MessageForegroundColor
        nameWithType: ErrorMessagesInColor.MessageForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor*
        name: MessageForegroundColor
        nameWithType: ErrorMessagesInColor.MessageForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.MessageForegroundColor
  System.Boolean:
    name:
      CSharp:
      - id: System.Boolean
        name: Boolean
        nameWithType: Boolean
        qualifiedName: System.Boolean
        isExternal: true
      VB:
      - id: System.Boolean
        name: Boolean
        nameWithType: Boolean
        qualifiedName: System.Boolean
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Boolean
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
        name: WriteLine
        nameWithType: ErrorMessagesInColor.WriteLine
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine*
        name: WriteLine
        nameWithType: ErrorMessagesInColor.WriteLine
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.WriteLine
  System.Char:
    name:
      CSharp:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
      VB:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Char
  System.Char[]:
    name:
      CSharp:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
  System.Decimal:
    name:
      CSharp:
      - id: System.Decimal
        name: Decimal
        nameWithType: Decimal
        qualifiedName: System.Decimal
        isExternal: true
      VB:
      - id: System.Decimal
        name: Decimal
        nameWithType: Decimal
        qualifiedName: System.Decimal
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Decimal
  System.Double:
    name:
      CSharp:
      - id: System.Double
        name: Double
        nameWithType: Double
        qualifiedName: System.Double
        isExternal: true
      VB:
      - id: System.Double
        name: Double
        nameWithType: Double
        qualifiedName: System.Double
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Double
  System.Single:
    name:
      CSharp:
      - id: System.Single
        name: Single
        nameWithType: Single
        qualifiedName: System.Single
        isExternal: true
      VB:
      - id: System.Single
        name: Single
        nameWithType: Single
        qualifiedName: System.Single
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Single
  System.Int64:
    name:
      CSharp:
      - id: System.Int64
        name: Int64
        nameWithType: Int64
        qualifiedName: System.Int64
        isExternal: true
      VB:
      - id: System.Int64
        name: Int64
        nameWithType: Int64
        qualifiedName: System.Int64
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Int64
  System.UInt32:
    name:
      CSharp:
      - id: System.UInt32
        name: UInt32
        nameWithType: UInt32
        qualifiedName: System.UInt32
        isExternal: true
      VB:
      - id: System.UInt32
        name: UInt32
        nameWithType: UInt32
        qualifiedName: System.UInt32
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.UInt32
  System.UInt64:
    name:
      CSharp:
      - id: System.UInt64
        name: UInt64
        nameWithType: UInt64
        qualifiedName: System.UInt64
        isExternal: true
      VB:
      - id: System.UInt64
        name: UInt64
        nameWithType: UInt64
        qualifiedName: System.UInt64
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.UInt64
  System.Object[]:
    name:
      CSharp:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
        name: Write
        nameWithType: ErrorMessagesInColor.Write
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write*
        name: Write
        nameWithType: ErrorMessagesInColor.Write
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.Write
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString*
        name: ToString
        nameWithType: ErrorMessagesInColor.ToString
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString*
        name: ToString
        nameWithType: ErrorMessagesInColor.ToString
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ToString
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor*
        name: FatalExceptionTextColor
        nameWithType: ErrorMessagesInColor.FatalExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor*
        name: FatalExceptionTextColor
        nameWithType: ErrorMessagesInColor.FatalExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionTextColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor*
        name: FatalExceptionBackgroundColor
        nameWithType: ErrorMessagesInColor.FatalExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor*
        name: FatalExceptionBackgroundColor
        nameWithType: ErrorMessagesInColor.FatalExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.FatalExceptionBackgroundColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor*
        name: RecoverableExceptionTextColor
        nameWithType: ErrorMessagesInColor.RecoverableExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor*
        name: RecoverableExceptionTextColor
        nameWithType: ErrorMessagesInColor.RecoverableExceptionTextColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionTextColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor*
        name: RecoverableExceptionBackgroundColor
        nameWithType: ErrorMessagesInColor.RecoverableExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor*
        name: RecoverableExceptionBackgroundColor
        nameWithType: ErrorMessagesInColor.RecoverableExceptionBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RecoverableExceptionBackgroundColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
        name: RGBWriteLine
        nameWithType: ErrorMessagesInColor.RGBWriteLine
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine*
        name: RGBWriteLine
        nameWithType: ErrorMessagesInColor.RGBWriteLine
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWriteLine
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
        name: RGBWrite
        nameWithType: ErrorMessagesInColor.RGBWrite
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite*
        name: RGBWrite
        nameWithType: ErrorMessagesInColor.RGBWrite
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.RGBWrite
  WizardWrx.ConsoleStreams:
    name:
      CSharp:
      - name: WizardWrx.ConsoleStreams
        nameWithType: WizardWrx.ConsoleStreams
        qualifiedName: WizardWrx.ConsoleStreams
      VB:
      - name: WizardWrx.ConsoleStreams
        nameWithType: WizardWrx.ConsoleStreams
        qualifiedName: WizardWrx.ConsoleStreams
    isDefinition: true
    commentId: N:WizardWrx.ConsoleStreams
  WizardWrx.ConsoleStreams.ErrorMessagesInColor:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor
        name: ErrorMessagesInColor
        nameWithType: ErrorMessagesInColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor
        name: ErrorMessagesInColor
        nameWithType: ErrorMessagesInColor
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColor
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
        name: ErrorMessagesInColor.ErrorSeverity
        nameWithType: ErrorMessagesInColor.ErrorSeverity
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
        name: ErrorMessagesInColor.ErrorSeverity
        nameWithType: ErrorMessagesInColor.ErrorSeverity
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors*
        name: GetDefaultErrorMessageColors
        nameWithType: ErrorMessagesInColor.GetDefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors*
        name: GetDefaultErrorMessageColors
        nameWithType: ErrorMessagesInColor.GetDefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultErrorMessageColors
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors*
        name: GetDefaultMessageColors
        nameWithType: ErrorMessagesInColor.GetDefaultMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors*
        name: GetDefaultMessageColors
        nameWithType: ErrorMessagesInColor.GetDefaultMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.GetDefaultMessageColors
  WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors*
        name: SetDefaultErrorMessageColors
        nameWithType: ErrorMessagesInColor.SetDefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors*
        name: SetDefaultErrorMessageColors
        nameWithType: ErrorMessagesInColor.SetDefaultErrorMessageColors
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColor.SetDefaultErrorMessageColors
  System.ComponentModel:
    name:
      CSharp:
      - name: System.ComponentModel
        nameWithType: System.ComponentModel
        qualifiedName: System.ComponentModel
        isExternal: true
      VB:
      - name: System.ComponentModel
        nameWithType: System.ComponentModel
        qualifiedName: System.ComponentModel
    isDefinition: true
    commentId: N:System.ComponentModel
  System.ComponentModel.TypeConverterAttribute:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverterAttribute
        name: TypeConverterAttribute
        nameWithType: TypeConverterAttribute
        qualifiedName: System.ComponentModel.TypeConverterAttribute
        isExternal: true
      VB:
      - id: System.ComponentModel.TypeConverterAttribute
        name: TypeConverterAttribute
        nameWithType: TypeConverterAttribute
        qualifiedName: System.ComponentModel.TypeConverterAttribute
        isExternal: true
    isDefinition: true
    parent: System.ComponentModel
    commentId: T:System.ComponentModel.TypeConverterAttribute
  System.ComponentModel.TypeConverterAttribute.#ctor(System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverterAttribute.#ctor(System.Type)
        name: TypeConverterAttribute
        nameWithType: TypeConverterAttribute.TypeConverterAttribute
        qualifiedName: System.ComponentModel.TypeConverterAttribute.TypeConverterAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverterAttribute.#ctor(System.Type)
        name: TypeConverterAttribute
        nameWithType: TypeConverterAttribute.TypeConverterAttribute
        qualifiedName: System.ComponentModel.TypeConverterAttribute.TypeConverterAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverterAttribute
    commentId: M:System.ComponentModel.TypeConverterAttribute.#ctor(System.Type)
  WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
        name: ErrorMessagesInColorConverter
        nameWithType: ErrorMessagesInColorConverter
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
        name: ErrorMessagesInColorConverter
        nameWithType: ErrorMessagesInColorConverter
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter
  System.Type:
    name:
      CSharp:
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      VB:
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Type
  System.Configuration:
    name:
      CSharp:
      - name: System.Configuration
        nameWithType: System.Configuration
        qualifiedName: System.Configuration
        isExternal: true
      VB:
      - name: System.Configuration
        nameWithType: System.Configuration
        qualifiedName: System.Configuration
    isDefinition: true
    commentId: N:System.Configuration
  System.Configuration.SettingsSerializeAsAttribute:
    name:
      CSharp:
      - id: System.Configuration.SettingsSerializeAsAttribute
        name: SettingsSerializeAsAttribute
        nameWithType: SettingsSerializeAsAttribute
        qualifiedName: System.Configuration.SettingsSerializeAsAttribute
        isExternal: true
      VB:
      - id: System.Configuration.SettingsSerializeAsAttribute
        name: SettingsSerializeAsAttribute
        nameWithType: SettingsSerializeAsAttribute
        qualifiedName: System.Configuration.SettingsSerializeAsAttribute
        isExternal: true
    isDefinition: true
    parent: System.Configuration
    commentId: T:System.Configuration.SettingsSerializeAsAttribute
  System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs):
    name:
      CSharp:
      - id: System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs)
        name: SettingsSerializeAsAttribute
        nameWithType: SettingsSerializeAsAttribute.SettingsSerializeAsAttribute
        qualifiedName: System.Configuration.SettingsSerializeAsAttribute.SettingsSerializeAsAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Configuration.SettingsSerializeAs
        name: SettingsSerializeAs
        nameWithType: SettingsSerializeAs
        qualifiedName: System.Configuration.SettingsSerializeAs
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs)
        name: SettingsSerializeAsAttribute
        nameWithType: SettingsSerializeAsAttribute.SettingsSerializeAsAttribute
        qualifiedName: System.Configuration.SettingsSerializeAsAttribute.SettingsSerializeAsAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Configuration.SettingsSerializeAs
        name: SettingsSerializeAs
        nameWithType: SettingsSerializeAs
        qualifiedName: System.Configuration.SettingsSerializeAs
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Configuration.SettingsSerializeAsAttribute
    commentId: M:System.Configuration.SettingsSerializeAsAttribute.#ctor(System.Configuration.SettingsSerializeAs)
  System.Configuration.SettingsSerializeAs:
    name:
      CSharp:
      - id: System.Configuration.SettingsSerializeAs
        name: SettingsSerializeAs
        nameWithType: SettingsSerializeAs
        qualifiedName: System.Configuration.SettingsSerializeAs
        isExternal: true
      VB:
      - id: System.Configuration.SettingsSerializeAs
        name: SettingsSerializeAs
        nameWithType: SettingsSerializeAs
        qualifiedName: System.Configuration.SettingsSerializeAs
        isExternal: true
    isDefinition: true
    parent: System.Configuration
    commentId: T:System.Configuration.SettingsSerializeAs
  WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider):
    name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<T>
        nameWithType: StringExtensions.RenderEvenWhenNull<T>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<T>
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of T)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of T)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of T)
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
  WizardWrx:
    name:
      CSharp:
      - name: WizardWrx
        nameWithType: WizardWrx
        qualifiedName: WizardWrx
      VB:
      - name: WizardWrx
        nameWithType: WizardWrx
        qualifiedName: WizardWrx
    isDefinition: true
    commentId: N:WizardWrx
  WizardWrx.StringExtensions:
    name:
      CSharp:
      - id: WizardWrx.StringExtensions
        name: StringExtensions
        nameWithType: StringExtensions
        qualifiedName: WizardWrx.StringExtensions
      VB:
      - id: WizardWrx.StringExtensions
        name: StringExtensions
        nameWithType: StringExtensions
        qualifiedName: WizardWrx.StringExtensions
    isDefinition: true
    parent: WizardWrx
    commentId: T:WizardWrx.StringExtensions
  ? WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<ErrorMessagesInColor.ErrorSeverity>
        nameWithType: StringExtensions.RenderEvenWhenNull<ErrorMessagesInColor.ErrorSeverity>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity>
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of ErrorMessagesInColor.ErrorSeverity)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of ErrorMessagesInColor.ErrorSeverity)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.ConsoleStreams.ErrorMessagesInColor.ErrorSeverity)
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
    parent: WizardWrx.StringExtensions
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
  System.ComponentModel.TypeConverter:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter
        name: TypeConverter
        nameWithType: TypeConverter
        qualifiedName: System.ComponentModel.TypeConverter
        isExternal: true
      VB:
      - id: System.ComponentModel.TypeConverter
        name: TypeConverter
        nameWithType: TypeConverter
        qualifiedName: System.ComponentModel.TypeConverter
        isExternal: true
    isDefinition: true
    parent: System.ComponentModel
    commentId: T:System.ComponentModel.TypeConverter
  System.ComponentModel.TypeConverter.CanConvertFrom(System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CanConvertFrom(System.Type)
        name: CanConvertFrom
        nameWithType: TypeConverter.CanConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CanConvertFrom(System.Type)
        name: CanConvertFrom
        nameWithType: TypeConverter.CanConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CanConvertFrom(System.Type)
  System.ComponentModel.TypeConverter.CanConvertTo(System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CanConvertTo(System.Type)
        name: CanConvertTo
        nameWithType: TypeConverter.CanConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CanConvertTo(System.Type)
        name: CanConvertTo
        nameWithType: TypeConverter.CanConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CanConvertTo(System.Type)
  System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type)
        name: CanConvertTo
        nameWithType: TypeConverter.CanConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type)
        name: CanConvertTo
        nameWithType: TypeConverter.CanConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CanConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Type)
  System.ComponentModel.TypeConverter.ConvertFrom(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFrom(System.Object)
        name: ConvertFrom
        nameWithType: TypeConverter.ConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFrom(System.Object)
        name: ConvertFrom
        nameWithType: TypeConverter.ConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFrom(System.Object)
  System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String)
        name: ConvertFromInvariantString
        nameWithType: TypeConverter.ConvertFromInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String)
        name: ConvertFromInvariantString
        nameWithType: TypeConverter.ConvertFromInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.String)
  System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String)
        name: ConvertFromInvariantString
        nameWithType: TypeConverter.ConvertFromInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String)
        name: ConvertFromInvariantString
        nameWithType: TypeConverter.ConvertFromInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFromInvariantString(System.ComponentModel.ITypeDescriptorContext,System.String)
  System.ComponentModel.TypeConverter.ConvertFromString(System.String):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFromString(System.String)
  System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.String)
  ? System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
  : name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
        name: ConvertFromString
        nameWithType: TypeConverter.ConvertFromString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFromString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFromString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)
  System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type)
        name: ConvertTo
        nameWithType: TypeConverter.ConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.ConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type)
        name: ConvertTo
        nameWithType: TypeConverter.ConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.ConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertTo(System.Object,System.Type)
  System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object)
        name: ConvertToInvariantString
        nameWithType: TypeConverter.ConvertToInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object)
        name: ConvertToInvariantString
        nameWithType: TypeConverter.ConvertToInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertToInvariantString(System.Object)
  System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: ConvertToInvariantString
        nameWithType: TypeConverter.ConvertToInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: ConvertToInvariantString
        nameWithType: TypeConverter.ConvertToInvariantString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToInvariantString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertToInvariantString(System.ComponentModel.ITypeDescriptorContext,System.Object)
  System.ComponentModel.TypeConverter.ConvertToString(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertToString(System.Object)
  System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Object)
  ? System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
  : name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
        name: ConvertToString
        nameWithType: TypeConverter.ConvertToString
        qualifiedName: System.ComponentModel.TypeConverter.ConvertToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertToString(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
  System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
        name: CreateInstance
        nameWithType: TypeConverter.CreateInstance
        qualifiedName: System.ComponentModel.TypeConverter.CreateInstance
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Collections.IDictionary
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.IDictionary
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
        name: CreateInstance
        nameWithType: TypeConverter.CreateInstance
        qualifiedName: System.ComponentModel.TypeConverter.CreateInstance
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Collections.IDictionary
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.IDictionary
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
  System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)
        name: CreateInstance
        nameWithType: TypeConverter.CreateInstance
        qualifiedName: System.ComponentModel.TypeConverter.CreateInstance
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Collections.IDictionary
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.IDictionary
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)
        name: CreateInstance
        nameWithType: TypeConverter.CreateInstance
        qualifiedName: System.ComponentModel.TypeConverter.CreateInstance
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Collections.IDictionary
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.IDictionary
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)
  System.ComponentModel.TypeConverter.GetConvertFromException(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)
        name: GetConvertFromException
        nameWithType: TypeConverter.GetConvertFromException
        qualifiedName: System.ComponentModel.TypeConverter.GetConvertFromException
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)
        name: GetConvertFromException
        nameWithType: TypeConverter.GetConvertFromException
        qualifiedName: System.ComponentModel.TypeConverter.GetConvertFromException
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)
  System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)
        name: GetConvertToException
        nameWithType: TypeConverter.GetConvertToException
        qualifiedName: System.ComponentModel.TypeConverter.GetConvertToException
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)
        name: GetConvertToException
        nameWithType: TypeConverter.GetConvertToException
        qualifiedName: System.ComponentModel.TypeConverter.GetConvertToException
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)
  System.ComponentModel.TypeConverter.GetCreateInstanceSupported:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        name: GetCreateInstanceSupported
        nameWithType: TypeConverter.GetCreateInstanceSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        name: GetCreateInstanceSupported
        nameWithType: TypeConverter.GetCreateInstanceSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetCreateInstanceSupported
  System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetCreateInstanceSupported
        nameWithType: TypeConverter.GetCreateInstanceSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetCreateInstanceSupported
        nameWithType: TypeConverter.GetCreateInstanceSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetCreateInstanceSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext)
  System.ComponentModel.TypeConverter.GetProperties(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.Object)
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.Object)
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetProperties(System.Object)
  System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object)
  System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[]):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Attribute
        name: Attribute
        nameWithType: Attribute
        qualifiedName: System.Attribute
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])
        name: GetProperties
        nameWithType: TypeConverter.GetProperties
        qualifiedName: System.ComponentModel.TypeConverter.GetProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Attribute
        name: Attribute
        nameWithType: Attribute
        qualifiedName: System.Attribute
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])
  System.ComponentModel.TypeConverter.GetPropertiesSupported:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetPropertiesSupported
        name: GetPropertiesSupported
        nameWithType: TypeConverter.GetPropertiesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetPropertiesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetPropertiesSupported
        name: GetPropertiesSupported
        nameWithType: TypeConverter.GetPropertiesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetPropertiesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetPropertiesSupported
  System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetPropertiesSupported
        nameWithType: TypeConverter.GetPropertiesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetPropertiesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetPropertiesSupported
        nameWithType: TypeConverter.GetPropertiesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetPropertiesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)
  System.ComponentModel.TypeConverter.GetStandardValues:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValues
        name: GetStandardValues
        nameWithType: TypeConverter.GetStandardValues
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValues
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValues
        name: GetStandardValues
        nameWithType: TypeConverter.GetStandardValues
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValues
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValues
  System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValues
        nameWithType: TypeConverter.GetStandardValues
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValues
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValues
        nameWithType: TypeConverter.GetStandardValues
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValues
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)
  System.ComponentModel.TypeConverter.GetStandardValuesExclusive:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        name: GetStandardValuesExclusive
        nameWithType: TypeConverter.GetStandardValuesExclusive
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        name: GetStandardValuesExclusive
        nameWithType: TypeConverter.GetStandardValuesExclusive
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValuesExclusive
  System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValuesExclusive
        nameWithType: TypeConverter.GetStandardValuesExclusive
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValuesExclusive
        nameWithType: TypeConverter.GetStandardValuesExclusive
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesExclusive
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)
  System.ComponentModel.TypeConverter.GetStandardValuesSupported:
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        name: GetStandardValuesSupported
        nameWithType: TypeConverter.GetStandardValuesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        name: GetStandardValuesSupported
        nameWithType: TypeConverter.GetStandardValuesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValuesSupported
  System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValuesSupported
        nameWithType: TypeConverter.GetStandardValuesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)
        name: GetStandardValuesSupported
        nameWithType: TypeConverter.GetStandardValuesSupported
        qualifiedName: System.ComponentModel.TypeConverter.GetStandardValuesSupported
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)
  System.ComponentModel.TypeConverter.IsValid(System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.IsValid(System.Object)
        name: IsValid
        nameWithType: TypeConverter.IsValid
        qualifiedName: System.ComponentModel.TypeConverter.IsValid
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.IsValid(System.Object)
        name: IsValid
        nameWithType: TypeConverter.IsValid
        qualifiedName: System.ComponentModel.TypeConverter.IsValid
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.IsValid(System.Object)
  System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: IsValid
        nameWithType: TypeConverter.IsValid
        qualifiedName: System.ComponentModel.TypeConverter.IsValid
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object)
        name: IsValid
        nameWithType: TypeConverter.IsValid
        qualifiedName: System.ComponentModel.TypeConverter.IsValid
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.IsValid(System.ComponentModel.ITypeDescriptorContext,System.Object)
  System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[]):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[])
        name: SortProperties
        nameWithType: TypeConverter.SortProperties
        qualifiedName: System.ComponentModel.TypeConverter.SortProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.PropertyDescriptorCollection
        name: PropertyDescriptorCollection
        nameWithType: PropertyDescriptorCollection
        qualifiedName: System.ComponentModel.PropertyDescriptorCollection
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[])
        name: SortProperties
        nameWithType: TypeConverter.SortProperties
        qualifiedName: System.ComponentModel.TypeConverter.SortProperties
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.PropertyDescriptorCollection
        name: PropertyDescriptorCollection
        nameWithType: PropertyDescriptorCollection
        qualifiedName: System.ComponentModel.PropertyDescriptorCollection
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.SortProperties(System.ComponentModel.PropertyDescriptorCollection,System.String[])
  System.ComponentModel.ITypeDescriptorContext:
    name:
      CSharp:
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      VB:
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
    isDefinition: true
    parent: System.ComponentModel
    commentId: T:System.ComponentModel.ITypeDescriptorContext
  System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type):
    name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
        name: CanConvertFrom
        nameWithType: TypeConverter.CanConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
        name: CanConvertFrom
        nameWithType: TypeConverter.CanConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.CanConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.CanConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Type)
  WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom*
        name: CanConvertFrom
        nameWithType: ErrorMessagesInColorConverter.CanConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom*
        name: CanConvertFrom
        nameWithType: ErrorMessagesInColorConverter.CanConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.CanConvertFrom
  System.Globalization:
    name:
      CSharp:
      - name: System.Globalization
        nameWithType: System.Globalization
        qualifiedName: System.Globalization
        isExternal: true
      VB:
      - name: System.Globalization
        nameWithType: System.Globalization
        qualifiedName: System.Globalization
    isDefinition: true
    commentId: N:System.Globalization
  System.Globalization.CultureInfo:
    name:
      CSharp:
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      VB:
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
    isDefinition: true
    parent: System.Globalization
    commentId: T:System.Globalization.CultureInfo
  ? System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
  : name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
        name: ConvertFrom
        nameWithType: TypeConverter.ConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
        name: ConvertFrom
        nameWithType: TypeConverter.ConvertFrom
        qualifiedName: System.ComponentModel.TypeConverter.ConvertFrom
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)
  WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom*
        name: ConvertFrom
        nameWithType: ErrorMessagesInColorConverter.ConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom*
        name: ConvertFrom
        nameWithType: ErrorMessagesInColorConverter.ConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertFrom
  ? System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
  : name:
      CSharp:
      - id: System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
        name: ConvertTo
        nameWithType: TypeConverter.ConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.ConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
        name: ConvertTo
        nameWithType: TypeConverter.ConvertTo
        qualifiedName: System.ComponentModel.TypeConverter.ConvertTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.ComponentModel.ITypeDescriptorContext
        name: ITypeDescriptorContext
        nameWithType: ITypeDescriptorContext
        qualifiedName: System.ComponentModel.ITypeDescriptorContext
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Globalization.CultureInfo
        name: CultureInfo
        nameWithType: CultureInfo
        qualifiedName: System.Globalization.CultureInfo
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Type
        name: Type
        nameWithType: Type
        qualifiedName: System.Type
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.ComponentModel.TypeConverter
    commentId: M:System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)
  WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo*
        name: ConvertTo
        nameWithType: ErrorMessagesInColorConverter.ConvertTo
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo
      VB:
      - id: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo*
        name: ConvertTo
        nameWithType: ErrorMessagesInColorConverter.ConvertTo
        qualifiedName: WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.ErrorMessagesInColorConverter.ConvertTo
  WizardWrx.ConsoleStreams.MessageInColor.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.#ctor*
        name: MessageInColor
        nameWithType: MessageInColor.MessageInColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.#ctor*
        name: MessageInColor
        nameWithType: MessageInColor.MessageInColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageInColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.#ctor
  WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor*
        name: OriginalBackgroundColor
        nameWithType: MessageInColor.OriginalBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor*
        name: OriginalBackgroundColor
        nameWithType: MessageInColor.OriginalBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.OriginalBackgroundColor
  WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor*
        name: OriginalForegroundColor
        nameWithType: MessageInColor.OriginalForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor*
        name: OriginalForegroundColor
        nameWithType: MessageInColor.OriginalForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.OriginalForegroundColor
  WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor*
        name: MessageBackgroundColor
        nameWithType: MessageInColor.MessageBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor*
        name: MessageBackgroundColor
        nameWithType: MessageInColor.MessageBackgroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.MessageBackgroundColor
  WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor*
        name: MessageForegroundColor
        nameWithType: MessageInColor.MessageForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor*
        name: MessageForegroundColor
        nameWithType: MessageInColor.MessageForegroundColor
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.MessageForegroundColor
  WizardWrx.ConsoleStreams.MessageInColor.ToString*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.ToString*
        name: ToString
        nameWithType: MessageInColor.ToString
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.ToString
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.ToString*
        name: ToString
        nameWithType: MessageInColor.ToString
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.ToString
  WizardWrx.ConsoleStreams.MessageInColor.WriteLine*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
        name: WriteLine
        nameWithType: MessageInColor.WriteLine
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.WriteLine
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.WriteLine*
        name: WriteLine
        nameWithType: MessageInColor.WriteLine
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.WriteLine
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.WriteLine
  WizardWrx.ConsoleStreams.MessageInColor.Write*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.Write*
        name: Write
        nameWithType: MessageInColor.Write
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.Write
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.Write*
        name: Write
        nameWithType: MessageInColor.Write
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.Write
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.Write
  WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear*
        name: SafeConsoleClear
        nameWithType: MessageInColor.SafeConsoleClear
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear*
        name: SafeConsoleClear
        nameWithType: MessageInColor.SafeConsoleClear
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.SafeConsoleClear
  WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
        name: RGBWriteLine
        nameWithType: MessageInColor.RGBWriteLine
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine*
        name: RGBWriteLine
        nameWithType: MessageInColor.RGBWriteLine
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.RGBWriteLine
  WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
        name: RGBWrite
        nameWithType: MessageInColor.RGBWrite
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite*
        name: RGBWrite
        nameWithType: MessageInColor.RGBWrite
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColor.RGBWrite
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColor.RGBWrite
  WizardWrx.ConsoleStreams.MessageInColorConverter:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter
        name: MessageInColorConverter
        nameWithType: MessageInColorConverter
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter
        name: MessageInColorConverter
        nameWithType: MessageInColorConverter
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.MessageInColorConverter
  WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom*
        name: CanConvertFrom
        nameWithType: MessageInColorConverter.CanConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom*
        name: CanConvertFrom
        nameWithType: MessageInColorConverter.CanConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColorConverter.CanConvertFrom
  WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom*
        name: ConvertFrom
        nameWithType: MessageInColorConverter.ConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom*
        name: ConvertFrom
        nameWithType: MessageInColorConverter.ConvertFrom
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertFrom
  WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo*
        name: ConvertTo
        nameWithType: MessageInColorConverter.ConvertTo
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo
      VB:
      - id: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo*
        name: ConvertTo
        nameWithType: MessageInColorConverter.ConvertTo
        qualifiedName: WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.MessageInColorConverter.ConvertTo
  System.IntPtr:
    name:
      CSharp:
      - id: System.IntPtr
        name: IntPtr
        nameWithType: IntPtr
        qualifiedName: System.IntPtr
        isExternal: true
      VB:
      - id: System.IntPtr
        name: IntPtr
        nameWithType: IntPtr
        qualifiedName: System.IntPtr
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.IntPtr
  WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
        name: StandardHandleInfo.StandardHandleState
        nameWithType: StandardHandleInfo.StandardHandleState
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
        name: StandardHandleInfo.StandardHandleState
        nameWithType: StandardHandleInfo.StandardHandleState
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState
  WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
        name: StandardHandleInfo.StandardConsoleHandle
        nameWithType: StandardHandleInfo.StandardConsoleHandle
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
        name: StandardHandleInfo.StandardConsoleHandle
        nameWithType: StandardHandleInfo.StandardConsoleHandle
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle
  WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState*
        name: GetStandardHandleState
        nameWithType: StandardHandleInfo.GetStandardHandleState
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState*
        name: GetStandardHandleState
        nameWithType: StandardHandleInfo.GetStandardHandleState
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.StandardHandleInfo.GetStandardHandleState
  WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName*
        name: GetRedirectedFileName
        nameWithType: StandardHandleInfo.GetRedirectedFileName
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName*
        name: GetRedirectedFileName
        nameWithType: StandardHandleInfo.GetRedirectedFileName
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.StandardHandleInfo.GetRedirectedFileName
  WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode*:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode*
        name: GetWin32StatusCode
        nameWithType: StandardHandleInfo.GetWin32StatusCode
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode*
        name: GetWin32StatusCode
        nameWithType: StandardHandleInfo.GetWin32StatusCode
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode
    isDefinition: true
    commentId: Overload:WizardWrx.ConsoleStreams.StandardHandleInfo.GetWin32StatusCode
  WizardWrx.ConsoleStreams.StandardHandleInfo:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo
        name: StandardHandleInfo
        nameWithType: StandardHandleInfo
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo
        name: StandardHandleInfo
        nameWithType: StandardHandleInfo
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo
    isDefinition: true
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo
  ? WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<StandardHandleInfo.ConsoleModes>
        nameWithType: StringExtensions.RenderEvenWhenNull<StandardHandleInfo.ConsoleModes>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes>
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of StandardHandleInfo.ConsoleModes)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of StandardHandleInfo.ConsoleModes)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes)
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
    parent: WizardWrx.StringExtensions
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
  WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
        name: StandardHandleInfo.ConsoleModes
        nameWithType: StandardHandleInfo.ConsoleModes
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
        name: StandardHandleInfo.ConsoleModes
        nameWithType: StandardHandleInfo.ConsoleModes
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleModes
  System.FlagsAttribute:
    name:
      CSharp:
      - id: System.FlagsAttribute
        name: FlagsAttribute
        nameWithType: FlagsAttribute
        qualifiedName: System.FlagsAttribute
        isExternal: true
      VB:
      - id: System.FlagsAttribute
        name: FlagsAttribute
        nameWithType: FlagsAttribute
        qualifiedName: System.FlagsAttribute
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.FlagsAttribute
  System.FlagsAttribute.#ctor:
    name:
      CSharp:
      - id: System.FlagsAttribute.#ctor
        name: FlagsAttribute
        nameWithType: FlagsAttribute.FlagsAttribute
        qualifiedName: System.FlagsAttribute.FlagsAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.FlagsAttribute.#ctor
        name: FlagsAttribute
        nameWithType: FlagsAttribute.FlagsAttribute
        qualifiedName: System.FlagsAttribute.FlagsAttribute
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.FlagsAttribute
    commentId: M:System.FlagsAttribute.#ctor
  ? WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<StandardHandleInfo.ConsoleOutputModes>
        nameWithType: StringExtensions.RenderEvenWhenNull<StandardHandleInfo.ConsoleOutputModes>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes>
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of StandardHandleInfo.ConsoleOutputModes)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of StandardHandleInfo.ConsoleOutputModes)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes)
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
    parent: WizardWrx.StringExtensions
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
  WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes:
    name:
      CSharp:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
        name: StandardHandleInfo.ConsoleOutputModes
        nameWithType: StandardHandleInfo.ConsoleOutputModes
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
      VB:
      - id: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
        name: StandardHandleInfo.ConsoleOutputModes
        nameWithType: StandardHandleInfo.ConsoleOutputModes
        qualifiedName: WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
    isDefinition: true
    parent: WizardWrx.ConsoleStreams
    commentId: T:WizardWrx.ConsoleStreams.StandardHandleInfo.ConsoleOutputModes
  ? WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<StandardHandleInfo.StandardConsoleHandle>
        nameWithType: StringExtensions.RenderEvenWhenNull<StandardHandleInfo.StandardConsoleHandle>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle>
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of StandardHandleInfo.StandardConsoleHandle)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of StandardHandleInfo.StandardConsoleHandle)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.ConsoleStreams.StandardHandleInfo.StandardConsoleHandle)
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
    parent: WizardWrx.StringExtensions
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
  ? WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<StandardHandleInfo.StandardHandleState>
        nameWithType: StringExtensions.RenderEvenWhenNull<StandardHandleInfo.StandardHandleState>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState>
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull(Of StandardHandleInfo.StandardHandleState)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of StandardHandleInfo.StandardHandleState)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.ConsoleStreams.StandardHandleInfo.StandardHandleState)
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.IFormatProvider
        name: IFormatProvider
        nameWithType: IFormatProvider
        qualifiedName: System.IFormatProvider
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
    parent: WizardWrx.StringExtensions
    commentId: M:WizardWrx.StringExtensions.RenderEvenWhenNull``1(``0,System.String,System.String,System.IFormatProvider)
