// ==============================================
// CallMapGrid.js (CallMapGrid_DEV.js in STAGING)
// ==============================================

//  ----------------------------------------------------------------------------
//  The following four constants resolve to values that leave a record in the
//  console log indicating the name, version, and last modified date of every
//  script file that loads into the Web browser context.
//  ----------------------------------------------------------------------------

const CallMapGrid_SCRIPTSOURCE = document.currentScript === null ? 'unknown' : document.currentScript.getAttribute ( 'src' );
const CallMapGrid_VERSION      = 1.004;
const CallMapGrid_LogTraces    = false;
const CallMapGrid_LastUpdated  = '2026/03/30 21:57:12 CDT'
console.log ( ScriptInfoForLog ( CallMapGrid_SCRIPTSOURCE ,
                                 CallMapGrid_VERSION ,
                                 CallMapGrid_LastUpdated ,
                                 'loading' ) );

// Main function to show Story So Far popup

function StorySoFar() {
     var gridData = [];
     var count = 0;
     console.log("api: ",'https://salestalktech.com/InsuranceDB/Open/StorySoFar?DomainId='+_domainid+'&TenantId=1000&leadId='+_leadid+'&userId=1049&src=test&TZOffset=' + (new Date()).getTimezoneOffset() +'&take=30&page=2&color=blue');

     // Fetch Data
     $.ajax({
         url: _llAppPath + 'Open/StorySoFar?DomainId='+_domainid+'&TenantId='+_tenantid+'&leadId='+_leadid+'&userId='+_userid+'&src=test&TZOffset=' + (new Date()).getTimezoneOffset() +'&take=30&page=2&color=blue',
         type: "GET",
         async: false,
         success: function (data) {
             if (data && data.data) {
                 count = data.data.length;
                 gridData = data.data;
             }
         }
     });

     // Destroy old popup if exists
     if ($("#storySoFarPopup").data("kendoWindow")) {
         $("#storySoFarPopup").data("kendoWindow").destroy();
         $("#storySoFarPopup").remove();
     }

     // Append popup container dynamically
     $("body").append('<div id="storySoFarPopup"><div id="storySoFarGrid" class="w2ASSF"></div></div>');

     // Initialize Kendo Window
     var popup = $("#storySoFarPopup").kendoWindow({
         width: "80%",
         //height: "650px",
         modal: true,
         title: "Story-So-Far",
visible: false,
         //position: {
             //top: 20,   // distance from top of screen
             //left: ($(window).width() - 100%) / 2   // center horizontally
        // },
         actions: ["Close"],
         close: function () {
             this.destroy();  // cleanup
         }
     }).data("kendoWindow");

    var wnd = $("#storySoFarPopup").data("kendoWindow");
    wnd.setOptions({
        position: {
            top: 20,
            left: ($(window).width() - wnd.wrapper.width()) / 2
        }
    });

     popup.open();

     // Initialize Grid inside popup
     BindKendoGridForSSF(gridData);
 }

 function BindKendoGridForSSF(gridData) {

    $("#storySoFarGrid").kendoGrid({

        dataSource: {
            data: gridData,
            pageSize: 25,

            schema: {
                model: {
                    fields: {
                        date: { type: "date" },
                        title: { type: "string" },
                        displayas: { type: "string" },
                        duration: {
                            type: "number",
                            // parse: function (v) {
                                // return v == null ? null : Number(v);
                            // }
						},
                        begins: {
                            type: "number",
                            parse: function (v) {
                                return v == null ? null : Number(v);
                            }
                        }
                    }
                }
            }
        },

        toolbar: ["excel"],

        excel: {
            fileName: "SSF_Grid.xlsx"
        },

        height: 650,

        pageable: {
            refresh: true
        },

        sortable: true,
		resizable: true,
        filterable: {
            mode: "menu",
            operators: {
                string: {
                    //startswith: "Starts with",
                    //eq: "Is equal to",
                    //neq: "Is not equal to",
                    contains: "Contains",
                    //endswith: "Ends with"
                },
                number: {
                    lt: "Less than",
                    gt: "Greater than",
                    eq: "Equal to"
                },
                date: {
                    lt: "Is Before",
                    gt: "Is After"
                }
            }
        },
		// filter: function (e) {
            // if (e.filter && e.filter.filters) {
				// console.log("filter:::", e.filter)
				// normalizeFilters(e.filter.filters);

				// //  FIX UI AFTER KENDO TOUCHES IT
				// setTimeout(function () {
					// restoreDurationFilterUI();
				// }, 0);
            // }

        // },

        columns: [

            /* ================= DATE ================= */
            {
                field: "date",
                title: "Date",
                width: "120px",
                filterable: {
                    operators: {
                        date: {
                            lt: "Is Before",
                            gt: "Is After"
                        }
                    }
                },
                format: "{0:MM/dd/yyyy hh:mm tt}"
            },

            /* ================= DURATION ================= */
            {
                field: "duration",
                title: "Duration",
                width: "70px",
				filterable: {
					ui: function (element) {
						element.kendoNumericTextBox({
							format: "0",
							decimals: 0,
							spinners: true
						});
					}
				},
                // filterable: {
                    // ui: function (element) {
                        // createDurationFilterPicker(element);
                    // },
                    // operators: {
                        // number: {
                            // lt: "Less than",
                            // gt: "Greater than",
                            // eq: "Equal to"
                        // }
                    // }
                // },

                template: function (data) {
                    // var duration = getDuration(data.duration);
                    var duration = data.duration;
                    return '<span title="time in minutes:seconds (max 999) of activity">' +
                        duration + '</span>';
                }
            },

            /* ================= BEGINS ================= */
            {
                field: "begins",
                title: "Begins",
                width: "70px",
                filterable: {
					ui: function (element) {
						element.kendoNumericTextBox({
							format: "0",
							decimals: 0,
							spinners: true
						});
					}
				},
                template: function (data) {
                    // var begins = getDuration(data.begins);
                    var begins = data.begins;
                    var safeData = encodeURIComponent(JSON.stringify(data));
                    return '<span onclick="storyItemClick2(\'' + safeData + '\')">' +
                        begins + '</span>';
                }
            },

            /* ================= TYPE ================= */
            {
                field: "type",
                title: "Type",
                width: "60px",
                filterable: false,
                template: function (data) {
                    if (data !== undefined) {
                        try {
                            var clicker = "";
                            var icontitle = getIcon(data.type, data.name, data.content, null, data.keyword);
                            var highcolor = (data.calloutcome4 !== undefined && data.calloutcome4 !== '0') ? 'black' : 'white';
                            var hightitle = (data.calloutcome4 !== undefined && data.calloutcome4 !== '0')
                                ? 'This box indicates that the Marked Important flag (Highlight Talking Point) has been set.'
                                : '';

                            return '<i style="color:' + data.iconcolor +
                                '" title="' + icontitle +
                                '" class="' + icontitle +
                                '" onClick="' + clicker + '"></i>';
                        } catch (ex) {
                            console.error(ex);
                        }
                    }
                    return '';
                }
            },

            /* ================= DESCRIPTION ================= */
  {
        field: "title",
        title: "Description",
        width: "400px",
        filterable: {
            cell: { operator: "contains", showOperators: true }
        },
        headerTemplate: '<span title="The Description filter may be used to find videos - use Contains and Video.">Description</span>',
        template: function (data) {
            if (data != undefined) {
                var content = '';
                if ((data.type === 'Conversation') && (data.name.indexOf("#") != 0) &&
                    ((data.contenttitle2 != undefined && data.contenttitle2 == 'notes') ||
                        (data.keyword.indexOf('IsClickNote audio ') >= 0 ||
                            (data.keyword.indexOf('IsClickNote video ') >= 0) ||
                            (data.name.indexOf('Click to view transcription') >= 0) ||
                            (data.name.indexOf('Uploaded file - ') >= 0)))) {
                    content = ((data.keyword == undefined) || (data.keyword.indexOf('\x7f') < 0)) ? '' : data.keyword.replace(/.*\x7f */, '').trim();
                    if (content !== '') {
                        content = ("<b style='font-weight: bolder'> => " + content + "</b>");
                    }
                }
                if (data.type !== "Conversation") {
                    var TypeDisplay = getTypeDisplay(data);
                    var Content = getContent(data);
                    var Color = getColor(data.name);
                    var contenttitle = ((data.contenttitle == undefined) || (data.contenttitle.indexOf("Call to ") == 0)) ? "" : data.contenttitle;
                    var contenttitle2 = ((data.contenttitle2 == undefined) || (data.contenttitle2.indexOf("Call to ") == 0)) ? "" : data.contenttitle2;
                    return ' <span title="' + data.displayas + '">' + TypeDisplay + '</span> ' +
                        '<span title="' + data.displayas + '" style="color:' + data.color + '; max-width: 100% !important">' +
                        data.displayas + content + '</span>';
                } else if (data.displayas.indexOf('CALL - To: ') == 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else if (data.displayas.indexOf('>Uploaded file - Telephone call recording<') > 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else if (data.displayas.indexOf('>Uploaded file - Zoom Meeting Recording<') > 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else if (data.displayas.toLowerCase().indexOf('>uploaded file - recording<') > 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else if (data.displayas.toLowerCase().indexOf('>uploaded file - audio recording<') > 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else if (data.displayas.toLowerCase().indexOf('>uploaded file - video recording<') > 0) {
                    return '<span style="color:' + data.color + '; max-width: 100% !important">' + data.displayas + content + '</span>';
                } else {
                    if (data.displayas === "notes" && data.content != null) {
                        data.displayas += stripHtml(data.content);
                    }
                    return '<span title="' + data.displayas + '">' + getTypeDisplay(data) +
                        '</span><span title="' + data.displayas + '" style="color:' + data.color +
                        '; max-width: 100% !important">' + data.displayas + content + '</span>';
                }
            }
            return '';
        }
    }
        ]
    });

    $("#storySoFarPopup")
        .find(".k-grid-toolbar")
        .insertBefore($("#storySoFarPopup .k-pager-first"));
}


// ================================
// Utility + Support functions
// ================================
 function deleteRow(id) {
        kendo.confirm("Are you sure you want to delete this record?")
            .then(function () {
                fetch(window._llAppPath + 'Sales/DeleteManaualTime?behaviorId=' + id, {
                    method: 'GET'
                })
                    .then(response => response.json())
                    .then(data => {
                        console.log("Response:", data);

                        //  Remove the row from grid instantly
                        var grid = $("#storySoFarGrid").data("kendoGrid");
                        var row = grid.dataSource.get(id);
                        if (row) {
                            grid.dataSource.remove(row);
                        }
                    })
                    .catch(error => {
                        console.error("Error deleting row:", error);
                    });
            })
            .catch(function () {
                console.log("Delete canceled");
            });
    }

 function stripHtml(v) {
     v = String(v);
     var regexp = /<("[^"]*"|'[^']*'|[^'">])*>/gi;
     if (v) {
         v = v.replace(regexp, "");
         return (v && v !== '&nbsp;' && v !== '&#160;') ? v.replace(/\"/g, "'") : "";
     }
     return v;
 }

    function deleteTP(tp) {
        tp.deleted = true;
        $http({
            method: 'GET',
            url: window._llAppPath + 'Sales/DeleteManaualTime?behaviorId=' + tp.id
        }).then(function (response) {
           // setStorySoFar();
        })
    }

function mmssToSeconds(value) {
    if (value == null || value === "") return null;

    // Case 1 → TimePicker gives Date object
    if (value instanceof Date) {
		console.log("case 1: ", value);
        return (value.getMinutes() * 60) + value.getSeconds();
    }

    value = value.toString().trim();

    // Case 2 → only number → treat as minutes
    if (/^\d+$/.test(value)) {
		console.log("case 2: ", value);
        return parseInt(value, 10) * 60;
    }

    // Case 3 → mm:ss
    if (/^\d{1,3}:\d{1,2}$/.test(value)) {
		console.log("case 3: ", value);
        var parts = value.split(":");
        var minutes = parseInt(parts[0], 10) || 0;
        var seconds = parseInt(parts[1], 10) || 0;

		console.log("case 3 parts: ", parts);
		console.log("case 3 minutes: ", minutes);
		console.log("case 3 seconds: ", seconds);

        if (seconds > 59) seconds = 59;

        return (minutes * 60) + seconds;
    }

    // Invalid input → ignore filter
    return null;
}


function normalizeFilters(filters) {
    filters.forEach(function (f) {

        if (f.filters) {
            normalizeFilters(f.filters);
            return;
        }

		console.log("f.value:", f.value);
		console.log("typeof f.value:", typeof f.value);

        if (f.field === "duration" || f.field === "begins") {

            var seconds = null;

            // Case 1 → TimePicker stored value
            if (f.value && typeof f.value === "object") {
				console.log("case 1");
                seconds =
                    (f.value.getMinutes() * 60) +
                    f.value.getSeconds();
            }

            // Case 2 → manual typing like "1" or "1:30"
            else if (typeof f.value === "string") {
				console.log("case 2:", f.value);

                seconds = mmssToSeconds(f.value);
            }

			console.log("seconds::", seconds);

            if (seconds != null) {
                f.value = seconds;
            }
        }
    });
}


function pad2(n) {
    return n < 10 ? "0" + n : "" + n;
}

function createDurationFilterPicker(element) {

    // 🔹 Convert existing seconds → mm:ss for UI when reopening filter
    var existingVal = element.val();
    var sec = mmssToSeconds(existingVal);

    if (sec != null && !isNaN(sec)) {
        var mm = Math.floor(sec / 60);
        var ss = sec % 60;
        element.val(pad2(mm) + ":" + pad2(ss));
    }

    // Make the filter input read-only so users use picker
    element.attr("readonly", true);

    var $popup = $("<div class='kendo-duration-popup' />").appendTo("body").hide();

    var minutes = [];
    var seconds = [];

    for (var i = 0; i <= 60; i++) {
        minutes.push({ text: pad2(i), value: i });
        seconds.push({ text: pad2(i), value: i });
    }

    $popup.html(`
        <div style="padding:10px; display:flex; gap:8px; align-items:center;">
            <input class="dur-min" style="width:70px;" />
            <span>:</span>
            <input class="dur-sec" style="width:70px;" />
            <button class="k-button k-primary dur-ok">OK</button>
        </div>
    `);

    var minDd = $popup.find(".dur-min").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: minutes,
        optionLabel: "MM"
    }).data("kendoDropDownList");

    var secDd = $popup.find(".dur-sec").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: seconds,
        optionLabel: "SS"
    }).data("kendoDropDownList");

    var popup = $popup.kendoPopup({
        anchor: element,
        origin: "bottom left",
        position: "top left"
    }).data("kendoPopup");

    element.on("focus click", function () {
		var parts = element.val().split(":");
			if (parts.length === 2) {
				minDd.value(parseInt(parts[0], 10));
				secDd.value(parseInt(parts[1], 10));
			}

        popup.open();
    });

    $popup.find(".dur-ok").on("click", function () {
        var mm = minDd.value();
        var ss = secDd.value();

        if (mm === "" || ss === "") {
            alert("Select both minutes and seconds");
            return;
        }

        // Keep UI as mm:ss
        var displayVal = pad2(mm) + ":" + pad2(ss);
        element.val(displayVal);

        // Trigger Kendo filter
        element.trigger("change");

        popup.close();
    });
}

function restoreDurationFilterUI() {
	console.log("restoreDurationFilterUI called");
    var input = $('.k-filter-menu-container input[title="Value"]');

	console.log("input", input, input.length);

    if (!input.length) return;

    var val = input.val();
	console.log("val:::", val)
    // if value is numeric seconds → convert to mm:ss
    if (val && !isNaN(val)) {
        var sec = parseInt(val, 10);
        var mm = Math.floor(sec / 60);
        var ss = sec % 60;

        input.val(pad2(mm) + ":" + pad2(ss));
    }
}


// $(document).on("change", ".k-filter-menu-container select", function () {
    // console.log("Operator changed");
	// restoreDurationFilterUI();
// });


    function getColor (name) {
        var color = '';
        if ((name.indexOf("Start Call") === 0) || (name.indexOf("End Call") === 0)) {
            color = "red; font-weight:bold";
        }
        return color;
    };

    function getIcon(type, name, content, behavior, keyword) {
        var icon = "fa fa-circle";
        if (behavior !== undefined && behavior !== null) {
            icon = (behavior.keyword !== undefined && behavior.keyword !== null && (behavior.keyword.indexOf('CallOutcome4') >= 0)) ? "fa fa-check-square-o" : "fa";
        } else {
            switch (type) {
                case "Click":
                    icon = "fa fa-link";
                    break;
                case "Open":
                case "Deliver":
                case "Email":
                case "CC":
                case "BCC":
                    icon = "fa fa-envelope";
                    break;
                case "Activities":
                case "CallPrep":
                case "Notes":
                    icon = "fa fa-paperclip";
                    break;
                case "FormFill":
                    icon = "fa fa-list-alt";
                    break;
                case "Conversation":
                    icon = "fa fa-comment-o";
                    if (name !== undefined && (name.indexOf("Start Call") === 0 || name.indexOf("End Call") === 0)) {
                        icon = "fa fa-phone";
                        break;
                    }
                    if (name !== undefined && name == 'notes') {
                        icon = "fa fa-paperclip";
                        if (content !== undefined && content.indexOf("-Follow Up-") > 0) {
                            icon = "fa fa-bullhorn";
                        }
                        break;
                    }
                    if (name !== undefined && (name.indexOf(">Uploaded file -") > 0 || name.indexOf(">Personalization for ") > 0 || name.indexOf(">Transcription of") > 0)) {
                        icon = "fa fa-arrow-up";
                        break;
                    }
                    if (keyword !== undefined && (keyword.indexOf("IsClickNote audio") >= 0)) {
                        icon = "fa fa-microphone";
                        break;
                    }
                    if (keyword !== undefined && (keyword.indexOf("IsClickNote video") >= 0)) {
                        icon = "fa fa-video-camera";
                        break;
                    }
                    break;
                case "PageView":
                    icon = "fa fa-file";
                    break;
                case "SpamComplaint":
                case "Unsubscribe":
                    icon = "fa fa-frown-o";
                    break;
                case "HardBounce":
                case "SoftBounce":
                    icon = "fa fa-cloud";
                    break;
                case "Created":
                    icon = "fa fa-download";
                    break;
                case "Activity":
                    icon = "fa fa-calendar";
                    if (name.endsWith('Phone Call')) {
                        icon = "fa fa-phone";
                    }
                    if (name.endsWith('Task')) {
                        icon = "fa fa-book";
                    }
                    break;
            }
        }
        return icon;
    };

    function getDuration (duration) {
        var dur = " 000:00";
        if (!(duration === null || duration === '' || duration === undefined) || (duration === "0") || (duration === "000000")) {
            var sec_num = parseInt(duration, 10);
            var minutes = Math.floor(sec_num / 60);
            var seconds = sec_num % 60;
            if (minutes > 999) {
                minutes = 999;
                seconds = 99;
            }
            if (minutes < 10) {
                minutes = "00" + minutes;
            } else {
                if (minutes < 100) {
                    minutes = "0" + minutes;
                }
            }
            if (seconds < 10) {
                seconds = "0" + seconds;
            }
            dur = ('  ' + minutes + ':' + seconds);
        }
        return (dur);
    };
    function getContent (behavior) {
        var content = "";
        switch (behavior.type) {
            case "Conversation":
                if (!behavior.manualEntry) {
                    content = behavior.name + behavior.content;
                    if (content.indexOf("<") == 0) {
                        content = "";
                    }
                }
                break;
        }
        return content;//setSafe(content);
    };
    function setSafe(x) {
        return trustAsHtml(x);
    };
    function getTypeDisplay (behavior) {

        if (behavior.type === 'Conversation') {
            return '';
        } else if (behavior.type === 'Activity') {
            return behavior.name;
        } else {
            return behavior.type;
        }
    };

    function onRowSelected(e) {

        var gview = $("#SSFGrid").data("kendoGrid");
        //Getting selected item
        var selectedItem = gview.dataItem(gview.select());
        var colValue = selectedItem["<columnName>"];
    }
    function storyItemClick2 (behavior) {
        console.log("storyItemClick2");
        debugger;

        try {
            const begins = parseInt(behavior.begins);
            const PlayerLeadId = _leadid;
            const PlayerLinkURL = localStorage.getItem('PlayerLinkURL') ? localStorage.getItem('PlayerLinkURL') : EMPTY_STRING;
            const winName = localStorage.getItem('PlayerWindowName') ? localStorage.getItem('PlayerWindowName') : EMPTY_STRING;
            const winFeatures = localStorage.getItem('PlayerWindowFeatures') ? localStorage.getItem('PlayerWindowFeatures') : EMPTY_STRING;
            const PlayerURL = location.origin + _llAppPath
                + 'COMMON/STT_VideoPlayer.HTML'
                + '?m4vurl=' + PlayerLinkURL
                + '&start=' + begins
                + '&leadid=' + _leadid
                + '&id=' + behavior.id;

            console.log('location.origin       = ' + location.origin);
            console.log('_llAppPath            = ' + _llAppPath);

            console.log('Begins Offset         = ' + begins);

            console.log('_leadid               = ' + _leadid);
            console.log('PlayerLeadId          = ' + _leadid);

            console.log('PlayerLinkURL         = ' + PlayerLinkURL);

            console.log('PlayerURL             = ' + PlayerURL);
            console.log('PlayerWindowName      = ' + winName);
            console.log('PlayerWindowFeatures  = ' + winFeatures);

            if (PlayerURL.length > EMPTY_STRING_LENGTH && _leadid > NUMERIC_ZERO) {
                console.log('window.open ' + PlayerURL);
                window.open(PlayerURL,
                    winName,
                    winFeatures);
            }   // if ( PlayerURL.length > EMPTY_STRING_LENGTH && _leadid > NUMERIC_ZERO )
        } catch (err) {
            console.error('storyItemClick2 error ' + err.stack);
        }
    };

   function storyItemClick (behavior) {
        console.log("behavior:", behavior)
        console.log("behavior type:", behavior.type)
        if (behavior.type === "Deliver" || behavior.type === "Open") {
            $("#dialog").dialog({ minWidth: 700, minHeight: 500 });
            previewEmailById(behavior.value, $('#storySoFarEmailContentIFrame'));
        } else if (behavior.type === "Click") {
            if (behavior.value !== null && behavior.value.indexOf('http') > -1) {
                window.open(behavior.value);
            }
        } else if (behavior.type === "FormFill") {
            if (behavior.value !== null && behavior.value.indexOf('http') > -1) {
                window.open(behavior.value);
            }
        } else if (behavior.type === "Email") {
            if (behavior.value !== null && behavior.value.length > 0) {
                showEmailDetails(behavior.value);
            }
        } else if (behavior.type === "PageView") {
            if (behavior.value !== null && behavior.value.indexOf('http') > -1) {
                window.open(behavior.value);
            }
        } else if (behavior.type === "Conversation") {
            if (behavior.name === "CC" || behavior.name === "BCC") {
                if (/^\d+$/.test(behavior.value)) {
                    $("#dialog").dialog({ minWidth: 700, minHeight: 500 });
                    salesApp.previewEmailById($http, behavior.value, $('#storySoFarEmailContentIFrame'));
                }
                return;
            }
            if (behavior.manualEntry) {
                $scope.loadManualTime(behavior);
            }
            if ((behavior.name === "notes") && ((behavior.deleted === undefined) || (behavior.deleted === null) || (!behavior.deleted))) {
                if ((behavior.noteid !== null) && (behavior.noteid !== '')) {
                    NotesDataFactory.noteId = behavior.noteid;
                    ModalService.showModal({
                        templateUrl: window._llAppPath + 'ClientApps/SalesApp/templates/note-modal.html',
                        controller: "NotesController",
                        inputs: {
                            modalTitle: 'View Note',
                            noteId: behavior.noteid
                        }
                    }).then(function (modal) {
                        modal.element.modal();
                        modal.close.then(function (result) {
                            return true;
                        });
                    });
                }
                return;
            }

        }
    };

    function previewEmailById(emailId, $contentArea) {
        $.get('Sales/GetEmail?id=' + emailId, function (data) {
            showEmailContent(data.body, $contentArea);
        });
    }
    function showEmailContent (content, $contentArea) {
        validateServiceAuth(content);
        var s = "<html><head><title>Email Preview</title></head><body><div>" + content + "</div></body></html>";
        $contentArea.contents().find('html').html(s);
    }
function PlayerLink ( link, pstrPosition, title )
{
    debugger;

    try
    {
        var URL2Play          = ( typeof link === 'string' || link instanceof String ) ? link : link.href;
        var PlayerURLSuffix   = 'COMMON/STT_VideoPlayer.HTML';
        var AbsolutePlayerURL = location.origin + _llAppPath + PlayerURLSuffix;

        console.log ( "PlayerURLSuffix   = " + PlayerURLSuffix );
        console.log ( "AbsolutePlayerURL = " + AbsolutePlayerURL );
        console.log ( "URL2Play          = " + URL2Play );
        console.log ( 'pstrPosition      = ' + pstrPosition );

        debugger;

        if ( ( URL2Play.match ( '[.][WwMm][AaPp4][Vv34Aa]$' ) > '' ) || ( URL2Play.match ( '[.][Tt][Xx][Tt]$' ) > '' ) || ( URL2Play.match( '[.][Vv][Tt][Tt]$' ) > '' ) )
        {
            var URL    = AbsolutePlayerURL + "?m4vurl=" + URL2Play.replace ( /^https*:/i, "HTTPS:" ) + "&start=0&leadid=" + _leadid;

            console.log ( "MP3/MP4/M4A Link = " + URL2Play );
            console.log ( "leadId           = " + _leadid );

            if ( title !== undefined && title !== null && title !== '' )
            {
                URL += "&title=" + encodeURIComponent ( title );
            }

            var winName     = 'STTPlayer';
            var winFeatures = 'width=700,height=550,left=' + SetLeft ( pstrPosition ) + ',top=50,resizable=1';

            console.log ( "Window Name      = " + winName );
            console.log ( "URL popup        = " + URL );
            console.log ( 'Window Features  = ' + winFeatures );

            localStorage.setItem ( '_leadid'              , _leadid );
            localStorage.setItem ( 'PlayerURL'            , URL );
            localStorage.setItem ( 'PlayerLinkURL'        , URL2Play );
            localStorage.setItem ( 'PlayerLeadId'         , _leadid );
            localStorage.setItem ( 'PlayerWindowName'     , winName );
            localStorage.setItem ( 'PlayerWindowFeatures' , winFeatures );

            window.open ( URL ,
                          winName ,
                          winFeatures );

            return false;
        }   // if ( ( URL2Play.match ( '[.][Mm][Pp4][34Aa]$' ) > '' ) || ( URL2Play.match ( '[.][Tt][Xx][Tt]$' ) > '' ) || ( URL2Play.match ( '[.][Vv][Tt][Tt]$' ) > '' ) )
    }
    catch ( err )
    {
        console.log( "PlayerLink Message    = " + err.message );
        console.log( "PlayerLink StackTrace = " + err.stack );
    }

    return true;


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

        if ( Object.is( pstrPosition, undefined ) )
        {
            return 50;
        }   // TRUE (The optional PlayerLink argument is absent.) block, if ( Object.is ( pstrPosition , undefined ) )
        else
        {
            if ( typeof pstrPosition === 'string' || pstrPosition instanceof String )
            {
                if ( pstrPosition.substring( 0, 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )
                {
                    var strLeftPos = pstrPosition.substring( 2 );
                    var dblLeftPosFraction = parseFloat( strLeftPos );
                    var intMaxLeft = Math.round( Screen.availWidth / 2 );

                    if ( isNaN( dblLeftPosFraction ) )
                    {
                        var intLeftPosition = parseInt( strLeftPos, 10 );

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
                            return Math.round( Screen.availWidth * dblLeftPosFraction );
                        }   // FALSE (anticipated outcome) block, if ( dblLeftPosFraction > 1.0 )
                    }   // FALSE (anticipated outcome) block, if ( isNaN ( dblLeftPosFraction ) )
                }   // TRUE (The first two characters and its length are acceptable.) block, if ( pstrPosition.substring ( 0 , 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )
                else
                {   // The format of the string is incorrect. Treat as if absent.
                    return 50;
                }   // FALSE (Either the first two characters or the overall length is wrong.) block, if ( pstrPosition.substring ( 0 , 2 ).toLowerCase === 'w=' && pstrPosition.length > 2 )                                   //
            }   // TRUE (The optional PlayerLink argument has the expected type.) block, if ( typeof pstrPosition === 'string' || pstrPosition instanceof String )
            else
            {   // The argument type is not String. Treat as if absent.
                return 50;
            }   // FALSE (Though present, the optional PlayerLink has the incorrect type.) block, if ( typeof pstrPosition === 'string' || pstrPosition instanceof String )
        }   // FALSE (The optional PlayerLink argument is present.) block, if ( Object.is ( pstrPosition , undefined ) )
    }   // function SetLeft
}   // function PlayerLink


console.log ( ScriptInfoForLog ( CallMapGrid_SCRIPTSOURCE ,
                                 CallMapGrid_VERSION ,
                                 CallMapGrid_LastUpdated ,
                                 'loaded' ) );

//  +--------------------------------------------------------------------------+
//  |                            End of CallMapGrid                            |
//  +--------------------------------------------------------------------------+
