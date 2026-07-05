/*eslint-env browser*/
/*global _leadid _leadidSource ARRAY_FIRST_ELEMENT ARRAY_NEXT_ELEMENT ARRAY_SECOND_ELEMENT CHARACTER_ZERO EMPTY_STRING EMPTY_STRING_LENGTH ERR_MESSAGE_STANDARD_PREFIX HYPHEN_CHAR JQUERY_SELECTOR_IS_ELEMENT_ID KEY_IS_LEAD_ID KEY_IS_EXTERNALCRMID KEY_IS_EXTERNALCRMTYPE LLCommon LOGICAL_NEGATE NO_LEAD_ID NULL_AS_STRING_VALUE NUMERIC_PLUS_ONE NUMERIC_ZERO ScriptInfoForLog SRC_IS_UNKNOWN SRC_IS_QUERY_STRING SRC_IS_FORM_FIELD SRC_IS_LOCAL_STORAGE SRC_IS_EXTERNALCRMID SRC_IS_SESSION_STORAGE*/

const LeadLifeJSHelpersGlobals_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const LeadLifeJSHelpersGlobals_VERSION      = 1.036;
const LeadLifeJSHelpersGlobals_LastUpdated  = '2025/05/11 06:55:42 CDT';

/*
    ============================================================================

    Name:               LeadLifeJSHelpersGlobals.js

    Goal:               Declare global JavaScript objects required by Landing
                        Pages that use LeadLifeJSHelpersLib.js.

    Dependencies:       By the time a couple of these functions execute, there
                        must be a working _LeadLifeJSHelpers or LLCommon object.

    Remarks:            This script is intnded to be loaded ahead of the script
                        that implements the LeadLifeJSHelpers class,
                        LeadLifeJSHelpersLib.js.

                        The following table sets forth the names and initial values of the global variables, followed by the names of the
                        globally visible routines to query, sat, and clear their values. All setters return the previous value of the flag.

                        These convenience functions allow code running in strict mode to interact with these global Boolean flags.

    -------------------------------------------------------------------------------------------------------------------------------------------
    Variable Name                 Default  Global Routine to Disable          Global Routine to Enable          Global Routine to Query Value
    ----------------------------  -------  -------------------------------    --------------------------------  -------------------------------
    _fDebugLogging                false    DisableDebugLogging                EnableDebugLogging                QueryDebugLogging
    _fCallRulesEngineOnSubmit     false    DisableRulesEngineOnSubmit         EnableRulesEngineOnSubmit         QueryCallRulesEngineOnSubmit
    _fSkipAsyncEventRegistration  false    DisableSkipAsyncEventRegistration  EnableSkipAsyncEventRegistration  QuerySkipAsyncEventRegistration
    -------------------------------------------------------------------------------------------------------------------------------------------

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By Remark/Brief Description
    ---------- ------- -- ------------------------------------------------------
    2022/10/03 0.999   DG Initial Implementation

    2022/10/14 1.000   DG Add function URLParameter to replace GetUrlParameter.

    2022/10/18 1.001   DG Define two new Boolean flags:

                          1) _fCallRulesEngineOnSubmit    Cause the Rules Engine
                                                          to be called after the
                                                          form submit occurs.

                                                          true      = call async
                                                          false     = call sync
                                                          Undefined = don't call
                                                          null      = don't call

                          2) _fSkipAsyncEventRegistration Skip the aysnchronous
                                                          routine that registers
                                                          event handlers for
                                                          form post buttons.

                                                          true      = skip it
                                                          false     = call it
                                                          Undefined = call it
                                                          null      = call it

    2022/10/20 1.002   DG Add accessor routines, all bound to the Window object,
                          with the intent of making the flags visible regardless
                          of execution context.

    2022/10/27 1.003   DG Declare _fCallRulesEngineOnSubmit, leaving it
                          uninitialized, so that code that references it sees
                          its value as undefined by default.

    2023/02/17 1.004   DG Set _fConfigure4DeferredLoading to FALSE unless the
                          URL is in a list of exceptions.

    2023/02/17 1.005   DG 1) Ditch global variable _fConfigure4DeferredLoading.
                             This eliminates the need for the three routines
                             that set, clear, and query it.

                          2) Define LeadLifeJSHelpersGlobals_SCRIPTSOURCE.

                          3) List the script source and version, along with the
                             initial values of the three global flags that are
                             defined herein.

    2023/05/12 1.006   DG When document.currentScript === null, set SCRIPTSOURCE
                          to 'unknown' so that it doesn't throw and prevent the
                          remainder of the library from being parsed and its
                          global functions implemented.

    2023/06/13 1.007   DG 1) Move stand-alone function GetNameOfCurrentFunction
                             from LeadLifeJSHelpersLib.js into this library
                             because said function is completely freestanding
                             and much more broadly useful.

                          2) Remove the global function mappings, which I found
                             are redundant because the functions are global in
                             the first place because of where they are defined.

                          3) Rename the global Boolean flag function sets to
                             match the table in the flower box, which showed the
                             redundant global function binding names.

    2023/06/27 1.008   DG Implement a new function, ShowOrHideElement.

    2023/07/12 1.009   DG 1) Implement a new function, GetLeadOrCrmIdFromUrl.

                          2) URLParameterFromQueryString matches become case
                             insensitive.

                          3) Move StringStartsWith, StringEndsWith, and
                             StringSplitSharp from LeadLifeJSHelpersLib.js.

    2023/07/22 1.010   DG 1) Move the following routines from LeadLifeJSHelpersLib:

                             a) GetLeadIdFromQueryString
                             b) GetParameterFromURLFormOrLocalStorage,
                                WAS GetUrlParameter
                             c) QueryLocalStorage
                             d) QueryPageFields

                          2) GetLeadOrCrmIdFromUrl geta a new pfLookEverywhere
                             argument that causes it to look beyond the query
                             string.

                          3) Make GetParameterFromURLFormOrLocalStorage case
                             insensitive, so that its behavior mirrors that of
                             URLParameterFromQueryString.

                          4) Store the lead id and its source in a pair of new
                             global variables, _leadid and _leadidSource.

    2023/07/22 1.011   DG 1) Change function GetLeadOrCrmIdFromUrl to take the
                             domain ID and lead Id into account in its call to
                             method GetVeryBasicLeadInfo4ExternamCRMId on the
                             Open controller.

                          2) Create like-named backing stores for variables
                             _leadid and _leadidSource.

    2023/07/30 1.012   DG Change GetLeadIdFromQueryString to conform to the new
                          URL format implemented to be compatible with DeepGram.

    2023/07/30 1.014   DG QueryLocalStorage referenced this.IsString, which lint
                          seems to have missed.

    2023/08/20 1.015   DG GetLeadIdFromQueryString differed from URLParameter in
                          that the latter employed decodeURIComponent to reveree
                          the URL encoding of the query string. This change also
                          simplified the application of a character class to let
                          the expression match string in which the lead ID is
                          bounded by either spaces or underscores.

    2023/08/22 1.016   DG Clone QueryLocalStorage to create QuerySssionStorage,
                          which operates upon the sessionStorage object in the
                          same way that QueryLocalStorage operates upon the
                          localStorage object. Both routines append a leadId to
                          the sessionStorage object.

                          QuerySssionStorage then replaces QueryLocalStorage in
                          GetParameterFromURLFormOrLocalStorage.

    2023/08/22 1.017   DG Account for consolidating DoAjax and LogException into
                          LLCommon.js.

    2023/08/29 1.018   DG Implement AddOrRemoveCssSelector, a new utility
                          function that safely adds or removes a CSS selector
                          from an element.

    2023/09/02 1.019   DG Move constants ELEMENT_HIDE and ELEMENT_SHOW, symbolic
                          constants for use as values for the second argument,
                          pfShowIt, to function ShowOrHideElement, also defined
                          herein, so that all three symbols travel together, and
                          change the functions that set the global flags so that
                          they return the previous value, rather than undefined.
                          This fixes a coding oversight, because code to save
                          the current value was already present in all of them.

    2023/09/03 1.020   DG Declare _UpdateIfChanged for use as a reference to an
                          object method defined as an instance method on object
                          _LeadLifeJSHelpers.

    2023/09/05 1.021   DG Since function UpdateIfChanged had to be moved into
                          this script and given global scope, _UpdateIfChanged
                          is redundant.

    2023/09/06 1.022   DG In function UpdateIfChanged, simplify the expression
                          that evaluates to local variable docChangedElement so
                          that it uses the eventTarget reference directly when
                          it has one, saving getElementById for when it gets a
                          string.

    2023/09/09 1.023   DG Fix an unhandled null reference exception in function
                          ShowOrHideElement, and change its default return type
                          from undefined to null.

    2023/09/11 1.024   DG Define Boolean symbolic constants CSS_SELECTOR_ADD and
                          CSS_SELECTOR_REMOVE; use with AddOrRemoveCssSelector.

    2023/09/16 1.025   DG 1) Global constants JQUERY_SELECTOR_IS_ELEMENT_ID and
                             EMPTY_STRING_LENGTH, both defined in LLCommon.js,
                             supersede like-named _LeadLifeJSHelpers constants,
                             eliminating dupicate constants.

                          2) Move LeftPadInteger, IndexFromOrdinal, and
                             OrdinalFromIndex to LeadLifeJSHelpersGlobals.

    2023/09/22 1.026   DG 1) Function GetLeadIdFromQueryString and several
                             otheers unconditionally set _leadidSource before
                             evaluating their query result, causing it to have a
                             misleading value when there was no lead ID in play.

                          2) All but the most trivial functions now define local
                             constant strMethodName.

    2023/10/25 1.027   DG Define ValidateOneFormField as a global function so it
                          can be registered as an event listener.

    2023/09/05 1.028   DG Function UpdateIfChanged gets an optional Boolean flag
                          pfUpdateLeadModDate that defaults to TRUE and causes
                          the last modified date of the controlling Lead row to
                          be updated.

    2024/08/13 1.029   DG Deprecate StringStartsWith and StringEndsWith in favor
                          of String.prototype.startsWith and
                          String.prototype.endsWith.

                          The single local reference, to StringStartsWith, has
                          already been updated to use String.prototype.endsWith.

    2024/09/26 1.030   DG Add a missing space character to the error message
                          logged by function UpdateIfChanged, and change that
                          function to short circuit when presented with elements
                          such as the CRM Entity pick list.

    2025/03/18 1.031   DG Mark functions IndexFromOrdinal and OrdinalFromIndex
                          as deprecated.

    2025/04/02 1.032   DG Adjust the date stamp to force recomputation of the
                          Subresource Integrity digest string.

    2025/04/20 1.033   DG Move function StringSplitSharp to LLCommon.js.

    2025/04/28 1.034   DG Move ValidateOneFormField to LeadLifeJSHelpersLib.

    2025/05/01 1.035   DG Change the way function UpdateIfChanged behaves when
                          its pfUpdateLeadModDate parameter is omitted so that
                          it defaults to FALSE when the entity type is WA-Task.

	2025/05/11 1.036   DG Correct a syntax error in the error reporting block of
	                      function UpdateIfChanged.
    ============================================================================
*/

    console.log ( ScriptInfoForLog ( LeadLifeJSHelpersGlobals_SCRIPTSOURCE ,
                                     LeadLifeJSHelpersGlobals_VERSION ,
                                     LeadLifeJSHelpersGlobals_LastUpdated ,
                                     'loading' ) );

    debugger;

    const ELEMENT_HIDE                   = false;
    const ELEMENT_SHOW                   = true;

    const CSS_SELECTOR_ADD               = true;
    const CSS_SELECTOR_REMOVE            = false;

    //  ------------------------------------------------------------------------
    //  The following variables support code defined in LeadLifeJSHelpersLib.js,
    //  which appears in Talking Points that contain forms.
    //  ------------------------------------------------------------------------

    var   _fCallRulesEngineOnSubmit;

    var   _fDebugLogging                = false;
    var   _fSkipAsyncEventRegistration  = false;

    const _astrUISelectElements2Ignore  = [
                                                'm4vurl',
                                                'm4vnoteid',
                                                'cbowords2actionslogin',
                                                'crmsearchableentities',
                                                'media',
                                                'nextaction',
                                                'nextaction_shadow'
                                          ];

    //  ------------------------------------------------------------------------
    //  Since the code that looks up the lead ID from basically everywhere lives
    //  here now, and we need to know from whence it came, a pair of global
    //  variables are set aside to hold the information.
    //
    //  The constants are the supported values for _leadidSource, which must be
    //  defined first so that _leadidSource can be appropriately initialized.
    //
    //  Since LLCommon uses these constants and loads first, its definitions
    //  control. For the same reason, _leadid and _leadidSource are moved there.
    //  ------------------------------------------------------------------------

    //  ------------------------------------------------------------------------
    //  Though a pointer for the LeadLifeJSHelpers object is set aside here, its
    //  initialization happens within a routine that is defined in the script
    //  that contains its class definition.
    //  ------------------------------------------------------------------------

    var   _LeadLifeJSHelpers;

    //  -------------------------------------------------------------------------
    //  Provide routines to query, set, and clear the global Boolean flag,
    //  _fDebugLogging.
    //  -------------------------------------------------------------------------

    function DisableDebugLogging ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      DisableDebugLogging

            Method Goal:        Turn the _fDebugLogging flag OFF, returning the
                                value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fDebugLogging flag as it stood on entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState = _fDebugLogging;
        _fDebugLogging       = false;

        return fPreviousState;
    }   // function DisableDebugLogging

    function EnableDebugLogging ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      EnableDebugLogging

            Method Goal:        Turn the _fDebugLogging flag ON, returning the
                                value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fDebugLogging flag as it stood on
                                entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState = _fDebugLogging;
        _fDebugLogging       = true;

        return fPreviousState;
    }   // function EnableDebugLogging

    function QueryDebugLogging ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QueryDebugLogging

            Method Goal:        Return the _fDebugLogging flag value.

            Input:              None

            Output:             The return value is the present value of Boolean
                                flag _fDebugLogging flag.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be read from any context.
            --------------------------------------------------------------------
        */

        return _fDebugLogging;
    }   // function QueryDebugLogging

    //  -------------------------------------------------------------------------
    //  Provide routines to query, set, and clear the global Boolean flag,
    //  _fCallRulesEngineOnSubmit.
    //  -------------------------------------------------------------------------

    function DisableRulesEngineOnSubmit ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      DisableRulesEngineOnSubmit

            Method Goal:        Turn the _fCallRulesEngineOnSubmit flag OFF,
                                returning the value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fCallRulesEngineOnSubmit flag as it stood on
                                entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState      = _fCallRulesEngineOnSubmit;
        _fCallRulesEngineOnSubmit = false;

        return fPreviousState;
    }   // function DisableRulesEngineOnSubmit

    function EnableRulesEngineOnSubmit ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      EnableRulesEngineOnSubmit

            Method Goal:        Turn the _fCallRulesEngineOnSubmit flag ON,
                                returning the value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fCallRulesEngineOnSubmit flag as it stood on
                                entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState      = _fCallRulesEngineOnSubmit;
        _fCallRulesEngineOnSubmit = true;

        return fPreviousState;
    }   // function EnableRulesEngineOnSubmit

    function QueryCallRulesEngineOnSubmit ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QueryCallRulesEngineOnSubmit

            Method Goal:        Return the _fCallRulesEngineOnSubmit flag value.

            Input:              None

            Output:             The return value is the present value of Boolean
                                flag _fCallRulesEngineOnSubmit flag.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be read from any context.
            --------------------------------------------------------------------
        */

        return _fCallRulesEngineOnSubmit;
    }   // function QueryCallRulesEngineOnSubmit

    //  -------------------------------------------------------------------------
    //  Provide routines to query, set, and clear the global Boolean flag,
    //  _fSkipAsyncEventRegistration.
    //  -------------------------------------------------------------------------

    function DisableSkipAsyncEventRegistration ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      DisableSkipAsyncEventRegistration

            Method Goal:        Turn the _fSkipAsyncEventRegistration flag OFF,
                                returning the value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fSkipAsyncEventRegistration flag as it stood on
                                entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState         = _fSkipAsyncEventRegistration;
        _fSkipAsyncEventRegistration = false;

        return fPreviousState;
    }   // function DisableSkipAsyncEventRegistration

    function EnableSkipAsyncEventRegistration ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      EnableSkipAsyncEventRegistration

            Method Goal:        Turn the _fSkipAsyncEventRegistration flag ON,
                                returning the value it had on entry.

            Input:              None

            Output:             The return value is the value of the Boolean
                                _fSkipAsyncEventRegistration flag as it stood on
                                entry.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be set from any context.
            --------------------------------------------------------------------
        */

        const fPreviousState         = _fSkipAsyncEventRegistration;
        _fSkipAsyncEventRegistration = true;

        return fPreviousState;
    }   // function EnableSkipAsyncEventRegistration

    function QuerySkipAsyncEventRegistration ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QuerySkipAsyncEventRegistration

            Method Goal:        Return the _fSkipAsyncEventRegistration flag value.

            Input:              None

            Output:             The return value is the present value of Boolean
                                flag _fSkipAsyncEventRegistration flag.

            Remarks:            Along with binding to the Window object, this
                                convenience function should guarantee that the
                                flag can be read from any context.
            --------------------------------------------------------------------
        */

        return _fSkipAsyncEventRegistration;
    }   // function QuerySkipAsyncEventRegistration

    //  -------------------------------------------------------------------------
    //  The remaining functions are free-standing general-purpose helpers.
    //  -------------------------------------------------------------------------

    function AddOrRemoveCssSelector ( pdocElement , pdocCssSelector , pfAddOrRemove )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      AddOrRemoveCssSelector

            Method Goal:        Depending upon whether Boolean pfAddOrRemove is
                                True (or truthy) or false (or falsy), add (when
                                True) or remove (when False) the CSS selector
                                identified by String pdocCssSelector from the
                                the HTML element identified by pdocElement.

            Input:              pdocElement     = This may be either a reference
                                                  to a DOM element or its ID as
                                                  a string.

                                pdocCssSelector = This string is the unqualified
                                                  name of the CSS selector to be
                                                  added or removed.

                                pfAddOrRemove   = This is either Boolean True or
                                                  a truthy value, to cause CSS
                                                  selector pdocCssSelector to be
                                                  added, or False, or a falsy
                                                  value, to cause selector
                                                  pdocCssSelector to be removed
                                                  from element pdocElement.

            Output:             The return value is the classList attribute of
                                the element as it stands when the function
                                returns. The classList attribute is a NodeList.

                                When pdocElement is either undefined or null,
                                the return value is null.

                                Unless pdocCssSelector is a String, the return
                                value is also null.

            Remarks:            This convenience function works by applying or
                                removing a specified CSS selector from the
                                specified page element.

                                This method of applying selectors transparently
                                and safely preserves other CSS selectors.

            Algorithm:          1) Treating pfAddOrRemove as a Boolean evaluate its
                                   truthiness.

                                2) If pfAddOrRemove evaluates to True, apply CSS
                                   selector pdocCssSelector to the element
                                   idenfified by the value of pdocElement.

                                3) If pfAddOrRemove evaluates to False, remove
                                   CSS selector pdocCssSelector from the element
                                   idenfified by the value of pdocElement.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        {   // When pdocElement is either undefined or null, there is nothing to do.
            if ( IsString  ( pdocCssSelector ) )
            {   // Unless pdocCssSelector is a String, all there is to do is return the classList as it stands.
                const docThisElement = IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

                if ( docThisElement !== null )
                {   // When docThisElement is null, there is nothing to do.
                    if ( pfAddOrRemove )
                    {
                        docThisElement.classList.add    ( pdocCssSelector );
                    }   // TRUE (The calling routine wants element pdocElement shown.) block, if ( pfAddOrRemove )
                    else
                    {
                        docThisElement.classList.remove ( pdocCssSelector );
                    }   // FALSE (The calling routine wants element pdocElement hidden.) block, if ( pfAddOrRemove )
                }   // if ( docThisElement !== null )

                return docThisElement.classList;
            }   // TRUE (anticipated outcome) block, if ( IsString  ( pdocCssSelector ) )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( IsString  ( pdocCssSelector ) )
        }   // TRUE (anticipated outcome) block, if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        else
        {
            return null;
        }   // FALSE (unanticipated outcome) block, if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
    }   // function AddOrRemoveCssSelector


    function GetLeadIdFromQueryString ( pfReturnAsString , pfGetUrlParameter )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetLeadIdFromQueryString

            Method Goal:        Extract the lead ID from the

            Input:              location.search   = This global property is
                                                    always parsed for a lead ID.

                                pfReturnAsString  = When present, this Boolean
                                                    flag causes the output to be
                                                    returned as a string.

                                pfGetUrlParameter = When present, this Boolean
                                                    flag prevents a recursive
                                                    URLParameterFromQueryString
                                                    function call.

            Output:             Digits embedded in query substring that starts
                                with "Contact " and ends with " --" converted to
                                integer by the inbuilt pareInt function, or zero
                                if no lead ID is found in the expected location

            Remarks:            This function covers the special case of a lead
                                ID embedded in the value of a larger key/value
                                pair in the Query String.

                                The regular expression matches a string of
                                decimal digits bounded by either space or
                                underscore characters. This regular expression
                                expects a string that has been passed through
                                decodeURIComponent, aligning the behavior of
                                this function with that of URLParameter.
            --------------------------------------------------------------------
        */

        const strMethodName          = LLCommon.GetNameOfCurrentFunction ( );

        var   regex                  = /\/Contact[ _](\d+)[ _]/;
        var   result                 = regex.exec ( decodeURIComponent ( location.search ) );

        if ( result === null )
        {
            regex                    = /\/Contact_(\d+)_/;
            result                   = regex.exec ( document.location.href );
        }   // if ( result === null )

        const strFinalResult         = result === null || pfGetUrlParameter ? URLParameterFromQueryString ( KEY_IS_LEAD_ID ) : result [ ARRAY_SECOND_ELEMENT ];

        if ( pfReturnAsString )
        {
            return strFinalResult;
        }   // TRUE (Implement the legacy behavior.) block, if ( pfReturnAsString )
        else
        {
            const intLeadIdCandidate = strFinalResult === null || strFinalResult === EMPTY_STRING || strFinalResult === NULL_AS_STRING_VALUE || ( ! Number.isInteger ( strFinalResult ) )
                                       ? NO_LEAD_ID
                                       : parseInt ( strFinalResult )

            if ( intLeadIdCandidate > NO_LEAD_ID )
            {
                _leadid              = intLeadIdCandidate;
                _leadidSource        = SRC_IS_QUERY_STRING;
                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( _leadid > NO_LEAD_ID )

            return intLeadIdCandidate;
        }   // FALSE (Implement the optional behavior.) block, if (  pfRetur07/20/2023 22:44:02nAsString === undefined )
    }   // function GetLeadIdFromQueryString


    function GetLeadOrCrmIdFromUrl ( pfLookEverywhere )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      GetLeadOrCrmIdFromUrl

            Function Goal:      Call this function to get the Lead ID either
                                directly from the query string, or indirectly by
                                looking up the ExternalCRMId that is presented
                                via the query string.

            Input:              pfLookEverywhere is a Boolean flag that, when
                                TRUE, causes it to call sibling function
                                GetParameterFromURLFormOrLocalStorage instead of
                                URLParameterFromQueryString in search of values
                                for ExternalCRMId and SysCRMLeadOrContact, and
                                to use it as well for LeadId if those two come
                                up empty.

            Output:             If a lead ID is present in the query string, it
                                is returned. Otherwise, if an ExternalCRMId is
                                present in the query string fragment of the URL,
                                a form field, or localStorage, the corresponding
                                lead ID is looked up.

            Dependencies:       This function relies upon the presence of either
                                a _LeadLifeJSHelpers or LLCommon object. It also
                                uses IsString and URLParameterFromQueryString,
                                and StringStartsWith, all sibling functions.

            Algorithm:          1) if Boolean argument pfLookEverywhere is TRUE
                                   and globally scoped script variable
                                   _leadidSource is unequal to SRC_IS_UNKNOWN,
                                   use the integer value stored in globally
                                   scoped script variable _leadidSource.

                                2) Look for the value found in query string
                                   parameter 'leadId' defined as script constant
                                   KEY_IS_LEAD_ID. If a value is found, convert
                                   it to an integer, update globally scoped
                                   script variable _leadid, set _leadidSource to
                                   SRC_IS_QUERY_STRING, and use that value.

                                3) Search for values for columns 'externalcrmid'
                                   and 'syscrmleadorcontact' in the following 3
                                   locations in order.

                                   a) The query string
                                   b) Like-named form fields
                                   c) Like-named localStorage keys

                                4) If values are found for one or both, use them
                                   to query the database for the ID of the lead
                                   that matches the values found. If such a lead
                                   is found, assign global script variable
                                   _leadidSource the value SRC_IS_EXTERNALCRMID.
            --------------------------------------------------------------------
        */

        const strMethodName                             = LLCommon.GetNameOfCurrentFunction ( );

        if ( pfLookEverywhere && _leadidSource !== SRC_IS_UNKNOWN )
        {
            if ( ( typeof _LeadLifeJSHelpers !== 'undefined' ) && ( ! Number.isInteger ( _LeadLifeJSHelpers.STTLeadId ) ) && ( Number.isInteger ( _leadid ) ) )
            {   // Update its STTLeadId if _LeadLifeJSHelpers is defined.
                _LeadLifeJSHelpers.STTLeadId            = _leadid;
            }   // if ( ( typeof _LeadLifeJSHelpers !== 'undefined' ) && ( ! Number.isInteger ( _LeadLifeJSHelpers.STTLeadId ) ) && ( Number.isInteger ( _leadid ) ) )

            return _leadid;
        }   // TRUE (The lead ID is identified already.) block, if ( pfLookEverywhere && _leadidSource !== SRC_IS_UNKNOWN )
        else
        {
            const strLeadId                             = URLParameterFromQueryString ( KEY_IS_LEAD_ID );

            if ( strLeadId.length > EMPTY_STRING_LENGTH && strLeadId !== NULL_AS_STRING_VALUE && Number.isInteger ( strLeadId ) )
            {
                return SpecialProcessing4LeadId ( KEY_IS_LEAD_ID ,
                                                  strLeadId ,
                                                  SRC_IS_QUERY_STRING );
            }   // TRUE (The query string contains a LeadId parameter.) block, if ( strLeadId.length > EMPTY_STRING_LEN && strLeadId !== NULL_AS_STRING_VALUE )
            else
            {
                const strExternalCRMId                  = pfLookEverywhere ? GetParameterFromURLFormOrLocalStorage ( KEY_IS_EXTERNALCRMID )   : URLParameterFromQueryString ( KEY_IS_EXTERNALCRMID );
                const strSysCRMLeadOrContact            = pfLookEverywhere ? GetParameterFromURLFormOrLocalStorage ( KEY_IS_EXTERNALCRMTYPE ) : URLParameterFromQueryString ( KEY_IS_EXTERNALCRMTYPE );

                if ( strExternalCRMId.length > EMPTY_STRING_LENGTH || strSysCRMLeadOrContact.length > EMPTY_STRING_LENGTH )
                {
                    try
                    {
                        const strVeryBasicLeadInfo      = LLCommon.DoAjax  ( 'GetVeryBasicLeadInfo4ExternamCRMId' ,
                                                                              'GET' ,
                                                                              {
                                                                                   'ExternalCRMId'       : strExternalCRMId ,
                                                                                   'SysCRMLeadOrContact' : strSysCRMLeadOrContact ,
                                                                                   'DomainId'            : GetParameterFromURLFormOrLocalStorage ( 'DomainId' ) ,
                                                                                   'TenantId'            : GetParameterFromURLFormOrLocalStorage ( 'TenantId' )
                                                                              } );

                        if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + ERR_MESSAGE_STANDARD_PREFIX ) )
                        {
                            LLCommon.LogException ( strVeryBasicLeadInfo );
                            return NO_LEAD_ID;
                        }   // TRUE (unanticipated outcome) block, if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + ERR_MESSAGE_STANDARD_PREFIX ) )
                        else
                        {
                            const astrVeryBasicLeadInfo = strVeryBasicLeadInfo.split ( LOGICAL_NEGATE );
                            const _leadid               = parseInt ( astrVeryBasicLeadInfo [ ARRAY_FIRST_ELEMENT ] );

                            if ( Number.isInteger ( _leadid ) && _leadid > NO_LEAD_ID )
                            {
                                if ( typeof _LeadLifeJSHelpers !== 'undefined')
                                {   // Update its STTLeadId if _LeadLifeJSHelpers is defined.
                                    _LeadLifeJSHelpers.STTLeadId = _leadid;
                                }   // if ( typeof _LeadLifeJSHelpers !== 'undefined')

                                _leadidSource           = SRC_IS_EXTERNALCRMID;

                                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                                sessionStorage.setItem ( 'leadid'       , _leadid );

                                return _leadid;
                            }   // TRUE (anticipated outcome) block, if ( Number.isInteger ( _leadid ) && _leadid > NO_LEAD_ID )
                            else
                            {
                                return NO_LEAD_ID;
                            }   // FALSE (unanticipated outcome) block, if ( Number.isInteger ( _leadid ) && _leadid > NO_LEAD_ID )
                        }   // FALSE (anticipated outcome) block, if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + ERR_MESSAGE_STANDARD_PREFIX ) )
                    }
                    catch ( ex )
                    {
                        LLCommon.LogException ( ex );
                        return NO_LEAD_ID;
                    }
                }   // TRUE (The query string contains an ExternalCRMId parameter.) block, if ( strExternalCRMId.length > EMPTY_STRING_LENGTH || strSysCRMLeadOrContact.length > EMPTY_STRING_LEN )
                else
                {
                    return NO_LEAD_ID;
                }   // FALSE (The query string is devoid of an ExternalCRMId parameter.) block, if ( strExternalCRMId.length > EMPTY_STRING_LEN || strSysCRMLeadOrContact.length > EMPTY_STRING_LEN )
            }   // FALSE (The query string is devoid of a LeadId parameter.) block, if ( strExternalCRMId.length > EMPTY_STRING_LENGTH || strSysCRMLeadOrContact.length > EMPTY_STRING_LEN )
        }   // FALSE (The lead ID has yet to be identified.) block, if ( pfLookEverywhere && _leadidSource !== SRC_IS_UNKNOWN )
    }   // function GetLeadOrCrmIdFromUrl


    function GetNameOfCurrentFunction ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      GetNameOfCurrentFunction

            Function Goal:      Call this function to get the name of the
                                function that is calling it, so that it can be
                                displayed in a log entry or other message.

            Output:             When called from function "myFunction" this
                                function returns "myFunction" as a String.

            Remarks:            Only Chromium browsers, which use Google's V8
                                rendering engine, implement a function named
                                Error.captureStackTrace. Since there is no good
                                way to detect this feature, it is detected by
                                catching the exception that arises when it is
                                undefined on the built-in Error object.

                                This implementation was developed and tested
                                against the Mozilla Gecko engine, the only other
                                engine that is readily at hand.

                                With some help from Bing and GPT-4, I have a
                                working version that should handle Safari, and
                                probably any other runtime engine that it
                                encounters.
            --------------------------------------------------------------------
        */

        const obj                       = { };

        try
        {
            Error.captureStackTrace ( obj , GetNameOfCurrentFunction );

            const intPosAt1             = obj.stack.indexOf ( ' at ' ) + 4;
            const intPosLParen1         = obj.stack.indexOf ( ' (' );

            return obj.stack.substring ( intPosAt1 , intPosLParen1 );
        }
        catch ( ex )
        {
            const  astrStackFrames = ex.stack.split ('\n' );

            if ( astrStackFrames.length > 1 )
            {
                const  intPosEndOfName = astrStackFrames [ 1 ].indexOf ( '@' );

                if ( intPosEndOfName > -1 )
                {
                    return astrStackFrames [ 1 ].substring ( 0 , intPosEndOfName );
                }   // TRUE (Stack trace looks like it's from Gecko.) block, if ( intPosEndOfName > -1 )
                else
                {
                    const intPosAt2     = obj.stack.indexOf ( ' at ' ) + 4;
                    const intPosLParen2 = obj.stack.indexOf ( ' (' );

                    if ( intPosAt2 > -1 )
                    {
                        if ( intPosLParen2 > -1 )
                        {
                            return obj.stack.substring ( intPosAt2 , intPosLParen2 );
                        }   // TRUE (anticipated outcome for Safari with named function at top of stack) block, if ( intPosLParen2 > -1 )
                        else
                        {
                            return '(anonymous)';
                        }   // FALSE (unanticipated outcome for Safari with anonymous function at top of stack) block, if ( intPosLParen2 > -1 )
                    }   // TRUE (anticipated outcome) block, if ( intPosAt2 > -1 )
                    else
                    {
                        return 'Unknown StackTrace Format';
                    }   // FALSE (unanticipated outcome) block, if ( intPosAt2 > -1 )
                }   // FALSE (Stack trace looks like it's from Safari.) block, if ( intPosEndOfName > -1 )
            }   // TRUE (anticipated outcome) block, if ( astrStackFrames.length > 1 )
            else
            {
                return 'Global Scope';
            }   // FALSE (unanticipated outcome) block, if ( astrStackFrames.length > 1 )
        }
    }   // function GetNameOfCurrentFunction


    function IsString ( poAnyJavaScriptObject )
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
    }   // function IsString


    function ShowOrHideElement ( pdocElement , pfShowIt )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      ShowOrHideElement

            Method Goal:        Apply one of a pair of CSS selectors, removing
                                its opposite, to Show or Hide an element.

            Maintenance Note:   Any function in another script that calls this
                                function must list it, along with ELEMENT_HIDE
                                and ELEMENT_SHOW, in the string that contains the
                                names of the external symbols to which it refers
                                to satisfy the ESLint no-undef rule.

            Input:              pdocElement = This may be either a reference to
                                              a DOM element or its ID as a
                                              string.

                                pfShowIt    = This is either Boolean True, or a
                                              truthy value, to cause element
                                              pdocElement to be shown, or False,
                                              or a falsy value, to cause element
                                              pdocElement to be hidden.

            Output:             The return value is the classList attribute of
                                the element as it stands when the function
                                returns. The classList attribute is a NodeList.

                                When pdocElement is either undefined or null,
                                the return value is undefined.

            Remarks:            This convenience function works by applying one
                                of a pair of CSS selectors, STT_HideElement,
                                removing its inverse selector, STT_ShowElement,
                                or vice versa, as indicated by the value of its
                                pfShowIt argument.

                                This method of applying classes transparently
                                preserves other selectors by taking advantage of
                                the fact that the classList attribute of every
                                HTML element is an array that implements an add
                                method and a remove method.

            Algorithm:          1) If pdocElement is undefined or a null
                                   reference, do nothing, returning null.

                                2) If pdocElement is a string, treat it as the
                                   ID of an element, and get a reference to it.

                                3) If pdocElement is not a string and it ia
                                   null, do nothing, returning null.

                                4) Treating pfShowIt as a Boolean, evaluate its
                                   truthiness.

                                5) If pfShowIt evaluates to True, make element
                                   pdocElement visible by removing CSS selector
                                   STT_HideElement and adding STT_ShowElement to
                                   its classList array.

                                6) If pfShowIt evaluates to False, make element
                                   pdocElement invisible by removing selector
                                   STT_ShowElement and adding STT_HideElement to
                                   its classList array.

            Reference:         Add a CSS class to an HTML element with JavaScript/jQuery
                               https://www.techiedelight.com/add-css-class-to-html-element-javascript
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        {   // When pdocElement is either undefined or null, there is nothing to do.
            const docThisElement = IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

            if ( docThisElement !== null )
            {   // When docThisElement is null, there is nothing to do.
                if ( pfShowIt )
                {
                    docThisElement.classList.remove ( 'STT_HideElement' );
                    docThisElement.classList.add    ( 'STT_ShowElement' );
                }   // TRUE (The calling routine wants element pdocElement shown.) block, if ( pfShowIt )
                else
                {
                    docThisElement.classList.remove ( 'STT_ShowElement' );
                    docThisElement.classList.add    ( 'STT_HideElement' );
                }   // FALSE (The calling routine wants element pdocElement hidden.) block, if ( pfShowIt )

                return docThisElement.classList;
            }   // TRUE (anticipated outcome) block, if ( docThisElement !== null )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( docThisElement !== null )
        }   // TRUE (anticipated outcome) block, if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        else
        {
            return null;
        }   // FALSE (unanticipated outcome) block, if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
    }   // function ShowOrHideElement


    function StringEndsWith ( pstrTestSubject , pstrTestString )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      StringEndsWith

            MAINTENANCE NOTE:   This function and its inverse, StringStartsWith,
                                are deprecated, and will be removed once we are
                                certain that all references have been updated to
                                use String.prototype.endsWith(), documented at
                                https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/endsWith.

            Function Goal:      Evaluate whether string pstrTestSubject
                                ends with string pstrTestString.

            Input:              pstrTestSubject = string to evaluate

                                pstrTestString  = string to evaluate
                                                  whether pstrTestSubject ends
                                                  with it

            Output:             If string pstrTestString ends with
                                string pstrTestString, the return value
                                is true. Otherwise, the return value is
                                false.

            See Also:           StringStartsWith
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
        {
            const intSubjectLength      = pstrTestSubject.length;
            const intTestLength         = pstrTestString.length;

            if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
            {
                if ( intSubjectLength >= intTestLength )
                {
                    return ( pstrTestSubject.substring ( pstrTestSubject.length - intTestLength ) === pstrTestString );
                }   // TRUE (The subject string is at least as long as the test string.) block, if ( intSubjectLength >= intTestLength )
                else
                {
                    return false;
                }   // FALSE (The subject string is shorter than the test string.) block, if ( intSubjectLength >= intTestLength )
            }   // TRUE (anticipated outcome) block, if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
            else
            {
                return false;
            }   // FALSE (unanticipated outcome) block, if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
        }   // TRUE (anticipated outcome) block, if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
        else
        {
            return false;
        }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
    }   // function StringEndsWith


    function StringStartsWith ( pstrTestSubject , pstrTestString )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      StringStartsWith

            MAINTENANCE NOTE:   This function and its inverse, StringEndsWith,
                                are deprecated, and will be removed once we are
                                certain that all references have been updated to
                                use String.prototype.startsWith(), documented at
                                https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/startsWith.

            Function Goal:      Evaluate whether string pstrTestSubject starts
                                with string pstrTestString.

            Input:              pstrTestSubject = string to evaluate

                                pstrTestString  = string to evaluate whether
                                                  pstrTestSubject starts with it

            Output:             If string pstrTestString starts with string
                                pstrTestString, the return value is true.
                                Otherwise, the return value is false.

            See Also:           StringEndsWith
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
        {
            const EMPTY_STRING_LENGTH   = 0;
            const SUBSTRING_FIRST_CHAR  = 0;

            const intSubjectLength      = pstrTestSubject.length;
            const intTestLength         = pstrTestString.length;

            if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
            {
                if ( intSubjectLength >= intTestLength )
                {
                    return ( pstrTestSubject.substring ( SUBSTRING_FIRST_CHAR , intTestLength ) === pstrTestString );
                }   // TRUE (The subject string is at least as long as the test string.) block, if ( intSubjectLength >= intTestLength )
                else
                {
                    return false;
                }   // FALSE (The subject string is shorter than the test string.) block, if ( intSubjectLength >= intTestLength )
            }   // TRUE (anticipated outcome) block, if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
            else
            {
                return false;
            }   // FALSE (unanticipated outcome) block, if ( intSubjectLength > EMPTY_STRING_LENGTH && intTestLength > EMPTY_STRING_LENGTH )
        }   // TRUE (anticipated outcome) block, if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
        else
        {
            return false;
        }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrTestSubject ) && IsString ( pstrTestString ) )
    }   // function StringStartsWith


    function GetParameterFromURLFormOrLocalStorage ( pstrKeyName , pobjDefaultValue )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        GetParameterFromURLFormOrLocalStorage

            Method Goal:        Extract a named parameter from a query string.
                                If absent from the query string, fall back to
                                a text input field with an ID of pstrKeyName,
                                then to a like-named property in Local Storage.

            Input:              pstrKeyName         = Name of key to extract

                                pobjDefaultValue    = Default string to return
                                                      when pstrKeyName is not
                                                      found in the query string

            Output:             Value of key named by pstrKeyName, if present,
                                otherwise the empty string

            Remarks:            Both arguments are type-tested that they are
                                truly String objects.

                                1)  If pobjDefaultValue is undefined, its value
                                    becomes the empty string, and argument
                                    pstrKeyName is evaluated.

                                2)  If pstrKeyName is not a string, the default,
                                    which MAY be the empty string, is returned.

            Source:             Get Query String Parameters with JavaScript
                                https://davidwalsh.name/query-string-javascript
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        const strDefaultValue           = Object.is ( pobjDefaultValue , undefined ) ? EMPTY_STRING : pobjDefaultValue;

        if ( IsString ( pstrKeyName ) )
        {
            const strResult             = URLParameterFromQueryString ( pstrKeyName );

            if ( strResult !== null && strResult !== NULL_AS_STRING_VALUE )
            {
                return strResult;                           // Function URLParameterFromQueryString fed this result to SpecialProcessing4LeadId.
            }   // TRUE (ideal outcome) block, if ( astrResults !== null )
            else
            {   // Since the query string cannot satisfy the query, see whether the page fields or Local Storage can do so.
                const strPageFieldValue = QueryPageFields ( pstrKeyName ,
                                                            strDefaultValue ,
                                                            true );

                if ( strPageFieldValue !== strDefaultValue )
                {   // The form satisfied the query.
                    return strPageFieldValue;
                }   // TRUE (The page fields answered the query.) block, if ( strPageFieldValue !== strDefaultValue )
                else
                {   // Either localStorage satisfies the query, or the default value will have to do.
                    return QuerySssionStorage ( pstrKeyName ,
                                                strDefaultValue ,
                                                true );
                }   // FALSE (Local Storage answered the query, even if only by returning the default value.) block, if ( strPageFieldValue !== strDefaultValue )
            }   // FALSE (The query string didn't satisfy the query.) block, if ( astrResults !== null )
        }   // TRUE (anticipated outcome) block, if ( IsString ( pstrKeyName ) )
        else
        {   // Without a usable key, the only choice is to return the default value.
            return strDefaultValue;
        }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrKeyName ) )
    }  // function GetParameterFromURLFormOrLocalStorage


    function IndexFromOrdinal ( pintOrdinal )
    {
        /*
            --------------------------------------------------------------------
            Name:       IndexFromOrdinal

            Goal:       Return the index (zero-based element subscript) for the
                        one-based element subscript specified by argument
                        pintOrdinal.

            Arguments:  pintOrdinal = Integer which is assumed to be a one-based
                                      array subscript

            Returns:    The value of pintOrdinal less one.
            --------------------------------------------------------------------
        */

        return pintOrdinal - ARRAY_NEXT_ELEMENT;
    }   // function IndexFromOrdinal


    function LeftPadInteger ( pintInputInteger , pintTotalLength )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        LeftPadInteger

            Method Goal:        Log an exception raised by the player or its
                                associated code.

            Input:              pintInputInteger = Positive or negative integer
                                                   to pad on its left end with
                                                   zeroes

                                pintTotalLength  = Total width of the returned
                                                   string, an integer greater
                                                   than zero

            Output:             If both arguments are valid, the returned string
                                contains the string representation of argument
                                pintInputInteger left-padded with zeroes such
                                that the total length of the string is equal to
                                pintTotalLength.

                                If one or both arguments is non-integral or if
                                pintTotalLength is less than or equal to zero,
                                the return value is a string that starts with
                                "ERROR: " followed by a descriptive message.

                                Since pintInputInteger may be either positive or
                                negative, the padding takes into account room to
                                include the minus sign, which appears on the
                                left end of the returned string.
            --------------------------------------------------------------------
        */

        const strMethodName                     = LLCommon.GetNameOfCurrentFunction ( );

        if ( Number.isInteger ( pintInputInteger ) && Number.isInteger ( pintTotalLength ) )
        {
            if ( pintTotalLength > EMPTY_STRING_LENGTH )
            {
                const strIntegerAsStr           = Math.abs ( pintInputInteger ).toString ( );
                const intIntegerLength          = strIntegerAsStr.length;

                var   rstrNumericString;
                var   intPaddingChars;

                if ( pintInputInteger > NUMERIC_ZERO )
                {
                    intPaddingChars             = pintTotalLength - intIntegerLength;
                    rstrNumericString           = EMPTY_STRING;
                }   // TRUE (anticipated outcome for most use cases) block, if ( pintInputInteger > this.NUMERIC_ZERO )
                else
                {
                    intPaddingChars             = pintTotalLength - intIntegerLength - NUMERIC_PLUS_ONE;
                    rstrNumericString           = HYPHEN_CHAR;
                }   // FALSE (unanticipated outcome for most use cases) block, if ( pintInputInteger > this.NUMERIC_ZERO )

                if ( intPaddingChars > NUMERIC_ZERO )
                {
                    for ( var intJ = EMPTY_STRING_LENGTH;
                              intJ < intPaddingChars;
                              intJ++ )
                    {
                        rstrNumericString       += CHARACTER_ZERO;
                    }   // for ( for ( var intJ = EMPTY_STRING_LENGTH; intJ < intPaddingChars; intJ++ )

                    rstrNumericString           += strIntegerAsStr;
                    return rstrNumericString;
                }   // TRUE (anticipated outcome) block, if ( intPaddingChars > NUMERIC_ZERO )
                else
                {
                    return rstrNumericString    += strIntegerAsStr;
                }   // FALSE (unanticipated outcome) block, if ( intPaddingChars > NUMERIC_ZERO )
            }   // TRUE (anticipated outcome) block, if ( pintTotalLength > EMPTY_STRING_LENGTH )
            else
            {
                return 'ERROR: Total length pintTotalLength = ' + pintTotalLength + ' is less than or equal to zero. Its value must be an integer that is greater than zero.';
            }   // FALSE (unanticipated outcome) block, if ( pintTotalLength > EMPTY_STRING_LENGTH )
        }   // TRUE (anticipated outcome) block, if ( Number.isInteger ( pintInputInteger ) && Number.isInteger ( pintTotalLength ) ))
        else
        {
            return 'ERROR: One or both of arguments pintInputInteger = ' + pintInputInteger + ' and pintTotalLength = ' + pintTotalLength + ' is non-integral. Both must be integers.';
        }   // FALSE (unanticipated outcome) block, if ( Number.isInteger ( pintInputInteger ) && Number.isInteger ( pintTotalLength ) ))
    }   // function LeftPadInteger


    function OrdinalFromIndex ( pintIndex )
    {
        /*
            --------------------------------------------------------------------
            Name:       OrdinalFromIndex

            Goal:       Return the ordinal (one-based element subscript) for the
                        zero-based element subscript specified by argument
                        pintIndex.

            Arguments:  pintIndex = Integer which is assumed to be a zero-based
                                    array subscript

            Returns:    One plus the value of pintIndex.
            --------------------------------------------------------------------
        */

        return pintIndex + ARRAY_NEXT_ELEMENT;
    }   // function OrdinalFromIndex


    function QueryLocalStorage ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QueryLocalStorage

            Function Goal:      Query Local Storage for the value of a specified
                                key, returning either the value found or a
                                specified default value.

            Input:              pstrKeyName         = String representation of
                                                      the key name for which to
                                                      query localStorage

                                pstrDefaultValue    = Value to return when a
                                                      localStorage value cannot
                                                      be found

                                pfInputsAreSafe     = Boolean flag that, if true
                                                      causes string validation
                                                      to be skipped

            Output:             Return either the LocalStorage value at key
                                pstrKeyName, or pstrDefaultValue.

            Remarks:            The objective of the third argument is to skip
                                validation of arguments that have already been
                                varified to be of type String.
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        var   strDefaultValue;
        var   strKeyName;

        if ( pfInputsAreSafe )
        {
            strDefaultValue             = pstrDefaultValue;
            strKeyName                  = pstrKeyName.toLowerCase ( );
        }   // TRUE (Caller already vetted both inputs.) block, if ( pfInputsAreSafe )
        else
        {   // Execution cannot proceed unless pstrKeyName is a String.
            if ( IsString ( pstrKeyName ) )
            {
                strKeyName              = pstrKeyName.toLowerCase ( );
            }   // TRUE (anticipated outcome) block, if ( IsString ( pstrKeyName ) )
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrKeyName ) )

            strDefaultValue = Object.is ( pstrDefaultValue , undefined ) ? pstrDefaultValue : EMPTY_STRING;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const strLocalStorageValue      = localStorage.getItem ( strKeyName );
        const strFinalLocalStorageValue = strLocalStorageValue === null ? strDefaultValue : strLocalStorageValue;

        return SpecialProcessing4LeadId ( strKeyName ,
                                          strFinalLocalStorageValue ,
                                          SRC_IS_LOCAL_STORAGE );
    }   // function QueryLocalStorage


    function QueryPageFields ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        QueryPageFields

            Method Goal:        Query INPUT fields in the active page for the
                                value of a specified key, returning either the
                                value found or a specified default value.

            Input:              pstrKeyName         = String representation of
                                                      the key name for which to
                                                      query the INPUT elements
                                                      on the current page.

                                pstrDefaultValue    = Value to return when a
                                                      localStorage value cannot
                                                      be found

                                pfInputsAreSafe     = Boolean flag that, if true
                                                      causes string validation
                                                      to be skipped

            Output:             Return either the INPUT element value at key
                                pstrKeyName, or strDefaultValue.

            Remarks:            The objective of the third argument is to skip
                                validation of arguments that have already been
                                varified to be of type String.
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        var   strDefaultValue;

        if ( pfInputsAreSafe )
        {
            strDefaultValue = pstrDefaultValue;
        }   // TRUE (Caller already vetted both inputs.) block, if ( pfInputsAreSafe )
        else
        {   // Execution cannot proceed unless pstrKeyName is a String.
            if ( !IsString ( pstrKeyName ) )
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // if ( !IsString ( pstrKeyName ) )

            strDefaultValue             = Object.is ( pstrDefaultValue , undefined ) ? EMPTY_STRING : pstrDefaultValue;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const docInputElement           = document.getElementById ( pstrKeyName );
        const strInputElementValue      = docInputElement === null ? strDefaultValue : docInputElement.value;

        return SpecialProcessing4LeadId ( pstrKeyName ,
                                          strInputElementValue ,
                                          SRC_IS_FORM_FIELD );
    }   // function QueryPageFields


    function QuerySssionStorage ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QuerySssionStorage

            Function Goal:      Query Session Storage for the value of a
                                specified key, returning either the value found
                                or a specified default value.

            Input:              pstrKeyName         = String representation of
                                                      the key name for which to
                                                      query sessionStorage

                                pstrDefaultValue    = Value to return when a
                                                      sessionStorage value cannot
                                                      be found

                                pfInputsAreSafe     = Boolean flag that, if true
                                                      causes string validation
                                                      to be skipped

            Output:             Return either the sessionStorage value at key
                                pstrKeyName, or pstrDefaultValue.

            Remarks:            The objective of the third argument is to skip
                                validation of arguments that have already been
                                varified to be of type String.
            --------------------------------------------------------------------
        */

        const strMethodName               = LLCommon.GetNameOfCurrentFunction ( );

        var   strDefaultValue;
        var   strKeyName;

        if ( pfInputsAreSafe )
        {
            strDefaultValue               = pstrDefaultValue;
            strKeyName                    = pstrKeyName.toLowerCase ( );
        }   // TRUE (Caller already vetted both inputs.) block, if ( pfInputsAreSafe )
        else
        {   // Execution cannot proceed unless pstrKeyName is a String.
            if ( IsString ( pstrKeyName ) )
            {
                strKeyName                = pstrKeyName.toLowerCase ( );
            }   // TRUE (anticipated outcome) block, if ( IsString ( pstrKeyName ) )
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrKeyName ) )

            strDefaultValue = Object.is ( pstrDefaultValue , undefined ) ? pstrDefaultValue : EMPTY_STRING;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const strSessionStorageValue      = sessionStorage.getItem ( strKeyName );
        const strFinalSessionStorageValue = strSessionStorageValue === null ? strDefaultValue : strSessionStorageValue;

        return SpecialProcessing4LeadId ( strKeyName ,
                                          strFinalSessionStorageValue ,
                                          SRC_IS_SESSION_STORAGE );
    }   // function QuerySssionStorage


    function SpecialProcessing4LeadId ( pstrKeyName , pstrKeyValue , pintValueSource )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      SpecialProcessing4LeadId

            Function Goal:      Perform special processing for Lead ID found in
                                any source.

            Input:              pstrKeyName         = String representation of
                                                      the key name to evaluate

                                pstrKeyValue        = Value assigned to key name
                                                      pstrKeyName

                                pintValueSource     = Integer to assign to
                                                      _leadidSource when a valid
                                                      lead ID is presented

            Output:             Regardless of the outcome, the return value is
                                pstrKeyValue.

            Remarks:            The objective of this routine is to make lead ID
                                processing consistent throughout.
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        if ( pstrKeyName.toLowerCase ( ) === KEY_IS_LEAD_ID && pstrKeyValue !== NULL_AS_STRING_VALUE && Number.isInteger ( pstrKeyValue ) )
        {
            const intLeadIdCandidate = parseInt ( pstrKeyValue );

            if ( intLeadIdCandidate > NO_LEAD_ID )
            {
                _leadid         = intLeadIdCandidate;
                _leadidSource   = pintValueSource;

                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( intLeadIdCandidate > NO_LEAD_ID )
        }   // if ( pstrKeyName.toLowerCase ( ) === KEY_IS_LEAD_ID && strResult !== NULL_AS_STRING_VALUE )

         return pstrKeyValue;
    }   // function SpecialProcessing4LeadId


    function UpdateIfChanged ( poChangeEvent , pfUpdateLeadModDate )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      UpdateIfChanged

            Function Goal:      Update the database with the new value in the
                                element (INPUT control) that receives the call
                                either directly or in answer to a change event.

            Input:              poChangeEvent       = JavaScript string containing
                                                      ID prefixed by the standard
                                                      JQUERY_SELECTOR_IS_ELEMENT_ID
                                                      token if the value is the ID
                                                      of an input element (control),
                                                      or a standard JavaScript event
                                                      object

                                pfUpdateLeadModDate = Boolean flag, defaulted to
                                                      TRUE, that controls whether
                                                      the last modified date of
                                                      the controlling Lead row is
                                                      updated

            Output:             This method returns void (nothing). Errors are
                                logged, and elicit messages delivered by alert
                                box only when absolutely necessary.

            Remarks:            Since it answers DOM events, this routine cannot
                                be implemented as an instance method on a
                                JavaScript object. Moreover, attempting to hide
                                that fact behind a binding to the Window object
                                confuses the runtime, causing it to be unable to
                                access the object properties through this.
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        try
        {
            const docChangedElement     = LLCommon.IsString ( poChangeEvent ) ? document.getElementById ( poChangeEvent ) : poChangeEvent.currentTarget;
            const fUpdateLeadModDate    = pfUpdateLeadModDate === undefined
                                          ? !( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                                          : pfUpdateLeadModDate;

         	console.log ( strMethodName + ' event called for element ' + docChangedElement.id + ' of type ' + docChangedElement.type + ', with pfUpdateLeadModDate mapped to ' + fUpdateLeadModDate );

            debugger;

            //  ----------------------------------------------------------------
            //  There is a list of elements that are somehow getting registered
            //  for blur events even though they are unbound controls and should
            //  not have their values passed to the database because there is no
            //  corresponding field to update, hence their unboundness.
            //  ----------------------------------------------------------------

            if ( _astrUISelectElements2Ignore.indexOf ( docChangedElement.id.toLowerCase ( ) ) === ARRAY_INVALID_INDEX )
            {
                //  ------------------------------------------------------------
                //  The element ID must be specified as it would be for jQuery.
                //
                //  Since activating the Rules Engine implies permission to
                //  update the last modified date of the controlling lead
                //  record, its flag has the same value as the new
                //  pfUpdateLeadModDate flag.
                //  ------------------------------------------------------------

                const strUpdateFormFieldResult = _LeadLifeJSHelpers.UpdateFormFieldById ( JQUERY_SELECTOR_IS_ELEMENT_ID + docChangedElement.id , docChangedElement.id , fUpdateLeadModDate , fUpdateLeadModDate ).toString ( );

                if ( strUpdateFormFieldResult.length > EMPTY_STRING_LENGTH )
                {
                    console.log ( 'JS Function ' + strMethodName
                                                 + ': strUpdateFormFieldResult = '
                                                 + strUpdateFormFieldResult
                                                 + 'docChangedElement.id = '
                                                 + docChangedElement.id
                                                 + ', docChangedElement.value = '
                                                 + docChangedElement.value );
                    alert ( LLCommon.LogException (   'JS Function ' + strMethodName
                                                    + ': An exception arose while updating field ' + docChangedElement.id
                                                    + ' value to ' + _LeadLifeJSHelpers.GetValueFromInputControl ( docChangedElement )
                                                    + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                    + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                    + 'Please contact SalesTalk customer support for assistance.' ) );
                }   // TRUE (unanticipated outcome) block, if ( strUpdateFormFieldResult.length > EMPTY_STRING_LENGTH )
                else
                {
                    console.log ( 'JS Function ' + strMethodName + ': UpdateFormFieldById SUCCEEDED, docChangedElement.id =' + docChangedElement.id + ', docChangedElement.value = ' + docChangedElement.value );
                }   // FALSE (anticipated outcome) block, if ( strUpdateFormFieldResult.length > EMPTY_STRING_LENGTH ))
            }   // if ( _astrUISelectElements2Ignore.indexOf ( docChangedElement.id.toLowerCase ( ) ) === ARRAY_INVALID_INDEX )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
            console.error ( 'JS Function ' + strMethodName + ': An exception arose while processing a focus change event on the Words2Actions mobile SalesTalk application page. Exception = ' + ex.message + ', Stack Trace = ' + ex.stack );
            alert ( 'JS Function ' + strMethodName + ': An exception arose while processing a focus change event on the Words2Actions mobile SalesTalk application page.'
                + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                + 'Please contact SalesTalk customer support for assistance.' );
            return ex.message;
        }
    }   // function UpdateIfChanged


    function URLParameterFromQueryString ( pstrParameterName )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      URLParameter

            Method Goal:        Get the value of a named query string parameter.

            Input:              pstrParameterName = string representation of the
                                                    URL query string parameter
                                                    to return

            Output:             The return value is a string representation of
                                the value of the parameter specified by
                                pstrParameterName, if it exists, otherwise, the
                                return value is the empty string.

            Remarks:            This convenience function was appropriated from
                                Story.cshtml, where it landed after Bud Pass got
                                it from somewhere on the Web. Apart from small
                                formatting changes, this is the code from the
                                Story-So-Far page.

            Algorithm:          The match function takes a regular expression,
                                returning an array of matches, from which the
                                second element (at subscript 1) is the desired
                                value. This is all standard regular expression
                                fare condensed into a single expression that is
                                fed into intrinsic function decodeURIComponent.

                                Version 1.009 adds the 'i' flag, causing the
                                match to be case insensitive.
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const strMatchResult    = decodeURIComponent ( ( window.location.search.match ( RegExp ( "[?|&]" + pstrParameterName + '=(.+?)(&|$)' , 'i' ) ) || [, null] ) [ 1 ] );  // Ignore the no_useless_escape and no_sparse_arrays rule violations raised by ESLint.

        return SpecialProcessing4LeadId ( pstrParameterName ,
                                          strMatchResult ,
                                          SRC_IS_QUERY_STRING );
    }   // function URLParameterFromQueryString, bound to Window as URLParameter


    console.log ( '_fCallRulesEngineOnSubmit    = ' + ( typeof _fCallRulesEngineOnSubmit === 'undefined' ? 'undefined' : _fCallRulesEngineOnSubmit ) );
    console.log ( '_fDebugLogging               = ' + _fDebugLogging );
    console.log ( '_fSkipAsyncEventRegistration = ' + _fSkipAsyncEventRegistration );

    console.log ( ScriptInfoForLog ( LeadLifeJSHelpersGlobals_SCRIPTSOURCE ,
                                     LeadLifeJSHelpersGlobals_VERSION ,
                                     LeadLifeJSHelpersGlobals_LastUpdated ,
                                     'loaded' ) );
/*
    ============================================================================
                      E n d   o f   S o u r c e   F i l e
    ============================================================================
*/