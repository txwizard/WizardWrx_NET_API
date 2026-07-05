/*eslint-env browser*/
/*global $ _dbname _dbnameSource _domainid _domainidSource _domainname _domainnameSource _fCallRulesEngineOnSubmit _fDebugLogging _fDomainAndTenantIDAreSafe _leadid _leadidSource _LeadLifeJSHelpers _llAppPath _login _pagename _tenantid _tenantidSource _userid _useridSource _loginSource _UpdateIfChanged AddOrRemoveCssSelector ARRAY_FIFTH_ELEMENT ARRAY_FIRST_ELEMENT ARRAY_FOURTH_ELEMENT ARRAY_INVALID_INDEX ARRAY_IS_EMPTY ARRAY_NOT_EMPTY ARRAY_SECOND_ELEMENT ARRAY_SIXTH_ELEMENT ARRAY_THIRD_ELEMENT ASTERISK_CHAR CHARACTER_ZERO CSS_SELECTOR_ADD CSV_SEPARATOR_CHAR DBNULL DEFAULT_DATE_SEPARATOR_CHAR DECIMAL_POINT EMPTY_STRING EMPTY_STRING_LENGTH EQUALS_CHAR EnableRulesEngineOnSubmit FULL_STOP GetLeadIdFromQueryString GetNameOfCurrentFunction GetParameterFromURLFormOrLocalStorage HostIsPurl HTML5_DATE_SEPARATOR_CHAR INDEXOF_NOT_FOUND JQUERY_SELECTOR_IS_CLASSNAME JQUERY_SELECTOR_IS_ELEMENT_ID KEY_VALUE_PAIR_IS_VALUE LLCommon LOGICAL_NEGATE MINIMUM_STT_ENTITY_ID NEXT_CHARACTER NO_LEAD_ID NUMERIC_ZERO PATH_PROTOCOL_DELIMITER PATH_SEPARATOR_CHAR PIPE_CHAR PIPE_CHAR_SPLIT_MATCH QUERY_STRING_START_DELIMITER QUOTE_DOUBLE ScriptInfoForLog SPLIT_NAME_FROM_VALUE SINGLE_CHARACTER SRC_IS_LLJS_HELPERS_SYNC SRC_IS_UNKNOWN SUBSTRING_FIRST_CHAR SUBSTRING_SECOND_CHARACTER TIME_SEPARATOR_CHAR URLParameterFromQueryString WINDOWS_PATH_SEPARATOR_CHAR*/
"use strict";

const LeadLifeJSHelpers_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const LeadLifeJSHelpers_Version      = 1.381;
const LeadLifeJSHelpers_LastUpdated  = '2026/01/06 03:59:51 CDT';

console.log ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                 LeadLifeJSHelpers_Version ,
                                 LeadLifeJSHelpers_LastUpdated ,
                                 'loading' ) );

/*
    ============================================================================

    Name:               LeadLifeJSHelpersLib.js

    Goal:               Define custom JavaScript functions used by the
                        STT_Video_Player and other 2KnowWho modules as methods
                        on a class.

    Dependencies:       The code defined in this module requires a working
                        JQuery object, which the calling page is expected to
                        supply.

    Remarks:            This object is intended to be declared with Window scope
                        and treated as a source of methods that might otherwise
                        be declared as static.

                        The callback functions that catch the Ajax events must
                        use hard coded literals because they cannot see the
                        object that contains them.

                        Passing a blank lead ID is harmless, since REST method
                        CreateABehavior adjusts its behavior based on the length
                        of the string.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       By Remark/Brief Description
    ---------- -- --------------------------------------------------------------
    2021/09/25 DG MVP: Working functions extracted from STT_VideoPlayer.HTML

    2022/03/11 DG Define a new method, IsKeyInDictionary and redefine the
                  entire object as an official class entity.

    2022/03/22 DG 1) Untabify the message in TrackEvent so that the second
                     line aligns vertically with the first.

                  2) Define a new Origin property that exposes a string
                     constructed by appending the protocol and hostname
                     properties on the window.location object, inserting the
                     standard delimiter, ://, between them.

                  3) Amend GetSTTDomainNameFromLocation to work with the current
                     crop of path strings.

    2022/06/08 DG 1) Correct the end comment on the constructor.

                  2) AddProtocolWhenMissing: Make test for servername "purl"
                     case insensitive.

                  3) ComputeEngagementTime: Add Page Hidden and URLClicked
                     to the list of events for which to compute engagement
                     time.

    2022/07/05 DG 1) Add numerous constants.

                  2) IsEmailAddressValid and GetSTTDatabaseNameFromLocation
                     methods are new.

                  3) Adjust capitalization of GetSTTDomainNameFromLocation
                     to agree with that of the function in the video player
                     code-behind that it replaced.

    2022/07/06 DG Define a new ProcessLandingPageForm method, which returns
                  the value returned by the CreateABehavior method that is
                  called on the server.

    2022/07/08 DG Make the two methods that parse things from the URL more
                  robust. The affected methods are:

                  - GetSTTDomainNameFromLocation
                  - GetSTTDatabaseNameFromLocation

    2022/07/09 DG Define CARRIAGE_RETURN_CHAR, LINE_FEED_CHAR, and TAB_CHAR.

    2022/07/10 DG Define SPACE_URLENCODED.

    2022/07/14 DG Define TODAY as a public property, along with two methods, one
                         of which uses the public property.

                         New Methods:

                         - GetElementByName
                         - GatherAdditionalFieldValues
                         - IdentifyContainingForm
                         - SubmitForm
                         - UTCMidnightToday

    2022/07/15 DG Constructor: Emit the version number in a console log message.

                  GatherAdditionalFieldValues: Move the list of controls to skip
                                               into Controls2Skip, and use the
                                               control name as the key when the
                                               ID of the control is is undefined
                                               (the empty string).

                  GetElementByName: Use array LeadColNames2CustomFieldNames to
                                    map incoming input control names to the
                                    names of the corresponding columns in the
                                    Lead table.

                  UTCMidnightToday: Use local midnight as a proxy for now.

    2022/07/18 DG Constructor: 1) Restore the value of SPACE_URLENCODED.

                               2) Eliminate the redundant CSV_SEPARATOR in favor
                                  of CSV_SEPARATOR_CHAR, named more consistently
                                  with similar public constants.

                               3) Eliminate the PURL_SERVER constant, which has
                                  no real value as an external symbol.

                               4) Implement a new STTContext constant, which is
                                  set in the ordinary course of things when the
                                  it calls GetSTTDatabaseNameFromLocation and
                                  GetSTTDomainNameFromLocation to initialize
                                  STTDatabaseName and STTDomainName.

                               5) Inplement PIPE_CHAR and PIPE_CHAR_SPLIT_MATCH
                                  constants.

                  GatherAdditionalFieldValues: Simplify this method to use an
                                               array that consumes less storage,
                                               eliminating a continue statement
                                               that was required to allow an IF
                                               expression to be stated in
                                               affirmative terms.

                  GetDomainTenant4LeadId: This new method takes an alternative
                                          route to extracting the lead ID from
                                          the query string, then uses it to get
                                          the domain name from the database.

                  GetElementByName: When duplicate element nams exist, return
                                    the first matching element.

                  GetSTTDatabaseNameFromLocation: This method becomes more
                                                  robust, and can in some cases
                                                  identify both the database
                                                  name and the domain name in
                                                  one method call, in which
                                                  case, the second call is
                                                  suppressed.

    2022/07/19 DG GatherAdditionalFieldValues: Check both the ID and Name
                                               attributes against the exclusion
                                               list.

                  GetElementByNameInContainer: GetElementByName, taking a second
                                               argument that identifies by name
                                               the container that will constrain
                                               the search.

                  PostEvent:  When the lead ID is available, which it will be in
                              the context of a Talking Point form submit or
                              other event, include it, so that it trumps the
                              email address.

                  SubmitForm: Append the lead ID to the list of fields when call
                              is in a Talking Point.

                  New elements in Controls2Skip array: ll_postToCRM

    2022/07/20 DG Move the document ready event handler that constructs and uses
                  the LeadLifeJSHelpers handler into this module, so that all
                  code required to implement form processing is self-contained.

    2022/07/27 DG GatherAdditionalFieldValues: Unless a new fAllowEmptyFields
                                               flag is TRUE (Its value is FALSE
                                               by default.), skip form controls
                                               that have blank values.

                  ProcessLandingPageForm: 1) Return a string representation of
                                             the outcome, and omit unnecessary
                                             log record of a successful outcome.

                                          2) Unless a new fAllowEmptyFields FLAG
                                             is TRUE (Its value is FALSE by
                                             default.), skip form controls that
                                             have blank values.

                  SubmitForm: Add reset, preventDefault and a confirmation
                              message in a dialog box, using the same text that
                              is displayed following a normal form submit.

    2022/08/01 DG GetVeryBasicLeadInfo4LeadId: This new method returns basic
                                               info about a lead given its Id.

                  VeryBasicLeadInfo:           This object is the value returned
                                               by GetVeryBasicLeadInfo4LeadId.

    2022/08/04 DG DisplayVersionAsAlert: This new method displays the version of
                                         the executing LeadLifeJSHelpers library
                                         in an Alert (MessageBox) window.

                  document.ready event:  Verify that the object is undefined
                                         before initializing it, and display an
                                         alert (in a message box) if it is.

    2022/08/21 DG GetFieldValue:         Search the array of values returned by
                                         method GetSelectedInfo4LeadId, in array
                                         poFieldValueArray, for a field that
                                         matches the name in pstrFieldName.

                  GetSelectedInfo4LeadId Get the value, if any, of each field
                                         named in comma-delimited list
                                         pstrFieldNameList, from the custom
                                         fields associated with the lead ID
                                         represented by pstrLeadId.

    2022/08/22 DG This update contains only cosmetic cleanup of the comments and
                  the ordering of new method GetSelectedInfo4LeadId, to keep all
                  methods arranged alphabedically by name.

    2022/08/28 DG New Methods: IndexFromOrdinal - Convert one-based ordinal to a
                                                  zero-based index (subscript).

                               OrdinalFromIndex - Convert zero-based index (an
                                                  array subscript) to one-based
                                                  ordinal.

    2022/08/29 DG New Methods: DatePartsFromInputElementValue - Parse the date
                                                                from OutrHTML of
                                                                an Input element
                                                                of type date.

                               FormatDate4Html5DatePicker - Reassemble the date
                                                            parts returned by
                                                            DatePartsFromInputElementValue
                                                            into a string that
                                                            conforms to the
                                                            requirements of the
                                                            HTML5 date picker.

                               GetInputControlsByType - Get an array of INPUT
                                                        controls (hdml document
                                                        elements) by type.

                  New Constants: COLON_SPACE = Colon followed by a single space

                                 HTML5_DATE_SEPARATOR_CHAR = HTML5 date part
                                                             separator - hyphen

                                 HYPHEN_CHAR = General-purpose hyphen character

                                 EQUALS_SIGN_SPACED = Equals sign enclosed in
                                                      space characters

                                 JQUERY_SELECTOR_IS_CLASSNAME = The prefix on a
                                                                JQuery selector
                                                                that identifies
                                                                it as a Class

                                 JQUERY_SELECTOR_IS_ELEMENT_ID = The prefix on a
                                                                 JQuery selector
                                                                 that identifies
                                                                 it as an ID

                  ApplyTimePartFixups: Adjust to handle input of numeric string
                                       containing a leading zero.

    2022/09/01 DG DatePartsFromInputElementValue and FormatDate4Html5DatePicker
                  must handle the case where the length of the text in the date
                  field exceeds 10 characters, so that such data is preserved,
                  even when that means that the HTML5 date picker cannot display
                  it. When such a situation arises, the value in question is
                  displayed in a message box.

                  A new IsString method encodes the two-step process required to
                  determine whether a JavaScript object either is or contains a
                  string.

                  Another new method, DatePartsFromDatabaseValue, employs a
                  simpler regular expression to evaluate a field read from a
                  database.

                  GetSelectedInfo4LeadId can be restricted to the fields listed
                  in its second argument.

                  SetDateFieldValues is a new method that sets the date INPUT
                  controls on a landing page from like-named fields in the
                  database. The document ready event calls this method when the
                  context is that of a Landing Page.

                  Simplify argument sanity checks in GetInputControlsByType via
                  the new IsString method.

                  Append a full stop to the message displayed by method
                  DisplayVersionAsAlert.

    2022/09/06 DG Correct a reference error that prevented the date display from
                  working.

    2022/09/13 DG FormatDate4Html5DatePicker:     Fix a variable name that needs
                                                  an object reference. The error
                                                  was in an error message that
                                                  is extremely unlikely to arise
                                                  in the real world, and was,
                                                  therefore, untested.

                  GetIdOrName                     New method to return the ID or
                                                  Name in that preference order,
                                                  of an element.

                  GetInputControlsByType:         Implement a wild card string,
                                                  to return all input controls.

                  GetSTTDatabaseNameFromLocation: Amend to leverage the domain
                                                  name to get the database name
                                                  from the LeadLifeMaster database
                                                  via the OpenController in the
                                                  SalesAcceleration database.

                  GetSTTDomainNameFromLocation:   Amend to prevent returning the
                                                  database name in error.

                  GetValueFromInputControl:       Get value from a Form Input
                                                  control.

                  GetVeryBasicLeadInfo4LeadId     Adapt to a breaking change in
                  VeryBasicLeadInfo constructor   the name-value pairs delimiter
                                                  returned in lists of fields
                                                  from the database.

                  SetDateFieldValues:             Rewrite, extending to populate
                                                  ALL input controls on Landing
                                                  Pages, changing the name to
                                                  HandleFormPrefill to reflect
                                                  the extended capabilities.

                  SubmitForm:                     Make this method a bit more
                                                  forgiving about missing form
                                                  controls for email, first and
                                                  last name, and mobile phone,
                                                  and swap the alert and reset,
                                                  so that users have the option
                                                  of making a screenshot of the
                                                  submitted form before the form
                                                  is cleared.

                  A new private member, AjaxUrlPrefix, stores the database name,
                  initialized after GetSTTDatabaseNameFromLocation returns in
                  the constructor, makes the methods that perform AJAX calls
                  database-agnosstic.

                  This change affects the following six methods:

                    1)  GetDomainTenant4LeadId
                    2)  GetSelectedInfo4LeadId (2 calls)
                    3)  GetVeryBasicLeadInfo4LeadId
                    4)  LogException
                    5)  PostEvent
                    6)  ProcessLandingPageForm

                  Going forward, AJAX methods append the URL that they want to
                  call to the string stored in AjaxUrlPrefix, which defaults
                  to SalesAcceleration.

                  As of 7 September 2022, AjaxUrlPrefix is set as soon as it is
                  safe to do so, enabling code called from within the method
                  that does so to perform its AJAX call.

                  Allow HandleFormPrefill to populate ALL forms.

    2022/09/14 DG GetSTTDatabaseNameFromLocation: Set this.AjaxUrlPrefix for the
                                                  case where the database is
                                                  read from _llAppPath.

                  GetFieldValue: Return the empty string when the database query
                                 returns string literal 'NULL' or the JavaScript
                                 primitive undefined as the value of a field.

    2022/09/16 DG 1) Implement HandleSpecialPrefill to populate span containers
                     on a form when the page loads, and again, by reading back
                     the values from the database, after the form is submitted
                     and reset.

                  2) Wrap the AJAX call that posts the form in a retry loop that
                     allows for a programmed number of retries encoded into the
                     AJAX_RETRY_LIMIT property.

                  3) Wrap all other AJAX calls in retry loops.

                  4) StringStartsWith is a new method that behaves like the
                     static StartsWith method on the system.String class in the
                     Microsoft .NET Framework.

    2022/09/21 DG 1) Replace GetSelectedInfo4LeadIdGet with GetSelectedInfo4LeadIPost.

                  2) Comment out the dialog box in the ready event.

                  3) GatherAdditionalFieldValues uses a new GetInputControlValue
                     method to resolve the value to report for an input control.
                     This solves at last the mystery of the missing checkboxes,
                     whose value property is always "on" regardless of the state
                     of its checked property. I implemented it as an instance
                     method in case we find other uses for it.

                  4) Set a flag to prevent submitting the same form more than
                     once.

    2022/09/22 DG Documentation change only: Misleading documentation of the
                                             GetSelectedInfo4LeadId return value

    2022/09/29 DG 1) Stop the stuttering submits.

                  2) StringSplitSharp is a new method that mirrors the behavior
                     of System.String.Split.

                  3) GetSelectedInfo4LeadId uses StringSplitSharp to preserve
                     equals characters embedded in the value of a text field
                     read from the database.

                  4) JquerySelectorByTagNameAndAttributeValue is a new method
                     that constructs a valid JQuery selector that returns all
                     objects that have a specified tagName and attribute value.

    2022/09/30 DG SubmitForm: Handle the case where the form contains a field
                              for an email address.

    2022/10/02 DG Replace the anonymous function that responds to the JQuery
                  document ready event with a garden variety JavaScript function
                  lockAndLoad that is defined and called inline.

    2022/10/03 DG GetSTTDomainNameFromLocation gets a a couple of minor fixes
                  that affect Landing Page processing.

    2022/10/04 DG GetUrlParameter becomes case insensitive with respect to its
                  input parameter.

    2022/10/05 DG HandleFormPrefill skips input controls marked with a CSS class
                  ID of STTformField2SkipPrefill.

    2022/10/06 DG The SubmitForm method performs Custom Portal processing when a
                  hidden button having the ID CustomPortalMoniker is present in
                  the body of its form element.

                  Method DoAjax is a generic Ajax caller, first put to use in
                  SubmitForm.

    2022/10/07 DG Make minor fixups here and there, mostly to do with garden
                  variety Landing Pages, as opposed to Custom Portal pages.

                  Amend HandleFormPrefill to consult a list of fields on Custom
                  Portal pages whose hard coded values must be preserved.

    2022/10/12 DG HandleFormPrefill: Correct error that prevented anything but a
                  Custom Portal from pre-poplulating its input values from the
                  database.

    2022/10/20 DG SubmitForm:   1) Add pfCallRulesEngine, an optional Boolean
                                   parameter that causes the rules engine to be
                                   called after the lead is created or updated.

                                2) Unless action is undefined, the empty string,
                                   or self, redirect the Web browser as if the
                                   form had been submitted.

                  PostFormData  Add a global flag, _fCallRulesEngineOnSubmit
                                that, when set, causes the SubmitForm method to
                                be called with the optional parameter described
                                above.

                  DoAjax        Add an optional Boolean parameter that sets the
                                value of the async parameter to the Ajax call.

                  anonymous     The anonymouns initialization function checks a
                                Boolean variable, _fSkipAsyncEventRegistration,
                                a global variable that is defined, but left
                                uninitialized in LeadLifeJSHelpersGlobals.js, to
                                determine whether it should exit without doing
                                any work.

    2022/10/23 DG UQFileNameFromHrefOrPathName is a new utility method that gets
                  the unqualified file name from the end of a URL. This method
                  is first applied to setting the STTContext when the host page
                  is the SalesTalk video player.

    2022/10/26 DG ComputeEngagementTime: Simplify the expression returned in the
                                         most cases, thereby correcting an error
                                         that was exposed by implementing strict
                                         mode, and add 'Player Hidden or Closed'
                                         to the list of pstrEventIdString values
                                         that cause a nonzero duration to be
                                         computed.

                  ComputeEngagementTime relies upon PageLoadTime_JS_Date
                  representing a JavaScript Date object. Though assigning
                  Date ( ) to PageLoadTime_JS_Date appears to produce a string,
                  new Date ( ) returns an object that JavaScript recognizes as a
                  Date object.

    2022/10/27 DG Constructor: AbsoluteLocation =   window.location.origin
                                                     + window.location.pathname;
                               Origin           = window.location.origin;

    2022/10/30 DG TestForSignalElementByIdAndType is a new method that allows
                  features to be controlled by defining hidden checkboxes in the
                  page.

    2022/11/01 DG VisibleRequiredFieldsHaveValues is a new method that displays
                  a message when one or more visible required fields has no
                  value.

                  GetLabelForInputElement is a convenience method that gets the
                  value of the first (or only) label attached to an input. If
                  the input element is unlabeled, it gets the ID, preferably, or
                  the name, as a last resort, from the specified element.

    2022/12/02 DG GetExtension is a new method that returns the extension from a
                  file name.

    2022/12/05 DG QueryAssociativeArray is a new method that returns the value
                  stored in an associative array at a specified key. This method
                  is syntactic sugar that could be replaced by the expression in
                  its one and only statement, but it is worth keeping since JS
                  doesn't support preprocessor macros.

                  GetLeadIdFromQueryString is amended so that it always returns
                  a valid integer, which is zero when the lead ID is absent from
                  the query string.

    2022/12/06 DG Adapt DoAjax so that it accepts URLs to locations other than
                  OpenController.

    2022/12/13 DG Add constants for the underscore character and for the Regular
                  Expression flags.

    2022/12/15 DG Define a new utility method, LeftPadInteger.

    2022/12/17 DG GetUrlParameter gets an optional parameter, pstrDefaultValue,
                  enabling it to return something besides the empty string when
                  its original parameter, pstrKeyName, is absent from the query
                  string.

                  JumpToElementById is a new method that puts the text at the
                  top of a specified ID at the top of the viewport.

                  Resolve undefined symbols in code that hasn't been exercised.
                  The undefined symbols were identified in a report generated by
                  the online ESLint tool.

                  Define a new constant, HTML_LINE_BREAK.

    2022/12/17 DG (v. 1.290) Define new constants for opening and closing
                  paragraph tags, HTML_NBSP, HTML_PARA_OPEN, HTML_PARA_CLOSE,
                  QUERYSTRING_ARG_1, QUERYSTRING_ARG_N and REGEXP_WORD_BOUNDARY.

    2022/12/22 DG (v. 1.291) PostEvent gets a new optional parameter intended to
                  accept the basename of a file, the video, audio, or transcript
                  file fed into STT_VideoPlayer.

    2022/12/24 DG (v. 1.292) ComputeEngagementTime returns the duration since
                  the page was loaded except when pstrEventIdString is 'Player
                  Opened'.

    2022/12/27 DG (v. 1.293) Make methods GetSTTDatabaseNameFromLocation and
                  GetSTTDomainNameFromLocation more robust and suppress the JS
                  alert when GetDomainTenant4LeadId throws an exception. Default
                  the database to SalesAcceleration for the file: protocol.

    2023/01/01 DG (v. 1.294 Adjust for the special case in which _llAppPath is
                  undefined by making it another determinant of the STTContext
                  value.

    2023/01/02 DG (v. 1.295) StringEndsWith is the inverse of StringStartsWith.

    2023/01/06 DG (v. 1.298) GetUrlParameter checks LocalStorage before giving
                  up. This feat is accomplished through a new QueryLocalStorage
                  method, which I made visible in case we need it for something
                  else.

    2023/01/10 DG (v. 1.299) Do the following:

                  1) Amend HandleFormPrefill and SubmitForm to account for the
                     lead ID being always numeric.

                  2) In the object constructor, when this.IsCustomPortal returns
                     TRUE, call global function SetRulesEngineOnSubmit to set
                     global Boolean flag _fCallRulesEngineOnSubmit to TRUE.

    2023/01/16 DG (v. 1.300) GetLocalTimeAsString gets an optional argument that
                             specifies the JavaScript Date object to format, and
                             the month day gets the same treatment already given
                             to other date parts.

    2023/01/27 DG (v. 1.301) Both SubmitForm and PostEvent call RulesForLeadId,
                             unless the latter has no lead ID. Though SubmitForm
                             did so already, I moved the code above the alert.

    2023/01/30 DG (v. 1.302) Correct GetLeadIdFromQueryString so that it returns
                             zero instead of NaN when the routines that query
                             for the lead ID return the empty string, which
                             parseInt evaluates as such.

    2023/02/01 DG (v. 1.303) Correct GetExtension so it parses extensionless
                             path strings correctly.

    2023/02/05 DG (v. 1.304) 1) FORM_FEED_CHAR is a new character constant added
                                for use with changes allowing the code behind a
                                page to generate domain-unique values from the
                                ID of a control and the last fragment of the URL
                                of the page.

                             2) ProcessLandingPageForm splits controls that have
                                pseudo-class LLMergeValueLists assigned into a
                                separate list of values that are merged with the
                                values already stored in the database, rather
                                than replacing them outright.

    2023/02/08 DG (v. 1.305) GetInputControlValue returns the empty string for a
                             checkbox unless it is checked.

    2023/02/15 DG (v. 1.306) 1) DoAjax allows its second argument, pstrVerb, to
                                be omitted, defaulting to GET, as do the methods
                                on the OpenController.

                             2) GetExtension gets corrected so that it tests the
                                candidate extension string, as was intended, not
                                the entire input string, for unwanted special
                                characters.

    2023/03/27 DG (v. 1.307) GetUrlParameter gets extended to evaluate the form,
                             GetSTTDomainNameFromLocation is modified to support
                             application URLs that point to locations within the
                             product, and the constructor short circuits when
                             GetSTTDomainNameFromLocation is handed a URL from
                             which it may as well also get the database name.

                             Finally, GetFieldValue evaluates both the id and
                             name attributes of every control, favoring the ID
                             over the Name attribute, in keeping with convention
                             that favor unique ID values over Name values, for
                             which the Document Object Model permits duplicates.

                             IsApplicationURL is a new method that returns TRUE
                             when the URL is part of the application.

    2023/03/29 DG (v. 1.308) GetSelectedInfo4LeadId gets more rigorous vetting,
                             causing it to raise an exception unless its result
                             contains at least one each of equals and logical
                             negate characters, indicating usually that the lead
                             ID supplied to it cannot be found in the attached
                             database.

    2023/03/29 DG (v. 1.309) DoAjax gets another optional argument to specify
                             the ASP.NET MVC controller to call.

    2023/03/30 DG (v. 1.310) IsApplicationURL originally tested for "Content"
                             after the database name, to which I added "Mobile."

    2023/03/31 DG (v. 1.311) HandleFormPrefill gets an optional pstrLeadId
                             argument that overrides the property value, if any,
                             stored by the constructor.

    2023/04/08 DG (v. 1.312) GetSTTDomainNameFromLocation reads the login email
                             from Local Storage and feeds it to Open controller
                             method GetDomainTenantUserIds4LoginName to get the
                             domain, tenant, and user ID, along with the domain
                             name, all four of which go into properties.

    2023/04/10 DG (v. 1.313) GetPickListValues does as its name implies.

    2023/04/19 DG (v. 1.314) 1) UpdateFormFieldById does as its name implies,
                                and optionally fires the Rules Engine.

                             2) HandleFormPrefill maintains the shadow fields
                                on the form by copying into each the value of
                                the field that it shadows as read from the
                                database.

                             3) CheckCurrentValueAgainstInitialValue does as its name
                                implies, returning TRUE when the value of a
                                field and that of its shadow differ, or if the
                                field has no shadow field.

                             4) Pass UtcOffsetMinutes to the server in the
                                following AJAX methods:

                                LeadLifeJSHelpers      SalesTalk MVC API
                                ---------------------- --------------------
                                GetSelectedInfo4LeadId GetSelectedInfo4LeadIPost
                                ProcessLandingPageForm CreateABehaviorPost
                                UpdateFormFieldById    UpdateFormFieldByInternalName

    2023/04/21 DG (v. 1.315) 1) In function PostFormData, correct an error that
                                arose when an unnamed form was submitted through
                                the post handler.

                             2) In method GatherAdditionalFieldValues, handle a
                                case where an element (a fieldset) has no length
                                property, and is, in any case, irrelevant.

    2023/04/22 DG (v. 1.316) 1) GetPickListValues is substantially rewritten to
                                accept a single string, required, that is the ID
                                of the SELECT element upon which it operates and
                                the name of the Custom Field that is its backing
                                store in the database.

                             2) GetValues4AllPickList is a new method that uses
                                an anonymous function called by a jQuery ForEach
                                loop to execute GetPickListValues against every
                                pick list in a form.

                             3) Before populating any fields, HandleFormPrefill
                                calls sibling method GetPickListValues through
                                GetValues4AllPickList. Its code is also wrapped
                                in a spiffy new try/catch block.

    2023/04/23 DG (v. 1.317) 1) Use the DOMContentLoaded event to simplify the
                                inline code block that uses DoingDeferredLoading
                                to decide how to execute its inline code.

                             2) Define SCRIPTSOURCE to report the source from
                                which the script loaded.

                             3) GetFileName and GetFileName extract the file and
                                path names, respectively, from a URL. Since the
                                latter preserves the protocol, a new URL can be
                                easily constructed from it.

    2023/05/08 DG (v. 1.318) 1) Implement GetVeryBasicLeadInfo4ExternamCRMId as
                                alternative to GetVeryBasicLeadInfo4LeadId.

                             2) Extend the VeryBasicLeadInfo object to store the
                                ExternalCRMId, CreatedDate, and LastmodifiedDate
                                of the lead, amd make it the STTLeadBasicInfo
                                property of the LeadLifeJSHelpers object.

    2023/05/12 DG (v. 1.319) GetSTTDomainNameFromLocation gets a bit smarter
                             about how it detects errors that bubble up.

    2023/05/12 DG (v. 1.320) When document.currentScript === null, set
                             SCRIPTSOURCE to 'unknown' so that it doesn't throw.

    2023/05/14 DG (v. 1.321) In method GatherAdditionalFieldValues, skip input
                             elements that are marked as read only, since usesr
                             cannot change their values.

    2023/05/14 DG (v. 1.322) In GetDomainTenant4LeadId, take into account when a
                             numeric lead ID is present.

    2023/05/17 DG (v. 1.323) Replace all occurrences of embedded full stops in
                             strings with this.FULL_STOP.

    2023/05/25 DG (v. 1.324) Fix a fatal exception that prevented object
                             creation in circumstances when the domain and
                             tenant ID are derived successfully from the login
                             name (email address/ID).

    2023/05/29 DG (v. 1.325) Change GetFieldValue to strip nondigits from values
                             of masked types identified by their className as
                             Numeric Strings. This change requires passing a new
                             argument that receives the value of the className,
                             or simplifying the signature and passing the whole
                             document element object. I chose the latter.

    2023/06/04 DG (v. 1.326) Inplement GetNameOfCurrentFunction.

    2023/06/05 DG (v. 1.327) 1) Make GetUrlParameter case insensitive.

                             2) GetLeadIdFromQueryString gets an optional
                                Boolean argument that, when set to anything but
                                UNDEFINED, causes it to leave the return value
                                as a String, foregoing conversion to an integer.

    2023/06/14 DG (v. 1.328) 1) Finish moving GetNameOfCurrentFunction to global
                                library LeadLifeJSHelpersGlobals.js.

                             2) Account for function SetRulesEngineOnSubmit
                                being renamed EnableRulesEngineOnSubmit.

                             3) GetLeadIdFromQueryString needs another optional
                                flag that, when set to anything besides symbolic
                                constant UNDEFINED, causes it to forego calling
                                GetUrlParameter, which would precipitate a stack
                                overflow.

    2023/06/18 DG (v. 1.329) GetSTTDomainNameFromLocation: Retrieve and save the
                             IDs returned by GetDomainTenant4LeadId, which are
                             stashed into hidden properties.

    2023/06/19 DG (v. 1.330) GetSTTDomainNameFromLocation: Save the login name
                             gathered from LocalStorage into a new STTLoginName
                             property, and set it to the login naame of the Last
                             user who modified the active lead record if that is
                             available. Adding userId, as STTUserId, and
                             loginName, as STTLoginName, to OpenController call
                             GetDomainTenant4LeadId, ensures that the LoginName
                             and UserID have usable values.

    2023/06/22 DG (v. 1.331) GetUrlParameter was falling back to searching for a
                             lead ID when the request is for something else.

    2023/06/22 DG (v. 1.332) 1) Method GetSelectedInfo4LeadId gets a new Boolean
                                argument, pfDateWithoutTime.

                             2) Arrow function GetDomainInfo4LeadId preserves
                                the login ID if it belongs to the same domain as
                                the lead ID.

    2023/06/26 DG (v. 1.333) DatePartsFromDatabaseValue truncates dates arriving
                             from the database by removing their time component.
                             This change makes processing input controls of type
                             date transparent and unobtrusive. This improvement
                             means that GetSelectedInfo4LeadId can lose its
                             pfDateWithoutTime argument.

    2023/06/28 DG (v. 1.334) Make HYPHEN_CHAR publicly visible.

    2023/07/09 DG (v. 1.335) GetDomainTenant4LeadId: An error returned by the
                             like-named OpenController method was being thrown
                             away, producing erroneous results by causing the
                             donain and tenant ID derived from the login ID to
                             be discarded (set to undefined).

    2023/07/12 DG (v. 1.336) 1) Move StringStartsWith, StringEndsWith, and
                                StringSplitSharp to LeadLifeJSHelpersGlobal.js.

                             2) In the GetVeryBasicLeadInfo4ExternamCRMId method
                                call in method GetDomainTenant4LeadId, add
                                SysCRMLeadOrContact as a second parameter.

    2023/07/17 DG (v. 1.337) GetDomainInfo4LeadId gets a 2-line cosmetic fixup
                             that has NO EFFECT on the executable code.

    2023/07/21 DG (v. 1.338) Move the following routines from LeadLifeJSHelpersLib:

                             1) GetLeadIdFromQueryString

                             2) GetParameterFromURLFormOrLocalStorage,
                                WAS GetUrlParameter

                             3) QueryLocalStorage

                             4) QueryPageFields

    2023/07/25 DG (v. 1.339) 1) Change function GetLeadOrCrmIdFromUrl to take
                                the domain ID and lead Id into account in its
                                call to GetVeryBasicLeadInfo4ExternamCRMId.

                             2) Change method DoAjax to log its argument list on
                                the Developer Console.

    2023/07/27 DG (v. 1.340) Adjust the code that handles the SalesTalk Talking
                             Point URL context to support requirements recently
                             added to support Landing Pages.

    2023/07/27 DG (v. 1.341) 1) LogException: Simplify the confusing messaging.

                             2) Move the call to LLCommon.GetUrlVarsFromSession
                                from Mobile_Index.js to the DOMContentLoaded
                                event listener defined herein, so that it always
                                happens.

                             3) Change GetSTTDomainNameFromLocation so that it
                                resolves the tenant and domain ID and the domain
                                name without reliance on a valid leadId, yet it
                                sets the leadId property if a value is present.

    2023/07/30 DG (v. 1.342) Change GetDomainTenant4LeadId to use PlayerLeadId
                             when the video player module takes the lead.

    2023/08/13 DG (v. 1.343) Near the top of the constructor, set all member
                             variables that have values gathered from either the
                             query string or sessionStorage, remove Hungarian
                             prefixes from externally visible variable names,
                             and align the handling of AjaxUrlPrefix with
                             LLCommon.

    2023/08/14 DG (v. 1.344) Amend global function lockAndLoad to generate and
                             inject a named MyView template into the form. When
                             the pagename parameter is omitted or the empty
                             string, the default MyView template for the logged
                             in user and the specified lead ID is returned.

    2023/08/20 DG (v. 1.345) Amend GetSTTDomainNameFromLocation so that it works
                             when LLCommon sets _dbname without marking the
                             domain, tenant, and lead ID as safe.

    2023/08/28 DG (v. 1.346) Retire the local DoAjax and LogException methods in
                             favor of the copy that I put into LLCommon, which
                             is now a vast improvement over the version that was
                             here, leaving only one version to maintain.

    2023/09/05 DG (v. 1.347) Amend CheckCurrentValueAgainstInitialValue to check for a
                             null shadow element.

    2023/09/06 DG (v. 1.348) GetElementValue is a new method that returns the
                             attribute of any broadly-defined INPUT element (to
                             include TEXTAREA and SELECT elements).

    2023/09/11 DG (v. 1.349) Fix algorithm errors that left this.STTDatabaseName
                             undefined, along with other issues, cosmetic and
                             otherwise, exposed during mobile page testing.
                             A case in point is that SetSelectedValue has been
                             relieved of the need for a reference to ita
                             caller's this object by leveraging constants that
                             are defined and exposed by LLCommon.

    2023/09/17 DG (v. 1.350) 1) Move the JQuery helper constants and methods to
                                LLCommon.

                             2) Move LeftPadInteger, IndexFromOrdinal, and
                                OrdinalFromIndex to LeadLifeJSHelpersGlobals.

                             3) Replace the local symbolic constants with the
                                globally defined constants implemented by the
                                LLCommon library, which is fully initialized by
                                the time this library begins to execute.

    2023/09/22 DG (v. 1.351) GetDomainTenant4LeadId queried localStorage for the
                             PlayerURL key, when it should instead have queried
                             for leadId. See function $scope.storyItemClick2,
                             defined in StorySoFarController.js.

    2023/10/04 DG (v. 1.352) ValidateFormFields returns a delimited list of form
                             element IDs that failed validation.

    2023/10/07 DG (v. 1.353) In function lockandload, skip OpenController method
                             GetMyViewScreenHTML when _pagename is null.

    2023/10/08 DG (v. 1.354) After the constructor has fully initialized the
                             object, sync the values that have corresponding
                             values in the global objects created by LLCommon.

                             NOTE: The ValidateFormFields method is temporarily
                                   commented out except for its first and last
                                   statements.

    2023/10/16 DG (v. 1.355) 1) Inside method GetFieldValue, define a new
                                private function, FixBullhornMultiSelect.

                             2) Method GetElementValue was returning innerHTML
                                as the value of a TEXTAREA element. It should
                                return its value property.

    2023/10/17 DG (v. 1.356) In public function lockAndLoad, if the current page
                             contains an element named STT_HandleFormPrefill,
                             its innerText identifies the element that contains
                             the INPUT controls to be populateed from like-named
                             fields in the database.

    2023/10/25 DG (v. 1.357) Reinstate ValidateFormFields by uncommenting it.

    2024/04/22 DG (v. 1.358) Ensure that the form contains populated elements
                             named firstName and lastName.

    2024/04/29 DG (v. 1.359) Since lockAndLoad may be called twice in some use
                             cases, and the first call populated My View, check
                             for a populated form before expending significant
                             effort populating it on the second call when the
                             first call filled it. Likewise, since the calling
                             routine needs it, return a reference to the element
                             into which it poured the My View document.

    2024/05/11 DG (v. 1.360) Replace virtually all calls to console.log with calls
                             to LLCommon.Trace, which can be centrally configured
                             to suppress logging.

    2024/05/28 DG (v. 1.361) UpdateFormFieldById gets a new optional argument,
                             pfUpdateLeadModDate, with a default value of TRUE,
                             that is passed through to OpenController method
                             UpdateFormFieldByInternalName, which updates the
                             last modified date of the controlling Lead row when
                             its value is TRUE.

    2024/08/07 DG (v. 1.362) ValidateFormFields is changed so that it skips
                             blank pick list fields.

    2024/08/23 DG (v. 1.363) 1) GetPickListValues had a longstanding bug that
                                prevented updating existing pick lists that is
                                finally swatted to death.

                             2) Replace StringStartsWith and StringEndsWith with
                                String.prototype.startsWith and endsWith.

                             2) HandleFormPrefill is changed so that when a CRM
                                is configured, it is queried before the database
                                is queried for values to go into the list of
                                form fields.

    2024/09/02 DG (v. 1.364) GetPickListValues had an undefined element that
                             caused an array index to be set to 2 BILLION, an
                             unworkable number of pick list options.

    2024/09/05 DG (v. 1.365) HandleFormPrefill calls CreateOrRefreshLeadFromCRM
                             to query the CRM for updated field values before it
                             populates the form. When LLCommon.EntityType is
                             null because the My View page is not associated
                             with a CRM Entity, CreateOrRefreshLeadFromCRM needs
                             to receive the empty string, rather than have the
                             call throw an exception because the EntityName
                             property of the EntityType is undefined.

    2024/09/26 DG (v. 1.366) Make the URL evaluation in IsWiseAgentMobilePage
                             case insensitive and aware of urls with and without
                             a terminal forward slash.

    2024/10/01 DG (v. 1.367) Method HandleFormPrefill gets two changes.

                             1) Skip INPUT elements that are really BUTTON
                                elements and, therefore, NEVER receive inputs.

                             2) Use the new OpenController method,
                                GetTitleFieldForCRMEntitiy, to set the value of
                                the Title field.

                             3) Use OpenController method IsEmailAddressValid to
                                validate email addresses through instance method
                                IsEmailAddressValid.

    2024/10/15 DG (v. 1.368) Method UpdateFormFieldById was returning a Boolean
                             value of false, causing the toString method to fail
                             when it returnd through UpdateIfChanged, which was
                             expected to return a string. Apparently, a Boolean
                             doesn't bother to implement toString.

    2024/10/16 DG (v. 1.369) HandleFormPrefill, which initially returned void,
                             returns radocFields, the array of objects that map
                             to the input elements in its input container.

    2024/11/03 DG (v. 1.370) HandleFormPrefill bypasses the CRM when a sessuib
                             storage key constructed from the current external
                             CRM ID and the SalesTalk lead ID exists. The only
                             action that destroys the key is a page exit event.

    2024/11/28 DG (v. 1.371) Cause UpdateFormFieldById to emit a log record when
                             it sets the form dirty flag, and stash the values
                             in a private member.

    2025/02/19 DG (v. 1.372) Change the background color of the UpdateCRMNow
                             button, if such a thing exists on the form that
                             called UpdateFormFieldById, by applying CSS style
                             (selector) STT_Stoplight_Green_1.

    2025/02/20 DG (v. 1.373) Populate the DomainId and DomainName fields in the
                             form if it contains either.

    2025/03/18 DG (v. 1.374) Account for IndexFromOrdinal and OrdinalFromIndex
                             being upgraded from ordinary functions to methods
                             on the LLCommon object.

    2025/04/02 DG (v. 1.375) Adjust the date stamp to force recomputation of the
                             Subresource Integrity digest string.

    2025/04/20 DG (v. 1.376) Account for StringSplitSharp moving to LLCommon.

    2025/04/23 DG (v. 1.377) Suppress populating the Wise Agent Task form.

    2024/05/14 DG (v. 1.378) 1) Adapt the IsPickListValueValid method so that
                                it evaluates the entity type description to
                                determine whether to evaluate the Name property
                                or the DisplayText property.

                             2) Except in global function LockAndLoad, replace
                                the undifferentiated global function
                                GetNameOfCurrentFunction ( ) with the like-named
                                LLCommon.GetNameOfCurrentFunction ( ) method. In
                                the case of LockAndLoad, which has global scope,
                                a private version of the function is implemented
                                inline.

                             3) Move global function ValidateOneFormField from
                                LeadLifeJSHelpersGlobals, making it an instance
                                method of class LeadLifeJSHelpers.

    2024/08/08 DG (v. 1.379) Revise the internal documentation of instance
                             method RequiredFieldHasValue to reflect its
                             additional ability to identify a required field
                             that is blank.

    2024/10/02 DG (v. 1.380) Add ExternalCRMId to the CreateOrRefreshLeadFromCRM
                             call when LLCommon knows it.

    2025/01/06 DG (v. 1.381) Delete button styling code from UpdateFormFieldById
                             and move UQFileNameFromHrefOrPathName out of this
                             library and into LLCommon.js where it is more
                             visible.
    ============================================================================
*/

class VeryBasicLeadInfo
{
    VERSION;
    // Private instance members.

    #strLeadId;
    #strLastName;
    #strFirstName;
    #strEmail;
    #strMobilePhone;
    #strExternalCRMId;
    #dtmCreatedDate;
    #dtmLastModifiedDate;

    constructor ( pstrVeryBasicLeadInfo )
    {
        this.VERSION             = 1.004;

        const LOGICAL_NEGAGE      = '¬';
        const EQUALS_CHAR         = '=';

        const INDEXOF_NOT_FOUND   = -1;

        const POS_LEAD_ID         = 0;  // 0    1310524
        const POS_LAST_NAME       = 1;  // 1    Demo I
        const POS_FIRST_NAME      = 2;  // 2    Transaction
        const POS_EMAIL           = 3;  // 3    Transaction.demoi@@gmail.com
        const POS_MOBILE_PHONE    = 4;  // 4    <MobilePhone>
        const POS_EXTERNALCRMID   = 5;  // 5    SA73722
        const POS_CREATED_DT      = 6;  // 6    05/05/2023 02:40:25
        const POS_MODIFIED_DT     = 7;  // 7    05/05/2023 04:52:09
        const POS_DOMAIN_ID       = 8;  // 8    1363
        const POS_TENANT_ID       = 9;  // 9    1374
        const POS_DOMAIN_NAME     = 10; // 10   Sweet_Assist

        const astrLeadInfoFields = pstrVeryBasicLeadInfo.split ( LOGICAL_NEGAGE );

        if ( pstrVeryBasicLeadInfo.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND )
        {
            this.#strLeadId       = ParseNVP ( astrLeadInfoFields [ POS_LEAD_ID ] );
            this.#strLastName     = ParseNVP ( astrLeadInfoFields [ POS_LAST_NAME ] );
            this.#strFirstName    = ParseNVP ( astrLeadInfoFields [ POS_FIRST_NAME ] );
            this.#strEmail        = ParseNVP ( astrLeadInfoFields [ POS_EMAIL ] );
            this.#strMobilePhone  = ParseNVP ( astrLeadInfoFields [ POS_MOBILE_PHONE ] );
        }   // TRUE (The input format follows the original pattern.) block, if ( pstrVeryBasicLeadInfo.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND )
        else
        {
            this.#strLeadId       = astrLeadInfoFields [ POS_LEAD_ID ];
            this.#strLastName     = astrLeadInfoFields [ POS_LAST_NAME ];
            this.#strFirstName    = astrLeadInfoFields [ POS_FIRST_NAME ];
            this.#strEmail        = astrLeadInfoFields [ POS_EMAIL ];
            this.#strMobilePhone  = astrLeadInfoFields [ POS_MOBILE_PHONE ];

            if ( astrLeadInfoFields.length > POS_MOBILE_PHONE )
            {
                this.#strExternalCRMId = astrLeadInfoFields [ POS_EXTERNALCRMID ];
                this.#dtmCreatedDate   = astrLeadInfoFields [ POS_CREATED_DT ];

                if ( astrLeadInfoFields [ POS_MODIFIED_DT ] > EMPTY_STRING_LENGTH )
                {   // Leave it uninitialized unless it has a value.
                    this.#dtmLastModifiedDate = astrLeadInfoFields [ POS_MODIFIED_DT ];
                }   // if ( astrLeadInfoFields [ POS_MODIFIED_DT ] > EMPTY_STRING_LENGTH )
            }   // if ( astrLeadInfoFields.length > POS_MOBILE_PHONE )
        }   // FALSE (The input format follows the newer pattern.) block, if ( pstrVeryBasicLeadInfo.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND )

        function ParseNVP ( pstrNVP )
        {
            const  astrNameAndValue = pstrNVP.split ( EQUALS_CHAR );
            return astrNameAndValue [ KEY_VALUE_PAIR_IS_VALUE ];
        }   // function ParseNVP
    }   // constructor


    GetLeadId ( )
    {
        return this.#strLeadId;
    }   // GetLeadId method


    GetLastName ( )
    {
        return this.#strLastName;
    }   // GetLeadId method


    GetFirstName ( )
    {
        return this.#strFirstName;
    }   // GetLeadId method


    GetEmail ( )
    {
        return this.#strEmail;
    }   // GetLeadId method


    GetMobilePhone ( )
    {
        return this.#strMobilePhone;
    }   // GetLeadId method


    GetExternalCRMId ( )
    {
        return this.#strExternalCRMId;
    }   // GetExternalCRMId method


    GetCreatedDate ( )
    {
        return this.#dtmCreatedDate;
    }   // GetCreatedDate method


    GetLastModifiedDate ( )
    {
        return this.#dtmLastModifiedDate;
    }   // GetLastModifiedDate method
}   // class VeryBasicLeadInfo


class LeadLifeJSHelpers
{
    //  ------------------------------------------------------------------------
    //  Define private properties before the constructor is defined.
    //  ------------------------------------------------------------------------

    VERSION;

    // Constant members.

    AJAX_RETRY_LIMIT;
    AJAX_REPORT_SUCCESS_ON_FIRST_TRY;

    NUMERIC_ZERO;
    NUMERIC_MINUS_ONE;
    NUMERIC_PLUS_ONE;

    ARRAY_INVALID_ELEMENT;
    ARRAY_FIRST_ELEMENT;
    ARRAY_NEXT_ELEMENT;
    ARRAY_SECOND_ELEMENT;
    ARRAY_THIRD_ELEMENT;
    ARRAY_FOURTH_ELEMENT;
    ARRAY_FIFTH_ELEMENT;
    ARRAY_SIXTH_ELEMENT;

    BRACKET_LEFT;
    BRACKET_RIGHT;
    DOUBLE_QUOTE_CHAR;

    SPLIT_NAME_FROM_VALUE;

    PURE_DATE_STRING_LENGTH;
    SUBSTRING_START;

    ASTERISK_CHAR;
    CARRIAGE_RETURN_CHAR;
    FORM_FEED_CHAR;
    LINE_FEED_CHAR;
    TAB_CHAR;

    CHARACTER_I_LC;
    CHARACTER_I_UC;
    CHARACTER_L_LC;
    CHARACTER_L_UC;
    CHARACTER_O_LC;
    CHARACTER_O_UC;

    CHARACTER_ZERO;
    CHARACTER_ONE;

    DBNULL;
    DROPDOWN_BUTTON_ID_SUFFIX;
    CLASSNNAME_STT_REQUIRED;
    DECIMAL_POINT;
    FULL_STOP;
    COMMA;

    HTML_LINE_BREAK;
    HTML_NBSP;
    HTML_PARA_OPEN;
    HTML_PARA_CLOSE;

    EMPTY_STRING;
    EQUALS_CHAR;
    LOGICAL_NEGATE;
    PIPE_CHAR;
    PIPE_CHAR_SPLIT_MATCH;

    QUERYSTRING_ARG_1;
    QUERYSTRING_ARG_N;

    SPACE_CHARACTER;
    SPACE_URLENCODED;

    CSV_SEPARATOR_CHAR;
    DATE_SEPARATOR_CHAR;
    HTML5_DATE_SEPARATOR_CHAR;
    HTTPS_PROTOCOL;
    HYPHEN_CHAR;
    PATH_PROTOCOL_DELIMITER;
    PATH_SEPARATOR_CHAR;
    WINDOWS_PATH_SEPARATOR_CHAR;
    TIME_SEPARATOR_CHAR;
    TODAY;
    UNDERSCORE_CHAR;

    NEXT_CHARACTER;
    SINGLE_CHARACTER;
    INDEXOF_NOT_FOUND;
    STT_MINIMUM_ID_VALUE;
    SUBSTRING_FIRST_CHAR;
    SUBSTRING_SECOND_CHARACTER;

    COLON_SPACE;
    EQUALS_SIGN_SPACED;

    REGEXP_NO_MORE_MATCHES;

    REGEXP_GENERATE_SUBSTRING_INDICES;
    REGEXP_GLOBAL_MATCH;
    REGEXP_CASE_INSENSITIVE_MATCH;
    REGEXP_MULTILINE_MATCH;
    REGEXP_SINGLE_LINE_MATCH;
    REGEXP_UNICODE_CODE_POINTS_MATCH;
    REGEXP_STICKY_MATCH;

    REGEXP_WORD_BOUNDARY;

    ERR_GETELEMENTBYNAME;
    ERR_MESSAGE_STANDARD_PREFIX;
    STANDARD_SEND_TO_TRACE_PREFIX;

    // Symbolic constants for form control types

    FORM_CONTROL_IS_INPUT;
    FORM_CONTROL_IS_SELECT;
    FORM_CONTROL_IS_TERXTAREA;

    // Symbolic constants for STTContext constant

    STT_LANDING_PAGE_CONTEXT;
    STT_TP_CONTEXT;

    // Session instance members

    STTContext
    STTFileSystemSubContext;
    STTPurlSubContext;
    STTRepositorySubContext;
    STTVideoPlayerSubContext;
    STTDatabaseName;
    STTLoginName;
    STTUserId;
    STTDomainId;
    STTDomainName;
    STTTenantId;
    STTLeadId;
    STTLeadBasicInfo;

    // Static arrays and such

    Controls2Skip;
    CustomPortalFields2Skip;
    CustomFieldNames2LeadColumnNames;
    DteFunctionString;
    PageLoadTime_JS_Date;
    UtcOffsetMinutes;
    fAllowEmptyFields;

    //  ------------------------------------------------------------------------
    //  The constants that follow ValidationErrorMessageArray represent elements
    //  in the array of error messages.
    //  ------------------------------------------------------------------------

    ValidationErrorMessageArray;

    VALIDATION_ERROR_REQUIRED;
    VALIDATION_ERROR_NOT_IN_PICK_LIST;
    VALIDATION_ERROR_INVALID_EMAIL;

    LeadColNames2CustomFieldNames;
    Origin;
    AbsoluteLocation;
    PageTitle;

    //  ------------------------------------------------------------------------
    //  Formerly private instance member set from argument to constructor, made
    //  public so that it gets serialized
    //  ------------------------------------------------------------------------

    fDebugFlag;

    //  ------------------------------------------------------------------------
    //  This property we keep private because it is accessible through method
    //  GetUIDisplaySubTypes.
    //  ------------------------------------------------------------------------

    #aoUIDisplaySubTypes;

    //  ------------------------------------------------------------------------
    //  This property we keep private to prevent tampering.
    //  ------------------------------------------------------------------------

    #dctInitialValues;

    constructor ( pfDebugFlag )
    {
        debugger;
        this.fDebugFlag                             = pfDebugFlag;

        //  --------------------------------------------------------------------
        //  The session may have a copy of the initialized instance in storage.
        //  If so, save some effort, including one or two trips to the server,
        //  by using it to set all the properties, including the dynamic ones.
        //  --------------------------------------------------------------------

        const strSessionKey                         = document.location.href + '¬' + 'LeadLifeJSHelpers';
        const oLlhProps                             = sessionStorage.getItem ( strSessionKey );

        if ( Object.is ( oLlhProps , null ) )
        {
            //  ----------------------------------------------------------------
            //  Based on information gathred from StackOvervlow article
            //  "document.currentScript is null," retrieved 7 September 2023
            //  from https://stackoverflow.com/questions/38769103/document-currentscript-is-null,
            //  I moved the script source code to LeadLifeJSHelpers_SCRIPTSOURCE
            //  at the very top of the module. As well, this placement aligns
            //  with the position of similar blocks at the top of its companions
            //  LeadLifeJSHelpersGlobals.js and LLCommon.js.
            //
            //  Nevertheless, the version number and load time stay where they
            //  are, so that their values go into the session variable that is
            //  created as the constructor is about to exit.
            //  ----------------------------------------------------------------

            this.VERSION                            = LeadLifeJSHelpers_Version;
            this.PageLoadTime_JS_Date               = new Date ( );

            console.log ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE , LeadLifeJSHelpers_Version , LeadLifeJSHelpers_LastUpdated , 'loading at ' + this.PageLoadTime_JS_Date ) );

            this.AJAX_RETRY_LIMIT                   = 10;
            this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY   = true;

            this.NUMERIC_ZERO                       = 0;
            this.NUMERIC_MINUS_ONE                  = -1;
            this.NUMERIC_PLUS_ONE                   = 1;

            this.ARRAY_INVALID_ELEMENT              = -1;
            this.ARRAY_FIRST_ELEMENT                = 0;
            this.ARRAY_NEXT_ELEMENT                 = 1;
            this.ARRAY_SECOND_ELEMENT               = 1;
            this.ARRAY_THIRD_ELEMENT                = 2;
            this.ARRAY_FOURTH_ELEMENT               = 3;
            this.ARRAY_FIFTH_ELEMENT                = 4;
            this.ARRAY_SIXTH_ELEMENT                = 5;

            this.BRACKET_LEFT                       = '[';
            this.BRACKET_RIGHT                      = ']';
            this.DOUBLE_QUOTE_CHAR                  = '"';

            this.PURE_DATE_STRING_LENGTH            = 10;
            this.SPLIT_NAME_FROM_VALUE              = 2;
            this.SUBSTRING_START                    = 0;
            this.SUBSTRING_SECOND_CHARACTER         = 1;

            this.ASTERISK_CHAR                      = '*';

            this.CARRIAGE_RETURN_CHAR               = '\u000D';
            this.FORM_FEED_CHAR                     = '\u000C';
            this.LINE_FEED_CHAR                     = '\u000A';
            this.TAB_CHAR                           = '\u0009';

            this.CHARACTER_I_LC                     = 'i';
            this.CHARACTER_I_UC                     = 'I';

            this.CHARACTER_L_LC                     = 'l';
            this.CHARACTER_L_UC                     = 'L';

            this.CHARACTER_O_LC                     = 'o';
            this.CHARACTER_O_UC                     = 'O';

            this.CHARACTER_ZERO                     = '0';
            this.CHARACTER_ONE                      = '1';

            this.DBNULL                             = 'NULL';
            this.DROPDOWN_BUTTON_ID_SUFFIX          = '_DropDown';
            this.CLASSNNAME_STT_REQUIRED            = 'STT_Required';
            this.DECIMAL_POINT                      = '.';
            this.COMMA                              = ',';
            this.FULL_STOP                          = '.';

            this.HTML_LINE_BREAK                    = '<br>';
            this.HTML_NBSP                          = '&NBSP;';
            this.HTML_PARA_OPEN                     = '<p>'
            this.HTML_PARA_CLOSE                    = '</p>'

            this.EMPTY_STRING                       = '';
            this.EQUALS_CHAR                        = '=';
            this.LOGICAL_NEGATE                     = '¬';
            this.PIPE_CHAR                          = '|';
            this.PIPE_CHAR_SPLIT_MATCH              = '\|';

            this.QUERYSTRING_ARG_1                  = '?';
            this.QUERYSTRING_ARG_N                  = '&';

            this.SPACE_CHARACTER                    = ' ';
            this.SPACE_URLENCODED                   = '%20';
            this.CSV_SEPARATOR_CHAR                 = ',';

            this.DATE_SEPARATOR_CHAR                = '/';
            this.HTML5_DATE_SEPARATOR_CHAR          = '-';
            this.HTTPS_PROTOCOL                     = 'https:';
            this.HYPHEN_CHAR                        = '-';
            this.PATH_PROTOCOL_DELIMITER            = '//';
            this.PATH_SEPARATOR_CHAR                = '/';
            this.TIME_SEPARATOR_CHAR                = ':';
            this.TODAY                              = '@today';
            this.UNDERSCORE_CHAR                    = '_'
            this.WINDOWS_PATH_SEPARATOR_CHAR        = '\\';

            this.SINGLE_CHARACTER                   = 1;
            this.NEXT_CHARACTER                     = 1;
            this.INDEXOF_NOT_FOUND                  = -1;
            this.STT_MINIMUM_ID_VALUE               = 1000;
            this.SUBSTRING_FIRST_CHAR               = 0;

            this.COLON_SPACE                        = ': ';
            this.EQUALS_SIGN_SPACED                 = ' = ';

            this.REGEXP_NO_MORE_MATCHES             = 0;        // Use with the test method and the lastIndex property.

            this.REGEXP_GENERATE_SUBSTRING_INDICES  = 'd';      // Generate indices for substring matches.
            this.REGEXP_GLOBAL_MATCH                = 'g';      // Global search.
            this.REGEXP_CASE_INSENSITIVE_MATCH      = 'i';      // Case-insensitive search.
            this.REGEXP_MULTILINE_MATCH             = 'm';      // Allow ^ and $ to match newline characters.
            this.REGEXP_SINGLE_LINE_MATCH           = 's';      // Allow . to match newline characters.
            this.REGEXP_UNICODE_CODE_POINTS_MATCH   = 'u'       // Treat a pattern as a sequence of Unicode code points.
            this.REGEXP_STICKY_MATCH                = 'y';      // Perform a "sticky" search that matches starting at the current position in the target string.

            this.REGEXP_WORD_BOUNDARY               = '\\b';    // Represent a word boundary in a regular expression.

            this.ERR_GETELEMENTBYNAME               = 'ERROR: The specified name';
            this.ERR_MESSAGE_STANDARD_PREFIX        = 'ERROR';
            this.STANDARD_SEND_TO_TRACE_PREFIX      = 'SalesTalk - URL - ';

            this.FORM_CONTROL_IS_INPUT              = 'i';
            this.FORM_CONTROL_IS_SELECT             = 's';
            this.FORM_CONTROL_IS_TERXTAREA          = 't';

            this.STT_LANDING_PAGE_CONTEXT           = 'L';
            this.STT_REPOSITORY_CONTEXT             = 'R';
            this.STT_TP_CONTEXT                     = 'T';

            //  --------------------------------------------------------------------
            //  Set the private member values, none of which change for the lifetime
            //  of an instance.
            //  --------------------------------------------------------------------

            this.STTFileSystemSubContext            = false;
            this.STTPurlSubContext                  = false;
            this.STTRepositorySubContext            = false;
            this.STTVideoPlayerSubContext           = false;

            this.UtcOffsetMinutes                   = ( new Date ( ) ).getTimezoneOffset ( );
            this.AbsoluteLocation                   = window.location.origin + window.location.pathname;
            this.Origin                             = window.location.origin;   // This is a reference; it is unused internally.
            this.PageTitle                          = document.title;

            this.DteFunctionString                  = 'date()';

            this.LeadColNames2CustomFieldNames     = {
                                                        "Company.AnnualRevenue "                   :"CompanyAnnualRevenue",
                                                        "Company.Description "                     :"CompanyDescription",
                                                        "Company.EmployeeCount "                   :"CompanyEmployeeCount",
                                                        "Company.Industry "                        :"CompanyIndustry",
                                                        "Company.Name "                            :"CompanyName",
                                                        "Company.TickerSymbol "                    :"CompanyTickerSymbol",
                                                        "Company.WebSite "                         :"CompanyWebSite",
                                                        "LeadPhones.Phone{Fax}.Number "            :"FaxPhone",
                                                        "LeadLocations.Location{Home}.Address1 "   :"HomeAddress1",
                                                        "LeadLocations.Location{Home}.Address2 "   :"HomeAddress2",
                                                        "LeadLocations.Location{Home}.Address3 "   :"HomeAddress3",
                                                        "LeadLocations.Location{Home}.Address4 "   :"HomeAddress4",
                                                        "LeadLocations.Location{Home}.City "       :"HomeCity",
                                                        "LeadLocations.Location{Home}.Country "    :"HomeCountry",
                                                        "LeadPhones.Phone{Home}.Number "           :"HomePhone",
                                                        "LeadLocations.Location{Home}.State "      :"HomeState",
                                                        "LeadLocations.Location{Home}.Zip "        :"HomeZip",
                                                        "OptInOuts.OptInOut{Email}.OptIndicator "  :"IsOptedOutOfEmail",
                                                        "OptInOuts.OptInOut{Phone}.OptIndicator "  :"IsOptedOutOfPhone",
                                                        "OptInOuts.OptInOut{Mail}.OptIndicator "   :"IsOptedOutOfPrintMail",
                                                        "LeadPhones.Phone{Mobile}.Number "         :"MobilePhone",
                                                        "LeadLocations.Location{Office}.Address1 " :"OfficeAddress1",
                                                        "LeadLocations.Location{Office}.Address2 " :"OfficeAddress2",
                                                        "LeadLocations.Location{Office}.Address3 " :"OfficeAddress3",
                                                        "LeadLocations.Location{Office}.Address4 " :"OfficeAddress4",
                                                        "LeadLocations.Location{Office}.City "     :"OfficeCity",
                                                        "LeadLocations.Location{Office}.Country "  :"OfficeCountry",
                                                        "LeadLocations.Location{Office}.State "    :"OfficeState",
                                                        "LeadLocations.Location{Office}.Zip "      :"OfficeZip",
                                                        "LeadPhones.Phone{Other}.Number "          :"OtherPhone",
                                                        "PrimaryCampaign.Name "                    :"PrimaryCampaignName",
                                                        "LeadPhones.Phone{Toll}.Number "           :"TollPhone",
                                                        "LeadPhones.Phone{Work}.Number "           :"WorkPhone",
                                                     };

            this.CustomFieldNames2LeadColumnNames  = {
                                                        "CompanyAnnualRevenue"                     : "Company.AnnualRevenue",
                                                        "CompanyDescription"                       : "Company.Description",
                                                        "CompanyEmployeeCount"                     : "Company.EmployeeCount",
                                                        "CompanyIndustry"                          : "Company.Industry",
                                                        "CompanyName"                              : "Company.Name",
                                                        "CompanyTickerSymbol"                      : "Company.TickerSymbol",
                                                        "CompanyWebSite"                           : "Company.WebSite",
                                                        "FaxPhone"                                 : "LeadPhones.Phone{Fax}.Number",
                                                        "HomeAddress1"                             : "LeadLocations.Location{Home}.Address1",
                                                        "HomeAddress2"                             : "LeadLocations.Location{Home}.Address2",
                                                        "HomeAddress3"                             : "LeadLocations.Location{Home}.Address3",
                                                        "HomeAddress4"                             : "LeadLocations.Location{Home}.Address4",
                                                        "HomeCity"                                 : "LeadLocations.Location{Home}.City",
                                                        "HomeCountry"                              : "LeadLocations.Location{Home}.Country",
                                                        "HomePhone"                                : "LeadPhones.Phone{Home}.Number",
                                                        "HomeState"                                : "LeadLocations.Location{Home}.State",
                                                        "HomeZip"                                  : "LeadLocations.Location{Home}.Zip",
                                                        "IsOptedOutOfEmail"                        : "OptInOuts.OptInOut{Email}.OptIndicator",
                                                        "IsOptedOutOfPhone"                        : "OptInOuts.OptInOut{Phone}.OptIndicator",
                                                        "IsOptedOutOfPrintMail"                    : "OptInOuts.OptInOut{Mail}.OptIndicator",
                                                        "MobilePhone"                              : "LeadPhones.Phone{Mobile}.Number",
                                                        "OfficeAddress1"                           : "LeadLocations.Location{Office}.Address1",
                                                        "OfficeAddress2"                           : "LeadLocations.Location{Office}.Address2",
                                                        "OfficeAddress3"                           : "LeadLocations.Location{Office}.Address3",
                                                        "OfficeAddress4"                           : "LeadLocations.Location{Office}.Address4",
                                                        "OfficeCity"                               : "LeadLocations.Location{Office}.City",
                                                        "OfficeCountry"                            : "LeadLocations.Location{Office}.Country",
                                                        "OfficeState"                              : "LeadLocations.Location{Office}.State",
                                                        "OfficeZip"                                : "LeadLocations.Location{Office}.Zip",
                                                        "OtherPhone"                               : "LeadPhones.Phone{Other}.Number",
                                                        "PrimaryCampaignName"                      : "PrimaryCampaign.Name",
                                                        "TollPhone"                                : "LeadPhones.Phone{Toll}.Number",
                                                        "WorkPhone"                                : "LeadPhones.Phone{Work}.Number",
                                                     };

            this.MobilePage_WA_Contact_FieldMap    = {
                                                        "FirstName"                                : "FirstName",
                                                        "LastName"                                 : "LastName",
                                                        "Email"                                    : "Email",
                                                        "Title"                                    : "WA-Title",
                                                        "Company.Name"                             : "Company.Name",
                                                        "LeadPhones.Phone{Work}.Number"            : "LeadPhones.Phone{Work}.Number",
                                                        "LeadPhones.Phone{Mobile}.Number"          : "LeadPhones.Phone{Mobile}.Number",
                                                     };

            this.MobilePage_WA_Contact_FormFieldMap = {
                                                        "FirstName"                                : "FirstName",
                                                        "LastName"                                 : "LastName",
                                                        "Email"                                    : "Email",
                                                        "WA-Title"                                 : "Title",
                                                        "Company.Name"                             : "Company.Name",
                                                        "LeadPhones.Phone{Work}.Number"            : "LeadPhones.Phone{Work}.Number",
                                                        "LeadPhones.Phone{Mobile}.Number"          : "LeadPhones.Phone{Mobile}.Number",
                                                      };

            this.Controls2Skip                     = [
                                                        'Submit',
                                                        'll_t',
                                                        'll_u',
                                                        'll_leadId',
                                                        'll_url_email',
                                                        'll_ignoreRepost',
                                                        'ControlId',
                                                        'll_f',
                                                        'll_dn',
                                                        'll_postToCRM',
                                                        'post',
                                                        'LeadId',
                                                     ];

            this.CustomPortalFields2Skip           = [
                                                        'CustomPortal Recruited GAC Num',
                                                        'GAC Hierarchy 1st',
                                                        'GAC Hierarchy 2nd',
                                                        'GAC Hierarchy 3rd',
                                                        'CustomPortal Recruited Name',
                                                        'Assignment Of Commissions',
                                                        'Upline',
                                                        'AgencyName',
                                                        'GAC BestOne Contracting',
                                                        'BestOne Contracting Origin',
                                                        'BestOne Dental Contracting WF',
                                                        'B1D Status',
                                                        'VBA Contracting WF',
                                                        'VBA Appt Status',
                                                        'VBA Contracting Origin',
                                                        'GAC VBA Contracting',
                                                        'NEA Contracting WF',
                                                        'NEA GRP Appt Status',
                                                        'NEA Contracting Origin',
                                                        'GAC NEA GROUP CONTRACTING',
                                                        'YAP Contracting WF',
                                                        'YAP Appt Status',
                                                        'YAP Contracting Origin',
                                                        'GAC YAP Contracting',
                                                     ];

            this.VALIDATION_ERROR_REQUIRED          = ARRAY_FIRST_ELEMENT;
            this.VALIDATION_ERROR_NOT_IN_PICK_LIST  = ARRAY_SECOND_ELEMENT;
            this.VALIDATION_ERROR_INVALID_EMAIL     = ARRAY_THIRD_ELEMENT;

            this.ValidationErrorMessageArray        = [
                                                        'This field must have a value. Either type it in or use the Update Fields button to add one by voice.',
                                                        'Input value must be in the pick list. Click the button to the right of this text box to see and use the list.',
                                                        'The input value must be a valid email address. We recommend updating by typing the desired address into the field.'
                                                      ];

            this.fAllowEmptyFields                  = false;

            //  --------------------------------------------------------------------
            //  If global flag _fDomainAndTenantIDAreSafe (owned and mamaged by the
            //  LLCommon object instance) is TRUE, set instance members that have a
            //  corresponding global variable for which the associated source flag
            //  has a value greater than zero SRC_IS_UNKNOWN.
            //  --------------------------------------------------------------------

            if ( _fDomainAndTenantIDAreSafe )
            {
                if ( _leadidSource     !== SRC_IS_UNKNOWN ) { this.STTLeadId       = _leadid; }

                if ( _loginSource      !== SRC_IS_UNKNOWN ) { this.STTLoginName    = _login; }
                if ( _useridSource     !== SRC_IS_UNKNOWN ) { this.STTUserId       = _userid; }

                if ( _domainnameSource !== SRC_IS_UNKNOWN ) { this.STTDomainName   = _domainname; }
                if ( _domainidSource   !== SRC_IS_UNKNOWN ) { this.STTDomainId     = _domainid; }
                if ( _tenantidSource   !== SRC_IS_UNKNOWN ) { this.STTTenantId     = _tenantid; }

                if ( _dbnameSource     !== SRC_IS_UNKNOWN ) { this.STTDatabaseName = _dbname; }
            }   // if ( _fDomainAndTenantIDAreSafe )

            //  --------------------------------------------------------------------
            //  Set the public session instance member values, none of which change
            //  for the lifetime of an instance.
            //  --------------------------------------------------------------------

            this.STTDomainName = this.STTDomainName === undefined ? this.GetSTTDomainNameFromLocation ( ) : this.STTDomainName;

            if ( this.STTDomainName === EMPTY_STRING || this.STTDomainName.startsWith ( 'ERROR in LeadLifeJSHelpers.GetDomainTenant4LeadId: ' ) )
            {
                if ( this.STTContext === this.STT_TP_CONTEXT )
                {   // This shouldn't be reported for a landing page.
                    const strErrorMessage = 'Internal error in LeadLifeJSHelpers constructor: The DomainName, upon which everything else depends, cannot be determined.';
                    LLCommon.LogException ( strErrorMessage );
                    alert ( strErrorMessage + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + 'Please contact SalesTalk customer support for assistance.' );
                }   // TRUE (anticipated outcome) block, if ( this.STTContext === this.STT_TP_CONTEXT )
                else
                {
                    if ( this.STTDomainName !== 'ERROR in LeadLifeJSHelpers.GetDomainTenant4LeadId: No lead record could be identified.' )
                    {
                        if ( !this.STTVideoPlayerSubContext )
                        {
                            alert ( LLCommon.LogException ( 'Internal error in LeadLifeJSHelpers constructor: ' + this.STTDomainName ) + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + 'Please contact SalesTalk customer support for assistance.' , 'native' );
                        }   // if ( !this.STTVideoPlayerSubContext )
                    }   // if ( this.STTDomainName !== 'ERROR in LeadLifeJSHelpers.GetDomainTenant4LeadId: No lead record could be identified.' )
                }   // FALSE (unanticipated outcome) block, if ( this.STTContext === this.STT_TP_CONTEXT )
            }   // if ( this.STTDomainName === EMPTY_STRING || this.STTDomainName.startsWith ( 'ERROR in LeadLifeJSHelpers.GetDomainTenant4LeadId: ' ) )

            if ( ( this.STTContext !== this.STT_TP_CONTEXT ) && ( Object.is ( this.STTDatabaseName , undefined ) ) )
            {
                //  ------------------------------------------------------------
                //  Though the database name is encoded into the URL, and is
                //  needed by the domain query, GetSTTDomainNameFromLocation
                //  left setting its value to GetSTTDatabaseNameFromLocation.
                //  ------------------------------------------------------------

                this.STTDatabaseName = _dbnameSource === SRC_IS_UNKNOWN ? this.GetSTTDatabaseNameFromLocation ( ) : this.STTDatabaseName;

                if ( this.STTDatabaseName === EMPTY_STRING )
                {
                    const strErrorMessage = 'Internal error in LeadLifeJSHelpers constructor: The Database Name, unpon which everything else depends, cannot be determined.';
                    LLCommon.LogException ( strErrorMessage );
                    alert ( strErrorMessage + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + 'Please contact SalesTalk customer support for assistance.' );
                }   // if ( this.STTDatabaseName === EMPTY_STRING )

                //  ------------------------------------------------------------
                //  Call this as if it were void to set the domain and tenant ID
                //  properties.
                //  ------------------------------------------------------------

                if ( this.IsCustomPortal ( ) )
                {   // Only Custom Portal landing pages require a lead ID.
                    this.GetDomainTenant4LeadId ( );

                    //  --------------------------------------------------------
                    //  This global variable is declared, but left undefined.
                    //  Assigning it a value causes the rules engine to be
                    //  invoked against the active lead (the one that was just
                    //  created or updated, as opposed to the upline lead). The
                    //  value, whether true or false, is assigned to the async
                    //  flag. Though we generally prefer synchronous AJAX calls,
                    //  this call warrants being made asyncrhonous so that the
                    //  calling process isn't blocked while the rules are
                    //  evaluated and executed.
                    //  --------------------------------------------------------

                    EnableRulesEngineOnSubmit ( );
                }   // if ( this.IsCustomPortal ( ) )
            }   // if ( ( this.STTContext !== this.STT_TP_CONTEXT ) && ( Object.Is ( this.STTDatabaseName , undefined ) ) )

            //  ----------------------------------------------------------------
            //  Cache a copy of the object in session storage so that subsequent
            //  page loads can avoid wasteful trips to the server.
            //  ----------------------------------------------------------------

            const jsonLLH = JSON.stringify ( this );
            sessionStorage.setItem ( strSessionKey , jsonLLH );
            console.log ( this.PageLoadTime_JS_Date + ': LeadLifeJSHelpers version ' + this.VERSION.toFixed ( 3 ) + ' loaded and initialized dynamically' );
            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' LeadLifeJSHelpers version ' + this.VERSION.toFixed ( 3 ) + ' loaded and initialized dynamically for lead record ' + this.STTLeadId } } );
        }   // TRUE (Session storage offers no shortcuts.) block, if ( Object.is ( oLlhProps , null ) )
        else
        {
            const llhProps = JSON.parse ( oLlhProps );

            for ( const k of Object.keys ( this ) )
            {
                if ( k in llhProps )
                {
                    this [ k ] = llhProps [ k ];
                }   // if ( k in llhProps )
            }   // for ( const k of Object.keys ( this ) )

            console.log ( 'Properties of LeadLifeJSHelpers instance set from Session Storage = ' + this );
            console.log ( this.PageLoadTime_JS_Date + ': LeadLifeJSHelpers version ' + this.VERSION.toFixed ( 3 ) + ' loaded and initialized from copy retrieved from session storage' );

            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' LeadLifeJSHelpers version ' + this.VERSION.toFixed ( 3 ) + ' loaded and initialized from copy retrieved from session storage for lead record ' + this.STTLeadId } } );
        }   // FALSE (Properties can be restored from data retrieved from Session Storage.) block, if ( Object.is ( oLlhProps , null ) )

        //  --------------------------------------------------------------------
        //  Ensure that the LeadId, DomainID, TenantID, UsserId, and LoginID
        //  in LeadLifeJSHelpers and LLCommon agree.
        //  --------------------------------------------------------------------

        if ( this.STTLeadId > MINIMUM_STT_ENTITY_ID && _leadid < MINIMUM_STT_ENTITY_ID )
        {
            _leadid             = this.STTLeadId;
            _leadidSource       = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( this.STTLeadId > MINIMUM_STT_ENTITY_ID && _leadid < MINIMUM_STT_ENTITY_ID )

        if ( this.STTDomainId > MINIMUM_STT_ENTITY_ID && _domainid < MINIMUM_STT_ENTITY_ID )
        {
            _domainid           = this.STTDomainId;
            _domainidSource     = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( this.STTDomainId > MINIMUM_STT_ENTITY_ID && _domainid < MINIMUM_STT_ENTITY_ID )

        if ( this.STTTenantId > MINIMUM_STT_ENTITY_ID && _tenantid < MINIMUM_STT_ENTITY_ID )
        {
            _tenantid           = this.STTTenantId;
            _tenantidSource     = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( this.STTTenantId > MINIMUM_STT_ENTITY_ID && _tenantid < MINIMUM_STT_ENTITY_ID )

        if ( this.STTUserId > MINIMUM_STT_ENTITY_ID && _userid < MINIMUM_STT_ENTITY_ID )
        {
            _userid             = this.STTUserId;
            _useridSource       = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( this.STTUserId > MINIMUM_STT_ENTITY_ID && _userid < MINIMUM_STT_ENTITY_ID )

        if ( LLCommon.IsString ( this.STTLoginName ) && this.STTLoginName.length > EMPTY_STRING_LENGTH && _login === null )
        {
            _login              = this.STTLoginName;
            _loginSource        = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( LLCommon.IsString ( this.STTLoginName ) && this.STTLoginName.length > EMPTY_STRING_LENGTH && _login === null )

        if ( LLCommon.IsString ( this.STTDomainName ) && this.STTDomainName.length > EMPTY_STRING_LENGTH && _domainname === null )
        {
            _domainname         = this.STTDomainName;
            _domainnameSource   = SRC_IS_LLJS_HELPERS_SYNC;
        }   // if ( LLCommon.IsString ( this.STTDomainName ) && this.STTDomainName.length > EMPTY_STRING_LENGTH && _domainname === null )

        //  --------------------------------------------------------------------
        //  List the key properties on the debugger console.
        //  --------------------------------------------------------------------

        console.log ( 'LeadLifeJSHelpers.STTContext       = ' + this.STTContext );
        console.log ( 'LeadLifeJSHelpers.STTDatabaseName  = ' + this.STTDatabaseName );
        console.log ( 'LeadLifeJSHelpers.STTLeadId        = ' + this.STTLeadId );
        console.log ( 'LeadLifeJSHelpers.STTDomainId      = ' + this.STTDomainId );
        console.log ( 'LeadLifeJSHelpers.STTTenantId      = ' + this.STTTenantId );
        console.log ( 'LeadLifeJSHelpers.STTDomainName    = ' + this.STTDomainName );
        console.log ( 'LeadLifeJSHelpers.STTUserId        = ' + this.STTUserId );
        console.log ( 'LeadLifeJSHelpers.STTDomainName    = ' + this.STTDomainName );
    }   // constructor taking 1 argument that sets the debug flag


    AddProtocolWhenMissing ( pstrURL )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        AddProtocolWhenMissing

            Method Goal:        Compute the time, in seconds, that the user has
                                engaged with the player.

            Input:              pstrURL = String containing the URL to evaluate

            Output:             If the origin name begins with 'purl.', prefix
                                it with the protocol of the calling HTML page.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( pstrURL.length >= 5 )
        {   // The input string is sufficiently long that it COULD begin with 'purl.salestalktech.com/'.
            if ( HostIsPurl ( pstrURL ) )
            {   // The input string begins with 'purl.salestalktech.com/'.
                return location.protocol + PATH_PROTOCOL_DELIMITER + pstrURL;                  // Future-proof it by making it protocol-agnostic.
            }   // TRUE (The string is trying to be an absolute URL.) block, if ( HostIsPurl ( pstrURL ) )
            else
            {   // The beginning of the string suggests that either it's not an absolute URL or that the protocol survived the parser.
                return pstrURL;
            }   // FALSE (The string appears to be a relative URL.) block, if ( HostIsPurl ( pstrURL ) )
        }   // TRUE (The string passes the minimum length test.) block, if ( pstrURL.length >= 5 )
        else
        {   // The string is too short to be an absolute URL.
            return pstrURL;
        }   // FALSE (The string fails the minimum length test.) block, if ( pstrURL.length >= 5 )
    }   // AddProtocolWhenMissing method


    AllowEmptyFields ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        AllowEmptyFields

            Method Goal:        Call this method to allow empty fields to post.

            Output:             This method returns void.
            --------------------------------------------------------------------
        */

        this.fAllowEmptyFields = true;
    }   // AllowEmptyFields method


    ApplyDatePartFixups ( pintDatePart )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ApplyDatePartFixups

            Method Goal:        Compute the time, in seconds, that the user has
                                engaged with the player.

            Input:              pintDatePart    = Integer containing a date
                                                  (month number or month day
                                                  number)

            Output:             A numeric string of exactly two characters

            Remarks:            In addition to padding the string with leading
                                zeroes, this method adjusts for the fact that
                                JavaScript month and day of month numbers are
                                zero based, whree, e. g. Janaury is 0, as is the
                                first day of any month.
            --------------------------------------------------------------------
        */

        return this.ApplyTimePartFixups ( pintDatePart + 1 );
    }   // ApplyDatePartFixups method


    ApplyInputMaskToValueReadFromDB ( pdocInputElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ApplyInputMaskToValueReadFromDB

            Method Goal:        Force the input mask to be applied to the value
                                that was just read into an input control from a
                                database field.

            Input:              pdocInputElement = Reference to the populated
                                                   element

            Output:             Manifested as a side effect, the output visible
                                in the INPUT element is the value that was read
                                from the database with the input mask associated
                                with the element applied to it.

            Remarks:            Besides hiding a disposable UIDisplaySubTypes
                                object that meets its objective when it becomes
                                the object of the conditional expression in the
                                next IF statement, this method briefly relaxes a
                                read only attribute on element pdocInputElement,
                                which is necessary because the target of a KeyUp
                                event must be a read/write INPUT element.
            --------------------------------------------------------------------
        */

        const strMethodName        = LLCommon.GetNameOfCurrentFunction ( );

        const ATTRIB_NAME_RO       = 'readOnly'
        const oLLMaskInfo          = this.GetUIDisplaySubTypes ( ).find ( data => pdocInputElement.className.indexOf ( data.ClassName ) > INDEXOF_NOT_FOUND );

        if ( !Object.is ( oLLMaskInfo , undefined ) )
        {
            const fRestoreReadOnly = pdocInputElement.getAttribute ( ATTRIB_NAME_RO ) === EMPTY_STRING ? true : false;

            if ( fRestoreReadOnly )
            {
                pdocInputElement.removeAttribute ( ATTRIB_NAME_RO );
            }   // if ( fRestoreReadOnly )

            $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( pdocInputElement.id ) ).trigger ( 'keyup' );

            if ( fRestoreReadOnly )
            {
                pdocInputElement.setAttribute ( ATTRIB_NAME_RO , ATTRIB_NAME_RO );
            }   // if ( fRestoreReadOnly )
        }   // TRUE (The field has an input mask.) block, if ( !Object.is ( oLLMaskInfo , undefined ) )
    }   // ApplyInputMaskToValueReadFromDB


    ApplyMillisecondsFixups ( pintMillisecondsPart )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ApplyMillisecondsFixups

            Method Goal:        Compute the time, in seconds, that the user has
                                engaged with the player.

            Input:              pintMillisecondsPart    = Integer containing the
                                                          milliseconds part of a
                                                          JavaScript Date.

            Output:             A numeric string of exactly three characters
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( pintMillisecondsPart < 10 )
        {   // Since the value is less than 10, it needs two leading zeros.
            return CHARACTER_ZERO + CHARACTER_ZERO + pintMillisecondsPart;
        }   // TRUE block, if ( pintMillisecondsPart < 10 )
        else
        {   // Since the value is greater than 9 and less than 100, it needs one leading zero.
            if ( pintMillisecondsPart < 100 )
            {
                return CHARACTER_ZERO + pintMillisecondsPart;
            }   // TRUE block, if ( pintMillisecondsPart < 100 )
            else
            {   // Since the value is greater than 100, it is ok as is, but it should be coerced to be a string.
                return EMPTY_STRING + pintMillisecondsPart;
            }   // FALSE block, if ( pintMillisecondsPart < 100 )
        }   // FALSE block, block, if ( pintMillisecondsPart < 10 )
    }   // ApplyMillisecondsFixups method


    ApplyTimePartFixups ( pintTimePart )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ApplyTimePartFixups

            Method Goal:        Compute the time, in seconds, that the user has
                                engaged with the player.

            Input:              pintTimePart    = Integer containing the hour,
                                                  minute, or second part of a
                                                  JavaScript Date

            Output:             A numeric string of exactly two characters
            --------------------------------------------------------------------
        */

        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        const intTimePartAsInt = parseInt ( pintTimePart );

        if ( intTimePartAsInt < 10 )
        {   // Since the date part value is less than 10, prefix its value with a zero.
            return CHARACTER_ZERO + intTimePartAsInt;
        }   // TRUE block, if ( intTimePartAsInt < 10 )
        else
        {   // Since the date part is greater than 9, it is ok as is, but it should be coerced to be a string.
            return EMPTY_STRING + intTimePartAsInt;
        }   // FALSE block, if ( pintTimePart < 10 )
    }   // ApplyTimePartFixups method


    CheckCurrentValueAgainstInitialValue ( pstrElementId )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        CheckCurrentValueAgainstInitialValue

            Method Goal:        Use a hidden shadow field to determine whether a
                                specified field has been changed and must be
                                updated. Please see the remarks for important
                                implemnentation details and dependencies.

            Input:              pstrElementId       = ID of the field as it
                                                      appears on the form

            Output:             The return value is TRUE when the current value
                                of a field and that of its entry in dictionary
                                this.#dctInitialValues differ.

            Remarks:            Since the dictionary stores only name/value
                                pairs, unlike the shadow elements that it
                                replaces, the sole remaining use of the nodeName
                                of the corresponding is determining the property
                                to evaluate and compare against the value stored
                                in the dictionary.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        console.log ( strMethodName + ' Arguments: pstrElementId = ' + pstrElementId );
        console.log ( strMethodName + ' this.#dctInitialValues ' + ( this.#dctInitialValues === undefined ? 'is UNDEFINED.' : this.#dctInitialValues === null ? ' is NULL.' : ' Field Count = ' + Object.keys ( this.#dctInitialValues ).length ) );

        try
        {
            if ( LLCommon.IsString ( pstrElementId ) )
            {
                debugger;

                const docElement            = document.getElementById ( pstrElementId );
                var   strOriginalValue      = EMPTY_STRING;

                if ( this.#dctInitialValues !== undefined && this.#dctInitialValues !== null && Object.prototype.hasOwnProperty.call ( this.#dctInitialValues , pstrElementId ) )
                {
                    strOriginalValue        = this.#dctInitialValues [ pstrElementId ];

                    console.log ( strMethodName + ': Element ID = ' + pstrElementId + ', Original Value = ' + strOriginalValue + ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' || docElement.nodeName === 'TEXTAREA' ? docElement.value : 'Irrelevant element type = ' + docElement.nodeName ) )

                    if ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' || docElement.nodeName === 'TEXTAREA' )
                    {
                        switch ( docElement.type )
                        {
                            case 'submit':
                            case 'reset':
                            case 'button':
                            case 'image':
                            case 'hidden':
                            case 'password':
                                return false;

                            case 'radio':
                            case 'checkbox':                    // The value of a RADIO or a CHECKBOX is meaningless; its Checked property is the value that matters.
                                return  ( ( docElement.checked ? 'true' : 'false' ) !== strOriginalValue );
                                break;  // case 'radio' AND case 'checkbox'

                            case 'date':
                            case 'select-one':
                            case 'textarea':
                            default:
                                return ( docElement.value !== strOriginalValue );
                                break;  // case 'date' AND case 'select-one' AND case 'textarea' AND default
                        }   // switch ( docElement.type )
                    }   // if ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' || docElement.nodeName === 'TEXTAREA' )
                }   // TRUE (Since the form is Read/Write, there is a dictionary of initial values.) block, if ( this.#dctInitialValues !== undefined && this.#dctInitialValues !== null && Object.prototype.hasOwnProperty.call ( this.#dctInitialValues , pstrElementId ) )
                else
                {
                    console.log ( strMethodName + ': Element ID = ' + pstrElementId + ', Original Value = ' + strOriginalValue + ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' ? docElement.value : 'Irrelevant element type = ' + docElement.nodeName ) )

                    if ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' || docElement.nodeName === 'TEXTAREA' )
                    {
                        switch ( docElement.type )
                        {
                            case 'submit':
                            case 'reset':
                            case 'button':
                            case 'image':
                            case 'hidden':
                            case 'password':
                                return false;

                            case 'radio':
                            case 'checkbox':                    // The value of a RADIO or a CHECKBOX is meaningless; its Checked property is the value that matters.
                                return docElement.checked ? true : false;
                                break;  // case 'radio' AND case 'checkbox'

                            case 'date':
                            case 'select-one':
                            case 'textarea':
                            default:
                                return docElement.value.length > EMPTY_STRING_LENGTH;
                                break;  // case 'date' AND case 'select-one' AND case 'textarea' AND default
                        }   // switch ( docElement.type )
                    }   // if ( docElement.nodeName === 'INPUT' || docElement.nodeName === 'SELECT' || docElement.nodeName === 'TEXTAREA' )
                }   // FALSE (Since the form is Write Only, the dictionary of initial values is undefined.) block, if ( this.#dctInitialValues !== undefined && this.#dctInitialValues !== null && Object.prototype.hasOwnProperty.call ( this.#dctInitialValues , pstrElementId ) )
            }   // if ( LLCommon.IsString ( pstrElementId ) )

            return false;       // Take no chances; always return a known value.
        }
        catch ( ex )
        {
            throw new Error (   'ERROR evaluating Boolean function CheckCurrentValueAgainstInitialValue with: '
                              + 'pstrElementId = ' + pstrElementId
                              + ', ex.message =' + ex.message
                              + ', ex.stack = ' + ex.stack );
        }
    }   // CheckCurrentValueAgainstInitialValue


    ComputeEngagementTime ( pstrEventIdString ,
                            pdtmCurrEventTime )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ComputeEngagementTime

            Method Goal:        Compute the time, in seconds, that the user has
                                engaged with the player.

            Input:              pstrEventIdString   = String passed into event
                                                      delegate function

                                pdtmCurrEventTime   = JavaScript Date object
                                                      instantiated on entry by
                                                      TrackEvent

            Output:             Engagement time in seconds, which is the total
                                running time of the player, as distinct from the
                                running time of the clip.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var   ret = NUMERIC_ZERO;

        try
        {
            switch ( pstrEventIdString )
            {
                case 'PageOpened':
                    return NUMERIC_ZERO;
                case 'Begin Playing':
                case 'Pause Playing':
                case 'Finished Playing':
                case 'Player Closed':
                case 'Player Hidden':
                case 'Page Hidden':
                case 'URLClicked':
                case 'Player Hidden or Closed':
                    //  Strict mode seems incapable of handing an implicit cast correctly.
                    ret = Math.round ( ( pdtmCurrEventTime.valueOf ( ) - this.PageLoadTime_JS_Date.valueOf ( ) ) / 1000 );
                    break;
                case 'Player Opened':
                    ret = NUMERIC_ZERO;
                default:
                    ret = Math.round ( ( pdtmCurrEventTime.valueOf ( ) - this.PageLoadTime_JS_Date.valueOf ( ) ) / 1000 );
            }   // switch ( pstrEventIdString )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
            ret = NUMERIC_ZERO;
        }

        return isNaN ( ret ) ? NUMERIC_ZERO : ret;
    }   // ComputeEngagementTime method


    DatePartsFromDatabaseValue ( pstrOuterHTML )
    {
        /*
            --------------------------------------------------------------------
            Name:       DatePartsFromInputElementValue

            Goal:       Return an array of date parts, the first of which is the
                        entire first date string that matched the expression.
                        The second, fourth, and sixth elements (corresponding to
                        subscripts 1, 3, and 5) are the first, second, and third
                        parts of the date, while the remaining elements hold the
                        delimiters (either / or -), which are discarded as
                        irrelevant by function FormatDate4Html5DatePicker.

            Arguments:  pstrOuterHTML = String containing the OuterHTML property
                                        of an Input element that has a Type of
                                        date (the HTML5 Date format)

            Returns:    The return value is an array of strings that contains
                        the three date parts and two date part delimiters.

                        When the input date includes a time component, the time
                        is discarded so that the remaining parts can be cobmined
                        into a string that is acceptable to the HTML5 date
                        picker control.

            Remarks:    The code in versions prior to 1.333 returned strings of
                        more than ten characters, treating them as invalid.

            See Also:   DatePartsFromInputElementValue
                        FormatDate4Html5DatePicker
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const regex             = /(\d{1,4})([\/-])(\d{1,2})([\/-])(\d{1,4})/;
        const strPureDateString = pstrOuterHTML.length > this.PURE_DATE_STRING_LENGTH ? pstrOuterHTML.substring ( SUBSTRING_FIRST_CHAR , this.PURE_DATE_STRING_LENGTH ) : pstrOuterHTML;
        const rastrDateParts    = regex.exec ( strPureDateString );

        if ( Object.is ( rastrDateParts , null ) )
        {   // When strPureDateString doesn't match the regular expression, regex.exec returns null.
            return strPureDateString;
        }   // TRUE (unanticipated outcome, match not found) block, if ( Object.is ( rastrDateParts , null ) )
        else
        {
            return rastrDateParts;
        }   // FALSE (anticipated outcome, match found) block, if ( Object.is ( rastrDateParts , null ) )
    }   // function DatePartsFromDatabaseValue


    DatePartsFromInputElementValue ( pstrOuterHTML )
    {
        /*
            --------------------------------------------------------------------
            Name:       DatePartsFromInputElementValue

            Goal:       Return an array of date parts, the first of which is the
                        entire first date string that matched the expression.
                        The second, fourth, and sixth elements (corresponding to
                        subscripts 1, 3, and 5) are the first, second, and third
                        parts of the date, while the remaining elements hold the
                        delimiters (either / or -), which are discarded as
                        irrelevant by function FormatDate4Html5DatePicker.

            Arguments:  pstrOuterHTML = String containing the OuterHTML property
                                        of an Input element that has a Type of
                                        date (the HTML5 Date format)

            Returns:    The return value is an array of strings that contains
                        the three date parts, two date part delimiters, and the
                        terminal delimiter, either a quote or a space followed
                        by the first digit of the time, which is discarded.

            See Also:   DatePartsFromDatabaseValue
                        FormatDate4Html5DatePicker
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        const regex          = /value=\x22(\d{1,4})([\/-])(\d{1,2})([\/-])(\d{1,4})(\x22| \d)/;
        const rastrDateParts = regex.exec ( pstrOuterHTML );

        if ( Object.is ( rastrDateParts , null ) )
        {   // When pstrOuterHTML doesn't match the regular expression, regex.exec returns null.
            return pstrOuterHTML;
        }   // TRUE (unanticipated outcome, match not found) block, if ( Object.is ( rastrDateParts , null ) )
        else
        {
            return rastrDateParts;
        }   // FALSE (anticipated outcome, match found) block, if ( Object.is ( rastrDateParts , null ) )
    }   // function DatePartsFromInputElementValue


    DisplayVersionAsAlert ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        DisplayVersionAsAlert

            Method Goal:        Display the version number in an alert box.

            Input:              None

            Output:             The return value is the string representation of
                                the version number of the object.
            --------------------------------------------------------------------
        */

        alert ( 'SalesTalk document helper object LeadLifeJSHelpers is executing from library version ' + this.VERSION.toFixed ( 3 ) + FULL_STOP );
        return this.VERSION.toFixed ( 3 ) ;
    }   // DisplayVersionAsAlert method


    FormatDate4Html5DatePicker ( pastrRegExpGroups )
    {
        /*
            --------------------------------------------------------------------
            Name:       FormatDate4Html5DatePicker

            Goal:       Transform the second, fourth, and sixth elements of the
                        array returned by DatePartsFromInputElementValue into a
                        string that is formatted YYYY-MM-DD, the default input
                        format supported by the HTML5 date picker displayed for
                        an INPUT element of type DATE.

            Arguments:  pastrRegExpGroups = This argument is the array of strings
                                            returned by companion function
                                            DatePartsFromInputElementValue,
                                            unless its input was invalid, in
                                            which case, it feeds in a string.

            Returns:    The return value is the date taken from the value of the
                        OuterHTML string that was passed as the pstrOuterHTML
                        argument to DatePartsFromInputElementValue reconstructed
                        in a format that is a valid input to the HTML5 date
                        input control.
            --------------------------------------------------------------------
        */

        const strMethodName          = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pastrRegExpGroups ) )
        {
            return pastrRegExpGroups;
        }   // TRUE (Input parameter pastrRegExpGroups is a string.) block, if ( LLCommon.IsString ( pastrRegExpGroups ) )
        else
        {
            const strDatePart1       = pastrRegExpGroups [ ARRAY_SECOND_ELEMENT ];
            const strDatePart2       = pastrRegExpGroups [ ARRAY_FOURTH_ELEMENT ];
            const strDatePart3       = pastrRegExpGroups [ ARRAY_SIXTH_ELEMENT ];

            const intDatePart1Length = strDatePart1.length;
            const intDatePart2Length = strDatePart2.length;
            const intDatePart3Length = strDatePart3.length;

            if ( intDatePart1Length === 4 )
            {   // The first substring of digits is the year.
                return strDatePart1 + HTML5_DATE_SEPARATOR_CHAR + this.ApplyTimePartFixups ( strDatePart2 ) + HTML5_DATE_SEPARATOR_CHAR + this.ApplyTimePartFixups ( strDatePart3 );
            }   // TRUE (The date is already formatted correctly for the HTML5 date control.) block, if ( intDatePart1Length === 4 )
            else
            {
                if ( intDatePart3Length === 4 )
                {   // The last substring of digits is the year.
                    return strDatePart3 + HTML5_DATE_SEPARATOR_CHAR + this.ApplyTimePartFixups ( strDatePart1 ) + HTML5_DATE_SEPARATOR_CHAR + this.ApplyTimePartFixups ( strDatePart2 );
                }   // TRUE (The date is formatted per the default EN-US short date format string.) block, if ( intDatePart3Length === 4 )
                else
                {
                    return 'DATE FORMAT ERROR: The date is in an unexpected format. Date value = ' + pastrRegExpGroups [ ARRAY_FIRST_ELEMENT ].substring ( 8 , 17 ) + '. Please contact customer support.';
                }   // FALSE (The date is in an unexpected format.) block, if ( intDatePart3Length === 4 )
            }   // FALSE (The date must be reformatted to go into the HTML5 date control.) block, if ( intDatePart1Length === 4 )
        }   // FALSE (Input parameter pastrRegExpGroups is an array.) block, if ( LLCommon.IsString ( pastrRegExpGroups ) )
    }   // FormatDate4Html5DatePicker method


    GatherAdditionalFieldValues ( poFormControls )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GatherAdditionalFieldValues

            Method Goal:        Scan the array of form controls and construct a
                                list of their names and values.

            Input:              poFormControls  = Array containing objects for
                                                  each input control in the form

            Output:             The return value is an object that contains an
                                array of elements, each having ControlId and
                                ControlValue properties.

            Remarks:            The Array.prototype.find() method returns the
                                first element in the array that meets criterion
                                defined by the arrow function; in other words,
                                the return value of the enclosing anonymous
                                function is the value assigned to element, an
                                implicitly defined function-scoped variable.

                                The code resembles LINQ, probably no accident.

            Reference:          Array.prototype.find()
                                https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/find
            --------------------------------------------------------------------
        */

        const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

        var   raoControls2Include = [ ];

        for ( var i = ARRAY_FIRST_ELEMENT,
                  iLen = poFormControls.length;
              i < iLen;
              i++ )
        {
            var oThisControl = poFormControls [ i ];
            var strIdOrName  = this.GetIdOrName ( poFormControls [ i ] );

            //  ----------------------------------------------------------------
            //  Check the ID and Name properties against the list.
            //  ----------------------------------------------`------------------

            if ( strIdOrName.length > EMPTY_STRING_LENGTH )
            {
                if ( this.Controls2Skip.find ( element => element === oThisControl.id ) === undefined && this.Controls2Skip.find ( element => element === oThisControl.name ) === undefined )
                {
                    if ( ( oThisControl.readOnly === false && ( ! Object.is ( oThisControl.value , undefined ) ) && oThisControl.value.length > EMPTY_STRING_LENGTH ) || this.fAllowEmptyFields )
                    {
                        var strValueLC = oThisControl.value.toLowerCase ( );

                        if ( strValueLC === this.TODAY || strValueLC === this.DteFunctionString )
                        {
                            oThisControl.value = this.UTCMidnightToday ( );
                        }   // if ( strValueLC === this.TODAY || strValueLC === this.DteFunctionString )

                        raoControls2Include.push ( {
                            ControlId    : strIdOrName ,
                            ClassName    : oThisControl.className ,
                            ControlValue : this.GetInputControlValue ( oThisControl )
                        } );
                    }   // if ( ( oThisControl.readOnly === false && ( ! Object.is ( oThisControl.value , undefined ) ) && oThisControl.value.length > EMPTY_STRING_LENGTH ) || this.fAllowEmptyFields )
                }   // if ( this.Controls2Skip.find ( element => element === oThisControl.id ) === undefined && this.Controls2Skip.find ( element => element === oThisControl.name ) === undefined )
            }   // if ( strIdOrName.length > EMPTY_STRING_LENGTH )
        }   // for ( var ARRAY_FIRST_ELEMENT = 0, iLen = poFormControls.length; i < iLen; i++ )

        return raoControls2Include;
    }   // GatherAdditionalFieldValues method


    GetControlValue ( pstrControlId , pdocControlArray )
    {
        /*
            ----------------------------------------------------------------
            Method Name:        GetControlValue

            Method Goal:        Search the array of control values returned
                                by method GatherAdditionalFieldValues, in
                                pdocControlArray, for a field that matches
                                the name in pstrControlId.

            Input:              pstrControlId     = String representation of
                                                    field ID or name for which
                                                    to search the array

                                pdocControlArray  = Array of name-value
                                                    pairs in which to search for
                                                    the value specified by string
                                                    pstrControlId

            Output:             If the name specified by pstrControlId exists
                                in pdocControlArray, return the matching
                                value.
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        var   retVal;

        var   fNotFound       = true;
        var   intIndex        = ARRAY_INVALID_INDEX;
        var   intLength       = pdocControlArray.length;

        while ( fNotFound )
        {
            intIndex++;

            if ( intIndex < intLength )
            {
                if ( pdocControlArray [ intIndex ].ControlId === pstrControlId )
                {
                    fNotFound = false;
                    retVal    = pdocControlArray [ intIndex ].ControlValue;
                }   // if ( pdocControlArray [ intIndex ].FieldName === pstrControlId )
            }   // TRUE (The index is in bounds.) block, if ( intIndex < intLength )
            else
            {
                fNotFound     = false;
            }   // FALSE (The index is out of bounds.) block, if ( intIndex < intLength )
        }   // while ( fNotFound )

        return Object.is ( retVal, undefined ) ? EMPTY_STRING : ( retVal === DBNULL ? EMPTY_STRING : retVal );
    }   // GetControlValue method


    GetDomainTenant4LeadId ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetDomainTenant4LeadId

            Method Goal:        Get the domain and tenant ID to which the lead
                                specified in the query string belongs, and stash
                                both into member variables, then return the name
                                of the domain.

            Input:              None

            Output:             This method parses the query string to get the
                                lead ID. This could be externalized, but it had
                                no further use to the object, so I left its
                                acquisition and disposition hidden.

                                This method employs an Ajax call to a method on
                                the Open Controller to look up the information
                                in the database.

                                This method could set the domain name into a
                                property directly. However, since its main
                                objective is the domain name, it made sense to
                                return it.

            2022/07/19 - DG     No sooner do I have the method working, and the
                                need arises to expose the lead ID as a property.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        var   strVeryBasicLeadInfo          = EMPTY_STRING;

        const GetUserIdForLoginName         = ( pstrLoginName ) =>
        {
            const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

            if ( pstrLoginName !== null && LLCommon.IsString ( pstrLoginName ) && pstrLoginName.length > EMPTY_STRING_LENGTH )
            {
                const strResultSet          = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                'GET',
                                                                {
                                                                   'loginName' : pstrLoginName
                                                                } );

                if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                {
                    const astrResultParts   = strResultSet.split ( PIPE_CHAR_SPLIT_MATCH );
                    return parseInt ( astrResultParts [ ARRAY_FIRST_ELEMENT ] );
                }   // TRUE (anticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                else
                {
                    return MINIMUM_STT_ENTITY_ID;
                }   // FALSE (unanticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
            }   // TRUE (anticipated outcome) block, if ( pstrLoginName !== null && LLCommon.IsString ( pstrLoginName ) && pstrLoginName.length > EMPTY_STRING_LENGTH )
            else
            {
                return MINIMUM_STT_ENTITY_ID;
            }   // FALSE (unanticipated outcome) block, if ( pstrLoginName !== null && LLCommon.IsString ( pstrLoginName ) && pstrLoginName.length > EMPTY_STRING_LENGTH )
        }   // const GetUserIdForLoginName         = ( pstrLoginName ) =>


        if ( Object.is ( this.STTLeadId , undefined ) || ( LLCommon.IsString ( this.STTLeadId ) && this.STTLeadId.length === EMPTY_STRING_LENGTH ) || ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId === NUMERIC_ZERO ) )
        {
            this.STTLeadId = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

            //  ----------------------------------------------------------------
            //  LocalStorage returns '0' when LLCommon has no lead ID to report,
            //  so it seems that LS is a numeric string that evaluates to zero.
            //  ----------------------------------------------------------------

            if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
            {
                this.STTLeadId              = localStorage.getItem ( 'leadId' );    // See function $scope.storyItemClick2, defined in LeadLife.Web/ClientApps/SalesApp/controllers/StorySoFarController.js.

                if ( this.STTLeadId === null )
                {
                    strVeryBasicLeadInfo    = 'ERROR in ' + strMethodName + ': Expected localStorage key PlayerURL is absent.';

                    LLCommon.LogException ( strVeryBasicLeadInfo );

                    return strVeryBasicLeadInfo;
                }   // if ( this.STTLeadId === null )

                strVeryBasicLeadInfo        = this.GetVeryBasicLeadInfo4LeadId ( );

                if ( strVeryBasicLeadInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                {
                    return ParseVeryBasicLendInfo ( strVeryBasicLeadInfo , this );
                }   // TRUE (anticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                else
                {
                    return LLCommon.LogException ( 'ERROR in ' + strMethodName + ': GetVeryBasicLeadInfo4LeadId in OpenController returned the following exception message: ' + strVeryBasicLeadInfo );
                }   // FALSE (unanticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
            }   // TRUE (Since the lead ID is absent, attempt to find it through the ExternalCRMId.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
            else
            {
                strVeryBasicLeadInfo        = LLCommon.DoAjax ( 'GetDomainTenant4LeadId' ,
                                                                'GET' ,
                                                                {
                                                                    'leadId'    : this.STTLeadId ,
                                                                    'userId'    : Object.is ( this.STTUserId    , undefined ) ? GetUserIdForLoginName ( localStorage.getItem ( 'UserName' ) ) : this.STTUserId ,
                                                                    'loginName' : Object.is ( this.STTLoginName , undefined ) ? localStorage.getItem ( 'UserName' )                           : this.STTLoginName
                                                                } );

                if ( strVeryBasicLeadInfo.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                {
                    var astrResultParts1    = strVeryBasicLeadInfo.split ( PIPE_CHAR_SPLIT_MATCH );

                    this.STTDomainId        = parseInt ( astrResultParts1 [ ARRAY_FIRST_ELEMENT  ] );
                    this.STTTenantId        = parseInt ( astrResultParts1 [ ARRAY_SECOND_ELEMENT ] );
                    this.STTUserId          = parseInt ( astrResultParts1 [ ARRAY_FOURTH_ELEMENT ] );
                    this.STTLoginName       =            astrResultParts1 [ ARRAY_FIFTH_ELEMENT  ];

                    return astrResultParts1 [ ARRAY_THIRD_ELEMENT ];
                }   // TRUE (anticipated outcome) block, if ( strAjaxResult.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                else
                {
                    return LLCommon.LogException ( 'ERROR in ' + strMethodName + this.COLON_SPACE + strVeryBasicLeadInfo + ' for lead ID ' + ( Object.is ( this.STTLeadId , undefined ) ? 'undefined' : this.STTLeadId ) );
                }   // FALSE (unanticipated outcome) block, if ( strAjaxResult.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            }   // FALSE (The lead ID is present.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
        }   // TRUE (The lead ID has yet to surface.) block, if ( Object.is ( this.STTLeadId , undefined ) || ( LLCommon.IsString ( this.STTLeadId ) && this.STTLeadId.length === EMPTY_STRING_LENGTH ) || ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId === NUMERIC_ZERO ) )
        else
        {
            if ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId > NUMERIC_ZERO )
            {
                strVeryBasicLeadInfo        = LLCommon.DoAjax ( 'GetDomainTenant4LeadId' ,
                                                                'GET' ,
                                                                {
                                                                    'leadId'    : this.STTLeadId ,
                                                                    'userId'    : this.STTUserId    === 'undefined' ? NUMERIC_ZERO : this.STTUserId ,
                                                                    'loginName' : this.STTLoginName === null        ? EMPTY_STRING : this.STTLoginName
                                                                } );

                if ( strVeryBasicLeadInfo.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                {
                    var astrResultParts2    = strVeryBasicLeadInfo.split ( PIPE_CHAR_SPLIT_MATCH );

                    this.STTDomainId2       = parseInt ( astrResultParts2 [ ARRAY_FIRST_ELEMENT  ] );
                    this.STTTenantId2       = parseInt ( astrResultParts2 [ ARRAY_SECOND_ELEMENT ] );
                    this.STTUserId2         = parseInt ( astrResultParts2 [ ARRAY_FOURTH_ELEMENT ] );
                    this.STTLoginName2      =            astrResultParts2 [ ARRAY_FIFTH_ELEMENT  ];

                    return astrResultParts2 [ ARRAY_THIRD_ELEMENT ];
                }   // TRUE (anticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                else
                {
                    if ( strVeryBasicLeadInfo.indexOf ( 'The specified Lead ID' ) > INDEXOF_NOT_FOUND && strVeryBasicLeadInfo.indexOf ( 'cannot be found in the database.' ) > INDEXOF_NOT_FOUND )
                    {
                        return LLCommon.LogException ( 'ERROR in ' + strMethodName + this.COLON_SPACE + 'No lead record could be identified.' ) ;
                    }   // TRUE (anticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( 'The specified Lead ID' ) > INDEXOF_NOT_FOUND && strVeryBasicLeadInfo.indexOf ( 'cannot be found in the database.' ) > INDEXOF_NOT_FOUND )
                    else {
                        return LLCommon.LogException ( 'ERROR in ' + strMethodName + this.COLON_SPACE + strVeryBasicLeadInfo ) ;
                    }   // FALSE (unanticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( 'The specified Lead ID' ) > INDEXOF_NOT_FOUND && strVeryBasicLeadInfo.indexOf ( 'cannot be found in the database.' ) > INDEXOF_NOT_FOUND )
                }   // FALSE (unanticipated outcome) block, if ( strVeryBasicLeadInfo.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            }   // TRUE (anticipated outcome) block, if ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId > NUMERIC_ZERO )
            else
            {
                this.STTLeadId = parseInt ( this.STTLeadId );
                strVeryBasicLeadInfo        = 'ERROR in ' + strMethodName + ': No lead record could be identified.';
                LLCommon.LogException ( strVeryBasicLeadInfo );

                 return strVeryBasicLeadInfo;
            }   // FALSE (unanticipated outcome) block, if ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId > NUMERIC_ZERO )
        }   // FALSE (The lead ID has already surfaced.) block, if ( Object.is ( this.STTLeadId , undefined ) || ( LLCommon.IsString ( this.STTLeadId ) && this.STTLeadId.length === EMPTY_STRING_LENGTH ) || ( Number.isInteger ( this.STTLeadId ) && this.STTLeadId === NUMERIC_ZERO ) )


        function ParseVeryBasicLendInfo ( pstrBasicLeadInfo , poSelf )
        {
            //  ----------------------------------------------------------------
            //  Function Name:  ParseVeryBasicLendInfo
            //
            //  Arguments:      pstrBasicLeadInfo   = Logical negate delimited
            //                                        string returned by method
            //                                        GetVeryBasicLeadInfo4ExternamCRMId
            //                                        on OpenController
            //
            //                  poSelf              = Reference to object in which
            //                                        ParseVeryBasicLendInfo is
            //                                        defined, so that its
            //                                        properties can be set
            //
            //  Returns         void
            //
            //  Remarks:        Parse a string that resembles this:
            //
            //                      1310524¬Demo I¬Transaction¬Transaction.demoi@@gmail.com¬¬SA73722¬05/05/2023 02:40:25¬05/05/2023 04:52:09¬1363¬1374¬Sweet_Assist
            //  ----------------------------------------------------------------

            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            const POS_LEAD_ID       = 0;    // 0    1310524
            const POS_LAST_NAME     = 1;    // 1    Demo I
            const POS_FIRST_NAME    = 2;    // 2    Transaction
            const POS_EMAIL         = 3;    // 3    Transaction.demoi@@gmail.com
            const POS_MOBILE_PHONE  = 4;    // 4    <MobilePhone>
            const POS_EXTERNALCRMID = 5;    // 5    SA73722
            const POS_CREATED_DT    = 6;    // 6    05/05/2023 02:40:25
            const POS_MODIFIED_DT   = 7;    // 7    05/05/2023 04:52:09
            const POS_DOMAIN_ID     = 8;    // 8    1363
            const POS_TENANT_ID     = 9;    // 9    1374
            const POS_DOMAIN_NAME   = 10;   // 10   Sweet_Assist

            const astrLeadInfos = pstrBasicLeadInfo.split ( poSelf.LOGICAL_NEGATE );

            poSelf.STTDomainId      = parseInt ( astrLeadInfos [ POS_DOMAIN_ID ] );
            poSelf.STTTenantId      = parseInt ( astrLeadInfos [ POS_TENANT_ID ] );
            poSelf.STTLeadId        = parseInt ( astrLeadInfos [ POS_LEAD_ID ] );

            poSelf.STTLeadBasicInfo = new VeryBasicLeadInfo ( pstrBasicLeadInfo );

            return astrLeadInfos [ POS_DOMAIN_NAME ];
        }   // function ParseVeryBasicLendInfo
    }   // GetDomainTenant4LeadId method


    GetElementByName ( pstrName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetElementByName

            Method Goal:        Get the first element in the document that
                                matches the name specified by pstrName.

            Input:              pstrName = Name for which to search the DOM, or
                                           a name followed by an asterisk,
                                           without an intervening space

            Output:             If the method succeeds, its return value is the
                                first node that matches the name unless the last
                                character is an asterisk, in which case the
                                return value is a nodeList that contains the
                                JavaScript representation of each matching node.

                                A message that starts with ERR_GETELEMENTBYNAME
                                is written on the console log when two or more
                                elements match the specified element name.

                                In the event that no matches are found, the
                                error message so indicates by showing the length
                                of the nodeList.
            --------------------------------------------------------------------
        */

        const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

        var   intPosAsterisk = pstrName.indexOf ( ASTERISK_CHAR );
        var   strName2Match  = intPosAsterisk === INDEXOF_NOT_FOUND ? pstrName : pstrName.substr ( SUBSTRING_FIRST_CHAR , pstrName.length - 1 );

        if ( strName2Match in this.LeadColNames2CustomFieldNames )
        {
            strName2Match    = this.LeadColNames2CustomFieldNames [ strName2Match ];
        }   // if ( strName2Match in poControls2Skip )

        var  oMatchingNodes  = document.getElementsByName ( strName2Match );

        if ( intPosAsterisk === INDEXOF_NOT_FOUND )
        {   // The caller expects exactly one element.

            switch ( oMatchingNodes.length )
            {
                case ARRAY_NOT_EMPTY:
                    break;
                case ARRAY_IS_EMPTY:
                default:
                    console.log ( this.ERR_GETELEMENTBYNAME + this.COLON_SPACE + strName2Match + ' matches ' + oMatchingNodes.length + ' elements in the document.');
                    break;
            }   // switch ( oMatchingNodes.length )

            if ( oMatchingNodes.length > ARRAY_IS_EMPTY )
            {
                return oMatchingNodes [ ARRAY_FIRST_ELEMENT ];
            }   // TRUE (anticipated outcome) block, if ( oMatchingNodes.length > ARRAY_IS_EMPTY )
        }   // TRUE (The caller expects one element.) block, if ( intPosAsterisk === this.INDEXOF_NOT_FOUND )
        else
        {   // The caller expects an array of elements. The nodeList can be treated like an array.
            return oMatchingNodes;
        }   // FALSE (The caller expects an array of elements.) block, if ( intPosAsterisk === this.INDEXOF_NOT_FOUND )
    }   // GetElementByName method


    GetElementByNameInContainer ( pstrName , poContainerElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetElementByNameInContainer

            Method Goal:        Get the first element in the document that
                                matches the name specified by pstrName.

            Input:              pstrName           = Name for which to search
                                                     the DOM, or a name followed
                                                     by an asterisk without an
                                                     intervening space

                                poContainerElement = Reference to the element to
                                                     be searched for matching
                                                     elements

            Output:             If the method succeeds, its return value is the
                                first node that matches the name unless the last
                                character is an asterisk, in which case the return
                                value is a nodeList that contains the JavaScript
                                representation of each matching node.

                                A message that starts with ERR_GETELEMENTBYNAME
                                is written on the console log when two or more
                                elements match the specified element name.

                                In the event that no matches are found, the
                                error message so indicates by showing the length
                                of the nodeList.
            --------------------------------------------------------------------
        */

        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        var   intPosAsterisk   = pstrName.indexOf ( ASTERISK_CHAR );
        var   strName2Match    = intPosAsterisk === this.INDEXOF_NOT_FOUND ? pstrName : pstrName.substring ( this.SUBSTRING_START , pstrName.length - this.NUMERIC_PLUS_ONE );

        if ( strName2Match in this.LeadColNames2CustomFieldNames )
        {
            strName2Match      = this.LeadColNames2CustomFieldNames [ strName2Match ];
        }   // if ( strName2Match in poControls2Skip )

        var   oNodesInDocument = document.getElementsByName ( strName2Match );

        if ( oNodesInDocument.length === ARRAY_IS_EMPTY )
        {
            oNodesInDocument   = $ ( '#' + strName2Match )
        }   // if ( oNodesInDocument.length === ARRAY_IS_EMPTY )

        var   oMatchingNodes   = [ ];

        for ( var iThis = ARRAY_FIRST_ELEMENT, iLen = oNodesInDocument.length;
                  iThis < iLen;
                  iThis++ )
        {
            var docContainingForm = this.IdentifyContainingForm ( oNodesInDocument [ iThis ] );

            if ( docContainingForm === undefined )
            {   // The element lies outside of all forms in the document.
                continue;
            }   // TRUE (unanticipated outcome - The element lies outside of any form element.) block, if ( docContainingForm === undefined )
            else
            {   // The element belongs to a form, but is is the right one?
                if ( docContainingForm.name === poContainerElement.name || docContainingForm.id === poContainerElement.id )
                {   // The element lies within the target form. Add it to the list.
                    oMatchingNodes.push ( oNodesInDocument [ iThis ] );
                }   // if ( docContainingForm.name === poContainerElement.name || docContainingForm.id === poContainerElement.id )
            }   // FALSE (anticipated outcome - The element lies within A form.) block, if ( docContainingForm === undefined )
        }   // for ( var iThis = ARRAY_FIRST_ELEMENT, iLen = oNodesInDocument.length; iThis < iLen; iThis++ )

        if ( intPosAsterisk === this.INDEXOF_NOT_FOUND )
        {   // The caller expects exactly one element.

            switch ( oMatchingNodes.length )
            {
                case ARRAY_NOT_EMPTY:
                    break;
                case ARRAY_IS_EMPTY:
                default:
                    console.log ( this.ERR_GETELEMENTBYNAME + this.COLON_SPACE + strName2Match + ' matches ' + oMatchingNodes.length + ' elements in the document.');
                    break;
            }   // switch ( oMatchingNodes.length )

            if ( oMatchingNodes.length > ARRAY_IS_EMPTY )
            {
                return oMatchingNodes [ ARRAY_FIRST_ELEMENT ];
            }   // TRUE (anticipated outcome) block, if ( oMatchingNodes.length > ARRAY_IS_EMPTY )
        }   // TRUE (The caller expects one element.) block, if ( intPosAsterisk === INDEXOF_NOT_FOUND )
        else
        {   // The caller expects an array of elements. The nodeList can be treated like an array.
            return oMatchingNodes;
        }   // FALSE (The caller expects an array of elements.) block, if ( intPosAsterisk === INDEXOF_NOT_FOUND )
    }   // GetElementByNameInContainer method


    GetExtension ( pstrUrlOrFileName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetExtension

            Method Goal:        Parse the extension from a URL or file
                                name.

            Input:              pstrUrlOrFileName = string to parse

            Output:             If string pstrUrlOrFileName is a well
                                formed file name or URL, the extension
                                is extracted and returned. Otherwise,
                                the return value is the empty string.

            Remarks:            Any of the following characters, /, ?,
                                and =, cause the candidate extension to
                                be truncated.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrUrlOrFileName ) )
        {
            if ( pstrUrlOrFileName.indexOf ( FULL_STOP ) > INDEXOF_NOT_FOUND )
            {
                const strExtensionCandidate = pstrUrlOrFileName.substring ( pstrUrlOrFileName.lastIndexOf ( FULL_STOP ) );

                const intPosPathSeparator   = strExtensionCandidate.indexOf ( PATH_SEPARATOR_CHAR );
                const intPosQueryStringArg1 = strExtensionCandidate.indexOf ( QUERY_STRING_START_DELIMITER );
                const intPosEqualsCharacter = strExtensionCandidate.indexOf ( EQUALS_CHAR );

                if ( intPosPathSeparator === INDEXOF_NOT_FOUND && intPosQueryStringArg1 === INDEXOF_NOT_FOUND && intPosEqualsCharacter === INDEXOF_NOT_FOUND )
                {
                    return strExtensionCandidate;
                }   // TRUE (anticipated outcome) block, if ( intPosPathSeparator === INDEXOF_NOT_FOUND && intPosQueryStringArg1 === INDEXOF_NOT_FOUND && intPosEqualsCharacter === INDEXOF_NOT_FOUND )
            }   // TRUE (anticipated outcome) block, if ( pstrUrlOrFileName.indexOf ( FULL_STOP ) > INDEXOF_NOT_FOUND )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrUrlOrFileName ) )

        return EMPTY_STRING
    }   // GetExtension method


    GetElementValue ( podcElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetElementValue

            Method Goal:        Get the value of an input element, using that
                                term in its broadest sense to cover ALL types of
                                input elements.

            Input:              podcElement       = Reference to HtmlElement in
                                                    form from which to read the
                                                    current value as either an
                                                    HTMLElement or the string
                                                    representation of its ID
                                                    attribute.

            Output:             If the element identified by podcElement exists,
                                return its intrinsic value.
            --------------------------------------------------------------------
        */

        function ResolvePickListValue ( pstrElementId , pstrInputValue )
        {
            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            console.log ( strMethodName + ' Arguments: pstrElementId = ' + pstrElementId + ', pstrInputValue = ' + pstrInputValue );

            try {
                if ( window._PickListValues !== undefined && window._PickListValues !== null && ! ( LLCommon.IsString ( window._PickListValues ) ) )
                {
                    const objTargetPickList = window._PickListValues [ pstrElementId ];

                    if ( objTargetPickList !== undefined && objTargetPickList !== null )
                    {
                        console.log ( strMethodName + ' objTargetPickList Length = ' + objTargetPickList.length );
                        const objMatched    = objTargetPickList.find ( ( element ) => element.DisplayText === pstrInputValue );
                        return objMatched === undefined ? pstrInputValue : objMatched.Name;
                    }   // TRUE (The input element maps to a pick list.) block, if ( objTargetPickList !== undefined && objTargetPickList !== null )
                    else
                    {
                        return pstrInputValue;
                    }   // FALSE (The input eleement doesn't map to a pick list.) block, if ( objTargetPickList !== undefined && objTargetPickList !== null )
                }   // TRUE (anticipated outcome: The list of pick lists is populated.) block, if ( window._PickListValues !== undefined && window._PickListValues !== null && !LLCommon.IsString ( window._PickListValues )
                else
                {
                    return pstrInputValue;
                }   // FALSE (unanticipated outcome: The list of pick lists is undefined.) block, if ( window._PickListValues !== undefined && window._PickListValues !== null && !LLCommon.IsString ( window._PickListValues )
            }
            catch ( ex )
            {
                console.log ( strMethodName + ' Exception caught: ' + ex.message );
                LLCommon.LogException ( ex );
            }
        }   // private function ResolvePickListValue


        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const docTargetElement  = LLCommon.IsString ( podcElement ) ? document.getElementById ( podcElement ) : podcElement;

        if ( docTargetElement !== null )
        {
            switch ( docTargetElement.nodeName )
            {
                case 'INPUT':
                    switch ( docTargetElement.type )
                    {
                        case 'submit':
                        case 'reset':
                        case 'button':
                        case 'image':
                        case 'hidden':
                        case 'password':
                            return null;

                        case 'radio':
                        case 'checkbox':        // The value of a RADIO or a CHECKBOX is meaningless; its Checked property is the value that matters.
                            return docTargetElement.checked;

                        case 'date':
                            return this.FormatDate4Html5DatePicker ( this.DatePartsFromDatabaseValue ( docTargetElement.outerHTML ) );

                        default:
                            return ResolvePickListValue ( docTargetElement.id , docTargetElement.value );
                    }   // switch ( docTargetElement.type )

                case 'SELECT':
                    switch ( docTargetElement.type )
                    {
                        case 'select-one':
                            return this.FormatDate4Html5DatePicker ( this.DatePartsFromDatabaseValue ( docTargetElement.outerHTML ) );
                        default:
                            return null;    // Only the select-one type is currently supported.
                    }   // switch ( docTargetElement.type )

                case 'TEXTAREA':
                    return docTargetElement.innerHTML.length > EMPTY_STRING_LENGTH
                           ? docTargetElement.innerHTML
                           : docTargetElement.value;

                default:
                    return null;
            }   // switch ( docTargetElement.nodeName )
        }   // TRUE (anticipated outcome) block, if ( docTargetElement !== null )
        else
        {
            return null;
        }   // FALSE (unanticipated outcome) block, if ( docTargetElement !== null )
    }   // GetElementValue method


    GetFieldValue ( pdocField , poFieldValueArray )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetFieldValue

            Method Goal:        Search the array of field values returned by
                                method GetSelectedInfo4LeadId, in array
                                poFieldValueArray, for a field that matches
                                the name in pstrFieldName.

            Input:              pdocField         = Reference to HtmlElement in
                                                    form from which to read the
                                                    current value

                                poFieldValueArray = Array of name-value
                                                    pairs for which to
                                                    search for a named value

            Output:             If the name specified by pstrFieldName exists
                                in poFieldValueArray, return the matching
                                value.
            --------------------------------------------------------------------
        */

        function FixBullhornMultiSelect ( pstrValue )
        {
            const BH_PREFIX     = '[\r\n  \"';
            const BH_SUFFIX     = '\"\r\n]';

            if ( pstrValue.startsWith ( BH_PREFIX ) && pstrValue.endsWith ( BH_SUFFIX ) )
            {
                return pstrValue.substring ( BH_PREFIX.length , pstrValue.length - ( BH_PREFIX.length - 2 ) );
            }   // TRUE (The input is a Bullhorn multiple-select value.) block, if ( pstrValue.startsWith ( BH_PREFIX ) && pstrValue.endsWith ( BH_SUFFIX ) )
            else
            {
                return pstrValue;
            }   // FALSE (The input is an ordinary text value.) block, if ( pstrValue.startsWith ( BH_PREFIX ) && pstrValue.endsWith ( BH_SUFFIX ) )
        }   // function FixBullhornMultiSelect


        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        const docMappedFldId  = this.IsWiseAgentMobilePage ( )
                                    ? this.CustomFieldNames2LeadColumnNames [ this.MobilePage_WA_Contact_FormFieldMap [ pdocField.id ] ] === undefined
                                        ? this.MobilePage_WA_Contact_FieldMap [ pdocField.id ]
                                        : this.CustomFieldNames2LeadColumnNames [ this.MobilePage_WA_Contact_FieldMap [ pdocField.id ] ]
                                    : pdocField.id;
        const docFieldElement = poFieldValueArray.find ( element => element.FieldName.toLowerCase ( ) === docMappedFldId.toLowerCase ( ) );

        if ( Object.is ( docFieldElement , undefined ) || docFieldElement.FieldValue === DBNULL )
        {
            return EMPTY_STRING;
        }   // TRUE (Dispatch both degenerate cases in one short circuited expression.) block, if ( Object.is ( retVal , undefined ) || retVal === DBNULL )
        else
        {
            const oLLMaskInfo = this.GetUIDisplaySubTypes ( ).find ( data => pdocField.className.indexOf ( data.ClassName ) > INDEXOF_NOT_FOUND );

            if ( Object.is ( oLLMaskInfo , undefined ) )
            {
                return FixBullhornMultiSelect ( docFieldElement.FieldValue );
            }   // TRUE (The field is unmasked. Therefore, there is nothing to do.) block, if ( Object.is ( oLLMaskInfo , undefined ) )
            else
            {
                if ( oLLMaskInfo.StoreAsNumericString )
                {
                    return FixBullhornMultiSelect ( docFieldElement.FieldValue.replace ( /\D/g , EMPTY_STRING ) );
                }   // TRUE (The field representation is a numeric string.) block, if ( oLLMaskInfo.StoreAsNumericString )
                else
                {
                    return FixBullhornMultiSelect ( docFieldElement.FieldValue );
                }   // FALSE (The field representation is alphanumeric.) block, if ( oLLMaskInfo.StoreAsNumericString )
            }   // FALSE (There is an input mask associated with one of the CSS classes applied to the element.) block, if ( Object.is ( oLLMaskInfo , undefined ) )
        }   // FALSE (This is one of those rare cases where it makes more sense to put the anticipated outcome in the ELSE block. ) block, if ( Object.is ( retVal , undefined ) || retVal === DBNULL )
    }   // GetFieldValuey method


    GetFileName ( pstrUrlOrFileName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetFileName

            Method Goal:        Parse the filename from a URL or file name.

            Input:              pstrUrlOrFileName = string to parse

            Output:             If string pstrUrlOrFileName is a well formed
                                file name or URL, the filename is extracted and
                                returned. Otherwise, the return value is the
                                empty string.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrUrlOrFileName ) )
        {
            const intPosLastUrlPathDelimiter = pstrUrlOrFileName.lastIndexOf ( PATH_SEPARATOR_CHAR );
            return pstrUrlOrFileName.substring ( LLCommon.OrdinalFromIndex ( intPosLastUrlPathDelimiter > ARRAY_INVALID_INDEX ? intPosLastUrlPathDelimiter : pstrUrlOrFileName.lastIndexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) ) );
        }   // if ( LLCommon.IsString ( pstrUrlOrFileName ) )

        return EMPTY_STRING;
    }   // GetFileName method


    GetIdOrName ( pdocElement )
    {
        /*
            --------------------------------------------------------------------
            Name:       GetIdOrName

            Goal:       Get the ID or Name of an element if it has one.

            Arguments:  pdocElement = Reference to the document element to query

            Returns:    String, possibly empty, containing the ID of the element
                        if it has one, its Name, if it has one, or the empty
                        string, if it has neither
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( !Object.is ( pdocElement , undefined ) )
        {   // First things first, pdocElement MUST be defined.
            switch ( pdocElement.type )
            {   // Some types of controls never have meaningful values.
                case 'submit':
                case 'reset':
                case 'button':
                case 'image':
                case 'hidden':
                case 'password':

                    return EMPTY_STRING;

                case 'radio':
                case 'checkbox':
                case 'date':
                case 'select-one':
                case 'textarea':

                default:

                    if ( !Object.is ( pdocElement.id , undefined ) && pdocElement.id.length > EMPTY_STRING_LENGTH )
                    {   // Since it must be unique within a well-formed document, an ID is preferable to a Name.
                        return pdocElement.id;
                    }   // TRUE (Preferred outcome) block, if ( !Object.is ( pdocElement.id  undefined ) && pdocElement.id.length > EMPTY_STRING_LENGTH )
                    else
                    {   // However, if the ID is unspecified, a Name that may not be unique is better than nothing.
                        if ( !Object.is ( pdocElement.name , undefined ) && pdocElement.name.length > EMPTY_STRING_LENGTH )
                        {
                            return pdocElement.name;
                        }   // TRUE (anticipated outcome) block, if ( !Object.is ( pdocElement.name , undefined ) && pdocElement.name.length > EMPTY_STRING_LENGTH )
                        else
                        {   // If neither an ID nor a Name has the control, it must be skipped.
                            return EMPTY_STRING;
                        }   // FALSE (unanticipated outcome) block, if ( !Object.is ( pdocElement.name , undefined ) && pdocElement.name.length > EMPTY_STRING_LENGTH )
                    }   // FALSE (Acceptable, though less desired, outcome) block, if ( !Object.is ( pdocElement.id , undefined ) && pdocElement.id.length > EMPTY_STRING_LENGTH )
            }   // switch ( pdocElement.type )
        }   // TRUE (anticipated outcome) block, if ( !Object.is ( pdocElement , undefined ) )
        else
        {   // Likewise, when the control, itself, is undefined, the only option is to return the empty string.
            return EMPTY_STRING;
        }   // FALSE (unanticipated outcome) block, if ( !Object.is ( pdocElement , undefined ) )
    }   // GetIdOrName method


    GetInputControlsByType ( pstrTypeName, pstrContainerName )
    {
        /*
            --------------------------------------------------------------------
            Name:       GetInputControlsByType

            Goal:       Return an array of Input controls (elements) of the
                        specified type, which may be empty.

            Arguments:  pstrTypeName      = String representation of type name,
                                            or asterisk for all types.

                        pstrContainerName = String representation of container
                                            class or ID (optional), prefixed
                                            with . to designate a class name, #
                                            to designate an ID, or undecorated
                                            to designate a regular Name, when
                                            undefined, the container is inferred
                                            to be the Document

            Returns:    Array, possibly empty, of Input elements of the Type
                        specified by pstrTypeName, which is required and cannot
                        be either undefined, null, or the empty string
            --------------------------------------------------------------------
        */

        const strMethodName        = LLCommon.GetNameOfCurrentFunction ( );

        var   raoDomInputElements  = [ ];

        var   fDocObjectIsRoot     = false;
        var   fHaveAllFormElements = false;
        var   fHaveRootObject      = false;

        var   strObjectName;
        var   odocRootElement;
        var   odocChildren;
        var   odocTemp;

        if ( LLCommon.IsString ( pstrTypeName ) )
        {
            if ( Object.is ( pstrContainerName , undefined ) )
            {
                fHaveRootObject    = true;
                fDocObjectIsRoot   = true;
            }   // TRUE (The optional argument was omitted.) block, if ( Object.is ( pstrContainerName , undefined ) )
            else
            {
                if ( LLCommon.IsString ( pstrContainerName ) )
                {
                    if ( pstrContainerName.length > EMPTY_STRING_LENGTH )
                    {
                        switch ( pstrContainerName.substring ( this.SUBSTRING_FIRST_CHAR ,
                                                               this.SUBSTRING_SECOND_CHARACTER ) )
                        {   // Evaluate the first character of a string.
                            case JQUERY_SELECTOR_IS_CLASSNAME:
                                strObjectName       = pstrContainerName.substring ( this.SUBSTRING_SECOND_CHARACTER );        // The rest of the string is the class name.
                                odocTemp            = document.getElementsByClassName ( strObjectName );

                                if ( ( !Object.is ( odocTemp , undefined ) ) && ( !Object.is ( odocTemp , null ) ) )
                                {   // For the record, it's a HTMLCollection.
                                    odocRootElement = odocTemp [ ARRAY_FIRST_ELEMENT ];
                                    fHaveRootObject = true;
                                }   // if ( ( !Object.is ( odocTemp , undefined ) ) && ( !Object.is ( odocTemp , null ) ) )

                                break;  // case JQUERY_SELECTOR_IS_CLASSNAME

                            case JQUERY_SELECTOR_IS_ELEMENT_ID:
                                strObjectName       = pstrContainerName.substring ( this.SUBSTRING_SECOND_CHARACTER );        // The rest of the string is the element ID.
                                odocTemp            = document.getElementById ( strObjectName );

                                if ( !Object.is ( odocTemp , undefined ) )
                                {   // It's an Element of whatever type has that ID.
                                    odocRootElement = odocTemp;
                                    fHaveRootObject = true;
                                }   // if ( !Object.is ( odocTemp , undefined ) )

                                break;  // case JQUERY_SELECTOR_IS_ELEMENT_ID

                            default:
                                odocTemp            = document.getElementsByName ( pstrContainerName );

                                if ( ( !Object.is ( odocTemp , undefined ) ) && ( !Object.is ( odocTemp , null ) ) && odocTemp.length >= ARRAY_NOT_EMPTY )
                                {   // For the record, and nothwithstanding documentation to the contrary, it's a NodeList. See https://developer.mozilla.org/en-US/docs/Web/API/Document/getElementsByName.
                                    odocRootElement = odocTemp [ ARRAY_FIRST_ELEMENT ];
                                    fHaveRootObject = true;
                                }   // if ( ( !Object.is ( odocTemp , undefined ) ) && ( !Object.is ( odocTemp , null ) ) && odocTemp.length >= ARRAY_NOT_EMPTY )

                                break;  // default
                        }   // switch ( pstrContainerName.substring ( this.SUBSTRING_FIRST_CHAR, this.SUBSTRING_SECOND_CHARACTER ) );
                    }   // TRUE (anticipated outcome) block, if ( pstrContainerName.length > EMPTY_STRING_LENGTH )
                    else
                    {
                        fHaveRootObject             = true;
                        fDocObjectIsRoot            = true;
                    }   // FALSE (unanticipated outcome) block, if ( pstrContainerName.length > EMPTY_STRING_LENGTH )
                }   // TRUE (anticipated outcome unless pstrContainerName is undefined) block, if ( LLCommon.IsString ( pstrContainerName ) )
                else
                {   // Since it is neither a string, nor null, see whether it is an array.
                    if ( Array.isArray ( pstrContainerName ) )
                    {
                        fHaveAllFormElements        = true;
                    }   // TRUE (It's an array, and most of our work is done.) block, if ( Array.isArray ( pstrContainerName ) )
                    else
                    {
                        fHaveRootObject             = true;
                        fDocObjectIsRoot            = true;
                    }   // FALSE (It isn't an array, so we behave as if the root object is the desired container.) block, if ( Array.isArray ( pstrContainerName ) )
                }   // FALSE (unanticipated outcome unless pstrContainerName is undefined) block, if ( LLCommon.IsString ( pstrContainerName ) )
            }   // FALSE (The optional argument is present.

            //  ----------------------------------------------------------------
            //  Receiving the entire list of form elements is a special case.
            //  ----------------------------------------------------------------

            if ( fHaveAllFormElements )
            {   // The optional second parameter, pstrContainerName, is an array of custom JavaScript objects.
                const intFormControlsCount          = pstrContainerName.length;

                for ( var intI = ARRAY_FIRST_ELEMENT;
                          intI < intFormControlsCount;
                          intI++ )
                {
                    if ( pstrContainerName [ intI ].ControlType === this.FORM_CONTROL_IS_INPUT )
                    {
                        if ( pstrContainerName [ intI ].ControlSubType === pstrTypeName )
                        {
                            raoDomInputElements.push ( pstrContainerName [ intI ].WholeControl );
                        }   // if ( pstrContainerName [ intI ].ControlSubType === pstrTypeName )
                    }   // if ( pstrContainerName [ intI ].ControlType === this.FORM_CONTROL_IS_INPUT )
                }   // for ( var intI = ARRAY_FIRST_ELEMENT; intI < intFormControlsCount; intI++ )
            }   // TRUE (The caller gave us all the form controls and their values per the database.) block, if ( fHaveAllFormElements )
            else
            {
                if ( fHaveRootObject )
                {
                    if ( fDocObjectIsRoot )
                    {
                        odocChildren                = document.getElementsByTagName ( 'input' );
                    }   // TRUE (The root object is Document.) block, if ( fDocObjectIsRoot )
                    else
                    {
                        odocChildren                = odocRootElement.getElementsByTagName ( 'input' );
                    }   // FALSE (The root object is a child element.) block, if ( fDocObjectIsRoot )
                }   // if ( fHaveRootObject )

                const intNInputElements = odocChildren.length;

                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < intNInputElements;
                          intJ++ )
                {
                    if ( odocChildren [ intJ ].type === pstrTypeName || pstrTypeName === ASTERISK_CHAR )
                    {
                        raoDomInputElements.push ( odocChildren [ intJ ] );
                    }   // if ( odocChildren [ intJ ].type === pstrTypeName || pstrTypeName === ASTERISK_CHAR )
                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intNInputElements; intJ++ )
            }   // FALSE (The caller left it to us to get the controls, wich we'll return sans database values.) block, if ( fHaveAllFormElements )
        }   // if ( LLCommon.IsString ( pstrTypeName ) )

        return raoDomInputElements;
    }   // GetInputControlsByType method


    GetInputControlValue ( poThisControl )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetInputControlValue

            Method Goal:        Get the value of an input control, which is a
                                function of its type.

            Input:              poThisControl = Reference to the HTML Input from
                                                which to return the value

            Output:             Value of control, which is its value property,
                                unless it is a checkbox.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( poThisControl.type === 'checkbox' )
        {
            return poThisControl.checked ? 'true' : EMPTY_STRING;
        }   // TRUE (Checkboxes are special because their value property is meaningless.) block, if ( poThisControl.type === 'checkbox' )
        else
        {
            return poThisControl.value;
        }   // FALSE (For all other input control types, the value can be taken at face value.) block, if ( poThisControl.type === 'checkbox' )
    }   // GetInputControlValue


    GetLabelForInputElement ( pdocElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetLabelForInputElement

            Method Goal:        Get the label for an INPUT element.

            Input:              pdocElement = JavaScript representation of the
                                              INPUT element for which to provide
                                              a label.

            Output:             If the element has an associated label, return
                                it. Otherwise, return its ID or name.

            Remarks:            This method throws when called with an element
                                that isn't an input.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( pdocElement.labels.length > ARRAY_IS_EMPTY )
        {
            return pdocElement.labels [ ARRAY_FIRST_ELEMENT ].innerText;
        }   // Since the element has an associated label, use it.) block, if ( pdocElement.labels.length > ARRAY_IS_EMPTY )
        else
        {
            return this.GetIdOrName ( pdocElement );
        }   // Since the element is unlabeled, use its ID or name.) block, if ( pdocElement.labels.length > ARRAY_IS_EMPTY )
    }   // GetLabelForInputElement method


    GetLocalTimeAsString ( pdtmJavaScriptDateObject  )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetLocalTimeAsString

            Method Goal:        Get the current local (machine) time and convert
                                it to a human-readable string in ISO8601 format.

            Input:              pdtmJavaScriptDateObject = Optional JavaScript
                                                           Date object to format

            Output:             The return value is the string representation of
                                the date, formatted as yyyy/mm/dd hh:mm:ss.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var   dtmThis       = Object.is ( pdtmJavaScriptDateObject , undefined ) ? new Date ( ) : pdtmJavaScriptDateObject;

        return dtmThis.getFullYear ( )
               + DEFAULT_DATE_SEPARATOR_CHAR
               + this.ApplyDatePartFixups ( dtmThis.getMonth ( ) )
               + DEFAULT_DATE_SEPARATOR_CHAR
               + this.ApplyTimePartFixups ( dtmThis.getDate ( ) )
               + this.SPACE_CHARACTER
               + this.ApplyTimePartFixups ( dtmThis.getHours ( ) )
               + TIME_SEPARATOR_CHAR
               + this.ApplyTimePartFixups ( dtmThis.getMinutes ( ) )
               + TIME_SEPARATOR_CHAR
               + this.ApplyTimePartFixups ( dtmThis.getSeconds ( ) )
               + DECIMAL_POINT
               + this.ApplyMillisecondsFixups ( dtmThis.getMilliseconds ( ) );
    }   // GetLocalTimeAsString method


    GetPathName ( pstrUrlOrFileName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetPathName

            Method Goal:        Parse the pathname from a URL or file name.

            Input:              pstrUrlOrFileName = string to parse

            Output:             If string pstrUrlOrFileName is a well formed
                                file name or URL, the pathname is extracted and
                                returned. Otherwise, the return value is the
                                empty string.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrUrlOrFileName ) )
        {
            const intPosLastUrlPathDelimiter = pstrUrlOrFileName.lastIndexOf ( PATH_SEPARATOR_CHAR );
            return pstrUrlOrFileName.substring ( SUBSTRING_FIRST_CHAR , intPosLastUrlPathDelimiter > ARRAY_INVALID_INDEX ? intPosLastUrlPathDelimiter : pstrUrlOrFileName.lastIndexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) );
        }   // if ( LLCommon.IsString ( pstrUrlOrFileName ) )

        return EMPTY_STRING;
    }   // GetPathName method


    GetPickListValues ( pstrSelectElementName , pfForceReinitialization )
    {
        /*
            --------------------------------------------------------------------
            Name:       GetPickListValues

            Goal:       Append OPTION elements to the SELECT elment named in
                        string pstrSelectElementName from CustomFieldValueLookup
                        table rows that have the CustomFieldId that matches the
                        CustomField row for which SystemProperty matches string
                        pstrSelectElementName.

            Arguments:  pstrSelectElementName   = This string represents the ID
                                                  of the SELECT element to which
                                                  to append an OPTION element for
                                                  each matching row in table
                                                  CustomFieldValueLookup for the
                                                  like-named (by SystemProperty)
                                                  Custom Field.

                        pfForceReinitialization = When logical TRUE, replace any
                                                  existing OPTION elements.

            Returns:    Since appending OPTION elements to a SELECT element is a
                        side effect, this method could return void. Neverthless,
                        it returns the count of options appended.

            Remarks:    Since the shadow object is also a SELECT element, it has
                        its own set of OPTION tags because elements are logical
                        objects, not references to which two or more elements
                        may point.
            --------------------------------------------------------------------
        */

        const strMethodName                         = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrSelectElementName ) )
        {
            try
            {
                const docSelectElement              = document.getElementById ( pstrSelectElementName );
                const docSelectShadowElement        = document.getElementById ( pstrSelectElementName + '_Shadow' );

                debugger;

                if ( docSelectElement !== null )
                {
                    if ( docSelectElement.tagName === 'SELECT' )
                    {
                        const docOptions            = docSelectElement.getElementsByTagName ( 'option' );

                        if ( docOptions != null )
                        {
                            const intCurrOptCount   = docOptions.length;

                            if ( intCurrOptCount === NUMERIC_ZERO || pfForceReinitialization )
                            {
                                const aobjPickListValues = LLCommon.DoAjax ( 'GetPickListValues',
                                                                              'GET',
                                                                              {
                                                                                  'systemProperty' : pstrSelectElementName ,
                                                                                  'tenantId'       : _LeadLifeJSHelpers.STTTenantId ,
                                                                                  'domainId'       : _LeadLifeJSHelpers.STTDomainId
                                                                              } );

                                if ( Array.isArray ( aobjPickListValues.PickListValues ) )
                                {
                                    const intOptionCount        = aobjPickListValues.PickListValues.length;

                                    if ( intOptionCount > NUMERIC_ZERO )
                                    {
                                        if ( pfForceReinitialization && intCurrOptCount > ARRAY_IS_EMPTY )
                                        {
                                            for ( var intI = LLCommon.OrdinalFromIndex ( intCurrOptCount );
                                                      intI > ARRAY_INVALID_INDEX;
                                                      intI-- )
                                            {   // Remove from the collection attached to the SELECT element.
                                                docSelectElement.remove ( intI );
                                            }   // for ( var intI = LLCommon.OrdinalFromIndex ( intCurrOptCount ); intI > ARRAY_INVALID_INDEX; intI-- )
                                        }   // if ( pfForceReinitialization && intCurrOptCount > ARRAY_IS_EMPTY )

                                        const docShadowOptions  = docSelectShadowElement === null ? null : docSelectShadowElement.getElementsByTagName ( 'option' );

                                        if ( Object.is ( docShadowOptions , undefined ) && docShadowOptions !== null && docShadowOptions.length > ARRAY_IS_EMPTY )
                                        {
                                            const intExistingShadowOptionCount = docShadowOptions.length;

                                            for ( var intJ = LLCommon.OrdinalFromIndex ( intExistingShadowOptionCount );
                                                      intJ > ARRAY_INVALID_INDEX;
                                                      intJ++ )
                                            {
                                                docSelectShadowElement.remove ( intJ );
                                            }   // for ( var intJ = LLCommon.OrdinalFromIndex ( intExistingShadowOptionCount ); intJ > ARRAY_INVALID_INDEX; intJ++ )
                                        }   // if ( Object.is ( docShadowOptions , undefined ) && docShadowOptions !== null && docShadowOptions.length > ARRAY_IS_EMPTY )

                                        for ( var intJ = ARRAY_FIRST_ELEMENT;
                                                  intJ < intOptionCount;
                                                  intJ++ )
                                        {
                                            var docOption       = document.createElement ( 'option' );

                                            docOption.value     = aobjPickListValues.PickListValues [ intJ ].Name
                                            docOption.innerHTML = aobjPickListValues.PickListValues [ intJ ].DisplayText

                                            docSelectElement.appendChild ( docOption );

                                            if ( docSelectShadowElement !== null )
                                            {
                                                var docShadowOption       = document.createElement ( 'option' );

                                                docShadowOption.value     = aobjPickListValues.PickListValues [ intJ ].Name
                                                docShadowOption.innerHTML = aobjPickListValues.PickListValues [ intJ ].DisplayText

                                                docSelectShadowElement.appendChild ( docShadowOption );
                                            }   // if ( docShadowOptions !== null )
                                        }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intOptionCount; intJ++ )

                                        return intOptionCount;
                                    }   // TRUE (anticipated outcome) block, if ( intOptionCount > NUMERIC_ZERO )
                                    else
                                    {
                                        LLCommon.LogException ( strMethodName + ': Element ID ' + pstrSelectElementName + ', associated with database field ' + pstrSelectElementName + ' has no pick list values in the database.' );
                                        return ARRAY_INVALID_INDEX;
                                    }   // FALSE (unanticipated outcome) block, if ( intOptionCount > NUMERIC_ZERO )
                                }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aobjPickListValues.PickListValues ) )
                                else
                                {
                                    LLCommon.Trace ( strMethodName + ': SalesTalk internal API GetPickListValues found NO pick list for input element = ' + pstrSelectElementName );
                                    return ARRAY_INVALID_INDEX;
                                }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aobjPickListValues.PickListValues ) )
                            }   // if ( intCurrOptCount === NUMERIC_ZERO )
                        }   // TRUE (anticipated outcome) block, if ( docOptions != null )
                        else
                        {
                            LLCommon.LogException ( strMethodName + ': Element ID ' + docSelectElement.id + ', though identified as a SELECT element, has no collection of option elements.' );
                            return ARRAY_INVALID_INDEX;
                        }   // FALSE (unanticipated outcome) block, if ( docOptions != null )
                    }   // TRUE (anticipated outcome) block, if ( docSelectElement.tagName === 'SELECT' )
                    else
                    {
                        LLCommon.LogException ( strMethodName + ': Element ID ' + docSelectElement.id + ' MUST be a SELECT element. Instead, it is a ' + docSelectElement.tagName + ' element.' );
                        return ARRAY_INVALID_INDEX;
                    }   // FALSE (unanticipated outcome) block, if ( docSelectElement.tagName === 'SELECT' )
                }   // TRUE (anticipated outcome) block, if ( docSelectElement !== null )
                else {
                    LLCommon.LogException ( strMethodName + ': Element ID ' + pstrSelectElementName + ' cannot be found in the document.' );
                    return ARRAY_INVALID_INDEX;
                }   // FALSE (unanticipated outcome) block, if ( docSelectElement !== null )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return ARRAY_INVALID_INDEX;
            }
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrSelectElementName ) )
        else
        {
            LLCommon.LogException ( strMethodName + ': Arguments pstrSelectElementName MUST be a String.' );
            return ARRAY_INVALID_INDEX;
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrSelectElementName ) )
    }   // GetPickListValues method


    GetSelectedInfo4LeadId ( pstrLeadId , pstrFieldNameList , pfCustomFieldsOnlyFlag )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetSelectedInfo4LeadId

            Method Goal:        Get the value, if any, of each field named in
                                comma-delimited list pstrFieldNameList, from
                                the custom fields associated with the lead ID
                                represented by pstrLeadId.

            Input:              pstrLeadId             = String representation
                                                         of LeadId for which to
                                                         retrieve info

                                pstrFieldNameList      = String containing the
                                                         comma-delimited list of
                                                         field names

                                pfCustomFieldsOnlyFlag = True to suppress all
                                                         but the fields listed
                                                         in pstrFieldNameList

            Output:             The return value is an array of JavaScript
                                objects, each having FieldName and FieldValue
                                properties.

            Remarks:            See _LeadLifeJSHelpers.STTLeadId for the ID of
                                the lead that is associated with the Talking
                                Point. However, please be advised that this
                                value is undefined unless object property
                                _LeadLifeJSHelpers.STTContext is equal to
                                _LeadLifeJSHelpers.STT_TP_CONTEXT. An alternate
                                method is by calling GetUrlParameter to read the
                                value from the query string.
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        var   strAjaxResult   = EMPTY_STRING;
        var   fAjaxError      = false;
        var   intTotalRetries = NUMERIC_ZERO;
        var   fKeepTrying     = true;

        LLCommon.Trace ( 'In method ' + strMethodName + ', AJAX URL = ' + LLCommon.AjaxUrlPrefix + 'Open/GetSelectedInfo4LeadIPost' );

        try
        {
            do  // while ( fKeepTrying )
            {
                if ( pfCustomFieldsOnlyFlag )
                {   // Return only the fields listed in pstrFieldNameList.
                    $.ajax (
                    {
                            type    : 'POST',
                            async   : false,
                            cache   : false,
                            url     : LLCommon.AjaxUrlPrefix + 'Open/GetSelectedInfo4LeadIPost',
                            data    : {
                                        'leadId'               : pstrLeadId,
                                        'CustomFields'         : pstrFieldNameList,
                                        'CustomFieldsOnlyFlag' : true,
                                        'tzOffsetMinutes'      : this.UtcOffsetMinutes
                                      },
                            success : function ( data )
                                      {
                                            if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                            {   // The above Ajax call returned a value. Capture it.
                                                strAjaxResult = data;
                                                return data;
                                            }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                      },
                            error   : function ( jqXHR , textStatus , errorThrown )
                                      {
                                            strAjaxResult = textStatus
                                                            + ' ' + jqXHR.responseText
                                                            + ' ' + errorThrown;
                                            fAjaxError    = true;
                                            return strAjaxResult;
                                      }
                    });
                }   // TRUE (The third argument has a truthy value.) block, if ( pfCustomFieldsOnlyFlag )
                else
                {   // Return the LastName, FirstName, Email, and MobilePhone in addition to the fields listed in pstrFieldNameList.

                    $.ajax (
                    {
                            type    : 'POST',
                            async   : false,
                            cache   : false,
                            url     : LLCommon.AjaxUrlPrefix + 'Open/GetSelectedInfo4LeadIPost',
                            data    : {
                                        'leadId'       : pstrLeadId,
                                        'CustomFields' : pstrFieldNameList
                                      },
                            success : function ( data )
                                      {
                                            if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                            {   // The above Ajax call returned a value. Capture it.
                                                strAjaxResult = data;
                                                return data;
                                            }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                      },
                            error   : function ( jqXHR , textStatus , errorThrown )
                                      {
                                            strAjaxResult = textStatus
                                                            + ' ' + jqXHR.responseText
                                                            + ' ' + errorThrown;
                                            fAjaxError    = true;
                                            return strAjaxResult;
                                      }
                    });
                }   // FALSE (The third argument has a falsy value.) block, if ( pfCustomFieldsOnlyFlag )

                if ( fAjaxError )
                {   // The API reported an error. Check the retry count.
                    if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                        intTotalRetries++;
                        fAjaxError = false;
                    }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    else
                    {   // The retry limit has been reached. Allow control to leave the do while loop.
                        fKeepTrying = false;
                    }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                else
                {   // The attempt succeeded. Post a log entry and leave the loop.
                    fKeepTrying = false;

                    if ( intTotalRetries === NUMERIC_ZERO )
                    {
                        if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetSelectedInfo4LeadIPost for lead record ' + this.STTLeadId + ' succeeded on the first try.' } } );
                        }   // if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                    }   // TRUE (ideal outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                    else
                    {
                        $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetSelectedInfo4LeadIPost for lead record ' + this.STTLeadId + ' succeeded after ' + intTotalRetries + ' retries.' } } );
                    }   // FALSE (less than ideal, but acceptable, outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
            } while ( fKeepTrying )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
            strAjaxResult = 'At ' + new Date ( ) + ', SalesTalk encountered an internal error while your updates to lead ID ' + this.STTLeadId + '. Please contact customer support for assistance, giving them the time and lead ID shown in this message.';
            alert ( strAjaxResult );
            return ex;
        }

        //  --------------------------------------------------------------------
        //  Though the success and error functions cannot see this, they can see
        //  variables defined within the scope of the closure in which they are
        //  called. Hence, the best solution to getting data out of them is by
        //  having the callbacks store it into the in-scope local variables and
        //  move the remaining processing code into the scope of the closure.
        //  --------------------------------------------------------------------

        if ( fAjaxError )
        {
            LLCommon.LogException ( strAjaxResult );
            return strAjaxResult;
        }   // if ( fAjaxError )

        console.log ( strMethodName + ': strAjaxResult = ' + strAjaxResult);

        if ( strAjaxResult.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND && strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
        {
            let anvp = strAjaxResult.split ( LOGICAL_NEGATE );

            if ( Object.is ( this.#dctInitialValues , undefined ) )
            {
                this.#dctInitialValues = { };
            }   // if ( Object.is ( #dctInitialValues , undefined ) )

            //  ----------------------------------------------------------------
            //  Because iterating one is trivial, this function returns its list
            //  of values as an array of Objects, each having two properties, a
            //  FieldName and a FieldValue, both represented as strings.
            //
            //  The internal object, this.#dctInitialValues, is treated as a
            //  Dictionary of string values indexed by string names to permit a
            //  quick lookup of the value for a named field, to determine if its
            //  value has changed.
            //  ----------------------------------------------------------------

            var raFields = [ ];

            for ( var intJ = ARRAY_FIRST_ELEMENT,
                      intK = anvp.length;
                  intJ < intK;
                  intJ++ )
            {   // Since the JavaScript string split method behavior deviates from what's needed here, StringSplitSharp takes its place.
                let theField = LLCommon.StringSplitSharp ( anvp [ intJ ] ,
                                                           EQUALS_CHAR ,
                                                           SPLIT_NAME_FROM_VALUE )
                console.log ( strMethodName + ': Field # ' + LLCommon.OrdinalFromIndex ( intJ ) + ' of ' + anvp.length + ' Name = ' + theField [ KEY_VALUE_PAIR_IS_KEY ] + ', value = ' + theField [ KEY_VALUE_PAIR_IS_VALUE ] );
                raFields.push({
                    FieldName  : theField [ KEY_VALUE_PAIR_IS_KEY ] ,
                    FieldValue : theField [ KEY_VALUE_PAIR_IS_VALUE ]
                });
                this.#dctInitialValues [ theField [ KEY_VALUE_PAIR_IS_KEY ] ] = theField [ KEY_VALUE_PAIR_IS_VALUE ];
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT, intK = nvp.length; intJ < intK; intJ++ )

            return raFields;
        }   // TRUE (anticipated outcome) block, if ( strAjaxResult.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND && strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
        else
        {
            strAjaxResult = 'ERROR in ' + strMethodName + this.COLON_SPACE + strAjaxResult + ' for lead ID ' + pstrLeadId;
            LLCommon.LogException ( strAjaxResult );

            return strAjaxResult;
        }   // FALSE (unanticipated outcome) block, if ( strAjaxResult.indexOf ( EQUALS_CHAR ) > INDEXOF_NOT_FOUND && strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
    }   // GetSelectedInfo4LeadId method


    GetSTTDatabaseNameFromLocation ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetSTTDatabaseNameFromLocation

            Method Goal:        Use an AJAX call to get the name of the database
                                from which to satisfy all subsequent requests
                                for customer data.

            Input:              None.   Its input is the location property on
                                        the Document object.

            Output:             Provided that the hostname encoded into the
                                document.location property is salestalktech.com,
                                return the database name.
            --------------------------------------------------------------------
        */

        const strMethodName        = LLCommon.GetNameOfCurrentFunction ( );

        if ( this.STTContext === this.STT_TP_CONTEXT )
        {
            const strDatabaseName  = _llAppPath.substring ( SUBSTRING_SECOND_CHARACTER ,
                                                            LLCommon.IndexFromOrdinal ( _llAppPath.length ) );
            LLCommon.Trace ( 'In method ' + strMethodName + ', STT_TP_CONTEXT block, LLCommon.AjaxUrlPrefix = ' + LLCommon.AjaxUrlPrefix );
            return strDatabaseName;
        }   // TRUE (Talking points keep it in a string variable.) block, if ( this.STTContext === this.STT_TP_CONTEXT )
        else
        {
            if ( document.location.hostname.indexOf ( 'salestalktech.com' ) > INDEXOF_NOT_FOUND  && this.STTDomainName !== EMPTY_STRING )
            {
                var strAjaxResult   = EMPTY_STRING;
                var fAjaxError      = false;
                var intTotalRetries = NUMERIC_ZERO;
                var fKeepTrying     = true;

                try
                {
                    do  // while ( fKeepTrying )
                    {
                        $.ajax (
                        {
                                type    : 'GET',
                                async   : false,
                                cache   : false,
                                url     : 'https://salestalktech.com/SalesAcceleration/Open/GetDatabaseFromDomain',
                                data    : {
                                            'domain' : this.STTDomainName
                                          },
                                success : function ( data )
                                          {
                                                if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                                {   // The above Ajax call returned a value. Capture it.
                                                    strAjaxResult = data;
                                                    return data;
                                                }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                          },
                                error   : function ( jqXHR , textStatus , errorThrown )
                                          {
                                                strAjaxResult = textStatus
                                                                + ' ' + jqXHR.responseText
                                                                + ' ' + errorThrown;
                                                fAjaxError    = true;
                                                return strAjaxResult;
                                          }
                        });

                        if ( fAjaxError )
                        {   // The API reported an error. Check the retry count.
                            if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                            {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                                intTotalRetries++;
                                fAjaxError = false;
                            }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                            else
                            {   // The retry limit has been reached. Allow control to leave the do while loop.
                                fKeepTrying = false;
                            }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                        }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                        else
                        {   // The attempt succeeded. Post a log entry and leave the loop.
                            fKeepTrying = false;

                            if ( intTotalRetries === NUMERIC_ZERO )
                            {
                                if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                                {
                                    $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetDatabaseFromDomain for SalesTalk domain name ' + this.STTDomainName + ' succeeded on the first try.' } } );
                                }   // if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                            }   // TRUE (ideal outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                            else
                            {
                                $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetDatabaseFromDomain for SalesTalk domain name ' + this.STTDomainName + ' succeeded after ' + intTotalRetries + ' retries.' } } );
                            }   // FALSE (less than ideal, but acceptable, outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                        }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
                    } while ( fKeepTrying )
                }
                catch ( ex )
                {
                    LLCommon.LogException ( ex );
                    return this.STTFileSystemSubContext ? 'salesacceleration' : EMPTY_STRING;
                }

                //  ------------------------------------------------------------
                //  Though the success and error functions cannot see this, they
                //  can see variables defined within the scope of the closure in
                //  which they are called. Hence, the best solution to getting
                //  data out of them is by haveing the callbacks store it into
                //  the in-scope local variables and move remaining processing
                //  code into the scope of the closure.
                //  ------------------------------------------------------------

                if ( fAjaxError )
                {   // The calling routine takes care of notifying the end user.
                    LLCommon.LogException ( strAjaxResult );
                    return this.STTFileSystemSubContext ? 'salesacceleration' : EMPTY_STRING;
                }   // TRUE (unanticipated outcome) block, if ( fAjaxError )
                else
                {
                    if ( strAjaxResult !== EMPTY_STRING )
                    {
                        LLCommon.Trace ( 'In method ' + strMethodName + ', Landing Page content block, LLCommon.AjaxUrlPrefix = ' + LLCommon.AjaxUrlPrefix );
                        return strAjaxResult;
                    }   // TRUE (anticipated outcome) block, if ( strAjaxResult !== EMPTY_STRING )
                    else
                    {
                        const strTryResult = TryGetDbNameFromURL ( this.STTVideoPlayerSubContext );

                        if ( strTryResult !== EMPTY_STRING )
                        {
                            return strTryResult
                        }   // TRUE (anticipated outcome) block, if ( strTryResult !== EMPTY_STRING )
                        else
                        {
                            return this.STTFileSystemSubContext ? 'salesacceleration' : EMPTY_STRING;
                        }   // FALSE (unanticipated outcome) block, if ( strTryResult !== EMPTY_STRING )
                    }   // FALSE (unanticipated outcome) block, if ( strAjaxResult !== EMPTY_STRING ))
                }   // FALSE (anticipated outcome) block, if ( fAjaxError )
            }   // TRUE (anticipated outcome) block, if ( document.location.hostname.indexOf ( 'salestalktech.com' ) > INDEXOF_NOT_FOUND  && this.STTDomainName !== EMPTY_STRING )
            else
            {
                return this.STTFileSystemSubContext ? 'salesacceleration' : EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( document.location.hostname.indexOf ( 'salestalktech.com' ) > INDEXOF_NOT_FOUND  && this.STTDomainName !== EMPTY_STRING )
        }   // FALSE (Landing pages make us work for it.) block, if ( this.STTContext === this.STT_TP_CONTEXT )


        function TryGetDbNameFromURL ( pfSTTVideoPlayerSubContext )
        {
            const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

            if ( pfSTTVideoPlayerSubContext )
            {
                const astrLocationHrefSegments = document.location.href.split ( '/' );
                return astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ];
            }   // TRUE (anticipated outcome) block, if ( pfSTTVideoPlayerSubContext )
            else
            {
                return EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( pfSTTVideoPlayerSubContext )
        }   // function TryGetDbNameFromURL
    }   // GetSTTDatabaseNameFromLocation method


    GetSTTDomainNameFromLocation ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetSTTDomainNameFromLocation

            Method Goal:        Extract the SalesTalk domain name from a string
                                that looks like so:

                                    /SalesAcceleration/Repository/SalesRelevance/Assets/STT_VideoPlayer.HTML

            Input:              None.   This method uses the document.location
                                        property.

            Output:             String representation of the SalesTalk domain
                                name.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        const astrLocationHrefSegments      = document.location.href.split ( PATH_SEPARATOR_CHAR );
        var   strDomainNameCandidate1       = null;

        //  --------------------------------------------------------------------
        //  This function is implemented as an arrow function so that it can use
        //  constants and methods stored in the object in which it is defined.
        //  Since arrow functions are always ononymous, it must be assigned to a
        //  JavaScript variable (an object) so that it can be used as if it was
        //  a normal function. However, since its definition is bound to a
        //  variable, it must be defined before use.
        //  --------------------------------------------------------------------

        const GetDomainInfo4LeadId          = ( ) =>
        {
            const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

            const strDomainNameCandidate2   = this.GetDomainTenant4LeadId ( );

            if ( ! strDomainNameCandidate2.startsWith ( 'ERROR' ) )
            {
                if ( Number.isInteger ( this.STTUserId2 ) && ( this.STTDomainId !== this.STTDomainId2 ) )
                {   // Preserve the current login if it belongs to the lead's domain.
                    this.STTUserId          = this.STTUserId2;
                    this.STTLoginName       = this.STTLoginName2;
                }   // if ( Number.isInteger ( this.STTUserId2 ) && ( this.STTDomainId !== this.STTDomainId2 ) )

                //  ------------------------------------------------------------
                //  Save the domain and tenant ID returned by instance method
                //  this.GetDomainTenant4LeadId for when you really need them.
                //  Though there may be other extenuating circumstances, in most
                //  cases, the established values should be preserved.
                //  ------------------------------------------------------------

                if ( ( !Number.isInteger ( this.STTDomainId ) ) && Number.isInteger ( this.STTDomainId2 ) )
                {
                    this.STTDomainId        = this.STTDomainId2;
                }   // if ( ( !Number.isInteger ( this.STTDomainId ) ) && Number.isInteger ( this.STTDomainId2 ) )

                if ( ( !Number.isInteger ( this.STTTenantId ) ) && Number.isInteger ( this.STTTenantId2 ) )
                {
                    this.STTTenantId        = this.STTTenantId2;
                }   // if ( ( !Number.isInteger ( this.STTTenantId2 ) ) && Number.isInteger ( this.STTTenantId2 ) )
            }   // if ( ! strDomainNameCandidate2.startsWith ( 'ERROR' ) )

            return strDomainNameCandidate2;
        };  // GetDomainInfo4LeadId, an anonymous function defined as a variable


        if ( astrLocationHrefSegments === null )
        {
            return EMPTY_STRING;
        }   // TRUE (Since astrLocationHrefSegments should NEVER be null, a serious error occurred.) block, if ( astrLocationHrefSegments === null )
        else
        {
            this.STTFileSystemSubContext    = ( document.location.protocol.toLowerCase ( ) === 'file:' );
            this.STTPurlSubContext          = ( document.location.hostname.substring ( SUBSTRING_FIRST_CHAR , 5 ).toLowerCase ( ) === 'purl.' );
            this.STTRepositorySubContext    = ( document.location.href.toLowerCase ( ).indexOf ( '/repository/' ) > INDEXOF_NOT_FOUND );
            this.STTVideoPlayerSubContext   = ( LLCommon.UQFileNameFromHrefOrPathName ( document.location.pathname.toLowerCase ( ) ) === 'stt_videoplayer.html' );

            this.STTContext = (    this.STTFileSystemSubContext
                                || this.STTPurlSubContext
                                || this.STTRepositorySubContext
                                || this.STTVideoPlayerSubContext
                                || typeof _llAppPath === 'undefined'
                              )
                                   ? this.STT_LANDING_PAGE_CONTEXT
                                   : this.STT_TP_CONTEXT;

            if ( this.STTContext === this.STT_TP_CONTEXT )
            {   // Since Talking Point URLs contain only the database name, the domain name must be obtained indirectly.
                const strDatabaseName       = _llAppPath.substring ( SUBSTRING_SECOND_CHARACTER ,
                                                                     LLCommon.IndexFromOrdinal ( _llAppPath.length ) );
                this.STTDatabaseName        = _dbnameSource === SRC_IS_UNKNOWN
                                              ? strDatabaseName
                                              : ( this.STTDatabaseName === undefined && _dbname !== null )
                                                  ? _dbname
                                                  : strDatabaseName;

                LLCommon.Trace ( 'In ' + strMethodName + ', STT_TP_CONTEXT block, LLCommon.AjaxUrlPrefix = ' + LLCommon.AjaxUrlPrefix );

                this.STTLoginName           = _loginSource === SRC_IS_UNKNOWN ? localStorage.getItem ( 'UserName' ) : this.STTLoginName;

                if ( this.STTLoginName !== null && ( _useridSource === SRC_IS_UNKNOWN && _domainidSource === SRC_IS_UNKNOWN && _tenantidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN ) )
                {
                    const strResultSet      = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                'GET',
                                                                {
                                                                   'loginName' : this.STTLoginName
                                                                } );

                    if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                    {
                        const astrResultParts   = strResultSet.split ( PIPE_CHAR_SPLIT_MATCH );

                        //  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //  const string SQL_QUERY_TEMPLATE = @"SELECT UserId, TenantId, Domainid, ( SELECT [Name] FROM leadlife.Domain WITH ( NOLOCK ) WHERE DomainId = [User].DomainId AND Deleted = 0 ) AS DomainName FROM leadlife.[User] WITH ( NOLOCK ) WHERE Email = N'{0}';";
                        //  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                        this.STTUserId          = parseInt ( astrResultParts [ ARRAY_FIRST_ELEMENT ] );

                        this.STTTenantId        = parseInt ( astrResultParts [ ARRAY_SECOND_ELEMENT ] );
                        this.STTDomainId        = parseInt ( astrResultParts [ ARRAY_THIRD_ELEMENT  ] );

                        strDomainNameCandidate1 = astrResultParts [ ARRAY_FOURTH_ELEMENT ];
                    }   // TRUE (anticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                    else
                    {
                        return 'ERROR in ' + strMethodName + ': GetDomainTenantUserIds4LoginName returned no information for UserName value of ' + this.STTLoginName + ' that was read from localStorage.';
                    }   // FALSE (unanticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                }   // TRUE (anticipated outcome) block, if ( this.STTLoginName !== null && ( _useridSource === SRC_IS_UNKNOWN && _domainidSource === SRC_IS_UNKNOWN && _tenantidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN ) )
                else
                {
                    if ( _useridSource === SRC_IS_UNKNOWN && _domainidSource === SRC_IS_UNKNOWN && _tenantidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN )
                    {
                        return 'ERROR in ' + strMethodName + ': UserName (login name) value is missing from localStorage, sessionStorage, and the query string.';
                    }   // if ( _useridSource === SRC_IS_UNKNOWN && _domainidSource === SRC_IS_UNKNOWN && _tenantidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN )

                    strDomainNameCandidate1     = _domainname;
                }   // FALSE (unanticipated outcome) block, if ( this.STTLoginName !== null && ( _useridSource === SRC_IS_UNKNOWN && _domainidSource === SRC_IS_UNKNOWN && _tenantidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN ) )

                if ( ( !Number.isInteger ( this.STTLeadId ) ) || ( this.STTLeadId === NO_LEAD_ID ) )
                {
                    this.STTLeadId              = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

                    if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                    {   // The Lead ID is always present for a Talking Point and is required for a Custom Portal landing page. Otherwise, its presence is optional.
                        if ( this.IsCustomPortal ( ) )
                        {   // Though this test is almost certainly redundant, I'll leave it for now.
                            return 'ERROR in ' + strMethodName + ': GetParameterFromURLFormOrLocalStorage returned the empty string, indicating that the leadId parameter is absent from the query string.';
                        }   // TRUE (This is a fatal exception for a Custom Portal form.) block, if ( this.IsCustomPortal ( ) )
                        else
                        {
                            this.STTLeadId      = NUMERIC_ZERO;
                        }   // FALSE (LeadId is optional.) block, if ( this.IsCustomPortal ( ) )
                    }   // TRUE (The Lead ID is absent from the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                    else
                    {
                        this.STTLeadId          = parseInt ( this.STTLeadId );
                    }   // FALSE (The Lead ID is present in the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                }   // if ( ( !Number.isInteger ( this.STTLeadId ) ) || ( this.STTLeadId === NO_LEAD_ID ) )

                return strDomainNameCandidate1;
            }   // TRUE (The constructor was called from a Talking Point.) block, if ( this.STTContext === this.STT_TP_CONTEXT )
            else
            {   // For a landing page, get it by parsing the URL.
                if ( this.STTVideoPlayerSubContext && _leadidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN )
                {
                    this.STTLeadId              = GetLeadIdFromQueryString ( );
                    this.STTDatabaseName        = _dbnameSource === SRC_IS_UNKNOWN ? astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] : _dbname;                              // 2023/08/08 - DG - In many cases, this is also derived from the like-named property of LLCommon.

                    return GetDomainInfo4LeadId ( );        // GetDomainTenant4LeadId saves the lead, domain and tenant IDs in object properties, then returns the domain name.
                }   // TRUE (The host page is the STT Video Player and the lead ID or domain name remains unknown.) block, if ( this.STTVideoPlayerSubContext && _leadidSource === SRC_IS_UNKNOWN && _domainnameSource === SRC_IS_UNKNOWN )

                //  ------------------------------------------------------------
                //  If control falls out of the special case for video player
                //  pages, process it as before. A page generated from a link in
                //  the Story-So-Far should return, rather than falling into the
                //  next block of code.
                //  ------------------------------------------------------------

                if ( astrLocationHrefSegments.length > ARRAY_FIFTH_ELEMENT )
                {
                    if ( astrLocationHrefSegments [ ARRAY_FIFTH_ELEMENT ].toLowerCase ( ) === 'repository' )
                    {
                        if ( astrLocationHrefSegments [ ARRAY_SIXTH_ELEMENT ] === this.STTDatabaseName && _dbnameSource === SRC_IS_UNKNOWN )
                        {
                            return EMPTY_STRING;
                        }   // TRUE (The array element that is expected to contain the domain name contains instead the database name and the database name is not supplied either directly or indirectly by te query string.) block, if ( astrLocationHrefSegments [ ARRAY_SIXTH_ELEMENT ] === this.STTDatabaseName && _dbnameSource === SRC_IS_UNKNOWN )
                        else
                        {
                            if ( Object.is ( this.STTLeadId , undefined ) )
                            {   // Video player URLs that arise inside the product differ markedly from the URLs displayed in email messages.
                                this.STTLeadId                   = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

                                if ( this.STTLeadId.length === EMPTY_STRING_LENGTH )
                                {   // The Lead ID is always present for a Talking Point and is required for a Custom Portal landing page. Otherwise, its presence is optional.
                                    if ( this.IsCustomPortal ( ) )
                                    {
                                        return 'ERROR in ' + strMethodName + ': Call to GetUrlParameter returned the empty string, indicating that the leadId parameter is absent from the query string.';
                                    }   // if ( this.IsCustomPortal ( ) )
                                }   // TRUE (The Lead ID is absent from the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH )
                                else
                                {
                                    this.STTLeadId               = parseInt ( this.STTLeadId );
                                }   // FALSE (The Lead ID is present in the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH )
                            }   // if ( Object.is ( this.STTLeadId , undefined ) )

                            return _domainnameSource === SRC_IS_UNKNOWN ? astrLocationHrefSegments [ ARRAY_SIXTH_ELEMENT ] : _domainname;
                        }   // FALSE (Insofar as we can determine, the array element that is expected to contain the domain name actually dones.) block, if ( astrLocationHrefSegments [ ARRAY_SIXTH_ELEMENT ] === this.STTDatabaseName && _dbnameSource === SRC_IS_UNKNOWN )
                    }   // TRUE (Since the 5th element is Repository, the domain name is in the 6th.) block, if ( astrLocationHrefSegments [ ARRAY_FIFTH_ELEMENT ].toLowerCase ( ) === 'repository' )
                    else
                    {
                        if ( this.IsApplicationURL ( ) )
                        {
                            this.STTLoginName                    = _loginSource === SRC_IS_UNKNOWN ? localStorage.getItem ( 'UserName' ) : _login;

                            if ( this.STTLoginName !== null && _loginSource !== SRC_IS_UNKNOWN )
                            {
                                const strResultSet               = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                                     'GET',
                                                                                     {
                                                                                        'loginName' : this.STTLoginName
                                                                                     } );

                                if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                                {
                                    const astrResultParts        = strResultSet.split ( PIPE_CHAR_SPLIT_MATCH );

                                    //  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    //  const string SQL_QUERY_TEMPLATE = @"SELECT UserId, TenantId, Domainid, ( SELECT [Name] FROM leadlife.Domain WITH ( NOLOCK ) WHERE DomainId = [User].DomainId AND Deleted = 0 ) AS DomainName FROM leadlife.[User] WITH ( NOLOCK ) WHERE Email = N'{0}';";
                                    //  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                                    this.STTUserId               = parseInt ( astrResultParts [ ARRAY_FIRST_ELEMENT ] );
                                    this.STTLeadId               = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

                                    if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === this.CHARACTER_ZERO )
                                    {   // The Lead ID is always present for a Talking Point and is required for a Custom Portal landing page. Otherwise, its presence is optional.
                                        if ( this.IsCustomPortal ( ) )
                                        {   // Though this test is almost certainly redundant, I'll leave it for now.
                                            return 'ERROR in ' + strMethodName + ': Call to GetUrlParameter returned the empty string, indicating that the leadId parameter is absent from the query string.';
                                        }   // TRUE (This is a fatal exception for a Custom Portal form.) block, if ( this.IsCustomPortal ( ) )
                                        else
                                        {
                                            this.STTLeadId       = NUMERIC_ZERO;
                                        }   // FALSE (LeadId is optional.) block, if ( this.IsCustomPortal ( ) )
                                    }   // TRUE (The Lead ID is absent from the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                                    else
                                    {
                                        this.STTLeadId           = parseInt ( this.STTLeadId );
                                    }   // FALSE (The Lead ID is present in the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )

                                    this.STTTenantId             = parseInt ( astrResultParts [ ARRAY_SECOND_ELEMENT ] );
                                    this.STTDomainId             = parseInt ( astrResultParts [ ARRAY_THIRD_ELEMENT  ] );
                                    this.STTDomainName           =            astrResultParts [ ARRAY_FOURTH_ELEMENT ];

                                    //  ----------------------------------------
                                    //  This IF statement relies upon javascript
                                    //  short circuit expression evaluation. The
                                    //  first two expressions MUST be evaluated
                                    //  first.
                                    //  ----------------------------------------

                                    if ( this.STTLeadId > NUMERIC_ZERO || Number.isNaN ( this.STTTenantId ) || Number.isNaN ( this.STTDomainId ) || this.STTTenantId < MINIMUM_STT_ENTITY_ID || this.STTDomainId < MINIMUM_STT_ENTITY_ID )
                                    {
                                        const strDomainInfo4LeadId = GetDomainInfo4LeadId ( );
                                        return strDomainInfo4LeadId.startsWith ( this.ERR_MESSAGE_STANDARD_PREFIX ) ? this.STTDomainName : strDomainInfo4LeadId;
                                    }   // TRUE (anticipated outcome) if ( this.STTLeadId > NUMERIC_ZERO || Number.isNaN ( this.STTTenantId ) || Number.isNaN ( this.STTDomainId ) || this.STTTenantId < MINIMUM_STT_ENTITY_ID || this.STTDomainId < MINIMUM_STT_ENTITY_ID )
                                    else
                                    {
                                        if ( ( ! Number.isNaN ( this.STTTenantId ) ) || ( ! Number.isNaN ( this.STTDomainId ) ) || this.STTTenantId >= MINIMUM_STT_ENTITY_ID || this.STTDomainId >= MINIMUM_STT_ENTITY_ID )
                                        {   // Bug out if the original condition was FALSE. Otherwise, fall through and attempt to solve for LeadId.
                                            return astrResultParts [ ARRAY_FOURTH_ELEMENT ];
                                        }   // if ( ( ! Number.isNaN ( this.STTTenantId ) ) || ( ! Number.isNaN ( this.STTDomainId ) ) || this.STTTenantId >= MINIMUM_STT_ENTITY_ID || this.STTDomainId >= MINIMUM_STT_ENTITY_ID )
                                    }   // FALSE (unanticipated outcome) block, iif ( this.STTLeadId > NUMERIC_ZERO || Number.isNaN ( this.STTTenantId ) || Number.isNaN ( this.STTDomainId ) || this.STTTenantId < MINIMUM_STT_ENTITY_ID || this.STTDomainId < MINIMUM_STT_ENTITY_ID )
                                }   // if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
                            }   // if ( this.STTLoginName !== null && _loginSource !== SRC_IS_UNKNOWN )

                            if ( Object.is ( this.STTLeadId , undefined ) )
                            {   // Video player URLs that arise inside the product differ markedly from the URLs displayed in email messages.
                                this.STTLeadId = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

                                if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                                {   // The Lead ID is always present for a Talking Point and is required for a Custom Portal landing page. Otherwise, its presence is optional.
                                    if ( this.IsCustomPortal ( ) )
                                    {   // Though this test is almost certainly redundant, I'll leave it for now.
                                        throw new Error ( 'ERROR in ' + strMethodName + ': Call to GetUrlParameter returned the empty string, indicating that the leadId parameter is absent from the query string.' );
                                    }   // TRUE (This is a fatal exception for a Custom Portal form.) block, if ( this.IsCustomPortal ( ) )
                                    else
                                    {
                                        this.STTLeadId = NUMERIC_ZERO;
                                    }   // FALSE (LeadId is optional.) block, if ( this.IsCustomPortal ( ) )
                                }   // TRUE (The Lead ID is absent from the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                                else
                                {
                                    this.STTLeadId = parseInt ( this.STTLeadId );
                                }   // FALSE (The Lead ID is present in the query string.) block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH || this.STTLeadId === CHARACTER_ZERO )
                            }   // if ( Object.is ( this.STTLeadId , undefined ) )

                            return GetDomainInfo4LeadId ( );
                        }   // TRUE (The URL is an Application URL.) block, if ( this.IsApplicationURL ( astrLocationHrefSegments ) )
                        else
                        {
                            if ( astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] === this.STTDatabaseName )
                            {
                                return EMPTY_STRING;
                            }   // TRUE (The array element that is expected to contain the domain name contains instead the database name.) block, if ( astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] === this.STTDatabaseName )
                            else
                            {
                                if ( Object.is ( this.STTLeadId , undefined ) )
                                {
                                    this.STTLeadId = GetParameterFromURLFormOrLocalStorage ( 'leadId' );

                                    if ( this.STTLeadId.length === EMPTY_STRING_LENGTH && ( this.STTContext === this.STT_TP_CONTEXT || this.IsCustomPortal ( ) ) )
                                    {   // Only Custom Portal landing pages require a lead ID.
                                        throw new Error ( 'ERROR in ' + strMethodName + ': Call to GetUrlParameter returned the empty string, indicating that the leadId parameter is absent from the query string.' );
                                    }   // TRUE block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH && ( this.STTContext === this.STT_TP_CONTEXT || this.IsCustomPortal ( ) )
                                    else
                                    {   // See if we got a valid ID. If so, cast it to an integer.
                                        if ( this.STTLeadId.length > EMPTY_STRING_LENGTH )
                                        {
                                            this.STTLeadId = parseInt ( this.STTLeadId );
                                        }   // TRUE (The Lead ID is in the URL.) block, if ( this.STTLeadId.length > EMPTY_STRING_LENGTH )
                                        else {
                                            this.STTLeadId = NUMERIC_ZERO;
                                        }   // FALSE (The lead ID is absent from the URL.) block, if ( this.STTLeadId.length > EMPTY_STRING_LENGTH )
                                    }   // FALSE block, if ( this.STTLeadId.length === EMPTY_STRING_LENGTH && ( this.STTContext === this.STT_TP_CONTEXT || this.IsCustomPortal ( ) )
                                }   // if ( Object.is ( this.STTLeadId , undefined ) )

                                return ( Object.is ( this.STTDomainName , undefined ) || this.STTDomainName === null ) ? ( this.STTFileSystemSubContext ? EMPTY_STRING : astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] ) : this.STTDomainName;
                            }   // FALSE (Insofar as we can determine, the array element that is expected to contain the domain name actually dones.) block, if ( astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] === this.STTDatabaseName )
                        }   // FALSE (The URL is NOT an Application URL.) block, if ( this.IsApplicationURL ( astrLocationHrefSegments ) )
                    }   // FALSE (The 5th element is the domain name.) block, if ( astrLocationHrefSegments [ ARRAY_FIFTH_ELEMENT ].toLowerCase ( ) === 'repository' )
                }   // TRUE (The array contains at least 5 elements, which should always be true.) block, if ( strSttDomainName.length > ARRAY_FIFTH_ELEMENT )
                else
                {
                    return EMPTY_STRING;
                }   // FALSE (The array contains fewer than 5 elements, indicating an error condition.) block, if ( strSttDomainName.length > ARRAY_FIFTH_ELEMENT )
            }   // FALSE (The constructor was called from a Landing Page.) block, if ( this.STTContext === this.STT_TP_CONTEXT )
        }   // FALSE (The anticipated outcome is that astrLocationHrefSegments is NOT NULL.) block, if ( astrLocationHrefSegments === null )
    }   // GetSTTDomainNameFromLocation method


    GetUIDisplaySubTypes ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetUIDisplaySubTypes

            Method Goal:        Get the array of LeadLife Input Masks.

            Input:              None

            Output:             The return value is an array of UIDisplaySubType
                                that is loaded from the database in response to
                                the first request made through this method.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        if ( Object.is ( this.#aoUIDisplaySubTypes , undefined ) )
        {
            const oUIDisplaySubTypes        = LLCommon.DoAjax ( 'GetInputMasks' , 'GET' );
            const intUIDisplaySubTypeCount  = oUIDisplaySubTypes.DisplaySubTypes.length;

            this.#aoUIDisplaySubTypes       = [ ];

            for ( var intJ = ARRAY_FIRST_ELEMENT ;
                      intJ < intUIDisplaySubTypeCount;
                      intJ++ )
            {
                this.#aoUIDisplaySubTypes.push ( oUIDisplaySubTypes.DisplaySubTypes [ intJ ] );
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT ; intJ < intUIDisplaySubTypeCount; intJ++ )
        }   // if ( Object.is ( this.#aoUIDisplaySubTypes , undefined ) )

        return this.#aoUIDisplaySubTypes;
    }   // GetUIDisplaySubTypes method


    GetValues4AllPickList ( pfForceReinitialization )
    {
        /*
            --------------------------------------------------------------------
            Name:       GetValues4AllPickList

            Goal:       Append OPTION elements to each SELECT elment in the
                        current document.

            Arguments:  pfForceReinitialization = When logical TRUE, replace any
                                                  existing OPTION elements.

            Returns:    Since appending OPTION elements to a SELECT element is a
                        side effect, this method could return void. Neverthless,
                        it returns the count of SELECT elements processed.

            Remarks:    This routine feeds the ID of each SELECT element in the
                        active document to sibling method GetPickListValues,
                        along with the value of pfForceReinitialization, if any,
                        that was fed into it. Each iteration of the anonymous
                        function imcrements the count, and the final tally is
                        returned.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        const ERROR_OUTCOME = ARRAY_INVALID_INDEX;
        const _self         = this;
        var   intNElements  = NUMERIC_ZERO;

        //  --------------------------------------------------------------------
        //  Within the scope of the ForEach loop, the this variable holds a
        //  reference to the current SELECT element. Meanwhile, the _self
        //  variable holds a reference to the object that defines the containing
        //  method, thus affording access to its methods and properties through
        //  the closure.
        //  --------------------------------------------------------------------

        try
        {
            $ ( 'SELECT' ).each ( function ( )
            {
                if ( this.id.endsWith ( '_Shadow' ) )
                {
                    LLCommon.Trace ( strMethodName + ': SELECT element ID = ' + this.id + ' is being SKIPPED because it is a shadow element, which was just processed.' );
                }   // TRUE (Skipping shadow elements that are processed along with the elements they shadow.) block, if ( this.id.endsWith ( '_Shadow' ) )
                else
                {
                    var intOptionCount = _self.GetPickListValues ( this.id , pfForceReinitialization );

                    if ( intOptionCount > ERROR_OUTCOME )
                    {
                        LLCommon.Trace ( strMethodName + ': SELECT element ID = ' + this.id + ', Count of options added = ' + intOptionCount );
                        intNElements++;
                    }   // TRUE (anticipated outcome) block, if ( intOptionCount > ERROR_OUTCOME )
                    else
                    {
                        return ARRAY_INVALID_INDEX;
                    }   // FALSE (unanticipated outcome) block, if ( intOptionCount > ERROR_OUTCOME )
                }   // FALSE (Process the element and its shadow if it has one.) block, if ( this.id.endsWith ( '_Shadow' ) )
            });
        }
        catch ( ex )
        {
            LLCommon.LogException ( strMethodName + ': Either jQuery or Sibling method GetPickListValues reported an exception, which was just logged.' , ex );
            return ARRAY_INVALID_INDEX;
        }

        return intNElements;
    }   // GetValues4AllPickList


    GetValueFromInputControl ( pdocFormInputControlElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetValueFromInputControl

            Method Goal:        Get the value from a specified input control.

            Input:              pdocFormInpuControlElement = Element matching an
                                                             ID from which to
                                                             return the value

            Output:             The value of the control is read from the
                                property that holds the value for a control of
                                its type.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        switch ( pdocFormInputControlElement.type )
        {
            case 'submit':
            case 'reset':
            case 'button':
            case 'image':
            case 'hidden':
            case 'password':
                return EMPTY_STRING;

            case 'radio':
            case 'checkbox':
                return pdocFormInputControlElement.checked ? 'true' : 'false';

            case 'date':
                return this.FormatDate4Html5DatePicker ( this.DatePartsFromDatabaseValue ( pdocFormInputControlElement.value ) );

            case 'select-one':
                return pdocFormInputControlElement.value;

            case 'textarea':
            default:
                return pdocFormInputControlElement.value;
        }   // switch ( pdocFormInputControlElement.type )
    }   // GetValueFromInputControl


    GetVeryBasicLeadInfo4LeadId ( pstrLeadId )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetVeryBasicLeadInfo4LeadId

            Method Goal:        Get the first and last names, email address, and
                                mobile phone number for a lead.

            Input:              pstrLeadId = String representation of LeadId for
                                             which to retrieve info

            Output:             Rather than limiting itself to the query string,
                                this method takes the lead ID as input,
                                returning its values as a new VeryBasicLeadInfo
                                object.

            Remarks:            See this.STTLeadId for the ID of the lead that
                                is associated with the Talking Point. However,
                                note that this value is undefined unless object
                                property this.STTContext is this.STT_TP_CONTEXT.
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        var   strAjaxResult   = EMPTY_STRING;
        var   fAjaxError      = false;
        var   intTotalRetries = NUMERIC_ZERO;
        var   fKeepTrying     = true;

        try
        {
            do  // while ( fKeepTrying )
            {
                $.ajax (
                {
                        type    : 'GET',
                        async   : false,
                        cache   : false,
                        url     : LLCommon.AjaxUrlPrefix + 'Open/GetVeryBasicLeadInfo4LeadId',
                        data    : {
                                    'leadId' : pstrLeadId
                                  },
                        success : function ( data )
                                  {
                                        if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                        {   // The above Ajax call returned a value. Capture it.
                                            strAjaxResult = data;
                                            return data;
                                        }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                  },
                        error   : function ( jqXHR , textStatus , errorThrown )
                                  {
                                        strAjaxResult = textStatus
                                                        + ' ' + jqXHR.responseText
                                                        + ' ' + errorThrown;
                                        fAjaxError    = true;
                                        return strAjaxResult;
                                  }
                });

                if ( fAjaxError )
                {   // The API reported an error. Check the retry count.
                    if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                        intTotalRetries++;
                        fAjaxError = false;
                    }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    else
                    {   // The retry limit has been reached. Allow control to leave the do while loop.
                        fKeepTrying = false;
                    }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                else
                {   // The attempt succeeded. Post a log entry and leave the loop.
                    fKeepTrying = false;

                    if ( intTotalRetries === NUMERIC_ZERO )
                    {
                        if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetVeryBasicLeadInfo4LeadId for lead record ' + pstrLeadId + ' succeeded on the first try.' } } );
                        }   // if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                    }   // TRUE (ideal outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                    else
                    {
                        $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' GetVeryBasicLeadInfo4LeadId for lead record ' + pstrLeadId + ' succeeded after ' + intTotalRetries + ' retries.' } } );
                    }   // FALSE (less than ideal, but acceptable, outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
            } while ( fKeepTrying )
        }
        catch ( e )
        {
            LLCommon.LogException ( e );
            alert ( 'At ' + new Date ( ) + ', SalesTalk encountered an internal error while your updates to lead ID ' + this.STTLeadId + '. Please contact customer support for assistance, giving them the time and lead ID shown in this message.' );
            return e;
        }

        //  --------------------------------------------------------------------
        //  Though the success and error functions cannot see this, they can see
        //  variables defined within the scope of the closure in which they are
        //  called. Hence, the best solution to getting data out of them is by
        //  haveing the callbacks store it into the in-scope local variables and
        //  move the remaining processing code into the scope of the closure.
        //  --------------------------------------------------------------------

        if ( fAjaxError )
        {
            LLCommon.LogException ( strAjaxResult );
            alert ( 'At ' + new Date ( ) + ', SalesTalk encountered an internal error while your updates to lead ID ' + this.STTLeadId + '. Please contact customer support for assistance, giving them the time and lead ID shown in this message.' );
            return strAjaxResult;
        }   // if ( fAjaxError )

        if ( strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
        {
            return new VeryBasicLeadInfo ( strAjaxResult );
        }   // TRUE (anticipated outcome) block, if ( strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
        else
        {
            return LLCommon.LogException ( 'ERROR in ' + strMethodName + this.COLON_SPACE + strAjaxResult + ' for lead ID ' + this.STTLeadId );
        }   // FALSE (unanticipated outcome) block, if ( strAjaxResult.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
    }   // GetVeryBasicLeadInfo4LeadId method


    HandleFormPrefill ( pstrContainerName , pobjLeadId )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        HandleFormPrefill

            Method Goal:        Identify all Input fields that have a type of
                                date and set their values from like-named fields
                                in the database record for the lead specified in
                                the leadId query string value.

            Input:              pstrContainerName    = Container identifier that
                                                       conforms to the spec for
                                                       GetInputControlsByType.

                                pobjLeadId           = Optional lead ID value to
                                                       override the value, if
                                                       any, in this.STTLeadId.

            Output:             If pstrContainerName is an array of form control
                                metadata, it is returned for further use by its
                                sibling methods. Otherwise, the return value is
                                undefined.

            References:         1)  How to set a javascript var as undefined
                                    https://stackoverflow.com/questions/5795936/how-to-set-a-javascript-var-as-undefined

                                2)  How to select child elements under id in jQuery
                                    https://stackoverflow.com/questions/11819622/how-to-select-child-elements-under-id-in-jquery
            --------------------------------------------------------------------
        */

        function InputIsButton ( pdocElement )
        {
            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            switch ( pdocElement.nodeName )
            {
                case 'INPUT':
                    return ( pdocElement.type === 'button' );
                case 'BUTTON':
                    return true;
                default:
                    return false;
            }   // switch ( pdocElement.nodeName )
        }   // function InputIsButton


        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        debugger;

        try
        {
            if ( ( ( !Object.is ( this.STTLeadId , undefined ) ) && ( this.STTLeadId > NUMERIC_ZERO ) ) || ( LLCommon.IsString ( pobjLeadId ) ) || ( Number.isInteger ( pobjLeadId ) ) )
            {   // Without a lead ID, this method has no work.
                if ( LLCommon.IsString ( pobjLeadId ) )
                {   // If present, pobjLeadId trumps whatever the constructor or other previous code put into this.STTLeadId.
                    this.STTLeadId = parseInt ( pobjLeadId );
                }   // TRUE block, if ( LLCommon.IsString ( pobjLeadId ) )
                else
                {
                    if ( Number.isInteger ( pobjLeadId ) )
                    {
                        this.STTLeadId = pobjLeadId;
                    }   // if ( Number.isInteger ( pobjLeadId ) )
                }   // FALSE block, if ( LLCommon.IsString ( pobjLeadId ) )

                const intNPickLists = this.GetValues4AllPickList ( true );

                if ( intNPickLists > ARRAY_INVALID_INDEX )
                {
                    LLCommon.Trace ( strMethodName + ': GetValues4AllPickList populated ' + intNPickLists + ' SELECT lists in the current form.'  )
                }   // TRUE (anticipated outcome) block, if ( intNPickLists > his.ARRAY_INVALID_ELEMENT )
                else
                {
                    throw ( strMethodName + ': GetValues4AllPickList logged an exception.' );
                }   // FALSE (unanticipated outcome) block, if ( intNPickLists > his.ARRAY_INVALID_ELEMENT )

                debugger;

                if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
                {
                    if ( ( LLCommon.EnabledCRM !== undefined ) && ( LLCommon.EnabledCRM  !== null ) && ( LLCommon.EnabledCRM.CrmName !== LLCommon.TOKEN_NOCRM ) && ( pstrContainerName !== 'leadlife_mobile_contact_view' ) )
                    {
                        // LLCommon.UserInfo.AgentLoginEmailId should work for ALL CRMs, including Wise Agent.
                        const strLeadId  = LLCommon.DoAjax ( 'CreateOrRefreshLeadFromCRM',
                                                             'GET',
                                                              {
                                                                  'LeadId'              : this.STTLeadId ,
                                                                  'DomainId'            : this.STTDomainId ,
                                                                  'TenantId'            : this.STTTenantId ,
                                                                  'SysCRMLeadOrContact' : LLCommon.EnabledCRM.SysCRMLeadOrContact ,
                                                                  'crmUserEmail'        : LLCommon.UserInfo.AgentLoginEmailId ,
                                                                  'crmUserId'           : LLCommon.UserInfo.AgentLoginEmailId ,
                                                                  'EntityType'          : LLCommon.EntityType !== null
                                                                                          ? LLCommon.EntityType.EntityName
                                                                                          : EMPTY_STRING ,
                                                                  'ExternalCRMId'       : _externalcrmidSource !== SRC_IS_UNKNOWN
                                                                                          ? _externalcrmid
                                                                                          : EMPTY_STRING
                                                              } );

                        if ( parseInt ( strLeadId ) !== this.STTLeadId )
                        {
                            console.log ( 'Lead ID ' + this.STTLeadId + ' has no record in the attached CRM, ' + LLCommon.EnabledCRM.CrmName + ' (SysCRMLeadOrContact = ' + LLCommon.EnabledCRM.SysCRMLeadOrContact + ')' );
                        }   // if ( parseInt ( strLeadId ) !== this.STTLeadId )
                    }   // if ( ( LLCommon.EnabledCRM !== undefined ) && ( LLCommon.EnabledCRM  !== null ) && ( LLCommon.EnabledCRM.CrmName !== LLCommon.TOKEN_NOCRM ) && ( pstrContainerName !== 'leadlife_mobile_contact_view' ) )
                }   // if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )

                var radocFields;
                var adocMaybeMapForEntity = [ ];

                if ( LLCommon.IsString ( pstrContainerName ) )
                {   // Confine the search to the INPUT elements in the container identified by pstrContainerName.
                    radocFields                              = $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( pstrContainerName ) ).find ( ':input' );
                }   // TRUE (Argument pstrContainerName is a value of type string.) block, if ( LLCommon.IsString ( pstrContainerName ) )
                else
                {   // Fall back to searching the whole document.
                    radocFields                              = $ ( ':input' );
                }   // FALSE (Argument pstrContainerName is a value of some other type, possibly NULL or even UNDEFINED.) block, if ( LLCommon.IsString ( pstrContainerName ) )

                const intInputControlsCount                 = radocFields.length;

                if ( intInputControlsCount > ARRAY_IS_EMPTY )
                {
                    var strFieldNames2Get                   = EMPTY_STRING;

                    if ( this.IsWiseAgentMobilePage ( ) )
                    {
                        for ( const k of Object.keys ( this.MobilePage_WA_Contact_FieldMap ) )
                        {
                            if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )
                            {
                                strFieldNames2Get           =   strFieldNames2Get
                                                              + CSV_SEPARATOR_CHAR
                                                              + this.MobilePage_WA_Contact_FieldMap [ k ];
                            }   // TRUE (for all but the first field name) block, if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )
                            else
                            {
                                strFieldNames2Get           = this.MobilePage_WA_Contact_FieldMap [ k ];;
                            }   // TRUE only for the first field name) block, if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )
                        }   // for ( const k of Object.keys ( this.MobilePage_WA_Contact_FieldMap ) )
                    }   // TRUE (The current page is an instance of the mobile page operating in the context of a Wise Agent Contact entity.) block, if ( this.IsWiseAgentMobilePage ( ) )
                    else
                    {
                        for ( var intJ = ARRAY_FIRST_ELEMENT;
                                  intJ < intInputControlsCount;
                                  intJ++ )
                        {
                            var strIdOrName                 = this.GetIdOrName ( radocFields [ intJ ] );
                            var strInputClassName           = radocFields [ intJ ].className;

                            if ( strIdOrName.length > EMPTY_STRING_LENGTH && strInputClassName.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND && ( !strIdOrName.toLowerCase ( ).endsWith ( '_shadow' ) ) )
                            {
                                if ( ( ! InputIsButton ( radocFields [ intJ ] ) ) || ( !this.IsCustomPortal ( ) ) || ( this.CustomPortalFields2Skip.find ( element => element.toLowerCase ( ) === strIdOrName.toLowerCase ( ) ) === undefined ) )
                                {
                                    if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )
                                    {
                                        strFieldNames2Get   =   strFieldNames2Get
                                                              + CSV_SEPARATOR_CHAR
                                                              + strIdOrName;
                                    }   // TRUE (for all but the first field name) block, if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )
                                    else
                                    {
                                        strFieldNames2Get   = strIdOrName;
                                    }   // TRUE only for the first field name) block, if ( strFieldNames2Get.length > EMPTY_STRING_LENGTH )

                                    if ( radocFields [ intJ ].id === 'Title' )
                                    {
                                        adocMaybeMapForEntity.push ( radocFields [ intJ ] );
                                    }   // if ( radocFields [ intJ ].id === 'Title' )
                                }   // if ( ( ! InputIsButton ( radocFields [ intJ ] ) ) || ( !this.IsCustomPortal ( ) ) || ( this.CustomPortalFields2Skip.find ( element => element.toLowerCase ( ) === strIdOrName.toLowerCase ( ) ) === undefined ) )
                            }  // if ( strIdOrName.length > EMPTY_STRING_LENGTH && strInputClassName.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND && ( !strIdOrName.toLowerCase ( ).endsWith ( '_shadow' ) ) )
                        }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intInputControlsCount; intJ++ )
                    }   // FALSE (The current page follows the default processing path.) block, if ( this.IsWiseAgentMobilePage ( ) )

                    const leadInfo                          = this.GetSelectedInfo4LeadId ( Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ),
                                                                                            strFieldNames2Get ,
                                                                                            true );

                    if ( Array.isArray ( leadInfo ) )
                    {
                        //  ----------------------------------------------------
                        //  When it succeeds, GetSelectedInfo4LeadId returns an
                        //  array. When it fails, it returns a string, but the
                        //  exception has already been logged and reported.
                        //  ----------------------------------------------------

                        const strFieldNames2GetLC           = strFieldNames2Get.toLowerCase ( );
                        var   strValueTemp;
                        var   strShadowElementId;
                        var   docShadowElement;
                        var   intSelectedIndex;

                        for ( intJ = ARRAY_FIRST_ELEMENT;
                              intJ < intInputControlsCount;
                              intJ++ )
                        {
                            //  ------------------------------------------------
                            //  Though the fields in the CustomPortalFields2Skip
                            //  are excluded from the list of fields to get,
                            //  re-checking  them here is necessary to prevent
                            //  values in the form being cleared.
                            //
                            //  Version 1.314: Fields that have the same ID
                            //                 suffixed by "_Shadow" store the
                            //                 input value so that it can be
                            //                 compared against the value when
                            //                 the field is submitted for
                            //                 posting. To mitigate the risk
                            //                 that could arise if name
                            //                 attributes were allowed, only the
                            //                 id attribute is supported.
                            //
                            //                 For similar reasons, the type of
                            //                 each shadow element MUST match
                            //                 that of the element that it
                            //                 shadows. This rule is strictly
                            //                 enforced
                            //  ------------------------------------------------

                            if ( radocFields [ intJ ].className.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND && strFieldNames2GetLC.indexOf ( this.GetIdOrName ( radocFields [ intJ ] ).toLowerCase ( ) ) > INDEXOF_NOT_FOUND )
                            {
                                strShadowElementId          = radocFields [ intJ ].id + '_Shadow';
                                docShadowElement            = document.getElementById ( strShadowElementId );

                                switch ( radocFields [ intJ ].type )
                                {
                                    case 'submit':
                                    case 'reset':
                                    case 'button':
                                    case 'image':
                                    case 'hidden':
                                    case 'password':
                                        break;

                                    case 'radio':
                                    case 'checkbox':        // The value of a RADIO or a CHECKBOX is meaningless; its Checked property is the value that matters.
                                        radocFields [ intJ ].checked = ( this.GetFieldValue ( radocFields [ intJ ] ,
                                                                                              leadInfo ).toLowerCase ( ) === 'true' );

                                        if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )
                                        {
                                            break;
                                        }   // if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )

                                        if ( docShadowElement.type === 'radio' )
                                        {
                                            docShadowElement.checked = radocFields [ intJ ].checked;
                                        }   // if ( docShadowElement.type === 'radio' )

                                        if ( docShadowElement.type === 'checkbox' )
                                        {
                                            docShadowElement.checked = radocFields [ intJ ].checked;
                                        }   // if ( docShadowElement.type === 'checkbox' )

                                        break;  // case 'radio' AND case 'checkbox'

                                    case 'date':
                                        radocFields [ intJ ].value   = this.FormatDate4Html5DatePicker ( this.DatePartsFromDatabaseValue ( this.GetFieldValue ( radocFields [ intJ ] ,
                                                                                                                                                               leadInfo ) ) );

                                        this.ApplyInputMaskToValueReadFromDB ( radocFields [ intJ ] );

                                        if ( Object.is ( docShadowElement , undefined ) || docShadowElement === null )
                                        {
                                            break;
                                        }   // if ( Object.is ( docShadowElement , undefined ) || docShadowElement === null )

                                        if ( docShadowElement.type === 'date' )
                                        {
                                            docShadowElement.value = radocFields [ intJ ].value;
                                        }   // if ( docShadowElement.type === 'date' )

                                        break;  // case 'date'

                                    case 'select-one':                  // The value of a SELECT control is meaningless; its SelectedIndex property is the value that matters.
                                        intSelectedIndex = SetSelectedValue ( radocFields [ intJ ].id ,
                                                                              radocFields [ intJ ].name ,
                                                                              this.GetFieldValue ( radocFields [ intJ ] ,
                                                                                                   leadInfo ) );

                                        if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )
                                        {
                                            break;
                                        }   // if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )

                                        if ( docShadowElement.type === 'select-one' )
                                        {
                                            docShadowElement.selectedIndex = intSelectedIndex;
                                        }   // if ( docShadowElement.type === 'checkbox' )

                                        break;  // case 'select-one'

                                    case 'textarea':
                                    default:
                                        radocFields [ intJ ].value = this.GetFieldValue ( radocFields [ intJ ] ,
                                                                                          leadInfo );

                                        this.ApplyInputMaskToValueReadFromDB ( radocFields [ intJ ] );

                                        if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )
                                        {
                                            break;
                                        }   // if ( Object.is ( docShadowElement , undefined ) ||  docShadowElement === null )

                                        if ( docShadowElement.type === 'textarea' )
                                        {
                                            docShadowElement.value = radocFields [ intJ ].value
                                        }   // if ( docShadowElement.type === 'textarea' )

                                        if ( docShadowElement.type === radocFields [ intJ ].type )
                                        {
                                            docShadowElement.value = radocFields [ intJ ].value
                                        }   // if ( docShadowElement.type === radocFields [ intJ ].type )

                                        break;  // case 'textarea' AND case default
                                }   // switch ( radocFields [ intJ ].type )
                            }   // if ( radocFields [ intJ ].className.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND && strFieldNames2GetLC.indexOf ( this.GetIdOrName ( radocFields [ intJ ] ).toLowerCase ( ) ) > INDEXOF_NOT_FOUND )
                        }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intInputControlsCount; intJ++ )

                        const adocFields2Redisplay = $( '.STTformField2Redisplay' );

                        if ( adocFields2Redisplay.length > ARRAY_IS_EMPTY )
                        {
                            this.HandleSpecialPrefill ( adocFields2Redisplay );
                        }   // if ( adocFields2Redisplay.length > ARRAY_IS_EMPTY )

                        const docDomainId   = document.getElementById ( 'DomainId' );

                        if ( docDomainId !== null && docDomainId.nodeName === 'INPUT' && docDomainId.type === 'text' )
                        {
                            docDomainId.value = this.STTDomainId;
                        }   // if ( docDomainId !== null && docDomainId.nodeName === 'INPUT' && docDomainId.type === 'text' )

                        const docDomainName = document.getElementById ( 'DomainName' );

                        if ( docDomainName !== null && docDomainName.nodeName === 'INPUT' && docDomainName.type === 'text' )
                        {
                            docDomainName.value = this.STTDomainName;
                        }   // if ( docDomainName !== null && docDomainName.nodeName === 'INPUT' && docDomainName.type === 'text' )
                    }   // if ( Array.isArray ( leadInfo ) )
                }   // if ( intInputControlsCount > ARRAY_IS_EMPTY )

                //  ------------------------------------------------------------
                //  Get values for standard fields that have been mapped to new
                //  custom fields. Presently, only the Title field is supported,
                //  though others could easily be accommodated.
                //  ------------------------------------------------------------

                if ( adocMaybeMapForEntity.length > ARRAY_IS_EMPTY )
                {
                    if ( LLCommon.EnabledCRM !== null && LLCommon.EntityType !== null && LLCommon.EnabledCRM !== LLCommon.TOKEN_NOCRM )
                    {
                        for ( var intMaybeMappedFoEntity = ARRAY_FIRST_ELEMENT;
                                  intMaybeMappedFoEntity < adocMaybeMapForEntity.length;
                                  intMaybeMappedFoEntity++ )
                        {
                            switch ( adocMaybeMapForEntity [ intMaybeMappedFoEntity ].id )
                            {
                                case 'Title':
                                    const strTitleForEntity = LLCommon.DoAjax ( 'GetTitleFieldForCRMEntitiy',
                                                                                'GET',
                                                                                {
                                                                                    'DomainId'            : _domainid,
                                                                                    'LeadId'              : this.STTLeadId,
                                                                                    'ExternalSystemId'    : LLCommon.EnabledCRM.ExternalSystemTypeId,
                                                                                    'SysCRMLeadOrContact' : document.getElementById ( 'SysCRMLeadOrContact' ).value
                                                                                } );

                                     if ( strTitleForEntity.length > EMPTY_STRING_LENGTH )
                                     {
                                        adocMaybeMapForEntity [ intMaybeMappedFoEntity ].value = strTitleForEntity;
                                     }  // if ( strTitleForEntity.length > EMPTY_STRING_LENGTH )
                                     break;

                                default:
                                    throw new Error ( 'Element ID ' + adocMaybeMapForEntity [ intMaybeMappedFoEntity ].id + ' is unsupported for CRM entity override mapping.' );
                            }   // switch ( adocMaybeMapForEntity [ intMaybeMappedFoEntity ].id )
                        }   // for ( var intMaybeMappedFoEntity = ARRAY_FIRST_ELEMENT; intMaybeMappedFoEntity < adocMaybeMapForEntity.length; intMaybeMappedFoEntity++ )
                    }   // if ( LLCommon.EnabledCRM !== null && LLCommon.EntityType !== null && LLCommon.EnabledCRM !== LLCommon.TOKEN_NOCRM )
                }   // if ( adocMaybeMapForEntity.length > ARRAY_IS_EMPTY )

                //  ------------------------------------------------------------
                //  Fields that have a Full Name field must populate it by
                //  concatenating the first and last names into a single string
                //  and pasing the concatenated string into the full name field.
                //
                //  This script assumees that the two input fields are FirstName
                //  and LastName and the full name field is called FulltName.
                //  All three are presumed to be INPUT elements of type text. If
                //  all three exist, the concatenation and field update happen.
                //  ------------------------------------------------------------

                const docFirstName = document.getElementById ( 'FirstName' );
                const docLastName  = document.getElementById ( 'LastName'  );
                const docFulltName = document.getElementById ( 'FulltName' );

                if ( ( docFirstName !== null ) && ( docLastName !== null ) && ( docFulltName !== null ) )
                {
                    docFulltName.value =   docFirstName.value
                                         + SPACE_CHARACTER
                                         + docLastName.value;
                }   // if ( ( docFirstName !== null ) && ( docLastName !== null ) && ( docFulltName !== null ) )
            }   // if ( ( ( !Object.is ( this.STTLeadId , undefined ) ) && ( this.STTLeadId > NUMERIC_ZERO ) ) || ( LLCommon.IsString ( pobjLeadId ) ) || ( Number.isInteger ( pobjLeadId ) ) )

            return radocFields;
        }
        catch ( ex )
        {
            const strMsg = strMethodName + ': Exception Message = ' + ex.message + ' Stack Trace = ' + ex.trace;
            LLCommon.Trace ( strMsg );
            LLCommon.LogException ( ex );
            return strMsg;
        }

        function SetSelectedValue ( pstrSelectElementId , pstrSelectElementName , pstrValueToSet )
        {
            /*
                ----------------------------------------------------------------
                Name:       SetSelectedValue

                Goal:       Compute the SelectedIndex value of a dropdown.

                Arguments:  pstrSelectElementId   = Reference to ID attribute of
                                                    the SELECT element to
                                                    evaluate

                            pstrSelectElementName = Reference to Name attribute
                                                    of the SELECT element to
                                                    evaluate

                            pstrValueToSet        = String representation of the
                                                    value property (the internal
                                                    SELECTED attribute) of the
                                                    option to mark as selected

                Returns:    The SelectedIndex is returned so that it can be
                            tested. If no match is found, the method returns -1.

                Remarks:    This function is adapted from the example shown in
                            the accepted answer to a StackOverflow question,
                            "How to Select a Value in a Select Dropdown with
                            JavaScript?"

                            Since its definition is nested in HandleFormPrefill,
                            this function cannot take advantage of the constants
                            defined in the containing class, although it can
                            freely use those defined by LLCommon.

                Reference:  How to Select a Value in a Select Dropdown with JavaScript?
                            https://stackoverflow.com/questions/8140862/how-to-select-a-value-in-a-select-dropdown-with-javascript
                ----------------------------------------------------------------
            */

            const strMethodName       = LLCommon.GetNameOfCurrentFunction ( );

            LLCommon.Trace ( strMethodName + ' Arguments : pstrSelectElementId = ' + LLCommon.safeString ( pstrSelectElementId , QUOTE_DOUBLE )
                                           + ', pstrSelectElementName = ' + LLCommon.safeString ( pstrSelectElementName , QUOTE_DOUBLE )
                                           + ', pstrValueToSet = ' + LLCommon.safeString ( pstrValueToSet , QUOTE_DOUBLE ) );

            if ( !LLCommon.IsString ( pstrSelectElementId ) )
            {
                LLCommon.LogException ( strMethodName + ': Input argument pstrSelectElementId must be a string.' );
                return ARRAY_INVALID_INDEX;
            }   // if ( !LLCommon.IsString ( pstrSelectElementId ) )

            if ( !LLCommon.IsString ( pstrSelectElementName ) )
            {
                LLCommon.LogException ( strMethodName + ': Input argument pstrSelectElementName must be a string.' );
                return ARRAY_INVALID_INDEX;
            }   // if ( !LLCommon.IsString ( pstrSelectElementName ) )

            if ( pstrSelectElementId.length === EMPTY_STRING_LENGTH && pstrSelectElementName.length === EMPTY_STRING_LENGTH )
            {
                LLCommon.LogException ( strMethodName + ': One of input arguments pstrSelectElementId and pstrSelectElementName must have a positive length. Neither of them does.' );
                return ARRAY_INVALID_INDEX;
            }   // if ( pstrSelectElementId.length === EMPTY_STRING_LENGTH && pstrSelectElementName.length === EMPTY_STRING_LENGTH )

            if ( LLCommon.IsString ( pstrValueToSet ) && pstrValueToSet.length > EMPTY_STRING_LENGTH )
            {
                const strValue2SetLC  = pstrValueToSet.toLowerCase ( );
                const docSelectObj    = document.getElementById ( pstrSelectElementId.length > EMPTY_STRING_LENGTH ? pstrSelectElementId : pstrSelectElementName );

                if ( docSelectObj !== null || docSelectObj.type !== 'select-one' )
                {
                    const intNOptions  = docSelectObj.options.length;

                    for ( var intOptionIndex = ARRAY_FIRST_ELEMENT;
                              intOptionIndex < intNOptions;
                              intOptionIndex++ )
                    {
                        if ( docSelectObj.options [ intOptionIndex ].value.toLowerCase ( ) === strValue2SetLC )
                        {
                            docSelectObj.options [ intOptionIndex ].selected = true;
                            return intOptionIndex;
                        }   // if ( docSelectObj.options [ intOptionIndex ].value.toLowerCase ( ) === value2SetLC )
                    }   // for ( var intOptionIndex = ARRAY_FIRST_ELEMENT; intOptionIndex < intNOptions;  intOptionIndex++ )
                }   // if ( docSelectObj !== null || docSelectObj.type !== 'select-one' )

                LLCommon.LogException ( strMethodName + ': The specified value, ' + LLCommon.safeString ( pstrValueToSet , QUOTE_DOUBLE ) + ', cannot be found for SELECT element.' );
                return ARRAY_INVALID_INDEX;  // Value not found
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrValueToSet ) && pstrValueToSet.length > EMPTY_STRING_LENGTH )
            else
            {
                LLCommon.LogException ( strMethodName + ': The specified value, ' + LLCommon.safeString ( pstrValueToSet , QUOTE_DOUBLE ) + ', must be a string that has a length greater than zero.' );
                return ARRAY_INVALID_INDEX;  // Value is an invalid combo box selection.
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrValueToSet ) && pstrValueToSet.length > EMPTY_STRING_LENGTH )
        }   // function SetSelectedValue
    }   // HandleFormPrefill method


    HandleSpecialPrefill ( padocField2Populate )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        HandleSpecialPrefill

            Method Goal:        Initialize the inner text of the spans in the
                                padocField2Populate array from fields that have
                                the same ID as does each element in the array.

            Input:              padocField2Populate = Array of elements to be
                                                      prefilled

            Output:             If it succeeds, the return value is the count of
                                elements prefiiled. Otherwise, the return value
                                is zero.

            Exception:          When the AJAX call to get the database fails, an
                                exception is raised, logged, and displayed.

            Remarks:            The expected return value is a count equal to
                                the length of the padocField2Populate array.
            --------------------------------------------------------------------
        */

        const strMethodName          = LLCommon.GetNameOfCurrentFunction ( );

        const intTotalElements       = padocField2Populate.length;

        if ( intTotalElements > ARRAY_IS_EMPTY )
        {
            var   strFieldNameList;

            for ( var intElementIndex = ARRAY_FIRST_ELEMENT;
                      intElementIndex < intTotalElements;
                      intElementIndex++ )
            {
                if ( intElementIndex === ARRAY_FIRST_ELEMENT )
                {
                    strFieldNameList = padocField2Populate [ intElementIndex ].id;
                }   // TRUE (The loop is on its first iteration, and the list is uninitialized.) block, if ( intElementIndex === ARRAY_FIRST_ELEMENT )
                else
                {
                    strFieldNameList = strFieldNameList + CSV_SEPARATOR_CHAR + padocField2Populate [ intElementIndex ].id;
                }   // FALSE (The loop is on a subsequent iteration, and other items are on the list.) block, if ( intElementIndex === ARRAY_FIRST_ELEMENT )
            }   // for ( var intElementIndex = ARRAY_FIRST_ELEMENT; intElementIndex < intTotalElements;intElementIndex++ )

            const leadInfo           = this.GetSelectedInfo4LeadId ( Object.is ( this.STTLeadId , undefined ) ? parseInt ( GetParameterFromURLFormOrLocalStorage ( 'leadId' ) ) : this.STTLeadId,
                                                                     strFieldNameList ,
                                                                     true );

            if ( Array.isArray ( leadInfo ) )
            {
                //  ------------------------------------------------------------
                //  When it succeeds, GetSelectedInfo4LeadId returns an array.
                //  When it fails, it returns a string, but the exception was
                //  already logged and reported.
                //  ------------------------------------------------------------

                for ( intElementIndex = ARRAY_FIRST_ELEMENT;
                      intElementIndex < intTotalElements;
                      intElementIndex++ )
                {
                    switch ( padocField2Populate [ intElementIndex ].type )
                    {
                        case 'submit':
                        case 'reset':
                        case 'button':
                        case 'image':
                        case 'hidden':
                        case 'password':
                            break;

                        case 'radio':
                        case 'checkbox':
                            padocField2Populate [ intElementIndex ].checked   = ( this.GetFieldValue ( padocField2Populate [ intElementIndex ] ,
                                                                                                       leadInfo ).toLowerCase ( ) === 'true' );
                            break;

                        case 'date':
                            padocField2Populate [ intElementIndex ].innerText = this.FormatDate4Html5DatePicker ( this.DatePartsFromDatabaseValue ( this.GetFieldValue ( padocField2Populate [ intElementIndex ] ,
                                                                                                                                                                         leadInfo ) ) );
                            break;

                        case 'select-one':
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' SELECT field with ID = ' + padocField2Populate [ intElementIndex ].id + ', name = ' + padocField2Populate [ intElementIndex ].name + ', and value = ' + this.GetFieldValue ( padocField2Populate [ intElementIndex ] , leadInfo ) + ' on lead ID ' + this.STTLeadId + ' is a pick list. HandleSpecialPrefill routine does not support this type of field.' } } );
                            break;

                        case 'textarea':
                        default:
                            padocField2Populate [ intElementIndex ].innerText = this.GetFieldValue ( padocField2Populate [ intElementIndex ] ,
                                                                                                     leadInfo );
                            this.ApplyInputMaskToValueReadFromDB ( padocField2Populate [ intElementIndex ] );
                            break;
                    }   // switch ( padocField2Populate [ intElementIndex ].type )
                }   // for ( intElementIndex = ARRAY_FIRST_ELEMENT; intElementIndex < intTotalElements;intElementIndex++ )
            }   // if ( Array.isArray ( leadInfo ) )
        }   // if ( intTotalElements > ARRAY_IS_EMPTY )
    }   // HandleSpecialPrefill method


    HasDropDownOverlay ( pdocElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        HasDropDownOverlay

            Method Goal:        Evaluate an INPUT element to determine whetheer
                                its adjacent element is a DropDown overlay
                                button.

            Input:              pdocElement = Element to evaluate for an
                                              associated DropDown overlay button

            Output:             If the element identified by pdocElement has a
                                DropDown overlay button associated with it, the
                                return value is True. Otherwise, the return
                                value is False.

            Algorithm:          1)  If the nodeName attribute of pdocElement is
                                    INPUT and its type is text, evaluate its
                                    nextElementSibling attributee.

                                2)  Compare the ID of the nextElementSibling
                                    against that of the pdocElement itself.
                                    If the ID is the same with a suffix of
                                    "_DropDown" and its nodeName is BUTTON, then
                                    the return value is True. Otherwise, the
                                    return value is False.

            Remarks:            The pdocElement argument may be a reference to a
                                document element or a string that contains its
                                ID.
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const docElement2Test   = LLCommon.IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

        try
        {
            if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
            {
                if ( docElement2Test.nextElementSibling !== null && docElement2Test.nextElementSibling.nodeName !== undefined && docElement2Test.nextElementSibling.nodeName === 'BUTTON' )
                {   // This is the only path of control flow that MAY return TRUE.
                    return ( docElement2Test.nextElementSibling.id.toLowerCase ( ) === docElement2Test.id.toLowerCase ( ) + '_dropdown' );
                }   // TRUE (The input element has a sibling that is a BUTTON.) block, if ( docElement2Test.nextElementSibling !== null && docElement2Test.nextElementSibling.nodeName !== undefined && docElement2Test.nextElementSibling.nodeName === 'BUTTON' )
                else
                {
                    return false;
                }   // FALSE (The input element either has no sibling at all or its next sibling isn't a BUTTON.) block, if ( docElement2Test.nextElementSibling !== null && docElement2Test.nextElementSibling.nodeName !== undefined && docElement2Test.nextElementSibling.nodeName === 'BUTTON' )
            }   // TRUE (The anticipated outcome is that the specified element has a nodeName of INPUT and a type of text.) block, if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
            else
            {
                return false;
            }   // FALSE (The unanticipated outcome is that one of the foregoing requirements is unmet, and the element is out of scope.) block, if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
        }
        catch ( ex )
        {
            LLCommon.Trace ( LLCommon.LogException ( ex ) );
            return false;
        }
    }   // HasDropDownOverlay method


    IdentifyContainingForm ( pdocElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        IdentifyContainingForm

            Method Goal:        Identify the form that contains the element
                                identified by argument poEvent.

            Input:              pdocElement = Element for which to identify the
                                              the containing form

            Output:             If it succeeds, the return value is an object
                                that represents the form that contains the
                                element represented by pdocElement. Otherwise,
                                the return value is undefined.

            Exception:          When pdocElement lies outside of any form
                                element, an exception having as its message a
                                string that starts with "HTML document element"
                                arises.

            Remarks:            Called correctly from a properly designed form,
                                this method should always return a form element.
            --------------------------------------------------------------------
        */

        const strMethodName      = LLCommon.GetNameOfCurrentFunction ( );

        var   fSearching4Form    = true;
        var   oParentElement     = pdocElement.target === undefined ? pdocElement : pdocElement.target;

        while ( fSearching4Form )
        {
            oParentElement       = oParentElement.parentNode;

            if ( oParentElement.nodeName === 'FORM' )
            {
                fSearching4Form  = false;
            }   // if ( oParentElement.nodeName === 'FORM' )

            if ( oParentElement.nodeName === 'BODY' )
            {
                throw ( 'HTML document element ' + pdocElement.target.id + ' must be contained within a Form element.' );
            }   // if ( oParentElement.nodeName === 'BODY' )
        }   // while ( fSearching4Form )

        return oParentElement;
    }   // IdentifyContainingForm method


    IsApplicationURL ( )
    {
        /*
            --------------------------------------------------------------------
            Name:       IsApplicationURL

            Goal:       Return Boolean TRUE when the page is a part of the
                        application.

            Arguments:  None

            Returns:    This method returns Boolean True when the page hosting
                        the script lives on an Application URL, such as a page
                        in the Content or Mobile folder.

                        When the return value is True, this method also sets
                        this.STTDatabaseName.
            --------------------------------------------------------------------
        */

        const strMethodName           = LLCommon.GetNameOfCurrentFunction ( );

        const strTruthOrConsequences  = LLCommon.DoAjax ( 'Is4thUrlSegmentDatabaseName',
                                                          'GET',
                                                          {
                                                              'url' : location.href
                                                          } );

        switch ( strTruthOrConsequences )
        {
            case 'true':
            {   // Localize lexican scope.
                const astrLocationHrefSegments = document.location.href.split ( PATH_SEPARATOR_CHAR );
                this.STTDatabaseName           = Object.is ( this.STTDatabaseName , undefined ) ? astrLocationHrefSegments [ ARRAY_FOURTH_ELEMENT ] : this.STTDatabaseName;
                return true;
            }   // Allow constant astrLocationHrefSegments to go out of scope.
            case 'false':
            default:
                this.STTDatabaseName           = Object.is ( this.STTDatabaseName , undefined ) ? EMPTY_STRING : this.STTDatabaseName;
                return false;
        }   // switch ( strTruthOrConsequences )
    }   // IsApplicationURL method


    IsCustomPortal ( )
    {
        /*
            --------------------------------------------------------------------
            Name:       IsCustomPortal

            Goal:       Return Boolean TRUE when the page is a Custom Portal.

            Arguments:  None.

            Returns:    This method returns Boolean True when the page hosting
                        the script is a Custom Portal, which is indicated by the
                        presence of an element named CustomPortalMoniker in it.
            --------------------------------------------------------------------
        */

        return ( document.getElementById ( 'CustomPortalMoniker' ) !== null );
    }   // IsCustomPortal method


    IsEmailAddressValid ( pstrEmailAddress2Validate )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        IsEmailAddressValid

            Method Goal:        Evaluate the input string, returning TRUE when
                                the string represents a valid email address.

            Input:              pstrEmailAddress2Validate   = Email address to
                                                              evaluate

            Output:             Returns true when pstrEmailAddress2Validate is a
                                valid email address, otherwise returns false.
            --------------------------------------------------------------------
        */

        const rx = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
        return rx.test ( pstrEmailAddress2Validate );
    }   // IsEmailAddressValid


    IsKeyInDictionary ( pstrEventIdString ,
                        pastrPageEventMap )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        IsEventTypeKnown

            Method Goal:        Evaluate the input string, returning TRUE when
                                it is in the list of recognized event types.

            Input:              pstrEventIdString   = String passed into event
                                                      delegate function

                                pastrPageEventMap   = Array of objects indexed
                                                      by names that contain no
                                                      spaces

            Output:             Returns true when pstrEventIdString is the key
                                of an element pastrPageEventMap, an associative
                                array, else returns false
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( Object.is ( pastrPageEventMap , undefined ) )
        {
            return false;
        }   // TRUE (degenerate case 1 of 2) block, if ( Object.is ( pastrPageEventMap , undefined ) )
        else
        {
            if ( Object.is ( pstrEventIdString , undefined ) )
            {
                return false;
            }   // TRUE (degenerate case 2 , of 2) block, if ( Object.is ( pstrEventIdString , undefined ) )
            else
            {
                if ( Object.is ( pastrPageEventMap [ pstrEventIdString ] , undefined ) )
                {
                    return false;
                }   // TRUE (Element is not in the specified array.) block, if ( Object.is ( pastrPageEventMap [ pstrEventIdString ] , undefined ) )
                else
                {
                    return true;
                }   // FALSE (Element is in the specified array.) block, if ( Object.is ( pastrPageEventMap [ pstrEventIdString ] , undefined ) )
            }   // FALSE (anticipated outcome - neither arg is undefined) if ( Object.is ( pstrEventIdString , undefined ) )
        }   // FALSE (The object passed in as the array is defined.) block, if ( Object.is ( pastrPageEventMap , undefined ) )
    }   // IsKeyInDictionary method


    IsPickListValueValid ( pdocThisElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:    IsPickListValueValid

            Method Goal:    Evaluate a SELECT element against the list of
                            valid values stored in the database.

            Input:          pdocThisElement         = Reference to element to
                                                      evaluate against its pick
                                                      list

            Output:         If the value of the element is in the pick list, the
                            return value is True. Otherwise, the return value is
                            False.

            Remarks:        Beginning with version 1.378, the local pick list
                            that is stored in the global _PickListValues object
                            is the validation source, eliminating a round trip
                            to the server.
            --------------------------------------------------------------------
        */

        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        const strW2APickListMatch   = LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 ? 'DisplayText' : 'Name';
        const aobjPickListValues    = _PickListValues [ pdocThisElement.id ].PickListValues

        if ( Array.isArray ( aobjPickListValues ) )
        {
            if ( aobjPickListValues.length > ARRAY_IS_EMPTY )
            {
                const strFieldValue = pdocThisElement.value.toLowerCase ( );

                //  ------------------------------------------------------------
                //  To acommodate verbal updates, matching is case insensitive.
                //  However, once a match is found, the value in the control is
                //  corrected.
                //  ------------------------------------------------------------

                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < aobjPickListValues.length;
                          intJ++ )
                {
                    switch ( strW2APickListMatch )
                    {
                        case 'DisplayText':
                            if ( aobjPickListValues [ intJ ].DisplayText.toLowerCase ( ) === strFieldValue )
                            {
                                pdocThisElement.value = aobjPickListValues [ intJ ].DisplayText;
                                return true;
                            }   // if ( aobjPickListValues [ intJ ].DisplayText.toLowerCase ( ) === strFieldValue )
                            break;
                        case EMPTY_STRING:
                        case 'Name':
                        default:
                            if ( aobjPickListValues [ intJ ].Name.toLowerCase ( ) === strFieldValue )
                            {
                                pdocThisElement.value = aobjPickListValues [ intJ ].Name;
                                return true;
                            }   // if ( aobjPickListValues [ intJ ].Name.toLowerCase ( ) === strFieldValue )
                            break;
                    }   // switch ( strW2APickListMatch )
                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < aobjPickListValues.length; intJ++ )
            }   // if ( aobjPickListValues.length > NUMERIC_ZERO )
        }   // if ( Array.isArray ( aobjPickListValues ) )

        return false;
    }   // IsPickListValueValid


    IsString ( poAnyJavaScriptObject )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        IsString

            Method Goal:        Return Boolean True when poAnyJavaScriptObject
                                is a string.

            Input:              poAnyJavaScriptObject = JavaScript object to
                                                        evaluate for whether it
                                                        is a string

            Output:             If poAnyJavaScriptObject is a string, the return
                                value is Boolean True, otherwise, it is False.

            Reference:          Check if a variable is a string in JavaScript
                                https://stackoverflow.com/questions/4059147/check-if-a-variable-is-a-string-in-javascript
            --------------------------------------------------------------------
        */

        if ( typeof poAnyJavaScriptObject === 'string' || poAnyJavaScriptObject instanceof String )
            return true;
        else
            return false;
    }   // IsString method


    IsWiseAgentMobilePage ( )
    {
        //  --------------------------------------------------------------------
        //  Since String.prototype.endswith lacks a case insensitive overload,
        //  convert the pathname to lower case and evaluate against a lower case
        //  argument. Likewise, test for strings with and without a terminal
        //  forward slash.
        //  --------------------------------------------------------------------

        const strPathNameLC = location.pathname.toLowerCase ( );

        if ( strPathNameLC.endsWith ( '/Mobile' ) || strPathNameLC.endsWith ( '/Mobile/' ) )
        {
            if ( ( ! Object.is ( LLCommon , undefined ) ) && LLCommon !== null )
            {
                if ( ( ! Object.is ( LLCommon.EntityType , undefined ) ) && LLCommon.EntityType !== null )
                {
                    if ( ( ! Object.is ( LLCommon.EntityType.EntityName , undefined ) ) && LLCommon.EntityType.EntityName !== null )
                    {
                        if ( LLCommon.EntityType.EntityName === 'WA-Contact' )
                        {
                            return true;
                        }   // if ( LLCommon.EntityType.AbsoluteEntityName === 'WA-Contact' )
                    }   // The AbsoluteEntityName property on the LLCommon.EntityType property is neither undefined nor a null reference.) block, if ( Object.is ( LLCommon.EntityType.AbsoluteEntityName , undefined ) && LLCommon.EntityType.AbsoluteEntityName !== null )
                }   // TRUE (The EntityType property on the LLCommon object is neither undefined nor a null reference.) block, if ( Object.is ( LLCommon.EntityType , undefined ) && LLCommon.EntityType !== null )
            }   // TRUE (The LLCommon object is neither undefined nor a null reference.) block, if ( Object.is ( LLCommon , undefined ) && LLCommon !== null )
        }   // TRUE (The current page is the mobile page.) block, if ( strPathNameLC.endsWith ( '/Mobile' ) || strPathNameLC.endsWith ( '/Mobile/' ) )

        return false;
    }   // IsWiseAgentMobilePage method


    JumpToElementById ( pstrAnchorId )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        JumpToElementById

            Method Goal:        Navigate to ANY HTML tag that has a given ID.

            Arguments:          pstrAnchorId = ID of HTML element

            Returns:            Though this function is evaluated solely for its
                                side effects, it nevertheless has a return value
                                that callers can check for two possible errors.

                                1)  Argument pstrAnchorId must be a string, and
                                    it is instead an object of some other type.

                                2)  Argument pstrAnchorId must be a valid
                                    element ID in the context of the current
                                    HTML document.

            References: 1)  Jump to Anchor in JavaScript
                            https://www.delftstack.com/howto/javascript/javascript-jump-to-anchor/#:~:text=Anchor%20Jumping%20in%20JavaScript%20In%20JavaScript%2C%20we%20can,the%20user%20to%20the%20target%20element%20using%20location.href.

                        1)  Demonstration file, also linked into this project under the same basename.
                            D:\SalesTalk\JavaScript\Scripted_Navigation.HTML
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrAnchorId ) )
        {
            if ( document.getElementById ( pstrAnchorId ) )
            {
                var url       = location.href;              // Save URL without hash.
                location.href = "#"+pstrAnchorId;           // Navigate to the target element.

                return EMPTY_STRING;
            }   // TRUE (anticipated outcome) block, if ( document.getElementById ( pstrAnchorId ) )
            else
            {
                return 'Error in JumpToElementById: pstrAnchorId must represent a valid element ID. The document contains no element with an ID = ' + pstrAnchorId;
            }   // FALSE (unanticipated outcome) block, if ( document.getElementById ( pstrAnchorId ) )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrAnchorId ) )
        else
        {
            return 'Error in JumpToElementById: pstrAnchorId must be a string';
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrAnchorId ) )
    }   // JumpToElementById method


    PostEvent ( pstrEventIdString ,
                pdtmCurrEventTime ,
                pdtmEngagementSeconds ,
                pstrFileBaseName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        PostEvent

            Method Goal:        Record a behavior event in the SalesTalk
                                application database.

            Input:              pstrEventIdString       = String passed into the
                                                          event delegate method

                                pdtmCurrEventTime       = Long integer that
                                                          represents a
                                                          JavaScript Date object

                                pdtmEngagementSeconds   = Total engagement time,
                                                          in seconds

                                pstrFileBaseName        = Optional string that
                                                          represents the base
                                                          name of the video,
                                                          audio, or text file
                                                          that is fed to the
                                                          STT_VideoPlayer script

            Output:             This method returns void (nothing), and has only
                                side effects, consisting of a message written
                                onto the JavaScript console and appended to the
                                data base table that provides the backing store
                                for an application event log.

            Remarks:            Since this method may be called when a page is
                                closing, and Google have decided in their
                                infinite wisdom that synchonous AJAX calls while
                                a page is closing "hurt the user experience,"
                                the call defaults to POST, falling back to GET
                                when the document.visibilityState flag value is
                                equal to 'hidden'.

                                The effect of this change is pervasive; not only
                                must the value of the async Boolean change, so
                                must the values of the url and type, of which
                                the former reflects a different method name, and
                                the latter corresponds to the HTTP verb.
            --------------------------------------------------------------------
        */

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        const strValue2Log    = Object.is ( pstrFileBaseName , undefined ) ? this.PageTitle : pstrFileBaseName;

        if ( this.fDebugFlag )
        {
            LLCommon.Trace ( strMethodName + ': at ' + pdtmCurrEventTime
                             + ': In trackEvent for event ID string '
                             + pstrEventIdString
                             + ', location.search = '
                             + location.search );

            LLCommon.Trace ( strMethodName + ': DatabaseName = ' + this.STTDatabaseName );
            LLCommon.Trace ( strMethodName + ': DomainName   = ' + this.STTDomainName );
            LLCommon.Trace ( strMethodName + ': Email        = ' + URLParameterFromQueryString ( 'll_e' ) );
            LLCommon.Trace ( strMethodName + ': CampaignId   = ' + URLParameterFromQueryString ( 'll_c' ) );

            //  ----------------------------------------------------------------
            //  The Name value is appended to the Type value, pstrEventIdString,
            //  to construct the value that goes into the Description column of
            //  the Story-So-Far.
            //  ----------------------------------------------------------------

            LLCommon.Trace ( strMethodName + ': Type         = ' + pstrEventIdString );
            LLCommon.Trace ( strMethodName + ': Name         = ' + strValue2Log );
            LLCommon.Trace ( strMethodName + ': Value        = ' + document.location.href );
            LLCommon.Trace ( strMethodName + ': RawValue     = ' + document.location.href );
            LLCommon.Trace ( strMethodName + ': Page Title   = ' + this.PageTitle );
            LLCommon.Trace ( strMethodName + ': Duration     = ' + pdtmEngagementSeconds );
        }   // if ( this.fDebugFlag )

        var   strAjaxResult   = EMPTY_STRING;
        var   fAjaxError      = false;
        var   fKeepTrying     = true;
        var   intTotalRetries = NUMERIC_ZERO;

        const fRunAsync       = document.visibilityState === 'hidden' ? true : false;
        const strHttpVerb     = fRunAsync ? 'GET' : 'POST';

        try
        {
            do  // while ( fKeepTrying )
            {
                $.ajax (
                {
                    type    : strHttpVerb,
                    async   : fRunAsync,
                    url     : LLCommon.AjaxUrlPrefix + 'Open/CreateABehavior' + ( fRunAsync ? 'Get' : 'Post' ),
                    data    : {
                                'DomainName'    : this.STTDomainName,
                                'LeadId'        : Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ),
                                'Email'         : URLParameterFromQueryString ( 'll_e' ),
                                'CampaignId'    : URLParameterFromQueryString ( 'll_c' ),
                                'Type'          : pstrEventIdString,
                                'Name'          : strValue2Log,
                                'Value'         : document.location.href,
                                'RawValue'      : document.location.href,
                                'Duration'      : pdtmEngagementSeconds
                              },
                    success : function ( data )
                              {
                                if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                {   // The above Ajax call returned a value. Capture it.
                                    LLCommon.Trace ( data );
                                    strAjaxResult = data;
                                    return strAjaxResult;
                                }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                            },
                    error   : function ( jqXHR , textStatus , errorThrown )
                            {
                                strAjaxResult = textStatus
                                                + ' ' + jqXHR.responseText
                                                + ' ' + errorThrown;
                                fAjaxError    = true;
                                return strAjaxResult;
                            }
                });

                if ( fAjaxError )
                {   // The API reported an error. Check the retry count.
                    if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                        intTotalRetries++;
                        fAjaxError = false;
                    }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    else
                    {   // The retry limit has been reached. Allow control to leave the do while loop.
                        fKeepTrying = false;
                    }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                else
                {   // The attempt succeeded. Post a log entry and leave the loop.
                    fKeepTrying = false;

                    if ( intTotalRetries === NUMERIC_ZERO )
                    {
                        if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' A garden variety CreateABehavior' + ( fRunAsync ? 'Get' : 'Post' ) + ' to lead record ' + this.STTLeadId + ' succeeded on the first try.' } } );
                        }   // if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                    }   // TRUE (ideal outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                    else
                    {
                        $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' A garden variety CreateABehavior' + ( fRunAsync ? 'Get' : 'Post' ) + ' to lead record ' + this.STTLeadId + ' succeeded after ' + intTotalRetries + ' retries.' } } );
                    }   // FALSE (less than ideal, but acceptable, outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
            } while ( fKeepTrying )

            if ( fAjaxError )
            {
                $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' Posting of a ' + pstrEventIdString + ' CreateABehavior' + ( fRunAsync ? 'Get' : 'Post' ) + ' event to lead record ' + this.STTLeadId + ' FAILED after ' + intTotalRetries + ' retries.' } } );
            }   // TRUE (unanticipated outcome) block, if ( fAjaxError )
            else {
                if ( Object.is ( pstrFileBaseName , undefined ) || IsPlayingMedia ( pstrFileBaseName , this ) )
                {
                    const strDomainInfo   = LLCommon.DoAjax ( 'RulesForLeadId',
                                                              'GET',
                                                              {
                                                                  'Id'       : this.STTLeadId.toString ( ),
                                                                  'DomainId' : this.STTDomainId.toString ( ),
                                                                  'TenantId' : this.STTTenantId.toString ( )
                                                              } );
                }   // if ( Object.is ( pstrFileBaseName , undefined ) || IsPlayingMedia ( pstrFileBaseName , this )
            }   // FALSE (anticipated outcome) block, if ( fAjaxError )
        }
        catch ( ex )
        {
            LLCommon.Trace ( ex );
            LLCommon.LogException ( ex );
            return ex;
        }

        function IsPlayingMedia ( pstrFileBaseName , poThis )
        {
            /*
                ----------------------------------------------------------------
                Name:   IsPlayingMedia

                Goal:   Evaluate the extension of the file name, returing TRUE
                        when it belongs to a media (audio or video) file.

                In:     pstrFileBaseName = Basename of input file
                        poThis           = Reference to this (containing object)
                                           so that this routine can invoke its
                                           methods

                Out:    True if the extension is either .MP3 (audio) or .MP4
                        (video), otherwise False
                ----------------------------------------------------------------
            */

            const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

            const strExtensionLC = poThis.GetExtension ( pstrFileBaseName ).toLowerCase ( );

            if ( strExtensionLC === '.mp3' || strExtensionLC === '.mp4' )
            {
                if ( Object.is ( poThis.STTLeadId , undefined ) )
                {
                    return false;
                }   // TRUE (The value of the property that stores the lead ID is undefined.) block, if ( Object.is ( this.STTLeadId , undefined ) )
                else
                {
                    return ( poThis.STTLeadId > NUMERIC_ZERO );
                }   // FALSE (The value of the property that stores the lead ID is defined.) block, if ( Object.is ( this.STTLeadId , undefined ) )
            }   // TRUE (The player is rendering a multimedia file.) block, if ( strExtensionLC === '.mp3' || strExtensionLC === '.mp4' )
            else
            {
                return false;
            }   // FALSE (The player is rendering a text file.) block, if ( strExtensionLC === '.mp3' || strExtensionLC === '.mp4' )
        }   // function IsPlayingMedia
    }   // PostEvent method


    ProcessLandingPageForm ( pstrBehaviorType ,
                             pstrEmailId ,
                             pstrFirstName ,
                             pstrLastName ,
                             pstrMobilePhone ,
                             poControls2Append )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ProcessLandingPageForm

            Method Goal:        Post fields input into a Form embedded in a
                                SalesTalk Landing Page.

            Input:              pstrBehaviorType     = String passed into the
                                                       event delegate function

                                pstrEmailId          = Email ID from form or
                                                       other source

                                pstrFirstName        = FirstName field from form

                                pstrLastName         = LastName field from form

                                pstrMobilePhone      = MobilePhone field from
                                                       form

                                poControls2Append    = Array of form inputs by
                                                       ID and value to append to
                                                       the CreateABehavior call

            Output:             The return value is the ID of the lead record
                                that was either created or appended, along with
                                the ID of bhe behavior record.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        if ( this.fDebugFlag )
        {
            LLCommon.Trace (   strMethodName + ': at ' + this.PageLoadTime_JS_Date
                             + ', In trackEvent for event ID string ' + pstrBehaviorType
                             + ', location.search = ' + location.search );

            LLCommon.Trace ( strMethodName + ': DatabaseName = ' + this.STTDatabaseName );
            LLCommon.Trace ( strMethodName + ': DomainName   = ' + this.STTDomainName );
            LLCommon.Trace ( strMethodName + ': Email        = ' + URLParameterFromQueryString ( 'll_e' ) );
            LLCommon.Trace ( strMethodName + ': CampaignId   = ' + pstrEmailId );
            LLCommon.Trace ( strMethodName + ': Type         = ' + pstrBehaviorType );
            LLCommon.Trace ( strMethodName + ': Name         = ' + this.PageTitle );
            LLCommon.Trace ( strMethodName + ': Value        = ' + this.AbsoluteLocation );
            LLCommon.Trace ( strMethodName + ': RawValue     = ' + window.location );

            LLCommon.Trace ( strMethodName + ': Calling CreateABehaviorPost via ajax: this.STTDomainName = ' + this.STTDomainName );
        }   // if ( this.fDebugFlag )

        try
        {
            var strAdditionalFields;
            var strFields2Merge;

            if ( poControls2Append === undefined )
            {
                strAdditionalFields = EMPTY_STRING;
                strFields2Merge     = EMPTY_STRING;
            }   // TRUE (There are no extra fields.) block, if ( poControls2Append === undefined )
            else
            {
                if ( poControls2Append === null )
                {
                    strAdditionalFields = EMPTY_STRING;
                    strFields2Merge     = EMPTY_STRING;
                }   // TRUE (There are no extra fields.) block, if ( poControls2Append === null )
                else
                {
                    if ( poControls2Append.length === SINGLE_CHARACTER )
                    {
                        strAdditionalFields = EMPTY_STRING;
                        strFields2Merge     = EMPTY_STRING;
                    }   // TRUE (There are no extra fields.) block, if ( poControls2Append.length === SINGLE_CHARACTER )
                    else
                    {
                        for ( var i = ARRAY_FIRST_ELEMENT,
                                  iLen = poControls2Append.length;
                              i < iLen;
                              i++ )
                        {   // Since the array already has the values, there is nothing to do but make a list for CreateABehavior.
                            if ( ( !Object.is ( poControls2Append [ i ].ControlValue, undefined ) && poControls2Append [ i ].ControlValue.length > EMPTY_STRING_LENGTH ) || this.fAllowEmptyFields )
                            {
                                var strFinalValue           = Object.is ( poControls2Append [ i ].ControlValue, undefined ) ? EMPTY_STRING : poControls2Append [ i ].ControlValue;
                                LLCommon.Trace ( strMethodName + ': For poControls2Append [ i ], where i = ' + i + ': poControls2Append [ i ].ControlId = ' + poControls2Append [ i ].ControlId + ', strFinalValue = ' + strFinalValue + ', and the final string = ' + ( poControls2Append [ i ].ControlId + EQUALS_CHAR + strFinalValue ) );

                                if ( poControls2Append [ i ].ClassName.indexOf ( 'LLMergeValueLists' ) > INDEXOF_NOT_FOUND )
                                {
                                    if ( strFields2Merge === undefined )
                                    {
                                        strFields2Merge     =   poControls2Append [ i ].ControlId
                                                              + EQUALS_CHAR
                                                              + strFinalValue;
                                    }   // TRUE (The list is empty.) block, if ( strFields2Merge === undefined )
                                    else
                                    {
                                        strFields2Merge     =   strAdditionalFields
                                                              + LOGICAL_NEGATE
                                                              + poControls2Append [ i ].ControlId
                                                              + EQUALS_CHAR
                                                              + strFinalValue;
                                    }   // FALSE (The list contains at least one item.) block, if ( strFields2Merge === undefined )
                                }   // TRUE (The control's value goes into the list of controls whose values are merged into a space-delimited list in a long text field.) block, if (  poControls2Append [ i ].ClassName.indexOf ( 'LLMergeValueLists' ) > INDEXOF_NOT_FOUND )
                                else
                                {
                                    if ( strAdditionalFields === undefined )
                                    {
                                        strAdditionalFields =   poControls2Append [ i ].ControlId
                                                              + EQUALS_CHAR
                                                              + strFinalValue;
                                    }   // TRUE (The list is empty.) block, if ( strAdditionalFields === undefined )
                                    else
                                    {
                                        strAdditionalFields =   strAdditionalFields
                                                              + LOGICAL_NEGATE
                                                              + poControls2Append [ i ].ControlId
                                                              + EQUALS_CHAR
                                                              + strFinalValue;
                                    }   // FALSE (The list contains at least one item.) block, if ( strAdditionalFields === undefined )
                                }   // FALSE (The control's value goes into the list of controls whose values update their like-named fields.) block, if (  poControls2Append [ i ].ClassName.indexOf ( 'LLMergeValueLists' ) > INDEXOF_NOT_FOUND )
                            }   // if ( ( !Object.is ( poControls2Append [ i ].ControlValue, undefined ) && poControls2Append [ i ].ControlValue.length > EMPTY_STRING_LENGTH ) || this.fAllowEmptyFields )
                        }   // for ( var i = ARRAY_FIRST_ELEMENT, iLen = poControls2Append.length; i < iLen; i++ )
                    }   // FALSE (There is at least one extra field.) block, if ( poControls2Append.length === SINGLE_CHARACTER )
                }   // FALSE (There is at least one extra field.) block, if ( poControls2Append === null )
            }   // FALSE (There is at least one extra field.) block, if ( poControls2Append === undefined )

            var strAjaxResult   = EMPTY_STRING;
            var fAjaxError      = false;
            var intTotalRetries = NUMERIC_ZERO;
            var fKeepTrying     = true;

            do  // while ( fKeepTrying )
            {
                $.ajax (
                {
                        type    : 'POST',
                        async   : false,
                        url     : LLCommon.AjaxUrlPrefix + 'Open/CreateABehaviorPost',
                        data    : {
                                    'DomainName'      : this.STTDomainName,
                                    'LeadId'          : Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ),
                                    'Email'           : pstrEmailId,
                                    'CampaignId'      : EMPTY_STRING,
                                    'Type'            : pstrBehaviorType,
                                    'Name'            : this.PageTitle,
                                    'Value'           : this.AbsoluteLocation,
                                    'RawValue'        : window.location.href,
                                    'Duration'        : CHARACTER_ZERO,
                                    'FirstName'       : pstrFirstName,
                                    'LastName'        : pstrLastName,
                                    'MobilePhone'     : pstrMobilePhone,
                                    'CustomFields'    : strAdditionalFields,
                                    'Fields2Merge'    : strFields2Merge,
                                    'tzOffsetMinutes' : this.UtcOffsetMinutes
                                } ,
                        success : function ( data )
                                {
                                    if ( ( data !== undefined ) && ( data !== null ) && ( data !== '' ) )
                                    {   // The above Ajax call returned a value. Capture it.
                                        LLCommon.Trace ( strMethodName + ': ' + data );
                                        strAjaxResult = data;
                                    }   // TRUE (anticipated outcome) block, if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                    else
                                    {
                                        LLCommon.Trace ( strMethodName +  ': CreateABehaviorPost returned the empty string' );
                                        return strAjaxResult;
                                    }   // FALSE (unanticipated outcome) block, if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                },
                        error   : function ( jqXHR , textStatus , errorThrown )
                                {
                                    strAjaxResult = textStatus
                                                    + ' ' + jqXHR.responseText
                                                    + ' ' + errorThrown;
                                    fAjaxError    = true;
                                    return strAjaxResult;
                                }

                });

                if ( fAjaxError )
                {   // The API reported an error. Check the retry count.
                    if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                        intTotalRetries++;
                        fAjaxError = false;
                    }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                    else
                    {   // The retry limit has been reached. Allow control to leave the do while loop.
                        fKeepTrying = false;
                    }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < this.AJAX_RETRY_LIMIT )
                }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                else
                {   // The attempt succeeded. Post a log entry and leave the loop.
                    fKeepTrying = false;

                    if ( intTotalRetries === NUMERIC_ZERO )
                    {
                        if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' Form update of lead record ' + this.STTLeadId + ' succeeded on the first try.' } } );
                        }   // if ( this.AJAX_REPORT_SUCCESS_ON_FIRST_TRY )
                    }   // TRUE (ideal outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                    else
                    {
                        $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' Form update of lead record ' + this.STTLeadId + ' succeeded after ' + intTotalRetries + ' retries.' } } );
                    }   // FALSE (less than ideal, but acceptable, outcome) block, if ( intTotalRetries === NUMERIC_ZERO )
                }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
            } while ( fKeepTrying )

            if ( fAjaxError )
            {   // There was an exception; log it.
                LLCommon.LogException ( strAjaxResult );
                strAjaxResult = 'At ' + new Date ( ) + ', SalesTalk made ' + this.AJAX_RETRY_LIMIT + ' futile attempts to post your updates to lead ID ' + this.STTLeadId + '. Please contact customer support for assistance, giving them the time and lead ID shown in this message.';
                alert ( strAjaxResult );
                throw ( strAjaxResult );
            }   // TRUE (Unanticipated outcome) block, if ( fAjaxError )

            return strAjaxResult;
        }
        catch ( ex )
        {
            LLCommon.Trace ( strMethodName + this.COLON_SPACE + ex.message );
            LLCommon.LogException ( ex );
            strAjaxResult = 'At ' + new Date ( ) + ', SalesTalk encountered an internal error while your updates to lead ID ' + this.STTLeadId + '. Please contact customer support for assistance, giving them the time and lead ID shown in this message.';
            alert ( strAjaxResult );
            throw ( strAjaxResult );
        }
    }   // ProcessLandingPageForm method


    QueryAssociativeArray ( poKey , poDefaultValue , poArray )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        QueryAssociativeArray

            Method Goal:        Test for existence of poKey in array poArray,
                                returning the corresponding value if so,
                                otherwise poDefaultValue.

            Input:              poKey           = Primitive value that serves as
                                                  the key of associative array
                                                  poArray

                                poDefaultValue  = Value to return when poKey is
                                                  absent from array poArray

                                poArray         = Associative array to search

                                                  See Remarks.

            Output:             Value stored at key poKey of associative array
                                poArray

            Remarks:            Array argument poArray is expected to be an
                                associative array that is organized like the
                                array shown below.

                                    {
                                        "Company.AnnualRevenue" :"CompanyAnnualRevenue",
                                        "Company.Description"   :"CompanyDescription",
                                        "Company.EmployeeCount" :"CompanyEmployeeCount",
                                        "Company.Industry"      :"CompanyIndustry",
                                    };

                                In the above example, the first string such as
                                "Company.AnnualRevenue" is the key, while
                                "CompanyAnnualRevenue" is the value that would
                                be returned for a poKey value of
                                "Company.AnnualRevenue".

                                Although poArray is characterized as an array,
                                Array.IsArray returns false.

                                Strings may be single or double quoted, and keys
                                need not be strings, but they must be of the
                                same type as the values given for argument poKey.
                                Likewise, the values expected to be returned may
                                be of any type, and need not be of the same type
                                in every element.

                                This function is syntactic sugar that is easy to
                                replace with its one expression. Nevertheless,
                                the notes assembled above justify keeping it.
            --------------------------------------------------------------------
        */

        return poKey in poArray ? poArray [ poKey ] : poDefaultValue;
    }   // QueryAssociativeArray method


    RequiredFieldHasValue ( pdocElement )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        RequiredFieldHasValue

            Method Goal:        Evaluate an INPUT element to determine whetheer
                                its adjacent element is a DropDown overlay
                                button OR the element is marked as REQUIRED AND
                                is empty.

            Input:              pdocElement = Element to evaluate for an
                                              associated DropDown overlay button
                                              or REQUIRED value

            Output:             If the element identified by pdocElement has a
                                DropDown overlay button associated with it OR it
                                is marked as REQUIRED but its value is the empty
                                string, the return value is True. Otherwise, the
                                return value is False.

            Algorithm:          1)  If the nodeName attribute of pdocElement is
                                    INPUT and its type is text, evaluate its
                                    nextElementSibling attributee.

                                2)  Compare the ID of the nextElementSibling
                                    against that of the pdocElement itself.
                                    If the ID is the same with a suffix of
                                    "_DropDown" and its nodeName is BUTTON, then
                                    the return value is True. Otherwise, advance
                                    to step 3.

                                3)  If the classList string contains the value
                                    `STT_REQUIRED` and the length of its Value
                                    property string is GREATER than zero, the
                                    return value is True.

                                4)  If neither of the above two conditions is
                                    true, the return value is False.

            Remarks:            The pdocElement argument may be a reference to a
                                document element or a string that contains its
                                ID, and the nodeName of the element must be
                                `INPUT`.
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const docElement2Test   = LLCommon.IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

        try
        {
            if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
            {
                if ( docElement2Test.className.indexOf ( this.CLASSNNAME_STT_REQUIRED ) > INDEXOF_NOT_FOUND )
                {
                    return ( docElement2Test.value.length > EMPTY_STRING_LENGTH );
                }   // TRUE (The field is marked as REQUIRED.) block, if ( docElement2Test.className.indexOf ( this.CLASSNNAME_STT_REQUIRED ) > INDEXOF_NOT_FOUND )
                else
                {
                    return true;
                }   // FALSE (The field is OPTIONAL.) block, if ( docElement2Test.className.indexOf ( this.CLASSNNAME_STT_REQUIRED ) > INDEXOF_NOT_FOUND )
            }   // TRUE (anticipated outcome) block, if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
            else
            {
                return true;
            }   // FALSE (unanticipated outcome) block, if ( docElement2Test.nodeName !== undefined && docElement2Test.nodeName === 'INPUT' && docElement2Test.type === 'text' )
        }
        catch ( ex )
        {
            LLCommon.Trace ( LLCommon.LogException ( ex ) );
            return true;
        }
    }   // RequiredFieldHasValue method


    SetLeadMasterFieldValue ( pstrFieldName , pstrValueFromLead , poaControls2Append )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        SetLeadMasterFieldValue

            Method Goal:        Respond to events raised by the Video Player tag
                                and the body tag of the HTML page that contains
                                it by interpreting the behavior in the context
                                of preveious events raised by the player.

            Input:              pstrFieldName       = String representation of
                                                      the field name as it
                                                      appears in the lead record
                                                      and the value token in the
                                                      form input element

                                pstrValueFromLead   = String representation of
                                                      the field value read from
                                                      the lead record in the
                                                      database, which may or may
                                                      not be blank

                                poaControls2Append  = Array that is treated as a
                                                      dictionary of field values
                                                      read from the form

            Output:             When the value read from the control is an empty
                                string, return the value read from the lead, so
                                that it is copied back when the form posts.

                                Otherwise, return the value read from the form
                                control, so that it is posted into the lead when
                                the form posts.

            Remarks:            The goal is to update the lead record from the
                                form when the form contains data, otherwise to
                                preserve the value already in the lead record.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var   strTempValue  = EMPTY_STRING;

        try
        {
            strTempValue    = this.GetControlValue ( pstrFieldName, poaControls2Append );
        }
        catch ( ex )
        {
            LLCommon.Trace ( 'Inside SetLeadMasterFieldValue: pstrFieldName = ' + pstrFieldName );
            LLCommon.Trace ( 'Inside SetLeadMasterFieldValue: strTempValue  = ' + strTempValue );
            LLCommon.LogException ( strMethodName + ': pstrFieldName = ' + pstrFieldName , ex );
        }

        return strTempValue === EMPTY_STRING ? pstrValueFromLead : strTempValue;
    }   // SetLeadMasterFieldValue method


    SkipEmptyFields ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        SkipEmptyFields

            Method Goal:        Call this method to restore the default behavior
                                of skipping (not posting) empty (blank0 fields.

            Output:             This method returns void.
            --------------------------------------------------------------------
        */

        this.fAllowEmptyFields = false;
    }   // SkipEmptyFields method


    SubmitForm ( pstrButtonName , pfCallRulesEngine )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        SubmitForm

            Method Goal:        This method wraps ProcessLandingPageForm.

            Input:              pstrButtonName    = String representation of the
                                                    button, formatted such that
                                                    JQuery can get the element

                                pfCallRulesEngine = Unless undefined, call the
                                                    the Rules Engine either sync
                                                    or async depending on the
                                                    value, as described below.

                                                    true              = call async
                                                    false             = call sync
                                                    Undefined or null = don't call

            Output:             This method has no return value.
            --------------------------------------------------------------------
        */

        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        try
        {
            const adocButtons               = $( pstrButtonName );
            const docContainingForm         = adocButtons [ ARRAY_FIRST_ELEMENT ].form;
            const oEmailIdInputControl      = this.GetElementByNameInContainer ( 'Email' ,
                                                                                 docContainingForm );

            //  ----------------------------------------------------------------
            //  Initialize strings to recive the four values from either form
            //  fields or the database. Since the lead ID is always available,
            //  we can get them from the database.
            //  ----------------------------------------------------------------

            var strEmailId;
            var strFirstName;
            var strLastName;
            var strMobilePhone;

            if ( Object.is ( this.STTDomainId , undefined ) || Object.is ( this.STTTenantId , undefined ) )
            {
                const strDomainInfo         = LLCommon.DoAjax ( 'GetDomainAndTenantIdForName',
                                                                'GET',
                                                                {
                                                                   'DomainName' : this.STTDomainName
                                                                } );
                const astrDomainIds         = strDomainInfo.split ( LOGICAL_NEGATE );
                this.STTDomainId            = parseInt ( astrDomainIds [ ARRAY_FIRST_ELEMENT  ] );
                this.STTTenantId            = parseInt ( astrDomainIds [ ARRAY_SECOND_ELEMENT ] );
            }   // if ( Object.is ( this.STTDomainId, undefined ) )

            var astrLeadInfoFields;

            debugger;

            if ( this.IsCustomPortal ( ) && oEmailIdInputControl !== null )
            {
                strEmailId                  = oEmailIdInputControl.value;
                const strLeadInfo           = LLCommon.DoAjax ( 'GetOrCreateBasicLeadRecord',
                                                                'GET',
                                                                {
                                                                   'Email'    : strEmailId,
                                                                   'DomainId' : this.STTDomainId
                                                                } );
                astrLeadInfoFields          = strLeadInfo.split ( LOGICAL_NEGATE );
                this.STTLeadId              = parseInt ( astrLeadInfoFields [ ARRAY_FIRST_ELEMENT ] );
            }   //  (The page that hosts this library is a Custom Portal.) block, if ( this.IsCustomPortal ( ) && oEmailIdInputControl !== null )

            if ( Object.is ( oEmailIdInputControl, undefined ) )
            {
                this.STTLeadBasicInfo       = this.GetVeryBasicLeadInfo4LeadId ( Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ) );

                if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
                {   // See ESLint: no-prototype-builtins, at https://eslint.org/docs/latest/rules/no-prototype-builtins
                    strEmailId              = this.STTLeadBasicInfo.GetEmail ( );
                    strFirstName            = this.STTLeadBasicInfo.GetFirstName ( );
                    strLastName             = this.STTLeadBasicInfo.GetLastName ( );
                    strMobilePhone          = this.STTLeadBasicInfo.GetMobilePhone ( );
                }   // TRUE (anticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
                else
                {
                    throw ( this.STTLeadBasicInfo );
                }   // FALSE (unanticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
            }   // TRUE (Since email, first and last name, and mobile phone number fields are absent, get them from the database.) block, if ( Object.is ( oEmailIdInputControl, undefined ) )
            else
            {
                strEmailId                  = oEmailIdInputControl.value;

                if ( Object.is ( this.STTLeadId , undefined ) || this.STTLeadId === NUMERIC_ZERO )
                {
                    const strLeadInfo       = LLCommon.DoAjax ( 'GetOrCreateBasicLeadRecord',
                                                                'GET',
                                                                {
                                                                   'Email'    : strEmailId,
                                                                   'DomainId' : this.STTDomainId.toString ( )
                                                                } );
                    astrLeadInfoFields      = strLeadInfo.split ( LOGICAL_NEGATE );
                    this.STTLeadId          = parseInt ( astrLeadInfoFields [ ARRAY_FIRST_ELEMENT ] );
                }   // if ( Object.is ( this.STTLeadId, undefined ) || this.STTLeadId === NUMERIC_ZERO )

                this.STTLeadBasicInfo       = this.GetVeryBasicLeadInfo4LeadId ( Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ) );

                if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo , "VERSION" ) )
                {
                    this.STTLeadId          = parseInt ( this.STTLeadBasicInfo.GetLeadId ( ) );

                    strFirstName            = this.STTLeadBasicInfo.GetFirstName ( );
                    strLastName             = this.STTLeadBasicInfo.GetLastName ( );
                    strMobilePhone          = this.STTLeadBasicInfo.GetMobilePhone ( );
                }   // TRUE (anticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( basicLeadInfo1, "VERSION" ) )
                else
                {
                    throw ( this.STTLeadBasicInfo );
                }   // FALSE (unanticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( basicLeadInfo1, "VERSION" ) )

                if ( Object.is ( strEmailId, undefined ) || Object.is ( strFirstName, undefined ) || Object.is ( strLastName, undefined ) || Object.is ( strMobilePhone, undefined ) )
                {
                    this.STTLeadBasicInfo   = this.GetVeryBasicLeadInfo4LeadId ( Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ) );

                    if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
                    {   // See ESLint: no-prototype-builtins, at https://eslint.org/docs/latest/rules/no-prototype-builtins
                        strEmailId          = this.STTLeadBasicInfo.GetEmail ( );
                        strFirstName        = this.STTLeadBasicInfo.GetFirstName ( );
                        strLastName         = this.STTLeadBasicInfo.GetLastName ( );
                        strMobilePhone      = this.STTLeadBasicInfo.GetMobilePhone ( );
                    }   // TRUE (anticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
                    else
                    {
                        throw ( this.STTLeadBasicInfo );
                    }   // FALSE (unanticipated outcome) block, if ( Object.prototype.hasOwnProperty.call ( this.STTLeadBasicInfo, "VERSION" ) )
                }   // if ( Object.is ( strFirstName, undefined ) || Object.is ( strLastName, undefined ) || Object.is ( strMobilePhone, undefined ) )
            }   // FALSE (Since email is present, anticipate that the rest are, too.) block, if ( Object.is ( oEmailIdInputControl, undefined ) )

            const oaControls2Append         = this.GatherAdditionalFieldValues ( docContainingForm.elements );
            const strSessionKey             = document.location.href + LOGICAL_NEGATE + 'PostFormData' + LOGICAL_NEGATE + pstrButtonName;
            const oaValuesPosted            = sessionStorage.getItem ( strSessionKey );

            if ( Object.is ( oaValuesPosted, null ) || oaValuesPosted !== JSON.stringify ( oaControls2Append ) )
            {
                strFirstName                = this.SetLeadMasterFieldValue ( 'FirstName' ,                       strFirstName ,   oaControls2Append );
                strLastName                 = this.SetLeadMasterFieldValue ( 'LastName' ,                        strLastName ,    oaControls2Append );
                strMobilePhone              = this.SetLeadMasterFieldValue ( 'LeadPhones.Phone{Mobile}.Number' , strMobilePhone , oaControls2Append );

                if ( this.VisibleRequiredFieldsHaveValues ( docContainingForm ) )
                {
                    const strRetVal         = this.ProcessLandingPageForm ( 'PostFormData' ,
                                                                            strEmailId ,
                                                                            strFirstName,
                                                                            strLastName,
                                                                            strMobilePhone,
                                                                            oaControls2Append );

                    if ( this.fDebugFlag )
                    {
                        LLCommon.Trace ( 'this.ProcessLandingPageForm return value = ' + strRetVal );
                    }   // if ( this.fDebugFlag )

                    alert ( 'Form successfully posted' , 'native' );

                    //  ----------------------------------------------------------------
                    //  After the form submits, refresh the reference fields represented
                    //  by spans that are assigned CSS selector STTformField2Redisplay.
                    //  ----------------------------------------------------------------

                    const adocFields2Redisplay = $( '.STTformField2Redisplay' );

                    if ( adocFields2Redisplay.length > ARRAY_IS_EMPTY )
                    {
                        this.HandleSpecialPrefill ( adocFields2Redisplay );
                    }   // if ( adocFields2Redisplay.length > ARRAY_IS_EMPTY )

                    //  ----------------------------------------------------------------
                    //  If the magic field is present in the form or the global flag is
                    //  set, call the rules engine.
                    //  ----------------------------------------------------------------

                    if ( ( ! Object.is ( pfCallRulesEngine , undefined ) ) || this.TestForSignalElementByIdAndType ( 'STT_FireRulesEngine_Flag' , 'checkbox' ) )
                    {   // The flag field affords direct fine-grained control over firiing the rules.
                        const strDomainInfo    = LLCommon.DoAjax ( 'RulesForLeadId',
                                                                   'GET',
                                                                   {
                                                                       'Id'       : Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ),
                                                                       'DomainId' : this.STTDomainId,
                                                                       'TenantId' : this.STTTenantId
                                                                   },
                                                                   pfCallRulesEngine );
                    }   // if ( ( ! Object.is ( pfCallRulesEngine , undefined ) ) || this.TestForSignalElementByIdAndType ( 'STT_FireRulesEngine_Flag' , 'checkbox' ) )

                    //  ----------------------------------------------------------------
                    //  Save the values that went into the current lead record, so that
                    //  we can know whether the user changed a value and resubmitted,
                    //  and the form should be treated as a new submit, or whether the
                    //  submit button was activated repeatedly by the operator or a
                    //  script, and submitting it again is redundant and wasteful.
                    //  ----------------------------------------------------------------

                    const jsonValuesPosted     = JSON.stringify ( oaControls2Append );

                    sessionStorage.setItem ( strSessionKey , jsonValuesPosted );
                    $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' SubmitForm posted new data to lead record ' + this.STTLeadId + FULL_STOP } } );

                    if ( this.TestForSignalElementByIdAndType ( 'STT_Redirect2ActionUrl_Flag' , 'checkbox' ) )
                    {
                        //  --------------------------------------------------------
                        //  The idea behind this bit is to do the redirect when the
                        //  form action is explicit and differs from the page that
                        //  hosts the form.
                        //  --------------------------------------------------------

                        const strFormActionUri = docContainingForm.action;

                        if ( strFormActionUri.length > EMPTY_STRING_LENGTH )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' SubmitForm redirecting to ' + strFormActionUri } } );
                            window.location.replace ( strFormActionUri );
                        }   // TRUE (anticipated outcome) block, if ( strFormActionUri.length > EMPTY_STRING_LENGTH )
                        else
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' Form STT_Redirect2ActionUrl_Flag checkbox is present but the action URI is undefined (the empty string).' } } );
                        }   // FALSE (unanticipated outcome) block, if ( strFormActionUri.length > EMPTY_STRING_LENGTH )
                    }   // if ( this.TestForSignalElementByIdAndType ( 'STT_Redirect2ActionUrl_Flag' , 'checkbox' ) )
                }   // if ( this.VisibleRequiredFieldsHaveValues ( docContainingForm ) )
            }   // TRUE (anticipated outcome) block, if ( Object.is ( oaValuesPosted, null ) || ( JSON.stringify ( oaValuesPosted ) !== oaValuesPosted ) )
            else
            {
                $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': this.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' SubmitForm suppressed posting identical inputs to lead record ' + this.STTLeadId + FULL_STOP } } );
            }   // FALSE (unanticipated outcome) block, if ( Object.is ( oaValuesPosted, null ) || ( JSON.stringify ( oaValuesPosted ) !== oaValuesPosted ) )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
        }
    }   // SubmitForm method


    TestForSignalElementByIdAndType ( pstrElementId , pstrElementType )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        TestForSignalElementByIdAndType

            Method Goal:        Extract the local (unqualified) file name from
                                the end of a URL.

            Input:              pstrElementId       = String representation of a
                                                      class name by which to
                                                      constrain the selection

                                pstrElementType     = String representation of a
                                                      type attribute by which to
                                                      constrain the selection

            Output:             If the current document contains an element with
                                the ID specified by pstrElementId and the type
                                specified by pstrElementType, the return value
                                is Boolean TRUE. Otherwise, the return value is
                                Boolean FALSE.

            Dependencies:       Custom function IsString returns TRUE when fed a
                                string or string-like object.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrElementId ) && LLCommon.IsString ( pstrElementType ) )
        {
            const odocRedirect2ActionURLFlag = $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( pstrElementId ) + '[type="' + pstrElementType + '"]' );
            return ( odocRedirect2ActionURLFlag.length > ARRAY_IS_EMPTY );
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrElementId ) && LLCommon.IsString ( pstrElementType ) )
        else
        {
            return false;
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrElementId ) && LLCommon.IsString ( pstrElementType ) )
    }   // TestForSignalElementByIdAndType method


    TrackEvent ( pstrEventIdString )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        TrackEvent

            Method Goal:        Respond to events raised by the Video Player tag
                                and the body tag of the HTML page that contains
                                it by interpreting the behavior in the context
                                of preveious events raised by the player.

            Input:              pstrEventIdString   = String passed into event
                                                      delegate function

            Output:             This function returns nothing, and has only side
                                effects, consisting of messages written onto the
                                JavaScript console and appended to the data base
                                table that provides the backing store for an
                                application event log.

            Remarks:            This single event delegate responds to every
                                event raised in the page that hosts it.

                                The value of argument pstrEventIdString causes
                                its behavior to match the type of event to which
                                it responded. Global variables that might become
                                object properties in a future version maintain
                                state information during the lifetime of the
                                page that hosts it.

                                For clarity, player state evaluation is handled
                                by a set of functions that have names that start
                                with Is, and logging is delegated to PostEvent,
                                another void function.
            --------------------------------------------------------------------
        */

        const strMethodName        = LLCommon.GetNameOfCurrentFunction ( );

        const dtmCurrEventTime     = new Date ( );

        if ( this.fDebugFlag )
        {
            LLCommon.Trace ( 'In TrackEvent method: Arguments: pstrEventIdString     = ' + pstrEventIdString );
            LLCommon.Trace ( '                                 dtmCurrEventTime      = ' + dtmCurrEventTime.getTime ( ) );
        }   // if ( this.fDebugFlag )

        var   dtmEngagementSeconds = this.ComputeEngagementTime ( pstrEventIdString ,
                                                                  dtmCurrEventTime );

        this.PostEvent ( pstrEventIdString,
                         dtmCurrEventTime ,
                         dtmEngagementSeconds );
    }   // TrackEvent method


    UpdateFormFieldById ( pstrElementId , pstrCustomFieldName , pfCallRulesEngine , pfUpdateLeadModDate )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        UpdateFormFieldById

            Method Goal:        Update a CHANGED input element value by sending
                                its new value to the server.

            Input:              pstrElementId       = ID or Name, with a strong
                                                      preference for a Name, of
                                                      the field as it appears on
                                                      the form, prefixing with a
                                                      # character to assert that
                                                      the string is an ID

                                pstrCustomFieldName = The internal name of the
                                                      Custom Field to update,
                                                      used in lieu of the value
                                                      of pstrElementId when
                                                      supplied

                                pfCallRulesEngine   = Unless undefined, call the
                                                      the Rules Engine either
                                                      sync or async depending on
                                                      the value, as described
                                                      in the SubmitForm flower
                                                      box.

                                pfUpdateLeadModDate = Boolean flag, defaulted to
                                                      TRUE, that controls whether
                                                      the last modified date of
                                                      the controlling Lead row is
                                                      updated, but see Remarks

            Output:             If it succeeds, the method returns the empty
                                string. Otherwise, it returns a string contaiing
                                a message, which the caller may ignore. It need
                                only take the length being greater than zero as
                                an indication that it might want to pop an alert
                                that the field update failed.

            Remarks:            Any exceptions that arise are logged before this
                                method returns control. Hence, it is enough to
                                pop an alert when one is detected by the return
                                value being a string with a length greater than
                                zero.

                                This method calls UpdateFormFieldByInternalName
                                on the OpenController object through DoAjax. The
                                new method has been shown to work correctly, by
                                a test conducted by calling it directly through
                                a Web browser.

                                When this routine is called on behalf of a task
                                that is associated with a CRMEntity, its
                                Description property is queried for a key named
                                UpdateLeadModDate. When that key is present, and
                                is set to Boolean FALSE, the pfUpdateLeadModDate
                                argument is overridden, suppressing update of
                                the LastModifiedDate value in the associated
                                Lead record.
            --------------------------------------------------------------------
        */

        const strMethodName                  = LLCommon.GetNameOfCurrentFunction ( );

        console.log ( strMethodName + ' Arguments: pstrElementId = ' + pstrElementId + ', pstrCustomFieldName = ' + pstrCustomFieldName + ', pfCallRulesEngine = ' + ( pfCallRulesEngine ? 'TRUE' : 'FALSE' ) + ', pfUpdateLeadModDate = ' + ( pfUpdateLeadModDate ? 'TRUE' : 'FALSE' ) );

        try
        {
            if ( LLCommon.IsString ( pstrElementId ) )
            {
                if ( this.CheckCurrentValueAgainstInitialValue ( pstrElementId.substring ( SUBSTRING_SECOND_CHARACTER ) ) )
                {
                    const docElement         = pstrElementId.startsWith ( JQUERY_SELECTOR_IS_ELEMENT_ID )
                                               ? document.getElementById ( pstrElementId.substring ( SUBSTRING_SECOND_CHARACTER ) )
                                               : this.GetElementByName ( pstrElementId );
                    const strCustomFieldName = LLCommon.IsString ( pstrCustomFieldName )
                                               ? pstrCustomFieldName
                                               : pstrElementId.startsWith ( JQUERY_SELECTOR_IS_ELEMENT_ID )
                                                    ? pstrElementId.substring ( SUBSTRING_SECOND_CHARACTER )
                                                    : pstrElementId;
                    const fUpdateLeadModDate = ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'UpdateLeadModDate' ) === 'false' ) )
                                               ? false
                                               : pfUpdateLeadModDate === undefined
                                                 ? true
                                                 : pfUpdateLeadModDate;
                    const strNewFieldValue   = this.GetValueFromInputControl ( docElement );

                    const strOutcome         = LLCommon.DoAjax ( 'UpdateFormFieldByInternalName',
                                                                 'GET' ,
                                                                 {
                                                                      'leadId'          : this.STTLeadId ,
                                                                      'customFieldName' : strCustomFieldName ,
                                                                      'newFieldValue'   : strNewFieldValue ,
                                                                      'domainId'        : this.STTDomainId ,
                                                                      'tenantId'        : this.STTTenantId ,
                                                                      'userId'          : this.STTUserId ,
                                                                      'tzOffsetMinutes' : this.UtcOffsetMinutes ,
                                                                      'UpdateModDate'   : fUpdateLeadModDate
                                                                 } );

                    if ( strOutcome.length > EMPTY_STRING_LENGTH )
                    {
                        throw new Error ( 'ERROR calling UpdateFormFieldByInternalName through DoAjax: '
                                          + 'LeadId = ' + this.STTLeadId
                                          + ', CustomFieldName = ' + strCustomFieldName
                                          + ', New Field Value = ' + strNewFieldValue
                                          + ', domainId = ' + this.STTDomainId
                                          + ', tenantId = ' + this.STTTenantId );
                    }   // if ( strOutcome.length > EMPTY_STRING_LENGTH )

                    //  --------------------------------------------------------
                    //  If execution gets this far, the value stored in the
                    //  field was updated.
                    //  --------------------------------------------------------

                    LLCommon._fFormIsDirty   = true;
                    console.log ( 'Inside method ' + strMethodName + ' for lead ID = ' + this.STTLeadId + ', the value of field ' + strCustomFieldName + ' is assigned a new value = ' + strNewFieldValue )

                    if ( ( ! Object.is ( pfCallRulesEngine , undefined ) ) || this.TestForSignalElementByIdAndType ( 'STT_FireRulesEngine_Flag' , 'checkbox' ) )
                    {   // The flag field affords direct fine-grained control over firiing the rules.
                        const strOutcome     = LLCommon.DoAjax ( 'RulesForLeadId',
                                                                 'GET',
                                                                 {
                                                                      'Id'       : Object.is ( this.STTLeadId , undefined ) ? GetParameterFromURLFormOrLocalStorage ( 'leadId' ) : this.STTLeadId.toString ( ) ,
                                                                      'DomainId' : this.STTDomainId ,
                                                                      'TenantId' : this.STTTenantId
                                                                 } );

                        if ( strOutcome.length === EMPTY_STRING_LENGTH )
                        {
                            return EMPTY_STRING;
                        }   // TRUE (anticipated outcome) block, if ( strOutcome.length === EMPTY_STRING_LENGTH )
                        else
                        {
                            throw new Error ( 'ERROR calling RulesForLeadId through DoAjax: '
                                              + 'LeadId = ' + this.STTLeadId
                                              + ', CustomFieldName = ' + strCustomFieldName
                                              + ', New Field Value = ' + strNewFieldValue
                                              + ', domainId = ' + this.STTDomainId
                                              + ', tenantId = ' + this.STTTenantId );
                        }   // FALSE (unanticipated outcome) block, if ( strOutcome.length === EMPTY_STRING_LENGTH )
                    }   // if ( ( ! Object.is ( pfCallRulesEngine , undefined ) ) || this.TestForSignalElementByIdAndType ( 'STT_FireRulesEngine_Flag' , 'checkbox' ) )
                }   // TRUE (The value stored in the shadow element differs.) block, if ( this.CheckCurrentValueAgainstInitialValue ( pstrElementId.substring ( this.SUBSTRING_SECOND_CHARACTER ) ) )
                else
                {
                    return EMPTY_STRING;
                }   // FALSE (The values of the input element and its shadow element agree.) block, if ( this.CheckCurrentValueAgainstInitialValue ( pstrElementId.substring ( this.SUBSTRING_SECOND_CHARACTER ) ) )
            }   // if ( LLCommon.IsString ( pstrElementId ) )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
            const strMsg = 'ERROR in ' + strMethodName + ': ' + ex.message;
            console.log ( strMsg );
            return strMsg;
        }
    }   // UpdateFormFieldById method


    UTCMidnightToday ( )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        UTCMidnightToday

            Method Goal:        Compute a JavaScript Date value that is equal to
                                midnight UTC of the current calendar date.

                                DEPRECATED: Moment.js offers a more robust way.

            Input:              None

            Output:             The return value is a string representation of
                                the first ten (10) characters of the string
                                representation of a Date object that represents
                                Midnight UTC of the current date per the host's
                                system clock.

            Remarks:            The date is returned as a string to suppress the
                                extra baggage that is appended when the native
                                object is cast to a string.

                                Date.Prototype.getDate ( ) is badly named, since
                                its return value is the day of the month, while
                                getDay returns the weekday (day of the week).
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        const dtmNow        = new Date ( );
        const dtmTodayUtc   = new Date ( dtmNow.getFullYear ( ),
                                         dtmNow.getMonth ( ),
                                         dtmNow.getDate ( ),
                                         0,
                                         0,
                                         0 );
       return   dtmTodayUtc.getFullYear ( )
              + DEFAULT_DATE_SEPARATOR_CHAR
              + this.ApplyDatePartFixups ( dtmTodayUtc.getMonth ( ) )
              + DEFAULT_DATE_SEPARATOR_CHAR
              + dtmTodayUtc.getDate ( );
    }   // UTCMidnightToday method


    ValidateFormFields ( pstrContainerName )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ValidateFormFields

            Method Goal:        Evaluate the INPUT elements in the container
                                element that is identified by element ID
                                pstrContainerName, returning a list of the
                                elements that have invalid values.

            Input:              pstrContainerName  = String representation of the
                                                     element ID of a HTML element,
                                                     probably a DIV or a FORM,
                                                     that contains the INPUT
                                                     elements to validate

            Output:             If all elements are valid, the return value is
                                an empty array of InvalidElement objects.
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        var   raobjInvalidFields        = [ ];
        var   adocAllFields;

        if ( LLCommon.IsString ( pstrContainerName ) )
        {   // Confine the search to the INPUT elements in the container identified by pstrContainerName.
            adocAllFields               = $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( pstrContainerName ) ).find ( ':input' );
        }   // TRUE (Argument pstrContainerName is a value of type string.) block, if ( LLCommon.IsString ( pstrContainerName ) )
        else
        {   // Fall back to searching the whole document.
            adocAllFields               = $ ( ':input' );
        }   // FALSE (Argument pstrContainerName is a value of some other type, possibly NULL or even UNDEFINED.) block, if ( LLCommon.IsString ( pstrContainerName ) )

        const intInputControlsCount     = adocAllFields.length;

        if ( intInputControlsCount > ARRAY_IS_EMPTY )
        {
            for ( var intJ = ARRAY_FIRST_ELEMENT;
                      intJ < intInputControlsCount;
                      intJ++ )
            {
                try
                {
                    var strElementIdLC = adocAllFields [ intJ ].id.toLowerCase ( );

                    if ( strElementIdLC.indexOf ( 'email' ) > INDEXOF_NOT_FOUND && ( adocAllFields [ intJ ].type === 'email' || adocAllFields [ intJ ].type === 'text') && adocAllFields [ intJ ].value.length > EMPTY_STRING_LENGTH )
                    {
                        var strResult = LLCommon.DoAjax ( 'IsEmailAddressValid',
                                                          'GET',
                                                          {
                                                             'EmailAddress' : adocAllFields [ intJ ].value
                                                          } );

                        if ( strResult === 'False' )
                        {
                            raobjInvalidFields.push ( {
                                ControlId       : adocAllFields [ intJ ].id ,
                                ControlValue    : this.GetInputControlValue ( adocAllFields [ intJ ] ) ,
                                ReasonMessageId : this.VALIDATION_ERROR_INVALID_EMAIL } );
                        }   // if ( strResult === 'False' )
                    }   // if ( strElementIdLC.indexOf ( 'email' ) > INDEXOF_NOT_FOUND && ( adocAllFields [ intJ ].type === 'email' || adocAllFields [ intJ ].type === 'text') && adocAllFields [ intJ ].value.length > EMPTY_STRING_LENGTH )

                    if ( !this.RequiredFieldHasValue ( adocAllFields [ intJ ] ) )
                    {   // Process fields marked as required.
                        raobjInvalidFields.push ( {
                            ControlId       : adocAllFields [ intJ ].id ,
                            ControlValue    : this.GetInputControlValue ( adocAllFields [ intJ ] ) ,
                            ReasonMessageId : this.VALIDATION_ERROR_REQUIRED } );
                    }   // if ( !this.RequiredFieldHasValue ( adocAllFields [ intJ ] ) )

                    if ( this.HasDropDownOverlay ( adocAllFields [ intJ ] ) )
                    {   // Process pick lists.
                        if ( this.GetInputControlValue ( adocAllFields [ intJ ] ).length > EMPTY_STRING_LENGTH )
                        {   // Skip blank pick list fields, which would otherwise be marked as invalid.
                            if ( !this.IsPickListValueValid ( adocAllFields [ intJ ] ) )
                            {
                                raobjInvalidFields.push ( {
                                    ControlId       : adocAllFields [ intJ ].id ,
                                    ControlValue    : this.GetInputControlValue ( adocAllFields [ intJ ] ),
                                    ReasonMessageId : this.VALIDATION_ERROR_NOT_IN_PICK_LIST } );
                            }   // if ( !this.IsPickListValueValid ( adocAllFields [ intJ ) )
                        }   // if ( this.GetInputControlValue ( adocAllFields [ intJ ] ).length > EMPTY_STRING_LENGTH )
                    }   // if ( this.HasDropDownOverlay ( adocAllFields [ intJ ] ) )
                }
                catch ( ex )
                {
                    LLCommon.LogException ( ex );
                }
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intInputControlsCount; intJ++ )
        }   // if ( intInputControlsCount > ARRAY_IS_EMPTY )

        return raobjInvalidFields;
    }   // ValidateFormFields


    ValidateOneFormField ( pdocFormField )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        ValidateOneFormField

            Method Goal:        Validate a single form field following a user
                                edit.

            Input:              pdocFormField = Reference to the element to
                                                re-validate

            Output:             The return value is TRUE when the field value is
                                valid. Otherise, the return value is FALSE.

            Remarks:            This function needs global scope so that it can
                                be assigned as an event listener.

                                Unlike method VisibleRequiredFieldsHaveValues on
                                a LeadLifeJSHelpers object, this method confines
                                itself to evaluating a single element.

            See Also:           LeadLifeJSHelper.VisibleRequiredFieldsHaveValues
            --------------------------------------------------------------------
        */

        const strFunctionName = LLCommon.GetNameOfCurrentFunction ( );

        try
        {
            if ( pdocFormField !== undefined && pdocFormField !== null && pdocFormField.nodeName === 'INPUT' && pdocFormField.type === 'text' )
            {
                if ( _LeadLifeJSHelpers.RequiredFieldHasValue ( pdocFormField ) )
                {
                    if ( this.IsPickListValueValid ( pdocFormField ) )
                    {
                        AddOrRemoveCssSelector ( pdocFormField ,
                                                 'STT_Field_with_Error' ,
                                                 CSS_SELECTOR_REMOVE );
                        pdocFormField.title = EMPTY_STRING;
                    }   // TRUE (anticipated outcome) block,  if ( _LeadLifeJSHelpers.IsPickListValueValid ( pdocFormField ) )
                    else
                    {
                        alert ( pdocFormField.currentTarget.id + ' value of ' + pdocFormField.currentTarget.value + ' MUST exactly match a value in the pick list.' );
                    }   // FALSE (unanticipated outcome) block,  if ( _LeadLifeJSHelpers.IsPickListValueValid ( pdocFormField ) )
                }   // TRUE (anticipated outcome) block, if ( _LeadLifeJSHelpers.RequiredFieldHasValue ( pdocFormField ) )
                else
                {
                    alert ( pdocFormField.currentTarget.id + ' MUST have a value.' );
                }   // FALSE (unanticipated outcome) block, if ( _LeadLifeJSHelpers.RequiredFieldHasValue ( pdocFormField ) )
            }   // TRUE (anticipated outcome) block, if ( pdocFormField.target )
            else
            {
                throw new Error ( 'Function ' + strFunctionName + ' expected an INPUT object of type text.' );
            }   // FALSE (unanticipated outcome) block, if ( pdocFormField.target )
        }
        catch ( ex )
        {
            LLCommon.LogException ( strFunctionName + ': Run-time exception' , ex );
        }
    }   // ValidateOneFormField


    VisibleRequiredFieldsHaveValues ( pdocContainingForm )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        VisibleRequiredFieldsHaveValues

            Method Goal:        Alert the user when one or more required fields
                                has no value.

            Input:              pdocContainingForm  = Reference to the element
                                                      that represents the form
                                                      that was submitted

            Output:             The return value is TRUE when all visible fields
                                marked as required have values.

            Remarks:            This method accumulates a list of required field
                                names and displays them in a formatted message
                                box.

            Reference:          jQuery: Find all the visible required fields
                                https://stackoverflow.com/questions/18659726/jquery-find-all-the-visible-required-fields#:~:text=If%20you%20want%20to%20find%20input%2C%20textarea%2Cor%20select,%5Brequired%5D%3Avisible%27%29%20or%20%24%20%28%27%3Ainput%20%5Brequired%5D%3Avisible%27%29%2F%2Fmight%20be%20little%20costlier

            See Also:           Sibling method ValidateOneFormField
            --------------------------------------------------------------------
        */

        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        const adocRequiredInputs    = $('input,textarea,select').filter('[required]:visible');
        const intNRequiredInputs    = adocRequiredInputs.length;

        if ( intNRequiredInputs > ARRAY_IS_EMPTY )
        {
            const strRelevantFormName = this.GetIdOrName ( pdocContainingForm );
            var   strMessage;

            for ( var intJ = ARRAY_FIRST_ELEMENT;
                      intJ < intNRequiredInputs;
                      intJ++ )
            {
                if ( adocRequiredInputs [ intJ ].form.id === strRelevantFormName || adocRequiredInputs [ intJ ].form.name === strRelevantFormName )
                {
                    if ( this.GetInputControlValue ( adocRequiredInputs [ intJ ] ).length === EMPTY_STRING_LENGTH )
                    {
                        if ( Object.is ( strMessage , undefined ) )
                        {
                            strMessage = 'The following REQUIRED fields have missing values: ' + this.GetLabelForInputElement ( adocRequiredInputs [ intJ ] );
                        }   // TRUE (The first required field that is missing a value has been identified.) block, if ( Object.is ( strMessage, undefined ) )
                        else
                        {
                            strMessage += ( ', ' + this.GetLabelForInputElement ( adocRequiredInputs [ intJ ] ) );
                        }   // FALSE (At least one other required field that is missing a value has been identified.) block, if ( Object.is ( strMessage, undefined ) )
                    }   // if ( this.GetInputControlValue ( adocRequiredInputs [ intJ ] ).length === EMPTY_STRING_LENGTH )
                }   // if ( adocRequiredInputs [ intJ ].form.id === strRelevantFormName || adocRequiredInputs [ intJ ].form.name === strRelevantFormName )
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intNRequiredInputs; intJ++ )

            if ( Object.is ( strMessage , undefined ) )
            {
                return true;
            }   // TRUE (anticipated outcome - All required fields have values.) block, if ( Object.is ( strMessage, undefined ) )
            else
            {
                alert ( strMessage + FULL_STOP );
                return false;
            }   // FALSE (unanticipated outcome - One or more required fields are missing their values.) block, if ( Object.is ( strMessage, undefined ) )
        }   // TRUE (The form contains at least one required field.) block, if ( intNRequiredInputs > ARRAY_IS_EMPTY )
        else
        {
            return true;
        }   // FALSE (None of the form fields is marked as required.) block, if ( intNRequiredInputs > ARRAY_IS_EMPTY )
    }   // VisibleRequiredFieldsHaveValues
}   // class LeadLifeJSHelpers


function PostFormData ( poFormSubmitEvent )
{
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    if ( _fDebugLogging )
    {
        alert ( 'Debugging global PostFormData function' );
    }   // if ( _fDebugLogging )


    debugger;
                                                                                                                                                                                                                                    $('td[name="tcol1"]')
    const strElementID  = ( !Object.is ( poFormSubmitEvent.currentTarget.id, undefined ) && poFormSubmitEvent.currentTarget.id.length > EMPTY_STRING_LENGTH )
                          ? ( JQUERY_SELECTOR_IS_ELEMENT_ID + poFormSubmitEvent.currentTarget.id )
                          : LLCommon.JquerySelectorByTagNameAndAttributeValue ( 'input' ,
                                                                                'name' ,
                                                                                poFormSubmitEvent.currentTarget.name );
    _LeadLifeJSHelpers.SubmitForm ( strElementID ,
                                    _fCallRulesEngineOnSubmit );
    return false;               // An event returning false implies preventDefault.
}   // function PostFormData


function lockAndLoad ( )
{
    function HideFirstAndLastNamesInForm ( pstrCustomFormContainerText )
    {
        const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

        const ELEMENT_ID_FIRST_NAME = 'id="firstName"';
        const ELEMENT_ID_LAST_NAME  = 'id="lastName"';
        const TABLE_PREFIX_TOKEN    = '<table>';
        const TOKEN_PREFIX          =   '<tr>'
                                      + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                      + '<td class="STT_HideElement" colspan="2">';
        const TOKEN_FIRST_NAME      = '<input datatype="String" type="text" ' + ELEMENT_ID_FIRST_NAME + ' class="STT_HideElement">';
        const TOKEN_LAST_NAME       = '<input datatype="String" type="text" ' + ELEMENT_ID_LAST_NAME  + ' class="STT_HideElement">';
        const TOKEN_SUFFIX          =   '</td>'
                                      + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                      + '</tr>'

        var   strFirstNameToken     = EMPTY_STRING;
        var   strLastNameToken      = EMPTY_STRING;

        if ( pstrCustomFormContainerText.indexOf ( ELEMENT_ID_FIRST_NAME ) === INDEXOF_NOT_FOUND )
        {
            strFirstNameToken       = TOKEN_FIRST_NAME;
        }   // if ( pstrCustomFormContainerText.indexOf ( ELEMENT_ID_FIRST_NAME ) === INDEXOF_NOT_FOUND )

        if ( pstrCustomFormContainerText.indexOf ( ELEMENT_ID_FIRST_NAME ) === INDEXOF_NOT_FOUND )
        {
            strLastNameToken       = TOKEN_LAST_NAME;
        }   // if ( pstrCustomFormContainerText.indexOf ( ELEMENT_ID_FIRST_NAME ) === INDEXOF_NOT_FOUND )

        if ( strFirstNameToken.length > EMPTY_STRING_LENGTH || strLastNameToken.length > EMPTY_STRING_LENGTH )
        {
            return pstrCustomFormContainerText.replace ( TABLE_PREFIX_TOKEN ,
                                                           TABLE_PREFIX_TOKEN
                                                         + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                         + TOKEN_PREFIX
                                                         + strFirstNameToken
                                                         + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                         + strLastNameToken
                                                         + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                         + TOKEN_SUFFIX );
        }   // TRUE (One or both fields are missing.) block, if ( strFirstNameToken.length > EMPTY_STRING_LENGTH || strLastNameToken.length > EMPTY_STRING_LENGTH )
        else
        {
            return pstrCustomFormContainerText;
        }   // FALSE (Both fields are present.) block, if ( strFirstNameToken.length > EMPTY_STRING_LENGTH || strLastNameToken.length > EMPTY_STRING_LENGTH )
    }   // private function HideFirstAndLastNamesInForm


    const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

    //  --------------------------------------------------------------------
    //  Though C# can store the value of a conditional expression in a
    //  variable, it appears that JavaScript cannot.
    //
    //  See https://stackoverflow.com/questions/10270351/how-to-write-an-inline-if-statement-in-javascript
    //
    //  As well, these two statements cannot execute until the strings that they evaluate are initialized
    //  by code in the block just above this comment.
    //  -------------------------------------------------------------------------------------------------

    if ( _fDebugLogging )
    {
        LLCommon.Trace ( 'Page fully loaded at ' + Date ( ) );
        LLCommon.Trace ( '    SalesTalk Database Name    = ' + _LeadLifeJSHelpers.STTDatabaseName );
        LLCommon.Trace ( '    SalesTalk Domain Name      = ' + _LeadLifeJSHelpers.STTDomainName );
    }   // if ( _fDebugLogging )

    LLCommon.GetUrlVarsFromSession ( true );

    const oSubmitButtons = _LeadLifeJSHelpers.GetElementByName ( 'post*' );

    if ( oSubmitButtons.length >= ARRAY_NOT_EMPTY )
    {
        for ( var intCurrentSubmitButton = _LeadLifeJSHelpers.ARRAY_FIRST_ELEMENT,
                  intTotalSubmitButtons  = oSubmitButtons.length;
                  intCurrentSubmitButton < intTotalSubmitButtons;
                  intCurrentSubmitButton++ )
        {
            try
            {
                oSubmitButtons [ intCurrentSubmitButton ].addEventListener ( 'click'   , PostFormData );
                oSubmitButtons [ intCurrentSubmitButton ].addEventListener ( 'keydown' , PostFormData );
            }
            catch ( ex )
            {
                LLCommon.LogException ( strMethodName + ': Attempting to register event handlers for forms. See exception log for details.' , ex );
            }
        }   // for ( var intCurrentSubmitButton = _LeadLifeJSHelpers.ARRAY_FIRST_ELEMENT, intTotalSubmitButtons = oSubmitButtons.length; intCurrentSubmitButton < intTotalSubmitButtons; intCurrentSubmitButton++ )
    }   // if ( oSubmitButtons.length >= ARRAY_NOT_EMPTY )

    var fMyViewIsEmpty                  = true;

    //  ------------------------------------------------------------------------
    //  If the current document contains an element named STTCustomFormContainer
    //  and a lead has been identified, ask the server, via GetMyViewScreenHTML,
    //  for the HTML, which will be a populated table element, to insert into
    //  the innerHTML of the STTCustomFormContainer element.
    //
    //  Since exceptions, should they arise, will do so before anything has been
    //  written into the output stream, cause the returned string to be empty,
    //  the probablility of an error message being displayed is nil.
    //  ------------------------------------------------------------------------

    try
    {
        const docSTTCustomFormContainer = document.getElementById ( 'STTCustomFormContainer' );

        if ( docSTTCustomFormContainer !== null && ( !Object.is ( _LeadLifeJSHelpers.STTLeadId , undefined ) && Number.isInteger ( _LeadLifeJSHelpers.STTLeadId ) && _LeadLifeJSHelpers.STTLeadId >= _LeadLifeJSHelpers.STT_MINIMUM_ID_VALUE && _pagename !== null ) )
        {
            if ( docSTTCustomFormContainer.innerHTML.length === EMPTY_STRING_LENGTH )
            {
                docSTTCustomFormContainer.innerHTML = HideFirstAndLastNamesInForm ( LLCommon.DoAjax ( 'GetMyViewScreenHTML',
                                                                                                      'GET',
                                                                                                      {
                                                                                                         'LeadId'   : _LeadLifeJSHelpers.STTLeadId ,
                                                                                                         'TenantId' : _LeadLifeJSHelpers.STTTenantId ,
                                                                                                         'DomainId' : _LeadLifeJSHelpers.STTDomainId ,
                                                                                                         'UserId'   : _LeadLifeJSHelpers.STTUserId ,
                                                                                                         'PageName' : _pagename
                                                                                                      } ) );

                //  ------------------------------------------------------------
                //  The element ID that is passed into HandleFormPrefill must be
                //  that of a parent of STTCustomFormContainer that is enough
                //  levels higher in the DOM tree that it includes the elements
                //  that contain the ExternalCRMID, SysCRMLeadOrContact, and the
                //  LeadId, so that they get populated along with the INPUT
                //  controls on the form. However, to exclude unrelated inputs,
                //  it should avoid going higher than necessary.
                //  ------------------------------------------------------------

                if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                {
                    console.log ( strMethodName + ': Skipping HandleFormPrefill becaue W2A_WriteOnly is TRUE.')
                }   // TRUE (The form is write-only.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                else
                {
                    console.log ( strMethodName + ': Calling HandleFormPrefill to populate the form.')
                    _LeadLifeJSHelpers.HandleFormPrefill ( 'docSearchResultsGrid' ,
                                                           _LeadLifeJSHelpers.STTLeadId );
                }   // FALSE (The form is a normal read/write form). block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
            }   // TRUE (The MyView form is unpopulated.) block, if ( docSTTCustomFormContainer.innerHTML.length === EMPTY_STRING_LENGTH )
        }   //if ( docSTTCustomFormContainer !== null && ( !Object.is ( _LeadLifeJSHelpers.STTLeadId , undefined ) && Number.isInteger ( _LeadLifeJSHelpers.STTLeadId ) && _LeadLifeJSHelpers.STTLeadId >= _LeadLifeJSHelpers.STT_MINIMUM_ID_VALUE && _pagename !== null ) )

        return docSTTCustomFormContainer;
    }
    catch ( ex )
    {
        LLCommon.LogException ( strMethodName + ': Attempting to inject MyView template into page. See exception log for details.' , ex );
        return null;
    }
}   // function lockAndLoad


//  ----------------------------------------------------------------------------
//  Wrapping function lockAndLoad in a function that answers a DOMContentLoaded
//  aevnt to be delayed until the document and all scripts are loaded, whether
//  they load syncrhonously, asynchronously, or deferred.
//  ----------------------------------------------------------------------------

console.log ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                 LeadLifeJSHelpers_Version ,
                                 LeadLifeJSHelpers_LastUpdated ,
                                 'Adding DOMContentLoaded event listener defined in _LeadLifeJSHelpers' ) );

window.addEventListener ( 'DOMContentLoaded', function ( )
{
    console.log ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                     LeadLifeJSHelpers_Version ,
                                     LeadLifeJSHelpers_LastUpdated ,
                                     'The anonymoun DOMContentLoaded function defined in LeadLifeJSHelpersLib.js is about to create a new LeadLifeJSHelpers object.' ) );
    _LeadLifeJSHelpers = Object.is ( _LeadLifeJSHelpers , undefined ) ? new LeadLifeJSHelpers ( _fDebugLogging ) : _LeadLifeJSHelpers;

    //  ------------------------------------------------------------------------
    //  In the beginning, the LeadLifeJSHelpers constructor could be allowed to
    //  run asynchronously, before the document was ready. Since it now relies
    //  upon code in LLCommon.js that is deferred until the document is ready,
    //  executing the constructor must also be deferred. So long as LLCommon.js
    //  is included first and both are deferred until the entire document has
    //  been read from the input stream, the DOMContentLoaded event listeners
    //  will run in the correct order.
    //  ------------------------------------------------------------------------

    LLCommon.Trace ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                        LeadLifeJSHelpers_Version ,
                                        LeadLifeJSHelpers_LastUpdated ,
                                        'The anonymoun DOMContentLoaded function defined in LeadLifeJSHelpersLib.js finished creating and initializing a new LeadLifeJSHelpers object.' ) );

    LLCommon.Trace ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                        LeadLifeJSHelpers_Version ,
                                        LeadLifeJSHelpers_LastUpdated ,
                                        'lockAndLoad function defined in anonymoun DOMContentLoaded function LeadLifeJSHelpersLib.js is being executed by the anonymouns function behind a DOMContentLoaded event.' ) );
    debugger;
    lockAndLoad ( );
    LLCommon.Trace ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                        LeadLifeJSHelpers_Version ,
                                        LeadLifeJSHelpers_LastUpdated ,
                                        'lockAndLoad function returned control to the DOMContentLoaded event procedure that invoked it.' ) );
});

console.log ( ScriptInfoForLog ( LeadLifeJSHelpers_SCRIPTSOURCE ,
                                 LeadLifeJSHelpers_Version ,
                                 LeadLifeJSHelpers_LastUpdated ,
                                 'loaded' ) );
