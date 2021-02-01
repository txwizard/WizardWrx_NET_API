id: WizardWrx.AssemblyUtils
language: CSharp
name:
  Default: WizardWrx.AssemblyUtils
qualifiedName:
  Default: WizardWrx.AssemblyUtils
type: Assembly
modifiers: {}
items:
- id: WizardWrx.AssemblyUtils
  commentId: N:WizardWrx.AssemblyUtils
  language: CSharp
  name:
    CSharp: WizardWrx.AssemblyUtils
    VB: WizardWrx.AssemblyUtils
  nameWithType:
    CSharp: WizardWrx.AssemblyUtils
    VB: WizardWrx.AssemblyUtils
  qualifiedName:
    CSharp: WizardWrx.AssemblyUtils
    VB: WizardWrx.AssemblyUtils
  type: Namespace
  assemblies:
  - WizardWrx.AssemblyUtils
  modifiers: {}
  items:
  - id: WizardWrx.AssemblyUtils.AssemblyContainer
    commentId: T:WizardWrx.AssemblyUtils.AssemblyContainer
    language: CSharp
    name:
      CSharp: AssemblyContainer
      VB: AssemblyContainer
    nameWithType:
      CSharp: AssemblyContainer
      VB: AssemblyContainer
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.AssemblyContainer
      VB: WizardWrx.AssemblyUtils.AssemblyContainer
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/AssemblyContainer.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: AssemblyContainer
      path: ../AssemblyUtils/AssemblyContainer.cs
      startLine: 88
    summary: "\nUse this class to hold a reference to an assembly that you want to\nconfine to a separate AppDomain, so that the assembly can be unloaded by\ndiscarding its domain.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class AssemblyContainer : MarshalByRefObject'
        VB: >-
          Public Class AssemblyContainer

              Inherits MarshalByRefObject
    inheritance:
    - System.Object
    - System.MarshalByRefObject
    inheritedMembers:
    - System.MarshalByRefObject.MemberwiseClone(System.Boolean)
    - System.MarshalByRefObject.GetLifetimeService
    - System.MarshalByRefObject.InitializeLifetimeService
    - System.MarshalByRefObject.CreateObjRef(System.Type)
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
    - id: WizardWrx.AssemblyUtils.AssemblyContainer.#ctor
      commentId: M:WizardWrx.AssemblyUtils.AssemblyContainer.#ctor
      language: CSharp
      name:
        CSharp: AssemblyContainer()
        VB: AssemblyContainer()
      nameWithType:
        CSharp: AssemblyContainer.AssemblyContainer()
        VB: AssemblyContainer.AssemblyContainer()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.AssemblyContainer.AssemblyContainer()
        VB: WizardWrx.AssemblyUtils.AssemblyContainer.AssemblyContainer()
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/AssemblyContainer.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/AssemblyContainer.cs
        startLine: 93
      summary: "\nThe public constructor creates an empty container.\n"
      example: []
      syntax:
        content:
          CSharp: public AssemblyContainer()
          VB: Public Sub New
      overload: WizardWrx.AssemblyUtils.AssemblyContainer.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.AssemblyContainer.Store(System.Reflection.AssemblyName)
      commentId: M:WizardWrx.AssemblyUtils.AssemblyContainer.Store(System.Reflection.AssemblyName)
      language: CSharp
      name:
        CSharp: Store(AssemblyName)
        VB: Store(AssemblyName)
      nameWithType:
        CSharp: AssemblyContainer.Store(AssemblyName)
        VB: AssemblyContainer.Store(AssemblyName)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.AssemblyContainer.Store(System.Reflection.AssemblyName)
        VB: WizardWrx.AssemblyUtils.AssemblyContainer.Store(System.Reflection.AssemblyName)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/AssemblyContainer.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Store
        path: ../AssemblyUtils/AssemblyContainer.cs
        startLine: 101
      summary: "\nCall this method to load an assembly into the container.\n"
      example: []
      syntax:
        content:
          CSharp: public void Store(AssemblyName panThis)
          VB: Public Sub Store(panThis As AssemblyName)
        parameters:
        - id: panThis
          type: System.Reflection.AssemblyName
          description: "\nDesignate the assembly to load by its AssemblyName.\n"
      overload: WizardWrx.AssemblyUtils.AssemblyContainer.Store*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe
      commentId: M:WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe
      language: CSharp
      name:
        CSharp: ShowMe()
        VB: ShowMe()
      nameWithType:
        CSharp: AssemblyContainer.ShowMe()
        VB: AssemblyContainer.ShowMe()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe()
        VB: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/AssemblyContainer.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ShowMe
        path: ../AssemblyUtils/AssemblyContainer.cs
        startLine: 114
      summary: "\nGet a transparent reference to the assembly stored in the container.\n"
      example: []
      syntax:
        content:
          CSharp: public Assembly ShowMe()
          VB: Public Function ShowMe As Assembly
        return:
          type: System.Reflection.Assembly
          description: "\nThe reference is returned through a transparent proxy, and the main\nAppDomain can treat it as if it were local. Hence, it can be used to\ninstantiate objects, query their properties, and call their methods.\n"
      overload: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
  - id: WizardWrx.AssemblyUtils.DependentAssemblies
    commentId: T:WizardWrx.AssemblyUtils.DependentAssemblies
    language: CSharp
    name:
      CSharp: DependentAssemblies
      VB: DependentAssemblies
    nameWithType:
      CSharp: DependentAssemblies
      VB: DependentAssemblies
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.DependentAssemblies
      VB: WizardWrx.AssemblyUtils.DependentAssemblies
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/DependentAssemblies.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: DependentAssemblies
      path: ../AssemblyUtils/DependentAssemblies.cs
      startLine: 141
    summary: "\nUse instances of this class to enumerate the dependents of an assembly.\n"
    example: []
    syntax:
      content:
        CSharp: public class DependentAssemblies
        VB: Public Class DependentAssemblies
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
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.#ctor
      language: CSharp
      name:
        CSharp: DependentAssemblies()
        VB: DependentAssemblies()
      nameWithType:
        CSharp: DependentAssemblies.DependentAssemblies()
        VB: DependentAssemblies.DependentAssemblies()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies()
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies()
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 147
      summary: "\nCreate an instance that has the calling assembly as its root.\n"
      example: []
      syntax:
        content:
          CSharp: public DependentAssemblies()
          VB: Public Sub New
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor(System.Reflection.Assembly)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.#ctor(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: DependentAssemblies(Assembly)
        VB: DependentAssemblies(Assembly)
      nameWithType:
        CSharp: DependentAssemblies.DependentAssemblies(Assembly)
        VB: DependentAssemblies.DependentAssemblies(Assembly)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies(System.Reflection.Assembly)
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies(System.Reflection.Assembly)
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 180
      summary: "\nCreate an instance that has a specified assembly as its root.\n"
      example: []
      syntax:
        content:
          CSharp: public DependentAssemblies(Assembly pasmTopLevel)
          VB: Public Sub New(pasmTopLevel As Assembly)
        parameters:
        - id: pasmTopLevel
          type: System.Reflection.Assembly
          description: "\nSpecify the assembly to establish as the top level reference assembly.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon(System.Reflection.AssemblyName)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon(System.Reflection.AssemblyName)
      language: CSharp
      name:
        CSharp: AssemblyDependsUpon(AssemblyName)
        VB: AssemblyDependsUpon(AssemblyName)
      nameWithType:
        CSharp: DependentAssemblies.AssemblyDependsUpon(AssemblyName)
        VB: DependentAssemblies.AssemblyDependsUpon(AssemblyName)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon(System.Reflection.AssemblyName)
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon(System.Reflection.AssemblyName)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: AssemblyDependsUpon
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 201
      summary: "\nReturn TRUE if the root assembly of the instance depends upon the\nassembly named in its argument.\n"
      example: []
      syntax:
        content:
          CSharp: public bool AssemblyDependsUpon(AssemblyName panMaybeDependent)
          VB: Public Function AssemblyDependsUpon(panMaybeDependent As AssemblyName) As Boolean
        parameters:
        - id: panMaybeDependent
          type: System.Reflection.AssemblyName
          description: "\nSpecify the AssemblyName property, preferably fully qualified.\n"
        return:
          type: System.Boolean
          description: "\nIf the assembly specified as the root when the instance was created\ndepends upon the assembly named in the argument, the return value is\nTRUE. Otherwise, the return value is FALSE.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded(System.Reflection.AssemblyName)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded(System.Reflection.AssemblyName)
      language: CSharp
      name:
        CSharp: DependentAssemblyIsLoaded(AssemblyName)
        VB: DependentAssemblyIsLoaded(AssemblyName)
      nameWithType:
        CSharp: DependentAssemblies.DependentAssemblyIsLoaded(AssemblyName)
        VB: DependentAssemblies.DependentAssemblyIsLoaded(AssemblyName)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded(System.Reflection.AssemblyName)
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded(System.Reflection.AssemblyName)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DependentAssemblyIsLoaded
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 220
      summary: "\nReturn TRUE if the root assembly of the instance depends upon the\nassembly named in its argument AND that assembly  is loaded.\n"
      example: []
      syntax:
        content:
          CSharp: public bool DependentAssemblyIsLoaded(AssemblyName panMaybeDependent)
          VB: Public Function DependentAssemblyIsLoaded(panMaybeDependent As AssemblyName) As Boolean
        parameters:
        - id: panMaybeDependent
          type: System.Reflection.AssemblyName
          description: "\nSpecify the AssemblyName property, preferably fully qualified.\n"
        return:
          type: System.Boolean
          description: "\nIf the assembly specified as the root when the instance was created\ndepends upon the assembly named in the argument, and the named\nassembly is loaded into the default Application Domain, the return\nvalue is TRUE. Otherwise, the return value is FALSE.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents
      language: CSharp
      name:
        CSharp: DestroyDependents()
        VB: DestroyDependents()
      nameWithType:
        CSharp: DependentAssemblies.DestroyDependents()
        VB: DependentAssemblies.DestroyDependents()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents()
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DestroyDependents
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 242
      summary: "\nThis object gets an explicitly implemented destructor, because it\nmay acquire a secondary AppDomain that should be destroyed before\nthe main processing routine progresses further.\n"
      example: []
      syntax:
        content:
          CSharp: public void DestroyDependents()
          VB: Public Sub DestroyDependents
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties(System.IO.StreamWriter,System.Char)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties(System.IO.StreamWriter,System.Char)
      language: CSharp
      name:
        CSharp: DisplayProperties(StreamWriter, Char)
        VB: DisplayProperties(StreamWriter, Char)
      nameWithType:
        CSharp: DependentAssemblies.DisplayProperties(StreamWriter, Char)
        VB: DependentAssemblies.DisplayProperties(StreamWriter, Char)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties(System.IO.StreamWriter, System.Char)
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties(System.IO.StreamWriter, System.Char)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DisplayProperties
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 266
      summary: "\nList the properties of each dependent assembly.\n"
      example: []
      syntax:
        content:
          CSharp: public void DisplayProperties(StreamWriter pswOut = null, char pchrDelimiter = ',')
          VB: Public Sub DisplayProperties(pswOut As StreamWriter = Nothing, pchrDelimiter As Char = ","c)
        parameters:
        - id: pswOut
          type: System.IO.StreamWriter
          description: "\nSpecify the optional output StreamWriter onto which the dependent\nassembly details are to be written. The default value is NULL, which\nsuppresses output.\n"
        - id: pchrDelimiter
          type: System.Char
          description: "\nSpecify the optional field delimiter. The default value is a comma.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents
      language: CSharp
      name:
        CSharp: EnumerateDependents()
        VB: EnumerateDependents()
      nameWithType:
        CSharp: DependentAssemblies.EnumerateDependents()
        VB: DependentAssemblies.EnumerateDependents()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents()
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: EnumerateDependents
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 328
      summary: "\nEnumerate the dependent assemblies.\n"
      example: []
      syntax:
        content:
          CSharp: public void EnumerateDependents()
          VB: Public Sub EnumerateDependents
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos
      language: CSharp
      name:
        CSharp: GetDependentAssemblyInfos()
        VB: GetDependentAssemblyInfos()
      nameWithType:
        CSharp: DependentAssemblies.GetDependentAssemblyInfos()
        VB: DependentAssemblies.GetDependentAssemblyInfos()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos()
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetDependentAssemblyInfos
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 367
      summary: "\nWhen called on an instance, this method returns a sorted list of the\ndependent assemblies of the assembly that was passed into its\nconstructor.\n"
      example: []
      syntax:
        content:
          CSharp: public List<DependentAssemblyInfo> GetDependentAssemblyInfos()
          VB: Public Function GetDependentAssemblyInfos As List(Of DependentAssemblyInfo)
        return:
          type: System.Collections.Generic.List{WizardWrx.AssemblyUtils.DependentAssemblyInfo}
          description: "\nIf the method succeeds, the return value is a generic list of\nDependentAssemblyInfo objects, sorted by name.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName(System.Reflection.AssemblyName,System.Boolean)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName(System.Reflection.AssemblyName,System.Boolean)
      language: CSharp
      name:
        CSharp: GetDependentAssemblyByName(AssemblyName, Boolean)
        VB: GetDependentAssemblyByName(AssemblyName, Boolean)
      nameWithType:
        CSharp: DependentAssemblies.GetDependentAssemblyByName(AssemblyName, Boolean)
        VB: DependentAssemblies.GetDependentAssemblyByName(AssemblyName, Boolean)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName(System.Reflection.AssemblyName, System.Boolean)
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName(System.Reflection.AssemblyName, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetDependentAssemblyByName
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 391
      summary: "\nGet a reference to the named dependent assembly.\n"
      example: []
      syntax:
        content:
          CSharp: public Assembly GetDependentAssemblyByName(AssemblyName panMaybeDependent, bool pfDynamicLoadingPermitted = false)
          VB: Public Function GetDependentAssemblyByName(panMaybeDependent As AssemblyName, pfDynamicLoadingPermitted As Boolean = False) As Assembly
        parameters:
        - id: panMaybeDependent
          type: System.Reflection.AssemblyName
          description: "\nSpecify the AssemblyName property, preferably fully qualified.\n"
        - id: pfDynamicLoadingPermitted
          type: System.Boolean
          description: "\nSet this flag to TRUE to permit an assembly to be loaded to satisfy\nthe request. The default is FALSE, so that a request is unsatisfied\nunless the required assembly is already loaded.\n"
        return:
          type: System.Reflection.Assembly
          description: "\nIf the named assembly is a dependent, and it is successfully loaded,\nthe return value is a reference to the assembly. If the assembly was\nalready loaded into the default application domain, the reference is\nto that assembly. Otherwise, the reference is to the assembly that\nwas loaded into the private application domain.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblies.Root
      commentId: P:WizardWrx.AssemblyUtils.DependentAssemblies.Root
      language: CSharp
      name:
        CSharp: Root
        VB: Root
      nameWithType:
        CSharp: DependentAssemblies.Root
        VB: DependentAssemblies.Root
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblies.Root
        VB: WizardWrx.AssemblyUtils.DependentAssemblies.Root
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblies.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Root
        path: ../AssemblyUtils/DependentAssemblies.cs
        startLine: 434
      summary: "\nGet the root assembly around which the instance was constructed.\n"
      example: []
      syntax:
        content:
          CSharp: public Assembly Root { get; }
          VB: Public ReadOnly Property Root As Assembly
        parameters: []
        return:
          type: System.Reflection.Assembly
      overload: WizardWrx.AssemblyUtils.DependentAssemblies.Root*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
  - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
    commentId: T:WizardWrx.AssemblyUtils.DependentAssemblyInfo
    language: CSharp
    name:
      CSharp: DependentAssemblyInfo
      VB: DependentAssemblyInfo
    nameWithType:
      CSharp: DependentAssemblyInfo
      VB: DependentAssemblyInfo
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/DependentAssemblyInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: DependentAssemblyInfo
      path: ../AssemblyUtils/DependentAssemblyInfo.cs
      startLine: 107
    summary: "\nAn instance of this class is created for each assembly listed as a\ndependent, and is used to track the assemblies that must be loaded to\nquery their properties.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class DependentAssemblyInfo : IComparable<DependentAssemblyInfo>'
        VB: >-
          Public Class DependentAssemblyInfo

              Implements IComparable(Of DependentAssemblyInfo)
    inheritance:
    - System.Object
    implements:
    - System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}
    inheritedMembers:
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
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
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IS_LOADED
      commentId: F:WizardWrx.AssemblyUtils.DependentAssemblyInfo.IS_LOADED
      language: CSharp
      name:
        CSharp: IS_LOADED
        VB: IS_LOADED
      nameWithType:
        CSharp: DependentAssemblyInfo.IS_LOADED
        VB: DependentAssemblyInfo.IS_LOADED
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IS_LOADED
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IS_LOADED
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IS_LOADED
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 114
      summary: "\nThe IsLoaded property has this value when the assembly is loaded\nwhen the DependentAssemblies query loop runs.\n"
      example: []
      syntax:
        content:
          CSharp: public const bool IS_LOADED = true
          VB: Public Const IS_LOADED As Boolean = True
        return:
          type: System.Boolean
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.NOT_LOADED
      commentId: F:WizardWrx.AssemblyUtils.DependentAssemblyInfo.NOT_LOADED
      language: CSharp
      name:
        CSharp: NOT_LOADED
        VB: NOT_LOADED
      nameWithType:
        CSharp: DependentAssemblyInfo.NOT_LOADED
        VB: DependentAssemblyInfo.NOT_LOADED
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.NOT_LOADED
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.NOT_LOADED
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: NOT_LOADED
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 120
      summary: "\nThe IsLoaded property has this value when the assembly is unloaded\nwhen the DependentAssemblies query loop runs.\n"
      example: []
      syntax:
        content:
          CSharp: public const bool NOT_LOADED = false
          VB: Public Const NOT_LOADED As Boolean = False
        return:
          type: System.Boolean
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor(System.Reflection.AssemblyName)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor(System.Reflection.AssemblyName)
      language: CSharp
      name:
        CSharp: DependentAssemblyInfo(AssemblyName)
        VB: DependentAssemblyInfo(AssemblyName)
      nameWithType:
        CSharp: DependentAssemblyInfo.DependentAssemblyInfo(AssemblyName)
        VB: DependentAssemblyInfo.DependentAssemblyInfo(AssemblyName)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DependentAssemblyInfo(System.Reflection.AssemblyName)
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DependentAssemblyInfo(System.Reflection.AssemblyName)
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 137
      summary: "\nThe public constructor requires an AssemblyName from which to\ncreate an initialized instance.\n"
      example: []
      syntax:
        content:
          CSharp: public DependentAssemblyInfo(AssemblyName panmAssemblyName)
          VB: Public Sub New(panmAssemblyName As AssemblyName)
        parameters:
        - id: panmAssemblyName
          type: System.Reflection.AssemblyName
          description: "\nAssemblyName fully describes an assembly, including properties that\ngive direct access to the base (simple) name, version, culture, and\npublic key token.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains
      language: CSharp
      name:
        CSharp: DestroyOwneAppdDomains()
        VB: DestroyOwneAppdDomains()
      nameWithType:
        CSharp: DependentAssemblyInfo.DestroyOwneAppdDomains()
        VB: DependentAssemblyInfo.DestroyOwneAppdDomains()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains()
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DestroyOwneAppdDomains
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 155
      summary: "\nYou must call this method once, when you are finished using the\nobject, but before it goes out of scope, to unload the private\nAppDomain, along with the assemblies that were loaded into it.\n"
      remarks: "\nThis activity cannot be performed by a destructor, because the\nunload fails with HRESULT 0x80131015 when the unload is initiated by\na destructor, or when a destructor is active.\n"
      example: []
      syntax:
        content:
          CSharp: public void DestroyOwneAppdDomains()
          VB: Public Sub DestroyOwneAppdDomains
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection
      language: CSharp
      name:
        CSharp: LoadForInspection()
        VB: LoadForInspection()
      nameWithType:
        CSharp: DependentAssemblyInfo.LoadForInspection()
        VB: DependentAssemblyInfo.LoadForInspection()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection()
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LoadForInspection
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 167
      summary: "\nThe name of this method reflects its motivation, which was to report\non the assemblies upon which a specified &quot;root&quot; assembly depends.\n"
      example: []
      syntax:
        content:
          CSharp: public void LoadForInspection()
          VB: Public Sub LoadForInspection
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded(System.Reflection.Assembly)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: MarkAsLoaded(Assembly)
        VB: MarkAsLoaded(Assembly)
      nameWithType:
        CSharp: DependentAssemblyInfo.MarkAsLoaded(Assembly)
        VB: DependentAssemblyInfo.MarkAsLoaded(Assembly)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded(System.Reflection.Assembly)
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded(System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: MarkAsLoaded
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 184
      summary: "\nCall this method to mark an assembly as loaded.\n"
      example: []
      syntax:
        content:
          CSharp: public void MarkAsLoaded(Assembly pasmThis)
          VB: Public Sub MarkAsLoaded(pasmThis As Assembly)
        parameters:
        - id: pasmThis
          type: System.Reflection.Assembly
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals(System.Object)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals(System.Object)
      language: CSharp
      name:
        CSharp: Equals(Object)
        VB: Equals(Object)
      nameWithType:
        CSharp: DependentAssemblyInfo.Equals(Object)
        VB: DependentAssemblyInfo.Equals(Object)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals(System.Object)
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals(System.Object)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Equals
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 204
      summary: "\nTest a pair of object instances for logical equality.\n"
      example: []
      syntax:
        content:
          CSharp: public override bool Equals(object obj)
          VB: Public Overrides Function Equals(obj As Object) As Boolean
        parameters:
        - id: obj
          type: System.Object
          description: "\nSupply a reference to the other object to test for equality with the\ncalling instance.\n"
        return:
          type: System.Boolean
          description: "\nThis method returns TRUE if the two objects are of the same or\nequivalent types and their FullName properties are equal. Otherwise,\nthe return value is FALSE.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals*
      overridden: System.Object.Equals(System.Object)
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode
      language: CSharp
      name:
        CSharp: GetHashCode()
        VB: GetHashCode()
      nameWithType:
        CSharp: DependentAssemblyInfo.GetHashCode()
        VB: DependentAssemblyInfo.GetHashCode()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode()
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetHashCode
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 225
      summary: "\nReturn the HashCode property of the FullName property of the instance.\n"
      example: []
      syntax:
        content:
          CSharp: public override int GetHashCode()
          VB: Public Overrides Function GetHashCode As Integer
        return:
          type: System.Int32
          description: ''
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode*
      overridden: System.Object.GetHashCode
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: DependentAssemblyInfo.ToString()
        VB: DependentAssemblyInfo.ToString()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString()
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 238
      summary: "\nReturn a string representation of the instance.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nThe returned string consists of the object type, as it would be\nreported by the base ToString method, followed by the FullName and\nIsLoaded property values.\n"
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString*
      overridden: System.Object.ToString
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - isEii: true
      id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
      commentId: M:WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
      language: CSharp
      name:
        CSharp: IComparable<DependentAssemblyInfo>.CompareTo(DependentAssemblyInfo)
        VB: System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo(DependentAssemblyInfo)
      nameWithType:
        CSharp: DependentAssemblyInfo.IComparable<DependentAssemblyInfo>.CompareTo(DependentAssemblyInfo)
        VB: DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo(DependentAssemblyInfo)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 250
      syntax:
        content:
          CSharp: int IComparable<DependentAssemblyInfo>.CompareTo(DependentAssemblyInfo other)
          VB: Function System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo(other As DependentAssemblyInfo) As Integer Implements IComparable(Of DependentAssemblyInfo).CompareTo
        parameters:
        - id: other
          type: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        return:
          type: System.Int32
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo*
      implements:
      - System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}.CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
      modifiers:
        CSharp: []
        VB: []
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
      commentId: P:WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
      language: CSharp
      name:
        CSharp: AssemblyDetails
        VB: AssemblyDetails
      nameWithType:
        CSharp: DependentAssemblyInfo.AssemblyDetails
        VB: DependentAssemblyInfo.AssemblyDetails
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: AssemblyDetails
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 263
      summary: "\nGet the Assembly, itself. If it was already loaded for use, this is\na reference to the live assembly. Otherwise, it is a copy that was\nloaded for reflection, and will be unloaded by the destructor.\n"
      example: []
      syntax:
        content:
          CSharp: public Assembly AssemblyDetails { get; }
          VB: Public ReadOnly Property AssemblyDetails As Assembly
        parameters: []
        return:
          type: System.Reflection.Assembly
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
      commentId: P:WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
      language: CSharp
      name:
        CSharp: FullName
        VB: FullName
      nameWithType:
        CSharp: DependentAssemblyInfo.FullName
        VB: DependentAssemblyInfo.FullName
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: FullName
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 288
      summary: "\nGet the FullName of the assembly.\n"
      example: []
      syntax:
        content:
          CSharp: public string FullName { get; }
          VB: Public ReadOnly Property FullName As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
      commentId: P:WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
      language: CSharp
      name:
        CSharp: IsLoaded
        VB: IsLoaded
      nameWithType:
        CSharp: DependentAssemblyInfo.IsLoaded
        VB: DependentAssemblyInfo.IsLoaded
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
        VB: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/DependentAssemblyInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IsLoaded
        path: ../AssemblyUtils/DependentAssemblyInfo.cs
        startLine: 293
      summary: "\nGet the load state of the assembly.\n"
      example: []
      syntax:
        content:
          CSharp: public bool IsLoaded { get; }
          VB: Public ReadOnly Property IsLoaded As Boolean
        parameters: []
        return:
          type: System.Boolean
      overload: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
  - id: WizardWrx.AssemblyUtils.PESubsystemInfo
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo
    language: CSharp
    name:
      CSharp: PESubsystemInfo
      VB: PESubsystemInfo
    nameWithType:
      CSharp: PESubsystemInfo
      VB: PESubsystemInfo
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo
      VB: WizardWrx.AssemblyUtils.PESubsystemInfo
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/PESubsystemInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: PESubsystemInfo
      path: ../AssemblyUtils/PESubsystemInfo.cs
      startLine: 106
    summary: "\nThis class exposes methods for obtaining the subsystem ID encoded into\nthe NT header of a Windows Portable Executable (PE) file. Such files\ninclude, but are not limited to, character mode and graphical mode\nprograms implemented in both native or managed programming languages,\ndynamic link libraries, and device drivers.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class PESubsystemInfo : GenericSingletonBase<PESubsystemInfo>'
        VB: >-
          Public Class PESubsystemInfo

              Inherits GenericSingletonBase(Of PESubsystemInfo)
    inheritance:
    - System.Object
    - WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}
    inheritedMembers:
    - WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.s_genTheOnlyInstance
    - WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.TheOnlyInstance
    - WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.GetTheSingleInstance
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
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_UNKNOWN
        VB: IMAGE_SUBSYSTEM_UNKNOWN
      nameWithType:
        CSharp: PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
        VB: PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_UNKNOWN
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_UNKNOWN
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 217
      summary: "\nThe ProcessSubsystmID property returns this value until the private\nconstructor initializes it.\n"
      example: []
      syntax:
        content:
          CSharp: public const short IMAGE_SUBSYSTEM_UNKNOWN = 0
          VB: Public Const IMAGE_SUBSYSTEM_UNKNOWN As Short = 0
        return:
          type: System.Int16
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: IMAGE_SUBSYSTEM_WINDOWS_GUI
      nameWithType:
        CSharp: PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_GUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_WINDOWS_GUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 229
      summary: "\nThe ProcessSubsystmID property returns this value when queried on an\nPESubsystemInfo singleton instance that was initialized by a Windows\n(graphical mode) entry assembly.\n\nStatic method GetPESubsystemID returns this value when queried about\nthe subsystem ID of a Windows (graphical mode) assembly or compiled\nnative program.\n"
      example: []
      syntax:
        content:
          CSharp: public const short IMAGE_SUBSYSTEM_WINDOWS_GUI = 2
          VB: Public Const IMAGE_SUBSYSTEM_WINDOWS_GUI As Short = 2
        return:
          type: System.Int16
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: IMAGE_SUBSYSTEM_WINDOWS_CUI
      nameWithType:
        CSharp: PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.IMAGE_SUBSYSTEM_WINDOWS_CUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_WINDOWS_CUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 241
      summary: "\nThe ProcessSubsystmID property returns this value when queried on an\nPESubsystemInfo singleton instance that was initialized by a console\n(character mode) entry assembly.\n\nStatic method GetPESubsystemID returns this value when queried about\nthe subsystem ID of a console (character mode) assembly or compiled\nnative program.\n"
      example: []
      syntax:
        content:
          CSharp: public const short IMAGE_SUBSYSTEM_WINDOWS_CUI = 3
          VB: Public Const IMAGE_SUBSYSTEM_WINDOWS_CUI As Short = 3
        return:
          type: System.Int16
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      language: CSharp
      name:
        CSharp: DefaultAppDomainSubsystemID
        VB: DefaultAppDomainSubsystemID
      nameWithType:
        CSharp: PESubsystemInfo.DefaultAppDomainSubsystemID
        VB: PESubsystemInfo.DefaultAppDomainSubsystemID
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DefaultAppDomainSubsystemID
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 301
      summary: "\nGet the subsystem ID of the default application domain.\n"
      example: []
      syntax:
        content:
          CSharp: public short DefaultAppDomainSubsystemID { get; }
          VB: Public ReadOnly Property DefaultAppDomainSubsystemID As Short
        parameters: []
        return:
          type: System.Int16
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID*
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
        commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
      references:
        WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem: 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
      commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
      language: CSharp
      name:
        CSharp: DefaultAppDomainSubsystem
        VB: DefaultAppDomainSubsystem
      nameWithType:
        CSharp: PESubsystemInfo.DefaultAppDomainSubsystem
        VB: PESubsystemInfo.DefaultAppDomainSubsystem
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DefaultAppDomainSubsystem
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 314
      summary: "\nGet the subsystem in which the default application domain executes.\n"
      example: []
      syntax:
        content:
          CSharp: public PESubsystemInfo.PESubsystemID DefaultAppDomainSubsystem { get; }
          VB: Public ReadOnly Property DefaultAppDomainSubsystem As PESubsystemInfo.PESubsystemID
        parameters: []
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem*
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
        commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
      references:
        WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID: 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
      commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
      language: CSharp
      name:
        CSharp: DomainEntryAssemblyLocation
        VB: DomainEntryAssemblyLocation
      nameWithType:
        CSharp: PESubsystemInfo.DomainEntryAssemblyLocation
        VB: PESubsystemInfo.DomainEntryAssemblyLocation
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DomainEntryAssemblyLocation
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 328
      summary: "\nGet the fully qualified name of the file from which the first\nassembly that was loaded into the default application domain was\nread.\n"
      example: []
      syntax:
        content:
          CSharp: public string DomainEntryAssemblyLocation { get; }
          VB: Public ReadOnly Property DomainEntryAssemblyLocation As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
      commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
      language: CSharp
      name:
        CSharp: DefaultDomainEntryAssemblyName
        VB: DefaultDomainEntryAssemblyName
      nameWithType:
        CSharp: PESubsystemInfo.DefaultDomainEntryAssemblyName
        VB: PESubsystemInfo.DefaultDomainEntryAssemblyName
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DefaultDomainEntryAssemblyName
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 345
      summary: "\nGet the fully qualified AssemblyName of the first assembly that was\nloaded into the default application domain.\n"
      remarks: "\nAssemblyName has properties that expose the parts of an assembly\nname, its simple name, version, culture, and public key token.\n"
      example: []
      syntax:
        content:
          CSharp: public AssemblyName DefaultDomainEntryAssemblyName { get; }
          VB: Public ReadOnly Property DefaultDomainEntryAssemblyName As AssemblyName
        parameters: []
        return:
          type: System.Reflection.AssemblyName
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      language: CSharp
      name:
        CSharp: GetPESubsystemID(String)
        VB: GetPESubsystemID(String)
      nameWithType:
        CSharp: PESubsystemInfo.GetPESubsystemID(String)
        VB: PESubsystemInfo.GetPESubsystemID(String)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetPESubsystemID
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 431
      summary: "\nCall this method with the name of a file to get its subsystem ID.\n"
      example: []
      syntax:
        content:
          CSharp: public static short GetPESubsystemID(string pstrFileName)
          VB: Public Shared Function GetPESubsystemID(pstrFileName As String) As Short
        parameters:
        - id: pstrFileName
          type: System.String
          description: "\nThis string must be the name of a file that can be found in the\ncurrent security context. Names may be either relative or fully\nqualified. Relative file names are resolved relative to the current\nworking directory.\n"
        return:
          type: System.Int16
          description: "\nIf the specified file exists and is a valid Windows Portable\nExecutable (PE) file, its subsystem ID is returned. Since the ID is\nrepresented internally as a 16 bit signed integer, the return type\nof Int16 is guaranteed to be correct, regardless of the machine \narchitecture. Debuggers and stack traces may represent this type as\na short, a common alias for Int16.\n"
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID*
      exceptions:
      - type: System.ArgumentException
        commentId: T:System.ArgumentException
        description: "\nThe following conditions elicit an ArgumentException exception.\n\n1) String argument pstrFileName is a null reference or points to the\nempty string.\n\n2) String argument pstrFileName specifies a file that cannot be\nfound in the current security context.\n\n3) String argument pstrFileName specifies a file that is too small\nto contain a valid PE header, let alone its associated code and&apos;\ndata.\n"
      - type: System.Exception
        commentId: T:System.Exception
        description: "\nThe following conditions elicit an Exception (the garden variety\nexception class) exception.\n\n1) The file read returned, indicating that fewer than the expected\nnumber of bytes was read. This is probably the result of an internal\nprogramming error, and is unlikely to arise in practice, since this\ntype of error should elicit an I/O exception.\n\n2) The Int16 (16 bit signed integer) that marks the start of a valid\nDOS file header is missing.\n\n3) The DWORD (32 bit unsigned integer) in the DOS header that should\npoint to the start of the NT header is either null, or it points to\na location beyond the first 1024 bytes of the file.\n\n4) The magic Int32 (32 bit signed integer) that marks the start of\nthe NT header is not where the pointer in the DOS header says it\nshould be.\n\n5) The I/O subsystem threw an exception. A new garden variety\nexception object is created, the I/O exception is attached as its\nInnerException property, and the new exception is thrown up the call\nstack. \n\nWrapping the I/O exception in a garden variety exception lets the\nfinal exception report include the name of the file that was being\nprocessed when the exception arose, which may provide useful clues\nabout its root cause.\n\n6) A completely unexpected event gave rise to an exception. A new\ngarden variety exception object is created, the original exception\nis attached as its InnerException property, and the new exception is\nthrown up the call stack.\n\nWrapping the original I/O exception in a new exception lets the \nfinal exception report include the name of the file that was being\nprocessed when the exception arose, which may provide useful clues\nabout its root cause.\n"
      see:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
        commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        : 
        ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        : 
        WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String): 
        WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID: 
        System.ArgumentException: 
        System.Exception: 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
      language: CSharp
      name:
        CSharp: GetPESubsystem(String)
        VB: GetPESubsystem(String)
      nameWithType:
        CSharp: PESubsystemInfo.GetPESubsystem(String)
        VB: PESubsystemInfo.GetPESubsystem(String)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetPESubsystem
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 660
      summary: "\nUse this wrapper to get the PESubsystemID enumeration member\nequivalent to the integer returned by GetPESubsystemID.\n"
      example: []
      syntax:
        content:
          CSharp: public static PESubsystemInfo.PESubsystemID GetPESubsystem(string pstrFileName)
          VB: Public Shared Function GetPESubsystem(pstrFileName As String) As PESubsystemInfo.PESubsystemID
        parameters:
        - id: pstrFileName
          type: System.String
          description: "\nThis string must be the name of a file that can be found in the\ncurrent security context. Names may be either relative or fully\nqualified. Relative file names are resolved relative to the current\nworking directory.\n"
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
          description: "\nIf the function succeeds, its return value is a PESubsystemID member\nthat corresponds to the 16-bit integer returned by GetPESubsystemID,\nwhich it calls internally.\n"
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem*
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String): 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      language: CSharp
      name:
        CSharp: GetPESubsystemDescription(Int16, PESubsystemInfo.SubsystemDescription)
        VB: GetPESubsystemDescription(Int16, PESubsystemInfo.SubsystemDescription)
      nameWithType:
        CSharp: PESubsystemInfo.GetPESubsystemDescription(Int16, PESubsystemInfo.SubsystemDescription)
        VB: PESubsystemInfo.GetPESubsystemDescription(Int16, PESubsystemInfo.SubsystemDescription)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16, WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16, WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetPESubsystemDescription
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 707
      summary: "\nGet the short (one or two word) or long (complete, grammatically\ncorrect sentence) description that corresponds to a Portable\nExecutable (PE) subsystem ID, such as the value returned by passing\na file name to GetPESubsystemID.\n"
      example: []
      syntax:
        content:
          CSharp: public static string GetPESubsystemDescription(short pintSubsystemID, PESubsystemInfo.SubsystemDescription penmSubsystemDescription)
          VB: Public Shared Function GetPESubsystemDescription(pintSubsystemID As Short, penmSubsystemDescription As PESubsystemInfo.SubsystemDescription) As String
        parameters:
        - id: pintSubsystemID
          type: System.Int16
          description: "\nSpecify the subsystem ID for which the corresponding short or long\ndescription is wanted. Suitable inputs include the signed integer\nreturned by GetPESubsystemID, which may be called as a nested method\nif you have no further use for the subsystem ID.\n"
        - id: penmSubsystemDescription
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
          description: "\nA member of the SubsystemDescription specifies whether to return the\nshort (one or two word) description or the long (complete sentence)\ndescription that corresponds to the specified subsystem ID.\n"
        return:
          type: System.String
          description: "\nIf the function succeeds, the return value is a string containing a\nshort or long description that corresponds to a specified subsystem\nID.\n"
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription*
      exceptions:
      - type: System.ArgumentOutOfRangeException
        commentId: T:System.ArgumentOutOfRangeException
        description: "\nIf the subsystem ID specified by argument pintSubsystemID is either\nnegative or greater than the largest valid subsystem ID (9, at\npresent, though future editions of the Microsoft Platform SDK might\ndefine additional IDs), an ArgumentOutOfRangeException exception is\nthrown, which reports the\n"
      - type: System.ComponentModel.InvalidEnumArgumentException
        commentId: T:System.ComponentModel.InvalidEnumArgumentException
        description: "\nAn System.ComponentModel.InvalidEnumArgumentException exception is\nthrown when the second argument, penmSubsystemDescription, is either\nSubsystemDescription.Unspecified or is not a valid member of the\nSubsystemDescription enumeration. Unspecified is defined, but marked\nas invalid to ensure that if penmSubsystemDescription is a variable,\nit is initialized.\n"
      see:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
        commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription: 
        WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String): 
        ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        : 
        System.ArgumentOutOfRangeException: 
        System.ComponentModel.InvalidEnumArgumentException: 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      language: CSharp
      name:
        CSharp: GetPESubsystemDescription(PESubsystemInfo.PESubsystemID, PESubsystemInfo.SubsystemDescription)
        VB: GetPESubsystemDescription(PESubsystemInfo.PESubsystemID, PESubsystemInfo.SubsystemDescription)
      nameWithType:
        CSharp: PESubsystemInfo.GetPESubsystemDescription(PESubsystemInfo.PESubsystemID, PESubsystemInfo.SubsystemDescription)
        VB: PESubsystemInfo.GetPESubsystemDescription(PESubsystemInfo.PESubsystemID, PESubsystemInfo.SubsystemDescription)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID, WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID, WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetPESubsystemDescription
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 770
      summary: "\nGet the short (one or two word) or long (complete, grammatically\ncorrect sentence) description that corresponds to a Portable\nExecutable (PE) subsystem ID, such as the value returned by passing\na file name to GetPESubsystemID.\n"
      remarks: "\nThis method casts penmSubsystemID to Int16, and feeds it to\nGetPESubsystemDescription, since the cast wound be necessary, sooner\nor later, to use the lookup tables that contain the descriptions.\n"
      example: []
      syntax:
        content:
          CSharp: public static string GetPESubsystemDescription(PESubsystemInfo.PESubsystemID penmSubsystemID, PESubsystemInfo.SubsystemDescription penmSubsystemDescription)
          VB: Public Shared Function GetPESubsystemDescription(penmSubsystemID As PESubsystemInfo.PESubsystemID, penmSubsystemDescription As PESubsystemInfo.SubsystemDescription) As String
        parameters:
        - id: penmSubsystemID
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
          description: "\nSpecify the PESubsystemID enumeration value for which the\ncorresponding string is needed. Suitable inputs include the value\nreturned by GetPESubsystem or the DefaultDomainSubsystem property\nreturned by the singleton instance.\n"
        - id: penmSubsystemDescription
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
          description: "\nA member of the SubsystemDescription specifies whether to return the\nshort (one or two word) description or the long (complete sentence)\ndescription that corresponds to the specified subsystem ID.\n"
        return:
          type: System.String
          description: "\nIf the function succeeds, the return value is a string containing a\nshort or long description that corresponds to a specified subsystem\nID.\n"
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription*
      see:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
        commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
      seealso:
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription: 
        WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String): 
        WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String): 
        ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
        : 
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: PESubsystemInfo.ToString()
        VB: PESubsystemInfo.ToString()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString()
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString()
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 837
      summary: "\nOverride the default ToString method to display the subsystem type.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nThe returned string resembles the following example.\n\n{{Subsystem ID = Console (3)}} WizardWrx.AssemblyUtils.PESubsystemInfo\n"
      overload: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString*
      overridden: System.Object.ToString
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
  - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
    language: CSharp
    name:
      CSharp: PESubsystemInfo.PESubsystemID
      VB: PESubsystemInfo.PESubsystemID
    nameWithType:
      CSharp: PESubsystemInfo.PESubsystemID
      VB: PESubsystemInfo.PESubsystemID
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
    type: Enum
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/PESubsystemInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: PESubsystemID
      path: ../AssemblyUtils/PESubsystemInfo.cs
      startLine: 116
    summary: "\nMap the unsigned integer returned by GetExeSubsystem onto an\nenumerated type that conveys its correct interpretation.\n"
    example: []
    syntax:
      content:
        CSharp: 'public enum PESubsystemID : uint'
        VB: Public Enum PESubsystemID As UInteger
    see:
    - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
    - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
    - linkId: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
    extensionMethods:
    - WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    - WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_UNKNOWN
        VB: IMAGE_SUBSYSTEM_UNKNOWN
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_UNKNOWN
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_UNKNOWN
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 121
      summary: "\nUnknown subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_UNKNOWN = 0U
          VB: IMAGE_SUBSYSTEM_UNKNOWN = 0UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_NATIVE
        VB: IMAGE_SUBSYSTEM_NATIVE
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_NATIVE
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 126
      summary: "\nImage doesn&apos;t require a subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_NATIVE = 1U
          VB: IMAGE_SUBSYSTEM_NATIVE = 1UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: IMAGE_SUBSYSTEM_WINDOWS_GUI
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_GUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_WINDOWS_GUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 131
      summary: "\nImage runs in the Windows GUI subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_WINDOWS_GUI = 2U
          VB: IMAGE_SUBSYSTEM_WINDOWS_GUI = 2UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: IMAGE_SUBSYSTEM_WINDOWS_CUI
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_WINDOWS_CUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 136
      summary: "\nImage runs in the Windows character subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_WINDOWS_CUI = 3U
          VB: IMAGE_SUBSYSTEM_WINDOWS_CUI = 3UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_OS2_CUI
        VB: IMAGE_SUBSYSTEM_OS2_CUI
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_OS2_CUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_OS2_CUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 141
      summary: "\nImage runs in the OS/2 character subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_OS2_CUI = 4U
          VB: IMAGE_SUBSYSTEM_OS2_CUI = 4UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_POSIX_CUI
        VB: IMAGE_SUBSYSTEM_POSIX_CUI
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_POSIX_CUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_POSIX_CUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 146
      summary: "\nImage runs in the Posix character subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_POSIX_CUI = 5U
          VB: IMAGE_SUBSYSTEM_POSIX_CUI = 5UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_NATIVE_WINDOWS
        VB: IMAGE_SUBSYSTEM_NATIVE_WINDOWS
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_NATIVE_WINDOWS
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_NATIVE_WINDOWS
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 151
      summary: "\nImage is a native Win9x driver.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_NATIVE_WINDOWS = 6U
          VB: IMAGE_SUBSYSTEM_NATIVE_WINDOWS = 6UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
        VB: IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_WINDOWS_CE_GUI
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 156
      summary: "\nImage runs in the Windows CE subsystem.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 7U
          VB: IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 7UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_EFI_APPLICATION
        VB: IMAGE_SUBSYSTEM_EFI_APPLICATION
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_APPLICATION
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_EFI_APPLICATION
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 161
      summary: "\nImage is an EFI Application.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_EFI_APPLICATION = 8U
          VB: IMAGE_SUBSYSTEM_EFI_APPLICATION = 8UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
        VB: IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 166
      summary: "\nImage is a EFI Boot Service Driver.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 9U
          VB: IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 9UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
        VB: IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 171
      summary: "\nImage is a EFI Runtime Driver.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 10U
          VB: IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 10UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_EFI_ROM
        VB: IMAGE_SUBSYSTEM_EFI_ROM
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_EFI_ROM
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_EFI_ROM
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 176
      summary: "\nImage runs from a EFI ROM.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_EFI_ROM = 11U
          VB: IMAGE_SUBSYSTEM_EFI_ROM = 11UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
      language: CSharp
      name:
        CSharp: IMAGE_SUBSYSTEM_XBOX
        VB: IMAGE_SUBSYSTEM_XBOX
      nameWithType:
        CSharp: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
        VB: PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.IMAGE_SUBSYSTEM_XBOX
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: IMAGE_SUBSYSTEM_XBOX
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 181
      summary: "\nImage runs on XBOX.\n"
      example: []
      syntax:
        content:
          CSharp: IMAGE_SUBSYSTEM_XBOX = 12U
          VB: IMAGE_SUBSYSTEM_XBOX = 12UI
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    references:
      WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String): 
      ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      : 
      ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
      : 
  - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
    language: CSharp
    name:
      CSharp: PESubsystemInfo.SubsystemDescription
      VB: PESubsystemInfo.SubsystemDescription
    nameWithType:
      CSharp: PESubsystemInfo.SubsystemDescription
      VB: PESubsystemInfo.SubsystemDescription
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      VB: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
    type: Enum
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/PESubsystemInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: SubsystemDescription
      path: ../AssemblyUtils/PESubsystemInfo.cs
      startLine: 190
    summary: "\nEach subsystem ID has both a short and long description. Use this\nenumeration as the second argument, penmSubsystemDescription, to\nstatic method GetPESubsystemDescription.\n"
    example: []
    syntax:
      content:
        CSharp: public enum SubsystemDescription
        VB: Public Enum SubsystemDescription
    extensionMethods:
    - WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    - WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Unspecified
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Unspecified
      language: CSharp
      name:
        CSharp: Unspecified
        VB: Unspecified
      nameWithType:
        CSharp: PESubsystemInfo.SubsystemDescription.Unspecified
        VB: PESubsystemInfo.SubsystemDescription.Unspecified
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Unspecified
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Unspecified
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Unspecified
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 197
      summary: "\nThis value is invalid as input to GetPESubsystemDescription, and\nis defined to require the parameter to be explicitly set, so\nthat there is no default value for penmSubsystemDescription.\n"
      example: []
      syntax:
        content:
          CSharp: Unspecified = 0
          VB: Unspecified = 0
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Short
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Short
      language: CSharp
      name:
        CSharp: Short
        VB: Short
      nameWithType:
        CSharp: PESubsystemInfo.SubsystemDescription.Short
        VB: PESubsystemInfo.SubsystemDescription.Short
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Short
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Short
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Short
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 202
      summary: "\nReturn the short (one and two word) description.\n"
      example: []
      syntax:
        content:
          CSharp: Short = 1
          VB: Short = 1
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Long
      commentId: F:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Long
      language: CSharp
      name:
        CSharp: Long
        VB: Long
      nameWithType:
        CSharp: PESubsystemInfo.SubsystemDescription.Long
        VB: PESubsystemInfo.SubsystemDescription.Long
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Long
        VB: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.Long
      type: Field
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/PESubsystemInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Long
        path: ../AssemblyUtils/PESubsystemInfo.cs
        startLine: 207
      summary: "\nReturn the long (complete sentence) description.\n"
      example: []
      syntax:
        content:
          CSharp: Long = 2
          VB: Long = 2
        return:
          type: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.AssemblyUtils.ReportGenerators
    commentId: T:WizardWrx.AssemblyUtils.ReportGenerators
    language: CSharp
    name:
      CSharp: ReportGenerators
      VB: ReportGenerators
    nameWithType:
      CSharp: ReportGenerators
      VB: ReportGenerators
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.ReportGenerators
      VB: WizardWrx.AssemblyUtils.ReportGenerators
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/ReportGenerators.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ReportGenerators
      path: ../AssemblyUtils/ReportGenerators.cs
      startLine: 94
    summary: "\nThe static members of this class generate reports about assemblies and\ntheir dependents.\n"
    example: []
    syntax:
      content:
        CSharp: public class ReportGenerators
        VB: Public Class ReportGenerators
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
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString(System.Reflection.Assembly)
      commentId: M:WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: GetAssemblyGuidString(Assembly)
        VB: GetAssemblyGuidString(Assembly)
      nameWithType:
        CSharp: ReportGenerators.GetAssemblyGuidString(Assembly)
        VB: ReportGenerators.GetAssemblyGuidString(Assembly)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString(System.Reflection.Assembly)
        VB: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString(System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/ReportGenerators.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetAssemblyGuidString
        path: ../AssemblyUtils/ReportGenerators.cs
        startLine: 107
      summary: "\nGet the GUID string (Registry format) attached to an assembly.\n"
      example: []
      syntax:
        content:
          CSharp: public static string GetAssemblyGuidString(Assembly pasm)
          VB: Public Shared Function GetAssemblyGuidString(pasm As Assembly) As String
        parameters:
        - id: pasm
          type: System.Reflection.Assembly
          description: "\nAssembly from which to return the GUID string.\n"
        return:
          type: System.String
          description: "\nIf the method succeeds, the return value is the GUID attached to it\nand intended to be associated with its type library if the assembly\nis exposed to COM.\n"
      overload: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties(System.IO.StreamWriter,System.Char)
      commentId: M:WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties(System.IO.StreamWriter,System.Char)
      language: CSharp
      name:
        CSharp: LabelKeyAssemblyProperties(StreamWriter, Char)
        VB: LabelKeyAssemblyProperties(StreamWriter, Char)
      nameWithType:
        CSharp: ReportGenerators.LabelKeyAssemblyProperties(StreamWriter, Char)
        VB: ReportGenerators.LabelKeyAssemblyProperties(StreamWriter, Char)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties(System.IO.StreamWriter, System.Char)
        VB: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties(System.IO.StreamWriter, System.Char)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/ReportGenerators.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LabelKeyAssemblyProperties
        path: ../AssemblyUtils/ReportGenerators.cs
        startLine: 140
      summary: "\nGenerate the label row on the <code data-dev-comment-type=\"paramref\" class=\"paramref\">pswOut</code> StreamWriter\nthat is delimited by <code data-dev-comment-type=\"paramref\" class=\"paramref\">pchrDlm</code> characters.\n"
      remarks: "\nThe label template is a managed resource string, REPORT_FIELD_NAMES,\nwhich governs the field order.\n"
      example: []
      syntax:
        content:
          CSharp: public static void LabelKeyAssemblyProperties(StreamWriter pswOut, char pchrDlm)
          VB: Public Shared Sub LabelKeyAssemblyProperties(pswOut As StreamWriter, pchrDlm As Char)
        parameters:
        - id: pswOut
          type: System.IO.StreamWriter
          description: "\nSpecify the open StreamWriter upon which to write.\n"
        - id: pchrDlm
          type: System.Char
          description: "\nSpecify the delimiter character.\n"
      overload: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties(System.Reflection.Assembly,System.IO.StreamWriter,System.Char)
      commentId: M:WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties(System.Reflection.Assembly,System.IO.StreamWriter,System.Char)
      language: CSharp
      name:
        CSharp: ListKeyAssemblyProperties(Assembly, StreamWriter, Char)
        VB: ListKeyAssemblyProperties(Assembly, StreamWriter, Char)
      nameWithType:
        CSharp: ReportGenerators.ListKeyAssemblyProperties(Assembly, StreamWriter, Char)
        VB: ReportGenerators.ListKeyAssemblyProperties(Assembly, StreamWriter, Char)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties(System.Reflection.Assembly, System.IO.StreamWriter, System.Char)
        VB: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties(System.Reflection.Assembly, System.IO.StreamWriter, System.Char)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/ReportGenerators.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ListKeyAssemblyProperties
        path: ../AssemblyUtils/ReportGenerators.cs
        startLine: 174
      summary: "\nCreate a record and append it to the flat file behind a\nStreamWriter.\n"
      remarks: "\nThe label template is a managed resource string, REPORT_FIELD_NAMES,\nwhich governs the field order.\n"
      example: []
      syntax:
        content:
          CSharp: public static void ListKeyAssemblyProperties(Assembly pasmSubject, StreamWriter pswOut, char pchrDlm)
          VB: Public Shared Sub ListKeyAssemblyProperties(pasmSubject As Assembly, pswOut As StreamWriter, pchrDlm As Char)
        parameters:
        - id: pasmSubject
          type: System.Reflection.Assembly
          description: "\nSpecify the assembly to be evaluated.\n"
        - id: pswOut
          type: System.IO.StreamWriter
          description: "\nSpecify the open StreamWriter upon which to write.\n"
        - id: pchrDlm
          type: System.Char
          description: "\nSpecify the delimiter character.\n"
      overload: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties(System.Reflection.Assembly,System.Int32,System.Int32)
      commentId: M:WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties(System.Reflection.Assembly,System.Int32,System.Int32)
      language: CSharp
      name:
        CSharp: ShowKeyAssemblyProperties(Assembly, Int32, Int32)
        VB: ShowKeyAssemblyProperties(Assembly, Int32, Int32)
      nameWithType:
        CSharp: ReportGenerators.ShowKeyAssemblyProperties(Assembly, Int32, Int32)
        VB: ReportGenerators.ShowKeyAssemblyProperties(Assembly, Int32, Int32)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties(System.Reflection.Assembly, System.Int32, System.Int32)
        VB: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties(System.Reflection.Assembly, System.Int32, System.Int32)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/ReportGenerators.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ShowKeyAssemblyProperties
        path: ../AssemblyUtils/ReportGenerators.cs
        startLine: 268
      summary: "\nList selected properties of any assembly on a console.\n"
      example: []
      syntax:
        content:
          CSharp: public static void ShowKeyAssemblyProperties(Assembly pasmSubject, int pintJ, int pintNDependents)
          VB: Public Shared Sub ShowKeyAssemblyProperties(pasmSubject As Assembly, pintJ As Integer, pintNDependents As Integer)
        parameters:
        - id: pasmSubject
          type: System.Reflection.Assembly
          description: "\nPass in a reference to the desired assembly, which may be the\nassembly that exports a specified type, the executing assembly, the\ncalling assembly, the entry assembly, or any other assembly for\nwhich you can obtain a reference.\n"
        - id: pintJ
          type: System.Int32
          description: "\nPass in the array subscript, a 32 bit signed integer.\n"
        - id: pintNDependents
          type: System.Int32
          description: "\nPass in the array element count, a 32 bit signed integer.\n"
      overload: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
  - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
    commentId: T:WizardWrx.AssemblyUtils.SortableManagedResourceItem
    language: CSharp
    name:
      CSharp: SortableManagedResourceItem
      VB: SortableManagedResourceItem
    nameWithType:
      CSharp: SortableManagedResourceItem
      VB: SortableManagedResourceItem
    qualifiedName:
      CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem
    type: Class
    assemblies:
    - WizardWrx.AssemblyUtils
    namespace: WizardWrx.AssemblyUtils
    source:
      remote:
        path: AssemblyUtils/SortableManagedResourceItem.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: SortableManagedResourceItem
      path: ../AssemblyUtils/SortableManagedResourceItem.cs
      startLine: 102
    summary: "\nInstances of this class represent arbitrary managed resource items.\n"
    remarks: "\nThis class is necessary because the public dictionary is opaque, so a\nconsumer has no control over the order in which they are returned. Since\nthis wrapper implements the IComparable interface, collections of these\nobjects can be sorted.\n"
    example: []
    syntax:
      content:
        CSharp: 'public class SortableManagedResourceItem : IComparable<SortableManagedResourceItem>'
        VB: >-
          Public Class SortableManagedResourceItem

              Implements IComparable(Of SortableManagedResourceItem)
    inheritance:
    - System.Object
    implements:
    - System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}
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
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor
      commentId: M:WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor
      language: CSharp
      name:
        CSharp: SortableManagedResourceItem()
        VB: SortableManagedResourceItem()
      nameWithType:
        CSharp: SortableManagedResourceItem.SortableManagedResourceItem()
        VB: SortableManagedResourceItem.SortableManagedResourceItem()
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem()
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem()
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 109
      summary: "\nOther than satisfying the requirements of the IList interface, the\nuninitialized object is useless.\n"
      example: []
      syntax:
        content:
          CSharp: public SortableManagedResourceItem()
          VB: Public Sub New
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor(System.String,System.Object)
      commentId: M:WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor(System.String,System.Object)
      language: CSharp
      name:
        CSharp: SortableManagedResourceItem(String, Object)
        VB: SortableManagedResourceItem(String, Object)
      nameWithType:
        CSharp: SortableManagedResourceItem.SortableManagedResourceItem(String, Object)
        VB: SortableManagedResourceItem.SortableManagedResourceItem(String, Object)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem(System.String, System.Object)
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem(System.String, System.Object)
      type: Constructor
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 127
      summary: "\nThis constructor creates a fully initialized instance from the data\nin the Current property of the IDictionaryEnumerator object returned\nby a ResourceSet instance.\n"
      example: []
      syntax:
        content:
          CSharp: public SortableManagedResourceItem(string pstrName, object pobjValue)
          VB: Public Sub New(pstrName As String, pobjValue As Object)
        parameters:
        - id: pstrName
          type: System.String
          description: "\nSet the Name to the string returned by the Key property of the\nenumerator.\n"
        - id: pobjValue
          type: System.Object
          description: "\nSet the Value to the System.object returned by the Value property of\nthe enumerator.\n"
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
      commentId: P:WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
      language: CSharp
      name:
        CSharp: Name
        VB: Name
      nameWithType:
        CSharp: SortableManagedResourceItem.Name
        VB: SortableManagedResourceItem.Name
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Name
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 157
      summary: "\nThe Name is the Name shown in the property sheet grid into which \ngarden variety managed string resources are input.\n"
      example: []
      syntax:
        content:
          CSharp: public string Name { get; }
          VB: Public ReadOnly Property Name As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
      commentId: P:WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
      language: CSharp
      name:
        CSharp: Value
        VB: Value
      nameWithType:
        CSharp: SortableManagedResourceItem.Value
        VB: SortableManagedResourceItem.Value
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Value
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 168
      summary: "\nThe ResourceValue property is the Value property shown in the\nproperty sheet grid into which garden variety managed string\nresources are input, and its type is usually System.string.\n"
      example: []
      syntax:
        content:
          CSharp: public object Value { get; }
          VB: Public ReadOnly Property Value As Object
        parameters: []
        return:
          type: System.Object
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
      commentId: P:WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
      language: CSharp
      name:
        CSharp: TypeName
        VB: TypeName
      nameWithType:
        CSharp: SortableManagedResourceItem.TypeName
        VB: SortableManagedResourceItem.TypeName
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
      type: Property
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: TypeName
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 181
      summary: "\nSince the full type specification is accessible through the Value\nproperty, and the string representation of the FullName is its most\nuseful property, it gets its own read only property, which also\nreturns just &quot;string&quot; for the common case, for which the prefix is\nredundant.\n"
      example: []
      syntax:
        content:
          CSharp: public string TypeName { get; }
          VB: Public ReadOnly Property TypeName As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - isEii: true
      id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
      commentId: M:WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
      language: CSharp
      name:
        CSharp: IComparable<SortableManagedResourceItem>.CompareTo(SortableManagedResourceItem)
        VB: System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo(SortableManagedResourceItem)
      nameWithType:
        CSharp: SortableManagedResourceItem.IComparable<SortableManagedResourceItem>.CompareTo(SortableManagedResourceItem)
        VB: SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo(SortableManagedResourceItem)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 215
      summary: "\nMaking the comparison based on the Name property permits sorting the\nresources in the most logical order.\n"
      example: []
      syntax:
        content:
          CSharp: int IComparable<SortableManagedResourceItem>.CompareTo(SortableManagedResourceItem other)
          VB: Function System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo(other As SortableManagedResourceItem) As Integer Implements IComparable(Of SortableManagedResourceItem).CompareTo
        parameters:
        - id: other
          type: WizardWrx.AssemblyUtils.SortableManagedResourceItem
          description: "\nThe explicit implementation puts the burden of enforcing type safety\non the runtime system.\n"
        return:
          type: System.Int32
          description: "\nSince it is a pass-through of another CompareTo method, the return\ntype is guaranteed to adhere to the interface specification.\n"
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo*
      implements:
      - System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}.CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
      modifiers:
        CSharp: []
        VB: []
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName(System.String,System.Reflection.Assembly)
      commentId: M:WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName(System.String,System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: GetInternalResourceName(String, Assembly)
        VB: GetInternalResourceName(String, Assembly)
      nameWithType:
        CSharp: SortableManagedResourceItem.GetInternalResourceName(String, Assembly)
        VB: SortableManagedResourceItem.GetInternalResourceName(String, Assembly)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName(System.String, System.Reflection.Assembly)
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName(System.String, System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetInternalResourceName
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 250
      summary: "\nUse the list of Manifest Resource Names returned by method\nGetManifestResourceNames on a specified assembly. Each of several\nmethods employs a different mechanism to identify the assembly of\ninterest.\n"
      remarks: "\nSince I cannot imagine any use for this method beyond its\ninfrastructure role in this class, I marked it private.\n"
      example: []
      syntax:
        content:
          CSharp: public static string GetInternalResourceName(string pstrResourceName, Assembly pasmSource)
          VB: Public Shared Function GetInternalResourceName(pstrResourceName As String, pasmSource As Assembly) As String
        parameters:
        - id: pstrResourceName
          type: System.String
          description: "\nSpecify the name of the file from which the embedded resource was\ncreated. Typically, this will be the local name of the file in the\nsource code tree.\n"
        - id: pasmSource
          type: System.Reflection.Assembly
          description: "\nPass a reference to the Assembly that is supposed to contain the\ndesired resource.\n"
        return:
          type: System.String
          description: "\nIf the function succeeds, the return value is the internal name of\nthe requested resource, which is fed to GetManifestResourceStream on\nthe same assembly, which returns a read-only Stream backed by the\nembedded resource. If the specified resource is not found, it\nreturns null.\n"
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName(System.Reflection.Assembly,System.IO.StreamWriter)
      commentId: M:WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName(System.Reflection.Assembly,System.IO.StreamWriter)
      language: CSharp
      name:
        CSharp: ListResourcesInAssemblyByName(Assembly, StreamWriter)
        VB: ListResourcesInAssemblyByName(Assembly, StreamWriter)
      nameWithType:
        CSharp: SortableManagedResourceItem.ListResourcesInAssemblyByName(Assembly, StreamWriter)
        VB: SortableManagedResourceItem.ListResourcesInAssemblyByName(Assembly, StreamWriter)
      qualifiedName:
        CSharp: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName(System.Reflection.Assembly, System.IO.StreamWriter)
        VB: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName(System.Reflection.Assembly, System.IO.StreamWriter)
      type: Method
      assemblies:
      - WizardWrx.AssemblyUtils
      namespace: WizardWrx.AssemblyUtils
      source:
        remote:
          path: AssemblyUtils/SortableManagedResourceItem.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ListResourcesInAssemblyByName
        path: ../AssemblyUtils/SortableManagedResourceItem.cs
        startLine: 279
      summary: "\nCall this static method from a console program to list the resources\ndefined in an assembly alphabetically by name.\n"
      remarks: "\nThis method creates and consumes a generic List of instances of the\nclass that hosts it, and uses string padding to vertically align the\nlist without resorting to composite format items.\n"
      example: []
      syntax:
        content:
          CSharp: public static void ListResourcesInAssemblyByName(Assembly pasmInWhichEmbedded, StreamWriter pswReportFile = null)
          VB: Public Shared Sub ListResourcesInAssemblyByName(pasmInWhichEmbedded As Assembly, pswReportFile As StreamWriter = Nothing)
        parameters:
        - id: pasmInWhichEmbedded
          type: System.Reflection.Assembly
          description: "\nSpecify the assembly that contains the resources to be enumerated.\n"
        - id: pswReportFile
          type: System.IO.StreamWriter
          description: "\nPass in a reference to an open StreamWriter to generate a\ntab-delimited report in addition to the console output. File output\nis suppressed when this parameter is null.\n"
      overload: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
references:
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
  System.MarshalByRefObject:
    name:
      CSharp:
      - id: System.MarshalByRefObject
        name: MarshalByRefObject
        nameWithType: MarshalByRefObject
        qualifiedName: System.MarshalByRefObject
        isExternal: true
      VB:
      - id: System.MarshalByRefObject
        name: MarshalByRefObject
        nameWithType: MarshalByRefObject
        qualifiedName: System.MarshalByRefObject
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.MarshalByRefObject
  System.MarshalByRefObject.MemberwiseClone(System.Boolean):
    name:
      CSharp:
      - id: System.MarshalByRefObject.MemberwiseClone(System.Boolean)
        name: MemberwiseClone
        nameWithType: MarshalByRefObject.MemberwiseClone
        qualifiedName: System.MarshalByRefObject.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Boolean
        name: Boolean
        nameWithType: Boolean
        qualifiedName: System.Boolean
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.MarshalByRefObject.MemberwiseClone(System.Boolean)
        name: MemberwiseClone
        nameWithType: MarshalByRefObject.MemberwiseClone
        qualifiedName: System.MarshalByRefObject.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Boolean
        name: Boolean
        nameWithType: Boolean
        qualifiedName: System.Boolean
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.MarshalByRefObject
    commentId: M:System.MarshalByRefObject.MemberwiseClone(System.Boolean)
  System.MarshalByRefObject.GetLifetimeService:
    name:
      CSharp:
      - id: System.MarshalByRefObject.GetLifetimeService
        name: GetLifetimeService
        nameWithType: MarshalByRefObject.GetLifetimeService
        qualifiedName: System.MarshalByRefObject.GetLifetimeService
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.MarshalByRefObject.GetLifetimeService
        name: GetLifetimeService
        nameWithType: MarshalByRefObject.GetLifetimeService
        qualifiedName: System.MarshalByRefObject.GetLifetimeService
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.MarshalByRefObject
    commentId: M:System.MarshalByRefObject.GetLifetimeService
  System.MarshalByRefObject.InitializeLifetimeService:
    name:
      CSharp:
      - id: System.MarshalByRefObject.InitializeLifetimeService
        name: InitializeLifetimeService
        nameWithType: MarshalByRefObject.InitializeLifetimeService
        qualifiedName: System.MarshalByRefObject.InitializeLifetimeService
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.MarshalByRefObject.InitializeLifetimeService
        name: InitializeLifetimeService
        nameWithType: MarshalByRefObject.InitializeLifetimeService
        qualifiedName: System.MarshalByRefObject.InitializeLifetimeService
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.MarshalByRefObject
    commentId: M:System.MarshalByRefObject.InitializeLifetimeService
  System.MarshalByRefObject.CreateObjRef(System.Type):
    name:
      CSharp:
      - id: System.MarshalByRefObject.CreateObjRef(System.Type)
        name: CreateObjRef
        nameWithType: MarshalByRefObject.CreateObjRef
        qualifiedName: System.MarshalByRefObject.CreateObjRef
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
      - id: System.MarshalByRefObject.CreateObjRef(System.Type)
        name: CreateObjRef
        nameWithType: MarshalByRefObject.CreateObjRef
        qualifiedName: System.MarshalByRefObject.CreateObjRef
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
    parent: System.MarshalByRefObject
    commentId: M:System.MarshalByRefObject.CreateObjRef(System.Type)
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
  WizardWrx.AssemblyUtils.AssemblyContainer.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.#ctor*
        name: AssemblyContainer
        nameWithType: AssemblyContainer.AssemblyContainer
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.AssemblyContainer
      VB:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.#ctor*
        name: AssemblyContainer
        nameWithType: AssemblyContainer.AssemblyContainer
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.AssemblyContainer
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.AssemblyContainer.#ctor
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
  System.Reflection.AssemblyName:
    name:
      CSharp:
      - id: System.Reflection.AssemblyName
        name: AssemblyName
        nameWithType: AssemblyName
        qualifiedName: System.Reflection.AssemblyName
        isExternal: true
      VB:
      - id: System.Reflection.AssemblyName
        name: AssemblyName
        nameWithType: AssemblyName
        qualifiedName: System.Reflection.AssemblyName
        isExternal: true
    isDefinition: true
    parent: System.Reflection
    commentId: T:System.Reflection.AssemblyName
  WizardWrx.AssemblyUtils.AssemblyContainer.Store*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.Store*
        name: Store
        nameWithType: AssemblyContainer.Store
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.Store
      VB:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.Store*
        name: Store
        nameWithType: AssemblyContainer.Store
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.Store
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.AssemblyContainer.Store
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
  WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe*
        name: ShowMe
        nameWithType: AssemblyContainer.ShowMe
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe
      VB:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe*
        name: ShowMe
        nameWithType: AssemblyContainer.ShowMe
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.AssemblyContainer.ShowMe
  WizardWrx.AssemblyUtils.AssemblyContainer:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer
        name: AssemblyContainer
        nameWithType: AssemblyContainer
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer
      VB:
      - id: WizardWrx.AssemblyUtils.AssemblyContainer
        name: AssemblyContainer
        nameWithType: AssemblyContainer
        qualifiedName: WizardWrx.AssemblyUtils.AssemblyContainer
    isDefinition: true
    commentId: T:WizardWrx.AssemblyUtils.AssemblyContainer
  WizardWrx.AssemblyUtils.DependentAssemblies.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor*
        name: DependentAssemblies
        nameWithType: DependentAssemblies.DependentAssemblies
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.#ctor*
        name: DependentAssemblies
        nameWithType: DependentAssemblies.DependentAssemblies
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblies
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.#ctor
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
  WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon*
        name: AssemblyDependsUpon
        nameWithType: DependentAssemblies.AssemblyDependsUpon
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon*
        name: AssemblyDependsUpon
        nameWithType: DependentAssemblies.AssemblyDependsUpon
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.AssemblyDependsUpon
  WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded*
        name: DependentAssemblyIsLoaded
        nameWithType: DependentAssemblies.DependentAssemblyIsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded*
        name: DependentAssemblyIsLoaded
        nameWithType: DependentAssemblies.DependentAssemblyIsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.DependentAssemblyIsLoaded
  WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents*
        name: DestroyDependents
        nameWithType: DependentAssemblies.DestroyDependents
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents*
        name: DestroyDependents
        nameWithType: DependentAssemblies.DestroyDependents
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.DestroyDependents
  System.IO:
    name:
      CSharp:
      - name: System.IO
        nameWithType: System.IO
        qualifiedName: System.IO
        isExternal: true
      VB:
      - name: System.IO
        nameWithType: System.IO
        qualifiedName: System.IO
    isDefinition: true
    commentId: N:System.IO
  System.IO.StreamWriter:
    name:
      CSharp:
      - id: System.IO.StreamWriter
        name: StreamWriter
        nameWithType: StreamWriter
        qualifiedName: System.IO.StreamWriter
        isExternal: true
      VB:
      - id: System.IO.StreamWriter
        name: StreamWriter
        nameWithType: StreamWriter
        qualifiedName: System.IO.StreamWriter
        isExternal: true
    isDefinition: true
    parent: System.IO
    commentId: T:System.IO.StreamWriter
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
  WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties*
        name: DisplayProperties
        nameWithType: DependentAssemblies.DisplayProperties
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties*
        name: DisplayProperties
        nameWithType: DependentAssemblies.DisplayProperties
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.DisplayProperties
  WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents*
        name: EnumerateDependents
        nameWithType: DependentAssemblies.EnumerateDependents
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents*
        name: EnumerateDependents
        nameWithType: DependentAssemblies.EnumerateDependents
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.EnumerateDependents
  System.Collections.Generic.List`1:
    name:
      CSharp:
      - id: System.Collections.Generic.List`1
        name: List
        nameWithType: List
        qualifiedName: System.Collections.Generic.List
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.Collections.Generic.List`1
        name: List
        nameWithType: List
        qualifiedName: System.Collections.Generic.List
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: T:System.Collections.Generic.List`1
  System.Collections.Generic:
    name:
      CSharp:
      - name: System.Collections.Generic
        nameWithType: System.Collections.Generic
        qualifiedName: System.Collections.Generic
        isExternal: true
      VB:
      - name: System.Collections.Generic
        nameWithType: System.Collections.Generic
        qualifiedName: System.Collections.Generic
    isDefinition: true
    commentId: N:System.Collections.Generic
  System.Collections.Generic.List{WizardWrx.AssemblyUtils.DependentAssemblyInfo}:
    name:
      CSharp:
      - id: System.Collections.Generic.List`1
        name: List
        nameWithType: List
        qualifiedName: System.Collections.Generic.List
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.Collections.Generic.List`1
        name: List
        nameWithType: List
        qualifiedName: System.Collections.Generic.List
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.Collections.Generic.List`1
    parent: System.Collections.Generic
    commentId: T:System.Collections.Generic.List{WizardWrx.AssemblyUtils.DependentAssemblyInfo}
  WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos*
        name: GetDependentAssemblyInfos
        nameWithType: DependentAssemblies.GetDependentAssemblyInfos
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos*
        name: GetDependentAssemblyInfos
        nameWithType: DependentAssemblies.GetDependentAssemblyInfos
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyInfos
  WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName*
        name: GetDependentAssemblyByName
        nameWithType: DependentAssemblies.GetDependentAssemblyByName
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName*
        name: GetDependentAssemblyByName
        nameWithType: DependentAssemblies.GetDependentAssemblyByName
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.GetDependentAssemblyByName
  WizardWrx.AssemblyUtils.DependentAssemblies.Root*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.Root*
        name: Root
        nameWithType: DependentAssemblies.Root
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.Root
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies.Root*
        name: Root
        nameWithType: DependentAssemblies.Root
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies.Root
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblies.Root
  WizardWrx.AssemblyUtils.DependentAssemblies:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies
        name: DependentAssemblies
        nameWithType: DependentAssemblies
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblies
        name: DependentAssemblies
        nameWithType: DependentAssemblies
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblies
    isDefinition: true
    commentId: T:WizardWrx.AssemblyUtils.DependentAssemblies
  System.IComparable`1:
    name:
      CSharp:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: T:System.IComparable`1
  System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}:
    name:
      CSharp:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.IComparable`1
    parent: System
    commentId: T:System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor*
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo.DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DependentAssemblyInfo
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor*
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo.DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DependentAssemblyInfo
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.#ctor
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains*
        name: DestroyOwneAppdDomains
        nameWithType: DependentAssemblyInfo.DestroyOwneAppdDomains
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains*
        name: DestroyOwneAppdDomains
        nameWithType: DependentAssemblyInfo.DestroyOwneAppdDomains
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.DestroyOwneAppdDomains
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection*
        name: LoadForInspection
        nameWithType: DependentAssemblyInfo.LoadForInspection
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection*
        name: LoadForInspection
        nameWithType: DependentAssemblyInfo.LoadForInspection
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.LoadForInspection
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded*
        name: MarkAsLoaded
        nameWithType: DependentAssemblyInfo.MarkAsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded*
        name: MarkAsLoaded
        nameWithType: DependentAssemblyInfo.MarkAsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.MarkAsLoaded
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals*
        name: Equals
        nameWithType: DependentAssemblyInfo.Equals
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals*
        name: Equals
        nameWithType: DependentAssemblyInfo.Equals
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.Equals
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
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode*
        name: GetHashCode
        nameWithType: DependentAssemblyInfo.GetHashCode
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode*
        name: GetHashCode
        nameWithType: DependentAssemblyInfo.GetHashCode
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.GetHashCode
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
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString*
        name: ToString
        nameWithType: DependentAssemblyInfo.ToString
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString*
        name: ToString
        nameWithType: DependentAssemblyInfo.ToString
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.ToString
  WizardWrx.AssemblyUtils:
    name:
      CSharp:
      - name: WizardWrx.AssemblyUtils
        nameWithType: WizardWrx.AssemblyUtils
        qualifiedName: WizardWrx.AssemblyUtils
      VB:
      - name: WizardWrx.AssemblyUtils
        nameWithType: WizardWrx.AssemblyUtils
        qualifiedName: WizardWrx.AssemblyUtils
    isDefinition: true
    commentId: N:WizardWrx.AssemblyUtils
  WizardWrx.AssemblyUtils.DependentAssemblyInfo:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
    isDefinition: true
    parent: WizardWrx.AssemblyUtils
    commentId: T:WizardWrx.AssemblyUtils.DependentAssemblyInfo
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo*
        name: IComparable<DependentAssemblyInfo>.CompareTo
        nameWithType: DependentAssemblyInfo.IComparable<DependentAssemblyInfo>.CompareTo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo*
        name: System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
        nameWithType: DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.System#IComparable{WizardWrx#AssemblyUtils#DependentAssemblyInfo}#CompareTo
  System.IComparable`1.CompareTo(`0):
    name:
      CSharp:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable<T>.CompareTo
        qualifiedName: System.IComparable<T>.CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable(Of T).CompareTo
        qualifiedName: System.IComparable(Of T).CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: M:System.IComparable`1.CompareTo(`0)
  System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}.CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo):
    name:
      CSharp:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable<DependentAssemblyInfo>.CompareTo
        qualifiedName: System.IComparable<WizardWrx.AssemblyUtils.DependentAssemblyInfo>.CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable(Of DependentAssemblyInfo).CompareTo
        qualifiedName: System.IComparable(Of WizardWrx.AssemblyUtils.DependentAssemblyInfo).CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo
        name: DependentAssemblyInfo
        nameWithType: DependentAssemblyInfo
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.IComparable`1.CompareTo(`0)
    parent: System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}
    commentId: M:System.IComparable{WizardWrx.AssemblyUtils.DependentAssemblyInfo}.CompareTo(WizardWrx.AssemblyUtils.DependentAssemblyInfo)
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails*
        name: AssemblyDetails
        nameWithType: DependentAssemblyInfo.AssemblyDetails
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails*
        name: AssemblyDetails
        nameWithType: DependentAssemblyInfo.AssemblyDetails
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.AssemblyDetails
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName*
        name: FullName
        nameWithType: DependentAssemblyInfo.FullName
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName*
        name: FullName
        nameWithType: DependentAssemblyInfo.FullName
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.FullName
  WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded*
        name: IsLoaded
        nameWithType: DependentAssemblyInfo.IsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
      VB:
      - id: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded*
        name: IsLoaded
        nameWithType: DependentAssemblyInfo.IsLoaded
        qualifiedName: WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.DependentAssemblyInfo.IsLoaded
  WizardWrx.GenericSingletonBase`1:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1
        name: GenericSingletonBase
        nameWithType: GenericSingletonBase
        qualifiedName: WizardWrx.GenericSingletonBase
      - name: <
        nameWithType: <
        qualifiedName: <
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: WizardWrx.GenericSingletonBase`1
        name: GenericSingletonBase
        nameWithType: GenericSingletonBase
        qualifiedName: WizardWrx.GenericSingletonBase
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - name: T
        nameWithType: T
        qualifiedName: T
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: T:WizardWrx.GenericSingletonBase`1
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
  WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1
        name: GenericSingletonBase
        nameWithType: GenericSingletonBase
        qualifiedName: WizardWrx.GenericSingletonBase
      - name: <
        nameWithType: <
        qualifiedName: <
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo
        name: PESubsystemInfo
        nameWithType: PESubsystemInfo
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: WizardWrx.GenericSingletonBase`1
        name: GenericSingletonBase
        nameWithType: GenericSingletonBase
        qualifiedName: WizardWrx.GenericSingletonBase
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo
        name: PESubsystemInfo
        nameWithType: PESubsystemInfo
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.GenericSingletonBase`1
    parent: WizardWrx
    commentId: T:WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}
  WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
        name: s_genTheOnlyInstance
        nameWithType: GenericSingletonBase<T>.s_genTheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase<T>.s_genTheOnlyInstance
      VB:
      - id: WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
        name: s_genTheOnlyInstance
        nameWithType: GenericSingletonBase(Of T).s_genTheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of T).s_genTheOnlyInstance
    isDefinition: true
    commentId: F:WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
  WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.s_genTheOnlyInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
        name: s_genTheOnlyInstance
        nameWithType: GenericSingletonBase<PESubsystemInfo>.s_genTheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase<WizardWrx.AssemblyUtils.PESubsystemInfo>.s_genTheOnlyInstance
      VB:
      - id: WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
        name: s_genTheOnlyInstance
        nameWithType: GenericSingletonBase(Of PESubsystemInfo).s_genTheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of WizardWrx.AssemblyUtils.PESubsystemInfo).s_genTheOnlyInstance
    isDefinition: false
    definition: WizardWrx.GenericSingletonBase`1.s_genTheOnlyInstance
    parent: WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}
    commentId: F:WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.s_genTheOnlyInstance
  WizardWrx.GenericSingletonBase`1.TheOnlyInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.TheOnlyInstance
        name: TheOnlyInstance
        nameWithType: GenericSingletonBase<T>.TheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase<T>.TheOnlyInstance
      VB:
      - id: WizardWrx.GenericSingletonBase`1.TheOnlyInstance
        name: TheOnlyInstance
        nameWithType: GenericSingletonBase(Of T).TheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of T).TheOnlyInstance
    isDefinition: true
    commentId: P:WizardWrx.GenericSingletonBase`1.TheOnlyInstance
  WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.TheOnlyInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.TheOnlyInstance
        name: TheOnlyInstance
        nameWithType: GenericSingletonBase<PESubsystemInfo>.TheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase<WizardWrx.AssemblyUtils.PESubsystemInfo>.TheOnlyInstance
      VB:
      - id: WizardWrx.GenericSingletonBase`1.TheOnlyInstance
        name: TheOnlyInstance
        nameWithType: GenericSingletonBase(Of PESubsystemInfo).TheOnlyInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of WizardWrx.AssemblyUtils.PESubsystemInfo).TheOnlyInstance
    isDefinition: false
    definition: WizardWrx.GenericSingletonBase`1.TheOnlyInstance
    parent: WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}
    commentId: P:WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.TheOnlyInstance
  WizardWrx.GenericSingletonBase`1.GetTheSingleInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
        name: GetTheSingleInstance
        nameWithType: GenericSingletonBase<T>.GetTheSingleInstance
        qualifiedName: WizardWrx.GenericSingletonBase<T>.GetTheSingleInstance
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
        name: GetTheSingleInstance
        nameWithType: GenericSingletonBase(Of T).GetTheSingleInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of T).GetTheSingleInstance
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: M:WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
  WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.GetTheSingleInstance:
    name:
      CSharp:
      - id: WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
        name: GetTheSingleInstance
        nameWithType: GenericSingletonBase<PESubsystemInfo>.GetTheSingleInstance
        qualifiedName: WizardWrx.GenericSingletonBase<WizardWrx.AssemblyUtils.PESubsystemInfo>.GetTheSingleInstance
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
        name: GetTheSingleInstance
        nameWithType: GenericSingletonBase(Of PESubsystemInfo).GetTheSingleInstance
        qualifiedName: WizardWrx.GenericSingletonBase(Of WizardWrx.AssemblyUtils.PESubsystemInfo).GetTheSingleInstance
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: WizardWrx.GenericSingletonBase`1.GetTheSingleInstance
    parent: WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}
    commentId: M:WizardWrx.GenericSingletonBase{WizardWrx.AssemblyUtils.PESubsystemInfo}.GetTheSingleInstance
  System.Int16:
    name:
      CSharp:
      - id: System.Int16
        name: Int16
        nameWithType: Int16
        qualifiedName: System.Int16
        isExternal: true
      VB:
      - id: System.Int16
        name: Int16
        nameWithType: Int16
        qualifiedName: System.Int16
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Int16
  WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem:
    commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
  WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID*
        name: DefaultAppDomainSubsystemID
        nameWithType: PESubsystemInfo.DefaultAppDomainSubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID*
        name: DefaultAppDomainSubsystemID
        nameWithType: PESubsystemInfo.DefaultAppDomainSubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
  WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID:
    commentId: P:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystemID
  WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
        name: PESubsystemInfo.PESubsystemID
        nameWithType: PESubsystemInfo.PESubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
        name: PESubsystemInfo.PESubsystemID
        nameWithType: PESubsystemInfo.PESubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
    isDefinition: true
    parent: WizardWrx.AssemblyUtils
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID
  WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem*
        name: DefaultAppDomainSubsystem
        nameWithType: PESubsystemInfo.DefaultAppDomainSubsystem
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem*
        name: DefaultAppDomainSubsystem
        nameWithType: PESubsystemInfo.DefaultAppDomainSubsystem
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultAppDomainSubsystem
  WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation*
        name: DomainEntryAssemblyLocation
        nameWithType: PESubsystemInfo.DomainEntryAssemblyLocation
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation*
        name: DomainEntryAssemblyLocation
        nameWithType: PESubsystemInfo.DomainEntryAssemblyLocation
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.DomainEntryAssemblyLocation
  WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName*
        name: DefaultDomainEntryAssemblyName
        nameWithType: PESubsystemInfo.DefaultDomainEntryAssemblyName
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName*
        name: DefaultDomainEntryAssemblyName
        nameWithType: PESubsystemInfo.DefaultDomainEntryAssemblyName
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.DefaultDomainEntryAssemblyName
  ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
  : commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(System.Int16,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
  ? WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
  : commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription(WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID,WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
  WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String):
    commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem(System.String)
  System.ArgumentException:
    commentId: T:System.ArgumentException
  System.Exception:
    commentId: T:System.Exception
  WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID*
        name: GetPESubsystemID
        nameWithType: PESubsystemInfo.GetPESubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID*
        name: GetPESubsystemID
        nameWithType: PESubsystemInfo.GetPESubsystemID
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID
  WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String):
    commentId: M:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemID(System.String)
  WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem*
        name: GetPESubsystem
        nameWithType: PESubsystemInfo.GetPESubsystem
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem*
        name: GetPESubsystem
        nameWithType: PESubsystemInfo.GetPESubsystem
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystem
  WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
        name: PESubsystemInfo.SubsystemDescription
        nameWithType: PESubsystemInfo.SubsystemDescription
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
        name: PESubsystemInfo.SubsystemDescription
        nameWithType: PESubsystemInfo.SubsystemDescription
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
    isDefinition: true
    parent: WizardWrx.AssemblyUtils
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription
  System.ArgumentOutOfRangeException:
    commentId: T:System.ArgumentOutOfRangeException
  System.ComponentModel.InvalidEnumArgumentException:
    commentId: T:System.ComponentModel.InvalidEnumArgumentException
  WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription*
        name: GetPESubsystemDescription
        nameWithType: PESubsystemInfo.GetPESubsystemDescription
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription*
        name: GetPESubsystemDescription
        nameWithType: PESubsystemInfo.GetPESubsystemDescription
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.GetPESubsystemDescription
  WizardWrx.AssemblyUtils.PESubsystemInfo.ToString*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString*
        name: ToString
        nameWithType: PESubsystemInfo.ToString
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString*
        name: ToString
        nameWithType: PESubsystemInfo.ToString
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.PESubsystemInfo.ToString
  WizardWrx.AssemblyUtils.PESubsystemInfo:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo
        name: PESubsystemInfo
        nameWithType: PESubsystemInfo
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo
      VB:
      - id: WizardWrx.AssemblyUtils.PESubsystemInfo
        name: PESubsystemInfo
        nameWithType: PESubsystemInfo
        qualifiedName: WizardWrx.AssemblyUtils.PESubsystemInfo
    isDefinition: true
    commentId: T:WizardWrx.AssemblyUtils.PESubsystemInfo
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
  ? WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<PESubsystemInfo.PESubsystemID>
        nameWithType: StringExtensions.RenderEvenWhenNull<PESubsystemInfo.PESubsystemID>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID>
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
        name: RenderEvenWhenNull(Of PESubsystemInfo.PESubsystemID)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of PESubsystemInfo.PESubsystemID)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.AssemblyUtils.PESubsystemInfo.PESubsystemID)
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
  ? WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<PESubsystemInfo.SubsystemDescription>
        nameWithType: StringExtensions.RenderEvenWhenNull<PESubsystemInfo.SubsystemDescription>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription>
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
        name: RenderEvenWhenNull(Of PESubsystemInfo.SubsystemDescription)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of PESubsystemInfo.SubsystemDescription)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.AssemblyUtils.PESubsystemInfo.SubsystemDescription)
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
  WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString*
        name: GetAssemblyGuidString
        nameWithType: ReportGenerators.GetAssemblyGuidString
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString
      VB:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString*
        name: GetAssemblyGuidString
        nameWithType: ReportGenerators.GetAssemblyGuidString
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.ReportGenerators.GetAssemblyGuidString
  WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties*
        name: LabelKeyAssemblyProperties
        nameWithType: ReportGenerators.LabelKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties
      VB:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties*
        name: LabelKeyAssemblyProperties
        nameWithType: ReportGenerators.LabelKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.ReportGenerators.LabelKeyAssemblyProperties
  WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties*
        name: ListKeyAssemblyProperties
        nameWithType: ReportGenerators.ListKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties
      VB:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties*
        name: ListKeyAssemblyProperties
        nameWithType: ReportGenerators.ListKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.ReportGenerators.ListKeyAssemblyProperties
  WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties*
        name: ShowKeyAssemblyProperties
        nameWithType: ReportGenerators.ShowKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties
      VB:
      - id: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties*
        name: ShowKeyAssemblyProperties
        nameWithType: ReportGenerators.ShowKeyAssemblyProperties
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.ReportGenerators.ShowKeyAssemblyProperties
  WizardWrx.AssemblyUtils.ReportGenerators:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.ReportGenerators
        name: ReportGenerators
        nameWithType: ReportGenerators
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators
      VB:
      - id: WizardWrx.AssemblyUtils.ReportGenerators
        name: ReportGenerators
        nameWithType: ReportGenerators
        qualifiedName: WizardWrx.AssemblyUtils.ReportGenerators
    isDefinition: true
    commentId: T:WizardWrx.AssemblyUtils.ReportGenerators
  System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}:
    name:
      CSharp:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.IComparable`1
        name: IComparable
        nameWithType: IComparable
        qualifiedName: System.IComparable
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.IComparable`1
    parent: System
    commentId: T:System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor*
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem.SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor*
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem.SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.SortableManagedResourceItem
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.#ctor
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name*
        name: Name
        nameWithType: SortableManagedResourceItem.Name
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name*
        name: Name
        nameWithType: SortableManagedResourceItem.Name
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.Name
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value*
        name: Value
        nameWithType: SortableManagedResourceItem.Value
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value*
        name: Value
        nameWithType: SortableManagedResourceItem.Value
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.Value
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName*
        name: TypeName
        nameWithType: SortableManagedResourceItem.TypeName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName*
        name: TypeName
        nameWithType: SortableManagedResourceItem.TypeName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.TypeName
  WizardWrx.AssemblyUtils.SortableManagedResourceItem:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
    isDefinition: true
    parent: WizardWrx.AssemblyUtils
    commentId: T:WizardWrx.AssemblyUtils.SortableManagedResourceItem
  ? WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo*
  : name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo*
        name: IComparable<SortableManagedResourceItem>.CompareTo
        nameWithType: SortableManagedResourceItem.IComparable<SortableManagedResourceItem>.CompareTo
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo*
        name: System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
        nameWithType: SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.System#IComparable{WizardWrx#AssemblyUtils#SortableManagedResourceItem}#CompareTo
  ? System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}.CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
  : name:
      CSharp:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable<SortableManagedResourceItem>.CompareTo
        qualifiedName: System.IComparable<WizardWrx.AssemblyUtils.SortableManagedResourceItem>.CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.IComparable`1.CompareTo(`0)
        name: CompareTo
        nameWithType: IComparable(Of SortableManagedResourceItem).CompareTo
        qualifiedName: System.IComparable(Of WizardWrx.AssemblyUtils.SortableManagedResourceItem).CompareTo
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem
        name: SortableManagedResourceItem
        nameWithType: SortableManagedResourceItem
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.IComparable`1.CompareTo(`0)
    parent: System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}
    commentId: M:System.IComparable{WizardWrx.AssemblyUtils.SortableManagedResourceItem}.CompareTo(WizardWrx.AssemblyUtils.SortableManagedResourceItem)
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName*
        name: GetInternalResourceName
        nameWithType: SortableManagedResourceItem.GetInternalResourceName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName*
        name: GetInternalResourceName
        nameWithType: SortableManagedResourceItem.GetInternalResourceName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.GetInternalResourceName
  WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName*:
    name:
      CSharp:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName*
        name: ListResourcesInAssemblyByName
        nameWithType: SortableManagedResourceItem.ListResourcesInAssemblyByName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName
      VB:
      - id: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName*
        name: ListResourcesInAssemblyByName
        nameWithType: SortableManagedResourceItem.ListResourcesInAssemblyByName
        qualifiedName: WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName
    isDefinition: true
    commentId: Overload:WizardWrx.AssemblyUtils.SortableManagedResourceItem.ListResourcesInAssemblyByName
