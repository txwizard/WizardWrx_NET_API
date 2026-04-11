/*eslint-env browser*/
/*global $ _domainid _leadid _login _leadidSource _LeadLifeJSHelpers _pagename _pagenameSource _tenantid _userid AddOrRemoveCssSelector ARRAY_FIRST_ELEMENT ARRAY_INVALID_INDEX ARRAY_IS_EMPTY ARRAY_NOT_EMPTY ARRAY_SECOND_ELEMENT ARRAY_THIRD_ELEMENT bootbox CSS_SELECTOR_ADD CSS_SELECTOR_REMOVE ELEMENT_HIDE ELEMENT_SHOW EMPTY_STRING EMPTY_STRING_LENGTH EQUALS_CHAR GetNameOfCurrentFunction GetParameterFromURLFormOrLocalStorage HTML_NBSP INDEXOF_NOT_FOUND JQUERY_SELECTOR_IS_ELEMENT_ID LLCommon LOGICAL_NEGATE NO_LEAD_ID NUMERIC_ZERO OrdinalFromIndex PIPE_CHAR_SPLIT_MATCH PopulateSearchGrid QUOTE_SINGLE ScriptInfoForLog SPACE_CHARACTER SPLIT_NAME_FROM_VALUE STTProcessMedia SUBSTRING_FIRST_CHAR UNDERSCORE_CHAR UpdateIfChanged*/
"use strict";

const Agent_Recent_Phone_Calls_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const Agent_Recent_Phone_Calls_VERSION      = 1.012;
const Agent_Recent_Phone_Calls_LogTraces    = false;
const Agent_Recent_Phone_Calls_LastUpdated  = '2026/03/05 20:52:06 CST'

/*

    ============================================================================
    Name:               Agent_Recent_Phone_Calls.js

    Goal:               Define custom JavaScript functions used by the
                        Agent Recent Calls List form.

    Dependencies:       The code defined in this module requires working JQuery
                        and LLCommon objects, which the calling page is expected
                        to supply via deferred loading.

    Remarks:            The DOMContentLoaded event listener defined in this
                        script invokes function LoadAgentRecentCallsList, which
                        calls the server code and processes its return to build
                        a read-only Kendo UI grid.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By Remark/Brief Description
    ---------- ------- -- ------------------------------------------------------
    2025/07/13 1.000   DG MVP: New code written from scratch.
    2025/08/01 1.001   DG Add CRM search when first and/or last name is blank.
    2025/08/08 1.002   DG Add the lookup button and color code the View button.
    2025/08/11 1.003   DG Convert call duration to hours, minutes, and seconds,
                          and provide a checkbox that, when checked, is stored
                          in the behavior table.
    2025/08/20 1.004   DG Add the 2Done check box.
    2025/09/02 1.005   DG Make the color coding of the View button and the code
                          that insists on a valid ExternalCRMId more robust.
    2025/09/24 1.006   DG Change `2Done` to `Done`.
    2025/10/01 1.007   DG NeedSomethingFromServer calls CheckForExternalCRMID to
                          decide whether the ExternalCRMID is the empty string,
                          and the server should be called to institute a search
                          of the CRM database for a matching contact.
    2025/11/09 1.008   DG Correct the statement in BuildViewButtonURL that sets
                          the value of the ExternalCRMId token that goes into
                          the URL that causes the Words2Actions form to replace
                          the Recent Calls List form.
    2025/12/29 1.009   DG Move the View button to the left and change its color
                          when it is unlinked to the CRM from stoplight red to
                          stoplight yellow, and relabel the "lookup" button as
                          "Add Info".
    2025/12/30 1.010   DG Resolve the duplicate title attribute in the grid, and
                          add relevant fields and selection criteria to cover
                          incoming email messages processed by Sweeper.
    2025/01/22 1.011   DG Resolve the duplicate title attribute in the grid, and
                          finish adding the new fields to cover Sweeper inbox.
    2025/02/08 1.012   DG Implement the UpdateIsPriorityFlagState check box.
    2025/03/05 1.013   DG Make the Microphone Page the default PageName for the
                          View button.
    ============================================================================
*/

console.log ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                 Agent_Recent_Phone_Calls_VERSION ,
                                 Agent_Recent_Phone_Calls_LastUpdated ,
                                 'loading' ) );

//  ----------------------------------------------------------------------------
//  Define constants for strings that are used at least twice, and integers as
//  symbolic constants for other things, such as array elements that represent
//  relevant values.
//  ----------------------------------------------------------------------------

const ACTION_BUTTON_PREFIX      = 'btnAction_';
const ACTION_CHECKBOX_PREFIX    = 'chkAction_';

const CSS_STT_ARPC_2DONE        = 'STT_ARPC_2DONE';
const CSS_STT_ARPC_VIP          = 'STT_ARPC_VIP';
const CSS_STT_ARPC_PRIORITY     = 'STT_ARPC_PRIORITY';

const NODENAME_IS_BUTTON        = 'BUTTON';
const NODENAME_IS_INPUT         = 'INPUT'

const NODE_TYPE_IS_CHECKBOX     = 'checkbox'

const BUTTON_GROUP_PREFIX       = ARRAY_FIRST_ELEMENT;
const BUTTON_ACTION_VERB        = ARRAY_SECOND_ELEMENT;
const BUTTON_ACTION_BEHAVIONRID = ARRAY_THIRD_ELEMENT;
const BUTTON_ACTION_LEADID      = ARRAY_FOURTH_ELEMENT;
const BUTTON_ID_EXPECTED_PARTS  = LLCommon.OrdinalFromIndex ( BUTTON_ACTION_LEADID );

var   objInitialValue           = null;

const aoRowColorRule            = [
                                      { ApplicationOrder : 1 , UniqueCheckBoxId: 'UpdateIsVIPFlagState'      , CSSClassName : CSS_STT_ARPC_VIP },
                                      { ApplicationOrder : 3 , UniqueCheckBoxId: 'UpdateIsDoneFlagState'     , CSSClassName : CSS_STT_ARPC_2DONE },
                                      { ApplicationOrder : 2 , UniqueCheckBoxId: 'UpdateIsPriorityFlagState' , CSSClassName : CSS_STT_ARPC_PRIORITY }
                                  ];
const astrRowColorSelector      = [ CSS_STT_ARPC_VIP , CSS_STT_ARPC_2DONE ,CSS_STT_ARPC_PRIORITY ];

console.log ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                 Agent_Recent_Phone_Calls_VERSION ,
                                 Agent_Recent_Phone_Calls_LastUpdated ,
                                 ' - Adding DOMContentLoaded event listener defined in the current page' ) );


LLCommon.sortBy ( aoRowColorRule, 'ApplicationOrder');


function ARPCHiglightCheckedRows ( pstrUniqueCheckBoxId , pstrCSSClassName )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
    console.log ( strMethodName + ': Highlight rows where check boxes are CHECKED having ID prefix = chkAction' + UNDERSCORE_CHAR + pstrUniqueCheckBoxId );

    debugger;

    document.querySelectorAll ( "input[id^='chkAction_" + pstrUniqueCheckBoxId + "_']:checked" ).forEach ( pdocCheckBox =>
    {
        ARPCHRemoveRedundantHiglight ( pdocCheckBox ,
                                       pstrCSSClassName );
        LLCommon.AddOrRemoveStyles ( pdocCheckBox.closest ( 'tr' ) ,
                                     pstrCSSClassName ,
                                     LLCommon.CSS_SELECTOR_ADD );
    }); // document.querySelectorAll ( "input[id^='chkAction_" + pstrUniqueCheckBoxId + "_']:checked" ).forEach ( pdocCheckBox =>
}   // function ARPCHiglightCheckedRows


function ARPCHRemoveRedundantHiglight ( pdocCheckBox , pstrCSSClassName )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    const docClosestTableRow    = pdocCheckBox.closest ( 'tr' );                // Hoist the lookup out of the 'for' loop.

    if ( docClosestTableRow !== null )
    {
        for ( var intRowColorCssRule = ARRAY_FIRST_ELEMENT;
                  intRowColorCssRule < astrRowColorSelector.length;
                  intRowColorCssRule++ )
        {
            if ( astrRowColorSelector [ intRowColorCssRule ] !== pstrCSSClassName )
            {
                LLCommon.AddOrRemoveStyles ( docClosestTableRow ,
                                             astrRowColorSelector [ intRowColorCssRule ] ,
                                             LLCommon.CSS_SELECTOR_REMOVE );
            }   // if ( astrRowColorSelector [ intRowColorCssRule ] !== pstrCSSClassName )
        }   // for ( var intRowColorCssRule = ARRAY_FIRST_ELEMENT; intRowColorCssRule < astrRowColorSelector.length; intRowColorCssRule++ )
    }   // TRUE (anticipated outcome) block, if ( docClosestTableRow !== null )
    else {
        throw new Error ( strMethodName + ': The element specified as input parameter pdocCheckBox, ID = ' +  pdocCheckBox.id + ', MUST live inside a HTML Table Row.' );
    }   // FALSE (unanticipated outcome) block, if ( docClosestTableRow !== null )
}   // function ARPCHRemoveRedundantHiglight


window.addEventListener ( 'DOMContentLoaded' , function ( )
{
    const strMethodName = 'DOMContentLoaded';

    LLCommon.Trace ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                        Agent_Recent_Phone_Calls_VERSION ,
                                        Agent_Recent_Phone_Calls_LastUpdated ,
                                        ' - Entering anonymoun DOMContentLoaded event listener function defined in the current page.' ) );

    try
    {
        debugger;

        if ( _useridSource !== SRC_IS_UNKNOWN )
        {
            if ( LLCommon.UserInfo !== null )
            {
                const docAgentGreeting = document.getElementById ( 'agentgreeting' );

                if ( docAgentGreeting )
                {
                    const strAgentGreeting = LLCommon.getTimeOfDayGreeting ( LLCommon.UserInfo.AgentFirstName );

                    if ( LLCommon.IsString ( strAgentGreeting ) && strAgentGreeting.length > EMPTY_STRING_LENGTH )
                    {
                        docAgentGreeting.innerText = strAgentGreeting;
                    }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( strAgentGreeting ) && strAgentGreeting.length > EMPTY_STRING_LENGTH )
                    else
                    {
                        throw new Error ( strMethodName + ': Function LLCommon.getTimeOfDayGreeting returned the empty string.' );
                    }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( strAgentGreeting ) && strAgentGreeting.length > EMPTY_STRING_LENGTH )
                }   // TRRUE (anticipated outcome) block, if ( docAgentGreeting )
                else
                {
                    throw new Error ( strMethodName + ': The agentgreeting element is missing from HTML page ' + location.href );
                }   // FALSE (unanticipated outcome) block, if ( docAgentGreeting )

//                Object.keys ( LLCommon.UserInfo ).forEach ( key =>
//                {
//                    console.log ( 'SalesTalk User ID ' + _userid + ': ' + key + ' = ' + LLCommon.UserInfo [ key ] );
//                    let docHeadingDisplay = document.getElementById ( key );
//
//                    if ( docHeadingDisplay !== null )
//                    {
//                        docHeadingDisplay.value = LLCommon.UserInfo [ key ];
//                    }   // if ( docHeadingDisplay !== null )
//                });
            }   // TRUE (anticipated outcome) block, if ( LLCommon.UserInfo !== null )
            else
            {
                throw new Error ( strMethodName + ': SalesTalk user ID ' + _userid + ' is invalid.' );
            }   // FALSE (unanticipated outcome) block, if ( LLCommon.UserInfo !== null )
        }   // TRUE (anticipated outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
        else
        {
            throw new Error ( strMethodName + ': The required SalesTalk user ID is MISSING.' );
        }   // FALSE (unanticipated outcome) block, if ( _useridSource !== SRC_IS_UNKNOWN )
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }

    try
    {
        LoadAgentRecentCallsList ( );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }

    LLCommon.Trace ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                        Agent_Recent_Phone_Calls_VERSION ,
                                        Agent_Recent_Phone_Calls_LastUpdated ,
                                        ' - Leaving anonymoun DOMContentLoaded function defined in the current page.' ) );
}); // window.addEventListener ( 'DOMContentLoaded' , function ( ) {


LLCommon.Trace ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                    Agent_Recent_Phone_Calls_VERSION ,
                                    Agent_Recent_Phone_Calls_LastUpdated ,
                                    ' - DOMContentLoaded event listener defined in the current page added.' ) );

function LoadAgentRecentCallsList ( e )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    window.boundCheckboxes = window.boundCheckboxes || new WeakSet ( );

    $( '.subBar.subBarMain' ).css ( 'width' , '1475px' );

    var model;
    var columns;

    if ( e !== undefined )
    {
        e.preventDefault ( );
    }

    $( $( '#AgentRecentCallLogGrid' ).attr ( 'data-parent' ) ).show ( );

    columns = [
        {
            field            : 'BehaviorId',
            title            : 'BehaviorId',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : '#=BehaviorId#'
                               },
            hidden           : true
        },
        {
            field            : 'IsDone',
            title            : '√',
            template         : function ( dataRow )
            {
                if ( !dataRow || !dataRow.LeadId ) return EMPTY_STRING;

                const isChecked = dataRow.IsDone ? 'checked="checked"' : EMPTY_STRING;
                return '<input type="checkbox"'
                     + ' class="STT_Kendo_Grid_CheckBox"'
                     + ' id="chkAction_UpdateIsDoneFlagState' + UNDERSCORE_CHAR + dataRow.BehaviorId + UNDERSCORE_CHAR + dataRow.LeadId + QUOTE_DOUBLE + SPACE_CHARACTER
                     + isChecked
                     + '/>';
            },
            width            : '30px',
            hidden           : false,
        },
        {
            field            : 'IsVIPFlag',
            title            : '!',
            template         : function ( dataRow )
            {
                if ( !dataRow || !dataRow.LeadId ) return EMPTY_STRING;

                const isChecked = dataRow.IsVIPFlag ? 'checked="checked"' : EMPTY_STRING;
                return   '<input type="checkbox"'
                       + ' class="STT_Kendo_Grid_CheckBox"'
                       + ' id="chkAction_UpdateIsVIPFlagState' + UNDERSCORE_CHAR + dataRow.BehaviorId + UNDERSCORE_CHAR + dataRow.LeadId + QUOTE_DOUBLE + SPACE_CHARACTER
                       + isChecked
                       + '/>';
            },
            width            : '30px',
            hidden           : false,
        },
        {
            field            : 'IsPriority',
            title            : '$',
            template         : function ( dataRow )
            {
                if ( !dataRow || !dataRow.LeadId ) return EMPTY_STRING;

                const isChecked = dataRow.IsPriority ? 'checked="checked"' : EMPTY_STRING;
                return '<input type="checkbox"'
                     + ' class="STT_Kendo_Grid_CheckBox"'
                     + ' id="chkAction_UpdateIsPriorityFlagState' + UNDERSCORE_CHAR + dataRow.BehaviorId + UNDERSCORE_CHAR + dataRow.LeadId + QUOTE_DOUBLE + SPACE_CHARACTER
                     + isChecked
                     + '/>';
            },
            width            : '30px',
            hidden           : false,
        },
        {
            field            : 'Name',
            title            : 'Name',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : '#= FirstName + SPACE_CHARACTER + LastName #'
                               },
            width            : '150px',
            hidden           : false
        },
        {
            field            : 'CompanyName',
            title            : 'Company',
            attributes       : {
                                    'class' : 'ellipse',
                                    'style' : 'max-width: 200px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;' ,
                                    'title' : '#=CompanyName#'
                               },
            width            : '125px',
            hidden           : false
        },
        {
            field            : 'Direction',
            title            : 'Type',
            attributes       : {
                                    'class' : 'ellipse',
                                    'style' : 'max-width: 125px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;' ,
                                    'title' : '#=CompanyName#'
                               },
            width            : '100px',
            hidden           : false
        },
        {
            field            : 'ExternalPhoneNumber',
            title            : 'Phone/Email',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : "#= BehaviorSource === 'Email' ? 'From' + FromName + '<' + FromAddress + '> Subject: ' + Subject : PhoneNumber #"
                               },
            width            : '250px',
            template         : function ( dataRow ) { return dataRow.BehaviorSource === 'Email' ? dataRow.FromAddress : dataRow.ExternalPhoneNumber; },
            hidden           : false
        },
        {
            field            : 'ExternalCRMId',
            title            : 'ExternalCRMId',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : '#=ExternalCRMId#'
                               },
            hidden           : true
        },
        {
            field            : 'SysCRMLeadOrContact',
            title            : 'SysCRMLeadOrContact',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : '#=SysCRMLeadOrContact#'
                               },
            hidden           : true
        },
        {
            field            : 'BehaviorDate',
            title            : 'Date & Time',
            attributes       : {
                                    'class' : 'ellipse',
                                    'title' : '#= BehaviorDate.split ( LOGICAL_NEGATE ) [ ARRAY_SECOND_ELEMENT ] #' },
            width            : '200px',
            hidden           : false,
            template         : function ( dataRow )
            {
                const strDisplayValue = dataRow.BehaviorDate.split ( LOGICAL_NEGATE ) [ ARRAY_FIRST_ELEMENT ]
                const strDisplayTitle = dataRow.BehaviorDate.split ( LOGICAL_NEGATE ) [ ARRAY_SECOND_ELEMENT ]
                return '<span class="ellipse" title="' + strDisplayTitle + '">' + strDisplayValue + '</span>';
            },
        },
        {
            field            : 'CallDuration',
            title            : 'Time',
            attributes       : {
                                    'class' : 'ellipse',
                                    'style' : 'text-align: right;',
                                    'title' : 'Call Duration, Minudes and Seconds'
                               },
            headerAttributes : {
                                    'style' : 'text-align: right;'
                               },
            width            : '80px',
            template         : function ( dataRow ) { return dataRow.BehaviorSource === 'Email' ? EMPTY_STRING : LLCommon.ConvertSecondsToMinutes ( dataRow.CallDuration , true ); },
            hidden           : false
        },
        {
            field            : 'LeadId',
            title            : 'Words2Actions',
            width            : '250px',
            template         : function ( dataRow )
                               {
                                   const strLookupButton   = EMPTY_STRING;
                                   const strViewButtonCSS  =   ( 'W2A_Recorder_Button_Plain_Box_Sm' + SPACE_CHARACTER )
                                                             + ( ( dataRow !== undefined ) && ( dataRow.LeadId !== undefined ) && ( dataRow.LeadId !== null ) && ( LLCommon.IsString ( dataRow.FirstName ) && ( dataRow.FirstName.length === EMPTY_STRING_LENGTH ) ) && ( LLCommon.IsString ( dataRow.LastName ) && ( dataRow.LastName.length === EMPTY_STRING_LENGTH ) )
                                                               ? 'STT_Stoplight_Red'
                                                               : ( dataRow !== undefined ) && ( dataRow.LeadId !== undefined ) && ( dataRow.LeadId !== null ) && ( LLCommon.IsString ( dataRow.ExternalCRMId ) ) && ( dataRow.ExternalCRMId.length === EMPTY_STRING_LENGTH )
                                                                 ? 'STT_Stoplight_Yellow'
                                                                 : ( dataRow !== undefined ) && ( dataRow.LeadId !== undefined ) && ( dataRow.LeadId !== null ) && ( LLCommon.IsString ( dataRow.ExternalCRMId ) ) && ( dataRow.ExternalCRMId.length > EMPTY_STRING_LENGTH )
                                                                   ? 'STT_Stoplight_Green'
                                                                   : 'STT_Stoplight_Yellow' );
                                   const strOtherButtonCSS = 'W2A_Recorder_Button_Plain_Box_Sm STT_Steel_Blue';
                                   return ( ( ( dataRow == undefined ) || ( dataRow.LeadId == undefined ) || ( dataRow.LeadId == null ) )
                                             ? EMPTY_STRING
                                             :   '<button type="button" id="btnAction_ViewThisLead_'   + dataRow.BehaviorId + '_' + dataRow.LeadId + '" class="' + strViewButtonCSS  + '" title="Click this button to view the contact with whom this call is associated. The Words2Actions form will replace this list.">View</button>'
                                               + '<button type="button" id="btnAction_RemoveThisCall_' + dataRow.BehaviorId + '_' + dataRow.LeadId + '" class="' + strOtherButtonCSS + '" title="Click this button to delete this call from your call history. This row will vanish when you refresh, and future calls from this number will appear on this list.">Remove</button>'
                                               + '<button type="button" id="btnAction_RemoveThisLead_' + dataRow.BehaviorId + '_' + dataRow.LeadId + '" class="' + strOtherButtonCSS + '" title="Click this button to mark this phone number as junk (spam). This row will vanish when you refresh, and the phone number will cease to appear in your list of recent calls.">Block</button>'
                                               + strLookupButton );
                               },
            hidden           : false
        }
    ];

    model = {
        fields : {
            IsDone        : { editable: true,  nullable: true,  type: 'boolean' },
            IsVIPFlag     : { editable: true,  nullable: true,  type: 'boolean' },
            IsPriority    : { editable: true,  nullable: true,  type: 'boolean' },
            FirstName     : { editable: false, nullable: true,  type: 'string'  },
            LastName      : { editable: false, nullable: true,  type: 'string'  },
            CompanyName   : { editable: false, nullable: true,  type: 'string'  },
            PhoneNumber   : { editable: false, nullable: false, type: 'number'  },
            CallDate      : { editable: false, nullable: false, type: 'date'    },
            CallDuration  : { editable: false, nullable: false, type: 'number'  },
            CallDirection : { editable: false, nullable: false, type: 'string'  },
            LeadId        : { editable: false, nullable: false, type: 'number'  }
        }
    };

    var dataSource = new kendo.data.DataSource({
        transport : {
            read : {
                url      : _llAppPath + 'Open/GetRecentCallsByAgentId?AUserId=' +_userid + '&TZOffset=' + ( new Date ( ) ).getTimezoneOffset ( ) + '&ALimit=300&QueryVersion=3' ,
                dataType : 'json'
            }/*,
            parameterMap: function ( data , type )
            {
                return
                {
                    AUserId: $( '#AUserId' ).val ( ),
                    ALimit:  $( '#ALimit' ).val ( )
                }
            }*/
        },
        /*
        sort: [
                { field : 'IsDone'      , dir : 'asc' },
                { field : 'IsVIPFlag'   , dir : 'desc'},
                { field : 'LastName'    , dir : 'asc' },
                { field : 'FirstName'   , dir : 'asc' },
                { field : 'CompanyName' , dir : 'asc' },
                { field : 'Direction'   , dir : 'asc' }
              ],
        */
        pageSize: 1000,
        schema: {
            data  : 'data',
            total : 'total',
            model : model
        }
    });

    if ( $ ( '#AgentRecentCallLogGrid' ).data ( 'kendoGrid' ) !== undefined )
    {
        $( '#AgentRecentCallLogGrid' ).empty ( );
        $( '#AgentRecentCallLogGrid' ).removeData ( 'kendoGrid' );
    }   // if ( $ ( '#AgentRecentCallLogGrid' ).data ( 'kendoGrid' ) !== undefined )

    $ ( '#AgentRecentCallLogGrid' ).kendoGrid (
    {
        dataSource: dataSource,
        theme: $( document ).data ( 'kendoSkin' ) || 'silver',
        sortable: true,
        filterable: {
            operators: {
                string: {
                    startswith : 'Starts with',
                    eq         : 'Is equal to',
                    neq        : 'Is not equal to',
                    contains   : 'Contains',
                    endswith   : 'Ends with'
                },
                date: {
                    gt         : 'Is after',
                    lt         : 'Is before',
                    isnull     : 'Is null',
                    isnotnull  : 'Is not null'
                }
            }
        },
        resizable : true,
        pageable  : true,
        columns   : columns,
        dataBound : function ( )
        {
            debugger;

            //  ------------------------------------------------------------------------
            //  Now that the grid is loaded, register click events for the buttons and
            //  change events to the check boxes.
            //  ------------------------------------------------------------------------

            $( '[id^="btnAction_"]' ).off ( 'click'  ).on ( 'click'  , PerformActionsOnCallListItem );
            $( '[id^="chkAction_"]' ).off ( 'change' ).on ( 'change' , PerformActionsOnCallListItem );

            //  ------------------------------------------------------------------------
            //  Apply colors via CSS class selectors to rows in which one or more boxes
            //  are checked.
            //  ------------------------------------------------------------------------

            debugger;

            for ( var intApplicationOrder = ARRAY_FIRST_ELEMENT;
                      intApplicationOrder < aoRowColorRule.length;
                      intApplicationOrder++ )
            {
                ARPCHiglightCheckedRows ( aoRowColorRule [ intApplicationOrder ].UniqueCheckBoxId , aoRowColorRule [ intApplicationOrder ].CSSClassName );
            }   // for ( var intApplicationOrder = ARRAY_FIRST_ELEMENT; intApplicationOrder < aoRowColorRule.length; intApplicationOrder++ )
        }   // dataBound: function ( )
    }); // $ ( '#AgentRecentCallLogGrid' ).kendoGrid (
}   // function LoadAgentRecentCallsList


function BuildViewButtonURL ( pintLeadId , pfCreateNewCRMRecord , pstrMarkAsRequired )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  BuildViewButtonURL

        Function Goal:  Build a URL in the following format.

                            https://salestalktech.com/SalesAcceleration/COMMON/Words2Actions_Form_TEMPLATE.HTML?pagename=WiseAgentPage&CI=True&login=Richard@SalesTalk.ai&leadid=1514256&CRM=WiseAgent&EntityType=Contact

        Inputs:         pintLeadId              = Integer representation of a
                                                  SalesTalk Lead ID

                        pfCreateNewCRMRecord    = Boolean value to indicate when
                                                  a new record must be added to
                                                  the CRM - See Remarks

                        pstrMarkAsRequired      = Comma delimited string of
                                                  fields (HTML form elements) to
                                                  mark as required - See Remarks

        Output:         The return value is a complete URL, ready to go into the
                        A tag behind a button.

        Remarks:        When the second argument, pfCreateNewCRMRecord, is True,
                        the third argument, pstrMarkAsRequired, is repurposed to
                        pass in the ExternalCRMId so that it can be interpolated
                        into the returned URL.

                        Following is an example of the third argument when the
                        second argument, pfCreateNewCRMRecord, is False.

                            'FirstName,LastName'

                        The list is taken at face value.

                        Since so many moving parts go into a URL, it is built up
                        in steps in this function, and returned ready to use by
                        the calling routine. It appears that the Kendo grid
                        generator calls this once for each View button on the
                        page, so that once the page is built, the URLs are ready
                        for use.

                        This routine is also tasked with setting the encoded
                        return URL to be used by the lost focus event of the
                        page.
        ------------------------------------------------------------------------
    */

    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
    debugger;
    const strStagePath          = LLCommon.AjaxUrlPrefix + ( ( location.href.toLowerCase ( ).indexOf ( '/staging/') > INDEXOF_NOT_FOUND )
                                                             ? 'COMMON/STAGING/'
                                                             : 'COMMON/' );
    const strAbsolutePath       = strStagePath + 'Words2Actions_Form_TEMPLATE.HTML';

    //  -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //  'EntityDescription': "MyView_Template_Name=WA-Contact,MyView_Page_Name=WiseAgentPage,EntitySearchParameters={ \"ao_Query_Criteria\" : [ { \"Label\":\"Name\" ? \"Parameter\":\"nameQuery\" ? \"Type\" : \"text\" }? { \"Label\":\"Phone\" ? \"Parameter\":\"phone\" ? \"Type\" : \"text\" }? { \"Label\":\"Email\" ? \"Parameter\":\"email\" ? \"Type\" : \"email\" } ]?\"ao_Result_Columns\" : [ { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol1\" ? \"ColumnName\" : \"ClientID\" }? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol2\" ? \"ColumnName\" : \"CFirst\" }? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol2\" ? \"ColumnName\" : \"CLast\" }? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol3\" ? \"ColumnName\" : \"Title\" }? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol4\" ? \"ColumnName\" : \"Company\" }? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol5\" ? \"ColumnName\" : \"MobilePhone\" } ]?\"ao_Result_Column_Map\" : [ { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol1\" ? \"LabelRowSelector\" : \"search_review_grid_label STT_HideElement\" ? \"DetailRowSelector\" : \"search_review_grid_detail STT_HideElement\" ? \"ColumnLabel\" : \"ClientID\" } ? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol2\" ? \"LabelRowSelector\" : \"search_review_grid_label\" ? \"DetailRowSelector\" : \"search_review_grid_detail\" ? \"ColumnLabel\" : \"Name\" } ? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol3\" ? \"LabelRowSelector\" : \"search_review_grid_label\" ? \"DetailRowSelector\" : \"search_review_grid_detail\" ? \"ColumnLabel\" : \"Title\" } ? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol4\" ? \"LabelRowSelector\" : \"search_review_grid_label\" ? \"DetailRowSelector\" : \"search_review_grid_detail\" ? \"ColumnLabel\" : \"Company\" } ? { \"CellIdTemplate\" : \"SearchCRMResults_DetailRowNCol5\" ? \"LabelRowSelector\" : \"search_review_grid_label\" ? \"DetailRowSelector\" : \"search_review_grid_detail\" ? \"ColumnLabel\" : \"Phone\" } ] },DisplayOrder=100",
    //  -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    let   strMyViewPageName     = 'MicrophonePage';

    if ( LLCommon.EntityType !== null )
    {
        let dctEntityDescript   = new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription );

        if ( dctEntityDescript !== null )
        {
            strMyViewPageName   = dctEntityDescript.GetValueAtKey ( 'MyView_Page_Name' );
        }   // TRUE because the current SalesTalk domain has an associated CRM, block, if ( dctEntityDescript !== null )
    }   // if ( LLCommon.EntityType !== null )

    const strMyViewPage         = strMyViewPageName.length > EMPTY_STRING_LENGTH
                                  ? '&PageName=' + strMyViewPageName
                                  : EMPTY_STRING;
    const strCRMName            = ( ( LLCommon.EnabledCRM !== null ) && ( LLCommon.EnabledCRM.CrmName !== null ) && ( LLCommon.EnabledCRM.CrmName.toLowerCase ( ) !== 'nocrm' ) )
                                    ? '&CRM=' + LLCommon.EnabledCRM.CrmName
                                    : EMPTY_STRING;
    const strEntityTypeName     = ( ( LLCommon.EntityType !== null ) && ( LLCommon.EntityType.EntityName !== null ) )
                                  ? ( LLCommon.EntityType.EntityName.length > LLCommon.EnabledCRM.SysCRMLeadOrContact.length )
                                      ? '&EntityType=' + LLCommon.EntityType.EntityName.substring ( LLCommon.EnabledCRM.SysCRMLeadOrContact.length )
                                      : '&EntityType=' + LLCommon.EntityType.EntityName
                                  : EMPTY_STRING;
    const strMarkAsRequired     = ( pfCreateNewCRMRecord && pstrMarkAsRequired !== undefined && LLCommon.IsString ( pstrMarkAsRequired ) && pstrMarkAsRequired.length > EMPTY_STRING_LENGTH )
                                  ? '&MarkAsRequired=' + pstrMarkAsRequired
                                  : EMPTY_STRING;
    const strExternalCRMId = LLCommon.GetKendoGridRowFieldValueOrDefault ( event ,
                                                                           '#AgentRecentCallLogGrid' ,
                                                                           'ExternalCRMId' ,
                                                                           EMPTY_STRING );
    const strExternalCRMIdToken = ( LLCommon.IsString ( strExternalCRMId ) && strExternalCRMId.length > EMPTY_STRING_LENGTH )
                                  ? '&ExternalCRMId=' + strExternalCRMId
                                  : EMPTY_STRING;
    return strAbsolutePath + '?leadId=' + pintLeadId
                           + '&login='  + LLCommon.UserInfo.AgentLoginEmailId
                           + strMyViewPage
                           + strCRMName
                           + strEntityTypeName
                           + '&CI=true'
                           + '&CreateNewCRMRecord=' + pfCreateNewCRMRecord
                           + strMarkAsRequired
                           + strExternalCRMIdToken;
}   // function BuildViewButtonURL


function GetFieldValuesFromCurrentRow ( event )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  GetFieldValuesFromCurrentRow

        Function Goal:  Given a reference to the Button that raised the Click
                        event (through its JavaScript Event object), return a
                        new JavaScript object that has two properties, FirstName
                        and LastName, that contain the values in the like-nameed
                        columns (cells) of the row that contains the button that
                        raised the Click event.

        Inputs:         event   = JavaScript Event object generated by onClick
                                  event

        Output:         The return value is a JavaScript object that has three
                        string properties, FirstName LastName and ExternalCRMId,
                        any of which may be the empty string.
        ------------------------------------------------------------------------
    */

    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    const $button       = $ ( event.target );
    const $row          = $button.closest ( 'tr' );
    const grid          = $button.closest ( '.k-grid' ).data ( 'kendoGrid' );
    const dataRow       = grid.dataItem ( $row );

    return {
                Name                : dataRow.Name,
                ExternalCRMId       : dataRow.ExternalCRMId,
                ExternalPhoneNumber : dataRow.ExternalPhoneNumber
           };
}   // function GetFieldValuesFromCurrentRow


async function PerformActionsOnCallListItem ( event )
{
    /*
        ------------------------------------------------------------------------
        Function Goal:  Send instructions to the server to implement the actions
                        appropriate to the button that was clicked.

        Inputs:         event   = Through its ID, the eventTarget property of
                                  this standard JavaScript Event object informs
                                  us of the actions to perform and the relevant
                                  Lead ID and/or Behavior ID.

        Output:         This function returns void.

        Remarks:        The ID of the button is of the following form.

                              btnAction_RemoveThisCall_'
                            + data.BehaviorId
                            + _
                            + data.LeadId

                        In the above string, btnAction is an action verb, one of
                        the following:

                        1)  RemoveThisCall  Remove this call from future
                                            rendering of the Recent Calls grid.

                        2)  RemoveThisLead  Remove this call from future
                                            rendering of the Recent Calls grid
                                            AND mark the Lead as Do Not Call.

                        3)  ViewThisLead    If the first or last name is
                                            missing from the current row, try
                                            to resolve both by searching the
                                            CRM associated with the current
                                            SalesTalk domain, then display the
                                            Words2Actions form.

                        4)  LookupThisLead  Attempt to match a lead for which no
                                            CRM ID is on file with a record in
                                            the CRM, and record its CRM ID in
                                            the SalesTalk lead record.

                        The BehaviorId and the LeadId, along with the verb, are
                        passed to the server through a single method defined in
                        the OpenController, which returns the empty string if it
                        succeeds, and an error message if it fails.

                        Finally, the btnAction_ prefix is used to create a list
                        of buttons to which this routine is attached as a Click
                        event listener.

                        This event listener is marked async to tell the runtime
                        to expect code within it to await a Promise.
        ------------------------------------------------------------------------
    */

    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    //  ------------------------------------------------------------------------
    //  Placing the call to GetNameOfCurrentFunction for this function here is a
    //  signal that you are **truly** inside the function, notwithstanding that
    //  the very next statement declares a nested function.
    //
    //  Although calling GetNameOfCurrentFunction at this point puts its value,
    //  which goes into string variable strMethodName, within the closures of
    //  the nested functions, some of them get their own strMethodName variables
    //  because they do significant work, and deserve independent tracking. When
    //  control returns from any of them, the value of the outer strMethodName
    //  becomes visible again.
    //  ------------------------------------------------------------------------

    const CheckForExternalCRMID = function ( event , pfIsViewThisLead )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  CheckForExternalCRMID

            Function Goal:  When Boolean argument pfIsViewThisLead is TRUE, use
                            the provided event object to get the ExternalCRMId
                            from the Kendo UI grid row from which the specified
                            click event arose.

            Inputs:         event               = This is a reference to the
                                                  JavaScript Event object
                                                  generated by a focus, blur,
                                                  or click event, passed in
                                                  explicitly.

                            pfIsViewThisLead    = This is a Boolean flag that is
                                                  TRUE when the event source is
                                                  the Click event of the View
                                                  button of a Kendo UI Grid row.

            Output:         The return value is Boolean TRUE if ExternalCRMId of
                            the LeadId in the current row is the empty string.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        if ( pfIsViewThisLead )
        {
            const strExternalCRMId = LLCommon.GetKendoGridRowFieldValueOrDefault ( event ,
                                                                                   '#AgentRecentCallLogGrid' ,
                                                                                   'ExternalCRMId' ,
                                                                                   EMPTY_STRING );
            return ( strExternalCRMId.length === EMPTY_STRING_LENGTH );
        }   // TRUE (The calling function answered a `View this Lead` button.) block, if ( pfIsViewThisLead )
        else
        {
            return false;
        }   // FALSE (Since other buttons aren't intended for updates, only a `View this Lead` button will do.) block, if ( pfIsViewThisLead )
    }   // const CheckForExternalCRMID


    const ExtractExternalCRMId = function ( pstrMessage )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  ExtractExternalCRMId

            Function Goal:  When string `pstrMessage`starts with text that says
                            that the API call found a match and returned a valid
                            `xternalCRMId`, extract the `ExternalCRMId` and
                            return it. Otherwise, return null.

            Inputs:         pstrMessage         = Message returned by call to
                                                  PerformActionsOnCallListItem

            Output:         The return value is null unless pstrMessage starts
                            with text that indicates that a match was found for
                            the relevant LeadId in the CRM.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        console.log ( strMethodName + ': API response Message = ' + pstrMessage );

        const expectedPrefix = 'Lead Record linked to External CRM record. Lead ID =';

        if ( pstrMessage.startsWith ( expectedPrefix ) )
        {
            const match = pstrMessage.match(/ExternalCRMId = ([A-Za-z0-9]+),/);
            const externalCRMId = match ? match [ ARRAY_SECOND_ELEMENT ] : EMPTY_STRING;

            console.log ( strMethodName + ': Extracted ExternalCRMId:' + externalCRMId );
            return externalCRMId;
        }   // TRUE (optimistic outcome) block, if ( pstrMessage.startsWith ( expectedPrefix ) )
        else
        {
            console.log('Lead Record linked to External CRM record NOT FOUND.');
            return null;
        }   // FALSE (pessimistic outcome) block, if ( pstrMessage.startsWith ( expectedPrefix ) )
    }   // function ExtractExternalCRMId


    const IsExpectedElement = function ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  IsExpectedElement

            Function Goal:  Evaluate the properties of the currentTarget of the
                            event that was fed into the calling event listener,
                            to ensure that it meets our expectations.

            Inputs:         event   = JS Event object generated by focus, blur,
                                      or click event, visible via closure

            Output:         The return value is Boolean TRUE if the event's
                            currentTarget meets the requirements of the calling
                            event listener.
            --------------------------------------------------------------------
        */

        if ( event.currentTarget.nodeName === NODENAME_IS_BUTTON                                                       && event.currentTarget.id.startsWith ( ACTION_BUTTON_PREFIX ) )
        {
            return true;
        }
        if ( event.currentTarget.nodeName === NODENAME_IS_INPUT && event.currentTarget.type === NODE_TYPE_IS_CHECKBOX && event.currentTarget.id.startsWith ( ACTION_CHECKBOX_PREFIX ) )
        {
            return true;
        }
        return false;
    }   // const IsExpectedElement = function ( )


    const IsCheckBoxEventOfType = function ( pstrExpectedEventType )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  IsCheckBoxEventOfType

            Function Goal:  Evaluate the properties of the currentTarget of the
                            event that was fed into the calling event listener,
                            to determine whether to preserve the state of a
                            checkbox in a focus event.

            Inputs:         pstrExpectedEventType   = String representation of
                                                      the expected event type,
                                                      which is evaluated against
                                                      the event.type property

                            event                   = JavaScript Event object
                                                      generated by focus, blur,
                                                      or click event, visible
                                                      via closure

            Output:         The return value is Boolean TRUE if the event's
                            currentTarget indicates the focus event of a
                            checkbox.

            Remarks:        Since its value varies depending on the point from
                            which this multi-use function is called, argument
                            pstrExpectedEventType requires an explicit argument.
            --------------------------------------------------------------------
        */

        return (    event.type === pstrExpectedEventType
                 && event.currentTarget.type === NODE_TYPE_IS_CHECKBOX
                 && event.currentTarget.id.startsWith ( ACTION_CHECKBOX_PREFIX ) )
    }   // const IsCheckBoxEventOfType = function ( )


    const SendToServer = async function ( pstrAction )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  SendToServer

            Function Goal:  Evaluate the properties of the currentTarget of the
                            event that was fed into the calling event listener,
                            to determine whether to preserve the state of a
                            checkbox in a focus event.

            Inputs:         pstrAction  = String representation of the Action
                                          encoded into the ID of the button.

            Output:         The return value is Boolean TRUE if the event's
                            action code (pstrAction) is one of several on the
                            list of supported actions that requires confirmation
                            and it has one, or is another supported actions that
                            is allowed to go forward without confirmation.

            Remarks:        The following table enumerates the supported actions
                            and indicates which need confirmation.

                                +---------------------------+---------+
                                | Action                    | Confirm |
                                +---------------------------+---------+
                                | RemoveThisCall            | Yes     |
                                | RemoveThisLead            | Yes     |
                                | ViewThisLead              | No      |
                                | LookupThisLead            | No      |
                                | UpdateIsDoneFlagState     | No      |
                                | UpdateIsVIPFlagState      | No      |
                                | UpdateIsPriorityFlagState | No      |
                                +---------------------------+---------+

                            Actions that require confirmation return the result
                            of a call to LLCommon.ShowConfirmationDialog, which
                            takes the place of window.confirm because it gives
                            use full control over which button responds to a
                            default return button.

                            Unfortunately, window.confirm defaults to the OK
                            response, very dangerous.

                            Unlike the foregoing nested functions, this one gets
                            its own strMethodName because it awaits the result
                            of a function that must sometimes await resolution
                            of a Promise.
            --------------------------------------------------------------------
        */

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        switch ( pstrAction )
        {
            case 'RemoveThisCall':
                return await LLCommon.ConfirmDefaultingToNo ( 'This call will be permanently removed from your list of recent calls. However, future calls to or from this number will be recorded, transcribed, and displayed.' );
            case 'RemoveThisLead':
                return await LLCommon.ConfirmDefaultingToNo ( 'This call, and all calls from this number will be permanently removed from your list of recent calls, and they will not be recorded or transcribed.' );
            case 'ViewThisLead':
            case 'LookupThisLead':
            case 'UpdateIsDoneFlagState':
            case 'UpdateIsVIPFlagState':
            case 'UpdateIsPriorityFlagState':
                return true;
            default:
                alert ( 'The specified action, "' + pstrAction + '", is undefined, and will be prevented.' , 'native' );
                return false;
        }   // switch ( pstrAction )
    }   // const SendToServer = function ( pstrAction )

    //  +------------------------------------------------------------------+  //
    //  |            Begin PerformActionsOnCallListItem Body               |  //
    //  +------------------------------------------------------------------+  //

    var   strErrorMessage               = null;
    var   strAction                     = null;
    var   fCallServer                   = false;
    var   oFields                       = null;

    debugger;

    try
    {
        if ( IsExpectedElement ( ) )
        {
            const astrIdSegs            = event.currentTarget.id.split ( UNDERSCORE_CHAR );

            if ( astrIdSegs.length === BUTTON_ID_EXPECTED_PARTS )
            {
                strAction               = astrIdSegs [ BUTTON_ACTION_VERB ];
                const intBehId          = astrIdSegs [ BUTTON_ACTION_BEHAVIONRID ].length > EMPTY_STRING_LENGTH
                                          ? parseInt ( astrIdSegs [ BUTTON_ACTION_BEHAVIONRID ] )
                                          : NUMERIC_ZERO;
                const intLeadId         = astrIdSegs [ BUTTON_ACTION_LEADID ].length > EMPTY_STRING_LENGTH
                                          ? parseInt ( astrIdSegs [ BUTTON_ACTION_LEADID ] )
                                          : NUMERIC_ZERO;

                if ( strAction.length == EMPTY_STRING_LENGTH )
                {
                    throw new Error ( 'ERROR in ' + strMethodName + ': The action value embedded in the button ID is the empty string. Button ID = ' + event.currentTarget.id );
                }   // if ( strAction.length == EMPTY_STRING_LENGTH )

                if ( intBehId < MINIMUM_STT_ENTITY_ID )
                {
                    throw new Error ( 'ERROR in ' + strMethodName + ': The BehaviorId value embedded in the button ID is out of range. Minimum Value = ' + MINIMUM_STT_ENTITY_ID + ', Actual Value = ' + intBehId + ', Button ID = ' + event.currentTarget.id );
                }   // if ( intBehId < MINIMUM_STT_ENTITY_ID )

                if ( intLeadId < MINIMUM_STT_ENTITY_ID )
                {
                    throw new Error ( 'ERROR in ' + strMethodName + ': The LeadId value embedded in the button ID is out of range. Minimum Value = ' + MINIMUM_STT_ENTITY_ID + ', Actual Value = ' + intLeadId + ', Button ID = ' + event.currentTarget.id );
                }   // if ( intLeadId < MINIMUM_STT_ENTITY_ID )

                if ( NeedSomethingFromServer ( ) )
                {
                    oFields             = GetFieldValuesFromCurrentRow ( event );

                    if ( oFields !== null )
                    {
                        if ( ( oFields.ExternalCRMId.length === EMPTY_STRING_LENGTH ) && ( LLCommon.EnabledCRM !== null || LLCommon.EnabledCRM.CrmName !== 'NoCRM' ) )
                        {
                            fCallServer = true;
                        }   // if ( ( oFields.ExternalCRMId.length === EMPTY_STRING_LENGTH ) && ( LLCommon.EnabledCRM !== null || LLCommon.EnabledCRM.CrmName !== 'NoCRM' ) )
                    }   // TRUE (anticipated outcome) block, if ( oFields !== null )
                    else
                    {
                        throw new Error ( 'ERROR in ' + strMethodName + ': Function GetFieldValuesFromCurrentRow returned a null reference. Button ID = ' + event.currentTarget.id );
                    }   // FALSE (unanticipated outcome) block, if ( oFields !== null )

                    let strResult = EMPTY_STRING;

                    if ( IsCheckBoxEventOfType ( 'change' ) )
                    {
                        const strCSSClassName = LLCommon.strictLookup ( aoRowColorRule ,      // paoLikeJSObjects   (array of Object)
                                                                        'UniqueCheckBoxId' ,  // pstrMatchPropName  (string)
                                                                        strAction ,           // poMatchValue       (Object)
                                                                        'CSSClassName' );     // pstrReturnPropName (string)
                        if ( LLCommon.IsString ( strCSSClassName ) )
                        {
                            ARPCHRemoveRedundantHiglight ( event.currentTarget ,
                                                           strCSSClassName );
                            LLCommon.AddOrRemoveStyles ( event.currentTarget.closest ( 'tr' ) ,
                                                         strCSSClassName ,
                                                         event.currentTarget.checked );
                        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( strCSSClassName ) )
                        else
                        {
                            strErrorMessage = 'ERROR: No CSS selector matching Task ID "' + strAction + '" was found in the aoRowColorRule table. Row highlighting suppressed.';
                            LLCommon.LogException ( strErrorMessage );
                            console.error ( strErrorMessage );
                        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( strCSSClassName ) )
                    }   // if ( IsCheckBoxEventOfType ( 'change' ) )

                    if ( await SendToServer ( strAction ) )
                    {
                        //  ----------------------------------------------------
                        //  Maintenance Note: The value of the last argument,
                        //  pintQueryVersion, is incremented whenever the shape
                        //  of the SQL query result set changes due to addition,
                        //  removal, or rearrangement of table columns.
                        //  ----------------------------------------------------

                        strResult = LLCommon.DoAjax ( 'PerformActionsOnCallListItem' ,
                                                      'GET' ,
                                                      {
                                                          'pstrTaskName'            : strAction,                                                                            // string pstrTaskName
                                                          'pintBehaviorId'          : intBehId,                                                                             // int    pintBehaviorId
                                                          'pintLeadId'              : intLeadId,                                                                            // int    pintLeadId
                                                          'pstrSysCRMLeadOrContact' : LLCommon.EnabledCRM.SysCRMLeadOrContact ?? EMPTY_STRING,                              // string pstrSysCRMLeadOrContact = SpecialStrings.EMPTY_STRING
                                                          'CRMEntityTypeId'         : LLCommon.EntityType !== null ? LLCommon.EntityType.CRMEntityTypeId : NUMERIC_ZERO,    // int    CRMEntityTypeId         = MagicNumbers.ZERO
                                                          'pintDomainId'            : LLCommon.DomainId,                                                                    // int    pintDomainId            = MagicNumbers.ZERO
                                                          'pintTenantId'            : LLCommon.TenantId,                                                                    // int    pintTenantId            = MagicNumbers.ZERO
                                                          'pintUserId'              : LLCommon.UserId,                                                                      // int    pintUserId              = MagicNumbers.ZERO
                                                          'pfCheckBoxState'         : IsCheckBoxEventOfType ( 'change' )                                                      // bool   pfCheckBoxState         = false
                                                                                      ? event.currentTarget.checked
                                                                                      : false,
                                                          'pintQueryVersion'        : 3                                                                                     // int    pintQueryVersion        = ACTIVITY_HUB_QUERY_VERSION_2
                                                      } );
                        LLCommon.LogImportantValue ( strMethodName + ': strResult = ' ,
                                                     strResult );

                        if ( ( strAction === 'ViewThisLead' ) && ( !strResult.startsWith ( 'Error' ) ) )
                        {
                            if ( strResult.startsWith ( 'Search of CRM for Phone Number returned the empty set.' ) )
                            {   // Since the following statement displays a native (synchronous) dialog box, the window.open that follows it is safe. If this dialog becomes a BootBox, the window.open must goe into a Bootbox event sink.
                                alert ( 'We found NO MATCH for phone number ' + oFields.ExternalPhoneNumber + ' in ' + LLCommon.EnabledCRM.CrmName + '. Please enter the fields marked in red in the screen that is about to display, then either click the "Update CRM" button, or just exit the form when done. If you got here by mistake, do nothing and exit the form, and this form will redisplay.' , 'native' );
                                window.open ( BuildViewButtonURL ( intLeadId , true , 'FirstName,LastName' ) , '_self' );
                            }   // TRUE (There are no matching records in the CRM.) block, if ( strResult.startsWith ( 'Search of CRM for Phone Number returned the empty set.' ) )
                            else
                            {
                                if ( strResult.startsWith ( 'Lead Record linked to External CRM record. Lead ID =' ) )
                                {   // When strResult starts with 'Lead Record linked to External CRM record. Lead ID =', expect ExtractExternalCRMId to return a valid ExternalCRMId.
                                    const strExternalCRMIdMatched = ExtractExternalCRMId ( strResult )
                                    window.open ( BuildViewButtonURL ( intLeadId ,
                                                                       strAction === 'ViewThisLead' && strExternalCRMIdMatched.length === EMPTY_STRING_LENGTH ,
                                                                       strExternalCRMIdMatched ) ,
                                                  '_self' );
                                }   // TRUE (desired outcome) block, if ( strResult.startsWith ( 'Lead Record linked to External CRM record. Lead ID =' ) )
                                else
                                {   // Otherwise, since CheckForExternalCRMID would always return false, we skip the overhead and just pass in a Boolean False.
                                    window.open ( BuildViewButtonURL ( intLeadId ,
                                                                       false ,
                                                                       'FirstName,LastName' ) ,
                                                  '_self' );
                                }   // FALSE (undesired outcome) block, if ( strResult.startsWith ( 'Lead Record linked to External CRM record. Lead ID =' ) )
                            }   // FALSE (We found a match in the CRM and linked it.) block, if ( strResult.startsWith ( 'Search of CRM for Phone Number returned the empty set.' ) )
                        }   // TRUE (Unless the server returns with an error message, the last step of the task opens the Words2Actions page, replacing the grid with it.) block, if ( ( strAction === 'ViewThisLead' ) && ( !strResult.startsWith ( 'Error' ) ) )
                        else
                        {
                            if ( strResult.length > EMPTY_STRING_LENGTH )
                            {   // Since the new update task won't return anything, suppress the message unless it has something in it.
                                alert ( strResult , 'native' );
                            }   // if ( strResult.length > EMPTY_STRING_LENGTH )
                        }   // FALSE (All other tasks finish with a confirmation dialog that displays the message returned by the server.) block, if ( ( strAction === 'ViewThisLead' ) && ( !strResult.startsWith ( 'Error' ) ) )
                    }   // if ( await SendToServer ( strAction ) )
                }   // TRUE (We need something from the server.) block, if ( ( ( strAction === 'ViewThisLead' || strAction === 'LookupThisLead' ) && fCallServer ) || ( strAction !== 'ViewThisLead' && strAction !== 'LookupThisLead' ) )
                else
                {   // Since we didn't call the server, we are on our own.
                    if ( strAction === 'LookupThisLead' || strAction === 'ViewThisLead' )
                    {
                        window.open ( BuildViewButtonURL ( intLeadId , CheckForExternalCRMID ( event , strAction === 'ViewThisLead' ) , 'FirstName,LastName' ) , '_self' );
                    }   // if ( strAction === 'LookupThisLead' || strAction === 'ViewThisLead' )
                }   // FALSE (Though the action MAY be ViewThisLead, the search is already done.) block, if ( ( ( strAction === 'ViewThisLead' || strAction === 'LookupThisLead' ) && fCallServer ) || ( strAction !== 'ViewThisLead' && strAction !== 'LookupThisLead' ) )
            }   // TRUE (anticipated outcome) block, if ( astrIdSegs.length === BUTTON_ID_EXPECTED_PARTS )
            else
            {
                strErrorMessage = 'ERROR: The format of the ID of the button to which this event is attached is invlid. It is expected to be divisible into ' + BUTTON_ID_EXPECTED_PARTS + ' on underscore (' + UNDERSCORE_CHAR + ') boundaries. Actual parts count = ' + astrIdSegs + ', ID presented = ' + event.currentTarget.id + ' No action will be taken.';
                LLCommon.LogException ( strErrorMessage );
                console.error ( strErrorMessage );
            }   // FALSE (unanticipated outcome) block, if ( astrIdSegs.length === BUTTON_ID_EXPECTED_PARTS )
        }   // TRUE (The routine either needs something from the server or it needs to send something to it.) block, if ( NeedSomethingFromServer ( ) )
        else
        {
            if ( IsCheckBoxEventOfType ( 'focus' ) )
            {   // Preserve current value for comparison against the value as it stands when the corresponding blur event arises.
                objInitialValue = event.currentTarget.checked;
            }   // if ( IsCheckBoxEventOfType ( 'focus' ) )

            //  ----------------------------------------------------------------
            //  If the code in this method is working correctly, this should
            //  NEVER happen.
            //  ----------------------------------------------------------------

            if ( event.currentTarget.nodeName === NODENAME_IS_BUTTON )
            {
                strErrorMessage = 'ERROR: The ID of the button to which this event is attached must start with "' + ACTION_BUTTON_PREFIX + '" but the ID presented is "' + event.currentTarget.id + '". No action will be taken.';
                LLCommon.LogException ( strErrorMessage );
                console.error ( strErrorMessage );
            }   // True (The event listener is attached to a button that has an unexpected ID.) block, if ( event.currentTarget.nodeName === NODENAME_IS_BUTTON )
            else
            {
                strErrorMessage = 'ERROR: The nodeName of the element to which this event is attached must be "' + NODENAME_IS_BUTTON + '" but the nodeName of the element presented is "' + event.currentTarget.nodeName + '", and its ID is "' + event.currentTarget.id + '". No action will be taken.';
                LLCommon.LogException ( strErrorMessage );
                console.error ( strErrorMessage );
            }   // FALSE (The event listener got attached to an element that is something besides a button.) block, if ( event.currentTarget.nodeName === NODENAME_IS_BUTTON )
        }   // FALSE (This routine can finish independently.) block, if ( NeedSomethingFromServer ( ) )
    }
    catch ( ex )
    {   // Though Exceptions are unexpected in this context, we must always BE PREPARED to handle them.
        LLCommon.LogException ( ex );
        alert ( 'An internal error occurred. If this persists, please contact customer support.' );
    }


    function NeedSomethingFromServer ( )
    {
        /*
            --------------------------------------------------------------------
            Function Name:  NeedSomethingFromServer

            Function Goal:  Evaluate the properties of the currentTarget of the
                            event that was fed into the calling event listener
                            to determine whether we need something from the
                            server, or have something for it.

            Inputs:         event       = JavaScript Event object generated by a
                                          focus, blur, or click event

                            strAction   = Substring parsed from `id` property of
                                          the `currentTarget` property of the
                                          incoming `event` object

                            fCallServer = Boolean flag set by the caller

            Output:         The return value is Boolean TRUE if the event's
                            attributes indicate that we need something from the
                            server. Otherwise, the return value is FALSE.

            Remarks:        1)  Since everything we need to see is visible in
                                the closure, there is no formal argument list.

                            2)  To make everything this function needs to see
                                part of the closure, this function must be
                                defined last.
            --------------------------------------------------------------------
        */

        if ( event.type === 'focus' )
        {
            return false;
        }   // if ( event.type === 'focus' )

        if ( ( strAction === 'UpdateIsDoneFlagState' || strAction === 'UpdateIsVIPFlagState' || strAction === 'UpdateIsPriorityFlagState' ) && event.type === 'blur' && objInitialValue !== event.currentTarget.checked )
        {
            return true;
        }   // if ( ( strAction === 'UpdateIsDoneFlagState' || strAction === 'UpdateIsVIPFlagState' || strAction === 'UpdateIsPriorityFlagState' ) && event.type === 'blur' && objInitialValue !== event.currentTarget.checked )

        if ( ( ( strAction === 'ViewThisLead' || strAction === 'LookupThisLead' ) && CheckForExternalCRMID ( event , strAction === 'ViewThisLead' ) ) || ( strAction !== 'ViewThisLead' && strAction !== 'LookupThisLead' ) )
        {
            return true;
        }   // if ( ( ( strAction === 'ViewThisLead' || strAction === 'LookupThisLead' ) && CheckForExternalCRMID ( event , strAction === 'ViewThisLead' ) ) || ( strAction !== 'ViewThisLead' && strAction !== 'LookupThisLead' ) )

        return false;
    }   // function NeedSomethingFromServer
}   // function PerformActionsOnCallListItem ( event )

console.log ( ScriptInfoForLog ( Agent_Recent_Phone_Calls_SCRIPTSOURCE ,
                                 Agent_Recent_Phone_Calls_VERSION ,
                                 Agent_Recent_Phone_Calls_LastUpdated ,
                                 'loaded' ) );

//  +--------------------------------------------------------------------------+
//  |                      End of Agent_Recent_Phone_Calls                     |
//  +--------------------------------------------------------------------------+
