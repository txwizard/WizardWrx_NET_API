/*eslint-env browser*/
/*global $ define global GetNameOfCurrentFunction LLCommon module ScriptInfoForLog*/
"use strict";

const InputMask_Engine_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const InputMask_Engine_VERSION      = 1.024;
const InputMask_Engine_LastUpdated  = '2024/05/12 00:11:51 CDT';

/*
    ============================================================================

    Name:               LeadLife_InputMask_Engine.js

    Goal:               Define custom JavaScript functions used by forms that
                        need input masks.

    Dependencies:       The code defined in this module requires working JQuery
                        and LeadLifeJSHelpers objects, which the calling page is
                        expected to supply via deferred loading.

    Remarks:            This routine takes the unusual step of incorporating the
                        jQuery Mask plug-in code inline to guarantee that it has
                        finished its work before the engine needs it.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By Remark/Brief Description
    ---------- ------- -- ------------------------------------------------------
    2023/02/13 1.000   DG MVP: New code implemented as code behind a page
    2023/02/15 1.001   DG Add the missing verb to the DoAjax method call.
    2023/02/16 1.002   DG Recite the array values on the console.
    2023/02/16 1.003   DG Identify CSS classes and their corresponding masks to
                          the JQuery mask add-in.
    2023/02/17 1.004   DG Replace the arbitrary setTimeout function with a
                          JQuery $ ( document ).ready.
    2023/02/17 1.005   DG Error trap the selector queries.
    2023/02/17 1.006   DG Skip unused selectors.
    2023/02/20 1.007   DG Eliminate irrelevant code.
    2023/02/20 1.008   DG Break free of LeadLifeJSHelpersLib.js in favor of
                          LLCommon.
    2023/02/22 1.011   DG Adapt to use methods on the _LeadLifeJSHelpers object
                          if it is defined, falling back on the like named
                          methods on the LLCommon object otherwise.
    2023/02/23 1.012   DG Copy jQuery Mask Plugin v1.14.16 inline.
    2023/02/24 1.014   DG Wrap most of the code in an object so that the 
                          initializer can be safely run whenever a new page is
                          loaded.
    2023/04/03 1.015   DG Improve activity logging in the initializer and make
                          the object initializer a tad more robust and
                          conventional.
    2023/04/23 1.016   DG Enable optional tracing of change events on input
                          controls that are expected to display their value
                          under a mask.
    2023/05/09 1.018   DG Suppress user alert displays, since users cannot do
                          anything, and the exception is logged in the system
                          event log table, LeadLife.[Log].
    2023/05/24 1.019   DG Move the jQuery InputMask code out.
    2023/08/28 1.020   DG Eliminate references to the _LeadLifeJSHelpers JS
                          library in favor of LLCommon.js.
    2023/09/10 1.021   DG Establish constants InputMask_Engine_SCRIPTSOURCE and
                          InputMask_Engine_LastUpdated, in line with other
                          scripts in the stack, and implement local function
                          variable strMethodName throughout.
    2023/09/16 1.022   DG Add standardized console logging of script source,
                          version and last update time
    2024/02/23 1.023   DG Adjust all calls to GetNameOfCurrentFunction to use
                          the instance defined in LLCommon.js.
    2024/05/12 1.024   DG Replace virtually all calls to console.log with calls
                          to LLCommon.Trace, which can be centrally configured
                          to suppress logging.
    ============================================================================
*/

console.log ( ScriptInfoForLog ( InputMask_Engine_SCRIPTSOURCE , InputMask_Engine_VERSION , InputMask_Engine_LastUpdated , 'loading' ) );

const _CaptureBlurEvent         = true;

function LeavingMaskedInputControl (  poBlurEventEvent )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    const docBlurredElement     = poBlurEventEvent.currentTarget.id;

    debugger;                                           // Though ESLint complains about it, this breakpoint is indispensable for testing.
    const strBlurMessage        =   'onBlur event called for element '
                                  + docBlurredElement.id
                                  + ' of type ' + docBlurredElement.type
                                  + ' and className = ' + docBlurredElement.className
                                  + ' with input value = ' + docBlurredElement.value;
    console.log ( ScriptInfoForLog ( InputMask_Engine_SCRIPTSOURCE ,
                                     InputMask_Engine_VERSION ,
                                     InputMask_Engine_LastUpdated ,
                                     strBlurMessage ) );
}   // function LeavingMaskedInputControl


function InputMask_Engine ( )
{
    const strMethodName         = LLCommon.GetNameOfCurrentFunction ( );

    InputMask_Engine.VERSION    = InputMask_Engine_VERSION;

    var oUIDisplaySubTypes;
    var intNInputMasks;

    InputMask_Engine.intNErrors = 0;

    InputMask_Engine.fIsInitialized =  typeof InputMask_Engine.fIsInitialized === 'undefined' ? false : InputMask_Engine.fIsInitialized;

    if ( !InputMask_Engine.fIsInitialized )
    {
        InitialiseMe ( );
        InputMask_Engine.fIsInitialized = true;
    }   // if ( !InputMask_Engine.fIsInitialized )


    function InitialiseMe ( )
    {
        const strMethodName            = LLCommon.GetNameOfCurrentFunction ( );

        const JQUERY_CLASS_NAME_PREFIX = '.';

        console.log ( 'LeadLife_InputMask_Engine version ' + InputMask_Engine.VERSION.toFixed ( 3 ) + ' initializing' );
        debugger;                                       // Though ESLint complains about it, this breakpoint is indispensable for testing.

        if ( typeof oUIDisplaySubTypes === 'undefined' )
        {
            oUIDisplaySubTypes = LLCommon.DoAjax ( 'GetInputMasks', 'GET' );
            intNInputMasks = oUIDisplaySubTypes.DisplaySubTypes.length;
        }

        LLCommon.Trace ( 'LeadLife Input Mask Engine: Registering ' + intNInputMasks + ' input masks' );

        var strSelectByClassName;

        for ( var intJ = 0;
                  intJ < intNInputMasks;
                  intJ++ )
        {
            strSelectByClassName = JQUERY_CLASS_NAME_PREFIX + oUIDisplaySubTypes.DisplaySubTypes [ intJ ].ClassName;

            try
            {
                $( strSelectByClassName ).mask ( oUIDisplaySubTypes.DisplaySubTypes [ intJ ].Mask );
                LLCommon.Trace (   'LeadLife Input Mask Engine function ' + strMethodName + ': Input Mask # ' + ( intJ + 1 )
                              + ': SubTypeId = ' + oUIDisplaySubTypes.DisplaySubTypes[ intJ ].SubTypeId
                              + ', SubTypeName = ' + oUIDisplaySubTypes.DisplaySubTypes[ intJ ].SubTypeName
                              + ', ClassName = ' + oUIDisplaySubTypes.DisplaySubTypes[ intJ ].ClassName
                              + ', Mask = ' + oUIDisplaySubTypes.DisplaySubTypes[ intJ ].Mask
                              + ', Placeholder = ' + oUIDisplaySubTypes.DisplaySubTypes[ intJ ].Placeholder
                );

                if ( _CaptureBlurEvent )
                {
                    $ ( strSelectByClassName ).each ( function ( index )
                    {
                        this.addEventListener ( 'blur' ,
                                                 LeavingMaskedInputControl );
                        LLCommon.Trace ( 'LeadLife Input Mask Engine function' + strMethodName + ': LeavingMaskedInputControl onBlur event registered on button ' + index + ": " + $ ( this ).attr ( 'id' ) );
                    });
                }   // if ( _CaptureBlurEvent )
            }
            catch ( ex )
            {
                ++InputMask_Engine.intNErrors;
                LLCommon.Trace ( 'LeadLife Input Mask Engine function ' + strMethodName + ': An exception arose while applying input masks, as follows: ' + LLCommon.LogException ( ex ) );
            }
        }   // for ( var intJ = 0; intJ < intNInputMasks; intJ++ )

        LLCommon.Trace ( 'LeadLife_InputMask_Engine version ' + InputMask_Engine.VERSION.toFixed ( 3 ) + ' initialized' );
    }   // InitialiseMe

    InputMask_Engine.Reload = InitialiseMe;
}   // function InputMask_Engine


LLCommon.Trace ( 'LeadLife_InputMask_Engine is registering a DOMContentLoaded event.' );

window.addEventListener ( 'DOMContentLoaded', ( event ) =>
{
    console.log ( 'DOMContentLoaded event registered by LeadLife_InputMask_Engine Begin' );
    debugger;

    try
    {
        if ( typeof window._LeadLife_InputMask_Engine === 'undefined' )
        {   // Initialize the input mask module unless this routine already ran successfully.
            const engine = InputMask_Engine ( );

            if ( InputMask_Engine.intNErrors === 0 )
            {   // Mark this routine as executed unless one or more run-time exceptions arose.
                const _LeadLife_InputMask_Engine = InputMask_Engine.bind ( window );
                console.log ( 'LeadLife Input Mask Engine initialization succeded without errors.' );
            }   // TRUE (anticipated outcome) block, if ( InputMask_Engine.intNErrors === 0 )
            else
            {
                console.log ( 'LeadLife Input Mask Engine initialization encountered and logged ' + InputMask_Engine.intNErrors + ' errors.' );
            }   // FALSE (unanticipated outcome) block, if ( InputMask_Engine.intNErrors === 0 )
        }
        else
        {
            console.log ( "LeadLife Input Mask Engine version " + InputMask_Engine.VERSION + ' reloading in a new document ready event' );
            InputMask_Engine.Reload ( );
            console.log ( 'LeadLife Input Mask Engine version ' + InputMask_Engine.VERSION + ' reinitialized from stored list of input masks' );
        }
    }
    catch ( ex )
    {
        LLCommon.LogException ( ex );
    }

    console.log ( 'DOMContentLoaded event registered by LeadLife_InputMask_Engine Done' );
});

LLCommon.Trace ( 'LeadLife_InputMask_Engine DOMContentLoaded event is registered with the Window object.' );
console.log ( ScriptInfoForLog ( InputMask_Engine_SCRIPTSOURCE , InputMask_Engine_VERSION , InputMask_Engine_LastUpdated , 'loaded' ) );
