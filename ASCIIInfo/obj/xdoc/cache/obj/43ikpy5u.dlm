id: WizardWrx.ASCIIInfo
language: CSharp
name:
  Default: WizardWrx.ASCIIInfo
qualifiedName:
  Default: WizardWrx.ASCIIInfo
type: Assembly
modifiers: {}
items:
- id: WizardWrx
  commentId: N:WizardWrx
  language: CSharp
  name:
    CSharp: WizardWrx
    VB: WizardWrx
  nameWithType:
    CSharp: WizardWrx
    VB: WizardWrx
  qualifiedName:
    CSharp: WizardWrx
    VB: WizardWrx
  type: Namespace
  assemblies:
  - WizardWrx.ASCIIInfo
  modifiers: {}
  items:
  - id: WizardWrx.ASCIICharacterDisplayInfo
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo
    language: CSharp
    name:
      CSharp: ASCIICharacterDisplayInfo
      VB: ASCIICharacterDisplayInfo
    nameWithType:
      CSharp: ASCIICharacterDisplayInfo
      VB: ASCIICharacterDisplayInfo
    qualifiedName:
      CSharp: WizardWrx.ASCIICharacterDisplayInfo
      VB: WizardWrx.ASCIICharacterDisplayInfo
    type: Class
    assemblies:
    - WizardWrx.ASCIIInfo
    namespace: WizardWrx
    source:
      remote:
        path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ASCIICharacterDisplayInfo
      path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
      startLine: 125
    summary: "\nInstances of this class represent individual ASCII characters. Since the\nNUL character is defined, and occupies the first element, the numeric \ncode that corresponds to a character maps directly to the corresponding\nelement in this array.\n"
    example:
    - "\nThe ASCII code for a space is 32. ASCIICharacterDisplayInfo[32], for C#,\nor ASCIICharacterDisplayInfo(32), for Visual Basic, returns the item for\nthe space character.\n\nLikewise, the ASCII code for a horizontal TAB character is 9. Hence, the\nC# expression ASCIICharacterDisplayInfo[9] evaluates to the information\nabout the TAB character. Likewise, ASCIICharacterDisplayInfo(9) does the\nsame thing in Visual Basic.\n\nThe following example comes from production code in the class library\nthat motivated me to create this library.\n\nASCIICharacterDisplayInfo [ ] asciiCharTbl = ASCII_Character_Display_Table.GetTheSingleInstance ( ).AllASCIICharacters;\nStringBuilder sbTheBadChar = new StringBuilder ( );\nsbTheBadChar.Append ( asciiCharTbl [ ( uint ) _chrBad ].DisplayText );\n\nObviously, more things go into the message before it is returned to the\ncalling routine.\n"
    syntax:
      content:
        CSharp: 'public class ASCIICharacterDisplayInfo : object'
        VB: >-
          Public Class ASCIICharacterDisplayInfo

              Inherits Object
    see:
    - linkId: WizardWrx.ASCII_Character_Display_Table
      commentId: T:WizardWrx.ASCII_Character_Display_Table
    inheritance:
    - System.Object
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
      language: CSharp
      name:
        CSharp: ASCIICharacter
        VB: ASCIICharacter
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.ASCIICharacter
        VB: ASCIICharacterDisplayInfo.ASCIICharacter
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
        VB: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ASCIICharacter
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 400
      summary: "\nGets the Unicode character represented by the code\n"
      example: []
      syntax:
        content:
          CSharp: public char ASCIICharacter { get; }
          VB: Public ReadOnly Property ASCIICharacter As Char
        parameters: []
        return:
          type: System.Char
      overload: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.AlternateText
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.AlternateText
      language: CSharp
      name:
        CSharp: AlternateText
        VB: AlternateText
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.AlternateText
        VB: ASCIICharacterDisplayInfo.AlternateText
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.AlternateText
        VB: WizardWrx.ASCIICharacterDisplayInfo.AlternateText
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: AlternateText
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 408
      summary: "\nGets an alternative visual representation of certain nonprintable\nand otherwise ambiguous characters, such as the SPACE character.\n"
      example: []
      syntax:
        content:
          CSharp: public string AlternateText { get; }
          VB: Public ReadOnly Property AlternateText As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.AlternateText*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.CHAR
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.CHAR
      language: CSharp
      name:
        CSharp: CHAR
        VB: CHAR
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CHAR
        VB: ASCIICharacterDisplayInfo.CHAR
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CHAR
        VB: WizardWrx.ASCIICharacterDisplayInfo.CHAR
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CHAR
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 426
      summary: "\nThis value is the standard two or three character acronymn for the\ncharacter, including the square brackets that usually enclose it.\nThis value is meaningful only for the Control Characters group. This\nvalue is blank for all others.\n"
      example: []
      syntax:
        content:
          CSharp: public string CHAR { get; }
          VB: Public ReadOnly Property CHAR As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.CHAR*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharType
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.CharType
      language: CSharp
      name:
        CSharp: CharType
        VB: CharType
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharType
        VB: ASCIICharacterDisplayInfo.CharType
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharType
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharType
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CharType
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 441
      summary: "\nGets the CharacterType enumeration mapping of the character\n"
      example: []
      syntax:
        content:
          CSharp: public ASCIICharacterDisplayInfo.CharacterType CharType { get; }
          VB: Public ReadOnly Property CharType As ASCIICharacterDisplayInfo.CharacterType
        parameters: []
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      overload: WizardWrx.ASCIICharacterDisplayInfo.CharType*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharSubType
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.CharSubType
      language: CSharp
      name:
        CSharp: CharSubType
        VB: CharSubType
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharSubType
        VB: ASCIICharacterDisplayInfo.CharSubType
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharSubType
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharSubType
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CharSubType
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 447
      summary: "\nGets the CharacterSubtype enumeration mapping of the character\n"
      example: []
      syntax:
        content:
          CSharp: public ASCIICharacterDisplayInfo.CharacterSubtype CharSubType { get; }
          VB: Public ReadOnly Property CharSubType As ASCIICharacterDisplayInfo.CharacterSubtype
        parameters: []
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      overload: WizardWrx.ASCIICharacterDisplayInfo.CharSubType*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.Code
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.Code
      language: CSharp
      name:
        CSharp: Code
        VB: Code
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.Code
        VB: ASCIICharacterDisplayInfo.Code
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.Code
        VB: WizardWrx.ASCIICharacterDisplayInfo.Code
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Code
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 452
      summary: "\nGets the unsigned integer representation of the ASCII code\n"
      example: []
      syntax:
        content:
          CSharp: public uint Code { get; }
          VB: Public ReadOnly Property Code As UInteger
        parameters: []
        return:
          type: System.UInt32
      overload: WizardWrx.ASCIICharacterDisplayInfo.Code*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
      language: CSharp
      name:
        CSharp: CodeAsDecimal
        VB: CodeAsDecimal
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CodeAsDecimal
        VB: ASCIICharacterDisplayInfo.CodeAsDecimal
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
        VB: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CodeAsDecimal
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 461
      summary: "\nGets a string representation of the raw ASCII code as a decimal\nnumber, formatted to always occupy the maximum number of characters\nneeded, three\n"
      example: []
      syntax:
        content:
          CSharp: public string CodeAsDecimal { get; }
          VB: Public ReadOnly Property CodeAsDecimal As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
      language: CSharp
      name:
        CSharp: CodeAsHexadecimal
        VB: CodeAsHexadecimal
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CodeAsHexadecimal
        VB: ASCIICharacterDisplayInfo.CodeAsHexadecimal
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
        VB: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CodeAsHexadecimal
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 476
      summary: "\nGets a string representation of the ASCII code as a hexadecimal\nnumber\n"
      example: []
      syntax:
        content:
          CSharp: public string CodeAsHexadecimal { get; }
          VB: Public ReadOnly Property CodeAsHexadecimal As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
      language: CSharp
      name:
        CSharp: UnicodeCodePoint
        VB: UnicodeCodePoint
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.UnicodeCodePoint
        VB: ASCIICharacterDisplayInfo.UnicodeCodePoint
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
        VB: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UnicodeCodePoint
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 490
      summary: "\nGets a string representation of the Unicode code point\n"
      example: []
      syntax:
        content:
          CSharp: public string UnicodeCodePoint { get; }
          VB: Public ReadOnly Property UnicodeCodePoint As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.Comment
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.Comment
      language: CSharp
      name:
        CSharp: Comment
        VB: Comment
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.Comment
        VB: ASCIICharacterDisplayInfo.Comment
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.Comment
        VB: WizardWrx.ASCIICharacterDisplayInfo.Comment
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Comment
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 505
      summary: "\nGets the associated comment, if one exists, or returns the empty\nstring.\n"
      example: []
      syntax:
        content:
          CSharp: public string Comment { get; }
          VB: Public ReadOnly Property Comment As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.Comment*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.Description
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.Description
      language: CSharp
      name:
        CSharp: Description
        VB: Description
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.Description
        VB: ASCIICharacterDisplayInfo.Description
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.Description
        VB: WizardWrx.ASCIICharacterDisplayInfo.Description
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Description
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 521
      summary: "\nWhen appropriate, this field returns a short text description of the\ncharacter.\n"
      example: []
      syntax:
        content:
          CSharp: public string Description { get; }
          VB: Public ReadOnly Property Description As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.Description*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayText
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.DisplayText
      language: CSharp
      name:
        CSharp: DisplayText
        VB: DisplayText
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.DisplayText
        VB: ASCIICharacterDisplayInfo.DisplayText
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.DisplayText
        VB: WizardWrx.ASCIICharacterDisplayInfo.DisplayText
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DisplayText
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 544
      summary: "\nUse this property to guarantee that any character will display\nsomething useful when the character is presented on its own, out of\ncontext, such as in an error message.\n"
      remarks: "\nThe objective of this property is to return something that is always\nacceptable in printed matter, without surprises such as line feeds,\nline wraps, and page ejects, among other things elicited by some of\nthe control codes.\n"
      example: []
      syntax:
        content:
          CSharp: public string DisplayText { get; }
          VB: Public ReadOnly Property DisplayText As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.DisplayText*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.HTMLName
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.HTMLName
      language: CSharp
      name:
        CSharp: HTMLName
        VB: HTMLName
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.HTMLName
        VB: ASCIICharacterDisplayInfo.HTMLName
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.HTMLName
        VB: WizardWrx.ASCIICharacterDisplayInfo.HTMLName
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: HTMLName
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 571
      summary: "\nThis field returns the HTML entity name, when one exists, of the\ncharacter, its HTML entity string. Otherwise, the return value is\nthe empty string, represented herein by a true constant,\nSpecialStrings.EMPTY_STRING.\n"
      remarks: "\nIf this returns anything besides the empty string, the string\nreturned by this property should probably take its place in HTML\ntext.\n"
      example: []
      syntax:
        content:
          CSharp: public string HTMLName { get; }
          VB: Public ReadOnly Property HTMLName As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.HTMLName*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
      commentId: P:WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
      language: CSharp
      name:
        CSharp: URLEncoding
        VB: URLEncoding
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.URLEncoding
        VB: ASCIICharacterDisplayInfo.URLEncoding
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
        VB: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: URLEncoding
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 588
      summary: "\nGets the URLEncoding value of the character, which is available for\nANY character, though it is needed only for punctuation, control\ncodes, white space, and other special characters\n"
      example: []
      syntax:
        content:
          CSharp: public string URLEncoding { get; }
          VB: Public ReadOnly Property URLEncoding As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.ASCIICharacterDisplayInfo.ToString
      commentId: M:WizardWrx.ASCIICharacterDisplayInfo.ToString
      language: CSharp
      name:
        CSharp: ToString()
        VB: ToString()
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.ToString()
        VB: ASCIICharacterDisplayInfo.ToString()
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.ToString()
        VB: WizardWrx.ASCIICharacterDisplayInfo.ToString()
      type: Method
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ToString
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 613
      summary: "\nOverride ToString to render all three defined formats and most other\nproperties.\n"
      example: []
      syntax:
        content:
          CSharp: public override string ToString()
          VB: Public Overrides Function ToString As String
        return:
          type: System.String
          description: "\nThe return value is a string containing a printable representation\nof the character, followed by its hexadecimal and decimal values,\nboth enclosed in a single pair of parenetheses, then by every\nconceivable way to represent the character except the URL encoding,\nwhich can be inferred from the hexadecimal string inserted as the\nCodeAsHexadecimal property value.\n"
      overload: WizardWrx.ASCIICharacterDisplayInfo.ToString*
      modifiers:
        CSharp:
        - public
        - override
        VB:
        - Public
        - Overrides
    - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo(System.Char)
      commentId: M:WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo(System.Char)
      language: CSharp
      name:
        CSharp: DisplayCharacterInfo(Char)
        VB: DisplayCharacterInfo(Char)
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.DisplayCharacterInfo(Char)
        VB: ASCIICharacterDisplayInfo.DisplayCharacterInfo(Char)
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo(System.Char)
        VB: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo(System.Char)
      type: Method
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DisplayCharacterInfo
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 644
      summary: "\nCreate a ASCIICharacterDisplayInfo instance to represent a specified\nASCII character, and call its ToString method to return all three\nrepresentations of it (Printable, Hexadecimal, and Decimal, in that\norder.\n"
      example: []
      syntax:
        content:
          CSharp: public static string DisplayCharacterInfo(char pchr)
          VB: Public Shared Function DisplayCharacterInfo(pchr As Char) As String
        parameters:
        - id: pchr
          type: System.Char
          description: "\nSpecify the character for which to render the three representations.\n"
        return:
          type: System.String
          description: "\nReturn the output of ToString on the ASCIICharacterDisplayInfo.\n"
      overload: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    references:
      WizardWrx.ASCII_Character_Display_Table: 
  - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo.CharacterType
    language: CSharp
    name:
      CSharp: ASCIICharacterDisplayInfo.CharacterType
      VB: ASCIICharacterDisplayInfo.CharacterType
    nameWithType:
      CSharp: ASCIICharacterDisplayInfo.CharacterType
      VB: ASCIICharacterDisplayInfo.CharacterType
    qualifiedName:
      CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
    type: Enum
    assemblies:
    - WizardWrx.ASCIIInfo
    namespace: WizardWrx
    source:
      remote:
        path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: CharacterType
      path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
      startLine: 132
    summary: "\nThis enumeration maps the CharType strings to fast, efficient, safe\ninteger values that correspond to three broad groups of characters.\n"
    example: []
    syntax:
      content:
        CSharp: 'public enum CharacterType : int'
        VB: Public Enum CharacterType As Integer
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Undefined
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Undefined
      language: CSharp
      name:
        CSharp: Undefined
        VB: Undefined
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterType.Undefined
        VB: ASCIICharacterDisplayInfo.CharacterType.Undefined
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Undefined
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Undefined
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Undefined
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 137
      summary: "\nThe value is uninitialized.\n"
      example: []
      syntax:
        content:
          CSharp: Undefined = 0
          VB: Undefined = 0
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.NonPrintable
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterType.NonPrintable
      language: CSharp
      name:
        CSharp: NonPrintable
        VB: NonPrintable
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterType.NonPrintable
        VB: ASCIICharacterDisplayInfo.CharacterType.NonPrintable
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.NonPrintable
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.NonPrintable
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: NonPrintable
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 143
      summary: "\nThe character is of a nonprintable type. Most of these are\noutput device control codes.\n"
      example: []
      syntax:
        content:
          CSharp: NonPrintable = 1
          VB: NonPrintable = 1
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Printable
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Printable
      language: CSharp
      name:
        CSharp: Printable
        VB: Printable
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterType.Printable
        VB: ASCIICharacterDisplayInfo.CharacterType.Printable
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Printable
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Printable
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Printable
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 148
      summary: "\nThese are the characters from which most text is composed.\n"
      example: []
      syntax:
        content:
          CSharp: Printable = 2
          VB: Printable = 2
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Extended
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Extended
      language: CSharp
      name:
        CSharp: Extended
        VB: Extended
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterType.Extended
        VB: ASCIICharacterDisplayInfo.CharacterType.Extended
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Extended
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterType.Extended
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Extended
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 154
      summary: "\nThese are the extended ASCII characters that have code points at\nor above 128, that require 8 bits to represent in binary coding.\n"
      example: []
      syntax:
        content:
          CSharp: Extended = 3
          VB: Extended = 3
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
    language: CSharp
    name:
      CSharp: ASCIICharacterDisplayInfo.CharacterSubtype
      VB: ASCIICharacterDisplayInfo.CharacterSubtype
    nameWithType:
      CSharp: ASCIICharacterDisplayInfo.CharacterSubtype
      VB: ASCIICharacterDisplayInfo.CharacterSubtype
    qualifiedName:
      CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
    type: Enum
    assemblies:
    - WizardWrx.ASCIIInfo
    namespace: WizardWrx
    source:
      remote:
        path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: CharacterSubtype
      path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
      startLine: 164
    summary: "\nThis enumeration maps the Subtype strings to fast, efficient, safe\ninteger values that correspond to ten specialized groups of\ncharacters. These correspond roughly to the Unicode character\ncategories.\n"
    example: []
    syntax:
      content:
        CSharp: 'public enum CharacterSubtype : int'
        VB: Public Enum CharacterSubtype As Integer
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
      language: CSharp
      name:
        CSharp: Undefined
        VB: Undefined
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Undefined
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Undefined
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 169
      summary: "\nThe value is uninitialized.\n"
      example: []
      syntax:
        content:
          CSharp: Undefined = 0
          VB: Undefined = 0
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.NULL
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.NULL
      language: CSharp
      name:
        CSharp: "NULL"
        VB: "NULL"
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.NULL
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.NULL
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.NULL
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.NULL
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: "NULL"
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 175
      summary: "\nThere is but one character in this subgroup, the ASCII NULL,\nsometimes called NUL, which has a numerical value of zero.\n"
      example: []
      syntax:
        content:
          CSharp: NULL = 1
          VB: NULL = 1
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Control
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Control
      language: CSharp
      name:
        CSharp: Control
        VB: Control
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Control
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Control
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Control
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Control
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Control
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 182
      summary: "\nThese are the characters that have code point values between 1\nand 31, most of which are device control characters that\noriginally drove line printers.\n"
      example: []
      syntax:
        content:
          CSharp: Control = 2
          VB: Control = 2
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
      language: CSharp
      name:
        CSharp: White_Space
        VB: White_Space
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.White_Space
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: White_Space
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 195
      summary: "\nWhite space is another tiny subgroup; it contains five code\npoints, 32, the everyday SPACE character, 160, the nonbreaking\nSpace, 8, the destructive backspace, 9, the horizontal tab, and\n11, the vertical tab, only one of which, the regular space, is\nconsidered printable. The nonbreaking space, also printable, is\nclassified as an extended character because its code point is\ngreater than 127, while the rest fall into the group that is\ncomprised otherwise of device control codes. Since that is their\nprimary function, they belong in this group.\n"
      example: []
      syntax:
        content:
          CSharp: White_Space = 3
          VB: White_Space = 3
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
      language: CSharp
      name:
        CSharp: Punctuation
        VB: Punctuation
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Punctuation
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Punctuation
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 202
      summary: "\nCharacters in this group are generally accepted as punctuation\nmarks, of which one, the inverted question mark, belongs to the\nExtended group, owing to its high code point, 191.\n"
      example: []
      syntax:
        content:
          CSharp: Punctuation = 4
          VB: Punctuation = 4
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
      language: CSharp
      name:
        CSharp: Symbol
        VB: Symbol
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Symbol
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Symbol
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 208
      summary: "\nCharacters in this group are a mixed bag of mathematical and\nother symbols.\n"
      example: []
      syntax:
        content:
          CSharp: Symbol = 5
          VB: Symbol = 5
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
      language: CSharp
      name:
        CSharp: Numeral
        VB: Numeral
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Numeral
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Numeral
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 213
      summary: "\nThese characters represent the Arabic numerals zero through nine.\n"
      example: []
      syntax:
        content:
          CSharp: Numeral = 6
          VB: Numeral = 6
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
      language: CSharp
      name:
        CSharp: UC_Latin_1_Letter
        VB: UC_Latin_1_Letter
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.UC_Latin_1_Letter
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: UC_Latin_1_Letter
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 219
      summary: "\nCharacters in this group represent the upper case letters of the\nLatin alphabet.\n"
      example: []
      syntax:
        content:
          CSharp: UC_Latin_1_Letter = 7
          VB: UC_Latin_1_Letter = 7
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
      language: CSharp
      name:
        CSharp: LC_Latin_1_Letter
        VB: LC_Latin_1_Letter
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.LC_Latin_1_Letter
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: LC_Latin_1_Letter
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 225
      summary: "\nCharacters in this group represent the lower case letters of the\nLatin alphabet.\n"
      example: []
      syntax:
        content:
          CSharp: LC_Latin_1_Letter = 8
          VB: LC_Latin_1_Letter = 8
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
      language: CSharp
      name:
        CSharp: Greek_Letter
        VB: Greek_Letter
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Greek_Letter
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Greek_Letter
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 233
      summary: "\nOnly a handful of characters, all of which fall into the\nExtended group, are specifically identified as letters of the\nGreek alphabet. Others may have been overlooked. If you find one\nof them, please submit a pull request or open an issue.\n"
      example: []
      syntax:
        content:
          CSharp: Greek_Letter = 9
          VB: Greek_Letter = 9
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
      commentId: F:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
      language: CSharp
      name:
        CSharp: Other_Letter
        VB: Other_Letter
      nameWithType:
        CSharp: ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
        VB: ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
      qualifiedName:
        CSharp: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
        VB: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype.Other_Letter
      type: Field
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCIICharacterDisplayInfo.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Other_Letter
        path: ../ASCIIInfo/ASCIICharacterDisplayInfo.cs
        startLine: 239
      summary: "\nThis large group represents latters and joined characters used\nin other Western alphabets.\n"
      example: []
      syntax:
        content:
          CSharp: Other_Letter = 10
          VB: Other_Letter = 10
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.ASCII_Character_Display_Table
    commentId: T:WizardWrx.ASCII_Character_Display_Table
    language: CSharp
    name:
      CSharp: ASCII_Character_Display_Table
      VB: ASCII_Character_Display_Table
    nameWithType:
      CSharp: ASCII_Character_Display_Table
      VB: ASCII_Character_Display_Table
    qualifiedName:
      CSharp: WizardWrx.ASCII_Character_Display_Table
      VB: WizardWrx.ASCII_Character_Display_Table
    type: Class
    assemblies:
    - WizardWrx.ASCIIInfo
    namespace: WizardWrx
    source:
      remote:
        path: ASCIIInfo/ASCII_Character_Display_Table.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: ASCII_Character_Display_Table
      path: ../ASCIIInfo/ASCII_Character_Display_Table.cs
      startLine: 117
    summary: "\nProvide read only access to a table of ASCII characters and text to\ndisplay for selected special characters.\n"
    example:
    - "\nThe ASCII code for a space is 32. ASCIICharacterDisplayInfo[32], for C#,\nor ASCIICharacterDisplayInfo(32), for Visual Basic, returns the item for\nthe space character.\n\nLikewise, the ASCII code for a horizontal TAB character is 9. Hence, the\nC# expression ASCIICharacterDisplayInfo[9] evaluates to the information\nabout the TAB character. Likewise, ASCIICharacterDisplayInfo(9) does the\nsame thing in Visual Basic.\n\nThe following example comes from production code in the class library\nthat motivated me to create this library.\n\nASCIICharacterDisplayInfo [ ] asciiCharTbl = ASCII_Character_Display_Table.GetTheSingleInstance ( ).AllASCIICharacters;\nStringBuilder sbTheBadChar = new StringBuilder ( );\nsbTheBadChar.Append ( asciiCharTbl [ ( uint ) _chrBad ].DisplayText );\n\nObviously, more things go into the message before it is returned to the\ncalling routine.\n"
    syntax:
      content:
        CSharp: 'public class ASCII_Character_Display_Table : object'
        VB: >-
          Public Class ASCII_Character_Display_Table

              Inherits Object
    seealso:
    - linkId: WizardWrx.ASCIICharacterDisplayInfo
      commentId: T:WizardWrx.ASCIICharacterDisplayInfo
    inheritance:
    - System.Object
    modifiers:
      CSharp:
      - public
      - class
      VB:
      - Public
      - Class
    items:
    - id: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance
      commentId: M:WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance
      language: CSharp
      name:
        CSharp: GetTheSingleInstance()
        VB: GetTheSingleInstance()
      nameWithType:
        CSharp: ASCII_Character_Display_Table.GetTheSingleInstance()
        VB: ASCII_Character_Display_Table.GetTheSingleInstance()
      qualifiedName:
        CSharp: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance()
        VB: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance()
      type: Method
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCII_Character_Display_Table.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GetTheSingleInstance
        path: ../ASCIIInfo/ASCII_Character_Display_Table.cs
        startLine: 166
      summary: "\nGets a reference to the single ASCII_Character_Display_Table\ninstance.\n"
      remarks: "\nThe example given under the help topic for this class shows you that\nyou need not actually allocate storage for the instance, since what\nyou really need is a copy of the ASCIICharacterDisplayInfo table,\navailable through the read only AllASCIICharacters property, which\ncan be assigned directly to an AllASCIICharacters array.\n\nTo preserve its independence, this class uses the archaic Singleton\nimplementation, rather than inherit from the abstract base class in\nWizardWrx.DllServices2.dll, although I could certainly fix that by\nlinking the source code into this assembly. Since that creates an\neven more awkward dependency, and I don&apos;t want to put an actual copy\nin this source tree, I&apos;ll leave it alone. After all, this class is\nnot exactly a high traffic property.\n"
      example: []
      syntax:
        content:
          CSharp: public static ASCII_Character_Display_Table GetTheSingleInstance()
          VB: Public Shared Function GetTheSingleInstance As ASCII_Character_Display_Table
        return:
          type: WizardWrx.ASCII_Character_Display_Table
          description: "\nThe return value is a reference to the single instance of this class\nthat is created in response to the first call to this method. Please\nsee the remarks.\n"
      overload: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
      commentId: P:WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
      language: CSharp
      name:
        CSharp: AllASCIICharacters
        VB: AllASCIICharacters
      nameWithType:
        CSharp: ASCII_Character_Display_Table.AllASCIICharacters
        VB: ASCII_Character_Display_Table.AllASCIICharacters
      qualifiedName:
        CSharp: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
        VB: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
      type: Property
      assemblies:
      - WizardWrx.ASCIIInfo
      namespace: WizardWrx
      source:
        remote:
          path: ASCIIInfo/ASCII_Character_Display_Table.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: AllASCIICharacters
        path: ../ASCIIInfo/ASCII_Character_Display_Table.cs
        startLine: 182
      summary: "\nGets the populated ASCIICharacterDisplayInfo array that is the sole\npublic property of this class, which exists to ensure that exactly\none instance of this table exists.\n"
      example: []
      syntax:
        content:
          CSharp: public ASCIICharacterDisplayInfo[] AllASCIICharacters { get; }
          VB: Public ReadOnly Property AllASCIICharacters As ASCIICharacterDisplayInfo()
        parameters: []
        return:
          type: WizardWrx.ASCIICharacterDisplayInfo[]
      overload: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    references:
      WizardWrx.ASCIICharacterDisplayInfo: 
references:
  WizardWrx.ASCII_Character_Display_Table:
    name:
      CSharp:
      - id: WizardWrx.ASCII_Character_Display_Table
        name: ASCII_Character_Display_Table
        nameWithType: ASCII_Character_Display_Table
        qualifiedName: WizardWrx.ASCII_Character_Display_Table
      VB:
      - id: WizardWrx.ASCII_Character_Display_Table
        name: ASCII_Character_Display_Table
        nameWithType: ASCII_Character_Display_Table
        qualifiedName: WizardWrx.ASCII_Character_Display_Table
    isDefinition: true
    parent: WizardWrx
    commentId: T:WizardWrx.ASCII_Character_Display_Table
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
  WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter*
        name: ASCIICharacter
        nameWithType: ASCIICharacterDisplayInfo.ASCIICharacter
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter*
        name: ASCIICharacter
        nameWithType: ASCIICharacterDisplayInfo.ASCIICharacter
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.ASCIICharacter
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
  WizardWrx.ASCIICharacterDisplayInfo.AlternateText*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.AlternateText*
        name: AlternateText
        nameWithType: ASCIICharacterDisplayInfo.AlternateText
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.AlternateText
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.AlternateText*
        name: AlternateText
        nameWithType: ASCIICharacterDisplayInfo.AlternateText
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.AlternateText
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.AlternateText
  WizardWrx.ASCIICharacterDisplayInfo.CHAR*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CHAR*
        name: CHAR
        nameWithType: ASCIICharacterDisplayInfo.CHAR
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CHAR
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CHAR*
        name: CHAR
        nameWithType: ASCIICharacterDisplayInfo.CHAR
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CHAR
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.CHAR
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
  WizardWrx.ASCIICharacterDisplayInfo.CharacterType:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
        name: ASCIICharacterDisplayInfo.CharacterType
        nameWithType: ASCIICharacterDisplayInfo.CharacterType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
        name: ASCIICharacterDisplayInfo.CharacterType
        nameWithType: ASCIICharacterDisplayInfo.CharacterType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharacterType
    isDefinition: true
    parent: WizardWrx
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo.CharacterType
  WizardWrx.ASCIICharacterDisplayInfo.CharType*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharType*
        name: CharType
        nameWithType: ASCIICharacterDisplayInfo.CharType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharType
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharType*
        name: CharType
        nameWithType: ASCIICharacterDisplayInfo.CharType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharType
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.CharType
  WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
        name: ASCIICharacterDisplayInfo.CharacterSubtype
        nameWithType: ASCIICharacterDisplayInfo.CharacterSubtype
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
        name: ASCIICharacterDisplayInfo.CharacterSubtype
        nameWithType: ASCIICharacterDisplayInfo.CharacterSubtype
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
    isDefinition: true
    parent: WizardWrx
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo.CharacterSubtype
  WizardWrx.ASCIICharacterDisplayInfo.CharSubType*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharSubType*
        name: CharSubType
        nameWithType: ASCIICharacterDisplayInfo.CharSubType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharSubType
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CharSubType*
        name: CharSubType
        nameWithType: ASCIICharacterDisplayInfo.CharSubType
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CharSubType
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.CharSubType
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
    commentId: '!:System.UInt32'
  WizardWrx.ASCIICharacterDisplayInfo.Code*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Code*
        name: Code
        nameWithType: ASCIICharacterDisplayInfo.Code
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Code
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Code*
        name: Code
        nameWithType: ASCIICharacterDisplayInfo.Code
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Code
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.Code
  WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal*
        name: CodeAsDecimal
        nameWithType: ASCIICharacterDisplayInfo.CodeAsDecimal
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal*
        name: CodeAsDecimal
        nameWithType: ASCIICharacterDisplayInfo.CodeAsDecimal
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.CodeAsDecimal
  WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal*
        name: CodeAsHexadecimal
        nameWithType: ASCIICharacterDisplayInfo.CodeAsHexadecimal
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal*
        name: CodeAsHexadecimal
        nameWithType: ASCIICharacterDisplayInfo.CodeAsHexadecimal
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.CodeAsHexadecimal
  WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint*
        name: UnicodeCodePoint
        nameWithType: ASCIICharacterDisplayInfo.UnicodeCodePoint
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint*
        name: UnicodeCodePoint
        nameWithType: ASCIICharacterDisplayInfo.UnicodeCodePoint
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.UnicodeCodePoint
  WizardWrx.ASCIICharacterDisplayInfo.Comment*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Comment*
        name: Comment
        nameWithType: ASCIICharacterDisplayInfo.Comment
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Comment
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Comment*
        name: Comment
        nameWithType: ASCIICharacterDisplayInfo.Comment
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Comment
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.Comment
  WizardWrx.ASCIICharacterDisplayInfo.Description*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Description*
        name: Description
        nameWithType: ASCIICharacterDisplayInfo.Description
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Description
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.Description*
        name: Description
        nameWithType: ASCIICharacterDisplayInfo.Description
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.Description
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.Description
  WizardWrx.ASCIICharacterDisplayInfo.DisplayText*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayText*
        name: DisplayText
        nameWithType: ASCIICharacterDisplayInfo.DisplayText
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.DisplayText
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayText*
        name: DisplayText
        nameWithType: ASCIICharacterDisplayInfo.DisplayText
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.DisplayText
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.DisplayText
  WizardWrx.ASCIICharacterDisplayInfo.HTMLName*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.HTMLName*
        name: HTMLName
        nameWithType: ASCIICharacterDisplayInfo.HTMLName
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.HTMLName
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.HTMLName*
        name: HTMLName
        nameWithType: ASCIICharacterDisplayInfo.HTMLName
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.HTMLName
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.HTMLName
  WizardWrx.ASCIICharacterDisplayInfo.URLEncoding*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding*
        name: URLEncoding
        nameWithType: ASCIICharacterDisplayInfo.URLEncoding
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding*
        name: URLEncoding
        nameWithType: ASCIICharacterDisplayInfo.URLEncoding
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.URLEncoding
  WizardWrx.ASCIICharacterDisplayInfo.ToString*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.ToString*
        name: ToString
        nameWithType: ASCIICharacterDisplayInfo.ToString
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.ToString
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.ToString*
        name: ToString
        nameWithType: ASCIICharacterDisplayInfo.ToString
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.ToString
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.ToString
  WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo*:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo*
        name: DisplayCharacterInfo
        nameWithType: ASCIICharacterDisplayInfo.DisplayCharacterInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo*
        name: DisplayCharacterInfo
        nameWithType: ASCIICharacterDisplayInfo.DisplayCharacterInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo
    isDefinition: true
    commentId: Overload:WizardWrx.ASCIICharacterDisplayInfo.DisplayCharacterInfo
  WizardWrx.ASCIICharacterDisplayInfo:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo
        name: ASCIICharacterDisplayInfo
        nameWithType: ASCIICharacterDisplayInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo
        name: ASCIICharacterDisplayInfo
        nameWithType: ASCIICharacterDisplayInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo
    isDefinition: true
    commentId: T:WizardWrx.ASCIICharacterDisplayInfo
  WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance*:
    name:
      CSharp:
      - id: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance*
        name: GetTheSingleInstance
        nameWithType: ASCII_Character_Display_Table.GetTheSingleInstance
        qualifiedName: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance
      VB:
      - id: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance*
        name: GetTheSingleInstance
        nameWithType: ASCII_Character_Display_Table.GetTheSingleInstance
        qualifiedName: WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance
    isDefinition: true
    commentId: Overload:WizardWrx.ASCII_Character_Display_Table.GetTheSingleInstance
  WizardWrx.ASCIICharacterDisplayInfo[]:
    name:
      CSharp:
      - id: WizardWrx.ASCIICharacterDisplayInfo
        name: ASCIICharacterDisplayInfo
        nameWithType: ASCIICharacterDisplayInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: WizardWrx.ASCIICharacterDisplayInfo
        name: ASCIICharacterDisplayInfo
        nameWithType: ASCIICharacterDisplayInfo
        qualifiedName: WizardWrx.ASCIICharacterDisplayInfo
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
  WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters*:
    name:
      CSharp:
      - id: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters*
        name: AllASCIICharacters
        nameWithType: ASCII_Character_Display_Table.AllASCIICharacters
        qualifiedName: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
      VB:
      - id: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters*
        name: AllASCIICharacters
        nameWithType: ASCII_Character_Display_Table.AllASCIICharacters
        qualifiedName: WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters
    isDefinition: true
    commentId: Overload:WizardWrx.ASCII_Character_Display_Table.AllASCIICharacters