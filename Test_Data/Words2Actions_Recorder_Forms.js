/*eslint-env browser*/
/*global $ _domainid _leadid _login _leadidSource _LeadLifeJSHelpers _pagename _pagenameSource _tenantid _userid LLCommon.AddOrRemoveStyles ARRAY_FIRST_ELEMENT ARRAY_INVALID_INDEX ARRAY_IS_EMPTY ARRAY_NOT_EMPTY ARRAY_SECOND_ELEMENT ARRAY_THIRD_ELEMENT bootbox EMPTY_STRING EMPTY_STRING_LENGTH EQUALS_CHAR GetNameOfCurrentFunction GetParameterFromURLFormOrLocalStorage HTML_NBSP INDEXOF_NOT_FOUND JQUERY_SELECTOR_IS_ELEMENT_ID LLCommon LOGICAL_NEGATE NO_LEAD_ID NUMERIC_ZERO OrdinalFromIndex PIPE_CHAR_SPLIT_MATCH PopulateSearchGrid QUOTE_SINGLE ScriptInfoForLog SPACE_CHARACTER SPLIT_NAME_FROM_VALUE STTProcessMedia SUBSTRING_FIRST_CHAR UNDERSCORE_CHAR UpdateIfChanged*/
"use strict";

const Words2Actions_Recorder_Forms_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const Words2Actions_Recorder_Forms_VERSION      = 1.103;
const Words2Actions_Recorder_Forms_LogTraces    = false;
const Words2Actions_Recorder_Forms_LastUpdated  = '2026/03/30 22:05:21 CDT'

/*
    ============================================================================
    Name:               Words2Actions_Recorder_Forms.js

    Goal:               Define custom JavaScript functions used by the
                        Words2Actions self-contained recording forms.

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

    References:         1)  Add a CSS class to an HTML element with JavaScript/jQuery
                            https://www.techiedelight.com/add-css-class-to-html-element-javascript

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By Remark/Brief Description
    ---------- ------- -- ------------------------------------------------------
    2023/06/21 1.001   DG MVP: New code copied from a working HTML page

    2023/06/21 1.002   DG MVP: Report separately that there are no transcripts.

    2023/06/28 1.003   DG 1) Attach an event listener to the m4vurl SELECT
                             element that causes it to reinitialize the
                             transcript viewer.

                          2) Consolidate the two DOMContentLoaded event
                             listeners into a single function.

    2023/07/10 1.004   DG Adjust the file name in the header to be consistent
                          with the labeling of other development scripts, and
                          fix other cosmetic inconsistencies in the internal
                          documentation.

                          Correct the IF statement that evaluates the value that
                          is returned by document.getElementById to conform to
                          the fact that it returns null, rather than 'undefined'
                          to signify that the queried element cannot be found.

                          Since the code is otherwise unchanged, that code needs
                          no testing, although it is about to be put through its
                          paces in a new context.

    2023/07/10 1.005   DG GetTranscriptList must return the empty string, not
                          zero, to conform to the expectations of the switch
                          expression that represents the one and only call.

    2023/07/13 1.006   DG Adjust functions StartRecording and the arrow function
                          that attaches itself to the mediaRecorder.onstop event
                          so that both leverage the ShowOrHideElement function,
                          along with the STT_HideElement and STT_ShowElement
                          selectors defined in leadlife.custom_form.css to show
                          and hide buttons. Though it does the same things as
                          the code it replaces, this implementation is much more
                          standardized.

    2023/07/15 1.007   DG 1) Assert stronger control over object placement by
                             injecting controls into a generated table.

                          2) Replace generic Boolean true with ELEMENT_SHOW and
                             generic Boolean False with ELEMENT_HIDE for the
                             second argument to all calls to global function
                             ShowOrHideElement, and ensure that all remaining
                             showing and hiding of elements is under its
                             control.

    2023/07/15 1.008   DG Add title attributes to generated controls, if for no
                          other reason than that displaying the tooltip provides
                          positive feedback that your mouse is over the hot zone
                          that will make the button do its thing.

    2023/07/17 1.009   DG Implement a keyup event listener on INPUT SearchParam
                          that fires the PopulateSearchGrid event routine, which
                          is defined in the Mobile_Index script.

    2023/07/18 1.010   DG I didn't follow my own notes (in an inline comment),
                          and allowed docMediaRecorder to be destroyed. Since it
                          points to a real element that is part of the static
                          page, removing it destroys not only the JavaScript
                          object reference, but removes it from the DOM.

    2023/07/19 1.011   DG Incorporate the code to support the cheat sheet.

    2023/07/24 1.012   DG 1) Hide the transcript viewer when the recorder is
                             activated.

                          2) Fix an errant exception report that was copied in
                             from LeadLifeJSHelpersLib.js without correcting the
                             object reference from this to _LeadLifeJSHelpers.

                          3) Change GetTranscriptList so that it returns the
                             empty array, averting the unwarranted report of an
                             internal error when a user navigates to a
                             Words2Actions page manually.

    2023/07/28 1.013   DG I'm skipping this number, in part to stay in sync with
                          Mobile_Index.js, to which this script is closely
                          related.

    2023/07/28 1.014   DG Move SearchParam from this script to Mobile_Index.js,
                          where it belongs.

    2023/08/13 1.015   DG Account for renaming of dtmUtcOffset in the
                          LeadLifeJSHelpers object to UtcOffsetMinutes. Note
                          that this is a breaking change, meaning that both libs
                          must be upgraded together.

    2023/08/16 1.016   DG 1) Amend direct Ajax calls (those that call directly
                             into the jQuery library, rather than use DoAjax) so
                             that they use LLCommon.AjaxUrlPrefix, which is
                             ALWAYS valid.

                          2) Amend function CheatSheet so that it expects a
                             delimited string, rather than the JSON string that
                             was originally expected.

    2023/08/17 1.017   DG Align the 3 recording disposition buttons vertically
                          on template forms that display lots of data in a form.

    2023/08/18 1.018   DG See to it that the recorder buttons or the transcript
                          controls are visible, but not both simultaneously. The
                          changes are accomplised inside function ToggleDivs, by
                          showing and hiding DIV elements.

    2023/08/23 1.019   DG Change function GetTranscriptList so that it makes the
                          pick list visible when there is at least one item in
                          the transcript list, and make the m4vurl change event
                          listener cause the transcript to become visible as if
                          the btnTranscriptReview button had been pressed.

    2023/08/27 1.020   DG Implement the AddANote action code in ToggleDivs.

    2023/08/29 1.021   DG Hide the My View side of the page when the pagename
                          parameter is omitted.

    2023/08/31 1.022   DG 2) Implement dynaic dropdown generation in function
                             DropDown.

                          3) Account for consolidating DoAjax and LogException
                             into LLCommon.js.

    2023/09/01 1.023   DG 1) Delay displaying the My View form until it is fully
                             populated.

                          2) When DropDown calls GetPickListValues, set optional
                             argument IgnoreDisplayOrder to Boolen True so that
                             the DisplayOrder is ignored in the sort.

                          3) In function ResetThisForm, replace
                             "poEvent.currentTarget.form.reset ( )" with
                             "location.reload ( )".

    2023/09/05 1.024   DG 1) Register an event listener for Change events on the
                             m4vNoteId SELECT element that is almost identical
                             to the routine that is registered for m4vurl.

                          2) Implement automatic update of changed fields.

    2023/09/06 1.025   DG 1) Fix bug that raised an exception when a lead had no
                             Notes.

                          2) Fix bug that raised an exception when an empty text
                             box lost focus.

    2023/09/09 1.026   DG Fix a bug that prevented displaying Transcripts and
                          Notes on the Mobile_Index page.

    2023/09/16 1.027   DG Replace constants defined on the _LeadLifeJSHelpers
                          object with global constants defined in LLCommon.js.

    2023/09/19 1.028   DG 1) Implement function DoGetRecordFromCRM, a Click
                             event listener that receives as its argument the ID
                             of the INPUT element that contains the external CRM
                             ID to look up.

                          2) Implement a set of "dirty" flags, as follows.

                             a) The _fFormIsDirty flag is TRUE when any field is
                                manually updated.

                             b) The _fUnPostedRecording flag is TRUE when a W2A
                                recording has been created but remains unposted.

    2023/09/29 1.029   DG Implement code to hide the CRM buttons and associated
                          elements unless a CRM is configured.

    2023/10/02 1.030   DG 1) Adjust for renaming of CSS selector Talk2UrCrm to
                             W2A_Talk2UrCrm.

                          2) Restore directive to make element docCRMContextTools
                             visible when a CRM is enabled.

    2023/10/06 1.031   DG Implement a signle event listener to handle change and
                          blur events on both transcript pick lists.

    2023/10/06 1.032   DG Implement function ShowConversationInsights to display
                          the Story-So-Far.

    2023/10/08 1.033   DG Correct off-by-one errors in functions GetNotesList
                          and GetTranscriptList that hid the transcript button
                          and its list unless thare are two or more transcripts.
                          Also, function ToggleDivs needed to make new element
                          docReviewingTools visible.

    2023/10/12 1.034   DG Simplify the button labeling performed by private
                          function ActionButtonFixup and add a placeholder
                          DialNumber function.

    2023/10/14 1.035   DG Change the value of BTN_UPDATE_CRM from `post2` to
                          `UpdateCRMNow` to prevent the built-in posting routine
                          from executing when it is activated.

    2023/10/16 1.036   DG Correct the dropdown list generator so that it ignores
                          invalid values encountered in the input field.

    2023/10/16 1.037   DG Add an onClick event declaration to generated button
                          UploadRecordedMedia2Server that calls ResetThisForm.

    2023/10/19 1.038   DG Show button UpdateCRMNow only when the lead record has
                          a ExteernalCRMID value. This happens when function
                          AdjustButtonProperties is called with its pintAction
                          argument set to BUTTON_STATE_INITIAL AND ExternalCRMId
                          has a length greater than zero.

    2023/10/25 1.039   DG Implement field validation through new instance method
                          _LeadLifeJSHelpers.ValidateFormFields. To disambiguate
                          its name, the local function is ValidateAllFormFields.

    2023/11/03 1.040   DG Implement the Recent Activity Search button, function
                          ShowRecentActivity.

    2023/11/13 1.041   DG Implement the Show Other W2A Forms button, functions
                          ShowOtherW2AForms and DisplayNewForm.

    2023/11/22 1.042   DG Implement playback and download buttons for the source
                          recording of each transcript.

    2023/11/22 1.043   DG Remove CSS selector W2A_Talk2UrCrm from TABLE element
                          docWords2ActionForm.

    2023/12/03 1.044   DG Explictly set the type property of all BUTTON elements
                          to 'button' to prevent them causing an unexpected
                          submit event.

    2023/12/08 1.045   DG Arrest event bubbling on most events.

    2023/12/11 1.046   DG Pass the Option flag to UploadMedia, to prevent auto
                          summarization via OpenAI.

    2023/12/30 1.047   DG Implement Zoom calls.

    2023/12/31 1.048   DG Hide the Call button in the absence of a Lead ID.

    2024/01/09 1.049   DG Make all CRM activities generic by taking advantage of
                          the new GetEnabledCrmInfo method on OpenController.

    2024/01/24 1.050   DG Reinstate the Bullhorn ID input button.

    2024/02/21 1.051   DG Implement three tokens in the TitleContainer element,
                          as follows:

                          1) ##PageTitle##  Page Title, per document.Title attribute
                          2) ##firstName##  Value of firstName element
                          3) ##lastName##   Value of lastName element

                          The second and third attributes are implemented as
                          generic tokens that are replaced if a like-named
                          element exists, and removed otherwise.

    2024/04/09 1.052   DG Replace the code in local function DialNumber with a
                          call to LLCommon.ManageCallButton.

    2024/04/21 1.053   DG Make provisions to display the ExternalCRMId and use a
                          visibilitychange event listener to force a final CRM
                          update.

    2024/04/23 1.054   DG Explicitly call preventDefault and return false from
                          event listener function ResetThisForm.

    2024/04/26 1.055   DG Hide the page until a valid SalesTalk login is
                          associated with the Bullhorn user ID specified in the
                          URL.

    2024/04/29 1.056   DG Suppress CRM processing when _W2AButton is TRUE.

    2024/04/30 1.057   DG Validate pick lists as part of CRM updating.

    2024/05/08 1.058   DG Improve error message for CRM update when forced
                          update is unnecessary.

    2024/05/09 1.059   DG Make comparison of pick lists case sensitive for
                          HubSpot.

    2024/05/11 1.060   DG Replace virtually all calls to console.log with calls
                          to LLCommon.Trace, which can be centrally configured
                          to suppress logging.

    2024/05/20 1.061   DG 1) Attach a click event to the ShowIDsHotSpot DIV that
                             toggles the visibility of the IDBox table row.

                          2) Hide the "Update CRM Mow" button when either the
                             ExternalCRMId or the SysCRMLeadOrContact is absent.

    2024/05/28 1.062   DG Change function EvaluatePickListValues to suppress the
                          updating of the Last Modified date of the controlling
                          Lead table row, preventing a premature attempt to
                          update the CRM.

    2024/06/09 1.063   DG Reinstate the CRM interaction button for use with Wise
                          Agent.

    2024/06/10 1.064   DG Hide the W2AActions element, a Table Row, unless the
                          Lead ID is valid.

    2024/06/10 1.065   DG Implement integration with the WiseAgent CRM.

    2024/06/13 1.066   DG Protect the code that registers an event listener for
                          element ShowIDsHotSpot so that it runs only when said
                          element exists.

    2024/07/01 1.067   DG Correct a bug in the login ID validation routine, back
                          off the pick list validation routine frequency from 5
                          seconds to 1 minute, and enable a PickListValidatorOff
                          True/False URL paramter to disable it totally.

    2024/07/01 1.068   DG Implement support for associating the PageName with an
                          External CRM Entity.

    2024/07/19 1.069   DG Substitute the native string.Prototype.startsWith
                          function for my polyfill, LLCommon.StringStartsWith,
                          and substitute LLCommon.ShowOrHideElement for the like
                          named function defined in LeadLifeJSHelpersGlobals.js.

    2024/09/26 1.070   DG Implement CRM record search and throttle CRM update.

    2024/09/26 1.071   DG Amend ValidateAllFormFields to implement email address
                          validation via _objAPI call to IsEmailAddressValid.

    2024/10/05 1.072   DG Trade the onClick declarative attribute for
                          addEventListener on button SearchCRM.

    2024/10/10 1.073   DG Add Click events to the transcript and note combo box
                          elements to enable selection of the first item.

    2024/10/14 1.074   DG Implement property search criteria searches against a
                          Wise Agent Contact record.

    2024/10/20 1.075   DG Replace back-to-back hard coded element ID strings for
                          the SearchCRM button element with a reference to an
                          element handle.

    2024/10/22 1.076   DG Mark the form as dirty when UploadMediaOption is equal
                          to ACTION_TALK2CRM (Talk2CRM) to force the CRM to be
                          updated when the Refresh button is activated.

    2024/11/03 1.077   DG Restrict form update when the form is dirty.

    2024/11/21 1.078   DG Make ShowRecentActivity slightly more performant by
                          substituting a locally stored value for a wasted trip
                          to the server.

    2024/11/26 1.079   DG Amend ShowRecentActivity to enable click handlers on
                          each of its rows.

    2025/02/08 1.080   DG Differentiate CRM update notices by CRM.

    2025/02/18 1.081   DG Make the CI icon always vislble, making its action
                          dependent upon whether the URL specifies that the user
                          is subscribed to that feature. Apart from making the
                          icon always visible, the changes are confined to
                          function ShowConversationInsights.

    2025/02/19 1.082   DG Make the cheat sheet sticky for each login ID.

    2025/02/20 1.083   DG Turn the Update CRM button background green when the
                          current record is updated, and refresh the transcript
                          list after a new one is created.

    2025/03/18 1.084   DG Implement Notes Search.

    2025/04/02 1.085   DG Adjust the date stamp to force recomputation of the
                          Subresource Integrity digest string.

    2025/05/14 1.086   DG Implement Task input via Words2Actions.

    2025/06/12 1.087   DG Hide the CIButtonHole element when the CRM is Wise
                          Agent.

    2025/07/14 1.088   DG Implement the ShowMyRecentCalls button event listener.

    2025/08/16 1.089   DG Implement validation of inputs on form load when a
                          contact that was not found in the CRM. This changes
                          the conditions under which the `UpdateCRMNow` button
                          is made visible.

    2025/08/31 1.090   DG Implement a conditional query for the user's identity.

    2025/11/01 1.091   DG In ProcessSelection, call LLCommon.ReSyncUserInfo ( ).

    2025/11/05 1.092   DG Guard the ternary expression in CreateTeamPickList
                          against the object whose properties it evaluates being
                          undefined and simplify it to a straight Boolean.

    2025/11/09 1.093   DG Make the value of the `disabled` flag on SELECT
                          element `WATeamSelector` dependent on its
                          `selectedIndex` being greater than zero (a placeholder
                          selection), and hide the controls completely for a
                          Wise Agent team of 1.

    2025/11/25 1.094   DG Use an opaque DIV with a high z-index to hide the rest
                          of the form until a value has been selected from the
                          Wise Agent Team Membership combo box.

    2025/11/26 1.095   DG In function CreateResetButton, change the button face
                          text on button `ResetWATeamMembership` from `reset` to
                          `Change User`.

    2025/12/03 1.096   DG Change all instances of "Conversation Intelligence" to
                          "Conversation Insights" in the UI.

    2025/12/20 1.097   DG Implement the Click2Note feature.

    2025/12/29 1.098   DG Implement the rationalized color scheme and disable
                          the CRM update button unless the form is dirty.

    2025/12/31 1.099   DG Eliminate the useless statement that tries to set the
                          disabled flag on the table cell that contains the user
                          ID pick list, which is alreeady so marked. The real
                          fix is in leadlife.custom_form.css, the base cascading
                          style sheet, which now has a global `:disabled` pseudo
                          class selector that is set to trump everything else.

    2026/01/06 1.100   DG Make the state of the CRM update button track with the
                          state of the global dirty form flag.

    2026/01/12 1.101   DG Make the warning displayed when the CI button is
                          activated optional based on the value of a domain
                          scoped Boolean flag with a user level override.

    2026/01/25 1.102   DG Render the Click2Note keywords in order of appearance
                          in the playbook, rather than alphabetically, and make
                          visibility of the instruction boxes user-settable, as
                          is already the case for the Cheat Sheet.

    2026/01/30 1.103   DG Toggle the instruction box the same way that we do the
                          Cheat Sheet.
    ============================================================================
*/

console.log ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                 Words2Actions_Recorder_Forms_VERSION ,
                                 Words2Actions_Recorder_Forms_LastUpdated ,
                                 'loading' ) );


const _DEFAULT_PAGE_TITLE       = 'Change CRM Field Values Verbally';

const ACTION_DICTATE_NOTE       = 'DictateNote';
const ACTION_TALK2CRM           = 'Talk2CRM';
const ACTION_TRANS_REVIEW       = 'TranscriptReview';
const ACTION_NOTE_REVIEW        = 'NoteReview';
const ACTION_SEARCH_NOTES       = 'SearchNotes'

const BTN_NOTES_REVIEW          = 'btnNoteReview';

const BTN_START_VID             = 'start_vid_recording';
const BTN_STOP_VID              = 'stop_vid_recording';

const BTN_START_AUD             = 'start_aud_recording';
const BTN_STOP_AUD              = 'stop_aud_recording';

const BTN_TRANSCRIPT_REVIEW     = 'btnTranscriptReview';
const BTN_UPDATE_CRM            = 'UpdateCRMNow';
const BTN_UPLOAD_RECORDING      = 'UploadRecordedMedia2Server';

const BUTTON_STATE_INITIAL      = 1;
const BUTTON_STATE_HIDDEN       = 2;
const BUTTON_STATE_VISIBLE      = 3;

const COUNTDOWN_DEFAULT         = 3600;
const CI_CATCH_UP_DELAY_DEFAULT = 5;

const CSS_ID_TALK2URCRM         = 'W2A_Talk2UrCrm';

const ELEMENT_ID_CHEAT_SHEET_BX = 'CheatSheetContainer';
const ELEMENT_ID_CHEAT_SHEET_GO = 'cheatsheet';
const ELEMENT_ID_INSTRUCTIONBOX = 'InstructionBox';
const ELEMENT_ID_CUSTOM_FORM    = 'STTCustomFormContainer';
const ELEMENT_ID_PLAYBACK_TOOLS = 'PlaybackToolz';
const ELEMENT_ID_REVIEW_TOOLS   = 'docReviewingTools';
const ELEMENT_ID_W2A_FORM       = 'docWords2ActionForm';
const ELEMENT_ID_W2A_RECORDER   = 'Words2ActionRecorder';
const ELEMENT_ID_W2A_VERIFIER   = 'Words2ActionVerifier';
const ERROR_MESSAGEE_INTERNAL   = 'An internal error has occurred. Please contact customer support.'

const MEDIA_IS_VIDEO            = 'vid';
const MEDIA_IS_AUDIO            = 'aud';

const NO_LEAD_ID_YET            = 0;
const NOTE_ID_PREFIX            = 'NoteId=';
const NOTE_ITEM_COUNT           = 3;

const PICK_LIST_NOTES           = 'm4vNoteId';
const NOTES_FILTER              = 'NotesFilter'
const NOTES_FILTER_CONTAINER    = 'NotesFilterSetupContainer';
const PICK_LIST_TRANSCRIPTS     = 'm4vurl';

const TOGGLE_WORD_LEN           = 6;

//  ----------------------------------------------------------------------------
//  Function ToggleDivs uses this array of objects.
//  ----------------------------------------------------------------------------

const _aoRecorderButtonTexts    = {
                                        'Talk2CRM_aud'    : {
                                                                'StartButtonId'     : BTN_START_AUD,
                                                                'StartButtonText'   : 'Start recording Field Values.',
                                                                'StopButtonId'      : BTN_STOP_AUD,
                                                                'StopButtonText'    : 'Stop recording Field Values.'
                                                            },
                                        'Talk2CRM_vid'    : {
                                                                'StartButtonId'     : BTN_START_VID,
                                                                'StartButtonText'   : 'Start recording Field Values.',
                                                                'StopButtonId'      : BTN_STOP_VID,
                                                                'StopButtonText'    : 'Stop recording Field Values.'
                                                            },
                                        'DictateNote_aud' : {
                                                                'StartButtonId'     : BTN_START_AUD,
                                                                'StartButtonText'   : 'Start recording Notes.',
                                                                'StopButtonId'      : BTN_STOP_AUD,
                                                                'StopButtonText'    : 'Stop recording Notes.'
                                                            },
                                        'DictateNote_vid' : {
                                                                'StartButtonId'     : BTN_START_VID,
                                                                'StartButtonText'   : 'Start recording Notes.',
                                                                'StopButtonId'      : BTN_STOP_VID,
                                                                'StopButtonText'    : 'Stop recording Notes.'
                                                            },
                                  };

//  ----------------------------------------------------------------------------
//  Function ReplaceTokensInTitlePlaceholder uses this array.
//  ----------------------------------------------------------------------------

const _aoTitleBlockSub          = {
                                        'ExternalCRMId'   : {
                                                                'Prefix'            : ' (CRM ID ',
                                                                'Suffix'            : ')'
                                                            },
                                        'firstName'       : {
                                                                'Prefix'            : '',
                                                                'Suffix'            : ''
                                                            },
                                  };

const _astrUISelectElements     = [
                                        'm4vurl',
                                        'm4vnoteid',
                                        'cbowords2actionslogin',
                                        'crmsearchableentities',
                                        'media',
                                        'nextaction',
                                        'nextaction_shadow'
                                  ];

const _astrHideForNotesOnly     = [
                                        'DoCallRail',
                                        'DoWords2Actions',
                                        'ValidateFields',
                                        'SearchCRM',
                                        'DoRefreshThisFormNow2',
                                        BTN_UPDATE_CRM,
                                        'ShowAnotherForm',
                                        'FindRecentActivity',
                                        'CheatSheetDisplayToggle'
                                  ];

//  ----------------------------------------------------------------------------
//  The following array maps the text recorded by DeepGram to the numeric values
//  that appear in the pick list values entered into the CustomFieldValueLookup
//  table rows for the WA_Task_EstimatedTime property of a WA-Task.
//  ----------------------------------------------------------------------------

const _astrW2A_Task_Word_Map    = [
                                        'fifteen¬15',
                                        'thirty¬30',
                                        'sixty¬60'
                                  ];

debugger;

// var  _fUnPostedRecording        = false;                    // The _fUnPostedRecording flag is TRUE when a W2A
                                                               // recording has been created but remains unposted.
var  _astrRegisteredForBlur     = [ ];
var  _astrRegisteredForFocus    = [ ];

var  _fIDBoxHidden              = true;

var  chunks                     = [ ];

var  _aNoteRecordingUris        = null;
var  _aW2ARecordingUris         = null;

var  countdown                  = parseInt ( LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                               'GET',
                                                               {
                                                                   'monikor'         : 'W2ARecordingLimit',
                                                                   'tenantId'        : _tenantid,
                                                                   'domainId'        : _domainid,
                                                                   'defaultValue'    : COUNTDOWN_DEFAULT
                                                               } ) );

var  docMediaRecorder           = null;
var  downloadButton             = null;
// var  uploadButton               = null;
var  uploadedButton             = null;
var  recordedMedia              = null;
var  selectedMedia              = null;

var  mediaSelector              = null;
var  webCamContainer            = null;

var  UploadMediaOption          = null;

var  objInitialValue            = null;
var  strCompanionValue          = null;

var  mediaRecStarted;
var  mediaRecStopped;
     window._PickListValues     = null;
     window._PageBlockDiv       = null;
     window._ToastMaster        = null;

//  ----------------------------------------------------------------------------
//  Get this value once at startup and lock in its value as a JavaScript Boolean
//  value.
//
//  This flag is used twice.
//
//  1)  Event delegate function Promote2CRM passes it as a native Boolean to the
//      like named function (ASP.NET MVC controller method) in the SalesTalk _objAPI
//      to inform it that it should call ReturnToSender without an ExternalCRMId
//      so that it creates a new record in the CRM.
//
//  2)
//  ----------------------------------------------------------------------------

const fCreateNewCRMRecord       = GetParamValue ( 'CreateNewCRMRecord' , paramsCollection ) === 'true' ? true : false;

console.log ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                 Words2Actions_Recorder_Forms_VERSION ,
                                 Words2Actions_Recorder_Forms_LastUpdated ,
                                 '- Adding DOMContentLoaded event listener defined in the current page' ) );


//  ----------------------------------------------------------------------------
//  Per "Window: pagehide event,"
//
//      The best event to use to signal the end of a user's session
//      is the visibilitychange event. In browsers that don't support
//      visibilitychange the pagehide event is the next-best
//      alternative.
//
//  https://developer.mozilla.org/en-US/docs/Web/_objAPI/Window/pagehide_event
//  ----------------------------------------------------------------------------


document.addEventListener ( 'visibilitychange', ( ) =>
{
    const strMethodName = 'visibilitychange event listener';

    debugger;

    window.clearInterval ( window._IntervalHandle );

    if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
    {
        if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
        {
            LLCommon.Trace ( 'The document is being hidden, calling DoUpdateCrmNow for async AJAX request' );

            if ( LLCommon._fFormIsDirty )
            {
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: Updating dirty record of SalesTalk lead ID ' + _leadid + ' in CRM' ) );

                if ( document.getElementById ( 'UpdateCRMNow' ).disabled )
                {
                    const strMessage =  'With respect to Lead ID ' + _leadid + ', the CRM was NOT updated because there are errors on the form, so the "UpdateCRMNow" button is disabled. If the ExternalCRMId has a value, EveryFewMinutes MAY be able to asynchronously update the lead record.';

                    console.log ( strMessage );
                    LLCommon.LogException ( strMethodName + ': ' + strMessage );
                }   // TRUE (Due to unresolved errors in the form, the asynchronous CRM update was suppressed.) block, if ( document.getElementById ( 'UpdateCRMNow' ).disabled )
                else
                {
                    if ( ( LLCommon.EntityType !== null ) && ( ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) ) )
                    {
                        if ( window.confirm ( 'LOSE your changes?' ) )
                        {
                            console.log ( 'The operator elected to DISCARD their unsaved work.' );
                        }   // TRUE (The operator elected to discard their unsaved work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                        else
                        {
                            DoUpdateCrmNow ( true );        // DoUpdateCRM turns OFF the LLCommon._fFormIsDirty flag.
                        }   // FALSE (The operator elected to save their work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                    }   // TRUE (The active form is write only.) block, if ( ( LLCommon.EntityType !== null ) && ( ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) || ( !document.getElementById ( 'UpdateCRMNow' ).disabled ) )
                    else
                    {
                        DoUpdateCrmNow ( true );            // DoUpdateCRM turns OFF the LLCommon._fFormIsDirty flag.
                    }   // FALSE (The active form is a standard read/write form.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                }   // FALSE (Since the form is clean, the asynchronous CRM update will happen.) block, if ( document.getElementById ( 'UpdateCRMNow' ).disabled )

                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: DoUpdateCrmNow returned' ) );
            }   // TRUE (The form is dirty.) block, if ( LLCommon._fFormIsDirty )
            else
            {
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: DoUpdateCrmNow SKIPPED because the form is clean.' ) );
            }   // FALSE (The form is clean.) block, if ( LLCommon._fFormIsDirty )
        }   // TRUE (The page that loaded this script is a Words2Actions form.) block, if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
        else
        {
            LLCommon.Trace ( 'Form (window) is closing, calling DoUpdateCrmNow SKIPPED' );
        }   // FALSE (Though the page that loaded this script implements some Words2Actions features, it is NOT a full-fledged Words2Actions form.) block, if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
    }   // TRUE (There are no pending CRM updates.) block, if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
    else
    {
        sessionStorage.removeItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId );
    }   // FALSE (An update to the CRM is pending. Withhold further updates.) block, if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
}); // document.addEventListener ( 'visibilitychange', ( ) =>


document.addEventListener ( 'beforeunload', ( ) =>
{
    const strMethodName = 'beforeunload event listener';

    debugger;

    window.clearInterval ( window._IntervalHandle );

    if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
    {
        if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
        {
            LLCommon.Trace ( 'The document is unloading, calling DoUpdateCrmNow for async AJAX request' );

            if ( LLCommon._fFormIsDirty )
            {
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: Updating dirty record of SalesTalk lead ID ' + _leadid + ' in CRM' ) );

                if ( document.getElementById ( 'UpdateCRMNow' ).disabled )
                {
                    const strMessage =  'With respect to Lead ID ' + _leadid + ', the CRM was NOT updated because there are errors on the form, so the "UpdateCRMNow" button is disabled. If the ExternalCRMId has a value, EveryFewMinutes MAY be able to asynchronously update the lead record.';

                    console.log ( strMessage );
                    LLCommon.LogException ( strMethodName + ': ' + strMessage );
                }   // TRUE (Due to unresolved errors in the form, the asynchronous CRM update was suppressed.) block, if ( ( LLCommon.EntityType !== null ) && ( ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) || document.getElementById ( 'UpdateCRMNow' ).disabled ) )
                else
                {
                    if ( ( LLCommon.EntityType !== null ) && ( ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) || document.getElementById ( 'UpdateCRMNow' ).disabled ) )
                    {
                        if ( window.confirm ( 'LOSE your changes?' ) )
                        {
                            console.log ( 'The operator elected to DISCARD their unsaved work.' );
                        }   // TRUE (The operator elected to discard their unsaved work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                        else
                        {
                            DoUpdateCrmNow ( true );        // DoUpdateCRM turns OFF the LLCommon._fFormIsDirty flag.
                        }   // FALSE (The operator elected to save their work.) block, if ( window.confirm ( 'LOSE your changes?' ) )
                    }   // TRUE (The active form is write only.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                    else
                    {
                        DoUpdateCrmNow ( true );            // DoUpdateCRM turns OFF the LLCommon._fFormIsDirty flag.
                    }   // FALSE (The active form is a standard read/write form.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                }   // FALSE (Since the form is clean, the asynchronous CRM update will happen.) block, if ( ( LLCommon.EntityType !== null ) && ( ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) || document.getElementById ( 'UpdateCRMNow' ).disabled ) )

                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: DoUpdateCrmNow returned' ) );
            }   // TRUE (The form is dirty.) block, if ( LLCommon._fFormIsDirty )
            else
            {
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: DoUpdateCrmNow SKIPPED because the form is clean.' ) );
            }   // FALSE (The form is clean.) block, if ( LLCommon._fFormIsDirty )
        }   // TRUE (The page that loaded this script is a Words2Actions form.) block, if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
        else
        {
            LLCommon.Trace ( 'Form (window) is closing, calling DoUpdateCrmNow SKIPPED' );
        }   // FALSE (Though the page that loaded this script implements some Words2Actions features, it is NOT a full-fledged Words2Actions form.) block, if ( location.pathname.endsWith ( '/Words2Actions_Form_TEMPLATE.HTML' ) )
    }   // TRUE (There are no pending CRM updates.) block, if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
    else
    {
        sessionStorage.removeItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId );
    }   // FALSE (An update to the CRM is pending. Withhold further updates.) block, if ( sessionStorage.getItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId ) === null )
}); // document.addEventListener ( 'beforeunload', ( ) =>


window.addEventListener ( 'DOMContentLoaded' , function ( )
{
    function ReplaceTokensInTitlePlaceholder ( pdocElement )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      ReplaceTokensInTitlePlaceholder
        //
        //  Function Goal:      Implement token substitution along the following lines: <p id="TitleContainer" class="W2A_MyView_Title_Centered">Page Name: ##PageTitle## for ##firstName## ##lastName##</p>
        //
        //  Function Arguments: pdocElement = Element in which to make substitution
        //
        //  Return Value:       New InnerHtml constructed from template
        //  --------------------------------------------------------------------

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        debugger;

        function GetPrefixOrSuffix ( pstrElementName , pchrWhich )
        {
            const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

            if ( _W2AButton )
            {
                return EMPTY_STRING;
            }
            else
            {
                const oReplacementTokens = pstrElementName in _aoTitleBlockSub ? _aoTitleBlockSub [ pstrElementName ] : null;

                if ( oReplacementTokens !== null )
                {
                    switch ( pchrWhich )
                    {
                        case 'P':
                            return oReplacementTokens.Prefix
                        case 'S':
                            return oReplacementTokens.Suffix
                    }   // switch ( pchrWhich )

                    return EMPTY_STRING;
                }   // TRUE (anticipated outcome) block, if ( oReplacementTokens !== null )
                else
                {
                    return EMPTY_STRING;
                }   // FALSE (unanticipated outcome) block, if ( oReplacementTokens !== null )
            }   // if ( _W2AButton )
        }   // function GetPrefixOrSuffix


        const rxp               = /\#\#(.*?)\#\#/ig;
        const rxpMatches        = pdocElement.innerHTML.matchAll ( rxp );
        var   intJ              = ARRAY_FIRST_ELEMENT;

        //  --------------------------------------------------------------------
        //  The rxpMatches JavaScript object is equivalent to the Matches
        //  collection in the Microsoft .NET regular expression engine. As is
        //  the case with every regular expression engine that I've used, the
        //  first match is always the whole match, the second is the first
        //  captured submatch, and so forth.
        //  --------------------------------------------------------------------

        var strNewInnerHTML     = EMPTY_STRING;
        var intPosCurrIndex     = SUBSTRING_FIRST_CHAR;
        var docElement4NewValue;

        for ( let rxpCurrentMatch of rxpMatches )
        {
            strNewInnerHTML     += pdocElement.innerHTML.substring ( intPosCurrIndex , rxpCurrentMatch.index );
            intPosCurrIndex     =  rxpCurrentMatch.index + rxpCurrentMatch [ ARRAY_FIRST_ELEMENT ].length;
            docElement4NewValue =  rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] === 'PageTitle' ? null : document.getElementById ( rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] );

            if ( docElement4NewValue !== null && docElement4NewValue.value !== null && docElement4NewValue.value.length > EMPTY_STRING_LENGTH )
            {
                strNewInnerHTML += GetPrefixOrSuffix ( rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] , 'P' )
                strNewInnerHTML += ( rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] === 'PageTitle' || ( rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] === 'ExternalCRMId' && _W2AButton ) ) ? document.title : docElement4NewValue !== null ? docElement4NewValue.value : EMPTY_STRING;
                strNewInnerHTML += GetPrefixOrSuffix ( rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] , 'S' )
            }   // TRUE (The reference to the element that holds the value to substitute is defined, it has a value, and the length of its string representation is greater than zero.) block, if ( docElement4NewValue !== null && docElement4NewValue.value !== null && docElement4NewValue.value.length > EMPTY_STRING_LENGTH )
            else
            {
                strNewInnerHTML += rxpCurrentMatch [ ARRAY_SECOND_ELEMENT ] === 'PageTitle' ? document.title : docElement4NewValue !== null ? docElement4NewValue.value : EMPTY_STRING;
            }   // FALSE (The reference to the element that holds the value to substitute is UNdefined, its value is null, or the length of its string representation is equal to zero.) block, if ( docElement4NewValue !== null && docElement4NewValue.value !== null && docElement4NewValue.value.length > EMPTY_STRING_LENGTH )
        }   // for ( let rxpCurrentMatch of rxpMatches )

        //  --------------------------------------------------------------------
        //  Scoop up all characters, if any, that follow the last match.
        //  --------------------------------------------------------------------

        if ( pdocElement.innerHTML.length > intPosCurrIndex )
        {
            strNewInnerHTML     += pdocElement.innerHTML.substring ( intPosCurrIndex );
        }   // if ( pdocElement.innerHTML.length > intPosCurrIndex )

        return strNewInnerHTML;
    }   // function ReplaceTokensInTitlePlaceholder


    function GetValues4AllPickLists ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      GetValues4AllPickLists
        //
        //  Function Goal:      Load a dictionary with a list of valid values of
        //                      all pick lists.
        //
        //  Function Arguments: None. Inputs are self-contained.
        //
        //  Return Value:       New Object containing the values for each pick
        //                      list.
        //  --------------------------------------------------------------------

        const strMethodName            = LLCommon.GetNameOfCurrentFunction ( );

        debugger;

        const docMyViewContainer       = lockAndLoad ( );

        LLCommon.ShowCRMEntityMessages ( document.getElementById ( 'SysCRMLeadOrContact' ).value );

        if ( docMyViewContainer !== null )
        {
            const adocMVButtons        = docMyViewContainer.querySelectorAll ( 'button' );
            const astrPickListInputs   = new Array ( adocMVButtons.length );

            for ( var intJ = ARRAY_FIRST_ELEMENT;
                      intJ < adocMVButtons.length;
                      intJ++ )
            {
                astrPickListInputs [ intJ ] = adocMVButtons [ intJ ].id.substring ( SUBSTRING_FIRST_CHAR , adocMVButtons [ intJ ].id.length - 9 );
            }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < adocMVButtons.length; intJ++ )

            return LLCommon.DoAjax ( 'GetPickListValuesForMultipleFields',
                                     'GET',
                                     {
                                         'systemPropertyList' : astrPickListInputs.join ( ),
                                         'tenantId'           : _tenantid,
                                         'domainId'           : _domainid
                                     } );
        }   // if ( docMyViewContainer !== null )
        else
        {
            return null;
        }
    }   // function GetValues4AllPickLists


    function EvaluatePickListValues ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      EvaluatePickListValues
        //
        //  Function Goal:      Validate the pick list values against the lists
        //                      created by GetValues4AllPickLists.
        //
        //  Function Arguments: None. Inputs are self-contained.
        //
        //  Return Value:       New InnerHtml constructed from template
        //  --------------------------------------------------------------------

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        LLCommon.Trace ( strMethodName + ': Evaluating fields that represent pick lists' );

        try
        {
            var   docTextBoxToEvaluate  = null;
            var   strTextBoxValue       = EMPTY_STRING;
            var   intJ                  = NUMERIC_ZERO;
            var   fValueIsValid         = false;

            if ( window._PickListValues !== null )
            {
                const intPickListCount      = Object.keys ( window._PickListValues ).length;

                for ( var strCustomFieldName of Object.keys ( window._PickListValues ) )
                {
                    /*
                        ------------------------------------------------------------------------------------
                        The following two examples demonstrate the architecture of the genric Dictionary
                        object returned by GetPickListValuesForMultipleFields.

                            window._PickListValues.employeeType.PickListValues[0].DisplayText   = '1099'
                            window._PickListValues.employeeType.PickListValues[0].Name          = '1099'

                        Each custom field name manifests as a property on window._PickListValues, which is
                        accessible dynamically by treating the window._PickListValues object like an
                        Associative Array (which it actually _is_ in the C# server side code).
                        ------------------------------------------------------------------------------------
                    */

                    LLCommon.Trace (   strMethodName + ': Pick List #' + ( ++intJ ) + ' of ' + intPickListCount
                                     + ': Name = ' + strCustomFieldName
                                     + ', Count of Valid Values = '
                                     + window._PickListValues [ strCustomFieldName ].PickListValues.length
                                     + ', as follows:' );

                    docTextBoxToEvaluate    = document.getElementById ( strCustomFieldName );

                    if ( docTextBoxToEvaluate !== null )
                    {
                        fValueIsValid       = false;
                        strTextBoxValue     = docTextBoxToEvaluate.value.toLowerCase ( );

                        if ( strTextBoxValue.length > EMPTY_STRING_LENGTH )
                        {
                            for ( var intValueIndex = ARRAY_FIRST_ELEMENT;
                                      intValueIndex < window._PickListValues [ strCustomFieldName ].PickListValues.length;
                                      intValueIndex++ )
                            {
                                var strPickListValueToMatch = LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12
                                                              ? window._PickListValues [ strCustomFieldName ].PickListValues [ intValueIndex ].DisplayText
                                                              : window._PickListValues [ strCustomFieldName ].PickListValues [ intValueIndex ].Name;
                                var strPickListDisplayValue = window._PickListValues [ strCustomFieldName ].PickListValues [ intValueIndex ].DisplayText;

                                if ( _astrUISelectElements.indexOf ( docTextBoxToEvaluate.id.toLowerCase ( ) ) === INDEXOF_NOT_FOUND )
                                {
                                    debugger;

                                    if ( strTextBoxValue === strPickListValueToMatch.toLowerCase ( ) )
                                    {
                                        LLCommon.Trace (   strMethodName + ': Pick List #' + intJ
                                                         + ': Name = '           + strCustomFieldName
                                                         + ' Valid Value '       + LLCommon.OrdinalFromIndex ( intValueIndex )
                                                         + ' of '                + window._PickListValues [ strCustomFieldName ].PickListValues.length
                                                         + ': Value to Match = ' + strPickListValueToMatch
                                                         + ', Display Text = '   + strPickListDisplayValue
                                                         + ', MATCHES value in textbox' );

                                        if ( strTextBoxValue !== docTextBoxToEvaluate.value )
                                        {
                                            UpdateIfChanged ( docTextBoxToEvaluate.id , false )
                                        }   // if ( strTextBoxValue !== docTextBoxToEvaluate.value )

                                        fValueIsValid = true;
                                    }   // TRUE (favorable outcome) block, if ( strTextBoxValue.toLowerCase ( ) === strPickListValueToMatch.toLowerCase ( ) )
                                    else
                                    {
                                        LLCommon.Trace (   strMethodName + ': Pick List #' + intJ
                                                         + ': Name = '           + strCustomFieldName
                                                         + ' Valid Value '       + LLCommon.OrdinalFromIndex ( intValueIndex )
                                                         + ' of '                + window._PickListValues [ strCustomFieldName ].PickListValues.length
                                                         + ': Value to Match = ' + strPickListValueToMatch
                                                         + ', Display Text = '   + strPickListDisplayValue );
                                    }   // FALSE (unfavorable outcome) block, if ( strTextBoxValue.toLowerCase ( ) === strPickListValueToMatch.toLowerCase ( ) )
                                }   // if ( _astrUISelectElements.indexOf ( docTextBoxToEvaluate.id.toLowerCase ( ) ) === INDEXOF_NOT_FOUND )
                            }   // for ( var intValueIndex = ARRAY_FIRST_ELEMENT; intValueIndex < window._PickListValues [ strCustomFieldName ].PickListValues.length; intValueIndex++ )

                            if ( fValueIsValid )
                            {
                                LLCommon.Trace ( 'Custom Field ' + strCustomFieldName + ' value in text box, ' + strTextBoxValue + ' IS VALID.' );
                                LLCommon.AddOrRemoveStyles ( docTextBoxToEvaluate ,
                                                             'STT_Field_with_Error' ,
                                                             LLCommon.CSS_SELECTOR_REMOVE );
                            }   // TRUE (desired outcome) block, if ( fValueIsValid )
                            else
                            {
                                LLCommon.Trace ( 'Custom Field ' + strCustomFieldName + ' value in text box, ' + strTextBoxValue + ' IS INVALID.' );
                                LLCommon.AddOrRemoveStyles ( docTextBoxToEvaluate ,
                                                             'STT_Field_with_Error' ,
                                                             LLCommon.CSS_SELECTOR_ADD );
                                // docErrorElement.title   = _LeadLifeJSHelpers.ValidationErrorMessageArray [ aoErrorInfo [ intCurrentFieldIndex ].ReasonMessageId ];
                            }   // FALSE (undesired outcome) block, if ( fValueIsValid )
                        }   // TRUE (anticipated outcome) block, if ( strTextBoxValue.length > EMPTY_STRING_LENGTH )
                        else
                        {
                            LLCommon.Trace ( 'Field ' + strCustomFieldName + ' value is empty.' );
                        }   // FALSE (unanticipated outcome) block, if ( strTextBoxValue.length > EMPTY_STRING_LENGTH )
                    }   // TRUE (anticipated outcome) block, if ( docTextBoxToEvaluate !== null )
                }   // for ( var strCustomFieldName of Object.keys ( window._PickListValues ) )
            }   // if ( window._PickListValues !== null )
        }
        catch ( ex )
        {
            LLCommon.Trace ( strMethodName + ex.stack );
        }

        LLCommon.Trace ( strMethodName + ': Finished evaluating fields that represent pick lists' );
    }   // function EvaluatePickListValues


    function MarkSelectedFieldsAsRequired ( pstrMarkAsRequired )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      MarkSelectedFieldsAsRequired
        //
        //  Function Goal:      Validate the pick list values against the lists
        //                      created by GetValues4AllPickLists.
        //
        //  Function Arguments: pstrMarkAsRequired  = Comma-delimited string of
        //                                            element IDs
        //
        //  Return Value:       Void. This function is executed for its side
        //                            effects.
        //  --------------------------------------------------------------------

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        LLCommon.Trace ( strMethodName + ': Marking selected fields as REQUIRED' );

        try
        {
            const astrElementsToMark = pstrMarkAsRequired.split ( CSV_SEPARATOR_CHAR );

            console.log ( strMethodName + ': Input string "' + pstrMarkAsRequired + '" split at comma boundaries into ' + astrElementsToMark.length + ' substrings.' );

            for ( let intJ = ARRAY_FIRST_ELEMENT;
                      intJ < astrElementsToMark.length;
                      intJ++ )
            {
                let docMarkThisElement = document.getElementById ( astrElementsToMark [ intJ ] );

                if ( docMarkThisElement !== null )
                {
                    LLCommon.AddOrRemoveStyles ( docMarkThisElement ,
                                                 'STT_Required' ,
                                                 LLCommon.CSS_SELECTOR_ADD );
                }   // TRUE (anticipated outcome) block, if ( docMarkThisElement !== null )
                else
                {
                    LLCommon.Trace ( strMethodName + ': Element ID ' + astrElementsToMark [ intJ ] + ' not found in document. Element SKIPPED' );
                }   // FALSE (unanticipated outcome) block, if ( docMarkThisElement !== null )
            }   // for ( let intJ = ARRAY_FIRST_ELEMENT; intJ < astrElementsToMark.length; intJ++ )
        }
        catch ( ex )
        {
            LLCommon.Trace ( strMethodName + ex.stack );
            LLCommon.LogException ( ex );
        }

        LLCommon.Trace ( strMethodName + ': Finished marking selected fields as REQUIRED' );
    }   // function MarkSelectedFieldsAsRequired


    (function(global) {
      const _objSecrets = { };

      const _objAPI = {
          set: ( key, value ) => {
              if ( LLCommon.IsString ( key ) && key.length > EMPTY_STRING_LENGTH )
              {
                  _objSecrets [ key ] = value;
              }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( key ) && key.length > EMPTY_STRING_LENGTH )
              else
              {
                  throw new Error ( 'Specified object key must be a non-empty String. Actual Type = ' + ( typeof key ) + ( typeof key === 'string' ? ', which is empty.' : ', value = ' + key ) );
              }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( key ) && key.length > EMPTY_STRING_LENGTH )
          },    // set
          get: ( key ) => {
                if ( LLCommon.IsString ( key ) && key.length > EMPTY_STRING_LENGTH )
                {
                    return _objSecrets [ key ];
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pobjKey ) && pobjKey.length > EMPTY_STRING_LENGTH )
                else
                {
                    throw new Error ( 'Specified object key must be a non-empty String. Actual Type = ' + ( typeof key ) + ( typeof key === 'string' ? ', which is empty.' : ', value = ' + key ) );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pobjKey ) && pobjKey.length > EMPTY_STRING_LENGTH )
          },    // get
          has: ( key ) => {

                if ( LLCommon.IsString ( key ) && key.length > EMPTY_STRING_LENGTH )
                {
                    return key in _objSecrets;
                }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pobjKey ) && pobjKey.length > EMPTY_STRING_LENGTH )
                else
                {
                    throw new Error ( 'Specified object key must be a non-empty String. Actual Type = ' + ( typeof key ) + ( typeof key === 'string' ? ', which is empty.' : ', value = ' + key ) );
                }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pobjKey ) && pobjKey.length > EMPTY_STRING_LENGTH )
          },    // has
          teamInfo:         function ( ) {
                                             return _objSecrets [ this.TEAM_INFO_KEY ]
                                         },
          teamMemberId:     function ( ) {
                                             const  objTeamInfoItem = _objSecrets [ this.TEAM_INFO_KEY ];
                                             return objTeamInfoItem ? objTeamInfoItem.InsideTeamId : NUMERIC_ZERO;
                                         },
          teamMemberEmail:  function ( ) {
                                             const  objTeamInfoItem = _objSecrets [ this.TEAM_INFO_KEY ];
                                             return objTeamInfoItem ? objTeamInfoItem.Email        : null;
                                         },
          teamMemberstatus: function ( ) {
                                             const  objTeamInfoItem = _objSecrets [ this.TEAM_INFO_KEY ];
                                             return objTeamInfoItem ? objTeamInfoItem.Status       : this.IS_UNKNOWN;
                                         },
          teamMemberType:   function ( ) {
                                             const  objTeamInfoItem = _objSecrets [ this.TEAM_INFO_KEY ];
                                             return objTeamInfoItem ? objTeamInfoItem.Type         : this.IS_TEAM_TYPE_UNKNOWN;
                                         }
      };    // const _objAPI

      // Define constants
      Object.defineProperties ( _objAPI, {
          VERSION : {
              value         : '1.0.0',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          CREATED_AT : {
              value         : new Date ( ).toISOString ( ),
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_KNOWN : {
              value         : true,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_UNKNOWN : {
              value         : false,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          TEAM_INFO_KEY : {
              value         : 'Wise_Agent_Team_Membership_Info',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          TEAM_ROSTER_KEY : {
              value         : 'Team_Roster',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          TELEPHONY_FLAG_KEY : {
              value         : 'TelephonyFlag',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          DISABLE_TELEPHONY_FLAG : {
              value         : false,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          ENABLE_TELEPHONY_FLAG : {
              value         : true,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },

          LABEL_INSIDETEAMID : {
              value         : 'InsideTeamId',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          LABEL_INSIDETEAMEMAIL : {
              value         : 'Email',
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_TEAM_TYPE_UNKNOWN : {
              value         : 0,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_TEAM_OWNER : {
              value         : 1,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_TEAM_MEMBER : {
              value         : 2,
              writable      : false,
              configurable  : false,
              enumerable    : true
          },
          IS_TEAM_PERMITTED : {
              value         : 3,
              writable      : false,
              configurable  : false,
              enumerable    : true
          }
      });

      // Freeze the _objAPI to prevent tampering
      Object.freeze ( _objAPI );

      global._Words2Actions_Recorder_Forms_pvt = _objAPI;
    })(window);


    class TeamMemberInfo
    {
        constructor ( pintInsideTeamId , pstrName , pstrEmail , pstrJobTitle )
        {
            if ( !Number.isInteger ( pintInsideTeamId ) )   { throw new Error ( 'Parameter pintInsideTeamId MUST be an Integer. Supplied Value = ' + pintInsideTeamId ); }

            if ( !LLCommon.IsString ( pstrName ) )          { throw new Error ( 'Parameter pstrName MUST be a String. Supplied Value = ' + pstrName ); }
            if ( !LLCommon.IsString ( pstrEmail ) )         { throw new Error ( 'Parameter pstrEmail MUST be a String. Supplied Value = ' + pstrEmail ); }
            if ( !LLCommon.IsString ( pstrJobTitle ) )      { throw new Error ( 'Parameter pstrJobTitle MUST be a String. Supplied Value = ' + pstrJobTitle ); }

            if ( pstrEmail.length === EMPTY_STRING_LENGTH ) { throw new Error ( 'Parameter pstrEmail cannot be BLANK.' ); }

            this.InsideTeamId   = pintInsideTeamId;
            this.Name           = pstrName;
            this.Email          = pstrEmail;
            this.JobTitle       = pstrJobTitle;

            this.TelephonyOK    = false;
            this.StalkUserId    = NUMERIC_ZERO;

            Object.freeze ( this );
        }   // constructor ( pintInsideTeamId , pstrName , pstrEmail , pstrJobTitle )


        SetTelephonyFlagAndStalkUserId ( pfTelephonyOK,  pintStalkUserId )
        {
            //  ----------------------------------------------------------------
            //  Method Name:    SetTelephonyFlagAndStalkUserId
            //
            //  Method Goal:    Set the TelephonyOK flag and the STalkUserId
            //                  property.
            //
            //  Inputs:         pfTelephonyOK   = Boolean flag. TRUE when the
            //                                    specified pintStalkUserId has
            //                                    valid phone and extension
            //                                    numbers
            //
            //                  pintStalkUserId = Integer ID of SalesTalk user
            //
            //  Output:         The return value is a new, frozen TeamMemberInfo
            //                  object.
            //  ----------------------------------------------------------------

            return Object.freeze({
                ...this,
                TelephonyOK : pfTelephonyOK,
                StalkUserId : pintStalkUserId
            });
        }   // SetTelephonyFlagAndStalkUserId method
    }   // class TeamMemberInfo


    function CreateResetButton ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      CreateResetButton
        //
        //  Function Goal:      Create a HTML BUTTON element to be placed to the
        //                      right of the SELECT that returns the Inside Team
        //                      ID to use for this connection.
        //
        //  Function Arguments: None. The button is generated from scratch using
        //                            literal values throughout.
        //
        //  Return Value:       The return value of this function is a reference
        //                      to the JavaScript object that represents the new
        //                      BUTTON element.
        //
        //  Remarks:            This routine is protected by the try/catch block
        //                      of its caller.
        //  --------------------------------------------------------------------

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        const rbtnReset                 = document.createElement ( 'button' );

        rbtnReset.id                    = 'ResetWATeamMembership';
        rbtnReset.type                  = 'button';
        rbtnReset.className             = 'STT_Stoplight_Red wa_control'
        rbtnReset.style.marginLeft      = "5px";
        rbtnReset.innerHTML             = '<span style="color: #f39c12;"><i class="fa fa-warning" aria-hidden="true"></i></span>&nbsp;Change User';
        rbtnReset.title                 = 'Click to select a different Wise Agent Team Member.';
        LLCommon.applyEssentialARIAProperties ( rbtnReset );

        rbtnReset.addEventListener ( 'click' , ( event ) =>
        {
            const docSelect             = event.currentTarget.previousElementSibling;

            if ( docSelect && docSelect.nodeName === 'SELECT' )
            {
                docSelect.disabled      = false;
                docSelect.focus ( );
            }   // if ( docSelect && docSelect.nodeName === 'SELECT' )
        }); // end of rbtnReset.addEventListener ( 'click'

        return rbtnReset;
    }   // function CreateResetButton


    function ProcessSelection ( event , poSelectedMember )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      ProcessSelection
        //
        //  Function Goal:      Perform the tasks that revolve around a selected
        //                      OPTION element in the Agent Internal ID list.
        //
        //  Function Arguments: event               = This JavaScript object is
        //                                            the event object that was
        //                                            passed into the event
        //                                            listener that calls it.
        //
        //                      poSelectedMember    = This JavaScript object is
        //                                            the TeamMemberInfo object
        //                                            that represents the team
        //                                            member to register on this
        //                                            machine.
        //
        //  Return Value:       The return value of this function is a reference
        //                      to the new JavaScript object that replaced the
        //                      poSelectedMember object that was passed into the
        //                      function.
        //
        //  Remarks:            This routine is protected by the try/catch block
        //                      of its call chain.
        //  --------------------------------------------------------------------

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        debugger;

        var   oSelectedMember   = poSelectedMember;         // Make a local copy that I can modify with impunity.
        const intStalkUserId    = parseInt ( LLCommon.DoAjax ( 'SalesTalkSalesforce/ValidateWALoginId',
                                                               'GET',
                                                               {
                                                                   'pstrWALoginId' : oSelectedMember.Email,
                                                                   'pintDomainID'  : _domainid
                                                               } ) , 10 );

        if ( intStalkUserId < NUMERIC_ZERO )
        {
            DisableCalling ( );
        }   // if ( intStalkUserId < NUMERIC_ZERO )

        const intAbsSTalkUserId = Math.abs ( intStalkUserId );

        //  --------------------------------------------------------------------
        //  Unless the User ID is zero, indicating a configuration error, the
        //  absolute value of the user ID, along with the RealEmailAddress per
        //  the SalesTalk user table are used to update the properties that live
        //  in the LLCommon object and the global variables that it defines.
        //
        //  SalesTalk API function GetBasicSalesTalkUserInfo is then called to
        //  update the LLCommon.UserInfo object's properties, and, finally, the
        //  AssociateWATeamIdWithStalkUserId method is called upon to store the
        //  InsideTeamId into the SalesTalk user record.
        //  --------------------------------------------------------------------

        if ( intAbsSTalkUserId > NUMERIC_ZERO )
        {
            oSelectedMember     = oSelectedMember.SetTelephonyFlagAndStalkUserId ( intStalkUserId > NUMERIC_ZERO ? _Words2Actions_Recorder_Forms_pvt.ENABLE_TELEPHONY_FLAG : _Words2Actions_Recorder_Forms_pvt.DISABLE_TELEPHONY_FLAG, intAbsSTalkUserId );

            _userid             = oSelectedMember.StalkUserId;
            LLCommon.UserId     = oSelectedMember.StalkUserId;

            LLCommon.DialerLogin= oSelectedMember.Email;
            _login              = oSelectedMember.Email;

            _useridSource       = SRC_IS_WISE_AGENT_TEAM_ROSTER;
            _loginSource        = SRC_IS_WISE_AGENT_TEAM_ROSTER;

            LLCommon.UserInfo   = LLCommon.DoAjax ( 'GetBasicSalesTalkUserInfo',
                                                    'GET',
                                                    {
                                                        'UserId' : _userid
                                                    });
            LLCommon.ReSyncUserInfo ( );
            const strOutcome    = LLCommon.DoAjax ( 'SalesTalkSalesforce/AssociateWATeamIdWithStalkUserId',
                                                    'GET',
                                                    {
                                                        'pintStalkUserId'   : intAbsSTalkUserId,
                                                        'pintDomainID'      : _domainid,
                                                        'pintInsideTeamId'  : oSelectedMember.InsideTeamId
                                                    });

            if ( strOutcome.length > EMPTY_STRING_LENGTH )
            {
                throw new Error ( strMethodName + ': Exception in AssociateWATeamIdWithStalkUserId method of SalesTalkSalesforceController, Message = ' + strOutcome );
            }   // if ( strOutcome.length > EMPTY_STRING_LENGTH )

            const strSerialized = JSON.stringify ( oSelectedMember );
            localStorage.setItem ( _Words2Actions_Recorder_Forms_pvt.TEAM_INFO_KEY ,
                                   strSerialized );
            console.log ( strMethodName + ': oSelectedMember serialized. Value = ' + strSerialized + ', Key = ' + _Words2Actions_Recorder_Forms_pvt.TEAM_INFO_KEY );

            _Words2Actions_Recorder_Forms_pvt.set ( _Words2Actions_Recorder_Forms_pvt.TEAM_INFO_KEY ,
                                                    oSelectedMember );

            //  ----------------------------------------------------
            //  Unless this is a subsequent rendering, append a small
            //  reset button. Then, disable the pick list.
            //  ----------------------------------------------------

            if ( !document.getElementById ( 'ResetWATeamMembership' ) )
            {
                event.currentTarget.parentElement.appendChild ( CreateResetButton ( ) );
            }   // if ( !document.getElementById ( 'ResetWATeamMembership' ) )

            const docWAPickr    = document.getElementById ( 'WATeamPickList' );

            if ( docWAPickr === null ) { throw new Error ( strMethodName + 'A required HTML element is missing from the document. ID of missing element = WATeamPickList.' ) }

            console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberstatus = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberstatus ( ) );
            console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberType   = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberType   ( ) );
            console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberId     = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberId     ( ) );
            console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberEmail  = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberEmail  ( ) );
        }   // TRUE (anticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
        else
        {
            if ( event.currentTarget.selectedIndex === ARRAY_FIRST_ELEMENT )
            {
                event.currentTarget.focus (  );
            }   // TRUE (anticipated outcome) block, if ( event.currentTarget.selectedIndex === ARRAY_FIRST_ELEMENT )
            else {
                throw new Error ( 'The combo box selectedIndex is out of range. Actual selectedIndex = ' + event.currentTarget.selectedIndex + ', Lower Limit = ' + ARRAY_INVALID_INDEX + ' (no selection made), Upper Limit = ' + event.currentTarget.options.length );
            }   // FALSE (unanticipated outcome) block, if ( event.currentTarget.selectedIndex === ARRAY_FIRST_ELEMENT )
        }   // FALSE (unanticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )

        return oSelectedMember;
    }   // function ProcessSelection


    function CreateTeamPickList ( paoTeamMembers , pintInitialChoice )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      CreateTeamPickList
        //
        //  Function Goal:      Create a HTML SELECT element with an invalid
        //                      default value, followed by a value for each
        //                      member of the Wise Agent Team roster for the
        //                      account.
        //
        //  Function Arguments: paoTeamMembers    = This JavaScript object is
        //                                          the deep frozen array that
        //                                          is returned by function
        //                                          GetWATeamRoster.
        //
        //                      pintInitialChoice = When specified, this
        //                                          optional integer sets the
        //                                          initial selection index of
        //                                          the pick list and indicates
        //                                          that it should be disabled
        //                                          as if it had been processed,
        //                                          and the reset button should
        //                                          be created and activated.
        //
        //  Return Value:       The return value of this function is a reference
        //                      to the JavaScript object that represents the new
        //                      SELECT element.
        //
        //  Remarks:            This routine is protected by the try/catch block
        //                      of its caller.
        //  --------------------------------------------------------------------

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        //  ------------------------------------------------------------
        //  Replace the nombreaking space that serves as a placholder in
        //  the table cell with an empty SELECT element, then fill it
        //  from the list of team members.
        //  ------------------------------------------------------------

        const docWAChooser      = document.getElementById ( 'WATeamPickList' );

        if ( docWAChooser === null ) { throw new Error ( strMethodName + ': A required HTML element is missing from the document. ID of missing element = WATeamPickList.' ) }

        //  --------------------------------------------------------------------
        //  If pintInitialChoice is undefined, skip lookup and assign undefined
        //  explicitly. This aligns with Object.find() behavior when no match is
        //  found.
        //  --------------------------------------------------------------------

        const oLogin2Match      = pintInitialChoice !== undefined
                                  ? paoTeamMembers.find ( oTeamMemberInfo => oTeamMemberInfo.InsideTeamId === pintInitialChoice )
                                  : undefined;

        if ( pintInitialChoice !== undefined && oLogin2Match === undefined )
        {
            console.warn ( `No team member matched pintInitialChoice: ${pintInitialChoice}` );
            LLCommon.LogException ( strMethodName + ': No team member matched pintInitialChoice = ' + pintInitialChoice );
        }   // if ( pintInitialChoice !== undefined && oLogin2Match === undefined )

        const fLoginIsMatched   = ( oLogin2Match !== undefined && oLogin2Match.Name !== 'Words2Actions interface to Wise Agent Production' );

        //  ------------------------------------------------------------
        //  Create the picklist element.
        //  ------------------------------------------------------------

        const docWATeamSelector = document.createElement ( 'select' );
        docWATeamSelector.id    = 'WATeamSelector';
        docWAChooser.innerHTML  = EMPTY_STRING;
        LLCommon.AddOrRemoveStyles ( docWATeamSelector,
                                     'STT_TranscriptPicker_BlueTheme' + SPACE_CHARACTER + 'wa_control',
                                     LLCommon.CSS_SELECTOR_ADD );

        docWAChooser.appendChild ( docWATeamSelector );

        //  ------------------------------------------------------------
        //  The first element is the initial default, which does double
        //  duty as instructions.
        //  ------------------------------------------------------------

        let docChoice           = document.createElement ( 'option' );

        docChoice.value         = '0';
        docChoice.innerHTML     = 'Please select your email ID.';
        docChoice.title         = docChoice.innerHTML;
        docChoice.selected      = ( ( pintInitialChoice === undefined ) && ( !fLoginIsMatched ) ) ? true : false;
        docChoice.setAttribute ( 'aria-label', docChoice.title );

        docWATeamSelector.appendChild ( docChoice );

        //  ------------------------------------------------------------
        //  Next, each element in the paoTeamMembers array begets a pick
        //  list option.
        //  ------------------------------------------------------------

        for ( let intMemberIndex = ARRAY_FIRST_ELEMENT;
                  intMemberIndex < paoTeamMembers.length;
                  intMemberIndex++ )
        {
            docChoice           = document.createElement ( 'option' );

            docChoice.value     = paoTeamMembers [ intMemberIndex ].InsideTeamId;
            docChoice.innerHTML = paoTeamMembers [ intMemberIndex ].Email;
            docChoice.title     =   paoTeamMembers [ intMemberIndex ].Email
                                  + ' ('
                                  + paoTeamMembers [ intMemberIndex ].Name
                                  + ')';
            docChoice.setAttribute ( 'aria-label', docChoice.title );
            docChoice.selected  = ( ( pintInitialChoice === paoTeamMembers [ intMemberIndex ].InsideTeamId ) || ( fLoginIsMatched && oLogin2Match.InsideTeamId === paoTeamMembers [ intMemberIndex ].InsideTeamId ) );

            docWATeamSelector.appendChild ( docChoice );
        }   // for ( let intMemberIndex = ARRAY_FIRST_ELEMENT; intMemberIndex < paoTeamMembers.length; intMemberIndex++; )

        //  ------------------------------------------------------------
        //  Register an event listener for the Change event.
        //  ------------------------------------------------------------

        docWATeamSelector.addEventListener ( 'change' , ( event ) =>
        {
            const strMethodName                 = 'Change event listener for element nodeName = ' + event.currentTarget.nodeName + ', element ID = ' + event.currentTarget.id;
            debugger;

            try
            {
                if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
                {
                    const intMemberId           = parseInt ( event.currentTarget.value , 10 );

                    //  --------------------------------------------------------
                    //  Object aoTeamMembers is an array of TeamMemberInfo
                    //  objects, each of which represents the InsideTeamId,
                    //  Email, Display Name, Phone Number, and Title as they are
                    //  in the Wise Agent team roster.
                    //
                    //  Though the email could be parsed from the display text
                    //  of the option in the SELECT (pick list) element, the
                    //  main reason for obtaining the object is so the SalesTalk
                    //  User ID and a flag that indicates whether to enable or
                    //  disable the telephony interface can be added to it, and
                    //  it can be serialized to localStorage on the workstation
                    //  on which this code executes, so that it knows on future
                    //  visits that the user has been identified, subject to
                    //  change by way of the reset button.
                    //
                    //  The value of intStalkUserId falls into one of three
                    //  ranges.
                    //
                    //  1)  A positive value greater than zero means that the ID
                    //      is valid for all applications, including telephony.
                    //
                    //  2)  A negative value means that the ID is valid for
                    //      identifying a team member to establish ownership of
                    //      new records, but is missing one or both essential
                    //      values (Axxess phone number and Axxess extension)
                    //      required to suppor the Axxess telephony interface.
                    //
                    //  3)  A value of zero means that no SalesTalk login ID
                    //      or RealEmailAddress matches the email address shown
                    //      in the Wise Agent team record. Hence, ownership of
                    //      new records cannot be established. This condition is
                    //      a FATAL ERROR.
                    //
                    //  Unless intStalkUserId is zero, its absolute value (as in
                    //  mathematics) is stored in the ExternalCRMId column of
                    //  the matching SalesTalk user record, for establishing who
                    //  owns new records created from this connection.
                    //  --------------------------------------------------------

                    const aoTeamMembers         = _Words2Actions_Recorder_Forms_pvt.get ( _Words2Actions_Recorder_Forms_pvt.TEAM_ROSTER_KEY );
                    let   oSelectedMember       = aoTeamMembers.find ( memberInfo => memberInfo.InsideTeamId === intMemberId );

                    if ( oSelectedMember )
                    {
                        oSelectedMember         = ProcessSelection ( event ,
                                                                     oSelectedMember );
                        if ( window._PageBlockDiv )
                        {
                            window._PageBlockDiv.remove ( );
                            window._PageBlockDiv    = null;

                            //  ------------------------------------------------
                            //  Once the _PageBlockDiv is removed, the SELECT
                            //  element can revert to its natural place in the
                            //  stacking order. Moreover, retaining the high
                            //  z-index causes it to render on top of the
                            //  Click2Note dialog, and probably also the Call
                            //  Map dialog.
                            //  ------------------------------------------------

                            event.currentTarget.style.removeProperty ( 'z-index' );
                        }   // if ( window._PageBlockDiv )
                    }   // TRUE (anticipated outcome) block, if ( intAbsSTalkUserId > NUMERIC_ZERO )
                    else
                    {
                        const strUnusable       = 'The selected email address, "' + oSelectedMember.Email + '" has no matching SalesTalk login ID, and cannot be used. Please contact support.';
                        console.error ( strUnusable );
                        LLCommon.LogException ( strMethodName + ': ' + strUnusable );
                        alert ( strUnusable , 'native' );
                    }   // FALSE (unanticipated outcome) block, if ( intAbsSTalkUserId > NUMERIC_ZERO )
                }   // TRUE (anticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
                else
                {   // I cannot anticipate a real world scenario in which this can happen. Nevertheless, it's covered.
                    throw new Error (   strMethodName + ': The selectedIndex value is out of range. '
                                      + 'The expected value is between ' + ARRAY_INVALID_INDEX
                                      + ' and ' + event.currentTarget.options.length
                                      + ', EXCLUSIVE (That is, the value must fall BETWEEN the upper and lower bounds.) '
                                      + 'The actual value is ' + event.currentTarget.selectedIndex + '.' );
                }   // FALSE (unanticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length ))
            }
            catch ( ex )
            {
                console.log ( ex.message );
                LLCommon.LogException ( ex );
                alert ( 'An internal error arose in the Change event handler of the Wise Agent User selection pick list. Please contact support if this error persists.' , 'native' );
            }

            event.stopPropagation ( );
            event.currentTarget.disabled = true;
        }); // end of docWATeamSelector.addEventListener ( 'change' , ( event )

        //  --------------------------------------------------------------------
        //  If an election is recorded on this machine, create and activate the
        //  reset button and disable the pick list, which the reset button
        //  enables.
        //  --------------------------------------------------------------------

        if ( pintInitialChoice !== undefined || fLoginIsMatched )
        {
            if ( !document.getElementById ( 'ResetWATeamMembership' ) )
            {
                docWAChooser.appendChild ( CreateResetButton ( ) );
            }   // if ( !document.getElementById ( 'ResetWATeamMembership' ) )

            docWATeamSelector.disabled = ( docWATeamSelector.selectedIndex > ARRAY_FIRST_ELEMENT );

            if ( fLoginIsMatched || docWATeamSelector.selectedIndex > ARRAY_FIRST_ELEMENT )
            {
                ProcessSelection ( event , oLogin2Match );
            }   // if ( fLoginIsMatched || docWATeamSelector.selectedIndex > ARRAY_FIRST_ELEMENT )
        }   // if ( pintInitialChoice !== undefined || fLoginIsMatched )

        return docWATeamSelector;
    }   // function CreateTeamPickList


    function GetWATeamRoster ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      GetWATeamRoster
        //
        //  Function Goal:      Get the Wise Agent Team roster for the account.
        //
        //  Function Arguments: None. This function relies upon globally visible
        //                            attributes.
        //
        //  Return Value:       If the Wise Agent account has a Team, this
        //                      routine returns a frozen array of TeamMemberInfo
        //                      objects, each of which is also frozen.
        //
        //                      Otherwise, the return value is null.
        //
        //  Exceptions:         In the event that SalesTalk API GetWATeam
        //                      returns a string instead of an array, or parsing
        //                      its JSON representation raises an Exception, the
        //                      Exception is passed along.
        //
        //  Remarks:            This routine is protected by the try/catch block
        //                      of its caller.
        //  --------------------------------------------------------------------

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const aoTeamList        = JSON.parse ( LLCommon.DoAjax ( 'SalesTalkSalesforce/GetWATeam',
                                                                 'GET',
                                                                 {
                                                                    'pintTenantId' : _tenantid,
                                                                    'pintDomainId' : _domainid
                                                                 } ) );
        const raoTeamMembers    = [ ];

        if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
        {
            //  ------------------------------------------------------------
            //  Assemble a list of team members comprised solely of the four
            //  values that are relevant to the pick list and the saved data
            //  for the selected item.
            //  ------------------------------------------------------------

            for ( const oCurrentMember of aoTeamList )
            {
                try
                {
                    const oThisTeamMember = new TeamMemberInfo (
                        oCurrentMember.InsideTeamId,
                        oCurrentMember.Name,
                        oCurrentMember.Email,
                        oCurrentMember.JobTitle
                    );
                    raoTeamMembers.push ( oThisTeamMember );
                }
                catch ( ex )
                {
                    console.warn ( 'Skipping invalid entry:', ex.message );
                    LLCommon.LogException ( ex );
                }
            }   // for ( const oCurrentMember of aoTeamList )

            //  ------------------------------------------------------------
            //  Since the TeamMemberInfo constructor freezes its properties,
            //  freezing the entire array yields a frozen solid block of
            //  read only data, which the asynchronously executing CHANGE
            //  event listener attached to the SELECT element that is about
            //  to spring into existence uses to complete its task.
            //  ------------------------------------------------------------

            Object.freeze ( raoTeamMembers );
            return raoTeamMembers;
        }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
        else
        {
            if ( Array.isArray ( aoTeamList ) )
            {
                return null;
            }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoTeamList ) )
            else
            {
                throw new Error ( 'GetWATeam returned an Exception. Exception Message = ' + aoTeamList );
            }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoTeamList ) )
        }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
    }   // function GetWATeamRoster


    function CheckWATeamMembership ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      CheckWATeamMembership
        //
        //  Function Goal:      Evaluate the Wise Agent Team Roster against the
        //                      membership registration, if any, stored in the
        //                      localStorage of the executing host.
        //
        //  Function Arguments: None. This function relies upon globally visible
        //                            attributes.
        //
        //  Return Value:       Three scenarios exist, differentiated by their
        //                      return type.
        //
        //                      1)  If the user is already registered, the team
        //                          member's info retrieved from localStorage is
        //                          parsed and returned as a JavaScript object.
        //
        //                      2)  If the user is unregistered and there is a
        //                          team roster in the Wise Agent account, the
        //                          empty string is returned, indicating that a
        //                          decision awaits, which will be recorded by
        //                          the Change event listener of the SELECT
        //                          element that receives the roster.
        //
        //                      3)  If the user is unregistered and there is no
        //                          team roster in the Wise Agent account, there
        //                          is no registration requirement, which this
        //                          function signals by returning null.
        //
        //  Remarks:            This routine is protected by the try/catch block
        //                      of its caller.
        //
        //                      The array of team member information objects
        //                      stored in the _Words2Actions_Recorder_Forms_pvt
        //                      property TEAM_ROSTER_KEY is retrieved and
        //                      scanned by the Change event listener bound to
        //                      the SELECT element.
        //  --------------------------------------------------------------------

        const strMethodName             = LLCommon.GetNameOfCurrentFunction ( );

        debugger;
        console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberstatus = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberstatus ( ) );
        console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberType   = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberType   ( ) );
        console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberId     = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberId     ( ) );
        console.log ( strMethodName + ': _Words2Actions_Recorder_Forms_pvt.teamMemberEmail  = ' + _Words2Actions_Recorder_Forms_pvt.teamMemberEmail  ( ) );

        const aoTeamList            = GetWATeamRoster ( );

        if ( Array.isArray ( aoTeamList ) )
        {
            if ( aoTeamList.length > ARRAY_IS_EMPTY )
            {
                _Words2Actions_Recorder_Forms_pvt.set ( _Words2Actions_Recorder_Forms_pvt.TEAM_ROSTER_KEY ,
                                                        aoTeamList );

                if ( aoTeamList.length === ARRAY_NOT_EMPTY && aoTeamList [ ARRAY_FIRST_ELEMENT ].InsideTeamId === 1 )
                {
                    const strOutcome    = LLCommon.DoAjax ( 'SalesTalkSalesforce/AssociateWATeamIdWithStalkUserId',
                                                            'GET',
                                                            {
                                                                'pintStalkUserId'   : LLCommon.UserInfo.AgentUserId,
                                                                'pintDomainID'      : LLCommon.DomainId,
                                                                'pintInsideTeamId'  : aoTeamList [ ARRAY_FIRST_ELEMENT ].InsideTeamId
                                                            });

                    if ( strOutcome.length > EMPTY_STRING_LENGTH )
                    {
                        throw new Error ( strMethodName + ': Exception in AssociateWATeamIdWithStalkUserId method of SalesTalkSalesforceController, Message = ' + strOutcome );
                    }   // if ( strOutcome.length > EMPTY_STRING_LENGTH )
                }   // TRUE (The Wise Agent account consists of a team of 1, the nominal leader.) block, if ( aoTeamList.length === ARRAY_NOT_EMPTY && aoTeamList [ ARRAY_FIRST_ELEMENT ].InsideTeamId === 1 )
                else
                {
                    let   docWATeamSelector = null;
                    const strSavedTeamInfo  = localStorage.getItem ( _Words2Actions_Recorder_Forms_pvt.TEAM_INFO_KEY );

                    if ( strSavedTeamInfo !== null )
                    {
                        const objTeamInfo   = JSON.parse ( strSavedTeamInfo );
                        docWATeamSelector   = CreateTeamPickList ( aoTeamList ,
                                                                   objTeamInfo.hasOwnProperty ( 'InsideTeamId' )
                                              ? objTeamInfo.InsideTeamId
                                              : undefined );
                        console.log ( strMethodName + ': Done creating team membership pick list and its change event listener, returning registration detail.' );
                        return objTeamInfo;
                    }   // TRUE (anticipated post-registration outcome) block, if ( strSavedTeamInfo !== null )
                    else
                    {
                        docWATeamSelector   = CreateTeamPickList ( aoTeamList );

                        if ( docWATeamSelector.selectedIndex === SELECTED_INDEX_UNSELECTED )
                        {
                            let sLabelText       = 'Please identify yourself by selecting your email address from the list. Ordinarily, you will need to do this once only.';
                            let oLabelCSSStyle   = {
                                                       color           : "white",
                                                       fontSize        : "14pt",
                                                       fontWeight      : "bold",
                                                       backgroundColor : "rgba(0,0,0,0.6)",
                                                       padding         : "4px 8px",
                                                       borderRadius    : "4px"
                                                   };
                            window._PageBlockDiv = LLCommon.enforceSelectChoice ( docWATeamSelector,                            // poSelectElOrId
                                                                                  'docW2AEverythingElse',                       // poOverlayTargetElOrId,
                                                                                   sLabelText,                                  // psLabelText,
                                                                                   oLabelCSSStyle,                              // poLabelStyle
                                                                                   { backgroundColor: 'rgba(0, 0, 0, 0.75)' } );// poOverlayStyleOverrides inline override
                        }   // if ( docWATeamSelector.selectedIndex === SELECTED_INDEX_UNSELECTED )

                        docWATeamSelector.focus ( );
                        console.log ( strMethodName + ': Done creating team membership pick list and its change event listener and awaiting user selection.' );
                        return EMPTY_STRING;
                    }   // FALSE (anticipated initial outcome) block, if ( strSavedTeamInfo !== null )
                }   // FALSE (The Wise Agent account consists of a Team Leader and one or mor Teammates.) block, if ( aoTeamList.length === ARRAY_NOT_EMPTY && aoTeamList [ ARRAY_FIRST_ELEMENT ].InsideTeamId === 1 )
            }   // TRUE (anticipated outcome) block, if ( aoTeamList.length > ARRAY_IS_EMPTY )
            else
            {
                console.log ( 'GetWATeamRoster returned the empty set.' );
                return null;
            }   // FALSE (unanticipated outcome) block, if ( aoTeamList.length > ARRAY_IS_EMPTY )
        }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
        else
        {
            throw new Error ( 'GetWATeamRoster returned an Exception. Exception Message = ' + aoTeamList );
        }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
    }   // function CheckWATeamMembership


    function DisableCalling ( )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      DisableCalling
        //
        //  Function Goal:      Disable the dialer interface when Wise Agent is
        //                      the CRM and the user cannot be uniquely
        //                      identified.
        //
        //  Function Arguments: None. This function relies upon globally visible
        //                            attributes.
        //
        //  Return Value:       Void. This function is executed for its side
        //                            effects.
        //  --------------------------------------------------------------------

        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );
        debugger;

        const docDoCallRail     = document.getElementById ( 'DoCallRail' );
        if ( docDoCallRail )    { docDoCallRail.disabled = true; }
        const docShowCalls      = document.getElementById ( 'ShowMyRecentCalls' );
        if ( docShowCalls )     { docShowCalls.disabled = true; }

        alert ( 'Calling is unavailable without an absolute user identity because we must know your Axxess extension.' , 'native' );
    }   // function DisableCalling


    LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                        Words2Actions_Recorder_Forms_VERSION ,
                                        Words2Actions_Recorder_Forms_LastUpdated ,
                                        '- Entering anonymoun DOMContentLoaded event listener function defined in the current page.' ) );

    //  ------------------------------------------------------------------------
    //  Since this part of the form takes several seconds to populate, it is
    //  kept hidden until it is fully populated, a condition that cannot be
    //  properly evaluated until the end of this routine.
    //
    //  To be on the safe side, the element is marked as hidden by way of CSS
    //  selector STT_HideElement.
    //
    //  IMPORTANT:  The instruction that shows the DoCallRail element was moved
    //              to LLCommon.Prompt4Words2ActionsLogin so that execution can
    //              be deferred until the LLCommon.DialerLogin property has a
    //              usable value.
    //  ------------------------------------------------------------------------

    const strMethodName         = 'W2A_Forms_event_listener_load';
    debugger;

    LLCommon.RegisterClickEventHandler ( 'btnSummarizeText' , LLCommon.SummarizeTextDialog );

    mediaSelector               = document.getElementById ( 'media' );
    webCamContainer             = document.getElementById ( 'web-cam-container' );
    const evtChangeEvent        = new Event ( 'change' );

    // Handler function to handle the "change" event when the user selects some option:
    mediaSelector.addEventListener ( 'change' , ( event ) =>
    {
       // Takes the current value of the mediaSeletor:
       selectedMedia = event.target.value;

       LLCommon.ShowOrHideElement ( `${ selectedMedia }-recorder` ,
                                    LLCommon.ELEMENT_SHOW );
       LLCommon.ShowOrHideElement ( `${ OtherRecorderContainer ( selectedMedia ) }-recorder` ,
                                    LLCommon.ELEMENT_HIDE );

        event.stopPropagation ( );
    }); // mediaSelector.addEventListener ( 'change' , ( event )

    //  ------------------------------------------------------------------------
    //  Configure the LLCommon.ToastFactory before first use, then freeze it to
    //  prevent further accidental mutation.
    //  ------------------------------------------------------------------------

    LLCommon.ToastFactory.configure ( { fontFamily          : 'Arial, Helvetica, sans-serif',
                                        alternateScheme     : true,             // If two or more messages enter the queue, they display in alternating colors.
                                        positionX           : 'center',         // Center the toast horizontally.
                                        positionY           : 'middle',         // Center the toast vertically.
                                        duration            : 3000              // ms before auto-dismiss
                                      } );
    // Freeze the ToastFactory to prevent accidental mutation.
    Object.freeze ( LLCommon.ToastFactory );


    mediaSelector.dispatchEvent ( evtChangeEvent );

    //  ------------------------------------------------------------------------
    //  Map variables that are declared with const volatility and need window
    //  scope to like-named variables (properties) on the window object.
    //  ------------------------------------------------------------------------

    window.mediaSelector        = mediaSelector;
    window.webCamContainer      = webCamContainer;

    if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 11 && location.href.toLowerCase ( ).indexOf ( 'w2abutton=true' ) === INDEXOF_NOT_FOUND )
    {   // The Wise Agent Contact page needs a button to display the New Task input form.

        try
        {
            LLCommon.ShowOrHideElement ( 'CreateNewWiseAgentTask' , LLCommon.ELEMENT_SHOW );

            debugger;

            if ( ! ( GetParamValue ( 'FromAgentRecentCalls' , paramsCollection ) === 'true' ) )
            {
                const oOutcome = CheckWATeamMembership ( );

                if ( oOutcome == null )
                {
                    const strNoTeamMsg = 'There is NO team in the current Wise Agent account. Everything belongs to the Account Owner.';

                    console.log ( strNoTeamMsg  )
                    LLCommon.LogException ( strNoTeamMsg );
                }   // TRUE block, if ( oOutcome == null )
                else
                {
                    if ( LLCommon.IsString ( oOutcome ) )
                    {
                        const strTeamPickMsg = 'There is a team, but this machine has no registration record. Awaiting user selection.';

                        console.log ( strTeamPickMsg  )
                        LLCommon.LogException ( strTeamPickMsg );
                    }   // TRUE (anticipated initial outcome in multi-agent accounts) block, if ( LLCommon.IsString ( oOutcome ) )
                    else
                    {
                        if ( oOutcome.hasOwnProperty ( 'InsideTeamId' ) )
                        {
                            console.log ( 'Wise Agent Team Member registered to this machine: Team Member ID = ' + oOutcome.InsideTeamId + ', SalesTalk User ID = ' + oOutcome.StalkUserId + ', TelephonyOK flag = ' + oOutcome.TelephonyOK + ', Name = ' + oOutcome.Name + ', Email = ' + oOutcome.Email );
                        }   // TRUE (anticipated outcome) block, if ( oOutcome.hasOwnProperty ( 'InsideTeamId' ) )
                        else
                        {
                            throw new Error ( 'The value returned by function CheckWATeamMembership is of an unexpected type. Actual type = ' + ( typeof oOutcome ) + ', String representation = ' + oOutcome );
                        }   // FALSE (unanticipated outcome) block, if ( oOutcome.hasOwnProperty ( 'InsideTeamId' ) )
                    }   // FALSE (anticipated subsequent outcome in multi-agent accounts) block, if ( LLCommon.IsString ( oOutcome ) )
                }   // FALSE block, if ( oOutcome == null )
            }   // if ( GetParamValue ( 'FromAgentRecentCalls' , paramsCollection ) === 'true' )
        }
        catch ( ex )
        {
             LLCommon.LogException ( ex );
        }
    }   // if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 11 && location.href.toLowerCase ( ).indexOf ( 'W2AButton=true' ) === INDEXOF_NOT_FOUND )

    //  ------------------------------------------------------------------------
    //  The WA-Task entity has a pick list that is marked as external. This list
    //  must be populated before the dictionary of pick list values is loaded.
    //  ------------------------------------------------------------------------

    if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
    {
        try
        {
            debugger;

            const aoTeamList = JSON.parse ( LLCommon.DoAjax ( 'SalesTalkSalesforce/GetWATeamsList',
                                                              'GET',
                                                              {
                                                                 'DomainId'                           : _domainid ,
                                                                 'TenantId'                           : _tenantid ,
                                                                 'TeamsListCustomFieldSystemProperty' : 'WA_Task_InsideTeamId'
                                                              } ) );

            if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
            {   // The original code added the list only locally. Though the routine still returns the list, it is discarded.
                console.log ( 'Added Wise Agent Team members to the pick list. Count = ' + aoTeamList.length );
            }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )
            else
            {
                throw new Error ( 'GetWATeamsList returned a value of ZERO, indicating that the combination of DomainId = ' + _domainid + ' and SystemProperty = WA_Task_InsideTeamId returned an Exception. Exception Message = ' + aoTeamList );
            }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoTeamList ) && aoTeamList.length > ARRAY_IS_EMPTY )

            const strNamesAndValues = LLCommon.DoAjax ( 'GetVeryBasicLeadInfo4LeadId',
                                                        'GET',
                                                        {
                                                            'LeadId' : _leadid
                                                        } );

            //  ----------------------------------------------------------------
            //  The output of GetVeryBasicLeadInfo4LeadId is something like the
            //  following.
            //
            //      LeadId=1514256¬DomainId=1380¬DomainName=WiseAgent_Master¬ExternalCRMId=111276773¬SysCRMLeadOrContact=WA-Contact¬LastName=Appleseed¬FirstName=Johnny¬Email=johnny@appletrees4all.org¬MobilePhone=
            //
            //  The fields are, and the values are loaded into INPUT elements
            //  that have IDs that match the field names in the list.
            //  ----------------------------------------------------------------

            if ( strNamesAndValues.length > EMPTY_STRING_LENGTH && strNamesAndValues.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
            {
                const aNameAndValue = strNamesAndValues.split ( LOGICAL_NEGATE );

                for ( let intNameAndValue = ARRAY_FIRST_ELEMENT;
                          intNameAndValue < aNameAndValue.length;
                          intNameAndValue++ )
                {
                    let aNameValue  = LLCommon.StringSplitSharp ( aNameAndValue [ intNameAndValue ] , EQUALS_CHAR , SPLIT_NAME_FROM_VALUE );
                    let docInpByNm  = document.getElementById ( aNameValue [ SPLIT_NAME_PART ] );

                    if ( docInpByNm !== null && docInpByNm.nodeName === 'INPUT' && docInpByNm.type === 'text' )
                    {
                        docInpByNm.value = aNameValue [ SPLIT_VALUE_PART ];
                        console.log ( 'From GetVeryBasicLeadInfo4LeadId, ' + LLCommon.OrdinalFromIndex ( intNameAndValue ) + ' of ' + aNameAndValue.length + ': Field Name = ' + aNameValue [ SPLIT_NAME_PART ] + ', Field Value = ' + aNameValue [ SPLIT_VALUE_PART ] + ' MATCHED' );
                    }   // TRUE (anticipated outcome) block, if ( docInpByNm !== null && docInpByNm.nodeName === 'INPUT' && docInpByNm.type === 'text' )
                    else
                    {
                        console.log ( 'From GetVeryBasicLeadInfo4LeadId, ' + LLCommon.OrdinalFromIndex ( intNameAndValue ) + ' of ' + aNameAndValue.length + ': Field Name = ' + aNameValue [ SPLIT_NAME_PART ] + ', Field Value = ' + aNameValue [ SPLIT_VALUE_PART ] + ' UNMATCHED' );
                    }   // FALSE (unanticipated outcome) block, if ( docInpByNm !== null && docInpByNm.nodeName === 'INPUT' && docInpByNm.type === 'text' )
                }   // for ( let intNameAndValue = ARRAY_FIRST_ELEMENT; intNameAndValue < aNameAndValue.length; intNameAndValue++ )
            }   // TRUE (anticipated outcome) block, if ( strNamesAndValues.length > EMPTY_STRING_LENGTH && strNamesAndValues.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
            else
            {
                throw new Error ( 'SalesTalk _objAPI GetVeryBasicLeadInfo4LeadId reported the following unexpectd result: ' + strNamesAndValues );
            }   // FALSE (unanticipated outcome) block, if ( strNamesAndValues.length > EMPTY_STRING_LENGTH && strNamesAndValues.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )

            const strFieldResetResult = LLCommon.DoAjax ( 'UpdateAllFormFieldsByInternalName',
                                                          'POST',
                                                          {
                                                              'leadId'          : _leadid,
                                                              'FieldsAndValues' : 'WA_Task_TaskNote=' + SET_VALUE_TO_NULL + TAB_CHARACTER + 'WA_Task_InsideTeamId=' + SET_VALUE_TO_NULL + TAB_CHARACTER + 'WA_Task_EstimatedTime=' + SET_VALUE_TO_NULL + TAB_CHARACTER + 'WA_Task_Priority=' + SET_VALUE_TO_NULL + TAB_CHARACTER + 'WA_Task_TaskDue=' + SET_VALUE_TO_NULL,
                                                              'domainId'        : _domainid,
                                                              'tenantId'        : _tenantid,
                                                              'userId'          : _userid,
                                                              'tzOffsetMinutes' : ( new Date ( ) ).getTimezoneOffset ( ),
                                                              'UpdateModDate'   : false
                                                          } );

            if ( strFieldResetResult.length > EMPTY_STRING_LENGTH )
            {
                throw new Error ( 'In DOMContentLoaded event listener, _objAPI routine UpdateAllFormFieldsByInternalName returned the following error: ' + strFieldResetResult );
            }   // if ( strFieldResetResult.length > EMPTY_STRING_LENGTH )
        }
        catch ( ex )
        {
             LLCommon.LogException ( ex );
        }
    }   // if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )

    window._PickListValues      = GetValues4AllPickLists ( );
    window._InvalidPickListVals = EvaluatePickListValues ( );

    if ( _fPickListValidatorOff || Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') )
    {
        console.log ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': EvaluatePickListValues loop DISABLED by URL parameeter.' );
    }   // TRUE (The PickListValidatorOff URL parameter was set to TRUE.) block, if ( _fPickListValidatorOff )
    else
    {
        console.log ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': EvaluatePickListValues loop DISABLED by URL parameeter.' );
        window._IntervalHandle  = window.setInterval ( EvaluatePickListValues , 60000 ); // 60 seconds (1 minute)
    }   // FALSE (The PickListValidatorOff URL parameter was set to FALSE, or was omitted.) block, if ( _fPickListValidatorOff )

    //  ------------------------------------------------------------------------
    //  When provided, the first of these two URL query string parameters lists
    //  one or more fields (HTML page elements) to be marked as Required. The
    //  second supplies a return URl that causes the specified URL to open in a
    //  new window when the current window closes.
    //  ------------------------------------------------------------------------

    debugger;

    const _MarkAsRequired       = GetParamValue ( 'MarkAsRequired' , paramsCollection );
    const _MarkAsRequiredSource = __intValueSource;

    const _ReturnURL            = GetParamValue ( 'ReturnURL' , paramsCollection );
    const _ReturnURLSource      = __intValueSource;

    if ( _MarkAsRequiredSource !== SRC_IS_UNKNOWN && _MarkAsRequired.length > EMPTY_STRING_LENGTH )
    {
        MarkSelectedFieldsAsRequired ( _MarkAsRequired );
        ValidateAllFormFields ( );
    }   // if ( _MarkAsRequiredSource !== SRC_IS_UNKNOWN && _MarkAsRequired.length > EMPTY_STRING_LENGTH )

    //  ------------------------------------------------------------------------
    //  Register a small event listener for the Change event of the list of W2A
    //  transcripts.
    //
    //  The two events are identical apart from the string passed to ToggleDivs,
    //  which enables that routine to choose the correct SELECT element value to
    //  send to STTProcessMedia.
    //  ------------------------------------------------------------------------

    if ( document.getElementById ( PICK_LIST_TRANSCRIPTS ) !== null )
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Change event listener for SELECT element ID m4vurl' ) );

        document.getElementById ( PICK_LIST_TRANSCRIPTS ).addEventListener ( 'change' ,
                                                                             DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Change event listener added for SELECT element ID m4vurl' ) );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Blur event listener for SELECT element ID m4vurl' ) );

        document.getElementById ( PICK_LIST_TRANSCRIPTS ).addEventListener ( 'blur' ,
                                                                             DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Blur event listener added for SELECT element ID m4vurl' ) );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Click event listener for SELECT element ID m4vurl' ) );

        document.getElementById ( PICK_LIST_TRANSCRIPTS ).addEventListener ( 'click' ,
                                                                             DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Click event listener added for SELECT element ID m4vurl' ) );
    }   // TRUE (anticipated outcome) block, if ( document.getElementById ( PICK_LIST_TRANSCRIPTS ) !== null )
    else
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Element ID m4vurl ia ABSENT from the form.' ) );
    }   // FALSE (unanticipated outcome) block, if ( document.getElementById ( PICK_LIST_TRANSCRIPTS ) !== null )

    //  ------------------------------------------------------------------------
    //  Register a small event listener for the Change event of the list of Note
    //  transcripts. In point of fact, ALL notes are visible because there is no
    //  provision for differentiating dictated notes from typed notes.
    //
    //  The two events are identical apart from the string passed to ToggleDivs,
    //  which enables that routine to choose the correct SELECT element value to
    //  send to STTProcessMedia.
    //  ------------------------------------------------------------------------

    if ( document.getElementById ( PICK_LIST_NOTES ) !== null )
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Change event listener for SELECT element ID m4vNoteId.' ) );

        document.getElementById ( PICK_LIST_NOTES ).addEventListener ( 'change' ,
                                                                       DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Change event listener added for SELECT element ID m4vNoteId.' ) );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Blur event listener for SELECT element ID m4vNoteId.' ) );

        document.getElementById ( PICK_LIST_NOTES ).addEventListener ( 'blur' ,
                                                                       DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Change Blur listener added for SELECT element ID m4vNoteId.' ) );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Adding Click event listener for SELECT element ID m4vNoteId.' ) );

        document.getElementById ( PICK_LIST_NOTES ).addEventListener ( 'click' ,
                                                                       DisplayTranscriptOrNote );

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Click event listener added for SELECT element ID m4vNoteId.' ) );
    }   // TRUE (anticipated outcome) block, if ( document.getElementById ( PICK_LIST_NOTES ) !== null )
    else
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Element ID m4vNoteId ia ABSENT from the form.' ) );
    }   // FALSE (unanticipated outcome) block, if ( document.getElementById ( PICK_LIST_NOTES ) !== null )

    //  ------------------------------------------------------------------------
    //  Executable code of the DOMContentLoaded event listener resumes at this
    //  point. The two addEventListener calls above only register event listener
    //  routines that execute when the registered Change events arise.
    //  ------------------------------------------------------------------------

    switch ( GetTranscriptList ( ) )
    {
        case ARRAY_INVALID_INDEX:      // Since the error has already been logged, just tell the user about it.
            alert ( ERROR_MESSAGEE_INTERNAL , 'native' );
            break;
        case EMPTY_STRING:
        default:
            if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
            {
                LLCommon.ShowOrHideElement ( BTN_TRANSCRIPT_REVIEW ,
                                             LLCommon.ELEMENT_HIDE );
            }   // TRUE (The current entity is a Wise Agent Task.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
            else
            {
                LLCommon.ShowOrHideElement ( BTN_TRANSCRIPT_REVIEW ,
                                             LLCommon.ELEMENT_SHOW );
            }   // FALSE (Show the button for all other entities.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
            break;
    }   // switch ( GetTranscriptList ( ) )

    switch ( GetNotesList ( ) )
    {
        case ARRAY_INVALID_INDEX:      // Since the error has already been logged, just tell the user about it.
            alert ( ERROR_MESSAGEE_INTERNAL , 'native' );
            break;
        case EMPTY_STRING:
        default:
            if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
            {
                LLCommon.ShowOrHideElement ( BTN_NOTES_REVIEW ,
                                             LLCommon.ELEMENT_HIDE );
            }   // TRUE (The current entity is a Wise Agent Task.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
            else
            {
                LLCommon.ShowOrHideElement ( BTN_NOTES_REVIEW ,
                                             LLCommon.ELEMENT_SHOW );
            }   // FALSE (Show the button for all other entities.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
    }   // switch ( GetTranscriptList ( ) )

    if ( _pagenameSource !== undefined && _pagename !== null )
    {
        LLCommon.ShowOrHideElement ( 'ShowAnotherForm' ,
                                     LLCommon.ELEMENT_SHOW );
    }   // if ( _pagenameSource !== undefined && _pagename !== null )

    debugger;

    const fShowCheatSheet = LLCommon.EntityType !== null && LLCommon.EntityType.EntityName === 'WA-Task'
                            ? 'false'
                            : LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                'GET',
                                                {
                                                    'monikor'         : ELEMENT_ID_CHEAT_SHEET_GO + UNDERSCORE_CHAR + _userid,
                                                    'tenantId'        : _tenantid,
                                                    'domainId'        : _domainid,
                                                    'defaultValue'    : false.toString ( )
                                                } );

    if ( fShowCheatSheet === 'true' )
    {
        CheatSheet ( );
    }   // if ( fShowCheatSheet === 'true' )

    const fShowInstructionBox = LLCommon.EntityType !== null && LLCommon.EntityType.EntityName === 'WA-Task'
                            ? 'false'
                            : LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                'GET',
                                                {
                                                    'monikor'         : ELEMENT_ID_INSTRUCTIONBOX + UNDERSCORE_CHAR + _userid,
                                                    'tenantId'        : _tenantid,
                                                    'domainId'        : _domainid,
                                                    'defaultValue'    : false.toString ( )
                                                } );
    if ( fShowInstructionBox === 'true' )
    {
        LLCommon.ShowOrHideElement ( ELEMENT_ID_INSTRUCTIONBOX , fShowInstructionBox );
    }   // if ( fShowInstructionBox === 'true' )


    // 20:13:22.637 LLCommon.js:649 ./Scripts/LLCommon.js EndOfDocumentReady: LLCommon.EnabledCRM        = CrmName : BullHorn, Monikor : BullHornEnabled, SysCRMLeadOrContact : BH-, Prefix : BH
    // 2024/04/20 20:55:09 - DAGray - Comment out the Bullhorn interaction button.
    // 2024/06/09 00:02:26 - DAGray - Reinstate the CRM interaction button for use with Wise Agent.

    if ( _CRMInteractionButton )
    {   // Show these two elements, of which the first is a container: NewExternalCRMId, post1, CRM_Interaction, and ShowAnotherFormButtonHole
        LLCommon.ShowOrHideElement ( 'CRM_Interaction' ,
                                     LLCommon.ELEMENT_SHOW );
    }   // if ( _CRMInteractionButton )

    debugger;

    const fTopRowButtonCount = LLCommon.DisableOrEnableButtonsInsideElement ( 'W2AActions' , ! ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID ) );

    //  ------------------------------------------------------------------------
    //  IMPORTANT:  The instruction that shows the DoCallRail element was moved
    //              to LLCommon.Prompt4Words2ActionsLogin so that execution can
    //              be deferred until the LLCommon.DialerLogin property has a
    //              usable value.
    //  ------------------------------------------------------------------------

    AdjustButtonProperties ( BUTTON_STATE_INITIAL );

    //  ------------------------------------------------------------------------
    //  The next code block is a robust way to insert the text in the TITLE tag
    //  of the document into the body of an element. Since it does nothing
    //  unless the expected element exists, this routine may be left in a
    //  finished page.
    //  ------------------------------------------------------------------------

    const docTitlePlaeholder    = document.getElementById ( 'TitleContainer' );

    if ( docTitlePlaeholder !== null )
    {
        if ( ( _pagenameSource === undefined ) || ( _pagenameSource !== undefined && _pagename === null ) )
        {
            document.title      = _DEFAULT_PAGE_TITLE;
//            LLCommon.AddOrRemoveStyles ( ELEMENT_ID_W2A_FORM ,
//                                         CSS_ID_TALK2URCRM ,
//                                         LLCommon.CSS_SELECTOR_ADD );
        }   // TRUE (The pagename parameter is absent from the URL.) block, if ( ( _pagenameSource === undefined ) || ( _pagenameSource !== undefined && _pagename === null ) )
        else
        {
            document.title      = _pagename;

            if ( document.getElementById ( ELEMENT_ID_CUSTOM_FORM ).innerHTML.length > EMPTY_STRING_LENGTH )
            {
                const adocFlds  = $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( ELEMENT_ID_CUSTOM_FORM ) ).find ( ':input' );
                const intNFlds  = adocFlds.length;

                if ( intNFlds > ARRAY_IS_EMPTY )
                {
                    for ( var intCurrentField = ARRAY_FIRST_ELEMENT;
                              intCurrentField < intNFlds;
                              intCurrentField++ )
                    {
                        debugger;

                        var strCurrElementNodeName      = adocFlds [ intCurrentField ].nodeName;

                        //  ----------------------------------------------------
                        //  Button and Select elements that need events have
                        //  specialized requirements, often applicable to single
                        //  instances. Hence, they are skipped when handing out
                        //  these Focus and Blur event listeners.
                        //
                        //  NOTE:   This change probably obviates the need for
                        //          the list of exclusions. Nevertheless, I'll
                        //          leave the check against the list in place
                        //          for Blur events.
                        //  ----------------------------------------------------

                        if ( ( strCurrElementNodeName !== 'BUTTON' ) && ( strCurrElementNodeName !== 'SELECT' ) )
                        {
                            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Adding focus event handler for element ID = ' + adocFlds [ intCurrentField ].id );

                            if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && Words2Actions_Recorder_Forms_LogTraces )
                            {
                                _astrRegisteredForFocus.push ( adocFlds [ intCurrentField ].id );
                            }   // if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && Words2Actions_Recorder_Forms_LogTraces )

                            adocFlds [ intCurrentField ].addEventListener ( 'focus' , ( event ) =>
                            {  // Save the current value of the docPickList into the value property of the docCompanionTextBox element, then remove the control.
                                objInitialValue         = _LeadLifeJSHelpers.GetElementValue ( event.currentTarget );
                                LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': An ' + event.type + ' event arose against ' + event.currentTarget.nodeName + ' ID ' + event.currentTarget.id + ' initial value on gaining focus = ' + objInitialValue );
                                event.stopPropagation ( );
                            });

                            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Adding blur event handler for element ID = ' + adocFlds [ intCurrentField ].id );

                            if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && Words2Actions_Recorder_Forms_LogTraces )
                            {
                                _astrRegisteredForBlur.push ( adocFlds [ intCurrentField ].id );
                            }   // if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && Words2Actions_Recorder_Forms_LogTraces )

                            if ( _astrUISelectElements.indexOf ( adocFlds [ intCurrentField ].id.toLowerCase ( ) ) === INDEXOF_NOT_FOUND )
                            {
                                debugger;

                                adocFlds [ intCurrentField ].addEventListener ( 'blur' , ( event ) =>
                                {  // Save the current value of the docPickList into the value property of the docCompanionTextBox element, then remove the control.
                                    debugger;
                                    const strNewValue   = _LeadLifeJSHelpers.GetElementValue ( event.currentTarget );
                                    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': An ' + event.type + ' event arose against ' + event.currentTarget.nodeName + ' ID ' + event.currentTarget.id + ', current value on losing focus = ' + strNewValue + ', previous value on gaining focus = ' + objInitialValue );

                                    if ( strNewValue === objInitialValue )
                                    {
                                        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Field update SKIPPED because strNewValue === objInitialValue.' );
                                    }   // TRUE (The field value is unchanged.) block, if ( strNewValue === objInitialValue )
                                    else
                                    {
                                        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': APPLYING field update because strNewValue !== objInitialValue.' );
                                        UpdateIfChanged ( event );

                                        debugger;

                                        if ( event.currentTarget.classList.contains ( 'STT_Required' ) && event.currentTarget.classList.contains ( 'STT_Field_with_Error' ) )
                                        {
                                            if ( event.currentTarget.value.length > EMPTY_STRING_LENGTH )
                                            {
                                                event.currentTarget.classList.remove ( 'STT_Field_with_Error' );
                                                const adocElementsMarkedAsInvalid = document.querySelectorAll ( '.STT_Field_with_Error' );

                                                if ( adocElementsMarkedAsInvalid.length == 1 &&  adocElementsMarkedAsInvalid [ ARRAY_FIRST_ELEMENT ].id === 'UpdateCRMNow' )
                                                {
                                                    const docUpdateCRMNowButton   = document.getElementById ( 'UpdateCRMNow' );

                                                    if ( docUpdateCRMNowButton !== null )
                                                    {
                                                        if ( docUpdateCRMNowButton.classList.contains ( 'STT_Field_with_Error' ) )
                                                        {
                                                            docUpdateCRMNowButton.classList.remove ( 'STT_Field_with_Error' );
                                                            docUpdateCRMNowButton.disabled = false;
                                                        }   // if ( docUpdateCRMNowButton.classList.contains ( 'STT_Field_with_Error' ) )
                                                    }   // TRUE (anticipated outcome) block, if ( docUpdateCRMNowButton !== null )
                                                    else
                                                    {
                                                        LLCommon.LogException ( 'The "UpdateCRMNow" element is missing from the form.' );
                                                    }   // FALSE (unanticipated outcome) block, if ( docUpdateCRMNowButton !== null )
                                                }   // if ( adocElementsMarkedAsInvalid.length == 1 &&  adocElementsMarkedAsInvalid [ ARRAY_FIRST_ELEMENT ].id === 'UpdateCRMNow' )
                                            }   // if ( event.currentTarget.value.length > EMPTY_STRING_LENGTH )
                                        }   // if ( event.currentTarget.classList.contains ( 'STT_Required' ) && event.currentTarget.classList.contains ( 'STT_Field_with_Error' ) )

                                        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Field update APPLIED because strNewValue !== objInitialValue.' );
                                    }   // FALSE (The field value is changed.) block, if ( strNewValue === objInitialValue )

                                    objInitialValue     = null;
                                    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Initial value cleared.' );
                                    event.stopPropagation ( );
                                }); // end of adocFlds [ intCurrentField ].addEventListener ( 'blur' , ( event ) =>
                            }   // if ( _astrUISelectElements.indexOf ( adocFlds [ intCurrentField ].id.toLowerCase ( ) ) === INDEXOF_NOT_FOUND )
                        }   // TRUE (anticipated outcome) block, if ( ( strCurrElementNodeName !== 'BUTTON' ) && ( strCurrElementNodeName !== 'SELECT' ) )
                        else
                        {
                            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Skipping element ID = ' + adocFlds [ intCurrentField ].id + ' because it is a BUTTON.' );
                        }   // FALSE (unanticipated outcome) block, if ( ( strCurrElementNodeName !== 'BUTTON' ) && ( strCurrElementNodeName !== 'SELECT' ) )
                    }   // for ( var intCurrentField = ARRAY_FIRST_ELEMENT; intCurrentField < intNFlds; intCurrentField++ )

                    LLCommon.AddOrRemoveStyles ( ELEMENT_ID_W2A_FORM ,
                                                 CSS_ID_TALK2URCRM ,
                                                 LLCommon.CSS_SELECTOR_REMOVE );
                }   // TRUE (anticipated outcome) block, if ( intNFlds > ARRAY_IS_EMPTY )
                else
                {
                    console.log ( 'My View ' + _pagename + ' contains no input fields.' );
                }   // FALSE (unanticipated outcome) block, if ( intNFlds > ARRAY_IS_EMPTY )
            }   // TRUE (anticipated outcome) block, if ( document.getElementById ( ELEMENT_ID_CUSTOM_FORM ).innerHTML.length > EMPTY_STRING_LENGTH )
            else
            {
                console.log ( 'My View ' + _pagename + ' returned the empty string, substituting a nonbreaking space.' );
                document.getElementById ( ELEMENT_ID_CUSTOM_FORM ).innerHTML = HTML_NBSP;
            }   // FALSE (unanticipated outcome) block, if ( document.getElementById ( ELEMENT_ID_CUSTOM_FORM ).innerHTML.length > EMPTY_STRING_LENGTH )
        }   // FALSE (The pagename parameter is present in the URL.) block, if ( ( _pagenameSource === undefined ) || ( _pagenameSource !== undefined && _pagename === null ) )

        if ( docTitlePlaeholder !== null )
        {   // This statement is skipped unless the TitleContainer element exists.
            docTitlePlaeholder.innerHTML = ReplaceTokensInTitlePlaceholder ( docTitlePlaeholder );
        }   // if ( docTitlePlaeholder !== null )
    }   // if ( docTitlePlaeholder !== null )

    debugger;           // These are moved from the TRUE (anticipated outcome) block of if ( intNFlds > ARRAY_IS_EMPTY ), above.

    LLCommon.ShowOrHideElement ( 'UpdateCRMButtonContainer' ,
                                 ( ( document.getElementById ( 'ExternalCRMId' ) !== null && document.getElementById ( 'ExternalCRMId' ).value.length > EMPTY_STRING_LENGTH ) || fCreateNewCRMRecord ) && ( LLCommon.EnabledCRM !== null && LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.CrmName !== 'NoCRM' ) )
    LLCommon.ShowOrHideElement ( 'docSearchResultsGrid' ,
                                 LLCommon.ELEMENT_SHOW );
    LLCommon.ShowOrHideElement ( ELEMENT_ID_REVIEW_TOOLS ,
                                 LLCommon.ELEMENT_SHOW );
    LLCommon.ShowOrHideElement ( 'post2' ,
                                 LLCommon.ELEMENT_SHOW );

    LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                        Words2Actions_Recorder_Forms_VERSION ,
                                        Words2Actions_Recorder_Forms_LastUpdated ,
                                        ' - Leaving anonymoun DOMContentLoaded function defined in the current page.' ) );
});


LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                    Words2Actions_Recorder_Forms_VERSION ,
                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                    ' - DOMContentLoaded event listener defined in the current page added.' ) );


LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                    Words2Actions_Recorder_Forms_VERSION ,
                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                    ' - Adding Document.Load(ed) event listener.' ) );

window.addEventListener ( 'load', ( event ) =>
{
    const strMethodName         = 'W2A_Forms_event_listener_load';

    console.log (  Words2Actions_Recorder_Forms_SCRIPTSOURCE + ' said: The page is fully loaded.' );
    const docShowIDsHotSpot     = document.getElementById ( 'ShowIDsHotSpot' );

    debugger;

    try
    {
        const strUQScriptName = LLCommon.UQFileNameFromHrefOrPathName ( Words2Actions_Recorder_Forms_SCRIPTSOURCE );
        LLCommon.__dirtyHandlers.register ( ( ) =>
        {
            debugger;
            LLCommon.Trace ( `EnablING button having ID = ${BTN_UPDATE_CRM}` ,
                             strUQScriptName );
            LLCommon.inputEnable ( BTN_UPDATE_CRM );
            LLCommon.Trace ( `EnablED button having ID = ${BTN_UPDATE_CRM}` ,
                             strUQScriptName );
        }, strUQScriptName );

        LLCommon.__cleanHandlers.register ( ( ) =>
        {
            debugger;
            LLCommon.Trace ( `DisablING button having ID = ${BTN_UPDATE_CRM}` , strUQScriptName );
            LLCommon.inputDisable ( BTN_UPDATE_CRM );
            LLCommon.Trace ( `DisablED button having ID = ${BTN_UPDATE_CRM}` , strUQScriptName );
        }, strUQScriptName );

        if ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID )
        {   // LLCommon.EnabledCRM.SysCRMLeadOrContact has the correct SysCRMLeadOrContact value.
            LLCommon.ExternalCRMId  = document.getElementById ( 'ExternalCRMId' ).value
        }   // if ( _leadidSource !== SRC_IS_UNKNOWN && _leadid > NO_LEAD_ID )

        if ( LLCommon !== null && LLCommon.EnabledCRM !== null && LLCommon.EnabledCRM.SysCRMLeadOrContact !== null && LLCommon.EnabledCRM.SysCRMLeadOrContact === 'WA-' )
        {
            debugger;
            const CIFlagPerConfig = LLCommon.checkSystemConfigAndUserOverride ( 'WiseAgent External CRM CI',
                                                                                'false',
                                                                                false );
//          let CIFlagPerConfig = LLCommon.DoAjax ( 'GetByMonikorFirst',
//                                                  'GET',
//                                                  {
//                                                      'monikor'      : 'WiseAgent External CRM CI',
//                                                      'tenantId'     : LLCommon.TenantId,
//                                                      'domainId'     : LLCommon.DomainId,
//                                                      'defaultValue' : 'false'
//                                                  } );
//
//          if ( CIFlagPerConfig === 'false' )
//          {
//              CIFlagPerConfig = LLCommon.DoAjax ( 'GetByMonikorFirst',
//                                                  'GET',
//                                                  {
//                                                      'monikor'      : ( 'WiseAgent External CRM CI' + SPACE_CHARACTER + LLCommon.UserInfo.AgentLoginEmailId ),
//                                                      'tenantId'     : LLCommon.TenantId,
//                                                      'domainId'     : LLCommon.DomainId,
//                                                      'defaultValue' : CIFlagPerConfig
//                                                  } );
//          }   // if ( CIFlagPerConfig === 'false' )

            const fCIFlagShow = LLCommon.parseBool ( CIFlagPerConfig );
            LLCommon.ShowOrHideElement ( 'CIButtonHole' ,
                                         fCIFlagShow );
            Object.defineProperty ( window, 'CIButtonNoWarning',
            {
                value        : fCIFlagShow
                               ? LLCommon.parseBool ( LLCommon.checkSystemConfigAndUserOverride ( 'WiseAgent External CRM CI NoWarning', 'false', true ) )
                               : false,
                writable     : false,   // Make it read-only.
                configurable : false    // Make it not configurable.
            });
        }   // if ( LLCommon !== null && LLCommon.EnabledCRM !== null && LLCommon.EnabledCRM.SysCRMLeadOrContact !== null && LLCommon.EnabledCRM.SysCRMLeadOrContact === 'WA-' )

        if ( LLCommon.EntityType !== null && LLCommon.IsString ( LLCommon.EntityType.EntityDescription ) && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )
        {
            const dctEntityTypeDescription = new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription );
            const strW2AFormElementsToHide = dctEntityTypeDescription.GetValueAtKey ( 'W2A_Form_ElementsToHide' );

            if ( strW2AFormElementsToHide.length > EMPTY_STRING_LENGTH )
            {
                const astrIDsOfElementsToHide  = strW2AFormElementsToHide.split ( LOGICAL_NEGATE );
                console.log ( 'Count of form elements to hide = ' + astrIDsOfElementsToHide.length );

                for ( var intPosElementID2Hide = ARRAY_FIRST_ELEMENT;
                          intPosElementID2Hide < astrIDsOfElementsToHide.length;
                          intPosElementID2Hide++ )
                {
                    console.log ( 'Hiding element ID = ' + astrIDsOfElementsToHide [ intPosElementID2Hide ] );
                    LLCommon.ShowOrHideElement ( astrIDsOfElementsToHide [ intPosElementID2Hide ] ,
                                                 LLCommon.ELEMENT_HIDE );
                }   // for ( var intPosElementID2Hide = ARRAY_FIRST_ELEMENT; intPosElementID2Hide < astrIDsOfElementsToHide.length; intPosElementID2Hide++ )
            }   // TRUE (Some form elements are hidden for the current entity.) block, if ( strW2AFormElementsToHide.length > EMPTY_STRING_LENGTH )
            else
            {
                console.log ( 'The list of elements to hide is empty.' );
            }   // FALSE (This entity leaves all form elements in their naturally occurring state.) block, if ( strW2AFormElementsToHide.length > EMPTY_STRING_LENGTH )
        }   // TRUE (The entity type and its Description property are defined.) block, if ( LLCommon.EntityType !== null && LLCommon.IsString ( LLCommon.EntityType.EntityDescription ) && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )
        else
        {
            console.log ( 'Either the CRM Entity Type or its Description property is undefined.' );
        }   // FALSE (Either the CRM Entity Type is unspecified or its Description is undefined.) block, if ( LLCommon.EntityType !== null && LLCommon.IsString ( LLCommon.EntityType.EntityDescription ) && LLCommon.EntityType.EntityDescription.length > EMPTY_STRING_LENGTH )

        //  --------------------------------------------------------------------
        //  If the executing script is the development version, construct lists
        //  of the elements that are registered for Blur and Focus eveents and
        //  report them via the console log.
        //  --------------------------------------------------------------------

        if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js' ) && Words2Actions_Recorder_Forms_LogTraces )
        {
            var strMsg;
            var intJ            = ARRAY_INVALID_INDEX;
            var intNRegistered  = _astrRegisteredForBlur.length;

            if ( intNRegistered > ARRAY_IS_EMPTY )
            {
                strMsg          = 'Elements registered for Blur eveents: ';

                for ( intJ = ARRAY_FIRST_ELEMENT;
                      intJ < intNRegistered;
                      intJ++ )
                {
                    if ( intJ === ARRAY_FIRST_ELEMENT )
                    {
                        strMsg  += _astrRegisteredForBlur [ intJ ];
                    }
                    else
                    {
                        strMsg  += ( ', ' + _astrRegisteredForBlur [ intJ ] );
                    }
                }   // for ( intJ = ARRAY_FIRST_ELEMENT; intJ < intNRegistered; intJ++ )

                console.log ( strMsg );
            }   // if ( intNRegistered > ARRAY_IS_EMPTY )

            intNRegistered      = _astrRegisteredForFocus.length;

            if ( intNRegistered > ARRAY_IS_EMPTY )
            {
                strMsg          = 'Elements registered for Focus eveents: ';

                for ( intJ = ARRAY_FIRST_ELEMENT;
                      intJ < intNRegistered;
                      intJ++ )
                {
                    if ( intJ === ARRAY_FIRST_ELEMENT )
                    {
                        strMsg  += _astrRegisteredForFocus [ intJ ];
                    }
                    else
                    {
                        strMsg  += ( ', ' + _astrRegisteredForFocus [ intJ ] );
                    }
                }   // for ( intJ = ARRAY_FIRST_ELEMENT; intJ < intNRegistered; intJ++ )

                console.log ( strMsg );
            }   // if ( intNRegistered > ARRAY_IS_EMPTY )
        }   // if ( Words2Actions_Recorder_Forms_SCRIPTSOURCE.toLowerCase ( ).endsWith ( '_dev.js') && Words2Actions_Recorder_Forms_LogTraces )

        //  --------------------------------------------------------------------
        //  Since the factory method for calling alert ( ), defined and
        //  implemented in LLCommon.js defaults to BootBox, which is
        //  asynchronous, we must override by passing 'native' as a second
        //  argument to alert() to coerce the native alert function to be called
        //  in its place.
        //  --------------------------------------------------------------------

        debugger;

        if ( LLCommon.IsRoleAssigned ( 'NotesCreateOnly' ) )
        {
            for ( var intL = ARRAY_FIRST_ELEMENT;
                      intL < _astrHideForNotesOnly.length;
                      intL++)
            {
                LLCommon.ShowOrHideElement ( _astrHideForNotesOnly [ intL ] ,
                                             LLCommon.ELEMENT_HIDE );
            }   // for ( var intL = ARRAY_FIRST_ELEMENT; intL < _astrHideForNotesOnly.length; intL++)

            const adocTextBoxes = $ ( JQUERY_SELECTOR_IS_ELEMENT_ID + LLCommon.JQuerySelectorEscape ( 'STTCustomFormContainer' ) ).find ( ':input'  ).not ( '.STT_HideElement' );
            debugger;

            for ( var intM = ARRAY_FIRST_ELEMENT;
                      intM < adocTextBoxes.length;
                      intM++ )
            {
                switch ( adocTextBoxes [ intM ].nodeName )
                {
                    case 'INPUT':
                        //  document.getElementById('InputFieldID').readOnly = true;
                        adocTextBoxes [ intM ].readOnly = true;
                        break;
                    case 'BUTTON':
                        LLCommon.ShowOrHideElement ( adocTextBoxes [ intM ].id ,
                                                     LLCommon.ELEMENT_HIDE );
                        break;
                }   // switch ( adocTextBoxes [ intM ].nodeName )
            }   // for ( var intM = ARRAY_FIRST_ELEMENT; intM < adocTextBoxes.length; intM++ )

            LLCommon.ShowOrHideElement ( 'btnTranscriptReview' ,
                                         LLCommon.ELEMENT_HIDE );
            LLCommon.ShowOrHideElement ( 'm4vurl' ,
                                         LLCommon.ELEMENT_HIDE );
            LLCommon.ShowOrHideElement ( 'cheatsheet' ,
                                         LLCommon.ELEMENT_HIDE );
            document.getElementById ( ELEMENT_ID_INSTRUCTIONBOX ).innerHTML = '<p style="color: #ffffff; background-color: #5e92ba;">Click the green button labeled "Create Notes" located in the upper left corner of the page, then press the blue button labeled "Start recording Notes." that appears slightly below and to the right of these instructions.</p>';
            document.getElementById ( 'DoWords2Notes').focus ( );
        }   // TRUE (Hide everything except the functions required to create and search notes.) block, if ( LLCommon.IsRoleAssigned ( 'NotesCreateOnly' ) )
        else
        {
            LLCommon.PositionRelativeToOffsetParent ( docShowIDsHotSpot , -1 , -1 );
            const docSearchCRM  = document.getElementById ( 'SearchCRM' );

            if ( !LLCommon.EntityTypeIsSearchable )
            {   // If it can't be hidden, it can at the very least be disabled.
                docSearchCRM.disabled   = true;
                docSearchCRM.style.setProperty ( 'display' , 'none' , 'important' );
            }   // if ( !LLCommon.EntityTypeIsSearchable )

            LLCommon.RegisterClickEventHandler ( docSearchCRM ,
                                                 DisplayCRMSearchForm );

            if ( docShowIDsHotSpot !== null )
            {
                docShowIDsHotSpot.addEventListener ( 'click' , ( event ) =>
                {
                    debugger;

                    if ( _fIDBoxHidden )
                    {
                        LLCommon.ShowOrHideElement ( 'IDBox' ,
                                                     LLCommon.ELEMENT_SHOW );
                        _fIDBoxHidden   = false;
                    }   // TRUE (Initial conditions prevail.) block, if ( _fIDBoxHidden )
                    else
                    {
                        LLCommon.ShowOrHideElement ( 'IDBox' ,
                                                     LLCommon.ELEMENT_HIDE );
                        _fIDBoxHidden   = true;
                    }   // FALSE (Since the hot spot has been clicked, hide the box.), if ( _fIDBoxHidden )

                    event.stopPropagation ( );;
                }); // docShowIDsHotSpot.addEventListener ( 'click' , ( event ) =>
            }   // if ( docShowIDsHotSpot !== null )
        }   // FALSE (Leave all features enabled.) block, if ( LLCommon.IsRoleAssigned ( 'NotesCreateOnly' ) )

        const docHasInitialFocus = LLCommon.SafeGetFocusedElement ( );

        if ( docHasInitialFocus )
        {
            console.log ( 'Type and ID of element having focus: Type = ' + docHasInitialFocus.nodeName + ', ID = ' + docHasInitialFocus.id );
        }   // TRUE (An element has the focus.) block, if ( docHasInitialFocus )
        else
        {
            console.log ( 'NO element currently has the focus.' );
        }   // FALSE (NO element has the focus.) block, if ( docHasInitialFocus )
    }
    catch (e)
    {
        LLCommon.LogException ( ex );
    }
}); // window.addEventListener ( 'load', ( event ) =>

LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                    Words2Actions_Recorder_Forms_VERSION ,
                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                    ' - Document.Load(ed) event listener added.' ) );

const CheatSheet = ( ) =>
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    const docCheatSheet         = document.getElementById ( ELEMENT_ID_CHEAT_SHEET_GO );
    const fToggle               = ( ( docCheatSheet.className.indexOf ( LLCommon.STT_HideElement ) > INDEXOF_NOT_FOUND ) || ( docCheatSheet.className.length === EMPTY_STRING_LENGTH ) );

    if ( fToggle )
    {   // Since the element is hidden, populate the cheet sheet, then show it.
        let strKeyWords         = EMPTY_STRING;

        if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
        {
            strKeyWords         = LLCommon.DoAjax ( 'FindKeyWords' ,
                                                    'GET' ,
                                                    {
                                                        'LeadId' : _leadid,
                                                        'Option' : 'createtask'
                                                    } );
        }   // TRUE (The Wise Agent Task entity, WA-Task, needs special handling.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
        else
        {
            strKeyWords         = LLCommon.DoAjax ( 'FindKeyWords' ,
                                                    'GET' ,
                                                    {
                                                        'LeadId' : _leadid
                                                    } );
        }   // FALSE (Everything else works as expected given only a lead ID.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )

        if ( strKeyWords === undefined || strKeyWords.length < 1 )
        {
            alert ( 'No keywords are defined.' , 'native' );
        }   // TRUE (The unanticpated outcome is easier to evaluate.) block, if ( data === undefined || data.length < 1 )
        else
        {
            docCheatSheet.innerHTML     = EMPTY_STRING;
            const astrKeyWords          = strKeyWords.split ( LOGICAL_NEGATE );
            const intKeywordCount       = astrKeyWords.length;

            for ( var intIndex = ARRAY_FIRST_ELEMENT;
                      intIndex < intKeywordCount;
                      intIndex++ )
            {
                if ( !/^duration of |>duration of /.test ( astrKeyWords [ intIndex ] ) )
                {
                    if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 && location.pathname.toLowerCase ( ).indexOf ( '/staging/' ) === INDEXOF_NOT_FOUND )
                    {
                        docCheatSheet.innerHTML += ( astrKeyWords [ intIndex ].substring ( SUBSTRING_FIRST_CHAR , astrKeyWords [ intIndex ].indexOf ( '</span>' ) + 8 ) + '<br />' );
                    }   // TRUE (The entity is a Wise Agent TASK and the URL is PRODUCTION.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 && location.pathname.toLowerCase ( ).indexOf ( '/staging/' ) === INDEXOF_NOT_FOUND )
                    else
                    {
                        docCheatSheet.innerHTML += ( astrKeyWords [ intIndex ] + '<br />' );
                    }   // FALSE (Everything else displays the values, including Wise Agent TASK when the URL is STAGING.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 && location.pathname.toLowerCase ( ).indexOf ( '/staging/' ) === INDEXOF_NOT_FOUND )
                }   // if ( !/^duration of |>duration of /.test ( item ) )
            }   // for ( var intIndex = ARRAY_FIRST_ELEMENT; intIndex < intKeywordCount; intIndex++ )
        }   // FALSE (The anticipated outcome is the FALSE block because of the way the test is implementeed.) block, if ( data === undefined || data.length < 1 )
    }   // TRUE (anticipated initial outcome and alternating subsequent outcome) block, if ( fToggle )

    LLCommon.DoAjax ( 'PutMonikorInDatabase' ,
                      'GET' ,
                      {
                          'Monikor'  : ELEMENT_ID_CHEAT_SHEET_GO + UNDERSCORE_CHAR + _userid,
                          'NewValue' : fToggle.toString ( ),
                          'DomainId' : _domainid,
                          'TenantId' : _tenantid
                      } );

    LLCommon.ShowOrHideElement ( docCheatSheet ,
                                 fToggle );
}   // const CheatSheet = ( ) => {


function CreateNewWiseAgentTask ( )
{
    //  ------------------------------------------------------------------------
    //  Transform the URL that rendered the page from which a click event called
    //  this routine into the URL that displays the Create Task form, then feed
    //  the new URL to a new Web browser window.
    //
    //  https://salestalktech.com/SalesAcceleration/COMMON/STAGING/Words2Actions_Form_TEMPLATE.HTML?pagename=WiseAgentPage&CI=True&login=Richard@SalesTalk.ai&leadid=1514256&CRM=WiseAgent&EntityType=Contact
    //  ------------------------------------------------------------------------

    const strMethodName    = LLCommon.GetNameOfCurrentFunction ( );

    const rxpPageNameToken = /pagename=.*?&/i;
    const rxpEntityType    = /&EntityType=Contact/i;
    const strNewHREF       = location.href.replace ( rxpPageNameToken , 'pagename=WiseAgentTask&' ).replace ( rxpEntityType , 'EntityType=WA-Task' );

    console.log ( strMethodName + 'strNewHREF = ' + strNewHREF );

    window.open ( strNewHREF , 'WA_Task_InputForm' );
}   // function CreateNewWiseAgentTask


function DialNumber ( )
{
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    try
    {
        LLCommon.ManageCallButton ( _leadid );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function DialNumber


function DisplayCRMSearchForm ( )
{
    function CreateCriterionFormTop ( pfReInitilize )
    {
        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        const docSearchTermsBody            = document.getElementById ( 'docSearchTermsBody' );

        if ( pfReInitilize )
        {
            docSearchTermsBody.innerHTML    = EMPTY_STRING;

            const docCRMSearchResults       = document.getElementById ( 'docCRMSearchResultsRows' );
            docCRMSearchResults.innerHTML   = EMPTY_STRING;
            ShowOrHideElement ( docCRMSearchResults.parentElement ,
                                LLCommon.ELEMENT_SHOW );
        }   // if ( pfReInitilize )

        return docSearchTermsBody;
    }   // function CreateCriterionFormTop


    function CreateCriterionLabel ( pstrLabel , pdocSrchLabelCell , pstrSrchCriterionId )
    {
        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const docSrchLabel      = document.createElement ( 'label' );

        docSrchLabel.htmlFor    = pstrSrchCriterionId;
        docSrchLabel.innerHTML  = pstrLabel;
        docSrchLabel.classList.add ( 'input_group_W2A' );
        pdocSrchLabelCell.appendChild ( docSrchLabel );
    }   // function CreateCriterionLabel


    const CreateSearchCriteriaForm = function ( poSearchCriteria , pfReInitilize )
    {
        const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

        const docSearchTermsBody    = CreateCriterionFormTop ( pfReInitilize );

        if ( docSearchTermsBody === null )
        {   // Since this routine is always called from a routine that has a catch block, this unlikely exception is handled.
            throw new Error ( strMethodName + ': Function CreateCriterionFormTop returned a null reference.' );
        }   // if ( docSearchTermsBody === null )

        const intLastRowIndex       = LLCommon.IndexFromOrdinal ( poSearchCriteria.SearchParams.ao_Query_Criteria.length );

        if ( intLastRowIndex > ARRAY_INVALID_INDEX )
        {
            for ( var intCriterionIndex = ARRAY_FIRST_ELEMENT;
                      intCriterionIndex < poSearchCriteria.SearchParams.ao_Query_Criteria.length;
                      intCriterionIndex++ )
            {
                var intOrdinal = LLCommon.OrdinalFromIndex ( intCriterionIndex );

                var strSrchTermRowId    = 'docCRMSearchTerm'  + intOrdinal;
                var strSrchLabelCellId  = 'docCRMSearchLabel' + intOrdinal;
                var strSrchValueCellId  = 'docSearchTerm'     + intOrdinal;
                var strSrchCriterionId  = 'docCriterion'      + intOrdinal;

                var docCriterionRow     = document.createElement ( 'tr' );

                var docSrchLabelCell    = document.createElement ( 'td' );
                docSrchLabelCell.id     = strSrchLabelCellId;
                docSrchLabelCell.style  = 'width: 100px; max-width: 100px;';
                LLCommon.AddOrRemoveStyles ( docSrchLabelCell,
                                             'input_group_W2A',
                                             LLCommon.CSS_SELECTOR_ADD );
                CreateCriterionLabel ( poSearchCriteria.SearchParams.ao_Query_Criteria [ intCriterionIndex ].Label,
                                       docSrchLabelCell ,
                                       strSrchCriterionId );

                var docSrchValueCellN   = document.createElement ( 'td' );
                docSrchValueCellN.id    = strSrchValueCellId;
                docSrchValueCellN.style = 'width: 425px; max-width: 425px;';
                LLCommon.AddOrRemoveStyles ( docSrchValueCellN,
                                             'input_group_W2A',
                                             LLCommon.CSS_SELECTOR_ADD );

                debugger;

                LLCommon.RegisterReturnKeyWatchdog ( docSrchValueCellN ,
                                                     DoCRMSearch ,
                                                     poSearchCriteria );

                var docSrchInputN       = document.createElement ( 'input' );
                docSrchInputN.id        = strSrchCriterionId;
                docSrchInputN.name      = strSrchCriterionId;
                docSrchInputN.type      = poSearchCriteria.SearchParams.ao_Query_Criteria [ intCriterionIndex ].Type;
                docSrchInputN.minLength = 5;
                docSrchInputN.maxLength = 128;
                LLCommon.AddOrRemoveStyles ( docSrchInputN,
                                             'input_group_W2A',
                                             LLCommon.CSS_SELECTOR_ADD );
                docSrchValueCellN.appendChild ( docSrchInputN );

                var docCRMActions       = document.createElement ( 'td' );
                docCRMActions.innerHTML = intCriterionIndex === intLastRowIndex
                                          ? '<div style="text-align: center;"><button type="button" id="DoCloseSearchCriteria" name="DoCloseSearchCriteria" onclick="DoCloseCRMSearch ( );" title="Click to close the search criteria form." class="W2A_Recorder_Button_Box"><span class="W2A_Recorder_Button_Text">Close Search</span></div>'
                                          : intCriterionIndex === ARRAY_FIRST_ELEMENT
                                            ? '<button type="button" id="DoCRMSearchNow" name="DoCRMSearchNow" onclick="DoCRMSearch ( );" title="Click to search for records in your CRM." class="W2A_Recorder_Button_Box"><i class="fa fa-search"></i><span class="W2A_Recorder_Button_Text">&nbsp;Search Now</span></button>'
                                            : '&nbsp;';

                docCriterionRow.appendChild ( docSrchLabelCell );
                docCriterionRow.appendChild ( docSrchValueCellN );
                docCriterionRow.appendChild ( docCRMActions );

                docSearchTermsBody.appendChild ( docCriterionRow );
            }   // for ( var intCriterionIndex = ARRAY_FIRST_ELEMENT; intCriterionIndex < poSearchCriteria.SearchParams.ao_Query_Criteria.length; intCriterionIndex++ )

            LLCommon.ShowOrHideElement ( docSearchTermsBody ,
                                         LLCommon.ELEMENT_SHOW );
            LLCommon.ShowOrHideElement ( 'docSearchTermsGrid' ,
                                         LLCommon.ELEMENT_SHOW );

            document.getElementById ( 'docCriterion1' ).focus ( );
        }   // TRUE (for most entities) block, if ( intLastRowIndex > ARRAY_INVALID_INDEX )
        else
        {
            switch ( poSearchCriteria.EntityId )
            {
                case 10:    // EntityName = WA-PropertySearchCriteria
                    DoCRMSearch ( poSearchCriteria );
                    break;
                default:
                    throw new Error ( strMethodName + ': CRM Entity ID ' + poSearchCriteria.EntityId + ',' + poSearchCriteria.EntityName + ' REQUIRES defined search criteria.' );
            }   // switch ( poSearchCriteria.EntityId )
        }   // FALSE (for certain entitis, limited, at least for now, to WA-PropertySearchCriteria) block, if ( intLastRowIndex > ARRAY_INVALID_INDEX )
    }   // const CreateSearchCriteriaForm = function ( poSearchCriteria , pfReInitilize )


    const FriendlyName = function ( pstrFriendlyNameCandidiate )
    {
        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        const fCRMIdIsPrefix    = ( pstrFriendlyNameCandidiate.substring ( SUBSTRING_FIRST_CHAR , LLCommon.EnabledCRM.SysCRMLeadOrContact.length ) === LLCommon.EnabledCRM.SysCRMLeadOrContact );

        switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
        {
            case 'BH-':
                const strEntityNameSansCRMIdLC = ( fCRMIdIsPrefix ? pstrFriendlyNameCandidiate.substring ( LLCommon.EnabledCRM.SysCRMLeadOrContact ) : pstrFriendlyNameCandidiate ).toLowerCase ( );
                return strEntityNameSansCRMIdLC.indexOf ( 'candidate' ) > INDEXOF_NOT_FOUND
                       ? 'Candidate'
                        : strEntityNameSansCRMIdLC.indexOf ( 'contact' ) > INDEXOF_NOT_FOUND
                          ? 'Contact'
                          : strEntityNameSansCRMIdLC.indexOf ( 'lead' ) > INDEXOF_NOT_FOUND
                            ? 'Lead'
                            : 'Candidate';
            case 'WA-':
                return fCRMIdIsPrefix ? pstrFriendlyNameCandidiate.substring ( LLCommon.EnabledCRM.SysCRMLeadOrContact.length ) : pstrFriendlyNameCandidiate;
            default:
                throw new Error (   strMethodName
                                  + ': Function called with pstrFriendlyNameCandidiate = '
                                  + pstrFriendlyNameCandidiate
                                  + ' and LLCommon.EnabledCRM.SysCRMLeadOrContact = '
                                  + LLCommon.EnabledCRM.SysCRMLeadOrContact
                                  + ', an unsupported CRM.' );
        }   // switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
    }   // const FriendlyName = function ( pstrFriendlyNameCandidiate )


    const GetEntitySelectedIndexForPage = function ( paoParameters , pdocEntityPickList )
    {
        const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

        var   rintJ                         = ARRAY_INVALID_INDEX

        if ( _EntityType !== null )
        {
            var   fMore                     = true;

            while ( fMore )
            {
                rintJ++;

                if ( rintJ < pdocEntityPickList.length )
                {
                    var strOptionEntityName = FriendlyName ( pdocEntityPickList.options [ rintJ ].text );

                    if ( strOptionEntityName === _EntityType )
                    {
                        fMore               = false;
                    }   // TRUE (Found a match) block, if ( strOptionEntityName === _EntityType )
                }   // TRUE (The entity name corresponds to the page on which the form is displayed.) block, if ( intJ < pdocEntityPickList.length )
                else
                {
                    rintJ                   = ARRAY_FIRST_ELEMENT;
                    fMore                   = false;
                }   // FALSE (No matching entity is specified in the U if ( intJ < pdocEntityPickList.length )RL.) block,
            }   // while ( fMore )
        }   // TRUE (An entity type was specified in the URL.) block, if ( _EntityType !== null )
        else
        {
            rintJ                           = ARRAY_FIRST_ELEMENT;
        }   // FALSE (The entity type was omitted from the URL.) block, if ( _EntityType !== null )

        pdocEntityPickList.selectedIndex    = [ rintJ ];

        return paoParameters [ rintJ ];
    }   // const GetEntitySelectedIndexForPage = function ( paoParameters , pdocEntityPickList )


    const GetSearchParameters2Use = function ( )
    {
        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        if ( LLCommon.EnabledCRM !== null )
        {
            const aoSrchParams  = LLCommon.DoAjax ( 'GetSearchParametersForAllCRMEntities',
                                                    'GET',
                                                    {
                                                        'CRMId' : LLCommon.EnabledCRM.SysCRMLeadOrContact
                                                    } );

            if ( aoSrchParams !== null )
            {
                console.log ( 'Return value of call to GetSearchParametersForAllCRMEntities for CRM ID = ' + LLCommon.EnabledCRM.SysCRMLeadOrContact + ', aoSrchParams, parsed into ' + aoSrchParams.length + ' JavaScript object aoSrchParams.' );

                if ( aoSrchParams.length > ARRAY_IS_EMPTY )
                {
                    var aoParameters = [ ];

                    //  --------------------------------------------------------
                    //  Though it seems a tad inefficient, coding two FOR loops
                    //  simplifies the logic a bit and makes it easier to grasp
                    //  because the second round deals in objects that have
                    //  named properties.
                    //  --------------------------------------------------------

                    for ( var intJ = ARRAY_FIRST_ELEMENT;
                              intJ < aoSrchParams.length;
                              intJ++ )
                    {
                        aoParameters.push ( JSON.parse ( aoSrchParams [ intJ ] ) );
                    }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < aoSrchParams.length; intJ++ )

                    const docEntityPickList = document.getElementById ( 'CRMSearchableEntities' );

                    if ( docEntityPickList !== null && docEntityPickList.nodeName === 'SELECT' && docEntityPickList.type === 'select-one' )
                    {
                        if ( docEntityPickList.innerHTML.length > EMPTY_STRING_LENGTH )
                        {   // Before populating the list, discard the one created by the last trip through this block.
                            docEntityPickList.innerHTML = EMPTY_STRING;
                        }   // if ( docEntityPickList.innerHTML.length > EMPTY_STRING_LENGTH )

                        for ( var intK = ARRAY_FIRST_ELEMENT;
                                  intK < aoParameters.length;
                                  intK++ )
                        {
                            var docChoice       = document.createElement ( 'option' );

                            docChoice.value     = JSON.stringify ( aoParameters [ intK ] );
                            docChoice.innerHTML = FriendlyName ( aoParameters [ intK ].EntityName );

                            docEntityPickList.appendChild ( docChoice );
                        }   // for ( var intK = ARRAY_FIRST_ELEMENT; intK < aoParameters.length; intK++; )

                        LLCommon.ShowOrHideElement ( docEntityPickList ,
                                                     LLCommon.ELEMENT_SHOW );
                        LLCommon.ShowOrHideElement ( 'Label4CRMSearchableEntities' ,
                                                     LLCommon.ELEMENT_SHOW );

                        docEntityPickList.addEventListener ( 'change' , ( event ) =>
                        {
                            debugger;
                            event.stopPropagation ( );
                            CreateSearchCriteriaForm ( JSON.parse ( event.currentTarget.value ) , true );
                        }); // end of docDetailRow click event listener

                        return GetEntitySelectedIndexForPage ( aoParameters , docEntityPickList );
                    }   // TRUE (anticipated outcome) block, if ( docEntityPickList !== null && docEntityPickList.nodeName === 'SELECT' && docEntityPickList.type === 'select-one' )
                    else
                    {
                        throw new Error ( strMethodName + ': ' + ( docEntityPickList === null ) ? 'Required HTML element "CRMSearchableEntities" is undefined.' : 'Although required HTML element "CRMSearchableEntities" is defined, it must be a SELECT element of type select-one. Instead, its nodeName is ' + docEntityPickList.nodeName + ' and its type is ' + docEntityPickList.type + '.' );
                    }   // FALSE (unanticipated outcome) block, if ( docEntityPickList !== null && docEntityPickList.nodeName === 'SELECT' && docEntityPickList.type === 'select-one' )
                }   // TRUE (anticipated outcome) block, if ( aoSrchParams.length > ARRAY_IS_EMPTY )
                else
                {
                    if ( LLCommon.EntityType !== null && LLCommon.EntityType.SearchParameters.SearchParameters !== null )
                    {
                        return LLCommon.EntityType.SearchParameters;
                    }   // TRUE (The preassigned entity, presumably the default entity for the active CRM, supports searching.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.SearchParameters.SearchParameters !== null )
                    else
                    {
                        return null;
                    }   // FALSE (Either there is no preassigned entity, or the default entity doesn't support searching.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.SearchParameters.SearchParameters !== null )
                }   // FALSE (unanticipated outcome) block, if ( aoSrchParams.length > ARRAY_IS_EMPTY )
            }   // TRUE (anticipated outcome) block, if ( aoSrchParams !== null )
            else
            {
                return null;
            }   // FALSE (unanticipated outcome) block, if ( aoSrchParams !== null )
        }   // TRUE (The current form has a CRM associated with it.) block, if ( LLCommon.EnabledCRM !== null )
        else
        {
            return null;
        }   // FALSE (The current form is freestanding.) block, if ( LLCommon.EnabledCRM !== null )
    }   // const GetSearchParameters2Use = function ( )

    //  ------------------------------------------------------------------------
    //  The body of function DisplayCRMSearchForm starts here, so that all
    //  functions called herein are defined. Its catch block guards the code in
    //  the foregoing functions.
    //  ------------------------------------------------------------------------

    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    try
    {
        const docSearchTermsBody        = CreateCriterionFormTop ( false );

        if ( docSearchTermsBody === null )
        {
            throw new Error ( strMethodName + ': Function CreateCriterionFormTop returned a null reference.' );
        }   // if ( docSearchTermsBody === null )

        const oActiveSearchParameters   = GetSearchParameters2Use ( );

        if ( oActiveSearchParameters === null )
        {
            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                Words2Actions_Recorder_Forms_VERSION ,
                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                  ' onClick function '   + strMethodName
                                                + ', Since there is no associated CRM to search, the CRM seearch feature is DISABLED.' ) );
            return;
        }   // if ( oActiveSearchParameters === null )

        if ( docSearchTermsBody === null )
        {
            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                Words2Actions_Recorder_Forms_VERSION ,
                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                  ' onClick function '   + strMethodName
                                                + ', Document element docSearchTermsBody cannot be found.' ) );
            return;
        }   // if ( docSearchTermsBody === null )

        if ( ( LLCommon.EnabledCRM !== undefined ) && ( LLCommon.EnabledCRM  !== null ) && ( LLCommon.EnabledCRM.CrmName === 'WiseAgent' ) )
        {
            document.getElementById ( 'CRMSearchCriteriaCaption' ).innerText = 'Enter ONE search criterion.';
        }   // if ( ( LLCommon.EnabledCRM !== undefined ) && ( LLCommon.EnabledCRM  !== null ) && ( LLCommon.EnabledCRM.CrmName === 'WiseAgent' ) )

        //  --------------------------------------------------------------------
        //  The code in private function CreateSearchCriteriaForm was originally
        //  coded inline. It became a function that receives a SearchParams
        //  object so that the handler of the change event of a SELECT element
        //  can call it with a new SearchParams object.
        //  --------------------------------------------------------------------

        CreateSearchCriteriaForm ( oActiveSearchParameters , false );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function DisplayCRMSearchForm


function DisplayNewForm ( event )
{   // Since this function implements an event listener, it accepts a single argument, an event object, and returns void.
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    debugger;
    const strOldHref            = location.href;
    const strOldPageParam       = 'pagename=' + _pagename;
    const strNewPageParam       = 'pagename=' + event.innerText;
    const strNewHref            = strOldHref.replace ( strOldPageParam ,
                                                       strNewPageParam );
    LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                        Words2Actions_Recorder_Forms_VERSION ,
                                        Words2Actions_Recorder_Forms_LastUpdated ,
                                          ' onClick function '   + strMethodName
                                        + ', original URL = '    + strOldHref
                                        + ', _pagename = '       + _pagename
                                        + ', strOldPageParam = ' + strOldPageParam
                                        + ', strNewPageParam = ' + strNewPageParam
                                        + ', strNewHref = '      + strNewHref ) );
    window.location.replace ( strNewHref );
    event.stopPropagation( );;
}   // function DisplayNewForm


function DisplayTranscriptOrNote ( event )
{   // Since this function implements an event listener, it accepts a single argument, an event object, and returns void.
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    if ( event.currentTarget.value.length > EMPTY_STRING_LENGTH )
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Calling STTProcessMedia with ' + event.currentTarget.id + ' value = ' + event.currentTarget.value ) );

        switch ( event.currentTarget.id )
        {
            case PICK_LIST_TRANSCRIPTS:
                ToggleDivs ( ACTION_TRANS_REVIEW );
                break;
            case PICK_LIST_NOTES:
                ToggleDivs ( ACTION_NOTE_REVIEW );
                break;
        }   // switch ( event.currentTarget.id )

        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Returned from calling STTProcessMedia with ' + event.currentTarget.id + ' value = ' + event.currentTarget.value ) );
    }   // TRUE (anticipated outcome) block, if ( event.currentTarget.value.length > EMPTY_STRING_LENGTH )
    else
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'SKIPPED calling STTProcessMedia with ' + event.currentTarget.id + ' value = THE EMPTY STRING' ) );
    }   // FALSE (unanticipated outcome) block, if ( event.currentTarget.value.length > EMPTY_STRING_LENGTH )

    event.stopPropagation ( );
}   // function DisplayTranscriptOrNote


function DoCRMSearch ( poSearchCriteria )
{
    //  ------------------------------------------------------------------------
    //  Function Name:          DoCRMSearch
    //
    //  Function Goal:          When called, this funcion creates a CRM search
    //                          and passes it to the SalesTalk application
    //                          server, which, in turn, passes it along to the
    //                          configured CRM, then displayes the returned list
    //                          of records. To facilitate lookup, their IDs are
    //                          stored in a hidden column of the display table.
    //
    //  Arguments:              poSearchCriteria     = JavaScript object that
    //                                                 defines search criteria
    //                                                 and, to some degree, the
    //                                                 construction of the _objAPI
    //                                                 request
    //
    //  Remarks:                Since this function relies upon several private
    //                          functions, which must be defined before they are
    //                          mentioned in a script statement, the body of the
    //                          function begins below the private functions.
    //  ------------------------------------------------------------------------


    const BuildQueryStringBH = function ( pstrSrchCriterionVal , pstrParameters , pintCriterionIndex , poSearchCriteria )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      BuildQueryStringBH
        //
        //  Function Goal:      Assemble a query string parameter for a Bullhorn
        //                      database query.
        //
        //  Arguments:          pstrSrchCriterionVal = Criterion value to append
        //
        //                      pstrParameters       = The built-up parameter
        //                                             list, to which parameter
        //                                             pstrSrchCriterionVal is
        //                                             appended
        //
        //                      pintCriterionIndex   = Zero-based array index,
        //                                             which identifies a field
        //                                             name to label parameter
        //                                             pstrSrchCriterionVal
        //
        //                      poSearchCriteria     = Search criteria object
        //
        //  Returns:            The return value is the new string created by
        //                      appending criterion value pstrSrchCriterionVal
        //                      to input string pstrParameters.
        //
        //  Remarks:            Since all three argument values are generated by
        //                      our code, they are taken at face value.
        //
        //                      Though it is theoretically possble to implemnt
        //                      this function as a single statement that uses
        //                      two nested function calls, the V8 engine appears
        //                      to be mishandling it, so this function needs
        //                      three statements that build up the final answer.
        //  --------------------------------------------------------------------

        const strMethodName              = LLCommon.GetNameOfCurrentFunction ( );

        const  strEmbeddedQuotesStripped = pstrSrchCriterionVal.replace ( /"/g , EMPTY_STRING );
        const  strEncodedParamValue      = encodeURIComponent ( strEmbeddedQuotesStripped );

        return pstrParameters + ( ( pstrParameters.length === EMPTY_STRING_LENGTH ) ? 'query=' : ' AND ' )
                              + poSearchCriteria.SearchParams.ao_Query_Criteria [ pintCriterionIndex ].Parameter
                              + ':'
                              + LLCommon.QuoteString ( strEncodedParamValue );
    }   // const BuildQueryStringBH = function


    const BuildQueryStringWA = function ( pstrSrchCriterionVal , pstrParameters , pintCriterionIndex , poSearchCriteria )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      BuildQueryStringWA
        //
        //  Function Goal:      Assemble a query string parameter for a Wise
        //                      Agent database query.
        //
        //  Arguments:          pstrSrchCriterionVal = Criterion value to append
        //
        //                      pstrParameters       = The built-up parameter
        //                                             list, to which parameter
        //                                             pstrSrchCriterionVal is
        //                                             appended
        //
        //                      pintCriterionIndex   = Zero-based array index,
        //                                             which identifies a field
        //                                             name to label parameter
        //                                             pstrSrchCriterionVal
        //
        //                      poSearchCriteria     = Search criteria object
        //
        //  Returns:            The return value is the new string created by
        //                      appending criterion value pstrSrchCriterionVal
        //                      to input string pstrParameters.
        //
        //  Remarks:            Since all three argument values are generated by
        //                      our code, they are taken at face value.
        //
        //                      The replace operation replaces only the first
        //                      occurrence of 'N' in pstrCellIdTemplate, owing
        //                      to the default behavior of the JavaScript string
        //                      replace method.
        //
        //                      Though it could easily be implemented inline, I
        //                      chose to make it a function for consistency
        //                      between the two usages in the calling routine.
        //  --------------------------------------------------------------------

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        return pstrParameters + LOGICAL_NEGATE
                              + poSearchCriteria.SearchParams.ao_Query_Criteria [ pintCriterionIndex ].Parameter
                              + EQUALS_CHAR
                              + encodeURIComponent ( pstrSrchCriterionVal );
    }   // const BuildQueryStringWA = function


    const CellIDTemplateIdFixup = function ( pstrCellIdTemplate , pintRowIndex )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      CellIDTemplateIdFixup
        //
        //  Function Goal:      Replace a string pstrCellIdTemplate that looks
        //                      like "SearchCRMResults_DetailRowNCol" with a new
        //                      string in which N is replaced by the integer
        //                      specified as the value of pintLabelColIndex.
        //
        //  Arguments:          pstrCellIdTemplate = Element ID template in
        //                                           which to make substitution
        //
        //                      pintRowIndex       = Integer to substitute for
        //                                           the character 'N' in input
        //                                           string pstrCellIdTemplate
        //
        //  Remarks:            Since both argument values are generated by our
        //                      code, they are taken at face value.
        //
        //                      The replace operation replaces only the first
        //                      occurrence of 'N' in pstrCellIdTemplate, owing
        //                      to the default behavior of the JavaScript string
        //                      replace method.
        //
        //                      Though it could easily be implemented inline, I
        //                      chose to make it a function for consistency
        //                      between the two usages in the calling routine.
        //  --------------------------------------------------------------------

        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        return pstrCellIdTemplate.replace ( 'N' , LLCommon.OrdinalFromIndex ( pintRowIndex ) );
    }   // const CellIDTemplateIdFixup = function


    const GetCriterionElement = function ( pintCriterionIndex )
    {
        const strMethodName       = LLCommon.GetNameOfCurrentFunction ( );
        const strSrchCriterionId  = 'docCriterion' + LLCommon.OrdinalFromIndex ( pintCriterionIndex );
        return document.getElementById ( strSrchCriterionId );
    }   // const GetCriterionElement = function

    //  -----------------------------------
    //  Begin body of function DoCRMSearch:
    //  -----------------------------------

    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    var   strParameters                 = EMPTY_STRING;

    debugger;

    try
    {
        //  --------------------------------------------------------------------
        //  Retrieve the search criteria from the current value in the combo box
        //  when DoCRMSearch is called without arguments. When the RETURN key
        //  event listener calls, it has the value in its working variables
        //  because it has just reset the value of the combo box.
        //  --------------------------------------------------------------------

        const oSearchCriteria               = Object.is ( poSearchCriteria , undefined )
                                              ? GetCRMSearchCriteriaFromPickList ( )
                                              : poSearchCriteria;

        switch ( oSearchCriteria.EntityId )
        {
            case 10:    // EntityName = WA-PropertySearchCriteria
                // Very simple: ClientID={ContactID}
                strParameters = 'ClientID=' + document.getElementById ( 'ExternalCRMId' ).value;
                break;
            default:
                //  ------------------------------------------------------------
                //  Though a single loop could build all three lists, the logic
                //  within it is much simpler when the criteria are handled
                //  separately.
                //  ------------------------------------------------------------

                for ( var intCriterionIndex = ARRAY_FIRST_ELEMENT;
                          intCriterionIndex < oSearchCriteria.SearchParams.ao_Query_Criteria.length;
                          intCriterionIndex++ )
                {
                    var strSrchCriterionVal = GetCriterionElement ( intCriterionIndex ).value.trim ( );

                    if ( strSrchCriterionVal.length > EMPTY_STRING_LENGTH )
                    {
                        switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                        {
                            case 'BH-':
                                strParameters = BuildQueryStringBH ( strSrchCriterionVal ,  // pstrSrchCriterionVal
                                                                     strParameters ,        // pstrParameters
                                                                     intCriterionIndex ,    // pintCriterionIndex
                                                                     oSearchCriteria );     // poSearchCriteria
                                break;
                            case 'WA-':
                                strParameters = BuildQueryStringWA ( strSrchCriterionVal ,  // pstrSrchCriterionVal
                                                                     strParameters ,        // pstrParameters
                                                                     intCriterionIndex ,    // pintCriterionIndex
                                                                     oSearchCriteria );     // poSearchCriteria
                                break;
                        }   // switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                    }   // if ( strSrchCriterionVal.length > EMPTY_STRING_LENGTH )
                }   // for ( var intCriterionIndex = ARRAY_FIRST_ELEMENT; intCriterionIndex < LLCommon.EntityType.SearchParameters.ao_Query_Criteria.length; intCriterionIndex++ )

                switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                {
                    case 'BH-':
                        var   strFieldList = LOGICAL_NEGATE + 'fields=';
                        var   strSortOrder = LOGICAL_NEGATE  + 'sort=';

                        const intFieldListPrefixLen = strFieldList.length;
                        const intSortOrderPrefixLen = strSortOrder.length;

                        for ( var intFieldIndex = ARRAY_FIRST_ELEMENT;
                                  intFieldIndex < oSearchCriteria.SearchParams.ao_Result_Columns.length;
                                  intFieldIndex++ )
                        {
                            //  ------------------------------------------------
                            //  The sort order list skips the first field, which
                            //  is the ID. However, the list of fields to
                            //  display is all-inclusive.
                            //  ------------------------------------------------

                            if ( intFieldIndex > ARRAY_FIRST_ELEMENT )
                            {
                                if ( strSortOrder.length === intSortOrderPrefixLen )
                                {
                                    strSortOrder +=                        oSearchCriteria.SearchParams.ao_Result_Columns [ intFieldIndex ].ColumnName;
                                }   // TRUE (The first iteration goes without a preceding delimiter.) block, if ( strSortOrder.length === intSortOrderPrefixLen )
                                else
                                {
                                    strSortOrder += ( CSV_SEPARATOR_CHAR + oSearchCriteria.SearchParams.ao_Result_Columns [ intFieldIndex ].ColumnName );
                                }   // FALSE (Subsequent iterations must be preceded by a delimiter character.) block, if ( strSortOrder.length === intSortOrderPrefixLen )
                            }   // if ( intFieldIndex > ARRAY_FIRST_ELEMENT )

                            if ( strFieldList.length === intFieldListPrefixLen )
                            {
                                strFieldList     +=                        oSearchCriteria.SearchParams.ao_Result_Columns [ intFieldIndex ].ColumnName;
                            }   // TRUE (The first iteration goes without a preceding delimiter.) block, if ( strFieldList === intFieldListPrefixLen )
                            else
                            {
                                strFieldList     += ( CSV_SEPARATOR_CHAR + oSearchCriteria.SearchParams.ao_Result_Columns [ intFieldIndex ].ColumnName );
                            }   // FALSE (Subsequent iterations must be preceded by a delimiter character.) block, if ( strFieldList === intFieldListPrefixLen )
                        }   // for ( var intFieldIndex = ARRAY_SECOND_ELEMENT; intFieldIndex < oSearchCriteria.SearchParams.ao_Result_Columns.length; intFieldIndex++ )

                        strParameters            += ( strFieldList + strSortOrder );
                        break;  // case 'BH-' (Bullhorn)

                    case 'WA-':
                        break;

                    default:
                        LLCommon.LogException ( strMethodName + ' CRN ID "' + LLCommon.EnabledCRM.SysCRMLeadOrContact + '" is unsupported.' );
                        break;
                }   // switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                break;  // default block for switch ( poSearchCriteria.EntityId )
        }   // switch ( poSearchCriteria.EntityId )

        //  --------------------------------------------------------------------
        //  Processing from this point forward is identical for all entities.
        //  --------------------------------------------------------------------

        if ( strParameters.length > EMPTY_STRING_LENGTH )
        {
            const docCRMEntitySel   = document.getElementById ( 'CRMSearchableEntities' );

            const oSearchParams     = JSON.parse ( docCRMEntitySel.value );
            const strCRMEntityName  = oSearchParams.EntityName.substring ( LLCommon.EnabledCRM.SysCRMLeadOrContact.length );
            const aoSearchResult    = LLCommon.DoAjax ( 'SalesTalkSalesforce/SearchCRM',
                                                        'GET',
                                                        {
                                                            'WhichCRM'   : LLCommon.EnabledCRM.SysCRMLeadOrContact,
                                                            'EntityName' : strCRMEntityName,
                                                            'Email'      : LLCommon.DialerLogin,
                                                            'Criteria'   : strParameters,
                                                            'tenantId'   : _tenantid,
                                                            'domainId'   : _domainid
                                                        } );

            if ( aoSearchResult !== '[]' )
            {   // When the result set is empty, Wise Agent returns the empty array.
                if ( aoSearchResult.startsWith ( '[{"' ) )
                {   // Otherwise, expect an array delimiter followed immediately by the opening delimiter of an Object.
                    const aoResultSet                   = JSON.parse ( aoSearchResult );
                    const docCRMSearchResults           = document.getElementById ( 'docCRMSearchResultsRows' );

                    if ( docCRMSearchResults.innerHTML.length > EMPTY_STRING_LENGTH )
                    {   // Unless it's already empty, make it so.
                        docCRMSearchResults.innerHTML   = EMPTY_STRING;
                    }   // if ( docCRMSearchResults.innerHTML.length > EMPTY_STRING_LENGTH )

                    //  --------------------------------------------------------
                    //  Create and populate the label row, of which there is
                    //  one, composed of a column for each element in the array
                    //  of oSearchCriteria.SearchParams.ao_Result_Column_Map
                    //  objects.
                    //  --------------------------------------------------------

                    var   docLabelRow                   = document.createElement ( 'tr' );

                    docCRMSearchResults.appendChild ( docLabelRow );

                    for ( var intLabelColIndex = ARRAY_FIRST_ELEMENT;
                              intLabelColIndex < oSearchCriteria.SearchParams.ao_Result_Column_Map.length;
                              intLabelColIndex++ )
                    {
                        var strLabelColumnCSSSelector   = oSearchCriteria.SearchParams.ao_Result_Column_Map [ intLabelColIndex ].LabelRowSelector;

                        if ( intLabelColIndex === ARRAY_FIRST_ELEMENT )
                        {   // Assign the primry class name (the first name that appears in the list of CSS selectors) to the entire Table Row (TR) element.
                            docLabelRow.classList.add ( strLabelColumnCSSSelector.substring ( SUBSTRING_FIRST_CHAR ,
                                                                                              strLabelColumnCSSSelector.indexOf ( SPACE_CHARACTER ) ) );
                        }   // if ( intLabelColIndex === ARRAY_FIRST_ELEMENT )

                        var docLabelCell                = document.createElement ( 'td' );
                        docLabelCell.innerText          = oSearchCriteria.SearchParams.ao_Result_Column_Map [ intLabelColIndex ].ColumnLabel;

                        //  --------------------------------------------------------
                        //  Since strDetailRowSelector MAY specify two or more
                        //  space-delimited CSS selectors, setting the attribute
                        //  applies all of them at once.
                        //  --------------------------------------------------------

                        LLCommon.AddOrRemoveStyles ( docLabelCell ,
                                                     strLabelColumnCSSSelector ,
                                                     LLCommon.CSS_SELECTOR_ADD );
                        docLabelRow.appendChild ( docLabelCell );
                    }   // for ( var intLabelColIndex = ARRAY_FIRST_ELEMENT; intLabelColIndex < LLCommon.EntityType.SearchParameters.ao_Result_Column_Map.length; intLabelColIndex++ )

                    //  ------------------------------------------------------------
                    //  Create and populate a row for each result returned by the
                    //  CRM server. There are expected to be at least as many fields
                    //  in the result set as there are columns in the label row, but
                    //  appending two result strings into the same cell, such as for
                    //  first and last names, is supported.
                    //  ------------------------------------------------------------

                    for ( var intResultRowIndex = ARRAY_FIRST_ELEMENT;
                              intResultRowIndex < aoResultSet.length;
                              intResultRowIndex++ )
                    {
                        var   docDetailRow              = document.createElement ( 'tr' );

                        docCRMSearchResults.appendChild ( docDetailRow );

                        for ( var intResultColIndex = ARRAY_FIRST_ELEMENT;
                                  intResultColIndex < oSearchCriteria.SearchParams.ao_Result_Column_Map.length;
                                  intResultColIndex++ )
                        {
                            var strDetailRowSelector    = oSearchCriteria.SearchParams.ao_Result_Column_Map [ intResultColIndex ].DetailRowSelector;
                            var docDetailCell           = document.createElement ( 'td' );
                            docDetailCell.id            = CellIDTemplateIdFixup ( oSearchCriteria.SearchParams.ao_Result_Column_Map [ intResultColIndex ].CellIdTemplate ,
                                                                                  intResultRowIndex );

                            if ( intResultColIndex === ARRAY_FIRST_ELEMENT )
                            {
                                debugger;

                                //  ------------------------------------------------
                                //  Assign the primary class name (the first name
                                //  that appears in the list of CSS selectors) to
                                //  the entire Table Row (TR) element.
                                //
                                //  Although the event listener is attached to the
                                //  whole row, the listener needs the ID of its
                                //  first cell, which contains the ID that it needs.
                                //  However, since variables within the scopee of a
                                //  closure, though visible to the enclosing arrow
                                //  function, pass their current values, which are
                                //  subject to change once the arrow function is
                                //  registered, one must rely instead upon the event
                                //  target, the row to which the event is attached.
                                //  The innerHTML that contains the required input
                                //  value is accessible through its firstChild
                                //  element. Since DoGetRecordFromCRM expects a
                                //  string representation of the ID, that is exactly
                                //  what it gets.
                                //  ------------------------------------------------

                                docDetailRow.classList.add ( strDetailRowSelector.substring ( SUBSTRING_FIRST_CHAR ,
                                                                                              strDetailRowSelector.indexOf ( SPACE_CHARACTER ) ) );
                                LLCommon.AddOrRemoveStyles ( docDetailRow ,
                                                             'STT_Cursor_is_Mouse' ,
                                                             LLCommon.CSS_SELECTOR_ADD );

                                docDetailRow.addEventListener ( 'click' , ( event ) =>
                                {
                                    event.stopPropagation ( );
                                    DoGetRecordFromCRM ( event.currentTarget.firstChild.id );
                                }); // end of docDetailRow click event listener
                            }   // if ( intResultColIndex === ARRAY_FIRST_ELEMENT )

                            //  ----------------------------------------------------
                            //  Since strDetailRowSelector MAY specify two or more
                            //  space-delimited CSS selectors, setting the className
                            //  attribute applies all of them at once.
                            //  ----------------------------------------------------

                            LLCommon.AddOrRemoveStyles ( docDetailCell ,
                                                         strDetailRowSelector ,
                                                         LLCommon.CSS_SELECTOR_ADD );
                            docDetailRow.appendChild ( docDetailCell );
                        }   // for ( var intResultColIndex = ARRAY_FIRST_ELEMENT; intResultColIndex < LLCommon.EntityType.SearchParameters.ao_Result_Column_Map.length; intResultColIndex++ )

                        //  --------------------------------------------------------
                        //  Since two or more columns (fields) of result data can go
                        //  into one cell in the result grid, the result is appended
                        //  once the row is otherwise complete.
                        //  --------------------------------------------------------

                        for ( var intDetailColumnIndex = ARRAY_FIRST_ELEMENT;
                                  intDetailColumnIndex < oSearchCriteria.SearchParams.ao_Result_Columns.length;
                                  intDetailColumnIndex++ )
                        {
                            var strDetailCellID         = CellIDTemplateIdFixup ( oSearchCriteria.SearchParams.ao_Result_Columns [ intDetailColumnIndex ].CellIdTemplate ,
                                                                                  intResultRowIndex );
                            var strDetailCellColumnName = oSearchCriteria.SearchParams.ao_Result_Columns [ intDetailColumnIndex ].ColumnName;
                            var varResultRecord         = aoResultSet [ intResultRowIndex ];
                            var strDetailCellValue      = strDetailCellColumnName in varResultRecord
                                                          ? varResultRecord [ strDetailCellColumnName ] !== undefined
                                                            ? varResultRecord [ strDetailCellColumnName ]
                                                            : EMPTY_STRING
                                                          : EMPTY_STRING;
                            var docDetailColCell        = document.getElementById ( strDetailCellID );

                            if ( docDetailColCell !== null )
                            {
                                if ( docDetailColCell.innerText.length === EMPTY_STRING_LENGTH )
                                {
                                    docDetailColCell.innerText  = strDetailCellValue;
                                }   // TRUE (The cell is empty.) block, if ( docDetailColCell.innerText.length === EMPTY_STRING_LENGTH )
                                else
                                {
                                    docDetailColCell.innerText  += ( SPACE_CHARACTER + strDetailCellValue );
                                }   // FALSE (The cell contains some text.) block, if ( docDetailColCell.innerText.length === EMPTY_STRING_LENGTH )
                            }   // if ( docDetailColCell !== null )
                        }   // for ( var intDetailColumnIndex = ARRAY_FIRST_ELEMENT; ntDetailColumnIndex < LLCommon.EntityType.SearchParameters.ao_Result_Columns.length; intDetailColumnIndex++ )
                    }   // for ( var intResultRowIndex = ARRAY_FIRST_ELEMENT; intResultRowIndex < aoResultSet.length; intResultRowIndex++ )

                    ShowOrHideElement ( 'docCRMSearchResultsGrid' ,
                                        LLCommon.ELEMENT_SHOW );
                }   // TRUE (anticipated outcome) block, if ( aoSearchResult.startsWith ( '[{"' ) )
                else
                {   // Treat anything else as an exception.
                    LLCommon.LogException ( strMethodName + 'SalesTalkSalesforce/SearchCRM reported an Exception. Details: ' + aoSearchResult );
                }   // FALSE (unanticipated outcome) block, if ( aoSearchResult.startsWith ( '[{"' ) )
            }   // TRUE (anticipated outcome) block, if ( aoSearchResult !== '[]' )
            else
            {
                alert ( 'No records match the specified criteria. Please edit and try a new search.' , 'native' );
            }   // FALSE (unanticipated outcome) block, if ( aoSearchResult !== '[]' )
        }   // TRUE (anticipated outcome) block, if ( strParameters.length > EMPTY_STRING_LENGTH )
        else
        {
            alert ( 'Please enter a search criterion.' , 'native' );
        }   // FALSE (unanticipated outcome) block, if ( strParameters.length > EMPTY_STRING_LENGTH )
    } catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function DoCRMSearch


function DoCloseCRMSearch ( )
{
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );
    debugger;
    ShowOrHideElement ( 'docSearchTermsGrid' ,
                        LLCommon.ELEMENT_HIDE );
    ShowOrHideElement ( 'docCRMSearchResultsGrid' ,
                        LLCommon.ELEMENT_HIDE );
}   // function DoCloseCRMSearch


function DoGetRecordFromCRM ( pstrExternalCrmIdinputElementId )
{
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    try
    {
        if ( LLCommon.IsString ( pstrExternalCrmIdinputElementId ) && pstrExternalCrmIdinputElementId.length > EMPTY_STRING_LENGTH )
        {
            const docExternalCrmIdInputElement  = document.getElementById ( pstrExternalCrmIdinputElementId );

            if ( docExternalCrmIdInputElement !== null )
            {
                var strNewExternalCrmId = EMPTY_STRING;
                LLCommon.ShowOrHideElement ( NOTES_FILTER_CONTAINER ,
                                             LLCommon.ELEMENT_HIDE );

                switch ( docExternalCrmIdInputElement.nodeName )
                {
                    case 'INPUT':
                        if ( docExternalCrmIdInputElement.type === 'text' )
                        {
                            strNewExternalCrmId = docExternalCrmIdInputElement.value;
                        }   // TRUE (anticipated outcome) block, if ( docExternalCrmIdInputElement.type === 'text' )
                        else
                        {
                            throw new Error ( 'ERROR in ' + strMethodName + ': The specified INPUT element, ' + LLCommon.QuoteString ( pstrExternalCrmIdinputElementId ) + ' MUST be an INPUT element and its type must be text. The nodeName attribute of the element is ' + docExternalCrmIdInputElement.nodeName + ( docExternalCrmIdInputElement.nodeName === 'INPUT' ? ', but its type is ' + docExternalCrmIdInputElement.type + '.' : ', which has no type attribute.' ) );
                        }   // FALSE (unanticipated outcome) block, if ( docExternalCrmIdInputElement.type === 'text' )
                        break;  // case 'INPUT'

                    case 'TD':
                        strNewExternalCrmId     = docExternalCrmIdInputElement.innerText;
                        break;  // case 'TD'
                }   // switch ( docExternalCrmIdInputElement.nodeName )

                //  ------------------------------------------------------------
                //  Unless the ID value is missing, perform a redirect to the
                //  configured CRM, which is expected to return a form populated
                //  with data from the record that has the requested ID, AFTER
                //  the currently displayed record is updated if necessary.
                //  ------------------------------------------------------------

                if ( strNewExternalCrmId.length > EMPTY_STRING_LENGTH )
                {
                    if ( LLCommon._fFormIsDirty )
                    {   // Since the Click event listener is a brand new regular function, calling it directly is the most efficient way to invoke its event listener.
                        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                            Words2Actions_Recorder_Forms_VERSION ,
                                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                                            'Function ' + strMethodName + ' said: Updating dirty record of SalesTalk lead ID ' + _leadid + ' in CRM' ) );
                        DoUpdateCrmNow ( );                 // DoUpdateCrmNow turns the LLCommon._fFormIsDirty flag OFF.
                    }   // if ( LLCommon._fFormIsDirty )

                    LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                        Words2Actions_Recorder_Forms_VERSION ,
                                                        Words2Actions_Recorder_Forms_LastUpdated ,
                                                        'Function ' + strMethodName + ' said: Retrieving record ID ' + LLCommon.QuoteString ( strNewExternalCrmId ) + ' from CRM' ) );

                    debugger;

                    //  --------------------------------------------------------
                    //  Evaluate the pathname property of the location object.
                    //  "pathname": "/InsuranceDB/COMMON/STAGING/Words2Actions_Form_TEMPLATE.HTML",
                    //  Do the redirect when it ends with "Words2Actions_Form_TEMPLATE.HTML",
                    //  otherwise, do CreateOrRefreshLeadFromCRM.
                    //  --------------------------------------------------------

                    if ( location.pathname.toLowerCase ( ).endsWith ( 'words2actions_form_template.html' ) )
                    {
                        var strRedirectUrl      = EMPTY_STRING;

                        switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )
                        {
                            case 'BH-':
                                strRedirectUrl  = DoGetRecordFromCRM_BH ( strNewExternalCrmId );
                                break;
                            case 'WA-':
                                strRedirectUrl  = DoGetRecordFromCRM_WA ( strNewExternalCrmId );
                                break;
                        }   // switch ( LLCommon.EnabledCRM.SysCRMLeadOrContact )

                        const strSearchedEntityTypeName = GetCRMEntityTypeFromPickList ( );

                        if ( strSearchedEntityTypeName !== LLCommon.TOKEN_NOCRM )
                        {
                            LLCommon.ShowOrHideElement ( 'DoWords2Notes' ,
                                                         _EntityType.toLowerCase ( ) !== 'wa-propertysearchcriteria' );

                            if ( _EntityType === strSearchedEntityTypeName )
                            {
                                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                                      'Function '
                                                                    + strMethodName
                                                                    + ' said: Redirecting TO URL '
                                                                    + strRedirectUrl ) );
                                window.location.replace ( strRedirectUrl );
                            }   // TRUE (The searched entity type is the same as that of the current page.) block, if ( _EntityType === strSearchedEntityTypeName )
                            else
                            {
                                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                                    'Function ' + strMethodName + ' said: Opening (in a new tab) URL ' + strRedirectUrl ) );
                                window.open ( strRedirectUrl ,
                                              strSearchedEntityTypeName );
                            }   // FALSE (The sarched entity type DIFFERS from that of the current page.) block, if ( _EntityType === strSearchedEntityTypeName )
                        }   // if ( strSearchedEntityTypeName !== LLCommon.TOKEN_NOCRM )
                    }   // TRUE (The request came from the Words2Actions page.) block, if ( location.pathname.toLowerCase ( ).endsWith ( 'words2actions_form_template.html' ) )
                    else
                    {
                        const intLeadId  = parseInt ( LLCommon.DoAjax ( 'CreateOrRefreshLeadFromCRM',
                                                                        'GET',
                                                                        {
                                                                            'LeadId'              : NO_LEAD_ID ,
                                                                            'DomainId'            : _domainid ,
                                                                            'TenantId'            : _tenantid ,
                                                                            'SysCRMLeadOrContact' : LLCommon.EnabledCRM.SysCRMLeadOrContact ,
                                                                            'crmUserEmail'        : LLCommon.DialerLogin ,
                                                                            'crmUserId'           : LLCommon.DialerLogin ,
                                                                            'EntityType'          : GetCRMEntityTypeFromPickList ( ) ,
                                                                            'ExternalCRMId'       : strNewExternalCrmId
                                                                        } ) );

                        if ( intLeadId >= MINIMUM_STT_ENTITY_ID )
                        {
                            DoDisplayContact ( intLeadId );
                        }   // TRUE (anticipateed outcome) block, if ( intLeadId >= MINIMUM_STT_ENTITY_ID )
                        else
                        {
                            throw new Error ( 'ERROR in ' + strMethodName + ': The specified INPUT element, ' + LLCommon.QuoteString ( pstrExternalCrmIdinputElementId ) + ' cannot be found in the document.' );
                        }   // FALSE (unanticipateed outcome) block, if ( intLeadId >= MINIMUM_STT_ENTITY_ID )
                    }   // FALSE (The request came from another page.) block, if ( location.pathname.toLowerCase ( ).endsWith ( 'words2actions_form_template.html' ) )
                }   // TRUE (anticipated outcome) block, if ( strNewExternalCrmId.length > EMPTY_STRING_LENGTH )
                else
                {
                    alert ( 'Please enter a valid record ID.' , 'native' );
                    throw new Error ( 'ERROR in ' + strMethodName + ': The specified ' + docExternalCrmIdInputElement.nodeName + ' element, ' + LLCommon.QuoteString ( pstrExternalCrmIdinputElementId ) + ' is devoid of a recognizable record ID.' );
                }   // FALSE (unanticipated outcome) block, if ( strNewExternalCrmId.length > EMPTY_STRING_LENGTH )
            }   // TRUE (anticipated outcome) block, if ( docExternalCrmIdInputElement !== null ))
            else
            {
                throw new Error ( 'ERROR in ' + strMethodName + ': Method CreateOrRefreshLeadFromCRM returned an error. See server log for details.' );
            }   // FALSE (unanticipated outcome) block, if ( docExternalCrmIdInputElement !== null ))
        }   // TRUE (anticipated outcome) block, if ( LLCommon.IsString ( pstrExternalCrmIdinputElementId ) && pstrExternalCrmIdinputElementId.length > EMPTY_STRING_LENGTH )
        else
        {
            throw new Error ( 'ERROR in ' + strMethodName + ': Input parameter pstrExternalCrmIdinputElementId must be a String, and the String must be the ID of the INPUT element that receives the external CRM ID to get.' );
        }   // FALSE (unanticipated outcome) block, if ( LLCommon.IsString ( pstrExternalCrmIdinputElementId ) && pstrExternalCrmIdinputElementId.length > EMPTY_STRING_LENGTH )
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }


    function DoGetRecordFromCRM_BH ( pstrNewExternalCrmId )
    {
        const strMethodName               = LLCommon.GetNameOfCurrentFunction ( );

        const strBullHornCorporationID    = LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                              'GET',
                                                              {
                                                                  'monikor'         : 'BullHorn CorporationID',
                                                                  'tenantId'        : _tenantid,
                                                                  'domainId'        : _domainid,
                                                                  'ignoreWebConfig' : true
                                                              } );
        var   strBullhornLoginId          = LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                              'GET',
                                                              {
                                                                  'monikor'         : 'Bullhorn UserId ' + _login,
                                                                  'tenantId'        : _tenantid,
                                                                  'domainId'        : _domainid,
                                                                  'ignoreWebConfig' : true
                                                              } );

        if ( strBullHornCorporationID.length > EMPTY_STRING_LENGTH )
        {
            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                Words2Actions_Recorder_Forms_VERSION ,
                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                'Function ' + strMethodName + ' said: Bullhorn Corporation ID = ' + strBullHornCorporationID ) );
        }   // TRUE (anticipated outcome) block, if ( strBullHornCorporationID.length > EMPTY_STRING_LENGTH )
        else
        {
            console.error ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                               Words2Actions_Recorder_Forms_VERSION ,
                                               Words2Actions_Recorder_Forms_LastUpdated ,
                                               'Function ' + strMethodName + ' said: Bullhorn Corporation ID IS MISSING.' ) );
        }   // FALSE (unanticipated outcome) block, if ( strBullHornCorporationID.length > EMPTY_STRING_LENGTH )

        if ( strBullhornLoginId.length > EMPTY_STRING_LENGTH )
        {
            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                Words2Actions_Recorder_Forms_VERSION ,
                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                'Function ' + strMethodName + ' said: Bullhorn Login ID = ' + strBullhornLoginId ) );
        }   // TRUE (anticipated outcome) block, if ( strBullhornLoginId.length > EMPTY_STRING_LENGTH )
        else
        {
            console.error ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                               Words2Actions_Recorder_Forms_VERSION ,
                                               Words2Actions_Recorder_Forms_LastUpdated ,
                                               'Function ' + strMethodName + ' said: Bullhorn Login ID IS MISSING. substituting 4.' ) );
            strBullhornLoginId = '4';
        }   // FALSE (unanticipated outcome) block, if ( strBullhornLoginId.length > EMPTY_STRING_LENGTH )

        return LLCommon.AjaxUrlPrefix + 'SalesTalkSalesforce?Bullhorn=true&EntityType=' + LLCommon.EntityType.AbsoluteEntityName + '&UserID=' + strBullhornLoginId + '&CorporationID=' + strBullHornCorporationID + '&PrivateLabelID=45649&EntityID=' + pstrNewExternalCrmId + '&currentBullhornUrl=' + encodeURIComponent ( location.href );
    }   // function DoGetRecordFromCRM_BH


    function DoGetRecordFromCRM_WA ( pstrNewExternalCrmId )
    {
        const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

        console.log ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                         Words2Actions_Recorder_Forms_VERSION ,
                                         Words2Actions_Recorder_Forms_LastUpdated ,
                                         'Function ' + strMethodName + ' said: Type=Lead-WiseAgent, EntityID=' + pstrNewExternalCrmId + ', UserEmail=' + _login ) );
        const strSearchedEntityTypeName = GetCRMEntityTypeFromPickList ( );
        const strNewPageName = _EntityType === strSearchedEntityTypeName
                               ? _pagename
                               : strSearchedEntityTypeName === 'PropertySearchCriteria'
                                 ? 'WiseAgentPropertySearch'
                                 : _pagename;

        return LLCommon.AjaxUrlPrefix + 'SalesTalkSalesforce?Type=' + strSearchedEntityTypeName + '-' + LLCommon.EnabledCRM.CrmName + '&EntityID=' + pstrNewExternalCrmId + '&UserEmail=' + _login + '&pagename=' + strNewPageName + '&EntityType=' + strSearchedEntityTypeName + '&CI=' + _CI;
    }   // function DoGetRecordFromCRM_WA


    return false;               // An event returning false implies preventDefault.
}   // function DoGetRecordFromCRM


function DoUpdateCrmNow ( pfAsync )
{
    const strMethodName = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    //  ------------------------------------------------------------------------
    /// <summary>
    /// Update the lead identified by <paramref name="leadid"/> on the BullHorn
    /// CRM.
    /// </summary>
    /// <param name="leadid">
    /// This required integer specifies the SalesTalk ID of the lead to promote
    /// to the BullHorn specialty CRM.
    /// </param>
    /// <returns>
    /// This string is the value returned by ReturnToSender, which is expected
    /// to be the empty string.
    /// </returns>
    //
    //  public string Promote2CRM ( int leadid ,
    //                              string UserEmailId = SpecialStrings.EMPTY_STRING )
    //
    //  ------------------------------------------------------------------------
    //  LLCommon.DialerLogin is either a valid login ID or the empty string.
    //  ------------------------------------------------------------------------

    try
    {
        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                            Words2Actions_Recorder_Forms_VERSION ,
                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                            'Function ' + strMethodName + ' said: Calling Open method Promote2CRM with leadid = ' + _leadid + ' and UserEmailId = ' + LLCommon.DialerLogin ) );

        var strOutcome          = EMPTY_STRING;

        if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
        {
            //PostNewTaskWA ( string Email , int LeadId , int DomainId , int TenantId )
            strOutcome          = LLCommon.DoAjax ( 'SalesTalkSalesforce/PostNewTaskWA',
                                                    'GET',
                                                    {
                                                        'Email'             : LLCommon.DialerLogin,
                                                        'LeadId'            : _leadid,
                                                        'DomainId'          : _domainid,
                                                        'TenantId'          : _tenantid
                                                    }
                                                  );

            if ( strOutcome.length > EMPTY_STRING_LENGTH )
            {
                throw new Error ( strMethodName + ': SalesTalk _objAPI PostNewTaskWA returned the following error message:' + strOutcome );
            }   // if ( strOutcome.length > EMPTY_STRING_LENGTH )
        }   // TRUE (The WA-Task entity gets special handling.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
        else
        {
            strOutcome          = LLCommon.DoAjax ( 'Promote2CRM',
                                                    'GET',
                                                    {
                                                        'leadid'                : _leadid,
                                                        'UserEmailId'           : LLCommon.DialerLogin,
                                                        'CreateNewCRMRecord'    : fCreateNewCRMRecord
                                                    },
                                                    pfAsync
                                                  ).replace ( 'was not synchronized' , 'was already synchronized' );

            if ( pfAsync || LLCommon._FSuppressCRMUpdateAlert )
            {
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    'Function ' + strMethodName + ' said: Returned from Open method Promote2CRM with leadid = ' + _leadid + ' and UserEmailId = ' + LLCommon.DialerLogin + ', reporting outcome = ' + strOutcome ) );
            }   // TRUE (Since the page is losing focus, we aren't waiting for an answer.) block, if ( pfAsync || LLCommon._FSuppressCRMUpdateAlert )
            else
            {
                alert ( strOutcome , 'native' );
            }   // FALSE (Since the request is on behalf of an active user, we waited for the return.) block, if ( pfAsync || LLCommon._FSuppressCRMUpdateAlert )
        }   // FALSE (All other requests get the standard processing.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )

        LLCommon._fFormIsDirty  = false;
        LLCommon.inputDisable ( BTN_UPDATE_CRM );
        sessionStorage.setItem ( 'CRM_Maybe_Dirty_flag_' + LLCommon.ExternalCRMId + UNDERSCORE_CHAR + LLCommon.LeadId , new Date ( ) );

        if ( LLCommon._FSuppressCRMUpdateAlert )
        {
            LLCommon._FSuppressCRMUpdateAlert   = false;
        }   // TRUE (Caller requested suppression of the page reset alert message.) block, if ( LLCommon._FSuppressCRMUpdateAlert )
        else
        {
            LLCommon.ShowResetAlert ( );
        }   // FALSE (The standard workflow requires the page reeset alert message.) block, if ( LLCommon._FSuppressCRMUpdateAlert )

        return true;
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function DoUpdateCrmNow


function DropDown ( event )
{
    const strMethodName                     = LLCommon.GetNameOfCurrentFunction ( );

    const SUFFIX_LENGTH                     = 9;        // Length of suffix '_DropDown'

    debugger;

    const docCompanionTextBox               = document.getElementById ( event.id.substring ( SUBSTRING_FIRST_CHAR , event.id.length - SUFFIX_LENGTH ) );
    const docTextBoxContainer               = docCompanionTextBox.parentElement;

    //  ------------------------------------------------------------------------
    //  Since this routine must address the case where the value in the text box
    //  is absent from the list of value stored in the database, it cannot use
    //  GetPickListValues. Nevertheless, the following code is derived from it.
    //  ------------------------------------------------------------------------

    try
    {
        const aobjPickListValues            = LLCommon.DoAjax ( 'GetPickListValues',
                                                                'GET',
                                                                {
                                                                    'systemProperty'     : docCompanionTextBox.id ,
                                                                    'tenantId'           : _LeadLifeJSHelpers.STTTenantId ,
                                                                    'domainId'           : _LeadLifeJSHelpers.STTDomainId ,
                                                                    'IgnoreDisplayOrder' : true,
                                                                    'FirstItem'          : LOGICAL_NEGATE
                                                                } );

        if ( Array.isArray ( aobjPickListValues.PickListValues ) )
        {
            const docPickList               = document.createElement ( 'select' );

            docTextBoxContainer.appendChild ( docPickList );
            docPickList.classList.add ( 'STT_DynamicPickList' );                // LLCommon.AddOrRemoveStyles is overkill for this brand new element.
            docPickList.id                  = docCompanionTextBox.id + '_DropDown'

            docPickList.addEventListener ( 'change' , ( event ) =>
            {  // Save the current value of the docPickList into the value property of the docCompanionTextBox element, then remove the control.
                debugger;

                //  ------------------------------------------------------------
                //  The objective is to ensure that IF the field value changed,
                //  the _fFormIsDirty flag becomes TRUE if it isn't already so.
                //  ------------------------------------------------------------

                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                    event.type + ' event raied on ' + event.currentTarget.nodeName + ' ID = ' + event.currentTarget.id + ' and selected value = ' + event.currentTarget.value ) );

                if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
                {
                    if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                    {
                        const strDisplayValue = event.currentTarget.options [ event.currentTarget.selectedIndex ].text;

                        if ( docCompanionTextBox.value !== strDisplayValue )
                        {
                            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                                Words2Actions_Recorder_Forms_VERSION ,
                                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                                event.type + ' event raied on ' + event.currentTarget.nodeName + ' Value in companion text box = ' + docCompanionTextBox.value + ' CHANGING to ' + event.currentTarget.value ) );
                            LLCommon._fFormIsDirty  = true;
                            LLCommon.inputEnable ( BTN_UPDATE_CRM );

                            console.log ( 'Inside function ' + strMethodName + ', a Change event listener, DIRTY flag switched ON because docCompanionTextBox.value = ' + docCompanionTextBox.value + ' AND event.currentTarget.value = ' + event.currentTarget.value );
                        }   // if ( docCompanionTextBox.value !== event.currentTarget.value )

                        docCompanionTextBox.value   = event.currentTarget.options [ event.currentTarget.selectedIndex ].text;
                        UpdateIfChanged ( docCompanionTextBox.id );
                    }   // TRUE (Evaluate the text box against the display text because the internal values are unique numeric IDs.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                    else
                    {
                        if ( docCompanionTextBox.value !== event.currentTarget.value )
                        {
                            LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                                Words2Actions_Recorder_Forms_VERSION ,
                                                                Words2Actions_Recorder_Forms_LastUpdated ,
                                                                event.type + ' event raied on ' + event.currentTarget.nodeName + ' Value in companion text box = ' + docCompanionTextBox.value + ' CHANGING to ' + event.currentTarget.value ) );
                            LLCommon._fFormIsDirty  = true;
                            LLCommon.inputEnable ( BTN_UPDATE_CRM );
                            console.log ( 'Inside function ' + strMethodName + ', a Change event listener, DIRTY flag switched ON because docCompanionTextBox.value = ' + docCompanionTextBox.value + ' AND event.currentTarget.value = ' + event.currentTarget.value );
                        }   // if ( docCompanionTextBox.value !== event.currentTarget.value )

                        docCompanionTextBox.value   = event.currentTarget.value;
                        UpdateIfChanged ( docCompanionTextBox.id );
                    }   // FALSE (Evaluating the text box against the internal value is the legacy algorithm.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                }   // TRUE (anticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
                else
                {
                    LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                        Words2Actions_Recorder_Forms_VERSION ,
                                                        Words2Actions_Recorder_Forms_LastUpdated ,
                                                        event.type + ' event raised on ' + event.currentTarget.nodeName + ' with no value selected.' ) );
                }   // FALSE (unanticipated outcome) block, if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )

                event.stopPropagation ( );
            }); // end of docPickList.addEventListener ( 'change' , ( event )

            docPickList.addEventListener ( 'blur' , ( event ) =>
            {  // Save the current value of the docPickList into the value property of the docCompanionTextBox element, then remove the control.
                debugger;
                LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                    Words2Actions_Recorder_Forms_VERSION ,
                                                    Words2Actions_Recorder_Forms_LastUpdated ,
                                                      event.type + ' event raied on ' + event.currentTarget.nodeName
                                                    + ' ID ' + event.currentTarget.id
                                                    + ' and selected value = ' + event.currentTarget.value
                                                    + ', while ' + docCompanionTextBox.nodeName
                                                    + ' ID ' + docCompanionTextBox.id
                                                    + ' value = ' + docCompanionTextBox.value
                                                    + '. Setting ' + docCompanionTextBox.nodeName
                                                    + ' value equal to ' + event.currentTarget.nodeName
                                                    + ' value of ' + event.currentTarget.value ) );

                if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )
                {
                    if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                    {
                        docCompanionTextBox.value   = event.currentTarget.options [ event.currentTarget.selectedIndex ].text;
                    }   // TRUE (Use the DisplayText value.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                    else
                    {
                        docCompanionTextBox.value   = event.currentTarget.value;
                    }   // FALSE (Use the element value.) block, if ( ( LLCommon.EntityType !== null ) && ( new LLCommon.DictionarySharp ( LLCommon.EntityType.EntityDescription ).GetValueAtKey ( 'W2A_WriteOnly' ) === 'true' ) )
                }   // if ( event.currentTarget.selectedIndex > ARRAY_INVALID_INDEX && event.currentTarget.selectedIndex < event.currentTarget.options.length )

                event.currentTarget.remove ( );             // Remove the pick list, leaving behind only the button that spawned it.
                _LeadLifeJSHelpers.ValidateOneFormField ( docCompanionTextBox );
                event.stopPropagation ( );
            }); // end of docPickList.addEventListener ( 'blur' , ( event )

            const intOptionCount            = aobjPickListValues.PickListValues.length;
            var   fUnMatched                = true;
            var   fSelectedOption;

            if ( intOptionCount > ARRAY_IS_EMPTY )
            {
                for ( var intJ = ARRAY_FIRST_ELEMENT;
                          intJ < intOptionCount;
                          intJ++ )
                {
                    var docChoice           = document.createElement ( 'option' );

                    docChoice.value         = aobjPickListValues.PickListValues [ intJ ].Name
                    docChoice.innerHTML     = aobjPickListValues.PickListValues [ intJ ].DisplayText
                    docChoice.selected      = fSelectedOption;

                    docPickList.appendChild ( docChoice );

                    fSelectedOption         = ( docCompanionTextBox.value === aobjPickListValues.PickListValues [ intJ ].Name );

                    if ( fSelectedOption )
                    {
                        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                            Words2Actions_Recorder_Forms_VERSION ,
                                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                                            'SELECTED value of ' + docCompanionTextBox.nodeName + ' id ' + docCompanionTextBox.id + ' set to value ' + docChoice.value + ' (displayText = ' + docChoice.innerHTML + ')' ) );
                         fUnMatched         = false;
                    }   // TRUE (anticipated outcome) block, if ( fSelectedOption )
                    else
                    {
                        LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE ,
                                                            Words2Actions_Recorder_Forms_VERSION ,
                                                            Words2Actions_Recorder_Forms_LastUpdated ,
                                                            'UNSELECTED value of ' + docCompanionTextBox.nodeName + ' id ' + docCompanionTextBox.id + ' set to value ' + docChoice.value + ' (displayText = ' + docChoice.innerHTML + ')' ) );
                    }   // FALSE (unanticipated outcome) block, if ( fSelectedOption )
                }   // for ( var intJ = this.ARRAY_FIRST_ELEMENT; intJ < intOptionCount; intJ++ )

               docPickList.focus ( );
            }   // if ( intOptionCount > ARRAY_IS_EMPTY )
        }   // if ( Array.isArray ( aobjPickListValues.PickListValues ) )

        if ( event.hasOwnProperty ( 'stopPropagation' ) )
        {
            console.log ( 'Since function ' + strMethodName + ' appears to have been handed an event object,something besides an event object, attempt to prevent propagation.' );
            event.stopPropagation ( );
        }   // TRUE (The event argument appears to be an instancee of the Event object.) block, if ( event.hasOwnProperty ( 'stopPropagation' ) )
        else
        {
            console.log ( 'Since function ' + strMethodName + ' was called with something besides an event object, event propagation is unpreventable.' );
        }   // FALSE (The event argument appeears to be something else, such as an element or a string.) block, if ( event.hasOwnProperty ( 'stopPropagation' ) )
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }

    return false;               // An event returning false implies preventDefault.
}   //function DropDown


function GetCRMEntityTypeFromPickList ( )
{
    const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

    const docEntityPickList = document.getElementById ( 'CRMSearchableEntities' );

    if ( docEntityPickList !== null )
    {
        if ( docEntityPickList.value.length > EMPTY_STRING_LENGTH )
        {
            const strValue      = docEntityPickList.value;
            const oCriteria     = JSON.parse ( strValue );
            const strEntityName = oCriteria.EntityName.substring ( LLCommon.EnabledCRM.SysCRMLeadOrContact.length );

            return strEntityName;
        }   // TRUE (Use the value of the functioning pick list.) block, if ( docEntityPickList.value.length > EMPTY_STRING_LENGTH )
        else
        {
            return Object.is ( LLCommon.EntityType , undefined )
                   ? LLCommon.TOKEN_NOCRM
                   : LLCommon.EntityType === null
                     ? LLCommon.TOKEN_NOCRM
                     : LLCommon.EntityType.EntityName;
        }   // FALSE (Fall back to values that live on the LLCommon object.) block, if ( docEntityPickList.value.length > EMPTY_STRING_LENGTH )
    }   // TRUE (anticipated outcome) block, if ( docEntityPickList !== null )
    else
    {
        throw new Error ( strMethodName + 'Required SELECT element "CRMSearchableEntities" cannot be found in the active document.' );
    }   // FALSE (unanticipated outcome) block, if ( docEntityPickList !== null )
}   // function GetCRMEntityTypeFromPickList


function GetCRMSearchCriteriaFromPickList ( )
{
    const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

    const docEntityPickList = document.getElementById ( 'CRMSearchableEntities' );

    if ( docEntityPickList !== null )
    {
        return JSON.parse ( docEntityPickList.value );
    }   // TRUE (anticipated outcome) block, if ( docEntityPickList !== null )
    else
    {
        throw new Error ( strMethodName + 'Required SELECT element "CRMSearchableEntities" cannot be found in the active document.' );
    }   // FALSE (unanticipated outcome) block, if ( docEntityPickList !== null )
}   // function GetCRMSearchCriteriaFromPickList


//  ----------------------------------------------------------------------------
//  Named functions and objects need only to be defined before first use.
//  ----------------------------------------------------------------------------

//  ----------------------------------------------------------------------------
//  This constraints object tells the browser to include only the audio Media
//  Track.
//  ----------------------------------------------------------------------------

const audioMediaConstraints = {
   audio: true,
   video: false,
};

//  ----------------------------------------------------------------------------
//  This constraints object tells the browser to include both the audio and
//  video Media Tracks.
//  ----------------------------------------------------------------------------

const videoMediaConstraints = {
   audio: true,
   video: true,
};


function AdjustButtonProperties ( pintAction )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.Prefix !== undefined )
    {
        if ( ( document.getElementById ( 'ExternalCRMId' ).value.length > EMPTY_STRING_LENGTH || fCreateNewCRMRecord ) && document.getElementById ( 'SysCRMLeadOrContact' ).value.startsWith ( LLCommon.EnabledCRM.Prefix ) )
        {
            LLCommon.ShowOrHideElement ( 'UpdateCRMNow' ,
                                         LLCommon.ELEMENT_SHOW );
        }   // if ( ( document.getElementById ( 'ExternalCRMId' ).value.length > EMPTY_STRING_LENGTH || fCreateNewCRMRecord ) && document.getElementById ( 'SysCRMLeadOrContact' ).value.startsWith ( LLCommon.EnabledCRM.Prefix ) )
    }   // if ( LLCommon.EnabledCRM !== undefined && LLCommon.EnabledCRM.Prefix !== undefined )

    var   docRefreshButtons     = document.getElementById ( 'DoRefreshThisFormNow1' );

    if ( docRefreshButtons !== null )
    {
        docRefreshButtons.addEventListener ( 'click'   , ResetThisForm );
        docRefreshButtons.addEventListener ( 'keydown' , ResetThisForm );
    }   // if ( docRefreshButtons !== null )

    docRefreshButtons           = document.getElementById ( 'DoRefreshThisFormNow2' );

    if ( docRefreshButtons !== null )
    {
        docRefreshButtons.addEventListener ( 'click'   , ResetThisForm );
        docRefreshButtons.addEventListener ( 'keydown' , ResetThisForm );
    }   // if ( docRefreshButtons !== null )

    const oCancelButtons        = _LeadLifeJSHelpers.GetElementByName ( 'CancelCRMUpdate*' );

    if ( oCancelButtons.length >= ARRAY_NOT_EMPTY )
    {
        for ( var intCurrentCancelButton = ARRAY_FIRST_ELEMENT,
                  intTotalCancelButtons  = oCancelButtons.length;
                  intCurrentCancelButton < intTotalCancelButtons;
                  intCurrentCancelButton++ )
        {
            try
            {
                switch ( pintAction )
                {
                    // The absence of a break statement is by design.
                    case BUTTON_STATE_INITIAL:
                        oCancelButtons [ intCurrentCancelButton ].addEventListener ( 'click'   ,
                                                                                     ResetThisForm );
                        oCancelButtons [ intCurrentCancelButton ].addEventListener ( 'keydown' ,
                                                                                     ResetThisForm );

                        //  ----------------------------------------------------
                        //  Falling into the next case block is intentional.
                        //  Ignore the ESLint diagnostic about breaking its
                        //  no-fallthrough rule.
                        //  ----------------------------------------------------

                    case BUTTON_STATE_VISIBLE:
                        ShowOrHideElement ( oCancelButtons [ intCurrentCancelButton ] ,
                                            LLCommon.ELEMENT_SHOW );
                        break;
                    case BUTTON_STATE_HIDDEN:
                        ShowOrHideElement ( oCancelButtons [ intCurrentCancelButton ] ,
                                            LLCommon.ELEMENT_HIDE );
                        break;
                }   // switch ( pintAction )
            }
            catch ( ex )
            {
                LLCommon.LogException ( strMethodName + ': Attempting to register event handlers for forms. See exception log for details.' ,
                                        ex );
            }
        }   // for ( var intCurrentCancelButton = ARRAY_FIRST_ELEMENT, intTotalCancelButtons = oCancelButtons.length; intCurrentCancelButton < intTotalCancelButtons; intCurrentCancelButton++ )
    }   // if ( oCancelButtons.length >= ARRAY_NOT_EMPTY )

    const oPostButtons = _LeadLifeJSHelpers.GetElementByName ( 'post*' );

    if ( oPostButtons.length >= ARRAY_NOT_EMPTY )
    {
        for ( var intCurrentSubmitButton = ARRAY_FIRST_ELEMENT,
                  intTotalSubmitButtons  = oPostButtons.length;
                  intCurrentSubmitButton < intTotalSubmitButtons;
                  intCurrentSubmitButton++ )
        {
            try
            {
                switch ( pintAction )
                {
                    case BUTTON_STATE_INITIAL:
                    case BUTTON_STATE_VISIBLE:
                        if ( oPostButtons [ intCurrentSubmitButton ].id !== BTN_UPDATE_CRM )
                        {   // Skip this button for now. Another routine further down the chain will show it when it is appropriate to do so.
                            LLCommon.ShowOrHideElement ( oPostButtons [ intCurrentSubmitButton ] ,
                                                         LLCommon.ELEMENT_SHOW );
                        }   // if ( oPostButtons [ intCurrentSubmitButton ].id !== BTN_UPDATE_CRM )
                        break;
                    case BUTTON_STATE_HIDDEN:
                        LLCommon.ShowOrHideElement ( oPostButtons [ intCurrentSubmitButton ] ,
                                                     LLCommon.ELEMENT_HIDE );
                        break;
                }   // switch ( pintAction )
            }
            catch ( ex )
            {
                LLCommon.LogException ( strMethodName + ': Attempting to show or hide form elements. See exception log for details.' ,
                                        ex );
            }
        }   // for ( var intCurrentSubmitButton = ARRAY_FIRST_ELEMENT, intTotalSubmitButtons = oCancelButtons.length; intCurrentSubmitButton < intTotalSubmitButtons; intCurrentSubmitButton++ )
    }   // if ( oPostButtons.length >= ARRAY_NOT_EMPTY )
}   // function AdjustButtonProperties


function GetNotesList ( )
{
    const strMethodName   = LLCommon.GetNameOfCurrentFunction ( );

    const docNoteList     = document.getElementById ( PICK_LIST_NOTES );

    debugger;

    if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
    {
        if ( docNoteList !== null && docNoteList.selectedIndex === ARRAY_INVALID_INDEX )
        {
            const strNotesList      = _LeadLifeJSHelpers.STTLeadId === NO_LEAD_ID_YET
                                      ? [ ]
                                      : LLCommon.DoAjax ( 'GetNotesList' ,
                                                          'GET' ,
                                                          {
                                                            'LeadId'          : _LeadLifeJSHelpers.STTLeadId ,
                                                            '_userid'         : _userid ,
                                                            'tzOffsetMinutes' : _LeadLifeJSHelpers.UtcOffsetMinutes ,
                                                            'Limit'           : 10
                                                          } );

            if ( strNotesList.length > EMPTY_STRING_LENGTH )
            {
                const aoNotesList   = strNotesList.split ( LOGICAL_NEGATE );
                const intNotesCount = aoNotesList.length;

                if ( intNotesCount > NUMERIC_ZERO )
                {
                    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strMethodName + ': Lead ID = ' + _LeadLifeJSHelpers.STTLeadId + ', Notes Count = ' + intNotesCount );

                    var aoNoteDetails       = [ ];
                    _aNoteRecordingUris     = [ ];

                    for ( var intNoteIndex = ARRAY_FIRST_ELEMENT;
                              intNoteIndex < intNotesCount;
                              intNoteIndex++ )
                    {
                        const astrValues    = aoNotesList [ intNoteIndex ].split ( PIPE_CHAR_SPLIT_MATCH );

                        if ( astrValues.length === NOTE_ITEM_COUNT )
                        {
                            aoNoteDetails.push({
                                NoteId       : astrValues [ ARRAY_FIRST_ELEMENT  ] ,
                                NoteTime     : astrValues [ ARRAY_SECOND_ELEMENT ]
                            });
                            _aNoteRecordingUris.push ( astrValues [ ARRAY_THIRD_ELEMENT ] );
                        }   // TRUE (anticipated outcome) block, if ( astrValues.length === NOTE_ITEM_COUNT )
                        else
                        {
                            LLCommon.LogException ( 'SalesTalk internal _objAPI ' + strMethodName + ' received an invalid note index item for lead ID = ' + _LeadLifeJSHelpers.STTLeadId + ', Actual count from split at character ' + QUOTE_SINGLE + EQUALS_CHAR + QUOTE_SINGLE + ' = ' + astrValues.length + ', expected split count = ' + NOTE_ITEM_COUNT );
                            return ARRAY_INVALID_INDEX;
                        }   // FALSE (unanticipated outcome) block, if ( astrValues.length === NOTE_ITEM_COUNT )
                    }   // for ( var intNoteIndex = ARRAY_FIRST_ELEMENT; intNoteIndex < intNotesCount; intNoteIndex++ )

                    if ( aoNoteDetails.length === intNotesCount )
                    {
                        for ( var intCurrentNote = ARRAY_FIRST_ELEMENT;
                                  intCurrentNote < intNotesCount;
                                  intCurrentNote++ )
                        {
                            var docNoteItem         = document.createElement ( 'option' );

                            docNoteItem.value       = aoNoteDetails [ intCurrentNote ].NoteId;
                            docNoteItem.label       = intCurrentNote === ARRAY_FIRST_ELEMENT
                                                                  ? 'Latest (' + aoNoteDetails [ intCurrentNote ].NoteTime + ')'
                                                                  : aoNoteDetails [ intCurrentNote ].NoteTime;
                            docNoteItem.selected    = intCurrentNote === ARRAY_FIRST_ELEMENT
                                                                  ? true
                                                                  : false;

                            docNoteList.appendChild ( docNoteItem );
                        }   // for ( var intCurrentWord = ARRAY_FIRST_ELEMENT; intCurrentWord < intNotesCount; intCurrentWord++ )

                        LLCommon.ShowOrHideElement ( docNoteList ,
                                                     LLCommon.ELEMENT_SHOW );
                        return docNoteList.value;
                    }   // TRUE (anticipated outcome) block, if ( aoNoteDetails.length === intNotesCount )
                    else
                    {
                        LLCommon.LogException ( 'SalesTalk internal _objAPI ' + strMethodName + ' notes array count mismatch - actual count = ' + aoNoteDetails.length + ', expected count = ' + intNotesCount );
                        return ARRAY_INVALID_INDEX;
                    }   // FALSE (unanticipated outcome) block, if ( aoNoteDetails.length === intNotesCount )
                }   // TRUE (anticipated outcome) block, if ( intNotesCount > NUMERIC_ZERO )
                else
                {
                    LLCommon.Trace ( 'SalesTalk internal _objAPI ' + strMethodName + ' found no notes for lead ID ' + _LeadLifeJSHelpers.STTLeadId );
                    return EMPTY_STRING;
                }   // FALSE (unanticipated outcome) block, if ( intNotesCount > NUMERIC_ZERO )
            }   // TRUE (anticipated outcome) block, if ( strNotesList.length > EMPTY_STRING_LENGTH )
            else
            {
                LLCommon.Trace ( 'SalesTalk internal _objAPI ' + strMethodName + ' found no notes for lead ID ' + _LeadLifeJSHelpers.STTLeadId );
                return EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( strNotesList.length > EMPTY_STRING_LENGTH )
        }   // TRUE (anticipated outome on first time through after lead ID is identified) block, if ( docNoteList !== null && docNoteList.selectedIndex === ARRAY_INVALID_INDEX )
        else
        {
            if ( docNoteList !== null )
            {
                LLCommon.ShowOrHideElement ( docNoteList ,
                                             docNoteList.selectedIndex > ARRAY_INVALID_INDEX );
                return docNoteList.value;                       // When the list is empty, the value is the empty string.
            }   // TRUE (anticipated outcome) block, if ( docTranscriptList !== null )
            else
            {
                LLCommon.LogException ( strMethodName + ': The current form is devoid of a container that can hold a list of Notes.' );
                return EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( docTranscriptList !== null )
        }   // FALSE (anticipated outcome of subsquent passes after a lead is identified and all passes in the absence of a Notes list) block, if ( docNoteList !== null && docNoteList.selectedIndex === ARRAY_INVALID_INDEX )
    }   // TRUE (anticipated outcome) block, if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
    else
    {
        LLCommon.Trace ( strMethodName + ': Lead ID is absent. Therefore, searching for transcripts is infeasible.' )
        return EMPTY_STRING;
    }   // FALSE (unanticipated outcome) block, if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
}   // function GetNotesList


function GetTranscriptList ( )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    const docTranscriptList     = document.getElementById ( PICK_LIST_TRANSCRIPTS );

    debugger;

    if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
    {
        if ( docTranscriptList !== null && docTranscriptList.selectedIndex === ARRAY_INVALID_INDEX )
        {
            _aW2ARecordingUris  = [ ];
            const aoTranscripts = _LeadLifeJSHelpers.STTLeadId === NO_LEAD_ID_YET
                                  ? [ ]
                                  : LLCommon.DoAjax ( 'GetTranscriptList' ,
                                                      'GET' ,
                                                      {
                                                        'LeadId'          : _LeadLifeJSHelpers.STTLeadId ,
                                                        'tzOffsetMinutes' : _LeadLifeJSHelpers.UtcOffsetMinutes ,
                                                        'Limit'           : 10
                                                      } );

            if ( Array.isArray ( aoTranscripts ) )
            {
                const intOptCnt = aoTranscripts.length;

                if ( intOptCnt > _LeadLifeJSHelpers.NUMERIC_ZERO )
                {
                    for ( var intCurrentTranscript = ARRAY_FIRST_ELEMENT;
                              intCurrentTranscript < intOptCnt;
                              intCurrentTranscript++ )
                    {
                        var docTranscriptItem       = document.createElement ( 'option' );

                        docTranscriptItem.value     = aoTranscripts [ intCurrentTranscript ].TranscriptURL;
                        docTranscriptItem.label     = intCurrentTranscript === ARRAY_FIRST_ELEMENT
                                                      ? 'Latest (' + aoTranscripts [ intCurrentTranscript ].CreatedDate + ')'
                                                      : aoTranscripts [ intCurrentTranscript ].CreatedDate;
                        docTranscriptItem.selected  = intCurrentTranscript === ARRAY_FIRST_ELEMENT
                                                      ? true
                                                      : false;
                        _aW2ARecordingUris.push ( aoTranscripts [ intCurrentTranscript ].RecordingLink );

                        docTranscriptList.appendChild ( docTranscriptItem );
                    }   // for ( var intCurrentWord = ARRAY_FIRST_ELEMENT; intCurrentWord < intOptCnt; intCurrentWord++ )

                    LLCommon.ShowOrHideElement ( docTranscriptList ,
                                                 LLCommon.ELEMENT_SHOW );
                    return docTranscriptList.value;
                }   // TRUE (anticipated outcome) block, if ( intOptCnt > _LeadLifeJSHelpers.NUMERIC_ZERO )
                else
                {
                    LLCommon.LogException ( strMethodName + ': Lead ID ' + _LeadLifeJSHelpers.STTLeadId + ' has no Transcripts.' );
                    return EMPTY_STRING;
                }   // FALSE (unanticipated outcome) block, if ( intOptCnt > _LeadLifeJSHelpers.NUMERIC_ZERO )
            }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aoTranscripts ) )
            else
            {
                LLCommon.LogException ( 'SalesTalk internal _objAPI ' + strMethodName + ' returned the following error message: ' + aoTranscripts );
                return ARRAY_INVALID_INDEX;
            }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aoTranscripts ) )
        }   // TRUE (The list of transcripts is uninitialized.) block, if ( docTranscriptList !== null && docTranscriptList.selectedIndex === ARRAY_INVALID_INDEX )
        else
        {
            if ( docTranscriptList !== null )
            {
                LLCommon.ShowOrHideElement ( docTranscriptList ,
                                             docTranscriptList.selectedIndex > ARRAY_INVALID_INDEX );
                return docTranscriptList.value;                 // When the list is empty, the value is the empty string.
            }   // TRUE (anticipated outcome) block, if ( docTranscriptList !== null )
            else
            {
                LLCommon.LogException ( strMethodName + ': The current form is devoid of a container that can hold a list of transcripts.' );
                return EMPTY_STRING;
            }   // FALSE (unanticipated outcome) block, if ( docTranscriptList !== null )
        }   // FALSE (Either the list of transcripts is initialized or there is no container on this form.)  block, if ( docTranscriptList !== null && docTranscriptList.selectedIndex === ARRAY_INVALID_INDEX )
    }   // TRUE (anticipated outcome) block, if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
    else
    {
        LLCommon.Trace ( strMethodName + ': Lead ID is absent. Therefore, searching for transcripts is infeasible.' )
        return EMPTY_STRING;
    }   // FALSE (unanticipated outcome) block, if ( _LeadLifeJSHelpers.STTLeadId > NO_LEAD_ID )
}   // function GetTranscriptList


function Click2Note ( )
{
    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    const W2A_INSTRUCTION_BOX           = 'W2A_InstructionBox';                 // CSS selector for styling of instruction box
    const W2A_LIST_ITEM                 = 'W2A_ListItem';                       // I found a need for what began as a dummy CSS selector for use with JS QuerySelectorAll.
    const W2A_KEYWORD_SELECTED          = 'STT_WhiteOnStoplightGreen'
    const W2A_KEYWORDS_DISCARDED        = 'STT_WhiteOnStoplightRed'

    function ToggleKeyWordSelection ( poEvent )
    {
        const docKeywordTD = poEvent.currentTarget;
        docKeywordTD.classList.toggle ( W2A_KEYWORD_SELECTED );
    }   // function ToggleKeyWordSelection

    debugger;

    try
    {
        const aobjClick2NoteKeyWord     = LLCommon.DoAjax ( 'Click2NoteGetPickList',
                                                            'GET',
                                                            {
                                                                'LeadId'   : LLCommon.LeadId,
                                                                'TenantId' : LLCommon.TenantId,
                                                                'DomainId' : LLCommon.DomainId,
                                                                'UserId'   : LLCommon.UserId
                                                            });

        if ( Array.isArray ( aobjClick2NoteKeyWord ) && aobjClick2NoteKeyWord.length > ARRAY_IS_EMPTY )
        {
            const docTable              = document.createElement ( 'table' );
            $( '#click2noteContainer' ).addClass ( 'W2A_ScrollContainer' );

            const docInstructionBtnCont = document.createElement ( 'tr' );
            const docInstructionBtnBox  = document.createElement ( 'td' );

            const docInstructionShowBtn = document.createElement ( 'button');
            docInstructionShowBtn.classList.add ( 'STT_Subtle' );
            docInstructionShowBtn.id        = 'ToggleClick2NoteInstructionBox'
            docInstructionShowBtn.innerText = 'Click to display instructions.';
            docInstructionShowBtn.type      = 'button';
            docInstructionShowBtn.addEventListener ( 'click', function ( )
            {
                ToggleFormSection ( this );
            });

            docInstructionBtnBox.appendChild ( docInstructionShowBtn );
            docInstructionBtnCont.appendChild ( docInstructionBtnBox );
            docTable.appendChild ( docInstructionBtnCont );

            const docInstructionBox     = document.createElement ( 'tr' );
            const docInstructionCont    = document.createElement ( 'td' );

            docInstructionCont.classList.add ( W2A_INSTRUCTION_BOX );
            docInstructionCont.classList.add ( LLCommon.STT_HideElement );
            docInstructionCont.id       = 'Click2NoteInstructionBox';

            //  ----------------------------------------------------------------
            //  Add the first paragraph, creating variable docInstructParagraph
            //  that we'll reuse by assigning it to a new Paragraph element.
            //  Once it is appended to another element, that element becomes
            //  responsible for it, and the script variable, being only a
            //  handle, can be reused as the target of another assignment
            //  statement.
            //  ----------------------------------------------------------------

            let   docInstructPara       = document.createElement ( 'p' );

            docInstructPara.innerHTML   = 'Click one or more keywords, which turn <span class="' + W2A_KEYWORD_SELECTED +'">green</span> when clicked. If you click one by mistake, click it again to deselect it.';
            docInstructPara.classList.add ( W2A_INSTRUCTION_BOX );
            docInstructionCont.appendChild ( docInstructPara );

            docInstructPara             = document.createElement ( 'p' );
            docInstructPara.innerHTML   = 'When you finish your selections, click the <span class="' + W2A_KEYWORD_SELECTED +'">Apply</span> button to post them all at once.';
            docInstructPara.classList.add ( W2A_INSTRUCTION_BOX );
            docInstructionCont.appendChild ( docInstructPara );

            docInstructPara             = document.createElement ( 'p' );
            docInstructPara.innerHTML   = 'If you change your mind and want to discard ALL selections, click the <span class="' + W2A_KEYWORDS_DISCARDED + '">Cancel</span> button to discard everything.';
            docInstructPara.classList.add ( W2A_INSTRUCTION_BOX );
            docInstructionCont.appendChild ( docInstructPara );

            docInstructionBox.appendChild ( docInstructionCont );
            docTable.appendChild ( docInstructionBox );

            //  ----------------------------------------------------------------
            //	Each keyword goes into its own one-celled row, and each cell is
            //	attached to global event listener ToggleKeyWordSelection.
            //  ----------------------------------------------------------------

            const intKeyWordCount = aobjClick2NoteKeyWord.length;

            for ( var intKeyWordIndex = ARRAY_FIRST_ELEMENT;
                      intKeyWordIndex < intKeyWordCount;
                      intKeyWordIndex++ )
            {
                const docTableRow                       = document.createElement ( 'tr' );
                const docKeyWordCell                    = document.createElement ( 'td' );

                docKeyWordCell.textContent              = aobjClick2NoteKeyWord [ intKeyWordIndex ].KeyWord;
                docKeyWordCell.dataset.talkingPointId   = aobjClick2NoteKeyWord [ intKeyWordIndex ].TalkingPointId;

                docKeyWordCell.classList.add ( W2A_LIST_ITEM );
                docKeyWordCell.addEventListener ( 'click',
                                                  ToggleKeyWordSelection );

                docTableRow.appendChild ( docKeyWordCell );
                docTable.appendChild ( docTableRow );
            }   // for ( var intKeyWordIndex = ARRAY_FIRST_ELEMENT; intKeyWordIndex < intKeyWordCount; intKeyWordIndex++ )

            var box = bootbox.dialog({
                message : '<div id="click2noteContainer"></div>',
                title   : 'Click2Note',
                size    : 'large',
                backdrop: 'static',     // With the backdrop present clicks outside the modal are blocked, nor can such a click cause it to dismiss itself.
                closeButton: false,     // This flag removes the “X” button, so that the only ways out are by the buttons that we provide.
                buttons : {
                    ok :
                    {
                        label     : 'Apply',
                        className : 'btn-success',
                        callback  : function ( )
                        {
                            const docSelectedKeyWords = docTable.querySelectorAll ( 'td.' + W2A_KEYWORD_SELECTED );
                            let intKeyWordCount = NUMERIC_ZERO;

                            if ( docSelectedKeyWords.length > ARRAY_IS_EMPTY )
                            {
                                let strKeyWordList = 'LeadId='   + LLCommon.LeadId   + LOGICAL_NEGATE
                                                   + 'DomainId=' + LLCommon.DomainId + LOGICAL_NEGATE
                                                   + 'TenantId=' + LLCommon.TenantId + LOGICAL_NEGATE
                                                   + 'UserId='   + LLCommon.UserId;

                                for ( const docKeyWordCell of docSelectedKeyWords )
                                {
                                    strKeyWordList += LOGICAL_NEGATE
                                                    + docKeyWordCell.innerText
                                                    + EQUALS_CHAR
                                                    + Number ( docKeyWordCell.dataset.talkingPointId );
                                    intKeyWordCount++;
                                }   // for ( const docKeyWordCell of docSelectedKeyWords )

                                console.log ( strMethodName + ': KeyWords to Return = ' + intKeyWordCount + ', as follows: ' + strKeyWordList );

                                const strOutcome = LLCommon.DoAjax ( 'Click2NotePutSelectedKeyWords',
                                                                     'POST',
                                                                     {
                                                                         'pstrLeadInfoAndKeyWords': strKeyWordList
                                                                     });

                                if ( strOutcome.length > EMPTY_STRING_LENGTH )
                                {
                                    console.log ( strMethodName + ': Click2NotePutSelectedKeyWords Exception Message = ' + strOutcome );
                                    alert ( 'Click2NotePutSelectedKeyWords reported an exception. Please contact support.', 'native' );
                                }   // if ( strOutcome.length > EMPTY_STRING_LENGTH )
                            }   // if ( docSelectedKeyWords.length > ARRAY_IS_EMPTY )

                            LLCommon.ToastFactory.show ( intKeyWordCount + ' Keywords recorded',
                                                         LLCommon.getClassStyles ( W2A_KEYWORD_SELECTED ) );
                        }
                    },
                    cancel :
                    {
                        label     : 'Cancel',
                        className : 'btn-danger',
                        callback  : function ( )
                        {
                            const docSelectedKeyWords = docTable.querySelectorAll ( 'td.' + W2A_KEYWORD_SELECTED );
                            LLCommon.ToastFactory.show ( docSelectedKeyWords.length + ' Keywords abandoned',
                                                         LLCommon.getClassStyles ( W2A_KEYWORDS_DISCARDED ) );
                        }
                    }
                }
            });

            //  --------------------------------------------------------------------
            //  Appending the table into the modal content container preserves the
            //  event hooks that are registered to its detail cells.
            //  --------------------------------------------------------------------

            const docClick2noteContainer = document.querySelector ( '#click2noteContainer' );
            docClick2noteContainer.classList.add ( 'W2A_ScrollContainer' );
            docClick2noteContainer.appendChild ( docTable );

            //  --------------------------------------------------------------------
            //  We need a handle to the dialog container itself to apply custom CSS.
            //  We apply vertical centering without window math by adding the
            //  Bootstrap `modal-dialog-centered` CSS selector, then set the border
            //  radius to 10px to make the corners rounded, set the width to auto,
            //  and enforce a maxiumn width of 500 px.
            //  --------------------------------------------------------------------

            var dialog = box.find ( '.modal-dialog' );

            dialog.addClass ( 'modal-dialog-right' );
            dialog.css({
                'border-radius' : '10px',
                'width'         : 'auto',
                'max-width'     : '500px'
            });
        }   // TRUE (anticipated outcome) block, if ( Array.isArray ( aobjClick2NoteKeyWord ) && aobjClick2NoteKeyWord.length > ARRAY_IS_EMPTY )
        else
        {
            alert ( 'There are NO Click2Note keywords defined. Please contact support.' , 'native' );
            throw new Error ( strMethodName + ': SalesTalk API routine "Click2NoteGetPickList" returned the empty set.' );
        }   // FALSE (unanticipated outcome) block, if ( Array.isArray ( aobjClick2NoteKeyWord ) && aobjClick2NoteKeyWord.length > ARRAY_IS_EMPTY )
    }
    catch ( ex )
    {
        LLCommon.LogException ( strMethodName + ': Attempting to execute the Click2Note feature. See exception log for details.' ,
                                ex );
    }
}   // function Click2Note


function OtherRecorderContainer ( pstrSelectedMedia )
{
    return ( pstrSelectedMedia === MEDIA_IS_VIDEO ? MEDIA_IS_AUDIO : MEDIA_IS_VIDEO );
}   // function OtherRecorderContainer


function ResetThisForm ( event )
{
    const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': The ResetThisForm event procedure is starting.' );
    AdjustButtonProperties ( BUTTON_STATE_VISIBLE );
    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': The ResetThisForm event procedure is handing off to the global reload event listener.' );
    LLCommon.ShowResetAlert ( );        // As of 31 July 2025, all alerts in this method are native.
    LLCommon.ResetThisForm ( event );

    return false;
}   // function ResetThisForm


function ShowConversationInsights ( )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    const ShowCINow             = ( pstrWindowName ) =>
    {
        const strMethodName     = LLCommon.GetNameOfCurrentFunction ( );

        LLCommon._FSuppressCRMUpdateAlert = true;
        DoUpdateCrmNow ( false );
        const strStorySoFarUrl  = LLCommon.AjaxUrlPrefix + 'Sales?leadId=' + _leadid;

        LLCommon.Trace (    Words2Actions_Recorder_Forms_SCRIPTSOURCE
                         + ': method ' + strMethodName
                         + ' opening URL ' + strStorySoFarUrl
                         + ' in a new window named ' + strMethodName );

        if ( pstrWindowName )
        {
            window.open ( strStorySoFarUrl , pstrWindowName )
        }   // TRUE (Open in a new _named_ window.) block, if ( pstrWindowName )
        else
        {
            window.location.href    = strStorySoFarUrl;    // In this case, navigation is much safer and more dependable.
        }   // FALSE (Redirect the location of the current window.) block, if ( pstrWindowName )
    }

    if ( _pagenameSource !== undefined && _pagename !== null && _leadidSource !== undefined && _leadid > NO_LEAD_ID_YET && GetParameterFromURLFormOrLocalStorage ( 'CI' , EMPTY_STRING ).toLowerCase ( ) === 'true' )
    {
        if ( window.CIButtonNoWarning )
        {
            LLCommon.Trace ( 'CI Window opening without a net' , strMethodName );
            ShowCINow ( strMethodName );
        }   // TRUE (User wants to dispense with the warning.) block, if ( window.CIButtonNoWarning )
        else
        {
            var box = bootbox.dialog({
                message:   '<div style="height:100%;width:100%;">'
                         + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #ff0000; background-color: #ffffff; text-align: center;">'
                         + '        Attention!!!'
                         + '    </p>'
                         + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '        We will update the CRM with all changes made on the Words2Action page and close this page to prevent accidental overrides when viewing the Story So Far.'
                         + '    </p>'
                         + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '        To reopen the Words to Action page, follow these steps:'
                         + '    </p>'
                         + '    <ol style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Close the CI page (crucial for preventing data corruption).'
                         + '        </li>'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Return to the Wise Agent page where you left off.'
                         + '        </li>'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Click Cancel in the pop-up window to resume contact viewing.'
                         + '        </li>'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Click the pencil icon, just as you did initially.'
                         + '        </li>'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Proceed to Words2Action.'
                         + '        </li>'
                         + '    </ol>'
                         + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '        &nbsp;'
                         + '    </p>'
                         + '    <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Click "Go to Conversation Insights" to proceed, <span style="color: #ff0000;">OR'
                         + '        </li>'
                         + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                         + '            Click "Cancel" to remain on the current page.'
                         + '        </li>'
                         + '    </ul>'
                         + '</div>',
                title: 'Go to Conversation Insights',
                size: "large",
                buttons: {
                    ok: {
                            label: "Go to Conversation Insights!",
                            className: "btn-success",
                            callback: function ( )
                            {
                                const docGoToCIMessage1     = document.getElementById ( 'GoToConversationIntelligence_Message_1' )
                                const docGoToCIMessage2     = document.getElementById ( 'GoToConversationIntelligence_Message_2' )
                                const intW2ACatchUpDelay    = parseInt ( LLCommon.DoAjax ( 'GetByMonikorFirst',
                                                                                           'GET',
                                                                                           {
                                                                                               'monikor'      : 'W2ACatchUpDelay',
                                                                                               'tenantId'     : _tenantid,
                                                                                               'domainId'     : _domainid,
                                                                                               'defaultValue' : CI_CATCH_UP_DELAY_DEFAULT
                                                                                           } ) );
                                ShowCINow ( );
                            }
                    },
                    cancel: {
                        label: "Cancel",
                        className: "btn-danger",
                        callback: function ( )
                        {
                            alert ( 'Staying right here!' , 'native' );
                        }
                    }
                }
            });
        }   // FALSE (User prefers safety for their data.) block, if ( window.CIButtonNoWarning )
    }   // TRUE (The client is subscribed.) block, if ( _pagenameSource !== undefined && _pagename !== null && _leadidSource !== undefined && _leadid > NO_LEAD_ID_YET && GetParameterFromURLFormOrLocalStorage ( 'CI' , EMPTY_STRING ).toLowerCase ( ) === 'true' )
    else
    {
        var box = bootbox.dialog({
            message:   '<div style="height:100%;width:100%;">'
                     + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '        The Conversation Insights tool offers a comprehensive suite of features to enhance your customer interactions and sales processes:'
                     + '    </p>'
                     + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; font-weight: 700; text-align: left;">'
                     + '        Story So Far'
                     + '    </p>'
                     + '    <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Provides a chronological overview of all interactions to date and Includes actual dates and duration of each interaction'
                     + '        </li>'
                     + '    </ul>'
                     + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; font-weight: 700; text-align: left;">'
                     + '        Call Analysis'
                     + '    </p>'
                     + '    <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Key points discussed during recorded calls'
                     + '        </li>'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Direct links to specific moments in call recordings'
                     + '        </li>'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Access to call transcripts'
                     + '        </li>'
                     + '    </ul>'
                     + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; font-weight: 700; text-align: left;">'
                     + '        Email Integration'
                     + '    </p>'
                     + '    <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Displays all emails sent to or received from prospects'
                     + '        </li>'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Click2Note&trade;'
                     + '        </li>'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Single click to update key fields'
                     + '        </li>'
                     + '    </ul>'
                     + '    <p style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; font-weight: 700; text-align: left;">'
                     + '        Time-Saving Features'
                     + '    </p>'
                     + '    <ul style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '        <li style="font-family: Arial, Helvetica, sans-serif; font-size: 18pt; color: #000000; background-color: #ffffff; text-align: left;">'
                     + '            Quick access to relevant information'
                     + '        </li>'
                     + '    </ul>'
                     + '</div>',
            title   : 'About Conversation Insights',
            size    : 'large',
            buttons : {
                ok  : {
                    label     : 'Cancel',
                    className : 'btn-danger',
                    callback  : function ( )
                    {
                        alert ( 'Staying right here!' , 'native' );
                    }
                }
            }
        });
    }

    return false;
}   // function ShowConversationInsights


function ShowMyRecentCalls ( )
{
    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName + ' begin' );

    debugger;

    try
    {
        const strDirectoryNameFromPath  = location.pathname.substring ( SUBSTRING_FIRST_CHAR , LLCommon.OrdinalFromIndex ( location.pathname.lastIndexOf ( PATH_SEPARATOR_CHAR ) ) );
        const strAbsoluteHttpPath       = location.origin + strDirectoryNameFromPath + 'Agent_Recent_Phone_Calls.html';
        const strMyRecentCallsUrl       = strAbsoluteHttpPath + '?userid=' + _userid;

        console.log ( 'location.origin          = ' + location.origin );
        console.log ( 'location.pathname        = ' + location.pathname );

        console.log ( 'strDirectoryNameFromPath = ' + strDirectoryNameFromPath );
        console.log ( 'strAbsoluteHttpPath      = ' + strAbsoluteHttpPath );
        console.log ( 'strMyRecentCallsUrl      = ' + strMyRecentCallsUrl );

        window.open ( strMyRecentCallsUrl , '_self' );
    }
    catch ( ex )
    {
        LLCommon.LogException ( strMethodName + ': ' + ex.message , ex );
    }
}   // function ShowMyRecentCalls


function ShowOtherW2AForms ( )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName + ' begin' );

    debugger;

    try
    {
        const aoOtherForms      = JSON.parse ( LLCommon.DoAjax ( 'GetMyViewTemplateList',
                                                                 'GET',
                                                                 {
                                                                    'DomainId'        : _domainid ,
                                                                    'tzOffsetMinutes' : _LeadLifeJSHelpers.UtcOffsetMinutes
                                                                 } ) );
        const intFormCount      = aoOtherForms.KeyValuePairsList.length;

        if ( intFormCount > 1 )
        {
            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName + ' Form Count = ' + intFormCount );

            const docMsg4Box            = document.createElement ( 'div' );
            docMsg4Box.id               = 'OtherFormList';
            docMsg4Box.style            = 'height:300px; width:600px;';

            //  ----------------------------------------------------------------
            //  Put the instructions inside a box.
            //  ----------------------------------------------------------------

            const docPromptCont         = document.createElement ( 'div' );
            docPromptCont.style         = 'display: flex; justify-content: center;';
            const docPromptTbl          = document.createElement ( 'table' );
            docPromptTbl.style          = 'width: 75%;';
            const docPromptRow          = document.createElement ( 'tr' );
            const docPromptCell         = document.createElement ( 'td' );

            docPromptCell.style         = "border: 3px solid; border-collapse: collapse; border-color: #0B0560; padding: 0.5em; background-color: #5e92ba;"
            docPromptCell.innerHTML     = '<span style="color: #ffffff;">Click on a <span style="font-weight: bold;">Form Name</span> to display it. To prevent confusion, the new form will replace the current one.</span>';

            docPromptRow.appendChild  ( docPromptCell );
            docPromptTbl.appendChild  ( docPromptRow );
            docPromptCont.appendChild ( docPromptTbl );
            docMsg4Box.appendChild    ( docPromptCont );

            //  ----------------------------------------------------------------
            //  Label the columns.
            //  ----------------------------------------------------------------

            const docTable4Box  = document.createElement ( 'table' );
            const docTableHead  = document.createElement ( 'thead' );

            const docCol1L      = document.createElement ( 'th' );
            docCol1L.innerHTML  = '<span style="font-weight: bold;">Name</span>';
            docTableHead.appendChild ( docCol1L );

            const docCol12      = document.createElement ( 'th' );
            docCol12.innerHTML  = '<span style="font-weight: bold;">Created Date</span>';
            docTableHead.appendChild ( docCol12 );

            const docCol13      = document.createElement ( 'th' );
            docCol13.innerHTML  = '<span style="font-weight: bold;">Updated</span>';
            docTableHead.appendChild ( docCol13 );

            docTable4Box.appendChild ( docTableHead );

            const docActivity   = document.createElement ( 'tbody' );
            docActivity.id      = 'OtherFormDetail';
            var   intJ          = ARRAY_FIRST_ELEMENT;

            //  ----------------------------------------------------------------
            //  Fill the table.
            //  ----------------------------------------------------------------

            for ( var intCurrentForm = ARRAY_FIRST_ELEMENT;
                      intCurrentForm < intFormCount;
                      intCurrentForm++ )
            {
                if ( aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue !== _pagename )
                {
                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName
                                     + ' Form Index ' + intCurrentForm
                                     + ': UserDataView (Form Name) = ' + aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue
                                     + ', CreatedDt = '                + aoOtherForms.KeyValuePairsList [ intCurrentForm ].CreatedDt.DisplayDate
                                     + ', LastModDt = '                + aoOtherForms.KeyValuePairsList [ intCurrentForm ].LastModDt.DisplayDate
                                     + ' INCLUDED' );
                    const docNewRow     = document.createElement ( 'tr' );
                    docNewRow.id        = 'Form_Item_' + ( LLCommon.OrdinalFromIndex ( intCurrentForm ) );

                    //  --------------------------------------------------------
                    //  Form (Page) Name:
                    //  --------------------------------------------------------

                    const docFormName           = document.createElement ( 'td' );
                    docFormName.id              = 'ActivateOtherForm_' + intJ;
                    docFormName.name            = aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue;
                    docFormName.innerHTML       = '<button type="button" class="TranscriptReview_BlueTheme" title="Click this button to display the form whose name appears on the button face." onclick="DisplayNewForm ( this );"><span class="W2A_Recorder_Button_Small_Text">' + aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue + '</span></button>';
                    docNewRow.appendChild ( docFormName );

                    //  --------------------------------------------------------
                    //  Form Created Date:
                    //  --------------------------------------------------------

                    const docFormCreatedDt      = document.createElement ( 'td' );
                    docFormCreatedDt.innerHTML  = aoOtherForms.KeyValuePairsList [ intCurrentForm ].CreatedDt.DisplayDate;
                    docNewRow.appendChild ( docFormCreatedDt );

                    //  --------------------------------------------------------
                    //  Form Last Modified Date:
                    //  --------------------------------------------------------

                    const docFormLastModDt      = document.createElement ( 'td' );
                    docFormLastModDt.innerHTML  = aoOtherForms.KeyValuePairsList [ intCurrentForm ].LastModDt.DisplayDate;
                    docNewRow.appendChild ( docFormLastModDt );

                    docActivity.appendChild ( docNewRow );
                }   // TRUE (This entry represents a different form.) block, if ( aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue !== _pagename )
                else
                {
                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName
                                     + ' Form Index ' + intCurrentForm
                                     + ': UserDataView (Form Name) = ' + aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue
                                     + ', CreatedDt = '                + aoOtherForms.KeyValuePairsList [ intCurrentForm ].CreatedDt.DisplayDate
                                     + ', LastModDt = '                + aoOtherForms.KeyValuePairsList [ intCurrentForm ].LastModDt.DisplayDate
                                     + ' SKIPPED' );
                }   // FALSE (This entry represents the forma that is currently on display.) block, if ( aoOtherForms.KeyValuePairsList [ intCurrentForm ].UserDataView.FieldValue !== _pagename )
            }   // for ( var intCurrentForm = ARRAY_FIRST_ELEMENT; intCurrentForm < intFormCount; intCurrentForm++ )

            //  ----------------------------------------------------------------
            //  Append the table body (tbody) tag to the DIVision tag, then put
            //  the whole thing into a BootBox, and give the box object global
            //  scope so that function ThisRowWasSelected can hide it.
            //  ----------------------------------------------------------------

            docTable4Box.appendChild ( docActivity );
            docMsg4Box.appendChild ( docTable4Box );
            const box           = bootbox.dialog ({
                                                    title   : 'Other Words2Actions Forms',
                                                    message : docMsg4Box.outerHTML,
                                                    buttons: {
                                                                cancel: {
                                                                    label     : "Close",
                                                                    className : "btn-danger",
                                                                    callback  : function ( )
                                                                    {
                                                                    }
                                                                }
                                                             }
                                                 });
        }   // TRUE (anticipated outcome) block, if ( intFormCount > 1 )
        else
        {
            alert ( 'There are no other forms to display.' , 'native' );
        }   // FALSE (unanticipated outcome) block, if ( intFormCount > 1 )
    }
    catch ( ex )
    {
        LLCommon.LogException ( strMethodName + ': ' + ex.message + ' See exception log for details.' ,
                                ex );
    }
}   // function ShowOtherW2AForms


function ShowRecentActivity ( )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': method ' + strMethodName
                     + ', LLCommon.DialerLogin = ' + LLCommon.DialerLogin );

    debugger;

    try
    {   // 2024/11/20 19:22:17 - DAGray - Save trips to the server for when we really need them.
        if ( _useridSource === SRC_IS_UNKNOWN )
        {
            throw new Error ( strMethodName + ': Processing cannot proceed because UserId is undefined.' );
        }   // if ( _useridSource === SRC_IS_UNKNOWN )

        const aoActivity        = LLCommon.DoAjax ( 'GetTranscriptList',
                                                    'GET',
                                                    {
                                                        'LeadId'          : _userid,
                                                        'tzOffsetMinutes' : _LeadLifeJSHelpers.UtcOffsetMinutes,
                                                        'Limit'           : 100,
                                                        'IdType'          : 'User'
                                                    } );
        const intActivityCount  = aoActivity.length;

        if ( intActivityCount > ARRAY_IS_EMPTY )
        {
            const docMsg4Box            = document.createElement ( 'div' );
            docMsg4Box.id               = 'ActiviytList';
            docMsg4Box.style            = 'height:300px;width:600px;';

            //  ----------------------------------------------------------------
            //  Put the instructions inside a box.
            //  ----------------------------------------------------------------

            const docPromptCont         = document.createElement ( 'div' );
            docPromptCont.style         = 'display: flex; justify-content: center;';
            const docPromptTbl          = document.createElement ( 'table' );
            docPromptTbl.style          = 'width: 75%;';
            const docPromptRow          = document.createElement ( 'tr' );
            const docPromptCell         = document.createElement ( 'td' );

            docPromptCell.style         = "border: 3px solid; border-collapse: collapse; border-color: #0B0560; padding: 0.5em; background-color: #5e92ba;"
            docPromptCell.innerHTML     = '<span style="color: #ffffff;">Click <span style="font-weight: bold;">anywhere in a row</span> to display its record and notes.</span>';

            docPromptRow.appendChild  ( docPromptCell );
            docPromptTbl.appendChild  ( docPromptRow );
            docPromptCont.appendChild ( docPromptTbl );
            docMsg4Box.appendChild    ( docPromptCont );

            //  ----------------------------------------------------------------
            //  Label the columns.
            //  ----------------------------------------------------------------

            const docTable4Box          = document.createElement ( 'table' );
            const docTableHead          = document.createElement ( 'thead' );

            const docCol1L              = document.createElement ( 'th' );
            docCol1L.innerHTML          = '<span style="font-weight: bold;">Date</span>';
            docCol1L.classList.add ( 'STT_NOWRAP' );
            docTableHead.appendChild ( docCol1L );

            const docCol12              = document.createElement ( 'th' );
            docCol12.innerHTML          = '<span style="font-weight: bold;">Name</span>';
            docCol12.classList.add ( 'STT_NOWRAP' );
            docTableHead.appendChild ( docCol12 );

            const docCol13              = document.createElement ( 'th' );
            docCol13.innerHTML          = '<span style="font-weight: bold;">External System ID</span>';
            docCol13.classList.add ( 'STT_NOWRAP' );
            docTableHead.appendChild ( docCol13 );

            docTable4Box.appendChild ( docTableHead );

            const docActivity           = document.createElement ( 'tbody' );
            docActivity.id              = 'ActivityDetail';
            var   intJ                  = ARRAY_FIRST_ELEMENT;

            //  ----------------------------------------------------------------
            //  Fill the table.
            //  ----------------------------------------------------------------

            aoActivity.forEach ( ( item ) =>
            {
                intJ++;
                LLCommon.Trace (   'Item Index = '      + intJ
                                 + ': Name = '          + item.LastName + ', ' + item.FirstName
                                 + ', ExternalCRMId = ' + item.ExternalCRMId
                                 + ', CreatedDate = '   + item.CreatedDate
                            );

                const docNewRow     = document.createElement ( 'tr' );
                docNewRow.id        = 'Activity_Item_' + intJ;

                //  --------------------------------------------------------
                //  Activity Date:
                //  --------------------------------------------------------

                const docActDt      = document.createElement ( 'td' );
                docActDt.innerHTML  = item.CreatedDate;
                docActDt.classList.add ( 'STT_NOWRAP' );

                docNewRow.classList.add ( 'STT_Cursor_is_Mouse' );
                docNewRow.appendChild ( docActDt );

                //  --------------------------------------------------------
                //  Contact Name:
                //  --------------------------------------------------------

                const docAcName     = document.createElement ( 'td' );
                docAcName.innerHTML = item.LastName + ', ' + item.FirstName;
                docAcName.classList.add ( 'STT_NOWRAP' );
                docNewRow.appendChild ( docAcName );

                //  --------------------------------------------------------
                //  ExternalCRMId:
                //  --------------------------------------------------------

                const docAcId       = document.createElement ( 'td' );
                docAcId.id          = 'ExteernlCrmId_' + intJ;
                docAcId.innerHTML   = item.ExternalCRMId;
                docAcId.classList.add ( 'STT_NOWRAP' );

                docNewRow.appendChild ( docAcId );
                docActivity.appendChild ( docNewRow );
            }); // aoActivity.forEach ( ( item ) =>

            //  ----------------------------------------------------------------
            //  Append the table body (tbody) tag to thee DIVision tag, then put
            //  the whole thing into a BootBox, and give the box objec global
            //  scope so that function ThisRowWasSelected can hide it.
            //  ----------------------------------------------------------------

            docTable4Box.appendChild ( docActivity );
            docMsg4Box.appendChild ( docTable4Box );

            const box                   = bootbox.dialog ({
                                                            title   : 'Your Contacts',
                                                            message : docMsg4Box.outerHTML,
                                                            buttons: {
                                                                cancel: {
                                                                    label     : "Close",
                                                                    className : "btn-danger",
                                                                    callback  : function ( )
                                                                    {
                                                                    }
                                                                }
                                                            }
                                                        }).on('shown.bs.modal', function ( event )
                                                        {
                                                            console.log ( 'Registering click events on each TR inside table ActivityDetail.' );
                                                            debugger;
                                                            //$('ActivityDetail').on( 'click' , 'tr' , null , ThisRowWasSelected );
                                                            const docRowContainer = document.getElementById ( 'ActivityDetail' );

                                                            for ( var intRowIndex = ARRAY_FIRST_ELEMENT;
                                                                      intRowIndex < docRowContainer.childElementCount;
                                                                      intRowIndex++ )
                                                            {
                                                                LLCommon.RegisterClickEventHandler ( docRowContainer.children [ intRowIndex ] ,                 // poElement    = Identify the element to watch for Click events by passing either its element ID or a reference to the DOM element.
                                                                                                     ThisRowWasSelected );                                      // pfnCallback  = This parameter MUST be a function that accepts either no arguments at all or an array of JavaScript objects.
                                                            }   // for ( var intRowIndex = ARRAY_FIRST_ELEMENT; intRowIndex < docRowContainer.childElementCount; intRowIndex++ )

                                                            console.log ( 'Click events registered on each TR inside table ActivityDetail.' );
                                                            debugger;
                                                        });
            window.SearchBox = box;
        }   // TRUE (anticipated outcome) block, if ( intActivityCount > ARRAY_IS_EMPTY )
        else
        {
            alert ( 'You have no transcripts yet.' , 'native' );
        }   // FALSE (unanticipated outcome) block, if ( intActivityCount > ARRAY_IS_EMPTY )
    }
    catch ( ex )
    {
        LLCommon.LogException ( strMethodName + ': ' + ex.message + ' See exception log for details.' ,
                                ex );
    }
}   // function ShowRecentActivity


function StartRecording ( pdocStartButton , pdocStopButton )
{
    //  ------------------------------------------------------------------------
    //  This function gets invoked when the user clicks a "Start Recording"
    //  button.
    //  ------------------------------------------------------------------------

    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    //  ------------------------------------------------------------------------
    //  Stepping through the code has demonstrated that setting the object to
    //  null is necessary to prevent the script attempting to remove an element
    //  from the DOM tree that has already been removed or is yet to be created.
    //
    //  All three of these objects are declared and initialized to null at the
    //  top of the script so that their scope is Window. Any of them that exists
    //  when the start button is activated must be deleted from the Document
    //  Object Model before this routine creates them. Otherwise, duplicates
    //  would be created. Next, the parent element of the start button is made
    //  visible, the start button is hidden, and the stop button is shown.
    //  ------------------------------------------------------------------------

    if ( downloadButton != null )
    {
        downloadButton.remove ( );
        downloadButton          = null;
    }

/*
    if ( uploadButton != null )
    {
        uploadButton.remove ( );
        uploadButton            = null;
    }
*/

    if ( uploadedButton != null )
    {
        uploadedButton.remove ( );
        uploadedButton          = null;
    }

    if ( recordedMedia !== null )
    {
        recordedMedia.remove ( );
        recordedMedia           = null;
    }

    LLCommon.ShowOrHideElement ( pdocStartButton.parentElement ,
                                 LLCommon.ELEMENT_SHOW );         // Since this element is devoid of any CSS attributes, making it visible is redundant.
    LLCommon.ShowOrHideElement ( pdocStartButton ,
                                 LLCommon.ELEMENT_HIDE );
    LLCommon.ShowOrHideElement ( pdocStopButton ,
                                 LLCommon.ELEMENT_SHOW );

    // Access the camera and microphone.
    navigator.mediaDevices.getUserMedia (
        selectedMedia === MEDIA_IS_VIDEO ? videoMediaConstraints : audioMediaConstraints )
        .then ( ( mediaStream ) =>
    {
        //  --------------------------------------------------------------------
        //  Create a new MediaRecorder instance, then make the mediaStream and
        //  mediaRecorder global.
        //  --------------------------------------------------------------------

        const mediaRecorder     = new MediaRecorder ( mediaStream );

        window.mediaStream      = mediaStream;
        window.mediaRecorder    = mediaRecorder;

        mediaRecStarted         = new Date ( );

        mediaRecorder.start ( );

        //  --------------------------------------------------------------------
        //  Whenever (here when the recorder stops recording) data is available,
        //  the MediaRecorder emits a "dataavailable" event with the recorded
        //  media data.
        //  --------------------------------------------------------------------

        mediaRecorder.ondataavailable = ( event ) =>
        {
            // Push the recorded media data to the chunks array
            chunks.push ( event.data );
        };  // mediaRecorder.ondataavailable event listener

        //  --------------------------------------------------------------------
        //  When the MediaRecorder stops recording, it raises a "stop" event.
        //  --------------------------------------------------------------------

        mediaRecorder.onstop = ( ) =>
        {
            const InsertPlaybackControlIntoPage = ( pdocNewTableCell , pdocTableRow , pfVAlignPlaybackCtrls ) =>
            {
                LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Entering named arrow function InsertPlaybackControlIntoPage with pdocNewTableCell ID = "' + pdocNewTableCell.id + '", pdocTableRow ID = "' + pdocTableRow.id + '", pfVAlignPlaybackCtrls = ' + pfVAlignPlaybackCtrls );

                //  ------------------------------------------------------------
                //  The original design spread the buttons across the page by
                //  adding them to a single table row. However, wide pages
                //  should have them spread down the page, requiring a new table
                //  row for each, as was done in the code block below that
                //  creates the PlaybackToolz row. The new table row becomes the
                //  alternate target of the appendChild operation that appends
                //  the cell (TD) to the row (TD) element.
                //
                //  Although insertAdjacentElement works as expected for the
                //  first of a series of elements, inserting two or more
                //  elements causes them to appear in reverse order because the
                //  next element pushes the previous element to its right in the
                //  DOM tree.
                //
                //  To insert two or more elements one after another into the
                //  DOM tree, they must be appended to the parent element.
                //  ------------------------------------------------------------

                if ( pfVAlignPlaybackCtrls )
                {
                    const docLocalBtYard        = document.createElement ( 'tr' );
                    docLocalBtYard.appendChild ( pdocNewTableCell );
                    pdocTableRow.parentElement.appendChild ( docLocalBtYard );
                }   // TRUE (The element lives on a page that is already wide.) block, if ( pfVAlignPlaybackCtrls )
                else
                {
                    pdocTableRow.appendChild ( pdocNewTableCell );
                }   // FALSE (The element lives on a page that has ample room to the right.) block, if ( pfVAlignPlaybackCtrls )
            }   // const InsertPlaybackControlIntoPage = ( pdocNewTableCell , pdocMediaRecorder , pfVAlignPlaybackCtrls ) =>

            debugger;

//            event.preventDefault ( );

            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': Entering mediaRecorder.onstop event listener ' + strMethodName );

            LLCommon.ShowOrHideElement ( pdocStopButton ,
                                         LLCommon.ELEMENT_HIDE );
            LLCommon.ShowOrHideElement ( pdocStartButton ,
                                         LLCommon.ELEMENT_SHOW );

            //  --------------------------------------------------------------------
            //  A Blob is a File like object. In fact, the File interface is based
            //  on Blob. File inherits the Blob interface and expands it to support
            //  the files on the user's system. The Blob constructor takes the chunk
            //  of media data as the first parameter and constructs a Blob of the
            //  type given as the second parameter, after which the chunks array
            //  from which they were copied is reinitialized to the empty array.
            //  --------------------------------------------------------------------

            const blob = new Blob (
                chunks, {
                    type: selectedMedia === MEDIA_IS_VIDEO ? 'video/mp4' : 'audio/mpeg'
                });
            chunks = [ ];

            //  --------------------------------------------------------------------
            //  Create a video or audio element that stores the recorded media.
            //  --------------------------------------------------------------------

            if ( recordedMedia !== null )
            {
                recordedMedia.remove ( );
            }   // if ( recordedMedia !== null )

            recordedMedia               = document.createElement ( selectedMedia === MEDIA_IS_VIDEO ? 'video' : 'audio' );
            recordedMedia.controls      = true;
            recordedMedia.title         = 'Click or tap the PLAY button to play back the recording that you just made.';
            LLCommon.ShowOrHideElement ( recordedMedia ,
                                         LLCommon.ELEMENT_SHOW );

            //  --------------------------------------------------------------------
            //  You can not directly set the blob as the source of the video or
            //  audio element.
            //
            //  You must create a URL for the blob using the URL.createObjectURL ( )
            //  method.
            //  --------------------------------------------------------------------

            const recordedMediaURL      = URL.createObjectURL ( blob );
            recordedMedia.src           = recordedMediaURL;

            //  --------------------------------------------------------------------
            //  Create a download button that lets the user download the recorded
            //  media.
            //  --------------------------------------------------------------------

            if ( downloadButton != null )
            {
                downloadButton.remove ( );
                downloadButton          = null;
            }   // if ( downloadButton != null )

            downloadButton = document.createElement ( 'a' );

            //  --------------------------------------------------------------------
            //  Set the download attribute to true so that when the user clicks the
            //  link, the recorded media is downloaded to their machine.
            //  --------------------------------------------------------------------

            downloadButton.download     = 'Recorded-Media';
            downloadButton.innerText    = 'Download locally';
            downloadButton.id           = 'LocalDownloadButton';
            downloadButton.href         = recordedMediaURL;
            downloadButton.title        = 'Click or tap this link to put a copy of the recording that you just made to the DOWNLOADS folder on your device.';
            downloadButton.classList.add ( 'STT_NOWRAP' );

            downloadButton.onclick = ( ) =>
            {
                URL.revokeObjectURL ( recordedMedia );
            };  // downloadButton.onclick event listener

            //  ----------------------------------------------------------------
            //  Variable docMediaRecorder is declared at the top to give it
            //  global scope.
            //
            //  If the element exists due to a previous pass through this
            //  routine, the reference is reinitialized so that it points to the
            //  grandparent of the start button that was passed into the
            //  enclosing StartRecording function. However, removing it from the
            //  DOM would destroy the document, unlike the downloadButton,
            //  recordedMedia, and uploadButton elements, all of which are
            //  created dynamically by this routine and appended to the
            //  grandparent of the start button that was passed into the
            //  enclosing StartRecording function.
            //
            //  To that end, global script variable docMediaRecorder gets a
            //  reference to the grandparent of the start button, which is the
            //  TR element that owns (contains) the TD element that owns
            //  (contains) the start and stop buttons. Taking a reference to the
            //  grandparent abstracts away the IDs of the buttons and their
            //  containing TD, TR, and TABLE elements, of which there are two
            //  essentially identical sets, one each for audio and video, of
            //  which only one is ever active and bound to this routine.
            //
            //  As each of the three objects springs into existence, it goes
            //  into a new table detail (TD) cell that is appended to the
            //  grandparent TR, affording precise control over its placement on
            //  the page.
            //
            //  In the interest of keeping object creation as close as possible
            //  to first use, setting the reference is deferred until the first
            //  object is ready to be appended.
            //  ----------------------------------------------------------------

            const docStartButtonGreatGP = pdocStartButton.parentElement.parentElement.parentElement;
            const fVAlignPlaybackCtrls  = docStartButtonGreatGP.id.endsWith ('RecordingControlsBody' );


            if ( fVAlignPlaybackCtrls )
            {
                const docPlaybackToolz  = document.createElement ( 'tr' );
                docPlaybackToolz.id     = ELEMENT_ID_PLAYBACK_TOOLS;
                LLCommon.ShowOrHideElement ( docPlaybackToolz ,
                                             LLCommon.ELEMENT_SHOW );
                docStartButtonGreatGP.appendChild ( docPlaybackToolz );
                docMediaRecorder        = document.getElementById ( ELEMENT_ID_PLAYBACK_TOOLS );
            }   // TRUE (The form is too wide to allow the playback controls to be placed to the right of the start and stop buttons.) block, if ( fVAlignPlaybackCtrls )
            else
            {
                docMediaRecorder        = pdocStartButton.parentElement.parentElement;
            }   // FALSE (This legacy form can acommodate the playback controls to the right of the start and stop buttons.) block, if ( fVAlignPlaybackCtrls )

            recordedMedia.id            = 'RecordedMediaPlayer';

            const docRecordedMediaHouse = document.createElement ( 'td' );
            docRecordedMediaHouse.id    = 'RecordedMediaHouse'
            docRecordedMediaHouse.appendChild ( recordedMedia );
            docMediaRecorder.appendChild ( docRecordedMediaHouse );

            const docLocalButtonHouse   = document.createElement ( 'td' );
            docLocalButtonHouse.id      = 'LocalDownloadButtonContainer'
            docLocalButtonHouse.appendChild ( downloadButton );

            InsertPlaybackControlIntoPage ( docLocalButtonHouse ,
                                            docMediaRecorder ,
                                            fVAlignPlaybackCtrls );

            debugger;

            //  ----------------------------------------------------------------
            //  Here begins the new implementation of the automated upload.
            //  ----------------------------------------------------------------

            var reader              = new window.FileReader ( );

            reader.readAsDataURL ( blob );

            //  ------------------------------------------------------------
            //  The function must fulfill the following:
            //
            //      public string ProcessRecording ( string LeadId,
            //                                       string Contents,
            //                                       int Duration,
            //                                       string FileType,
            //                                       int DomainId,
            //                                       int TenantId,
            //                                       string DomainName,
            //                                       int UserId = 1000,
            //                                       string Email = "")
            //
            //  Of the foregoing, I need only supply the email (user login
            //  ID), and the server can get the rest.
            //  ------------------------------------------------------------

            reader.onloadend = function ( )
            {
                debugger;
                $.ajax( {
                    url     : LLCommon.AjaxUrlPrefix + 'Open/UploadMedia' ,
                    type    : 'POST',
                    async   : false ,
                    cache   : false ,
                    data    : {
                                'leadId'            : _LeadLifeJSHelpers.STTLeadId ,
                                'chunks'            : reader.result ,
                                'recordingDuration' : Math.round ( ( mediaRecStopped.getTime ( ) - mediaRecStarted.getTime ( ) ) / 1000 ) ,
                                'loginEmail'        : _LeadLifeJSHelpers.STTLoginName ,
                                'TZOffset'          : _LeadLifeJSHelpers.UtcOffsetMinutes ,
                                'Option'            : UploadMediaOption === ACTION_DICTATE_NOTE
                                                      ? UploadMediaOption
                                                      : LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12
                                                        ? 'Recording, createtask'
                                                        : 'Recording'
                    },
                    success : function ( response )
                    {
                        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': UploadRecording succeeded: response = ' + response );

                        debugger;

                        //  ----------------------------------------------------
                        //  Once the transcript is completely processed, the
                        //  form can be updated and validated.
                        //  ----------------------------------------------------

                        // GetSelectedInfo4LeadIdGet ( string LeadId , string CustomFields = SpecialStrings.EMPTY_STRING , string CustomFieldsOnlyFlag = @"false" , int tzOffsetMinutes = MagicNumbers.ZERO , string DateWithoutTime = @"false" )

                        if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                        {
                            const strSelectedInfo       = LLCommon.DoAjax ( 'GetSelectedInfo4LeadIdGet',
                                                                            'GET',
                                                                            {
                                                                                'LeadId'                      : _leadid.toString ( ),
                                                                                'CustomFields'                : 'WA_Task_TaskNote,WA_Task_InsideTeamId,WA_Task_EstimatedTime,WA_Task_Priority,WA_Task_TaskDue,WA_Task_Description',
                                                                                'CustomFieldsOnlyFlag'        : true,
                                                                                'tzOffsetMinutes'             : ( new Date ( ) ).getTimezoneOffset ( ),
                                                                                'DateWithoutTime'             : true,
                                                                                'ReturnPickListDisplayValues' : true
                                                                            } );

                            if ( strSelectedInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                            {
                                const astrNameValuePairs = strSelectedInfo.split ( LOGICAL_NEGATE );

                                for ( var intJ = ARRAY_FIRST_ELEMENT;
                                          intJ < astrNameValuePairs.length;
                                          intJ++ )
                                {
                                    var astrKeyAndValue = LLCommon.StringSplitSharp ( astrNameValuePairs [ intJ ] ,
                                                                                      EQUALS_CHAR ,
                                                                                      SPLIT_NAME_FROM_VALUE );
                                    var docCurrElement  = document.getElementById ( astrKeyAndValue [ SPLIT_NAME_PART ] );

                                    if ( docCurrElement !== null )
                                    {
                                        docCurrElement.value = astrKeyAndValue [ SPLIT_VALUE_PART ];
                                    }   // TRUE (anticipated outcome) block, if ( docCurrElement !== null )
                                    else
                                    {
                                        LLCommon.LogException ( strMethodName + ' Exception: Required HTML document element ' + astrKeyAndValue [ SPLIT_NAME_PART ] + ' NOT FOUND' );
                                    }   // FALSE (unanticipated outcome) block, if ( docCurrElement !== null )
                                }   // for ( var intJ = ARRAY_FIRST_ELEMENT; intJ < astrNameValuePairs.length; intJ++ )

                                ValidateAllFormFields ( );
                            }   // TRUE (anticipated outcome) block, if ( strSelectedInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                            else
                            {
                                LLCommon.LogException ( strMethodName + ' Exception: ' + strSelectedInfo );
                            }   // FALSE (unanticipated outcome) block, if ( strSelectedInfo.indexOf ( LOGICAL_NEGATE ) > INDEXOF_NOT_FOUND )
                        }   // if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )

                        //  ----------------------------------------------------
                        //  The transcript list cannot be refreshed until
                        //  UploadMedia returns, an event that is best handled
                        //  by putting the code in the success event handler of
                        //  the AJAX call that sends it to the server and awaits
                        //  its response.
                        //
                        //  Though GetTranscriptList and GetNotesList return -1
                        //  to indicate an error, I see no need to evaluate it,
                        //  since both routines succeeded when the page first
                        //  loaded. If it is later deemed useful or necessary, a
                        //  common evaluation can execute when the switch block
                        //  ends.
                        //  ----------------------------------------------------

                        const docUpdateTranscriptList                   = document.getElementById ( PICK_LIST_TRANSCRIPTS );
                        const docUpdateNotestList                       = document.getElementById ( PICK_LIST_NOTES );

                        if ( docUpdateTranscriptList !== null && docUpdateNotestList !== null )
                        {
                            switch ( UploadMediaOption )
                            {
                                case ACTION_TALK2CRM:
                                    LLCommon._fFormIsDirty              = true;
                                    docUpdateTranscriptList.innerHTML   = EMPTY_STRING;
                                    LLCommon.inputEnable ( BTN_UPDATE_CRM );
                                    console.log ( 'Inside function ' + strMethodName + ', an onstop event listener, DIRTY flag switched ON because UploadMediaOption = ' + UploadMediaOption );
                                    LLCommon.ShowOrHideElement ( NOTES_FILTER_CONTAINER ,
                                                                 LLCommon.ELEMENT_HIDE );
                                    GetTranscriptList ( );
                                    break;  // case ACTION_TALK2CRM

                                case ACTION_DICTATE_NOTE:
                                    docUpdateNotestList.innerHTML       = EMPTY_STRING;
                                    LLCommon.AddOrRemoveStyles ( docUpdateNotestList ,
                                                                 'STT_Stoplight_Green_1' ,
                                                                 LLCommon.CSS_SELECTOR_ADD );
                                    LLCommon.AddOrRemoveStyles ( docUpdateTranscriptList ,
                                                                 'STT_Stoplight_Green_1' ,
                                                                 LLCommon.CSS_SELECTOR_REMOVE );
                                    LLCommon.ShowOrHideElement ( NOTES_FILTER_CONTAINER ,
                                                                 LLCommon.ELEMENT_HIDE );
                                    GetNotesList ( );
                                    break;  // case ACTION_DICTATE_NOTE
                            }   // switch ( UploadMediaOption )
                        }   // if ( docUpdateTranscriptList !== null && docUpdateNotestList !== null )
                    },
                    error   : function ( response ) { LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': UploadRecording FAILED: response = ' + response ); }
                } );
            }   // reader.onloadend

            if ( uploadedButton != null )
            {
                uploadedButton.remove ( );
                uploadedButton = null;
            }   // if ( uploadedButton != null )

            uploadedButton              = document.createElement ( 'button' );
            uploadedButton.id           = BTN_UPLOAD_RECORDING;
            uploadedButton.type         = 'button';
            uploadedButton.innerText    = 'Changes recorded - Hit "Update CRM Now" button now.';
            uploadedButton.title        = 'The transcript has been generated and uploaded. Please press the "Update CRM Now" button now.';
            uploadedButton.disabled     = false;

            LLCommon.AddOrRemoveStyles ( uploadedButton ,
                                         'EmailForm4AgentSendMessage' ,
                                         LLCommon.CSS_SELECTOR_ADD );

            const docUploadBtnHouse     = document.createElement ( 'td' );

            docUploadBtnHouse.id        = 'uploadedButtonContainer';
            docUploadBtnHouse.appendChild ( uploadedButton );
            InsertPlaybackControlIntoPage ( docUploadBtnHouse ,
                                            docMediaRecorder ,
                                            fVAlignPlaybackCtrls );

            //  ================================================================
            //  Since the upload functionality is being integrated into the stop
            //  recording routine, all of its code is commented out, in case the
            //  change gets vetoed.
            //  ================================================================

            //  ----------------------------------------------------------------
            //  Create a upload button that lets the user upload the recorded
            //  media to a Web server.
            //  ----------------------------------------------------------------

/*
            if ( uploadButton != null )
            {
                uploadButton.remove ( );
                uploadButton = null;
            }

            uploadButton = document.createElement ( 'button' );

            //  --------------------------------------------------------------------
            //  Set the upload attribute to true so that when the user clicks the
            //  link, the recorded media is uploaded to the server on which the page
            //  is hosted.
            //  --------------------------------------------------------------------

            uploadButton.id             = BTN_UPLOAD_RECORDING;
            uploadButton.type           = 'button';
            uploadButton.innerText      = 'Upload and Transcribe';
            uploadButton.title          = 'Creating the transcription from the recording may require up to 5 minutes. Check for results by refreshing the Story So Far page, where you will also find a copy of the recording.';
            uploadButton.className      = 'EmailForm4AgentCancelMessage';

            const docUploadBtnHouse     = document.createElement ( 'td' );
            docUploadBtnHouse.id        = 'UploadButtonContainer';
            docUploadBtnHouse.appendChild ( uploadButton );
            InsertPlaybackControlIntoPage ( docUploadBtnHouse ,
                                            docMediaRecorder ,
                                            fVAlignPlaybackCtrls );

            //  ----------------------------------------------------------------
            //  Adding the Click event listener to the uploadButton (ID =
            //  UploadRecordedMedia2Server) must await its creation in the last
            //  step of the Stop event listener that instantiates it. Hence, it
            //  is defined as an anonymous arrow function as part of defining
            //  the Click event listener. Both arrow functions are evaluated
            //  when the script loads.
            //  ----------------------------------------------------------------

            uploadButton.onclick = ( event ) =>
            {
                debugger;

                event.preventDefault ( );
                var reader              = new window.FileReader ( );

                reader.readAsDataURL ( blob );

                //  ------------------------------------------------------------
                //  The function must fulfill the following:
                //
                //      public string ProcessRecording ( string LeadId,
                //                                       string Contents,
                //                                       int Duration,
                //                                       string FileType,
                //                                       int DomainId,
                //                                       int TenantId,
                //                                       string DomainName,
                //                                       int UserId = 1000,
                //                                       string Email = "")
                //
                //  Of the foregoing, I need only supply the email (user login
                //  ID), and the server can get the rest.
                //  ------------------------------------------------------------

                reader.onloadend = function ( )
                {
                    $.ajax( {
                        url     : LLCommon.AjaxUrlPrefix + 'Open/UploadMedia' ,
                        type    : 'POST',
                        async   : false ,
                        cache   : false ,
                        data    : {
                                    leadId            : _LeadLifeJSHelpers.STTLeadId ,
                                    chunks            : reader.result ,
                                    recordingDuration : Math.round ( ( mediaRecStopped.getTime ( ) - mediaRecStarted.getTime ( ) ) / 1000 ) ,
                                    loginEmail        : _LeadLifeJSHelpers.STTLoginName ,
                                    TZOffset          : _LeadLifeJSHelpers.UtcOffsetMinutes ,
                                    'Option'          : UploadMediaOption
                        },
                        success : function ( response ) { LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': UploadRecording succeeded: response = ' + response ); },
                        error   : function ( response ) { LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + ': UploadRecording FAILED: response = ' + response ); _fUnPostedRecording = false; }
                    } );
                }   // reader.onloadend

                event.currentTarget.innerText  = 'Sent: Refresh to see results.';
                event.currentTarget.disabled   = true;
                LLCommon.SwapCssSelectorsOnElement ( event.currentTarget ,
                                                     'EmailForm4AgentCancelMessage' ,
                                                     'EmailForm4AgentSendMessage' );
                AdjustButtonProperties ( BUTTON_STATE_VISIBLE );

                return false;
            };  // uploadButton.onclick event listener
*/
        };  // mediaRecorder.onstop event listener

        if ( selectedMedia === MEDIA_IS_VIDEO )
        {
            //  ----------------------------------------------------------------
            //  Remember to use the srcObject attribute since the src attribute
            //  doesn't support media stream as a value.
            //  ----------------------------------------------------------------

            webCamContainer.srcObject   = mediaStream;
        }   // if ( selectedMedia === MEDIA_IS_VIDEO )

        //  --------------------------------------------------------------------
        //  Since it is initially hidden, the record status element must be made
        //  visible after its innerText property is set.
        //  --------------------------------------------------------------------

        const docRecordStatus       = document.getElementById ( `${selectedMedia}-record-status` );
        docRecordStatus.innerText   = 'Recording';

        LLCommon.ShowOrHideElement ( docRecordStatus ,
                                     LLCommon.ELEMENT_SHOW );

        //  --------------------------------------------------------------------
        //  Because the navigator.mediaDevices.getUserMedia event listener is an
        //  arrow function, it can see the arguments to enclosing function
        //  StartRecording, which forms a closure when it gets control.
        //  --------------------------------------------------------------------

        pdocStartButton.disabled    = true;
        pdocStopButton.disabled     = false;
    }); // navigator.mediaDevices.getUserMedia event listener

    StartTimer ( Number.isNaN ( Number.parseInt ( countdown ) ) ? COUNTDOWN_DEFAULT : Number.parseInt ( countdown ) ,
                 document.getElementById ( selectedMedia === 'vid' ? 'vidtimer' : 'audtimer' ) ,
                 pdocStopButton );
}  // function StartRecording


function StartTimer ( pintDurationSeconds , pdocCountDownDisplay , pdocStopButton )
{
    const strMethodName                 = LLCommon.GetNameOfCurrentFunction ( );

    window.recordingTimer               = pintDurationSeconds;

    var interval                        = setInterval ( function ( )
    {
        pdocCountDownDisplay.value      = pdocStopButton.disabled ? EMPTY_STRING : window.recordingTimer;
        LLCommon.ShowOrHideElement ( pdocCountDownDisplay.parentElement ,
                                     window.recordingTimer < 60 && window.recordingTimer > 0 );

        if ( ( window.recordingTimer > 0 ) && ( window.recordingTimer % 60 === 0 ) )
        {
            LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strMethodName + ': Time remaining = ' + ( window.recordingTimer / 60 ) + ' minutes' );
        }   // if ( ( window.recordingTimer > 0 ) && ( window.recordingTimer % 60 === 0 ) )

        //  --------------------------------------------------------------------
        //  Presently, unless the operator clocks the stop button, when the
        //  countdown clock expires, the stop button, represented by argument
        //  pdocStopButton to enclosing function StartTimer, which is viaible as
        //  a closure, is programmatically clicked 1 second after it expires, at
        //  which point this interval event sink decrements the counter once
        //  more, from zero to minus one, then unregisteres this function as an
        //  interval timer.
        //  --------------------------------------------------------------------

        if ( window.recordingTimer <= 0 )
        {
            clearInterval ( interval );

            if ( window.recordingTimer === -1 )
            {
                pdocStopButton.disabled = true;
            }   // if ( window.recordingTimer === -1 )

            if ( !pdocStopButton.disabled )
            {
                pdocStopButton.click ( );
            }   // if ( !pdocStopButton.disabled )
        }   // TRUE block, if ( window.recordingTimer <= 0 )
        else
        {
           window.recordingTimer--;
        }   // FALSE block, if ( window.recordingTimer <= 0 )
    }, 1000 );  // setInterval function
}  // function StartTimer


function StopRecording ( thisButton , otherButton )
{
    //  ------------------------------------------------------------------------
    //  Stop the recording.
    //  ------------------------------------------------------------------------

    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    window.recordingTimer       = -1;
    mediaRecStopped             = new Date ( );

    window.mediaRecorder.stop ( );

    //  ------------------------------------------------------------------------
    //  Stop all the tracks in the received media stream.
    //  ------------------------------------------------------------------------

    window.mediaStream.getTracks ( )
    .forEach ( ( track ) => {
        track.stop ( );
    });

    document.getElementById ( `${ selectedMedia }-record-status` ).innerText = 'Recording Completed!';

    thisButton.disabled         = true;
    otherButton.disabled        = false;
}  // function StopRecording


function ThisRowWasSelected ( event )
{
    const strFunctionName = LLCommon.GetNameOfCurrentFunction ( );

    console.log ( 'Inside function ' + strFunctionName + ', ID of selected row = ' + event.currentTarget.id );

    debugger;

    const strClickedRowId       = event.currentTarget.id;
    const astrIdParts           = strClickedRowId.split ( UNDERSCORE_CHAR );

    if ( astrIdParts.length > ARRAY_IS_EMPTY )
    {
        const intRowNumber      = parseInt ( astrIdParts [ LLCommon.IndexFromOrdinal ( astrIdParts.length ) ] );
        const strExternalIDCell = 'ExteernlCrmId_' + intRowNumber;
        DoGetRecordFromCRM ( strExternalIDCell );
    }   // TRUE (anticipated outcome) block, if ( astrIdParts.length > ARRAY_IS_EMPTY )
    else
    {
        const strMessage = 'The format of the ID of the selected element, "' + strClickedRowId + '" is invalid.';
        LLCommon.LogException ( strMessage )
        alert ( strMessage , 'native' );
    }   // FALSE (unanticipated outcome) block, if ( astrIdParts.length > ARRAY_IS_EMPTY )
}   // function ThisRowWasSelected


function ToggleDivs ( pstrAction )
{
    const ActionButtonFixup = ( ) =>
    {
        const strFunctionName           = LLCommon.GetNameOfCurrentFunction ( );

        const strActionMedia            = UploadMediaOption + UNDERSCORE_CHAR + selectedMedia;
        const oRecorderButtonTexts      = strActionMedia in _aoRecorderButtonTexts ? _aoRecorderButtonTexts [ strActionMedia ] : null;

        if ( oRecorderButtonTexts !== null )
        {
            document.getElementById ( oRecorderButtonTexts.StartButtonId ).innerHTML = oRecorderButtonTexts.StartButtonText;
            document.getElementById ( oRecorderButtonTexts.StopButtonId ).innerHTML  = oRecorderButtonTexts.StopButtonText;
        }   // TRUE (anticipated outcome) block, if ( oRecorderButtonTexts !== null )
        else
        {
            LLCommon.Trace ( LLCommon.LogException ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + 'ActionButtonFixup: INTERNAL ERROR UploadMediaOption = ' + UploadMediaOption + ', selectedMedia = ' + selectedMedia ) );
        }   // FALSE (unanticipated outcome) block, if ( oRecorderButtonTexts !== null )
    }   // const ActionButtonFixup = ( ) =>


    const strFunctionName = LLCommon.GetNameOfCurrentFunction ( );

    debugger;

    try
    {
        UploadMediaOption = pstrAction;
        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName + ': Begin ' + strFunctionName + ' function with UploadMediaOption = ' + UploadMediaOption );

        //  --------------------------------------------------------------------
        //  For some forms, including the Mobile_Index page (the "Walking to the
        //  Car" form), since the lead ID is not immediately available, lists of
        //  Transcripts and Notes must be evaluated early and often.
        //  --------------------------------------------------------------------

        switch ( GetTranscriptList ( ) )
        {
            case ARRAY_INVALID_INDEX:      // Since the error has already been logged, just tell the user about it.
                alert ( ERROR_MESSAGEE_INTERNAL , 'native' );
                break;
            case EMPTY_STRING:
            default:
                if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                {
                    LLCommon.ShowOrHideElement ( BTN_TRANSCRIPT_REVIEW ,
                                                 LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( PICK_LIST_TRANSCRIPTS ,
                                                 LLCommon.ELEMENT_HIDE );
                }   // TRUE (The current entity is a Wise Agent Task.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                else
                {
                    LLCommon.ShowOrHideElement ( BTN_TRANSCRIPT_REVIEW ,
                                                 LLCommon.ELEMENT_SHOW );
                    LLCommon.ShowOrHideElement ( PICK_LIST_NOTES ,
                                                 LLCommon.ELEMENT_SHOW );
                }   // FALSE (Show the button for all other entities.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                break;
        }   // switch ( GetTranscriptList ( ) )

        switch ( GetNotesList ( ) )
        {
            case ARRAY_INVALID_INDEX:      // Since the error has already been logged, just tell the user about it.
                alert ( ERROR_MESSAGEE_INTERNAL , 'native' );
                break;
            case EMPTY_STRING:
            default:
                if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                {
                    LLCommon.ShowOrHideElement ( BTN_NOTES_REVIEW ,
                                                 LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( PICK_LIST_NOTES ,
                                                 LLCommon.ELEMENT_HIDE );
                }   // TRUE (The current entity is a Wise Agent Task.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                else
                {
                    LLCommon.ShowOrHideElement ( BTN_NOTES_REVIEW ,
                                                 LLCommon.ELEMENT_SHOW );
                }   // FALSE (Show the button for all other entities.) block, if ( LLCommon.EntityType !== null && LLCommon.EntityType.CRMEntityTypeId === 12 )
                break;
        }   // switch ( GetTranscriptList ( ) )

        switch ( pstrAction )
        {
            case ACTION_DICTATE_NOTE:
            case ACTION_TALK2CRM:
                {   // This lexical block exists to satisfy the no-case-declarations rule of ESLint. See https://eslint.org/docs/latest/rules/no-case-declarations for details.
                    const docW2ARecorder            = document.getElementById  ( ELEMENT_ID_W2A_RECORDER );

                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName
                                  + ': Action = ' + pstrAction
                                  + ', Button ID = ' + docW2ARecorder.id
                                  + ', Baseline className = ' + docW2ARecorder.className );

                    LLCommon.ShowOrHideElement ( docW2ARecorder ,
                                                 LLCommon.ELEMENT_SHOW );
                    LLCommon.ShowOrHideElement ( ELEMENT_ID_CHEAT_SHEET_BX ,
                                                 pstrAction === ACTION_DICTATE_NOTE
                                                    ? ELEMENT_HIDE
                                                    : ELEMENT_SHOW );
                    LLCommon.ShowOrHideElement ( ELEMENT_ID_W2A_VERIFIER ,
                                                 LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( ELEMENT_ID_REVIEW_TOOLS ,
                                                 LLCommon.ELEMENT_SHOW );

                    ActionButtonFixup ( );

                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName
                                  + ': Action = ' + pstrAction
                                  + ', Button ID = ' + docW2ARecorder.id
                                  + ', Revised className = ' + docW2ARecorder.className );
                }   // The lexical block for this case clause ends here.

                break;  // case  ACTION_DICTATE_NOTE or ACTION_TALK2CRM

            case ACTION_TRANS_REVIEW:
                {   // This lexical block exists to satisfy the no-case-declarations rule of ESLint. See https://eslint.org/docs/latest/rules/no-case-declarations for details.
                    const docW2ATranscript          = document.getElementById ( ELEMENT_ID_W2A_VERIFIER );

                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName
                                     + ': Action = '             + pstrAction
                                     + ', Button ID = '          + docW2ATranscript.id
                                     + ', Baseline className = ' + docW2ATranscript.className );

                    LLCommon.ShowOrHideElement ( ELEMENT_ID_W2A_RECORDER ,
                                                 LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( docW2ATranscript ,
                                                 LLCommon.ELEMENT_SHOW );

                    const docTranscriptChoiceList   = document.getElementById ( PICK_LIST_TRANSCRIPTS );

                    //  --------------------------------------------------------
                    //  In the unlikely event that docTranscriptChoiceList is
                    //  NULL, indicating an internal document error, function
                    //  ShowOrHideElement already does nothing. Hence, argument
                    //  pfShowIt is simplified to assume docTranscriptChoiceList
                    //  exists, so that it hides the pick list control when the
                    //  list is empty.
                    //  --------------------------------------------------------

                    LLCommon.ShowOrHideElement ( docTranscriptChoiceList ,
                                                 docTranscriptChoiceList.value.length > EMPTY_STRING_LENGTH
                                                     ? true
                                                     : false );

                    STTProcessMedia ( docTranscriptChoiceList.value ,                                   // event is required.
                                      _aW2ARecordingUris [ docTranscriptChoiceList.selectedIndex ] );   // pAudioPlaybackUri is optional.
                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName
                                     + ': Action = '            + pstrAction
                                     + ', Button ID = '         + docW2ATranscript.id
                                     + ', Revised className = ' + docW2ATranscript.className );
                }   // The lexical block for this case clause ends here.

                break;  // case ACTION_TRANS_REVIEW

            case ACTION_NOTE_REVIEW:
                {   // This lexical block exists to satisfy the no-case-declarations rule of ESLint. See https://eslint.org/docs/latest/rules/no-case-declarations for details.
                    const docNote2Review            = document.getElementById ( ELEMENT_ID_W2A_VERIFIER );

                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName + ': Action = ' + pstrAction
                                     + ', Button ID = '          + docNote2Review.id
                                     + ', Baseline className = ' + docNote2Review.className );

                    LLCommon.ShowOrHideElement ( ELEMENT_ID_W2A_RECORDER ,
                                                 LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( docNote2Review ,
                                                 LLCommon.ELEMENT_SHOW );

                    const docNotesList              = document.getElementById ( PICK_LIST_NOTES );

                    //  --------------------------------------------------------
                    //  In the unlikely event that docTranscriptChoiceList is
                    //  NULL, indicating an internal document error, function
                    //  ShowOrHideElement already does nothing. Hence, argument
                    //  pfShowIt is simplified to assume docTranscriptChoiceList
                    //  exists, so that it hides the pick list control when the
                    //  list is empty.
                    //  --------------------------------------------------------

                    LLCommon.ShowOrHideElement ( docNotesList ,
                                                 docNotesList.value.length > EMPTY_STRING_LENGTH
                                                    ? true
                                                    : false );

                    STTProcessMedia ( NOTE_ID_PREFIX + docNotesList.value ,                         // event is required.
                                      _aNoteRecordingUris [ docNotesList.selectedIndex ] );         // pAudioPlaybackUri is optional.
                    LLCommon.Trace (   Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName
                                     + ': Action = '            + pstrAction
                                     + ', Button ID = '         + docNote2Review.id
                                     + ', Revised className = ' + docNote2Review.className );
                }   // The lexical block for this case clause ends here.

                break;  // case ACTION_NOTE_REVIEW

            case ACTION_SEARCH_NOTES:
                {   // This lexical block exists to satisfy the no-case-declarations rule of ESLint. See https://eslint.org/docs/latest/rules/no-case-declarations for details.
                    debugger;
                    const docNotesFilterInput = document.getElementById ( NOTES_FILTER )
                    LLCommon.ShowOrHideElement ( PICK_LIST_NOTES        , LLCommon.ELEMENT_HIDE );
                    LLCommon.ShowOrHideElement ( NOTES_FILTER_CONTAINER , LLCommon.ELEMENT_SHOW );
                    docNotesFilterInput.focus ( );
                }   // The lexical block for this case clause ends here.
                break;  // case ACTION_SEARCH_NOTES
        }   // switch ( pstrAction )

        LLCommon.Trace ( Words2Actions_Recorder_Forms_SCRIPTSOURCE + SPACE_CHARACTER + strFunctionName + ': Finished with ' + pstrAction + ' function.' );
    }
    catch ( ex )
    {
        LLCommon.LogException ( strFunctionName + ': Attempting to implement the ' + pstrAction + ' function. See exception log for details.' , ex );
    }
}   // function ToggleDivs


function ToggleFormSection ( pdocThisBtn )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );
    debugger;

    try
    {
        const strThisId         = pdocThisBtn.id;
        const strTargetId       = strThisId.substring ( TOGGLE_WORD_LEN );
        const docTargetElem     = LLCommon.getElementOrThrow ( strTargetId );

        const fNewState         = docTargetElem.classList.contains ( LLCommon.STT_HideElement );
        const strButtonText     = pdocThisBtn.innerText;
        pdocThisBtn.innerText   = fNewState
                                   ? strButtonText.replace ( 'display' , 'hide' )
                                   : strButtonText.replace ( 'hide'    , 'display' );
        LLCommon.ShowOrHideElement ( docTargetElem , fNewState );

        LLCommon.DoAjax ( 'PutMonikorInDatabase' ,
                          'GET' ,
                          {
                              'Monikor'  : strTargetId + UNDERSCORE_CHAR + _userid,
                              'NewValue' : fNewState.toString ( ),
                              'DomainId' : _domainid,
                              'TenantId' : _tenantid
                          } );
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }
}   // function ToggleFormSection


function ValidateAllFormFields ( )
{   // See the revision history item for veersion 1.031.
    const MapPickListWords              = ( pstrFieldId ) =>
    {
        const strFunctionName           = LLCommon.GetNameOfCurrentFunction ( );

        var   rintNReplacements         = NUMERIC_ZERO;

        const docInputField             = document.getElementById ( pstrFieldId );

        if ( docInputField !== null && docInputField.nodeName === 'INPUT' && docInputField.type === 'text' && docInputField.value.length > EMPTY_STRING_LENGTH )
        {
            var   strNewValue           = docInputField.value;

            for ( var intMapEntry = ARRAY_FIRST_ELEMENT;
                      intMapEntry < _astrW2A_Task_Word_Map.length;
                      intMapEntry++ )
            {
                var astrMapPair = _astrW2A_Task_Word_Map [ intMapEntry ].split ( LOGICAL_NEGATE );

                if ( astrMapPair.length === SPLIT_NAME_FROM_VALUE )
                {
                    if ( strNewValue.indexOf ( astrMapPair [ SPLIT_NAME_PART ] ) > INDEXOF_NOT_FOUND )
                    {
                        var oRegExp     = new RegExp ( astrMapPair [ SPLIT_NAME_PART ] , 'gi' ); // Construct a RegExp with global and case-insensitive matching.
                        strNewValue     = strNewValue.replace ( oRegExp , astrMapPair [ SPLIT_VALUE_PART ] );

                        if ( strNewValue !== docInputField.value )
                        {
                            docInputField.value = strNewValue;
                            rintNReplacements++;
                        }   // if ( strNewValue !== docInputField.value )
                    }   // if ( strNewValue.indexOf ( astrMapPair [ SPLIT_NAME_PART ] ) > INDEXOF_NOT_FOUND )
                }   // if ( astrMapPair.length === SPLIT_NAME_FROM_VALUE )
            }   // for ( var intMapEntry = ARRAY_FIRST_ELEMENT; intMapEntry < _astrW2A_Task_Word_Map.length; intMapEntry++ )
        }   // if ( docInputField !== null && docInputField.nodeName === 'INPUT' && docInputField.type === 'text' && docInputField.value.length > EMPTY_STRING_LENGTH )

        return rintNReplacements;
    }   // const MapPickListWords

    const strFunctionName       = LLCommon.GetNameOfCurrentFunction ( );;
    debugger;
    const intNReplacements      = MapPickListWords ( 'WA_Task_EstimatedTime' );

    console.log ( strFunctionName + ': Private function MapPickListWords made ' + intNReplacements + ' replacements in the value of INPUT element WA_Task_EstimatedTime.' );

    //  ------------------------------------------------------------------------
    //  When intNReplacements indicates that the value in INPUT element
    //  WA_Task_EstimatedTime has changed, the amended value must go back to the
    //  server.
    //
    //  The element ID must be specified as it would be for jQuery.
    //
    //  Since activating the Rules Engine implies permission to update the last
    //  modified date of the controlling lead record, its flag has the same
    //  value as the new pfUpdateLeadModDate flag.
    //  ------------------------------------------------------------------------

    if ( intNReplacements > NUMERIC_ZERO )
    {
        const strUpdateFormFieldResult  = _LeadLifeJSHelpers.UpdateFormFieldById ( JQUERY_SELECTOR_IS_ELEMENT_ID + 'WA_Task_EstimatedTime' , 'WA_Task_EstimatedTime' , false , false ).toString ( );

        if ( strUpdateFormFieldResult.length === EMPTY_STRING_LENGTH )
        {
            console.log ( 'JS Function ' + strFunctionName + ': UpdateFormFieldById SUCCEEDED, docChangedElement.id =' + docChangedElement.id + ', docChangedElement.value = ' + docChangedElement.value );
        }   // TRUE (anticipated outcome) block, if ( strUpdateFormFieldResult.length === EMPTY_STRING_LENGTH )
        else
        {
            console.log ( 'JS Function ' + strFunctionName + ': strUpdateFormFieldResult = ' + strUpdateFormFieldResult + 'Element ID = WA_Task_EstimatedTime, value = ' + document.getElementById ( 'WA_Task_EstimatedTime' ).value );
            alert ( LLCommon.LogException (   'JS Function ' + strMethodName
                                            + ': An exception arose while updating field WA_Task_EstimatedTime'
                                            + ' value to ' + document.getElementById ( 'WA_Task_EstimatedTime' ).value
                                            + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                            + LLCommon.CARRIAGE_RETURN_CHAR + LLCommon.LINE_FEED_CHAR
                                            + 'Please contact SalesTalk customer support for assistance.' ) ,
                    'native' );
        }   // FALSE (uanticipated outcome) block, if ( strUpdateFormFieldResult.length === EMPTY_STRING_LENGTH )
    }   // if ( intNReplacements > NUMERIC_ZERO )

    const aoErrorInfo           = _LeadLifeJSHelpers.ValidateFormFields ( ELEMENT_ID_W2A_FORM );
    const intErrorCount         = aoErrorInfo.length;

    const docUpdateCRMNow       = document.getElementById ( 'UpdateCRMNow' );

    debugger;

    if ( docUpdateCRMNow !== null )
    {
        if ( intErrorCount === ARRAY_IS_EMPTY )
        {
            LLCommon.AddOrRemoveStyles ( docUpdateCRMNow ,
                                         'STT_Field_with_Error' ,
                                         LLCommon.CSS_SELECTOR_REMOVE );
            LLCommon.AddOrRemoveStyles ( docUpdateCRMNow ,
                                         'STT_Electric_Blue_1' ,
                                         LLCommon.CSS_SELECTOR_REMOVE );
            LLCommon.AddOrRemoveStyles ( docUpdateCRMNow ,
                                         'STT_Stoplight_Green_1' ,
                                         LLCommon.CSS_SELECTOR_ADD );
            console.log ( 'Inside function ' + strFunctionName + ', the form is marked as dirty to force the lost focus event to display an alert, and the update CRM button is set to stop light green.' );

            document.querySelectorAll ( '.STT_Field_with_Error' ).forEach ( el => {
                el.classList.remove ( 'STT_Field_with_Error' );
            });

            docUpdateCRMNow.disabled    = false;
            LLCommon._fFormIsDirty      = true;
            LLCommon.inputEnable ( BTN_UPDATE_CRM );
            alert ( 'Everything looks good!' , 'native' );
        }   // TRUE (desired outcome) block, if ( intErrorCount === ARRAY_IS_EMPTY )
        else
        {
            var docErrorElement         = null;

            for ( var intCurrentFieldIndex = ARRAY_FIRST_ELEMENT;
                      intCurrentFieldIndex < intErrorCount;
                      intCurrentFieldIndex++ )
            {
                docErrorElement         = document.getElementById ( aoErrorInfo [ intCurrentFieldIndex ].ControlId );

                if ( docErrorElement !== null )
                {
                    LLCommon.AddOrRemoveStyles ( docErrorElement ,
                                                 'STT_Field_with_Error' ,
                                                 LLCommon.CSS_SELECTOR_ADD );
                    docErrorElement.title   = _LeadLifeJSHelpers.ValidationErrorMessageArray [ aoErrorInfo [ intCurrentFieldIndex ].ReasonMessageId ];
                }   // TRUE (anticipated outcome) block, if ( docErrorElement !== null )
                else
                {
                    LLCommon.LogException ( strFunctionName + ': Document element ' + aoErrorInfo [ intCurrentFieldIndex ].ControlId + ' cannot be found.' );
                }   // FALSE (unanticipated outcome) block, if ( docErrorElement !== null )
            }   // for ( var intCurrentFieldIndex = ARRAY_FIRST_ELEMENT; intCurrentFieldIndex < intErrorCount; intCurrentFieldIndex++ )

            LLCommon.AddOrRemoveStyles ( docUpdateCRMNow ,
                                         'STT_Field_with_Error' ,
                                         LLCommon.CSS_SELECTOR_ADD );
            docUpdateCRMNow.disabled = true;
            bootbox.alert (   'Some field values are invalid.<br>'
                            + 'Invaild fields have <span style="color: #ffffff; background-color : #880000; font-weight : bold;">'
                            + 'red</span> backgrounds.<br>'
                            + 'Hover over a field for an explanation of the issue and how to fix it.' );
        }   // FALSE (undesired outcome) block, if ( intErrorCount === ARRAY_IS_EMPTY ))
    }   // TRUE (anticipated outcome) block, if ( docUpdateCRMNow !== null )
    else
    {
        LLCommon.LogException ( strFunctionName + ': The for has no button with ID "UpdateCRMNow."' );
    }   // FALSE (unanticipated outcome) block, if ( docUpdateCRMNow !== null ))

    return false;
}   // function ValidateAllFormFields


LLCommon.Trace ( ScriptInfoForLog ( Words2Actions_Recorder_Forms_SCRIPTSOURCE , Words2Actions_Recorder_Forms_VERSION , Words2Actions_Recorder_Forms_LastUpdated , 'loaded' ) );
