/*eslint-env browser*/
/*global $ __version _fDebugLogging _LeadLifeJSHelpers ARRAY_FIRST_ELEMENT ARRAY_INVALID_ELEMENT ARRAY_IS_EMPTY bootbox ELEMENT_HIDE ELEMENT_SHOW EMPTY_STRING EMPTY_STRING_LENGTH FULL_STOP GetLeadIdFromQueryString GetNameOfCurrentFunction GetParameterFromURLFormOrLocalStorage INDEXOF_NOT_FOUND KeyWordDispositionMap KeyWordHighLighter LeftPadInteger LLCommon NUMERIC_PLUS_ONE NUMERIC_ZERO REGEXP_ESCAPE_CHARACTER REGEXP_GLOBAL_MATCH ScriptInfoForLog ShowOrHideElement SPACE_CHARACTER UNDERSCORE_CHAR*/

const STTVideoPlayer_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );    // Ignore the complaint raised by ESLint that this constant is unreferenced.
const STTVideoPlayer_VERSION      = 1.30;
const STTVideoPlayer_LastUpdated  = '2026/01/04 23:05:00 CDT';

console.log ( ScriptInfoForLog ( STTVideoPlayer_SCRIPTSOURCE ,
                                 STTVideoPlayer_VERSION ,
                                 STTVideoPlayer_LastUpdated ,
                                 'loading' ) );

/**
 * STT_VideoPlayer.js

/**
 * @license
 *
 * Copyright 2021-2025 SalesTalk Technologies, LLC. <https://SalesTalk.ai/>
 * This code is proprietary to its owners.
 * You are hereby granted a non-exclusive license to allow the code to execute
 * in your Web browser.
 *
 * ToDo: Figure out why setting the poster property is failing.
 */

/*
    ============================================================================
    Module Name:        STT_VideoPlayer.js

    Module Goal:        This module implements the SalesTalk Technologies
                        Instrumented Video Player, which leverages our
                        patent-pending Story So Far module to provide a detailed
                        report on individual visitors' interactions with videos
                        provided to them in system-generated email messages.

    Dependencies:       The following JavaScript libraries must be included in
                        the ORDER LISTED and BEFORE this script is called into
                        the document data stream.

                            LeadLifeJSHelpersGlobals.js Version 1.003 or higher
                            LeadLifeJSHelpersLib.js     Version 1.296 or higher
                            KeyWordHighLighter.js       Version 0.143 or higher

    Module Author:      David A. Gray

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       By Remark/Brief Description
    ---------- -- --------------------------------------------------------------
    2020/12/30 DG MVP - Behavior mapping is not yet implemented, but the stubs
                        exist as two event sinks.

    2020/12/31 DG Incorporate the analytics code.

    2021/01/06 DG Make the whole page respond to a mouse click.

    2021/01/07 DG The whole page mouse click usurped the buttons.

    2021/01/09 DG Incorporate the new analytics method that Bud created.

    2021/01/10 DG Compensate for SalesTalk stripping the protocol and for the
                  Play event also raising the Playing event.

    2021/01/11 DG Experiment with eliminating the Playing event sink and set the
                  Poster attribute.

    2021/01/12 DG Implement onpagehide and onunload event sinks, both of which
                  fire specially marked instances of TrackEvent.

    2021/01/14 DG Suppress onpagehide and onunload event sinks when the onended
                  event has first arisen.

    2021/01/15 DG Relabel the 'Pause Play' event as 'Pause Playing' so that all
                  event names use the present participle verb.

    2021/01/16 DG Compute engagement time and return it as the Duration on all
                  events.

    2021/01/20 DG Implement YouTube playback.

    2021/02/13 DG Implement event tracking when a YouTube video plays.

    2021/02/14 DG Start keeping track of the time when play begins for the first
                  time.

    2021/02/14 DG Log the PageLoaded event as Player Opened.

    2021/04/20 DG Increase the player version from 0.12 to 0.14.

                  1) LogException: Truncate message at character position 128,
                     and wrap the Ajax call to SendToTrace in a try/catch block.

                  2) ComputeEngagementTime: Cover the case when the event ID is
                     Player Opened by returning zero.

                  3) GetLocalNowAsString: Correct the erroneous addition of 1 to
                     the day of the month returned by getDate, which returna an
                     ordinal day of the month.

    2021/04/22 DG TrackEventCallback: Eliminate double-posting of Player Opened.
                  Replace TAB characters in output with SPACE characters in all
                  console logging operations, and replace calls to console.log
                  with console.info throughout.

    2022/06/29 DG Create this new source file from known working code that was
                  embedded into STT_VideoPlayer.HTML.

    2022/06/30 DG Make tracking more robust, lay the groundwork for hosting a
                  form that sends email, and make processing database agnostic.

    2022/07/05 DG 1) Though it supports ternary expressions, it appears that
                     JavaScript cannot assign the value of a conditional
                     expression to a variable.

                  2) Replace the folllowing functions with identical methods on
                     the _LeadLifeJSHelpers object:

                     - AddProtocolWhenMissing
                     - ApplyMillisecondsFixups and its dependencies
                     - ComputeEngagementTime
                     - LogException
                     - PostEvent
                     - TrackEvent

                     The net result of the above is that there is a single
                     instance of each function to maintain and support.

    2022/07/10 DG Finish implementing email.

    2022/10/23 DG Adjust for breaking changes in behavior of _LeadLifeJSHelpers.

    2022/12/18 DG Adapt to support dual use as either a player for audio and
                  video files or a viewer for transcripts of call recordings.

    2022/12/18 DG v. 0.89 Change the KeyWordHighLighter constroctor call to
                          accept an array of KeywordDispositionMap objects in
                          place of the simple array of CSS class name strings.

    2022/12/21 DG v. 0.90 Resize the window when the document URL points to a
                          text file.

    2022/12/24 DG v. 0.91 Resolve issues that appear to be preventing player
                          events being recorded in the Story-So-Far.

    2023/01/06 DG v. 0.94 Bypass the keyword highlighter when domain or tenant
                          ID is absent (undefined).

    2023/01/08 DG v. 0.95 When _intPlayerMode is equal to _MODE_IS_TRANSCRIPT,
                          reduce the width of the window from 75% to 50%,
                          leaving the height set to 75% of the available window.

    2023/02/02 DG v. 0.96 Set path extension to .MP3 to coerce _intPlayerMode to
                          _MODE_IS_AUDIOVISUAL when there is no usable extension
                          at the end of the video URL string.

    2023/04/26 DG v. 0.97 When fed a CallRail URL, swap it for a copy of the MP3
                          stored in a PURL repository.

    2023/05/19 DG v. 0.98 Define a KeyWordDispositionMap named Words2ActionInput
                          associated with CSS selector Words2ActionInputLabel,
                          and implement a keyword search tool. Upon discovering
                          that TrackEventCallback ignores the state of the
                          _fCanTrackBehavior flag, amend it to do nothing unless
                          the flag is set to Boolean TRUE.

    2023/06/29 DG v. 0.99 Enable the player to respond to updated player URLs.

    2023/07/15 DG v. 1.00 Completely suppress warrantless calls to TrackEvent to
                          report meaningless 'Player Closed' events that are the
                          result of programmed state changes as opposed to real
                          events that arise during a playback session.

    2023/07/21 DG v. 1.01 Account for _LeadLifeJSHelpers.GetUrlParameter being
                          renamed GetParameterFromURLFormOrLocalStorage and made
                          a global function.

    2023/07/23 DG v. 1.02 Account for _LeadLifeJSHelpers.StringStartsWith being
                          made globally visible.

    2023/07/24 DG v. 1.03 Account for LeadLifeJSHelpers.GetLeadIdFromQueryString
                          being made globally visible.

    2023/07/30 DG v. 1.04 Implement public GetNameOfCurrentFunction to set
                          string strMethodName and use it throughout console log
                          messages.

    2023/08/08 DG v. 1.05 Adjust to acommodate renaming of _LeadLifeJSHelpers
                          property strPageTitle to PageTitle, a breaking change.
                          The objective of the change is eliminating Hungarian
                          prefixes in externally visible property names.

                          Since this is a breaking change, LeadLifeJSHelpers.js
                          and this library must be promoted together.

    2023/08/13 DG v. 1.06 Account for renaming of dtmUtcOffset in the
                          LeadLifeJSHelpers object to PageLoadTime_JS_Date. Note
                          that this is a breaking change, meaning that both libs
                          must be upgraded together.

    2023/08/16 DG v. 1.07 1) Replace the Ajax URL that was hard coded to send
                             via SalesAcceleration so that it respects the
                             current database per in LLCommon.AjaxUrlPrefix.

                          2) Account for renaming of strPageTitle to PageTitle
                             in the LeadLifeJSHelpers object. Since this is a
                             breaking change, both libs must be upgraded
                             together.

    2023/08/21 DG v. 1.08 Make the VCR button display more compact.

    2023/08/23 DG v. 1.09 Apply the new CSS selector, STT_KeywordPicker, to the
                          KeyWordList SELECT element.

    2023/08/25 DG v. 1.10 Implement displaying Notes as if they were Transcripts.

    2023/08/28 DG v. 1.11 Account for consolidating DoAjax and LogException into
                          LLCommon.js.

    2023/09/02 DG v. 1.12 In function STTProcessMedia, replace a direct attack
                          on the className property of EmailForm4AgentContainer
                          with a call to global function ShowOrHideElement,
                          passing Boolean constant ELEMENT_SHOW. This change
                          makes the code immune to other CSS classes that may be
                          assigned to that element by preserving them, whereass
                          the original implementation would have replaced them
                          with the STT_ShowElement selector.

    2023/09/17 DG v. 1.14 Replace most constants from LeadLifeJSHelpers with
                          constants defined at global scope in LLCommon, and
                          substitute AJAX URL prefix LLCommon.AJaxUrlPrefix for
                          LeadLifeJSHelpers.AJaxUrlPrefix, which was removed due
                          to being unreliable, and add script name, version, and
                          last modified date to major console log messages.

    2023/09/22 DG v. 1.15 List script version details in console log messages.

    2023/10/07 DG v. 1.16 Apply CSS selectors STT_TranscriptToolz_TR1 and
                          STT_TranscriptToolz_TR2 to the two table rows that
                          comprise the contents of the sticky zone, and replace
                          all className settings with calls to classList.add, a
                          more granular mechanism for applying styles to HTML
                          elements.

    2023/10/09 DG v. 1.17 Remove CSS selector STT_FlushRight from the classList
                          of the nameless TD that contains the VCR buttons.

    2023/10/19 DG v. 1.18 Replace all instances in a transcript of a period
                          followed by a space with a period followed by a
                          newline, implementeed as a new MakeFullStopsVisible
                          function that is private to function STTProcessMedia.

    2023/11/26 DG v. 1.19 Implement an optional argument, pAudioPlaybackUri, to
                          STTProcessMedia that identifies a URL to be injected
                          into small audio playback and download buttons.

    2023/12/08 DG v. 1.20 1) Implement crude support for using ChatGPT to
                             generate transcript summaries.

                          2) Explictly set the type property of all BUTTON
                             elements to 'button' to prevent them causing an
                             unexpected submit event.

    2023/12/12 DG v. 1.21 Implement recording playback and summarization for any
                          transcript displayed via the Story-So-Far.

    2024/01/26 DG v. 1.22 Implement playback of WAV files.

    2024/03/31 DG v. 1.23 Condition showing the form on activation of ShareMedia
                          button, and implement multiple TO addresses.

    2024/05/11 DG v. 1.24 Replace virtually all calls to console.log with calls
                          to LLCommon.Trace, which can be centrally configured
                          to suppress logging.

    2024/07/17 DG v. 1.25 Update the revision date so that it agrees with the
                          time stamp in the local file system. Otherwise, this
                          module is the same as version 1.24.

    2024/07/19 DG v. 1.26 Substitute the native string.Prototype.startsWith
                          function for my polyfill, LLCommon.StringStartsWith.

    2025/04/02 DG v. 1.27 Adjust the date stamp to force recomputation of the
                          Subresource Integrity digest string.

    2025/06/22 DG v. 1.28 Actively varify that the specified url points to an
                          existing resource.

    2025/07/21 DG v. 1.29 See to it that every function defines a local string
                          named strMethodName that contains its name.

    2026/01/04 DG v. 1.30 Account for relocation of UQFileNameFromHrefOrPathName
                          from LeadLifeJSHelpersLib to LLCommon.
    ============================================================================
*/

const STT_VIDEOPLAYER_TRANSCIPT_TOOLS_CONTAINER_ID          = 'STT_VideoPlayer_Transcipt_Tools_Container';
const STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID             = 'STT_VideoPlayer_TranscriptControlTools';
const STT_VIDEOPLAYER_TRANSCRIPTHOURGLASS_CONTAINER_ID      = 'STT_VideoPlayer_TranscriptHourGlass_Container';
const STT_VIDEOPLAYER_TRANSCRIPTHOURGLASS_BUBBLEWRAP_ID     = 'STT_VideoPlayer_TranscriptHourGlass_BubbleWrap';
const STT_HOURGLASS_GIF_URI_ID                              = 'STT_Hourglass_GIF_URI';
const STT_HOURGLASS_ID                                      = 'STT_Hourglass';
const STT_SUMMARIZE_BUTTON_BOX_PADDING                      = 'padding-top: 20px; padding-bottom : 20px;';
const STT_NOTE_ID_PREFIX                                    = 'NoteId=';


function GetMediaPlayer (  )
{
    const strMethodName         = GetNameOfCurrentFunction ( );

    try {
        const strPathExtn       = _LeadLifeJSHelpers.GetExtension ( _strVideoURL );

        const objRecordingType  = _LeadLifeJSHelpers.QueryAssociativeArray ( strPathExtn.length > EMPTY_STRING_LENGTH ? strPathExtn : '.mp3' ,  // poKey
                                                                             null ,                                                             // poDefaultValue
                                                                             _aoRecordedMediaTypes );                                           // poArray

        if ( objRecordingType !== null )
        {
            return document.getElementById ( objRecordingType.ElementId );
        }   // TRUE (anticipated outcome) block, if ( objRecordingType !== null )
        else
        {
            console.log ( LLCommon.LogException ( strMethodName + ': Files of the type represented by "' + _strVideoURL + '" are unsupported. This function must return NULL.' ) );
            return null;
        }   // FALSE (unanticipated outcome) block, if ( objRecordingType !== null )
    }
    catch ( ex )
    {
        console.log ( LLCommon.LogException ( strMethodName + ': Files of the type represented by "' + _strVideoURL + '" are unsupported. This function must return NULL.' ) );
        return null;
    }
}   // function GetMediaPlayer


function ClipPosition ( )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  IsPauseInTheMiddle

        Function Goal:  Evaluate whether the event arose at the end of play.

        Input:          Since this function gets its inputs from internal state,
                        it has no arguments.

        Output:         True if the current time is greater than zero and less
                        than the length of the clip, otherwise False
        ------------------------------------------------------------------------
    */

    const strMethodName = GetNameOfCurrentFunction ( );

    const docPlayer = GetMediaPlayer ( );

    if ( docPlayer.currentTime === 0 )
    {
        return 1;
    }   // TRUE block, if ( docPlayer.currentTime === 0 )
    else
    {
        if ( docPlayer.duration > docPlayer.currentTime )
        {
            return 2;
        }   // TRUE block, if ( docPlayer.duration > docPlayer.currentTime )
        else
        {
            return 3;
        }   // FALSE block, if ( docPlayer.duration > docPlayer.currentTime )
    }   // FALSE block, if ( docPlayer.currentTime === 0 )
}   // function ClipPosition


function ComputeSrc ( pstrVideoURL , pdblStartIndex , pintStopIndex )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  ComputeSrc

        Function Goal:  Respond to events raised by the Video Player tag and the
                        body tag of the HTML page that contains it by
                        interpreting the behavior in the context of preveious
                        events raised by the player.

        Input:          pstrVideoURL   = String representation of video URL

                        pdblStartIndex = Start index (position) expressed as a
                                         double-precision floating point number

                        pintStopIndex  = Stop index (position) expressed as an
                                         integer

        Output:         This function returns the string that is assigned to the
                        src property of the player, and includes any start and
                        stop indices provided as input.

        Remarks:        Though passed in as strings, both pdblStartIndex and
                        pintStopIndex are converted to the represntations of
                        their respective native values, both of which are
                        expressed in seconds or fractions thereof since the
                        beginning of the clip.

                        Moreover, although both have default values, zero in
                        the case of pdblStartIndex and minus one in the case
                        of pintStopIndex, they are omitted from the returned
                        URL unless their values exceed the defaults.

                        A string that specifies both values looks like the
                        following.

                            https://www.MyDomain/VideoClip.mp4#t=15,20

                        In the interest of robust performance, invalid values
                        are ignored.

        Reference:      10 Advanced Features In The HTML5 <video> Player
                        https://blog.addpipe.com/10-advanced-features-in-html5-video-player/#startorstopthevideoatacertainpointortimestamp
        ------------------------------------------------------------------------
    */

    const strMethodName   = GetNameOfCurrentFunction ( );

    const TIMESTAMP_TOKEN = '#t=';

    const strStartIndex   = pdblStartIndex > _LeadLifeJSHelpers.NUMERIC_ZERO      ? pdblStartIndex.toString ( ) : EMPTY_STRING;
    const strStopIndex    = pintStopIndex  > _LeadLifeJSHelpers.NUMERIC_MINUS_ONE ? pintStopIndex.toString ( )  : EMPTY_STRING;

    if ( strStartIndex === EMPTY_STRING && strStopIndex === EMPTY_STRING )
    {
        return pstrVideoURL;
    }   // TRUE (Both indices have their default values.) block, if ( strStartIndex === EMPTY_STRING && strStopIndex === EMPTY_STRING )

    if ( strStartIndex !== EMPTY_STRING && strStopIndex !== EMPTY_STRING )
    {
        return pstrVideoURL + TIMESTAMP_TOKEN + strStartIndex + _LeadLifeJSHelpers.COMMA + strStopIndex;
    }   // TRUE (Neither index has its default value.) block,     if ( strStartIndex !== EMPTY_STRING && strStopIndex !== EMPTY_STRING )

    if ( strStartIndex !== EMPTY_STRING )
    {
        return pstrVideoURL + TIMESTAMP_TOKEN + strStartIndex;
    }   // TRUE block, if ( strStartIndex !== EMPTY_STRING )
    else
    {
        return pstrVideoURL + TIMESTAMP_TOKEN + _LeadLifeJSHelpers.COMMA + strStopIndex;
    }   // FALSE block, if ( strStartIndex !== EMPTY_STRING )
}   // function ComputeSrc


function TrackEventCallback ( pstrEventIdString )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  TrackEventCallback

        Function Goal:  Respond to events raised by the Video Player tag and the
                        body tag of the HTML page that contains it by
                        interpreting the behavior in the context of preveious
                        events raised by the player.

        Input:          pstrEventIdString   = String passed into event delegate
                                              function

        Output:         This function returns nothing, and has only side effects
                        consisting of messages written onto the JavaScript
                        console and appended to the data base table that serves
                        as the backing store for an application event log.

        Remarks:        This single event delegate responds to every event
                        raised in the page that hosts it.

                        The value of argument pstrEventIdString causes its
                        behavior to match the type of event to which it
                        responded. Global variables that might become object
                        properties in a future version maintain state
                        information during the lifetime of the page that hosts
                        it.

                        For clarity, player state evaluation is handled by a set
                        of functions that have names that start with Is, and
                        logging is delegated to PostEvent, also a void function.
        ------------------------------------------------------------------------
    */

    const strMethodName    = GetNameOfCurrentFunction ( );

    const dtmCurrEventTime = new Date ( );

    console.info    ( 'In function TrackEvent: Arguments: pstrEventIdString = ' + pstrEventIdString );
    console.info    ( '                                   dtmCurrEventTime  = ' + dtmCurrEventTime.getTime ( ) );
    console.info    ( '                        _fCanTrackBehavior           = ' + _fCanTrackBehavior );

    //  ------------------------------------------------------------------------
    //  The Ready event listener, the anonymous function that fires when JQuery
    //  raises the $(document).ready event, invokes this function.
    //
    //  Its goal is to independently log the Click event that opened the player,
    //  by which I mean that the event arises as a direct result of events
    //  raised in the context of the document that hosts the player.
    //
    //  The "Click" event recorded by the SalesTalk system that hosts the
    //  present embodiment of the 2KnowWho engine is temporally unreliaable,
    //  since it is communicated indirectly to the analytics module that
    //  records events in the Story-So-Far for the person who invoked the
    //  player. By the time it passes through the intermediaries that stand
    //  between the mouse click in the prospect's email reader and the
    //  analytics module, the player has opened as much as several seconds
    //  earlier. Even the Play event may have already arisen and been logged.
    //
    //  Having the player log its own event creates a complete, temporally
    //  accurate chronology of the engagement activity.
    //  ------------------------------------------------------------------------

    const dtmEngagementSeconds = _LeadLifeJSHelpers.ComputeEngagementTime ( pstrEventIdString ,
                                                                            dtmCurrEventTime );

    if ( _fCanTrackBehavior && _intPlayerMode === _MODE_IS_AUDIOVISUAL )
    {
        switch ( pstrEventIdString )
        {
            case 'Player Opened':
                _LeadLifeJSHelpers.PostEvent ( pstrEventIdString,
                                               dtmCurrEventTime ,
                                               dtmEngagementSeconds ,
                                               _strFileBaseName );
                break;  // case 'Player Opened'

            case 'Player Playing':
            case 'Player Paused':
                switch ( ClipPosition ( ) )
                {
                    case 3:
                        // This case should never arise because it should be trumped by the ended event.
                        break;
                    case 2:
                        {   // Set up a lexical scope around this docPlayer instance.
                            const docPlayer = GetMediaPlayer ( )
                            _LeadLifeJSHelpers.PostEvent ( pstrEventIdString + ' at ' + docPlayer.currentTime.toFixed ( ) + ' second mark' ,
                                                           dtmCurrEventTime ,
                                                           dtmEngagementSeconds ,
                                                           _strFileBaseName );
                        }   // End the lexical scope around this docPlayer instance.

                        break;  // case 2: The event happened with the clip positioned in the middle.
                    case 1:
                        _LeadLifeJSHelpers.PostEvent ( pstrEventIdString + ' at beginning of clip' ,
                                                       dtmCurrEventTime ,
                                                       dtmEngagementSeconds ,
                                                       _strFileBaseName );
                        break;  // case 1: The event happened with the clip positioned at the beginning.
                }   // switch ( ClipPosition ( ) )

                break;  // case 'Player Paused':

            case 'Player Ended':
                _LeadLifeJSHelpers.PostEvent ( pstrEventIdString ,
                                               dtmCurrEventTime ,
                                               dtmEngagementSeconds ,
                                               _strFileBaseName );
                break   // case 'Player Ended':

            case 'Player Closed':
                if (  _intPlayerMode === _MODE_IS_TRANSCRIPT )
                {
                    _LeadLifeJSHelpers.PostEvent ( pstrEventIdString.replace ( 'Player Closed' ,
                                                                               'Viewer Closed' ),
                                                   dtmCurrEventTime ,
                                                   dtmEngagementSeconds ,
                                                   _strFileBaseName );
                }   // TRUE (The player is displaying a transcript or other text file.) block, if (  _intPlayerMode === _MODE_IS_TRANSCRIPT )
                else
                {
                    if ( !_fHasPlayEnded )
                    {
                        _LeadLifeJSHelpers.PostEvent ( pstrEventIdString ,
                                                       dtmCurrEventTime ,
                                                       dtmEngagementSeconds ,
                                                       _strFileBaseName );
                     }  // if ( !_fHasPlayEnded )
                }   // FALSE (The player is performing its legacy video player function.) block, if (  _intPlayerMode === _MODE_IS_TRANSCRIPT )

                break;  // case 'Player Closed'
        }   // switch ( pstrEventIdString )
    }   // if ( _fCanTrackBehavior && _intPlayerMode === _MODE_IS_AUDIOVISUAL )
}  // function TrackEventCallback


function XitField ( poEvent )
{
    const strMethodName = GetNameOfCurrentFunction ( );

    if ( poEvent.target.value.length > EMPTY_STRING_LENGTH )
    {
        if ( !_LeadLifeJSHelpers.IsEmailAddressValid ( poEvent.target.value ) )
        {
            alert ( 'ERROR: The email address entered into the' + poEvent.target.id + ' text box is invalid.' , 'native' );
        }   // if ( !_LeadLifeJSHelpers.IsEmailAddressValid ( poEvent.target.value ) )
    }   // TRUE (anticipated outcome) block, if ( poEvent.target.value.length > EMPTY_STRING_LENGTH )
    else
    {
        alert ( 'CAUTION: You must enter a valid email address into the ' + poEvent.target.id + ' text box.' , 'native' );
    }   // FALSE (unanticipated outcome) block, if ( poEvent.target.value.length > EMPTY_STRING_LENGTH )
}   // function XitField


function XmitMessage ( poEvent )
{
    const strMethodName  = GetNameOfCurrentFunction ( );

    const strFromEmail   = document.getElementById ( 'FromEmail'   ).value;
    const strFromPhone   = document.getElementById ( 'FromPhone'   ).value;
    const strToEmail     = document.getElementById ( 'ToEmail'     ).value;
    const strSubject     = document.getElementById ( 'Subject'     ).value;
    const strMessageBody = document.getElementById ( 'MessageBody' ).value
                           + _LeadLifeJSHelpers.LINE_FEED_CHAR + _LeadLifeJSHelpers.LINE_FEED_CHAR + strFromEmail
                           + _LeadLifeJSHelpers.LINE_FEED_CHAR + strFromPhone

    if ( _fDebugLogging )
    {
        console.log ( 'strFromEmail   = ' + strFromEmail );
        console.log ( 'strFromPhone   = ' + strFromPhone );
        console.log ( 'strToEmail     = ' + strToEmail );
        console.log ( 'strSubject     = ' + strSubject );
        console.log ( 'strMessageBody = ' + strMessageBody );
    }   // if ( _fDebugLogging )

    try
    {
        $.ajax (
        {
                type     : 'POST',
                url      : LLCommon.AjaxUrlPrefix + 'Open/SendHTMLEmailPost' ,
                async    : false ,
                cache    : false ,
                data     :  {
                                'MailFromEmail' : strFromEmail ,
                                'MailToEmail'   : strToEmail ,
                                'Subject'       : strSubject ,
                                'Body'          : strMessageBody ,
                                'TenantID'      : _tenantid ,
                                'DomainId'      : _domainid
                           } ,
                success : function ( data )
                {
                    if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                    {   // The above Ajax call returned a value. Capture it.
                        LLCommon.Trace ( data );

                        return data;
                    }   // TRUE (anticipated outcome) block, if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                    else
                    {
                        LLCommon.Trace ( 'SendHTMLEmailPost returned the empty string' );

                        return EMPTY_STRING;
                    }   // FALSE (unanticipated outcome) block, if ( ( data !== undefined ) && ( data !== null ) && ( data !== EMPTY_STRING ) )
                } ,
                error: function ( jqXHR , textStatus , errorThrown )
                {
                    var strErrrMessage =   'SendHTMLEmailPost failed, returning ' + textStatus
                                         + SPACE_CHARACTER + jqXHR.responseText
                                         + SPACE_CHARACTER + errorThrown;
                    LLCommon.Trace ( strErrrMessage );
                    LLCommon.LogException ( strErrrMessage );

                    return strErrrMessage;
                }
        });

        //  --------------------------------------------------------------------
        //  Hide the form and show the button.
        //  --------------------------------------------------------------------

        HideFormShowButton ( );
    }
    catch ( ex )
    {
        LLCommon.Trace ( 'XmitMessage function Exception caught: Message = ' + ex.message + ', Stack Trace = ' + ex.stack );
        LLCommon.LogException ( ex );
    }

    //  ------------------------------------------------------------------------
    //  Set a flag equal to true and initialize an object as a reference to the
    //  element that is the target of the event handler. Inside a WHILE loop,
    //  assign the object variable to the parent of the node to which it refers,
    //  then evaluate its value. When it is nodename (its type, in this context)
    //  is equal to FORM (note the capitalization), the flag is reset, causing
    //  execution to fall out of the loop, after which the Reset method on the
    //  form is called.
    //  ------------------------------------------------------------------------

    var fFoundForm = true;
    var oDomWalker = poEvent.target;

    while ( fFoundForm )
    {
        oDomWalker = oDomWalker.parentNode;

        if ( oDomWalker.nodeName === 'FORM' )
        {
            fFoundForm = false;
        }   // if ( oDomWalker.nodeName === 'FORM' )
    }   // while ( fFoundForm )

    oDomWalker.reset ( );

    const txtMessageBody = document.getElementById ( 'MessageBody' );
    txtMessageBody.value = _strURL4Preamble;

    if ( poEvent.type === 'click' )
    {
        poEvent.preventDefault ( );
    }   // if ( poEvent.type === 'click' )

    return true;
}   // function XmitMessage


function VCRButtonClick ( poEvent )
{
    const strFunctionName   = GetNameOfCurrentFunction ( );

    debugger;

    const docKeywordList    = document.getElementById ( 'KeyWordList' );
    const docCountView      = document.getElementById ( 'VCRContainer_KeywordOccurrencedCount' );
    const docPosView        = document.getElementById ( 'VCRContainer_CurrentPosition' );

    LLCommon.Trace ( strFunctionName + ': Click event raised for VCR button ' + poEvent.currentTarget.id );

    LLCommon.Trace ( '                KeyWordList current value = ' + docKeywordList.value );
    LLCommon.Trace ( '                Current Position          = ' + docPosView.innerText );
    LLCommon.Trace ( '                Keyword Count             = ' + docCountView.innerText );

     VCRMoveCarat ( poEvent.currentTarget.id ,              // pstrButtonId
                    docKeywordList.value ,                  // pstrKeyWordAnchorPrefix
                    docPosView.innerText ,                  // pstrCurrentPosition
                    docCountView.innerText )                // pstrKeyWordCount
    LLCommon.Trace ( strFunctionName + ': Click event handled for VCR button ' + poEvent.currentTarget.id );
}   // function VCRButtonClick


function VCRMoveCarat ( pstrButtonId , pstrKeyWordAnchorPrefix , pstrCurrentPosition , pstrKeyWordCount )
{
    /*
        ------------------------------------------------------------------------
        Function Name:  VCRMoveCarat

        Function Goal:  Construct the ID of the ANCHOR element (tag) to which to
                        move the document to display the text that includes the
                        desired occurrence of the text, then append it to the
                        document HREF property, causing the document to advance
                        or retreat in the viewport as indicated.

        Input:          pstrButtonId            = The element ID of a VCR button
                                                  drives a switch statement that
                                                  determines whether and how the
                                                  current position is adjusted.

                        pstrKeyWordAnchorPrefix = The keyword anchor prefix is
                                                  constructed from the keyword
                                                  by substituting an underscore
                                                  character for each embedded
                                                  space character, so that it is
                                                  a valid HTML element ID part.

                        pstrCurrentPosition     = Since it is read from the
                                                  innerText property of a HTML
                                                  SPAN element, where it serves
                                                  double duty as a UI display
                                                  element, the current position
                                                  is stored as a numeric string.

                        pstrKeyWordCount        = Since it is read from the
                                                  innerText property of a HTML
                                                  SPAN element, where it serves
                                                  double duty as a UI display
                                                  element, the current position
                                                  is stored as a numeric string.

        Output:         This function has only side effects, and returns the
                        reserved Javascript value undefined.
        ------------------------------------------------------------------------
    */

    const strFunctionName               = GetNameOfCurrentFunction ( );

    const docKeywordList                = document.getElementById ( 'KeyWordList' );

    if ( docKeywordList !== null )
    {
        if ( docKeywordList.value.length > EMPTY_STRING_LENGTH )
        {
            const intCurrentPosition    = pstrButtonId === 'KeyWordList'
                                          ? ARRAY_INVALID_ELEMENT
                                          : parseInt ( pstrCurrentPosition );
            const intKeyWordCount       = parseInt ( pstrKeyWordCount );

            var   intNewPosition        = intCurrentPosition;

            switch ( pstrButtonId )
            {
                case 'KeyWordList':
                    intNewPosition      = _LeadLifeJSHelpers.NUMERIC_PLUS_ONE;

                    break;  // case 'KeyWordList'

                case 'VCRButton_fa_step_forward':
                    if ( intKeyWordCount > intCurrentPosition )
                    {
                        intNewPosition++;
                    }   // TRUE (anticipated outcome) block, if ( intKeyWordCount > intCurrentPosition )
                    else
                    {
                        alert (   'You have already reached the last occurrence of keyword '
                                + pstrKeyWordAnchorPrefix.replace ( UNDERSCORE_CHAR ,
                                                                    SPACE_CHARACTER + FULL_STOP ) ,
                                'native' );
                    }   // FALSE (unanticipated outcome) block, if ( intKeyWordCount > intCurrentPosition )

                    break;  // case 'VCRButton_fa_step_forward'

                case 'VCRButton_fa_step_backward':
                    if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )
                    {
                        intNewPosition--;
                    }   // TRUE (anticipated outcome) block, if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )
                    else
                    {
                        alert (   'You have already reached the first occurrence of keyword '
                                + pstrKeyWordAnchorPrefix.replace ( UNDERSCORE_CHAR ,
                                                                    SPACE_CHARACTER + FULL_STOP ) , 'native' );
                    }   // FALSE (unanticipated outcome) block, if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )

                    break;  // case 'VCRButton_fa_step_backward'

                case 'VCRButton_fa_fast_forward':
                    if ( intKeyWordCount > intCurrentPosition )
                    {
                        intNewPosition  = intKeyWordCount;
                    }   // TRUE (anticipated outcome) block, if ( intKeyWordCount > intCurrentPosition )
                    else
                    {
                        alert (   'You have already reached the last occurrence of keyword '
                                + pstrKeyWordAnchorPrefix.replace ( UNDERSCORE_CHAR ,
                                                                    SPACE_CHARACTER + FULL_STOP ) , 'native' );
                    }   // FALSE (unanticipated outcome) block, if ( intKeyWordCount > intCurrentPosition )

                    break;  // case 'VCRButton_fa_fast_forward'

                case 'VCRButton_fa_fast_backward':
                    if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )
                    {
                        intNewPosition  = _LeadLifeJSHelpers.NUMERIC_PLUS_ONE;
                    }   // TRUE (anticipated outcome) block, if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )
                    else
                    {
                        alert (   'You have already reached the first occurrence of keyword '
                                + pstrKeyWordAnchorPrefix.replace ( UNDERSCORE_CHAR ,
                                                                    SPACE_CHARACTER + FULL_STOP ) , 'native' );
                    }   // FALSE (unanticipated outcome) block, if ( intCurrentPosition > _LeadLifeJSHelpers.NUMERIC_PLUS_ONE )

                    break;  // case 'VCRButton_fa_fast_backward'

                default:
                    LLCommon.LogException ( strFunctionName + 'Internal error in audio/video/transcript player event handler routine VCRMoveCarat: Argument pstrButtonId value of ' + pstrButtonId + ' was unexpected.' );
            }   // switch ( pstrButtonId )

            LLCommon.Trace ( strFunctionName + ': VCR button clicked = ' + pstrButtonId
                                          + ', Current Position = ' + intCurrentPosition
                                          + ', Keyword Count = ' + intKeyWordCount
                                          + ', New Position = ' + intNewPosition );
            if ( intNewPosition !== intCurrentPosition )
            {
                const docPosView        = document.getElementById ( 'VCRContainer_CurrentPosition' );
                docPosView.innerText    = intNewPosition;

                const strAnchor         =   pstrKeyWordAnchorPrefix
                                          + UNDERSCORE_CHAR
                                          + LeftPadInteger ( intNewPosition ,
                                                             oKeyWordHighLighter.PADDED_ORDINAL_WIDTH );
                _LeadLifeJSHelpers.JumpToElementById ( strAnchor );
            }   // if ( intNewPosition  !== intCurrentPosition )
        }   // TRUE (anticipated outcome) block, if ( docKeywordList.value.length > EMPTY_STRING_LENGTH )
        else
        {
            alert ( 'Please select a keyword from the list.' , 'native' );
            docKeywordList.focus ( );
        }   // FALSE (unanticipated outcome) block, if ( docKeywordList.value.length > EMPTY_STRING_LENGTH )
    }   // TRUE (anticipated outcome) block, if ( docKeywordList !== null )
    else
    {
        LLCommon.LogException ( 'Internal error in audio/video/transcript player: The keyword selection list widget is unexpectedly missing.' );
    }   // FALSE (unanticipated outcome) block, if ( docKeywordList !== null )
}   // function VCRMoveCarat


const _STTVideoPlayer_TrackEvent            = TrackEventCallback.bind ( window );
const STT_VPLAYER_DOCTOOLORGANIZER          = 'docToolOrganizer';

var   _fHasPlayEnded                        = false;

var   oKeyWordHighLighter;
var   _intBehaviorLeadId;
var   _strEmailPerMG;
var   _strPosterURL;
var   _dblStartIndex;
var   _intStopIndex;
var   _strTitle;
var   _strURL4Preamble;
var   _strVideoURL;
var   _strFileBaseName;
var   _strWorkflowIdPerMG;

//  --------------------------------------------------------------------
//  These variables are initialized in the Document Ready event.
//  --------------------------------------------------------------------

var _fCanTrackBehavior;

//  --------------------------------------------------------------------
//  These variables are initialized when an entry is selected from the
//  search SELECT element in the transcript viewer.
//  --------------------------------------------------------------------

var _intSearchPosition;
var _intKeywordMatchCount;

const _MODE_IS_UNSUPPORTED      = 0;
const _MODE_IS_AUDIOVISUAL      = 1;
const _MODE_IS_TRANSCRIPT       = 2;

const _aoExtensionDispositions  = {
                                        '.mp3'  : _MODE_IS_AUDIOVISUAL ,
                                        '.mp4'  : _MODE_IS_AUDIOVISUAL ,
                                        '.m4a'  : _MODE_IS_AUDIOVISUAL ,
                                        '.wav'  : _MODE_IS_AUDIOVISUAL ,
                                        '.txt'  : _MODE_IS_TRANSCRIPT ,
                                        '.ttx'  : _MODE_IS_TRANSCRIPT ,
                                  };

const _aoRecordedMediaTypes     = {
                                        '.mp3'  : { DisplayText : 'audio' , ElementNodeName : 'AUDIO' , ElementId : 'html5AudioPlayer1' } ,
                                        '.mp4'  : { DisplayText : 'video' , ElementNodeName : 'VIDEO' , ElementId : 'html5VideoPlayer1' } ,
                                        '.m4a'  : { DisplayText : 'audio' , ElementNodeName : 'AUDIO' , ElementId : 'html5AudioPlayer1' } ,
                                        '.wav'  : { DisplayText : 'audio' , ElementNodeName : 'AUDIO' , ElementId : 'html5AudioPlayer1' } ,
                                  };

var   _intPlayerMode            = _MODE_IS_UNSUPPORTED;    // This cannot be initialized until the constant is defined.

//  --------------------------------------------------------------------
//  This must come last because the foregoing functions must be defined
//  before the interpreter sees it.
//
//  2022/10/18 - A consequence of deferred loading of the libraries is
//               that the document ready event happens before the script
//               has finished loading. Hence, that event is superseded
//               by the following setTimeout block. The chosen timeout
//               value of 100 milliseconds is sufficiently small that no
//               user is likely to notice.
//
//  2023/06/28 - Replace the setTimeout function with a DOMContentLoaded
//               event listener and eliminate the separate start date,
//               _dtmEngagementStart.
//  --------------------------------------------------------------------

window.addEventListener ( 'DOMContentLoaded' , STTProcessMedia );


async function checkVideoURL ( pstrVideoURL , pstrMethodName )
{
    const strMethodName = GetNameOfCurrentFunction ( );

    try
    {
        const exists = await LLCommon.HttpHead ( pstrVideoURL );

        if ( exists )
        {
            console.log ( pstrMethodName + ': Specified video URL found. URL = ' + pstrVideoURL );
            return true;
        }   // TRUE (anticipated outcome) block, if ( exists )
        else
        {
            console.log ( LLCommon.LogException ( pstrMethodName + ': Specified video URL NOT FOUND. URL = ' + pstrVideoURL ) );
            alert ( 'The requested video, "' + pstrVideoURL + '" cannot be found. This window will now close.' , 'native' );
            window.close ( );
            return false;
        }   // FALSE (unanticipated outcome) block, if ( exists )
    }
    catch ( ex )
    {
        console.log ( LLCommon.LogException(pstrMethodName + 'Error checking video URL: ' + ex.message ) );
        alert ( 'An error occurred while checking the video URL, "' + pstrVideoURL + '", and this window will now close.' , 'native' );
        window.close ( );
        return false;
    }
}   // async function checkVideoURL


function HideFormShowButton ( event )
{
    const strMethodName = GetNameOfCurrentFunction ( );

    ShowOrHideElement ( 'EmailForm4AgentContainer' , ELEMENT_HIDE );
    ShowOrHideElement ( 'ShareMedia'               , ELEMENT_SHOW );

    if ( event !== undefined  )
    {
        event.stopPropagation ( );
        event.preventDefault ( );
    }   // if ( event !== undefined  )
}   // function HideFormShowButton


function ShowShareMediaForm ( )
{
    const strMethodName = GetNameOfCurrentFunction ( );

    //  ------------------------------------------------------------------------
    //  Hide the button and show the form.
    //  ------------------------------------------------------------------------

    ShowOrHideElement ( 'ShareMedia'               , ELEMENT_HIDE );
    ShowOrHideElement ( 'EmailForm4AgentContainer' , ELEMENT_SHOW );
}   // function ShowShareMediaForm


async function STTProcessMedia ( poEventArg , pAudioPlaybackUri )
{
    function AppendPlaybackTools ( pdocPlaybackTools , pAudioPlaybackUri )
    {
        //  --------------------------------------------------------------------
        //  Function Name:      AppendPlaybackTools
        //
        //  Arguments:          pdocPlaybackTools   = Pointer to element that is
        //                                            set aside to hold the
        //                                            transcript recording
        //                                            playback and download link
        //
        //                      pAudioPlaybackUri   = String representation of
        //                                            the absolute URI pointing
        //                                            to the audio or video
        //                                            recording from which the
        //                                            displayed transcript was
        //                                            created
        //
        //  Returns:            When called for the first time on a W2A page,
        //                      the return value is a reference to a new SPAN
        //                      that contains the playback and download URLs.
        //
        //                      Subsequent calls expect to find the playback and
        //                      download elements, each with its own ID, already
        //                      in the page. Hence, the relevant attributes of
        //                      each are updated, and the return value is NULL.
        //
        //  Since playback URLs can be a mixture of types, the type is checked
        //  on all URLs before the state of the DIV is evaluated.
        //  --------------------------------------------------------------------

        const strMethodName             = GetNameOfCurrentFunction ( );

        const strPathExtn               = _LeadLifeJSHelpers.GetExtension ( pAudioPlaybackUri );
        const objRecordingType          = _LeadLifeJSHelpers.QueryAssociativeArray ( strPathExtn.length > EMPTY_STRING_LENGTH ? strPathExtn : '.mp3' ,  // poKey
                                                                                     NULL ,                                                             // poDefaultValue
                                                                                     _aoRecordedMediaTypes );                                           // poArray

        if ( objRecordingType !== null )
        {
            const strRecordingType      = objRecordingType.DisplayText;

            if ( pdocPlaybackTools.children.length === NUMERIC_ZERO )
            {
                //  ------------------------------------------------------------
                //  The new entry is a TABLE element, which contains only a BODY
                //  element composed of a single TR (Table Row) element. The two
                //  controls go into TD (Table Detail) elements inside the row.
                //
                //  Since the elements of the table are just containers for the
                //  named elements, they are nameless.
                //  ------------------------------------------------------------

                const rdocPlaybackTools = document.createElement ( 'table' );
                rdocPlaybackTools.id    = STT_VIDEOPLAYER_TRANSCIPT_TOOLS_CONTAINER_ID;
                const docTableBody      = document.createElement ( 'tbody' );

                docTableBody.id         = STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID;
                rdocPlaybackTools.appendChild ( docTableBody );

                const docRow1           = document.createElement ( 'tr' );
                docTableBody.appendChild ( docRow1 );

                const XScriptSrc        = document.createElement ( strRecordingType );
                XScriptSrc.controls     = true;
                XScriptSrc.id           = 'STT_VideoPlayer_TranscriptRecordingPlayer';
                XScriptSrc.type         = 'button';
                XScriptSrc.title        = 'Click or tap the PLAY button to play the recording from which the transcript below was made.';
                XScriptSrc.src          = pAudioPlaybackUri;

                const docCell1          = document.createElement ( 'td' );
                docCell1.appendChild ( XScriptSrc );
                docRow1.appendChild ( docCell1 );

                //  --------------------------------------------------------------------
                //  Set the download attribute to true so that when the user clicks the
                //  link, the recorded media is downloaded to their machine.
                //  --------------------------------------------------------------------

                const XScrpLnk          = document.createElement ( 'button' );
                XScrpLnk.value          = 'Download locally';
                XScrpLnk.innerHTML      = 'Download locally';
                XScrpLnk.title          = 'Click or tap this link to put a copy of the recording from which the transcript displayed below was created into the DOWNLOADS folder on your device.';
                XScrpLnk.id             = 'STT_VideoPlayer_TranscriptLocalDownloadLink';
                XScrpLnk.type           = 'button';
                XScrpLnk.value          = pAudioPlaybackUri;
                XScrpLnk.onclick        = ( poEvent ) =>
                {
                    debugger;
                    LLCommon.DownloadFile2Client ( poEvent.currentTarget.value );
                };  // XScrpLnk.onclick event listener
                XScrpLnk.classList.add ( 'TranscriptReview_BlueTheme' );        // Color it to match the other similar buttons.

                const docCell2          = document.createElement ( 'td' );
                docCell2.classList.add ( 'STT_VAlign_Middle' );                 // Vertical alignment MUST be applied to the CELL that contains the object(s) to be vertically aligned.
                docCell2.appendChild ( XScrpLnk );
                docRow1.appendChild ( docCell2 );

                return rdocPlaybackTools;
            }   // TRUE (The first time through, the controls must be created AND populated.) block, if ( pdocPlaybackTools.children.length === NUMERIC_ZERO )
            else
            {
                const docPlayback       = document.getElementById ( 'STT_VideoPlayer_TranscriptRecordingPlayer' );

                if ( docPlayback !== null )
                {
                    docPlayback.src     = pAudioPlaybackUri;

                    const docDlBtn      = document.getElementById ( 'STT_VideoPlayer_TranscriptLocalDownloadLink' );

                    if ( docDlBtn !== null )
                    {
                        docDlBtn.value  = pAudioPlaybackUri;
                    }   // TRUE (anticipated outcome) block, if ( docDlBtn !== null )
                    else
                    {
                        LLCommon.LogException ( strMethodName + ': Expected document element STT_VideoPlayer_TranscriptLocalDownloadLink is absent, preventing update of playback URI.' );
                        return null;
                    }   // FALSE (unanticipated outcome) block, if ( docDlBtn !== null )
                }   // TRUE (anticipated outcome) block, if ( docPlayback !== null )
                else
                {
                    LLCommon.LogException ( strMethodName + ': Expected document element STT_VideoPlayer_TranscriptRecordingPlayer is absent, preventing update of playback URI.' );
                    return null;
                }   // FALSE (unanticipated outcome) block, if ( docPlayback !== null )
            }   // FALSE (On subsequent passes, only the playback URLs need attention.) block, if ( pdocPlaybackTools.children.length === NUMERIC_ZERO )
        }   // TRUE (anticipated outcome) block, if ( objRecordingType !== null )
        else
        {
            LLCommon.LogException ( strMethodName + ': Media file type ' + strPathExtn + ' of recording URL ' + pAudioPlaybackUri + ' is unsupported.' );
            bootbox.alert ( 'Media file type <span style="color: #0000ff; background-color : #ffffff; font-weight : bold;">' + strPathExtn + '</span> of recording URL <span style="color: #0000ff; background-color : #ffffff; font-weight : bold;">' + pAudioPlaybackUri + '</span> is unsupported.' )
            return null;
        }   // FALSE (unanticipated outcome) block, if ( objRecordingType !== null )
    }   // function AppendPlaybackTools


    function MakeFullStopsVisible ( pstrFileContents )
    {
        const strMethodName     = GetNameOfCurrentFunction ( );

        const re2Match          = new RegExp (   REGEXP_ESCAPE_CHARACTER
                                               + FULL_STOP
                                               + SPACE_CHARACTER ,
                                               REGEXP_GLOBAL_MATCH );
        const strRepl           = FULL_STOP + _LeadLifeJSHelpers.HTML_LINE_BREAK;
        return LLCommon.IsString ( pstrFileContents )
               ? pstrFileContents.replace ( re2Match , strRepl )
               : EMPTY_STRING;
    }   // private function MakeFullStopsVisible


    const strMethodName         = GetNameOfCurrentFunction ( );

    var   fInputIsNote          = false;

    debugger;

    LLCommon.Trace ( ScriptInfoForLog ( STTVideoPlayer_SCRIPTSOURCE ,
                                     STTVideoPlayer_VERSION ,
                                     STTVideoPlayer_LastUpdated ,
                                       'using LeadLifeJSHelpers version '
                                     + _LeadLifeJSHelpers.VERSION.toFixed ( 3 )
                                     + ' entering function '
                                     + strMethodName ) );
    LLCommon.Trace ( strMethodName + ': poEventArg = ' + poEventArg );

    try
    {
        //  ----------------------------------------------------------------
        //  Per "Window: pagehide event,"
        //
        //      The best event to use to signal the end of a user's session
        //      is the visibilitychange event. In browsers that don't support
        //      visibilitychange the pagehide event is the next-best
        //      alternative.
        //
        //  https://developer.mozilla.org/en-US/docs/Web/API/Window/pagehide_event
        //  ----------------------------------------------------------------

        document.addEventListener ( 'visibilitychange', ( ) =>
        {
            if ( document.visibilityState !== 'visible' && _fCanTrackBehavior && _intPlayerMode === _MODE_IS_AUDIOVISUAL )
            {
                _STTVideoPlayer_TrackEvent ( 'Player Closed' );
            }   // if ( document.visibilityState !== 'visible' && _fCanTrackBehavior && _intPlayerMode === _MODE_IS_AUDIOVISUAL )
        });

        _intBehaviorLeadId      = (    Object.is ( _LeadLifeJSHelpers.STTLeadId , undefined )
                                     || _LeadLifeJSHelpers.STTLeadId === _LeadLifeJSHelpers.NUMERIC_ZERO )
                                            ? GetLeadIdFromQueryString ( )
                                            : _LeadLifeJSHelpers.STTLeadId;
        _strEmailPerMG          = GetParameterFromURLFormOrLocalStorage ( 'll_e' );
        _strWorkflowIdPerMG     = GetParameterFromURLFormOrLocalStorage ( 'll_c' );
        _strPosterURL           = _LeadLifeJSHelpers.AddProtocolWhenMissing ( GetParameterFromURLFormOrLocalStorage ( 'thumbnail' ) );

        //  --------------------------------------------------------------------
        //  1) Since _LeadLifeJSHelpers (the global variable holding a reference
        //     to the LeadLifeJSHelpers Singleton) has a copy of the title that
        //     it gets from the Document Object Model, we'll use it if it's
        //     there, leaving leave the title parameter in the query string as a
        //     fallback.
        //
        //  2) While the first (or only) call to function STTProcessMedia is as
        //     an event listener, which receives an instance of the Event
        //     interface, subsequent calls directly receive the video URL as a
        //     string that is passed into the function via a direct call, saving
        //     an expensive regular expression.
        //
        //  I considered (and even tested) using the hasOwnProperty of the
        //  poEventArg object. However since poEventArg is derived from Event,
        //  its currentTarget property is inherited. Evaluating whether argument
        //  poEventArg is a string is comparatively cheaper.
        //  --------------------------------------------------------------------

        _strTitle               = LLCommon.IsString ( _LeadLifeJSHelpers.PageTitle ) && _LeadLifeJSHelpers.PageTitle.length > EMPTY_STRING_LENGTH ? _LeadLifeJSHelpers.PageTitle : GetParameterFromURLFormOrLocalStorage ( 'title' );
        _strVideoURL            = LLCommon.IsString ( poEventArg ) ? poEventArg : _LeadLifeJSHelpers.AddProtocolWhenMissing ( GetParameterFromURLFormOrLocalStorage ( 'm4vurl' ) );

        //  --------------------------------------------------------------------
        //  When _strVideoURL is equal to the empty string, there is nothing to
        //  do. More significantly, processing beyond this point would set flags
        //  in incorrect positions, causing the display to render parts that are
        //  intended to be hidden.
        //  --------------------------------------------------------------------

        if ( _strVideoURL.length > EMPTY_STRING_LENGTH )
        {
            if ( _strVideoURL.startsWith ( STT_NOTE_ID_PREFIX ) )
            {
                fInputIsNote    = true;
                _intPlayerMode  = _MODE_IS_TRANSCRIPT;
            }   // TRUE (The input is the ID of a Note.) block, if ( _strVideoURL.startsWith ( STT_NOTE_ID_PREFIX ) )
            else
            {
                /*
                    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    Sample CallRail URL: https://salestalktech.com/InsuranceDB/Open/GetCallRailRecording?callRailUrl=https://app.callrail.com/calls/CAL27b45f5f135b4730b63d139b124d132d/recording/redirect%3faccess_key%3d84113e6e40d815d908d7&databaseName=InsuranceDB&domainName=InsuranceDB&LeadId=1746
                    --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                */

                if ( _strVideoURL.toLowerCase ( ).startsWith ( 'https://app.callrail.com/calls/' ) && _strVideoURL.indexOf ( '?access_key=' ) > INDEXOF_NOT_FOUND )
                {
                    _strVideoURL        = LLCommon.DoAjax ( 'GetCallRailRecording' ,
                                                            'GET' ,
                                                            { 'callRailUrl'  : _strVideoURL ,
                                                              'databaseName' : _LeadLifeJSHelpers.STTDatabaseName ,
                                                              'domainName'   : _LeadLifeJSHelpers.STTDomainName ,
                                                              'LeadId'       : _LeadLifeJSHelpers.STTLeadId
                                                            } );
                }   // TRUE (The URL points to a resource on the CallRail service.) block, if ( _strVideoURL.toLowerCase ( ).startsWith ( 'https://app.callrail.com/calls/' ) && _strVideoURL.indexOf ( '?access_key=' ) > INDEXOF_NOT_FOUND )
                else
                {
                    debugger;

                    const fShouldContinue = await checkVideoURL ( _strVideoURL , strMethodName );
                    debugger;
                    if ( !fShouldContinue ) return;
                }   // FALSE (The URL points to a resource on the PURL site or elsewhhere.) block, if ( _strVideoURL.toLowerCase ( ).startsWith ( 'https://app.callrail.com/calls/' ) && _strVideoURL.indexOf ( '?access_key=' ) > INDEXOF_NOT_FOUND )

                const strPathExtn       = _LeadLifeJSHelpers.GetExtension ( _strVideoURL );
                _intPlayerMode          = _LeadLifeJSHelpers.QueryAssociativeArray ( strPathExtn.length > EMPTY_STRING_LENGTH ? strPathExtn : '.mp3' ,
                                                                                      _MODE_IS_UNSUPPORTED ,
                                                                                      _aoExtensionDispositions );
                _dblStartIndex          = parseFloat ( GetParameterFromURLFormOrLocalStorage ( 'start' , '0'  ) );
                _intStopIndex           = parseInt   ( GetParameterFromURLFormOrLocalStorage ( 'stop'  , '-1' ) );

                _strURL4Preamble        = ( _intPlayerMode === _MODE_IS_AUDIOVISUAL
                                                ? 'Video URL: '
                                                : 'Transcript URL: ' )
                                            + _strVideoURL.replace ( new RegExp ( SPACE_CHARACTER ,
                                                                                  _LeadLifeJSHelpers.REGEXP_GLOBAL_MATCH ) ,
                                                                 _LeadLifeJSHelpers.SPACE_URLENCODED )
                                            + ( _strTitle.length > EMPTY_STRING_LENGTH
                                                 ?   _LeadLifeJSHelpers.LINE_FEED_CHAR
                                                   + 'Video Title: '
                                                   + _strTitle
                                                   + _LeadLifeJSHelpers.LINE_FEED_CHAR
                                                   + _LeadLifeJSHelpers.LINE_FEED_CHAR
                                                 :   _LeadLifeJSHelpers.LINE_FEED_CHAR
                                            + _LeadLifeJSHelpers.LINE_FEED_CHAR );
                _strFileBaseName        = LLCommon.UQFileNameFromHrefOrPathName ( _strVideoURL );

                //  ----------------------------------------------------------------
                //  Though C# can store the value of a conditional expression in a
                //  variable, it appears that JavaScript cannot.
                //
                //  See https://stackoverflow.com/questions/10270351/how-to-write-an-inline-if-statement-in-javascript
                //
                //  As well, these two statements cannot execute until the strings
                //  that they evaluate are initialized by code in the block just
                //  above this comment.
                //  ----------------------------------------------------------------

                _fCanTrackBehavior      = (    Object.is ( _strEmailPerMG , undefined )
                                             || Object.is ( _strVideoURL , undefined )
                                          )
                                          ? false
                                          : (    _strVideoURL.length > EMPTY_STRING_LENGTH
                                               && (    _strEmailPerMG.length > EMPTY_STRING_LENGTH
                                                    || _intBehaviorLeadId    > _LeadLifeJSHelpers.NUMERIC_ZERO
                                                  )
                                            );
            }   // FALSE (The input is something besides the ID of a Note.) block, if ( _strVideoURL.startsWith ( STT_NOTE_ID_PREFIX ) )

            if ( _fDebugLogging )
            {
                const _astrVideoModeTexts = [ 'UNSUPPORTED' ,
                                              'AUDIOVISUAL' ,
                                              'TRANSCRIPT'
                                            ];

                console.log ( strMethodName + ': STT_VideoPlayer.js version ' + __version + ' started at ' + _LeadLifeJSHelpers.PageLoadTime_JS_Date );

                console.log ( strMethodName + ':     SalesTalk Database Name    = ' + _LeadLifeJSHelpers.GetSTTDatabaseNameFromLocation ( ) );
                console.log ( strMethodName + ':     SalesTalk Domain Name      = ' + _LeadLifeJSHelpers.GetSTTDomainNameFromLocation ( ) );

                console.log ( strMethodName + ':     Title                      = ' + _strTitle );
                console.log ( strMethodName + ':     Video URL                  = ' + _strVideoURL );
                console.log ( strMethodName + ':     Poster URL                 = ' + _strPosterURL );
                console.log ( strMethodName + ':     Start Index                = ' + _dblStartIndex );
                console.log ( strMethodName + ':     Stop Index                 = ' + _intStopIndex );

                console.log ( strMethodName + ':     Email                      = ' + _strEmailPerMG );
                console.log ( strMethodName + ':     CampaignId                 = ' + _strWorkflowIdPerMG );
                console.log ( strMethodName + ':     Lead ID per Behavior       = ' + _intBehaviorLeadId );

                console.log ( strMethodName + ':     Player Can Track Behavior  = ' + _fCanTrackBehavior );
                console.log ( strMethodName + ':     Player operating mode      = ' + _intPlayerMode + ' (' + _astrVideoModeTexts [ _intPlayerMode ] + ')' );
                console.log ( strMethodName + ':     Boolean flag fInputIsNote  = ' + fInputIsNote );
            }   // if ( _fDebugLogging )

            const txtMessageBody1    = document.getElementById ( 'MessageBody' );
            const docSubmitButton    = document.getElementById ( 'SendMessage' );
            const docCancelButton    = document.getElementById ( 'CancelMessage' );

            txtMessageBody1.value    = _strURL4Preamble;

            docSubmitButton.addEventListener ( 'click'   , XmitMessage );
            docSubmitButton.addEventListener ( 'keydown' , XmitMessage );

            docCancelButton.addEventListener ( 'click'   , HideFormShowButton );
            docCancelButton.addEventListener ( 'keydown' , HideFormShowButton );

            $ ( '.EmailForm4AgentInputIsRequired' ).map ( function ( )
            {
                this.addEventListener ( 'blur' , XitField );
            })

            var docContainerDiv;

            switch ( _intPlayerMode )
            {
                case _MODE_IS_AUDIOVISUAL:
                    {   // Constrain the lexical scope of variable docPlayer, which is irrelevant outside the scope of this lexical block.
                        docContainerDiv                     = document.getElementById ( 'MediaPlayerContainer' );
                        docContainerDiv.classList.replace ( 'STT_HideElement' ,
                                                            'STT_ShowElement' );
                        let   strPathExtn                   = _LeadLifeJSHelpers.GetExtension ( _strVideoURL )
                        const objRecordingType              = _LeadLifeJSHelpers.QueryAssociativeArray ( strPathExtn.length > EMPTY_STRING_LENGTH ? strPathExtn : '.mp3' ,  // poKey
                                                                                                         null ,                                                             // poDefaultValue
                                                                                                         _aoRecordedMediaTypes );                                           // poArray
                        let docPlayer = null;

                        if ( objRecordingType !== null )
                        {
                            console.log ( strMethodName + ': Recording type        = ' + objRecordingType.DisplayText );
                            docPlayer                       = document.getElementById ( objRecordingType.ElementId );
                            docPlayer.classList.replace ( 'STT_HideElement' , 'STT_ShowElement' );

                            //  ----------------------------------------------------
                            //  Initialize the player and set its properties.
                            //  ----------------------------------------------------

                            //    docPlayer.poster               = _strPosterURL;

                            //  ----------------------------------------------------
                            //  Register player events, all of which wind up in the
                            //  same event handler with distinct arguments that
                            //  identify the event that invoked the registered
                            //  listener.
                            //
                            //  Source: 4.8.11.16 Events summary
                            //          https://html.spec.whatwg.org/multipage/media.html#mediaevents
                            //  ----------------------------------------------------

                            docPlayer.addEventListener ( 'play', ( ) =>
                            {
                                _STTVideoPlayer_TrackEvent ( 'Player Playing' );
                            });
                            docPlayer.addEventListener ( 'pause', ( ) =>
                            {
                                _STTVideoPlayer_TrackEvent ( 'Player Paused' );
                            });
                            docPlayer.addEventListener ( 'ended', ( ) =>
                            {
                                _fHasPlayEnded = true;
                                _STTVideoPlayer_TrackEvent ( 'Player Ended' );
                            });

                            debugger;

                            docPlayer.crossOrigin           = 'anonymous';
                            docPlayer.src                   = ComputeSrc ( _strVideoURL ,
                                                                           _dblStartIndex ,
                                                                           _intStopIndex );
                            console.log ( strMethodName + ': docPlayer.crossOrigin = ' + docPlayer.crossOrigin );
                            console.log ( strMethodName + ': docPlayer.src         = ' + docPlayer.src );

                            debugger;
                            docPlayer.load ( );
                        }   // TRUE (anticipated outcome) block, if ( objRecordingType !== null )
                        else
                        {
                            console.log ( LLCommon.LogException ( strMethodName + ': Unsupported file extension = ' + strPathExtn + '. This window must close now.' ) );
                            window.close ( );
                            return;
                        }   // FALSE (unanticipated outcome) block, if ( objRecordingType !== null )
                    }   // Variable docPlayer goes out of scope.

                    break;  // case _MODE_IS_AUDIOVISUAL

                case _MODE_IS_TRANSCRIPT:
                    debugger;
                    {   // Constrain the scopes of several lexical declarations that are meaningless outside this block.

                        //  ----------------------------------------------------
                        //  Adjust the window to cover 85% of the screen by
                        //  width and by height.
                        //  ----------------------------------------------------

                        window.resizeTo (
                          window.screen.availWidth  * 0.50,
                          window.screen.availHeight * 0.75
                        );

                        docContainerDiv                     = document.getElementById ( 'TranscriptContainer' );
                        ShowOrHideElement ( docContainerDiv ,
                                            ELEMENT_SHOW );

                        const docTextViewPort               = document.getElementById ( 'TranscriptViewPort' );
                        const strFileContents               = MakeFullStopsVisible ( fInputIsNote
                                                                                     ? LLCommon.DoAjax ( 'GetANote' ,
                                                                                                         'GET' ,
                                                                                                         {
                                                                                                            'LeadId' : _LeadLifeJSHelpers.STTLeadId ,
                                                                                                            'NoteId' : _strVideoURL.substring ( STT_NOTE_ID_PREFIX.length )
                                                                                                         } )
                                                                                     : LLCommon.DoAjax ( 'GetSalesTalkFileAtUrl' ,
                                                                                                         'GET' ,
                                                                                                         {
                                                                                                            'URL'    : _strVideoURL
                                                                                                         } ) );

                        if ( Object.is ( _LeadLifeJSHelpers.STTDomainId , undefined ) ||  Object.is ( _LeadLifeJSHelpers.STTTenantId , undefined  ) )
                        {   // The degenerate case is that we can display only the unhighlighted text.
                            docTextViewPort.innerHTML       = strFileContents;
                        }   // TRUE (Skip keyword higliighting when the domain ID or tenant ID is undefined.) block, if ( Object.is ( _LeadLifeJSHelpers.STTDomainId , undefined ) ||  Object.is ( _LeadLifeJSHelpers.STTTenantId , undefined  ) )
                        else
                        {   // The preferred outcome is that we can higlight keywords in the text.
                            oKeyWordHighLighter             = new KeyWordHighLighter ( strFileContents ,
                                                                                       LLCommon.DoAjax ( 'GetKeyWordsByBehaviorId',
                                                                                                         'GET' ,
                                                                                                         {
                                                                                                              'DomainId'   : _LeadLifeJSHelpers.STTDomainId,
                                                                                                              'TenantId'   : _LeadLifeJSHelpers.STTTenantId,
                                                                                                              'BehaviorId' : GetParameterFromURLFormOrLocalStorage ( 'id' , '0' )
                                                                                                         } ) ,
                                                                                       [
                                                                                          new KeyWordDispositionMap ( 'keyword'           , 'KeywordText'            , false ) ,
                                                                                          new KeyWordDispositionMap ( 'match'             , 'MatchedkeywordText'     , false ) ,
                                                                                          new KeyWordDispositionMap ( 'match key'         , 'MatchKeyText'           , true  ) ,
                                                                                          new KeyWordDispositionMap ( 'Words2ActionInput' , 'Words2ActionInputLabel' , false ) ,
                                                                                       ] ,
                                                                                       _LeadLifeJSHelpers );
                            docTextViewPort.innerHTML       = oKeyWordHighLighter.HighlightKeywordsInText ( );
                        }   // FALSE (We have the means to highhlight keywords.) block, if ( Object.is ( _LeadLifeJSHelpers.STTDomainId , undefined ) ||  Object.is ( _LeadLifeJSHelpers.STTTenantId , undefined  ) )

                        const docToolChest                  = document.getElementById ( 'TranscriptToolz' );

                        if ( docToolChest !== null )
                        {
                            const aobjKeywordInfo           = oKeyWordHighLighter === undefined ? [ ] : oKeyWordHighLighter.GetKeywordsReferenced ( );
                            const intKeyWordCount           = aobjKeywordInfo.length;

                            if ( intKeyWordCount > ARRAY_IS_EMPTY )
                            {   // Suppress the combo box and the VCR controls unless the document contains keywords.
                                const docToolOrganizer      = document.getElementById ( STT_VPLAYER_DOCTOOLORGANIZER ) === null ? document.createElement ( 'table' ) : document.getElementById ( STT_VPLAYER_DOCTOOLORGANIZER );

                                if ( docToolOrganizer.id.length === EMPTY_STRING_LENGTH )
                                {
                                    docToolOrganizer.id     = STT_VPLAYER_DOCTOOLORGANIZER;

                                    //  ----------------------------------------
                                    //  Declare this array such that its scope
                                    //  is extremely limited.
                                    //  ----------------------------------------

                                    const astrVCRControls   = [
                                                                { 'ClassName' : 'fa-fast-backward' , 'ToolTip' : 'Jump to the first occurrence of the selected keyword.'    } ,
                                                                { 'ClassName' : 'fa-step-backward' , 'ToolTip' : 'Move to the previous occurrence of the selected keyword.' } ,
                                                                { 'ClassName' : 'fa-step-forward'  , 'ToolTip' : 'Move to the next occurrence of the selected keyword.'     } ,
                                                                { 'ClassName' : 'fa-fast-forward'  , 'ToolTip' : 'Jump to the first occurrence of the selected keyword.'    } ,
                                                              ];

                                    docToolOrganizer.classList.add ( 'TranscriptViewerControls' );
                                    docToolChest.appendChild ( docToolOrganizer );

                                    const docRow1           = document.createElement ( 'tr' );

                                    docRow1.classList.add ( 'TranscriptViewerControls' );
                                    docRow1.classList.add ( 'STT_TranscriptToolz_TR1' );

                                    docToolOrganizer.appendChild ( docRow1 );

                                    const docRow1Col1       = document.createElement ( 'td' );
                                    docRow1Col1.classList.add ( 'TranscriptViewerControls' );
                                    docRow1.appendChild ( docRow1Col1 );

                                    const docRow1Col2       = document.createElement ( 'td' );
                                    docRow1Col2.classList.add ( 'TranscriptViewerControls' );
                                    docRow1.appendChild ( docRow1Col2 );

                                    const docLabel4KeywordList      = document.createElement ( 'label' );

                                    docLabel4KeywordList.setAttribute ( 'id'  , 'Label4KeyWordList' );
                                    docLabel4KeywordList.setAttribute ( 'for' , 'KeyWordList' );
                                    docLabel4KeywordList.innerText  = 'Keywords';

                                    docRow1Col1.appendChild ( docLabel4KeywordList );

                                    const docKeyWordList            = document.createElement ( 'select' );
                                    docKeyWordList.setAttribute ( 'id' , 'KeyWordList' );
                                    docKeyWordList.classList.add ( 'STT_KeywordPicker' );

                                    const docPleaseSelect           = document.createElement ( 'option' );

                                    docPleaseSelect.value           = EMPTY_STRING;
                                    docPleaseSelect.innerHTML       = 'Please select';

                                    docKeyWordList.appendChild ( docPleaseSelect );

                                    for ( var intCurrentWord = ARRAY_FIRST_ELEMENT;
                                              intCurrentWord < intKeyWordCount;
                                              intCurrentWord++ )
                                    {
                                        var docKeyWordOption        = document.createElement ( 'option' );

                                        docKeyWordOption.value      = aobjKeywordInfo [ intCurrentWord ].KeyWordAnchorPrefix;
                                        docKeyWordOption.innerHTML  = aobjKeywordInfo [ intCurrentWord ].RawKeyowrd;

                                        docKeyWordList.appendChild ( docKeyWordOption );
                                    }   // for ( var intCurrentWord = ARRAY_FIRST_ELEMENT; intCurrentWord < intKeyWordCount; intCurrentWord++ )

                                    debugger;

                                    docRow1Col2.appendChild ( docKeyWordList );

                                    //  ----------------------------------------
                                    //  Unless the list of FontAwesome selectors
                                    //  is empty, which should never be so, add
                                    //  one of each below the keyword selector
                                    //  control so that they align as they would
                                    //  on a tape, CD, or DVD player.
                                    //  ----------------------------------------

                                    {   // Erect a lexical block around intNControls.
                                        const intNControls          = astrVCRControls.length;

                                        if ( intNControls > ARRAY_IS_EMPTY )
                                        {   // Append the four VCR controls, each of which gets an ID, for use by its click event handler.

                                            const docRow2           = document.createElement ( 'tr' );
                                            docRow2.classList.add ( 'TranscriptViewerControls' );
                                            docRow2.classList.add ( 'STT_TranscriptToolz_TR2' );

                                            docToolOrganizer.appendChild ( docRow2 );

                                            const docRow2Span       = document.createElement ( 'td' );
                                            docRow2Span.colSpan     = 2;
                                            docRow2Span.classList.add ( 'TranscriptViewerControls' );
                                            //  docRow2Span.classList.add ( 'STT_FlushRight' );
                                            docRow2.appendChild ( docRow2Span );

                                            for ( var intCurrentControl = ARRAY_FIRST_ELEMENT;
                                                      intCurrentControl < intNControls;
                                                      intCurrentControl++ )
                                            {
                                                const strElementIdSuffix    = astrVCRControls [ intCurrentControl ].ClassName.replace ( new RegExp ( _LeadLifeJSHelpers.HYPHEN_CHAR ,
                                                                                                                                                     _LeadLifeJSHelpers.REGEXP_GLOBAL_MATCH ) ,
                                                                                                                                        UNDERSCORE_CHAR );

                                                const docContainerSpan      = document.createElement ( 'span' );

                                                docContainerSpan.classList.add ( 'VCRControls4Search' );
                                                docContainerSpan.id         = 'VCRContainer_' + strElementIdSuffix;

                                                const docVCRControl         = document.createElement ( 'i' );

                                                docVCRControl.classList.add ( 'VCRControls4Search' );
                                                docVCRControl.classList.add ( 'fa' );
                                                docVCRControl.classList.add ( astrVCRControls [ intCurrentControl ].ClassName );
                                                docVCRControl.title         = astrVCRControls [ intCurrentControl ].ToolTip;
                                                docVCRControl.id            = 'VCRButton_' + strElementIdSuffix;

                                                docVCRControl.addEventListener ( 'click'   , VCRButtonClick );
                                                docVCRControl.addEventListener ( 'keydown' , VCRButtonClick );

                                                docContainerSpan.appendChild ( docVCRControl );
                                                docRow2Span.appendChild ( docContainerSpan );
                                            }   // for ( var intCurrentControl = ARRAY_FIRST_ELEMENT; intCurrentControl < intNControls; intCurrentControl++ )

                                            //  --------------------------------
                                            //  Between the forward and backward
                                            //  controls goes a read-only view
                                            //  of the current position and the
                                            //  count of occurrences of the
                                            //  selected keyword.
                                            //  --------------------------------

                                            const docVCRPositionContainer   = document.getElementById ( 'VCRContainer_fa_step_backward' );

                                            const docPosView                = document.createElement ( 'span' );
                                            docPosView.classList.add ( 'VCRControls4Search' );
                                            docPosView.classList.add ( 'VCRControlsPosition' );
                                            docPosView.id                   = 'VCRContainer_CurrentPosition';
                                            docPosView.innerHTML            = 1;
                                            docVCRPositionContainer.appendChild ( docPosView );

                                            const docCountLabel             = document.createElement ( 'span' );
                                            docCountLabel.classList.add ( 'VCRControls4Search' );
                                            docCountLabel.id                = 'VCRContainer_CountLabel';
                                            docCountLabel.innerText         = ' of ';
                                            docVCRPositionContainer.appendChild ( docCountLabel );

                                            const docCountView              = document.createElement ( 'span' );
                                            docCountLabel.classList.add ( 'VCRControls4Search' );
                                            docCountLabel.classList.add ( 'VCRControlsPosition' );
                                            docCountView.id                 = 'VCRContainer_KeywordOccurrencedCount';
                                            docCountView.innerHTML          = intKeyWordCount;
                                            docVCRPositionContainer.appendChild ( docCountView );

                                            //  --------------------------------
                                            //  Since there is but one change
                                            //  event, its listener is coded as
                                            //  an inline arrow function, giving
                                            //  it easy access to the adjacent
                                            //  controls.
                                            //  --------------------------------

                                            docKeyWordList.addEventListener ( 'change', ( poEventTarget ) =>
                                            {
                                                debugger;
                                                const strFunctionName       = GetNameOfCurrentFunction ( );

                                                const strMessage            = 'Change event arose for ' + poEventTarget.currentTarget.id + ', setting value = ' + poEventTarget.currentTarget.value;

                                                LLCommon.Trace ( strFunctionName + ': ' + strMessage );
                                                _STTVideoPlayer_TrackEvent ( strMessage );

                                                const objKeyWordInfo        = aobjKeywordInfo.find ( element => element.KeyWordAnchorPrefix === poEventTarget.currentTarget.value );
                                                docCountView.innerText      = objKeyWordInfo.ReferenceCount;
                                                docPosView.innerText        = _LeadLifeJSHelpers.NUMERIC_PLUS_ONE;

                                                VCRMoveCarat ( 'KeyWordList' ,                      // pstrButtonId
                                                               poEventTarget.currentTarget.value ,  // pstrKeyWordAnchorPrefix
                                                               docPosView.innerText ,               // pstrCurrentPosition
                                                               docCountView.innerText )             // pstrKeyWordCount
                                            });
                                        }   // TRUE (anticipated outcome) block, if ( intNControls > ARRAY_IS_EMPTY )
                                        else
                                        {
                                            LLCommon.LogException ( strMethodName + ': Internal error in audio/video/transcript player: The list of FontAwesome VCR icons is empty.' );
                                        }   // FALSE (unanticipated outcome) block, if ( intNControls > ARRAY_IS_EMPTY )
                                    }   // Ending the lexical block causes intNControls to go out of scope and be garbage collected.
                                }   // if ( docToolOrganizer.id.length === EMPTY_STRING_LENGTH )
                            }   // TRUE (Generate the keyword list combo box and the VCR controls for navigating.) block, if ( intKeyWordCount > ARRAY_IS_EMPTY )

                            //  ------------------------------------------------
                            //  Append playback controls if a pointer to the
                            //  recording exists.
                            //  ------------------------------------------------

                            const docSrcRecContainer = document.getElementById ( 'TranscriptSourceRec' );

                            if ( LLCommon.IsString ( pAudioPlaybackUri ) && pAudioPlaybackUri.length > EMPTY_STRING_LENGTH )
                            {
                                //  --------------------------------------------
                                //  When a previous pass through function
                                //  AppendPlaybackTools created new objects,
                                //  the routine returns undefined, causing a
                                //  TypeError exception. Since the objects were
                                //  appended by that first pass, the exception
                                //  is harmless. However, since an Exception
                                //  unwinds the stack, testing the return value
                                //  is computationally cheaper. Nevertheless,
                                //  the try/catch block may as well stay.
                                //  --------------------------------------------

                                try
                                {
                                    const docNewStuff   = AppendPlaybackTools ( docSrcRecContainer , pAudioPlaybackUri );

                                    if ( docNewStuff !== undefined )
                                    {
                                        docSrcRecContainer.appendChild ( docNewStuff );

                                        const docTranscriptTools = document.getElementById ( STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID )

                                        if ( docTranscriptTools !== null )
                                        {
                                            const docSummarizeButton    = LLCommon.CreateChatGPTXscripSummarytButton ( 'SummarizeTranscriptButton' ,            // pstrButtonID
                                                                                                                       'Summarize' ,                            // pstrButtonFaceText
                                                                                                                       poEventArg ,                             // pstrContainerId
                                                                                                                       'TranscriptReview_BlueTheme' );          // pstrButtonStyleName
                                            const docAppendButton       = LLCommon.CreateChatGPTXscripSummarytButton ( 'AppendTranscriptButton' ,               // pstrButtonID
                                                                                                                       'Summarize & Append' ,                   // pstrButtonFaceText
                                                                                                                       poEventArg ,                             // pstrContainerId
                                                                                                                       'TranscriptReview_BlueTheme' ,           // pstrButtonStyleName
                                                                                                                       true );                                  // pfAppend2Transcript
                                            const docCopy2CbButton      = LLCommon.CreateCopy2ClipboardButton        ( 'CopyTranscriptButton' ,                 // pstrButtonID
                                                                                                                       'Copy to Clipboard' ,                    // pstrButtonFaceText
                                                                                                                       'TranscriptViewPort' ,                   // pstrContainerId
                                                                                                                       'TranscriptReview_BlueTheme' );          // pstrButtonStyleName

                                            if ( docSummarizeButton !== null && docAppendButton !== null && docCopy2CbButton !== null )
                                            {
                                                const docSummarizeControlsRow   = document.createElement ( 'tr' );
                                                const docSummaryControlCell     = document.createElement ( 'td' );

                                                docSummaryControlCell.colSpan   = 2;
                                                docSummaryControlCell.appendChild ( docSummarizeButton );
                                                docSummaryControlCell.appendChild ( LLCommon.CreateNbspSpacer ( 3 ) );
                                                docSummaryControlCell.appendChild ( docAppendButton );
                                                docSummaryControlCell.appendChild ( LLCommon.CreateNbspSpacer ( 3 ) );
                                                docSummaryControlCell.appendChild ( docCopy2CbButton );

                                                docSummaryControlCell.style     = STT_SUMMARIZE_BUTTON_BOX_PADDING;

                                                docSummarizeControlsRow.appendChild ( docSummaryControlCell );

                                                docTranscriptTools.appendChild ( docSummarizeControlsRow );
                                            }   // TRUE (anticipated outcome) block, if ( docSummarizeButton !== null && docAppendButton !== null && docCopy2CbButton !== null )
                                            else
                                            {
                                                throw new Error ( strMethodName + ': Internal function LLCommon.CreateChatGPTXscripSummarytButton was unable to create the ChatGPT Summarize button for pstrButtonID = SummarizeTranscriptButton, pstrButtonFaceText = Summarize, and pstrButtonStyleName = TranscriptReview_BlueTheme.' );
                                            }   // FALSE (unanticipated outcome) block, if ( docSummarizeButton !== null )
                                        }   // TRUE (anticipated outcome) block, if ( docSummarizeButton !== null && docAppendButton !== null && docCopy2CbButton !== null )
                                        else
                                        {
                                            throw new Error ( strMethodName + ': Required document element "' + STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID + '" is missing.'  );
                                        }   // FALSE (unanticipated outcome) block, if ( docTranscriptTools !== null )
                                    }   // if ( docNewStuff !== undefined )
                                }
                                catch ( e )
                                {
                                    if ( e.stack.startsWith ( 'TypeError:' ) )
                                    {
                                        console.info ( 'The appendChild failure is harmless because nothing is missing in the first place. The important thing is that the relevant properties got updated.' );
                                    }   // TRUE (anticipated outcome when the recording playback player and download button already exist.) block, if ( e.stack.startsWith ( 'TypeError:' ) )
                                    else
                                    {
                                        LLCommon.LogException ( e );
                                    }   // FALSE (unanticipated outcome when another type of eexception arises) block, if ( e.stack.startsWith ( 'TypeError:' ) )
                                }   // A longish catch block ends here.
                            }   // TRUE (A valid pointer to the recording from which the transcript was made is PRESENT.) block, if ( LLCommon.IsString ( pAudioPlaybackUri ) && pAudioPlaybackUri.length > EMPTY_STRING_LENGTH )
                            else
                            {
                                let docXscriptControlToolsOuterBox          = document.getElementById ( STT_VIDEOPLAYER_TRANSCIPT_TOOLS_CONTAINER_ID );
                                let docXscriptControlToolsContainer;

                                if ( docXscriptControlToolsOuterBox === null )
                                {
                                    docXscriptControlToolsOuterBox          = document.createElement ( 'table' );
                                    docXscriptControlToolsOuterBox.id       = STT_VIDEOPLAYER_TRANSCIPT_TOOLS_CONTAINER_ID;
                                    docSrcRecContainer.appendChild ( docXscriptControlToolsOuterBox  );
                                }   // TRUE (The table doesn't yet exist.) block, if ( docXscriptControlToolsOuterBox === null )
                                else
                                {
                                    docXscriptControlToolsContainer         = document.getElementById ( STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID );

                                    if ( docXscriptControlToolsContainer !== null )
                                    {   // Remove the element and its children, then destroy the reference.
                                        docXscriptControlToolsContainer.remove ( );
                                        docXscriptControlToolsContainer     = null;
                                    }   // if ( docXscriptControlToolsContainer !== null )
                                }   // FALSE (The table exists, and its body must be rebuilt with different contents.) block, if ( docXscriptControlToolsOuterBox === null )

                                //  ----------------------------------------
                                //  The table should exist and be empty.
                                //  ----------------------------------------

                                docXscriptControlToolsContainer             = document.createElement ( 'tbody' );
                                docXscriptControlToolsContainer.id          = STT_VIDEOPLAYER_TRANSCRIPTCONTROLTOOLS_ID;

                                let docXtraButtonRow                        = document.createElement ( 'tr' );
                                let docXtraButton1                          = document.createElement ( 'td' );

                                docXtraButton1.appendChild ( LLCommon.CreateChatGPTXscripSummarytButton ( 'SummarizeTranscriptButton' ,                         // pstrButtonID
                                                                                                          'Summarize' ,                                         // pstrButtonFaceText
                                                                                                          docContainerDiv.id ,                                  // pstrContainerId, which ALWAYS exists in this context
                                                                                                          'TranscriptReview_BlueTheme' ) );                     // pstrButtonStyleName
                                docXtraButton1.appendChild ( LLCommon.CreateNbspSpacer ( 3 ) );
                                docXtraButton1.appendChild ( LLCommon.CreateChatGPTXscripSummarytButton ( 'AppendTranscriptButton' ,                            // pstrButtonID
                                                                                                          'Summarize & Append' ,                                // pstrButtonFaceText
                                                                                                          poEventArg ,                                          // pstrContainerId
                                                                                                          'TranscriptReview_BlueTheme' ,                        // pstrButtonStyleName
                                                                                                          true ) );                                             // pfAppend2Transcript
                                docXtraButton1.appendChild ( LLCommon.CreateNbspSpacer ( 3 ) );
                                docXtraButton1.appendChild ( LLCommon.CreateCopy2ClipboardButton        ( 'CopyTranscriptButton' ,                              // pstrButtonID
                                                                                                          'Copy to Clipboard' ,                                 // pstrButtonFaceText
                                                                                                          'TranscriptViewPort' ,                                // pstrContainerId
                                                                                                          'TranscriptReview_BlueTheme' ) );                     // pstrButtonStyleName
                                docXtraButton1.style                        = STT_SUMMARIZE_BUTTON_BOX_PADDING;

                                docXtraButtonRow.appendChild ( docXtraButton1 );
                                docXscriptControlToolsContainer.appendChild ( docXtraButtonRow );
                                docXscriptControlToolsOuterBox.appendChild ( docXscriptControlToolsContainer );
                            }   // FALSE (A pointer to the recording from which the transcript was made is ABSENT or INVALID.) block, if ( LLCommon.IsString ( pAudioPlaybackUri ) && pAudioPlaybackUri.length > EMPTY_STRING_LENGTH )
                        }   // TRUE (anticipated outcome) block, if ( docToolChest !== null )
                        else
                        {
                            LLCommon.LogException ( strMethodName + ': Internal error in audio/video/transcript player: The list of FontAwesome VCR icons is empty.' );
                        }   // FALSE (unanticipated outcome) block, if ( docToolChest !== null )

                        const strInitialJumpElementId = oKeyWordHighLighter.GetInitialJumpElementID ( );

                        //  ----------------------------------------------------
                        //  Though the examples returned at the top of the
                        //  result set returned by a query about the difference
                        //  between innerText and innerHTML are vague, my
                        //  observations make clear that text that contains HTML
                        //  markup MUST be assigned to innerHTML. Otherwise, the
                        //  markup is escaped and rendered impotent.
                        //  ----------------------------------------------------

                        if ( strInitialJumpElementId.length > EMPTY_STRING_LENGTH )
                        {
                            _LeadLifeJSHelpers.JumpToElementById ( strInitialJumpElementId );
                        }   // TRUE (anticipated outcome) block, if ( strInitialJumpElementId.length > EMPTY_STRING_LENGTH )
                        else
                        {   // Downgrade this from a runtime error to a note in the console log.
                            console.info ( 'CAUTION: IN audio/video/transcript player, no element ID was identified as a jump point.' );
                        }   // FALSE (unanticipated outcome) block, if ( strInitialJumpElementId.length > EMPTY_STRING_LENGTH )
                    }   // End lexical scope of _MODE_IS_TRANSCRIPT case block.

                    break;  // case _MODE_IS_TRANSCRIPT

                default:
                    {   // Establish the final switch block as a lexical scope.
                        const strErrorMessage         = 'Internal error in audio/video/transcript player: Mode is ' + _intPlayerMode + ', an UNSUPPORTED value.'
                        LLCommon.LogException ( strErrorMessage );
                        alert (   strErrorMessage
                                + _LeadLifeJSHelpers.CARRIAGE_RETURN_CHAR
                                + _LeadLifeJSHelpers.LINE_FEED_CHAR
                                + 'Please contact SalesTalk customer support for assistance.' , 'native' );
                    }   // End the lexical scope of the final switch block.
            }   // switch ( _intPlayerMode )
        }   // if ( _strVideoURL.length > EMPTY_STRING_LENGTH )
    }
    catch ( ex )
    {
        LLCommon.Trace ( strMethodName + ': STT_VideoPlayer initializer function Exception caught: Message = ' + ex.message + ', Stack Trace = ' + ex.stack );
        LLCommon.LogException ( ex );
    }
}   // function STTProcessMedia


console.log ( ScriptInfoForLog ( STTVideoPlayer_SCRIPTSOURCE ,
                                 STTVideoPlayer_VERSION ,
                                 STTVideoPlayer_LastUpdated ,
                                 'loaded' ) );
