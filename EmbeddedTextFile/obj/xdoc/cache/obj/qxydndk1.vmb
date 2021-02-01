id: WizardWrx.EmbeddedTextFile
language: CSharp
name:
  Default: WizardWrx.EmbeddedTextFile
qualifiedName:
  Default: WizardWrx.EmbeddedTextFile
type: Assembly
modifiers: {}
items:
- id: WizardWrx.EmbeddedTextFile
  commentId: N:WizardWrx.EmbeddedTextFile
  language: CSharp
  name:
    CSharp: WizardWrx.EmbeddedTextFile
    VB: WizardWrx.EmbeddedTextFile
  nameWithType:
    CSharp: WizardWrx.EmbeddedTextFile
    VB: WizardWrx.EmbeddedTextFile
  qualifiedName:
    CSharp: WizardWrx.EmbeddedTextFile
    VB: WizardWrx.EmbeddedTextFile
  type: Namespace
  assemblies:
  - WizardWrx.EmbeddedTextFile
  modifiers: {}
  items:
  - id: WizardWrx.EmbeddedTextFile.ByteOrderMark
    commentId: T:WizardWrx.EmbeddedTextFile.ByteOrderMark
    language: CSharp
    name:
      CSharp: ByteOrderMark
      VB: ByteOrderMark
    nameWithType:
      CSharp: ByteOrderMark
      VB: ByteOrderMark
    qualifiedName:
      CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark
      VB: WizardWrx.EmbeddedTextFile.ByteOrderMark
    type: Class
    assemblies:
    - WizardWrx.EmbeddedTextFile
    namespace: WizardWrx.EmbeddedTextFile
    source:
      remote:
        path: EmbeddedTextFile/ByteOrderMark.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ByteOrderMark
      path: ../EmbeddedTextFile/ByteOrderMark.cs
      startLine: 79
    summary: "\nUse this class to evaluate arbitrary byte arrays for the presence of a Byte Order Mark.\n"
    example: []
    syntax:
      content:
        CSharp: public class ByteOrderMark
        VB: Public Class ByteOrderMark
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
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor(System.Byte[])
      commentId: M:WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor(System.Byte[])
      language: CSharp
      name:
        CSharp: ByteOrderMark(Byte[])
        VB: ByteOrderMark(Byte())
      nameWithType:
        CSharp: ByteOrderMark.ByteOrderMark(Byte[])
        VB: ByteOrderMark.ByteOrderMark(Byte())
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.ByteOrderMark(System.Byte[])
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.ByteOrderMark(System.Byte())
      type: Constructor
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 323
      summary: "\nThe only public constructor demands a reference to the byte array to evaluate.\n"
      example: []
      syntax:
        content:
          CSharp: public ByteOrderMark(byte[] bytes)
          VB: Public Sub New(bytes As Byte())
        parameters:
        - id: bytes
          type: System.Byte[]
          description: "\nSupply a reference to the byte array to test for a Byte Order Mark.\n"
      overload: WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
      commentId: P:WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
      language: CSharp
      name:
        CSharp: Kind
        VB: Kind
      nameWithType:
        CSharp: ByteOrderMark.Kind
        VB: ByteOrderMark.Kind
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
      type: Property
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Kind
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 335
      summary: "\nThis read-only property returns the type of Byte Order Mark present\nin the byte array that was supplied to the constructor.\n"
      example: []
      syntax:
        content:
          CSharp: public ByteOrderMark.BOMType Kind { get; }
          VB: Public ReadOnly Property Kind As ByteOrderMark.BOMType
        parameters: []
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      overload: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
      commentId: P:WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
      language: CSharp
      name:
        CSharp: Length
        VB: Length
      nameWithType:
        CSharp: ByteOrderMark.Length
        VB: ByteOrderMark.Length
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
      type: Property
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Length
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 354
      summary: "\nThis read-only property returns the length of the Byte Order Mark.\n"
      remarks: "\nSince the property is initialized on first use, it may display an\ninvalid value of -1 in a watch window.\n"
      example: []
      syntax:
        content:
          CSharp: public int Length { get; }
          VB: Public ReadOnly Property Length As Integer
        parameters: []
        return:
          type: System.Int32
      overload: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM
      commentId: M:WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM
      language: CSharp
      name:
        CSharp: TestForBOM()
        VB: TestForBOM()
      nameWithType:
        CSharp: ByteOrderMark.TestForBOM()
        VB: ByteOrderMark.TestForBOM()
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM()
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM()
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: TestForBOM
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 374
      summary: "\nIf it hasn&apos;t been directly called, the first call to either property\ngetter calls this method, so that the work required to evaluate the\narray for a byte order mark is deferred until it is needed, and it\nis never subsequently repeated for the lifetime of the instance.\n"
      example: []
      syntax:
        content:
          CSharp: public void TestForBOM()
          VB: Public Sub TestForBOM
      overload: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
  - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
    commentId: T:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
    language: CSharp
    name:
      CSharp: ByteOrderMark.BOMType
      VB: ByteOrderMark.BOMType
    nameWithType:
      CSharp: ByteOrderMark.BOMType
      VB: ByteOrderMark.BOMType
    qualifiedName:
      CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
    type: Enum
    assemblies:
    - WizardWrx.EmbeddedTextFile
    namespace: WizardWrx.EmbeddedTextFile
    source:
      remote:
        path: EmbeddedTextFile/ByteOrderMark.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: BOMType
      path: ../EmbeddedTextFile/ByteOrderMark.cs
      startLine: 86
    summary: "\nMembers of this enumeration report the type of byte order mark, if\nany, present at its beginning.\n"
    example: []
    syntax:
      content:
        CSharp: public enum BOMType
        VB: Public Enum BOMType
    extensionMethods:
    - WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    - WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.None
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.None
      language: CSharp
      name:
        CSharp: None
        VB: None
      nameWithType:
        CSharp: ByteOrderMark.BOMType.None
        VB: ByteOrderMark.BOMType.None
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.None
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.None
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: None
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 91
      summary: "\nThe array has no Byte Order Mark.\n"
      example: []
      syntax:
        content:
          CSharp: None = 0
          VB: None = 0
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF8
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF8
      language: CSharp
      name:
        CSharp: UTF8
        VB: UTF8
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UTF8
        VB: ByteOrderMark.BOMType.UTF8
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF8
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF8
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UTF8
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 96
      summary: "\nThe array has a UTF-8 Byte Order Mark.\n"
      example: []
      syntax:
        content:
          CSharp: UTF8 = 1
          VB: UTF8 = 1
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16LE
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16LE
      language: CSharp
      name:
        CSharp: UTF16LE
        VB: UTF16LE
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UTF16LE
        VB: ByteOrderMark.BOMType.UTF16LE
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16LE
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16LE
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UTF16LE
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 106
      summary: "\nThe array has a UTF-16 Byte Order Mark that indicates \nlittle-endian character encoding.\n"
      remarks: "\nThe little-endian encoding is the native encoding of the entire\nIntel processor family and its clones (e. g., AMD).\n"
      example: []
      syntax:
        content:
          CSharp: UTF16LE = 2
          VB: UTF16LE = 2
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16BE
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16BE
      language: CSharp
      name:
        CSharp: UTF16BE
        VB: UTF16BE
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UTF16BE
        VB: ByteOrderMark.BOMType.UTF16BE
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16BE
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF16BE
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UTF16BE
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 116
      summary: "\nThe array has a UTF-16 Byte Order Mark that indicates big-endian\ncharacter encoding.\n"
      remarks: "\nThe big-endian encoding is the native encoding of the MIPS Alpha\nfamily of processors, among others.\n"
      example: []
      syntax:
        content:
          CSharp: UTF16BE = 3
          VB: UTF16BE = 3
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeLittleEndian
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeLittleEndian
      language: CSharp
      name:
        CSharp: UnicodeLittleEndian
        VB: UnicodeLittleEndian
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UnicodeLittleEndian
        VB: ByteOrderMark.BOMType.UnicodeLittleEndian
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeLittleEndian
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeLittleEndian
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UnicodeLittleEndian
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 122
      summary: "\nUnicodeLittleEndian is a synonym of UTF16LE, the encoding that\ncorresponds to the wide character encoding on Microsoft Windows.\n"
      example: []
      syntax:
        content:
          CSharp: UnicodeLittleEndian = 2
          VB: UnicodeLittleEndian = 2
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeBigEndian
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeBigEndian
      language: CSharp
      name:
        CSharp: UnicodeBigEndian
        VB: UnicodeBigEndian
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UnicodeBigEndian
        VB: ByteOrderMark.BOMType.UnicodeBigEndian
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeBigEndian
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UnicodeBigEndian
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UnicodeBigEndian
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 130
      summary: "\nUnicodeLittleEndian is a synonym of UTF16BE, the encoding that\ncorresponds to the wide character encoding on Microsoft Windows,\nrunning on a big-endian processor, which MAY include code that\nruns on ARM processors.\n"
      example: []
      syntax:
        content:
          CSharp: UnicodeBigEndian = 3
          VB: UnicodeBigEndian = 3
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32LE
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32LE
      language: CSharp
      name:
        CSharp: UTF32LE
        VB: UTF32LE
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UTF32LE
        VB: ByteOrderMark.BOMType.UTF32LE
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32LE
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32LE
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UTF32LE
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 140
      summary: "\nThe array has a UTF-32 Byte Order Mark that indicates \nlittle-endian character encoding.\n"
      remarks: "\nThis seldom-used encoding is defined for completeness, and in\nanticipation of its usage increasing.\n"
      example: []
      syntax:
        content:
          CSharp: UTF32LE = 4
          VB: UTF32LE = 4
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32BE
      commentId: F:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32BE
      language: CSharp
      name:
        CSharp: UTF32BE
        VB: UTF32BE
      nameWithType:
        CSharp: ByteOrderMark.BOMType.UTF32BE
        VB: ByteOrderMark.BOMType.UTF32BE
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32BE
        VB: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.UTF32BE
      type: Field
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/ByteOrderMark.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UTF32BE
        path: ../EmbeddedTextFile/ByteOrderMark.cs
        startLine: 150
      summary: "\nThe array has a UTF-32 Byte Order Mark that indicates big-endian\ncharacter encoding.\n"
      remarks: "\nThis seldom-used encoding is defined for completeness, and in\nanticipation of its usage increasing.\n"
      example: []
      syntax:
        content:
          CSharp: UTF32BE = 5
          VB: UTF32BE = 5
        return:
          type: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.EmbeddedTextFile.Readers
    commentId: T:WizardWrx.EmbeddedTextFile.Readers
    language: CSharp
    name:
      CSharp: Readers
      VB: Readers
    nameWithType:
      CSharp: Readers
      VB: Readers
    qualifiedName:
      CSharp: WizardWrx.EmbeddedTextFile.Readers
      VB: WizardWrx.EmbeddedTextFile.Readers
    type: Class
    assemblies:
    - WizardWrx.EmbeddedTextFile
    namespace: WizardWrx.EmbeddedTextFile
    source:
      remote:
        path: EmbeddedTextFile/Readers.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: Readers
      path: ../EmbeddedTextFile/Readers.cs
      startLine: 91
    summary: "\nThis static class exposes methods for loading text from custom resources\nthat are embedded in an assembly.\n"
    example: []
    syntax:
      content:
        CSharp: public static class Readers
        VB: Public Module Readers
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
    - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
      language: CSharp
      name:
        CSharp: LoadTextFileFromCallingAssembly(String)
        VB: LoadTextFileFromCallingAssembly(String)
      nameWithType:
        CSharp: Readers.LoadTextFileFromCallingAssembly(String)
        VB: Readers.LoadTextFileFromCallingAssembly(String)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
        VB: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LoadTextFileFromCallingAssembly
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 107
      summary: "\nLoad the lines of a plain ASCII text file that has been stored with\nthe assembly as a embedded resource into an array of native strings.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] LoadTextFileFromCallingAssembly(string pstrResourceName)
          VB: Public Shared Function LoadTextFileFromCallingAssembly(pstrResourceName As String) As String()
        parameters:
        - id: pstrResourceName
          type: System.String
          description: "\nSpecify the absolute (fully qualified) resource name, which is its\nsource file name appended to the default assembly namespace.\n"
        return:
          type: System.String[]
          description: "\nThe return value is an array of Unicode strings, each of which is\nthe text of a line from the original text file, sans terminator.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly*
      see:
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
      seealso:
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly): 
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String): 
    - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
      language: CSharp
      name:
        CSharp: LoadTextFileFromEntryAssembly(String)
        VB: LoadTextFileFromEntryAssembly(String)
      nameWithType:
        CSharp: Readers.LoadTextFileFromEntryAssembly(String)
        VB: Readers.LoadTextFileFromEntryAssembly(String)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
        VB: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LoadTextFileFromEntryAssembly
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 130
      summary: "\nLoad the lines of a plain ASCII text file that has been stored with\nthe assembly as a embedded resource into an array of native strings.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] LoadTextFileFromEntryAssembly(string pstrResourceName)
          VB: Public Shared Function LoadTextFileFromEntryAssembly(pstrResourceName As String) As String()
        parameters:
        - id: pstrResourceName
          type: System.String
          description: "\nSpecify the fully qualified resource name, which is its source file\nname appended to the default application namespace.\n"
        return:
          type: System.String[]
          description: "\nThe return value is an array of Unicode strings, each of which is\nthe text of a line from the original text file, sans terminator.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly*
      see:
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
      seealso:
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly): 
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String): 
    - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: LoadTextFileFromAnyAssembly(String, Assembly)
        VB: LoadTextFileFromAnyAssembly(String, Assembly)
      nameWithType:
        CSharp: Readers.LoadTextFileFromAnyAssembly(String, Assembly)
        VB: Readers.LoadTextFileFromAnyAssembly(String, Assembly)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String, System.Reflection.Assembly)
        VB: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String, System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LoadTextFileFromAnyAssembly
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 156
      summary: "\nLoad the lines of a plain ASCII text file that has been stored with\na specified assembly as a embedded resource into an array of native\nstrings.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] LoadTextFileFromAnyAssembly(string pstrResourceName, Assembly pasmSource)
          VB: Public Shared Function LoadTextFileFromAnyAssembly(pstrResourceName As String, pasmSource As Assembly) As String()
        parameters:
        - id: pstrResourceName
          type: System.String
          description: "\nSpecify the absolute (fully qualified) resource name, which is its\nsource file name appended to the default assembly namespace name.\n"
        - id: pasmSource
          type: System.Reflection.Assembly
          description: "\nPass in a reference to the Assembly from which you expect to load\nthe text file. Use any means at your disposal to obtain a reference\nfrom the System.Reflection namespace.\n"
        return:
          type: System.String[]
          description: ''
      overload: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly*
      seealso:
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
      - linkId: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
        commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String): 
        WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String): 
    - id: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly(System.String,System.Reflection.Assembly)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly(System.String,System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: LoadBinaryResourceFromAnyAssembly(String, Assembly)
        VB: LoadBinaryResourceFromAnyAssembly(String, Assembly)
      nameWithType:
        CSharp: Readers.LoadBinaryResourceFromAnyAssembly(String, Assembly)
        VB: Readers.LoadBinaryResourceFromAnyAssembly(String, Assembly)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly(System.String, System.Reflection.Assembly)
        VB: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly(System.String, System.Reflection.Assembly)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LoadBinaryResourceFromAnyAssembly
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 226
      summary: "\nLoad the named embedded binary resource into a byte array.\n"
      remarks: "\nSince all other resource types ultimately come out as byte arrays,\nthe text file loaders call upon this routine to extract their data.\n\nThe notes in the cited reference refreshed my memory of observations\nthat I made and documented a couple of weeks ago. However, it was a\nlot easier to let Google find a reference document, which was\nintended for students in the Computer Science department at Columbia\nUniversity, at http://www1.cs.columbia.edu/~lok/csharp/refdocs/System.IO/types/Stream.html&quot;/>,\nthan find my own source.\n"
      example: []
      syntax:
        content:
          CSharp: public static byte[] LoadBinaryResourceFromAnyAssembly(string pstrResourceName, Assembly pasmSource)
          VB: Public Shared Function LoadBinaryResourceFromAnyAssembly(pstrResourceName As String, pasmSource As Assembly) As Byte()
        parameters:
        - id: pstrResourceName
          type: System.String
          description: "\nSpecify the external name of the file as it appears in the source\nfile tree and the Solution Explorer.\n"
        - id: pasmSource
          type: System.Reflection.Assembly
          description: "\nSupply a System.Reflection.Assembly reference to the assembly that\ncontains the embedded resource.\n"
        return:
          type: System.Byte[]
          description: "\nIf the function succeeds, it returns a byte array containing the raw\nbytes that comprise the embedded resource. Hence, this method can\nload ANY embedded resource.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly*
      exceptions:
      - type: System.Exception
        commentId: T:System.Exception
        description: "\nAn Exception (the base Exception type) arises when the method is\ncalled with a <code data-dev-comment-type=\"paramref\" class=\"paramref\">pstrResourceName</code> value that cannot\nbe found in the <code data-dev-comment-type=\"paramref\" class=\"paramref\">pasmSource</code> assembly. When the\nexception arises during the read operation, the generic Exception\nwraps an InvalidDataException exception, which is returned as its\nInnnerException property.\n"
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
      references:
        System.Exception: 
    - id: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray(System.Byte[])
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray(System.Byte[])
      language: CSharp
      name:
        CSharp: StringFromANSICharacterArray(Byte[])
        VB: StringFromANSICharacterArray(Byte())
      nameWithType:
        CSharp: Readers.StringFromANSICharacterArray(Byte[])
        VB: Readers.StringFromANSICharacterArray(Byte())
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray(System.Byte[])
        VB: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray(System.Byte())
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StringFromANSICharacterArray
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 371
      summary: "\nTransform an array of bytes, each representing one ANSI character, into a string.\n"
      remarks: "\nI did this refactoring, thinking that I had a new use for the code,\nonly to realize as I finished cleaning it up that I can&apos;t use it,\nbecause it deals in ANSI characters, and my present need involves\nUnicode characters. Nevertheless, the exercise is not a total loss,\nbecause it reminded me of the trick that I needed to transform the\narray of Unicode characters into a string.\n"
      example: []
      syntax:
        content:
          CSharp: public static string StringFromANSICharacterArray(byte[] pabytWholeFile)
          VB: Public Shared Function StringFromANSICharacterArray(pabytWholeFile As Byte()) As String
        parameters:
        - id: pabytWholeFile
          type: System.Byte[]
          description: "\nSpecify the array to transform.\n"
        return:
          type: System.String
          description: "\nThe <code data-dev-comment-type=\"paramref\" class=\"paramref\">pabytWholeFile</code> array is returned as a string.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String)
      language: CSharp
      name:
        CSharp: StringOfLinesToArray(String)
        VB: StringOfLinesToArray(String)
      nameWithType:
        CSharp: Readers.StringOfLinesToArray(String)
        VB: Readers.StringOfLinesToArray(String)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String)
        VB: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StringOfLinesToArray
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 404
      summary: "\nSplit a string containing lines of text into an array of strings.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] StringOfLinesToArray(string pstrLines)
          VB: Public Shared Function StringOfLinesToArray(pstrLines As String) As String()
        parameters:
        - id: pstrLines
          type: System.String
          description: "\nString containing lines of text, terminated by CR/LF pairs.\n"
        return:
          type: System.String[]
          description: "\nArray of strings, one line per string. Blank lines are preserved as\nempty strings.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String,System.StringSplitOptions)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String,System.StringSplitOptions)
      language: CSharp
      name:
        CSharp: StringOfLinesToArray(String, StringSplitOptions)
        VB: StringOfLinesToArray(String, StringSplitOptions)
      nameWithType:
        CSharp: Readers.StringOfLinesToArray(String, StringSplitOptions)
        VB: Readers.StringOfLinesToArray(String, StringSplitOptions)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String, System.StringSplitOptions)
        VB: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray(System.String, System.StringSplitOptions)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StringOfLinesToArray
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 439
      summary: "\nSplit a string containing lines of text into an array of strings,\nas modified by the StringSplitOptions flag.\n"
      remarks: "\nUse this overload to convert a string, discarding blank lines.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] StringOfLinesToArray(string pstrLines, StringSplitOptions penmStringSplitOptions)
          VB: Public Shared Function StringOfLinesToArray(pstrLines As String, penmStringSplitOptions As StringSplitOptions) As String()
        parameters:
        - id: pstrLines
          type: System.String
          description: "\nString containing lines of text, terminated by CR/LF pairs.\n"
        - id: penmStringSplitOptions
          type: System.StringSplitOptions
          description: "\nA member of the StringSplitOptions enumeration, presumably other\nthan StringSplitOptions.None, which is assumed by the first\noverload. The only option supported by version 2 of the Microsoft\n.NET CLR is RemoveEmptyEntries.\n"
        return:
          type: System.String[]
          description: "\nArray of strings, one line per string. Blank lines are preserved as\nempty strings unless penmStringSplitOptions is RemoveEmptyEntries,\nas is most likely to be the case.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.EmbeddedTextFile.Readers.StringToArray(System.String)
      commentId: M:WizardWrx.EmbeddedTextFile.Readers.StringToArray(System.String)
      language: CSharp
      name:
        CSharp: StringToArray(String)
        VB: StringToArray(String)
      nameWithType:
        CSharp: Readers.StringToArray(String)
        VB: Readers.StringToArray(String)
      qualifiedName:
        CSharp: WizardWrx.EmbeddedTextFile.Readers.StringToArray(System.String)
        VB: WizardWrx.EmbeddedTextFile.Readers.StringToArray(System.String)
      type: Method
      assemblies:
      - WizardWrx.EmbeddedTextFile
      namespace: WizardWrx.EmbeddedTextFile
      source:
        remote:
          path: EmbeddedTextFile/Readers.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: StringToArray
        path: ../EmbeddedTextFile/Readers.cs
        startLine: 462
      summary: "\nReturn a one-element array containing the input string.\n"
      example: []
      syntax:
        content:
          CSharp: public static string[] StringToArray(string pstr)
          VB: Public Shared Function StringToArray(pstr As String) As String()
        parameters:
        - id: pstr
          type: System.String
          description: "\nString to place into the returned array.\n"
        return:
          type: System.String[]
          description: "\nArray of strings, containing exactly one element, which contains\nthe single input string.\n"
      overload: WizardWrx.EmbeddedTextFile.Readers.StringToArray*
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
  System.Byte[]:
    name:
      CSharp:
      - id: System.Byte
        name: Byte
        nameWithType: Byte
        qualifiedName: System.Byte
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: System.Byte
        name: Byte
        nameWithType: Byte
        qualifiedName: System.Byte
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
  WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor*
        name: ByteOrderMark
        nameWithType: ByteOrderMark.ByteOrderMark
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.ByteOrderMark
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor*
        name: ByteOrderMark
        nameWithType: ByteOrderMark.ByteOrderMark
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.ByteOrderMark
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.ByteOrderMark.#ctor
  WizardWrx.EmbeddedTextFile:
    name:
      CSharp:
      - name: WizardWrx.EmbeddedTextFile
        nameWithType: WizardWrx.EmbeddedTextFile
        qualifiedName: WizardWrx.EmbeddedTextFile
      VB:
      - name: WizardWrx.EmbeddedTextFile
        nameWithType: WizardWrx.EmbeddedTextFile
        qualifiedName: WizardWrx.EmbeddedTextFile
    isDefinition: true
    commentId: N:WizardWrx.EmbeddedTextFile
  WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
        name: ByteOrderMark.BOMType
        nameWithType: ByteOrderMark.BOMType
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
        name: ByteOrderMark.BOMType
        nameWithType: ByteOrderMark.BOMType
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
    isDefinition: true
    parent: WizardWrx.EmbeddedTextFile
    commentId: T:WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType
  WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind*
        name: Kind
        nameWithType: ByteOrderMark.Kind
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind*
        name: Kind
        nameWithType: ByteOrderMark.Kind
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.ByteOrderMark.Kind
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
  WizardWrx.EmbeddedTextFile.ByteOrderMark.Length*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length*
        name: Length
        nameWithType: ByteOrderMark.Length
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length*
        name: Length
        nameWithType: ByteOrderMark.Length
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.ByteOrderMark.Length
  WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM*
        name: TestForBOM
        nameWithType: ByteOrderMark.TestForBOM
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM*
        name: TestForBOM
        nameWithType: ByteOrderMark.TestForBOM
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.ByteOrderMark.TestForBOM
  WizardWrx.EmbeddedTextFile.ByteOrderMark:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark
        name: ByteOrderMark
        nameWithType: ByteOrderMark
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark
      VB:
      - id: WizardWrx.EmbeddedTextFile.ByteOrderMark
        name: ByteOrderMark
        nameWithType: ByteOrderMark
        qualifiedName: WizardWrx.EmbeddedTextFile.ByteOrderMark
    isDefinition: true
    commentId: T:WizardWrx.EmbeddedTextFile.ByteOrderMark
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
  ? WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<ByteOrderMark.BOMType>
        nameWithType: StringExtensions.RenderEvenWhenNull<ByteOrderMark.BOMType>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType>
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
        name: RenderEvenWhenNull(Of ByteOrderMark.BOMType)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of ByteOrderMark.BOMType)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.EmbeddedTextFile.ByteOrderMark.BOMType)
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
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly):
    commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly(System.String,System.Reflection.Assembly)
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String):
    commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly(System.String)
  System.String[]:
    name:
      CSharp:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
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
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly*
        name: LoadTextFileFromCallingAssembly
        nameWithType: Readers.LoadTextFileFromCallingAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly*
        name: LoadTextFileFromCallingAssembly
        nameWithType: Readers.LoadTextFileFromCallingAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String):
    commentId: M:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromCallingAssembly(System.String)
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly*
        name: LoadTextFileFromEntryAssembly
        nameWithType: Readers.LoadTextFileFromEntryAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly*
        name: LoadTextFileFromEntryAssembly
        nameWithType: Readers.LoadTextFileFromEntryAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromEntryAssembly
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
  WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly*
        name: LoadTextFileFromAnyAssembly
        nameWithType: Readers.LoadTextFileFromAnyAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly*
        name: LoadTextFileFromAnyAssembly
        nameWithType: Readers.LoadTextFileFromAnyAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.LoadTextFileFromAnyAssembly
  System.Exception:
    commentId: T:System.Exception
  WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly*
        name: LoadBinaryResourceFromAnyAssembly
        nameWithType: Readers.LoadBinaryResourceFromAnyAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly*
        name: LoadBinaryResourceFromAnyAssembly
        nameWithType: Readers.LoadBinaryResourceFromAnyAssembly
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.LoadBinaryResourceFromAnyAssembly
  WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray*
        name: StringFromANSICharacterArray
        nameWithType: Readers.StringFromANSICharacterArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray*
        name: StringFromANSICharacterArray
        nameWithType: Readers.StringFromANSICharacterArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.StringFromANSICharacterArray
  WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray*
        name: StringOfLinesToArray
        nameWithType: Readers.StringOfLinesToArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray*
        name: StringOfLinesToArray
        nameWithType: Readers.StringOfLinesToArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.StringOfLinesToArray
  System.StringSplitOptions:
    name:
      CSharp:
      - id: System.StringSplitOptions
        name: StringSplitOptions
        nameWithType: StringSplitOptions
        qualifiedName: System.StringSplitOptions
        isExternal: true
      VB:
      - id: System.StringSplitOptions
        name: StringSplitOptions
        nameWithType: StringSplitOptions
        qualifiedName: System.StringSplitOptions
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.StringSplitOptions
  WizardWrx.EmbeddedTextFile.Readers.StringToArray*:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringToArray*
        name: StringToArray
        nameWithType: Readers.StringToArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringToArray
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers.StringToArray*
        name: StringToArray
        nameWithType: Readers.StringToArray
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers.StringToArray
    isDefinition: true
    commentId: Overload:WizardWrx.EmbeddedTextFile.Readers.StringToArray
  WizardWrx.EmbeddedTextFile.Readers:
    name:
      CSharp:
      - id: WizardWrx.EmbeddedTextFile.Readers
        name: Readers
        nameWithType: Readers
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers
      VB:
      - id: WizardWrx.EmbeddedTextFile.Readers
        name: Readers
        nameWithType: Readers
        qualifiedName: WizardWrx.EmbeddedTextFile.Readers
    isDefinition: true
    commentId: T:WizardWrx.EmbeddedTextFile.Readers
