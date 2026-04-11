function UploadFile() {
    var box = bootbox.dialog({
        message:
            "<div style='text-align:left;' title='Specify each file to be uploaded or linked. Very large (over 1 GB) upload requests are not supported.'><strong>" +
            "<span style='font-size:20px; width:300px; text-align: center; color: black; background-color: white;'>&nbsp;Upload/Link One File For Current Contact</span><br /><br />" +
            "<span style='font-size:14px;'>" +
            "Optionally add a description of the file being uploaded or linked&nbsp;<input type='text' id='friendly' style='width:750px;' onblur='debugger; localStorage.setItem(\"Friendly\", $(\"#friendly\").val().trim());'><br />" +
            "&nbsp;&nbsp;For example:<br />&nbsp;&nbsp;'32 min Zoom Meeting on 11/20/22 - discussed proposal' or<br />&nbsp;&nbsp;'18 min phone call on 11/21/22 - discussed contract terms'<br /><br />" +
            "<div style='margin-bottom:10px;'>" +
            "<label>" +
            "<input type='checkbox' id='enableTranscribe' /> Transcribe" +
            "</label>" +
            "</div>" +
            "Then: Select file name to be uploaded and attached&nbsp;<input type='file' id='file-input' accept='.mp3,.mp4,.wav,.w4a, audio/ mpeg, audio / wav, video / mp4'><br />" +
            "<span title='Specify the full name (https:// ...) of a file already on a public site (Amazon, DropBox, Google, Microsoft, ...).'>Or: Specify the full name of a file on a public site to be linked and attached&nbsp;" +
            "<input type='text' id='publicfile' style='width:750px'></span><br />" +
            "</span></strong></div>",
        size: "large",
        buttons: {
            ok: {
                label: "Ok",
                className: "btn-success",
                callback: function () {
                    var enableTranscribe = $("#enableTranscribe").is(":checked");
                    var PublicSite = $("#publicfile").val().trim();
                    if (PublicSite.length < 1) {
                        return true;
                    } 
                    if (PublicSite.toLowerCase().indexOf("http") != 0) {
                        bootbox.alert("<br /><b>Public site name must start with 'http:' or 'https:'.</b><br />");
                        return false;
                    }
                    if (PublicSite.indexOf("\\") > 0) {
                        bootbox.alert("<br /><b>Public site name must contain forward slashes, not backward slashes.</b><br />");
                        return false;
                    }
                    if (PublicSite.indexOf("/") < 1) {
                        bootbox.alert("<br /><b>Public site name is invalid.</b><br />");
                        return false;
                    }
                    Friendly = $("#friendly").val().trim();
                    localStorage.removeItem("Friendly");
                    $.ajax({
                        type: "POST",
                        url: _llAppPath + "Open/ApplicantUpload",//_llAppPath + "Home/ApplicantUpload",
                        data: { "Code": "FFFFFFFFX", "LeadId":_leadid, "FileName": file.name, "Contents": e.target.result, "CaseId": "", "Friendly": Friendly, "Domain":_domainid, "Tenant":_tenantid, "User":_userid , "Login":_login, "EnableTranscribe": enableTranscribe },
                        success: function (data) {
                            if ((data !== undefined) && (data !== null) && (data !== '')) {
                                bootbox.alert(data);
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            bootbox.alert(textStatus + " " + jqXHR.responseText + " " + errorThrown);
                        }
                    });
                }
            },
            cancel: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    return;
                }
            }
        }
    });
    var dialog = box.find('.modal-dialog');
    box.css('display', 'block'); box.css('border-radius', '10px !important');
    // dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
    document.getElementById('file-input').addEventListener('change', ReadSingleFile, false);
}

function ReadSingleFile(e) {
    var enableTranscribe = $("#enableTranscribe").is(":checked");
    var file = e.target.files[0];
    if (!file) {
        bootbox.alert("<br /><b>Cannot access file.</b><br />");
        return;
    }
    var reader = new FileReader();
    reader.onload = function (e) {
        debugger;
        Friendly = localStorage.getItem("Friendly");
        localStorage.removeItem("Friendly");
        $.ajax({
            type: "POST",
            url: _llAppPath + "Open/ApplicantUpload",//_llAppPath + "Home/ApplicantUpload",
            data: { "Code": "FFFFFFFFX", "LeadId":_leadid, "FileName": file.name, "Contents": e.target.result, "CaseId": "", "Friendly": Friendly, "Domain":_domainid, "Tenant":_tenantid, "User":_userid ,"Login":_login, "EnableTranscribe": enableTranscribe },
            success: function (data) {
                if ((data !== undefined) && (data !== null) && (data !== '')) {
                    bootbox.alert(data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                bootbox.alert(textStatus + " " + jqXHR.responseText + " " + errorThrown);
            }
        });
    };
    reader.readAsDataURL(file); // base64
	
	
}