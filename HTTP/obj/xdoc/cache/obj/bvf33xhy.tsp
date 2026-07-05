id: WizardWrx.HTTP
language: CSharp
name:
  Default: WizardWrx.HTTP
qualifiedName:
  Default: WizardWrx.HTTP
type: Assembly
modifiers: {}
items:
- id: WizardWrx.HTTP
  commentId: N:WizardWrx.HTTP
  language: CSharp
  name:
    CSharp: WizardWrx.HTTP
    VB: WizardWrx.HTTP
  nameWithType:
    CSharp: WizardWrx.HTTP
    VB: WizardWrx.HTTP
  qualifiedName:
    CSharp: WizardWrx.HTTP
    VB: WizardWrx.HTTP
  type: Namespace
  assemblies:
  - WizardWrx.HTTP
  modifiers: {}
  items:
  - id: WizardWrx.HTTP.RequestEngine
    commentId: T:WizardWrx.HTTP.RequestEngine
    language: CSharp
    name:
      CSharp: RequestEngine
      VB: RequestEngine
    nameWithType:
      CSharp: RequestEngine
      VB: RequestEngine
    qualifiedName:
      CSharp: WizardWrx.HTTP.RequestEngine
      VB: WizardWrx.HTTP.RequestEngine
    type: Class
    assemblies:
    - WizardWrx.HTTP
    namespace: WizardWrx.HTTP
    source:
      remote:
        path: HTTP/RequestEngine.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: RequestEngine
      path: ../HTTP/RequestEngine.cs
      startLine: 53
    summary: "\nInstances of this class export a single method that supports all five\nHTTP Request verbs.\n"
    example: []
    syntax:
      content:
        CSharp: public class RequestEngine
        VB: Public Class RequestEngine
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
    - id: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
      commentId: F:WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
      language: CSharp
      name:
        CSharp: REQUEST_EXPECTS_JSON_RESPONSE
        VB: REQUEST_EXPECTS_JSON_RESPONSE
      nameWithType:
        CSharp: RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
        VB: RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
        VB: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_JSON_RESPONSE
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: REQUEST_EXPECTS_JSON_RESPONSE
        path: ../HTTP/RequestEngine.cs
        startLine: 60
      summary: "\nUse this constant to explicitly assert the default value of the last\nparameter to <xref href=\"WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String%2cSystem.Action%7bNewtonsoft.Json.Linq.JObject%7d%2cWizardWrx.JsonSupport.JSON_Deserialized_Object%2cWizardWrx.HTTP.RequestEngine.HttpVerb%2cSystem.Boolean)\" data-throw-if-not-resolved=\"false\"></xref>,\n<code>pfExpectJSON</code>.\n"
      example: []
      syntax:
        content:
          CSharp: public const bool REQUEST_EXPECTS_JSON_RESPONSE = true
          VB: Public Const REQUEST_EXPECTS_JSON_RESPONSE As Boolean = True
        return:
          type: System.Boolean
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
      references:
        ? WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
        : 
    - id: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
      commentId: F:WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
      language: CSharp
      name:
        CSharp: REQUEST_EXPECTS_OTHER_RESPONSE
        VB: REQUEST_EXPECTS_OTHER_RESPONSE
      nameWithType:
        CSharp: RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
        VB: RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
        VB: WizardWrx.HTTP.RequestEngine.REQUEST_EXPECTS_OTHER_RESPONSE
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: REQUEST_EXPECTS_OTHER_RESPONSE
        path: ../HTTP/RequestEngine.cs
        startLine: 68
      summary: "\nUse this constant to override the default value of the last\nparameter to <xref href=\"WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String%2cSystem.Action%7bNewtonsoft.Json.Linq.JObject%7d%2cWizardWrx.JsonSupport.JSON_Deserialized_Object%2cWizardWrx.HTTP.RequestEngine.HttpVerb%2cSystem.Boolean)\" data-throw-if-not-resolved=\"false\"></xref>,\n<code>pfExpectJSON</code>.\n"
      example: []
      syntax:
        content:
          CSharp: public const bool REQUEST_EXPECTS_OTHER_RESPONSE = false
          VB: Public Const REQUEST_EXPECTS_OTHER_RESPONSE As Boolean = False
        return:
          type: System.Boolean
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
      references:
        ? WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
        : 
    - id: WizardWrx.HTTP.RequestEngine.Options
      commentId: P:WizardWrx.HTTP.RequestEngine.Options
      language: CSharp
      name:
        CSharp: Options
        VB: Options
      nameWithType:
        CSharp: RequestEngine.Options
        VB: RequestEngine.Options
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.Options
        VB: WizardWrx.HTTP.RequestEngine.Options
      type: Property
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Options
        path: ../HTTP/RequestEngine.cs
        startLine: 115
      summary: "\nThis immutable reference to a RequestOptions object is a concise way\nto attach options such as an OAuth access token and a method to get\na new token, along with a robust exception logging object that has\nmany configurable settings that determine how much detail it reports\nand how its reports are delivered.\n"
      example: []
      syntax:
        content:
          CSharp: public RequestOptions Options { get; }
          VB: Public ReadOnly Property Options As RequestOptions
        parameters: []
        return:
          type: WizardWrx.HTTP.RequestOptions
      overload: WizardWrx.HTTP.RequestEngine.Options*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.HTTP.RequestEngine.#ctor
      commentId: M:WizardWrx.HTTP.RequestEngine.#ctor
      language: CSharp
      name:
        CSharp: RequestEngine()
        VB: RequestEngine()
      nameWithType:
        CSharp: RequestEngine.RequestEngine()
        VB: RequestEngine.RequestEngine()
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.RequestEngine()
        VB: WizardWrx.HTTP.RequestEngine.RequestEngine()
      type: Constructor
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../HTTP/RequestEngine.cs
        startLine: 122
      summary: "\nThe default constructor leaves the Options property null, which is\nOK for most use cases.\n"
      example: []
      syntax:
        content:
          CSharp: public RequestEngine()
          VB: Public Sub New
      overload: WizardWrx.HTTP.RequestEngine.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.HTTP.RequestEngine.#ctor(WizardWrx.HTTP.RequestOptions)
      commentId: M:WizardWrx.HTTP.RequestEngine.#ctor(WizardWrx.HTTP.RequestOptions)
      language: CSharp
      name:
        CSharp: RequestEngine(RequestOptions)
        VB: RequestEngine(RequestOptions)
      nameWithType:
        CSharp: RequestEngine.RequestEngine(RequestOptions)
        VB: RequestEngine.RequestEngine(RequestOptions)
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.RequestEngine(WizardWrx.HTTP.RequestOptions)
        VB: WizardWrx.HTTP.RequestEngine.RequestEngine(WizardWrx.HTTP.RequestOptions)
      type: Constructor
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../HTTP/RequestEngine.cs
        startLine: 139
      summary: "\nThis constructor initializes the Options property, which is not\nstrictly required for most use cases, and can be passed in as a\nRequestOptions constructor call, since the RequestOptions object is\na POCO that doesn&apos;t even need System; its only reference is to the\nWizardWrx.DLLConfigurationManager namespace, where the \nExceptionLogger lives.\n"
      example: []
      syntax:
        content:
          CSharp: public RequestEngine(RequestOptions pobjRequestOptions)
          VB: Public Sub New(pobjRequestOptions As RequestOptions)
        parameters:
        - id: pobjRequestOptions
          type: WizardWrx.HTTP.RequestOptions
          description: "\nWhen supplied, the RequestOptions object reference is stored in the\nOptions property, providing a read-only reference to everything but\nthe OAuth Access Token, which must, of course, be updateable because\naccess tokens are short-lived.\n"
      overload: WizardWrx.HTTP.RequestEngine.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
    - id: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
      commentId: M:WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
      language: CSharp
      name:
        CSharp: CallWebApiAndProcessResultASync(String, Action<JObject>, JSON_Deserialized_Object, RequestEngine.HttpVerb, Boolean)
        VB: CallWebApiAndProcessResultASync(String, Action(Of JObject), JSON_Deserialized_Object, RequestEngine.HttpVerb, Boolean)
      nameWithType:
        CSharp: RequestEngine.CallWebApiAndProcessResultASync(String, Action<JObject>, JSON_Deserialized_Object, RequestEngine.HttpVerb, Boolean)
        VB: RequestEngine.CallWebApiAndProcessResultASync(String, Action(Of JObject), JSON_Deserialized_Object, RequestEngine.HttpVerb, Boolean)
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String, System.Action<Newtonsoft.Json.Linq.JObject>, WizardWrx.JsonSupport.JSON_Deserialized_Object, WizardWrx.HTTP.RequestEngine.HttpVerb, System.Boolean)
        VB: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String, System.Action(Of Newtonsoft.Json.Linq.JObject), WizardWrx.JsonSupport.JSON_Deserialized_Object, WizardWrx.HTTP.RequestEngine.HttpVerb, System.Boolean)
      type: Method
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CallWebApiAndProcessResultASync
        path: ../HTTP/RequestEngine.cs
        startLine: 235
      summary: "\nCalls the protected web API and processes the result\n"
      example: []
      syntax:
        content:
          CSharp: public object CallWebApiAndProcessResultASync(string pstrWebApiUrl, Action<JObject> pfunProcessResultCallback = null, JSON_Deserialized_Object pjSON_Deserialized = null, RequestEngine.HttpVerb penmVerb = RequestEngine.HttpVerb.POST, bool pfExpectJSON = true)
          VB: Public Function CallWebApiAndProcessResultASync(pstrWebApiUrl As String, pfunProcessResultCallback As Action(Of JObject) = Nothing, pjSON_Deserialized As JSON_Deserialized_Object = Nothing, penmVerb As RequestEngine.HttpVerb = RequestEngine.HttpVerb.POST, pfExpectJSON As Boolean = True) As Object
        parameters:
        - id: pstrWebApiUrl
          type: System.String
          description: "\nURL of the web API to call (supposed to return Json)\n"
        - id: pfunProcessResultCallback
          type: System.Action{Newtonsoft.Json.Linq.JObject}
          description: "\n<p>\nThis optional Action JObject reference points to the callback used\nto process the result of the call to the web API. This was intended\nfor processing JSON objects of assorted arbitrary shapes returned by\nvarious Microsoft Graph endpoints.\n</p>\n<p>\nNow that the routine is evolving to meet additional needs, it seemed\nwiser to abandon the void return type in favor of returning the\nresult in a more conventional fashion.\n</p>\n<p>\nThough it now returns in a more &quot;natural&quot; way, the callback remains\nrelevant for the Graph API because it allows a single entry point to\nprocess \n</p>\n"
        - id: pjSON_Deserialized
          type: WizardWrx.JsonSupport.JSON_Deserialized_Object
          description: "\n<p>\nWhen present, the optional JSON_Deserialized_Object object is\nsyntactic sugar sprinkled over a standard string that is expected to\nhave passed a very cursory validity check.\n</p>\n<p>\nThis string is the payload required of all POST nad PUT requests.\n</p>\n<p>\nWhen this value is null, unless <code data-dev-comment-type=\"paramref\" class=\"paramref\">penmVerb</code> happens\nto be DELETE, the verb is coerced to GET, the only other verb that\ndispenses with a payload.\n</p>\n"
        - id: penmVerb
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
          description: "\n<p>\nThis optional member of the HttpVerb enumeration identifies the HTT\nverb to emit for the request. For backwards compatibility, its value\ndefaults to POST.\n</p>\n<p>\nWhen <code data-dev-comment-type=\"paramref\" class=\"paramref\">pjSON_Deserialized</code> is null, unless this value\nis DELETE, the verb is coerced to GET, the only other verb that can\nfunction without a payload.\n</p>\n"
        - id: pfExpectJSON
          type: System.Boolean
          description: "\nBy default, we expect the returned string to be JSON, and use the\nJsonConvert.DeserializeObject method to return it as a JObject.\nOtherwise, the raw string is returned as is.\n"
        return:
          type: System.Object
          description: "\n<p>\nThis routine was originally intended for use as part of a library\nthat interacts with the Microsoft Graph API and expects to return an\narbitrarily shaped object as a JObject, which it passed to the\ncallback routine <code data-dev-comment-type=\"paramref\" class=\"paramref\">pfunProcessResultCallback</code>.\n</p>\n<p>\nSince it is being pulled out of that class library so that it can be\napplied elsewhere, it is changed so that it returns whichever object\nit has, either JSON or an ordinary string. Hence, altough the return\ntype is specified as object, in practice, it is either a JObject or\na garden variety String.\n</p>\n"
      overload: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync*
      exceptions:
      - type: System.Exception
        commentId: T:System.Exception
        description: "\nTwo distinct circumstances can give rise to an Exception exception.\n<ol><li></li><li></li></ol>\n"
      - type: System.ComponentModel.InvalidEnumArgumentException
        commentId: T:System.ComponentModel.InvalidEnumArgumentException
        description: "\nAn InvalidEnumArgumentException Exception arises when the value of\noptional argument <code data-dev-comment-type=\"paramref\" class=\"paramref\">penmVerb</code> is invalid, ordinarily\ndue to a programming error, since it is reasonbly safe to assume\nthat the verb is always hard coded per toe API.\n"
      modifiers:
        CSharp:
        - public
        VB:
        - Public
      references:
        System.Exception: 
        System.ComponentModel.InvalidEnumArgumentException: 
    - id: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary(System.Uri)
      commentId: M:WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary(System.Uri)
      language: CSharp
      name:
        CSharp: ParseQueryToDictionary(Uri)
        VB: ParseQueryToDictionary(Uri)
      nameWithType:
        CSharp: RequestEngine.ParseQueryToDictionary(Uri)
        VB: RequestEngine.ParseQueryToDictionary(Uri)
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary(System.Uri)
        VB: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary(System.Uri)
      type: Method
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: ParseQueryToDictionary
        path: ../HTTP/RequestEngine.cs
        startLine: 360
      summary: "\nExtract the Query from any URI into an IDictionary of name-value\npairs that can be URL encoded properly.\n"
      example: []
      syntax:
        content:
          CSharp: public static IDictionary<string, string> ParseQueryToDictionary(Uri puriToParse)
          VB: Public Shared Function ParseQueryToDictionary(puriToParse As Uri) As IDictionary(Of String, String)
        parameters:
        - id: puriToParse
          type: System.Uri
          description: "\nPass in a reference to the System.Uri to process.\n"
        return:
          type: System.Collections.Generic.IDictionary{System.String,System.String}
          description: "\nThe return value is the IDictionary populated with keys for each\nnamed value in the Query of <code data-dev-comment-type=\"paramref\" class=\"paramref\">puriToParse</code>. If the query is\nempty or absent, the returned dictionary is empty.\n"
      overload: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
  - id: WizardWrx.HTTP.RequestEngine.HttpVerb
    commentId: T:WizardWrx.HTTP.RequestEngine.HttpVerb
    language: CSharp
    name:
      CSharp: RequestEngine.HttpVerb
      VB: RequestEngine.HttpVerb
    nameWithType:
      CSharp: RequestEngine.HttpVerb
      VB: RequestEngine.HttpVerb
    qualifiedName:
      CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb
      VB: WizardWrx.HTTP.RequestEngine.HttpVerb
    type: Enum
    assemblies:
    - WizardWrx.HTTP
    namespace: WizardWrx.HTTP
    source:
      remote:
        path: HTTP/RequestEngine.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: HttpVerb
      path: ../HTTP/RequestEngine.cs
      startLine: 76
    summary: "\nThe HttpVerb enumeration identifies the HTTP verbs supported by the\nHttpClient object, and establishes which HttpClienti instance method\nCallWebApiAndProcessResultASync invokes.\n"
    example: []
    syntax:
      content:
        CSharp: public enum HttpVerb
        VB: Public Enum HttpVerb
    extensionMethods:
    - WizardWrx.HTTP.RequestEngine.HttpVerb.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
    modifiers:
      CSharp:
      - public
      - enum
      VB:
      - Public
      - Enum
    items:
    - id: WizardWrx.HTTP.RequestEngine.HttpVerb.GET
      commentId: F:WizardWrx.HTTP.RequestEngine.HttpVerb.GET
      language: CSharp
      name:
        CSharp: GET
        VB: GET
      nameWithType:
        CSharp: RequestEngine.HttpVerb.GET
        VB: RequestEngine.HttpVerb.GET
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb.GET
        VB: WizardWrx.HTTP.RequestEngine.HttpVerb.GET
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: GET
        path: ../HTTP/RequestEngine.cs
        startLine: 81
      summary: "\nThis value maps to the most basic verb, GET.\n"
      example: []
      syntax:
        content:
          CSharp: GET = 0
          VB: GET = 0
        return:
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.HTTP.RequestEngine.HttpVerb.PATCH
      commentId: F:WizardWrx.HTTP.RequestEngine.HttpVerb.PATCH
      language: CSharp
      name:
        CSharp: PATCH
        VB: PATCH
      nameWithType:
        CSharp: RequestEngine.HttpVerb.PATCH
        VB: RequestEngine.HttpVerb.PATCH
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb.PATCH
        VB: WizardWrx.HTTP.RequestEngine.HttpVerb.PATCH
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: PATCH
        path: ../HTTP/RequestEngine.cs
        startLine: 87
      summary: "\nThis value maps to the increasingly popular PATCH verb, usually used to\nupdate selected columns of a row in a database table.\n"
      example: []
      syntax:
        content:
          CSharp: PATCH = 1
          VB: PATCH = 1
        return:
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.HTTP.RequestEngine.HttpVerb.POST
      commentId: F:WizardWrx.HTTP.RequestEngine.HttpVerb.POST
      language: CSharp
      name:
        CSharp: POST
        VB: POST
      nameWithType:
        CSharp: RequestEngine.HttpVerb.POST
        VB: RequestEngine.HttpVerb.POST
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb.POST
        VB: WizardWrx.HTTP.RequestEngine.HttpVerb.POST
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: POST
        path: ../HTTP/RequestEngine.cs
        startLine: 92
      summary: "\nThis value maps to the most commonly used verb after GET.\n"
      example: []
      syntax:
        content:
          CSharp: POST = 2
          VB: POST = 2
        return:
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.HTTP.RequestEngine.HttpVerb.PUT
      commentId: F:WizardWrx.HTTP.RequestEngine.HttpVerb.PUT
      language: CSharp
      name:
        CSharp: PUT
        VB: PUT
      nameWithType:
        CSharp: RequestEngine.HttpVerb.PUT
        VB: RequestEngine.HttpVerb.PUT
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb.PUT
        VB: WizardWrx.HTTP.RequestEngine.HttpVerb.PUT
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: PUT
        path: ../HTTP/RequestEngine.cs
        startLine: 98
      summary: "\nThis value maps to the infrequently used PUT verb, usually used\nto update selected columns of a row in a database table.\n"
      example: []
      syntax:
        content:
          CSharp: PUT = 3
          VB: PUT = 3
        return:
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: WizardWrx.HTTP.RequestEngine.HttpVerb.DELETE
      commentId: F:WizardWrx.HTTP.RequestEngine.HttpVerb.DELETE
      language: CSharp
      name:
        CSharp: DELETE
        VB: DELETE
      nameWithType:
        CSharp: RequestEngine.HttpVerb.DELETE
        VB: RequestEngine.HttpVerb.DELETE
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestEngine.HttpVerb.DELETE
        VB: WizardWrx.HTTP.RequestEngine.HttpVerb.DELETE
      type: Field
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestEngine.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: DELETE
        path: ../HTTP/RequestEngine.cs
        startLine: 104
      summary: "\nThis value maps to the dangerous DELETE verb, and is usually \nsent without a payload.\n"
      example: []
      syntax:
        content:
          CSharp: DELETE = 4
          VB: DELETE = 4
        return:
          type: WizardWrx.HTTP.RequestEngine.HttpVerb
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
  - id: WizardWrx.HTTP.RequestOptions
    commentId: T:WizardWrx.HTTP.RequestOptions
    language: CSharp
    name:
      CSharp: RequestOptions
      VB: RequestOptions
    nameWithType:
      CSharp: RequestOptions
      VB: RequestOptions
    qualifiedName:
      CSharp: WizardWrx.HTTP.RequestOptions
      VB: WizardWrx.HTTP.RequestOptions
    type: Class
    assemblies:
    - WizardWrx.HTTP
    namespace: WizardWrx.HTTP
    source:
      remote:
        path: HTTP/RequestOptions.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: RequestOptions
      path: ../HTTP/RequestOptions.cs
      startLine: 10
    summary: "\nInstances of this object represent options for configuring request\nbehavior, including provision of exception logging tooling and OAuth\ntoken retrieval methods.\n"
    example: []
    syntax:
      content:
        CSharp: public sealed class RequestOptions
        VB: Public NotInheritable Class RequestOptions
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
      - sealed
      - class
      VB:
      - Public
      - NotInheritable
      - Class
    items:
    - id: WizardWrx.HTTP.RequestOptions.Logger
      commentId: P:WizardWrx.HTTP.RequestOptions.Logger
      language: CSharp
      name:
        CSharp: Logger
        VB: Logger
      nameWithType:
        CSharp: RequestOptions.Logger
        VB: RequestOptions.Logger
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestOptions.Logger
        VB: WizardWrx.HTTP.RequestOptions.Logger
      type: Property
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestOptions.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: Logger
        path: ../HTTP/RequestOptions.cs
        startLine: 42
      summary: "\nThis read-only property returns an instance of ExceptionLogger for\nlogging exceptions encountered during the processing of HTTP\nrequests.\n"
      example: []
      syntax:
        content:
          CSharp: public ExceptionLogger Logger { get; }
          VB: Public ReadOnly Property Logger As ExceptionLogger
        parameters: []
        return:
          type: WizardWrx.DLLConfigurationManager.ExceptionLogger
      overload: WizardWrx.HTTP.RequestOptions.Logger*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.HTTP.RequestOptions.TokenGetter
      commentId: P:WizardWrx.HTTP.RequestOptions.TokenGetter
      language: CSharp
      name:
        CSharp: TokenGetter
        VB: TokenGetter
      nameWithType:
        CSharp: RequestOptions.TokenGetter
        VB: RequestOptions.TokenGetter
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestOptions.TokenGetter
        VB: WizardWrx.HTTP.RequestOptions.TokenGetter
      type: Property
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestOptions.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: TokenGetter
        path: ../HTTP/RequestOptions.cs
        startLine: 49
      summary: "\nThis read-only property returns a delegate that retrieves OAuth Bearer\ntokens for inclusion in the Authorization header of HTTP requests.\n"
      example: []
      syntax:
        content:
          CSharp: public RequestOptions.OAuthTokenGetter TokenGetter { get; }
          VB: Public ReadOnly Property TokenGetter As RequestOptions.OAuthTokenGetter
        parameters: []
        return:
          type: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
      overload: WizardWrx.HTTP.RequestOptions.TokenGetter*
      modifiers:
        CSharp:
        - public
        - get
        VB:
        - Public
        - ReadOnly
    - id: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
      commentId: P:WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
      language: CSharp
      name:
        CSharp: CurrentOAuthToken
        VB: CurrentOAuthToken
      nameWithType:
        CSharp: RequestOptions.CurrentOAuthToken
        VB: RequestOptions.CurrentOAuthToken
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
        VB: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
      type: Property
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestOptions.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: CurrentOAuthToken
        path: ../HTTP/RequestOptions.cs
        startLine: 56
      summary: "\nThis read-only string property gets the current OAuth token,\nretrieving and caching it if necessary.\n"
      example: []
      syntax:
        content:
          CSharp: public string CurrentOAuthToken { get; set; }
          VB: Public Property CurrentOAuthToken As String
        parameters: []
        return:
          type: System.String
      overload: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken*
      modifiers:
        CSharp:
        - public
        - get
        - set
        VB:
        - Public
    - id: WizardWrx.HTTP.RequestOptions.#ctor(WizardWrx.DLLConfigurationManager.ExceptionLogger,WizardWrx.HTTP.RequestOptions.OAuthTokenGetter,System.String)
      commentId: M:WizardWrx.HTTP.RequestOptions.#ctor(WizardWrx.DLLConfigurationManager.ExceptionLogger,WizardWrx.HTTP.RequestOptions.OAuthTokenGetter,System.String)
      language: CSharp
      name:
        CSharp: RequestOptions(ExceptionLogger, RequestOptions.OAuthTokenGetter, String)
        VB: RequestOptions(ExceptionLogger, RequestOptions.OAuthTokenGetter, String)
      nameWithType:
        CSharp: RequestOptions.RequestOptions(ExceptionLogger, RequestOptions.OAuthTokenGetter, String)
        VB: RequestOptions.RequestOptions(ExceptionLogger, RequestOptions.OAuthTokenGetter, String)
      qualifiedName:
        CSharp: WizardWrx.HTTP.RequestOptions.RequestOptions(WizardWrx.DLLConfigurationManager.ExceptionLogger, WizardWrx.HTTP.RequestOptions.OAuthTokenGetter, System.String)
        VB: WizardWrx.HTTP.RequestOptions.RequestOptions(WizardWrx.DLLConfigurationManager.ExceptionLogger, WizardWrx.HTTP.RequestOptions.OAuthTokenGetter, System.String)
      type: Constructor
      assemblies:
      - WizardWrx.HTTP
      namespace: WizardWrx.HTTP
      source:
        remote:
          path: HTTP/RequestOptions.cs
          branch: master
          repo: https://github.com/txwizard/WizardWrx_NET_API.git
        id: .ctor
        path: ../HTTP/RequestOptions.cs
        startLine: 107
      summary: "\nThe public constructor initializes both read-only properties of this\nclass, so that consumers of this class must make their choices about\nthese properties explicit.\n"
      example: []
      syntax:
        content:
          CSharp: public RequestOptions(ExceptionLogger plogger, RequestOptions.OAuthTokenGetter ptokenGetter, string pstrInitialOAuthToken = null)
          VB: Public Sub New(plogger As ExceptionLogger, ptokenGetter As RequestOptions.OAuthTokenGetter, pstrInitialOAuthToken As String = Nothing)
        parameters:
        - id: plogger
          type: WizardWrx.DLLConfigurationManager.ExceptionLogger
          description: "\nThe ExceptionLogger instance to be used for logging Exceptions is\noptional; if no object is supplied, no Exception logging happens\ninside the other methods exposed by this library.\n"
        - id: ptokenGetter
          type: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
          description: "\nThe delegate that retrieves OAuth Bearer tokens for inclusion in the\nAuthorization header of HTTP requests is optional; if no delegate is\nprovided, requests proceed without Authorization headers.\n"
        - id: pstrInitialOAuthToken
          type: System.String
          description: "\nThis optional parameter allows the caller to supply an initial OAuth\ntoken that the class caches for future use. If this parameter is \nnull and <code data-dev-comment-type=\"paramref\" class=\"paramref\">ptokenGetter</code> is not null, the class \nattempts to obtain a token the first time the property value is\nqueried.\n"
      overload: WizardWrx.HTTP.RequestOptions.#ctor*
      modifiers:
        CSharp:
        - public
        VB:
        - Public
  - id: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
    commentId: T:WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
    language: CSharp
    name:
      CSharp: RequestOptions.OAuthTokenGetter
      VB: RequestOptions.OAuthTokenGetter
    nameWithType:
      CSharp: RequestOptions.OAuthTokenGetter
      VB: RequestOptions.OAuthTokenGetter
    qualifiedName:
      CSharp: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
      VB: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
    type: Delegate
    assemblies:
    - WizardWrx.HTTP
    namespace: WizardWrx.HTTP
    source:
      remote:
        path: HTTP/RequestOptions.cs
        branch: master
        repo: https://github.com/txwizard/WizardWrx_NET_API.git
      id: OAuthTokenGetter
      path: ../HTTP/RequestOptions.cs
      startLine: 32
    summary: "\nThis delegate signature represents a method that attempts to obtain an\nOAuth token and return it through an output parameter, along with the\nexisting true/false outcome.\n"
    example: []
    syntax:
      content:
        CSharp: public delegate bool OAuthTokenGetter(out string token, ExceptionLogger logger = null);
        VB: Public Delegate Function OAuthTokenGetter(ByRef token As String, logger As ExceptionLogger = Nothing) As Boolean
      parameters:
      - id: token
        type: System.String
        description: "\nWhen this method returns, contains the retrieved OAuth token if\nsuccessful; otherwise, null.\n"
      - id: logger
        type: WizardWrx.DLLConfigurationManager.ExceptionLogger
        description: "\nThis represents the optional logger object for recording exceptions\nthat arise during token retrieval.\n"
      return:
        type: System.Boolean
        description: "\nThe delegated function returns true if the token was successfully\nobtained; otherwise, it returns false and the <code data-dev-comment-type=\"paramref\" class=\"paramref\">token</code>\nvalue is undefined. The supplied copy of the access token is for use\nby the HTTP access method; the object that exposes the OAuth Access\nToken Getter is responsible for stashing a copy in protectd storage.\n"
    modifiers:
      CSharp:
      - public
      - delegate
      VB:
      - Public
      - Delegate
    items: []
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
  ? WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
  : commentId: M:WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync(System.String,System.Action{Newtonsoft.Json.Linq.JObject},WizardWrx.JsonSupport.JSON_Deserialized_Object,WizardWrx.HTTP.RequestEngine.HttpVerb,System.Boolean)
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
  WizardWrx.HTTP:
    name:
      CSharp:
      - name: WizardWrx.HTTP
        nameWithType: WizardWrx.HTTP
        qualifiedName: WizardWrx.HTTP
      VB:
      - name: WizardWrx.HTTP
        nameWithType: WizardWrx.HTTP
        qualifiedName: WizardWrx.HTTP
    isDefinition: true
    commentId: N:WizardWrx.HTTP
  WizardWrx.HTTP.RequestOptions:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions
        name: RequestOptions
        nameWithType: RequestOptions
        qualifiedName: WizardWrx.HTTP.RequestOptions
      VB:
      - id: WizardWrx.HTTP.RequestOptions
        name: RequestOptions
        nameWithType: RequestOptions
        qualifiedName: WizardWrx.HTTP.RequestOptions
    isDefinition: true
    parent: WizardWrx.HTTP
    commentId: T:WizardWrx.HTTP.RequestOptions
  WizardWrx.HTTP.RequestEngine.Options*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine.Options*
        name: Options
        nameWithType: RequestEngine.Options
        qualifiedName: WizardWrx.HTTP.RequestEngine.Options
      VB:
      - id: WizardWrx.HTTP.RequestEngine.Options*
        name: Options
        nameWithType: RequestEngine.Options
        qualifiedName: WizardWrx.HTTP.RequestEngine.Options
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestEngine.Options
  WizardWrx.HTTP.RequestEngine.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine.#ctor*
        name: RequestEngine
        nameWithType: RequestEngine.RequestEngine
        qualifiedName: WizardWrx.HTTP.RequestEngine.RequestEngine
      VB:
      - id: WizardWrx.HTTP.RequestEngine.#ctor*
        name: RequestEngine
        nameWithType: RequestEngine.RequestEngine
        qualifiedName: WizardWrx.HTTP.RequestEngine.RequestEngine
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestEngine.#ctor
  System.Exception:
    commentId: T:System.Exception
  System.ComponentModel.InvalidEnumArgumentException:
    commentId: T:System.ComponentModel.InvalidEnumArgumentException
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
  System.Action`1:
    name:
      CSharp:
      - id: System.Action`1
        name: Action
        nameWithType: Action
        qualifiedName: System.Action
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
      - id: System.Action`1
        name: Action
        nameWithType: Action
        qualifiedName: System.Action
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
    commentId: T:System.Action`1
  System.Action{Newtonsoft.Json.Linq.JObject}:
    name:
      CSharp:
      - id: System.Action`1
        name: Action
        nameWithType: Action
        qualifiedName: System.Action
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - id: Newtonsoft.Json.Linq.JObject
        name: JObject
        nameWithType: JObject
        qualifiedName: Newtonsoft.Json.Linq.JObject
        isExternal: true
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.Action`1
        name: Action
        nameWithType: Action
        qualifiedName: System.Action
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - id: Newtonsoft.Json.Linq.JObject
        name: JObject
        nameWithType: JObject
        qualifiedName: Newtonsoft.Json.Linq.JObject
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.Action`1
    parent: System
    commentId: T:System.Action{Newtonsoft.Json.Linq.JObject}
  WizardWrx.JsonSupport:
    name:
      CSharp:
      - name: WizardWrx.JsonSupport
        nameWithType: WizardWrx.JsonSupport
        qualifiedName: WizardWrx.JsonSupport
      VB:
      - name: WizardWrx.JsonSupport
        nameWithType: WizardWrx.JsonSupport
        qualifiedName: WizardWrx.JsonSupport
    isDefinition: true
    commentId: N:WizardWrx.JsonSupport
  WizardWrx.JsonSupport.JSON_Deserialized_Object:
    name:
      CSharp:
      - id: WizardWrx.JsonSupport.JSON_Deserialized_Object
        name: JSON_Deserialized_Object
        nameWithType: JSON_Deserialized_Object
        qualifiedName: WizardWrx.JsonSupport.JSON_Deserialized_Object
      VB:
      - id: WizardWrx.JsonSupport.JSON_Deserialized_Object
        name: JSON_Deserialized_Object
        nameWithType: JSON_Deserialized_Object
        qualifiedName: WizardWrx.JsonSupport.JSON_Deserialized_Object
    isDefinition: true
    parent: WizardWrx.JsonSupport
    commentId: T:WizardWrx.JsonSupport.JSON_Deserialized_Object
  WizardWrx.HTTP.RequestEngine.HttpVerb:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine.HttpVerb
        name: RequestEngine.HttpVerb
        nameWithType: RequestEngine.HttpVerb
        qualifiedName: WizardWrx.HTTP.RequestEngine.HttpVerb
      VB:
      - id: WizardWrx.HTTP.RequestEngine.HttpVerb
        name: RequestEngine.HttpVerb
        nameWithType: RequestEngine.HttpVerb
        qualifiedName: WizardWrx.HTTP.RequestEngine.HttpVerb
    isDefinition: true
    parent: WizardWrx.HTTP
    commentId: T:WizardWrx.HTTP.RequestEngine.HttpVerb
  WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync*
        name: CallWebApiAndProcessResultASync
        nameWithType: RequestEngine.CallWebApiAndProcessResultASync
        qualifiedName: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync
      VB:
      - id: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync*
        name: CallWebApiAndProcessResultASync
        nameWithType: RequestEngine.CallWebApiAndProcessResultASync
        qualifiedName: WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestEngine.CallWebApiAndProcessResultASync
  System.Collections.Generic.IDictionary`2:
    name:
      CSharp:
      - id: System.Collections.Generic.IDictionary`2
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.Generic.IDictionary
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
      - name: TKey
        nameWithType: TKey
        qualifiedName: TKey
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - name: TValue
        nameWithType: TValue
        qualifiedName: TValue
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.Collections.Generic.IDictionary`2
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.Generic.IDictionary
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
      - name: TKey
        nameWithType: TKey
        qualifiedName: TKey
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - name: TValue
        nameWithType: TValue
        qualifiedName: TValue
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    commentId: T:System.Collections.Generic.IDictionary`2
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
  System.Collections.Generic.IDictionary{System.String,System.String}:
    name:
      CSharp:
      - id: System.Collections.Generic.IDictionary`2
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.Generic.IDictionary
        isExternal: true
      - name: <
        nameWithType: <
        qualifiedName: <
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
      - name: '>'
        nameWithType: '>'
        qualifiedName: '>'
      VB:
      - id: System.Collections.Generic.IDictionary`2
        name: IDictionary
        nameWithType: IDictionary
        qualifiedName: System.Collections.Generic.IDictionary
        isExternal: true
      - name: '(Of '
        nameWithType: '(Of '
        qualifiedName: '(Of '
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
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: false
    definition: System.Collections.Generic.IDictionary`2
    parent: System.Collections.Generic
    commentId: T:System.Collections.Generic.IDictionary{System.String,System.String}
  System.Uri:
    name:
      CSharp:
      - id: System.Uri
        name: Uri
        nameWithType: Uri
        qualifiedName: System.Uri
        isExternal: true
      VB:
      - id: System.Uri
        name: Uri
        nameWithType: Uri
        qualifiedName: System.Uri
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Uri
  WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary*
        name: ParseQueryToDictionary
        nameWithType: RequestEngine.ParseQueryToDictionary
        qualifiedName: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary
      VB:
      - id: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary*
        name: ParseQueryToDictionary
        nameWithType: RequestEngine.ParseQueryToDictionary
        qualifiedName: WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestEngine.ParseQueryToDictionary
  WizardWrx.HTTP.RequestEngine:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestEngine
        name: RequestEngine
        nameWithType: RequestEngine
        qualifiedName: WizardWrx.HTTP.RequestEngine
      VB:
      - id: WizardWrx.HTTP.RequestEngine
        name: RequestEngine
        nameWithType: RequestEngine
        qualifiedName: WizardWrx.HTTP.RequestEngine
    isDefinition: true
    commentId: T:WizardWrx.HTTP.RequestEngine
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
  ? WizardWrx.HTTP.RequestEngine.HttpVerb.WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
  : name:
      CSharp:
      - id: WizardWrx.StringExtensions.RenderEvenWhenNull``1(System.String,System.String,System.IFormatProvider)
        name: RenderEvenWhenNull<RequestEngine.HttpVerb>
        nameWithType: StringExtensions.RenderEvenWhenNull<RequestEngine.HttpVerb>
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull<WizardWrx.HTTP.RequestEngine.HttpVerb>
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
        name: RenderEvenWhenNull(Of RequestEngine.HttpVerb)
        nameWithType: StringExtensions.RenderEvenWhenNull(Of RequestEngine.HttpVerb)
        qualifiedName: WizardWrx.StringExtensions.RenderEvenWhenNull(Of WizardWrx.HTTP.RequestEngine.HttpVerb)
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
  WizardWrx.DLLConfigurationManager:
    name:
      CSharp:
      - name: WizardWrx.DLLConfigurationManager
        nameWithType: WizardWrx.DLLConfigurationManager
        qualifiedName: WizardWrx.DLLConfigurationManager
      VB:
      - name: WizardWrx.DLLConfigurationManager
        nameWithType: WizardWrx.DLLConfigurationManager
        qualifiedName: WizardWrx.DLLConfigurationManager
    isDefinition: true
    commentId: N:WizardWrx.DLLConfigurationManager
  WizardWrx.DLLConfigurationManager.ExceptionLogger:
    name:
      CSharp:
      - id: WizardWrx.DLLConfigurationManager.ExceptionLogger
        name: ExceptionLogger
        nameWithType: ExceptionLogger
        qualifiedName: WizardWrx.DLLConfigurationManager.ExceptionLogger
      VB:
      - id: WizardWrx.DLLConfigurationManager.ExceptionLogger
        name: ExceptionLogger
        nameWithType: ExceptionLogger
        qualifiedName: WizardWrx.DLLConfigurationManager.ExceptionLogger
    isDefinition: true
    parent: WizardWrx.DLLConfigurationManager
    commentId: T:WizardWrx.DLLConfigurationManager.ExceptionLogger
  WizardWrx.HTTP.RequestOptions.Logger*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions.Logger*
        name: Logger
        nameWithType: RequestOptions.Logger
        qualifiedName: WizardWrx.HTTP.RequestOptions.Logger
      VB:
      - id: WizardWrx.HTTP.RequestOptions.Logger*
        name: Logger
        nameWithType: RequestOptions.Logger
        qualifiedName: WizardWrx.HTTP.RequestOptions.Logger
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestOptions.Logger
  WizardWrx.HTTP.RequestOptions.OAuthTokenGetter:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
        name: RequestOptions.OAuthTokenGetter
        nameWithType: RequestOptions.OAuthTokenGetter
        qualifiedName: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
      VB:
      - id: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
        name: RequestOptions.OAuthTokenGetter
        nameWithType: RequestOptions.OAuthTokenGetter
        qualifiedName: WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
    isDefinition: true
    parent: WizardWrx.HTTP
    commentId: T:WizardWrx.HTTP.RequestOptions.OAuthTokenGetter
  WizardWrx.HTTP.RequestOptions.TokenGetter*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions.TokenGetter*
        name: TokenGetter
        nameWithType: RequestOptions.TokenGetter
        qualifiedName: WizardWrx.HTTP.RequestOptions.TokenGetter
      VB:
      - id: WizardWrx.HTTP.RequestOptions.TokenGetter*
        name: TokenGetter
        nameWithType: RequestOptions.TokenGetter
        qualifiedName: WizardWrx.HTTP.RequestOptions.TokenGetter
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestOptions.TokenGetter
  WizardWrx.HTTP.RequestOptions.CurrentOAuthToken*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken*
        name: CurrentOAuthToken
        nameWithType: RequestOptions.CurrentOAuthToken
        qualifiedName: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
      VB:
      - id: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken*
        name: CurrentOAuthToken
        nameWithType: RequestOptions.CurrentOAuthToken
        qualifiedName: WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestOptions.CurrentOAuthToken
  WizardWrx.HTTP.RequestOptions.#ctor*:
    name:
      CSharp:
      - id: WizardWrx.HTTP.RequestOptions.#ctor*
        name: RequestOptions
        nameWithType: RequestOptions.RequestOptions
        qualifiedName: WizardWrx.HTTP.RequestOptions.RequestOptions
      VB:
      - id: WizardWrx.HTTP.RequestOptions.#ctor*
        name: RequestOptions
        nameWithType: RequestOptions.RequestOptions
        qualifiedName: WizardWrx.HTTP.RequestOptions.RequestOptions
    isDefinition: true
    commentId: Overload:WizardWrx.HTTP.RequestOptions.#ctor
