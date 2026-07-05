/*eslint-env browser*/
/*global $ _leadid _leadidSource _LeadLifeJSHelpers AddOrRemoveCssSelector ARRAY_FIRST_ELEMENT ARRAY_INVALID_INDEX ARRAY_IS_EMPTY CSS_SELECTOR_ADD EMPTY_STRING_LENGTH EMPTY_STRING GetLeadOrCrmIdFromUrl GetNameOfCurrentFunction InputMask_Engine LLCommon NO_LEAD_ID ScriptInfoForLog ShowOrHideElement SPACE_CHARACTER SRC_IS_FORM_FIELD UNDERSCORE_CHAR UpdateIfChanged*/
/* 09/15/2023 13:42:52 - DG - The previous two lines satisfy requirement for ESLint. Unresolved omissions have no effect on script execution, since the script sees them as standard comments. */
"use strict";

const LeadLife_Mobile_Index_Page_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );    // Ignore the complaint raised by ESLint that this constant is unreferenced.
const LeadLife_Mobile_Index_Page_VERSION      = 1.030;                                                                                          // Ignore the complaint raised by ESLint that this constant is unreferenced.
const LeadLife_Mobile_Index_Page_LastUpdated  = '2024/11/04 22:08:26 CDT';

/*
    ============================================================================

    Name:               Mobile_Index.js

    Goal:               Define custom JavaScript functions used by the
                        Words2Actions mobile HTML page.

    Dependencies:       The code defined in this module requires working JQuery
                        and LeadLifeJSHelpers objects, which the calling page is
                        expected to supply via deferred loading.

    Remarks:            Since pairs of field names and values are passed to the
                        database in a long string delimited by LOGICAL_NEGATE
                        characters, multiple values such as the ID of an input
                        control must be delimited by a different character.
                        Since it appears that spaces are invalid in element ID
                        names, they work as well, and make the text a tad easier
                        to read when displayed in a database report.

                        While this code is customized for a specific page, it is
                        highly desireable to create a generic version of it that
                        can run behind ANY landing page that hosts a form.

    References:         Add a CSS class to an HTML element with JavaScript/jQuery
                        https://www.techiedelight.com/add-css-class-to-html-element-javascript

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By Remark/Brief Description
    ---------- ------- -- ------------------------------------------------------
    2023/04/04 1.000   DG MVP: New code implemented as code behind a page
    2023/05/25 1.001   DG Implement input masks.
    2023/05/29 1.002   DG In event delegate function SelectLeadIdByRow, swap
                          ToggleTabs and HandleFormPrefill.
    2023/06/18 1.003   DG Correct the comment appended to the closing bracket of
                          the catch block in function ToggleTabs. This is a doc
                          change only, the code is unchanged.
    2023/07/09 1.004   DG Document function DoWords2Action and mark it as
                          deprecated.
    2023/07/09 1.005   DG Function SelectLeadIdByRow must call function (method)
                          _LeadLifeJSHelpers.HandleFormPrefill BEFORE calling
                          ToggleTabs because the latter constructs the full name
                          from the first and last names read from the database
                          by HandleFormPrefill.
    2023/07/11 1.006   DG Implement the new ShowOrHideElement in ToggleTabs.
    2023/07/12 1.007   DG Adapt to support passing in an external CRM ID or lead
                          ID to bypass the search grid.
    2023/07/13 1.008   DG When operating in search mode, set the initil focus
                          on the input box.
    2023/07/15 1.009   DG Set the title attribute of the index rows to provide a
                          hint that they do something when clicked.
    2023/07/19 1.010   DG Add ExtraFilters argument to GetLeadSearchList API
                          call.
    2023/07/23 1.011   DG Add Boolean TRUE flag to GetLeadOrCrmIdFromUrl call,
                          and correct the statement that changes the visibility
                          of the divs, and define a special argument to pass to
                          ToggleTabs defined as constant LEAD_ID_IS_IN_URL that
                          causes it to fix up the full name on the form.
    2023/07/24 1.012   DG Correct name of element ID from 'leadId' to 'leadid'.
    2023/07/25 1.013   DG Invoke global function LLCommon.GetUrlVarsFromSession
                          at the top of the DOMContentLoaded event listener.
    2023/07/28 1.014   DG 1) Clear the screen when the Search4Contacts button is
                             clicked or tapped.
                          2) Move SearchParam event listener to this script
                             from Words2Actions_Recorder_Forms.js; it belongs
                             here because this script controls the page.
                          3) Move the call to LLCommon.GetUrlVarsFromSession
                             from this script to the DOMContentLoaded event
                             listener defined in LeadLifeJSHelpers.
    2023/08/07 1.015   DG Replace qualified references to _LeadLifeJSHelpers
                          constants with references to like-named constants that
                          are defined in LLCommon, eliminating the need for the
                          local declarations.
    2023/08/28 1.016   DG Account for consolidating DoAjax and LogException into
                          LLCommon.js.
    2023/08/28 1.017   DG Move local function UpdateIfChanged to class library
                          LeadLifeJSHelpers, in which it is exposed as a public
                          method.
    2023/09/12 1.018   DG 1) Preserve and restore the search criteria for reuse
                             when the search button is selected.
                          2) Preserve the lead ID so that a page refresh can
                             restore the page with the selected lead when a lead
                             is selected from the search list.
    2023/09/15 1.019   DG 1) Hide the lead ID cell in the search results grid.
                          2) In function DoDisplayContact, set _leadid along
                             with STTLeadId on the LeadLifeJSHelpers object.
    2023/09/17 1.020   DG Add script source, version, and last modified date to
                          major console log messages.
    2024/05/11 1.021   DG Replace virtually all calls to console.log with calls
                          to LLCommon.Trace, which can be centrally configured
                          to suppress logging.
    2024/07/19 1.022   DG Substitute LLCommon.ShowOrHideElement for the like
                          named function defined in LeadLifeJSHelpersGlobals.js.
    2024/08/25 1.023   DG Adapt DialNumber in Words2Actions_Recorder_Form to fit
                          the mobile page.
    2024/09/05 1.024   DG Implement LLCommon.RegisterReturnKeyWatchdog in place
                          of the local event listener.
    2024/10/15 1.025   DG Improve the efficiency of ClearSearchResultsGrid by
                          having it set the innerHTML to the empty string, and
                          leave the last lead record visible when the search
                          query and/or its result grid is visible.
    2024/10/17 1.026   DG Grab the values from the Wise Agent Contact record and
                          store them into any Property Search Criteria records
                          that are subsequently displayed.
    2024/10/20 1.027   DG Implement a Load event listener to position the logout
                          button in the upper right corner of the screen.
    2024/10/21 1.028   DG Display a message under the Create Notes button when a
                          Property Search Criteria record is displayed.
    2024/10/23 1.029   DG Prevent the hard error that arises when a local search
                          happens before the CRM search button is activated.
    2024/11/04 1.030   DG Remove the code that forces the logout button to the
                          far right corner of the viewport.
    ============================================================================
*/


console.log ( ScriptInfoForLog ( LeadLife_Mobile_Index_Page_SCRIPTSOURCE , LeadLife_Mobile_Index_Page_VERSION , LeadLife_Mobile_Index_Page_LastUpdated , 'loading' ) );

debugger;

const LEAD_ID_IS_IN_URL                     = 'LeadIdIsInUrl';
const LEAD_ID_FORM_STORAGE_ELEMENT_ID       = 'leadid';
const SEARCH_PARAM_INPUT_CONTROL_NAME       = 'SearchParam';
const SELECTED_BUTTON_IS_SEARCH4CONTACTS    = 'Search4Contacts';

const SEARCH_PARAM_QUERY_COOKIE             = LeadLife_Mobile_Index_Page_SCRIPTSOURCE + UNDERSCORE_CHAR + SEARCH_PARAM_INPUT_CONTROL_NAME;
const SEARCH_PARAM_SELECTED_LEAD_ID_COOKIE  = LeadLife_Mobile_Index_Page_SCRIPTSOURCE + UNDERSCORE_CHAR + LEAD_ID_FORM_STORAGE_ELEMENT_ID;

const _avarWords2ActionOperatingParameters  = [
                                                    { 'Search4Contacts' : { 'Selected_Button_Id'      : 'Search4Contacts' ,
                                                                            'DeSelected_Button_Id'    : 'DisplayContact' ,
                                                                            'Selected_Button_Color'   : 'Add=WhiteOnBlue,Rem=BlueOnWhite' ,
                                                                            'DeSelected_Button_Color' : 'Add=BlueOnWhite,Rem=WhiteOnBlue' ,
                                                                            'Div_ID_to_Show'          : 'leadlife_mobile_search_view' ,
                                                                            'Div_ID_to_Hide'          : 'leadlife_mobile_contact_view'
                                                                          }
                                                    },
                                                    { 'DisplayContact'  : { 'Selected_Button_Id'      : 'DisplayContact' ,
                                                                            'DeSelected_Button_Id'    : 'Search4Contacts' ,
                                                                            'Selected_Button_Color'   : 'Add=WhiteOnBlue,Rem=BlueOnWhite' ,
                                                                            'DeSelected_Button_Color' : 'Add=BlueOnWhite,Rem=WhiteOnBlue' ,
                                                                            'Div_ID_to_Show'          : 'leadlife_mobile_contact_view' ,
                                                                            'Div_ID_to_Hide'          : null
                                                                          }
                                                    }
                                              ];
var _intWords2ActionLeadIdColumnIndex       = ARRAY_INVALID_INDEX;

window.addEventListener ( 'DOMContentLoaded', ( event ) =>
{
    console.log ( LeadLife_Mobile_Index_Page_SCRIPTSOURCE + ' version ' + LeadLife_Mobile_Index_Page_VERSION + ' last updated ' +  LeadLife_Mobile_Index_Page_LastUpdated + ' DOMContentLoaded event listener STARTING' );

    debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

    const QuerySessionStorage4LeadIdCookie = ( ) =>
    {
        const  strLeadIdFromCookie = sessionStorage.getItem ( SEARCH_PARAM_SELECTED_LEAD_ID_COOKIE );
        return strLeadIdFromCookie === null ? NO_LEAD_ID : parseInt ( strLeadIdFromCookie );
    }   // const QuerySessionStorage4LeadIdCookie = ( ) =>


    if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.Prefix !== undefined )
    {
        ShowOrHideElement ( 'SearchCRM',
                            LLCommon.ELEMENT_SHOW );
        debugger;                                       // Though ESLint complains about it, this breakpoint is indispensable for testing.
        ShowOrHideElement ( 'UpdateCRMNow',
                            LLCommon.ELEMENT_SHOW );
        ShowOrHideElement ( 'UpdateCRMNow',
                            LLCommon.ELEMENT_SHOW );
    }   // if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.Prefix !== undefined )

    var intLeadId = GetLeadOrCrmIdFromUrl ( true );

    if ( intLeadId > NO_LEAD_ID )
    {
        const docLeadViewLeaId              = document.getElementById ( LEAD_ID_FORM_STORAGE_ELEMENT_ID );

        docLeadViewLeaId.value              = intLeadId;

        ShowOrHideElement ( 'leadlife_mobile_tabs_view' ,
                            LLCommon.ELEMENT_SHOW );
        ShowOrHideElement ( 'leadlife_mobile_search_view' ,
                            LLCommon.ELEMENT_HIDE );
        ShowOrHideElement ( 'leadlife_mobile_contact_view' ,
                            LLCommon.ELEMENT_SHOW );

        ToggleTabs ( LEAD_ID_IS_IN_URL );
    }   // TRUE (A lead ID has been selected either directly or indirectly by way of an ExternalCRMId.) block, if ( intLeadId > NO_LEAD_ID )
    else
    {
        $ ( '.leadlife_button_collection').each ( function ( index )
        {
            this.addEventListener ( 'click' ,
                                     ToggleTabs );
            LLCommon.Trace ( 'ToggleTabs click event registered on button ' + index + ": " + $ ( this ).attr ( 'id' ) );
        });
    }   // FALSE (The query string provided neither a lead ID or an External CRM ID that could be used to look one up.) block, if ( intLeadId > NO_LEAD_ID )

    const docResetButton = document.getElementById ( 'cancel' );

    if ( Object.is ( docResetButton , null ) )
    {
        LLCommon.Trace ( 'The cancel button is absent from this form.' );
    }   // TRUE (The form is devoid of a cancel button.) block, f ( Object.is ( docResetButton , null ) )
    else
    {
        docResetButton.addEventListener ( 'click', ( poEventTarget ) =>
        {
            poEventTarget.currentTarget.form.reset ( );
        });

        LLCommon.Trace ( 'Click event registered for the cancel button.' );
    }   // FALSE (The form has a cancel button, against which a click event was registered.) block, // if ( !Object.is ( docResetButton , null ) )

    $( '.input_group_updatable' ).each ( function ( index )
    {
        this.addEventListener ( 'blur' ,
                                UpdateIfChanged );
        LLCommon.Trace ( 'UpdateIfChanged blur event registered on button ' + index + ": " + $ ( this ).attr ( 'id' ) );
    });

    InputMask_Engine.Reload ( );

    intLeadId                           = intLeadId === null ? null : QuerySessionStorage4LeadIdCookie ( );

    if ( intLeadId === null || intLeadId === NO_LEAD_ID )
    {
        const strSearchParam = sessionStorage.getItem ( SEARCH_PARAM_SELECTED_LEAD_ID_COOKIE );

        if ( strSearchParam !== null )
        {
            const docSearchParamInput   = document.getElementById ( 'SearchParam' );
            docSearchParamInput.value   = strSearchParam;
        }   // if ( strSearchParam !== null )

        ToggleTabs ( 'Search4Contacts' );
        document.getElementById ( 'SearchParam' ).focus ( );
    }   // TRUE (Neither the URL, nor session storage, supplied a lead ID.) block, if ( intLeadId === null || intLeadId === NO_LEAD_ID )
    else
    {
        DoDisplayContact ( intLeadId );
    }   // FALSE (Display the most recently selected lead.) block, if ( intLeadId === null || intLeadId === NO_LEAD_ID )

    LLCommon.RegisterReturnKeyWatchdog ( 'SearchParam' ,
                                         PopulateSearchGrid );

    console.log ( LeadLife_Mobile_Index_Page_SCRIPTSOURCE + ' version ' + LeadLife_Mobile_Index_Page_VERSION + ' last updated ' +  LeadLife_Mobile_Index_Page_LastUpdated + ' DOMContentLoaded event listener DONE!' );
});

//  +--------------------------------------------------------------------------+
//  |                     H e l p e r   F u n c t i o n s                      |
//  +--------------------------------------------------------------------------+

function ApplyStyles2Button ( pdocElement , pstrColorInfo )
{
    /*
        ------------------------------------------------------------------------
        ApplyStyles2Button      Use the list of CSS selectors specified by
                                pstrColorInfo to apply or remove styles on the
                                button identified by pdocElement.

        Inputs:                 pdocElement     = This argument is a JavaScript
                                                  object that references an HTML
                                                  element.

                                pstrColorInfo   = This argument is a JavaScript
                                                  string composed of a delimited
                                                  list of CSS selectors to be
                                                  added to or removed from the
                                                  element to which pdocElement
                                                  points.

        Outputs:                The value returned by this function is undefined
                                and it should be treaded as a void function.
        ------------------------------------------------------------------------
    */

    const strMethodName      = GetNameOfCurrentFunction ( );

    const astrStyleInfoItems = pstrColorInfo.split ( ',' );

    for ( var intJ = ARRAY_FIRST_ELEMENT,
              intK = astrStyleInfoItems.length;
              intJ < intK;
              intJ++ )
    {
        var astrVerbObject = astrStyleInfoItems [ intJ ].split ( '=' );

        switch ( astrVerbObject [ 0 ] )
        {
            case 'Add':
                pdocElement.classList.add    ( astrVerbObject [ 1 ] );
                break;
            case 'Rem':
                pdocElement.classList.remove ( astrVerbObject [ 1 ] );
                break;
        }   // switch ( astrVerbObject [ 0 ] )
    }   // for ( var intJ = 0, intK = astrStyleInfoItems.length; intJ < intK; intJ++ )
}   // function ApplyStyles2Button


function ClearSearchResultsGrid ( pdocSearchResultsTable )
{
    const strMethodName      = GetNameOfCurrentFunction ( );

    //  The NEW way:

    pdocSearchResultsTable.innerHTML = EMPTY_STRING;

    //  The OLD way:

    //  ------------------------------------------------------------
    //  Delete rows in the table, working from the bottom up.
    //  ------------------------------------------------------------

    //  for ( var i = pdocSearchResultsTable.rows.length - _LeadLifeJSHelpers.NUMERIC_PLUS_ONE;
    //            i > ARRAY_INVALID_INDEX;
    //            i-- )
    //  {
    //      pdocSearchResultsTable.deleteRow ( i );
    //  }   // for ( var i = pdocSearchResultsTable.rows.length - _LeadLifeJSHelpers.NUMERIC_PLUS_ONE; i > ARRAY_INVALID_INDEX; i-- )
}   // function ClearSearchResultsGrid


function DialNumber ( )
{
    const strMethodName = GetNameOfCurrentFunction ( );

    debugger;

    try
    {
        LLCommon.ManageCallButton ( document.getElementById ( LEAD_ID_FORM_STORAGE_ELEMENT_ID ).value );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function DialNumber


function DoDisplayContact ( pintSelectedLeadId )
{
    /*
        ------------------------------------------------------------------------
        Function Name:      DoDisplayContact

        Function Goal:      Invoke routine _LeadLifeJSHelpers.HandleFormPrefill,
                            followed by sibling function ToggleTabs to display a
                            lead record for interaction.

        Input:              pintSelectedLeadId  = Depending on how it is called,
                                                  the Lead ID may come from one
                                                  of two places, either a form
                                                  element or a session storage
                                                  key.

        Output:             Since this function is evaluated solely for its side
                            effects,  it returns void.

        Remarks:            This routine encapsulates activity that can arise at
                            one of two temporal points in the lifetime of the
                            page that it sits behind and animates.
        ------------------------------------------------------------------------
    */

    const strMethodName         = GetNameOfCurrentFunction ( );

    try
    {
        ShowOrHideElement ( 'leadlife_mobile_tabs_view'    , LLCommon.ELEMENT_SHOW );
        ShowOrHideElement ( 'leadlife_mobile_search_view'  , LLCommon.ELEMENT_HIDE );
        ShowOrHideElement ( 'leadlife_mobile_contact_view' , LLCommon.ELEMENT_SHOW );

        _leadid                         = pintSelectedLeadId;
        _LeadLifeJSHelpers.STTLeadId    = pintSelectedLeadId;
        _leadidSource                   = SRC_IS_FORM_FIELD;

        //  --------------------------------------------------------------------
        //  When LLCommon.EntityType.CRMEntityTypeId = 11 (WA-Contact), save the
        //  values of the non-blank form elements into an array that can be read
        //  by element ID, which corresponds to its decorated custom field name
        //  (SystemProperty) in the Lead record.
        //  --------------------------------------------------------------------

        var aoFieldValues       = null;
        var intNFldVals         = ARRAY_IS_EMPTY;

        debugger;

        LLCommon.ResetCheatSheet ( );

        const strCurrPickLstVal = document.getElementById ( 'CRMSearchableEntities' ).value;

        //  --------------------------------------------------------------------
        //  Since the regular search was implemented before CRM search, it works
        //  fine without it. However, until the Search CRM button is activated,
        //  the CRMSearchableEntities is uninitialized, causing a hard error in
        //  JSON.parse.
        //  --------------------------------------------------------------------

        if ( strCurrPickLstVal.length > EMPTY_STRING_LENGTH )
        {
            const oCurrSelCriteria  = JSON.parse ( strCurrPickLstVal );

            switch ( oCurrSelCriteria.EntityId )
            {
                case 10:
                    intNFldVals     = LLCommon.SetInputValuesFromStringifiedArray ( sessionStorage.getItem ( LLCommon.WA_CONTACT_MOBILEPAGE ) );
                    console.log ( strMethodName + ': Field values restored to identify record for CRM Entity Type = ' + oCurrSelCriteria.EntityId + ': ' + oCurrSelCriteria.EntityName + ', Count = ' + intNFldVals );

                    break;          // case 10

                case 11:
                    aoFieldValues   = LLCommon.GetInputValuesFromContainer ( _LeadLifeJSHelpers.HandleFormPrefill ( 'leadlife_mobile_contact_view' ,
                                                                                                                     pintSelectedLeadId ) ,
                                                                             'leadid' + LOGICAL_NEGATE + 'SysCRMLeadOrContact' + LOGICAL_NEGATE + 'ExternalCRMId' + LOGICAL_NEGATE + 'media' );
                    console.log ( strMethodName + ': Field values saved from record for CRM Entity Type = ' + oCurrSelCriteria.EntityId + ': ' + oCurrSelCriteria.EntityName + ', Count = ' + aoFieldValues.length );
                    sessionStorage.setItem ( LLCommon.WA_CONTACT_MOBILEPAGE , JSON.stringify ( aoFieldValues ) );

                    break;          // case 11
            }   // switch ( oCurrSelCriteria.EntityId )

            LLCommon.ShowCRMEntityMessages ( oCurrSelCriteria.EntityName );
        }   // TRUE (The new CRM Search feature is active.) block, if ( strCurrPickLstVal.length > EMPTY_STRING_LENGTH )
        else
        {
            aoFieldValues   = LLCommon.GetInputValuesFromContainer ( _LeadLifeJSHelpers.HandleFormPrefill ( 'leadlife_mobile_contact_view' ,
                                                                                                             pintSelectedLeadId ) );
        }   // FALSE (Only the original SalesTalk/LeadLife database search is active.) block, if ( strCurrPickLstVal.length > EMPTY_STRING_LENGTH )

        ToggleTabs ( 'DisplayContact' );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
        console.error ( 'JS Function ' + strMethodName ( ) + ': An exception arose while selecting a lead record. Exception = ' + ex.message + ', Stack Trace = ' + ex.stack );
    }
}   // function DoDisplayContact


function DoWords2Action ( poClickEventEvent )
{
    /*
        ------------------------------------------------------------------------
        Function Name:      DoWords2Action

        Function Goal:      Invoke the server-side routine that uses CallRail to
                            record a message and upload it to DeepGram to create
                            a transcript.

        Input:              poClickEventEvent   = Event object, including a
                                                  reference to the DOM element
                                                  that gave rise to the event

        Output:             Since this function is coded as an event listener,
                            it returns void.

        Remarks:            Though this routine is superseded by the mechanism
                            that uses the recorder that is built into all modern
                            Web browsers that support HTML5, I am leaving it in
                            case we need it again for another use case.
        ------------------------------------------------------------------------
    */

    const strMethodName           = GetNameOfCurrentFunction ( );

    try
    {
        debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

        const strPhoneNumber2Call = LLCommon.DoAjax ( 'GetSystemConfigurationMonikorValue',
                                                      'GET',
                                                      {
                                                          'Monikor' : 'Words2Action'
                                                      } );

        if ( strPhoneNumber2Call.length > EMPTY_STRING_LENGTH )
        {
            const docLeadViewLeaId  = document.getElementById ( LEAD_ID_FORM_STORAGE_ELEMENT_ID );
            const strLeadIdString   = docLeadViewLeaId.value;
            const strCallResult     = LLCommon.DoAjax ( 'Words2Action',
                                                        'GET',
                                                        {
                                                            'LeadId' : strLeadIdString,
                                                            'Email'  : localStorage['UserName']
                                                        },
                                                        false );
            LLCommon.Trace ( strCallResult );
        }   // TRUE (anticipated outcime) block, if ( strPhoneNumber2Call.length > EMPTY_STRING_LENGTH )
        else
        {
            const strMessage = 'GetSystemConfigurationMonikorValue cannot find the required Words2Action key in the System Configuration table.';
            LLCommon.LogException ( strMessage );
            console.error ( 'JS Function ' + GetNameOfCurrentFunction ( ) + ': An exception arose while taking dictation. The Words2Action key in the System Configuration table is missing or empty.' );
            alert ( 'JS Function ' + strMethodName + ': An exception arose while recording Words2Action on the SalesTalk mobile application page.'
                    + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
                    + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
                    + 'Please contact SalesTalk customer support for assistance.' );
        }   // FALSE (unanticipated outcime) block, if ( strPhoneNumber2Call.length > EMPTY_STRING_LENGTH )
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
        console.error ( 'JS Function ' + strMethodName + ': An exception arose while taking dictation. Exception = ' + ex.message + ', Stack Trace = ' + ex.stack );
        alert ( 'JS Function ' + strMethodName + ': An exception arose while recording Words2Action on the SalesTalk mobile application page.'
            + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
            + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
            + 'Please contact SalesTalk customer support for assistance.' );
    }
}   // function DoWords2Action


function PopulateSearchGrid ( )
{
    const strMethodName  = GetNameOfCurrentFunction ( );

    debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

    const strSearchParam = document.getElementById ( 'SearchParam' ).value;

    if ( strSearchParam.length > EMPTY_STRING_LENGTH )
    {
        const aobjLeadList   = LLCommon.DoAjax ( 'GetLeadSearchList',
                                                 'GET',
                                                 {
                                                     'searchparam'       : strSearchParam,
                                                     'Columns2Include'   : 'LeadId,Top=50,NeverNull,UnFiltered~FirstName+LastName=Name~Title~CompanyName' ,
                                                     'tenantId'          : _LeadLifeJSHelpers.STTTenantId ,
                                                     'domainId'          : _LeadLifeJSHelpers.STTDomainId ,
                                                     'ExtraFilters'      :   ' AND [tenantid] = ' + _LeadLifeJSHelpers.STTTenantId
                                                                           + ' AND domainId = '   + _LeadLifeJSHelpers.STTDomainId
                                                                           + ' AND [deleted] = 0',
                                                     'ExtraParamFilters' : 'Email,MobilePhone,WorkPhone'
                                                 } );

        if ( Array.isArray ( aobjLeadList ) )
        {
            if ( aobjLeadList.length > ARRAY_IS_EMPTY )
            {
                sessionStorage.setItem ( SEARCH_PARAM_QUERY_COOKIE ,
                                         strSearchParam );

                //  ------------------------------------------------------------
                //  Get a reference to table element in HTML that holds the list
                //  of leads that meet the search parameter.
                //  ------------------------------------------------------------

                const docSearchResultsTable = document.getElementById ( 'docSearchResultsGrid' );

                ClearSearchResultsGrid ( docSearchResultsTable );

                //  ------------------------------------------------------------
                //  Create header row and take a reference so that it can be
                //  edited.
                //  ------------------------------------------------------------

                const headerRow     = document.createElement ( 'tr' );

                //  ------------------------------------------------------------
                //  Loop through keys of the first object in data to create
                //  table headers, setting _intWords2ActionLeadIdColumnIndex
                //  equal to the index of the column that holds the lead ID,
                //  which will be used later to retrieve the Lead ID from the
                //  row selected by the user.
                //  ------------------------------------------------------------

                Object.keys ( aobjLeadList [ _LeadLifeJSHelpers.ARRAY_FIRST_ELEMENT ] ).forEach ( ( key ) =>
                {
                    debugger;                               // Though ESLint complains about it, this breakpoint is indispensable for testing.

                    const headerCell                        = document.createElement ( 'th' );
                    headerCell.innerHTML                    = key;

                    if ( key.toLowerCase ( ) === LEAD_ID_FORM_STORAGE_ELEMENT_ID )
                    {
                        _intWords2ActionLeadIdColumnIndex   = headerRow.cells.length;

                        //  ----------------------------------------------------
                        //  AddOrRemoveCssSelector is more efficient for a brand
                        //  new element that has no CSS style selectors assigned
                        //  to it because it performs a single operation that
                        //  adds or removes a single selector. Conversely,
                        //  ShowOrHideElement also attempts to remove a selector
                        //  that is always absent.
                        //  ----------------------------------------------------

                        AddOrRemoveCssSelector ( headerCell ,
                                                 'STT_HideElement' ,
                                                 CSS_SELECTOR_ADD )
                    }   // TRUE (Hide the LeadId column, which exists solely to store a value for the Click event.) block, if ( key.toLowerCase ( ) === LEAD_ID_FORM_STORAGE_ELEMENT_ID )
                    else
                    {
                        AddOrRemoveCssSelector ( headerCell ,
                                                 'leadlife_mobile_row_label' ,
                                                 CSS_SELECTOR_ADD )
                    }   // FALSE (Apply the label row style to the remaining columns.) block, if ( key.toLowerCase ( ) === LEAD_ID_FORM_STORAGE_ELEMENT_ID )

                    headerRow.appendChild ( headerCell );
                });

                //  ------------------------------------------------------------
                //  Add header row to table.
                //  ------------------------------------------------------------

                docSearchResultsTable.appendChild ( headerRow );

                //  ------------------------------------------------------------
                //  Loop through data and create rows.
                //  ------------------------------------------------------------

                aobjLeadList.forEach ( ( item ) =>
                {
                    const row                        = document.createElement ( 'tr' );

                    row.className                    = 'leadlife_mobile_row_clickable';
                    row.title                        = 'Click or tap a row to display more info about a contact and to interact with it.';

                    row.addEventListener ( 'click' , SelectLeadIdByRow );

                    const intRowIndex                = docSearchResultsTable.rows.length;        // Since the header row counts, the index of the first detail row is 1.
                    const strLeadListRowId           = 'LeadList_Row_' + intRowIndex;

                    row.id = strLeadListRowId;

                    //  --------------------------------------------------------
                    //  Loop through keys of each object to create cells.
                    //  --------------------------------------------------------

                    Object.keys ( item ).forEach ( ( key ) =>
                    {
                        const detailCell             = document.createElement ( 'td' );
                        detailCell.innerHTML         = item [ key ] + '&nbsp;&nbsp;'

                        if ( key.toLowerCase ( ) === LEAD_ID_FORM_STORAGE_ELEMENT_ID )
                        {
                            AddOrRemoveCssSelector ( detailCell ,
                                                     'STT_HideElement' ,
                                                     CSS_SELECTOR_ADD );
                        }   // TRUE (The cell contains the LeadId. Hide it.) block, if ( key.toLowerCase ( ) === LEAD_ID_FORM_STORAGE_ELEMENT_ID )
                        else
                        {
                            AddOrRemoveCssSelector ( detailCell ,
                                                     'leadlife_mobile_row_clickable' ,
                                                     CSS_SELECTOR_ADD );
                        }   // FALSE (Decorate other cells in the row so that they are explicitly visible.) block, if ( key === LEAD_ID_FORM_STORAGE_ELEMENT_ID )

                        row.appendChild ( detailCell );
                    });

                    //  --------------------------------------------------------
                    //  Append the row to the table that was identified
                    //  immediately after the AJAX call.
                    //  --------------------------------------------------------

                    docSearchResultsTable.appendChild ( row );
                });
            }   // TRUE (anticipated outcome) block, if ( aobjLeadList.length > ARRAY_IS_EMPTY )
            else
            {
                sessionStorage.removeItem ( SEARCH_PARAM_QUERY_COOKIE );
                alert ( 'No leads matched your search parameter, "' + strSearchParam + '" - please try again.' );
            }   // FALSE (unanticipated outcome) block, if ( aobjLeadList.length > ARRAY_IS_EMPTY )
        }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aobjLeadList ) )
        else
        {
            sessionStorage.removeItem ( SEARCH_PARAM_QUERY_COOKIE );
            const strMsg = 'At ' + location.href + ', in code-behind function PopulateSearchGrid, server routine GetLeadSearchList advises that an exception arose and was dutifully logged on its end.'
            LLCommon.LogException ( strMsg );
            console.error ( 'In code-behind JS Function ' + strMethodName + ', server routine GetLeadSearchList advised of the following Exception: ' + aobjLeadList );
            alert ( 'JS Function ' + strMethodName + ': An exception arose while processing a lead search query.'
                + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
                + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
                + 'Please contact SalesTalk customer support for assistance.' );
        }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aobjLeadList ) )
    }   // TRUE (anticipated outcome) block, if ( strSearchParam.length > EMPTY_STRING_LENGTH )
    else
    {
        alert ( 'Please input a search term.' );
        sessionStorage.removeItem ( SEARCH_PARAM_QUERY_COOKIE );
    }   // FALSE (unanticipated outcome) block, if ( strSearchParam.length > EMPTY_STRING_LENGTH )
}   // function PopulateSearchGrid


function SelectLeadIdByRow ( poClickEventEvent )
{
    const strMethodName                     = GetNameOfCurrentFunction ( );

    try
    {
        debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

        const docClickedElement             = document.getElementById ( $.type ( poClickEventEvent ) === 'string' ? poClickEventEvent : poClickEventEvent.currentTarget.id );
        const docLeadIdCell                 = docClickedElement.cells [_intWords2ActionLeadIdColumnIndex];
        const intSelectedLeadId             = parseInt ( docLeadIdCell.innerText );
        const docLeadViewLeaId              = document.getElementById ( LEAD_ID_FORM_STORAGE_ELEMENT_ID );
        docLeadViewLeaId.value              = intSelectedLeadId;
//        docLeadViewLeaId.valueAsNumber      = intSelectedLeadId;              // This property is reserved for Number types.

        sessionStorage.setItem ( SEARCH_PARAM_SELECTED_LEAD_ID_COOKIE ,
                                 intSelectedLeadId );
        DoDisplayContact ( intSelectedLeadId );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
        console.error ( 'JS Function ' + strMethodName + ': An exception arose while selecting a lead record. Exception = ' + ex.message + ', Stack Trace = ' + ex.stack );
        alert ( 'JS Function ' + strMethodName + ': An exception arose while processing a selection from the search responses on the Words2Actions mobile SalesTalk application page.'
            + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
            + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR
            + 'Please contact SalesTalk customer support for assistance.' );
    }
}   // function SelectLeadIdByRow


function ToggleTabs ( poClickEventEvent )
{
    const strMethodName                             = GetNameOfCurrentFunction ( );

    try
    {
        debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.

        var docClickedElement;

        if ( poClickEventEvent !== LEAD_ID_IS_IN_URL )
        {
            docClickedElement                       = document.getElementById ( $.type ( poClickEventEvent ) === 'string' ? poClickEventEvent : poClickEventEvent.currentTarget.id );

            if ( docClickedElement.id === SELECTED_BUTTON_IS_SEARCH4CONTACTS )
            {
                const strSearchParamCookie          = sessionStorage.getItem ( SEARCH_PARAM_QUERY_COOKIE );

                if ( strSearchParamCookie === null )
                {
                    ClearSearchResultsGrid ( document.getElementById ( 'docSearchResultsGrid' ) );
                }   // TRUE (The session is brand new.) block, if ( strSearchParamCookie === null )
                else
                {
                    document.getElementById ( SEARCH_PARAM_INPUT_CONTROL_NAME ).value = strSearchParamCookie;
                }   // FALSE (The session is established, and it has a saved search parameter.) block, if ( strSearchParamCookie === null )
            }   // if ( docClickedElement.id === SELECTED_BUTTON_IS_SEARCH4CONTACTS )

            const docSearchTermsGrid                = document.getElementById ( 'docSearchTermsGrid' );
            const docCRMSearchResultsGrid           = document.getElementById ( 'docCRMSearchResultsGrid' );

            if ( ( docSearchTermsGrid !== null && docSearchTermsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) || ( docCRMSearchResultsGrid !== null && docCRMSearchResultsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) )
            {
                console.log ( strMethodName + ': leaving the existing record visible because it can coexist with CRM search.' );
            }   // TRUE (CRM search can display side by side with a SalesTalk lead record.) block, if ( ( docSearchTermsGrid !== null && docSearchTermsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) || ( docCRMSearchResultsGrid !== null && docCRMSearchResultsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) )
            else
            {
                const varActionParameterMap         = _avarWords2ActionOperatingParameters.find ( obj => Object.keys ( obj ) [ 0 ] === docClickedElement.id );

                const docElement2Show               = document.getElementById ( varActionParameterMap [ docClickedElement.id ] [ 'Div_ID_to_Show' ] );
                const docElement2Hide               = document.getElementById ( varActionParameterMap [ docClickedElement.id ] [ 'Div_ID_to_Hide' ] );

                const docSelectedButtonId           = document.getElementById ( varActionParameterMap [ docClickedElement.id ] [ 'Selected_Button_Id'   ] );
                const docDeSelectedButtonId         = document.getElementById ( varActionParameterMap [ docClickedElement.id ] [ 'DeSelected_Button_Id' ] );

                const strSelectedButtonColorInfo    = varActionParameterMap [ docClickedElement.id ] [ 'Selected_Button_Color'   ];
                const strDeSelectedButtonColorInfo  = varActionParameterMap [ docClickedElement.id ] [ 'DeSelected_Button_Color' ];

                if ( docElement2Show !== null )
                {
                    LLCommon.ShowOrHideElement ( docElement2Show ,
                                                 LLCommon.ELEMENT_SHOW );
                    ApplyStyles2Button ( docSelectedButtonId ,
                                         strSelectedButtonColorInfo );
                }   // if ( docElement2Show !== null )

                if ( docElement2Hide !== null )
                {
                    LLCommon.ShowOrHideElement ( docElement2Hide ,
                                                 LLCommon.ELEMENT_HIDE );
                    ApplyStyles2Button ( docDeSelectedButtonId ,
                                         strDeSelectedButtonColorInfo );
                }   // if ( docElement2Hide !== null )
            }   // FALSE (In the absence of a CRM search form and/or results, reset for a SalesTalk search.) block, if ( ( docSearchTermsGrid !== null && docSearchTermsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) || ( docCRMSearchResultsGrid !== null && docCRMSearchResultsGrid.className.indexOf ( 'STT_ShowElement' ) > INDEXOF_NOT_FOUND ) )
        }   // if ( poClickEventEvent !== LEAD_ID_IS_IN_URL )

        if ( poClickEventEvent === LEAD_ID_IS_IN_URL || docClickedElement.id === 'DisplayContact' )
        {
            const docFirstName = document.getElementById ( 'FirstName' );
            const docLastName  = document.getElementById ( 'LastName'  );
            const docFulltName = document.getElementById ( 'FulltName' );

            docFulltName.value =   docFirstName.value
                                 + SPACE_CHARACTER
                                 + docLastName.value;
//            $ ( '#NextActionDate' ).datetimepicker ( );
        }   // if ( poClickEventEvent === LEAD_ID_IS_IN_URL || docClickedElement.id === 'DisplayContact' )

        LLCommon.Trace ( 'JS function ' + strMethodName + ': Done for button having ID = ' + ( poClickEventEvent === LEAD_ID_IS_IN_URL ? LEAD_ID_IS_IN_URL : docClickedElement.id ) );
    }
    catch ( ex )
    {
        if ( ex.message === "Cannot read properties of undefined (reading 'allowTimes')" )
        {
            LLCommon.Trace ( LLCommon.LogException ( 'JS Function ' + strMethodName + ': An exception arose while activating a tab. Exception = ' + ex.message ) );
        }   // TRUE (baffling, but anticipated, outcome) block, if ( ex.message === "Cannot read properties of undefined (reading 'allowTimes')" )
        else
        {
            LLCommon.LogException ( ex );
        }   // FALSE (anticipated outcome) block, if ( ex.message === "Cannot read properties of undefined (reading 'allowTimes')" )E (un
    }   // catch block in ToggleTabs
}   // function ToggleTabs

console.log ( ScriptInfoForLog ( LeadLife_Mobile_Index_Page_SCRIPTSOURCE , LeadLife_Mobile_Index_Page_VERSION , LeadLife_Mobile_Index_Page_LastUpdated , 'loaded' ) );