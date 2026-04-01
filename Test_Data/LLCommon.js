/* eslint-env browser */
/* global $ _fDebugLogging bootbox data grid hopscotch isCallFinished isSalesPage jQuery kendo LLCommon loadView moment nalert saveAs STT_HOURGLASS_ID STT_NOTE_ID_PREFIX STT_VIDEOPLAYER_TRANSCRIPTHOURGLASS_CONTAINER_ID Type ViewModel */
/*
 * Definition of global attached to window properties
*/

var LLCommon_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
var LLCommon_VERSION      = '*** NONE ***';
var LLCommon_LogTraces    = false;
var LLCommon_LastUpdated  = '2026/03/30 21:00:28 CDT';

debugger;

function ScriptInfoForLog ( pstrScriptSource , pfltVersionNumber , pstrLastUpdated , pstrMessage )
{
    /*
        ------------------------------------------------------------------------
        Function Name:          ScriptInfoForLog

        Method Goal:            Construct a message for display on the debug
                                console log.

        Input:                  pstrScriptSource  = string representation of
                                                    URL, relative to origin,
                                                    from which the script loaded

                                pfltVersionNumber = floating point version
                                                    number, usually having three
                                                    decimal places

                                pstrLastUpdated   = String representation of the
                                                    date and time of the most
                                                    recent script update, ending
                                                    with the abbreviated time
                                                    zone

                                pstrMessage       = Optional string to append to
                                                    the message, preceded by a
                                                    space, unless undefined or
                                                    null

        Output:                 The return value is a string, formatted for
                                display on the debug console log.
        ------------------------------------------------------------------------
    */

    const toFixedDecimalString = function ( pobjDecimalCandidate , pintDecimals )
    {
        const fltNumberCandidate = Number.parseFloat ( pobjDecimalCandidate );
        return Number.isNaN ( fltNumberCandidate ) ? fltNumberCandidate : fltNumberCandidate.toFixed ( Number.isInteger ( Number ( pintDecimals ) ) ? pintDecimals : 2 );
    };   // const toFixedDecimalString

    const strVersion = toFixedDecimalString ( pfltVersionNumber , 3 );
    return (   pstrScriptSource
             + ', version '      + ( Number.isNaN ( strVersion ) ? pfltVersionNumber : strVersion )
             + ', last updated ' + pstrLastUpdated
             + ( pstrMessage === undefined || pstrMessage === null ? '' : ' ' + pstrMessage ) );
}   // function ScriptInfoForLog


console.log ( 'Processing scripts for document.location.href = ' + document.location.href + ', origin = ' + origin );
console.log ( ScriptInfoForLog ( LLCommon_SCRIPTSOURCE , LLCommon_VERSION , LLCommon_LastUpdated , 'loading' ) );


var isInCallQueue = false;


(function ( )
{
    nalert = window.alert;
    Type = {
        native : 'native',
        custom : 'custom'
    };
})();

// Save the original fetch
const originalFetch = window.fetch;

// Override global fetch
window.fetch = async ( ...args ) =>
{
    const [resource, config = {}] = args;

    // Identify your own requests.
    const isLLCommon = config.headers?.['X-LLCommon-Request'] === 'true';

    console.log ( `[Fetch Intercept] ${isLLCommon ? '[LLCommon]' : '[External]'} ${config.method || 'GET'} ${resource}`);

    // Optional: log stack trace for unexpected calls.
    if ( !isLLCommon )
    {
        console.trace('[Fetch Intercept] Stack trace for external fetch');
    }   // if ( !isLLCommon )

    try
    {
        const response = await originalFetch ( ...args );
        console.log ( 'Fetch Intercept: response.status = ' + response.status );
        return response;
    }
    catch ( ex )
    {
        if ( ex.name === 'AbortError' )
        {
            console.warn ( `[Fetch Intercept] Fetch aborted: ${reason || 'no reason provided'}` );
        }   // TRUE (anticipated outcome) block, if ( ex.name === 'AbortError' )
        else
        {
            console.warn(`[Fetch Intercept] Fetch failed: ${ex}`);
        }   // FALSE (unanticipated outcome) block, if ( ex.name === 'AbortError' )

        throw ex;
    }
};


//  Extension method to supply the Internet domain name component of a Location as a read-only property
Object.defineProperty ( location , 'domain' ,
{
    get          : function ( )
                   {
                        var   rstrDomainName          = '';

                        const astrParts               = this.hostname.split ( '.' );
                        const intHostNamePartsCount   = astrParts.length;
                        const intDonmainStartPosition = intHostNamePartsCount - 2;

                        for ( var intCurrentPart = intDonmainStartPosition;
                                  intCurrentPart < intHostNamePartsCount;
                                  intCurrentPart++ )
                        {
                            if ( intCurrentPart > intDonmainStartPosition )
                            {
                                rstrDomainName += '.';
                            }   // if ( intCurrentPart > intDonmainStartPosition )

                            rstrDomainName += astrParts [ intCurrentPart ];
                        }   // for ( var intCurrentPart = intDonmainStartPosition; intCurrentPart < intHostNamePartsCount; intCurrentPart++ )

                        return rstrDomainName;
                   }    // get method
}); // Object.defineProperty ( location , 'domain'


/**
 * Factory method for calling alert ( ). It will call a native alert ( ) or a
 * custom redefined alert() by a Type param. This definition is needed for IE
 * @returns {void}
 */
(function ( proxy )
{
    proxy.alert = function ( )
    {
        var message = ( ! arguments [ 0 ] ) ? 'null' : arguments [ 0 ];
        var type    = ( ! arguments [ 1 ] ) ? ''     : arguments [ 1 ];

        if ( type && type === 'native' )
        {
            nalert ( message );
        } else {
            bootbox.alert ( message );
        }
    };
})(this);


(function ( global )
{
    const _LoginIdState = { isSet: false };
    global._fLoginIdIsSet =
    {
        get isSet ( )     { return _LoginIdState.isSet;  },
        set isSet ( pfVal ) { _LoginIdState.isSet = !!pfVal; }
    };
})(window);

//  ----------------------------------------------------------------------------
//  Since these constants have global scope, any module can use them.
//  ----------------------------------------------------------------------------

const ALTERNATE_DB_NAME                 = '/SalesTalk';
const ARRAY_INVALID_INDEX               = -1;
const ARRAY_FIRST_ELEMENT               = 0;
const ARRAY_IS_EMPTY                    = 0;
const ARRAY_NEXT_ELEMENT                = 1;
const ARRAY_NOT_EMPTY                   = 1;
const ARRAY_SECOND_ELEMENT              = 1;
const ARRAY_THIRD_ELEMENT               = 2;
const ARRAY_FOURTH_ELEMENT              = 3;
const ARRAY_FIFTH_ELEMENT               = 4;
const ARRAY_SIXTH_ELEMENT               = 5;
const ARRAY_SEVENTH_ELEMENT             = 6;
const ARRAY_EIGHTH_ELEMENT              = 7;
const ASTERISK_CHAR                     = '*';              // When you must know absolutely, positively, that it's an ASTERISK character, use this in its place.

const BELL_CONTROL_CODE                 = '\x07';           // Use this token when you need the ASCII BEL character.
const BRACKET_LEFT                      = '[';
const BRACKET_RIGHT                     = ']';
const CHARACTER_ZERO                    = '0';              // When you must know absolutely, positively, that it's ZERO as a CHARACTER, use this in its place.
const CSV_SEPARATOR_CHAR                = ',';              // When you must know absolutely, positively, that it's COMMA, intended as a CSV string separator character, use this in its place.
const DBNULL                            = 'NULL';
const DECIMAL_POINT                     = '.';              // When you must know absolutely, positively, that it's a DECIMAL POINT, use this in its place.
const DEFAULT_DATE_SEPARATOR_CHAR       = '/';
const DEFAULT_DB_NAME                   = '/SalesAcceleration';

const EMAIL_ADDRESS_ALTERNATE_DELIMITER = ';'
const EMPTY_STRING                      = '';               // When you must know absolutely, positively, that it's the empty string, use this in its place.
const EMPTY_STRING_LENGTH               = 0;
const EQUALS_CHAR                       = '=';              // When you must know absolutely, positively, that it's an EQUALS character, use this in its place.
const EXPECTING_AN_INTEGER_VALUE        = true;             // Use this as the third argument to function GetParamValue (defined herein).

const FULL_STOP                         = '.';              // When you must know absolutely, positively, that it's a FULL STOP (PERIOD or DOT) character, use this in its place.

const HASH_CHARACTER                    = '#'               // When you must know absolutely, positively, that it's a QUESTION MARK character, use this in its place.
const HTML5_DATE_SEPARATOR_CHAR         = '-';              // Use this character to evaluate whether a strting represents a valid HTML5 date string, or to construct such a string from the parts of a date.
const HTML_NBSP                         = '&nbsp;';         // When you must know absolutely, positively, that it's the HTML entity representation of the nonbreaking space character, use this in its place.
const HYPHEN_CHAR                       = '-';              // When you must know absolutely, positively, that it's a QUESTION MARK character, use this in its place.

const INDEXOF_NOT_FOUND                 = -1;               // Test the return value of string.indexOf against this constant to make your intention crystal clear.

const JQUERY_SELECTOR_IS_CLASSNAME      = FULL_STOP;        // Prefix a string with this character to instruct jQuery to interpret the string as the name of a CSS class selector.
const JQUERY_SELECTOR_IS_ELEMENT_ID     = HASH_CHARACTER;   // Prefix a string with this character to instruct jQuery to interpret the string as the ID of an HTML element.

const KEY_IS_LEAD_ID                    = 'leadid';
const KEY_IS_EXTERNALCRMID              = 'externalcrmid';
const KEY_IS_EXTERNALCRMTYPE            = 'syscrmleadorcontact';
const KEY_VALUE_PAIR_IS_KEY             = ARRAY_FIRST_ELEMENT;
const KEY_VALUE_PAIR_IS_VALUE           = ARRAY_SECOND_ELEMENT;

const LOGICAL_NEGATE                    = '¬';

const MINIMUM_STT_ENTITY_ID             = 1000;
const NUMBER_MINUS_ONE                  = -1;               // Use this integer to represent the integer value -1 to explicitly differentiate it from an arithmetic operation.
const NEXT_CHARACTER                    = 1;                // Add this integer to a pointer to advance it to the next character. A simpler approach is to substitute an increment (++) operation.
const NO_LEAD_ID                        = 0;
const NULL_AS_STRING_VALUE              = 'null';
const NUMERIC_PLUS_ONE                  = +1;               // When you must know absolutely, positively, that it's a positive integer one, use this in its place.
const NUMERIC_ZERO                      = 0;                // When you must know absolutely, positively, that it's a numeral zero, use this in its place.

const PATH_PROTOCOL_DELIMITER           = '//';             // Use this two-character string with string.indexOf to evaluate whether a URI is absoloute (fully qualified) or to construct such a URI.
const PATH_SEPARATOR_CHAR               = '/';              // This applies to Web paths and Unix paths. See also WINDOWS_PATH_SEPARATOR_CHAR.
const PIPE_CHAR                         = '|';              // When you must know absolutely, positively, that it's a PIPE character, use this in its place.
const PIPE_CHAR_SPLIT_MATCH             = '\|';             // Use this with the JavaScript string.split method to split on PIPE characters.

const QUERY_STRING_START_DELIMITER      = '?';              // Use this with string.indexOf to find the start of a query string in a href string.
const QUERY_STRING_PARAM_DELIMITER      = '&';              // Use this to split a query string into its components.
const QUESTION_MARK_CHAR                = '?';              // When you must know absolutely, positively, that it's a QUESTION MARK character, use this in its place.
const QUOTE_DOUBLE                      = '"';              // When you must know absolutely, positively, that it's a DOUBLE QUOTE character, use this in its place, especially useful when incorporating a quoted string into a larger string.
const QUOTE_SINGLE                      = "'";              // When you must know absolutely, positively, that it's a SINGLE QUOTE character, use this in its place, especially useful when incorporating a quoted string into a larger string.

const REGEXP_CASE_INSENSITIVE_MATCH     = 'i';              // Case-insensitive search.
const REGEXP_GLOBAL_MATCH               = 'g';              // Global search - return all matches in string
const REGEXP_SINGLE_LINE                = 's';              // Allows . to match newline characters, sets dotAll = true.
const REGEXP_ESCAPE_CHARACTER           = '\\'              // When you must know absolutely, positively, that it's an Escape character in a Regular Expression, use this in its place.

const SELECTED_INDEX_UNSELECTED         = 0;                // Use this to evaluate whether a SELECT element has a Selected Option.
const SET_VALUE_TO_NULL                 = '[null]'          // Use this with UpdateAllFormFieldsByInternalName and UpdateFormFieldByInternalName to set the value of a Custom Field to NULL.
const SINGLE_CHARACTER                  = 1;                // Use this integer as the second argument to string.substring to extract a single character at the position given by the first argument.

const SPACE_CHARACTER                   = ' ';              // When you must know absolutely, positively, that it's a SPACE character, use this in its place.

const SPLIT_NAME_FROM_VALUE             = 2;                // Use this with string.split or, better yet, StringSplitSharp, to split a key and its value into two substrings.
const SPLIT_NAME_PART                   = 0;
const SPLIT_VALUE_PART                  = 1;

const SRC_IS_UNKNOWN                    = 0;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value is unknown.
const SRC_IS_QUERY_STRING               = 1;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was the value found in a like-named query string parameter.
const SRC_IS_HTTP_SESSION_VARIABLE      = 2;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was found in the HTTP Session variable maintained by the server
const SRC_IS_FORM_FIELD                 = 3;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was found in a form field.
const SRC_IS_LOCAL_STORAGE              = 4;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was obtained from a like-named key in localStorage.
const SRC_IS_SESSION_STORAGE            = 5;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was obtained from a like-named key in sessionStorage.
const SRC_IS_EXTERNALCRMID              = 6;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was obtained by looking up an ExternalCRMId in the lead table.
const SRC_IS_DATABASE                   = 7;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value was read from the database.
const SRC_IS_LOGIN_NAME_PER_URL         = 8;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value is derived from the Login Name.
const SRC_IS_LEAD_ID_PER_URL            = 9;                // Function GetParamValue sets global integer __intValueSource, so that the caller can store it to indicate that the value is derived from the Lead ID.
const SRC_IS_LLJS_HELPERS_SYNC          = 10;
const SRC_IS_BULLHORN_DEFAULT_LOGIN     = 11;
const SRC_IS_WORDS2ACTIONS_LOGIN        = 12;
const SRC_IS_SERVER_HTTP_CONTEXT        = 13;               // GetLoginIdOfLoggedInUser, called through DoAjax, returns this value from System.Web.HttpContext.Current.User.Identity.Name.
const SRC_IS_USERID_PER_URL             = 14;               // GetBasicSalesTalkUserInfo, called through DoAjax when only the userid is specified in the URL.
const SRC_IS_WISE_AGENT_TEAM_ROSTER     = 15;

const STT_VALIDITY_FLAG                 = 'InputIsValid';
const STT_INPUT_IS_VALID                = 'true';
const STT_INPUT_IS_INVALID              = 'false';
const STT_VALUE_TOKEN                   = '##Value##';
const STT_WHITEONSTOPLIGHTGREEN         = 'STT_WhiteOnStoplightGreen';
const STT_WHITEONSTOPLIGHTRED           = 'STT_WhiteOnStoplightRed';

const SUBSTRING_FIRST_CHAR              = 0;                // Use this as the first argument to string.substring to case it to start the returned substring at the beginning of the input string.
const SUBSTRING_LAST_CHARACTER          = 1;                // Subtract this from the length of a string, passing the remainder as the second argument to string.substring to indicate the last character of a string as the end point.
const SUBSTRING_SECOND_CHARACTER        = 1;                // Use this as the first argument to string.substring to cause it to start the returned substring at the second character of the input string.

const TAB_CHARACTER                     = '\x09';           // Use this token when you need the ASCII TAB character.
const TIME_SEPARATOR_CHAR               = ':';              // When you must know absolutely, positively, that it's an TIME SEPARATOR character, use this in its place.
const WINDOWS_DRIVE_DELIMITER           = TIME_SEPARATOR_CHAR;  // Alias
const UNDERSCORE_CHAR                   = '_';              // When you must know absolutely, positively, that it's an UNDERSCORE character, use this in its place.
const WINDOWS_PATH_SEPARATOR_CHAR       = '\\';             // This applies only to Windows path strings. See also PATH_SEPARATOR_CHAR.

//  The default (uninitialized) valoe is included so that it can
//  serve as a default value for the Behavior parameter that
//  replaces the <bold>ignoreWebConfig</bold> Boolean.
//  None = 0

//  Setting this value alone duplicates the behavior indicated by
//  the original <bold>ignoreWebConfig</bold> Boolean.
//  IgnoreWebConfig = 1

//  Setting this value bypasses checking domain 1000 when a domain
//  is specified and is devoid of the specified key.
//  IgnoreDomain1000 = 2

//  Set both flags, IgnoreWebConfig and IgnoreDomain1000
//  IgnoreBoth = 3

// Enum simulation in JavaScript.
const GetByMonikorFirstBehavior = Object.freeze({
    None             : 0,
    IgnoreWebConfig  : 1,
    IgnoreDomain1000 : 2,
    IgnoreBoth       : 3
});

//  ----------------------------------------------------------------------------
//  The strings in the following array identify the keys that may be safely
//  discarded from sessionStorage when the pagename value changes.
//  ----------------------------------------------------------------------------

const __SessionParams2Discard           = [
                                            'dbname',
                                            'domainid',
                                            'domainname',
                                            'externalcrmid',
                                            'leadid',
                                            'login',
                                            'mobile',
                                            'SysCRMLeadOrContact',
                                            'tenantid',
                                            'pagename',
                                            'userid',
                                          ];

const __CritcalParamNames               = [
                                            'externalcrmid',
                                            'leadid',
                                            'login',
                                            'pagename',
                                          ];

const __ClickEvents2Fire                = [
                                             'SummarizeTranscriptButton'
                                          ];

const __ValidDialerLoginSources         = [
                                              SRC_IS_QUERY_STRING,
                                              SRC_IS_LOCAL_STORAGE,
                                              SRC_IS_SERVER_HTTP_CONTEXT,
                                              SRC_IS_WORDS2ACTIONS_LOGIN,
                                              SRC_IS_USERID_PER_URL,
                                              SRC_IS_WISE_AGENT_TEAM_ROSTER
                                          ];

function CheckCriticalparams ( pastrCritcalParamNames , poParamsCollection )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  CheckCriticalparams

        Function Goal:  Check the values of input parameters indicative of the
                        need to re-initialize the session variables.

        Input:          pastrCritcalParamNames  = Array of strings each of which
                                                  is the name of a critical
                                                  input parameter that, if its
                                                  value changes, requires a
                                                  session reset.

                        poParamsCollection      = The opaque collection of query
                                                  string parameters returned as
                                                  the URLSearchParamsCI object

        Output:         Return TRUE to indicate the need to reset the session.

        Remarks:        If the session is deemed in need of a reset, critical
                        parameters are discarded from the sessionStorage list.
        ------------------------------------------------------------------------
    */

    const strMethodName                 = 'CheckCriticalparams';

    const intCriticalParamsCount        = pastrCritcalParamNames.length;

    var   strValuePerUrl                = null;
    var   strValuePerSession            = null;
    var   fUrlAndStorageAreTheSame      = false;

    if ( intCriticalParamsCount > ARRAY_IS_EMPTY )
    {
        var afSaneOrDifferent = [ ];

        for ( var intCurrentParamIndex = ARRAY_FIRST_ELEMENT;
                  intCurrentParamIndex < intCriticalParamsCount;
                  intCurrentParamIndex++ )
        {
            strValuePerUrl              = poParamsCollection.getCI ( pastrCritcalParamNames [ intCurrentParamIndex ] );
            strValuePerSession          = sessionStorage.getItem ( pastrCritcalParamNames [ intCurrentParamIndex ] );
            fUrlAndStorageAreTheSame    = ( strValuePerUrl === strValuePerSession );

            afSaneOrDifferent.push (
            {
                'strValuePerUrl'            : strValuePerUrl,
                'strValuePerSession'        : strValuePerSession ,
                'fUrlAndStorageAreTheSame'  : fUrlAndStorageAreTheSame
            } );
        }   // for ( var intCurrentParamIndex = ARRAY_FIRST_ELEMENT; intCurrentParamIndex < intCriticalParamsCount; intCurrentParamIndex++ )

        var intItemNumber               = NUMERIC_ZERO;
        var intNDeletedKeys             = NUMERIC_ZERO;

        for ( intCurrentParamIndex = ARRAY_FIRST_ELEMENT;
              intCurrentParamIndex < intCriticalParamsCount;
              intCurrentParamIndex++ )
        {
            if ( afSaneOrDifferent [ intCurrentParamIndex ].fUrlAndStorageAreTheSame )
            {
                continue;
            }   // TRUE (Values per the URL and the session are the same.) block, if ( afSaneOrDifferent [ intCurrentParamIndex ].fUrlAndStorageAreTheSame )
            else
            {
                Object.keys ( sessionStorage ).forEach ( ( key ) =>
                {
                    intItemNumber++;

                    if ( __SessionParams2Discard.find ( ( element ) => element === key ) )
                    {
                        intNDeletedKeys++;
                        sessionStorage.removeItem ( key );
                    }   // TRUE (The current key is on the kill list.) block, if ( __SessionParams2Discard.find ( ( element ) => element === key ) )
                    else
                    {
                      console.log ( 'CheckCriticalparams:    Key # ' + intItemNumber + ': ' + key + ' PRESERVED' );
                    }   // FALSE (The current key will be spared.) block, if ( __SessionParams2Discard.find ( ( element ) => element === key ) )
                });

                return ( intNDeletedKeys > NUMERIC_ZERO );
            }   // FALSE (Values per the URL and the session differ.) block, if ( afSaneOrDifferent [ intCurrentParamIndex ].fUrlAndStorageAreTheSame )
        }   // for ( intCurrentParamIndex = ARRAY_FIRST_ELEMENT; intCurrentParamIndex < intCriticalParamsCount; intCurrentParamIndex++ )

        return false;
    }   // TRUE (anticipated outcome) block, if ( intCriticalParamsCount > ARRAY_IS_EMPTY )
    else
    {
        return false;
    }   // FALSE (unanticipated outcome) block, if ( intCriticalParamsCount > ARRAY_IS_EMPTY )
}   // function CheckCriticalparams = ( pastrCritcalParamNames , poParamsCollection ) =>


function ClearSessionIfNewPage ( )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  ClearSessionIfNewPage

        Function Goal:  If this instance of LLCommon is loading into a new page,
                        remove everything from sessionStorage so that the values
                        can be evaluated anew in the context of the new URL.

        Input:          This function relies upon a private key that it sets in
                        sessionStorage each time it runs to determine whether to
                        wipe sessionStorage clean, and the properties on the
                        global location object.

        Output:         The return value is a short string that is recorded in
                        the console log to indicate its action.
        ------------------------------------------------------------------------
    */

    const strMethodName     = 'ClearSessionIfNewPage';

    const CURR_ORIGIN_PAGE  = 'CurrOriginPage';
    const strCurrOriginPage = location.href.substring ( location.origin.length );
    const strSessOriginPage = sessionStorage.getItem ( CURR_ORIGIN_PAGE );

    if ( strSessOriginPage === null )
    {
        sessionStorage.setItem ( CURR_ORIGIN_PAGE , strCurrOriginPage );
        return 'The session appears to be new.';
    }   // TRUE (The session appears to be a new one.) block, if ( strSessOriginPage === null )
    else
    {
        if ( strCurrOriginPage === strSessOriginPage )
        {
            return 'The session context is unchanged, suggesting a refresh operation. Session storage is preserved.';
        }   // TRUE (The session context is unchanged because the current action is a page refresh.) block, if ( strCurrOriginPage === strSessOriginPage )
        else
        {
            sessionStorage.clear ( );
            sessionStorage.setItem ( CURR_ORIGIN_PAGE , strCurrOriginPage );
            return 'The session context is changed, suggesting a page navigation. Session storage is being discarded.';
        }   // FALSE (The session context has changed due to a page navigation. Discard session storage.) block, if ( strCurrOriginPage === strSessOriginPage )
    }   // FALSE (The session is an established one that just refreshed or navigated.) block, if ( strSessOriginPage === null )
}   // function ClearSessionIfNewPage


function GetParamValue ( pstrParamName , poParamsCollection , pfExpectingAnInteger )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  GetParamValue

        Function Goal:  Get the value, if any, associated with the parameter
                        identified by string pstrParamName. The first of two or
                        more passes gets the value from the query string, while
                        subsequent passes get it from a sessionStorage key.

        Input:          pstrParamName           = Name of parameter, which maps
                                                  to either a query string param
                                                  or a sessionStorage key

                        poParamsCollection      = The opaque collection of query
                                                  string parameters returned as
                                                  the URLSearchParamsCI object

                        pfExpectingAnInteger    = Optional Boolean flag that, if
                                                  TRUE, signifies that the call
                                                  is expected to return an INT.

        Output:         Unless the value associated with pstrParamValue as a
                        query string parameter or the sessionStorage key that is
                        the value stored therein from the initial processing of
                        a query string is NULL, that value is returned.

                        Otherwise, the return value is NULL unless argument
                        pfExpectingAnInteger is TRUE, in which case the return
                        value is either the integer representation of the named
                        key or zero if the value would otherwise be NULL.
        ------------------------------------------------------------------------
    */

    const strMethodName                 = 'GetParamValue';

    if ( typeof pstrParamName === 'string' || pstrParamName instanceof String )
    {
        var intParamValue               = NUMERIC_ZERO;
        var intValueCandidate           = NUMERIC_ZERO;
        var strParamValue               = poParamsCollection.getCI ( pstrParamName );

        __intValueSource                = strParamValue === null ? SRC_IS_UNKNOWN : SRC_IS_QUERY_STRING;

        if ( pstrParamName === 'login' && strParamValue !== null && strParamValue.indexOf ( SPACE_CHARACTER ) > INDEXOF_NOT_FOUND )
        {
            strParamValue               = strParamValue.replace ( SPACE_CHARACTER , '+' );
        }   // if ( pstrParamName === 'login' && strParamValue !== null && strParamValue.indexOf ( SPACE_CHARACTER ) > INDEXOF_NOT_FOUND )

        if ( pfExpectingAnInteger )
        {
            intValueCandidate           = parseInt ( strParamValue );
            intParamValue               = Number.isNaN ( intValueCandidate ) ? NUMERIC_ZERO : intValueCandidate;
        }   // xpectingAnInteger )

        if ( strParamValue === null )
        {
            strParamValue               = sessionStorage.getItem ( pstrParamName );
            __intValueSource            = strParamValue === null ? ( __intValueSource ) : SRC_IS_SESSION_STORAGE;

            if ( pfExpectingAnInteger )
            {
                intValueCandidate       = parseInt ( strParamValue );
                return Number.isNaN ( intValueCandidate ) ? NUMERIC_ZERO : intValueCandidate;
            }   // TRUE (The caller expects an integer.) block, if ( pfExpectingAnInteger )
            else
            {
                return pfExpectingAnInteger ? intParamValue : strParamValue;
            }   // FALSE (The caller expects a string.) block, if ( pfExpectingAnInteger )
        }   // TRUE (Since strParamValue is null, return whatever is in sessionStorage, which may also be null.) block, if ( strParamValue === null )
        else
        {
            sessionStorage.setItem ( pstrParamName , strParamValue );

            return pfExpectingAnInteger ? intParamValue : strParamValue;
        }   // FALSE (Since strParamValue is NOT null, save its value into sessionStorage, then return it.) block, if ( strParamValue === null )
    }   // TRUE (anticipated outcome) block, if ( typeof pstrParamName === 'string' || pstrParamName instanceof String )
    else
    {   // Since __intValueSource is initialized to SRC_IS_UNKNOWN, its value is already correct.
        return pfExpectingAnInteger ? NUMERIC_ZERO : null;
    }   // FALSE (unanticipated outcome) block, if ( typeof pstrParamName === 'string' || pstrParamName instanceof String )
}   // function GetParamValue ( pstrParamName , poParamsCollection , pfExpectingAnInteger ) =>


function HostIsPurl ( pstrHostName )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  HostIsPurl

        Function Goal:  Return Boolean True when pstrHostName is a string that
                        specifies the name of the PURL host.

        Input:          pstrHostName            = Optional String that contains
                                                  a hostname string to evaluate

        Output:         Return Boolean True when pstrHostName is a String that
                        represents a PURL hostname. Otherwise, return boolean
                        False.

                        When pstrHostName is omitted or evaluates to anything
                        but a String, evaluate document.location.hostname.
        ------------------------------------------------------------------------
    */

    const PURL_PREFIX           = 'purl.';
    const PURL_PREFIX_LENGTH    = PURL_PREFIX.length;

    const strHostName2Test      = typeof pstrHostName === 'string' || pstrHostName instanceof String ? pstrHostName : document.location.hostname;
    return ( strHostName2Test.substring ( SUBSTRING_FIRST_CHAR , PURL_PREFIX_LENGTH ).toLowerCase ( ) === PURL_PREFIX );
}   // function HostIsPurl ( pstrHostName ) =>


function URLSearchParamsCI ( QueryString )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  URLSearchParamsCI

        Function Goal:  Construct a fully case insensitive implementation of the
                        URLSearchParams interface.

        Input:          QueryString = OPTIONAL queery string or other string
                                      composed of key/value pairs delimited in
                                      the same way

        Output:         The constructor returns a wrapper around a new instance
                        of URLSearchParams that contains the whole collection of
                        parameters captured from the query string as lower case
                        strings.

                        Since the object stores the URLSearchParams object, it
                        supports all of its native methods, in addition to the
                        case insensitive getCI method, which converts its input
                        to lower case.

        Remarks:        Since its original intent was to support the querystring
                        segment of a URI, the parameter is optional, and its
                        default value is document.location.search.
        ------------------------------------------------------------------------
    */

    const getUrlVars = ( ) =>
    {
        //  --------------------------------------------------------------------
        //  This private method initializes and populates the case-insensitive
        //  URLSearchParams class, discarding the original case-sensitive class.
        //  --------------------------------------------------------------------

        const paramsCS = new URLSearchParams ( typeof QueryString === 'string' || QueryString instanceof String ? QueryString : document.location.search );
        const paramsCI = new URLSearchParams ( );

        for ( const [ name, value ] of paramsCS )
        {
            paramsCI.append ( name.toLowerCase ( ), value );
        }   // for ( const [ name, value ] of params )

        return paramsCI;
    }   // const getUrlVars = ( ) =>

    this.urlVars = getUrlVars ( );
}   // function URLSearchParamsCI ( )


URLSearchParamsCI.prototype.getCI = function ( key )
{
    //  ------------------------------------------------------------------------
    //  This method is the case insensitive getter method that otherwise behaves
    //  just like the get method on the underlying URLSearchParams object.
    //  ------------------------------------------------------------------------

    return this.urlVars.get ( key.toLowerCase ( ) );
}   // URLSearchParamsCI.prototype.getCI = function ( key )


/**
 * ToastFactory - creates and manages queued toast notifications.
 *
 * ## Usage Examples
 *
 * Configure global defaults:
 * ```js
 * ToastFactory.configure({
 *   cap: 20,
 *   capStrategy: "dropNewest",
 *   type: "slideDown",
 *   fontFamily: "Verdana",
 *   fontSize: "16px",
 *   color: "#222",
 *   backgroundColor: "#004080",
 *   altBackgroundColor: "#e6f0ff",
 *   altColor: "#002244",
 *   border: "2px solid #0066cc",
 *   alternateScheme: true,
 *   positionX: "center",
 *   positionY: "bottom"
 * });
 * ```
 *
 * Create a toast with current defaults:
 * ```js
 * ToastFactory.show("Hello world!");
 * ```
 *
 * Override specific properties at call time:
 * ```js
 * ToastFactory.show("Custom toast", {
 *   type: "fade",
 *   backgroundColor: "#0066cc",
 *   color: "#fff"
 * });
 * ```
 *
 * @property {string} positionX - Horizontal position of container ('left','center','right'). Default: 'right'.
 * @property {string} positionY - Vertical position of container ('top','bottom'). Default: 'top'.
 * @property {number} cap - Maximum queue length. Default: 10.
 * @property {string} capStrategy - Strategy when cap is exceeded ('dropOldest' or 'dropNewest'). Default: 'dropOldest'.
 * @property {string} type - Animation type ('fade','slideDown','slideRight','slideUp'). Default: 'slideRight'.
 * @property {string} fontFamily - Font face for toast text. Default: 'sans-serif'.
 * @property {string} fontSize - Font size (CSS units). Default: '14px'.
 * @property {string} color - Foreground text color. Default: '#fff'.
 * @property {string} backgroundColor - Background color. Default: '#333'.
 * @property {string} altColor - Alternate foreground color. Default: '#222'.
 * @property {string} altBackgroundColor - Alternate background color. Default: '#eee'.
 * @property {string} border - Optional border CSS (e.g., '2px solid #666'). Default: ''.
 * @property {boolean} alternateScheme - If true, alternates between default and alt scheme. Default: false.
 */
const ToastFactory = {
    defaults: {
      positionX                 : 'right',
      positionY                 : 'top',
      cap                       : 10,
      capStrategy               : 'dropOldest',
      type                      : 'slideRight',
      fontFamily                : 'sans-serif',
      fontSize                  : '14px',
      color                     : '#fff',
      backgroundColor           : '#333',
      altColor                  : '#222',
      altBackgroundColor        : '#eee',
      border                    : EMPTY_STRING,
      alternateScheme           : false
    },
    queues: { },
    active: { },

    /**
     * Configure global runtime defaults for all properties.
     * @param {object} options - Properties to override in defaults.
     */
    configure ( options = { } )
    {
        this.defaults = { ...this.defaults, ...options };
    },  // configure method

    /**
     * Create a toast with optional overrides.
     * @param {string} message - The message text.
     * @param {object} [options] - Override options.
     */
    show ( message, options = { } )
    {
        const settings          = { ...this.defaults, ...options };
        const type              = settings.type;

        if ( !this.queues [ type ] ) this.queues [ type ] = [ ];
        if ( !this.active [ type ] ) this.active [ type ] = false;

        // Enforce cap with configurable strategy.
        if ( this.queues [ type ].length >= settings.cap )
        {
            if ( settings.capStrategy === 'dropNewest' )
            {
                return;                                     // Ignore new toast.
            }   // TRUE (Drop strategy is `dropNewest`, not the default.) block, if ( settings.capStrategy === 'dropNewest' )
            else
            {
                this.queues [ type ].shift ( );             // Drop oldest.
            }   // FALSE (Drop strategy is the default, `dropOldest`.) block, if ( settings.capStrategy === 'dropNewest' )
        }   // if ( this.queues [ type ].length >= settings.cap )

        this.queues [ type ].push ( { message, settings } );
        if ( !this.active [ type ] ) this._processQueue ( type );
    },  // show method


    _processQueue ( type )
    {
        if ( this.queues [ type ].length === ARRAY_IS_EMPTY )
        {
          this.active [ type ]          = false;
          return;
        }   // if ( this.queues [ type ].length === ARRAY_IS_EMPTY )

        this.active[type]               = true;
        const { message, settings }     = this.queues [ type ].shift ( );

        this._showToast ( message,
                          settings,
                          () => this._processQueue ( type ) );
    },  // _processQueue

    _showToast ( message, settings, callback )
    {
        const containerId               = `STT_${settings.type}Container`;
        let container                   = document.getElementById(containerId);

        if ( !container )
        {
            container                   = document.createElement ( 'div' );
            container.id                = containerId;
            container.className         = `STT_toast_container STT_${settings.type}_container`;

            document.body.appendChild ( container );
        }   // if ( !container )

        const toast                     = document.createElement ( 'div' );
        toast.className                 = `STT_toast STT_${settings.type}`;
        toast.textContent               = message;

        toast.setAttribute ( 'role', 'alert' );

        // apply style overrides
        toast.style.fontFamily          = settings.fontFamily;
        toast.style.fontSize            = settings.fontSize;
        toast.style.color               = settings.color;
        toast.style.backgroundColor     = settings.backgroundColor;

        if ( settings.border )
        {
            toast.style.border          = settings.border;
        }   // if ( settings.border )

        // alternate scheme
        if ( settings.alternateScheme && container.childElementCount % 2 === 1)
        {
          toast.style.color             = settings.altColor           || this._deriveAltColor ( settings.color );
          toast.style.backgroundColor   = settings.altBackgroundColor || this._deriveAltColor ( settings.backgroundColor );

          toast.classList.add ( 'STT_toast-alt' );
        }   // if ( settings.alternateScheme && container.childElementCount % 2 === 1)

        container.appendChild ( toast );

        setTimeout ( ( ) => toast.classList.add ( 'STT_show' ), 50 );

        setTimeout(() =>
        {
            toast.classList.remove('STT_show');
            setTimeout ( ( ) =>
            {
                if ( container.contains ( toast ) ) container.removeChild ( toast );
                callback ( );
            }, 500 );   // INNER setTimeout ( ( ) =>
          }, 3000 );    // OUTER setTimeout ( ( ) =>
    },  // _showToast

    /**
     * Simple fallback to derive alternate colors if none provided.
     * Lightens/darkens hex by fixed amount.
     */
    _deriveAltColor ( hex, amount = 40 )
    {
        try
        {
            let col = parseInt ( hex.slice ( 1 ), 16 );
            let r = Math.min ( 255, Math.max ( 0, ( col >> 16 )  + amount ) );
            let g = Math.min ( 255, Math.max ( 0, ( ( col >> 8 ) & 0xFF ) + amount ) );
            let b = Math.min ( 255, Math.max ( 0, ( col & 0xFF ) + amount ) );
            return '#' + ( r << 16 | g << 8 | b ).toString ( 16 ).padStart ( 6, '0' );
        } catch {
            return hex; // fallback: return original
        }
    }   // _deriveAltColor(hex, amount = 40)
};  // const ToastFactory


/**
 * Utility for managing a list of event handler functions with
 * identity-based duplicate prevention and safe invocation semantics.
 *
 * Each EventHandlerList instance maintains its own Set of handlers. Adding the
 * same function reference more than once has no effect, preserving the invariant
 * that a handler is invoked at most once per notification cycle.
 *
 * Handler invocation is resilient: errors thrown by one handler are caught and
 * logged, and do not prevent subsequent handlers from running. This ensures that
 * one faulty handler cannot disrupt the overall event pipeline.
 *
 * The optional identifier string is used for diagnostics, logging, and
 * debugging, allowing callers to distinguish between EventHandlerList
 * instances.
 *
 * Unless otherwise noted, everything is logged to the console and, via AJAX
 * function call to the server.
 *
 * @constructor
 * @param {string} pstrListId - Identifier used for diagnostics and logging.
 * @returns {object} An EventHandlerList instance exposing:
 *   - register ( fn, pstrLabel ):   Register a handler. Optional label is
 *                                   for log. Returns true if added, false
 *                                   if duplicate or pfn is not a function.
 *   - unRegister ( fn, pstrLabel ): Unregister a handler. Optional label
 *                                   is for log. Returns true if removed.
 *   - isRegistered ( fn ):          Return true if the handler is
 *                                   registered. Nothing ls logged.
 *   - count ( ):                    Returns the number of registered
 *                                   handlers. Nothing is logged.
 *   - invokeAll ( ...args ):        Invokes all handlers with the given
 *                                   arguments, isolating errors.
 *   - enumerate ( ):                Returns an array of the registered
 *                                   handlers. Nothing is logged.
 *   - id ( ):                       Returns the identifier associated with
 *                                   this instance. Nothing is logged.
 */
class EventHandlerList
{
    #functionName ( pfn )
    {
        return pfn.id || '(anonymous)';
    }   // #functionName

    #ObjectName;
    #instanceLabel;
    #handlers;

    constructor ( pstrListLabel )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
        debugger;
        this.#ObjectName    = strMethodName.startsWith ( 'new' ) ? strMethodName.substring ( 4 ) : strMethodName;
        this.#instanceLabel = pstrListLabel || '(unnamed EventList)';
        this.#handlers      = new Set ( );
    }  // constructor method

    register ( pfn , pstrLabel )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
        debugger;

        if ( typeof pfn !== 'function' )
        {
            LLCommon.LogException ( strMethodName + ': Exception - ' + this.#ObjectName + SPACE_CHARACTER + '- expected a function, got ' + ( typeof pfn ) );
            return false;
        }   // if ( typeof pfn !== 'function' )

        const intBeforeCount = this.#handlers.size;
        this.#handlers.add ( pfn );
        const intAfterCount = this.#handlers.size;

        if ( intAfterCount === intBeforeCount )
        {   // Report cause of failure on the way out.
            LLCommon.LogException (   strMethodName
                                    + ': Exception - '
                                    + this.#ObjectName
                                    + SPACE_CHARACTER
                                    + this.#instanceLabel
                                    + ': ignoring duplicate handler, Name = '
                                    + this.#functionName ( pfn ) );
            return false;
        }   // if ( intAfterCount === intBeforeCount )

        LLCommon.Trace (   this.#ObjectName
                         + SPACE_CHARACTER
                         + this.#instanceLabel
                         + ': Handler = '
                         + this.#functionName ( pfn ) ,
                         ( pstrLabel || '(anonymous)' ) );
        return true;
    }  // register method

    unRegister ( pfn , pstrLabel )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
        debugger;
        LLCommon.Trace (   this.#ObjectName
                         + SPACE_CHARACTER
                         + this.#instanceLabel
                         + WINDOWS_DRIVE_DELIMITER
                         + SPACE_CHARACTER
                         + this.#functionName ( pfn ) ,
                         ( pstrLabel || '(anonymous)' ) );
        return this.#handlers.delete ( pfn );
    }  // unRegister method

    isRegistered ( pfn )
    {
        return this.#handlers.has ( pfn );
    }  // isRegistered method

    count ( )
    {
        return this.#handlers.size;
    }  // count method

    invokeAll ( ...args )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
        debugger;

        LLCommon.Trace ( `${this.#ObjectName} ${this.#instanceLabel}: invokeAll with arguments as follows: ${LLCommon.enumerateRestParameters ( ...args )}` , `${this.#ObjectName} ${this.#instanceLabel}` );
        LLCommon.Trace ( `${this.#ObjectName} ${this.#instanceLabel}: invokeAll Handler Count = ${this.#handlers.size}` , `${this.#ObjectName} ${this.#instanceLabel}` );
        let intIndex = NUMERIC_ZERO;

        this.#handlers.forEach ( thisHandler =>
        {
            try
            {
                LLCommon.Trace ( `${this.#ObjectName}, ${this.#instanceLabel}: invokeAll iteration # ${++intIndex} of ${this.#handlers.size}: function ${this.#functionName ( thisHandler )} Begin` , `${this.#ObjectName} ${this.#instanceLabel}` );
                thisHandler ( ...args );
                LLCommon.Trace ( `${this.#ObjectName}, ${this.#instanceLabel}: invokeAll iteration # ${intIndex} of ${this.#handlers.size}: function ${this.#functionName ( thisHandler )} Done`    , `${this.#ObjectName} ${this.#instanceLabel}` );
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex, `${this.#ObjectName} ${this.#instanceLabel}: Error in handler ${this.#functionName ( thisHandler )} of ${this.#ObjectName}, ${this.#instanceLabel}: Message = ${ex.message}` );
            }
        });
    }  // invokeAll method

    enumerate ( )
    {
        return Array.from ( this.#handlers );
    }  // enumerate method

    id ( )
    {
        return this.#instanceLabel;
    }   // id method
}   // EventHandlerList


//  ----------------------------------------------------------------------------
//  Since availability of data from the Storage API is independent of the
//  readiness state of the document, sessionStorage can be tested for saved
//  _leadid and _leadidSource values at any time. Testing inline at script load
//  time guarantees that persisted values are recovered during a page refreash.
//  ----------------------------------------------------------------------------

const paramsCollection                  = new URLSearchParamsCI ( );

var   _fDomainAndTenantIDAreSafe        = false;
var   __intValueSource                  = SRC_IS_UNKNOWN

//  ----------------------------------------------------------------------------
//  Define a new _pagename and its conjoined twin, _pagenameSource, to generate
//  forms dynamically. This block of code has expanded far beyond the intent set
//  forth by the foregoing sentence. It now populates pairs of variables for all
//  commonly specified URL parameters.
//
//  As of Wednesday, 30 July 2025, it begins by clearing sessionStorage when the
//  pageName of the current page changes. We do this because everything that was
//  in sessionStorage must be re-evaluated in the context of a new page and the
//  query string appended to its URI.
//  ----------------------------------------------------------------------------

console.log ( LLCommon_SCRIPTSOURCE + SPACE_CHARACTER + ClearSessionIfNewPage ( ) );

const _fSessionWasReset                 = CheckCriticalparams ( __CritcalParamNames , paramsCollection );

var   _pagename                         = GetParamValue ( 'pagename' , paramsCollection );
var   _pagenameSource                   = __intValueSource;

var   _IsMobilePage                     = GetParamValue ( 'mobile' , paramsCollection ) === 'true' ? true : false;
var   _IsMobilePageSource               = __intValueSource;

var   _leadid                           = GetParamValue ( 'leadid' , paramsCollection , EXPECTING_AN_INTEGER_VALUE );
var   _leadidSource                     = __intValueSource;

var   _dbname                           = GetParamValue ( 'dbname' , paramsCollection );
var   _dbnameSource                     = __intValueSource;

var   _externalcrmid                    = GetParamValue ( 'externalcrmid' , paramsCollection );
var   _externalcrmidSource              = __intValueSource;

var   _SysCRMLeadOrContact              = GetParamValue ( 'SysCRMLeadOrContact' , paramsCollection );
var   _SysCRMLeadOrContactSource        = __intValueSource;

var   _CRM                              = GetParamValue ( 'CRM' , paramsCollection );
var   _CRMSource                        = __intValueSource;

var   _EntityType                       = GetParamValue ( 'EntityType' , paramsCollection );
var   _EntityTypeSource                 = __intValueSource;

var   _CI                               = GetParamValue ( 'CI' , paramsCollection );
var   _CISource                         = __intValueSource;

var   _CorporationID                    = GetParamValue ( 'CorporationID' , paramsCollection );
var   _CorporationIDSource              = __intValueSource;

var   _W2AButton                        = GetParamValue ( 'W2AButton' , paramsCollection ) === 'true' ? true : false;
var   _W2AButtonSource                  = __intValueSource;

var   _CRMInteractionButton             = GetParamValue ( 'CRMButton' , paramsCollection ) === 'true' ? true : false;
var   _W2CRMInteractionButtonSource     = __intValueSource;

var   _fPickListValidatorOff            = GetParamValue ( 'PickListValidatorOff' , paramsCollection ) === 'true' ? true : false;
var   _fPickListValidatorOffSource      = __intValueSource;

//  ----------------------------------------------------------------------------
//  Since these must have global scope, they are initialized here.
//  ----------------------------------------------------------------------------

var   _domainid                         = NUMERIC_ZERO;
var   _domainidSource                   = SRC_IS_UNKNOWN;

var   _domainname                       = null;
var   _domainnameSource                 = SRC_IS_UNKNOWN;

var   _login                            = null;
var   _loginSource                      = SRC_IS_UNKNOWN;

var   _tenantid                         = NUMERIC_ZERO;
var   _tenantidSource                   = SRC_IS_UNKNOWN;

var   _userid                           = NUMERIC_ZERO;
var   _useridSource                     = SRC_IS_UNKNOWN;

//  PageName should be WiseAgentPropertySearch
//  bud pass  to  Everyone 19:03
//  WiseAgentPropertySearch
//  EntityType=PropertySearchCriteria
//  pagename=WiseAgentPage
//  bud pass  to  Everyone 19:24
//  https://salestalktech.com/SalesAcceleration/COMMON/Words2Actions_Form_TEMPLATE.HTML?pagename=WiseAgentPage&CI=True&login=w2a4wiseagent@salestalk.ai&leadid=1408965&CRM=WiseAgent&EntityType=PropertySearchCriteriawhen EntityType is PropertySearchCriteria AND pagename=WiseAgentPage
//  https://salestalktech.com/SalesAcceleration/COMMON/STAGING/Words2Actions_Form_TEMPLATE.HTML?pagename=WiseAgentPage&CI=True&login=w2a4wiseagent@salestalk.ai&leadid=1408965&CRM=WiseAgent&EntityType=PropertySearchCriteriawhen EntityType is PropertySearchCriteria AND pagename=WiseAgentPage

// bud pass  to  Everyone 19:03
// WiseAgentPropertySearch
// EntityType=PropertySearchCriteria
// pagename=WiseAgentPage

// bud pass  to  Everyone 19:24
// https://salestalktech.com/SalesAcceleration/COMMON/Words2Actions_Form_TEMPLATE.HTML?pagename=WiseAgentPage&CI=True&login=w2a4wiseagent@salestalk.ai&leadid=1408965&CRM=WiseAgent&EntityType=PropertySearchCriteria

debugger;

if ( _pagenameSource !== SRC_IS_UNKNOWN && _EntityTypeSource !== SRC_IS_UNKNOWN && _pagename === 'WiseAgentPage' && _EntityType === 'PropertySearchCriteria' )
{
    _pagename = 'WiseAgentPropertySearch';
}

var   _llAppPath                        = DEFAULT_DB_NAME;

try
{
    if ( HostIsPurl ( ) )
    {
        _llAppPath                      = _dbnameSource === SRC_IS_UNKNOWN ? DEFAULT_DB_NAME : PATH_SEPARATOR_CHAR + _dbname;
    }
    else if ( document.location.hostname.substring ( SUBSTRING_FIRST_CHAR , 9 ).toLowerCase ( ) === 'localhost' )
    {
        // 2024/04/29 14:06:05 - DAGray - Omit the database name when the host is localhost.
        _llAppPath                      = document.location.hostname + ':' + document.location.port;
    }
    else if ( document.location.hostname.toLowerCase ( ) === 'salestalk' )
    {
        _llAppPath                      = _dbnameSource === SRC_IS_UNKNOWN ? ALTERNATE_DB_NAME : PATH_SEPARATOR_CHAR + _dbname;
    }
    else if ( document.location.href.substring ( SUBSTRING_FIRST_CHAR , document.location.href.indexOf ( QUERY_STRING_START_DELIMITER ) ).toLowerCase ( ).indexOf ( '/repository/' ) > INDEXOF_NOT_FOUND )
    {
        _llAppPath                      = _dbnameSource === SRC_IS_UNKNOWN ? DEFAULT_DB_NAME : PATH_SEPARATOR_CHAR + _dbname;
    }
    else if ( document.location.protocol.toLowerCase ( ) === 'file:' )
    {
        _llAppPath                      = _dbnameSource === SRC_IS_UNKNOWN ? DEFAULT_DB_NAME : PATH_SEPARATOR_CHAR + _dbname;
    }
    else
    {
        const astrLocHrefSegs           = document.location.href.split ( PATH_SEPARATOR_CHAR );
        _llAppPath                      = PATH_SEPARATOR_CHAR + astrLocHrefSegs [ ARRAY_FOURTH_ELEMENT ];
    }
}
catch ( ex )
{
    console.log ( 'Exception caught: Message = ' + ex.message + ', Stack =' + ex.stack );
}

_llAppPath += PATH_SEPARATOR_CHAR;

const pathName                          = document.location.pathname.replace ( /\/[0-9]+$/ , EMPTY_STRING ).replace ( /^\//, EMPTY_STRING );

const __GlobalParameterSourceMap    = [ 'UNKNOWN' , 'QueryString' , 'HTTPSEssionVariable' , 'FormField' , 'LocalStorage' , 'SessionStorage' , 'ExternalCRMId' , 'Database' , 'LoginName' , 'LeadId' , 'LeadLifeJSHelpers Sync' , 'Bullhorn Default Login' , 'Words2Actions Login' , 'Login ID per Server' , 'Login ID per UserID alone via URL' , 'Wise Agent Team Roster Lookup' ];
const DisplayGlobalParameterSource  = ( pintSourceIndex ) => pintSourceIndex > ARRAY_INVALID_INDEX && pintSourceIndex < __GlobalParameterSourceMap.length ? __GlobalParameterSourceMap [ pintSourceIndex ] : 'ERROR - value of ' + pintSourceIndex + ' is an Invalid value.';


function ShowGlobalVars ( pstrScriptPhase )
{
    const strMethodName                 = 'ShowGlobalVars';

    const OrdinalFromIndex              = ( pintIndex ) => pintIndex + ARRAY_NEXT_ELEMENT;
    debugger;

    const strLogMessagePrefix           = LLCommon_SCRIPTSOURCE + SPACE_CHARACTER + pstrScriptPhase + ': ';

    console.log ( strLogMessagePrefix + '_fDomainAndTenantIDAreSafe     = ' + _fDomainAndTenantIDAreSafe );
    console.log ( strLogMessagePrefix + '_fSessionWasReset              = ' + _fSessionWasReset );
    console.log ( strLogMessagePrefix + 'Unedited Query String          = ' + location.search );

    console.log ( strLogMessagePrefix + '_IsMobilePage                  = ' + _IsMobilePage );
    console.log ( strLogMessagePrefix + '_IsMobilePageSource            = ' + _IsMobilePageSource        + ' (' + DisplayGlobalParameterSource ( _IsMobilePageSource )              + ')' );

    console.log ( strLogMessagePrefix + '_leadid                        = ' + _leadid );
    console.log ( strLogMessagePrefix + '_leadidSource                  = ' + _leadidSource              + ' (' + DisplayGlobalParameterSource ( _leadidSource )                    + ')' );

    console.log ( strLogMessagePrefix + '_CRM                           = ' + _CRM );
    console.log ( strLogMessagePrefix + '_CRMSource                     = ' + _CRMSource                 + ' (' + DisplayGlobalParameterSource ( _CRMSource )                       + ')' );

    console.log ( strLogMessagePrefix + '_EntityType                    = ' + _EntityType );
    console.log ( strLogMessagePrefix + '_EntityTypeSource              = ' + _EntityTypeSource          + ' (' + DisplayGlobalParameterSource ( _EntityTypeSource )                + ')' );

    console.log ( strLogMessagePrefix + '_CI                            = ' + _CI );
    console.log ( strLogMessagePrefix + '_CISource                      = ' + _CISource                  + ' (' + DisplayGlobalParameterSource ( _CISource )                        + ')' );

    console.log ( strLogMessagePrefix + '_CorporationID                 = ' + _CorporationID );
    console.log ( strLogMessagePrefix + '_CorporationIDSource           = ' + _CorporationIDSource       + ' (' + DisplayGlobalParameterSource ( _CorporationIDSource )             + ')' );

    console.log ( strLogMessagePrefix + '_W2AButton                     = ' + _W2AButton );
    console.log ( strLogMessagePrefix + '_W2AButtonSource               = ' + _W2AButtonSource          + ' (' + DisplayGlobalParameterSource ( _W2AButtonSource )                  + ')' );

    console.log ( strLogMessagePrefix + '_CRMInteractionButton          = ' + _CRMInteractionButton );
    console.log ( strLogMessagePrefix + '_W2CRMInteractionButtonSource  = ' + _W2CRMInteractionButtonSource + ' (' + DisplayGlobalParameterSource ( _W2CRMInteractionButtonSource ) + ')' );

    console.log ( strLogMessagePrefix + '_fPickListValidatorOff         = ' + _fPickListValidatorOff );
    console.log ( strLogMessagePrefix + '_fPickListValidatorOffSource   = ' + _fPickListValidatorOffSource + ' (' + DisplayGlobalParameterSource ( _fPickListValidatorOffSource )   + ')' );

    console.log ( strLogMessagePrefix + '_dbname                        = ' + _dbname );
    console.log ( strLogMessagePrefix + '_dbnameSource                  = ' + _dbnameSource              + ' (' + DisplayGlobalParameterSource ( _dbnameSource )              + ')' );

    console.log ( strLogMessagePrefix + '_externalcrmid                 = ' + _externalcrmid );
    console.log ( strLogMessagePrefix + '_externalcrmidSource           = ' + _externalcrmidSource       + ' (' + DisplayGlobalParameterSource ( _externalcrmidSource )       + ')' );

    console.log ( strLogMessagePrefix + '_SysCRMLeadOrContact           = ' + _SysCRMLeadOrContact );
    console.log ( strLogMessagePrefix + '_SysCRMLeadOrContactSource     = ' + _SysCRMLeadOrContactSource + ' (' + DisplayGlobalParameterSource ( _SysCRMLeadOrContactSource ) + ')' );

    console.log ( strLogMessagePrefix + '_pagename                      = ' + _pagename );
    console.log ( strLogMessagePrefix + '_pagenameSource                = ' + _pagenameSource            + ' (' + DisplayGlobalParameterSource ( _pagenameSource )            + ')' );

    if ( pstrScriptPhase === 'EndOfDocumentReady' )
    {   // Initialization of these values is deferred until the DOMContentLoaded event fires.
        console.log ( strLogMessagePrefix + '_domainname                    = ' + _domainname );
        console.log ( strLogMessagePrefix + '_domainnameSource              = ' + _domainnameSource          + ' (' + DisplayGlobalParameterSource ( _domainnameSource )          + ')' );

        console.log ( strLogMessagePrefix + '_domainid                      = ' + _domainid );
        console.log ( strLogMessagePrefix + '_domainidSource                = ' + _domainidSource            + ' (' + DisplayGlobalParameterSource ( _domainidSource )            + ')' );

        console.log ( strLogMessagePrefix + '_tenantid                      = ' + _tenantid );
        console.log ( strLogMessagePrefix + '_tenantidSource                = ' + _tenantidSource            + ' (' + DisplayGlobalParameterSource ( _tenantidSource )            + ')' );

        console.log ( strLogMessagePrefix + '_userid                        = ' + _userid );
        console.log ( strLogMessagePrefix + '_useridSource                  = ' + _useridSource              + ' (' + DisplayGlobalParameterSource ( _useridSource )              + ')' );

        console.log ( strLogMessagePrefix + '_login                         = ' + _login );
        console.log ( strLogMessagePrefix + '_loginSource                   = ' + _loginSource               + ' (' + DisplayGlobalParameterSource ( _loginSource )               + ')' );

        //  --------------------------------------------------------------------
        //  The values stored in these LLCommon properties duplicate values
        //  stored in individual global variables, each paired with an integer
        //  that specifies how its value was derived. Since the eventual goal is
        //  elimination of the globals in favor of these, new code should rely
        //  on these values.
        //  --------------------------------------------------------------------

        console.log ( strLogMessagePrefix + 'LLCommon.LeadId                = ' + LLCommon.LeadId );
        console.log ( strLogMessagePrefix + 'LLCommon.DomainId              = ' + LLCommon.DomainId );
        console.log ( strLogMessagePrefix + 'LLCommon.TenantId              = ' + LLCommon.TenantId );
        console.log ( strLogMessagePrefix + 'LLCommon.DomainName            = ' + LLCommon.DomainName );
        console.log ( strLogMessagePrefix + 'LLCommon.UserId                = ' + LLCommon.UserId );

        //  --------------------------------------------------------------------
        //  See Super Hack # 10, Nullish Coalescing, in "45 JavaScript Super
        //  Hacks Every Developer Should Know" at https://blog.devgenius.io/45-javascript-super-hacks-every-developer-should-know-92aecfb33ee8.
        //  --------------------------------------------------------------------

        console.log ( strLogMessagePrefix + 'LLCommon.DialerLogin           = ' + LLCommon.DialerLogin );
        console.log ( strLogMessagePrefix + 'LLCommon.EnabledCRM            = '
                                          +   'ExternalSystemTypeId          : ' + ( LLCommon.EnabledCRM.CrmName !== undefined ? LLCommon.EnabledCRM.ExternalSystemTypeId : '*** NONE ***' )
                                          + ', CrmName                       : ' + ( LLCommon.EnabledCRM.CrmName !== undefined ? LLCommon.EnabledCRM.CrmName              : EMPTY_STRING )
                                          + ', Monikor                       : ' + ( LLCommon.EnabledCRM.Monikor             ?? EMPTY_STRING )
                                          + ', SysCRMLeadOrContact           : ' + ( LLCommon.EnabledCRM.SysCRMLeadOrContact ?? EMPTY_STRING )
                                          + ', Prefix                        : ' + ( LLCommon.EnabledCRM.Prefix              ?? EMPTY_STRING ) );

        if ( LLCommon.EntityType !== null )
        {
            console.log ( strLogMessagePrefix + 'LLCommon.EntityType            = '
                                              +   'AbsoluteEntityName            : ' + LLCommon.EntityType.AbsoluteEntityName
                                              + ', CRMEntityTypeId               : ' + LLCommon.EntityType.CRMEntityTypeId
                                              + ', EntityName                    : ' + LLCommon.EntityType.EntityName
                                              + ', EntityDescription             : ' + LLCommon.EntityType.EntityDescription
                                              + ', CreatedDate                   : ' + LLCommon.EntityType.CreatedDate
                                              + ', CreatedByUserId               : ' + LLCommon.EntityType.CreatedByUserId
                                              + ', CreatedOrLastModifiedDate     : ' + LLCommon.EntityType.CreatedOrLastModifiedDate
                                              + ', CreatedOrLastModifiedByUserId : ' + LLCommon.EntityType.CreatedOrLastModifiedByUserId );
        }   // TRUE (The active page has an associated CRM Entity.) block, if ( LLCommon.EntityType !== null )
        else
        {
            console.log ( strLogMessagePrefix + 'LLCommon.EntityType            = NULL' );
        }   // FALSE (The active page is disassociated from CRM entitities.) block, if ( LLCommon.EntityType !== null )

        //  --------------------------------------------------------------------
        //  Unless it's already set, invoke GetBasicSalesTalkUserInfo to set the
        //  properties of the UserInfo object.
        //  --------------------------------------------------------------------

        if ( LLCommon.UserInfo === null )
        {
            console.log ( strLogMessagePrefix + 'Calling GetBasicSalesTalkUserInfo to initialize the UserInfo structure.' );
            LLCommon.UserInfo   = LLCommon.DoAjax ( 'GetBasicSalesTalkUserInfo',
                                                    'GET',
                                                    {
                                                        'UserId' : _userid
                                                    });
        }   // if ( LLCommon.UserInfo === null )

        if ( LLCommon.UserInfo !== null )
        {
            Object.keys ( LLCommon.UserInfo ).forEach ( key =>
            {
                console.log ( strLogMessagePrefix + ': LLCommon.UserInfo.' + key + ' = ' + LLCommon.UserInfo [ key ] );
            });
        }   // TRUE (preferred outcome) block, if ( LLCommon.UserInfo !== null )
        else
        {
            console.log ( strLogMessagePrefix + 'LLCommon.UserInfo              = NULL' );
        }   // FALSE (unpreferred outcome) block, if ( LLCommon.UserInfo !== null )

        console.log ( strLogMessagePrefix + 'LLCommon.Roles4User Length     = ' + LLCommon.Roles4User.length );

        for ( var intJ = ARRAY_FIRST_ELEMENT;
                  intJ < LLCommon.Roles4User.length;
                  intJ++ )
        {
            console.log ( strLogMessagePrefix + 'LLCommon.Roles4User # ' + LLCommon.OrdinalFromIndex ( intJ ) + '        = ' + LLCommon.Roles4User [ intJ ] );
        }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < LLCommon.Roles4User.length; intJ++ )
    }   // TRUE block, if ( pstrScriptPhase === 'EndOfDocumentReady' )
    else
    {
        console.log ( strLogMessagePrefix + 'LLCommon.DialerLogin           = *** Valuee not yet set ***' );
        console.log ( strLogMessagePrefix + 'LLCommon.EnabledCRM            = *** Valuee not yet set ***' );
    }   // FALSE block, if ( pstrScriptPhase === 'EndOfDocumentReady' )

    console.log ( strLogMessagePrefix + '_llAppPath                     = ' + _llAppPath );
    console.log ( strLogMessagePrefix + 'pathName                       = ' + pathName );
}   // function ShowGlobalVars


ShowGlobalVars ( 'BeforeDocumentReady' );

//  ----------------------------------------------------------------------------
//  Since $(document).ready happens AFTER the DOMContentLoaded event has come
//  and gone and LeadLifeJSHelpersLib.js instantiates its object when the
//  DOMContentLoaded event arises, LLCommon.js must follow suit so that the
//  values expected by LeadLifeJSHelpers are initialized when its
//  DOMContentLoaded event listener takes the helm.
//
//  Since the LLCommon object constructor runs when the script loads, its code
//  is ready to go when the DOMContentLoaded event arises with respect to it.
//  ----------------------------------------------------------------------------

window.addEventListener ( 'DOMContentLoaded', function ( )
{
    function SetDialerLogin ( pintSource , pobjValue )
    {
        //  --------------------------------------------------------------------
        //  Name:       SetDialerLogin
        //
        //  Goal:       Compute the Dialer Login value from the pobjValue value,
        //              falling back to the value read from sessionStorage,
        //              unless there is none. In that case, return the empty
        //              string.
        //
        //  Arguments:  pintSource  = This numeric value may be SRC_IS_UNKNOWN,
        //                            SRC_IS_QUERY_STRING, SRC_IS_FORM_FIELD,
        //                            SRC_IS_SESSION_STORAGE, or SRC_IS_LEAD_ID_PER_URL.
        //                            Though other values are defined, they are
        //                            unlikely to appear in this parameter.
        //
        //              pobjValue   = This value is a string unless pintSource
        //                            is SRC_IS_UNKNOWN, in which case its value
        //                            is null.
        //
        //  Returns:    See the Function Goal.
        //  --------------------------------------------------------------------

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        const strLoginId    = sessionStorage.getItem ( 'DialerLogin' )

        LLCommon.Trace ( strMethodName + ': pintSource = ' + pintSource + ', pobjValue = ' + pobjValue + ', strLoginId = ' + strLoginId );

        if ( strLoginId !== null && ( ( pobjValue !== null ) && ( pobjValue !== undefined ) ) )
        {
            if ( strLoginId !== pobjValue )
            {   // Overwrite the value currently in sessionStorage with the value given in the URL parameter.
                    sessionStorage.setItem ( 'DialerLogin' ,
                                             pobjValue );
            }   // if ( strLoginId !== pobjValue )

            return pobjValue;
        }   // TRUE (Login provided via URL parameter and ID saved into sessionStorage.) if ( strLoginId !== null && ( ( pobjValue !== null ) && ( pobjValue !== undefined ) ) )

        if ( ( pobjValue !== null ) && ( pobjValue !== undefined ) && pobjValue.length > EMPTY_STRING_LENGTH )
        {
            sessionStorage.setItem ( 'DialerLogin' ,
                                     pobjValue );
            return pobjValue;
        }   // TRUE (Though there is no login ID saved in the session, the URL provided one.) block, if ( ( pobjValue !== null ) && ( pobjValue !== undefined ) && pobjValue.length > EMPTY_STRING_LENGTH )

        if ( strLoginId !== null )
        {   // In the absence of a value from the URL, use the value read from sessionStorage.
            return strLoginId;
        }   // if ( strLoginId !== null )

        return EMPTY_STRING;    // When all else fails, return the empty string.
    }   // private function SetDialerLogin


    function SetDomainTenantAndUserIds ( )
    {
        //  --------------------------------------------------------------------
        //  The following algorithm is the result of many trials over a span of
        //  weeks.
        //
        //  1)  If a SalesTalk numeric UserId is supplied in the URL, it takes
        //      priority over a login name supplied in a different parameter of
        //      the same URL.
        //
        //  2)  If a SalesTalk numeric LeadId is supplied in the URL, it governs
        //      the DomainId, TenantId, and DomainName to use in the code behind
        //      the current page.
        //
        //  3)  If the SalesTalk LeadId is absent from the URL and the UserId is
        //      present in the URL, the UserId governs the DomainId, TenantId,
        //      and DomainName to use in the code behind the current page.
        //
        //  4)  If both the SalesTalk LeadId and UserId are absent from the URL, but
        //      the Login Email ID is present, it governs the DomainId, TenantId,
        //      and DomainName to use in the code behind the current page.
        //  ------------------------------------------------------------------------

        /**
         * Evaluate the Wise Agent and Axxess Networks configuration stat
         * to determine whether to use the `RealEmailAddress` or the `Email`
         * (login email ID) as the reference and dialer login values.
         *
         * @param {integer} pintDomainId        - The SalesTalk Domain ID
         * @param {integer} pintTenantId        - The SalesTalk Tenant ID
         * @param {string} pstrRealEmailAddress - The user's real, reachable
         *                                        email address
         * @returns {boolean} - Resolves to TRUE if the Wise Agent CRM is
         *                      configured AND the real email address is present
         *                      and something besides the empty string, FALSE
         *                      otherwise.
         */
        function UseRealEmail ( pintDomainId , pintTenantId , pstrRealEmailAddress )
        {
            const strMethodName                             = LLCommon.GetNameOfCurrentFunction ( );

            const strWiseAgentEnabled                       = LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                                                'GET',
                                                                                {
                                                                                    'monikor'   : 'WiseAgentEnabled',
                                                                                    'tenantId'  : pintTenantId,
                                                                                    'domainId'  : pintDomainId,
                                                                                    'Behavior'  : 3 // IgnoreWebConfig = 1 | IgnoreDomain1000 = 2 = 3 for a bitmapped C# enumeration
                                                                                } );

            if ( strWiseAgentEnabled === 'true' )
            {
                return pstrRealEmailAddress !== EMPTY_STRING;
            }   // if ( strWiseAgentEnabled === 'true' )
        }   // function UseRealEmail


        const strMethodName                                 = LLCommon.GetNameOfCurrentFunction ( );

        _fLoginIdIsSet.isSet                                = false;            // This flag has no value beyond the scope of this function.

        debugger;

        try
        {
            //  ----------------------------------------------------------------
            //  When present in the URL, the Lead ID is processed first, and
            //  independely of the User ID, which the next block addressses.
            //  ----------------------------------------------------------------

            if ( _leadidSource !== SRC_IS_UNKNOWN )
            {   // This TRUE block implements point 2 in the above algorithm description.
                console.info ( strMethodName + ': Evaluating the "leadid" URL parameter')

                //  --------------------------------------------------------------------
                //  Example:    return $"{intDomainId}|{intTenantId}|{strDomainName}";
                //  --------------------------------------------------------------------

                const strDomain4LeadId                      = LLCommon.DoAjax ( 'GetDomainTenant4LeadIdInt',
                                                                                'GET',
                                                                                {
                                                                                    'LeadId' : _leadid
                                                                                } );

                if ( strDomain4LeadId.length > EMPTY_STRING_LENGTH && strDomain4LeadId.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                {
                    const astrDomainTenant4Lead             = strDomain4LeadId.split ( PIPE_CHAR_SPLIT_MATCH );

                    if ( astrDomainTenant4Lead.length == 3 )
                    {
                        const strDomain                     = astrDomainTenant4Lead [ ARRAY_FIRST_ELEMENT ];
                        const strTenant                     = astrDomainTenant4Lead [ ARRAY_SECOND_ELEMENT ];
                        const strName                       = astrDomainTenant4Lead [ ARRAY_THIRD_ELEMENT ];

                        if ( LLCommon.IsValidInteger ( strDomain ) )
                        {
                            _domainid                       = parseInt ( strDomain , 10 );
                            _domainidSource                 = SRC_IS_LEAD_ID_PER_URL;
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )
                        else
                        {
                            throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for DomainId. Supplied Field Value = ' + strDomain + ', LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )

                        if ( LLCommon.IsValidInteger ( strTenant ) )
                        {
                            _tenantid                       = parseInt ( strTenant , 10 )
                            _tenantidSource                 = SRC_IS_LEAD_ID_PER_URL;
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )
                        else
                        {
                            throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for TenantId. Supplied Field Value = ' + strTenant + ', LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )

                        if ( strName.length > EMPTY_STRING_LENGTH )
                        {
                            _domainname                     = strName;
                            _domainnameSource               = SRC_IS_LEAD_ID_PER_URL;

                            _fDomainAndTenantIDAreSafe      = true;
                        }   // TRUE (anticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )
                        else
                        {
                            throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for DomainName. Supplied Field Value = ' + strName + ', LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                        }   // FALSE (unanticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )
                    }   // TRUE (anticipated outcome) block, if ( astrDomainTenant4Lead.length == 3 )
                    else
                    {
                        throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected number of fields. Expected Field Count = 3. Actual Field Count = ' + astrDomainTenant4Lead.length + ', LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                    }   // FALSE (unanticipated outcome) block, if ( astrDomainTenant4Lead.length == 3 )
                }   // TRUE (anticipated outcome) block, if ( strDomain4LeadId.length > EMPTY_STRING_LENGTH && strDomain4LeadId.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                else
                {
                    throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected result. LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                }   // FALSE (unanticipated outcome) block, if ( strDomain4LeadId.length > EMPTY_STRING_LENGTH && strDomain4LeadId.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND ))

                console.info ( strMethodName + ': Evaluating the "leadid" URL parameter is done.')
            }   // TRUE (In most cases, this is the anticipated outcome.) block, if ( _leadidSource !== SRC_IS_UNKNOWN )

            //  ----------------------------------------------------------------
            //  Up to this point, nothing has been done about the numeric user
            //  ID. User ID processing is independent of lead ID processing.
            //  ----------------------------------------------------------------

            _userid                                         = GetParamValue ( 'userid' , paramsCollection , EXPECTING_AN_INTEGER_VALUE );
            _useridSource                                   = __intValueSource;

            if ( _useridSource !== SRC_IS_UNKNOWN )
            {   // This TRUE block implements point 3 in the above algorithm description, regardless of whether a LeadId is present in the URL.

                //  --------------------------------------------------------
                //  Example:    $"{UserId}{SpecialCharacters.LOGICAL_NEGATE}{strLoginEmailId}{SpecialCharacters.LOGICAL_NEGATE}{strUserFirstNam}{SpecialCharacters.LOGICAL_NEGATE}{strUserLastName}{SpecialCharacters.LOGICAL_NEGATE}{strRealEmailAddress}{SpecialCharacters.LOGICAL_NEGATE}{intDomainId}{SpecialCharacters.LOGICAL_NEGATE}{intTenantId}{SpecialCharacters.LOGICAL_NEGATE}{strDomainNames}
                //  --------------------------------------------------------

                const strUserDomain                         = LLCommon.DoAjax ( 'GetDomainTenant4UserId',
                                                                                'GET',
                                                                                {
                                                                                    'UserId'            : _userid,
                                                                                    'IncludeRealEmail'  : true
                                                                                } );

                if ( strUserDomain.length > EMPTY_STRING_LENGTH && strUserDomain.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                {
                    const astrUserInfo                      = strUserDomain.split ( LOGICAL_NEGATE );

                    //  -----------------------------------------------------------------------------------------------------------------
                    //  Example: '4366Â¬dagray4WiseAgent@SalesTalk.aiÂ¬DavidÂ¬GrayÂ¬1380Â¬1393Â¬WiseAgent_MasterÂ¬david.gray@salesrelevance.com'
                    //            0    1                             2     3    4    5    6                7
                    //  -----------------------------------------------------------------------------------------------------------------

                    if ( astrUserInfo.length === 8 )
                    {
                        const strUserId                     = astrUserInfo [ ARRAY_FIRST_ELEMENT ];
                        const strLoginEmailId               = astrUserInfo [ ARRAY_SECOND_ELEMENT ];
                        const strUserFirstName              = astrUserInfo [ ARRAY_THIRD_ELEMENT ];
                        const strUserLastName               = astrUserInfo [ ARRAY_FOURTH_ELEMENT ];
                        const strDomain                     = astrUserInfo [ ARRAY_FIFTH_ELEMENT ];
                        const strTenant                     = astrUserInfo [ ARRAY_SIXTH_ELEMENT ];
                        const strName                       = astrUserInfo [ ARRAY_SEVENTH_ELEMENT ];
                        const strRealEmailAddress           = astrUserInfo [ ARRAY_EIGHTH_ELEMENT ];

                        if ( _fDomainAndTenantIDAreSafe )
                        {
                            console.info ( strMethodName + ': Setting the domain and tenant IDs from the "userid" URL parameter SKIPPED because they were set based upon the "leadid" parameter.')
                        }   // TRUE (anticipated, in most circumstances, outcome) block, if ( _fDomainAndTenantIDAreSafe )
                        else
                        {
                            if ( LLCommon.IsValidInteger ( strDomain ) )
                            {
                                _domainid                   = parseInt ( strDomain , 10 );
                            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4UserId returned an unexpected value for DomainId. Supplied Field Value = ' + strDomain + ', UserId = ' + _userid + ', Message = ' + strUserDomain );
                            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )

                            if ( LLCommon.IsValidInteger ( strTenant ) )
                            {
                                _tenantid                   = parseInt ( strTenant , 10 )
                            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for TenantId. Supplied Field Value = ' + strTenant + ', UserId = ' + _userid + ', Message = ' + strUserDomain );
                            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )

                            if ( strName.length > EMPTY_STRING_LENGTH )
                            {
                                _domainname                 = strName;
                            }   // TRUE (anticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for DomainName. Supplied Field Value is the empty string, UserId = ' + _userid + ', Message = ' + strUserDomain );
                            }   // FALSE (unanticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )

                            if ( strRealEmailAddress.length === EMPTY_STRING_LENGTH )
                            {
                                console.warn ( strMethodName + ': The RealEmailAddress value is the empty string. When the CRM is Wise Agent, this will prevent Inside Teams using a shared login from using the system.' );
                            }   // if ( strRealEmailAddress.length === EMPTY_STRING_LENGTH )

                            _domainidSource                 = SRC_IS_USERID_PER_URL;
                            _tenantidSource                 = SRC_IS_USERID_PER_URL;
                            _domainnameSource               = SRC_IS_USERID_PER_URL;

                            console.info ( strMethodName + ': Domain and tenant IDs set from the "userid" URL parameter SKIPPED because the "leadid" parameter is ABSENT from the URL and the Session store.')
                        }   // FALSE (unanticipated, in most circumstances, outcome) block, if ( _fDomainAndTenantIDAreSafe )

                        _login                              = UseRealEmail ( _domainid , _tenantid , strRealEmailAddress )
                                                              ? strRealEmailAddress
                                                              : strLoginEmailId;
                        _loginSource                        = SRC_IS_USERID_PER_URL;

                        _fLoginIdIsSet.isSet                = true;
                        _fDomainAndTenantIDAreSafe          = true;
                    }   // TRUE (anticipated outcome) block, if ( astrUserInfo.length === 8 )
                    else
                    {
                        throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4UserId returned an unexpected number of fields. Expected field count = 8, Actual Field Count = ' + astrUserInfo.length + ', UserId = ' + _userid + ', Message = ' + strUserDomain );
                    }   // FALSE (unanticipated outcome) block, if ( astrUserInfo.length === 8 )
                }   // TRUE (anticipated outcome) block, if ( strUserDomain.length > EMPTY_STRING_LENGTH && strUserDomain.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                else
                {
                    throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4UserId returned an unexpected result. UserId = ' + _userid + ', Message = ' + strUserDomain );
                }   // FALSE (unanticipated outcome) block, if ( strUserDomain.length > EMPTY_STRING_LENGTH && strUserDomain.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )

                console.info ( strMethodName + ': Evaluating the "userid" URL parameter is done.')
            }   // TRUE (Implementation of point 3 in the above algorithm description ends here.) block, if ( _useridSource !== SRC_IS_UNKNOWN )

            //  ----------------------------------------------------------------
            //  Point 4 in the algorithm described above, which relies upon the
            //  login ID, is safe ONLY when the Wise Agent CRM is DISABLED, such
            //  that the login email governs because RealEmailAddress is not
            //  allowed to override it.
            //
            //  For that reason, among others, evaluating the login email ID is
            //  saved for last, and is skipped when _fLoginIdIsSet.isSet
            //  is TRUE, which indicates, among other things, that _login is
            //  already validated and set.
            //  ----------------------------------------------------------------

            if ( _fLoginIdIsSet.isSet )
            {
                console.info ( strMethodName + ': Evaluating the "login" URL parameter SKIPPED because its value is already established.')
            }   // TRUE (in many circumstnaces) block, if ( fLoginIdIsSet )
            else
            {
                console.info ( strMethodName + ': Evaluate "login" URL parameter.' );

                _login                                      = GetParamValue ( 'login' , paramsCollection );
                _loginSource                                = __intValueSource;

                debugger;

                if ( _loginSource !== SRC_IS_UNKNOWN )
                {
                    console.debug ( strMethodName + ': Login URL parameter found.');

                    let strUserDomain                       = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                                'GET',
                                                                                {
                                                                                    'loginName'         : _login,
                                                                                    'IncludeRealEmail'  : true,
                                                                                    'DomainId'          : UseRealEmail ( _domainid , _tenantid ) ? _domainid : NUMERIC_ZERO
                                                                                } );

                    if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )
                    {   // It's easier to test for the adverse event and throw. Moreover, a very specific exception can arise for the leader of a Wise Agent Team.

                        if ( strUserDomain.startsWith ( 'ERROR: The specified login ID, ' ) && strUserDomain.endsWith ( ', is invalid.' ) )
                        {   // Though we don't yet know whether the Agent is the Leader of a Wise Agent Team, it is safe to assume that in the absence of a RealEmailAddress, we should evaluate the Email column of the LeadLife.[User] table.
                            strUserDomain                   = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                                'GET',
                                                                                {
                                                                                    'loginName'         : _login,
                                                                                    'IncludeRealEmail'  : false,
                                                                                    'DomainId'          : UseRealEmail ( _domainid , _tenantid ) ? _domainid : NUMERIC_ZERO
                                                                                } );

                            if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an Exception. loginName = ' + _login + ', IncludeRealEmail = false, Error Message = ' + strUserDomain );
                            }   // TRUE (unanticipated, and unwelcome, outcome) block, if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )
                            else
                            {
                                console.log ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName SUCCEEDED. loginName = ' + _login + ', IncludeRealEmail = false, strUserDomain = ' + strUserDomain );
                            }   // FALSE (anticipated outcome) block, if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )
                        }   // TRUE (anticipated outcome for the Leader of a Team in a Wise Agent account.) block, if ( strUserDomain.startsWith ( 'ERROR: The specified login ID, ' ) && strUserDomain.endsWith ( ', is invalid.' ) )
                        else
                        {
                            throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an Exception. loginName = ' + _login + ', IncludeRealEmail = true, Error Message = ' + strUserDomain );
                        }   // FALSE (unanticipated outcome) block, if ( strUserDomain.startsWith ( 'ERROR: The specified login ID, ' ) && strUserDomain.endsWith ( ', is invalid.' ) )
                    }   // TRUE (unanticipated outcome) block, if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )
                    else
                    {
                        console.log ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName SUCCEEDED. loginName = ' + _login + ', IncludeRealEmail = true, strUserDomain = ' + strUserDomain );
                    }   // FALSE (anticipated outcome) block, if ( strUserDomain.length === EMPTY_STRING_LENGTH || strUserDomain.startsWith ( 'ERROR:' + SPACE_CHARACTER ) || strUserDomain.indexOf ( PIPE_CHAR ) === ARRAY_INVALID_INDEX )                        const astrUserInfo                  = strUserDomain.split ( PIPE_CHAR_SPLIT_MATCH );

                    //  ----------------------------------------------------------------------
                    //  Example: 4366|1393|1380|WiseAgent_Master|david.gray@salesrelevance.com
                    //           0    1    2    3                4
                    //  ----------------------------------------------------------------------

                    astrUserInfo = strUserDomain.split ( PIPE_CHAR_SPLIT_MATCH );

                    if ( astrUserInfo.length === 4 || astrUserInfo.length === 5 )
                    {
                        const strUserId                     = astrUserInfo [ ARRAY_FIRST_ELEMENT ];
                        const strTenant                     = astrUserInfo [ ARRAY_SECOND_ELEMENT ];
                        const strDomain                     = astrUserInfo [ ARRAY_THIRD_ELEMENT ];
                        const strName                       = astrUserInfo [ ARRAY_FOURTH_ELEMENT ];
                        const strRealEmailAddress           = astrUserInfo.length === 5 ? astrUserInfo [ ARRAY_FIFTH_ELEMENT ] : EMPTY_STRING;

                        if ( LLCommon.IsValidInteger ( strUserId ) )
                        {
                            _userid                         = parseInt ( strUserId , 10 );
                            _useridSource                   = SRC_IS_LOGIN_NAME_PER_URL;
                            _fLoginIdIsSet.isSet            = true;
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )
                        else
                        {
                            throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an unexpected value for UserId. Supplied Field Value = ' + strUserId  + ', loginName = ' + _login + ', IncludeRealEmail = true, Error Message = ' + strUserDomain );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )

                        if ( _fDomainAndTenantIDAreSafe )
                        {
                            console.info ( strMethodName + ': Domain and Tenant evaluation SKIPPED because their values are established.' );
                        }   // TRUE (anticipated outcome) block, if ( _fDomainAndTenantIDAreSafe )
                        else
                        {
                            console.info ( strMethodName + ': Evaluating Login URL parameter to assign SalesTalk domain and tenant ID values.' );

                            if ( LLCommon.IsValidInteger ( strDomain ) )
                            {
                                _domainid                   = parseInt ( strDomain , 10 );
                            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an unexpected value for DomainId. Supplied Field Value = ' + strDomain  + ', loginName = ' + _login + ', IncludeRealEmail = true, Error Message = ' + strUserDomain );
                            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strDomain ) )

                            if ( LLCommon.IsValidInteger ( strTenant ) )
                            {
                                _tenantid                   = parseInt ( strTenant , 10 )
                            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an unexpected value for TenantId. Supplied Field Value = ' + strTenant + ', loginName = ' + _login + ', IncludeRealEmail = true, Error Message = ' + strUserDomain );
                            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( strTenant ) )

                            if ( strName.length > EMPTY_STRING_LENGTH )
                            {
                                _domainname                 = strName;
                            }   // TRUE (anticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )
                            else
                            {
                                throw new Error ( strMethodName + ': SalesTalk API GetDomainTenant4LeadIdInt returned an unexpected value for DomainName. Supplied Field Value = ' + strName + ', LeadId = ' + _leadid + ', Message = ' + strDomain4LeadId );
                            }   // FALSE (unanticipated outcome) block, if ( strName.length > EMPTY_STRING_LENGTH )

                            if ( strRealEmailAddress.length === EMPTY_STRING_LENGTH )
                            {
                                console.warn ( strMethodName + ': The RealEmailAddress value is the empty string. When the CRM is Wise Agent, this will prevent Inside Teams using a shared login from using the system.' );
                            }   // if ( strRealEmailAddress.length === EMPTY_STRING_LENGTH )

                            _domainidSource                 = SRC_IS_LOGIN_NAME_PER_URL;
                            _tenantidSource                 = SRC_IS_LOGIN_NAME_PER_URL;
                            _domainnameSource               = SRC_IS_LOGIN_NAME_PER_URL;
                        }   // FALSE (unanticipated outcome) block, if ( _fDomainAndTenantIDAreSafe )
                    }   // TRUE (anticipated outcome) block, if ( astrUserInfo.length === 4 || astrUserInfo.length === 5 )
                    else
                    {
                        throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned an unexpected number of delimited list items. Expected item count = 5, Actual Count = ' + astrLoginInfo.length + ', loginName = ' + _login + ', IncludeRealEmail = true, Error Message = ' + strUserDomain );
                    }   // FALSE (unanticipated outcome) block, if ( astrUserInfo.length === 4 || astrUserInfo.length === 5 )
                }   // if ( _loginSource !== SRC_IS_UNKNOWN )
            }   // FALSE (in most circumstances) block, if ( _fLoginIdIsSet.isSet )

            if ( _leadidSource !== SRC_IS_UNKNOWN )
            {
                LLCommon.LeadId                             = _leadid;
            }   // if ( _leadidSource !== SRC_IS_UNKNOWN )

            if ( _fDomainAndTenantIDAreSafe )
            {
                LLCommon.DomainId                           = _domainid;
                LLCommon.TenantId                           = _tenantid;
                LLCommon.DomainName                         = _domainname;
            }   // if ( _fDomainAndTenantIDAreSafe )

            if ( _fLoginIdIsSet.isSet )
            {
                LLCommon.UserInfo                           = LLCommon.DoAjax ( 'GetBasicSalesTalkUserInfo',
                                                                                'GET',
                                                                                {
                                                                                    'UserId' : _userid
                                                                                });
                LLCommon.DialerLogin                        = _login;
                LLCommon.UserId                             = _userid;
            }   // TRUE block, if ( _fLoginIdIsSet.isSet )
            else
            {
                debugger;
	            LLCommon.AuthenticatedLoginId               = LLCommon.DoAjax ( 'GetLoginIdOfLoggedInUser' );

                if ( _useridSource === SRC_IS_UNKNOWN && LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH && LLCommon.AuthenticatedLoginId.indexOf ( '@' ) > INDEXOF_NOT_FOUND )
                {
					var strDomainTenantUserIds4LoginName    = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                                'GET',
                                                                                {
																					'loginName' : LLCommon.AuthenticatedLoginId,
																					'IncludeRealEmail' : false,
																					'DomainId'         : LLCommon.DomainId
																				});

					if ( strDomainTenantUserIds4LoginName.length  > EMPTY_STRING_LENGTH && strDomainTenantUserIds4LoginName.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
					{
						var astrDomainTenantUserIds4Login   = strDomainTenantUserIds4LoginName.split ( PIPE_CHAR_SPLIT_MATCH );

						if ( astrDomainTenantUserIds4Login.length > ARRAY_NOT_EMPTY && LLCommon.IsValidInteger ( astrDomainTenantUserIds4Login [ ARRAY_FIRST_ELEMENT ] ) )
						{
							_userid                         = parseInt ( astrDomainTenantUserIds4Login [ ARRAY_FIRST_ELEMENT ] , 10 );
						}	// TRUE (anticipated outcome) block, if ( astrDomainTenantUserIds4Login.length > ARRAY_NOT_EMPTY && LLCommon.IsValidInteger ( astrDomainTenantUserIds4Login [ ARRAY_FIRST_ELEMENT ] ) )
						else
						{
							throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned the following UNEXPECTED result : ' + strDomainTenantUserIds4LoginName );
						}	// FALSE (unanticipated outcome) block, if ( astrDomainTenantUserIds4Login.length > ARRAY_NOT_EMPTY && LLCommon.IsValidInteger ( astrDomainTenantUserIds4Login [ ARRAY_FIRST_ELEMENT ] ) )
					}	// TRUE (anticipated outcome) block, if ( strDomainTenantUserIds4LoginName.length  > EMPTY_STRING_LENGTH && strDomainTenantUserIds4LoginName.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
					else
					{
						throw new Error ( strMethodName + ': SalesTalk API GetDomainTenantUserIds4LoginName returned the following error message : ' + strDomainTenantUserIds4LoginName );
					}	// FALSE (unanticipated outcome) block, if ( strDomainTenantUserIds4LoginName.length  > EMPTY_STRING_LENGTH && strDomainTenantUserIds4LoginName.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )

                    LLCommon.UserInfo                       = LLCommon.DoAjax ( 'GetBasicSalesTalkUserInfo',
                                                                                'GET',
                                                                                {
                                                                                    'UserId' : _userid
                                                                                });

                    //  ----------------------------------------------------
                    //  Since the user ID supplied in the URL checks out, it
                    //  is allowed to trump the login ID, even if it belongs
                    //  to an authenticated user. There is method in our
                    //  madness.
                    //  ----------------------------------------------------

                    if ( LLCommon.UserInfo !== null )
                    {
                        _login                          	= LLCommon.UserInfo.AgentLoginEmailId;
                        _loginSource                    	= SRC_IS_USERID_PER_URL;
                        LLCommon.DialerLogin            	= LLCommon.UserInfo.AgentLoginEmailId;
                         _fLoginIdIsSet.isSet               = true;
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.UserInfo !== null )
                    else
                    {
                        LLCommon.LogException (   strMethodName
                                                + ' encountered an unexpected MISSING login ID.'
                                                + ' AJAX call to GetBasicSalesTalkUserInfo returned NULL,'
                                                + ' and DialerLogin will be INVALID.' );
                    }	// FALSE (unanticipated outcome) block, if ( LLCommon.UserInfo !== null )
                }	// TRUE (anticipated outcome) block, if ( _useridSource === SRC_IS_UNKNOWN && LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH && LLCommon.AuthenticatedLoginId.indexOf ( '@' ) > INDEXOF_NOT_FOUND )
                else
                {
                    LLCommon.LogException ( strMethodName + ' encountered an unexpected MISSING user ID, and DialerLogin will be INVALID.' );
                }	// FALSE (unanticipated outcome) block, if ( _useridSource === SRC_IS_UNKNOWN && LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH && LLCommon.AuthenticatedLoginId.indexOf ( '@' ) > INDEXOF_NOT_FOUND )
            }	// FALSE block, if ( _fLoginIdIsSet.isSet )
        }
        catch ( ex )
        {
            LLCommon.LogException ( ex );
        }

        return _fDomainAndTenantIDAreSafe;                  // This function is called through an IF statement that evaluates its return value.
    }   // private function SetDomainTenantAndUserIds


    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    LLCommon.DialerLogin                = null;
    LLCommon.EnabledCRM                 = null;
    LLCommon.EntityType                 = null;
    LLCommon.ExternalCRMId              = null;
    LLCommon.LeadId                     = null;
    LLCommon.DomainId                   = null;
    LLCommon.DomainName                 = null;
    LLCommon.TenantId                   = null;
    LLCommon.UserId                     = null;
    LLCommon.UserInfo                   = null;

    LLCommon._FSuppressCRMUpdateAlert   = false;

    debugger;

    //  ========================================================================
    //  Form Dirty/Clean State Management
    //
    //  All object names prefixed with `LLCommon.__` are intended to be PRIVATE.
    //
    //  They should **never** be mutated directly under **any** circumstances.
    //  Use the provided utility methods defined next.
    //  ========================================================================

    /**
     * Internal backing field for {@link LLCommon._fFormIsDirty}. This field should
     * never be accessed directly. All reads and writes must go through the public
     * accessor property so that LLCommon can correctly dispatch dirty/clean
     * notifications.
     *
     * @private
     * @type {boolean}
     */
    LLCommon.__formIsDirty = false;


    /**
    * Registry of event handlers invoked whenever the form transitions from
    * "dirty" to "clean". This is an instance of {@link LLCommon.EventHandlerList}
    * and provides duplicate-prevention and safe-invocation semantics.
    *
    * Handlers registered here are invoked automatically when
    * {@link LLCommon._fFormIsDirty} is set to `false` after previously being `true`.
    *
    * Typical usage:
    *   LLCommon.__cleanHandlers.register   ( fn );
    *   LLCommon.__cleanHandlers.unRegister ( fn );
    *
    * @memberof LLCommon
    * @type {LLCommon.EventHandlerList}
     */
    LLCommon.__cleanHandlers = new LLCommon.EventHandlerList ( 'FormCleanHandlers');

    /**
    * Registry of event handlers invoked whenever the form transitions from
    * "clean" to "dirty". This is an instance of {@link LLCommon.EventHandlerList}
    * and provides duplicate-prevention and safe-invocation semantics.
    *
    * Handlers registered here are invoked automatically when
    * {@link LLCommon._fFormIsDirty} is set to `true` after previously being `false`.
    *
    * Typical usage:
    *   LLCommon.__dirtyHandlers.register   ( fn );
    *   LLCommon.__dirtyHandlers.unRegister ( fn );
    *
    * @memberof LLCommon
    * @type {LLCommon.EventHandlerList}
     */
    LLCommon.__dirtyHandlers = new LLCommon.EventHandlerList ( 'FormDirtyHandlers');

    /**
     * Invokes all handlers registered for the "clean" transition.
     *
     * @private
     * @function __notifyClean
     * @memberof LLCommon
     */
    LLCommon.__notifyClean = function ( )
    {
        LLCommon.__cleanHandlers.invokeAll ( );
    };  // LLCommon.__notifyClean method

    /**
     * Invokes all handlers registered for the "dirty" transition.
     *
     * @private
     * @function __notifyDirty
     * @memberof LLCommon
     */
    LLCommon.__notifyDirty = function ( )
    {
        LLCommon.__dirtyHandlers.invokeAll ( );
    };  // LLCommon.__notifyDirty

    if ( SetDomainTenantAndUserIds ( ) )
    {
        console.info ( strMethodName + ': Domain and Tenant are set. LLCommon.DomainId = ' + LLCommon.DomainId + ' (' + LLCommon.DomainName + '), Tenant ID = ' + LLCommon.TenantId + ', User ID = ' + LLCommon.UserId + ' (' + LLCommon.DialerLogin + ')' );
    }   // TRUE (anticipated outcome) block, if ( SetDomainTenantAndUserIds ( ) )
    else
    {
        console.warn ( strMethodName + ': Domain and Tenant ID NOT SET. Some features may be impaired.' );
    }   // FALSE (unanticipated outcome) block, if ( SetDomainTenantAndUserIds ( ) )

    LLCommon.logPageChange ( pathName + ( document.location.search !== null ? document.location.search : EMPTY_STRING ) );

    if ( _CRMSource !== SRC_IS_UNKNOWN && _CRM.toLowerCase ( ) === 'bullhorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )
    {
        //  --------------------------------------------------------------------
        //  Since the first pass told GetParamValue to expect an integer, the
        //  call is repeated because a Bullhorn login is alphanumeric, the call
        //  must be repeated without the optional constraint before attempting
        //  to map a userid passed in via URL by the Bullhorn iFrame interface.
        //
        //  Though the Bullhorn iFrame interface passes a hyphenated unique user
        //  ID, the routine must compensate for the ID being passed in bare,
        //  because GetSalesTalkUserId4BullhornUserId expects a fully qualified
        //  Bullhorn user ID.
        //  --------------------------------------------------------------------

        _userid                             = GetParamValue ( 'userid' ,
                                                              paramsCollection );
        _useridSource                       = __intValueSource;

        const strSalesTalkLogin4BHLogin     = LLCommon.DoAjax ( 'GetSalesTalkUserId4BullhornUserId',
                                                                'GET',
                                                                {
                                                                   'BullhornUserId' : ( _userid.indexOf ( HYPHEN_CHAR ) == INDEXOF_NOT_FOUND
                                                                                        ? _CorporationID + HYPHEN_CHAR + _userid
                                                                                        : _userid )
                                                                } );
        var fDomainMismatch = false;

        if ( _leadidSource !== SRC_IS_UNKNOWN )
        {
            const strDomain4LeadId          = LLCommon.DoAjax ( 'GetDomainTenant4LeadIdInt',
                                                                'GET',
                                                                {
                                                                    'LeadId' : _leadid
                                                                } );

            if ( strDomain4LeadId.length > EMPTY_STRING_LENGTH && strDomain4LeadId.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            {
                const astrDomainInfoLD      = strDomain4LeadId.split ( PIPE_CHAR_SPLIT_MATCH );
                const intDomainId4Lead      = parseInt ( astrDomainInfoLD [ ARRAY_FIRST_ELEMENT ] );

                const strDomain4Login       = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                'GET',
                                                                {
                                                                   'loginName' : _login
                                                                } );

                if ( strDomain4Login.length > EMPTY_STRING_LENGTH && strDomain4Login.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                {
                    const astrDomainInfoLI  = strDomain4Login.split ( PIPE_CHAR_SPLIT_MATCH );
                    const intDomainId4Login = parseInt ( astrDomainInfoLI [ ARRAY_THIRD_ELEMENT ] );
                    fDomainMismatch         = ( intDomainId4Lead !== intDomainId4Login );
                }   // if ( strDomain4Login.length > EMPTY_STRING_LENGTH && strDomain4Login.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            }   // if ( strDomain4LeadId.length > EMPTY_STRING_LENGTH && strDomain4LeadId.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
        }   // if (_leadidSource !== SRC_IS_UNKNOWN )

        if ( strSalesTalkLogin4BHLogin === _CorporationID + '@Bullhorn.com' || fDomainMismatch )
        {
            const dummy                     = LLCommon.Prompt4Words2ActionsLogin ( _userid ,
                                                                                   fDomainMismatch
                                                                                        ? 'tblW2ADomainMismatch'
                                                                                        : 'tblW2ALoginPrompt' );
        }   // TRUE (UNanticipated outcome) block, if ( strSalesTalkLogin4BHLogin === _CorporationID + '@Bullhorn.com' || fDomainMismatch )
        else
        {
            _fDomainAndTenantIDAreSafe      = LLCommon.SynchronizeLoginInfo ( strSalesTalkLogin4BHLogin,
                                                                              SRC_IS_WORDS2ACTIONS_LOGIN );
            LLCommon.ShowOrHideElement ( 'STT_HOURGLASS' ,
                                          LLCommon.ELEMENT_HIDE );
            LLCommon.ShowOrHideElement ( 'LLTheWholePage' ,
                                          LLCommon.ELEMENT_SHOW );
        }   // FALSE (anticipated outcome) block, if ( strSalesTalkLogin4BHLogin === _CorporationID + '@Bullhorn.com' || fDomainMismatch )
    }   // TRUE (The page is executing on behalf of a Bullhorn user.) block, if ( _CRMSource !== SRC_IS_UNKNOWN && _CRM.toLowerCase ( ) === 'bullhorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )
    else
    {
        LLCommon.ShowOrHideElement ( 'STT_HOURGLASS' ,
                                      LLCommon.ELEMENT_HIDE );
        LLCommon.ShowOrHideElement ( 'LLTheWholePage' ,
                                     LLCommon.ELEMENT_SHOW );

        debugger;

        //  --------------------------------------------------------------------
        //  Since presence of the .ASPXAUTH cookie cannot be reliably tested,
        //  the server is queried for the login ID of the current user. When
        //  GetLoginIdOfLoggedInUser returns the empty string, the user is
        //  UNAUTHENTICATED. Otherwise, the login name is returned.
        //  --------------------------------------------------------------------

        LLCommon.AuthenticatedLoginId           = LLCommon.DoAjax ( 'GetLoginIdOfLoggedInUser' );

        if ( LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH )
        {
            if ( LLCommon.AuthenticatedLoginId.indexOf ( 'Exception arose.' ) === INDEXOF_NOT_FOUND )
            {
                if ( localStorage.getItem ( 'UserName' ) === LLCommon.AuthenticatedLoginId && !_fLoginIdIsSet.isSet )
                {
                    if ( _loginSource === SRC_IS_UNKNOWN )
                    {   // The authenticated login is a last resort.
                        _login                  = LLCommon.AuthenticatedLoginId;
                        _loginSource            = SRC_IS_SERVER_HTTP_CONTEXT;
                    }   // if ( _loginSource === SRC_IS_UNKNOWN )
                }   // TRUE (anticipated outcome) block, if ( _loginSource === SRC_IS_UNKNOWN )
                else
                {
                    if ( ! _fLoginIdIsSet.isSet )
                    {
                        LLCommon.LogException (   strMethodName
                                                + ' encountered an unexpected Login ID mismatch.'
                                                + ': AJAX call to GetLoginIdOfLoggedInUser for loginName = ' + LLCommon.AuthenticatedLoginId
                                                + ' LocalStorage key localStorage.UserName value = ' + localStorage.UserName );
                    }   // if ( ! _fLoginIdIsSet.isSet )

                    //  --------------------------------------------------------
                    //  In the absence of a localStorage value or conflicting
                    //  values, unless overridden by a URL parameter, take
                    //  authenticated login ID at face value.
                    //
                    //  2025/07/31 01:32:08 - At last, this block behaves as it
                    //                        is documented in the foregoing
                    //                        comment.
                    //  --------------------------------------------------------

                    if ( ! _fLoginIdIsSet.isSet )
                    {
                        if ( _loginSource === SRC_IS_UNKNOWN && _useridSource === SRC_IS_UNKNOWN )
                        {   // The authenticated login is a last resort.
                            _login                  = LLCommon.AuthenticatedLoginId;
                            _loginSource            = SRC_IS_SERVER_HTTP_CONTEXT;
                        }   // TRUE (Neither a login ID, nor a user ID is supplied by the UrL.) block, if ( _loginSource === SRC_IS_UNKNOWN && _useridSource === SRC_IS_UNKNOWN )
                        else
                        {
                            if ( _useridSource !== SRC_IS_UNKNOWN )
                            {
                                LLCommon.CheckUserName ( true );
                            }   // TRUE (anticipated outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                            else
                            {
                                if ( _loginSource !== SRC_IS_UNKNOWN )
                                {
                                    LLCommon.CheckUserName ( false );
                                }
                                else
                                {   // Since it isn't matched by a localStorage entry, take the auntenticated login as a fallback.
                                    _login          = LLCommon.AuthenticatedLoginId;
                                    _loginSource    = SRC_IS_SERVER_HTTP_CONTEXT;
                                }   // FALSE (unanticipated outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                            }   // FALSE (unanticipated outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                        }   // FALSE (Either a login ID OR a numerical user ID appears in the URL. See whether it is the latter.) block, if ( _loginSource === SRC_IS_UNKNOWN && _useridSource === SRC_IS_UNKNOWN )
                    }   // if ( ! _fLoginIdIsSet.isSet )
                }   // FALSE (unanticipated outcome) block, if ( _loginSource === SRC_IS_UNKNOWN )
            }   // TRUE (anticipated outcome) block, if ( LLCommon.AuthenticatedLoginId.indexOf ( 'Exception arose.' ) === INDEXOF_NOT_FOUND )
            else
            {
                LLCommon.LogException ( strMethodName + ': ' + LLCommon.AuthenticatedLoginId );
                LLCommon.CheckUserName ( );
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.AuthenticatedLoginId.indexOf ( 'Exception arose.' ) === INDEXOF_NOT_FOUND )
        }   // TRUE (desired outcome) block, if ( LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH )
        else
        {
            LLCommon.CheckUserName ( );
        }   // FALSE (undesired outcome) block, if ( LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH )

        //  --------------------------------------------------------------------
        //  Skipt this block unless _fDomainAndTenantIDAreSafe is equal to FALSE
        //  AND _login is a string.
        //  --------------------------------------------------------------------

        if ( ( !_fDomainAndTenantIDAreSafe ) && LLCommon.IsString ( _login ) )
        {
            const strResultSet                  = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                                    'GET',
                                                                    {
                                                                       'loginName' : _login
                                                                    } );

            // Example of expected return value: $"{intUserId}|{intTenantId}|{intDomainId}|{strDomainName}";

            if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            {
                const astrResults           = strResultSet.split ( PIPE_CHAR_SPLIT_MATCH );

                _userid                     = parseInt ( astrResults [ ARRAY_FIRST_ELEMENT  ] );
                _tenantid                   = parseInt ( astrResults [ ARRAY_SECOND_ELEMENT ] );
                _domainid                   = parseInt ( astrResults [ ARRAY_THIRD_ELEMENT  ] );
                _domainname                 =            astrResults [ ARRAY_FOURTH_ELEMENT ];

                _useridSource               = SRC_IS_LOGIN_NAME_PER_URL;
                _tenantidSource             = SRC_IS_LOGIN_NAME_PER_URL;
                _domainidSource             = SRC_IS_LOGIN_NAME_PER_URL;
                _domainnameSource           = SRC_IS_LOGIN_NAME_PER_URL;

                _fDomainAndTenantIDAreSafe  = true;
            }   // TRUE (anticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            else
            {
                LLCommon.LogException (   strMethodName
                                        + ': AJAX call to GetDomainTenantUserIds4LoginName for loginName = ' + _login
                                        + ' reported the following exception: ' + strResultSet );
            }   // FALSE (unanticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
        }   // if ( ( !_fDomainAndTenantIDAreSafe ) && LLCommon.IsString ( _login ) )
    }   // FALSE (The page is executing on behalf of someone who isn't using Bullhorn.) block, if ( _CRMSource !== SRC_IS_UNKNOWN && _CRM.toLowerCase ( ) === 'bullhorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )

    LLCommon.EnabledCRM                     = LLCommon.GetEnabledCrmInfo ( _tenantid ,
                                                                           _domainid );
    LLCommon.EvaluateEntityType ( );

    const aoURLVars                         = LLCommon.GetUrlVarsFromSession ( true );
    const intVarCount                       = aoURLVars.length;

    try
    {
        {   // Though docTitlePlaeholder could be hidden in a function, I opted to do so by setting a lexical scope around it.
            let docTitlePlaeholder = document.getElementById ( 'TitleContainer' );

            if ( docTitlePlaeholder !== null && ( docTitlePlaeholder.innerText.length === EMPTY_STRING_LENGTH || docTitlePlaeholder.innerText ==='##DocumentTitle##' ) )
            {   // This statement is skipped unless the TitleContainer element exists.
                docTitlePlaeholder.innerText = document.title;
            }   // if ( docTitlePlaeholder !== null && ( docTitlePlaeholder.innerText.length === EMPTY_STRING_LENGTH || docTitlePlaeholder.innerText ==='##DocumentTitle##' ) )
        }   // Variable docTitlePlaeholder has done its job. Set it on the curb.

        if ( intVarCount > ARRAY_IS_EMPTY )
        {
            LLCommon.Trace ( 'Processing ' + intVarCount + ' query string values recovered from ASP.NET Session object' );

            for ( var intCurrVar = ARRAY_FIRST_ELEMENT;
                      intCurrVar < intVarCount;
                      intCurrVar++ )
            {
                switch ( aoURLVars [ intCurrVar ].KeyName )
                {
                    case 'login':
                        if ( _loginSource === SRC_IS_UNKNOWN )
                        {
                            _login          = aoURLVars [ intCurrVar ].KeyValue;
                            _loginSource    = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Login set from session, value = ' + _login );
                        }   // if ( _loginSource === SRC_IS_UNKNOWN )

                        break;          // case 'login'

                    case 'dbname':
                        if ( _dbnameSource === SRC_IS_UNKNOWN )
                        {
                            _dbname         = aoURLVars [ intCurrVar ].KeyValue;
                            _dbnameSource   = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Database Name set from session, value = ' + _dbname );
                        }   // if ( _dbnameSource === SRC_IS_UNKNOWN )

                        break;          // case 'dbname'

                    case 'domainname':
                        if ( _domainnameSource === SRC_IS_UNKNOWN )
                        {
                            _domainname         = aoURLVars [ intCurrVar ].KeyValue;
                            _domainnameSource   = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Domain Name set from session, value = ' + _domainname );
                        }   // if ( _domainnameSource === SRC_IS_UNKNOWN )

                        break;          // case 'domainname'

                    case 'mobile':
                        if ( _IsMobilePageSource === SRC_IS_UNKNOWN )
                        {
                            _IsMobilePage       = aoURLVars [ intCurrVar ].KeyValue;
                            _IsMobilePageSource = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Mobile Flag set from session, value = ' + _IsMobilePage );
                        }   // if ( _IsMobilePageSource === SRC_IS_UNKNOWN )

                        break;          // case 'mobile'

                    case 'pagename':
                        if ( _pagenameSource === SRC_IS_UNKNOWN )
                        {
                            _pagename       = aoURLVars [ intCurrVar ].KeyValue;
                            _pagenameSource = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Page Name set from session, value = ' + _pagename );
                        }   // if ( _pagenameSource === SRC_IS_UNKNOWN )

                        break;          // case 'pagename'

                    case 'externalcrmid':
                        if ( _externalcrmidSource === SRC_IS_UNKNOWN )
                        {
                            _externalcrmid          = aoURLVars [ intCurrVar ].KeyValue;
                            _externalcrmidSource    = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': External CRM ID set from session, value = ' + _externalcrmid );
                        }   // if ( _externalcrmidSource === SRC_IS_UNKNOWN )

                        break;          // case 'externalcrmid':

                    case 'syscrmleadorcontact':
                        if ( _SysCRMLeadOrContactSource === SRC_IS_UNKNOWN )
                        {
                            _SysCRMLeadOrContact        = aoURLVars [ intCurrVar ].KeyValue;
                            _SysCRMLeadOrContactSource  = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': CRM ID set from session, value = ' + _SysCRMLeadOrContact );
                        }   // if ( _SysCRMLeadOrContactSource === SRC_IS_UNKNOWN )

                        break;          // case 'externalcrmid':

                    case 'leadid':
                        if ( _leadidSource === SRC_IS_UNKNOWN )
                        {
                            _leadid             = aoURLVars [ intCurrVar ].KeyValue;
                            _leadidSource       = SRC_IS_HTTP_SESSION_VARIABLE;
                            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Lead ID set from session, value = ' + _leadid );
                        }   // if ( _leadidSource === SRC_IS_UNKNOWN )

                        break;          // case 'leadid':

                    default:
                        LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': Unexpected KeyName found in query string parameters - KeyName = ' + aoURLVars [ intCurrVar ].KeyName + ', KeyValue = ' + aoURLVars [ intCurrVar ].KeyValue );
                }   // switch ( aoURLVars [ intCurrVar ].KeyName )
            }   // for ( var intCurrVar = ARRAY_FIRST_ELEMENT; intCurrVar < intVarCount; intCurrVar++ )
        }   // if ( intVarCount > ARRAY_IS_EMPTY )

        if ( _loginSource !== SRC_IS_UNKNOWN && _CRMSource !== SRC_IS_UNKNOWN && _CRM !== 'BullHorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )
        {
            var   fLoginIsInvalid               = true;
            var   fQuit                         = false;
            var   box                           = null;
            var   strLastCheckedLogin           = null;
            var   strResultSet                  = null;

            while ( fLoginIsInvalid )
            {
                debugger;

                if ( strLastCheckedLogin !== _login )
                {
                    strLastCheckedLogin         = _login;

                    if ( _fDomainAndTenantIDAreSafe )
                    {
                        fLoginIsInvalid         = false;
                    }   // TRUE (The domain and tenant ID values are set and sanity checked.) block, if ( _fDomainAndTenantIDAreSafe )
                    else
                    {
                        fLoginIsInvalid             = ! ( LLCommon.SynchronizeLoginInfo ( strLastCheckedLogin,
                                                                                          SRC_IS_LOGIN_NAME_PER_URL ) );
                        _fDomainAndTenantIDAreSafe  = ! fLoginIsInvalid;

                    }   // FALSE (The validity of the domain and tenant ID values are questionable. ) block, if ( _fDomainAndTenantIDAreSafe )

                    fQuit                           = fLoginIsInvalid;
                }   // if ( strLastCheckedLogin !== _login )
            }   // while ( fLoginIsInvalid )

            if ( fQuit )
            {
                window.alert ( strResultSet + ' Please close the window and correct the issue.' );
                LLCommon.ShowOrHideElement ( 'LLTheWholePage' ,
                                             LLCommon.ELEMENT_HIDE );
            }   // if ( fQuit )

            if ( _leadidSource === SRC_IS_UNKNOWN && _externalcrmidSource !== SRC_IS_UNKNOWN && _SysCRMLeadOrContactSource !== SRC_IS_UNKNOWN )
            {
                const strVeryBasicLeadInfo      = LLCommon.DoAjax ( 'GetVeryBasicLeadInfo4ExternamCRMId' ,
                                                                    'GET' ,
                                                                    {
                                                                         'ExternalCRMId'       : _externalcrmid ,
                                                                         'SysCRMLeadOrContact' : _SysCRMLeadOrContact ,
                                                                         'DomainId'            : _domainid ,
                                                                         'TenantId'            : _tenantid
                                                                    } )

                if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + LLCommon.ERR_MESSAGE_STANDARD_PREFIX ) )
                {
                    throw new Error ( strVeryBasicLeadInfo );
                }   // TRUE (unanticipated outcome) block, if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + LLCommon.ERR_MESSAGE_STANDARD_PREFIX ) )
                else
                {
                    const astrLeadInfo          = strVeryBasicLeadInfo.split ( LOGICAL_NEGATE );
                    _leadid                     = parseInt ( astrLeadInfo [ ARRAY_FIRST_ELEMENT ] );
                    _leadidSource               = SRC_IS_EXTERNALCRMID;

                    sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                    sessionStorage.setItem ( 'leadid'       , _leadid );
                }   // FALSE (anticipated outcome) block, if ( strVeryBasicLeadInfo.startsWith ( 'GetVeryBasicLeadInfo4ExternamCRMId ' + LLCommon.ERR_MESSAGE_STANDARD_PREFIX ) )
            }   // TRUE (anticipated outcome) if ( _leadidSource === SRC_IS_UNKNOWN && _externalcrmidSource !== SRC_IS_UNKNOWN && _SysCRMLeadOrContactSource !== SRC_IS_UNKNOWN )
            else
            {
                if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )
                {
                    throw new Error ( 'The REQUIRED leadid parameter is missing from the URL query string.' );
                }   // if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )
            }   // FALSE (unanticipated outcome) block, if ( _leadidSource === SRC_IS_UNKNOWN && _externalcrmidSource !== SRC_IS_UNKNOWN && _SysCRMLeadOrContactSource !== SRC_IS_UNKNOWN )
        }   // TRUE (anticipated outcome) block, if ( _loginSource !== SRC_IS_UNKNOWN && _CRMSource !== SRC_IS_UNKNOWN && _CRM !== 'BullHorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )
        else
        {
            if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )
            {
                throw new Error ( 'The REQUIRED login parameter is missing from the URL query string.' );
            }   // if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )
        }   // FALSE (unanticipated outcome) block, if ( _loginSource !== SRC_IS_UNKNOWN && _CRMSource !== SRC_IS_UNKNOWN && _CRM !== 'BullHorn' && _CorporationIDSource !== SRC_IS_UNKNOWN )

        switch ( _leadidSource )
        {
            case SRC_IS_EXTERNALCRMID:
                _fDomainAndTenantIDAreSafe      = true;
                break;
            case SRC_IS_UNKNOWN:
                if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )
                {
                    throw new Error ( 'The REQUIRED leadid parameter and its proxy ExternalCRMId are both missing from the URL query string.' );
                }   // if ( LLCommon.PageLivesOnPURLAndIsFullSize ( ) )

                if ( _useridSource === SRC_IS_LOGIN_NAME_PER_URL || _useridSource === SRC_IS_LEAD_ID_PER_URL || _useridSource === SRC_IS_SESSION_STORAGE )
                {
                    _fDomainAndTenantIDAreSafe  = true;
                }   // if ( _useridSource === SRC_IS_LOGIN_NAME_PER_URL || _useridSource === SRC_IS_LEAD_ID_PER_URL || _useridSource === SRC_IS_SESSION_STORAGE )

                break;
            default:
                if ( !_fDomainAndTenantIDAreSafe )
                {
                    LLCommon.ResolveDomainAndTenantIDs ( );
                }   // if ( !_fDomainAndTenantIDAreSafe )
        }   // switch ( _leadidSource )

        LLCommon.GetCommonObjects ( 'SetDomainTenantAndUserIds (LLCommon)' );

        LLCommon.LeadId     = _leadid;
        LLCommon.DomainId   = _domainid;
        LLCommon.DomainName = _domainname;
        LLCommon.TenantId   = _tenantid;
        LLCommon.UserId     = _userid;

        //  --------------------------------------------------------------------
        //  All common object keys have their final values for this run. Set the
        //  values into the W2A Common Object members in the ASP.NET Session
        //  object, then enumerate everything in the console log.
        //  --------------------------------------------------------------------

        ShowGlobalVars ( 'EndOfDocumentReady' );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
});

function keepMeAlive ( imgName )
{
    $.ajax({
        url: _llAppPath + "account/keepMeAlive",
        type: "GET"
    });
}   // function keepMeAlive


window.setInterval ( "keepMeAlive('keepAliveIMG')", 600000 ); // 5 minutes

//  ----------------------------------------------------------------------------
//  In the following line, ESLint flags the third argument as follows.
//
//      Shadowing of global property 'undefined'.  (no-shadow-restricted-names)
//
//  However, the narrative states that shadowning is permitted when the Variable
//  is never assigned.
//
//  See "no-shadow-restricted-names" at https://eslint.org/docs/latest/rules/no-shadow-restricted-names.
//  ----------------------------------------------------------------------------

(function ( LLCommon , $ , undefined )
{
    var   isInitialized = false;
    var   MaxRows       = 1000;

    if ( !isInitialized )
        init ( );

    function init ( )
    {
        if ( isInitialized )
            return;

        LLCommon.CARRIAGE_RETURN_CHAR   = '\u000D';
        LLCommon.EXCEPTION_MSG_PREFIX   = 'Exception';
        LLCommon.FORM_FEED_CHAR         = '\u000C';
        LLCommon.LINE_FEED_CHAR         = '\u000A';
        LLCommon.PROTOCOL_IS_HTTP       = 'http://';
        LLCommon.PROTOCOL_IS_HTTPS      = 'https://';
        LLCommon.STT_HideElement        = 'STT_HideElement';
        LLCommon.STT_INVALIDLOGININURL  = 'InvalidLoginInURL';
        LLCommon.STT_ShowElement        = 'STT_ShowElement';
        LLCommon.STT_TEXT_TO_SUMMARIZE  = 'TextToSummarize';
        LLCommon.STT_SUMMARY_OF_TEXT    = 'SummaryOfText'
        LLCommon.TAB_CHAR               = '\u0009';
        LLCommon.TOKEN_NOCRM            = 'NoCRM';
        LLCommon.TRACE                  = 'silent';
        LLCommon.TRACE_SILENT           = 'silent';
        LLCommon.TRACE_PHONE_HOME       = 'phonehome';
        LLCommon.TRACE_CONSOLE          = 'console';
        LLCommon.WEB_PATH_SEPARATOR     = '/';
        LLCommon.WA_CONTACT_MOBILEPAGE  = 'WA_Contact_MobilePage_Values';
        LLCommon.WINDOWS_PATH_SEPARATOR = '\\';

        LLCommon.ELEMENT_HIDE           = false;
        LLCommon.ELEMENT_SHOW           = true;

        LLCommon.CSS_SELECTOR_ADD       = true;
        LLCommon.CSS_SELECTOR_REMOVE    = false;

        LLCommon.ToastFactory           = ToastFactory;
        LLCommon.EventHandlerList       = EventHandlerList;

        if ( !window.location.origin && LLCommon.baseURL === undefined )
        {
            window.location.origin = window.location.protocol + "//" + window.location.host;
        }   // if ( !window.location.origin && LLCommon.baseURL === undefined )

        LLCommon.PageLivesOnPURLAndIsFullSize = ( ) => { HostIsPurl ( ) && ( !_IsMobilePage ); }

        //  --------------------------------------------------------------------
        //  Read a page's GET URL variables and return them as an associative
        //  array. E.G. var me = LLCommon.getUrlVars.getUrlVars()["me"];
        //  --------------------------------------------------------------------

        LLCommon.getUrlVars = function ( )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            var   vars          = [], hash;
            const hashes        = window.location.href.slice ( window.location.href.indexOf ( QUERY_STRING_START_DELIMITER ) + 1 ).split ( QUERY_STRING_PARAM_DELIMITER );

            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < hashes.length;
                      i++ )
            {
                hash = hashes [ i ].split ( EQUALS_CHAR );
                vars.push ( hash [ KEY_VALUE_PAIR_IS_KEY ] );
                vars [ hash [ KEY_VALUE_PAIR_IS_KEY ] ] = hash [ KEY_VALUE_PAIR_IS_VALUE ];
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < hashes.length; i++ )

            return vars;
        };  // LLCommon.getUrlVars


        LLCommon.setKendoDropDownWidth = function ( e )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            var   ddl           = e.sender;

            if ( !ddl ) ddl = e;

            var popupWidth      = ddl.popup.element.outerWidth ( );
            var boxWidth        = ddl.element.width ( );
            var id              = ddl.element [ 0 ].id;

            if ( popupWidth >= boxWidth )
            {
                ddl.list.css ( 'min-width' , boxWidth + 'px' );

                //  ------------------------------------------------------------
                //  If the list contains more than 6 items, then there is a
                //  vertical scrollbar and we need to add 20 to the width.
                //  ------------------------------------------------------------

                if ( ddl.dataSource.data ( ).length > 6 ) {
                    ddl.list.width ( ddl.list.width ( ) + 20 );
                } else {
                    ddl.list.width ( popupWidth );
                }
            }   // if ( popupWidth >= boxWidth )
        };  // LLCommon.setKendoDropDownWidth


        LLCommon.logPageChange = function ( toPageName )
        {
            const ISO_YEAR_CHARS        = 4;

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            //  ----------------------------------------------------------------
            //  Put this block first, to ensure that it happens on EVERY call.
            //  ----------------------------------------------------------------

            const docShowCopyrightYear  = document.getElementById ( 'ShowCopyrightYear' );

            if ( docShowCopyrightYear !== null )
            {
                const strTimeCheck      = LLCommon.DoAjax ( 'TimeCheck' ,
                                                             'GET' ,
                                                             {
                                                                 'TZOffset' : ( new Date ( ) ).getTimezoneOffset ( )
                                                             }
                                                           );

                docShowCopyrightYear.innerHTML = docShowCopyrightYear.innerHTML.replace ( 'Year' ,
                                                                                          strTimeCheck.substring ( SUBSTRING_FIRST_CHAR ,
                                                                                                                   ISO_YEAR_CHARS ) );

                //  ------------------------------------------------------------
                //  This function does nothing, failing gracefully, when the
                //  specified element cannot be found. The next one appears to
                //  have failed when I put it ahead of ShowOrHideElement.
                //  ------------------------------------------------------------

                LLCommon.ShowOrHideElement ( 'CopyrightNotice' ,
                                             LLCommon.ELEMENT_SHOW );
                docShowCopyrightYear.addEventListener ( 'click' ,
                                                        LLCommon.ShowLoadedScriptVersions );
            }   // if ( docShowCopyrightYear !== null )

            if ( localStorage.toPageName === undefined )
            {
                localStorage.toPageName = toPageName;
                localStorage.startTime  = new Date ( );

                return false;
            }   // if ( localStorage.toPageName === undefined )

            if ( localStorage.toPageName !== toPageName )
            {
                var startTime           = new Date ( localStorage.startTime );
                var leadid              = localStorage.leadId;

                //LLCommon.Trace('page change start time = ' + startTime + ' leadid =' + localStorage.leadId);
                //$.ajax({
                //    url: _llAppPath + "account/PutPageTracking",
                //    data: {
                //        "URL": localStorage.toPageName,
                //        "SDate": startTime.toUTCString(),
                //        "EDate": new Date().toUTCString(),
                //        "LeadId": leadid
                //    },
                //    type: "GET"
                //});

                localStorage.startTime  = new Date ( );
                localStorage.toPageName = toPageName;

                if ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID )
                {
                    localStorage.leadId = _leadid;
                }   // TRUE (The LeadId value is known.) block, if ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID )
                else
                {
                    localStorage.removeItem ( 'leadId' )
                }   // FALSE (The LeadId value is UNKNOWN.) block, if ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID )
            }   // if (localStorage.toPageName !== toPageName)
        };  // LLCommon.logPageChange


        /**
         * Enumerate the rest parameters of a function.
         * already have one.
         *
         * Accepts either an object that represents a document element or a
         * string that represents its ID attribute.
         *
         * @function LLCommon.enumerateRestParameters
         * @param {array of Object} ...paTheRestArgs - The rest parameters of a
         *                                             function
         * Returns: string enumerating arrays in order.
         */
        LLCommon.enumerateRestParameters = function ( ...paTheRestArgs )
        {
            if ( paTheRestArgs.length > ARRAY_IS_EMPTY )
            {
                let rstrRestParamValues = EMPTY_STRING;
                for ( let intJ = ARRAY_FIRST_ELEMENT;
                          intJ < paTheRestArgs.length;
                          intJ++ )
                {
                    rstrRestParamValues += `Parameter # ${LCommon.OrdinalFromIndex ( intJ )} of ${paTheRestArgs.length} = {paTheRestArgs [intJ ].toString ( )}`
                }   // for ( let intJ = ARRAY_FIRST_ELEMENT; intJ < paTheRestArgs.length; intJ++ )

                return rstrRestParamValues;
            }   // TRUE (The function has rest parameters.) block, if ( paTheRestArgs.length > ARRAY_IS_EMPTY )
            else
            {
                return 'The rest parameter list is empty.';
            }   // FALSE (The function is devoid of rest parameters.) block, if ( paTheRestArgs.length > ARRAY_IS_EMPTY )
        }


        LLCommon.initializeTimeZone = function ( )
        {
            const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

            if ( localStorage.AnyTZ === undefined )
            {
                localStorage.AnyTZ = true;
            }

            if ( localStorage.ESTTZ === undefined )
            {
                localStorage.ESTTZ = false;
            }

            if ( localStorage.CSTTZ === undefined )
            {
                localStorage.CSTTZ = false;
            }

            if ( localStorage.MSTTZ === undefined )
            {
                localStorage.MSTTZ = false;
            }

            if ( localStorage.PSTTZ === undefined )
            {
                localStorage.PSTTZ = false;
            }

            if ( localStorage.NonTZ === undefined )
            {
                localStorage.NonTZ = false;
            }

            if ( localStorage.OthTZ === undefined )
            {
                localStorage.OthTZ = false;
            }

            LLCommon.timeZones = localStorage.AnyTZ + "|" + localStorage.ESTTZ + "|" + localStorage.CSTTZ + "|" + localStorage.MSTTZ + "|" + localStorage.PSTTZ + "|" + localStorage.NonTZ + "|" + localStorage.OthTZ;
        };  // LLCommon.initializeTimeZone

        //  ------------------------------------------------------------------------
        //  The following functions began as a collection that came over from
        //  LeadLifeJSHelpersLib.js by way of LeadLifeJSHelpersGlobals.js. Many more
        //  have followed, and it mode as much sense to put them here as anywhere
        //  else.
        //  ------------------------------------------------------------------------

        LLCommon.AddOrRemoveStyles = function ( poElement , pstrSelectors , pfAddOrRemove )
        {
            /*
                ----------------------------------------------------------------
                Name:       AddOrRemoveStyles

                Goal:       Add or remove CSS selectors from an HTML element.

                Arguments:  poElement     = If this parameter is a string, it is
                                            assumed to be the ID of the element
                                            to which to add CSS selectors, or
                                            from which to remove them.

                                            Otherwise, it is assumed to be a
                                            reference to an element in the
                                            active document.

                            pstrSelectors = This parameter MUST be a string,
                                            which is expected to be composed of
                                            the name(s) of one or more CSS
                                            selectors.

                                            If the string identifies more than
                                            one selector, the names must be
                                            SPACE delimited, as they appear in a
                                            className attribute.

                            pfAddOrRemove = The truthineess of this Boolean flag
                                            determines whether the selectors in
                                            string pstrSelectors are ADDed to
                                            the classList of element poElement
                                            or REMOVEd from it.

                                            Be aware that this parameter MUST be
                                            specified EXPLICITLY unless your
                                            intent is to REMOVE the selectors
                                            specified in string pstrSelectors.

                                            This class implemeents two helper
                                            constants, LLCommon.CSS_SELECTOR_ADD
                                            and LLCommon.CSS_SELECTOR_REMOVE, to
                                            clearly document the value given for
                                            this parameter.

                Returns:    This method returns the classList object as it stood
                            on exit. Slnce classList is an array, its length can
                            be evaluated, and it can be iterated. Exceptions, if
                            any, are logged and signaled to the calling routine
                            by a return value of null.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            try
            {
                if ( Object.is ( poElement , undefined ) )
                {   // Ensure that the value of poElement is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter poElement is undefined.' );
                }   // if ( Object.is ( poElement , undefined ) )

                if ( Object.is ( pstrSelectors , undefined ) )
                {   // Ensure that the value of pstrSelectors is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter pstrSelectors is undefined.' );
                }   // if ( Object.is ( pstrSelectors , undefined ) )

                if ( !LLCommon.IsString ( pstrSelectors ) )
                {   // Ensure that the type of pstrSelectors is String.
                    throw new Error ( strMethodName + ': Required parameter pstrSelectors must be a string. Actual type = ' + ( typeof pstrSelectors ) );
                }   // if ( !LLCommon.IsString ( pstrSelectors ) )

                if ( pstrSelectors.length === EMPTY_STRING_LENGTH )
                {
                    throw new Error ( strMethodName + ': Required parameter pstrSelectors is the empty string, a non-actionable value.' );
                }   // if ( pstrSelectors.length === EMPTY_STRING_LENGTH )

                const docAfectedElement = LLCommon.IsString ( poElement )
                                          ? document.getElementById ( poElement )
                                          : poElement;

                if ( docAfectedElement === null )
                {   // If docAfectedElement is a null referents, it's game over.
                    throw new Error ( strMethodName + ': Required parameter poElement cannot be found in the active document.' );
                }   // if ( docAfectedElement === null )if ( docAfectedElement === null )

                const astrSelectors = pstrSelectors.split ( SPACE_CHARACTER );

                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < astrSelectors.length;
                          intJ++ )
                {
                    if ( pfAddOrRemove )
                    {   // Replacing the indexOf with the contains method on classList prevents accidental overmatches that could arise when other selector names contain the name of the target selector.
                        if ( !docAfectedElement.classList.contains ( astrSelectors [ intJ ] ) )
                        {   // Append the selector only when it is absent.
                            docAfectedElement.classList.add ( astrSelectors [ intJ ] );
                        }   // if ( !docAfectedElement.classList.contains ( astrSelectors [ intJ ] ) )
                    }   // TRUE (ADD selectors to the collection.) block, if ( pfAddOrRemove )
                    else
                    {
                        if ( docAfectedElement.classList.contains ( astrSelectors [ intJ ] ) )
                        {   // Remove the selector only when it is present.
                            docAfectedElement.classList.remove ( astrSelectors [ intJ ] );
                        }   // if ( docAfectedElement.classList.contains ( astrSelectors [ intJ ] ) )
                    }   // FALSE (REMOVE selectors from the collection.) block, if ( pfAddOrRemove )
                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < astrSelectors.length; intJ++ )

                return docAfectedElement.classList;
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return null;
            }
        }   // LLCommon.AddOrRemoveStyles


        /**
         * Ensure that an element has an `aria-label` attribute if it doesn't
         * already have one.
         *
         * Accepts either an object that represents a document element or a
         * string that represents its ID attribute.
         *
         * @function LLCommon.applyEssentialARIAProperties
         * @param {poTarget} element - The element whose `aria-label` attrobite
         *                             will be checked and added if missing
         *                             **and** the element has a valid 'title'.
         * @throws {Error} If the target element is not found or invalid.
         * Returns:
         *   void (applies DOM changes directly)
         */
        LLCommon.applyEssentialARIAProperties = function ( poTarget )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            try
            {
                const docTarget = LLCommon.resolveElement ( poTarget );

                if ( !docTarget.hasAttribute ( 'aria-label' ) && docTarget.title )
                {
                    docTarget.setAttribute ( 'aria-label', docTarget.title );
                }
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
            }
        }   // LLCommon.applyEssentialARIAProperties method


        /**
         * Apply default styles to an element, with optional overrides.
         * Accepts either a plain object of defaults or an existing CSSStyleDeclaration.
         *
         * @function LLCommon.applyStyleDefaultsWithOverrides
         * @param {HTMLElement} element - The element whose style will be set.
         * @param {Object|CSSStyleDeclaration} defaults - Default style properties and values.
         * @param {Object} [overrides] - Optional overrides to selectively replace defaults.
         */
        LLCommon.applyStyleDefaultsWithOverrides = function ( element, defaults, overrides )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            // Normalize defaults: if it's a CSSStyleDeclaration, convert to plain object.
            let normalizedDefaults = { };

            if ( defaults instanceof CSSStyleDeclaration )
            {
                for ( let i = 0;
                          i < defaults.length;
                          i++ )
                {
                    const prop                  = defaults [ i ];
                    normalizedDefaults [ prop ] = defaults.getPropertyValue ( prop );
                }   // for ( let i = 0; i < defaults.length; i++ )
            }   // TRUE (Defaults is a CSSStyleDeclaration object.) block, if ( defaults instanceof CSSStyleDeclaration )
            else if ( typeof defaults === 'object' && defaults !== null )
            {
                normalizedDefaults = defaults;
            }   // ELSE (Defaults is an ordinary JavaScript object.) block, if ( defaults instanceof CSSStyleDeclaration )

            // Merge defaults + overrides.
            const effectiveStyle = { ...normalizedDefaults, ...( overrides || { } ) };

            // Apply to element.
            for ( const [ key, value ] of Object.entries ( effectiveStyle ) )
            {
                element.style [ key ] = value;
            }   // for ( const [ key, value ] of Object.entries ( effectiveStyle ) )
        };  // LLCommon.applyStyleDefaultsWithOverrides


        LLCommon.AudioVideoPlayer = function ( AbsoluteServerFileName , OffsetTimeSeconds , LeadId , VideoTitle , Position )
        {
            /*
                ----------------------------------------------------------------
                Name:       AudioVideoPlayer

                Goal:       Create the window.open script that goes into a Click
                            event of the HTML tag that is expected to open the
                            SalesTalk Multimedia Player to render the file given
                            by the first argument, AbsoluteServerFileName.

                Arguments:  AbsoluteServerFileName  = This string specifies the
                                                      absolute name of a file in
                                                      a LeadLife repository.

                            OffsetTimeSeconds       = This string specifies the
                                                      offset, in seconds, from
                                                      the beginning of the
                                                      multimedia file at which
                                                      to begin playback.

                            LeadId                  = This string specifies the
                                                      string representation of a
                                                      Lead ID to pass along for
                                                      logging.

                            VideoTitle              = This OPTIONAL string is
                                                      displayed as the Title
                                                      attribute of the player
                                                      window.

                            Position                = This optional string sets
                                                      the position of the left
                                                      edge of the playwr window.

                Returns:    Since it is the subject of an event listener, the
                            appropriate return value is FALSE, to suppress the
                            default event listener because the event is handled.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            LLCommon.Trace ( strMethodName +': AbsoluteServerFileName = ' + AbsoluteServerFileName );
            LLCommon.Trace ( strMethodName +': OffsetTimeSeconds      = ' + OffsetTimeSeconds );
            LLCommon.Trace ( strMethodName +': LeadId                 = ' + LeadId );

            debugger;

            try
            {
                if ( LLCommon.IsString ( AbsoluteServerFileName ) && AbsoluteServerFileName.length > EMPTY_STRING_LENGTH )
                {
                    LLCommon.Trace ( strMethodName +': Setting strURL2Play' );
                    const strURL2Play   = LLCommon.IsAbsoluteUri ( AbsoluteServerFileName )
                                          ? AbsoluteServerFileName
                                          : LLCommon.DoAjax ( 'GetURLForFileName' ,
                                                              'GET' ,
                                                              {
                                                                  'FileSystemPath' : AbsoluteServerFileName
                                                              }
                                                            );

                    LLCommon.Trace ( strMethodName +': strURL2Play = ' + strURL2Play );

                    if ( strURL2Play.indexOf ( LLCommon.EXCEPTION_MSG_PREFIX ) === INDEXOF_NOT_FOUND )
                    {
                        if ( ( strURL2Play.match ( /\.[WM][AP4][V34A]$/i ) > EMPTY_STRING ) || ( strURL2Play.match ( /\.TXT$/i ) > EMPTY_STRING ) || ( strURL2Play.match( /\.VTT$/i ) > EMPTY_STRING ) )
                        {
                            const strOffset             = ( OffsetTimeSeconds !== undefined && OffsetTimeSeconds > NUMERIC_ZERO ) ? OffsetTimeSeconds : CHARACTER_ZERO;
                            const strLeadIdToken        = ( parseInt ( LeadId ) ) !== NaN ? QUERY_STRING_PARAM_DELIMITER + KEY_IS_LEAD_ID + EQUALS_CHAR + LeadId : EMPTY_STRING;

                            //  ------------------------------------------------
                            //  When executing from a STAGING page, load the
                            //  STAGING version of the player page. This hack
                            //  avoids the need for a fixup during promotion.
                            //  ------------------------------------------------

                            const strPlayerURLSuffix    = location.pathname.indexOf ( '/STAGING/' ) == INDEXOF_NOT_FOUND ? 'COMMON/STT_VideoPlayer.HTML' : 'COMMON/STAGING/STT_VideoPlayer.HTML';
                            const strAbsolutePlayerURL  = location.origin + _llAppPath + strPlayerURLSuffix;
                            var   strURL                = strAbsolutePlayerURL + '?m4vurl=' + encodeURIComponent ( strURL2Play.replace( /^https*:/i, 'HTTPS:' ) ) + '&start=' + strOffset + strLeadIdToken

                            if ( VideoTitle !== undefined && VideoTitle !== null && VideoTitle !== EMPTY_STRING )
                            {
                                LLCommon.Trace ( strMethodName +': VideoTitle         = ' + VideoTitle );
                                strURL += '&title=' + encodeURIComponent ( VideoTitle );
                            }   // if ( VideoTitle !== undefined && VideoTitle !== null && VideoTitle !== EMPTY_STRING )

                            const strWindowName         = 'STTPlayer';
                            const strWindowFeatures     =   'width=700,height=550,left='
                                                          + SetLeft ( Position )
                                                          + ',top=50,resizable=1';

                            LLCommon.Trace ( strMethodName +': URL2Play           = ' + strURL2Play );
                            LLCommon.Trace ( strMethodName +': OffsetSeconds      = ' + strOffset );
                            LLCommon.Trace ( strMethodName +': LeadIdToken        = ' + strLeadIdToken );
                            LLCommon.Trace ( strMethodName +': PlayerURLSuffix    = ' + strPlayerURLSuffix );
                            LLCommon.Trace ( strMethodName +': AbsolutePlayerURL  = ' + strAbsolutePlayerURL );
                            LLCommon.Trace ( strMethodName +': Window Name        = ' + strWindowName );
                            LLCommon.Trace ( strMethodName +': URL popup          = ' + strURL );
                            LLCommon.Trace ( strMethodName +': Window Features    = ' + strWindowFeatures );

                            localStorage.setItem ( 'PlayerURL'            , strURL );
                            localStorage.setItem ( 'PlayerLinkURL'        , strURL2Play );
                            localStorage.setItem ( 'PlayerLeadId'         , LeadId );
                            localStorage.setItem ( 'PlayerWindowName'     , strWindowName );
                            localStorage.setItem ( 'PlayerWindowFeatures' , strWindowFeatures );
                            localStorage.setItem ( 'leadid'               , LeadId );

                            window.open ( strURL ,
                                          strWindowName ,
                                          strWindowFeatures );
                            return false;
                        }   // TRUE (anticipated outcome) block, if ( ( strUrl.match ( /\.[WM][AP4][Vv34A]$/i ) > EMPTY_STRING ) || ( strUrl.match ( /\.[TX][T$/i ) > EMPTY_STRING ) || ( strUrl.match( /\.VTT$/i ) > EMPTY_STRING ) )
                        else
                        {
                            throw new Error ( 'The specified URL, "' + strURL2Play + '" is invalid for the SalesTalk Multimedia Player.' );
                        }   // FALSE (unanticipated outcome) block, if ( ( strUrl.match ( /\.[WM][AP4][Vv34A]$/i ) > EMPTY_STRING ) || ( strUrl.match ( /\.[TX][T$/i ) > EMPTY_STRING ) || ( strUrl.match( /\.VTT$/i ) > EMPTY_STRING ) ))
                    }   // TRUE (anticipated outcome) block, if ( strURL2Play.indexOf ( LLCommon.EXCEPTION_MSG_PREFIX ) == INDEXOF_NOT_FOUND )
                    else
                    {
                        throw new Error ( strURL2Play );
                    }   // FALSE (unanticipated outcome) block, if ( strURL2Play.indexOf ( LLCommon.EXCEPTION_MSG_PREFIX ) == INDEXOF_NOT_FOUND )
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( AbsoluteServerFileName ) && AbsoluteServerFileName.length > EMPTY_STRING_LENGTH )
                else
                {
                    throw new Error ( strMethodName + ': Argument AbsoluteServerFileName must be a String that has a length greater than zero.' );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( AbsoluteServerFileName ) && AbsoluteServerFileName.length > EMPTY_STRING_LENGTH )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return false;
            }

            function SetLeft ( pstrPosition )
            {
                //  ------------------------------------------------
                //  Unless pstrPosition is undefined, its value
                //  resembles 'w=0.5' which is parsed to yield a
                //  fraction of the available width to assign as the
                //  left edge of the window.
                //
                //  When pstrPosition is undefined, assign a default
                //  position of 50 pixels from the left edge.
                //  ------------------------------------------------

                if ( Object.is ( pstrPosition , undefined ) )
                {
                    return 50;
                }   // TRUE (The optional pstrPosition argument is absent.) block, if ( Object.is ( pstrPosition , undefined ) )
                else
                {
                    if ( LLCommon.IsString ( pstrPosition ) )
                    {
                        if ( pstrPosition.substring ( SUBSTRING_FIRST_CHAR , 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )
                        {
                            var strLeftPos = pstrPosition.substring ( 2 );
                            var dblLeftPosFraction = parseFloat ( strLeftPos );
                            var intMaxLeft = Math.round ( Screen.availWidth / 2 );

                            if ( isNaN ( dblLeftPosFraction ) )
                            {
                                var intLeftPosition = parseInt ( strLeftPos , 10 );

                                if ( isNaN( intLeftPosition ) )
                                {
                                    return 50;
                                }   // TRUE (unanticipated outcome) block, if ( isNaN ( intLeftPosition ) )
                                else
                                {
                                    return intLeftPosition > intMaxLeft ? intMaxLeft : intLeftPosition;
                                }   // FALSE (anticipated outcome) block, if ( isNaN ( intLeftPosition ) )
                            }   // TRUE (unanticipated outcome) block, if ( isNaN ( dblLeftPosFraction ) )
                            else
                            {
                                if ( dblLeftPosFraction > 1.0 )
                                {
                                    return intMaxLeft;
                                }   // TRUE (unanticipated outcome) block, if ( dblLeftPosFraction > 1.0 )
                                else
                                {
                                    return Math.round ( Screen.availWidth * dblLeftPosFraction );
                                }   // FALSE (anticipated outcome) block, if ( dblLeftPosFraction > 1.0 )
                            }   // FALSE (anticipated outcome) block, if ( isNaN ( dblLeftPosFraction ) )
                        }   // TRUE (The first two characters and its length are acceptable.) block, if ( pstrPosition.substring ( SUBSTRING_FIRST_CHAR , 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )
                        else
                        {   // The format of the string is incorrect. Treat as if absent.
                            return 50;
                        }   // FALSE (Either the first two characters or the overall length is wrong.) block, if ( pstrPosition.substring ( SUBSTRING_FIRST_CHAR , 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )
                    }   // TRUE (The optional pstrPosition argument has the expected type.) block, if ( typeof pstrPosition === 'string' || pstrPosition instanceof String )
                    else
                    {   // The argument type is not String. Treat as if absent.
                        return 50;
                    }   // FALSE (Though present, the optional PlayerLink has the incorrect type.) block, if ( typeof pstrPosition === 'string' || pstrPosition instanceof String )
                }   // FALSE (The optional pstrPosition argument is present.) block, if ( Object.is ( pstrPosition , undefined ) )
            }   // function SetLeft
        }   // LLCommon.AudioVideoPlayer method


        /**
         * Evaluate the value of a System Configuration Key
         * Accepts either a plain object of defaults or an existing CSSStyleDeclaration.
         *
         * @function LLCommon.checkSystemConfigAndUserOverride
         * @param {string} pstrBaseKeyName          - Name of base configuration
         *                                            key
         * @param {string} pstrDefaultValue         - Default value to return if
         *                                            not found
         * @param {Boolean} [pfActionWhenNoBaseKey] - Optional Boolean flag that
         *                                            if True causes the user
         *                                            override to be always
         *                                            evaluated
         */
        LLCommon.checkSystemConfigAndUserOverride = function ( pstrBaseKeyName, pstrDefaultValue, pfActionWhenNoBaseKey )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            let strValueString  = LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                    'GET',
                                                    {
                                                        'monikor'      : pstrBaseKeyName,
                                                        'tenantId'     : LLCommon.TenantId,
                                                        'domainId'     : LLCommon.DomainId,
                                                        'defaultValue' : pstrDefaultValue,
                                                        'Behavior'     : GetByMonikorFirstBehavior.IgnoreWebConfig
                                                    } );

            if ( strValueString === pstrDefaultValue || pfActionWhenNoBaseKey )
            {
                strValueString = LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                    'GET',
                                                    {
                                                        'monikor'      : ( pstrBaseKeyName + SPACE_CHARACTER + LLCommon.UserInfo.AgentLoginEmailId ),
                                                        'tenantId'     : LLCommon.TenantId,
                                                        'domainId'     : LLCommon.DomainId,
                                                        'defaultValue' : strValueString,
                                                        'Behavior'     : GetByMonikorFirstBehavior.IgnoreBoth
                                                    } );
            }   // if ( strValueString === pstrDefaultValue || pfActionWhenNoBaseKey )

            return strValueString;
        }   // LLCommon.checkSystemConfigAndUserOverride


        LLCommon.CheckUserName = function ( pfIgnoreAuthenticatedLogin )
        {
            /*
                ----------------------------------------------------------------
                Name:       CheckUserName

                Goal:       Unless a login was passed in via the URL, check the
                            UserName key in localStorage. If present, assign its
                            value to _login and set _loginSource to
                            SRC_IS_LOCAL_STORAGE. Otherwise, log an exception
                            and return.

                Arguments:  pfIgnoreAuthenticatedLogin      = Boolean flag to
                                                              force bypass of
                                                              _loginSource

                Returns:    Void (nothing). Object variables get new values.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            if ( _loginSource === SRC_IS_UNKNOWN )
            {
                if ( ( !pfIgnoreAuthenticatedLogin ) && ( localStorage.getItem ( 'UserName' ) !== null ) )
                {
                    LLCommon.LogException (   strMethodName
                                            + ' encountered an unexpected Login ID mismatch. Reluctantly trusting localStorage name.'
                                            + ': AJAX call to GetLoginIdOfLoggedInUser for loginName = EMPTY STRING'
                                            + ' LocalStorage key localStorage.UserName value = ' + localStorage.UserName );
                    _login                      = localStorage.getItem ( 'UserName' );
                    _loginSource                = SRC_IS_LOCAL_STORAGE;
                }   // TRUE (marginally acceptable outcome) block, if ( ( !pfIgnoreAuthenticatedLogin ) && ( localStorage.getItem ( 'UserName' ) !== null ) )
                else
                {
                    debugger;

                    if ( _useridSource !== SRC_IS_UNKNOWN )
                    {
                        debugger;
                        LLCommon.UserInfo   = LLCommon.DoAjax ( 'GetBasicSalesTalkUserInfo',
                                                                'GET',
                                                                {
                                                                    'UserId' : _userid
                                                                });

                        //  ----------------------------------------------------
                        //  Since the user ID supplied in the URL checks out, it
                        //  is allowed to trump the login ID, even if it belongs
                        //  to an authenticated user. There is method in our
                        //  madness.
                        //  ----------------------------------------------------

                        if ( LLCommon.UserInfo !== null )
                        {
                            _domainid                       = LLCommon.UserInfo.AgentDomainId;
                            _domainname                     = LLCommon.UserInfo.AgentDomainName;
                            _tenantid                       = LLCommon.UserInfo.AgentTenantId;
                            _login                          = LLCommon.UserInfo.AgentLoginEmailId;

                            LLCommon.DialerLogin            = LLCommon.UserInfo.AgentLoginEmailId;

                            _domainidSource                 = SRC_IS_USERID_PER_URL;
                            _domainnameSource               = SRC_IS_USERID_PER_URL;
                            _loginSource                    = SRC_IS_USERID_PER_URL;
                            _tenantidSource                 = SRC_IS_USERID_PER_URL;

                            _fDomainAndTenantIDAreSafe      = true;
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.UserInfo !== null )
                        else
                        {
                            LLCommon.LogException (   strMethodName
                                                    + ' encountered an unexpected MISSING login ID. The next call to the server will register a failure.'
                                                    + ': AJAX call to GetLoginIdOfLoggedInUser for loginName = EMPTY STRING'
                                                    + ': localStorag query for UserName = NULL' );
                        }	// FALSE (unanticipated outcome) block, if ( LLCommon.UserInfo !== null )
                    }   // TRUE (preferred outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                    else
                    {
                        LLCommon.LogException (   strMethodName
                                                + ' encountered an unexpected MISSING login ID. The next call to the server will register a failure.'
                                                + ': AJAX call to GetLoginIdOfLoggedInUser for loginName = EMPTY STRING'
                                                + ': localStorag query for UserName = NULL' );
                    }   // FALSE (unfavorable outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                }   // FALSE (Either there is no sign of a usable login name, or we've been told to override it in favor of the supplied UserId.) block, if ( ( !pfIgnoreAuthenticatedLogin ) && ( localStorage.getItem ( 'UserName' ) !== null ) )
            }   // TRUE (The URL omits mention of a login ID.) block, if ( _loginSource === SRC_IS_UNKNOWN )
            else
            {
                console.log ( strMethodName +': Accepting at face value Login ID per ' + DisplayGlobalParameterSource ( _loginSource ) + ' = ' + _login );
            }   // FALSE (The login supplied in the URL trumps everything else.) block, if ( _loginSource === SRC_IS_UNKNOWN )
        }   // LLCommon.CheckUserName method


        LLCommon.CloseNotesSearchDialog = function ( poThisButton )
        {
            /*
                ----------------------------------------------------------------
                Name:       CloseNotesSearchDialog

                Goal:       Create the button that invokes a ChatGPT transcript
                            summary generator method.

                Arguments:  poThisButton        = This JavaScript Object should
                                                  be a reference to the button
                                                  for which it answers a Click
                                                  event.

                Returns:    To prevent the default behavior of a button, which
                            raises a Submit event, this function returns FALSE.

                Remarks:    Though this method could use the Event object that
                            is routinely passed int an event delegate, passing a
                            reference to the object through an argument makes
                            its properties more readily accessible.

                            It uses the object reference to find its parent, and
                            use a CSS selector to hide it. For good measure, it
                            also clears its innerHTML, thereby releasing memory.

                            If anything is out of order, such as that the input
                            argument is not a Button or its parent Element isn't
                            a DIVision, this method does nothing, although it
                            does in that case return TRUE, allowing the default
                            event to happen.
                ----------------------------------------------------------------
            */

            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( poThisButton !== null && poThisButton instanceof Element )
            {
                const docThisParent = poThisButton.parentElement;

                if ( docThisParent !== null && docThisParent.nodeName === 'DIV' )
                {
                    docThisParent.innerHTML = EMPTY_STRING;
                    LLCommon.ShowOrHideElement ( docThisParent , LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( 'wrapper'     , LLCommon.ELEMENT_SHOW );

                    return false;
                }   // TRUE (anticipated outcome) block, if ( docThisParent !== null && docThisParent.nodeName === 'DIV' )
                else
                {
                    return true;
                }   // FALSE (unanticipated outcome) block, if ( docThisParent !== null && docThisParent.nodeName === 'DIV' )
            }   // TRUE (anticipated outcome) block, if ( poThisButton !== null && poThisButton instanceof Element )
            else
            {
                return true;
            }   // FALSE (unanticipated outcome) block, if ( poThisButton !== null && poThisButton instanceof Element )
        }   // LLCommon.CloseNotesSearchDialog


        LLCommon.ConvertSecondsToMinutes = function ( pintSeconds , pfIncludeHours )
        {
            /*
                ----------------------------------------------------------------
                Name:       ConvertSecondsToMinutes

                Goal:       Convert a time expressed in whole seconds to minutes
                            and optionally hours.

                Arguments:  pintSeconds         = This parameter is expected to
                                                  either be a primitive Integer
                                                  or be convertible to one by
                                                  the parseInt function.

                            pfIncludeHours      = This parameter is treated as a
                                                  Boolean, and its truthiness is
                                                  evaluated.

                                                  When it evaluates to TRUE, the
                                                  function converts minutes to
                                                  hours, returning a complete
                                                  time in hours, minutes, and
                                                  seconds.

                                                  Otherwise, the returned string
                                                  represents the time in minutes
                                                  and seconds ONLY.

                Returns:    By default, this function returns a time in `mm:ss`
                            format. However, when parameter pfIncludeHours has a
                            truthy value, the format is `hh:mm:ss`, where `hh`
                            may be any number of hours, thus allowing it to show
                            the time of a long call in an easily understood time
                            format.

                Algorithm:  1)  Verify that input parameter pintSeconds is an
                                integer or a string that can be converted to an
                                integer.

                            2)  Use the built-in parseInt function to convert
                                pintSeconds to a native primitive Integer called
                                intTotalSeconds. The optional radix parameter is
                                specified as 10 to guarantee that the returned
                                value is represented in the Decimal (base 10)
                                number system.

                            3)  Perform an integer division by 60, the number of
                                seconds in one minute, to obtain the number of
                                whole minutes represented by the time. For this,
                                Math.floor rounds the floating point quotient
                                down to the nearest whole number.

                            4)  Use the Modulus operator to derive the number of
                                seconds, apart from those that represent whole
                                minutes, in input time intTotalSeconds.

                            5)  Next, unless input parameter pfIncludeHours has
                                a truthy value, if the numberr of minutes
                                exceeds 999, display all 9's for both minutes
                                and seconds.

                            6)  Otherwise, when input parameter pfIncludeHours
                                has a truthy value and the minutes value exceeds
                                59, the hours and remaining minutes that are the
                                remaining fractionl hour are computed in the
                                same way as were the minutes, by first feeding
                                the quotient of total minutes and 60 through the
                                Math.floor function, then using the modulus
                                operator to obtain the number of minutes in the
                                fractional hour.

                            7)  If the remaining seconds value is less than 10,
                                prefix the value with a leading zero, making it
                                a two-digit number.

                            8)  When input parameter pfIncludeHours has a truthy
                                value, the remaining minutes value gets the same
                                treatment, so that it, too, is a two-digit
                                number.

                            9)  Finally, the output string is constructed from
                                the two (or three, when hours are required)
                                numeric values, of which the two that represent
                                minutes and seconds are guaranteed to have two
                                digits, so that they follow the expected pattern
                                in the final time string.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            var rstrDuration            = ' 000:00';

            if ( ! ( pintSeconds === null || pintSeconds === '' || pintSeconds === undefined ) && ( LLCommon.IsValidInteger ( pintSeconds ) ) )
            {
                var intTotalSeconds     = parseInt ( pintSeconds , 10 );
                var objMinutes          = Math.floor ( intTotalSeconds / 60 );
                var objSeconds          = intTotalSeconds % 60;

                var intHours            = null;

                if ( objMinutes > 999 && ( !pfIncludeHours ) )
                {
                    objMinutes          = 999;
                    objSeconds          = 99;
                }   // TRUE (legacy use case) block, if ( objMinutes > 999 && ( !pfIncludeHours ) )
                else
                {
                    if ( pfIncludeHours && objMinutes > 59 )
                    {
                        intHours        = Math.floor ( objMinutes / 60 );
                        objMinutes      = objMinutes % 60;
                    }   // if ( pfIncludeHours && objMinutes > 60 )
                }   // FALSE (extending to cover times measured in hours and minutes) block, if ( objMinutes > 999 && ( !pfIncludeHours ) )

                if ( objMinutes < 10 && pfIncludeHours && intHours !== null )
                {
                    objMinutes          = CHARACTER_ZERO + objMinutes;
                }   // if ( objMinutes < 10 && pfIncludeHours && intHours !== null )

                if ( objSeconds < 10 )
                {
                    objSeconds = CHARACTER_ZERO + objSeconds;
                }   // if ( objSeconds < 10 )

                rstrDuration            =   ( ( pfIncludeHours && intHours !== null )
                                            ? ( intHours + TIME_SEPARATOR_CHAR )
                                            : EMPTY_STRING )
                                          + objMinutes
                                          + TIME_SEPARATOR_CHAR
                                          + objSeconds;
            }   // if ( ! ( pintSeconds === null || pintSeconds === '' || pintSeconds === undefined ) && ( LLCommon.IsValidInteger ( pintSeconds ) ) )

            return rstrDuration;
        }   // LLCommon.ConvertSecondsToMinutes


        LLCommon.CreateChatGPTXscripSummarytButton = function ( pstrButtonID , pstrButtonFaceText , pstrContainerId , pstrButtonStyleName , pfAppend2Transcript )
        {
            /*
                ----------------------------------------------------------------
                Name:       CreateChatGPTXscripSummarytButton

                Goal:       Create the button that invokes a ChatGPT transcript
                            summary generator method.

                Arguments:  pstrButtonID        = This string specifies the ID
                                                  and name to assign to the
                                                  returned Button.

                            pstrButtonFaceText  = This string specifies the text
                                                  to display on the Button.

                            pstrContainerId     = This string specifies the ID
                                                  of the container, usually a
                                                  DIV, that contains the text to
                                                  summarize.

                            pstrButtonStyleName = This string specifies the
                                                  logical-negate delimited list
                                                  of CSS selector names to
                                                  attach to the returned Button.

                            pfAppend2Transcript = When this value evaluates to
                                                  Boolean TRUE, the summary is
                                                  appeended to the transcript.
                                                  Please see the Remarks.

                Returns:    The return value is a reference to the Document
                            Element to insert into the active document.
                ----------------------------------------------------------------
            */

            const fnClick4Summary = ( poEvent , pfAppend2Transcript ) =>
            {   // This function has the same argument list as CreateChatGPTXscripSummarytButton, but it returns void.
                debugger;
                poEvent.currentTarget.disabled = true;
                LLCommon.SummarizeText ( poEvent.currentTarget.value , pfAppend2Transcript );
                poEvent.currentTarget.disabled = false;
            }   // const fnClick4Summary = ( poEvent , pfAppend2Transcript ) =>


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
            {
                if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH )
                {
                    const rdocBtn       = document.createElement ( 'button' );

                    rdocBtn.value       = pstrContainerId;
                    rdocBtn.innerHTML   = LLCommon.IsString ( pstrButtonFaceText ) ? pstrButtonFaceText : 'Summarize';
                    rdocBtn.title       =   'Click or tap this button to request ChatGPT 4 to summarize the selected transcript'
                                          + pfAppend2Transcript ? ' and append it to the transcript.' : '.';
                    rdocBtn.id          = pstrButtonID;
                    rdocBtn.type        = 'button';
                    rdocBtn.onclick     = ( poEvent , pfAppend2Transcript ) => { fnClick4Summary ( poEvent , pfAppend2Transcript ); }

                    //  ------------------------------------------------------------
                    //  Since CSS styles are technically optional, skipping them is
                    //  a benign error, although the appearance of the buttton may
                    //  fall short of your expectations.
                    //  ------------------------------------------------------------

                    if ( LLCommon.IsString ( pstrButtonStyleName ) )
                    {
                        if ( pstrButtonStyleName.length > EMPTY_STRING_LENGTH )
                        {
                            const astrClass = pstrButtonStyleName.split ( LOGICAL_NEGATE );

                            for ( var intCurrStyleNameIndex = ARRAY_FIRST_ELEMENT;
                                      intCurrStyleNameIndex < astrClass.length;
                                      intCurrStyleNameIndex++ )
                            {
                                rdocBtn.classList.add ( astrClass [ intCurrStyleNameIndex ] );
                            }   // for ( var intCurrStyleNameIndex = ARRAY_FIRST_ELEMENT; intCurrStyleNameIndex < astrClass.length; intCurrStyleNameIndex++ )
                        }   // if ( pstrButtonStyleName.length > EMPTY_STRING_LENGTH )
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrButtonStyleName ) ))

                    return rdocBtn;
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH ))
                else
                {
                    LLCommon.LogException ( strMethodName + ': Argument pstrContainerId must be a String, which cannot be empty. Type = ' + ( typeof pstrContainerId ) + ', Value = ' + pstrContainerId );
                    return null;
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH ))
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
            else
            {
                LLCommon.LogException ( strMethodName + ': Argument pstrButtonID must be a String, which cannot be empty. Type = ' + ( typeof pstrButtonID ) + ', Value = ' + pstrButtonID );
                return null;
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
        }   // LLCommon.CreateChatGPTXscripSummarytButton method


        LLCommon.CreateCopy2ClipboardButton = function ( pstrButtonID , pstrButtonFaceText , pstrContainerId , pstrButtonStyleName )
        {
            /*
                ----------------------------------------------------------------
                Name:       CreateChatGPTXscripSummarytButton

                Goal:       Create the button that invokes a copy to clipboard.

                Arguments:  pstrButtonID        = This string specifies the ID
                                                  and name to assign to the
                                                  returned Button.

                            pstrButtonFaceText  = This string specifies the text
                                                  to display on the Button.

                            pstrContainerId     = This string specifies the ID
                                                  of the container, usually a
                                                  DIV, that contains the text to
                                                  copy onto the clipboard.

                            pstrButtonStyleName = This string specifies the
                                                  logical-negate delimited list
                                                  of CSS selector names to
                                                  attach to the returned Button.

                Returns:    The return value is a reference to the Document
                            Element to insert into the active document.
                ----------------------------------------------------------------
            */

            const fnClick4Copy2Cb = ( poEvent ) =>
            {   // This function has the same argument list as CreateChatGPTXscripSummarytButton, but it returns void.
                debugger;
                poEvent.currentTarget.disabled = true;
                LLCommon.PasteTextOntoClipboard  ( poEvent ,
                                                   pstrContainerId );
                poEvent.currentTarget.disabled = false;
            }   // const fnClick4Copy2Cb = ( poEvent ) =>


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
            {
                if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH )
                {
                    const rdocBtn       = document.createElement ( 'button' );

                    rdocBtn.value       = pstrContainerId;
                    rdocBtn.innerHTML   = LLCommon.IsString ( pstrButtonFaceText ) ? pstrButtonFaceText : 'Summarize';
                    rdocBtn.title       = 'Click or tap this button to request the text to be copied into the device clipboard. If nothing happens, select the text, then hit CTRL-C or COMMAND-C.';
                    rdocBtn.id          = pstrButtonID;
                    rdocBtn.type        = 'button';
                    rdocBtn.onclick     = ( poEvent ) => { fnClick4Copy2Cb ( poEvent ); }

                    //  ------------------------------------------------------------
                    //  Since CSS styles are technically optional, skipping them is
                    //  a benign error, although the appearance of the buttton may
                    //  fall short of your expectations.
                    //  ------------------------------------------------------------

                    if ( LLCommon.IsString ( pstrButtonStyleName ) )
                    {
                        if ( pstrButtonStyleName.length > EMPTY_STRING_LENGTH )
                        {
                            const astrClass = pstrButtonStyleName.split ( LOGICAL_NEGATE );

                            for ( var intCurrStyleNameIndex = ARRAY_FIRST_ELEMENT;
                                      intCurrStyleNameIndex < astrClass.length;
                                      intCurrStyleNameIndex++ )
                            {
                                rdocBtn.classList.add ( astrClass [ intCurrStyleNameIndex ] );
                            }   // for ( var intCurrStyleNameIndex = ARRAY_FIRST_ELEMENT; intCurrStyleNameIndex < astrClass.length; intCurrStyleNameIndex++ )
                        }   // if ( pstrButtonStyleName.length > EMPTY_STRING_LENGTH )
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrButtonStyleName ) ))

                    return rdocBtn;
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH ))
                else
                {
                    LLCommon.LogException ( strMethodName + ': Argument pstrContainerId must be a String, which cannot be empty. Type = ' + ( typeof pstrContainerId ) + ', Value = ' + pstrContainerId );
                    return null;
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH ))
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
            else
            {
                LLCommon.LogException ( strMethodName + ': Argument pstrButtonID must be a String, which cannot be empty. Type = ' + ( typeof pstrButtonID ) + ', Value = ' + pstrButtonID );
                return null;
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrButtonID ) && pstrButtonID.length > EMPTY_STRING_LENGTH )
        }   // LLCommon.CreateCopy2ClipboardButton method


        LLCommon.CreateHourglassIconContainer = function ( pstrContainerId , pstrBoxId , pstrHourglassURITag , pstrHourglassId , pintEmptyCellCount , pstrHourglassStyle )
        {
            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH )
            {
                if ( LLCommon.IsString ( pstrBoxId ) && pstrBoxId.length > EMPTY_STRING_LENGTH )
                {
                    if ( LLCommon.IsString ( pstrHourglassURITag ) && pstrHourglassURITag.length > EMPTY_STRING_LENGTH )
                    {
                        const docHourglassURITag                = document.getElementById ( pstrHourglassURITag );

                        if ( docHourglassURITag !== null )
                        {
                            if ( docHourglassURITag.innerText.length > EMPTY_STRING_LENGTH )
                            {
                                if ( LLCommon.IsString ( pstrHourglassId ) && pstrHourglassId.length > EMPTY_STRING_LENGTH )
                                {
                                    const rdocContainerRow      = document.createElement ( 'tr' );
                                    rdocContainerRow.id         = pstrContainerId;

                                    if ( pintEmptyCellCount !== undefined && LLCommon.IsValidInteger ( pintEmptyCellCount ) && pintEmptyCellCount > NUMERIC_ZERO )
                                    {
                                        var docPlaceholder;

                                        for ( var intJ = NUMERIC_ZERO;
                                                  intJ < pintEmptyCellCount;
                                                  intJ++ )
                                        {
                                            docPlaceholder              = document.createElement ( 'td' );
                                            docPlaceholder.innerHTML    = HTML_NBSP;
                                            rdocContainerRow.appendChild ( docPlaceholder );
                                        }   // for ( var intJ = NUMERIC_ZERO; intJ < pintEmptyCellCount; intJ++ )
                                    }   // if ( pintEmptyCellCount !== undefined && LLCommon.IsValidInteger ( pintEmptyCellCount ) && pintEmptyCellCount > NUMERIC_ZERO )

                                    const docContainerBox       = document.createElement ( 'td' );
                                    docContainerBox.id          = pstrBoxId;

                                    const docHourglassImage     = document.createElement ( 'img' );
                                    docHourglassImage.id        = pstrHourglassId;
                                    docHourglassImage.src       = docHourglassURITag.innerText;

                                    if ( LLCommon.IsString ( pstrHourglassStyle ) && pstrHourglassStyle.length > EMPTY_STRING_LENGTH )
                                    {
                                        docHourglassImage.style = pstrHourglassStyle;
                                    }   // if ( LLCommon.IsString ( pstrHourglassStyle ) && pstrHourglassStyle.length > EMPTY_STRING_LENGTH )

                                    docContainerBox.appendChild ( docHourglassImage );
                                    rdocContainerRow.appendChild ( docContainerBox );

                                    return rdocContainerRow;
                                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrHourglassId ) && pstrHourglassId.length > EMPTY_STRING_LENGTH )
                                else {
                                    throw new Error ( strMethodName + ': Argument pstrHourglassId must be a String that has a length greater than zero.' );
                                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrHourglassId ) && pstrHourglassId.length > EMPTY_STRING_LENGTH )
                            }   // TRUE (anticipated outcome) block, if ( docHourglassURITag.innerText.length > EMPTY_STRING_LENGTH )
                            else
                            {
                                throw new Error ( strMethodName + ': Argument pstrHourglassURITag points to an element that has an empty innerText.' );
                            }   // FALSE (unanticipated outcome) block, if ( docHourglassURITag.innerText.length > EMPTY_STRING_LENGTH )
                        }   // TRUE (anticipated outcome) block, if ( docHourglassURITag !== null )
                        else
                        {
                            throw new Error ( strMethodName + ': Argument pstrHourglassURITag must identify the element that has as its innerText attribute the URI of the hourglass imagee tag.' );
                        }   // FALSE (unanticipated outcome) block, if ( docHourglassURITag !== null )
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrHourglassURITag ) && pstrHourglassURITag.length > EMPTY_STRING_LENGTH )
                    else
                    {
                        throw new Error ( strMethodName + ': Argument pstrHourglassURITag must be a String that has a length greater than zero.' );
                    }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrHourglassURITag ) && pstrHourglassURITag.length > EMPTY_STRING_LENGTH )
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrBoxId ) && pstrBoxId.length > EMPTY_STRING_LENGTH )
                else
                {
                    throw new Error ( strMethodName + ': Argument pstrBoxId must be a String that has a length greater than zero.' );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrBoxId ) && pstrBoxId.length > EMPTY_STRING_LENGTH )
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH )
            else
            {
                throw new Error ( strMethodName + ': Argument pstrContainerId must be a String that has a length greater than zero.' );
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrContainerId ) && pstrContainerId.length > EMPTY_STRING_LENGTH )
        }   // LLCommon.CreateHourglassIconContainer method


        LLCommon.CreateNbspSpacer = function ( pintSpaceCount )
        {
            /*
                ----------------------------------------------------------------
                Name:       CreateNbspSpacer

                Goal:       Return a SPAN element populated with at least one
                            nonbreaking space.

                Arguments:  pintSpaceCount = This OPTIONAL integer specifies the
                                             number of NBSP chracters to inject.

                Returns:    The return value is a span that contains the number
                            of nonbreaking spaces specified by pintSpaceCount.
                            If pintSpaceCount is omitted, one nonbreaking space
                            is injected into the span.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            const rdocTheSpan           = document.createElement ( 'span' );
            const intNSpaces            = LLCommon.IsValidInteger ( pintSpaceCount ) && pintSpaceCount > NUMERIC_ZERO ? pintSpaceCount : NUMERIC_PLUS_ONE;
            var   strInnerHtml          = EMPTY_STRING;

            for ( var intJ = ARRAY_FIRST_ELEMENT;
                      intJ < intNSpaces;
                      intJ++ )
            {
                strInnerHtml            += HTML_NBSP;
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < intNSpaces; intJ++ )

            rdocTheSpan.innerHTML       = strInnerHtml;

            return rdocTheSpan;
        }   // LLCommon.CreateNbspSpacer method


        /**
         * Creates a semi-transparent overlay DIV positioned over the target element.
         *
         * @function LLCommon.createOverlayDivForElement
         * @param {HTMLElement|string} poElementOrId - The target element or its ID string.
         * @param {string} [psContext]               - Optional context label for error reporting.
         * @param {Object} [poOverlayStyleOverrides] - Optional style overrides for the overlay DIV.
         * @returns {HTMLDivElement} The overlay DIV element.
         * @throws {Error} If the target element is not found or invalid.
         */
        LLCommon.createOverlayDivForElement = function ( poElementOrId, psContext, poOverlayStyleOverrides )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            const el            = LLCommon.getElementOrThrow ( poElementOrId, psContext || strMethodName + ': Overlay target' );
            const box           = LLCommon.getTotalRenderedBox ( el );
            const overlay       = document.createElement ( 'div' );

            // Specity defaults for overlay positioning and appearance.
            const defaults = {
                position        : 'absolute',
                top             : `${box.top}px`,
                left            : `${box.left}px`,
                width           : `${box.width}px`,
                height          : `${window.innerHeight}px`,
                backgroundColor : 'rgba(0, 0, 0, 0.4)',
                zIndex          : '9998',
                pointerEvents   : 'auto'
            };

            // Delegate styling to the helper.
            LLCommon.applyStyleDefaultsWithOverrides ( overlay,
                                                       defaults,
                                                       poOverlayStyleOverrides );

            document.body.appendChild ( overlay );
            return overlay;
        };  // LLCommon.createOverlayDivForElement method


        /**
         * Creates a constructor function (a "record type") whose instances contain a
         * fixed set of public, read‑only properties. Instances are frozen to prevent
         * mutation, ensuring predictable, audit‑friendly behavior.
         *
         * Each instance is constructed using a single "named arguments" object whose
         * keys must match the declared property names exactly.
         *
         * @function CreateRecordType
         * @memberof LLCommon
         * @param {...string} propNames
         *     The list of property names that instances of the generated record type
         *     must contain. These become public, read‑only fields on each instance.
         *
         * @returns {Function}
         *     A constructor function that accepts a single object argument containing
         *     the required properties. Missing properties cause an exception.
         *
         * @example
         * // Define a record type with four read‑only properties.
         * const FourProps = LLCommon.CreateRecordType ( 'a', 'b', 'c', 'd' );
         *
         * // Construct an instance using named arguments.
         * const obj = new FourProps ( {
         *     a: 1,
         *     b: 2,
         *     c: 3,
         *     d: 4
         * } );
         *
         * console.log ( obj.a ); // 1
         * obj.a = 99;            // Ignored (object is frozen)
         */
        LLCommon.CreateRecordType = function ( ... propNames )
        {
            return class
            {
                /**
                 * Constructs a new frozen record instance.
                 *
                 * @param {Object} args
                 *     An object containing values for each declared property name.
                 *     All required properties must be present.
                 *
                 * @throws {Error}
                 *     Thrown if any required property is missing.
                 */
                constructor ( args )
                {
                    for ( const name of propNames )
                    {
                        if ( ! ( name in args ) )
                        {
                            throw new Error ( `Missing required property: ${name}` );
                        }   // if ( ! ( name in args ) )

                        this [ name ] = args [ name ];
                    }   // for ( const name of propNames )

                    // Prevent mutation of the record instance.
                    Object.freeze ( this );
                }   // constructor ( args )
            };  // return class
        };  // LLCommon.CreateRecordType


        LLCommon.DisableOrEnableButtonsInsideElement = function ( poTarget , pfDisabled )
        {
            /*
                ----------------------------------------------------------------
                Name:       DisableOrEnableButtonsInsideElement

                Goal:       Disable or enable the BUTTON elements in the element
                            to which poTarget refers, depending upon the value
                            of pfDisabled, which is evaluated in a Boolean
                            context.

                Arguments:  poTarget       = Specify either a reference to the
                                             container element or a string that
                                             represents its ID.

                            pfDisabled     = This string argument specifies the
                                             HTTP verb to use for the AJAX call,
                                             which defaults to GET.

                Returns:    When pfDisabled is Boolean TRUE, the return value is
                            the number of buttons nested inside the element with
                            an ID of pstrElementId.

                            When pfDisabled is Boolean FALSE, the return value
                            is the number of buttons nested inside the element
                            with ID pstrElementId, times minus one.

                Remarks:    When pfDisabled is an expression, the Boolean value
                            (TRUE or FALSE) of the expression can be inferred
                            from the sign of the return value.

                See Also:   LLCommon.inputDisable
                            LLCommon.inputEnable
                            LLCommon.inputIsDisabled
                ----------------------------------------------------------------
            */

            const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

            const docTargetElement = LLCommon.IsString ( poTarget ) ? document.getElementById ( poTarget ) : poTarget;

            if ( docTargetElement !== null )
            {
                const adocButtons  = docTargetElement.querySelectorAll ( 'button' );

                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < adocButtons.length;
                          intJ++ )
                {
                    if ( adocButtons [ intJ ].className.indexOf ( 'STT_Leave_Enabled' ) === INDEXOF_NOT_FOUND )
                    {
                        adocButtons [ intJ ].disabled = pfDisabled;
                    }   // if ( adocButtons [ intJ ].className.indexOf ( 'STT_Leave_Enabled') === INDEXOF_NOT_FOUND )
                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < adocButtons.length; intJ++ )

                return pfDisabled ? adocButtons.length : ( adocButtons.length * ( -1 ) );
            }   // if ( docTargetElement !== null )

            return NUMERIC_ZERO;
        }   // LLCommon.DisableOrEnableButtonsInsideElement method


        LLCommon.DoAjax = function ( pstrMethodName , pstrVerb , poArgs , pfAsync )
        {
            /*
                ----------------------------------------------------------------
                Name:       DoAjax

                Goal:       Use the JQuery wrapper to perform an AJAX function
                            call.

                Arguments:  pstrMethodName = This string argument specifies the
                                             name of the OpenController method
                                             to call.

                            pstrVerb       = This string argument specifies the
                                             HTTP verb to use for the AJAX call,
                                             which defaults to GET.

                            poArgs         = This is a JavaScript object that
                                             has a property for each argument,
                                             along with its value.

                            pfAsync        = Optional Boolean to override async
                                             (default when omitted = false) to
                                             coerce synchronous behavior

                Returns:    The return value is the raw data returned by the
                            method.
                ----------------------------------------------------------------
            */

            const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

            if ( Object.is ( pfAsync, undefined ) || pstrVerb === 'GET' || pstrVerb === 'POST' )
            {
                if ( ! ( LLCommon.IsString ( pstrMethodName ) ) )
                {
                    return 'ERROR in ' + strMethodName + ': The specified method name must be a string.';
                }   // TRUE (unanticipated outcome) block, if ( ! ( LLCommon.IsString ( pstrMethodName ) ) )
            }   // TRUE (anticipated outcome) block, if ( Object.is ( pfAsync, undefined ) || pstrVerb === 'GET' || pstrVerb === 'POST' )
            else
            {
                return 'ERROR in ' + strMethodName + ': The specified HTTP verb is ' + pstrVerb + '. The only supported values are GET and POST, both case sensitive.'
            }   // FALSE (unanticipated outcome) block, if ( Object.is ( pfAsync, undefined ) || pstrVerb === 'GET' || pstrVerb === 'POST' )

            const strUrl          = encodeURI ( pstrMethodName.substring ( SUBSTRING_FIRST_CHAR ,
                                                                           LLCommon.HTTPS_PROTOCOL.length ).toLowerCase ( ) === LLCommon.HTTPS_PROTOCOL
                                                                                ? pstrMethodName
                                                                                : LLCommon.AjaxUrlPrefix +  ( pstrMethodName.indexOf ( PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND ? pstrMethodName : 'Open/' + pstrMethodName ) );
            const strVerb         = Object.is ( pstrVerb , undefined ) ? 'GET' : pstrVerb;

            var   fAjaxError      = false;
            var   fKeepTrying     = true;
            var   intTotalRetries = NUMERIC_ZERO;
            var   objAjaxResult   = EMPTY_STRING;

            try
            {
                do  // while ( fKeepTrying )
                {
                    if ( poArgs instanceof Object )
                    {
                        $.ajax (
                        {
                                type    : strVerb,
                                async   : Object.is ( pfAsync, undefined ) ? false : pfAsync,
                                cache   : false,
                                url     : strUrl,
                                data    : poArgs,
                                success : function ( data )
                                          {
                                              if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                              {   // The above Ajax call returned a value. Capture it.
                                                  objAjaxResult = data;
                                              }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                          },
                                error   : function ( jqXHR , textStatus , errorThrown )
                                          {
                                               objAjaxResult =   textStatus
                                                               + TIME_SEPARATOR_CHAR + SPACE_CHARACTER + jqXHR.responseText
                                                               + SPACE_CHARACTER + errorThrown;
                                               fAjaxError    = true;

                                               debugger;

                                               if ( intTotalRetries > ( LLCommon.AJAX_RETRY_LIMIT - ARRAY_NEXT_ELEMENT ) )
                                               {   // Let the first ocurrencee of an error pass.
                                                   console.log ( strMethodName + ' ERROR : pstrMethodName = ' + pstrMethodName + ', pstrVerb = ' + pstrVerb + ', strUrl = ' + strUrl + ', poArgs = ' + JSON.stringify ( poArgs ) + ', jqXHR.status = ' + jqXHR.status + ', jqXHR.responseText = ' + jqXHR.responseText + ', textStatus = ' + textStatus + ', objAjaxResult = ' + objAjaxResult );
                                               }   // if ( intTotalRetries > ( LLCommon.AJAX_RETRY_LIMIT - ARRAY_NEXT_ELEMENT ) )

                                          }
                        });
                    }   // True (Most requests to OpenController and other endpoints expect data.) block, if ( poArgs instanceof Object )
                    else
                    {
                        $.ajax (
                        {
                                type    : strVerb,
                                async   : Object.is ( pfAsync, undefined ) ? false : pfAsync,
                                cache   : false,
                                url     : strUrl,
                                success : function ( data )
                                          {
                                                if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                                {   // The above Ajax call returned a value. Capture it.
                                                    objAjaxResult = data;
                                                }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                                          },
                                error   : function ( jqXHR , textStatus , errorThrown )
                                          {
                                                objAjaxResult = textStatus
                                                                + TIME_SEPARATOR_CHAR + SPACE_CHARACTER + jqXHR.responseText
                                                                + SPACE_CHARACTER + errorThrown;
                                                fAjaxError    = true;
                                          }
                        });
                    }   // FALSE (Some requests are self-sufficient.) block, if ( poArgs instanceof Object )

                    if ( fAjaxError )
                    {   // The API reported an error. Check the retry count.
                        if ( intTotalRetries < LLCommon.AJAX_RETRY_LIMIT )
                        {   // Unused tries remain. Count this one as a failure, and reset the state flag.
                            intTotalRetries++;
                            fAjaxError = false;
                        }   // TRUE (anticipated outcome, more tries allowed) block, if ( intTotalRetries < LLCommon.AJAX_RETRY_LIMIT )
                        else
                        {   // The retry limit has been reached. Allow control to leave the do while loop.
                            fKeepTrying = false;
                            throw new Error ( strMethodName + ' ERROR : pstrMethodName = ' + pstrMethodName + ', pstrVerb = ' + pstrVerb + ', strUrl = ' + strUrl + ', poArgs = ' + JSON.stringify ( poArgs ) + objAjaxResult );
                        }   // FALSE (unanticipated outcome, retry limit exceeded) blck, if ( intTotalRetries < LLCommon.AJAX_RETRY_LIMIT )
                    }   // TRUE (The AJAX API reported an exception.) block, if ( fAjaxError )
                    else
                    {   // The attempt succeeded. Post a log entry and leave the loop.
                        fKeepTrying = false;

                        if ( intTotalRetries > NUMERIC_ZERO )
                        {
                            $.ajax ( { url: LLCommon.AjaxUrlPrefix + 'Open/SendToTrace', type: 'GET', cache: false, data: { 'Message': LLCommon.STANDARD_SEND_TO_TRACE_PREFIX + window.location.href + ' DoAjax call to OpenController method ' + pstrMethodName + ' succeeded after ' + intTotalRetries + ' tries.' } } );
                        }   // if ( intTotalRetries > NUMERIC_ZERO )
                    }   // FALSE (The AJAX API call succeeded.) block, if ( fAjaxError )
                } while ( fKeepTrying )

                LLCommon.LogImportantValue ( pstrMethodName + ' return value' , objAjaxResult );
                return objAjaxResult;
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return 'ERROR in ' + strMethodName + ': ' + ex.message;
            }
        };   // LLCommon.DoAjax method


        LLCommon.DownloadFile2Client = function ( pAudioPlaybackUri )
        {
            /*
                ----------------------------------------------------------------
                Name:       DownloadFile2Client

                Goal:       Arrange to download a file to a client machine by
                            opening a new window with the specified file URL.

                Arguments:  pAudioPlaybackUri = String representation of the
                                                absolute URI that points to the
                                                document to be downloaded to the
                                                client

                Returns:    Since the objective is to download a document to the
                            client's device, this function has nothing to return
                            to its caller. Hence, its return value is undefined.

                Remarks:    The only real work performed by this function is the
                            construction and encoding of the URI. Logging to the
                            local console is for documentation. It hides the
                            complexity of constructing and encoding a URI that
                            is sent to the server to invoke the MVC method that
                            returns the document to the client's machine.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            const strUrl        = encodeURI (   LLCommon.AjaxUrlPrefix
                                              + 'Open/DownloadFile?DocumentName='
                                              + pAudioPlaybackUri );
            console.info (   'INFO: ' + window.location.href
                           + ', method = ' + strMethodName
                           + ' pAudioPlaybackUri = ' + pAudioPlaybackUri
                           + ', strUrl = ' + strUrl
                         );
            window.open ( strUrl );
        };   // LLCommon.DownloadFile2Client method


        /**
         * enforceSelectChoice
         * -------------------
         * Purpose:
         *   Enforces a choice from a <select> element by overlaying a label and target element.
         *
         * Parameters:
         *   Required:
         *     - poSelectElOrId          {string|object} Reference to the <select> element or its ID.
         *     - poOverlayTargetElOrId   {string|object} Reference to the overlay target element or its ID.
         *     - psLabelText             {string}        Text to display in the overlay label.
         *
         *   Optional:
         *     - poLabelStyle            {object}        CSS style overrides for the label element.
         *     - poOverlayStyleOverrides {object}        CSS style overrides for the overlay element.
         *     - nLabelLeftOffsetPx      {number}        Horizontal offset in pixels (default: 8).
         *
         * Validation:
         *   - Required parameters must be provided and of the correct type.
         *   - Optional parameters are validated if present; invalid types throw by default.
         *
         * Returns:
         *   void (applies DOM changes directly)
         *
         * Notes:
         *   - Uses LLCommon.validateParams for defensive type checking.
         *   - Invalid argument types are treated as coding errors (fail-fast).
         *   - Pattern: Validate → Normalize → Execute → Return.
         *
         * JSDoc Tags:
         * @param {string|HTMLElement} poSelectElOrId        - Reference to the <select> element or its ID.
         * @param {string|HTMLElement} poOverlayTargetElOrId - Reference to the overlay target element or its ID.
         * @param {string} psLabelText                       - Text to display in the overlay label.
         * @param {object} [poLabelStyle]                    - CSS style overrides for the label element.
         * @param {object} [poOverlayStyleOverrides]         - CSS style overrides for the overlay element.
         * @param {number} [nLabelLeftOffsetPx=8]            - Horizontal offset in pixels.
         * @returns {void}
         * @throws {TypeError} If required parameters are missing or invalid.
         */
        LLCommon.enforceSelectChoice = function ( poSelectElOrId,
                                                  poOverlayTargetElOrId,
                                                  psLabelText,
                                                  poLabelStyle,
                                                  poOverlayStyleOverrides,
                                                  nLabelLeftOffsetPx = 8
                                                )
        {
            const strMethodName           = LLCommon.GetNameOfCurrentFunction ( );

            // Validate optional parameters (throws immediately if invalid).
            LLCommon.validateParams (
                {
                    poLabelStyle            : { value: poLabelStyle,             type: "object" },
                    poOverlayStyleOverrides : { value: poOverlayStyleOverrides,  type: "object" },
                    nLabelLeftOffsetPx      : { value: nLabelLeftOffsetPx,       type: "number" }
                },
                strMethodName
            );

            // Validate required parameters (throws immediately if invalid).
            LLCommon.validateParams (
                {
                    poSelectElOrId          : { value: poSelectElOrId,           type: ["string", "object"], required: true },
                    poOverlayTargetElOrId   : { value: poOverlayTargetElOrId,    type: ["string", "object"], required: true },
                    psLabelText             : { value: psLabelText,              type: "string",             required: true }
                },
                strMethodName
            );

            const selectEl                = LLCommon.getElementOrThrow ( poSelectElOrId,
                                                                         strMethodName + ': Combo box'
                                                                       );
            const overlay                 = LLCommon.createOverlayDivForElement ( poOverlayTargetElOrId,
                                                                                  strMethodName + ': Overlay target',
                                                                                  poOverlayStyleOverrides
                                                                                );
            selectEl.style.position       = 'relative';
            selectEl.style.zIndex         = '9999';

            if ( psLabelText )
            {
                const selectBox           = selectEl.getBoundingClientRect ( );
                const overlayBox          = overlay.getBoundingClientRect ( );

                const label = document.createElement ( 'div' );

                const maxWidth            = Math.max ( 0, ( selectBox.left - overlayBox.left ) - ( nLabelLeftOffsetPx * 2 ) );

                label.style.maxWidth      = `${maxWidth}px`;
                label.textContent         = psLabelText;
                label.style.position      = 'absolute';
                label.style.whiteSpace    = 'normal';
                label.style.wordBreak     = 'break-word';
                label.style.textAlign     = 'left';
                label.style.left          = `${nLabelLeftOffsetPx}px`;
                label.style.top           = `${selectBox.top - overlayBox.top}px`;
                label.style.zIndex        = '9999';
                label.style.pointerEvents = 'none';

                // Defaults for label styling
                const defaults = {
                    color      : 'red',
                    fontWeight : 'bold'
                };

                // Apply defaults + optional overrides
                LLCommon.applyStyleDefaultsWithOverrides ( label,
                                                           defaults,
                                                           poLabelStyle );

                overlay.appendChild ( label );
            }   // if ( psLabelText )

            return overlay;
        };  // LLCommon.enforceSelectChoice


        LLCommon.EvaluateEntityType = function ( )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        EvaluateEntityType

                Method Goal:        Evaluate the CRMEntityType property.

                Input:              All parameters live in global variables.

                Output:             This function returns void (nothing) and has
                                    only side effects, consisting of setting the
                                    value of property LLCommon.EntityType.
                ----------------------------------------------------------------
            */

            const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );


            function GetSearchParameters ( pstrEntityDescr )
            {
                /*
                    ------------------------------------------------------------
                    Method Name:        GetSearchParameters

                    Method Goal:        Parse search parameters, if present,
                                        from the Description column of the CRM
                                        Entity table row from which EntityType
                                        is initialized.

                    Input:              pstrEntityDescr = The String representa-
                                                          tion of Description is
                                                          expected to have a\
                                                          usable value.

                    Output:             If a page has an associated CRM Entity,
                                        and its Description property is a String
                                        of one or more characters, the string is
                                        searched for a key (name) value equal to
                                        'EntitySearchParameters', its Value is
                                        expected to be parseable into an object
                                        that contains three propeerties, each of
                                        which is an array of JavaScript objects.
                                        These objects define the parameters of a
                                        search of records associated with the
                                        CRM entity type to which it is attached.

                                        Otherwise, the return value is NULL, and
                                        the entity is marked as unsearchable.

                    Remarks:            Boolen LLCommon.EntityTypeIsSearchable
                                        property of the root LLCommon oject is
                                        set to True or False to provide a quick
                                        test that the form controller can use to
                                        determine whether to display or hide the
                                        CRM Search button.
                    ------------------------------------------------------------
                */

                const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );;

                try
                {
                    const astrKeysAndValues = pstrEntityDescr.split ( CSV_SEPARATOR_CHAR );

                    for ( var intKvpIndex   = ARRAY_FIRST_ELEMENT;
                              intKvpIndex   < astrKeysAndValues.length;
                              intKvpIndex++ )
                    {
                        var astrKeyAndValue = astrKeysAndValues [ intKvpIndex ].split ( EQUALS_CHAR );

                        if ( astrKeyAndValue [ SPLIT_NAME_PART ] === 'EntitySearchParameters' )
                        {
                            const strSearchParams1 = astrKeyAndValue [ SPLIT_VALUE_PART ].replace ( /¬/g , CSV_SEPARATOR_CHAR )
                            const strSearchParams2 = strSearchParams1.replace ( /\} *, *\]/g , '} ]');

                            return JSON.parse ( strSearchParams2 );
                        }   // if ( astrKeyAndValue [ SPLIT_NAME_PART ] === 'EntitySearchParameters' )
                    }   // for ( var intKvpIndex = ARRAY_FIRST_ELEMENT; intKvpIndex < astrKeysAndValues.astrKeysAndValues; intKvpIndex++ )

                    return null;
                }
                catch ( ex )
                {
                    LLCommon.LogException ( ex );
                    return null;
                }
            }   // function GetSearchParameters


            debugger;

            if ( _pagenameSource !== SRC_IS_UNKNOWN )
            {
                LLCommon.EntityType                         = LLCommon.DoAjax ( 'GetCRMEntityTypeMetaDataByPageName',
                                                                                'GET',
                                                                                {
                                                                                   'PageName' : _pagename,
                                                                                   'DomainId' : _domainid
                                                                                } );

                if ( LLCommon.IsString ( LLCommon.EntityType ) )
                {
                    LLCommon.LogException ( LLCommon.EntityType );
                    LLCommon.EntityType                     = null;
                }   // TRUE (unanticipateed outcome) block, if ( LLCommon.IsString ( LLCommon.EntityType ) )
                else
                {
                    LLCommon.EntityType.Is4Form             = true;
                }   // FALSE (anticipateed outcome) block, if ( LLCommon.IsString ( LLCommon.EntityType ) )
            }   // TRUE (The page name is associated with a CRM.) block, if ( _pagenameSource !== SRC_IS_UNKNOWN )
            else
            {
                if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.CRMEntityTypeID !== undefined )
                {
                    LLCommon.EntityType                     = LLCommon.GetEntityTypeInfo ( _EntityType ,                                                                           _EntityTypeSource ,
                                                                                           LLCommon.EnabledCRM.CRMEntityTypeID );

                    if ( LLCommon.EntityType !== null )
                    {
                        if ( LLCommon.IsString ( LLCommon.EntityType ) )
                        {
                            LLCommon.LogException ( LLCommon.EntityType );
                            LLCommon.EntityType             = null;
                        }   // TRUE (unanticipateed outcome) block, if ( LLCommon.IsString ( LLCommon.EntityType ) )
                        else
                        {
                            LLCommon.EntityType.Is4Form     = false;
                        }   // FALSE (anticipateed outcome) block, if ( LLCommon.IsString ( LLCommon.EntityType ) )
                    }   // if ( LLCommon.EntityType !== null )
                }   // TRUE (anticipated outcome) block, if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.CRMEntityTypeID !== undefined )
            }   // FALSE (The page name is NOT associated with a CRM.) block, if ( _pagenameSource !== SRC_IS_UNKNOWN )

            //  ----------------------------------------------------------------
            //  Regardless of whether it is associated with a form, an entity
            //  may as well have a set of search attributes.
            //  ----------------------------------------------------------------

            if ( LLCommon.EntityType !== null && ( !Object.is ( LLCommon.EntityType.EntityDescription , undefined ) ) && LLCommon.EntityType.EntityDescription !== null && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )
            {
                LLCommon.EntityType.SearchParameters        = GetSearchParameters ( LLCommon.EntityType.EntityDescription );
            }   // TRUE (The EntityType exists and its Description proprty has a usable value.) block, if ( LLCommon.EntityType !== null && ( !Object.is ( LLCommon.EntityType.EntityDescription , undefined ) ) && LLCommon.EntityType.EntityDescription !== null && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )
            else
            {
                if ( LLCommon.EntityType !== null )
                {
                    LLCommon.EntityType.SearchParameters    = null;
                }   // if ( LLCommon.EntityType !== null )
            }   // FALSE (Either the entire EntityType is null (undefined) or its Description property is meaningless.) block, if ( LLCommon.EntityType !== null && ( !Object.is ( LLCommon.EntityType.EntityDescription , undefined ) ) && LLCommon.EntityType.EntityDescription !== null && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )

            LLCommon.EntityTypeIsSearchable                 = ( LLCommon.EntityType !== null && LLCommon.EntityType.SearchParameters !== null && LLCommon.EntityType.SearchParameters.ao_Query_Criteria.length > ARRAY_IS_EMPTY );
        }   // LLCommon.EvaluateEntityType method


        LLCommon.EvaluateRelatedTargetOfBlurEvent = function ( pdocBlurNextTarget )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        EvaluateBlurRelatedTarget

                Method Goal:        Compare the ID of element pdocBlurNextTarget
                                    against the list of elementt IDs in array
                                    __ClickEvents2Fire. If the ID of element
                                    pdocBlurNextTarget is in the list, dispatch
                                    a Click evvent to it and return True.

                                    Otherwise, return false.

                Input:              pdocBlurNextTarget = Reference to the
                                                         relatedTarget attribute
                                                         of the blur EventTarget

                Output:             In the unlikely event that argument
                                    pdocBlurNextTarget isn't an Element, the
                                    return value is also false, and no alarm is
                                    raised.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            if ( pdocBlurNextTarget instanceof Element )
            {
                const strElementId      = pdocBlurNextTarget.id;

                if ( __ClickEvents2Fire.find ( ( element ) => element === strElementId ) )
                {
                    const evtClickEvent = new Event ( 'click' );
                    pdocBlurNextTarget.dispatchEvent ( evtClickEvent );

                    return true;
                }   // TRUE (anticipated outcome) block, if ( __ClickEvents2Fire.find ( ( element ) => element === strElementId ) )
                else
                {
                    return false;
                }   // FALSE (unanticipated outcome) block, if ( __ClickEvents2Fire.find ( ( element ) => element === strElementId ) )
            }   // TRUE (anticipated outcome) block, if ( pdocBlurNextTarget instanceof Element )
            else
            {
                return false;
            }   // FALSE (unanticipated outcome) block, if ( pdocBlurNextTarget instanceof Element )
        }   // LLCommon.EvaluateRelatedTargetOfBlurEvent method



        /**
         * Assemble one or more properties from a CSS selector into a style
         * object that can be merged with another style object and/or applied
         * directly to an element so that they are treated as inline styles by
         * the CSS specificity algorithm.
         * @param {string}           pstrClassName   - Name of class to apply to
         *                                             the scratch object
         * @param {array of strings} pastrProperties - Array of strings, each of
         *                                             which identifies a class
         *                                             attribute to include in
         *                                             the returned Style object
         *
         *                                             If omitted, the returned
         *                                             style object has the
         *                                             color and backgroundColor
         *                                             attributes of the class.
         * @returns {Style object} - JavaScript object in which each property is
         *                           the name of a CSS style attribute, and its
         *                           value is the value of that property in the
         *                           CSS selector identified by pstrClassName
         */
        LLCommon.getClassStyles = function  ( pstrClassName, pastrProperties = [ 'backgroundColor', 'color' ] )
        {
            // Create scratch element.
            const temp          = document.createElement ( 'div' );

            temp.className      = pstrClassName;    // Apply desired className to scratch DIV.
            temp.style.display  = 'none';           // Make the scratch DIV invisible.

            document.body.appendChild ( temp );

            // Read computed styles.
            const styles        = getComputedStyle ( temp );
            const result        = { };

            for ( const prop of pastrProperties )
            {
              result [ prop ]   = styles [ prop ];
            }   // for ( const prop of pastrProperties )

            // Dispose scratch element.
            document.body.removeChild ( temp );

            return result;
        }   // LLCommon.getClassStyles method


        /**
         * Evaluate the SetCommonObjects string and extract from it the Role(s)
         * assigned to the current SalesTalk user, then recite the roles in a
         * series of console log messages. At the second of two call sites, the
         * instance UserInfo property was just synced with the value per the
         * current Wise Agent Team membership setting.
         * @param {string} pstrCallSiteInfo
         * @returns {void}
         */
        LLCommon.GetCommonObjects = function ( pstrCallSiteInfo )
        {
            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            const strLogMessagePrefix   = strMethodName + ' FOR ' + pstrCallSiteInfo + ': ';
            const strCommonObjects      = LLCommon.DoAjax ( 'SetCommonObjects' ,
                                                            'GET' ,
                                                            {
                                                                 'domainId' : _domainid ,
                                                                 'tenantId' : _tenantid ,
                                                                 'userId'   : _userid
                                                            } );
            debugger;

            const intPosRolesList       = strCommonObjects.indexOf ( ', Roles = ' );

            if ( intPosRolesList > INDEXOF_NOT_FOUND )
            {
                LLCommon.Trace (   strMethodName + ': For domainId = ' + _domainid
                              + ', tenantId = ' + _tenantid
                              + ', and userId = ' + _userid
                              + ', SetCommonObjects = ' + strCommonObjects );
                LLCommon.Roles4User = strCommonObjects.substring ( intPosRolesList + 10 ).split ( CSV_SEPARATOR_CHAR )
            }   // TRUE (anticipated outcome) block, if ( intPosRolesList > INDEXOF_NOT_FOUND )
            else
            {
                throw new Error ( strMethodName + ': ' + intPosRolesList );
            }   // FALSE (unanticipated outcome) block, if ( intPosRolesList > INDEXOF_NOT_FOUND )

            for ( var intJ = ARRAY_FIRST_ELEMENT;
                      intJ < LLCommon.Roles4User.length;
                      intJ++ )
            {
                console.log ( strLogMessagePrefix + 'LLCommon.Roles4User # ' + LLCommon.OrdinalFromIndex ( intJ ) + '        = ' + LLCommon.Roles4User [ intJ ] );
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < LLCommon.Roles4User.length; intJ++ )
        }   // LLCommon.GetCommonObjects method


        LLCommon.GetDomainId = function ( )
        {
            return LLCommon.DomainId;
        }   // LLCommon.GetDomainId


        LLCommon.GetDomainName = function ( )
        {
            return LLCommon.DomainName;
        }   // LLCommon.GetDomainName


        /**
         * Resolve an element by ID or returns the element itself.
         * Throws a descriptive error if the ID is provided and no matching
         * element is found.
         *
         * @function LLCommon.getElementOrThrow
         * @param {HTMLElement|string} poElementOrId - The element itself or its
         *                                             ID string.
         * @param {string} [psContext]               - Optional context label to
         *                                             clarify the error message.
         * @returns {HTMLElement} The resolved DOM element.
         * @throws {Error} If the ID is provided and no matching element is found.
         *
         * @example
         * const el = LLCommon.getElementOrThrow ( "myDiv" );
         * const el = LLCommon.getElementOrThrow ( someElement );
         * const el = LLCommon.getElementOrThrow ( "myDiv", "Overlay target" );
         */
        LLCommon.getElementOrThrow = function ( poElementOrId, psContext )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            if ( LLCommon.IsString ( poElementOrId ) )
            {
                const el = document.getElementById ( poElementOrId );

                if ( el )
                {
                    return el;
                }   // TRUE (anticipated outcome) block, if ( el )
                else
                {
                    const label = psContext ? `${psContext} ` : EMPTY_STRING;
                    throw new Error(`${label}element not found with ID "${poElementOrId}"`);
                }   // FALSE (unanticipated outcome) block, if ( el )
            }

            if ( !poElementOrId || typeof poElementOrId !== 'object' || !( 'nodeType' in poElementOrId ) )
            {
                throw new Error("Invalid element reference provided");
            }

            return poElementOrId;
        };  // LLCommon.getElementOrThrow method


        LLCommon.GetEnabledCrmInfo = function ( pintTenantId , pintDomainId )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        GetEnabledCrmInfo

                Method Goal:        Parse the delimited string returned by Ajax
                                    method GetEnabledCrmInfo into a JavaScript
                                    object that exposes the returned names and
                                    their values as three string properties.

                Input:              pintTenantId = Integer Tenant ID, subjected
                                                   to a basic sanity check

                                    pintDomainId = Integer Domain ID, subjected
                                                   to a basic sanity check

                Output:             In the unlikely event that GetEnabledCrmInfo
                                    returns an exception, the return value is a
                                    NULL.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            var strCrmInfo      = null;

            if ( _CRMSource == SRC_IS_UNKNOWN )
            {
                if ( LLCommon.IsValidInteger ( pintTenantId ) && LLCommon.IsValidInteger ( pintDomainId ) )
                {
                    strCrmInfo  = LLCommon.DoAjax ( 'GetEnabledCrmInfo' ,
                                                    'GET' ,
                                                    {
                                                      'tenantId'     : pintTenantId ,
                                                      'domainId'     : pintDomainId
                                                    } );
                }   // TRUE (anticipated outcomee) block, if ( LLCommon.IsValidInteger ( pintTenantId ) && LLCommon.IsValidInteger ( pintDomainId ) )
                else
                {
                    LLCommon.LogException ( strMethodName + ': Argument pintTenantId = ' + pintTenantId + ' and pintDomainId = ' + pintDomainId + '. Both must be integers. At least one of the two is NOT an integer.' );
                }   // FALSE (unanticipated outcomee) block, if ( LLCommon.IsValidInteger ( pintTenantId ) && LLCommon.IsValidInteger ( pintDomainId ) )
            }   // TRUE (The query string is devoid of a reference to a CRM.) block, if ( _CRMSource == SRC_IS_UNKNOWN )
            else
            {
                strCrmInfo      = LLCommon.DoAjax ( 'GetSpecifiedCrmInfo' ,
                                                    'GET' ,
                                                    {
                                                      'CRMNameOrPrefix' : _CRM
                                                    } );
            }   // FALSE (The query string specifies the CRM to associate with this page.) block, if ( _CRMSource == SRC_IS_UNKNOWN )

            if ( strCrmInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
            {
                const astrNameValuePairs = strCrmInfo.split ( LOGICAL_NEGATE );

                if ( astrNameValuePairs.length === 7 )
                {   // Parse this: CrmName=HubSpotÂ¬Monikor=HubSpotEnabledÂ¬SysCRMLeadOrContact=HS-
                    const astrExternalSystemTypeIdNVP       = astrNameValuePairs [ ARRAY_FIRST_ELEMENT   ].split ( EQUALS_CHAR );
                    const astrCrmNameNVP                    = astrNameValuePairs [ ARRAY_SECOND_ELEMENT  ].split ( EQUALS_CHAR );
                    const astrMonikorNVP                    = astrNameValuePairs [ ARRAY_THIRD_ELEMENT   ].split ( EQUALS_CHAR );
                    const astrSysCRMLeadOrContactNVP        = astrNameValuePairs [ ARRAY_FOURTH_ELEMENT  ].split ( EQUALS_CHAR );
                    const astrMyViewTemplateName            = astrNameValuePairs [ ARRAY_FIFTH_ELEMENT   ].split ( EQUALS_CHAR );
                    const astrMyViewPageName                = astrNameValuePairs [ ARRAY_SIXTH_ELEMENT   ].split ( EQUALS_CHAR );
                    const astrCRMEntityTypeID               = astrNameValuePairs [ ARRAY_SEVENTH_ELEMENT ].split ( EQUALS_CHAR );

                    if ( astrCrmNameNVP.length === SPLIT_NAME_FROM_VALUE && astrMonikorNVP.length === SPLIT_NAME_FROM_VALUE && astrSysCRMLeadOrContactNVP.length === SPLIT_NAME_FROM_VALUE && astrCrmNameNVP [ SPLIT_NAME_PART ] === 'CrmName' && astrMonikorNVP [ SPLIT_NAME_PART ] === 'Monikor' && astrSysCRMLeadOrContactNVP [ SPLIT_NAME_PART ] === 'SysCRMLeadOrContact' )
                    {
                        const intExternalSystemTypeId       = isNaN ( parseInt ( astrExternalSystemTypeIdNVP [ SPLIT_VALUE_PART ] ) )
                                                              ? NUMERIC_ZERO
                                                              :       parseInt ( astrExternalSystemTypeIdNVP [ SPLIT_VALUE_PART ] );
                        const strCrmNameValue               =                    astrCrmNameNVP              [ SPLIT_VALUE_PART ];
                        const strMonikorValue               =                    astrMonikorNVP              [ SPLIT_VALUE_PART ];
                        const strSysCRMLeadOrContactValue   =                    astrSysCRMLeadOrContactNVP  [ SPLIT_VALUE_PART ];
                        const strMyViewTemplateNameValue    =                    astrMyViewTemplateName      [ SPLIT_VALUE_PART ];
                        const strMyViewPageNameValue        =                    astrMyViewPageName          [ SPLIT_VALUE_PART ];
                        const intCRMEntityTypeIDValue       = isNaN ( parseInt ( astrCRMEntityTypeID         [ SPLIT_VALUE_PART ] ) )
                                                              ? NUMERIC_ZERO
                                                              :       parseInt ( astrCRMEntityTypeID         [ SPLIT_VALUE_PART ] );
                        debugger;

                        return {
                                 ExternalSystemTypeId : intExternalSystemTypeId ,
                                 CrmName              : strCrmNameValue ,
                                 Monikor              : strMonikorValue ,
                                 SysCRMLeadOrContact  : strSysCRMLeadOrContactValue ,
                                 Prefix               : strSysCRMLeadOrContactValue.substring ( SUBSTRING_FIRST_CHAR , 2 ) ,
                                 MyViewTemplateName   : strMyViewTemplateNameValue ,
                                 MyViewPageNameValue  : strMyViewPageNameValue ,
                                 CRMEntityTypeID      : intCRMEntityTypeIDValue };
                    }   // TRUE (anticipated outcome) block, if ( astrCrmName.length === SPLIT_NAME_FROM_VALUE && astrMonikor.length === SPLIT_NAME_FROM_VALUE && astrSysCRMLeadOrContact.length === SPLIT_NAME_FROM_VALUE )
                    else
                    {
                        LLCommon.LogException ( strMethodName + ': Argument pintTenantId = ' + pintTenantId + ' and pintDomainId = ' + pintDomainId + ', which split into expeected 3 substrings, of which at least one failed to split neatly into a name and value. AJAX API GetEnabledCrmInfo return value = ' + strCrmInfo );
                    }   // FALSE (anticipated outcome) block, if ( astrCrmName.length === SPLIT_NAME_FROM_VALUE && astrMonikor.length === SPLIT_NAME_FROM_VALUE && astrSysCRMLeadOrContact.length === SPLIT_NAME_FROM_VALUE )
                }   // TRUE (anticipated outcome) block, if ( astrNameValuePairs.length === 7 )
                else
                {
                    LLCommon.LogException ( strMethodName + ': Argument pintTenantId = ' + pintTenantId + ' and pintDomainId = ' + pintDomainId + ', which split into ' + astrNameValuePairs.length + 'substrings, rather than the expeected 3 substrings. AJAX API GetEnabledCrmInfo return value = ' + strCrmInfo );
                }   // FALSE (unanticipated outcome) block, if ( astrNameValuePairs.length === 7 )
            }   // TRUE (anticipated outcome) block, if ( strCrmInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
            else
            {
                LLCommon.LogException ( strMethodName + ': Argument pintTenantId = ' + pintTenantId + ' and pintDomainId = ' + pintDomainId + '. AJAX API function GetEnabledCrmInfo returned the following exception: ' + strCrmInfo );
            }   // FALSE (unanticipated outcome) block, if ( strCrmInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
        }   // LLCommon.GetEnabledCrmInfo


        LLCommon.GetEntityTypeInfo = function ( pstrEntityTypeName , pintEntityTypeSource , pintCRMEntityTypeID )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        GetEntityTypeInfo

                Method Goal:        Get the CRM Entity Type metadata that maps
                                    to either the pintEntityType value, unless
                                    pintEntityTypeSource equals SRC_IS_UNKNOWN,
                                    or the value that corresponds to the default
                                    entity type per the CRM Info, if any.

                Input:              pstrEntityTypeName   = String Entity Type
                                                           name, undefined unless
                                                           pintEntityTypeSource
                                                           is not SRC_IS_UNKNOWN

                                    pintEntityTypeSource = Integer Entity Type
                                                           source, which may be
                                                           SRC_IS_UNKNOWN (0)

                                    pintCRMEntityTypeID  = Integer default
                                                           Entity Type ID from
                                                           the CRM record, or 0
                                                           if undefined

                Output:             In the unlikely event that GetEnabledCrmInfo
                                    returns an exception, the return value is a
                                    NULL.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( pintEntityTypeSource !== undefined && pintEntityTypeSource !== null && pintEntityTypeSource !== SRC_IS_UNKNOWN )
            {
                return LLCommon.DoAjax ( 'GetCRMEntityTypeMetaDataByName' ,
                                         'GET' ,
                                         {
                                             'CRMEntityName' : pstrEntityTypeName
                                         } );
            }   // TRUE (The query string specified the entity name.) block, if ( pintEntityTypeSource !== undefined && pintEntityTypeSource !== null && pintEntityTypeSource !== SRC_IS_UNKNOWN )
            else
            {
                if ( pintCRMEntityTypeID !== undefined && pintCRMEntityTypeID !== null && pintCRMEntityTypeID > NUMERIC_ZERO )
                {
                    return LLCommon.DoAjax ( 'GetCRMEntityTypeMetaDataByEntityId' ,
                                             'GET' ,
                                             {
                                                 'CRMEntityId' : pintCRMEntityTypeID ,
                                                 'CRMId'       : LLCommon.EnabledCRM.SysCRMLeadOrContact
                                             } );
                }   // TRUE (The CRM supplied a defalut entity type ID.) block, if ( pintCRMEntityTypeID !== undefined && pintCRMEntityTypeID !== null && pintCRMEntityTypeID > NUMERIC_ZERO )
                else
                {
                    console.log ( 'Request for pintEntityTypeSource = ' + pintEntityTypeSource
                                   + ', pstrEntityTypeName = ' + ( LLCommon.IsString ( pstrEntityTypeName ) ? pstrEntityTypeName : 'NULL' )
                                   + ', pintCRMEntityTypeID =' + ( pintCRMEntityTypeID !== undefined && pintCRMEntityTypeID !== null ? pintCRMEntityTypeID : 'NULL' )
                                   + ' returned a NULL' );
                    return null;
                }   // FALSE (Either the CRM omitted a default entity type ID or there is NO CRM.) block, if ( pintCRMEntityTypeID > NUMERIC_ZERO )
            }   // FALSE (The query string omitted the entity name.) block, if ( pintEntityTypeSource !== SRC_IS_UNKNOWN )
        }   // LLCommon.GetEntityTypeInfo method


        LLCommon.GetInputValuesFromContainer = function ( poContainer , pstrFields2Omit )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        GetInputValuesFromContainer

                Method Goal:        Given a reference to a HTML container or the
                                    string representation of its name, return an
                                    array that contains the values stored in the
                                    input controls (elements) inside the
                                    containing HTML element identified directly
                                    or indirectly by the value of poContainer,
                                    excluding elements whose IDs appear in the
                                    optional pstrFields2Omit string.

                Input:              poContainer     = This JavaScript variable
                                                      must be either a handle to
                                                      the HTML element from
                                                      which to extract the INPUT
                                                      elements' values, or the
                                                      string representation of a
                                                      valid container element ID
                                                      such as a DIV or a TABLE.

                                    pstrFields2Omit = When present, this String
                                                      is a list, delimited by
                                                      LOGICAL_NEGATE characters,
                                                      of names to omit from the
                                                      returned array.

                Output:             Unless the value of poContainer is invalid,
                                    the return value is an array, which MAY be
                                    empty.

                                    When the value of poContainer is invalid, a
                                    null entity is returned.

                                    Exceptions, if any, are caught and reported,
                                    and generally have no adverse effect on the
                                    outcome.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            var   radocFields           = [ ];
            var   fDiscardArray         = false;

            debugger;

            try
            {
                if ( Object.is ( poContainer , undefined ) )
                {
                    fDiscardArray       = true;
                    throw new Error ( strMethodName + ': Input parameter poContainer is UNDEFINED.' );
                }   // if ( Object.is ( poContainer , undefined ) )

                if ( poContainer === null )
                {
                    fDiscardArray       = true;
                    throw new Error ( strMethodName + ': Input parameter poContainer is NULL.' );
                }   // if ( poContainer === null )

                const docContainer      = LLCommon.IsString ( poContainer )
                                          ? document.getElementById ( poContainer )
                                          : ( poContainer instanceof HTMLElement || poContainer instanceof Node )
                                            ? poContainer
                                            : poContainer instanceof Object
                                              ? poContainer
                                              : null;

                if ( docContainer === null )
                {
                    fDiscardArray       = true;
                    throw new Error ( strMethodName + ': Input parameter poContainer appears to be neither the string representation of a valid container element, nor a reference to such an element.' );
                }   // if ( docContainer === null )

                var   astrFieldIDs2Omit = [ ];

                if ( ( !Object.is ( pstrFields2Omit , undefined ) ) && ( pstrFields2Omit !== null ) && ( LLCommon.IsString ( pstrFields2Omit ) ) && ( pstrFields2Omit.length > EMPTY_STRING_LENGTH ) )
                {
                    astrFieldIDs2Omit   = pstrFields2Omit.split ( LOGICAL_NEGATE );
                }   // if ( ( !Object.is ( pstrFields2Omit , undefined ) ) && ( pstrFields2Omit !== null ) && ( LLCommon.IsString ( pstrFields2Omit ) ) && ( pstrFields2Omit.length > EMPTY_STRING_LENGTH ) )

                const adocInputElements = docContainer.filter ( ( docThisElement ) => docThisElement.nodeName = 'INPUT' );
                debugger;

                if ( adocInputElements !== null )
                {
                    const intNFlds      = adocInputElements.length;
                    debugger;

                    if ( intNFlds > ARRAY_IS_EMPTY )
                    {
                        for ( var intCurrFld = ARRAY_FIRST_ELEMENT;
                                  intCurrFld < intNFlds ;
                                  intCurrFld++ )
                        {
                            try
                            {
                                //  --------------------------------------------
                                //  LeadLifeJSHelpers method GetIdOrName returns
                                //  the empty string for INPUT elements that are
                                //  buttons, images, or other unwanted elements,
                                //  enabling all but shadow elements, those
                                //  designated with dummy CSS selector (class)
                                //  ID STTformField2SkipPrefill, and those where
                                //  the element value is the empty string to be
                                //  excluded based solely on its return value.
                                //  --------------------------------------------

                                var strFieldId = _LeadLifeJSHelpers.GetIdOrName ( adocInputElements [ intCurrFld ] );

                                if ( strFieldId.length > EMPTY_STRING_LENGTH && astrFieldIDs2Omit.findIndex ( ( strThisElement ) => strThisElement === strFieldId ) === INDEXOF_NOT_FOUND )
                                {
                                    if ( ( adocInputElements [ intCurrFld ].className.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND ) && ( !strFieldId.toLowerCase ( ).endsWith ( '_shadow' ) ) && ( adocInputElements [ intCurrFld ].value.length > EMPTY_STRING_LENGTH ) )
                                    {
                                        radocFields.push({
                                            FieldId    : strFieldId ,
                                            FieldValue : adocInputElements [ intCurrFld ].value
                                        });
                                    }   // if ( ( adocInputElements [ intCurrFld ].className.indexOf ( 'STTformField2SkipPrefill' ) === INDEXOF_NOT_FOUND ) && ( !strFieldId.toLowerCase ( ).endsWith ( '_shadow' ) ) )
                                }   // if ( strFieldId.length > EMPTY_STRING_LENGTH && astrFieldIDs2Omit.findIndex ( ( strThisElement ) => strThisElement === strFieldId ) === INDEXOF_NOT_FOUND )
                            }
                            catch ( ex2 )
                            {
                                LLCommon.LogException ( ex2 );

                                if ( ex2.stack.startsWith ( 'ReferenceError:' ) )
                                {   // Since ReferenceError Exceptions won't magically resolve on subsequent iterations, the Exception is re-thrown to end the loop.
                                    throw ex2;
                                }   // if ( ex2.stack.startsWith ( 'ReferenceError:' ) )
                            }
                        }   // for ( var intCurrFld = ARRAY_FIRST_ELEMENT; intCurrFld < intNFlds ;intCurrFld++ )
                    }   // if ( intNFlds > ARRAY_IS_EMPTY )
                }   // TRUE (anticipated outcome) block, if ( adocInputElements !== null )
                else
                {
                    fDiscardArray = true;
                    throw new Error ( strMethodName + ': Input parameter poContainer resolved to a null reference from JQuery for element ID = ' + docContainer.id );
                }   // FALSE (unanticipated outcome) block, if ( adocInputElements !== null )
            }
            catch ( ex1 )
            {
                LLCommon.LogException ( ex1 );
            }

            return fDiscardArray ? null : radocFields;      // Since the outer catch block doesn't set fDiscardArray, partially completed arrays are returned.
        }   // LLCommon.GetInputValuesFromContainer


        LLCommon.GetKendoGridRowFieldValueOrDefault = function ( pobjEvent , pstrGridSelector , pstrFieldName , pvarDefaultValue )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        GetKendoGridRowFieldValueOrDefault

                Method Goal:        Given a reference to a JavaScript event
                                    object, strings containing the jQuery ID
                                    of a Kendo UI grid and the ID of one of its
                                    columns (fields), and a default value of any
                                    type, return either the value in the named
                                    column or the specified default value.

                Input:              pobjEvent        = This JavaScript variable
                                                       must be either a handle
                                                       to the JavaScript Event
                                                       on behalf of which the
                                                       call is made. It is used
                                                       to locate the relevant
                                                       grid row.

                                    pstrGridSelector = This must be the string
                                                       representation of the
                                                       jQuery grid selector.

                                    pstrFieldName    = This must be the string
                                                       representation of the
                                                       field (column) name for
                                                       which a value is sought.

                                    pvarDefaultValue = This must be a JavaScript
                                                       object that represents a
                                                       default value to return
                                                       when the field cannot be
                                                       found. Stick to simple
                                                       scalars or native objects
                                                       such as Date objects.

                Output:             The return value is the value in the field,
                                    if one exists, or the specified default.

                Remarks:            Though this method was written entirely by
                                    Microsoft CoPilot in response to a series of
                                    prompts during which its design evolved, it
                                    remains subject to the usual testing rigors.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            var rvarValue = pvarDefaultValue;

            if ( pobjEvent && pstrGridSelector && pstrFieldName )
            {
                var $row      = $( pobjEvent.currentTarget ).closest ( 'tr' );
                var objGrid   = $( pstrGridSelector ).data ( 'kendoGrid' );

                if ( objGrid && typeof objGrid.dataItem === 'function' )
                {
                    var objDataItem = objGrid.dataItem ( $row );

                    if ( objDataItem && Object.prototype.hasOwnProperty.call ( objDataItem , pstrFieldName ) )
                    {
                        var rvarCandidate = objDataItem [ pstrFieldName ];

                        if ( rvarCandidate !== null && rvarCandidate !== undefined )
                        {
                            rvarValue = rvarCandidate;
                        }   // if ( rvarCandidate !== null && rvarCandidate !== undefined )
                    }   // if ( objDataItem && Object.prototype.hasOwnProperty.call ( objDataItem , pstrFieldName ) )
                }   // if ( objGrid && typeof objGrid.dataItem === 'function' )
            }   // if ( pobjEvent && pstrGridSelector && pstrFieldName )

            return rvarValue;
        };   // LLCommon.GetKendoGridRowFieldValueOrDefault


        LLCommon.GetLeadId = function ( )
        {
            return LLCommon.LeadId;
        }   // LLCommon.GetLeadId


        /**
         * ---------------------------------------------------------------------
         * Method Name:        GetNameOfCurrentFunction
         *
         * Method Goal:        Call this function to get the name of the
         *                     function that is calling it, so that it can be
         *                     displayed in a log entry or message.
         *
         * Output:             When called from function "myFunction" this
         *                     function returns "myFunction" as a String.
         *
         * Remarks:            Only Chromium browsers, which use Google's V8
         *                     rendering engine, implement a function named
         *                     Error.captureStackTrace. Since there is no good
         *                     way to detect this feature, it is detected by
         *                     catching the exception that arises when it is
         *                     undefined on the built-in Error object.
         *
         *                     This implementation was developed and tested
         *                     against the Mozilla Gecko engine, the only other
         *                     engine that is readily at hand.
         *
         *                     With some help from Bing and GPT-4, I have a
         *                     working version that should handle Safari, and
         *                     probably any other runtime engine that it
         *                     encounters.
         * ---------------------------------------------------------------------
         *
         * @function LLCommon.GetNameOfCurrentFunction
         * @returns {string} The name of the calling function, or a fallback label such as
         *                   "Mainline or anonymous", "anonymous", "Unknown StackTrace Format", or "Global Scope".
         */
        LLCommon.GetNameOfCurrentFunction = function ( )
        {
            const obj = { };

            try
            {
                Error.captureStackTrace ( obj, LLCommon.GetNameOfCurrentFunction );

                const intPosAt1             = obj.stack.indexOf ( ' at ' ) + 4;                 // Magic number 4 is the length of string ' at '.
                const intPosLParen1         = obj.stack.indexOf ( ' (' );

                return intPosLParen1 > INDEXOF_NOT_FOUND
                    ? obj.stack.substring ( intPosAt1, intPosLParen1 ).trim ( )
                    : 'Mainline or anonymous';
            }
            catch ( ex )
            {
                const astrStackFrames       = ex.stack.split ( '\n' );

                if ( astrStackFrames.length > SINGLE_CHARACTER )
                {
                    const frame             = astrStackFrames [ ARRAY_SECOND_ELEMENT ];
                    const intPosEndOfName   = frame.indexOf ( '@' );

                    if ( intPosEndOfName > INDEXOF_NOT_FOUND )
                    {
                        const fnName        = frame.substring ( SUBSTRING_FIRST_CHAR, intPosEndOfName ).trim ( );
                        return fnName.length > EMPTY_STRING_LENGTH
                            ? fnName
                            : 'anonymous';
                    }   // TRUE (anticipated outcome) block, if ( intPosEndOfName > INDEXOF_NOT_FOUND )
                    else
                    {
                        const intPosAt2     = ex.stack.indexOf ( ' at ' ) + 4;  // Plus 4 skips over the matched text.
                        const intPosLParen2 = ex.stack.indexOf ( ' (' );

                        if ( intPosAt2 > INDEXOF_NOT_FOUND )
                        {
                            if ( intPosLParen2 > INDEXOF_NOT_FOUND )
                            {
                                return ex.stack.substring ( intPosAt2, intPosLParen2 ).trim ( );
                            }   // TRUE (anticipated outcome) block, if ( intPosLParen2 > INDEXOF_NOT_FOUND )
                            else
                            {
                                return 'anonymous';
                            }   // FALSE (unanticipated outcome) block, if ( intPosLParen2 > INDEXOF_NOT_FOUND )
                        }   // TRUE (anticipated outcome) block, if ( intPosAt2 > INDEXOF_NOT_FOUND )
                        else
                        {
                            // Regex fallback for Gecko/Safari-style "functionName@file:line" when " at " is absent
                            const match     = ex.stack.match ( /([^\s@]+)@/ );

                            if ( match && match [ ARRAY_SECOND_ELEMENT ] )
                            {
                                return match [ ARRAY_SECOND_ELEMENT ].trim ( );
                            }   // TRUE (anticipated outcome) block, if ( match && match [ ARRAY_SECOND_ELEMENT ] )
                            else
                            {
                                return 'Unknown StackTrace Format';
                            }   // FALSE (unanticipated outcome) block, if ( match && match [ ARRAY_SECOND_ELEMENT ] )
                        }   // FALSE (unanticipated outcome) block, if ( intPosAt2 > INDEXOF_NOT_FOUND )
                    }   // FALSE (unanticipated outcome) block, if ( intPosEndOfName > INDEXOF_NOT_FOUND )
                }   // TRUE (anticipated outcome) block, if ( astrStackFrames.length > SINGLE_CHARACTER )
                else
                {
                    return 'Global Scope';
                }   // FALSE (unanticipated outcome) block, if ( astrStackFrames.length > SINGLE_CHARACTER )
            }   // catch ( ex )
        }; // LLCommon.GetNameOfCurrentFunction



        /**
         * Return a time of day greeting string that is determined by the hour
         * of the day when the function is invoked.
         *
         * @function LLCommon.getTimeOfDayGreeting
         * @param {string} [pstrDisplayName] - Required name for display in the
         *                                     greeting
         * @returns {string} A string that represents a temporally relevant
         *                   greeting
         * @throws {Error} If argument pstrDisplayName is not a string or is the
         *                 empty string.
         */
        LLCommon.getTimeOfDayGreeting = function ( pstrDisplayName )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction();

            if ( LLCommon.IsString ( pstrDisplayName ) && pstrDisplayName.length > EMPTY_STRING_LENGTH )
            {
                const intHour = new Date ( ).getHours ( );

                const strTimeOfDay =
                    intHour < 12
                        ? 'morning'
                        : intHour < 18
                            ? 'afternoon'
                            : 'evening';

                return `Good ${strTimeOfDay}, ${pstrDisplayName}`;
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrDisplayName ) && pstrDisplayName.length > EMPTY_STRING_LENGTH )
            false
            {
                throw new Error ( strMethodName + ': Input parameter pstrDisplayName resolved to a null reference, a reference to an object that is not a string, or the empty string.' );
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrDisplayName ) && pstrDisplayName.length > EMPTY_STRING_LENGTH )
        }   // LLCommon.getTimeOfDayGreeting


        /**
         * Return the full rendered box of an element, including margins and
         * scroll offset.
         *
         * @function LLCommon.getTotalRenderedBox
         * @param {HTMLElement|string} poElementOrId - The target element or its
         *                                             ID string.
         * @param {string} [psContext]               - Optional context label
         *                                             for error reporting.
         * @returns {Object} An object with top, left, width, and height properties in pixels.
         * @throws {Error} If the element is not found or invalid.
         */
        LLCommon.getTotalRenderedBox = function ( poElementOrId, psContext )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            const el            = LLCommon.getElementOrThrow ( poElementOrId, psContext || strMethodName +  ': Box target' );

            const rect          = el.getBoundingClientRect ( );
            const style         = window.getComputedStyle ( el );

            const marginTop     = parseFloat ( style.marginTop )    || 0;
            const marginBottom  = parseFloat ( style.marginBottom ) || 0;
            const marginLeft    = parseFloat ( style.marginLeft )   || 0;
            const marginRight   = parseFloat ( style.marginRight )  || 0;

            return {
                top    : window.scrollY + rect.top - marginTop,
                left   : window.scrollX + rect.left - marginLeft,
                width  : rect.width + marginLeft + marginRight,
                height : rect.height + marginTop + marginBottom
            };
        };  // LLCommon.getTotalRenderedBox


        /**
         * Calculates the total rendered height of an element, including vertical margins.
         *
         * @function LLCommon.getTotalRenderedHeight
         * @param {HTMLElement|string} poElementOrId - The target element or its ID string.
         * @param {string} [psContext] - Optional context label for error reporting.
         * @returns {number} The total height in pixels including top and bottom margins.
         * @throws {Error} If the element is not found or invalid.
         */
        LLCommon.getTotalRenderedHeight = function ( poElementOrId, psContext )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            const el            = LLCommon.getElementOrThrow ( poElementOrId, psContext || strMethodName + ': Height target' );

            const style         = window.getComputedStyle ( el );

            const marginTop     = parseFloat ( style.marginTop )    || 0;
            const marginBottom  = parseFloat ( style.marginBottom ) || 0;

            const height        = el.getBoundingClientRect ( ).height;

            return height + marginTop + marginBottom;
        };  // LLCommon.getTotalRenderedHeight method


        /**
         * Calculates the total rendered width of an element, including horizontal margins.
         *
         * @function LLCommon.getTotalRenderedWidth
         * @param {HTMLElement|string} poElementOrId - The target element or its ID string.
         * @param {string} [psContext] - Optional context label for error reporting.
         * @returns {number} The total width in pixels including left and right margins.
         * @throws {Error} If the element is not found or invalid.
         */
        LLCommon.getTotalRenderedWidth = function ( poElementOrId, psContext )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction();
            const el            = LLCommon.getElementOrThrow(poElementOrId, psContext || strMethodName + ': Width target' );

            const style         = window.getComputedStyle ( el );

            const marginLeft    = parseFloat ( style.marginLeft )  || 0;
            const marginRight   = parseFloat ( style.marginRight ) || 0;

            const width         = el.getBoundingClientRect ( ).width;

            return width + marginLeft + marginRight;
        };  // LLCommon.getTotalRenderedWidth method


        LLCommon.GetNotesSearchFilter = function ( )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        GetNotesSearchFilter

                Method Goal:        Get the filter string posted by the last
                                    call to LLCommon.PutNotesSearchFilter.

                Output:             If it succeeds, as well it should, its
                                    return value is the current value of the
                                    filter stored for the current user and
                                    agent. If no value is stored, the return
                                    value is the empty string.

                See Also:           LLCommon.PutNotesSearchFilter
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
            debugger;
            return LLCommon.DoAjax ( 'GetNotesSearchFilter' ,
                                     'GET' ,
                                     {
                                         'DomainId' : _domainid ,
                                         'TenantId' : _tenantid ,
                                         'LeadId'   : _leadid ,
                                         'UserId'   : _userid ,
                                         'Filter'   : EMPTY_STRING
                                     } );
        }   // LLCommon.GetNotesSearchFilter


        LLCommon.GetTenantId = function ( )
        {
            return LLCommon.TenantId;
        }   // LLCommon.GetTenantId


        LLCommon.GetUserId = function ( )
        {
            return LLCommon.UserId;
        }   // LLCommon.UserId


        LLCommon.HideButton = function ( Monikor , Default , AlsoCheckLoginEmail )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        HideButton

                Method Goal:        Call this function to evaluate the value of
                                    a System Configuration key, first by itself,
                                    then followed by the login email ID.

                Input:              Monikor             = System Configuration
                                                          key

                                    Default             = String default value

                                    AlsoCheckLoginEmail = Boolean flag, TRUE to
                                                          check in addition the
                                                          monikor followed by a
                                                          space and the login
                                                          email id

                Output:             The return value is the value found at the
                                    specified monikor, if present, or the
                                    specified default value.

                Remarks:            To evaluate for TRUE, test for equal to
                                    'true'.

                                    2024/04/08 - DAGray - Replace Open metthod
                                    GetByMonikorFirst, which falls back to
                                    domain 1000 and optionally Web.Config, with
                                    GetSystemConfigurationMonikorValue, which
                                    does neither.
               ----------------------------------------------------------------
            */

            const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

            var TrialResult      = LLCommon.DoAjax ( 'GetSystemConfigurationMonikorValue' ,
                                                     'GET' ,
                                                     {
                                                         'monikor'         : Monikor,
                                                         'tenantId'        : _tenantid ,
                                                         'domainId'        : _domainid ,
                                                         'defaultValue'    : Default
                                                     } );

            if ( AlsoCheckLoginEmail )
            {
                TrialResult      = LLCommon.DoAjax ( 'GetSystemConfigurationMonikorValue' ,
                                                     'GET' ,
                                                     {
                                                         'monikor'         : Monikor + SPACE_CHARACTER + LLCommon.DialerLogin ,
                                                         'tenantId'        : _tenantid ,
                                                         'domainId'        : _domainid ,
                                                         'defaultValue'    : TrialResult
                                                     } );
            }   // if ( AlsoCheckLoginEmail )

            return TrialResult;
        }   // LLCommon.HideButton


        LLCommon.HttpHead = async function ( pstrURL, paintOptions = { } )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        HttpHead

                Method Goal:        Perform a HTTP HEAD call against a URL to
                                    verify that it exists before attempting to
                                    use it.

                Input:              pstrURL             = String representation
                                                          of the URL to evaluate

                                    paintOptions        = Array of options, each
                                                          of which is an Integer

                                    AlsoCheckLoginEmail = Boolean flag, TRUE to
                                                          check in addition the
                                                          monikor followed by a
                                                          space and the login
                                                          email id

                Output:             If the resource to which string pstrURL
                                    points is valid (accessible), the return
                                    value is Boolean TRUE. Otherwise, the return
                                    value is Boolean FALSE.

                Remarks:            The three options in array paintOptions have
                                    default values that should work for most use
                                    cases.
                ----------------------------------------------------------------
            */

            const strMethodName  = LLCommon.GetNameOfCurrentFunction ( );

            const {
                intMaximumRetries      = 3,
                intTimeoutMilliseconds = 30000,
                intDelayMilliseconds   = 1000
            } = paintOptions;

            console.log ( strMethodName + ': pstrURL                = ' + pstrURL );
            console.log ( strMethodName + ': intMaximumRetries      = ' + intMaximumRetries );
            console.log ( strMethodName + ': intTimeoutMilliseconds = ' + intTimeoutMilliseconds );
            console.log ( strMethodName + ': intDelayMilliseconds   = ' + intDelayMilliseconds );

            for ( let intAttempt = 1;
                      intAttempt <= intMaximumRetries;
                      intAttempt++ )
            {
                try
                {
                    console.log ( strMethodName + ': attempt  = ' + intAttempt + ' of ' + intMaximumRetries );
                    const controller    = new AbortController ( );
                    const timer         = setTimeout ( ( ) => controller.abort ( 'Timeout exceeded' ), intTimeoutMilliseconds );
                    const fetchArgs     = {
                                              method  : 'HEAD',
                                              mode    : 'cors',
                                              headers : {
                                                            'Origin'                      : 'https://salestalktech.com',
                                                            'Access-Control-Allow-Origin' : 'https://salestalktech.com',
                                                            'X-LLCommon-Request'          : 'true'  // Custom marker for logging/debugging    },
                                                        },
                                              signal  : controller.signal
                                          };
                    const response      = await fetch ( pstrURL , fetchArgs );
                    console.log ( strMethodName + ': response.status = ' + response.status + ', attempt = ' + intAttempt + ' of ' + intMaximumRetries );
                    clearTimeout ( timer );

                    if ( response.ok )
                    {
                        console.log ( strMethodName + ': Returning TRUE because reponsse = OK, presumably 200 or 204' );
                        return true;
                    }   // if ( response.ok )

                    if ( response.status === 404 )
                    {
                        console.log ( strMethodName + ': Returning FALSE because status = 404' );
                        return false;
                    }   // if ( response.status === 404 )
                }
                catch ( ex )
                {
                    if ( intAttempt === intMaximumRetries ) return false;
                    // await new Promise ( res => setTimeout ( res , intDelayMilliseconds ) );
                }
            }   // for ( let intAttempt = 1; intAttempt <= intMaximumRetries; intAttempt++ ) )

            return false;
        }   // LLCommon.HttpHead = async function...


        LLCommon.Import_Tear_Sheet = function ( )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        Import_Tear_Sheet

                Method Goal:        Use a BootBox dialog box to manage uploading
                                    a Tear Sheet import file.

                Input:              None

                Output:             A Target Audience is created and populated.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            const strMessage =   '<form id="frmTearSheetImport" name="frmTearSheetImport">'
                               + '    <table class="table">'
                               + '        <tbody>'
                               + '            <tr>'
                               + '                <td>'
                               + '                    <p>'
                               + '                        <label for "txtTearSheetName"     id="lblTearSheetName">Tear Sheet Name</label>'
                               + '                        &nbsp;&nbsp;&nbsp;'
                               + '                        <input type="text"                id="txtTearSheetName" size="200" placeholder="Enter the name to assign to the Tear Sheet." />'
                               + '                    </p>'
                               + '                </td>'
                               + '            </tr>'
                               + '            <tr>'
                               + '                <td>'
                               + '                    <p>'
                               + '                        <label for "txtTearSheetFileName" id="lblTearSheetFileName">Tear Sheet File Name</label>'
                               + '                        &nbsp;&nbsp;&nbsp;'
                               + '                        <input type="file"                id="UploadFileBrowser">'
                               + '                    </p>'
                               + '                    <p>'
                               + '                        <input type="text"                id="txtTearSheetFileName" size="200" placeholder="Enter the name of the file that contains the Tear Sheet." />'
                               + '                    </p>'
                               + '                </td>'
                               + '            </tr>'
                               + '        </tbody>'
                               + '    </table>'
                               + '</form>';

            const box = bootbox.dialog ( {
                message : strMessage,
                size    : 'large',
                buttons : {
                    ok     : {
                                 label     : 'Ok',
                                 className : 'btn-success',
                                 callback: function ( )
                                 {
                                     debugger;
                                     const strUploadResult   = LLCommon.TearSheetUpload ( document.getElementById ( 'UploadFileBrowser' ) ,
                                                                                          document.getElementById ( 'txtTearSheetName' ).value );
                                     debugger;
                                 }
                             },
                    cancel : {
                                 label     : 'Cancel',
                                 className : 'btn-danger',
                                 callback  : function ( )
                                 {
                                    return;
                                 }
                             }
                          }
            });

            var dialog = box.find ( '.modal-dialog' );
            box.css ( 'display', 'block');
            box.css ( 'border-radius', '10px !important' );
            dialog.css ( 'margin-top', Math.max ( 0, ($(window).height ( ) - dialog.height ( ) ) / 2 ) );
            debugger;
            document.getElementById ( 'UploadFileBrowser' ).addEventListener ( 'change',
                                                                               ( event )  =>
                                                                                          {
                                                                                              debugger;
                                                                                              document.getElementById ( 'txtTearSheetFileName' ).value =  event.target.files[ARRAY_FIRST_ELEMENT].name;
                                                                                              debugger;
                                                                                              event.stopPropagation ( );
                                                                                          },
                                                                               false );
            debugger;
        }   // LLCommon.Import_Tear_Sheet


        // ============================================================================
        //  Element Resolution and Button State Utilities
        // ----------------------------------------------------------------------------
        //  These helpers provide a unified interface for enabling, disabling, and
        //  querying the disabled state of real <button> elements. Each method accepts
        //  either:
        //
        //      • A direct element reference, or
        //      • A string representing the element's ID
        //
        //  LLCommon.resolveElement enforces this contract. If the argument is a
        //  non-empty string, it must resolve to a valid element ID. If the argument is
        //  an object, it must be a truthy element reference. Any invalid input results
        //  in an explicit exception.
        //
        //  All state changes flow through the native `disabled` attribute. This ensures
        //  correct browser semantics, accessibility behavior, keyboard navigation,
        //  pointer handling, and form submission rules. No CSS classes or ARIA flags
        //  are required.
        //
        //  The triad below provides:
        //      • inputDisable    — sets disabled = true
        //      • inputEnable     — sets disabled = false
        //      • inputIsDisabled — returns the current disabled state
        //
        //  Each method is a single, intention-revealing statement.
        //  No breadcrumbs required.
        //
        // See also: LLCommon.DisableOrEnableButtonsInsideElement
        // ============================================================================


        // --- Element Resolver --------------------------------------------------------

        LLCommon.resolveElement = poelementOrId =>
        {
            // String case: treat as element ID.
            if ( LLCommon.IsString ( poelementOrId ) && poelementOrId.length > EMPTY_STRING_LENGTH )
            {
                const el = document.getElementById ( poelementOrId );

                if ( !el )
                    throw new Error ( LLCommon.GetNameOfCurrentFunction ( ) +
                                      ': "'
                                      + poelementOrId
                                      + '" is not a valid element ID.');

                return el;
            }   // if ( LLCommon.IsString ( poelementOrId ) && poelementOrId.length > EMPTY_STRING_LENGTH )

            // Object case: must be a truthy element reference.
            if ( poelementOrId )
            {
                return poelementOrId;
            }   // if ( poelementOrId )

            // Neither string nor object → contract violation
            throw new Error ( LLCommon.GetNameOfCurrentFunction ( ) +
                              ': Argument is neither a valid element nor a valid element ID.');
        };  // LLCommon.resolveElement


        // --- Disabled Attribute Triad ----------------------------------------

        LLCommon.inputDisable = poelementOrId =>
        {
            const el = LLCommon.resolveElement ( poelementOrId );
            if ( el && typeof el.disabled === 'boolean' ) el.disabled = true;
        };  // LLCommon.inputDisable


        LLCommon.inputEnable = poelementOrId =>
        {
            const el = LLCommon.resolveElement(poelementOrId);
            if ( el && typeof el.disabled === 'boolean' ) el.disabled = false;
        };  // LLCommon.inputEnable


        LLCommon.inputIsDisabled = poelementOrId => {
            const el = LLCommon.resolveElement ( poelementOrId );
            return ( el && typeof el.disabled === 'boolean' ) ? el.disabled : false;
        };  // LLCommon.inputIsDisabled


        LLCommon.IsAbsoluteUri = function ( poAnyJavaScriptObject )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        IsAbsoluteUri

                Method Goal:        Return Boolean True when its input argument,
                                    poAnyJavaScriptObject, is the string
                                    representation of an absolute (fully
                                    qualified) URI.

                Input:              poAnyJavaScriptObject = JavaScript object to
                                                            evaluate for whether
                                                            it represents an
                                                            absolute URI

                Output:             If poAnyJavaScriptObject is a string that
                                    represents an absolute (fully qualified)
                                    URL, the return value is Boolean True,
                                    otherwise, it is Boolean False.

                Remarks:            Since StringStartsWith returns Boolean false
                                    unless both of its inputs are strings, there
                                    is no point evaluating that attribute of
                                    poAnyJavaScriptObject internally.

                                    This function is admittedly syntactic sugar.
                ----------------------------------------------------------------
            */

            return  ( poAnyJavaScriptObject.toLowerCase ( ).startsWith ( LLCommon.PROTOCOL_IS_HTTPS ) || poAnyJavaScriptObject.toLowerCase ( ).startsWith ( LLCommon.PROTOCOL_IS_HTTP ) )
        }   // LLCommon.IsAbsoluteUri


        LLCommon.IsRoleAssigned = function ( pstrRoleName )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        IsRoleAssigned

                Method Goal:        Return Boolean True when its input argument,
                                    pstrRoleName, is the name of a Role that is
                                    assigned to the current user.

                Input:              pstrRoleName    = String representation of a
                                                      Role to evaluate

                Output:             If role pstrRoleName is assigned to the user
                                    identified by _userid, the return value is
                                    TRUE. Otherwise, the return value is FALSE.
                ----------------------------------------------------------------
            */

            return LLCommon.Roles4User.find ( ( element ) => element === pstrRoleName ) ? true : false;
        }   // LLCommon.IsRoleAssigned


        /**
        * Returns true if the input is a string (primitive or String object).
        *
        * This function does not throw.
        *
        * @function LLCommon.IsString
        * @param {*} poAnyJavaScriptObject - The object to evaluate.
        * @returns {boolean} True if the input is a string, otherwise false.
        * @see https://stackoverflow.com/questions/4059147/check-if-a-variable-is-a-string-in-javascript
        */
        LLCommon.IsString = function ( poAnyJavaScriptObject )
        {
            return ( typeof poAnyJavaScriptObject === 'string' || poAnyJavaScriptObject instanceof String );
        };   // LLCommon.IsString method


        /**
        * Evaluate a string to determine whether it is one or more email addresses.
        *
        * This function does not throw.
        *
        * @function LLCommon.IsValidEmailAddress
        * @param {Event | string} event - JavaScript Event object that represents
        *                                 the sending Input element, expected to be
        *                                 Text, OR a string that is expected to
        *                                 represent one or more email addresses
        * @param {integer}              - Maximum number of addresses, intended to
        *                                 limit from addresses to a single address
        * @returns {boolean} True if the input is a string, otherwise false.
        */
        LLCommon.IsValidEmailAddress = function ( event , MaxAddresses )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            try
            {
                const rxpPattern    = /^([\w\-\.'*+]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,16}))$/i;

                const strCandidates = LLCommon.IsString ( event )
                                      ? event
                                      : event instanceof Element && event.nodeName === 'INPUT' && LLCommon.IsString ( event.value )
                                        ? event.value
                                        : EMPTY_STRING;

                if ( strCandidates.length > EMPTY_STRING_LENGTH )
                {
                    if ( MaxAddresses !== undefined && MaxAddresses === NUMERIC_PLUS_ONE )
                    {
                        return rxpPattern.test ( strCandidates )
                    }   // TRUE (The requeest expects a single email address.) block, if ( MaxAddresses !== undefined && MaxAddresses === NUMERIC_PLUS_ONE )
                    else
                    {
                        astrCandidates = SplitMultipleAddresses ( strCandidates );

                        for ( var intJ = ARRAY_FIRST_ELEMENT;
                                  intJ < astrCandidates.length;
                                  intJ++ )
                        {
                            if ( !rxpPattern.test ( astrCandidates [ intJ ] ) )
                            {   // One false result invalidates the whole list.
                                return false;
                            }   // if ( !rxpPattern.test ( astrCandidates [ intJ ] ) )
                        }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < astrCandidates.length; intJ++ )

                        return true;
                    }   // FALSE (The request expects one or more email addresses.) block, if ( MaxAddresses !== undefined && MaxAddresses === NUMERIC_PLUS_ONE )
                }   // TRUE (anticipated outcome) block, if ( strCandidates.length > EMPTY_STRING_LENGTH )
                else
                {
                    return false;
                }   // FALSE (unanticipated outcome) block, if ( strCandidates.length > EMPTY_STRING_LENGTH )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return false;
            }


            /**
            * Split a delimited string into two or more individul email addresses
            * candidates.
            *
            * Though it's probably feasible to replace this function with a split
            * that takes a regular expression, I didn't think about it when I wrote
            * the method that invokes it. Since it's now production code, I am
            * unwilling to replace it until I have a working regular expression.
            *
            * This function does not throw.
            *
            * @function SplitMultipleAddresses
            * @param {string} pstrString2Split - string, possibly delimited by
            *                                    either commas or semicolons
            * @returns {string []}             - Array of strings, each of which MAY
            *                                    be a valid emial address
            */
            function SplitMultipleAddresses ( pstrString2Split )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                var rastrSplits     = [ ];

                if ( pstrString2Split === undefined || pstrString2Split === null || pstrString2Split === EMPTY_STRING )
                {
                    rastrSplits.push ( EMPTY_STRING );
                }   // TRUE (Dispatch the first 3 degenerate cases.) block, if ( pstrString2Split === undefined || pstrString2Split === null || pstrString2Split === EMPTY_STRING )
                else
                {
                    if ( LLCommon.IsString ( pstrString2Split ) )
                    {
                        if ( pstrString2Split.indexOf ( CSV_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
                        {
                            rastrSplits = LLCommon.StringSplitSharp ( pstrString2Split , CSV_SEPARATOR_CHAR );
                        }   // TRUE (The input string contains at least one comma.) block, if ( pstrString2Split.indexOf ( CSV_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
                        else if ( pstrString2Split.indexOf ( EMAIL_ADDRESS_ALTERNATE_DELIMITER ) > INDEXOF_NOT_FOUND )
                        {
                            rastrSplits = LLCommon.StringSplitSharp ( pstrString2Split , EMAIL_ADDRESS_ALTERNATE_DELIMITER );
                        }   // TRUE (The input string contains at least one semicolon.) block, if ( pstrString2Split.indexOf ( CSV_SEPARATOR_CHAR ) ) > INDEXOF_NOT_FOUND )
                        else
                        {
                            rastrSplits.push ( pstrString2Split );
                        }   // FALSE (The string contains neither a comma, nor a semicolon.) block, else if ( pstrString2Split.indexOf ( EMAIL_ADDRESS_ALTERNATE_DELIMITER ) > INDEXOF_NOT_FOUND )
                    }   // TRUE (The input is a non-empty string.) block, if ( LLCommon.IsString ( pstrString2Split ) )
                    else
                    {
                        rastrSplits.push ( EMPTY_STRING );
                    }   // FALSE (The input isn't a string.) block, if ( LLCommon.IsString ( pstrString2Split ) )
                }   // FALSE (The input isn't one of the first 3 degenerate cases.) block, if ( pstrString2Split === undefined || pstrString2Split === null || pstrString2Split === EMPTY_STRING )

                return rastrSplits;
            }   // function SplitMultipleAddresses
        }   // LLCommon.IsValidEmailAddress method


        LLCommon.LogException = function ( pstrMessage , event )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        LogException

                Method Goal:        Log an exception raised by the player or its
                                    associated code.

                Input:              pstrMessage = String passed into event
                                                  delegate function

                                    event       = String that identifies the
                                                  event in which the exception
                                                  arose

                Output:             This method returns strMessage, the text of
                                    the message appended to the database log and
                                    the console log.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            const strRequiredPrefix     = 'SalesTalk - URL - ' + window.location.href + SPACE_CHARACTER;

            var   strMessage            = EMPTY_STRING;
            var   strExceptionMessage   = EMPTY_STRING;
            var   strExceptionStack     = EMPTY_STRING;
            var   strAdditionalInfo     = EMPTY_STRING;

            if ( LLCommon.IsString ( event ) )
            {
                strMessage              =   'While processing an '
                                          + event
                                          + ', the following exception arose: '
                                          + pstrMessage;
            }   // TRUE (Event is a string.) block, if ( LLCommon.IsString ( event ) )
            else
            {   // Unless the first argument, event, is a string, the caller is expected to have supplied an Exception object and a message.
                if ( LLCommon.IsString ( pstrMessage ) )
                {
                    strMessage          = pstrMessage;
                }   // TRUE (This is by far the most common case.) block, if ( LLCommon.IsString ( pstrMessage ) )
                else
                {
                    if ( Object.is ( event, undefined ) )
                    {   // The first, and only, argument, is the Exception object.
                        strExceptionMessage = pstrMessage.hasOwnProperty ( 'message' ) ? pstrMessage.message : EMPTY_STRING;
                        strExceptionStack   = pstrMessage.hasOwnProperty ( 'stack'   ) ? pstrMessage.stack   : EMPTY_STRING;
                    }   // TRUE (There is no separate event object.) block, if ( Object.is ( event, undefined ) )
                    else
                    {   // Though we got oone of each, we trust, but verify, that the object passed second looks like an Exceiption object.
                        strExceptionMessage = event.hasOwnProperty ( 'message' ) ? event.message : EMPTY_STRING;
                        strExceptionStack   = event.hasOwnProperty ( 'stack'   ) ? event.stack   : EMPTY_STRING;
                        strAdditionalInfo   = '- Additional information supplied by calling routine: ' + pstrMessage;
                    }   // FALSE (There is an event object, and it isn't a string. Trust, but verify that it is an event object.) block, if ( Object.is ( event, undefined ) )

                    strMessage  =   'Error: Message = ' + QUOTE_DOUBLE + strExceptionMessage + QUOTE_DOUBLE
                                  + ', Stack Trace = ' + strExceptionStack
                                  + ', Additional Info = ' + strAdditionalInfo;
                }   // FALSE (This is by far the LEAST common case.) block, if ( LLCommon.IsString ( pstrMessage ) )
            }   // FALSE (Event is an object.) block, if ( LLCommon.IsString ( event ) )

            console.error ( strMethodName + ': ' + strMessage );

            //  ----------------------------------------------------------------
            //  Guarantee that the log always gets a stack trace and the URL.
            //  ----------------------------------------------------------------

            console.trace ( );
            console.error ( strRequiredPrefix );

            if ( ! strMessage.startsWith ( strRequiredPrefix ) )
            {
                strMessage      = strRequiredPrefix + strMessage;
            }   // if ( ! strMessage.startsWith ( strRequiredPrefix ) )

            var fKeepTrying     = true;
            var intRetryCount   = NUMERIC_ZERO;
            var fAjaxError      = false;

            while ( fKeepTrying )
            {
                $.ajax (
                {
                    url     : LLCommon.AjaxUrlPrefix + 'Open/PostToTrace',
                    type    : 'POST',
                    async   : false,
                    cache   : false,
                    data    : { 'Message': strMessage },
                    error   : function ( jqXHR , textStatus , errorThrown )
                              {
                                  if ( intRetryCount > NUMERIC_ZERO )
                                  {
                                      console.warn ( strMethodName + ': Retry Count = ' + intRetryCount + ': ' + textStatus
                                                     + SPACE_CHARACTER + jqXHR.responseText
                                                     + SPACE_CHARACTER + errorThrown );
                                  } // if ( intRetryCount > NUMERIC_ZERO )

                                  fAjaxError = true;
                              }
                });

                if ( fAjaxError )
                {
                    intRetryCount++;

                    if ( intRetryCount > LLCommon.AJAX_RETRY_LIMIT )
                    {
                        fKeepTrying = false;
                    }   // if ( intRetryCount > LLCommon.AJAX_RETRY_LIMIT )

                    fAjaxError      = false;
                }   // TRUE (undesired outcome) block, if ( fAjaxError )
                else
                {
                    fKeepTrying     = false;
                }   // FALSE (desired outcome) block, if ( fAjaxError )
            }   // while ( fKeepTrying )

            return strMessage;
        };   // LLCommon.LogException method


        LLCommon.LogImportantValue = function ( pstrLabel , pobjValue , pobjContext )
        {
            /*
                ----------------------------------------------------------------
                Name:       LogImportantValue

                Goal:       Log an Important Value on the console.

                Arguments:  pstrLabel   = This string is the label to display
                                          with the value. If the supplied value
                                          is not a string, the absolute name of
                                          this function is substituted.

                            pobjValue   = This object can be of any type.

                            pobjContext = This object can be of any type, and it
                                          may be omitted. Use it to add context
                                          to the generated log message.

                Returns:    This function has no return value; it is called for
                            its side effect of generating a console log record.

                ToDo:       See notes at time index 2025/10/01 14:14:32 in
                            \WebHooks4Phone\AXXESS\AXXESS_Phone_Interface.TXT.
                ----------------------------------------------------------------
            */

            const strLabel2Use = typeof pstrLabel === 'string' ? pstrLabel : LLCommon.GetNameOfCurrentFunction();

            let strSummary     = EMPTY_STRING;

            if ( typeof pobjValue === 'string' || typeof pobjValue === 'number' || typeof pobjValue === 'boolean' )
            {
                strSummary     = `returned scalar: ${pobjValue}`;
            }
            else if ( pobjValue && typeof pobjValue === 'object' )
            {
                const keys     = Object.keys ( pobjValue ).slice ( ARRAY_FIRST_ELEMENT , 5 );
                const summary  = keys.map ( k => `${k}: ${pobjValue [ k ]}`).join ( ', ' );
                strSummary     = `returned object: { ${summary} }`;
            }
            else
            {
                strSummary     = `returned unexpected type: ${pobjValue}`;
            }

            if ( pobjContext )
            {
                if ( typeof pobjContext === 'string' )
                {
                    console.log ( `${strLabel2Use} ${strSummary} | Context: { ${pobjContext} }` );
                }   // TRUE (The context takes its most likely form, a string.) block, if ( typeof pobjContext === 'string' )
                else if ( typeof pobjContext === 'object' )
                {
                    const contextKeys    = Object.keys ( pobjContext ).slice ( ARRAY_FIRST_ELEMENT );
                    const contextSummary = contextKeys.map ( k => `${k}: ${pobjContext [ k ]}`).join ( ', ' );

                    console.log ( `${strLabel2Use} ${strSummary} | Context: { ${contextSummary} }` );
                }   // TRUE (The context is an object, and we list all keys.) block, else if ( typeof pobjContext === 'object' )
                else
                {
                    console.log ( `${strLabel2Use} ${strSummary} | Context: { ${pobjContext} }` );
                }   // FALSE (The context is neither string nor object. Presumably it's a scalar, such as a number.)
            }   // TRUE (Context has a value.) block, if ( pobjContext )
            else
            {
                console.log ( `${strLabel2Use} ${strSummary}` );
            }   // FALSE (Context is absent.) block, if ( pobjContext )
        };  // LLCommon.LogImportantValue method


        LLCommon.ManageCallButton = function ( leadId )
        {
            /*
                ----------------------------------------------------------------
                Name:       ManageCallButton

                Goal:       Create and manage the Call Button modal dialog box.

                Arguments:  leadId  = This string is the string representation
                                      of the Lead ID to pass along to server
                                      side code that records the activity.

                Returns:    Since this function is called for its side effects,
                            its return value is undefined.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
            LLCommon.Trace ( strMethodName +': leadId = ' + leadId );

            debugger;

            const strLogin2Use = LLCommon.IsString ( LLCommon.DialerLogin ) && LLCommon.DialerLogin.length > EMPTY_STRING_LENGTH
                                 ? LLCommon.DialerLogin
                                 : LLCommon.IsString ( LLCommon.AuthenticatedLoginId ) && LLCommon.AuthenticatedLoginId.length > EMPTY_STRING_LENGTH
                                   ? LLCommon.AuthenticatedLoginId
                                   : _loginSource !== SRC_IS_UNKNOWN
                                     ? _login
                                     : EMPTY_STRING;

            $.ajax({
                url    : LLCommon.AjaxUrlPrefix + 'Open/GetLeadPhoneList',
                data   : {
                           "LeadId": leadId
                         },
                success: function ( data )
                {
                    LLCommon.Trace ( strMethodName +': data = ' + data );
                    debugger;

                    if ( data.length > EMPTY_STRING_LENGTH )
                    {
                        var zoomenabled = LLCommon.HideButton ( 'ZoomEnabled', 'false', true ) === 'true';
                        var zoomphone   = EMPTY_STRING;

                        if ( zoomenabled )
                        {
                            zoomphone = ( LLCommon.HideButton ( 'ZoomPhoneEnabled' , 'false', true ) === 'true') ? LLCommon.HideButton ( 'ZoomPhone' , EMPTY_STRING , true ) : EMPTY_STRING;

                            if ( zoomphone === EMPTY_STRING )
                            {
                                zoomenabled = false;
                            }
                            else
                            {
                                zoomphone = '<br/><span style="font-size:16px"><b>Zoom Phone Number ' + zoomphone + ' will be used to dial out.</b><br/><b>An incorrect number may prevent the call from being recorded.</b></span></br>';
                            }
                        }

                        var box = bootbox.dialog({
                            title: 'Select Number To Dial' + (zoomenabled ? ' - or Select Zoom Meeting' : EMPTY_STRING),
                            message: '<div id="dialContact" style="height:100%;width:100%;font-size:14px" '
                                + ( zoomenabled ? '<span>Ensure that Zoom is logged in, corresponding to SalesTalk login.</span><br>' : EMPTY_STRING )
                                + '<span></span>'
                                + '<input id="ddlLeadPhoneNumbers" name="LeadPhoneNumbers" style="width:100%" />'
                                + '</br>' + zoomphone
                                + '</br><b>Select a Phone Number to dial - then Click the Dial button.</b>'
                                + ( zoomenabled ? '</br><b>Alternately, Click the Zoom Meeting button.</br>Ensure that Zoom is logged in, corresponding to SalesTalk login.</b>' : EMPTY_STRING )
                                + '</br>'
                                + '</div>',
                            buttons: {
                                cancel: {
                                    label     : 'Cancel',
                                    className : 'btn-danger',
                                    callback  : function ( )
                                    {
                                    }
                                },
                                dial: {
                                    label     : 'Dial',
                                    className : 'btn-success',
                                    target    : '_blank',
                                    callback  : function ( )
                                    {
                                        debugger;
                                        $.ajaxSetup ( { async: false } );
                                        $.get ( _domainid === 0 ? LLCommon.AjaxUrlPrefix + 'Zoom/IsZoomTokenRowPresent?LeadId=' + leadId
                                                                : LLCommon.AjaxUrlPrefix + 'Zoom/IsZoomTokenRowPresent?LeadId=' + leadId
                                                                                         + '&domainId='                         + _domainid
                                                                                         + '&tenantId='                         + _tenantid
                                                                                         + '&userid='                           + _userid
                                                                                         + '&loginEmail='                       + strLogin2Use,
                                        function ( data )
                                        {
                                            debugger;
                                            LLCommon.Trace ( 'dada:::', data );

                                            if ( data !== 'True' )
                                            {
                                                var strPhoneNumber = $( '#ddlLeadPhoneNumbers' ).data ( 'kendoDropDownList' ).value ( );
                                                LogZoomEvent ( 'StartCall' , leadId , false , strPhoneNumber );
                                                $.ajax({
                                                    url     : LLCommon.AjaxUrlPrefix + 'Open/StartCall',
                                                    data    : {
                                                                'LeadId': leadId,
                                                                'Phone' : strPhoneNumber,
                                                                'Email' : _domainid === 0 ? EMPTY_STRING : strLogin2Use
                                                              },
                                                    success : function ( data )
                                                              {
                                                                  if ( data.startsWith ( 'zoom' ) )
                                                                  {
                                                                      window.open ( data, 'ZoomPhone' ); // window.close();
                                                                  } else {
                                                                      bootbox.alert ( data );
                                                                  }
                                                              },
                                                    error   : function ( jqXHR, textStatus, errorThrown )
                                                              {
                                                                  LLCommon.Trace ( textStatus + SPACE_CHARACTER + jqXHR.responseText + SPACE_CHARACTER + errorThrown );
                                                              }
                                                          });
                                            } else {
                                                var phoneNo  = $( '#ddlLeadPhoneNumbers' ).data ( 'kendoDropDownList') .value ( );
                                                var Url2Open = _domainid === 0 ? LLCommon.AjaxUrlPrefix + 'Zoom/SignIn?LeadID=' + leadId
                                                                                      + '&doLaunchMeeting=false'
                                                                                      + '&PhoneNo='           + phoneNo
                                                                               : LLCommon.AjaxUrlPrefix + 'Zoom/SignIn?LeadID=' + leadId
                                                                                      + '&doLaunchMeeting=false'
                                                                                      + '&PhoneNo='           + phoneNo
                                                                                      + '&domainId='          + _domainid
                                                                                      + '&tenantId='          + _tenantid
                                                                                      + '&userid='            + _userid
                                                                                      + '&loginEmail='        + strLogin2Use;
                                                LogZoomEvent ( 'SignIn FOR CALL' , leadId , false , Url2Open , phoneNo );
                                                window.open ( Url2Open , '_blank' );
                                            }
                                        });
                                    }
                                },
                                zoomLogin : {
                                    label    : 'Zoom Meeting',
                                    className: 'btn-warning',
                                    target   : '_blank',
                                    callback: function ( )
                                    {
                                        var Url2Open = _domainid === 0 ? LLCommon.AjaxUrlPrefix + 'Zoom/SignIn?LeadID=' + leadId
                                                                              + '&doLaunchMeeting=true'
                                                                       : LLCommon.AjaxUrlPrefix + 'Zoom/SignIn?LeadID=' + leadId
                                                                              + '&doLaunchMeeting=true'
                                                                              + '&domainId='          + _domainid
                                                                              + '&tenantId='          + _tenantid
                                                                              + '&userid='            + _userid
                                                                              + '&loginEmail='        + strLogin2Use;
                                        LogZoomEvent ( 'SignIn FOR MEETING' , leadId , true , Url2Open );   // Omitting the phone number suppresses it in the log message.
                                        window.open ( Url2Open , '_blank' );
                                    }
                                },
                            }
                        });

                        var dialog = box.find ( '.modal-dialog' );
                        box.css ( 'display', 'block' );
                        box.css ( 'border-radius' , '10px !important' );
                        dialog.css ( "margin-top" , Math.max ( 0 , ( $( window ).height ( ) - dialog.height ( ) ) / 2 ) );

                        if ( LLCommon.HideButton ( 'ZoomEnabled' , 'false' , true ) === 'false')
                        {
                            dialog.find ( '.btn-primary' ).hide ( );
                        }

                        box.on ( 'shown.bs.modal', function ( )
                        {
                            $( '#ddlLeadPhoneNumbers').kendoDropDownList ( {
                                dataSource     : data,
                                //autoWidth    : true,
                                dataTextField  : 'Key',
                                dataValueField : 'Value',
                                dataBound      : function ( e )
                                                 {
                                                     LLCommon.setKendoDropDownWidth ( e );
                                                 }
                            });
                        });
                    }
                    else
                    {
                        bootbox.alert ( 'There are no phone numbers to dial' );
                        return;
                    }
                },
                error: function ( jqXHR, textStatus, errorThrown )
                       {
                           console.error ( strMethodName +': AJAX Exception: textStatus = ' + textStatus + ', errorThrown = ' + errorThrown );
                           bootbox.alert ( textStatus + SPACE_CHARACTER + jqXHR.responseText + SPACE_CHARACTER + errorThrown );
                       }
            });

            function LogZoomEvent ( pstrEventDescription , pLeadId , pDoLaunchMeeting , pUrl2Open , pPhoneNo )
            {
                var strSignInArgs =   'EventDescription = ' + pstrEventDescription
                                    + ', LeadId = '         + pLeadId
                                    + ', DoLaunchMeeting =' + pDoLaunchMeeting;
                    strSignInArgs += pPhoneNo !== undefined ? pPhoneNo : EMPTY_STRING;
                    strSignInArgs += ', domainId = '        + _domainid;
                    strSignInArgs += ', tenantId = '        + _tenantid;
                    strSignInArgs += ', userid = '          + _userid;
                    strSignInArgs += ', loginEmail = '      + strLogin2Use;
                    strSignInArgs += ', Url2Open = '        + pUrl2Open;

                $.ajax ( { url   : LLCommon.AjaxUrlPrefix + 'Open/SendToTrace',
                           type  : 'GET',
                           cache : false,
                           data  : {
                                        'Message':   LLCommon.STANDARD_SEND_TO_TRACE_PREFIX
                                                   + window.location.href
                                                   + ' Opening new Zoom tab with the following parameters: '
                                                   + strSignInArgs
                                   }
                          }
                       );
            }   // function LogZoomEvent
        }   // LLCommon.ManageCallButton method


        /**
         * Parses a string into a Boolean value.
         *
         * Accepts a variety of common representations for true/false:
         *   - True values:  "true",  "1", "yes", "y", "t"
         *   - False values: "false", "0", "no",  "n", "f"
         *
         * Input is case-insensitive and trimmed of surrounding whitespace.
         *
         * @function LLCommon.parseBool
         * @param {string|String} pstrBooleanString - The string to parse. Must
         *                                            be a primitive string
         *                                            or a String object. Throws
         *                                            if not a string.
         * @returns {boolean} The parsed Boolean value.
         * @throws {TypeError} If the input is not a string.
         * @throws {Error} If the string cannot be parsed into a Boolean.
         *
         * @example
         * LLCommon.parseBool ( 'true');    // → true
         * LLCommon.parseBool ( 'YES');     // → true
         * LLCommon.parseBool ( 'n');       // → false
         * LLCommon.parseBool ( '0');       // → false
         *
         * @example
         * // Invalid input
         * LLCommon.parseBool ( 'maybe' );  // throws Error
         * LLCommon.parseBool ( 42 );       // throws TypeError
         */
        LLCommon.parseBool = function ( pstrBooleanString )
        {
            if ( ! ( LLCommon.IsString ( pstrBooleanString ) ) )
            {
                throw new TypeError ( 'Expected a string' );
            }   // if ( ! ( LLCommon.IsString ( pstrBooleanString ) ) )

            switch ( pstrBooleanString.trim ( ).toLowerCase ( ) )
            {
                case 'true' : case '1': case 'yes': case 'y': case 't': return true;
                case 'false': case '0': case 'no' : case 'n': case 'f': return false;
                default: throw new Error ( `Cannot parse boolean from "${str}"` );
            }   // switch ( pstrBooleanString.trim ( ).toLowerCase ( ) )
        };  // LLCommon.parseBool


        LLCommon.PasteTextFromClipboard = function ( event , pstrMsgTpl , poTarget )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PasteTextFromClipboard

                Method Goal:        Paste text from the Clipboard into an input
                                    control.

                Input:              event       = Reference to BUTTON activated
                                                  by the user

                                    pstrMsgTpl  = Template from which to create
                                                  a message to display to the
                                                  user as a confirmation

                                    poTarget    = Reference to target (usually
                                                  an INPUT element that receives
                                                  the text from the Clipboard

                Output:             This method has only side effects. However,
                                    it returns Boolean false as a fallback to
                                    the preventDefault that is issued against
                                    the sending button or other element.

                Remarks:            1)  When specified, argument pstrMsgTpl is
                                        the template from which a message is
                                        constructed and returned to the user in
                                        a dialog box. When pstrMsgTpl is
                                        undefined, null, the empty string, or
                                        another type of object, it is discarded,
                                        and no confirmation message is displayed
                                        to the user.

                                    2)  Argument pstrMsgTpl is expected to be a
                                        tokenized string in which substring
                                        STT_VALUE_TOKEN (##Value##) is a
                                        placeholder for the string that is read
                                        from the clipboard.

                                    3)  When specified, the type of argument
                                        poTarget is evaluated. If its type is
                                        String, it is treated as the ID of the
                                        document element that receives the text,
                                        in lieu of the previousElementSibling of
                                        the event source identifieed by argument
                                        event. Otherwise, it is assumed to be a
                                        reference to the document element that
                                        should receive the input.
                ----------------------------------------------------------------
            */

            function CBShowConfirmation ( pstrCopiedText , pstrMsgTpl )
            {
                const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );
                const strTemplate       = LLCommon.IsString ( pstrMsgTpl ) ? pstrMsgTpl : STT_VALUE_TOKEN + ' copied from clipboard';
                const strMessage        = strTemplate.replace ( STT_VALUE_TOKEN , pstrCopiedText.length > EMPTY_STRING_LENGTH ? pstrCopiedText : '** NOTHING **' );
                alert ( strMessage );
            }   // function ShowConfirmation


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            navigator.clipboard.readText ( ).then ( ( copiedText ) => {
                poTarget.value          = copiedText;
                CBShowConfirmation ( copiedText , pstrMsgTpl );
            } );
            event.preventDefault ( );
            return false;
        }   // LLCommon.PasteTextFromClipboard method


        LLCommon.PasteTextOntoClipboard = function ( event , poTarget , pstrMsgTpl )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PasteTextOntoClipboard

                Method Goal:        Copy (paste) text from an input control onto
                                    the Clipboard.

                Input:              event       = Reference to BUTTON activated
                                                  by the user

                                    poTarget    = Reference to target (usually
                                                  an INPUT element that supplies
                                                  the text that goes onto the
                                                  Clipboard

                                    pstrMsgTpl  = Template from which to create
                                                  a message to display to the
                                                  user as a confirmation

                Output:             This method has only side effects. However,
                                    it returns Boolean false as a fallback to
                                    th preventDefault that is issued against the
                                    sending button or other element.

                Remarks:            1)  When specified, argument pstrMsgTpl is
                                        the template from which a message is
                                        constructed and returned to the user in
                                        a dialog box. When pstrMsgTpl is
                                        undefined, null, the empty string, or
                                        another type of object, it is discarded,
                                        and no confirmation message is displayed
                                        to the user.

                                    2)  Argument pstrMsgTpl is expected to be a
                                        tokenized string in which ##Value## is
                                        a placeholder for the string that is
                                        read from the target element.

                                    3)  When specified, the type of argument
                                        poTarget is evaluated. If its type is
                                        String, it is treated as the ID of the
                                        document element that supplies the text,
                                        in lieu of the previousElementSibling of
                                        the event source identifieed by argument
                                        event. Otherwise, it is assumed to be a
                                        reference to the document element that
                                        should supply the text. In either case,
                                        the text comes from its value attribute.
                ----------------------------------------------------------------
            */

            function GetValueFromElement ( pdocTargetElement )
            {
                const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

                if ( Object.is ( pdocTargetElement.value , undefined ) )
                {
                    return pdocTargetElement.innerText;
                }   // TRUE (The object of the clipboard copy is an INPUT element that has a value property.) block, if ( Object.is ( pdocTargetElement.value , undefined ) )
                else
                {
                    return pdocTargetElement.value;
                }   // FALSE (The object of the clipboard copy is a container element.) block, if ( Object.is ( pdocTargetElement.value , undefined ) )
            }   // function GetValueFromElement


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
            event.preventDefault ( );   // Keep the dialog box open.
            const docTargetElement      = LLCommon.IsString ( poTarget ) ? document.getElementById ( poTarget ) : poTarget;
            var strAlertMessage;

            //  ----------------------------------------------------------------
            //  The following works when the nodeName of docTargetElement is
            //  TEXTAREA. This may need adjustment for other types of controls
            //  such as some INPUT elements.
            //  ----------------------------------------------------------------

            if ( docTargetElement !== null )
            {
                navigator.clipboard.writeText ( GetValueFromElement ( docTargetElement ) ).then (
                    ( ) =>
                    {
                        debugger;

                        strAlertMessage = pstrMsgTpl === undefined ? QUOTE_DOUBLE + docTargetElement.innerText + '" copied to Clipboard.' : pstrMsgTpl.replace ( STT_VALUE_TOKEN , docTargetElement.innerText );

                        if ( _fDebugLogging )
                        {
                            console.info ( strAlertMessage );
                            alert ( strAlertMessage );
                        } else {
                            console.info ( strAlertMessage );
                        }
                    },
                    ( ) =>
                    {
                        debugger;

                        strAlertMessage = QUOTE_DOUBLE + docTargetElement.innerText + '" Clipboard copy FAILED. Select text and copy it by hand.'

                        if ( _fDebugLogging ) {
                            console.info ( strAlertMessage );
                            alert ( strAlertMessage );
                        } else {
                            console.info ( strAlertMessage );
                        }
                    }
                );
            }   // TRUE (anticipated outcome) block, if ( docTargetElement !== null )
            else
            {
                debugger;
                strAlertMessage         = 'Clipboard copy failed due to an internal error. Internal function argument poTarget must be either a String or a Document Element. Howeveer, its true type is ' + typeof poTarget;

                if ( _fDebugLogging ) {
                    console.info ( strAlertMessage );
                    alert ( strAlertMessage );
                } else {
                    console.info ( strAlertMessage );
                }
            }   // FALSE (unanticipated outcome) block, if ( docTargetElement !== null )

            return false;
        }   // LLCommon.PasteTextOntoClipboard method


        LLCommon.PositionRelativeToOffsetParent = function ( poElementTarget , pintOffsetTop , pintOffsetLeft )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PositionRelativeToOffsetParent

                Method Goal:        Use a native HTML DIALOG tag to prompt for a
                                    required value.

                Input:              poElementTarget = String representation of
                                                      the ID of the element to
                                                      be positioned or an object
                                                      reference to the element

                                    pintOffsetTop   = Integer offset from top of
                                                      offsetParent in pixels

                                    pintOffsetLeft  = Integer offset from left
                                                      of offsetParent in pixels

                Output:             Evaluate this function for its side effects.
                                    Expect nothing in return.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            try
            {
                debugger;
                const docElementTarget  = LLCommon.IsString ( poElementTarget )
                                          ? document.getElementById ( poElementTarget )
                                          : poElementTarget instanceof Element || poElementTarget instanceof HTMLDocument
                                            ? poElementTarget
                                            : null;

                if ( docElementTarget !== null )
                {
                    if ( LLCommon.IsValidInteger ( pintOffsetTop ) )
                    {
                        if ( LLCommon.IsValidInteger ( pintOffsetLeft ) )
                        {
                            if ( docElementTarget.offsetParent !== null )
                            {
                                const intParentOffsetHeight = docElementTarget.offsetParent.offsetHeight;
                                const intParentOffsetLeft   = docElementTarget.offsetParent.offsetLeft;

                                const intAbsoluteTop        = pintOffsetTop  === ( -1 )
                                                              ? intParentOffsetHeight - poElementTarget.clientHeight
                                                              : pintOffsetTop;
                                const intAbsoluteLeft       = pintOffsetLeft === ( -1 )
                                                              ? 0
                                                              : pintOffsetLeft;
                                docElementTarget.style.setProperty ( 'top'  , intAbsoluteTop  + 'px' );
                                docElementTarget.style.setProperty ( 'left' , intAbsoluteLeft + 'px' );
                            }   // TRUE (anticipated outcome) block, if ( docElementTarget.offsetParent !== null )
                            else
                            {
                                throw new Error ( strMethodName + ': Element' + docElementTarget.id + ' must have an offsetParent.' );
                            }   // FALSE (unanticipated outcome) block, if ( docElementTarget.offsetParent !== null )
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( pintOffsetLeft ) )
                        else
                        {
                            throw new Error ( strMethodName + ': Argument value for pintOffsetLeft "' + pintOffsetLeft + '" is invalid. It must be an integer. Instead, its object type is "' + typeof pintOffsetLeft + '".' );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( pintOffsetLeft ) )
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( pintOffsetTop ) )
                    else
                    {
                        throw new Error ( strMethodName + ': Argument value for pintOffsetTop "' + pintOffsetTop + '" is invalid. It must be an integer. Instead, its object type is "' + typeof pintOffsetTop + '".' );
                    }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( pintOffsetTop ) )
                }   // TRUE (anticipated outcome) block, if ( docElementTarget !== null )
                else
                {
                    throw new Error ( strMethodName + ': Argument value for poElementTarget "' + poElementTarget + '" is invalid. It must be either the string representation of a valid element ID or the JavaScript object refernce to a valid document element. Instead, its object type is "' + typeof poElementTarget + '".' );
                }   // FALSE (unanticipated outcome) block, if ( docElementTarget !== null )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
            }
        }   // LLCommon.PositionRelativeToOffsetParent


        LLCommon.PromptForInput = function ( pstrMessage2Show , pstrLabel4Prompt , pstrDefault , pstrInputType , pdocParentElement )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PromptForInput

                Method Goal:        Use a native HTML DIALOG tag to prompt for a
                                    required value.

                Input:              pstrMessage2Show    = Short text to display
                                                          above the prompt

                                    pstrLabel4Prompt    = Short label to display
                                                          to the left of the
                                                          INPUT element that
                                                          receives the input

                                    pstrDefault         = Default text to return
                                                          when the dialog is
                                                          canceled, defaulting
                                                          to 'cancelled'

                                    pstrInputType       = Type to assign to the
                                                          INPUT text, defaulting
                                                          to 'text'

                                    pdocParentElement   = String containing the
                                                          ID of the element that
                                                          should own the dialog,
                                                          or a reference to the
                                                          element itself,
                                                          defaulting to DOCUMENT

                Output:             The return value is the value entered into
                                    the INPUT element, unless the dialog box is
                                    cancelled, in which case it is pstrDefault.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            function AwaitReturnValue ( poDialog )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                debugger;

                return new Promise ( ( resolve ) =>
                {
                    const intervalId = setInterval( ( ) =>
                    {
                        debugger;

                        if ( poDialog.returnValue.length > EMPTY_STRING_LENGTH )
                        {
                            debugger;
                            clearInterval ( intervalId );
                            resolve ( );
                        }   // if ( poDialog.returnValue.length > EMPTY_STRING_LENGTH )
                    },
                    100);
                });
            }   // private function AwaitReturnValue

            async function AwaitDialogClose ( poDialog )
            {
                debugger;
                const x = await AwaitReturnValue ( poDialog );
                LLCommon.Trace ( 'poDialog.returnValue = '+ poDialog.returnValue + ' AND x = ' + x );
            }

            debugger;

            const docParent     = LLCommon.IsString ( pdocParentElement )
                                  ? document.getElementById ( pdocParentElement )
                                  : pdocParentElement instanceof Element || pdocParentElement instanceof HTMLDocument
                                    ? pdocParentElement
                                    : document.body;

            if ( docParent === null )
            {
                throw new Error ( strMethodName + ': Argument value for pdocParentElement "' + pdocParentElement + '" is invalid.'  );
            }   // if ( docParent === null )

            const strIdPrefix   = strMethodName.replace ( '\.' , UNDERSCORE_CHAR ) + UNDERSCORE_CHAR;
            const docDlg        = document.createElement ( 'dialog' );
            docDlg.id           = strIdPrefix + 'Dialog';

            const docTheForm    = document.createElement ( 'form' );
            docDlg.appendChild ( docTheForm );

            const strInputType  = LLCommon.IsString ( pstrInputType ) ? pstrInputType : 'text';
            const strDefaultVal = LLCommon.IsString ( pstrDefault )   ? pstrDefault   : 'cancelled';
            let   rstrRetVal    = strDefaultVal;

            if ( LLCommon.IsString ( pstrMessage2Show ) )
            {
                const docMessage2Show       = document.createElement ( 'span' );
                docMessage2Show.innerHTML   = pstrMessage2Show;
                docTheForm.appendChild ( docMessage2Show );

                docTheForm.appendChild ( document.createElement ( 'br' ) );
            }   // if ( LLCommon.IsString ( pstrMessage2Show ) )

            const strInputBoxId = strIdPrefix + 'InputBox'; // This value is used twice.

            if ( LLCommon.IsString ( pstrLabel4Prompt ) )
            {
                const docLabel4Prompt       = document.createElement (  'label' );
                docLabel4Prompt.setAttribute ( 'for' , strInputBoxId );
                docLabel4Prompt.innerHTML   = pstrLabel4Prompt;
                docTheForm.appendChild ( docLabel4Prompt );
            }   // if ( LLCommon.IsString ( pstrLabel4Prompt ) )

            const docInputBox   = document.createElement ( 'input' );
            docInputBox.id      = strInputBoxId;
            docInputBox.type    = strInputType;
            docTheForm.appendChild ( docInputBox );

            docTheForm.appendChild ( document.createElement ( 'br' ) );

            const docOKBtn      = document.createElement ( 'button' );
            docOKBtn.id         = strIdPrefix + 'OKButton';
            docOKBtn.type       = 'button';
            docOKBtn.className  = 'btn-success';
            docOKBtn.value      = strDefaultVal;
            docOKBtn.innerHTML  = 'OK';
            docTheForm.appendChild ( docOKBtn );

            const docSpacer     = document.createElement ( 'span' );
            docSpacer.innerHTML = HTML_NBSP + HTML_NBSP + HTML_NBSP;
            docSpacer.style     = 'width: 20px;';
            docTheForm.appendChild ( docSpacer );

            const docCanBtn     = document.createElement ( 'button' );
            docCanBtn.id        = strIdPrefix + 'CancelButton';
            docCanBtn.type      = 'button';
            docCanBtn.className = 'btn-danger';
            docCanBtn.innerHTML = 'Cancel';
            docTheForm.appendChild ( docCanBtn );

            docInputBox.addEventListener  ( 'change' , ( event ) => {
                                              debugger;
                                              docOKBtn.value    = docInputBox.value;
                                          });
            docOKBtn.addEventListener     ( 'click' , ( event ) => {
                                              debugger;
                                              docDlg.close ( docInputBox.value );               // Send the input box value here.
                                          });
            docDlg.addEventListener ( 'close' , ( event ) => {
                                              debugger;
                                              rstrRetVal        = docInputBox.value;
                                          });
            docParent.appendChild ( docDlg );

            docDlg.showModal ( );

            debugger;

            AwaitDialogClose ( docDlg ).then ( LLCommon.Trace ( 'docDlg.returnValue = ' + docDlg.returnValue ) );

            debugger;

            return rstrRetVal;
        }   // LLCommon.PromptForInput


        LLCommon.Prompt4Words2ActionsLogin = function ( pBHuserid , pWhichTable )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        Prompt4Words2ActionsLogin

                Method Goal:        Use a native HTML DIALOG tag to prompt for a
                                    required value.

                Input:              pBHuserid   = Bullhorn userid also passed
                                                  into server-side Open method
                                                  GetSalesTalkUserId4BullhornUserId

                                    pWhichTable = ID of table to make visible

                                    Everything else that it needs lives in
                                    globalvariables.

                Output:             The return value is the new Words2Actions
                                    login ID.
                ----------------------------------------------------------------
            */

            const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            LLCommon.ShowOrHideElement ( 'STT_HOURGLASS'    , LLCommon.ELEMENT_HIDE );
            LLCommon.ShowOrHideElement ( 'W2APrerequisites' , LLCommon.ELEMENT_SHOW );
            LLCommon.ShowOrHideElement ( pWhichTable        , LLCommon.ELEMENT_SHOW );
            LLCommon.ResolveDomainAndTenantIDs ( );

            const aoWords2ActionsLoginInfos = LLCommon.DoAjax ( 'GetDomainUsers4PickList',
                                                                'GET',
                                                                {
                                                                   'DomainId'       : _domainid,
                                                                   'BullhornUserId' : pBHuserid
                                                                } );

            if ( Array.isArray ( aoWords2ActionsLoginInfos ) )
            {
                const docW2AUsers           = document.getElementById ( 'cboWords2ActionsLogin' );

                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < aoWords2ActionsLoginInfos.length;
                          intJ++ )
                {
                    /*
                        --------------------------------------------------------
                        Model array element:

                          {
                              "Name": "Abhi Awade",
                              "id": 1063,
                              "Email": "abhi@optiliza.com"
                          },
                        --------------------------------------------------------
                    */

                    var docOption           = document.createElement ( 'option' );

                    docOption.value         = aoWords2ActionsLoginInfos [ intJ ].id   + LOGICAL_NEGATE + aoWords2ActionsLoginInfos [ intJ ].Email;
                    docOption.innerHTML     = aoWords2ActionsLoginInfos [ intJ ].Name + '('            + aoWords2ActionsLoginInfos [ intJ ].Email + ')';

                    docW2AUsers.appendChild ( docOption );
                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < aoWords2ActionsLoginInfos.length; intJ++ )

                debugger;

                document.getElementById ( 'SubmitW2ALogin' ).addEventListener ( 'click' , ( event ) => { Fixup4W2A ( true  ); } );
                document.getElementById ( 'CancelW2ALogin' ).addEventListener ( 'click' , ( event ) => { Fixup4W2A ( false ); } );
            }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoWords2ActionsLoginInfos ) )
            else
            {
                LLCommon.LogException ( aoWords2ActionsLoginInfos );
                alert ( 'The Words2Actions module raised an exception. Please contact support.' );
            }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoWords2ActionsLoginInfos ) )

            debugger;

            function Fixup4W2A ( pfOutcome )
            {
                const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

                debugger;

                if ( pfOutcome )
                {
                    _login              = document.getElementById ( 'cboWords2ActionsLogin' ).value;
                    _loginSource        = SRC_IS_WORDS2ACTIONS_LOGIN;
                    const strWords2ActionsLoginInfo = LLCommon.DoAjax ( 'SetSalesTalkUserId4BullhornUserId',
                                                                        'GET',
                                                                        {
                                                                           'BullhornUserId'  : _userid,
                                                                           'SalesTalkUserId' : _login
                                                                        } );

                    if ( strWords2ActionsLoginInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND && ( ! strWords2ActionsLoginInfo.startsWith ( 'Exception' ) ) )
                    {
                        //  -----------------------------------------------------
                        //  Model record:
                        //
                        //  25311-4Â¬dgray@leadlife.comÂ¬1051Â¬1000Â¬1000Â¬InsuranceDB
                        //  0       1                  2    3    4    5
                        //  1st     2nd                3rd  4th  5th  6th
                        //  -----------------------------------------------------

                        const astrW2ALoginInfos = strWords2ActionsLoginInfo.split ( LOGICAL_NEGATE );

                        _login                  =            astrW2ALoginInfos [ ARRAY_SECOND_ELEMENT ];
                        _userid                 = parseInt ( astrW2ALoginInfos [ ARRAY_THIRD_ELEMENT  ] );
                        _domainid               = parseInt ( astrW2ALoginInfos [ ARRAY_FOURTH_ELEMENT ] );
                        _tenantid               = parseInt ( astrW2ALoginInfos [ ARRAY_FIFTH_ELEMENT  ]  );
                        _domainname             =            astrW2ALoginInfos [ ARRAY_SIXTH_ELEMENT  ];

                        _domainnameSource       = SRC_IS_WORDS2ACTIONS_LOGIN;
                        _domainidSource         = SRC_IS_WORDS2ACTIONS_LOGIN
                        _tenantidSource         = SRC_IS_WORDS2ACTIONS_LOGIN
                        _useridSource           = SRC_IS_WORDS2ACTIONS_LOGIN;

                        LLCommon.DialerLogin    = _login;

                        //  ----------------------------------------------------
                        //  Since the code in Words2Actions_Recorder_Forms.js
                        //  runs before LLCommon.DialerLogin is properly set,
                        //  these instructions must go here because this code is
                        //  executed after LLCommon.DialerLogin is initialized.
                        //  ----------------------------------------------------

                        LLCommon.ShowOrHideElement ( 'DoCallRail' ,
                                                     LLCommon.DialerLogin.length > EMPTY_STRING_LENGTH && _leadid > NO_LEAD_ID );

                        if ( LLCommon.DialerLogin.length > EMPTY_STRING_LENGTH )
                        {
                            ShowOrHideElement ( 'SearchButtonHole' ,
                                                LLCommon.ELEMENT_SHOW );
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.DialerLogin.length > EMPTY_STRING_LENGTH )
                        else
                        {
                            LLCommon.Trace ( strMethodName + ': The login parameter is missing from the page URL.<br>Though the parameter NAME is case sensitive, the parameter value is not.' );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.DialerLogin.length > EMPTY_STRING_LENGTH )

                        LLCommon.EnabledCRM     = LLCommon.GetEnabledCrmInfo ( _tenantid ,
                                                                               _domainid );
                        LLCommon.EvaluateEntityType ( );
                        LLCommon.ShowOrHideElement ( 'W2APrerequisites' , LLCommon.ELEMENT_HIDE );
                        LLCommon.ShowOrHideElement ( 'LLTheWholePage'   , LLCommon.ELEMENT_SHOW );
                    }   // TRUE (anticipated outcome) block, if ( strWords2ActionsLoginInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND && ( ! strWords2ActionsLoginInfo.startsWith ( 'Exception' ) ) )
                    else
                    {
                        LLCommon.LogException ( strMethodName + ': Exception in SetSalesTalkUserId4BullhornUserId = ' + strWords2ActionsLoginInfo );
                        bootbox.alert ( 'An internal error arose. Please contact Words2Actions customer support.' );

                        return Revert4W2A ( _CorporationID + '@Bullhorn.com' );
                    }   // FALSE (unanticipated outcome) block, if ( strWords2ActionsLoginInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND && ( ! strWords2ActionsLoginInfo.startsWith ( 'Exception' ) ) )
                }   // TRUE (anticipated outcome) block, if ( pfOutcome )
                else
                {
                    LLCommon.LogException ( strMethodName + ': User failed to enter a valid login ID.' );
                    bootbox.alert ( 'To obtain a Words2Actions login, please contact Words2Actions customer support.' );

                    return Revert4W2A ( _CorporationID + '@Bullhorn.com' );
                }   // FALSE (unanticipated outcome) block, if ( pfOutcome )
            }   // function Fixup4W2A


            function Revert4W2A ( pstrRevertedLogin )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                debugger;

                _fDomainAndTenantIDAreSafe = LCommon.SynchronizeLoginInfo ( pstrRevertedLogin ,
                                                                            SRC_IS_BULLHORN_DEFAULT_LOGIN );

                if ( _fDomainAndTenantIDAreSafe )
                {
                    LLCommon.Trace ( strMethodName + ': SalesTalk login value reverted to ' + pstrRevertedLogin + ', with source set to ' + SRC_IS_BULLHORN_DEFAULT_LOGIN );
                }   // TRUE (anticipated outcome) block, if ( _fDomainAndTenantIDAreSafe )
                else
                {
                    LLCommon.Trace ( strMethodName + ': FAILURE while reverting SalesTalk login value to ' + pstrRevertedLogin + ', with source set to ' + SRC_IS_BULLHORN_DEFAULT_LOGIN );
                }   // FALSE (unanticipated outcome) block, if ( _fDomainAndTenantIDAreSafe )
            }   // function Revert4W2A
        }   // LLCommon.Prompt4Words2ActionsLogin


        LLCommon.PutNotesSearchFilter = function ( pstrFilterString , pintLeadId )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PutNotesSearchFilter

                Method Goal:        Given a reference to JavaScript String, post
                                    it in a System Configuration key that links
                                    it to the current User ID and Lead ID.

                Input:              pstrFilterString = This string is the filter
                                                       value to post for the
                                                       current user and lead IDs.

                                    pintLeadId       = For a search of Notes for
                                                       one lead, the Integer
                                                       representation of a Lead
                                                       ID, otherwise zero, to
                                                       search all Notes in the
                                                       current SalesTalk Domain

                Output:             If it succeeds, as well it should, its
                                    return value is the empty string. Otherwise,
                                    the return value is an error message.

                Remarks:            Since this method is currently used in only
                                    one other place, sibling LLCommon method
                                    LLCommon.SearchNotesAndDisplayMatches, there
                                    is none of the usual input validation. Other
                                    callers MUST supply a valid integer.

                See Also:           LLCommon.GetNotesSearchFilter
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
            debugger;

            if ( LLCommon.IsString ( pstrFilterString ) )
            {
                return LLCommon.DoAjax ( 'PutNotesSearchFilter' ,
                                         'GET' ,
                                         {
                                             'DomainId' : _domainid ,
                                             'TenantId' : _tenantid ,
                                             'LeadId'   : pintLeadId ,
                                             'UserId'   : _userid ,
                                             'Filter'   : pstrFilterString
                                         } );
            }   // TRUE (anticipated outcime) block, if ( LLCommon.IsString ( pstrFilterString ) )
            else
            {
                return 'Argument pstrFilterString must be a String. Actual Type = ' + typeof pstrFilterString + ', Actual Value = ' + ( pstrFilterString === null ? 'NULL' : pstrFilterString );
            }   // FALSE (unanticipated outcime) block, if ( LLCommon.IsString ( pstrFilterString ) )
        }   // LLCommon.PutNotesSearchFilter


        LLCommon.RegisterClickEventHandler = function ( poElement , pfnCallback , paoArgV )
        {
            /*
                ----------------------------------------------------------------
                Name:       RegisterClickEventHandler

                Goal:       Register a Click event handler calls a specified
                            function, passing an optional argument list.

                Arguments:  poElement   = Identify the element to watch for Click
                                          events by passing either its element ID
                                          or a reference to the DOM element.

                            pfnCallback = This parameter MUST be a function that
                                          accepts either no arguments at all or
                                          an array of JavaScript objects.

                            paoArgV     = When used, this parameter MUST be an
                                          array of JavaScript objects, which MAY
                                          have mixed object types, or a single
                                          object.

                Returns:    This function returns TRUE if ALL of its arguments
                            pass the smell test and the event registration seems
                            to have succeeded.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            try
            {
                if ( Object.is ( poElement , undefined ) )
                {   // Ensure that the value of poElement is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter poElement is undefined.' );
                }   // if ( Object.is ( poElement , undefined ) )

                if ( Object.is ( pfnCallback , undefined ) )
                {   // Ensure that the value of pfnCallback is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter pfnCallback is undefined.' );
                }   // if ( Object.is ( pfnCallback , undefined ) )

                const docEventTarget = LLCommon.IsString ( poElement ) ? document.getElementById ( poElement ) : poElement;

                if ( docEventTarget === null )
                {   // Ensure that the value of argument poElement is either a string that represents the ID of a DOM element or is a reference to such an element.
                    throw new Error ( strMethodName + ': The value of required poElement is neither an element, nor a string that is the ID of an exlement in the active document.' );
                }   // if ( docEventTarget === null )

                if ( pfnCallback instanceof Function )
                {
                    console.log ( 'Registering a Click event on document element ID ' + docEventTarget.id + '.' );

                    docEventTarget.addEventListener ( 'click' , ( event ) =>
                    {
                        debugger;                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

                        console.log ( 'Document element ID = ' + event.currentTarget.id + ' was just clicked.' );

                        event.stopPropagation ( );

                        if ( Object.is ( paoArgV , undefined ) )
                        {
                            pfnCallback ( event );          // Pass along the event object.
                        }   // TRUE (The callback function is devoid of arguments.) block, if ( Object.is ( paoArgV , undefined ) )
                        else
                        {
                            pfnCallback ( paoArgV );
                        }   // FALSE (The callback function accepts at most one argument, which MAY be an array.) block, if ( Object.is ( paoArgV , undefined ) )
                    }); // end of docEventTarget.addEventListener ( 'keyup' , ( event )

                    console.log ( 'Click event registered for document element ID ' + docEventTarget.id + '.' );
                }   // TRUE (anticipated outcome) block, if ( pfnCallback instanceof Function )
                else
                {
                    throw new Error ( strMethodName + ': The value of required parameter pfnCallback MUST be a JavaScript Function.' );
                }   // FALSE (unanticipated outcome) block, if ( pfnCallback instanceof Function )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
            }
        }   // LLCommon.RegisterClickEventHandler


        LLCommon.RegisterReturnKeyWatchdog = function ( poElement , pfnCallback , paoArgV )
        {
            /*
                ----------------------------------------------------------------
                Name:       RegisterReturnKeyWatchdog

                Goal:       Register an event listener that calls a specified
                            function, passing an optional argument list.

                Arguments:  poElement   = Identify the element to watch for
                                          return (enter) key events by passing
                                          either its element ID or a reference
                                          to the DOM element.

                            pfnCallback = This parameter MUST be a function that
                                          accepts either no arguments at all or
                                          an array of JavaScript objects.

                            paoArgV     = When used, this parameter MUST be an
                                          array of JavaScript objects, which MAY
                                          have mixed object types, or a single
                                          object.

                Returns:    This function returns TRUE if ALL of its arguments
                            pass the smell test and the event registration seems
                            to have succeeded.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            try
            {
                if ( Object.is ( poElement , undefined ) )
                {   // Ensure that the value of poElement is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter poElement is undefined.' );
                }   // if ( Object.is ( poElement , undefined ) )

                if ( Object.is ( pfnCallback , undefined ) )
                {   // Ensure that the value of pfnCallback is at lease DEFINED.
                    throw new Error ( strMethodName + ': The value of required parameter pfnCallback is undefined.' );
                }   // if ( Object.is ( pfnCallback , undefined ) )

                const docEventTarget = LLCommon.IsString ( poElement ) ? document.getElementById ( poElement ) : poElement;

                if ( docEventTarget === null )
                {   // Ensure that the value of argument poElement is either a string that represents the ID of a DOM element or is a reference to such an element.
                    throw new Error ( strMethodName + ': The value of required poElement is neither an element, nor a string that is the ID of an exlement in the active document.' );
                }   // if ( docEventTarget === null )

                if ( pfnCallback instanceof Function )
                {
                    docEventTarget.addEventListener ( 'keyup' , ( event ) =>
                    {
                        debugger;                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

                        if ( event.isComposing || event.keyCode === 229 )
                        {
                            return;
                        }   // TRUE (The event is in the middle of processing in an IME.) block, if ( event.isComposing || event.keyCode === 229 )
                        else
                        {
                            if ( event.code === 'Enter' || event.key === 'Enter' || event.keyCode === 13 )
                            {
                                event.stopPropagation ( );

                                if ( Object.is ( paoArgV , undefined ) )
                                {
                                    pfnCallback ( );
                                }   // TRUE (The callback function is devoid of arguments.) block, if ( Object.is ( paoArgV , undefined ) )
                                else
                                {
                                    pfnCallback ( paoArgV );
                                }   // FALSE (The callback function accepts at most one argument, which MAY be an array.) block, if ( Object.is ( paoArgV , undefined ) )
                            }   // if ( event.code === 'Enter' || event.key === 'Enter' || event.keyCode === 13 )
                        }   // FALSE (The event is a normal event.) block, if ( event.isComposing || event.keyCode === 229 )
                    }); // end of docEventTarget.addEventListener ( 'keyup' , ( event )
                }   // TRUE (anticipated outcome) block, if ( pfnCallback instanceof Function )
                else
                {
                    throw new Error ( strMethodName + ': The value of required parameter pfnCallback MUST be a JavaScript Function.' );
                }   // FALSE (unanticipated outcome) block, if ( pfnCallback instanceof Function )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
            }
        }   // LLCommon.RegisterReturnKeyWatchdog


        LLCommon.ReportThroughAnyTextElement = function ( pdocDisplayElement , pstrDisplayValue )
        {
            /*
                ----------------------------------------------------------------
                Name:       ReportThroughAnyTextElement

                Goal:       Return the string specified by pstrDisplayValue
                            through text element pdocDisplayElement.

                            Currently, it supports INPUT elements of type text,
                            TD, P, SPAN, and DIV.

                Arguments:  pdocDisplayElement = This String argument specifies
                                                 the ID of the element through
                                                 which to display the report.

                            pstrDisplayValue   = This String argument specifies
                                                 the text of the report to make.

                Returns:    This function returns void (It has no return value.)
                            which appears to calling routines as returning the
                            value of undefined.
                ----------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
            debugger;

            if ( pdocDisplayElement !== null )
            {
                switch ( pdocDisplayElement.nodeName )
                {
                    case 'INPUT':
                        if ( pdocDisplayElement.type === 'text' )
                        {
                            pdocDisplayElement.value = pstrDisplayValue;
                        }   // TRUE (anticipated outcome) block, if ( docExternalCrmIdInputElement.type === 'text' )
                        else {
                            throw new Error ( 'ERROR in ' + strMethodName + ': The specified INPUT element, ' + LLCommon.QuoteString ( pdocDisplayElement ) + ' MUST be an INPUT element and its type must be text. The nodeName attribute of the element is ' + docExternalCrmIdInputElement.nodeName + ( docExternalCrmIdInputElement.nodeName === 'INPUT' ? ', but its type is ' + docExternalCrmIdInputElement.type + '.' : ', which has no type attribute.' ) );
                        }   // FALSE (unanticipated outcome) block, if ( docExternalCrmIdInputElement.type === 'text' )
                        break;  // case 'INPUT'

                    case 'TD':
                    case 'P':
                    case 'SPAN':
                    case 'DIV':
                        pdocDisplayElement.innerText = pstrDisplayValue;
                        break;  // case 'TD'
                }   // switch ( docExternalCrmIdInputElement.nodeName )

            }   // TRUE (anticipated outcome) block, if ( pdocDisplayElement !== null )
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': The specified INPUT element, ' + LLCommon.QuoteString ( pdocDisplayElement ) + ' MUST be an INPUT element and its type must be text. The nodeName attribute of the element is ' + docExternalCrmIdInputElement.nodeName + ( docExternalCrmIdInputElement.nodeName === 'INPUT' ? ', but its type is ' + docExternalCrmIdInputElement.type + '.' : ', which has no type attribute.' ) );
            }   // FALSE (unanticipated outcome) block, if ( pdocDisplayElement !== null ))
        }   // LLCommon.ReportThroughAnyTextElement


        LLCommon.ResetCheatSheet = function ( )
        {
            const docCheatSheet         = document.getElementById ( 'cheatsheet' );
            const fToggle               = ( ( docCheatSheet.className.indexOf ( LLCommon.STT_HideElement ) > INDEXOF_NOT_FOUND ) || ( docCheatSheet.className.length === EMPTY_STRING_LENGTH ) );

            if ( fToggle )
            {
//              CheatSheet ( ); // Opening it causes it to be populated with fresh values.
                // Though opening it once only should fix it, prudence dictates leaving it as it.
            }   // TRUE (If closed it is, call the CheatSheet routine once, which opens and populates it.) block, if ( fToggle )
            else
            {
                CheatSheet ( ); // Close it.
                CheatSheet ( ); // Populate it and re-open it.
            }   // FALSE (If open it was, call the CheatSheet routine TWICE, to close it, then re-open it populated with updated field values.) block, if ( fToggle )

            return fToggle;
        }   // LLCommon.ResetCheatSheet


        LLCommon.ResetThisForm = function  ( event )
        {
            /*
                ----------------------------------------------------------------
                Method Name:        PromptForInput

                Method Goal:        Use a native HTML DIALOG tag to prompt for a
                                    required value.

                Input:              event    = Event on which to emp.

                Output:             The return value is the value entered into
                                    the INPUT element, unless the dialog box is
                                    cancelled, in which case it is pstrDefault.
                ----------------------------------------------------------------
            */

            const strMethodName     = GetNameOfCurrentFunction ( );

            debugger;

            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': The ResetThisForm event procedure is starting.' );

            if ( LLCommon._fFormIsDirty )
            {   // Since the Click event listener is a brand new regular function, calling it directly is the most efficient way to invoke its event listener.
                LLCommon.Trace ( ScriptInfoForLog ( LLCommon_SCRIPTSOURCE ,
                                                    LLCommon_VERSION ,
                                                    LLCommon_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: Updating dirty record of SalesTalk lead ID ' + _leadid + ' in CRM' ) );
                LLCommon._FSuppressCRMUpdateAlert = true;

                if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                {
                    if ( window.confirm ( 'LOSE your changes?' ) )
                    {
                        console.log ( 'The operator elected to DISCARD their unsaved work.' );
                    }   // TRUE (The operator elected to discard their unsaved work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                    else
                    {
                        DoUpdateCrmNow ( );
                    }   // FALSE (The operator elected to save their work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                }   // TRUE (The active form is write only.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                else
                {
                    DoUpdateCrmNow ( );
                    _LeadLifeJSHelpers.HandleFormPrefill ( 'leadlife_mobile_contact_view' ,
                                                           _leadid );
                }   // FALSE (The active form is a standard read/write form.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
            }   // if ( LLCommon._fFormIsDirty )

            AdjustButtonProperties ( BUTTON_STATE_VISIBLE );
            LLCommon.Trace ( LLCommon_SCRIPTSOURCE + ': The ResetThisForm event procedure is ending.' );
            event.stopPropagation ( );
            debugger;
            event.preventDefault ( );
            debugger;

            //  ----------------------------------------------------------------
            //  Though maybe unnecessary, best practice is for tests of URL
            //  string values to be case insensitive. Since the string.prototype
            //  functions lack case insensitive overloads, the strings are first
            //  converted to lower case, then tested.
            //  ----------------------------------------------------------------

            const strLocationPathNameLC = location.pathname.toLowerCase ( );

            if ( ( !strLocationPathNameLC.endsWith ( '/mobile' ) ) && ( !strLocationPathNameLC.endsWith ( '/mobile/' ) ) )
            {
//              poEvent.currentTarget.form.reset ( ); === BAD IDEA!!!
                location.reload ( );
            }   // if ( ( !strLocationPathNameLC.endsWith ( '/mobile' ) ) && ( !strLocationPathNameLC.endsWith ( '/mobile/' ) ) )

            LLCommon.ResetCheatSheet ( );

            return false;
        }   // LLCommon.ResetThisForm


        LLCommon.ResolveDomainAndTenantIDs = function ( )
        {   // This code is separated into a function to simplify the flow of control.
            const strMethodName                     = LLCommon.GetNameOfCurrentFunction ( );

            try
            {
                if ( !_fDomainAndTenantIDAreSafe )
                {   // Avoid wasted trips to the server for work that's already done.
                    const strBasicLeadInfo          = LLCommon.DoAjax ( 'GetDomainTenant4LeadId' ,
                                                                        'GET' ,
                                                                        {
                                                                            'leadId'    : _leadid ,
                                                                            'userId'    : _userid ,
                                                                            'loginName' : _login
                                                                        } );

                    if ( strBasicLeadInfo.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
                    {
                        const astrBasicLeadInfo = strBasicLeadInfo.split ( PIPE_CHAR_SPLIT_MATCH );

                        const STTDomainId           = parseInt ( astrBasicLeadInfo [ ARRAY_FIRST_ELEMENT  ] );
                        const STTTenantId           = parseInt ( astrBasicLeadInfo [ ARRAY_SECOND_ELEMENT ] );
                        const STTUserId             = parseInt ( astrBasicLeadInfo [ ARRAY_FOURTH_ELEMENT ] );
                        const STTLoginName          =            astrBasicLeadInfo [ ARRAY_FIFTH_ELEMENT  ];
                        const STTDomainName         =            astrBasicLeadInfo [ ARRAY_THIRD_ELEMENT  ];

                        if ( _domainidSource !== SRC_IS_UNKNOWN )
                        {
                            if ( STTDomainId !== _domainid )
                            {
                                if ( STTTenantId > MINIMUM_STT_ENTITY_ID )
                                {   // MINIMUM_STT_ENTITY_ID is a special ID that is reserved for use by the system, and may be associated with any entity.
                                    LLCommon.LogException ( strMethodName + ': Security Warning - Domain ID associated with lead ID ' + _leadid + ' = ' + STTTenantId + ', while Domain ID associated with user login ' + _login + ' = ' + _domainid + ' - Logged-in user is accessing a record that is associated with another user in the same SalesTalk domain.' );
                                }   // if ( STTTenantId > MINIMUM_STT_ENTITY_ID )

                                _domainid           = STTDomainId;
                                _domainidSource     = SRC_IS_LEAD_ID_PER_URL;

                                _domainname         = STTDomainName;
                                _domainnameSource   = SRC_IS_LEAD_ID_PER_URL;

                                sessionStorage.setItem ( 'domainid'   , _domainid );
                                sessionStorage.setItem ( 'domainname' , _domainname );
                            }   // if ( STTDomainId !== _domainid )
                        }   // TRUE (We have a valid domain ID.) block, if ( _domainidSource !== SRC_IS_UNKNOWN )
                        else
                        {
                            _domainid               = STTDomainId;
                            _domainidSource         = SRC_IS_LEAD_ID_PER_URL;

                            _domainname             = STTDomainName;
                            _domainnameSource       = SRC_IS_LEAD_ID_PER_URL;

                            sessionStorage.setItem ( 'domainid'   , _domainid );
                            sessionStorage.setItem ( 'domainname' , _domainname );
                        }   // FALSE (We haven't a valid domain ID.) block, if ( _domainidSource !== SRC_IS_UNKNOWN )

                        if ( _tenantidSource !== SRC_IS_UNKNOWN )
                        {
                            if ( STTTenantId !== _tenantid )
                            {
                                if ( STTTenantId > MINIMUM_STT_ENTITY_ID )
                                {   // MINIMUM_STT_ENTITY_ID is a special ID that is reserved for use by the system, and may be associated with any entity.
                                    LLCommon.LogException ( strMethodName + ': Security Warning - Tenant ID associated with lead ID ' + _leadid + ' = ' + STTTenantId + ', while Tenant ID associated with user login ' + _login + ' = ' + _tenantid + ' - Logged-in user is accessing a record that is associated with another user in the same SalesTalk domain.' );
                                }   // if ( STTTenantId > MINIMUM_STT_ENTITY_ID )

                                _tenantid           = STTTenantId;
                                _tenantidSource     = SRC_IS_LEAD_ID_PER_URL;

                                sessionStorage.setItem ( 'tenantid' , _tenantid );
                            }   // if ( STTTenantId !== _tenantid )
                        }   // TRUE (We have a valid tenant ID.) block, if ( _tenantidSource !== SRC_IS_UNKNOWN )
                        else
                        {
                            _tenantid               = STTTenantId;
                            _tenantidSource         = SRC_IS_LEAD_ID_PER_URL;

                            sessionStorage.setItem ( 'tenantid' , _tenantid );
                        }   // FALSE (We haven't a valid tenant ID.) block, if ( _tenantidSource !== SRC_IS_UNKNOWN )

                        if ( _useridSource !== SRC_IS_UNKNOWN )
                        {
                            if ( STTUserId !== _userid )
                            {
                                if ( STTUserId > MINIMUM_STT_ENTITY_ID )
                                {   // MINIMUM_STT_ENTITY_ID is a special ID that is reserved for use by the system, and may be associated with any entity.
                                    LLCommon.LogException ( strMethodName + ': Security Warning - User ID associated with lead ID ' + _leadid + ' = ' + STTUserId + ', while User ID associated with user login ' + _login + ' = ' + _userid + ' - Logged-in user is accessing a record that is associated with another user in the same SalesTalk domain.' );
                                }   // if ( STTUserId > MINIMUM_STT_ENTITY_ID )
                            }   // if ( STTUserId !== _userid )
                        }   // TRUE (We have a valid user ID.) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                        else
                        {
                            if ( _loginSource !== SRC_IS_UNKNOWN )
                            {
                                _fDomainAndTenantIDAreSafe  = LLCommon.SynchronizeLoginInfo ( _login,
                                                                                              SRC_IS_LOGIN_NAME_PER_URL );
                            }   // TRUE (anticipated outcome) block, if ( _loginSource !== SRC_IS_UNKNOWN )
                            else
                            {
                                _userid             = STTUserId;
                                _useridSource       = SRC_IS_LEAD_ID_PER_URL;
                                _login              = STTLoginName;
                                _loginSource        = SRC_IS_LEAD_ID_PER_URL;
                            }   // FALSE (unanticipated outcome) block, if ( _loginSource !== SRC_IS_UNKNOWN )

                            if ( this.DialerLogin.length === EMPTY_STRING_LENGTH )
                            {
                                this.DialerLogin    = _login;
                            }   // if ( LLCommon.DialerLogin.length === EMPTY_STRING_LENGTH )

                            sessionStorage.setItem ( 'userid' , _userid );
                            sessionStorage.setItem ( 'login'  , _login );
                        }   // FALSE (We haven't a valid user ID.) block, if ( _useridSource !== SRC_IS_UNKNOWN )
                    }   // TRUE (anticipated outcome) block, if ( strAjaxResult.indexOf ( this.PIPE_CHAR ) > this.INDEXOF_NOT_FOUND )
                    else
                    {
                        throw new Error ( 'ERROR in ' + strMethodName + ': ' + strBasicLeadInfo + ' for lead ID = ' + _leadid );
                    }   // FALSE (unanticipated outcome) block, if ( strAjaxResult.indexOf ( this.PIPE_CHAR ) > this.INDEXOF_NOT_FOUND )

                    if ( LLCommon.EnabledCRM === null )
                    {
                        LLCommon.EnabledCRM         = LLCommon.GetEnabledCrmInfo ( _tenantid ,
                                                                                   _domainid );
                    }   // if ( LLCommon.EnabledCRM === null )

                    if ( LLCommon.EntityType === null )
                    {
                        LLCommon.EvaluateEntityType ( );
                    }   // if ( LLCommon.EntityType === null )

                    _fDomainAndTenantIDAreSafe      = true;
                }   // if ( !_fDomainAndTenantIDAreSafe )
            }
            catch ( ex )
            {
                alert ( LLCommon.LogException ( ex ) + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + 'Please contact SalesTalk customer support for assistance.' );
            }
        }   // function LLCommon.ResolveDomainAndTenantIDs


        LLCommon.SearchNotesAndDisplayMatches = function ( pstrSearchFilterId , pstrNoteSearchResultId , pstrHighlightStyle , pstrCloseButtons )
        {
            /*
                ----------------------------------------------------------------
                Name:       SearchNotesAndDisplayMatches

                Goal:       Use the filter specified by pstrSearchFilterId to
                            search the UserNotes table rows connected to the
                            current lead for the text specified therein and show
                            the matching notes in the innerHTML of the DIVision
                            identified by element ID pstrNoteSearchResultId.

                Arguments:  pstrSearchFilterId      = This JavaScript String is
                                                      the ID of the text INPUT
                                                      element that contains the
                                                      search criteria.

                            pstrNoteSearchResultId  = This JavaScript String is
                                                      the ID of the hidden empty
                                                      DIV into which the routine
                                                      pours the search result.

                            pstrHighlightStyle      = This OPTIONAL JavaScript
                                                      String is the styling, if
                                                      any, to apply to each
                                                      occurrence of the keyword
                                                      string in the displayed
                                                      note text.

                            pstrCloseButtons        = When TRUE, meaning that it
                                                      is a String that is NOT
                                                      EMPTY, the String must be
                                                      valid HTML markup which is
                                                      injected at the top and
                                                      bottom of the output
                                                      DIVision.

                Returns:    Though its goal is to render a display, this routine
                            returns FALSE unless it throws an exception. In that
                            case, it returns TRUE, allowing the default events
                            to fire.

                Remarks:    Both pstrSearchFilterId and pstrNoteSearchResultId
                            are REQUIRED to be strings that ideentify HTML
                            elements of the appropriate type, which this routine
                            enforces.
                ----------------------------------------------------------------
            */

            const FIELD_COUNT_ALL_NOTES    = 4;
            const FIELD_COUNT_DOMAIN_NOTES = 8;

            const strMethodName            = LLCommon.GetNameOfCurrentFunction ( );

            console.log ( strMethodName + ' arguments: pstrSearchFilterId = ' + pstrSearchFilterId + ', pstrNoteSearchResultId = ' + pstrNoteSearchResultId + ', pstrHighlightStyle = ' + pstrHighlightStyle + ', pstrCloseButtons = ' + pstrCloseButtons );
            debugger;

            try
            {
                if ( LLCommon.IsString ( pstrSearchFilterId ) && LLCommon.IsString ( pstrNoteSearchResultId ) )
                {
                    const docSearchFilterText = document.getElementById ( pstrSearchFilterId );

                    if ( docSearchFilterText !== null && docSearchFilterText.nodeName === 'INPUT' && docSearchFilterText.type === 'text' && docSearchFilterText.value.length > EMPTY_STRING_LENGTH )
                    {
                        LLCommon.PutNotesSearchFilter ( docSearchFilterText.value ,
                                                        pstrCloseButtons === null ? NUMERIC_ZERO
                                                                                  : LLCommon.LeadId );
                        const docNoteSearchResult = document.getElementById ( pstrNoteSearchResultId );

                        //  ----------------------------------------------------
                        //  When pstrCloseButtons is undefined or anything but a
                        //  non-empty string, it evaluates as FALSE in a Boolean
                        //  context such as a ternary expression. This works
                        //  because the empty string is evaluated as falsy, thus
                        //  causing the first clause in the ternary expression
                        //  to evaluate to FALSE and short-circuit the IsString
                        //  test. Though it should evaluate the length of the
                        //  string, since the empty string is harmless, it is
                        //  irrelevant.
                        //
                        //  This routine leverages the fact that null evaluates
                        //  as falsy by passing in said value from SideMenuBar
                        //  to instruct this routine to send a LeadId of zero to
                        //  GetNotesList.
                        //  ----------------------------------------------------

                        const fUseingCloseButtons = pstrCloseButtons && LLCommon.IsString ( pstrCloseButtons ) ? true : false;
                        const strCloseButttonHTML = fUseingCloseButtons && pstrCloseButtons ? pstrCloseButtons : EMPTY_STRING;

                        if ( docNoteSearchResult !== null )
                        {
                            if ( docNoteSearchResult.nodeName === 'DIV' )
                            {
                                //  --------------------------------------------
                                //  Since GetNotesList ignores DomainId unless
                                //  LeadId is zero, coding a single call that
                                //  always passes DomainId is safe and simpler.
                                //  --------------------------------------------

                                const strNotesList = LLCommon.DoAjax ( 'GetNotesList' ,
                                                                       'GET' ,
                                                                       {
                                                                           'LeadId'          : pstrCloseButtons === null ? NUMERIC_ZERO : LLCommon.LeadId ,
                                                                           'UserId'          : LLCommon.UserId ,
                                                                           'tzOffsetMinutes' : ( new Date ( ) ).getTimezoneOffset ( ) ,
                                                                           'Limit'           : 10 ,
                                                                           'SearchMode'      : true ,
                                                                           'DomainId'        : LLCommon.DomainId
                                                                       } );

                                if ( strNotesList.length > EMPTY_STRING_LENGTH )
                                {
                                    var   strNotes2Show = fUseingCloseButtons ? strCloseButttonHTML + '<div class="STT_SelectedNotesView">' : strCloseButttonHTML;
                                    const aoNotesList   = strNotesList.split ( LOGICAL_NEGATE );
                                    const intNotesCount = aoNotesList.length;

                                    if ( intNotesCount > NUMERIC_ZERO )
                                    {
                                        LLCommon.Trace ( LLCommon_SCRIPTSOURCE + SPACE_CHARACTER + strMethodName + ': Lead ID = ' + LLCommon.LeadId + ', Notes matching filter = "' + pstrSearchFilterId + '", Count = ' + intNotesCount );

                                        // Prepare the regular expression with global and case-insensitive flags.
                                        let rxpSearchPattern = new RegExp ( docSearchFilterText.value, 'gi' );
                                        // Perform the replacement on each note body in turn.

                                        for ( var intNoteIndex = ARRAY_FIRST_ELEMENT;
                                                  intNoteIndex < intNotesCount;
                                                  intNoteIndex++ )
                                        {
                                            var astrNotesInfo = aoNotesList [ intNoteIndex ].split ( PIPE_CHAR_SPLIT_MATCH );

                                            if ( astrNotesInfo.length === FIELD_COUNT_ALL_NOTES || astrNotesInfo.length === FIELD_COUNT_DOMAIN_NOTES )
                                            {   // Length = FIELD_COUNT_ALL_NOTES (4) for searche against one lead. Length = FIELD_COUNT_DOMAIN_NOTES (8) for search against one domain.
                                                switch ( astrNotesInfo.length )
                                                {
                                                    case FIELD_COUNT_ALL_NOTES:
                                                        strNotes2Show += '<div class="STT_FilteredNotes_Break">' + astrNotesInfo [ ARRAY_SECOND_ELEMENT ] + '</div>';
                                                        break;
                                                    case FIELD_COUNT_DOMAIN_NOTES:
                                                        let strCompanyNameToken = astrNotesInfo [ ARRAY_EIGHTH_ELEMENT ].length > EMPTY_STRING_LENGTH ? ', of ' + astrNotesInfo [ ARRAY_EIGHTH_ELEMENT ] : EMPTY_STRING;
                                                        strNotes2Show += '<div class="STT_FilteredNotes_Break">' + astrNotesInfo [ ARRAY_SECOND_ELEMENT ] + ' Contact: ' + astrNotesInfo [ ARRAY_SIXTH_ELEMENT ] + SPACE_CHARACTER + astrNotesInfo [ ARRAY_SEVENTH_ELEMENT ] + strCompanyNameToken + '</div>';
                                                        break;
                                                }   // switch ( astrNotesInfo.length )

                                                //  --------------------------------
                                                //  Since CSS has taken a notion to
                                                //  ignore the !important flag, in
                                                //  deference to the styling of the
                                                //  paragraph tag, we force its hand
                                                //  by applying the STT_NotesText
                                                //  selector directly to the P tag.
                                                //
                                                //  To prevent replacement of markup
                                                //  that happens to contain a search
                                                //  term, replaecments are applied
                                                //  to each note body in turn, and
                                                //  the final two replacements, one
                                                //  of which injects a CSS selector,
                                                //  are deferred until the end.
                                                //  --------------------------------

                                                let strThisNoteTransformed1 = astrNotesInfo [ ARRAY_FOURTH_ELEMENT ].replaceAll ( BELL_CONTROL_CODE , PIPE_CHAR );
                                                let strThisNoteTransformed2 = LLCommon.IsString ( pstrHighlightStyle ) && pstrHighlightStyle.length > EMPTY_STRING_LENGTH && docSearchFilterText.value.length > EMPTY_STRING_LENGTH
                                                                              ? strThisNoteTransformed1 = strThisNoteTransformed1.replace ( rxpSearchPattern, ( match ) => {
                                                                                    return `<span style="${pstrHighlightStyle}">${match}</span>`;
                                                                                })
                                                                              : strThisNoteTransformed1;
                                                strNotes2Show += '<div class="STT_NotesText">' + strThisNoteTransformed2.replaceAll ( '<p>' , '<p class="STT_NotesText">' ).replaceAll ( '<br>' , '<br><br>' ) + '</div>';
                                            }   // TRUE (anticipated outcome) block, if ( astrNotesInfo.length === FIELD_COUNT_ALL_NOTES || astrNotesInfo.length === FIELD_COUNT_DOMAIN_NOTES )
                                            else
                                            {
                                                throw new Error ( strMethodName + ': Note item at position ' + LLCommon.OrdinalFromIndex ( intNoteIndex ) + ' was expected to split into 4 substrings. Actual split count = ' + astrNotesInfo.length );
                                            }   // FALSE (unanticipated outcome) block, if ( astrNotesInfo.length === FIELD_COUNT_ALL_NOTES || astrNotesInfo.length === FIELD_COUNT_DOMAIN_NOTES )
                                        }   // for ( var intNoteIndex = ARRAY_FIRST_ELEMENT; intNoteIndex < intNotesCount; intNoteIndex++ )

                                        strNotes2Show += ( ( fUseingCloseButtons ? '</div>' : EMPTY_STRING ) + strCloseButttonHTML );
                                        docNoteSearchResult.innerHTML = strNotes2Show;
                                        LLCommon.ShowOrHideElement ( docNoteSearchResult ,
                                                                     LLCommon.ELEMENT_SHOW );
                                        LLCommon.ShowOrHideElement ( 'TranscriptContainer' ,
                                                                     LLCommon.ELEMENT_HIDE );

                                        if ( astrNotesInfo.length === FIELD_COUNT_ALL_NOTES )
                                        {   // Do this last one ONLY for searches of notes attached to ONE lead.
                                            LLCommon.ShowOrHideElement ( 'wrapper' ,
                                                                         LLCommon.ELEMENT_HIDE );
                                        }   // if ( astrNotesInfo.length === FIELD_COUNT_ALL_NOTES )
                                    }   // TRUE (The result set contains at least one note.) block, if ( intNotesCount > NUMERIC_ZERO )
                                    else
                                    {
                                        LLCommon.Trace ( LLCommon_SCRIPTSOURCE + SPACE_CHARACTER + strMethodName + ': Lead ID = ' + _LeadLifeJSHelpers.STTLeadId + ', Notes matching filter = "' + pstrSearchFilterId + '", Count = 0' );
                                    }   // FALSE (The result set is empty.) block, if ( intNotesCount > NUMERIC_ZERO )
                                }   // TRUE (anticipated outcome) block, if ( intNotesCount > NUMERIC_ZERO )
                                else
                                {
                                    LLCommon.Trace ( 'SalesTalk internal API ' + strMethodName + ' found no notes for lead ID ' + _LeadLifeJSHelpers.STTLeadId );
                                }   // FALSE (unanticipated outcome) block, if ( intNotesCount > NUMERIC_ZERO )
                            }   // TRUE (anticipated outcome) block, if ( docNoteSearchResult.nodeName === 'DIV' )
                            else
                            {
                                throw new Error ( strMethodName + ': Parameter pstrNoteSearchResultId value "' + pstrNoteSearchResultId + '" must be the ID of a valid DIV element. Instead, its nodeName is ' + docNoteSearchResult.nodeName + '.' );
                            }   // FALSE (unanticipated outcome) block, if ( docNoteSearchResult.nodeName === 'DIV' )
                        }   // TRUE (anticipated outcome) block, if ( docNoteSearchResult !== null )
                        else
                        {
                            throw new Error ( strMethodName + ': Parameter pstrNoteSearchResultId value "' + pstrNoteSearchResultId + '" must be the ID of a valid HTML element.' );
                        }   // FALSE (unanticipated outcome) block, if ( docNoteSearchResult !== null )
                    }   // TRUE (anticipated outcome) block, if ( docSearchFilterText !== null && docSearchFilterText.nodeName === 'INPUT' && docSearchFilterText.type === 'text' && docSearchFilterText.value.length > EMPTY_STRING_LENGTH )
                    else
                    {
                        throw new Error ( strMethodName + ': Parameter pstrSearchFilterId, "' + pstrSearchFilterId + '" must be the ID of a valid test INPUT element.' + ( docSearchFilterText === null ? 'No such element exists in the current documeent.' : 'Though a document with the expected ID exists, its nodeName is ' + docSearchFilterText.nodeName + ' and its type is ' + docSearchFilterText.type + '.' + ( docSearchFilterText.nodeName === 'INPUT' && docSearchFilterText.type === 'text' ? ' Though its nodeName and type are valid, its value is the empty string.' : EMPTY_STRING ) ) );
                    }   // FALSE (unanticipated outcome) block, if ( docSearchFilterText !== null && docSearchFilterText.nodeName === 'INPUT' && docSearchFilterText.type === 'text' && docSearchFilterText.value.length > EMPTY_STRING_LENGTH )
                }   //  TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrSearchFilterId ) && LLCommon.IsString ( pstrNoteSearchResultId ) )
                else
                {
                    throw new Error ( strMethodName + ': Either or both of input parameters pstrSearchFilterId and pstrNoteSearchResultId is something other than a String. The type of parameter pstrSearchFilterId is ' + ( pstrSearchFilterId === null ? 'NULL' : typeof pstrSearchFilterId ) + '. The type of parameter pstrNoteSearchResultId is ' + ( pstrNoteSearchResultId === null ? 'NULL' : typeof pstrNoteSearchResultId ) );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrSearchFilterId ) && LLCommon.IsString ( pstrNoteSearchResultId ) )

                return false;   // Suppress default event handlers.
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
                return true;    // Allow default event handlers to run.
            }
        }   // LLCommon.SearchNotesAndDisplayMatches


        LLCommon.SetInputValuesFromStringifiedArray = function ( pstrFieldValues )
        {
            /*
                ----------------------------------------------------------------
                Function Name:      SetInputValuesFromStringifiedArray

                Method Goal:        Apply one of a pair of CSS selectors,
                                    removing its opposite, to Show or Hide an
                                    element.

                Input:              pstrFieldValues = This object is expected to
                                                      be a string representation
                                                      of an array of JavaScript
                                                      objects.

                Output:             The return value is the count of objects in
                                    the array that correspond to fields in the
                                    active document.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            var   rintFieldCount        = NUMERIC_ZERO;

            debugger;

            try
            {
                if ( ( !Object.is ( pstrFieldValues , undefined ) ) && ( pstrFieldValues !== null ) && ( LLCommon.IsString ( pstrFieldValues ) ) )
                {
                    const aoFields      = JSON.parse ( pstrFieldValues );

                    if ( ( !Object.is ( aoFields , undefined ) ) && ( aoFields !== null ) && ( Array.isArray ( aoFields ) ) && ( aoFields.length > ARRAY_IS_EMPTY ) )
                    {
                        for ( var intCurrFld = ARRAY_FIRST_ELEMENT;
                                  intCurrFld < aoFields.length;
                                  intCurrFld++ )
                        {
                            if ( aoFields [ intCurrFld ].hasOwnProperty ( 'FieldId' ) && aoFields [ intCurrFld ].hasOwnProperty ( 'FieldValue' ) )
                            {
                                document.getElementById ( aoFields [ intCurrFld ].FieldId ).value = aoFields [ intCurrFld ].FieldValue;
                                rintFieldCount++;
                            }   // TRUE (anticipated outcome) block, if ( aoFields [ intCurrFld ].hasOwnProperty ( 'FieldId' ) && aoFields [ intCurrFld ].hasOwnProperty ( 'FieldValue' ) )
                            else
                            {
                                throw new Error ( strMethodName + ': The field value at array index ' + intCurrFld + ' is missing one or more of its required properties, FieldId and FieldValue.' );
                            }   // FALSE (unanticipated outcome) block, if ( aoFields [ intCurrFld ].hasOwnProperty ( 'FieldId' ) && aoFields [ intCurrFld ].hasOwnProperty ( 'FieldValue' ) )
                        }   // for ( var intCurrFld = ARRAY_FIRST_ELEMENT; intCurrFld < aoFields.length; intCurrFld++ )
                    }   // TRUE (anticipated outcome) block, if ( ( !Object.is ( aoFields , undefined ) ) && ( aoFields !== null ) && ( Array.isArray ( aoFields ) ) && ( aoFields.length > ARRAY_IS_EMPTY ) )
                    else
                    {
                        throw new Error ( strMethodName + ': Input parameter pstrFieldValues MUST convert to an array of JavaScript Objects.' );
                    }   // FALSE (unanticipated outcome) block, if ( ( !Object.is ( aoFields , undefined ) ) && ( aoFields !== null ) && ( Array.isArray ( aoFields ) ) && ( aoFields.length > ARRAY_IS_EMPTY ) )
                }   // TRUE (anticipated outcome) block, if ( ( !Object.is ( pstrFieldValues , undefined ) ) && ( pstrFieldValues !== null ) && ( LLCommon.IsString ( pstrFieldValues ) ) )
                else
                {
                    throw new Error ( strMethodName + ': Input parameter pstrFieldValues MUST be a string.' );
                }   // FALSE (unanticipated outcome) block, if ( ( !Object.is ( pstrFieldValues , undefined ) ) && ( pstrFieldValues !== null ) && ( LLCommon.IsString ( pstrFieldValues ) ) )
            }
            catch ( ex )
            {
                LLCommon.LogException ( ex );
            }

            return rintFieldCount
        }   // LLCommon.SetInputValuesFromStringifiedArray


        /**
         * Displays a confirmation dialog and either:
         * - Executes provided callbacks (if any), or
         * - Returns a boolean indicating user's choice.
         * @param {string} pstrMessage - The prompt to display.
         * @param {boolean} pfUseYesNo - TRUE for Yes/No buttons, FALSE for OK/Cancel.
         * @param {function=} pfnOnConfirm - Optional callback if user confirms.
         * @param {function=} pfnOnCancel - Optional callback if user cancels.
         * @returns {Promise<boolean>} - Resolves to TRUE if confirmed, FALSE otherwise.
         */
        LLCommon.ConfirmDefaultingToNo = async function ( pstrMessage, pfUseYesNo, pfnOnConfirm, pfnOnCancel )
        {
            /**
             * Displays a Bootbox confirmation dialog with either OK/Cancel or Yes/No buttons.
             * Resolves to TRUE if user confirms, FALSE otherwise.
             * @param {string} pstrPromptMessage - The dialog message to display.
             * @param {boolean} pfUseYesNo - If TRUE, uses 'Yes'/'No' buttons; otherwise 'OK'/'Cancel'.
             * @returns {Promise<boolean>}
             */
            function ShowConfirmationDialog ( pstrPromptMessage, pfUseYesNo )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                if ( typeof bootbox === 'undefined' )
                {
                    console.warn ( 'Bootbox.js is required but not loaded.' );
                    return Promise.resolve ( false );
                }   // if ( typeof bootbox === 'undefined' )

                return new Promise ( function ( resolve )
                {
                    bootbox.dialog({
                        message : pstrPromptMessage,
                        buttons :
                        {
                            confirm :
                            {
                                label     : pfUseYesNo ? 'Yes' : 'OK',
                                className : 'btn-secondary',
                                callback  : function ( )
                                {
                                    resolve ( true );
                                }   // end of confirm button callback function
                            },  // end of confirm button declaration
                            cancel :
                            {
                                label     : pfUseYesNo ? 'No' : 'Cancel',
                                className : 'btn-primary',
                                callback  : function ( )
                                {
                                    resolve ( false );
                                }   // end of cancel button callback function
                            }   // end of cancel button declaration
                        }   // end of buttons object
                    }); // bootbox.dialog
                }); // new Promise ( function ( resolve )
            }   // Private function ShowConfirmationDialog


            const fUserConfirmed = await ShowConfirmationDialog ( pstrMessage , pfUseYesNo );

            if ( fUserConfirmed )
            {
                pfnOnConfirm?.( );      // Executes if user clicks OK or Yes
            } else {
                pfnOnCancel?.( );       // Executes if user clicks Cancel or No
            }

            return fUserConfirmed;
        };  // LLCommon.ConfirmDefaultingToNo method


        LLCommon.ShowCRMEntityMessages = function ( pstrSyCRMLeadOrContact )
        {
            /*
                ----------------------------------------------------------------
                Function Name:      ShowCRMEntityMessages

                Method Goal:        Display message(s) as indicated by the value
                                    of the pstrSyCRMLeadOrContact string.

                Input:              pstrSyCRMLeadOrContact = This string is the
                                                             SyCRMLeadOrContact
                                                             value of the active
                                                             Lead record.

                Output:             This function has only side effects. Its
                                    return value is undefined.

                Remarks:            The original intent of this function was to show
                                    a special message when the active record was
                                    associated with a specific CRM entity, hence the
                                    name.

                                    The current implementation instead hides the
                                    button in question. The default block shows it,
                                    in case a previous pass through this routine hid
                                    it.
                --------------------------------------------------------------------
            */

            const ENTITY_10_ALERT_ID        = 'EntityId10Alert';                // This is declared because it is used twice.

            const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            if ( LLCommon.IsString ( pstrSyCRMLeadOrContact ) && pstrSyCRMLeadOrContact.length > EMPTY_STRING_LENGTH )
            {
                switch ( pstrSyCRMLeadOrContact.toLowerCase ( ) )
                {
                    case 'wa-propertysearchcriteria':
                        LLCommon.ShowOrHideElement ( 'DoWords2Notes' ,
                                                     LLCommon.ELEMENT_HIDE );
                        document.getElementById ( 'DoWords2Notes' ).style.setProperty ( 'display' , 'none' , 'important' );
                        break;
                    default:
                        if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                        {   // Unless the entity type is 12 (WA-Task), explicitly set the display style attibute to block.
                            document.getElementById ( 'DoWords2Notes' ).style.setProperty ( 'display' , 'block' , 'important' );
                        }   // if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                }   // switch ( pstrSyCRMLeadOrContact.toLowerCase ( ) )
            }   // if ( LLCommon.IsString ( pstrSyCRMLeadOrContact ) && pstrSyCRMLeadOrContact.length > EMPTY_STRING_LENGTH )
        }   // LLCommon.ShowCRMEntityMessages


        LLCommon.ShowOrHideElement = function ( pdocElement , pfShowIt )
        {
            /*
                ----------------------------------------------------------------
                Function Name:      ShowOrHideElement

                Method Goal:        Apply one of a pair of CSS selectors,
                                    removing its opposite, to Show or Hide an
                                    element.

                Maintenance Note:   Any function in another script that calls
                                    this function must list the LLCommon object
                                    as a dependency.

                Input:              pdocElement = This may be either a reference
                                                  to a DOM element or its ID as
                                                  a string.

                                    pfShowIt    = This is either Boolean True,
                                                  or a truthy value, to cause
                                                  element pdocElement to be
                                                  shown, or False, or a falsy
                                                  value, to cause element
                                                  pdocElement to be hidden.

                Output:             The return value is the classList attribute
                                    of the element as it stands when it returns.
                                    The classList attribute is a NodeList.

                                    When pdocElement is either undefined or null,
                                    the return value is a null NodeList.

                Remarks:            This convenience function works by applying
                                    one of a pair of selectors, STT_HideElement,
                                    removing its inverse, STT_ShowElement, or
                                    vice versa, as indicated by the value of its
                                    pfShowIt argument.

                                    This method of applying classes preserves
                                    other selectors by taking advantage of the
                                    fact that the classList attribute of every
                                    HTML element is an array that implements an
                                    add method and a remove method.

                Algorithm:          1)  If pdocElement is undefined or a null
                                        reference, do nothing, returning null.

                                    2)  If pdocElement is a string, treat it as
                                        the ID of an element and get a reference
                                        to it.

                                    3)  If pdocElement is not a string and it ia
                                        null, do nothing, returning null.

                                    4)  Treating pfShowIt as a Boolean, evaluate
                                        its truthiness.

                                    5)  If pfShowIt evaluates to True, make
                                        element pdocElement visible by removing
                                        CSS selector STT_HideElement and adding
                                        CSS selector STT_ShowElement to its
                                        classList array.

                                    6)  If pfShowIt evaluates to False, make
                                        element pdocElement invisible by
                                        removing CSS selector STT_ShowElement
                                        and adding CSS selector STT_HideElement
                                        to its classList array.

                Reference:          Add a CSS class to an HTML element with JavaScript/jQuery
                                    https://www.techiedelight.com/add-css-class-to-html-element-javascript
                --------------------------------------------------------------------
            */

            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            if ( ( !Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
            {   // When pdocElement is either undefined or null, there is nothing to do.
                const docThisElement = LLCommon.IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

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


        LLCommon.ShowResetAlert = function ( pstrSyCRMLeadOrContact )
        {
             if ( LLCommon.EnabledCRM != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != LLCommon.TOKEN_NOCRM )
             {
                 switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                 {
                     case 'WA-':
                         if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                         {
                             alert ( 'Close this page, close the Update pop-up if shown, and select a different record, or refresh the Wise Agent main Contact page.' , 'native' );
                         }   // TRUE (default outcome) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                         else
                         {
                             alert ( 'When this page closes, please select a different record, or refresh the Wise Agent main Contact page.' , 'native' );
                             window.close ( );
                         }   // FALSE (alternate outcome) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                         break;
                     case 'BH-':
                         alert ( 'Close this page, and select a different record, or use the refresh icon on the Bullhorn page to refresh it.' , 'native' );
                         break;
                     default:
                         alert ( 'Close this page, and select a different record OR refresh your ' + LLCommon.EnabledCRM.SysCRMLeadOrContact.CrmName + ' page to see the updates.' , 'native' );
                         break;
                 }   // switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
             }   // TRUE (There is an external CRM behind this page.) block, if ( LLCommon.EnabledCRM != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != LLCommon.TOKEN_NOCRM )
             else
             {   // Rather than determine why the opening IF statement failed, the ELSE block can handle it.
                 if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                 {
                     alert ( 'Close this page, and select a different record, or use the refresh icon in the upper right corner to refresh it.' , 'native' );
                 }   // TRUE (default outcome) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
                 else
                 {
                     alert ( 'When this page closes, please select a different record, or refresh the Wise Agent main Contact page.' , 'native' );
                     window.close ( );
                 }   // FALSE (alternate outcome) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId !== 12 )
             }   // FALSE (SalesTalk IS the CRM.) block, if ( LLCommon.EnabledCRM != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != null && LLCommon.EnabledCRM.SysCRMLeadOrContact != LLCommon.TOKEN_NOCRM )
         }   // LLCommon.ShowResetAlert


         LLCommon.Sleep = function ( pdocSleepTime , pdocRemainingTime )
         {
            /*
                ----------------------------------------------------------------
                Method Name:        Sleep

                Method Goal:        Delay the main thread for a specified time.

                Input:              pdocSleepTime     = This String represents
                                                        the name of the document
                                                        element that stores the
                                                        amount of time expressed
                                                        in seconds to sleep.

                                    pdocRemainingTime = This String represents
                                                        the name of the document
                                                        element that displays a
                                                        progress report once per
                                                        second during the sleep.

                Output:             If it succeeds, this function returns true.
                                    Otherwise, it returns false.

                Remarks:            The real work happens in LLCommon.GoToSleep,
                                    which expects an integer and an optional
                                    element through which to report.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            debugger;

            const docSleepTime          = document.getElementById ( pdocSleepTime );
            const docRemainingTime      = document.getElementById ( pdocRemainingTime );

            if ( docSleepTime !== null && docRemainingTime !== null )
            {
                if ( docSleepTime.value.length > EMPTY_STRING_LENGTH )
                {
                    //  ----------------------------------------------------
                    //  Having learned why Number.isInteger failed in this
                    //  context, we can substitute LLCommon.IsValidInteger
                    //  and move parseInt into the TRUE block of the IF.
                    //  ----------------------------------------------------

                    let intSeconds;

                    if ( LLCommon.IsValidInteger ( docSleepTime.value ) )
                    {
                        const intSeconds = parseInt ( docSleepTime.value , 10 )

                        if ( intSeconds > NUMERIC_ZERO )
                        {
                            console.log ( 'Total Time to Sleep = ' + intSeconds );
                            LLCommon.SleepNow ( intSeconds,
                                                docRemainingTime );
                            debugger;
                            ProgressReport ( docRemainingTime , 'WAKE UP!' );
                            return true;
                        }   // TRUE (anticipated outcome) block, if ( intSeconds > NUMERIC_ZERO )
                        else
                        {
                            console.log ( 'Total Time to Sleep must be greater than zero. Specified value = ' + intSeconds );
                        }   // FALSE (unanticipated outcome) block, if ( intSeconds > NUMERIC_ZERO )
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( docSleepTime.value ) )
                    else
                    {
                        console.log ( 'Total Time to Sleep must be an integer. The specified value, "' + docSleepTime.value + '", is not.' );
                    }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( docSleepTime.value ) )
                }   // TRUE (anticipated outcome) block, EMPTY_STRING_LENGTH
                else
                {
                    console.log ( 'Total Time to Sleep is unspecified. Please enter a valid time.' );
                }   // FALSE (unanticipated outcome) block, if ( docSleepTime.value.length > EMPTY_STRING_LENGTH )
            }   // TRUE (anticipated outcome) block, if ( docSleepTime !== null && docRemainingTime !== null )
            else
            {
                if ( docSleepTime === null && docRemainingTime === null )
                {
                    console.log ( 'Error: both docSleepTime and docRemainingTime elements are absent.' );
                }   // TRUE (Both elements are null.) block, if ( docSleepTime === null && docRemainingTime === null )
                else
                {   // One or the other is. Figure out which one.
                    if ( docSleepTime == null )
                    {
                        console.log ( 'Error: the docSleepTime element is absent.' );
                    }   // TRUE block, if ( docSleepTime == null )
                    else
                    {
                        console.log ( 'Error: the docRemainingTime element is absent.' );
                    }   // FALSE block, if ( docSleepTime == null )
                }   // FALSE (Only one of the two elements is null.) block, if ( docSleepTime === null && docRemainingTime === null )
            }   // FALSE (unanticipated outcome) block, if ( docSleepTime !== null && docRemainingTime !== null )

            return false;
         }   // function Sleep

         LLCommon.SleepNow = function ( pintSeconds, pdocRemainingTime )
         {
            /*
                ----------------------------------------------------------------
                Method Name:        SleepNow

                Method Goal:        Given a time expressed in seconds and an
                                    optional element that can display text, wait
                                    the specified number of seconds, optionally
                                    reporting once per second through the text
                                    element.

                Input:              pintSeconds       = This integer specifies
                                                        the number of seconds to
                                                        sleep.

                                    pdocRemainingTime = Unless null, this object
                                                        identifies an element of
                                                        a type that can display
                                                        text, such as an INPUT
                                                        element of type text or
                                                        a paragraph, span, or
                                                        division tag through
                                                        which a progress report
                                                        is rendered each second.

                Output:             This function has no return value.

                Remarks:            Unlike its predecessor, this routine uses an
                                    internal variable to maintain its state.
                                    This is possible because the routine that
                                    runs the timer is defined within it as a
                                    local variable.
                ----------------------------------------------------------------
            */

            function Blink ( pintSeconds , pintRemainingTime , pdocRemainingTime )
            {
                /*
                    ------------------------------------------------------------
                    Function Name:      Blink

                    Function Goal:      Append to the console log and optionally
                                        update the text in a document element.

                    Input:              pintSeconds       = This integer specifies
                                                            the total number of
                                                            seconds to sleep,
                                                            which goes into the
                                                            final message.

                                        pintRemainingTime =

                                        pdocRemainingTime = Unless null, this object
                                                            identifies an element of
                                                            a type that can display
                                                            text, such as an INPUT
                                                            element of type text or
                                                            a paragraph, span, or
                                                            division tag through
                                                            which a progress report
                                                            is rendered each second.

                    Output:             This function has no return value.

                    Remarks:            Unlike its predecessor, this routine uses an
                                        internal variable to maintain its state.
                                        This is possible because the routine that
                                        runs the timer is defined within it as a
                                        local variable.
                    ----------------------------------------------------------------
                */

                const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

                var strMessage          = 'Time remaining (seconds) = ' + pintRemainingTime;
                console.log ( strMessage );

                debugger;

                if ( pdocRemainingTime !== null )
                {
                    LLCommon.ReportThroughAnyTextElement ( pdocRemainingTime,
                                                           strMessage );
                }   // if ( pdocRemainingTime !== null )

                if ( pintRemainingTime <= 0 )
                {
                    debugger;

                    strMessage          = 'Timer set for ' + pintSeconds + ' seconds has expired.';
                    console.log ( strMessage );

                    if ( pdocRemainingTime !== null )
                    {
                        LLCommon.ReportThroughAnyTextElement ( pdocRemainingTime,
                                                               strMessage );
                    }   // if ( pdocRemainingTime !== null )
                }   // FALSE block, if ( window.recordingTimer <= 0 )
            }   // function Blink

            function Blink2 ( ) { console.log ( 'I am the body of a setTimeout function.'); }


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            for ( var intRemainingSeconds = pintSeconds;
                      intRemainingSeconds > NUMERIC_ZERO;
                      intRemainingSeconds-- )
            {
                debugger;
                setTimeout ( Blink2 , 1000 );
                debugger;
                Blink ( pintSeconds ,
                        intRemainingSeconds ,
                        pdocRemainingTime );
            }   // for ( var intRemainingSeconds = pintSeconds; intRemainingSeconds > NUMERIC_ZERO; intRemainingSeconds-- )

            debugger;
            return true;
        }   // LLCommon.SleepNow


        /**
         * Sort an array of objects by one or more keys using deterministic,
         * type-aware comparison rules. Supports numbers, strings, dates,
         * null/undefined, ascending/descending order, and optional
         * case-insensitive string comparison.
         *
         * See companion script `LLCommon_sortBy_Tests.JS`, a unit test harness
         * that can be run in a Chrome Developer Tools console once the LLCommon
         * object has sprung into existence. Copy its contents into the console
         * and press ENTER to execute the unit tests, which report via the
         * console.
         *
         * Keys may be specified as:
         *   - a single string: "lastName"
         *   - an array of strings: ["lastName", "firstName"]
         *   - an array of objects:
         *       [
         *         { key: "lastName", direction: "asc", ignoreCase: true },
         *         { key: "age", direction: "desc" }
         *       ]
         *
         * Each key object supports:
         *   - key:         (string)  property name to sort by
         *   - direction:   (string)  "asc" (default) or "desc"
         *   - ignoreCase:  (boolean) case-insensitive string comparison (default: false)
         *
         * Comparison rules:
         *   - null/undefined always sort before non-null values
         *   - numbers are compared numerically
         *   - dates are compared by timestamp
         *   - strings use localeCompare; case-insensitive mode preserves accents
         *   - fallback uses < and > for deterministic ordering
         *
         * @param {Array<object>} paoArray
         *        The array to sort. Must be an Array.
         *
         * @param {string|Array<string|object>} poKeys
         *        Sorting criteria. See above for accepted formats.
         *
         * @returns {Array<object>}
         *        The same array instance, sorted in place.
         *
         * @throws {TypeError}
         *         If `paoArray` is not an Array.
         *
         * @throws {Error}
         *         If `poKeys` is not a valid keys specification.
         *
         * @example
         * // Sort by a single key (ascending)
         * LLCommon.sortBy(people, "lastName");
         *
         * @example
         * // Sort by a single key (descending)
         * LLCommon.sortBy(people, [{ key: "age", direction: "desc" }]);
         *
         * @example
         * // Multi-key sort: lastName, then firstName
         * LLCommon.sortBy(people, ["lastName", "firstName"]);
         *
         * @example
         * // Case-insensitive string sorting
         * LLCommon.sortBy(people, [
         *   { key: "lastName", ignoreCase: true },
         *   { key: "firstName", ignoreCase: true }
         * ]);
         *
         * @example
         * // Mixed types: status (case-insensitive), priority (desc), createdDate (asc)
         * LLCommon.sortBy(records, [
         *   { key: "status", ignoreCase: true },
         *   { key: "priority", direction: "desc" },
         *   { key: "createdDate" }
         * ]);
         */
        LLCommon.sortBy = function ( paoArray, poKeys )
        {
            const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

            if ( !Array.isArray ( paoArray ) )
            {
                const actualType = paoArray === null ? 'null' : typeof paoArray;
                throw new TypeError ( strMethodName + ': expected an Array, got ' + actualType );
            }   // if ( !Array.isArray ( paoArray ) )

            //  ----------------------------------------------------------------
            //  The main function resumes at the bottom.
            //  ----------------------------------------------------------------

            function normalizeKeys ( poKeys )
            {
                //  ------------------------------------------------------------
                //  A default assending sort by one key can be specified as a
                //  string, which is converted into an array of exactly one full
                //  sortKey object.
                //  ------------------------------------------------------------

                if ( typeof poKeys === 'string' )
                {
                    return [ { key: poKeys, direction: 'asc', ignoreCase: false } ];
                }   // if ( typeof poKeys === 'string' )

                if ( Array.isArray ( poKeys ) )
                {
                    return poKeys.map ( k =>
                    {
                        if ( typeof k === 'string' )
                        {   // Coerce a bare string into a full sortKey object.
                            return { key: k, direction: 'asc', ignoreCase: false };
                        }   // if ( typeof k === 'string' )

                        //  ----------------------------------------------------
                        //  Otherwise, treat the object as a full sortKey object
                        //  that may be missing one or more of its properties.
                        //  ----------------------------------------------------

                        return {
                            key: k.key,
                            direction: (k.direction || 'asc').toLowerCase(),
                            ignoreCase: !!k.ignoreCase
                        };
                    });
                }   // if ( Array.isArray ( poKeys ) )

                throw new Error ( strMethodName + ': invalid keys specification' );
            }   // private function normalizeKeys

            function compareValues ( a, b, ignoreCase )
            {
                //  ------------------------------------------------------------
                //  Handle the degenerate cases when one or both comparands is a
                //  null.
                //  ------------------------------------------------------------

                const aNull = a == null;
                const bNull = b == null;

                if ( aNull && bNull ) return 0;
                if ( aNull ) return -1;
                if ( bNull ) return 1;

                if ( a instanceof Date && b instanceof Date )
                {   // Dates compare by UTC timestamp (getTime), avoiding timezone/DST issues.
                    return a.getTime ( ) - b.getTime ( );
                }   // if ( a instanceof Date && b instanceof Date )

                if ( typeof a === 'number' && typeof b === 'number' )
                {
                    return a - b;
                }   // if ( typeof a === 'number' && typeof b === 'number' )

                if ( typeof a === 'string' && typeof b === 'string' )
                {
                    if ( ignoreCase )
                    {   // Accents and other diacritical marks participate fully in the sort.
                        return a.localeCompare ( b, 'en-US', { sensitivity: 'accent' } );
                    }   // if ( ignoreCase )

                    //  --------------------------------------------------------
                    //  Case-sensitive string comparison
                    //
                    //  We explicitly request the locale "en-US-u-kf-upper"
                    //  instead of relying on the browser's default locale. This
                    //  is required because:
                    //
                    //  1. localeCompare() is locale-dependent. If the browser
                    //     lacks full ICU data for "en-US", or if the OS/browser
                    //     locale uses a different collation table, then "Smith"
                    //     and "smith" may sort in the opposite order.
                    //
                    //  2. Some environments (notably certain Windows builds,
                    //     corporate-managed images, and Chromium variants with
                    //     reduced ICU data) silently fall back to a locale
                    //     where lowercase letters sort BEFORE uppercase
                    //     letters, producing: ["smith", "Smith"].
                    //
                    //  3. The Unicode extension "u-kf-upper" forces *uppercase-
                    //     first* ordering, independent of the system locale.
                    //     This guarantees deterministic, cross-browser, cross-
                    //     machine behavior.
                    //
                    //  4. Using "sensitivity: 'case'" preserves accent
                    //     distinctions while still treating case as a
                    //     significant difference.
                    //
                    //  In short: "en-US-u-kf-upper" is the only ICU locale
                    //  string that reliably enforces the intended ordering:
                    //  "Smith" < "smith"  on all platforms.
                    //  --------------------------------------------------------

                    return a.localeCompare ( b, 'en-US-u-kf-upper', { sensitivity: "case" } );
                }   // if ( typeof a === 'string' && typeof b === 'string' )

                if ( a < b ) return -1;
                if ( a > b ) return 1;
                return 0;
            }   // private function compareValues

            function compareByCriteria ( a, b, criteria )
            {
                for ( const { key, direction, ignoreCase } of criteria )
                {
                    const factor = direction === 'desc' ? -1 : 1;

                    const av = a [ key ];
                    const bv = b [ key ];

                    const result = compareValues ( av, bv, ignoreCase );

                    if ( result !== 0 )
                    {
                        return result * factor;
                    }   // if ( result !== 0 )
                }   // for ( const { key, direction, ignoreCase } of criteria )

                return 0;
            } // private function compareByCriteria ( a, b, criteria )

            //  ----------------------------------------------------------------
            //  The main function resumes here.
            //  ----------------------------------------------------------------

            const criteria = normalizeKeys ( poKeys );
            return paoArray.sort ( ( a, b ) => compareByCriteria ( a, b, criteria ) );
        };  // LLCommon.sortBy

        /**
         * Looks up an object in `paoLikeJSObjects` where `pstrMatchPropName === poMatchValue`
         * and returns the value of `pstrReturnPropName`.
         *
         * Throws if:
         *   - paoLikeJSObjects is not an array
         *   - any element lacks pstrMatchPropName or pstrReturnPropName
         *
         * @param {Array<Object>} paoLikeJSObjects
         * @param {string} pstrMatchPropName
         * @param {*} poMatchValue
         * @param {string} pstrReturnPropName
         * @returns {*}
         * @throws {TypeError}
         */
        LLCommon.strictLookup = function ( paoLikeJSObjects, pstrMatchPropName, poMatchValue, pstrReturnPropName )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            if ( !Array.isArray ( paoLikeJSObjects ) )
            {
                throw new TypeError ( `${strMethodName}: expected an array, got ${typeof paoLikeJSObjects}` );
            }   // if ( !Array.isArray ( paoLikeJSObjects ) )

            for ( const [ index, obj ] of paoLikeJSObjects.entries ( ) )
            {
                if ( obj == null || typeof obj !== 'object' )
                {
                    throw new TypeError ( `${strMethodName}: element at index ${index} is not an object` );
                }   // if ( obj == null || typeof obj !== 'object' )

                if ( ! ( pstrMatchPropName in obj ) )
                {
                    throw new TypeError ( `${strMethodName}: element at index ${index} is missing property ${pstrMatchPropName}` );
                }   // if ( ! ( pstrMatchPropName in obj ) )

                if ( ! ( pstrReturnPropName in obj ) )
                {
                    throw new TypeError ( `${strMethodName}: element at index ${index} is missing property ${pstrReturnPropName}` );
                }   // if ( ! ( pstrReturnPropName in obj ) )
            }   // for ( const [ index, obj ] of paoLikeJSObjects.entries ( ) )

            const found = paoLikeJSObjects.find ( o => o [ pstrMatchPropName ] === poMatchValue );
            return found ? found [ pstrReturnPropName ] : undefined;
        }   // LLCommon.strictLookup


        LLCommon.SummarizeTextDialog = function ( )
        {
            const GetSummary = function ( )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                var Command         = $('#Commands').val ( );
                var Summary_Text    = document.getElementById ( LLCommon.STT_TEXT_TO_SUMMARIZE ).value.trim ( );

                document.getElementById ( LLCommon.STT_SUMMARY_OF_TEXT ).value = EMPTY_STRING;

                if ( Summary_Text.length > EMPTY_STRING_LENGTH )
                {
                    $.ajax({
                        url     : _llAppPath + 'Open/ChatGPTSummarize',
                        data    : {
                                     'text'    : Summary_Text,
                                     'command' : Command
                                  },
                        type    : 'POST',
                        async   : false,
                        success : function ( data )
                        {
                            if ( ( data !== undefined ) && ( data !== null ) )
                            {
                                document.getElementById ( LLCommon.STT_SUMMARY_OF_TEXT ).value = data.trim ( );
                            }
                        }
                    });
                };  // if ( Summary_Text.length > EMPTY_STRING_LENGTH )

                return document.getElementById ( LLCommon.STT_SUMMARY_OF_TEXT ).value;
            }   // const GetSummary = function ( )


            //  ----------------------------------------------------------------
            //  Each button is constructed as a separate object so that the
            //  ternary expression that is assigned to the buttons object has a
            //  proprty for each button that is valid in the circumstances. The
            //  resulting button object, which contains the three buttons that
            //  are always applicable, plus two more that are applicable when a
            //  lead ID is available, is assigned to the buttons property of the
            //  constructed BootBox object.
            //  ----------------------------------------------------------------

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
            debugger;

            const btnText2SSF           = {
                                              label     : 'Save Text in Transcripts',
                                              className : 'TranscriptReview_Padded_BlueTheme STT_btnText2SSF',
                                              callback  : function ( )
                                                          {
                                                              debugger;
                                                              const strOutcome = LLCommon.DoAjax ( 'GetDeepGramTrans' ,
                                                                                                   'POST' ,
                                                                                                   {
                                                                                                       'LeadId'        : _leadid ,
                                                                                                       'Transcription' : document.getElementById ( LLCommon.STT_TEXT_TO_SUMMARIZE ).value,
                                                                                                       'Option'        : 'conv_external_text2file'
                                                                                                   } );
                                                              return false;     // Leave the dialog box open.
                                                          }
                                          };

            const btnTextAndSummary2SSF = {
                                              label     : 'Save Text & Summary in Transcripts',
                                              className : 'TranscriptReview_Padded_BlueTheme STT_btnTextAndSummary2SSF',
                                              callback  : function ( )
                                                          {   // Since it appears that HTML markup such as '<br /><br />' is invalid, I replaced each with a CR/LF pair.
                                                              debugger;
                                                              const strOutcome = LLCommon.DoAjax ( 'GetDeepGramTrans' ,
                                                                                                   'POST' ,
                                                                                                   {
                                                                                                       'LeadId'        : _leadid ,
                                                                                                       'Transcription' :   document.getElementById ( LLCommon.STT_TEXT_TO_SUMMARIZE ).value
                                                                                                                         + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR + '-------- Summary --------' + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                                                                                                         + GetSummary ( ),
                                                                                                       'Option'        : 'conv_external_text2file'
                                                                                                   } );
                                                              return false;     // Leave the dialog box open.
                                                          }
                                          };

            const btnCopySummary2CB     = {
                                              label     : 'Copy Summary to Clipboard',
                                              className : 'TranscriptReview_Padded_BlueTheme STT_btnCopySummary2CB',
                                              callback  : function ( )
                                                          {
                                                              debugger;
                                                              LLCommon.PasteTextOntoClipboard  ( event , document.getElementById ( LLCommon.STT_SUMMARY_OF_TEXT ) );
                                                              return false;     // Leave the dialog box open.
                                                          }
                                          };
            const btnSummarizeText      = {
                                              label     : 'Summarize',
                                              className : 'TranscriptReview_Padded_BlueTheme STT_btnSummarizeText',
                                              callback  : function ( )
                                                          {
                                                              GetSummary ( );
                                                              return false;     // Leave the dialog box open.
                                                          }
                                          };
            const btnClose              = {
                                              label     : 'Close',
                                              className : 'btn-danger STT_btnClose',
                                              callback  : function ( )
                                                          {
                                                              return true;      // Close the dialog box.
                                                          }
                                          };

            const buttons               = _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID
                                          ? {
                                                btnText2SSF,
                                                btnTextAndSummary2SSF,
                                                btnCopySummary2CB,
                                                btnSummarizeText,
                                                btnClose
                                            }
                                          : {
                                                btnCopySummary2CB,
                                                btnSummarizeText,
                                                btnClose
                                            };

            const SumTypes              = '<select id="Commands" title="Select summary type">'
                                           + '<option value="Summary">Summary</option>'
                                           + '<option value="Sentiment">Sentiment</option>'
                                           + '<option value="Key Points">Key Points</option>'
                                           + '<option value="Action Items">Action Items</option>'
                                           + '</select>';
            var box                     = bootbox.dialog({
                                              message :   "<div style='font-size:12pt;' title='Summarize text - copy / paste text, select Summary type, hit Ok, then copy / paste summary'><strong>"
                                                        + "<span style='font-size:20px; width:80%; text-align: center; color: black; background-color: white;'>&nbsp;Summarize Text</span><br /><br />"
                                                        + "<span style='font-size:12px'><b>&nbsp;Text to summarize:</b></span><br />"
                                                        + "&nbsp;<textarea id='" + LLCommon.STT_TEXT_TO_SUMMARIZE + "' style='width:90%;height:300px;'></textarea><br /><br />"
                                                        + "<span style='font-size:12px'><b>&nbsp;Select summary type:</b>&nbsp;&nbsp;" + SumTypes + "</span><br /><br />"
                                                        + "<span style='font-size:12px'><b>&nbsp;Summarized text:</b></span><br />"
                                                        + "&nbsp;<textarea id='" + LLCommon.STT_SUMMARY_OF_TEXT + "' style='width:90%;height:200px;' readonly></textarea><br /><br />"
                                                        + "&nbsp;Chat GPT 4o limits input text to about 16,000 words - if text is too long, summarize multiple parts separately.<br />"
                                                        + "</strong></div>",
                                              size    : 'large',
                                              title   : EMPTY_STRING,
                                              buttons : buttons,
            }).init ( function ( )
            {
                console.log ( 'Determine whether and how I can add a tooltip to a "built-in" BootBox button.' );
            });

            document.querySelector ( '.STT_btnText2SSF'           ).title = 'Click ONCE to save the text WITHOUT the summary to a text file and attach it to the Story-So-Far.';
            document.querySelector ( '.STT_btnTextAndSummary2SSF' ).title = 'Click ONCE to save the text and summary to a text file and attach it to the Story-So-Far.';
            document.querySelector ( '.STT_btnCopySummary2CB'     ).title = 'Click ONCE to copy the summary to the clipboard.';
            document.querySelector ( '.STT_btnSummarizeText'      ).title = 'Click ONCE to summarize. It may be several seconds beforee the summary box fills.';

            var dialog = box.find ( '.modal-dialog' );

            box.css ( 'display'      , 'block' );
            box.css ( 'border-radius', '10px !important' );
            box.css ( 'width'        , '1000px' );
            box.css ( 'overflow-y'   , 'auto' );

            dialog.css ( 'margin-top', Math.max ( 0 , ( $(window).height ( ) - dialog.height ( ) ) / 2 ) );
        }   // LLCommon.SummarizeTextDialog


        LLCommon.SummarizeText = function ( pstrText2Summarize , pfAppend2Transcript )
        {
            /*
                ----------------------------------------------------------------
                Name:       SummarizeText

                Goal:       Ask the ChatGPT4 model to summarize text.

                Arguments:  pstrText2Summarize  = This string specifies the ID
                                                  of the HTML element that has
                                                  as its innerHTML property the
                                                  text to transcribe.

                            pfAppend2Transcript = When this value evaluates to
                                                  Boolean TRUE, the summary is
                                                  appeended to the transcript.
                                                  Please see the Remarks.

                Returns:    As of now, the return value is undefined.

                Remarks:    Since omitting pfAppend2Transcript causes its value
                            to be treated as if it was explicitly specified as
                            False, and AddSummary argument ChatGPTSummarize has
                            a type of Boolean, its value can safely be passed
                            through to ChatGPTSummarize without evaluation.
                ----------------------------------------------------------------
            */

            function GetText2Summarize ( pstrText2Summarize )
            {
                const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

                if (    pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTP )
                     || pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTPS )
                     || pstrText2Summarize.startsWith ( STT_NOTE_ID_PREFIX ) )
                {
                    LLCommon.Trace ( strMethodName + ': Summarizing text from ' + pstrText2Summarize );

                    return pstrText2Summarize;
                }   // TRUE (anticipated outcome per the improved algorithm) block, if (    pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTP ) || pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTPS ) || pstrText2Summarize.startsWith ( STT_NOTE_ID_PREFIX ) )
                else
                {
                    LLCommon.Trace ( strMethodName + ': Text to summarize  = ' + pstrText2Summarize );
                    LLCommon.Trace ( strMethodName + ': Words to summarize = ' + pstrText2Summarize.split ( SPACE_CHARACTER ).length );

                    return document.getElementById ( pstrText2Summarize ).innerText;
                }   // FALSE (unanticipated outcome, per thee original algorithm) block, if (    pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTP ) || pstrText2Summarize.startsWith ( LLCommon.PROTOCOL_IS_HTTPS ) || pstrText2Summarize.startsWith ( STT_NOTE_ID_PREFIX ) )
            }   // function GetText2Summarize


            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
            debugger;
            const strText2Summarize     = GetText2Summarize ( pstrText2Summarize );

            if ( strText2Summarize !== null )
            {
                const strSummary        = LLCommon.DoAjax ( 'ChatGPTSummarize' ,
                                                            'POST' ,
                                                            {
                                                                'text'       : strText2Summarize ,
                                                                'DomainId'   : _domainid ,
                                                                'TenantId'   : _tenantid ,
                                                                'Email'      : _login ,
                                                                'AddSummary' : pfAppend2Transcript
                                                            } );

                if ( strSummary.length > EMPTY_STRING_LENGTH )
                {
                    const docSumm       = document.createElement ( 'div' );
                    docSumm.style       = 'text-align: left;';
                    docSumm.id          = LLCommon.STT_TEXT_TO_SUMMARIZE;
                    docSumm.innerHTML   = strSummary;
                    const box           = bootbox.dialog ({
                                                            title   : 'Transcript Summary',
                                                            message : docSumm.outerHTML,
                                                            size    : 'large',
                                                            buttons: {
                                                                copy2CB: {
                                                                    label     : 'Copy to Clipboard',
                                                                    className : 'TranscriptReview_BlueTheme',
                                                                    callback  : function ( )
                                                                    {
                                                                        debugger;
                                                                        LLCommon.PasteTextOntoClipboard  ( event , document.getElementById ( LLCommon.STT_TEXT_TO_SUMMARIZE ) );
                                                                    }
                                                                },
                                                                dismiss: {
                                                                    label     : 'Close',
                                                                    className : 'btn-danger',
                                                                    callback  : function ( )
                                                                    {
                                                                    }
                                                                }
                                                            }
                                                        });
                    window.SummaryBox = box;
                }   // if ( strSummary.length > EMPTY_STRING_LENGTH )
            }   // TRUE (anticipated outcome) block, if ( docText2Summarize !== null )
            else
            {
                const strErrorMessage   = 'An internal error arose. The pointer to the transcript is corrupted.';
                LLCommon.LogException ( strMethodName + ': ' + strErrorMessage );
                bootbox.alert ( strErrorMessage );
            }   // FALSE (unanticipated outcome) block, if ( docText2Summarize !== null )
        }   // LLCommon.SummarizeText


        LLCommon.SynchronizeLoginInfo = function ( Login2Sync, InputSource )
        {
            /*
                ----------------------------------------------------------------
                Name:       SynchronizeLoginInfo

                Goal:       Synchronize the domain, tenant, and user IDs with a
                            specified SalesTalk login, and mark their source as
                            specified.

                Arguments:  Login2Sync  = This is the string representation
                                          of the SalesTalk login to process.

                            InputSource = This integer is the value to store as
                                          the source of the information.

                Returns:    If the specified login is valid, the return value is
                            Boolean TRUE. Otherwise, the return value is FALSE.
                ----------------------------------------------------------------
            */

            const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

            _login                      = Login2Sync;
            _loginSource                = InputSource

            const strResultSet          = LLCommon.DoAjax ( 'GetDomainTenantUserIds4LoginName',
                                                            'GET',
                                                            {
                                                               'loginName' : Login2Sync
                                                            } );

            if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR ) > INDEXOF_NOT_FOUND )
            {
                const astrResults       = strResultSet.split ( PIPE_CHAR_SPLIT_MATCH );

                _userid                 = parseInt ( astrResults [ ARRAY_FIRST_ELEMENT  ] );
                _tenantid               = parseInt ( astrResults [ ARRAY_SECOND_ELEMENT ] );
                _domainid               = parseInt ( astrResults [ ARRAY_THIRD_ELEMENT  ] );
                _domainname             =            astrResults [ ARRAY_FOURTH_ELEMENT ];

                _useridSource           = InputSource;
                _domainidSource         = InputSource;
                _tenantidSource         = InputSource;
                _domainnameSource       = InputSource;

                sessionStorage.setItem ( 'login'      , _login );
                sessionStorage.setItem ( 'userid'     , _userid );
                sessionStorage.setItem ( 'tenantid'   , _tenantid );
                sessionStorage.setItem ( 'domainid'   , _domainid );
                sessionStorage.setItem ( 'domainname' , _domainname );

                LLCommon.DialerLogin    = Login2Sync;
                return true;
            }   // TRUE (anticipated outcome) block, if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )
            else
            {
                if ( strResultSet.startsWith ( 'The specified login ID, ' ) && strResultSet.endsWith ( ', is invalid.' ) )
                {
                    return false;
                }   // TRUE (The most likely cause of an exception is that _login is invalid. ) block, if ( strResultSet.startsWith ( 'The specified login ID, ' ) && strResultSet.endsWith ( ', is invalid.' ) )
                else
                {
                    throw new Error ( 'ERROR in LLCommon document ready event listener: GetDomainTenantUserIds4LoginName returned no information for UserName value of ' + _login + ' that was read from sessionStorage.' );
                }   // FALSE (An entirely unexpected type of exception arose.) block, if ( strResultSet.startsWith ( 'The specified login ID, ' ) && strResultSet.endsWith ( ', is invalid.' ) )
            }   // FALSE (unanticipated outcome) block,if ( strResultSet.length > EMPTY_STRING_LENGTH && strResultSet.indexOf ( PIPE_CHAR_SPLIT_MATCH ) > INDEXOF_NOT_FOUND )

            return rfLoginIsInvalid;    // Though marked as unreachable by ESLint, I'm leaving it for now.
        }   // LLCommon.SynchronizeLoginInfo method


        LLCommon.TearSheetUpload = function ( pdocFile , pstrTearSheetName )
        {
            debugger;
            var oFile = pdocFile.files [ ARRAY_FIRST_ELEMENT ];

            if ( !oFile )
            {
                bootbox.alert ( '<br /><b>Cannot access oFile.</b><br />' );
                return;
            }
            var reader = new FileReader ( );

            reader.onload = ( e ) =>
            {
                debugger;
                $.ajax({
                    type    : 'POST',
                    url     : _llAppPath + 'Home/TearSheetUpload',
                    data    : {
                                  'FileName'         : oFile.name,
                                  'LastModifiedDate' : oFile.lastModifiedDate,
                                  'FileSize'         : oFile.size,
                                  'MimeType'         : oFile.type,
                                  'Contents'         : e.target.result,
                                  'TearSheetName'    : pstrTearSheetName
                              },
                    success : function ( data )
                    {
                        if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                        {
                            bootbox.alert ( data ,
                                            function ( )
                                            {
                                                if ( pfRefreshBigBicture )
                                                {
                                                    location.reload ( );
                                                }   // TRUE (default form setting) block, if ( pfRefreshBigBicture )
                                                else
                                                {
                                                    bootbox.alert ( 'Refresh the page to display the new list.' );
                                                }   // FALSE (alternate form setting) block, if ( pfRefreshBigBicture )
                                            }
                                          );
                        }   // if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                    },
                    error: function ( jqXHR, textStatus, errorThrown )
                    {
                        bootbox.alert ( textStatus + SPACE_CHARACTER + jqXHR.responseText + SPACE_CHARACTER + errorThrown );
                    }
                });
            };  // reader.onload = function ( e )

            reader.readAsDataURL ( oFile ); // base64
        }   // LLCommon.TearSheetUpload


        /**
         * Record a message on console and/or system trace log on server,
         * depending on the values of an input parameter, the truthiness of
         * global symbol LLCommon_LogTraces, and the current value of object
         * property LLCommon.TRACE, which is treated as an enumeration.
         *
         * The design of this function is such that it should **never** throw.
         *
         * @function LLCommon.Trace
         * @param {Object}  pstrMessage - An object that is expected to have a
         *                                string representation of some kind,
         *                                though it need not be a String
         * @param {Boolean} pfCoerce    - If truthy, force logging to the
         *                                console and the trace log, with a
         *                                distinctive prefix,
         *                                'Coerced trace, Source = ', followed
         *                                by the string representation of the
         *                                value of pfCoerce on the system trace
         *                                message
         * @returns {void}
         */
        LLCommon.Trace = function ( pstrMessage , pfCoerce )
        {
            if ( pfCoerce )
            {
                console.log ( pstrMessage );

                //  ------------------------------------------------------------
                //  Since the default value of async is TRUE, this request is
                //  non-blocking.
                //  ------------------------------------------------------------

                $.ajax ( { url   : LLCommon.AjaxUrlPrefix + 'Open/SendToTrace',
                           type  : 'GET',
                           cache : false,
                           data  : { 'Message' :   'Coerced trace, Source = ' + pfCoerce
                                                 + LLCommon.STANDARD_SEND_TO_TRACE_PREFIX
                                                 + window.location.href
                                                 + ', Message = '+ pstrMessage
                                   }
                         }
                       );
            }   // TRUE (The `pfCoerce` flag is truthy, and it trumps everything else.) block, if ( pfCoerce )
            else
            {
                if ( LLCommon_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js' ) && LLCommon_LogTraces )
                {
                    console.log ( pstrMessage );
                }   // TRUE (This function executed from the development library.) block, if ( LLCommon_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && LLCommon_LogTraces )
                else
                {
                    switch ( LLCommon.TRACE )
                    {
                        case LLCommon.TRACE_CONSOLE:
                            console.log ( pstrMessage );
                            break;

                        case LLCommon.TRACE_PHONE_HOME:
                            //  ------------------------------------------------
                            //  Since the default value of async is TRUE, this
                            //  request is non-blocking.
                            //  ------------------------------------------------

                            $.ajax ( { url   : LLCommon.AjaxUrlPrefix + 'Open/SendToTrace',
                                       type  : 'GET',
                                       cache : false,
                                       data  : { 'Message' :   LLCommon.STANDARD_SEND_TO_TRACE_PREFIX
                                                             + window.location.href
                                                             + ', Message = '
                                                             + pstrMessage }
                                     }
                                   );
                            break;

                        case LLCommon.TRACE_SILENT:
                            break;
                    }   // switch ( LLCommon.TRACE )
                }   // FALSE (This function executed from the production library.) block, if ( LLCommon_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && LLCommon_LogTraces )
            }   // FALSE (The `pfCoerce` flag is falsy, so the original algorithm still rules.) block, if ( pfCoerce )
        }   // LLCommon.Trace


        /**
         * Validates parameters against expected types.
         *
         * @function LLCommon.validateParams
         * @param {Object} spec - A map of parameter names to descriptors:
         *                        { value, type, required }
         * @param {string} callerName - Name of the calling function for error reporting.
         * @param {boolean} [tryMode=false] - If true, return array of invalid parameters instead of throwing.
         * @returns {Array} Empty array if all valid, otherwise list of invalid parameter names (only if tryMode=true).
         * @throws {TypeError} If any parameter fails validation and tryMode=false.
         */
        LLCommon.validateParams = function ( spec, callerName, tryMode = false )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
            const invalidParams = [ ];

            for ( const [ name, descriptor ] of Object.entries ( spec ) )
            {
                const { value, type, required = false } = descriptor;
                const allowedTypes = Array.isArray ( type )
                                     ? type
                                     : [ type ];
                const actualType   = ( value === null
                                       ? 'null'
                                       : typeof value );

                if ( value === undefined && required )
                {
                    invalidParams.push ( name );

                    if ( !tryMode )
                    {
                        throw new TypeError(`${callerName}: ${name} is required but was not provided.`);
                    }   // if ( !tryMode )
                }   // if ( value === undefined && required )
                else if ( value !== undefined && !allowedTypes.includes ( actualType ) )
                {
                    invalidParams.push ( name );

                    if ( !tryMode )
                    {
                        throw new TypeError ( `${callerName}: ${name} must be of type ${allowedTypes.join ( ' or ' )}.` );
                    }   // if ( !tryMode )
                }   // else if ( value !== undefined && !allowedTypes.includes ( actualType ) )
            }   // for ( const [ name, descriptor ] of Object.entries ( spec ) )

            return tryMode ? invalidParams : [ ];
        };  // LLCommon.validateParams

        //  ====================================================================
        //  Everything from here to the end is by and large legacy LLCommon.
        //  ====================================================================

        LLCommon.initializeTimeZone ( );

        LLCommon.baseURL                = window.location.origin;
        LLCommon.DatabaseName           = _llAppPath.substring ( SUBSTRING_SECOND_CHARACTER ,
                                                                 _llAppPath.length - SUBSTRING_LAST_CHARACTER );
        LLCommon.AjaxUrlPrefix          = ( HostIsPurl ( )
                                            ? location.protocol + PATH_SEPARATOR_CHAR + PATH_SEPARATOR_CHAR + location.domain
                                            : LLCommon.baseURL )
                                          + _llAppPath;
        //LLCommon.AjaxUrlPrefix          = 'http://localhost:60366/';	2026/03/30 20:51:41 - DG - **NEVER** DO THIS AGAIN!
        LLCommon.AJAX_RETRY_LIMIT       = 10;
        LLCommon.HTTPS_PROTOCOL         = 'https:';

        if ( _dbnameSource === SRC_IS_UNKNOWN )
        {
            _dbname                     = LLCommon.DoAjax ( 'GetDatabaseNameFromURL' ,
                                                            'GET' ,
                                                            {
                                                                'url' : location.href
                                                            } );
            _dbnameSource               = SRC_IS_DATABASE;
            sessionStorage.setItem ( 'dbname' , _dbname );
        }   // if ( _dbnameSource === SRC_IS_UNKNOWN )

        isInitialized                   = true;
    }   // function init

    /**
    * ---------------------------------------------------------------------------
    * Dirty State Manager (Design Note)
    * ---------------------------------------------------------------------------
    * LLCommon centralizes all "form dirty" state transitions so that UI components
    * can reliably react to changes without each form implementing its own logic.
    *
    * The subsystem consists of three coordinated parts:
    *
    *   1. LLCommon._fFormIsDirty
    *      A getter/setter property that represents the authoritative dirty/clean
    *      state for the current UI. All state transitions must go through this
    *      property. Direct modification of LLCommon.__formIsDirty is prohibited.
    *
    *   2. LLCommon.__dirtyHandlers / LLCommon.__cleanHandlers
    *      Instances of LLCommon.EventHandlerList that store handlers to be invoked
    *      when the form transitions to the "dirty" or "clean" state, respectively.
    *      These lists enforce identity-based duplicate prevention and safe
    *      invocation semantics.
    *
    *   3. LLCommon.__notifyDirty() / LLCommon.__notifyClean()
    *      Internal dispatch functions that delegate to the corresponding
    *      EventHandlerList instance. These are invoked automatically by the
    *      _fFormIsDirty setter when the state changes.
    *
    * Together, these components provide a predictable, audit-friendly mechanism
    * for reacting to form state changes across the UI. Any module may register
    * handlers with the appropriate EventHandlerList to participate in the dirty/
    * clean lifecycle.
    * ---------------------------------------------------------------------------
    */

    /**
     * Global "form dirty" flag for the current UI. This property is implemented as
     * a getter/setter pair so that LLCommon can centrally manage UI behavior that
     * depends on the form's dirty state.
     *
     * Reading this property returns the current dirty state.
     *
     * Writing `true` transitions the form to "dirty" and automatically invokes all
     * registered dirty handlers **only if** the previous value was `false`.
     *
     * Writing `false` transitions the form to "clean" and automatically invokes all
     * registered clean handlers **only if** the previous value was `true`.
     *
     * Code should treat this property as the single source of truth for the form's
     * dirty state. Do **not** modify {@link LLCommon.__formIsDirty} directly.
     *
     * @name _fFormIsDirty
     * @memberof LLCommon
     * @type {boolean}
     */
    Object.defineProperty ( LLCommon, '_fFormIsDirty',
    {
        get: function ( )
        {
            return LLCommon.__formIsDirty;
        },
        set: function ( pfValue )
        {
            debugger;
            const fNewState = Boolean ( pfValue );          // Normalize to a true Boolean.

            if ( fNewState === LLCommon.__formIsDirty )
            {
                // Since the state is unchange, there is nothing to do but bug out.
                return;
            }   // if ( fNewState === LLCommon.__formIsDirty )

            LLCommon.__formIsDirty = fNewState;

            if ( fNewState )
            {
                LLCommon.__notifyDirty ( );
            }   // TRUE (Since the state is transitioning from False to True, call the routines in the _notifyDirty list.) block, if ( fNewState )
            else
            {
                LLCommon.__notifyClean ( );
            }   // FALSE (Since the state is transitioning from True to False, call the routines in the _notifyClean list.) block, if ( fNewState )
        },
        enumerable: true,
        configurable: false
    });


    Date.prototype.addHours = function ( h )
    {
        if (this instanceof Date) {
            this.setHours(this.getHours() + h);
            return this;
        }
    };  // Date.prototype.addHours


    function addOffset ( data )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        var   offseHours       = new Date ( ).getTimezoneOffset ( ) / 60;

        for ( var i = ARRAY_FIRST_ELEMENT;
                  i < data.length;
                  i++ )
        {
            var dt             = new Date ( data [ i ].StartDate );
            var StartDate      = new Date ( Date.UTC ( dt.getFullYear ( ),
                                                       dt.getMonth ( ),
                                                       dt.getDate ( ) ,
                                                       dt.getHours ( ) ,
                                                       dt.getMinutes ( ) ,
                                                       dt.getSeconds ( ) ,
                                                       dt.getMilliseconds ( ) ) );

            data [ i ].StartDate = StartDate.addHours ( -offseHours + dt.getTimezoneOffset ( ) / 30 );
        }   // for ( var i = ARRAY_FIRST_ELEMENT; i < data.length; i++ )
    }   // function addOffset


    /*
    LLCommon.DisplayInboxEmail = function (id) { // deprecated
        sessionStorage.removeItem('InboxEmailBody');
        if (id !== undefined && id.toString().length > 0) {
            $.get(_llAppPath + "InBox/GetEmailBody?id=" + id, function (data) {
                if (data !== undefined && data.toString().trim().length > 0) {
                    bootbox.dialog({
                        message: data,
                        size: "large",
                        title: "Email &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<button onclick='LLCommon.PushInboxEmail()'>Click here to reply to this email</button>"
                    });
                    var dialog = box.find('.modal-dialog');
                    box.css('display', 'block'); box.css('border-radius', '10px !important');
                    dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
                    sessionStorage.InboxEmailBody = data;
                }
            });
        }
    };  // LLCommon.DisplayInboxEmail
    */

    LLCommon.ERR_MESSAGE_STANDARD_PREFIX        = 'ERROR';
    LLCommon.STANDARD_SEND_TO_TRACE_PREFIX      = 'SalesTalk - URL - ';


    LLCommon.PushInboxEmail = function ( ) { // deprecated

        if ( sessionStorage.InboxEmailBody !== undefined ) {
            loadView ( 'emailfollowup' );
            // display the email template and close the bootbox dialog with the email body
            // in email template, if the InboxEmailBody sessionStorage exists, show the button
            // when user hits the button, copy the sessionStorage to the end of the email template
            // hopefully will not need to modify the revised email template to add the body
            //sessionStorage.removeItem('InboxEmailBody');
        }
    };  // LLCommon.PushInboxEmail


    LLCommon.ClearUpdateTargetAudience = function ( param )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        if ( param === undefined )
        {
            bootbox.confirm ( 'Are you sure? (Remember to click Save at the bottom)', function ( result )
            {
                if ( result )
                {
                    var link = '<a href=# id="updateTargetAudience" onclick="LLCommon.editCallQueue()">(none)</a>';
                    $('#updateTargetAudience').html(link);
                }
            });
        }
    };  // LLCommon.ClearUpdateTargetAudience


    LLCommon.createCallQueue = function ( )
    {
        sessionStorage.removeItem ( 'CreateTargetAudienceId' );
        sessionStorage.CreateCallCampaignName = EMPTY_STRING;

        var box = bootbox.dialog({
            message: "<div style='height:100%;width:100%;'>"
                + "<label for='ddlSubClient'>Call Campaign Name</label>"
                + HTML_NBSP
                + HTML_NBSP
                + "<input id='ddlSubClient' name='SubClient' style=width:'250px' />"
                + "</br>"
                + "</br><b>Select a Contact List to create a Call Queue - then scroll down to the bottom and click OK when ready</b>"
                + "</br>"
                + "<iframe id='targetAudienceIframe' style='height:700px;width:850px' </iframe>"
                + "</div>",
            title: 'Create Call Queue',
            size: "large",
            buttons: {
                ok: {
                    label: "OK!",
                    className: "btn-success",
                    callback: function () {

                        sessionStorage.CreateTargetAudienceId = sessionStorage.UpdateTargetAudienceId;
                        LLCommon.updateCallQueue();
                    }
                },
                cancel: {
                    label: "Cancel",
                    className: "btn-danger",
                    callback: function () {
                    }
                }
            }
        });

        var dialog = box.find ( '.modal-dialog' );
        box.css ( 'display' , 'block' ); box.css ( 'border-radius' , '10px !important' );
        dialog.css ( 'margin-top' , Math.max ( 0 , ($( window ).height ( ) - dialog.height ( ) ) / 2 ) );

        box.on("shown.bs.modal", function () {
            var dataSourceDDLSubClient = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: _llAppPath + "TargetAudience/GetSalesTeam",
                        dataType: "json"
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                parse: function (data) {
                    return data;
                }
            });

            $("#ddlSubClient").kendoDropDownList({
                dataSource: dataSourceDDLSubClient,
                dataTextField: "Key",
                dataValueField: "Value",
                change: function (e) {
                    sessionStorage.CreateCallCampaignName = this.value();
                }
            });

            document.getElementById('targetAudienceIframe').src = _llAppPath + "TargetAudience/Browse?disableAdd=true&windowId=TargetAudience";
        });

        box.on('hidden.bs.modal'), function () {
            $(this).data('bs.modal', null);
        };
    };  // LLCommon.createCallQueue


    LLCommon.updateCallQueue = function ( )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        sessionStorage.removeItem ( 'UpdateTargetAudienceId' );

        var box = bootbox.dialog({
            message: "<div style='height:100%;width:100%'>"
                + "<br />"
                + "<br /><b>Optionally select a Contact List to append contacts to the Call Queue - then scroll down to the bottom and click OK when ready</b>"
                + "<br />"
                + "<iframe id='UpdateTargetAudienceIframe' style='height:700px;width:850px' </iframe>"
                + "</div>",
            title: 'Update Call Queue',
            size: "large",
            buttons: {
                ok: {
                    label: "OK!",
                    className: "btn-success",
                    callback: function ( )
                    {
                        if ( sessionStorage.UpdateTargetAudienceId === undefined )
                        {
                            $.ajax({
                                url: _llAppPath + 'TargetAudience/CreateCallQueueFromTAInput',
                                data: { 'orderBy': EMPTY_STRING, 'SalesTeam': sessionStorage.CreateCallCampaignName, 'UpdateTargetAudienceName': EMPTY_STRING, 'TAId': sessionStorage.CreateTargetAudienceId },
                                success: function (result) {
                                    bootbox.alert(result, function () {
                                        window.location.href = _llAppPath + 'TargetAudience/ManageCallQueues';
                                    });
                                }
                            });
                        }
                        else
                        {
                            $.ajax({
                                url: _llAppPath + 'TargetAudience/GetTargetAudienceName',
                                data: { id: sessionStorage.UpdateTargetAudienceId },
                                success: function (UpdateTargetAudienceName) {

                                    $.ajax({
                                        url: _llAppPath + 'TargetAudience/CreateCallQueueFromTAInput',
                                        data: { orderBy: EMPTY_STRING, 'SalesTeam': sessionStorage.CreateCallCampaignName, 'UpdateTargetAudienceName': UpdateTargetAudienceName, 'TAId': sessionStorage.CreateTargetAudienceId },
                                        success: function (result) {
                                            bootbox.alert(result, function () {
                                                window.location.href = _llAppPath + 'TargetAudience/ManageCallQueues';
                                            });
                                        }
                                    });
                                }
                            });
                        }
                    }
                },
                cancel: {
                    label: "Cancel",
                    className: "btn-danger",
                    callback: function () {
                    }
                }
            }
        });

        var dialog = box.find('.modal-dialog');
        box.css('display', 'block'); box.css('border-radius', '10px !important');
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));

        box.on("shown.bs.modal", function () {
            document.getElementById('UpdateTargetAudienceIframe').src = _llAppPath + "TargetAudience/Browse?disableAdd=true&windowId=TargetAudience";
        });
    };  // LLCommon.updateCallQueue


    LLCommon.editCallQueue = function ( )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var box = bootbox.dialog({
            message: "<div style='height:100%;width:100%' >"
                + "</br>"
                + "</br><b>To append contacts to the Call Queue, optionally select a Contact List - then scroll down to the bottom and click OK when ready</b>"
                + "</br>"
                + "<iframe id='targetAudienceIframe' style='height:800px;width:850px' </iframe>"
                + "</div>",
            title: $("#Name").val(),
            size: "large",
            buttons: {
                ok: {
                    label: "OK!",
                    className: "btn-success",
                    callback: function () {
                        $.ajax({
                            url: _llAppPath + "TargetAudience/GetTargetAudienceName",
                            data: { id: sessionStorage.UpdateTargetAudienceId },
                            success: function (name) {
                                var link = '<a href=# style="width:98%" id="updateTargetAudience" onclick="LLCommon.editCallQueue()">' + name + '</a>';
                                $('#updateTargetAudience').html(link);
                                sessionStorage.removeItem('UpdateTargetAudienceId');
                            }
                        });
                    }
                },
                cancel: {
                    label: "Cancel",
                    className: "btn-danger",
                    callback: function () {
                        $('#CreateCallQueue').prop('checked', false);
                    }
                }
            }
        });

        var dialog = box.find('.modal-dialog');
        box.css('display', 'block'); box.css('border-radius', '10px !important');
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));

        box.on("shown.bs.modal", function () {
            document.getElementById('targetAudienceIframe').src = _llAppPath + "TargetAudience/Browse?disableAdd=true&windowId=TargetAudience&javascriptCallback=addTargetAudience&byId=true";
        });

    };  // LLCommon.editCallQueue


    LLCommon.SelectContactList = function ( )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var box = bootbox.dialog({
            message: "<div style='height:100%;width:100%' >"
                + "</br>"
                + "</br><b>Select a Contact List, then scroll down to the bottom and click OK</b>"
                + "</br>"
                + "<iframe id='targetAudienceIframe' style='height:800px;width:850px' </iframe>"
                + "</div>",
            title: $("#Name").val(),
            size: "large",
            buttons: {
                ok: {
                    label: "OK!",
                    className: "btn-success",
                    callback: function ( )
                    {
                        $.ajax({
                            url: _llAppPath + "TargetAudience/GetTargetAudienceName",
                            data: { id: sessionStorage.UpdateTargetAudienceId },
                            success: function (name) {

                                var link = '<a href=# style="width:98%" id="SelectContactList" onclick="LLCommon.SelectContactList()">' + name + '</a>';
                                $('#SelectContactList').html(link);
                                sessionStorage.removeItem('UpdateTargetAudienceId');
                            }
                        });

                    }
                },
                cancel: {
                    label: "Cancel",
                    className: "btn-danger",
                    callback: function () {
                        $('#UpdateTargetAudienceId').prop('checked', false);
                    }
                }
            }
        });

        box.on("shown.bs.modal", function () {

            document.getElementById('targetAudienceIframe').src = _llAppPath + "TargetAudience/Browse?disableAdd=true&windowId=TargetAudience&javascriptCallback=addTargetAudience&byId=true";

        });

        var dialog = box.find('.modal-dialog');
        box.css('display', 'block'); box.css('border-radius', '10px !important');
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
    };  // LLCommon.SelectContactList


    LLCommon.DisplayCallbacks = function ( newWindow )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( sessionStorage.CallQueueHeaderId && isCallFinished ( ) )
        {
            var box = bootbox.dialog({
                size: "large",
                title: "Select Callback To Process",
                message: '<div class="row">  ' +
                    '<div class="col-md-12" > ' +
                    '<form class="form-horizontal"> ' +
                    '<div id="CbGrid" class="xcol-md-16" style="width:100%"></div>' +
                    '</form> </div>  </div>',
                buttons: {
                    cancel: {
                        label: "Cancel",
                        className: "btn-danger",
                        callback: function () {

                        }
                    }
                }
            });

            var dialog = box.find ( '.modal-dialog' );
            box.css ('display', 'block' );
            box.css ( 'border-radius' , '10px !important' );
            dialog.css ('margin-top', Math.max ( 0 , ($(window).height ( ) - dialog.height ( ) ) / 2 ) );

            // Detail Grid

            var model = {
                id: "CallQueueDetailId",
                fields: {
                    CallQueueDetailId: { editable: false, nullable: false, type: "number" },
                    AgentName: { editable: false, type: "string" },
                    Name: { editable: false, type: "string" },
                    Title: { editable: false, type: "string" },
                    Recycles: { editable: false, type: "number" },
                    LeadId: { editable: false, type: "number" },
                    UserId: { editable: false, type: "number" },
                    StartDate: { editable: false, type: "date" },
                    ClientDate: { editable: false, type: "date" },
                    ClientTimeZone: { editable: false, type: "string" }
                }
            };

            var callQueueHeaderId = sessionStorage.CallQueueHeaderId;

            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: _llAppPath + "TargetAudience/GetCallBacks?CallQueueHeaderId=" + callQueueHeaderId + "&CallQueueDetailId=" + sessionStorage.PrevCallQueueDetailId,
                        dataType: "json"
                    }
                },
                //                requestEnd: function(e) {
                //                        if (e.response.data && e.response.data.length) {
                //                            var data = e.response.data;
                //                            addOffset(data);
                //                    };
                //                  },
                pageSize: 1000,
                sort: [{ field: "CallQueueDetailId", dir: "asc" }],
                schema: {
                    data: "data",
                    total: "total",
                    model: model
                }
            });
            $("#CbGrid").kendoGrid({
                theme: $(document).data("kendoSkin") || "silver",
                dataSource: dataSource,
                pageable: true,
                columns: [
                    {
                        field: "AgentName",
                        title: "Agent Name",
                        width: "100px",
                        attributes: { "class": "ellipse", "title": "#=AgentName#" }
                    }, {
                        field: "Name",
                        title: "Name",
                        width: "100px",
                        attributes: { "class": "ellipse", "title": "#=Name#" }
                    }, {
                        field: "Title",
                        title: "Title",
                        width: "100px",
                        attributes: { "class": "ellipse", "title": "#=Title#" }
                    }, {
                        field: "CompanyName",
                        title: "Company Name",
                        width: "100px",
                        attributes: { "class": "ellipse", "title": "#=CompanyNameMulti#" }
                    }, {
                        field: "Recycles",
                        title: "Recycles",
                        width: "75px",
                        format: '{0:n0}',
                        attributes: { "class": "ellipse", "title": "#=Recycles#" }
                    }, {
                        field: "LeadId",
                        title: "LeadId",
                        width: "75px",
                        attributes: { "class": "ellipse", "title": "CallQueueDetailId=#=CallQueueDetailId#" }
                    }, {
                        field: "StartDate",
                        title: "Callback Date",
                        width: "110px",
                        template: function (data) {
                            if (data.StartDate === null) {
                                return EMPTY_STRING;
                            }
                            else {
                                return kendo.toString(new Date(data.StartDateUTC), "MM/dd/yyyy hh:mm tt");
                            }
                        },
                        attributes: { "class": "ellipse", "title": "#=StartDate#" }
                    }, {
                        field: "ClientDate",
                        title: "Client Date",
                        width: "150px",
                        hidden: "true",
                        template: function (data) {
                            if (data.ClientDate === null) {
                                return EMPTY_STRING;
                            }
                            else {
                                return kendo.toString(data.ClientDate, "MM/dd/yyyy hh:mm tt");
                            }
                        },
                        attributes: { "class": "ellipse", "title": "#=ClientDate#" }
                    }, {
                        field: "ClientTimeZone",
                        title: "Client TZ",
                        width: "75px",
                        attributes: { "class": "ellipse", "title": "#=ClientTimeZone#" }
                    }, {
                        command: [
                            {
                                name: "callback",
                                click: function (e) {
                                    var tr = $(e.target).closest("tr");
                                    var data = this.dataItem(tr);

                                    $.get(_llAppPath + "Sales/NextCallQueue?Which=CallBackGrid&CallQueueHeaderId=" + sessionStorage.CallQueueHeaderId + "&PrevDetailId=" + sessionStorage.PrevCallQueueDetailId + "&CallDisposition=" + sessionStorage.PrevCallDisposition + "&Complete=" + sessionStorage.PrevCallComplete + "&CallBackTime=" + (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime) + "&CallBackId=" + data.CallQueueDetailId + "&CallBackLeadId=" + data.LeadId + "&CallQueueDetailId=" + data.CallQueueDetailId + "&TimeZoneSelections=" + LLCommon.timeZones, function (data) {
                                        var idS = data.split("|"); //[0] LeadId //[1] PrevCall Detail Id //[2] Current Call Disposition//[3]Company Name
                                        sessionStorage.AllowExit = "true";
                                        if ((idS.length !== 4) || isNaN(parseInt(idS[0]))) {
                                            LLCommon.CallQueueError(data);
                                        } else {
                                            sessionStorage.PrevCallQueueDetailId = idS[1];
                                            sessionStorage.PrevCallDisposition = sessionStorage.PrevCallComplete = sessionStorage.CalbackTime = EMPTY_STRING;
                                            sessionStorage.CurrentCallDisposition = idS[2];
                                            if (idS[3].trim().length > 0) {
                                                sessionStorage.MultiContact = idS[3];
                                            }
                                            if (newWindow === undefined) {
                                                window.location.href = _llAppPath + 'Sales?leadId=' + idS[0] + '&CB=true&FL=true';
                                            }
                                            else {
                                                window.open(_llAppPath + 'Sales?leadId=' + idS[0] + '&CB=true&FL=true');
                                            }
                                            window.close();
                                            $('.bootbox').modal('hide');
                                        }
                                    });
                                }
                            }
                        ], width: "85px"
                    }
                ]
            });

            // Edit grid row on double click.

            $('#CbGrid').on('dblclick', 'tr', function (e) {
                if ($(".k-grid-edit-row").length <= 0) {
                    $("#CbGrid").data("kendoGrid").editRow($("#CbGrid tr:eq(" + ($(this).index() + 1) + ")"));
                }
            });
        }

    };  // LLCommon.DisplayCallbacks


    LLCommon.CompanyCallQueueList = function ( firstTime, newWindow, fromMultiContact, isCallback, isSkip )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        sessionStorage.removeItem("MultiContact");

        if ( sessionStorage.CallQueueHeaderId && ( ( firstTime === 'FirstTime' ) || isCallFinished ( ) ) )
        {
            var callQueueHeaderId = sessionStorage.CallQueueHeaderId;
            firstTime = "firstTime";

            // Detail Grid
            var model = {
                id: "CallQueueDetailId",
                fields: {
                    CallQueueDetailId: { editable: false, nullable: false, type: "number" },
                    AgentName: { editable: false, type: "string" },
                    Recycles: { editable: false, type: "number" },
                    LeadId: { editable: false, type: "number" },
                    UserId: { editable: false, type: "number" },
                    StartDate: { editable: false, type: "date" },
                    ClientDate: { editable: false, type: "date" },
                    Timezone: { editable: false, type: "string" },
                    CompanyName: { editable: false, type: "string" }
                }
            };

            $.get(_llAppPath + "TargetAudience/GetCallQueueLeadsForOneCompany?CallQueueHeaderId=" + callQueueHeaderId + "&CallQueueDetailId=" + sessionStorage.PrevCallQueueDetailId + "&ContactsButton=" + (fromMultiContact === undefined ? "false" : fromMultiContact) + "&TimeZoneSelections=" + LLCommon.timeZones + "&IsSkip=" + (isSkip === undefined ? false : true), function (data) {
                LLCommon.Trace("TargetAudience/GetCallQueueLeadsForOneCompany?CallQueueHeaderId=" + callQueueHeaderId, data);
                if (data !== undefined) {
                    if (data.restarted === true) {
                        $.notifyBar({
                            cssClass: "warning",
                            html: "End of call queue - restarting",
                            delay: 10000
                        });
                    }
                    else {
                        if (data.data[0] !== undefined && data.data[0].NextRecycleLevel !== undefined && data.data[0].NextRecycleLevel.length > 0) {
                            $.notifyBar({
                                cssClass: "warning",
                                html: data.data[0].NextRecycleLevel,
                                delay: 10000
                            });
                        }
                    }
                    if (data.data[0] !== undefined) {
                        sessionStorage.RecycleCount = data.data[0].Recycles;
                        sessionStorage.RecycleLevel = data.data[0].RecycleLevel;
                        sessionStorage.RecycleMinCount = data.data[0].RecycleMinCount;
                    }
                }

                if (data !== undefined && data.total < 2) {
                    sessionStorage.removeItem("MultiContact");
                    // Close dialog and call NextCallQueue since there are less than two records.
                    LLCommon.NextCallQueue(firstTime, "0", EMPTY_STRING, newWindow, ((data.total < 1) ? "0" : data.data[0].CallQueueDetailId), "Call");
                    return data;
                }

                sessionStorage.MultiContact = data.data[0].CompanyName;

                if (sessionStorage.PrevCallDisposition !== undefined && sessionStorage.PrevCallDisposition.trim().length > 0) {
                    $.ajax({
                        url: _llAppPath + "Sales/FinalCallQueue",
                        data: {
                            "Which": ((window.location.href.indexOf('FL=true') === -1) ? "MultiCompanyButtonFinal" : "MultiCompanyButtonFinalNoRecycle"),
                            "CallQueueHeaderId": sessionStorage.CallQueueHeaderId,
                            "PrevDetailId": sessionStorage.PrevCallQueueDetailId,
                            "CallDisposition": sessionStorage.PrevCallDisposition,
                            "Complete": sessionStorage.PrevCallComplete,
                            "CallBackTime": (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime),
                            "TimeZoneSelections": LLCommon.timeZones
                        },
                        type: "GET",
                        success: function (data) {
                            sessionStorage.removeItem("PrevCallQueueDetailId");
                            sessionStorage.removeItem("CalbackTime");
                            sessionStorage.removeItem("PrevCallDisposition");
                            sessionStorage.removeItem("PrevCallComplete");

                            // LLCommon.CompanyCallQueueList(firstTime, newWindow, false); ////////// fix short queue ?? //////////
                        },
                        error: function (xhr) {
                            alert('ERROR Contact Click:' + xhr);
                        }
                    });
                }

                sessionStorage.removeItem("MultiContactNextButton");

                var box = bootbox.dialog({
                    size: "large",
                    title: ((isCallback === undefined || (!isCallback)) ? "Select Alternate Contact at " : "Select Callback to Process at ") + sessionStorage.MultiContact,
                    message: '<div class="row">  ' +
                        '<div class="col-md-12" > ' +
                        '<form class="form-horizontal"> ' +
                        '<div id="CbCompanyGrid" class="xcol-md-16" style="width:99%;"></div>' +
                        '<div id="noCallbacks" style="display:none;margin-right:9px"><h2>There are no callbacks to display - check time zones and recycled time limit</h2></div>' +
                        '</form> </div>  </div>',
                    buttons: {
                        cancel: {
                            label: "Cancel",
                            className: "btn-danger",
                            callback: function () {
                                localStorage.removeItem('callQueueLaunched');
                                if (sessionStorage.CallIsComplete !== undefined && sessionStorage.CallIsComplete === 'true') {
                                    $('.markToHide').hide();
                                }
                                $('.bootbox').modal('hide');
                                sessionStorage.AllowExit = "true";
                                if (isCallback !== undefined) {
                                    isCallback = undefined;
                                    //LLCommon.CompanyCallQueueList(firstTime, newWindow, fromMultiContact, isCallback, true);
                                }
                            }
                        },
                        next: {
                            label: "Next",
                            className: "btn-primary",
                            callback: function () {
                                sessionStorage.MultiContactNextButton = true;
                                $('.bootbox').modal('hide');

                                $.ajax({
                                    url: _llAppPath + "Sales/FinalCallQueue",
                                    data: {
                                        "Which": ((window.location.href.indexOf('FL=true') === -1) ? "NextButtonFinal" : "NextButtonFinalNoRecycle"),
                                        "CallQueueHeaderId": sessionStorage.CallQueueHeaderId,
                                        "PrevDetailId": sessionStorage.PrevCallQueueDetailId,
                                        "CallDisposition": sessionStorage.PrevCallDisposition,
                                        "Complete": sessionStorage.PrevCallComplete,
                                        "CallBackTime": (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime),
                                        "TimeZoneSelections": LLCommon.timeZones
                                    },
                                    type: "GET",
                                    success: function (data) {
                                        sessionStorage.removeItem("PrevCallQueueDetailId"); ///////
                                        sessionStorage.removeItem("CalbackTime");
                                        sessionStorage.removeItem("PrevCallDisposition");
                                        sessionStorage.removeItem("PrevCallComplete");

                                        LLCommon.CompanyCallQueueList(firstTime, newWindow, false);

                                    },
                                    error: function (xhr) {
                                        alert('ERROR Contact Click:' + xhr);
                                    }
                                });

                            }
                        },
                        mark: {
                            label: "Mark Complete",
                            className: "btn",
                            callback: function (e) {
                                bootbox.confirm("Mark un-worked alternate contacts as Complete? This is not reversible.", function (result) {
                                    if (result) {
                                        $('.bootbox').modal('hide');
                                        sessionStorage.AllowExit = "true";
                                        $.ajax({
                                            url: _llAppPath + "TargetAudience/MarkAllAlternateContacts",
                                            data: { CallQueueHeaderId: sessionStorage.CallQueueHeaderId, CompanyName: sessionStorage.MultiContact },
                                            success: function (rows) {
                                                //if (rows !== "0") {
                                                LLCommon.CompanyCallQueueList(firstTime, newWindow, fromMultiContact, isCallback, true);
                                                //}
                                            }
                                        });
                                    }
                                });

                                return false;
                            }
                        }
                    }
                }
                );

                var dialog = box.find('.modal-dialog');
                box.css('display', 'block'); box.css('border-radius', '10px !important');
                dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));

                box.on("shown.bs.modal", function () {
                    var filter = { 'field': "CallOutcome", 'operator': "neq", 'value': "Recycled" };

                    if (isCallback !== undefined && isCallback) {

                        $('.modal-footer [data-bb-handler="mark"]').hide();

                        filter = { 'logic': 'or', 'filters': [{ 'field': "CallOutcome", 'operator': "contains", 'value': "Callback" }, { 'field': "CallOutcome", 'operator': "contains", 'value': "Call back" }] };
                    }
                    else {
                        $('#ActiveCBWarning').remove();
                        $('.modal-footer').prepend('<span id="ActiveCBWarning" style="padding-right:25px; font-size: 12px; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;"><b>Alternate Contact panel will be re-displayed when Next button is hit if there are active CallBacks.</b></span>');
                    }

                    if (isCallback) {
                        $('.btn-primary').hide();
                    }

                    $(document).off('focusin.modal');

                    $("#CbCompanyGrid").off('click');
                    if ($("#CbCompanyGrid").data('kendoGrid') !== null) {
                        $("#CbCompanyGrid").removeData('kendoGrid');
                        $("#CbCompanyGrid").empty();
                    }

                    $("#CbCompanyGrid").kendoGrid({
                        theme: $(document).data("kendoSkin") || "silver",
                        sortable: true,
                        filterable: true,
                        pageable: true,
                        dataSource: {
                            pageSize: 20,
                            data: data,
                            filter: filter,
                            schema: {
                                data: "data",
                                total: function (response) {

                                    return response.total; // total is returned in the "total" field of the response
                                }
                            }
                        },
                        dataBound: function (e) {
                            var grid = $("#CbCompanyGrid").data("kendoGrid");

                            if (grid === undefined) {
                                return;
                            }

                            if (isCallback && grid.dataSource.total() === 0) {
                                $('#noCallbacks').show();
                            }
                            else {
                                $('#noCallbacks').hide();
                            }

                            var gridData = grid.dataSource.view();
                            $("#CbCompanyGrid tr.k-alt").removeClass("k-alt");

                            var dtnow = (new Date()).getTime(); // current date and time - time zone offset?

                            for (var i = 0; i < gridData.length; i++) {
                                var currentUid = gridData[i].uid;
                                var complete = gridData[i].Complete;
                                var recycleCount = gridData[i].Recycles;
                                var recycleLevel = gridData[i].RecycleLevel;
                                var recycleMinCount = gridData[i].RecycleMinCount;
                                var callOutcome = gridData[i].CallOutcome.replace(/ /g, EMPTY_STRING);
                                var currentRow = grid.table.find("tr[data-uid='" + currentUid + "']");
                                var callButton = $(currentRow).find(".k-grid-Select");
                                var selectButton = $(currentRow).find(".k-grid-Select");
                                var startDate = gridData[i].StartDate;
                                var startDateString = EMPTY_STRING;
                                var dt = EMPTY_STRING;
                                var dtlong = 0;

                                if ((startDate !== undefined) && (startDate !== null) && (startDate !== EMPTY_STRING) && (callOutcome.toLowerCase() === "callback")) {
                                    try {
                                        dt = new Date(startDate);
                                        dtlong = dt.getTime() - dt.getTimezoneOffset() * 60000; // dtlong is callback date and time and comparable to dtnow, which is current date and time in same format
                                        startDateString = kendo.toString(new Date(dtlong), "MM/dd/yyyy hh:mm tt");
                                    }
                                    catch (err) {
                                        dt = EMPTY_STRING;
                                        dtlong = dtnow + 1;
                                    }
                                }

                                var recycle = (recycleCount === undefined ? 0 : recycleCount) + "/" + (recycleLevel === undefined ? 0 : recycleLevel) + "/" + (recycleMinCount === undefined ? 0 : recycleMinCount);
                                $(selectButton).closest('a').attr('title', ((recycle !== "0/0/0") ? "Recycle: " + recycle : EMPTY_STRING));
                                if ((callOutcome !== null && callOutcome.toLowerCase().replace(/\s/g, EMPTY_STRING).indexOf("callback") >= 0 && complete === "Complete")) {
                                    $(callButton).attr('title', startDateString);
                                    $(selectButton).closest('a').css('cursor', 'default').attr('pointer-events', 'none').css('color', '#c0c0c0').css('background-color', '#ffffff').bind("click", function (e) {
                                        e.preventDefault();
                                        return false;
                                    });
                                }
                                if ((complete !== null && complete === "Complete") || (dtlong > dtnow)) { // time zone offset?
                                    $(callButton).css('cursor', 'default').attr('pointer-events', 'none').css('color', '#c0c0c0').css('background-color', '#ffffff').bind("click", function (e) {
                                        e.preventDefault();
                                        return false;
                                    });
                                }
                            }
                        },
                        columns: [
                            {
                                field: "ClientName",
                                title: "Client Name",
                                width: "100px",
                                attributes: { "class": "ellipse", "title": "#=ClientName#" }
                            }, {
                                field: "Title",
                                title: "Title",
                                width: "100px",
                                attributes: { "class": "ellipse", "title": "#=Title#" }
                            }, {
                                field: "WorkPhone",
                                title: "Work Phone",
                                width: "100px",
                                attributes: { "class": "ellipse", "title": "#=WorkPhone#" }
                            },
                            //{
                            //  field: "LeadId",
                            //  title: "LeadId",
                            //  width: "70px",
                            //  attributes: { "class": "ellipse", "title": "CallQueueDetailId=#=CallQueueDetailId#" }
                            //},
                            {
                                field: "TZ",
                                title: "TZ",
                                width: "60px",
                                attributes: { "class": "ellipse", "title": "#=TZ#" }
                            }, {
                                field: "CallOutcome",
                                title: "Status",
                                width: "80px",
                                attributes: { "class": "ellipse", "title": "#=CallOutcome#" }
                            }, {
                                field: "TotalScore",
                                title: "Score",
                                width: "80px",
                                attributes: { "class": "ellipse", "title": "#=TotalScore#" }
                            }, {
                                field: "StartDate",
                                title: "Callback When",
                                width: "110px",
                                template: function (data) {
                                    if (!((data.StartDate === null) || (data.StartDate === undefined) || (data.StartDate === EMPTY_STRING) || (data.CallOutcome.toLowerCase() !== "callback"))) {
                                        try {
                                            var dt = new Date(data.StartDate);
                                            return kendo.toString(new Date(dt.getTime() - dt.getTimezoneOffset() * 60000), "MM/dd/yyyy hh:mm tt");
                                        }
                                        catch (ex) {
                                            var x = 1;
                                        }
                                    }
                                    return EMPTY_STRING;
                                },
                                attributes: { "class": "ellipse", "title": "#=CompanyName#" }
                            }, {
                                command: [
                                    {
                                        name: "Select",
                                        click: function (e) {
                                            var tr = $(e.target).closest("tr");
                                            var data = this.dataItem(tr);
                                            $(".bootbox-close-button").click();
                                            sessionStorage.RecycleCount = data.Recycles;
                                            sessionStorage.RecycleLevel = data.RecycleLevel;
                                            sessionStorage.MultiCompanyName = data.CompanyName;
                                            sessionStorage.RecycleMinCount = data.RecycleMinCount;
                                            LLCommon.NextCallQueue(firstTime, data.LeadId, EMPTY_STRING, newWindow, data.CallQueueDetailId, "Call");
                                        }
                                    }

                                ], title: SPACE_CHARACTER, width: 70 //135
                            }
                        ]
                    });

                    //Edit grid row on double click
                    //$('#CbCompanyGrid').on('dblclick', 'tr', function (e) {
                    //  if ($(".k-grid-edit-row").length <= 0) {
                    //      $("#CbCompanyGrid").data("kendoGrid").editRow($("#CbCompanyGrid tr:eq(" + ($(this).index() + 1) + ")"));
                    //  }
                    //});
                });
            });
        }
    };  // LLCommon.CompanyCallQueueList


    LLCommon.FinalCallQueue = function ( which, noRemove, allowClose )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        $.ajax({
            url: _llAppPath + "Sales/FinalCallQueue",
            data: {
                "Which": ((window.location.href.indexOf('FL=true') === -1) ? which : which + "NoRecycle"),
                "CallQueueHeaderId": sessionStorage.CallQueueHeaderId,
                "PrevDetailId": sessionStorage.PrevCallQueueDetailId,
                "CallDisposition": sessionStorage.PrevCallDisposition,
                "Complete": sessionStorage.PrevCallComplete,
                "CallBackTime": (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime),
                "TimeZoneSelections": LLCommon.timeZones
            },
            type: "GET",
            success: function (data) {

                if (noRemove === undefined) {
                    sessionStorage.AllowExit = "true";
                    sessionStorage.removeItem("PrevCallQueueDetailId");
                    sessionStorage.removeItem("CalbackTime");
                    sessionStorage.removeItem("PrevCallDisposition");
                    sessionStorage.removeItem("PrevCallComplete");
                }

                if (allowClose === undefined) {
                    window.close();
                }
            },
            error: function (xhr) {
                alert('ERROR Contact Click:' + xhr);
            }
        });
    };  // LLCommon.FinalCallQueue


    LLCommon.getParameterByName = function ( name )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        name                = name.replace ( /[\[]/, "\\[" ).replace ( /[\]]/ , "\\]" );
        var regex           = new RegExp ( "[\\?&]" + name + "=([^&#]*)" ),
            results         = regex.exec(location.search);

        return results === null ? EMPTY_STRING : decodeURIComponent ( results [ 1 ].replace ( /\+/g , SPACE_CHARACTER ) );
    };  // LLCommon.getParameterByName


    LLCommon.NextCallQueue = function ( firstTime, leadId, msg, newWindow, CallQueueDetailId, DupCall, isSkip )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var   whereTo;

        if ( firstTime !== undefined && firstTime === 'firstTime' )
        {
            whereTo = _llAppPath + 'Sales/NextCallQueue?Which=NextCallQueueFirstTime&CallQueueHeaderId=' + sessionStorage.CallQueueHeaderId + "&CallQueueDetailId=" + CallQueueDetailId + "&First=True" + "&TimeZoneSelections=" + LLCommon.timeZones + "&IsSkip=" + ( isSkip === undefined ? false : true );
        }   // TRUE block, if ( firstTime !== undefined && firstTime === 'firstTime' )
        else
        {
            if ( firstTime !== undefined && firstTime === 'FL' )
            {
                $.ajax({
                    url: _llAppPath + "Sales/FinalCallQueue",
                    data: {
                        "Which": "NextCallQueueFind/AddLeadNoRecycle",
                        "CallQueueHeaderId": sessionStorage.CallQueueHeaderId,
                        "PrevDetailId": sessionStorage.PrevCallQueueDetailId,
                        "CallDisposition": sessionStorage.PrevCallDisposition,
                        "Complete": sessionStorage.PrevCallComplete,
                        "CallBackTime": (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime),
                        "TimeZoneSelections": LLCommon.timeZones
                    },
                    type: "GET",
                    success: function (data) {
                        sessionStorage.removeItem("PrevCallQueueDetailId");
                        sessionStorage.removeItem("CalbackTime");
                        sessionStorage.removeItem("PrevCallDisposition");
                        sessionStorage.removeItem("PrevCallComplete");
                        window.close();

                    },
                    error: function (xhr) {
                        alert('ERROR Contact Click:' + xhr);
                    }
                });

                return;
            }   // TRUE block, if ( firstTime !== undefined && firstTime === 'FL' )
            else
            {
                if (DupCall !== undefined) {
                    whereTo = _llAppPath + "Sales/NextCallQueue?Which=NextCallQueueDupCall&CallQueueHeaderId=" + sessionStorage.CallQueueHeaderId + "&PrevDetailId=" + sessionStorage.PrevCallQueueDetailId + "&CallDisposition=" + sessionStorage.PrevCallDisposition + "&Complete=" + sessionStorage.PrevCallComplete + "&CallQueueDetailId=" + CallQueueDetailId + "&CallBackTime=" + (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime) + "&CallBackId=" + CallQueueDetailId + "&CallBackLeadId=" + leadId + "&TimeZoneSelections=" + LLCommon.timeZones; ///////------
                }
                else {
                    whereTo = _llAppPath + "Sales/NextCallQueue?Which=NextCallQueueNotDupCall&CallQueueHeaderId=" + sessionStorage.CallQueueHeaderId + "&PrevDetailId=" + sessionStorage.PrevCallQueueDetailId + "&CallDisposition=" + sessionStorage.PrevCallDisposition + "&Complete=" + sessionStorage.PrevCallComplete + "&CallBackTime=" + (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime) + "&CallBackId=" + CallQueueDetailId + "&CallBackLeadId=" + leadId + "&TimeZoneSelections=" + LLCommon.timeZones;
                }
            }   // FALSE block, if ( firstTime !== undefined && firstTime === 'FL' )
        }   // FALSE block, if ( firstTime !== undefined && firstTime === 'firstTime' )

        $.get(whereTo, function (data)
        {
            var idS = data.split ( "|" ); //[0] LeadId //[1] PrevCall Detail Id //[2] Current Call Disposition//[3] Company Name
            sessionStorage.AllowExit = "true";

            if ( ( idS.length !== 4 ) || isNaN ( idS [ 1 ] ) )
            {
                LLCommon.CallQueueError(data);
            }
            else
            {
                sessionStorage.PrevCallQueueDetailId = idS[1];
                sessionStorage.PrevCallDisposition = sessionStorage.PrevCallComplete = sessionStorage.CalbackTime = EMPTY_STRING;
                sessionStorage.CurrentCallDisposition = idS[2];

                if ( idS [ 3 ].trim ( ).length > 0 )
                {
                    sessionStorage.MultiContact = idS [ 3 ];
                }

                if ( newWindow === undefined )
                {
                    window.location.href = _llAppPath + 'Sales?leadId=' + idS[0];
                }
                else
                {
                    if ( newWindow === 'none' )
                    {
                        return;
                    }
                    else
                    {
                        window.open ( _llAppPath + 'Sales?leadId=' + idS [ 0 ] );
                    }
                }
            }
        });
    };  // LLCommon.NextCallQueue


    LLCommon.CallQueueError = function ( data )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        localStorage.removeItem ( 'callQueueLaunched' );

        alert ( ( data === null ) ? 'Call queue has not been configured for this call campaign and agent' : data );

        if ( window.location.href.indexOf ( 'AgentCallQueueSelection' ) < 0 )
        {
            setTimeout ( function ( )
            {
                const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

                if ( window.location.href.indexOf ( _llAppPath + 'Sales?' ) >= 0 )
                {
                    window.close ( );
                }
                else
                {
                    window.location.href = _llAppPath + 'TargetAudience/AgentCallQueueSelection';
                }
            }, 5000 );
        }
    };  // LLCommon.CallQueueError


    LLCommon.safeString = function ( pstrString2MakeSafe , pstrQuoteChar )
    {
        const  strQuoteChar  = ( pstrQuoteChar === 'undefined' || pstrQuoteChar === null ) ? EMPTY_STRING : pstrQuoteChar;
        const  strSafeString = pstrString2MakeSafe === 'undefined' ? 'undefined' : pstrString2MakeSafe == null ? 'null' : pstrString2MakeSafe;
        return strQuoteChar + strSafeString + strQuoteChar;
    }   // LLCommon.safeString


    LLCommon.safeDivide = function ( num, denom )
    {
        return denom === 0 ? 0 : num / denom;
    };  // LLCommon.safeDivide


    LLCommon.printGrid = function ( gridName )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var   gridElement = $( HASH_CHARACTER + gridName ),
              printableContent = EMPTY_STRING,
              win = window.open ( EMPTY_STRING , EMPTY_STRING , 'width=800, height=500' ),
              doc = win.document.open ( );

        var htmlStart =
            '<!DOCTYPE html>' +
            '<html>' +
            '<head>' +
            '<meta charset="utf-8" />' +
            '<title>Kendo UI Grid</title>' +
            '<link href="//kendo.cdn.telerik.com/' + kendo.version + '/styles/kendo.common.min.css" rel="stylesheet" /> ' +
            '<style>' +
            'html { font: 11pt sans-serif; }' +
            '.k-grid { border-top-width: 0; }' +
            '.k-grid, .k-grid-content { height: auto !important; }' +
            '.k-grid-content { overflow: visible !important; }' +
            'div.k-grid table { table-layout: auto; width: 100% !important; }' +
            '.k-grid .k-grid-header th { border-top: 1px solid; }' +
            '.k-grid-toolbar, .k-grid-pager > .k-link { display: none; }' +
            '</style>' +
            '</head>' +
            '<body>';

        var htmlEnd =
            '</body>' +
            '</html>';

        var gridHeader = gridElement.children ( '.k-grid-header' );

        if ( gridHeader [ 0 ] )
        {
            var thead = gridHeader.find ( 'thead' ).clone ( ).addClass ( 'k-grid-header' );
            printableContent = gridElement
                .clone ( )
                .children ( '.k-grid-header' ).remove ( )
                .end ( )
                .children ( '.k-grid-content' )
                .find ( 'table' )
                .first ( )
                .children ( 'tbody' ).before ( thead )
                .end ( )
                .end ( )
                .end ( )
                .end ( ) [ 0 ].outerHTML;
        }
        else
        {
            printableContent = gridElement.clone()[0].outerHTML;
        }

        doc.write ( htmlStart + printableContent + htmlEnd );
        doc.close ( );
        win.print ( );
    }; // LLCommon.printGrid - Needs work only prints visible rows.


    LLCommon.exportGridToExcel = function ( gridId, fileName )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        var   grid             = $("#" + gridId).data("kendoGrid");
        var   originalPageSize = grid.dataSource.pageSize();
        var   csv              = EMPTY_STRING;
        fileName               = fileName || 'download.csv';

        // Increase page size to cover all the data and get a reference to that data.

        grid.dataSource.pageSize ( grid.dataSource.view ( ).length );
        // var data = grid.dataSource.view();
        var data = grid.dataSource.data ( );

        // Add the header row.

        for ( var i = 0; i < grid.columns.length; i++ )
        {
            var field = grid.columns [ i ].field;
            var title = grid.columns [ i ].title || field;

            // NO DATA

            if ( ( !field ) || ( ( i > 3 ) && grid.columns [ i ].hidden ) )
            {
                continue;
            }

            title = title.replace ( /"/g , QUOTE_DOUBLE + QUOTE_DOUBLE );
            csv += QUOTE_DOUBLE + title + QUOTE_DOUBLE;

            if ( i < grid.columns.length - 1 )
            {
                csv += CSV_SEPARATOR_CHAR;
            }
        }   // for (var i = 0; i < grid.columns.length; i++) {

        csv += '\n';

        //  Add each row of data.

        for ( var row in data )
        {
            for ( i = 0; i < grid.columns.length; i++ )
            {
                var fieldName    = grid.columns [ i ].field;
                var template     = grid.columns [ i ].template;
                var exportFormat = grid.columns [ i ].exportFormat;

                // VALIDATE COLUMN
                if ( !fieldName ) continue;
                var value = EMPTY_STRING;

                if ( fieldName.indexOf ( '.' ) >= 0 )
                {
                    var properties = fieldName.split ( '.' );
                    var valuex = data [ row ];

                    for ( var j = 0; j < properties.length; j++ )
                    {
                        var prop = properties [ j ];
                        value = valuex [ prop ] || EMPTY_STRING;
                    }
                }
                else
                {
                    if ( ( typeof data [ row ] === 'function' ) || ( ( i > 3 ) && grid.columns [ i ].hidden ) )
                    {
                        continue;
                    }

                    if ( ( data [ row ] [ fieldName ] === undefined || data [ row ] [ fieldName ] === null ) && grid.columns [ i ].template !== undefined )
                    {
                        //Template Field
                        value = grid.columns[i].template(data[row]);
                    }
                    else if ( !isNaN ( data [ row ] [ fieldName ] ) )
                    {
                        value = ( ( data [ row ] [ fieldName]  + 0 ) === 0 ? CHARACTER_ZERO : data [ row ] [ fieldName] || EMPTY_STRING );
                    }
                    else if ( data [ row ] [ fieldName ] !== undefined )
                    {
                        value = data [ row ] [ fieldName ] || EMPTY_STRING;
                    }
                }   // if (fieldName.indexOf('.') >= 0) {

                //    if (value && template && exportFormat !== false) {
                //        value = $.isFunction(template)
                //            ? template(data[row])
                //            : kendo.template(template) (data[row]);
                //}

                value = value.toString ( ).replace ( /"/g , '""' ).replace ( /\n/g , SPACE_CHARACTER ).replace ( /\r/g , SPACE_CHARACTER).replace(/HTML_NBSP/g, SPACE_CHARACTER );

                while ( value.indexOf ( '<' ) === 0 && value.indexOf ( '>' ) > 0 )
                {
                    value = $(value).html ( );
                }   // while (value.indexOf("<") === 0 && value.indexOf(">") > 0) {

                if ( value === 'NaN' )
                {
                    value = EMPTY_STRING;
                }   // if (value === "NaN") {

                csv += QUOTE_DOUBLE + value + QUOTE_DOUBLE;

                if ( i < grid.columns.length - 1 )
                {
                    csv += CSV_SEPARATOR_CHAR;
                }   // if (i < grid.columns.length - 1) {
            }   // for (i = 0; i < grid.columns.length; i++) {

            csv += '\r\n';
        }   // for (var row in data) {

        // Reset datasource
        grid.dataSource.pageSize ( originalPageSize );

        // EXPORT TO BROWSER
        csv = csv.trim ( ) + '\r\n';
        var blob = new Blob ( [ csv ] , { type: 'text/csv;charset=utf-8' } ); //Blob.js
        saveAs ( blob , fileName ); //FileSaver.js
    };  // LLCommon.exportGridToExcel


    LLCommon.setActiveSalesButtons = function ( clear )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        $('.story-btn').removeClass('active');

        if ( clear === undefined )
        {
            if ( localStorage.activeView === undefined )
            {
                var i = 0;
            }
            else
            {
                $('#btn' + localStorage.activeView).addClass('active');
            }   // if (localStorage.activeView === undefined) {
        }   // if (clear === undefined) {
    };  // LLCommon.setActiveSalesButtons


    LLCommon.exportTableToCSV = function ( $table, filename )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        // example call
        //    $("#exportLink").on('click', function (event) {
        //        exportTableToCSV.apply(this, [$('#reportGrid'), reportdesc + '.csv']);
        //    });

        var $rows = $table.find ( 'tr:has(th,td)' ),

            // Temporary delimiter characters unlikely to be typed by keyboard
            // This is to avoid accidentally splitting the actual contents
            tmpColDelim = String.fromCharCode ( 11 ), // vertical tab character
            tmpRowDelim = String.fromCharCode ( 0 ),  // null character

            // actual delimiter characters for CSV format
            colDelim = '","',
            rowDelim = '"\r\n"',

            // Grab text from table into CSV formatted string
            csv = QUOTE_DOUBLE + $rows.map ( function ( i , row )
            {
                var $row = $(row),
                    $cols = $row.find ( 'th,td' );

                return $cols.map ( function ( j , col )
                {
                    var $col = $(col),
                        text = $col.text ( );

                    return text.replace ( QUOTE_DOUBLE , QUOTE_DOUBLE + QUOTE_DOUBLE ); // escape double quotes

                }).get().join ( tmpColDelim );

            }).get ( ).join ( tmpRowDelim )
                .split ( tmpRowDelim).join ( rowDelim )
                .split ( tmpColDelim).join ( colDelim ) + QUOTE_DOUBLE,

            // Data URI
            csvData = 'data:application/csv;charset=utf-8,' + encodeURIComponent ( csv );

        $(this)
            .attr({
                'download': filename,
                'href'    : csvData,
                'target'  : '_blank'
            });
    };  // LLCommon.exportTableToCSV


    LLCommon.exportRawGridToExcel = function ( gridId, fileName )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        var   grid             = $("#" + gridId).data ( "kendoGrid" );
        var   originalPageSize = grid.dataSource.pageSize ( );
        var   csv              = EMPTY_STRING;
        fileName               = fileName || 'download.csv';

        // Increase page size to cover all the data and get a reference to that data.

        grid.dataSource.pageSize ( grid.dataSource.view ( ).length );

        // var data = grid.dataSource.view();

        var data = grid.dataSource.data ( );

        // Add each row of data.

        for ( var row in data )
        {
            for ( var i = 0; i < grid.columns.length; i++ )
            {
                var fieldName    = grid.columns [ i ].field;
                var template     = grid.columns [ i ].template;
                var exportFormat = grid.columns [ i ].exportFormat;

                // VALIDATE COLUMN
                if ( !fieldName ) continue;
                var value = EMPTY_STRING;
                if ( fieldName.indexOf ( '.' ) >= 0 )
                {
                    var properties = fieldName.split ( '.' );
                    var valuex = data [ row ];
                    for ( var j = 0; j < properties.length; j++ )
                    {
                        var prop = properties [ j ];
                        value    = valuex [ prop ] || EMPTY_STRING;
                    }
                }
                else
                {
                    if ( !isNaN ( data [ row ] [ fieldName ] ) )
                    {
                        value = ( ( data [ row ] [ fieldName ] + 0 ) === 0 ? CHARACTER_ZERO : data [ row ] [ fieldName ] || EMPTY_STRING );
                    }
                    else
                    {
                        value = data [ row ] [ fieldName ] || EMPTY_STRING;
                    }
                }
                if ( value && template && exportFormat !== false )
                {
                    value = $.isFunction ( template )
                        ? template ( data [ row ])
                        : kendo.template ( template )( data [ row ] );
                }
                csv += value;
            }
            csv += '\r\n';
        }

        // Reset datasource
        grid.dataSource.pageSize ( originalPageSize );

        // EXPORT TO BROWSER
        var blob = new Blob ( [ csv ] , { type: 'text/csv;charset=utf-8' } ); //Blob.js
        saveAs ( blob , fileName ); //FileSaver.js
    };  // LLCommon.exportRawGridToExcel


    LLCommon.ClearCallQueueState = function ( removeHeader )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        sessionStorage.removeItem ( 'AllowExit' );
        sessionStorage.removeItem ( 'CalbackTime' );

        if ( removeHeader !== undefined )
        {
            sessionStorage.removeItem ( 'CallQueueHeaderId' );
        }

        sessionStorage.removeItem ( 'IsInCallQueue' );
        sessionStorage.removeItem ( 'MiscellaneousCallQueueId' );
        sessionStorage.removeItem ( 'PrevCallComplete' );
        sessionStorage.removeItem ( 'PrevCallDisposition' );
        sessionStorage.removeItem ( 'PrevCallQueueDetailId' );
        sessionStorage.removeItem ( "MultiContact" );
    };  // LLCommon.ClearCallQueueState


    LLCommon.DisplayLeadSearch = function ( e )
    {
        const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

        //if (localStorage.callQueueLaunched !== undefined) {
        //    $.notifyBar({
        //        cssClass: "warning",
        //        html: "Please close open call queue before trying this.",
        //        delay: 5000
        //    });
        //    return false;
        //}

        if ( ( e !== undefined && e.id === 'aQfindLead') || ( sessionStorage.IsInCallQueue !== undefined ) )
        {
            $('#navMain ul').hide ( );

            isInCallQueue = true;

            if ( sessionStorage.MiscellaneousCallQueueId !== undefined )
            {
                sessionStorage.CallQueueHeaderId = sessionStorage.MiscellaneousCallQueueId;
            }
        }

        LLCommon.wireUpLeadSearchAutoComplete ( );

        $("#leadSearchDialog").kendoWindow({
            modal: true,
            width: "1170px",
            title: "Find Contacts",
            appendTo: "#leadSearchForm",
            open: function (e) {
                $("#emailAutocomplete").val(EMPTY_STRING);
                $("#companyNamelAutocomplete").val(EMPTY_STRING);
                $("#lastNameAutocomplete").val(EMPTY_STRING);
                $("#phoneAutocomplete").val(EMPTY_STRING);
                this.wrapper.css({ top: 100, left: '5%' });
            },
            close: function (e) {
                if (isInCallQueue) {
                    $('#navMain ul').show();
                    //$('#btnAddLead').attr('onclick', $('#btnAddLead').attr('onclick').toString().replace(sessionStorage.CallQueueHeaderId, sessionStorage.MiscellaneousCallQueueId));
                }
                $('#leadSearchGrid').empty();
                $('#leadSearchGrid').hide();
                $("#leadSearchDialog").data("kendoWindow").setOptions({
                    width: "1170px"
                });
            }
        });

        $("#leadSearchDialog").data("kendoWindow").open();
        $("#leadSearchDialog").data("kendoWindow").center();

    };  // LLCommon.DisplayLeadSearch


    $(document).off('click', '#leadSearchForm .k-i-close');
    $(document).on('click', '#leadSearchForm .k-i-close', function () {
        LLCommon.CloseLeadSearch();
    });


    LLCommon.CloseLeadSearch = function ( )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        var dialog          = $( '#leadSearchDialog' ).data ( 'kendoWindow' );
        dialog.close ( );
    };  // LLCommon.CloseLeadSearch


    LLCommon.ApplyLeadSearchCriteria = function ( )
    {
        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        var   previewButtonText = "Edit Contact";
        var   email             = $("#emailAutocomplete").data ( "kendoAutoComplete" ).value ( );
        var   companyname       = $("#companyNamelAutocomplete").data ( "kendoAutoComplete" ).value ( );
        var   lastname          = $("#lastNameAutocomplete").data ( "kendoAutoComplete" ).value ( );
        var   workphone         = $("#phoneAutocomplete").data ( "kendoAutoComplete" ).value ( );

        if ( ($.trim ( email + companyname + lastname + workphone ) ).length === 0 )
        {
            // Required at least one field to be filled in
            alert ( 'Please specify at least one value to search on' );

            return false;
        }

        if ( isInCallQueue )
        {
            previewButtonText = "Preview";
        }

        $("#leadSearchDialog").data("kendoWindow").setOptions({
            width: "95%", height: "580px"
        });
        $("#leadSearchDialog").data('kendoWindow').wrapper.css({ top: 100 });

        var loadurl;

        if ( isInCallQueue )
        {
            var agentId = sessionStorage.CallQueueAgentId === undefined ? $('#hdnCurrentUser').val() : sessionStorage.CallQueueAgentId;
            loadurl = _llAppPath + 'case/GetCallQueueContactMatches?AgentId=' + agentId;

        }
        else
        {
            loadurl = _llAppPath + 'case/GetContactMatches';
        }

        var command = EMPTY_STRING;

        if ( localStorage.callQueueLaunched !== undefined )
        {
            loadurl = _llAppPath + 'case/GetContactMatches';
        }
        else
        {
            command = [{
                name: previewButtonText,
                click: LLCommon.previewLeadClick
            },
            {
                name: "Select",
                click: function (e) {
                    var tr = $(e.target).closest("tr");
                    var data = this.dataItem(tr);

                    if (isInCallQueue) {
                        sessionStorage.removeItem("MultiContact");
                        sessionStorage.CallQueueHeaderId = data.CallQueueHeaderId;
                        sessionStorage.CallQueueName = data.CallQueueName;
                        sessionStorage.PrevCallQueueDetailId = EMPTY_STRING;
                        sessionStorage.PrevCallDisposition = sessionStorage.PrevCallComplete = sessionStorage.CalbackTime = EMPTY_STRING;

                        $.ajax({
                            url: _llAppPath + "Sales/NextCallQueue",
                            data: {
                                "Which": "ApplyLeadSearchCriteria",
                                "CallQueueHeaderId": sessionStorage.CallQueueHeaderId,
                                "PrevDetailId": sessionStorage.PrevCallQueueDetailId,
                                "CallDisposition": sessionStorage.PrevCallDisposition,
                                "Complete": sessionStorage.PrevCallComplete,
                                "CallBackTime": (sessionStorage.CalbackTime === undefined ? EMPTY_STRING : sessionStorage.CalbackTime),
                                "CallQueueDetailId": data.CallQueueDetailId,
                                "CallBackLeadId": data.leadid,
                                "TimeZoneSelections": LLCommon.timeZones
                            },
                            type: "GET",
                            success: function (data) {
                                var idS = data.split("|"); // [0] LeadId // [1] PrevCall Detail Id // [2] Current Call Disposition // [3] Company Name
                                sessionStorage.AllowExit = "true";

                                if ((idS.length !== 4) || isNaN(parseInt(idS[0]))) {

                                    LLCommon.CallQueueError(data);

                                }
                                else {
                                    //sessionStorage.MultiContact;
                                    sessionStorage.PrevCallQueueDetailId = idS[1];
                                    sessionStorage.PrevCallDisposition = sessionStorage.PrevCallComplete = sessionStorage.CalbackTime = EMPTY_STRING;
                                    sessionStorage.CurrentCallDisposition = idS[2];

                                    if (idS[3].trim().length > 0) {
                                        sessionStorage.MultiContact = idS[3];
                                    }   // if (idS[3].trim().length > 0) {

                                    window.open(_llAppPath + 'Sales?leadId=' + idS[0] + '&FL=true');
                                }   // if ((idS.length !== 4) || isNaN(parseInt(idS[0]))) {
                            },
                            error: function (xhr) {
                                LLCommon.CallQueueError(xhr);
                            }
                        });
                    }
                    else {
                        grid = $('#leadSearchGrid').data("kendoGrid");
                        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));

                        isSalesPage = (window.location.pathname === '/Sales');
                        if (isSalesPage) {
                            LLCommon.CloseLeadSearch();
                            $('#btnHdnSalesLaunch').data('WhereTo', _llAppPath + 'Sales?leadId=' + dataItem.leadid).click();
                        }
                        else {
                            window.open(_llAppPath + 'Sales?leadId=' + dataItem.leadid + '&FL=true');
                        }
                    }

                }
            }];
        }

        // Define columns names to retrieve here in a comma delimited list.

        var columnnames = "[email], [companyname], [firstname], [lastname], [workphone], [OfficeCity], [Officestate]";
        var gridColumns = [
            //{ field: "CallQueueHeaderId", title: "CallQueueHeaderId", attributes: {"class": "ellipse", "title": "#=CallQueueHeaderId#"} },
            //{ field: "CallQueueDetailId", title: "CallQueueDetailId", attributes: {"class": "ellipse", "title": "#=CallQueueDetailId#"} },
            {
                field: "companyname",
                title: "Company Name",
                width: "140px",
                attributes: { "class": "ellipse", "title": "#=companyname#" }
            }, {
                field: "email",
                title: "Email Address",
                attributes: { "class": "ellipse", "title": "#=email#" }
            }, {
                field: "firstname",
                title: "First Name",
                attributes: { "class": "ellipse", "title": "#=firstname#" }
            }, {
                field: "lastname",
                title: "Last Name",
                attributes: { "class": "ellipse", "title": "#=lastname#" }
            }, {
                field: "workphone",
                title: "Work Phone ",
                attributes: { "class": "ellipse", "title": "#=workphone#" }
            }, {
                field: "OfficeCity",
                title: "City",
                width: "100px",
                attributes: { "class": "ellipse", "title": "#=OfficeCity#" }
            }, {
                field: "Officestate",
                title: "State",
                width: "80px",
                attributes: { "class": "ellipse", "title": "#=Officestate#" }
            },
            {
                command: command, title: SPACE_CHARACTER, width: "160px"
            }
        ];

        $.ajax({
            url: loadurl,
            data: JSON.stringify({ email: email, companyname: companyname, lastname: lastname, workphone: workphone, columnnames: columnnames, MaxRows: MaxRows }),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, jqXHR) {

                email = EMPTY_STRING;
                companyname = EMPTY_STRING;
                lastname = EMPTY_STRING;
                workphone = EMPTY_STRING;
                columnnames = EMPTY_STRING;

                if (data.total === 0) {
                    $('#leadSearchGridrNoDataFound').show();
                    $("#leadSearchGrid").hide();
                }
                else if (data.total === MaxRows) {
                    alert('The maximum number of rows (' + MaxRows + ') was returned; more matches may exist');
                }
                else {
                    $('#leadSearchGridrNoDataFound').hide();
                    $("#leadSearchGrid").show();
                }

                $("#leadSearchGrid").off('click');

                if ($("#leadSearchGrid").data('kendoGrid') !== null) {
                    $("#leadSearchGrid").removeData('kendoGrid');
                    $("#leadSearchGrid").empty();
                }

                if (isInCallQueue) {
                    gridColumns.unshift({ "field": "CallQueueName", "title": "Call Queue", "attributes": { "class": "ellipse", "title": "#=CallQueueName# - #=CallQueueHeaderId#:#=CallQueueDetailId#" } });
                }

                $("#leadSearchGrid").kendoGrid({
                    theme: $(document).data("kendoSkin") || "silver",
                    height: "325px",
                    columns: gridColumns,
                    pageable: true,
                    filterable: true,
                    sortable: true,
                    resizable: true,
                    dataSource: {
                        data: data,
                        sort: [{ field: "companyname", dir: "asc" }, { field: "email", dir: "asc" }],
                        pageSize: 1000,
                        schema: {
                            data: "data",
                            total: "total"
                        }
                    }
                });

                // Edit grid row on double click.

                $('#leadSearchGrid').on('dblclick', 'tr', function (e) {
                    if ($(".k-grid-edit-row").length <= 0) {
                        $("#leadSearchGrid").data("kendoGrid").editRow($("#leadSearchGrid tr:eq(" + ($(this).index() + 1) + ")"));
                    }
                });
            }
        });
    };  // LLCommon.ApplyLeadSearchCriteria


    LLCommon.previewLeadClick = function (e) {

        grid = $('#leadSearchGrid').data("kendoGrid");
        var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));

        if (isInCallQueue) {
            sessionStorage.removeItem("IsInCallQueue");
            var Qid = (sessionStorage.removeItem("IsInCallQueue") === undefined) ? sessionStorage.MiscellaneousCallQueueId : sessionStorage.CallQueueHeaderId;
            window.open(_llAppPath + 'Lead/Edit/' + dataItem.leadid + "?QId=" + dataItem.CallQueueHeaderId + "&DId=" + dataItem.CallQueueDetailId);
        }
        else {
            window.open(_llAppPath + 'Lead/Edit/' + dataItem.leadid);
        }
    };  // LLCommon.previewLeadClick


    LLCommon.wireUpLeadSearchAutoComplete = function ( ) {

        var agentId   = sessionStorage.CallQueueAgentId === undefined ? $('#hdnCurrentUser').val() : sessionStorage.CallQueueAgentId;

        var loadurl   = _llAppPath + 'Case/GetLeadsBy' + (isInCallQueue ? "CallQueue?AgentId=" + agentId : EMPTY_STRING);
        var minLength = 2;

        var emaildataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: {
                    url: loadurl,
                    dataType: "json"
                },
                parameterMap: function (data, action) {
                    return { param: 'email', value: document.activeElement.value };
                }
            }
        });

        var companydataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: {
                    url: loadurl,
                    dataType: "json"
                },
                parameterMap: function (data, action) {
                    return { param: 'companyname', value: document.activeElement.value };
                }
            }
        });

        var lastNamedataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: {
                    url: loadurl,
                    dataType: "json"
                },
                parameterMap: function (data, action) {
                    return { param: 'lastname', value: document.activeElement.value };
                }
            }
        });

        var phonedataSource = new kendo.data.DataSource({
            serverFiltering: true,
            transport: {
                read: {
                    url: loadurl,
                    dataType: "json"
                },
                parameterMap: function (data, action) {
                    return { param: 'workphone', value: document.activeElement.value };
                }
            }
        });

        if ($("#emailAutocomplete").data('kendoAutoComplete') === undefined) {
            $("#emailAutocomplete").kendoAutoComplete({
                placeholder: "Enter email address ...",
                filter: "startswith",
                dataTextField: "value",
                minLength: minLength,
                dataSource: emaildataSource
            });
        }

        if ($("#companyNamelAutocomplete").data('kendoAutoComplete') === undefined) {
            $("#companyNamelAutocomplete").kendoAutoComplete({
                placeholder: "Enter company name ...",
                filter: "startswith",
                dataTextField: "value",
                minLength: minLength,
                dataSource: companydataSource
            });
        }

        if ($("#lastNameAutocomplete").data('kendoAutoComplete') === undefined) {
            $("#lastNameAutocomplete").kendoAutoComplete({
                placeholder: "Enter last name ...",
                filter: "startswith",
                dataTextField: "value",
                minLength: minLength,
                dataSource: lastNamedataSource
            });
        }

        if ($("#phoneAutocomplete").data('kendoAutoComplete') === undefined) {
            $("#phoneAutocomplete").kendoAutoComplete({
                placeholder: "Enter company phone number ...",
                filter: "startswith",
                dataTextField: "value",
                minLength: minLength,
                dataSource: phonedataSource
            });
        }
    };  // LLCommon.wireUpLeadSearchAutoComplete


    LLCommon.BuildFormInputs = function ( obj , element , display , data )
    {
        const strMethodName               = LLCommon.GetNameOfCurrentFunction ( );

        var   bindings                    = new Array ( );
        var   me                          = EMPTY_STRING;
        var   table                       = EMPTY_STRING;
        var   useDropDownOverAutoComplete = false;

        // Check to see if we should be using autocomplete or dropdown.
        $.ajaxSetup({ async: false });
        var loadurl = _llAppPath + 'Case/GetOptionByName?KeyWord=UseDropDownForAutoComplete';

        $.ajax({
            type: "POST",
            url: loadurl,
            success: function (data, status, jqXHR) {
                useDropDownOverAutoComplete = ($.trim(data.toString()).length > 0);
            },
            dataType: "json"
        });

        $.each(obj, function (key, value) {

            var cntr = 0;
            table = key;

            if (!display || $.inArray(key, display) > -1) {
                $.each(value, function (k, v) {

                    if (v.Name) {
                        if (data) {
                            var lookingFor = v.Field;
                            var rowCntr = 0;

                            $.each(data, function (a, b) {
                                for (var i = 0; i < b.length; i++) {
                                    if (b[i].Field === lookingFor) {

                                        // If there is a default value and the field value is blank use it.

                                        if (ViewModel[key][cntr].DefaultValue !== undefined && $.trim(ViewModel[key][cntr].DefaultValue).length > 0 && $.trim(b[i].Value).length === 0) {
                                            LLCommon.setDefaults(key, cntr);
                                        }
                                        else
                                        {
                                            ViewModel[key][cntr].Value = b[i].Value;

                                            if (ViewModel[key][cntr].LocalTime !== undefined && b[i].Value !== null && b[i].Value.toString().length > 0) {
                                                ViewModel[key][cntr].LocalTime = LLCommon.convertTFormatToDateFormat(LLCommon.FormatIsoDate(parseInt(b[i].Value.substr(6)), 'MM/dd/yyyy hh:mm tt'));
                                            }
                                        }   // if (ViewModel[key][cntr].DefaultValue !== undefined && $.trim(ViewModel[key][cntr].DefaultValue).length > 0 && $.trim(b[i].Value).length === 0) {

                                        return false;
                                    }   // if (b[i].Field === lookingFor) {
                                }   // for (var i = 0; i < b.length; i++) {
                            });
                        }   // if (data) {

                        var isRequired = (v.Name.indexOf('*') > 0) ? '  required' : SPACE_CHARACTER;
                        var type = (v.Field.toLowerCase() === 'email') ? 'email' : 'text ';
                        var classes = SPACE_CHARACTER;
                        var style = SPACE_CHARACTER;
                        var attr = SPACE_CHARACTER;
                        var elementOverride = EMPTY_STRING;
                        var input = EMPTY_STRING;
                        var trstyle = SPACE_CHARACTER;
                        var search = EMPTY_STRING;

                        if (v.NoDisplay !== undefined)
                        {
                            trstyle = style = 'display: none';
                        }

                        if ( v.AllowSearch !== undefined ) {
                            search = '<button type="button" id="btn' + v.Field + '" style="border-radius:10px" class="sn-btn btn-xs buttonColor" data-toggle="collapse" data-target="collapse' + v.Field + '"><i class="fa fa-search"></i>Advanced</button>';
                        }

                        if ( v.AllowDropDown !== undefined && useDropDownOverAutoComplete ) {
                            v.Editor = 'dropdown';
                        }

                        if ( v.Editor !== undefined ) {
                            switch ( v.Editor.toLowerCase ( ) )
                            {
                                case 'autocomplete':
                                    bindings.push ( v.Field + '|autocomplete|' + v.URL + '|' + v.MinLength + '|' + v.Name + '|' + table );
                                    if ( v.Param !== undefined )
                                    {
                                        attr = 'data-param="' + v.Param + QUOTE_DOUBLE;
                                    }
                                    break;
                                case 'dropdown':
                                    bindings.push ( v.Field + '|dropdown|' + v.URL + '|' + v.Name + '|' + table );
                                    if (v.Param !== undefined) {
                                        attr = 'data-param="' + v.Param + QUOTE_DOUBLE;
                                    }
                                    break;
                                case 'datepicker':
                                    bindings.push ( v.Field + '|datepicker' );
                                    break;
                                case 'datetimepicker':
                                    bindings.push ( v.Field + '|datetimepicker' );
                                    elementOverride = '<input type="' + type + '" class="' + classes + '"name="' + v.Field + '" id="' + v.Field + '" style="width: 400px;' + style + '" data-bind="value:' + key + '[' + cntr + '].LocalTime" ' + isRequired + attr + '/>';
                                    break;
                                case 'textarea':
                                    // bindings.push(v.Field + '|editor|' + v.Tools);
                                    type = 'textarea';
                                    style = 'height: ' + v.Height;
                                    attr = ' Rows=' + v.Rows + ' Cols=' + v.Cols;
                                    elementOverride = '<textarea class="' + classes + '"name="' + v.Field + '" id="' + v.Field + '" style="width: 400px;' + style + '" data-bind="value:' + key + '[' + cntr + '].Value" ' + isRequired + attr + '/>';
                                    break;
                            }   // switch (v.Editor.toLowerCase()) {
                        }   // if (v.Editor !== undefined)

                        if ( v.UseId !== undefined )
                        {
                            attr += ( ' data-LinkedId="' + v.UseId + QUOTE_DOUBLE );
                        }

                        //, attr: {validationMessage:' + key + '[' + cntr + '].ValidatorMsg}

                        if ( $.trim ( elementOverride ).length === EMPTY_STRING_LENGTH )
                        {
                            input = '<input type="' + type + '" class="' + classes + '"name="' + v.Field + '" id="' + v.Field + '" style="width: 400px;' + style + '" data-bind="value:' + key + '[' + cntr + '].Value" ' + isRequired + attr + '/>';
                        }
                        else
                        {
                            input = elementOverride;
                        }

                        if ( $.trim ( search ).length > EMPTY_STRING_LENGTH )
                        {
                            input += ( SPACE_CHARACTER + HTML_NBSP + search );
                        }

                        var row = '<tr style="' + trstyle + '"><td  style="padding-bottom:10px">' + v.Name + '</td><td style="padding-bottom:10px">' + input + '</td></tr>' + ($.trim(search).length > 0 ? '<tr id="target' + v.Field + '"></tr>' : EMPTY_STRING);

                        $( HASH_CHARACTER + element ).append ( row );
                        cntr++;
                    }   // if (v.Name) {
                });
            }   // if (!display || $.inArray(key, display) > -1) {

            for ( var i = 0; i < bindings.length; i++ )
            {
                //Bind Kendo Objects

                var b = bindings [ i ].split ( '|' ); //Id is [0] - type is [1]

                switch ( b [ 1 ] )
                {
                    case 'datepicker':
                        $("#" + b[0]).kendoDatePicker({
                            animation: {
                                close: {
                                    effects: "fadeOut zoom:out",
                                    duration: 300
                                },
                                open: {
                                    effects: "fadeIn zoom:in",
                                    duration: 300
                                }
                            }
                        });
                        break;
                    case 'datetimepicker':
                        $("#" + b[0]).kendoDateTimePicker({
                            animation: {
                                close: {
                                    effects: "fadeOut zoom:out",
                                    duration: 300
                                },
                                open: {
                                    effects: "fadeIn zoom:in",
                                    duration: 300
                                }
                            }
                        });
                        break;
                    case 'autocomplete': // -- URL is [2] -- MinLength is [3] -- Field Name is [4] -- View Model Key Value is [5]
                        $( HASH_CHARACTER + b [ 0 ] ).attr ( 'title' , "Enter " + b [ 4 ] + " ..." );

                        $( "#" + b [ 0 ] ).kendoAutoComplete({
                            placeholder: "Enter " + b [ 4 ] + " ...",
                            filter: "startswith",
                            dataTextField: "Name",
                            minLength: b [ 3 ],
                            dataSource: {
                                serverFiltering: true,
                                transport: {
                                    read: {
                                        url: _llAppPath + b [ 2 ],
                                        dataType: "json"
                                    },
                                    parameterMap: function ( data , action )
                                    {
                                        var p = me.attr ( 'data-param' );
                                        if ( p !== undefined )
                                        {
                                            return { param: document.activeElement.value, opptionalParam: p };
                                        }
                                        else
                                        {
                                            return { param: document.activeElement.value };
                                        }
                                    }
                                }
                            },
                            select: function ( e ) {
                                var dataItem = this.dataItem ( e.item.index ( ) );
                                var linkedId = $( e.sender.element ).attr ( 'data-LinkedId' );

                                if ( linkedId !== undefined )
                                {
                                    LLCommon.SetViewModelValue ( [ b [ 5 ] ], linkedId, ( ( dataItem === undefined ) ? null : dataItem.id ) );
                                }
                            },
                            change: function ( e )
                            {
                                if ( $.trim ( this.value ( ) ).length === EMPTY_STRING_LENGTH )
                                {
                                    var linkedId = $( e.sender.element ).attr ( 'data-LinkedId' );
                                    if (linkedId !== undefined) {
                                        LLCommon.SetViewModelValue ( [ b [ 5 ] ],
                                                                     linkedId,
                                                                     null );
                                    }
                                }
                            }
                        }).focus(function ( e )
                        {
                            me = $( e.currentTarget );
                        });

                        if ( b [ 5 ] !== undefined )
                        {
                            $("#" + b [ 0 ] ).val ( b [ 5 ] );
                        }
                        break;
                    case 'dropdown': // -- URL is [2] -- MinLength is [3] -- Field Name is [4] -- View Model Key Value is [5]
                        $( "#" + b [ 0 ] ).kendoDropDownList ({
                            dataTextField        : "Name",
                            dataValueField       : "id",
                            dataSource           : {
                                serverFiltering: true,
                                transport        : {
                                    read         : {
                                        url      : _llAppPath + b [ 2 ],
                                        dataType : "json"
                                    },
                                    parameterMap : function ( data , action ) {
                                        var p = $( "#" + b [ 0 ] ).attr ( 'data-param' );
                                        if ( p !== undefined )
                                        {
                                            return { param: '%', opptionalParam: p };
                                        }
                                        else {
                                            return { param: '%' };
                                        }
                                    }
                                }
                            },
                            select: function ( e ) {
                                var dataItem = this.dataItem ( e.item.index ( ) );
                                var linkedId = $( e.sender.element ).attr ( 'data-LinkedId' );
                                LLCommon.SetViewModelValue ( [ b [ 4 ] ] ,
                                                             linkedId ,
                                                             ( ( dataItem === undefined ) ? null : dataItem.id ) );
                            }
                        });

                        var p = $( "#" + b [ 0 ] ).attr ( 'data-LinkedId' );
                        var ddlValue = LLCommon.GetViewModelValue ( [ b [ 4 ] ] , ( p === undefined ? b [ 0] : p ) );

                        if ( p !== undefined )
                        {
                            LLCommon.SetViewModelValue ( [ b [ 4 ] ] ,
                                                         b [ 0 ] ,
                                                         ddlValue );
                        }
                        break;
                    case 'editor':
                        $("#" + b [ 0 ] ).kendoEditor ({
                            tools: [
                                b [ 2 ]
                            ]
                        });
                        break;
                }   // switch (b[1]) {
            }   // for (var i = 0; i < bindings.length; i++) {
        });
    };  // LLCommon.BuildFormInputs


    LLCommon.FormatIsoDate = function ( dateColumn , format )
    {
        return ( dateColumn === null ) ? EMPTY_STRING : kendo.toString ( new Date ( dateColumn ).getTime ( ) - data.dateColumn.getTimezoneOffset ( ) * 60000 , format );

    };  // LLCommon.FormatIsoDate


    LLCommon.ClearViewModelValues = function ( tableNames )
    {
        $.each ( tableNames , function ( tablevalue , table )
        {
            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < ViewModel [ table ].length;
                      i++ )
            {
                // If there is a default value and the field value is blank, use it.
                if ( ViewModel [ table ] [ i ].DefaultValue !== undefined && $.trim ( ViewModel [ table ] [ i ].DefaultValue ).length > 0 )
                {
                    LLCommon.setDefaults ( table , i );
                }   // TRUE (A default value exists and the field value is blank.) block, if ( ViewModel [ table ] [ i ].DefaultValue !== undefined && $.trim ( ViewModel [ table ] [ i ].DefaultValue ).length > 0 )
                else
                {
                    ViewModel [ table ] [ i ].Value = EMPTY_STRING;

                    if ( ViewModel [ table ] [ i ].LocalTime !== undefined )
                    {
                        ViewModel [ table ] [ i ].LocalTime = EMPTY_STRING;
                    }
                }   // FALSE (Either the field value is not blank or the default value is missing.) block, if ( ViewModel [ table ] [ i ].DefaultValue !== undefined && $.trim ( ViewModel [ table ] [ i ].DefaultValue ).length > 0 )
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < ViewModel [ table ].length; i++ )
        });
    };  // LLCommon.ClearViewModelValues


    LLCommon.UpdateValuesFromLocalTimeToUTC = function ( tableNames )
    {
        $.each ( tableNames , function ( tablevalue , table )
        {
            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < ViewModel [ table ].length;
                      i++ )
            {
                if ( ViewModel [ table ] [ i ].LocalTime !== undefined )
                {
                    // Convert local time to UTC Time if its not already in Json format.

                    if ( ViewModel [ table ] [ i ].LocalTime !== null && ViewModel [ table ] [ i ].LocalTime.toString ( ).length > EMPTY_STRING_LENGTH )
                    {
                        if ( ViewModel [ table ] [ i ].LocalTime.toString ( ).indexOf ( 'Date' ) < 0 )
                        {
                            var isoDate = new Date ( ViewModel [ table ] [ i ].LocalTime ).toISOString ( );
                            ViewModel [ table ] [ i ].Value = isoDate;
                        }   // if ( ViewModel [ table ] [ i ].LocalTime.toString ( ).indexOf ( 'Date' ) < 0 )
                    }   // TRUE (anticipated outcome) block, if ( ViewModel [ table ] [ i ].LocalTime !== null && ViewModel [ table ] [ i ].LocalTime.toString ( ).length > EMPTY_STRING_LENGTH )
                    else
                    {
                        ViewModel [ table ] [ i ].LocalTime = ViewModel [ table ] [ i ].Value = new Date ( 0 );
                    }   // FALSE (unanticipated outcome) block, if ( ViewModel [ table ] [ i ].LocalTime !== null && ViewModel [ table ] [ i ].LocalTime.toString ( ).length > EMPTY_STRING_LENGTH )
                }   // if ( ViewModel [ table ] [ i ].LocalTime !== undefined )
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < ViewModel [ table ].length; i++ )
        });
    };  // LLCommon.UpdateValuesFromLocalTimeToUTC


    LLCommon.SetViewModelValue = function ( tableNames , field,  value )
    {
        $.each(tableNames, function ( tablevalue , table )
        {
            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < ViewModel [ table ].length;
                      i++ )
            {
                if ( ViewModel [ table ] [ i ].Field === field )
                {
                    ViewModel [ table ] [ i ].Value = value;
                }   // if ( ViewModel [ table ] [ i ].Field === field )
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < ViewModel [ table ].length; i++ )
        });
    };  // LLCommon.SetViewModelValue


    LLCommon.GetViewModelValue = function ( tableNames , field )
    {
        var foundValue = EMPTY_STRING;

        $.each ( tableNames , function ( tablevalue , table )
        {
            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < ViewModel [ table ].length;
                      i++ )
            {
                if ( ViewModel [ table ] [ i ].Field === field )
                {
                    foundValue = ViewModel [ table ] [ i ].Value;
                    return false;
                }   // if ( ViewModel [ table ] [ i ].Field === field )
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < ViewModel [ table ].length; i++ )
        });

        return foundValue;
    };  // LLCommon.GetViewModelValue


    LLCommon.GetViewModelFieldNames = function ( tableNames )
    {
        var fieldnames = SPACE_CHARACTER;

        $.each ( tableNames , function ( tablevalue , table )
        {
            for ( var i = ARRAY_FIRST_ELEMENT;
                      i < ViewModel [ table ].length;
                      i++ )
            {
                fieldnames = fieldnames + '[' + ViewModel [ table ] [ i ].Field + '],';
            }   // for ( var i = ARRAY_FIRST_ELEMENT; i < ViewModel [ table ].length; i++ )
        });

        return fieldnames.substr ( 0 , fieldnames.length - 1 );
    };  // LLCommon.GetViewModelFieldNames


    LLCommon.convertUTCDateToLocalDate = function ( date )
    {
        var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

        var offset = date.getTimezoneOffset() / 60;
        var hours = date.getHours();

        newDate.setHours(hours - offset);

        return newDate;
    };  // LLCommon.convertUTCDateToLocalDate


    LLCommon.convertTFormatToDateFormat = function ( strDate )
    {

        var dt = new Date(strDate);
        var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));

        return '/Date(' + newDate.getTime() + ')/';
    };  // LLCommon.convertTFormatToDateFormat


    LLCommon.convertToDateFormat = function ( strDate )
    {
        var dt = new Date(strDate);
        var newDate = new Date(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds());

        return '/Date(' + newDate.getTime() + ')/';
    };  // LLCommon.convertToDateFormat


    LLCommon.convertToUTCDateString = function ( strDate )
    {
        var dt = new Date(strDate);

        return (         dt.getUTCMonth ( ) + 101 ).toString ( ).substring ( 1 )
                 + "/" + ( dt.getUTCDate ( ) + 100 ).toString ( ).substring ( 1 )
                 + "/" + dt.getUTCFullYear ( )
                 +       SPACE_CHARACTER
                 +       ( dt.getUTCHours ( ) + 100 ).toString ( ).substring ( 1 )
                 + ":" + ( dt.getUTCMinutes ( ) + 100 ).toString ( ).substring ( 1 )
                 + ":" + ( dt.getUTCSeconds ( ) + 100 ).toString ( ).substring ( 1 );

    };  // LLCommon.convertToUTCDateString


    LLCommon.convertFromUTCDateString = function ( strDate )
    {
        var dt = new Date ( strDate );
        dt = new Date ( Date.UTC ( dt.getFullYear ( ),
                                   dt.getMonth ( ),
                                   dt.getDate ( ),
                                   dt.getHours ( ),
                                   dt.getMinutes ( ),
                                   dt.getSeconds ( ),
                                   dt.getMilliseconds ( ) ) );
        return (           dt.getMonth ( ) + 101 ).toString ( ).substring ( 1 )
                 + "/" + ( dt.getDate ( ) + 100 ).toString ( ).substring ( 1 )
                 + "/" + dt.getFullYear ( )
                 +       SPACE_CHARACTER
                 +       ( dt.getHours ( ) + 100 ).toString ( ).substring ( 1 )
                 + ":" + ( dt.getMinutes ( ) + 100 ).toString ( ).substring ( 1 )
                 + ":" + ( dt.getSeconds ( ) + 100 ).toString ( ).substring ( 1 );
    };  // LLCommon.convertFromUTCDateString


    LLCommon.convertToUTCDate = function ( value )
    {
        return ( new Date ( value ).toISOString ( ) );

    };  // LLCommon.convertToUTCDate


    LLCommon.setDefaults = function ( key , cntr )
    {
        switch ( ViewModel [ key ] [ cntr ].DefaultValue )
        {
            case 'CurrentDate':
                ViewModel[key][cntr].Value     = LLCommon.convertToDateFormat ( new Date ( ) );
                ViewModel[key][cntr].LocalTime = LLCommon.convertToDateFormat ( new Date ( ) );
                break;
            case 'CurrentUser':
                var loadurl = _llAppPath + 'Case/GetCurrentUser';
                $.ajax({
                    async: false,
                    type: "POST",
                    url: loadurl,
                    success: function (data, status, jqXHR) {
                        ViewModel [ key ] [ cntr - 1 ].Value = data [ 0 ].id;    // User ID
                        ViewModel [ key ] [ cntr ].Value = data [ 0 ].Name;      // Name: These must be next to each other serially in the ViewModel.
                    },
                    dataType: "json"
                });
                break;
        }   // switch (ViewModel[key][cntr].DefaultValue) {
    };  // LLCommon.setDefaults


    LLCommon.SynchronizeCalendar = function ( )
    {
        $.ajax({
            url: _llAppPath + 'Base/SynchronizeCalendar',
            success: function (data) {
                alert(data);
            }
        });
    };  // LLCommon.SynchronizeCalendar


    LLCommon.getTourName = function ( )
    {
        var activeTab = window.location.pathname.replace ( HASH_CHARACTER, EMPTY_STRING ) + "/////";
        var path = activeTab.split("/");
        var hostname = window.location.hostname.toLowerCase();
        var index = ((hostname === "localhost") || (hostname === "app.salesxtreme.com")) ? 1 : 2;
        activeTab = path[index];

        if ((activeTab === "Report") || (activeTab === "report") || (activeTab === "Sales") || (activeTab === "TargetAudience")) {
            activeTab = path[++index];
        }

        switch (activeTab.toLowerCase()) {
            case EMPTY_STRING:
                activeTab = ($('#btnAddTargetAudienceTour').length > 0) ? 'Leads' : ($('#btnCreateCampaignTour').length > 0) ? "Workflows" : EMPTY_STRING;
                break;
            case "campaign":
            case "workflow":
                activeTab = "Workflows";
                break;
            case "export":
                activeTab = "Export";
                break;
            case "import":
                activeTab = "Import";
                break;
            case "contact":
            case "contacts":
            case "lead":
                activeTab = "Leads";
                break;
            case "rule":
                activeTab = "Rules";
                break;
            case "salesnavigatoradmin":
                activeTab = "SalesNavigators";
                break;
            case "talkingpointsadmin":
                activeTab = "TalkingPoints";
                break;
        }   // switch (activeTab.toLowerCase()) {

        LLCommon.Trace("getTourName " + activeTab);

        return (activeTab + 'Tour');
    };  // LLCommon.getTourName


    LLCommon.startTour = function ( e )
    {
        var tourName = LLCommon.getTourName();
        if ($('#AssetTabStrip').length > 0) {
            tourName += 'Add' + $('#AssetTabStrip li.t-state-active a').text().replace(/\s/g, EMPTY_STRING);
        }
        else if ($('#btnSaveTalkingPoint').length > 0) {
            tourName += 'AddTalkingPoint';
        }
        else if ($('#navigatorName').length > 0) {
            tourName += 'AddNavigator';
        }
        else if ($('#CampaignManagerTabStrip').length > 0) {
            tourName += 'AddCampaign';
        }
        else if ($('#SaveRuleBtn').length > 0) {
            tourName += 'AddRule';
        }
        else if ($('#btnAddTargetAudienceTour').length > 0) {
            tourName += 'AddTargetAudience';
        }
        else if ($('#btnCreateCampaignTour').length > 0) {
            tourName += 'CreateCampaign';
        }
        else if (($('#choose').length > 0) && (tourName == 'Tour')) {
            tourName += 'ImportLead';
        }
        LLCommon.Trace("StartTour " + tourName);

        switch (tourName) {

            case 'TourAddEmailGeneral':
                tourName = 'AssetsTourAddEmailGeneral';
                break;
            case 'TourAddLandingPages':
                tourName = 'AssetsTourAddLandingPages';
                break;
            case 'TourAddTalkingPoint':
            case 'EditTalkingPointTourAddTalkingPoint':
                tourName = 'AssetsTourAddTalkingPoint';
                break;
            case 'TourAddNavigator':
                tourName = 'AssetsTourAddNavigator';
                break;
            case 'TourEmail':
            case 'EmailTour':
                tourName = 'AssetsTourEmail';
                break;
            case 'TourLandingPage':
            case 'LandingPageTour':
                tourName = 'AssetsTourAddLandingPages';
                break;
            case 'TourTalkingPoints':
            case 'TalkingPointsTour':
                tourName = 'AssetsTourTalkingPoints';
                break;
            case 'TourSalesNavigators':
            case 'TourSalesPlaybooks':
            case 'SalesNavigatorsTour':
            case 'SalesPlaybooksTour':
                tourName = 'AssetsTourSalesNavigators';
                break;
            case 'TourImportLead':
            case 'ImportLeadTour':
            case 'TourImport':
            case 'NewImportTour':
            case 'ImportTour':
                tourName = 'LeadsTourImportLead';
                break;
            case 'TourAddCampaign':
                tourName = 'CampaignsTourAddCampaign';
                break;
            case 'TourCreateCampaign':
                tourName = 'CampaignTourCreateCampaign';
                break;
            case 'TourAddTargetAudience':
                tourName = 'LeadsTourAddTargetAudience';
                break;
            case 'TourAddRule':
                tourName = 'RulesTourAddRule';
                break;
        }   // switch (tourName) {

        switch (tourName) {

            case 'AssetsTourEmail':
                $($('.fa-files-o')[0]).attr('id', 'copyEmails');
                $('#FolderView li span:contains("All Emails")').attr('id', 'AllEmails');
                $($('#FolderView li span')[0]).attr('id', 'createNewFolder');
                break;
            case 'AssetsTourAddEmailGeneral':
                $('#TabStrip li a[href="#TabStrip-1"]').click();
                $('#TabStrip li a[href="#TabStrip-1"]').attr('id', 'testEmail');
                break;
        }   // switch (tourName) {

        try {

            var defaultURL = EMPTY_STRING; // '<br /><a href="http://purl.salestalktech.com/SalesTalkHelp/SalesTalkHelpVideos" target="_blank">Learn More</a>';

            // Assets main tab

            var assetCreateNewEmailLink = defaultURL;
            var assetCopyExistingEmailLink = defaultURL;
            var assetViewAllEmailsLink = defaultURL;
            var assetCreateNewFolderLink = defaultURL;

            // Asset add an email

            var assetIdentifyEachEmailLink = defaultURL;
            var assetAddCommonLinksLink = defaultURL;
            var assetPersonalizeEmailsLink = defaultURL;
            var assetBeSureToSaveEmailsLink = defaultURL;
            var assetTestAnEmailLink = defaultURL;

            // Asset Landing Page

            var assetIdentifyLandingPageLink = defaultURL;
            var assetMaintainLandingPageLink = defaultURL;
            var assetBeSureToSaveLandingPageLink = defaultURL;
            var assetTestLandingPageLink = defaultURL;

            // Asset Talking Point Page

            var assetCreateNewTalkingPointLink = defaultURL;
            var assetCreateNewTalkingPointFolderLink = defaultURL;

            // Asset Talking Point Add Page

            var assetTalkingPointTitleLink = defaultURL;
            var assetTalkingPointColorLink = defaultURL;
            var assetTalkingPointSalesStageLink = defaultURL;
            var assetTalkingPointClickNoteLink = defaultURL;
            var assetTalkingPointQualifiedCallDispositionLink = defaultURL;
            var assetTalkingPointCRMNoteLink = defaultURL;
            var assetTalkingPointEditorLink = defaultURL;
            var assetBeSureToSaveTalkingPointLink = defaultURL;

            // Asset Navigator Point Page

            var assetCreateNewNavigatorLink = defaultURL;
            var assetCreateNewNavigatorFolderLink = defaultURL;

            // Asset Navigator Add Page

            var assetNavigatorNameLink = defaultURL;

            // Workflows Main Tab

            var beginNewCampaignLink = defaultURL;
            var copyAnExistingCampaignLink = defaultURL;

            // Workflows Tour Add Workflow

            var campaignCreateCampaignLink = defaultURL;
            var campaignCompleteCampaignLink = defaultURL;
            var campaignDocumentCampaignLink = defaultURL;
            var campaignProcessRulesLink = defaultURL;
            var campaignDeleteCampaignLink = defaultURL;
            var campaignLinkCampaignLink = defaultURL;
            var campaignScheduleCampaignLink = defaultURL;
            var campaignLaunchOrScheduleCampaignLink = defaultURL;
            var campaignIgnoreBusinessCalendarLink = defaultURL;
            var campaignSetEndDateLink = defaultURL;

            // Workflow Create New Workflow

            var campaignTourCreateCampaignNameCampaignLink = defaultURL;
            var campaignTourCreateCampaignAddDescriptionLink = defaultURL;
            var campaignTourCreateCampaignContinueDesigningLink = defaultURL;

            // Leads Main Tab

            var leadAddNewLeadLink = defaultURL;
            var leadsCreateTargetAudienceLink = defaultURL;
            var leadsEditTargetAudienceLink = defaultURL;
            var leadsMoveTargetAudienceLink = defaultURL;
            var leadsManageCallQueuesLink = defaultURL;
            var leadsImportLeadsLink = defaultURL;

            var leadAddNewTargetAudienceNameLink = defaultURL;
            var leadsBrowseToYourListLink = defaultURL;
            var leadsImportedFieldsLink = defaultURL;
            var leadsAddAFieldLink = defaultURL;
            var leadsAutoMapFieldsLink = defaultURL;
            var leadsConfirmImportLink = defaultURL;

            // LeadsTourAddTargetAudience

            var leadsTourAddTargetAudienceNameLink = defaultURL;
            var leadsTourAddTargetAudienceTypeLink = defaultURL;
            var leadsTourAddTargetAudienceOrganizeLink = defaultURL;
            var leadsTourAddTargetAudienceCriteriaLink = defaultURL;
            var leadsTourAddTargetAudienceMultipleAttributesLink = defaultURL;
            var leadsTourAddTargetAudienceFurtherCriteriaLink = defaultURL;
            var leadsTourAddTargetAudienceApplyCriteriaLink = defaultURL;
            var leadsTourAddTargetAudienceSaveCriteriaLink = defaultURL;

            // Rules Main Tab

            var ruleCreateNewRuleLink = defaultURL;
            var rulesFlagLink = defaultURL;

            var ruleAddNewRuleLink = defaultURL;
            var ruleTriggersLink = defaultURL;
            var ruleAdditionalTriggersLink = defaultURL;
            var ruleSpecifyActionsLink = defaultURL;

            // Visitor Insights Main Tab

            var visitorInsightsDateRange = defaultURL;
            var visitorJigSawRange = defaultURL;
            var visitorAnalyticsRange = defaultURL;

            // Assets Tab Tour

            var assetsTourEmail = {
                id: 'AssetsTourEmail',
                steps: [
                    {
                        title: 'Content Overview',
                        content: 'From the Content screens you are able to add/manage playbooks, email templates, landing pages and talking points.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Content%20Overview.mp4"target="_blank">Learn More</a>',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new email',
                        content: 'To create a new Email, click the Add tab.',
                        target: 'Add',
                        placement: 'bottom'
                    },
                    {
                        title: 'Copy an existing email',
                        content: 'To copy an existing Email, click the <span class="fa fa-files-o"></span> icon to the right of the Email information.',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'View all existing emails',
                        content: 'To view all of the Emails within your system, click on this button, and click on the All Emails tab.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new folder',
                        content: 'To create a new folder, click on this button, right click on an existing folder, and select New Folder.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var assetsTourAddEmailGeneral = {
                id: 'AssetsTourAddEmailGeneral',
                steps: [
                    {
                        title: 'Identify each email',
                        content: 'Click on the General tab to complete basic information. Fly-over help will guide you in this section.  NOTE:  You cannot send a Test Email without first filling out this section. ' + assetIdentifyEachEmailLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Add common links',
                        content: 'To add common links â€“ unsubscribe, and view as a web page â€“ click on the System link tab. ' + assetAddCommonLinksLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Personalize your emails',
                        content: 'To personalize your Emails, click on Merge Field link tab.  You can merge any of the fields that you have established within your contact records.  You can add fields, such as, first name, company name, and phone number or any number of unique fields. ' + assetPersonalizeEmailsLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Be sure to SAVE your email',
                        content: 'Be sure to SAVE your Email.  The system does NOT automatically save Emails. ' + assetBeSureToSaveEmailsLink,
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Test an email',
                        content: 'To test an Email before sending to your Contact List, click the Test tab. Use these test Emails to check your fields, forms and links.  ' + assetTestAnEmailLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                ]
            };

            var assetsTourAddLandingPages = {
                id: 'AssetsTourAddLandingPages',
                steps: [
                    {
                        title: 'Content Overview',
                        content: 'From the Content screens you are able to add/manage playbooks, email templates, landing pages and talking points.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Content%20Overview.mp4"target="_blank">Learn More</a>',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Identify a landing page',
                        content: 'To identify Landing Pages, give each Landing Page a unique "keyword" that is displayed at the end of the page URL (note: keywords cannot contain spaces or special characters).',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Test your landing page',
                        content: 'To test your Landing Page, copy the Landing Page URL to your browser and hit the Enter key.',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new landing page',
                        content: 'To create a new Landing Page, click the Add tab.',
                        target: 'Add',
                        placement: 'bottom'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'View all existing landing pages',
                        content: 'To view all of the Landing Pages within your system, click on this button, and click on the All Landing Pages tab.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new folder',
                        content: 'To create a new folder, click on this button, right click on an existing folder, and select New Folder.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var assetsTourTalkingPoints = {
                id: 'AssetsTourTalkingPoints',
                steps: [
                    {
                        title: 'Content Overview',
                        content: 'From the Content screens you are able to add/manage playbooks, email templates, landing pages and talking points.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Content%20Overview.mp4"target="_blank">Learn More</a>',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new talking point',
                        content: 'To create a new Talking Point, click the Add tab.',
                        target: 'Add',
                        placement: 'bottom'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'View all existing talking points',
                        content: 'To view all of the Talking Points within your system, click on this button, and click on the All Talking Points tab.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new folder',
                        content: 'To create a new folder, click on this button, right click on an existing folder, and select New Folder.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var assetsTourAddTalkingPoint = {
                id: 'AssetsTourAddTalkingPoint',
                steps: [
                    {
                        title: 'Talking point title',
                        content: 'Enter or change the title of your Talking Point in the Title box.  This title is the name your sales reps will see so it is important that title is meaningful and resonates.   ' + assetTalkingPointTitleLink,
                        target: 'Title',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point color',
                        content: 'The Color feature box allows you to color coordinate your Talking Points. This is the color that will appear in your Playbook, and can be used as an indicator in your sales process. ' + assetTalkingPointColorLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point sales stage',
                        content: 'To indicate the sales stage at which this Talking Point would logically appear, select a stage from the Sales Stage list. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point Update Only',
                        content: 'To make your Talking Point a ClickNote, click on the ClickNote box.  For example if you want an easily accessible button in the Playbook for "left voicemail" that can be pressed and automatically added into the Story So Far. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point qualified call disposition',
                        content: 'To make your Talking Point a Qualified Call Disposition, click on the Qualified Call Disposition box.  A qualified call disposition indicates a Talking Point that logically terminates a call, such as "left voicemail". ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point note',
                        content: 'To make your Talking Point a Note, click on the Note in CRM box.  This creates an entry in the notes. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Edit marketing material',
                        content: 'To write or copy your marketing material for your Talking Points, use the editor.  ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Be sure to SAVE the talking point',
                        content: 'Note:  Be sure to SAVE before exiting.  The system does NOT automatically save or update the Talking Points.',
                        target: 'q',
                        placement: 'bottom'
                    },
                ]
            };

            var assetsTourSalesNavigators = {
                id: 'AssetsTourSalesNavigators',
                steps: [
                    {
                        title: 'Content Overview',
                        content: 'From the Content screens you are able to add/manage playbooks, email templates, landing pages and talking points.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Content%20Overview.mp4"target="_blank">Learn More</a>',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new playbook',
                        content: 'To create a new Playbook, click the Add tab.',
                        target: 'Add',
                        placement: 'bottom'
                    },
                    {
                        title: 'Copy an existing playbook',
                        content: 'To copy an existing Playbook, click the <span class="fa fa-files-o"></span> icon to the right of the Playbook information.',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'View all existing playbooks',
                        content: 'To view all of the Playbooks within your system, click on this button, and click on the All Playbooks tab.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Create a new folder',
                        content: 'To create a new folder, click on this button, right click on an existing folder, and select New Folder.',
                        target: 'TreeViewList',
                        placement: 'bottom'
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var assetsTourAddNavigator = {
                id: 'AssetsTourAddNavigator',
                steps: [
                    {
                        title: 'To name your playbook',
                        content: 'To name your Playbook, fill the name in the Name field.  To make the Playbook that you are working on be the "default" Playbook, click the Default field. ' + assetNavigatorNameLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Talking point list',
                        content: 'This list identifies by name all the Talking Points that you have created so far.  To add a Talking Point to the Playbook, simply drag and drop.  To remove a Talking Point from a Playbook, simply drag it out.' + assetTalkingPointColorLink,
                        target: 'q',
                        placement: 'bottom',
                    },
                ]
            };

            // CampaignsTour
            var campaignsTour = {
                id: 'CampaignsTour',
                steps: [
                    {
                        title: 'Workflows',
                        content: 'Workflows allow you to create Marketing Campaigns in a variety of forms. To learn more, click on the three videos below.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Create%20a%20Workflow.mp4"target="_blank">Create Workflow</a><br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/How%20to%20use%20Workflow%20Templates.mp4"target="_blank">Workflow Templates</a><br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Workflow%20Dashboard%20Report.mp4"target="_blank">Workflow Dashboard</a>',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Begin a new Workflow',
                        content: 'To begin a new Workflow, click on the Add tab. ',
                        target: 'Add',
                        placement: 'bottom'
                    },
                    {
                        title: 'Refresh Workflow Page',
                        content: 'To refresh the current Workflow page, click on the Refresh tab. ',
                        target: 'Refresh',
                        placement: 'bottom'
                    },
                    {
                        title: 'Copy an existing Workflow',
                        content: 'To duplicate an existing Workflow, click the <span class="fa fa-files-o"></span> icon for that Workflow and enter the new name. ',
                        target: 'CampaignGrid',
                        placement: 'top'
                    },
                    {
                        title: 'Delete an existing Workflow',
                        content: 'To delete an existing Workflow, click the <span class="fa fa-trash"></span> icon for that Workflow and verify the deletion. ',
                        target: 'CampaignGrid',
                        placement: 'top'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var campaignsTourAddCampaign = {
                id: 'CampaignsTourAddCampaign',
                steps: [
                    {
                        title: 'Create your Workflow',
                        content: 'To create your Workflow, drag and drop the design tools.  Use the flyover help by moving the cursor over a design tool to see what each one does.   ' + campaignCreateCampaignLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Complete your Workflow',
                        content: 'To complete your Workflow prior to launching it, add your Contact List.   ' + campaignCompleteCampaignLink,
                        target: 'q',
                        placement: 'bottom',
                        onNext: function () {
                            $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-3"]').click();
                        }
                    },
                    {
                        title: 'Document your Workflow',
                        content: 'To document additional details about your Workflow, click the Attributes tab. ' + campaignDocumentCampaignLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Process rules',
                        content: 'To process rules at the time a Workflow launches, check the Process Rules on Launch field.   ' + campaignProcessRulesLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Delete your Workflow',
                        content: 'To delete your Workflow, first make it a "test Workflow".  Test Workflows may be deleted so results will not be included in active Workflow reporting.  Only Workflows that are identified as a "test Workflow" can be deleted.  ' + campaignDeleteCampaignLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Link your Workflow',
                        content: 'To link your Workflow to other sales and marketing efforts, add keywords. This provides tracking and determination for inbound contacts. ' + campaignLinkCampaignLink,
                        target: 'q',
                        placement: 'bottom',
                        onNext: function () {
                            $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-4"]').click();
                        }
                    },
                    {
                        title: 'Schedule your Workflow',
                        content: 'To specify the date and time to launch and end a Workflow, click the Schedule tab. Learn  ' + campaignScheduleCampaignLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Launch or Schedule your Workflow',
                        content: 'You can either launch immediately or you can schedule a specific date and time for your Workflow to launch. ' + campaignLaunchOrScheduleCampaignLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Do not use business calendar',
                        content: 'Workflows normally run on a business calendar.  To launch a Workflow during off hours or non-business days, check the Do Not Use Business Calendar field. ' + campaignIgnoreBusinessCalendarLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Specify Workflow end date',
                        content: 'To specify a date on which a Workflow should end, complete the "Stop this Workflow automatically on" field. ' + campaignSetEndDateLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Workflows Tab',
                        content: 'The Workflow tab allows you to create and manage your Workflows.',
                        target: 'q',
                        placement: 'bottom'
                    }
                ],
                onStart: function () {
                    $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-1"]').click();
                }
            };

            var campaignTourCreateCampaign = {
                id: 'CampaignTourCreateCampaign',
                steps: [
                    {
                        title: 'Name your Workflow',
                        content: 'To name your Workflow, fill the name in the Name field.  To enable your sales reps to add contacts to your Workflow, be sure to check that field. ' + campaignTourCreateCampaignNameCampaignLink,
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Add a description to your Workflow',
                        content: 'To add a description for your Workflow, add the description in the Description field â€“ however, a description is not required.' + campaignTourCreateCampaignAddDescriptionLink,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Continue designing your Workflow',
                        content: 'To continue designing your Workflow, click the Create tab.' + campaignTourCreateCampaignContinueDesigningLink,
                        target: 'q',
                        placement: 'bottom'
                    }
                ]
            };

            // LeadsTour

            var leadsTour = {
                id: 'LeadsTour',
                steps: [
                    {
                        title: 'Contacts Overview',
                        content: 'This screen will allow you to perform all functions related to your contacts.',
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'New Contact',
                        content: 'This screen will allow you to add a new contact.',
                        target: 'CreateNewContacts',
                        placement: 'left'
                    },
                    {
                        title: 'Import Contacts',
                        content: 'To import contacts and behaviors, click on the import contacts icon. ',
                        target: 'ImportContacts',
                        placement: 'left'
                    },
                    {
                        title: 'Export Contacts',
                        content: 'To export contacts and behaviors, click on the export contacts icon. ',
                        target: 'ExportContacts',
                        placement: 'left'
                    },
                    {
                        title: 'Manage List',
                        content: 'To create or modify a Contact List, click on the Manage Lists icon.',
                        target: 'ManageList',
                        placement: 'left'
                    },
                    {
                        title: 'Exclude/Merge List',
                        content: 'To exclude Contacts or merge a Contact List, click on the Exclude or Merge Lists icon.',
                        target: 'ExcludeMerge',
                        placement: 'left'
                    },
                    {
                        title: 'Segment List',
                        content: 'To segment Contact List into new Lists, based on rules, click on the Segment List icon.',
                        target: 'SegmentList',
                        placement: 'left',
                    },
                    {
                        title: 'Split List',
                        content: 'To split a Contact List into new Lists, click on the Split List icon.',
                        target: 'SplitList',
                        placement: 'left',
                    },
                    {
                        title: 'Call Queues',
                        content: 'To manage call queues, click on the Call Queues icon. ',
                        target: 'CallQueues',
                        placement: 'left',
                    },
                    {
                        title: 'Big Picture',
                        content: 'To display the Big Picture page, click on the Big Picture icon. ',
                        target: 'ShowBigPicture',
                        placement: 'left'
                    },
                    {
                        title: 'Quick Contact Finder',
                        content: 'To quickly create a Contact List by Company, Email, Last Name, or Phone, click on the Quick Contact Finder button. To cancel the List, click on the Clear Quick Contact Finder button.',
                        target: 'QuickFind',
                        placement: 'bottom',
                    },
                    {
                        title: 'Find List',
                        content: 'To quickly find a Contact List by Name, click on the Find List button.',
                        target: 'FindList',
                        placement: 'bottom',
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var leadsTourImportLead = {
                id: 'LeadsTourImportLead',
                steps: [
                    {
                        title: 'New or Existing Contact List Name',
                        content: 'Create a new name for the Contact List, or choose an existing Contact List, into which to import the Contacts.',
                        target: 'TargetAudienceNameText',
                        placement: 'bottom'
                    },
                    {
                        title: 'Choose Existing Contact List',
                        content: 'Select an existing contact list from a list of existing contact lists.',
                        target: 'choose',
                        placement: 'bottom'
                    },
                    {
                        title: 'Choose File',
                        content: 'Browse your computer for a Contact List (.csv, .xls, .xlsx, xml file), then use the Upload button to copy the selected to the server.',
                        target: 'file',
                        placement: 'bottom'
                    },
                    {
                        title: 'Upload File',
                        content: 'Use the Upload button to copy the selected file to the server.',
                        target: 'upload',
                        placement: 'bottom'
                    },
                    {
                        title: 'Imported contacts fields',
                        content: 'Once the contact records have been uploaded into the system, the column headings from the file will be identified in the Import Fields column. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Previous Mappings',
                        content: 'The list of previous mappings will be displayed for the user to select. Even if a previous mapping is just close to the current mapping, loading previous mappings may save substantial effort. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Add a field',
                        content: 'To manually add a field, highlight the specific field in Import Fields and the specific field in System Fields, then click the Add button.  The selected field will be mapped into the system and listed in the Mapped Fields column.  If an automatically mapped field or a manually mapped field is not desired as part of your uploaded data (or incorrect), highlight that field in the Mapped Fields column and click the Remove button. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Auto map fields',
                        content: 'To have the system automatically map the fields in the Import Fields column to the predefined System Fields, click the Auto button.  Check the Mapped Fields column to confirm that the auto-mapping is correct. Fields may be manually added to or deleted from the mapping.',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Map keys for import',
                        content: 'The Map Keys button supports overriding the default keys with Alternate Keys, and allows specifying Deduplication Keys.',
                        target: 'btnMapKeys',
                        placement: 'top'
                    },
                    {
                        title: 'Preview mapping for import',
                        content: 'The Preview Mapping button displays a representation of the mapped fields.',
                        target: 'btnPreviewMapping',
                        placement: 'top'
                    },
                    {
                        title: 'Confirm import',
                        content: 'The last step is to confirm the import. An email will be sent with a summary of the results of the import operation.',
                        target: 'confirm',
                        placement: 'top'
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var leadsTourAddTargetAudience = {
                id: 'LeadsTourAddTargetAudience',
                steps: [
                    {
                        title: 'Contact List name',
                        content: 'Fill in the name of a Contact List and include a description. ' + leadsTourAddTargetAudienceNameLink,
                        target: 'Name',
                        placement: 'bottom'
                    },
                    {
                        title: 'Contact List type',
                        content: 'Choose your Contact List type.  A "dynamic" Contact List is based on criteria that you define and can continuously have new contacts added.  A "static" Contact List is a list of contacts that does not change once it is established (e.g., a list of contacts from a trade show). ' + leadsTourAddTargetAudienceTypeLink,
                        target: 'Type_Id',
                        placement: 'bottom'
                    },
                    {
                        title: 'Organize Contact Lists',
                        content: 'To organize a Contact List into a specific folder, select the folder from the drop-down folder names in the field labeled "Put the Contact List in this folder" ' + leadsTourAddTargetAudienceOrganizeLink,
                        target: 'FolderId',
                        placement: 'bottom',
                        onNext: function () {
                            $('a[href="#TabStrip-2"]').click();
                        }
                    },
                    {
                        title: 'Set up specific criteria',
                        content: 'To set up specific criteria for your Contact List, click the Criteria tab.  You can use the rule triggers to add attributes that to your contacts in that Contact List allowing you to view contacts with those specific attributes. ' + leadsTourAddTargetAudienceCriteriaLink,
                        target: 'btnAddTargetAudienceTour',
                        placement: 'bottom',
                        xOffset: 100,
                        yOffset: 20
                    },
                    {
                        title: 'Add multiple attributes',
                        content: 'You can add multiple attributes within a section. ' + leadsTourAddTargetAudienceMultipleAttributesLink,
                        target: 'condition-section-0',
                        placement: 'bottom',
                        yOffset: -100
                    },
                    {
                        title: 'Set up further criteria',
                        content: 'You can add further criteria and distinguish between "all must be true" and "any can be true" by creating an additional section.' + leadsTourAddTargetAudienceFurtherCriteriaLink,
                        target: 'condition-section-0',
                        placement: 'bottom',
                        yOffset: 20
                    },
                    {
                        title: 'Apply criteria to test your list',
                        content: 'Apply criteria to create your list of contacts to sort through.' + leadsTourAddTargetAudienceApplyCriteriaLink,
                        target: 'ExecuteCriteria',
                        placement: 'top'
                    },
                    {
                        title: 'Save your criteria',
                        content: 'Be sure to SAVE your Contact List when you have completed the criteria.' + leadsTourAddTargetAudienceSaveCriteriaLink,
                        target: 'MainSave',
                        placement: 'top'
                    }
                ],
                onStart: function () {
                    $('a[href="#TabStrip-1"]').click();
                }
            };

            // RulesTour

            var rulesTour = {
                id: 'RulesTour',
                steps: [
                    {
                        title: 'Manage Rules',
                        content: 'This screen allows you to add or edit the rules managed by the rules engine. To learn more, click on the video below.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Creating%20Rules.mp4"target="_blank">Learn More</a>',
                        target: 'Manage Rules',
                        placement: 'bottom',
                    },
                    {
                        title: 'Create a new rule',
                        content: 'To create a new rule, click the Add button. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'The flag icon',
                        content: 'The <span class="activate ui-icon-deactivate-regular-16x16"></span> icon indicates whether a Rule is active or inactive. A red flag indicates that a Rule is not active (OFF) while a green flag indicates that a Rule is active (ON). ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Rules Tab',
                        content: 'To modify Rules, click the appropriate Rule name in the grid. Rules instruct the system to perform a specified action and can be system-wide (will apply across all Workflows) or can be Workflow-specific.',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Select List',
                        content: 'Click on this button to select a List to process.',
                        target: 'TreeViewList',
                        placement: 'bottom',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var rulesTourAddRule = {
                id: 'RulesTourAddRule',
                steps: [
                    {
                        title: 'Manage Rules',
                        content: 'This screen allows you to add or edit the rules engine. To learn more, click on the video below.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Creating%20Rules.mp4"target="_blank">Learn More</a>',
                        target: 'ManageRule',
                        placement: 'bottom',
                    },
                    {
                        title: 'Add new rule',
                        content: 'To add a new Rule, complete the name, description and check "active" to turn the Rule ON. If you choose to make the Rule unique (by checking the Unique box), the Rule will only be processed against a given contact one time.  For example, you may want to increase a contactâ€™s score by 10 points when a contact opens a companyâ€™s pricing page only one time â€“ not each time that contact goes to the pricing page. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Digital behavior (clicks) can be used as triggers ',
                        content: 'A contactâ€™s digital behavior (clicks) can be used as Triggers for a specified action, e.g., increasing scores, sending rep alerts, adding to Workflows, etc.  If using multiple Triggers, chose between "any" and "all" â€“ if the Trigger criteria is described as "any", then any one of the multiple criteria you have defined will cause the action to take place; if "all", then the action will take place only if all the criteria are met.  ',
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Add additional triggers',
                        content: 'Rules can be made more complex by adding another section to contain additional Triggers.  For each "new section" added, you must chose between "either" or "both" â€“ if "either", any of the Trigger criteria must be true; if "both", all Trigger criteria must be true. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Specify actions',
                        content: 'To specify an action that should occur based on the criteria you have established, complete the Actions section.  There is no limit to how many actions you can specify.  Examples include, alerting sales reps, promoting to a CRM system, and adding to a contactâ€™s score. ',
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'Saving your rule',
                        content: 'Be sure to SAVE a new or modified rule by clicking the Save button. To return to the previous page without saving, click the Return button',
                        target: 'SaveRuleBtn',
                        placement: 'bottom'
                    },
                    {
                        title: 'Rules Tab',
                        content: 'To set up Rules, click the Rules Conditions and Actions tab. Rules instruct the system to perform a specified action and can be system-wide (will apply across all Workflows) or can be Workflow-specific.',
                        target: 'q',
                        placement: 'bottom'
                    }
                ]
            };

            // VisitorInsightsTour

            var visitorInsightsTour = {
                id: 'VisitorInsightsTour',
                steps: [
                    {
                        title: 'Select a date range',
                        content: 'Select the date range for website visitors that you would like to view.  ' + visitorInsightsDateRange,
                        target: 'q',
                        placement: 'bottom'
                    },
                    {
                        title: 'using Jigsaw ',
                        content: 'Visitors on your website can be identified by company name using Jigsaw.  The system also allows you to view website behavior, such as, pages visited, time on each page.  Information is captured even if a website visitor abandons a form designed to collect information. ' + visitorJigSawRange,
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Visitor analytics',
                        content: 'You can either use the Visitor Analytics dashboard for real-time updates or subscribe to a daily Visitor Insights newsletter and marketing automation will send you a daily email with a list of all the visitors to your website during the previous day.' + visitorAnalyticsRange,
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Visitor Insights Tab',
                        content: 'To view the anonymous visitors that have viewed or are viewing your website, click the Visitor Insights tab.',
                        target: 'q',
                        placement: 'bottom',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var BigPictureTour = {
                id: 'bigPictureTour',
                steps: [
                    {
                        title: 'The Virtual Tour',
                        content: 'Welcome!<br />This is your Virtual Tour of the system.<br />Click on the Next button to navigate through the tour.<br />Want to jump straight to a preview of the content?<br /> Click on the START CALL LIST button below.<br /> The Learn More button will give you a quick Demo as well.<br /><br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Assets/Videos/Take%20a%20Tour.mp4" target="_blank" > Learn More</a> ',
                        target: 'BigPic',
                        placement: 'left',
                    },
                    {
                        title: 'The Big Picture',
                        content: 'The Big Picture screen may be your default home screen. From this screen you will be able to import your contacts, create call lists and start call lists.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Assets/Videos/Big%20Picture%20Overview.mp4" target="_blank">Learn More</a>',
                        target: 'BigPic',
                        placement: 'left',
                    },
                    {
                        title: 'Build Content',
                        content: 'Selecting this button will allow you to convert word documents into PlayBooks and Talking Points. This will facilitate conversion of existing content to become SalesTalk content.',
                        target: 'buildContent',
                        placement: 'left',
                    },
                    {
                        title: 'Create List',
                        content: 'Selecting this button will allow you to create Call list from your existing contacts in the system. You can create as many lists as you like based upon a variety of criteria. You can find instructions on the following screens along with an option to watch a short video.',
                        target: 'CreateList',
                        placement: 'left',
                    },
                    {
                        title: 'Create List Step One',
                        content: 'Create a list from contacts already loaded into the system by creating a new and description of your list. You will also choose whether it is a static (only added once) or a dynamic (continually adding) list along with who it is assigned to. On the next screen you can set your triggers.<br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Create%20a%20List%20(Big%20Picture).mp4" target="_blank">Learn More</a>',
                        target: 'CreateList',
                        placement: 'left',
                    },
                    {
                        title: 'Create List Step Two',
                        content: 'Choosing your conditions will allow you to choose who gets added to your list, based on their behaviors (i.e. they clicked on a link from an email you sent them). For a more in-depth review of this please view the video. <br /><a href="https://purl.salestalktech.com/SalesAcceleration/Repository/SalesRelevance/Documents/Create%20a%20List%20(Big%20Picture).mp4" target="_blank">Learn More</a>',
                        target: 'CreateList',
                        placement: 'left',
                    },
                    {
                        title: 'Import Contacts',
                        content: 'Selecting this button will allow you to Import Contacts into the system. The following screens will walk you through the process, along with the option to watch a short video.',
                        target: 'ImportContacts',
                        placement: 'left',
                    },
                    {
                        title: 'Import Contacts Step One',
                        content: 'To import your contacts follow these steps: <br /> ï‚§ Create a list name or choose an existing list <br /> ï‚§ Choose your file (must be .csv, .xls, .xlsx, or .xml formatted)<br /> ï‚§ Select upload and a new screen will appear confirming the file has been uploaded.',
                        target: 'ImportContacts',
                        placement: 'left',
                    },
                    {
                        title: 'Import Contacts Step Two',
                        content: 'Now that your file has been uploaded successfully follow these steps:<br /> ï‚§ By selecting the Auto Map button the system will merge your headings and fields with the fields in the system <br /> ï‚§ If there are any fields that the system doesnâ€™t recognize you can manually add them (See video on step one for help) <br /> ï‚§ After all of the fields have been merged select Confirm Imports button <br /> ï‚§ You will receive a confirmation that the list is being imported. Navigate back to the Big Picture for the menu bar to see your list.',
                        target: 'ImportContacts',
                        placement: 'left',
                    },
                    {
                        title: 'List Name',
                        content: 'When you select a list, you will then see all of the contacts in that list and a Start Call List button will appear (If the list is Static). Selecting the Start Call List button will open your first lead record.<br /><a href="https://purl.salestalktech.com/salesacceleration/repository/salesrelevance/assets/videos/start%20a%20call%20list%20(big%20picture).mp4" target="_blank">Learn More</a>',
                        target: 'LeadListGrid',
                        placement: 'top',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var PredictiveAnalyticsAdminTour = {
                id: 'predictiveAnalyticsAdminTour',
                steps: [
                    {
                        title: 'Build Model',
                        content: 'This page assists in constructing Best Practices Revenue Plan Models, which are used by the Best Practices Revenue Plan Evaluator.',
                        target: 'BuilderTitle',
                        placement: 'bottom',
                    },
                    {
                        title: 'Model Grid',
                        content: 'The detailed Best Practices Revenue Plan Model will appear in the grid at the bottom of the page. The user can modify the contents of the grid by deleting events and by changing weights of counts and times for events.',
                        target: 'ModelGrid',
                        placement: 'top',
                    },
                    {
                        title: 'Create Model',
                        content: 'This user may specify a static Contact List for creating a new Best Practices Revenue Plan Model.',
                        target: 'step1',
                        placement: 'bottom',
                    },
                    {
                        title: 'Specify Contact List for Model',
                        content: 'This automatic completion text box allows the user to specify a static Contact List for creating a Best Practices Revenue Plan Model, by incrementally entering at least three characters starting the name. Percent and Underscore may be used as wild-card criteria.',
                        target: 'targetAudienceAutoComplete',
                        placement: 'bottom',
                    },
                    {
                        title: 'Specify Start Date for Model',
                        content: 'This optional date selection allows the user to provide a start date for limiting inclusion of events in the Contact List.',
                        target: 'txtLimitDate',
                        placement: 'bottom',
                    },
                    {
                        title: 'Save New Model',
                        content: 'This button allows the user to provide a name for the new Best Practices Revenue Plan Model. The name of the new Model must be specified before clicking this button.',
                        target: 'btnCreateModel',
                        placement: 'top',
                    },
                    {
                        title: 'New Model Name',
                        content: 'This text box allows the user to provide a name for the new Best Practices Revenue Plan Model. The name of the new Model must be specified before clicking the button to the left.',
                        target: 'txtCreateName',
                        placement: 'top',
                    },
                    {
                        title: 'Edit Existing Model',
                        content: 'This user may edit an existing Best Practices Revenue Plan Model by selecting a Best Practices Revenue Plan Model name from a dropdown list.',
                        target: 'step2',
                        placement: 'top',
                    },
                    {
                        title: 'Existing Model Names',
                        content: 'This dropdown list displays the existing Best Practices Revenue Plan Model Names to allow the user to select one.',
                        target: 'ddlFunnelReport',
                        placement: 'top',
                    },
                    {
                        title: 'Save Edited Model',
                        content: 'This button allows the user to provide a name for the edited Best Practices Revenue Plan Model. The name of the Model must be specified before clicking this button.',
                        target: 'btnCreate',
                        placement: 'top',
                    },
                    {
                        title: 'Edited Model Name',
                        content: 'This text box allows the user to provide a name for the edited Best Practices Revenue Plan Model. The name of the Model must be specified before clicking the button to the left.',
                        target: 'txtCopyName',
                        placement: 'top',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var PredictiveAnalyticsTour = {
                id: 'predictiveAnalyticsTour',
                steps: [
                    {
                        title: 'Model Validator',
                        content: 'This page allows the user to evaluate Best Practices Revenue Plan Models with selected Contact Lists.',
                        target: 'ModelTitle',
                        placement: 'bottom',
                    },
                    {
                        title: 'Model Grid',
                        content: 'The evaluated Best Practices Revenue Plan Model will appear in the grid at the bottom of the page, showing the results of evaluating selected Contact Lists.',
                        target: 'ModelGrid',
                        placement: 'top',
                    },
                    {
                        title: 'Specify Contact List for Model',
                        content: 'This automatic completion text box allows the user to specify a static Contact List for evaluating a Best Practices Revenue Plan Model, by incrementally entering at least three characters starting the name. Percent and Underscore may be used as wild-card criteria.',
                        target: 'targetAudienceAutoComplete',
                        placement: 'bottom',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            var PredictiveAnalyticsRevenueTour = {
                id: 'predictiveAnalyticsTour',
                steps: [
                    {
                        title: 'Model Validator',
                        content: 'This page allows the user to evaluate Best Practices Revenue Plan Models for the current Contact.',
                        target: 'ModelTitle',
                        placement: 'bottom',
                    },
                    {
                        title: 'Model Grid',
                        content: 'The evaluated Best Practices Revenue Plan Model will appear in the grid at the bottom of the page, showing the results of evaluating then for the current Contact.',
                        target: 'ModelGrid',
                        placement: 'top',
                    },
                    {
                        title: 'Model Names',
                        content: 'This dropdown list displays the Best Practices Revenue Plan Model Names to allow the user to select one.',
                        target: 'ModelId',
                        placement: 'top',
                    },
                    {
                        title: 'Send an SMS Message',
                        content: 'Click to Send an SMS Message and create Note and Story So Far entries for current Contact.',
                        target: 'a_SendSMS',
                        placement: 'left',
                    },
                    {
                        title: 'Provide Suggestions to a User',
                        content: 'Click to Provide Suggestions to a User by creating a Note and a Story So Far entry for current Contact, then sending text to a User to Check Notes for the current Contact.',
                        target: 'a_StartSuggestion',
                        placement: 'left',
                    },
                    {
                        title: 'Send an SMS Message to a User',
                        content: 'Click to send SMS Message to a Mobile Phone for a User with Number and Provider configured in Users and Security Templates.',
                        target: 'a_PutText',
                        placement: 'left',
                    },
                    {
                        title: 'Set Engagement or Opportunity Context',
                        content: 'Click to Set Engagement or Opportunity Context for current Contact.',
                        target: 'a_SetEngagementq',
                        placement: 'left',
                    },
                    {
                        title: 'Start Take A Tour',
                        content: 'Click to start Take a Tour, after user requests page refresh.',
                        target: 'a_TakeATour',
                        placement: 'left',
                    },
                    {
                        title: 'Display Help Resources Page',
                        content: 'Click to display Help Resources Page.',
                        target: 'a_HelpResources',
                        placement: 'left',
                    },
                    {
                        title: 'Display Additional Selections Menu',
                        content: 'Click to display Additional Selections, including logging out.',
                        target: 'drop3',
                        placement: 'left',
                    },
                ]
            };

            LLCommon.Trace("Hopscotch.startTour " + tourName);

            switch ( tourName )
            {
                case 'AssetsTourEmail':
                    hopscotch.startTour(assetsTourEmail);
                    break;
                case 'AssetsTourAddEmailGeneral':
                    $('#TabStrip li a[href="#TabStrip-1"]').attr('id', 'identifyEachEmail');
                    hopscotch.startTour(assetsTourAddEmailGeneral);
                    break;
                case 'AssetsTourAddLandingPages':
                    $('#AssetTabStrip li a[href="#AssetTabStrip-2"]').attr('id', 'identifyLandingPage');
                    hopscotch.startTour(assetsTourAddLandingPages);
                    break;
                case 'AssetsTourTalkingPoints':
                    $($('#FolderView li span')[0]).attr('id', 'createNewFolder');
                    hopscotch.startTour(assetsTourTalkingPoints);
                    break;
                case 'AssetsTourAddTalkingPoint':
                case 'CreateTalkingPointTourAddTalkingPoint':
                case 'EditTalkingPointTourAddTalkingPoint':
                    $('#ddlColor').next().attr('id', 'colorAutoComplete');
                    hopscotch.startTour(assetsTourAddTalkingPoint);
                    break;
                case 'AssetsTourSalesNavigators':
                    $($('#FolderView li span')[0]).attr('id', 'createNewFolder');
                    hopscotch.startTour(assetsTourSalesNavigators);
                    break;
                case 'AssetsTourAddNavigator':
                case 'CreateSalesNavigatorTourAddNavigator':
                case 'EditSalesNavigatorTourAddNavigator':
                    hopscotch.startTour(assetsTourAddNavigator);
                    break;
                case 'BigPictureTour':
                case "AgentDashboardsTour":
                    hopscotch.startTour(BigPictureTour);
                    break;
                case 'CampaignsTour':
                case "WorkflowsTour":
                    $($('.fa-files-o')[0]).attr('id', 'copyCampaign');
                    hopscotch.startTour(campaignsTour);
                    break;
                case 'CampaignsTourAddCampaign':
                case 'WorkflowsTourAddCampaign':
                    $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-1"]').attr('id', 'createCampaign');
                    $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-3"]').attr('id', 'documentCampaign');
                    $('#CampaignManagerTabStrip li a[href="#CampaignManagerTabStrip-4"]').attr('id', 'scheduleCampaign');
                    hopscotch.startTour(campaignsTourAddCampaign);
                    break;
                case 'CampaignTourCreateCampaign':
                    hopscotch.startTour(campaignTourCreateCampaign);
                    break;
                case 'LeadsTour':
                    hopscotch.startTour(leadsTour);
                    break;
                case 'LeadsTourImportLead':
                case 'NewImportTour':
                case 'ImportTour':
                    hopscotch.startTour(leadsTourImportLead);
                    break;
                case 'LeadsTourAddTargetAudience':
                    hopscotch.startTour(leadsTourAddTargetAudience);
                    break;
                case 'RulesTour':
                    $($('.ui-icon-deactivate-regular-16x16')[0]).attr('id', 'flagIcon');
                    hopscotch.startTour(rulesTour);
                    break;
                case 'RulesTourAddRule':
                    hopscotch.startTour(rulesTourAddRule);
                    break;
                case 'VisitorInsightsTour':
                    hopscotch.startTour(visitorInsightsTour);
                    break;
                case 'PredictiveAnalyticsAdminTour':
                    hopscotch.startTour(PredictiveAnalyticsAdminTour);
                    break;
                case 'PredictiveAnalyticsTour':
                    hopscotch.startTour(window.location.search.includes("?LeadId=") ? PredictiveAnalyticsRevenueTour : PredictiveAnalyticsTour);
                    break;
            }   // switch (tourName)
        }
        catch ( ex )
        {
            var i = 0;
        }
    };  // LLCommon.startTour


    try
    {
        var originalLeave = $.fn.popover.Constructor.prototype.leave;

        jQuery.fn.popover.Constructor.prototype.leave = function ( obj )
        {
            try
            {
                var self = obj instanceof this.constructor ? obj : $(obj.currentTarget)[this.type](this.getDelegateOptions()).data('bs.' + this.type);
                var container, timeout;

                originalLeave.call(this, obj);

                if ( obj.currentTarget )
                {
                    container = $( obj.currentTarget ).siblings ( '.popover' );
                    timeout = self.timeout;
                    container.one ( 'mouseenter' , function ( )
                    {
                        // We entered the actual popover; call off the dogs.
                        clearTimeout(timeout);

                        // Let's monitor popover content instead.

                        container.one ( 'mouseleave' , function ( )
                        {
                            $.fn.popover.Constructor.prototype.leave.call(self, self);
                        });
                    });
                }
            }
            catch ( ex )
            {
                var i = 0;
            }
        };
    }
    catch ( exx )
    {
        var i = 0;
    }

    LLCommon.DictionarySharp = function ( pstrSourceString , pchrArraySplitCharacter, pchrKeyValueSplitCharacter )
    {
        /*
            ------------------------------------------------------------
            Class Name:         DictionarySharp

            Class Goal:         Implement a Dictionary<string,string>
                                that is constructed from a delimited
                                string.

            Input:              pstrSourceString           = string from
                                                             which to
                                                             create the
                                                             dictionary

                                pchrArraySplitCharacter    = Character
                                                             on which to
                                                             split array

                                pchrKeyValueSplitCharacter = Character
                                                             on which to
                                                             split key
                                                             from value

            Output:             If string pstrSourceString is a well
                                formed delimited string, this function
                                returns an object that contains within
                                it an array that can be treated as if it
                                is a BCL Dictionary<string,string>.

            Remarks:            1)  This function expects the following
                                    symbolic constants to be defined.

                                    ARRAY_FIRST_ELEMENT
                                    CSV_SEPARATOR_CHAR
                                    EMPTY_STRING_LENGTH
                                    EQUALS_CHAR
                                    INDEXOF_NOT_FOUND
                                    SUBSTRING_FIRST_CHAR
                                    SUBSTRING_SECOND_CHARACTER
                                    SPLIT_NAME_FROM_VALUE
                                    SPLIT_NAME_PART
                                    SPLIT_VALUE_PART

                                    All of the above are defined in
                                    LLCommon.js as global symbols.

                                2)  This function expects the following
                                    LLCommon methods to be defined.

                                    IsString
                                    GetNameOfCurrentFunction
                                    OrdinalFromIndex
                                    QuoteString
            ------------------------------------------------------------
        */

        const ParamTypeIs               = ( paramRef ) => paramRef === undefined ? 'undefined' : typeof paramRef;

        //  --------------------------------------------------------------------
        //  The DictionarySharp constructor starts here. GetValueAtKey is its
        //  one and only instance method. Defining the object as a property on
        //  the LLCommon object increases its visibility.
        //  --------------------------------------------------------------------

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrSourceString ) )
        {
            this.ArraySplitCharacter    = LLCommon.IsString ( pchrArraySplitCharacter )    ? pchrArraySplitCharacter.substring    ( SUBSTRING_FIRST_CHAR , SUBSTRING_SECOND_CHARACTER ) : CSV_SEPARATOR_CHAR;
            this.KeyValueSplitCharacter = LLCommon.IsString ( pchrKeyValueSplitCharacter ) ? pchrKeyValueSplitCharacter.substring ( SUBSTRING_FIRST_CHAR , SUBSTRING_SECOND_CHARACTER ) : EQUALS_CHAR;

            if ( pstrSourceString.indexOf ( this.ArraySplitCharacter ) > INDEXOF_NOT_FOUND )
            {
                this.KeyValuePairsArray = [ ];
                const astrKeyValuePairs = pstrSourceString.split ( this.ArraySplitCharacter );

                for ( var intPosition = ARRAY_FIRST_ELEMENT;
                          intPosition < astrKeyValuePairs.length;
                          intPosition++ )
                {
                    if ( ( astrKeyValuePairs [ intPosition ].length > EMPTY_STRING_LENGTH ) && ( astrKeyValuePairs [ intPosition ].indexOf ( this.KeyValueSplitCharacter ) > INDEXOF_NOT_FOUND ) )
                    {
                        astrKVP         = LLCommon.StringSplitSharp ( astrKeyValuePairs [ intPosition ] , this.KeyValueSplitCharacter , SPLIT_NAME_FROM_VALUE );

                        if ( astrKVP [ SPLIT_NAME_PART ].length > EMPTY_STRING_LENGTH )
                        {
                            this.KeyValuePairsArray.push ( {
                                KeyName  : astrKVP [ SPLIT_NAME_PART ] ,
                                KeyValue : astrKVP [ SPLIT_VALUE_PART ]
                            } );
                        }   // TRUE (The Key Name value is valid.) block, if ( astrKVP [ SPLIT_NAME_PART ].length > EMPTY_STRING_LENGTH )
                        else
                        {
                            throw new Error ( strMethodName + ': The substring at position ' + LLCommon.OrdinalFromIndex ( intPosition ) + 'in constructor parameter pstrSourceString MUST contain a valid Key Name. Value of substring = ' + LLCommon.QuoteString ( astrKeyValuePairs [ intPosition ] ) + '. Value of pstrSourceString = ' + LLCommon.QuoteString ( pstrSourceString ) + '.' );
                        }   // FALSE (The Key Name value is invalid.) block, if ( astrKVP [ SPLIT_NAME_PART ].length > EMPTY_STRING_LENGTH )
                    }   // TRUE (anticipated outcome) block, if ( astrKeyValuePairs [ intPosition ].indexOf ( this.KeyValueSplitCharacter ) > INDEXOF_NOT_FOUND )
                    else
                    {
                        if ( astrKeyValuePairs [ intPosition ].length > EMPTY_STRING_LENGTH )
                        {
                            throw new Error ( strMethodName + ': The substring at position ' + LLCommon.OrdinalFromIndex ( intPosition ) + 'in constructor parameter pstrSourceString MUST contain at least one ' + this.KeyValueSplitCharacter + ' character. Value of substring = ' + LLCommon.QuoteString ( astrKeyValuePairs [ intPosition ] ) + '. Value of pstrSourceString = ' + LLCommon.QuoteString ( pstrSourceString ) + '.' );
                        }   // TRUE (The substring contains SOME text.) block, if ( astrKeyValuePairs [ intPosition ].length > EMPTY_STRING_LENGTH )
                        else
                        {
                            throw new Error ( strMethodName + ': The substring at position ' + LLCommon.OrdinalFromIndex ( intPosition ) + 'in constructor parameter pstrSourceString CANNOT be the empty string. Value of pstrSourceString = ' + LLCommon.QuoteString ( pstrSourceString ) + '.' );
                        }   // FALSE (The substring is empty.) block, if ( astrKeyValuePairs [ intPosition ].length > EMPTY_STRING_LENGTH )
                    }   // FALSE (unanticipated outcome) block, if ( astrKeyValuePairs [ intPosition ].indexOf ( this.KeyValueSplitCharacter ) > INDEXOF_NOT_FOUND )
                }   // for ( var intPosition = ARRAY_FIRST_ELEMENT; intPosition < astrKeyValuePairs.length; intPosition++ )
            }   // TRUE (anticipated outcome) block, if ( pstrSourceString.indexOf ( this.ArraySplitCharacter ) > INDEXOF_NOT_FOUND )
            else
            {
                throw new Error ( strMethodName + ': Constructor parameter pstrSourceString MUST contain at least one ' + this.ArraySplitCharacter + ' character. Value of pstrSourceString = ' + LLCommon.QuoteString ( pstrSourceString ) + '.' );
            }   // FALSE (unanticipated outcome) block, if ( pstrSourceString.indexOf ( this.ArraySplitCharacter ) > INDEXOF_NOT_FOUND )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrSourceString ) )
        else
        {
            throw new Error ( strMethodName + ': Constructor parameter pstrSourceString MUST be a string. Its actual type is ' + ParamTypeIs ( pstrSourceString ) + '.' );
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrSourceString ) )
    }   // LLCommon.DictionarySharp object definition


    LLCommon.DictionarySharp.prototype.GetValueAtKey = function ( pstrKeyName )
    {
        /*
            ------------------------------------------------------------
            Method Name:        GetValueAtKey

            Method Goal:        Return the value stored at a given key
                                in the DictionarySharp object.

            Input:              pstrKeyName = String representation of a
                                              key name for which the
                                              value is wanted

            Output:             If string pstrKeyName matches the value
                                of a key in the associated dictionary,
                                return the corresponding value.

                                Otherwise, the return value is the empty
                                string, represented as global constant
                                EMPTY_STRING.
            ------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrKeyName ) && this.KeyValuePairsArray.length >  ARRAY_IS_EMPTY )
        {
            const raFound = this.KeyValuePairsArray.find ( ( element ) => element.KeyName === pstrKeyName );

            if ( raFound !== undefined )
            {
                return raFound.KeyValue;
            }   // TRUE (anticipated outcome) block, if ( raFound !== undefined )
            else
            {
                return EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( raFound !== undefined )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrKeyName ) && this.KeyValuePairsArray.length >  ARRAY_IS_EMPTY )
        else
        {
            return EMPTY_STRING;
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrKeyName ) && this.KeyValuePairsArray.length >  ARRAY_IS_EMPTY )
    }   // LLCommon.DictionarySharp.prototype.GetValueAtKey method

    //  ------------------------------------------------------------------------
    //  The following functions came over from LeadLifeJSHelpersLib.js and
    //  LeadLifeJSHelpersGlobals.js.
    //  ------------------------------------------------------------------------

    LLCommon.GetParameterFromURLFormOrLocalStorage = function ( pstrKeyName , pobjDefaultValue )
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

        if ( LLCommon.IsString ( pstrKeyName ) )
        {
            const strResult             = LLCommon.URLParameterFromQueryString ( pstrKeyName );

            if ( strResult !== null && strResult !== NULL_AS_STRING_VALUE )
            {
                if ( pstrKeyName.toLowerCase ( ) === KEY_IS_LEAD_ID && strResult !== NULL_AS_STRING_VALUE && LLCommon.IsValidInteger ( strResult ) )
                {
                    _leadid             = parseInt ( strResult );

                    if ( _leadid > NO_LEAD_ID )
                    {
                        _leadidSource       = SRC_IS_QUERY_STRING;

                        sessionStorage.setItem ( 'leadid'       , _leadid );
                        sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                    }   // if ( _leadid > NO_LEAD_ID )
                }   // if ( pstrKeyName.toLowerCase ( ) === KEY_IS_LEAD_ID && strResult !== NULL_AS_STRING_VALUE && LLCommon.IsValidInteger ( strResult ) )

                return strResult;
            }   // TRUE (ideal outcome) block, if ( astrResults !== null )
            else
            {   // Since the query string cannot satisfy the query, see whether the page fields or Local Storage can do so.
                const strPageFieldValue = LLCommon.QueryPageFields ( pstrKeyName ,
                                                                     strDefaultValue ,
                                                                     true );

                if ( strPageFieldValue !== strDefaultValue )
                {   // The form satisfied the query.
                    return strPageFieldValue;
                }   // TRUE (The page fields answered the query.) block, if ( strPageFieldValue !== strDefaultValue )
                else
                {   // Either localStorage satisfies the query, or the default value will have to do.
                    return LLCommon.QuerySssionStorage ( pstrKeyName ,
                                                         strDefaultValue ,
                                                         true );
                }   // FALSE (Local Storage answered the query, even if only by returning the default value.) block, if ( strPageFieldValue !== strDefaultValue )
            }   // FALSE (The query string didn't satisfy the query.) block, if ( astrResults !== null )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrKeyName ) )
        else
        {   // Without a usable key, the only choice is to return the default value.
            return strDefaultValue;
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrKeyName ) )
    }  // function LLCommon.GetParameterFromURLFormOrLocalStorage


    LLCommon.GetUrlVarsFromSession = function ( pfSaveInSessionStorage )
    {
        /*
            --------------------------------------------------------------------
            Name:       GetUrlVarsFromSession

            Goal:       Use OpenController method GetQueryStringFromSession to
                        gather the query string parameters that were captured by
                        SalesTalk when a user inputs a URL and the session is
                        logged it, causing the LogOn controller to be bypassed.

            Arguments:  pfSaveInSessionStorage = When True, this Boolean value
                                                 causes the URL parameters to be
                                                 saved into sessionStorage keys
                                                 before being returned in an
                                                 array of JavaScript objects.

            Returns:    The return value is an array that contains an object
                        that has KeyName and KeyValue properties for each query
                        string value returned by GetQueryStringFromSession,
                        except the password key, which is discarded.
            --------------------------------------------------------------------
        */

        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        var   rakvpSet              = [ ];

        const strDeliminated        = LLCommon.DoAjax ( 'GetQueryStringFromSession' , 'GET' );

        if ( strDeliminated.length > EMPTY_STRING_LENGTH )
        {
            const astrKVPSets       = strDeliminated.split ( LOGICAL_NEGATE );
            const intKVPCount       = astrKVPSets.length;
            var   intOutputSlot     = ARRAY_INVALID_INDEX;

            for ( var intSlot = ARRAY_FIRST_ELEMENT;
                      intSlot < intKVPCount;
                      intSlot++ )
            {
                var astr1KeyValPair = astrKVPSets [ intSlot ].split ( EQUALS_CHAR );

                if ( astr1KeyValPair [ ARRAY_FIRST_ELEMENT ] !== 'password' )
                {
                    intOutputSlot++;
                    rakvpSet.push ( {
                                      'KeyName'  :                      astr1KeyValPair [ KEY_VALUE_PAIR_IS_KEY ] ,
                                      'KeyValue' : decodeURIComponent ( astr1KeyValPair [ KEY_VALUE_PAIR_IS_KEY ] === 'login'
                                                                        ? astr1KeyValPair [ KEY_VALUE_PAIR_IS_VALUE ].replace ( SPACE_CHARACTER , '+' )
                                                                        : astr1KeyValPair [ KEY_VALUE_PAIR_IS_VALUE ] )
                                    } );

                    if ( pfSaveInSessionStorage )
                    {
                        sessionStorage.setItem ( rakvpSet [ intOutputSlot ].KeyName ,
                                                 rakvpSet [ intOutputSlot ].KeyValue );
                    }   // if ( pfSaveInSessionStorage )
                }   // if ( astr1KeyValPair [ ARRAY_FIRST_ELEMENT ] !== 'password' )
            }   // for ( var intSlot = ARRAY_FIRST_ELEMENT; intSlot < intKVPCount; intSlot++ )
        }   // if ( strDeliminated.length > EMPTY_STRING_LENGTH )

        return rakvpSet;
    }   // function LLCommon.GetUrlVarsFromSession


    LLCommon.IndexFromOrdinal = function ( pintOrdinal )
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
    }   // function LLCommon.IndexFromOrdinal



    /**
     * Evaluates whether the input string is a valid Boolean representation.
     *
     * Accepts anything that LLCommon.parseBool would successfully convert:
     *   - True values: "true", "1", "yes", "y", "t"
     *   - False values: "false", "0", "no", "n", "f"
     *
     * Input is case-insensitive and trimmed of surrounding whitespace.
     *
     * @function LLCommon.IsValidBooleanString
     * @param {string|String} pstrMaybeBoolean - The string to evaluate.
     * @returns {boolean} True if the string is a valid Boolean representation,
     *                    otherwise false.
     *
     * @example
     * LLCommon.IsValidBooleanString ( "true" );   // → true
     * LLCommon.IsValidBooleanString ( "YES" );    // → true
     * LLCommon.IsValidBooleanString ( "n" );      // → true
     * LLCommon.IsValidBooleanString ( "0" );      // → true
     *
     * @example
     * // Invalid input
     * LLCommon.IsValidBooleanString ( "maybe" );  // → false
     * LLCommon.IsValidBooleanString ( 42 );       // → false
     */
    LLCommon.IsValidBooleanString = function ( pstrMaybeBoolean )
    {
        if ( ! (LLCommon.IsString ( pstrMaybeBoolean ) ) )
        {
            return false;
        }   // if ( ! (LLCommon.IsString ( pstrMaybeBoolean ) ) )

        switch ( pstrMaybeBoolean.trim ( ).toLowerCase ( ) )
        {
            case "true":  case "1": case "yes": case "y": case "t":
            case "false": case "0": case "no":  case "n": case "f":
                return true;
            default:
                return false;
        }   // switch ( pstrMaybeBoolean.trim ( ).toLowerCase ( ) )
    }  // LLCommon.IsValidBooleanString


    /**
     * Evaluate whether its input is a valid floating-point number, accepting
     * actual numeric inputs and any string that looks like a number and can be
     * converted to a finite number by the native Number function.
     *
     * Accepts:
     *   - Integers (e.g. 42, "42")
     *   - Floats (e.g. 42.5, "42.5")
     *   - Scientific notation (e.g. 1e3, "1e3")
     *
     * Rejects:
     *   - Non-numeric strings (e.g. "foo", "42abc")
     *   - Values that coerce to NaN
     *   - Infinity and -Infinity
     *
     * @function LLCommon.IsValidFloat
     * @param {number|string} input - Numeric value or numeric string to evaluate.
     * @returns {boolean} - Returns TRUE if the input is a finite number (integer or float).
     *
     * @example
     * LLCommon.IsValidFloat ( 42.5 );     // → true
     * LLCommon.IsValidFloat ( "42.5" );   // → true
     * LLCommon.IsValidFloat ( "1e3" );    // → true
     * LLCommon.IsValidFloat ( 42 );       // → true
     *
     * @example
     * // Invalid input
     * LLCommon.IsValidFloat ( "foo" );    // → false
     * LLCommon.IsValidFloat ( "42abc" );  // → false
     * LLCommon.IsValidFloat ( NaN );      // → false
     * LLCommon.IsValidFloat ( Infinity ); // → false
     */
    LLCommon.IsValidFloat = function ( input )
    {
        const num = Number ( input );
        return Number.isFinite ( num );
    }  // LLCommon.IsValidFloat


    /**
     * Evaluates whether the input represents a valid integer.
     *
     * Accepts:
     *   - Actual integer values (e.g. 42)
     *   - Numeric strings that can be coerced into integers (e.g. "42")
     *
     * Rejects:
     *   - Non-numeric strings (e.g. "foo")
     *   - Floating-point numbers or strings with decimals (e.g. 42.5, "42.5")
     *
     * @function LLCommon.IsValidInteger
     * @param {number|string} input - Integer or numeric string to evaluate.
     * @returns {boolean} True if the input is an integer or a numeric string
     *                    that can be converted to an integer.
     *
     * @example
     * LLCommon.IsValidInteger ( 42 );     // → true
     * LLCommon.IsValidInteger ( "42" );   // → true
     * LLCommon.IsValidInteger ( "42.5" ); // → false
     * LLCommon.IsValidInteger ( "foo" );  // → false
     */
    LLCommon.IsValidInteger = function ( input )
    {
        //  --------------------------------------------------------------------
        //  This utility method uses the native Number function to coerce input
        //  to a Number that isInteger recognizes as such. If input is
        //  non-numeric, Number returns NaN. Hence, LLCommon.IsValidInteger is
        //  safer and more robust than Number.isInteger on its own.
        //
        //  Accordingly, LLCommon.IsValidInteger has replaced Number.isInteger
        //  in the LLCommon routines for which it was written, as it shall
        //  throughout in due course.
        //  --------------------------------------------------------------------

        return Number.isInteger ( Number ( input ) );
    }   // LLCommon.IsValidInteger


    LLCommon.JQuerySelectorEscape = function ( pstrRawSelector )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        JQuerySelectorEscape

            Method Goal:        Escape special characters that occur in jQuery
                                selectors.

            Input:              pstrRawSelector    = Escape special characters
                                                     that trip up the jQuery
                                                     selector parser.

            Output:             The return value is pstrRawSelector reformatted
                                with problem characters escaped.

            Remarks:            This method transforms "#some.id" into something
                                that looks like "#some\\.id".

            Reference:          How do I select an element by an ID that has
                                characters used in CSS notation?

                                https://learn.jquery.com/using-jquery-core/faq/how-do-i-select-an-element-by-an-id-that-has-characters-used-in-css-notation/
            --------------------------------------------------------------------
        */

        return pstrRawSelector.replace ( /(:|\.|\[|\]|\{|\}|,|=|@)/g , "\\$1" );
    }   // function LLCommon.JQuerySelectorEscape


    LLCommon.JquerySelectorByTagNameAndAttributeValue = function ( pstrTagName , pstrAttributeName , pstrAttributeValue )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        JquerySelectorByTagNameAndAttributeValue

            Method Goal:        Assemble a JQuery selector string that matches a
                                given TagName and attribute value. The return is
                                limited to exact matches of attribute name and
                                value.

            Input:              pstrTagName        = TagName of element(s) to
                                                     return

                                pstrAttributeName  = Name of attribute for which
                                                     pstrAttributeValue is the
                                                     value

                                pstrAttributeValue = Value expected of attribute
                                                     pstrAttributeName

            Output:             Unless all three inputs are strings, the return
                                value is the empty string.

            Remarks:            This method assembles a very complex selector so
                                that its users can concentrate on the task at
                                hand, leaving the syntax to this routine.

                                An example of the constructed string follows.

                                    'input[name="Value"]'

                                Please feel free to write them inline if you are
                                a glutton for punishment.

            Reference:          How can I select an element by name with jQuery?
                                https://stackoverflow.com/questions/1107220/how-can-i-select-an-element-by-name-with-jquery
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( this.IsString ( pstrTagName ) && this.IsString ( pstrAttributeName ) && this.IsString ( pstrAttributeValue ) )
        {
            return   pstrTagName
                   + BRACKET_LEFT
                   + pstrAttributeName
                   + EQUALS_CHAR
                   + QUOTE_DOUBLE
                   + pstrAttributeValue
                   + QUOTE_DOUBLE
                   + BRACKET_RIGHT;
        }   // TRUE (anticipated outcome) block, if ( IsString ( pstrTagName ) && IsString ( pstrAttributeName ) && IsString ( pstrAttributeValue ) )
        else
        {
            return EMPTY_STRING;
        }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrTagName ) && IsString ( pstrAttributeName ) && IsString ( pstrAttributeValue ) )
    }   // function LLCommon.JquerySelectorByTagNameAndAttributeValue


    LLCommon.LeftPadInteger = function ( pintInputInteger , pintTotalLength )
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

        const strMethodName                     = GetNameOfCurrentFunction ( );

        if ( LLCommon.IsValidInteger ( pintInputInteger ) && LLCommon.IsValidInteger ( pintTotalLength ) )
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
                    }   // for ( var intJ = EMPTY_STRING_LENGTH; intJ < intPaddingChars; intJ++ )

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
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsValidInteger ( pintInputInteger ) && LLCommon.IsValidInteger ( pintTotalLength ) )
        else
        {
            return 'ERROR: One or both of arguments pintInputInteger = ' + pintInputInteger + ' and pintTotalLength = ' + pintTotalLength + ' is non-integral. Both must be integers.';
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsValidInteger ( pintInputInteger ) && LLCommon.IsValidInteger ( pintTotalLength ) )
    }   // function LLCommon.LeftPadInteger


    LLCommon.OrdinalFromIndex = function ( pintIndex )
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
    }   // function LLCommon.OrdinalFromIndex


    LLCommon.QueryLocalStorage = function ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
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
            if ( LLCommon.IsString ( pstrKeyName ) )
            {
                strKeyName              = pstrKeyName.toLowerCase ( );
            }   // TRUE (anticipated outcome) block, if ( this.IsString ( pstrKeyName ) )
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // FALSE (unanticipated outcome) block, if ( this.IsString ( pstrKeyName ) )

            strDefaultValue = Object.is ( pstrDefaultValue , undefined ) ? pstrDefaultValue : EMPTY_STRING;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const strLocalStorageValue      = localStorage.getItem ( strKeyName );
        const strFinalLocalStorageValue = strLocalStorageValue === null ? strDefaultValue : strLocalStorageValue;

        if ( strFinalLocalStorageValue !== strDefaultValue && strKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strFinalLocalStorageValue ) )
        {
            _leadid                     = parseInt ( strFinalLocalStorageValue );

            if ( _leadid > NO_LEAD_ID )
            {
                _leadidSource           = SRC_IS_LOCAL_STORAGE;
                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( _leadid > NO_LEAD_ID )
        }   // if ( strFinalLocalStorageValue !== strDefaultValue && strKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strFinalLocalStorageValue ) )

        return strFinalLocalStorageValue;
    }   // function LLCommon.QueryLocalStorage


    LLCommon.QueryPageFields = function ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
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

        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        var   strDefaultValue;

        if ( pfInputsAreSafe )
        {
            strDefaultValue = pstrDefaultValue;
        }   // TRUE (Caller already vetted both inputs.) block, if ( pfInputsAreSafe )
        else
        {   // Execution cannot proceed unless pstrKeyName is a String.
            if ( !LLCommon.IsString ( pstrKeyName ) )
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // if ( !IsString ( pstrKeyName ) )

            strDefaultValue        = Object.is ( pstrDefaultValue , undefined ) ? EMPTY_STRING : pstrDefaultValue;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const docInputElement      = document.getElementById ( pstrKeyName );
        const strInputElementValue = docInputElement === null ? strDefaultValue : docInputElement.value;

        if ( strInputElementValue !== strDefaultValue && pstrKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strInputElementValue ) )
        {
            _leadid                = parseInt ( strInputElementValue );

            if ( _leadid > NO_LEAD_ID )
            {
                _leadidSource      = SRC_IS_FORM_FIELD;

                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( _leadid > NO_LEAD_ID )
        }   // if ( strInputElementValue !== strDefaultValue && pstrKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strInputElementValue ) )

        return strInputElementValue;
    }   // function LLCommon.QueryPageFields


    LLCommon.QuerySssionStorage = function  ( pstrKeyName , pstrDefaultValue , pfInputsAreSafe )
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
            if ( LLCommon.IsString ( pstrKeyName ) )
            {
                strKeyName              = pstrKeyName.toLowerCase ( );
            }   // TRUE (anticipated outcome) block, if ( IsString ( pstrKeyName ) )
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Argument pstrKeyName is a ' + typeof pstrKeyName + '; it must be a String.' );
            }   // FALSE (unanticipated outcome) block, if ( IsString ( pstrKeyName ) )

            strDefaultValue = Object.is ( pstrDefaultValue , undefined ) ? pstrDefaultValue : EMPTY_STRING;
        }   // FALSE (Caller didn't certify that both inputs are valid.) block, if ( pfInputsAreSafe )

        const strSessionStorageValue      = sessionStorage.getItem ( strKeyName );
        const strFinalSessionStorageValue = strSessionStorageValue === null ? strDefaultValue : strSessionStorageValue;

        if ( strFinalSessionStorageValue !== strDefaultValue && strKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strFinalSessionStorageValue ) )
        {
            _leadid                     = parseInt ( strFinalSessionStorageValue );

            if ( _leadid > NO_LEAD_ID )
            {
                _leadidSource      = SRC_IS_FORM_FIELD;

                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( _leadid > NO_LEAD_ID )
        }   // if ( strFinalSessionStorageValue !== strDefaultValue && strKeyName === KEY_IS_LEAD_ID && LLCommon.IsValidInteger ( strFinalSessionStorageValue ) )

        return strFinalSessionStorageValue;
    }   // function LLCommon.QuerySssionStorage


    LLCommon.QuoteString = function ( pstr2Quote , pstrQuoteChar )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      QuoteString

            Function Goal:      Enclose a string or string-like object in one
                                of a specified character.

            Input:              pstr2Quote          = String or object that
                                                      implements toString, to be
                                                      wrapped in the first or
                                                      only character in string
                                                      pstrQuoteChar

                                pstrQuoteChar       = Optional single-character
                                                      string containing the one
                                                      character in which to
                                                      enclose pstr2Quote

            Output:             Return string pstr2Quote enclosed in the first
                                or only character in string pstrQuoteChar. When
                                pstrQuoteChar is omitted, subtitute the double
                                quotation mark.

            Remarks:            In the unlikely event that pstr2Quote is the
                                JavaScript undefined entity, that becomes the
                                return value. No attempt is made to sugar coat
                                it.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        const strQuoteChar  = ( LLCommon.IsString ( pstrQuoteChar ) && pstrQuoteChar.length > EMPTY_STRING_LENGTH )
                              ? pstrQuoteChar.substring ( SUBSTRING_FIRST_CHAR , SINGLE_CHARACTER )
                              : QUOTE_DOUBLE;

        return ( pstr2Quote === undefined ) ? undefined
                                            : ( LLCommon.IsString ( pstr2Quote ) )
                                                ? ( strQuoteChar + pstr2Quote + strQuoteChar )
                                                : ( strQuoteChar + pstr2Quote.toString ( ) + strQuoteChar );
    }   // function LLCommon.QuoteString


    /**
     * Evaluate whether the _userid, _login, LLCommon.UserId, and
     * LLCommon.DialerLogin property value agree with the AgentUserId and
     * AgentLoginEmailId properties on the instance UserInfo property. If not,
     * make them so. At the planned call site, the instance UserInfo property
     * was just synced with the value per the current Wise Agent Team membership
     * setting.
     * @returns {void}
     */
    LLCommon.ReSyncUserInfo = function ( )
    {
        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        debugger;

        console.log ( strMethodName + ' Values on entry: LLCommon.UserInfo.AgentUserId = ' + LLCommon.UserInfo.AgentUserId + ', _userid = ' +  _userid + ', LLCommon.UserId = ' + LLCommon.UserId );

        if ( _userid !== LLCommon.UserInfo.AgentUserId || LLCommon.UserId !== LLCommon.UserInfo.AgentUserId )
        {
            _userid                     = LLCommon.UserInfo.AgentUserId;
            LLCommon.UserId             = LLCommon.UserInfo.AgentUserId;
            console.info ( strMethodName + ' SYNCED Values on exit: LLCommon.UserInfo.AgentUserId = ' + LLCommon.UserInfo.AgentUserId + ', _userid = ' +  _userid + ', LLCommon.UserId = ' + LLCommon.UserId );
        }   // if ( _userid !== LLCommon.UserInfo.AgentUserId || LLCommon.UserId !== LLCommon.UserInfo.AgentUserId )

        console.log ( strMethodName + ' Values on entry: LLCommon.UserInfo.AgentLoginEmailId = ' + LLCommon.UserInfo.AgentLoginEmailId + ', _login = ' +  _login + ', LLCommon.DialerLogin = ' + LLCommon.DialerLogin );

        if ( _login !== LLCommon.UserInfo.AgentLoginEmailId || LLCommon.DialerLogin !== LLCommon.UserInfo.AgentLoginEmailId )
        {
            _login                      = LLCommon.UserInfo.AgentLoginEmailId;
            LLCommon.DialerLogin        = LLCommon.UserInfo.AgentLoginEmailId;
            console.info ( strMethodName + ' SYNCED Values on exit: LLCommon.UserInfo.AgentLoginEmailId = ' + LLCommon.UserInfo.AgentLoginEmailId + ', _login = ' +  _login + ', LLCommon.DialerLogin = ' + LLCommon.DialerLogin );
        }   // if ( _login !== LLCommon.UserInfo.AgentLoginEmailId || LLCommon.DialerLogin !== LLCommon.UserInfo.AgentLoginEmailId )

        LLCommon.GetCommonObjects ( 'LLCommon.ReSyncUserInfo' );
    }   // function LLCommon.ReSyncUserInfo


    LLCommon.SafeGetFocusedElement = function ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      SafeGetFocusedElement

            Function Goal:      Return a reference to the document element that
                                has the focus.

            Input:              None. This method relies upon global objects.

            Output:             This method returns a reference to the element
                                that has the focus, or it returns null if the
                                document is unfocused.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( typeof document === 'undefined' ) return null;
        const el = document.activeElement;
        return el && el !== document.body ? el : null;
    }   // function SafeGetFocusedElement


    LLCommon.ShowLoadedScriptVersions = function ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      ShowLoadedScriptVersions

            Function Goal:      List the versions of all active scripts.

            Input:              None.

            Output:             List the version strings of all loaded scripts.
            --------------------------------------------------------------------
        */

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        var   strMessage        = 'Info about libraries loaded for page (URL):<br><br>' + location.origin + location.pathname;

        strMessage              += '<br><br>LLCommon LastUpdated: '     + LLCommon_LastUpdated;

        if ( !Object.is ( KeyWordHighLighter_LastUpdated , undefined ) && LLCommon.IsString ( KeyWordHighLighter_LastUpdated ) && LeadLifeJSHelpers_LastUpdated.length > EMPTY_STRING_LENGTH )
        {
            strMessage          += '<br>KeyWordHighLighter: '           + KeyWordHighLighter_LastUpdated;
        }   // if ( !Object.is ( KeyWordHighLighter_LastUpdated , undefined ) && LLCommon.IsString ( KeyWordHighLighter_LastUpdated ) && LeadLifeJSHelpers_LastUpdated.length > EMPTY_STRING_LENGTH )

        if ( !Object.is ( LeadLifeJSHelpers_LastUpdated , undefined ) && LLCommon.IsString ( LeadLifeJSHelpers_LastUpdated ) && LeadLifeJSHelpers_LastUpdated.length > EMPTY_STRING_LENGTH )
        {
            strMessage          += '<br>LeadLifeJSHelpers: '            + LeadLifeJSHelpers_LastUpdated;
        }   // if ( !Object.is ( Words2Actions_Recorder_Forms_LastUpdated , undefined ) && LLCommon.IsString ( Words2Actions_Recorder_Forms_LastUpdated ) && Words2Actions_Recorder_Forms_LastUpdated.length > EMPTY_STRING_LENGTH )

        if ( !Object.is ( LeadLifeJSHelpersGlobals_LastUpdated , undefined ) && LLCommon.IsString ( LeadLifeJSHelpersGlobals_LastUpdated ) && LeadLifeJSHelpers_LastUpdated.length > EMPTY_STRING_LENGTH )
        {
            strMessage          += '<br>LeadLifeJSHelpersGlobals: '    + LeadLifeJSHelpersGlobals_LastUpdated;
        }   // if ( !Object.is ( LeadLifeJSHelpersGlobals_LastUpdated , undefined ) && LLCommon.IsString ( LeadLifeJSHelpersGlobals_LastUpdated ) && LeadLifeJSHelpers_LastUpdated.length > EMPTY_STRING_LENGTH )

        const strLocationPathNameLC = location.pathname.toLowerCase ( );

        if ( ( strLocationPathNameLC.endsWith ( '/mobile' ) ) || ( strLocationPathNameLC.endsWith ( '/mobile/' ) ) )
        {
            if ( !Object.is ( LeadLife_Mobile_Index_Page_LastUpdated , undefined ) && LLCommon.IsString ( LeadLife_Mobile_Index_Page_LastUpdated ) && LeadLife_Mobile_Index_Page_LastUpdated.length > EMPTY_STRING_LENGTH )
            {
                strMessage      += '<br>LeadLife_Mobile_Index '         + LeadLife_Mobile_Index_Page_LastUpdated;
            }   // if ( !Object.is ( LeadLife_Mobile_Index_Page_LastUpdated , undefined ) && LLCommon.IsString ( LeadLife_Mobile_Index_Page_LastUpdated ) && LeadLife_Mobile_Index_Page_LastUpdated.length > EMPTY_STRING_LENGTH )
        }   // if ( ( strLocationPathNameLC.endsWith ( '/mobile' ) ) || ( strLocationPathNameLC.endsWith ( '/mobile/' ) ) )

        if ( !Object.is ( STTVideoPlayer_LastUpdated , undefined ) && LLCommon.IsString ( STTVideoPlayer_LastUpdated ) && STTVideoPlayer_LastUpdated.length > EMPTY_STRING_LENGTH )
        {
            strMessage          += '<br>STTVideoPlayer: '               + STTVideoPlayer_LastUpdated;
        }   // if ( !Object.is ( STTVideoPlayer_LastUpdated , undefined ) && LLCommon.IsString ( STTVideoPlayer_LastUpdated ) && STTVideoPlayer_LastUpdated.length > EMPTY_STRING_LENGTH )

        if ( !Object.is ( Words2Actions_Recorder_Forms_LastUpdated , undefined ) && LLCommon.IsString ( Words2Actions_Recorder_Forms_LastUpdated ) && Words2Actions_Recorder_Forms_LastUpdated.length > EMPTY_STRING_LENGTH )
        {
            strMessage          += '<br>Words2Actions_Recorder_Forms: ' + Words2Actions_Recorder_Forms_LastUpdated;
        }   // if ( !Object.is ( Words2Actions_Recorder_Forms_LastUpdated , undefined ) && LLCommon.IsString ( Words2Actions_Recorder_Forms_LastUpdated ) && Words2Actions_Recorder_Forms_LastUpdated.length > EMPTY_STRING_LENGTH )

        bootbox.alert ( strMessage );
    }   // LLCommon.ShowLoadedScriptVersions


    LLCommon.StringSplitSharp = function ( pstrString2Split , pstrDelimiter , pintLimit )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      StringSplitSharp

            Function Goal:      Treat a limit on the count of returned
                                substrings the way the corresponding method on
                                System.String in the Microsoft .NET Framework
                                Base Class Library does.

            Input:              pstrString2Split = string to split

                                pstrDelimiter    = character or string to use as
                                                   delimiter

                                pintLimit        = Integer > 0 to limit number
                                                   of substrings returned

            Output:             The first argument equates to this on the static
                                String.prototype.split method, while the second
                                and third correspond to its first and second
                                arguments. The difference is that, unlike the
                                built-in method, this method treats the limit as
                                does System.String.Split in the Microsoft .NET
                                Framework Base Class Library.

            Remarks:            To ensure compatiblity with the built-in split,
                                this routine calls it, omitting the limit, so
                                that it receives the whole array, from which it
                                reconstructs the last element in the manner that
                                the BCL method would.
            --------------------------------------------------------------------
        */

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        try
        {
            if ( LLCommon.IsString ( pstrString2Split ) && pstrString2Split.length > EMPTY_STRING_LENGTH )
            {
                if ( LLCommon.IsString ( pstrDelimiter ) && pstrDelimiter.length === 1 )
                {
                    if ( Object.is ( pintLimit , undefined ) )
                    {
                        return pstrString2Split.split ( pstrDelimiter );
                    }   // TRUE (The number of substrings to return is unlimited.) block, if ( Object.is ( pintLimit, undefined ) )
                    else
                    {
                        const astrSubStrings        = pstrString2Split.split ( pstrDelimiter );
                        const intSubstringCount     = astrSubStrings.length;

                        if ( intSubstringCount <= pintLimit )
                        {
                            return astrSubStrings;
                        }   // TRUE (Treat as a regular JavaScript split.) block, if ( intSubstringCount <= pintLimit )
                        else
                        {
                            var   rastrSharpSplit   = [ ];
                            const intTreatSameAsJS  = pintLimit - NUMERIC_PLUS_ONE;

                            for ( var intSame = ARRAY_FIRST_ELEMENT;
                                      intSame < intTreatSameAsJS ;
                                      intSame++ )
                            {
                                rastrSharpSplit.push ( astrSubStrings [ intSame ] );
                            }   // for ( var intSame = ARRAY_FIRST_ELEMENT; intSame < intTreatSameAsJS ; intSame++ )

                            var strLastSubString    = EMPTY_STRING;

                            for ( var intJoin = intTreatSameAsJS;
                                      intJoin < intSubstringCount;
                                      intJoin++ )
                            {
                                strLastSubString    = strLastSubString + astrSubStrings [ intJoin ];
                            }   // for ( var intJoin = intTreatSameAsJS; intJoin < intSubstringCount; intJoin++ )

                            rastrSharpSplit.push ( strLastSubString );
                            return rastrSharpSplit;
                        }   // FALSE (This is the special case that must be treated differently.) block, if ( intSubstringCount <= pintLimit )
                    }   // FALSE (The caller specified a maximum number of substrings to return.) block, if ( Object.is ( pintLimit, undefined ) )
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrDelimiter ) && pstrDelimiter.length === 1 )
                else
                {
                    throw new Error ( strMethodName + ': The second argument, pstrDelimiter, must be a String that contains exactly one character.' );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrDelimiter ) && pstrDelimiter.length === 1 )
            }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrString2Split ) && pstrString2Split.length > EMPTY_STRING_LENGTH )
            else
            {
                throw new Error ( strMethodName + ': The first argument, pstrString2Split, must be a non-empty String.' );
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrString2Split ) && pstrString2Split.length > EMPTY_STRING_LENGTH )
        } catch ( ex )
        {
            LLCommon.LogException ( ex )
            return null;
        }
    }   // Function StringSplitSharp


    LLCommon.SwapCssSelectorsOnElement = function ( pdocElement , pstrOldSelector , pstrNewSelector )
    {
        /*
            --------------------------------------------------------------------
            Method Name:        LLCommon.SwapCssSelectorsOnElement

            Method Goal:        Swap one CSS selector for another on an element.

            Input:              pdocElement     = This may be either a reference
                                                  to a DOM element or its ID as
                                                  a string.

                                pstrOldSelector = This string names the "old"
                                                  CSS selector to be replaced
                                                  by the CSS selector identified
                                                  by pstrNewSelector on element
                                                  pdocElement.

                                pstrNewSelector = This string names the "new"
                                                  CSS selector that will replace
                                                  the CSS selector identified by
                                                  pstrOldSelector on element
                                                  pdocElement.

            Output:             Unless the element identified by pdocElement is
                                absent, null, or undefined, the return value is
                                a reference to its classList property.

                                Otherwise, the return value is null.

            Remarks:            Both CSS selector arguments are type-tested that
                                they are truly String objects.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( ( ! Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        {   // When pdocElement is either undefined or null, there is nothing to do.
            const docThisElement = LLCommon.IsString ( pdocElement ) ? document.getElementById ( pdocElement ) : pdocElement;

            if ( docThisElement !== null )
            {
                if ( LLCommon.IsString ( pstrOldSelector ) && LLCommon.IsString ( pstrNewSelector ) )
                {
                    docThisElement.classList.remove ( pstrOldSelector );
                    docThisElement.classList.add    ( pstrNewSelector );
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrOldSelector ) && LLCommon.IsString ( pstrNewSelector ) )

                return docThisElement.classList;
            }   // TRUE (anticipated outcome) block, if ( docThisElement !== null )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( docThisElement !== null )
        }   // TRUE (anticipated outcome) block, if ( ( ! Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
        else {
            return null;
        }   // FALSE (unanticipated outcome) block, if ( ( ! Object.is ( pdocElement , undefined ) ) && ( ! Object.is ( pdocElement , null ) ) )
    }   // function LLCommon.SwapCssSelectorsOnElement


    /**
     * Evaluate the pathname fragment of a URL, returning the local
     * (unqualified), or base, filename of the resource.
     *
     * @function LLCommon.UQFileNameFromHrefOrPathName
     * @param {string|String} pstrHrefOrPathName - The string to evaluate.
     * @returns {String} String representation of local (unqualified) file name
     *
     * @example
     * LLCommon.UQFileNameFromHrefOrPathName ( 'https://salestalktech.com/SalesAcceleration/COMMON/STAGING/Words2Actions_Form_TEMPLATE.HTML' );   // → Words2Actions_Form_TEMPLATE.HTML
     *
     * This method compensates for the absence of a way to extract the local
     * (unqualified) name of a file (resource), for which the accepted excuse is
     * that the filename is a platform-dependent entity. I think that's a pretty
     * flimsy excuse.
     *
     * This method is platform-agnostic, which it achieves by inspecting the
     * input for path delimiters and choosing one of two substring expressions
     * based on the outcome. If neither path delimiter is found, the entire
     * input string is returned. This approach covers both potential failure
     * modes for most of the other examples shown in the Stack Overflow page
     * cited below, since an unqualified file name and the empty string are both
     * covered by the final ELSE block that covers this case.
     *
     * Reference: How to get the file name from a full path using JavaScript?
     *            https://stackoverflow.com/questions/423376/how-to-get-the-file-name-from-a-full-path-using-javascript
     */
    LLCommon.UQFileNameFromHrefOrPathName = function ( pstrHrefOrPathName )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.IsString ( pstrHrefOrPathName ) )
        {
            if ( pstrHrefOrPathName.indexOf ( PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
            {
                return pstrHrefOrPathName.substring ( pstrHrefOrPathName.lastIndexOf ( PATH_SEPARATOR_CHAR ) + NEXT_CHARACTER );
            }   // TRUE (The string is either a WWW URI or a Unix pathname.) block, if ( pstrHrefOrPathName.indexOf ( PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
            else if ( pstrHrefOrPathName.indexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
            {
                return pstrHrefOrPathName.substring ( pstrHrefOrPathName.lastIndexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) + NEXT_CHARACTER );
            }   // TRUE (The string is a Windows pathname.) block, else if ( pstrHrefOrPathName.indexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
            else
            {
                return pstrHrefOrPathName;
            }   // FALSE (The string is neither of the above.) block, else if ( pstrHrefOrPathName.indexOf ( WINDOWS_PATH_SEPARATOR_CHAR ) > INDEXOF_NOT_FOUND )
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrHrefOrPathName ) )
        else
        {
            return EMPTY_STRING;
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrHrefOrPathName ) )
    }   // UQFileNameFromHrefOrPathName method


    LLCommon.URLParameterFromQueryString = function ( pstrParameterName )
    {
        /*
            --------------------------------------------------------------------
            Function Name:      URLParameterFromQueryString

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

        const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

        const strMatchResult  = decodeURIComponent ( ( window.location.search.match ( RegExp ( "[?|&]" + pstrParameterName + '=(.+?)(&|$)' , 'i' ) ) || [, null] ) [ 1 ] );  // Ignore the no_useless_escape and no_sparse_arrays rule violations raised by ESLint.

        if ( pstrParameterName.toLowerCase ( ) === KEY_IS_LEAD_ID && strMatchResult !== NULL_AS_STRING_VALUE && LLCommon.IsValidInteger ( strMatchResult ) )
        {
            _leadid           = parseInt ( strMatchResult );

            if ( _leadid > NO_LEAD_ID )
            {
                _leadidSource = SRC_IS_FORM_FIELD;

                sessionStorage.setItem ( 'leadidSource' , _leadidSource );
                sessionStorage.setItem ( 'leadid'       , _leadid );
            }   // if ( _leadid > NO_LEAD_ID )
        }   // if ( pstrParameterName.toLowerCase ( ) === KEY_IS_LEAD_ID && strMatchResult !== NULL_AS_STRING_VALUE && LLCommon.IsValidInteger ( strMatchResult ) )

        return strMatchResult;
    }   // function LLCommon.URLParameterFromQueryString, bound to Window as URLParameter
}(window.LLCommon = window.LLCommon || {}, jQuery));


/*! js-cookie v3.0.5 | MIT */
!function(e,t){
    "object"==typeof exports&&"undefined"!=typeof module?module.exports=t():"function"==typeof define&&define.amd?define(t):(e="undefined"!=typeof globalThis?globalThis:e||self,function(){
        var n=e.Cookies,o=e.Cookies=t();
        o.noConflict=function(){
            return e.Cookies=n,o
        }
    }
    ())
}
(this,(function(){
    "use strict";
    function e(e){
        for(var t=1;t<arguments.length;t++){
            var n=arguments[t];
            for(var o in n)e[o]=n[o]
        }
        return e
    }
    var t=function t(n,o){
        function r(t,r,i){
            if("undefined"!=typeof document){
                "number"==typeof(i=e({
                },
                o,i)).expires&&(i.expires=new Date(Date.now()+864e5*i.expires)),i.expires&&(i.expires=i.expires.toUTCString()),t=encodeURIComponent(t).replace(/%(2[346B]|5E|60|7C)/g,decodeURIComponent).replace(/[()]/g,escape);
                var c="";
                for(var u in i)i[u]&&(c+="; "+u,!0!==i[u]&&(c+="="+i[u].split(";")[0]));
                return document.cookie=t+"="+n.write(r,t)+c
            }
        }
        return Object.create({
            set:r,
            get:function(e){
                if("undefined"!=typeof document&&(!arguments.length||e)){
                    for(var t=document.cookie?document.cookie.split("; "):[],o={
                    },
                    r=0;r<t.length;r++){
                        var i=t[r].split("="),c=i.slice(1).join("=");
                        try{
                            var u=decodeURIComponent(i[0]);
                            if(o[u]=n.read(c,u),e===u)break
                        }
                        catch(e){
                        }
                    }
                    return e?o[e]:o
                }
            },
            remove:function(t,n){
                r(t,"",e({
                },
                n,{
                    expires:-1
                })
                )
            },
            withAttributes:function(n){
                return t(this.converter,e({
                },
                this.attributes,n))
            },
            withConverter:function(n){
                return t(e({
                },
                this.converter,n),this.attributes)
            }
        },
        {
            attributes:{
                value:Object.freeze(o)
            },
            converter:{
                value:Object.freeze(n)
            }
        })
    }
    ({
        read:function(e){
            return QUOTE_DOUBLE === e [ 0 ] && ( e=e.slice(1,-1)),e.replace(/(%[\dA-F]{2})+/gi,decodeURIComponent)
        },
        write:function(e){
            return encodeURIComponent(e).replace(/%(2[346BF]|3[AC-F]|40|5[BDE]|60|7[BCD])/g,decodeURIComponent)
        }
    },
    {
        path:"/"
    }
    );
    return t
})
);

console.log ( ScriptInfoForLog ( LLCommon_SCRIPTSOURCE , LLCommon_VERSION , LLCommon_LastUpdated , 'loaded' ) );
