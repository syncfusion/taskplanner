var newdialogObj;
var progressModel = document.getElementById('progressDialogModal');
var isEdit = false;
var projectIdstring; 
ej.base.enableRipple(true);
/**
 *  Sample for types of toast
 */

function editClick(id) {
    projectIdstring = id;
    progressModel.style.cssText = "display : block";
    $.ajax({
        data: { 'projectId': id },
        success: function (response) {
            if (response.status === true) {
                document.getElementById('projectmodalDialog_title').innerHTML = "Edit Project";
                var projectNameContainer = document.getElementById('projectnameinput');
                projectNameContainer.value = response.name;
                var descriptionContainer = document.getElementById('descriptioninput');
                descriptionContainer.value = response.description;
                newdialogObj.show();
            }
            else {
                toasts[0].content = response.message;
                toastObj.show(toasts[0]);;
            }
        },
        error: function () {
            toasts[0].content = "Unexpected error occured";
            toastObj.show(toasts[0]);
        },
        complete: function () {
            progressModel.style.cssText = "display : none";
        },
        type: 'POST',
        timeout: 180000,
        url: 'project/edit',
    });
}
function deleteClick(id) {
    var confirm = window.confirm("Are you sure you want to delete project? Once deleted it cannot be recovered.");
    if (!confirm)
        return false;
    $.ajax({
        data: { 'projectId': id },
        success: function (response) {
            if (response.isSuccess === true) { 
                toasts[1].content = response.message;
                toastObj.show(toasts[1]);
                setTimeout(function () {
                    window.location.reload();
                }, 200);
            }
            else {
                toasts[0].content = response.message;
                toastObj.show(toasts[0]);;
            }
        },
        error: function () {
            toasts[0].content = "Unexpected error occured";
            toastObj.show(toasts[0]);
        }, 
        type: 'POST',
        timeout: 180000,
        url: 'project/delete/' + id,
    });
}
function faviouriteClick(id, isFavourite) {
    $.ajax({
        data: { 'projectId': id, 'isFavourite': isFavourite },
        success: function (response) {
            if (response.isSuccess === true) {
                toasts[1].content = response.message;
                toastObj.show(toasts[1]);
                setTimeout(function () {
                    window.location.reload();
                }, 200);
            }
            else {
                toasts[0].content = response.message;
                toastObj.show(toasts[0]);;
            }
        },
        error: function () {
            toasts[0].content = "Unexpected error occured";
            toastObj.show(toasts[0]);
        }, 
        type: 'POST',
        timeout: 180000,
        url: 'project/favourite/',
    });
}
function permissionClick(id) {
    alert(id);
}
function favouritesClick(id) {
    loadprojectsTab(id);
}
function yoursClick(id) {
    loadprojectsTab(id);
};
function allClick(id) {
    loadprojectsTab(id);
}
function loadprojectsTab(projectId) {
    progressModel.style.cssText = "display : block";
    document.getElementById('projecttabGrid').innerHTML = "";
    var url = "projecttab/";
    $.ajax({
        data: {
            'projectId': projectId,
        },
        error: function (response) {
        },
        success: function (response) {
            $('#projecttabGrid').html(response);
            progressModel.style.cssText = "display : none";
        },
        type: "POST",
        url: url,
    });
}

function addProject(data) {
    document.getElementById('projecttabGrid').innerHTML = "";
    var url = "project/addproject";
    $.ajax({
        data: {
            'projectId': projectId,
        },
        error: function (response) {
        },
        success: function (response) {
            $('#projecttabGrid').html(response);
            progressModel.style.cssText = "display : none";
        },
        type: "POST",
        url: url,
    });
}

var ajax = new ej.base.Ajax('project/addproject', 'GET', true);
ajax.send().then(); // Rendering Dialog on AJAX success 
ajax.onSuccess = function (data) {
    newdialogObj = new ej.popups.Dialog({
        width: '600px',
        height: '300px',
        header: 'Add Project',
        content: data,
        closeOnEscape: false,
        target: document.getElementById('targetbody'),
        isModal: true,
        showCloseIcon: false,
        animationSettings: { effect: 'None' },
        buttons: [{
            click: clearButtonClick,
            buttonModel: { id: 'addprojectClearButton', content: 'Clear', cssClass: 'e-flat dlg-btn-secondary' },
        },
        {
            click: cancelButtonClick,
            buttonModel: { id: 'addprojectCancelButton', content: 'Cancel', cssClass: 'e-flat dlg-btn-secondary' },
        },
        {
            click: addButtonClick,
            buttonModel: { id: 'addprojectAddbutton', content: 'Update', cssClass: 'e-flat dlg-btn-primary', isPrimary: true },
        }],
        open: addMilestonedialogOpen,
    });
    newdialogObj.appendTo('#projectmodalDialog');
    newdialogObj.hide();

    document.getElementById('projectmodalDialog').style.maxHeight = '300px';
    function addButtonClick() {
        $('#erroraddproject').css('display', 'none');
        progressModel.style.cssText = "display : block";
        var canCreateproject = true;
        document.getElementById("errorprojectDescription").style.visibility = "hidden";
        var errorprojectName = document.getElementById('errorprojectName');
        var errorprojectDescription = document.getElementById('errorprojectDescription');
        var projectNameContainer = document.getElementById('projectnameinput');
        var descriptionContainer = document.getElementById('descriptioninput');
        if (projectNameContainer.value !== "" && descriptionContainer.value !== "") {
            $.ajax({
                dataType: 'json',
                type: "GET",
                url: 'project/updateproject',
                data: {
                    'description': descriptionContainer.value, 'projectname': projectNameContainer.value, 'projectId': projectIdstring
                },
                error: function (response) {
                    $('#erroraddproject').text("Unexpected error occured");
                    $('#erroraddproject').css('display', 'block');
                },
                success: function (response) {
                    if (response.status === false) {
                        if (response.isprojectNameExists === true) {
                            document.getElementById('errorprojectName').innerHTML = response.result;
                            document.getElementById("errorprojectName").style.display = "block";
                        }
                        else {
                            $('#erroraddproject').text(response.result);
                            $('#erroraddproject').css('display', 'block');
                        }
                    }
                    else if (response.status === true) {
                        toasts[1].content = response.message;
                        toastObj.show(toasts[1]);
                        newdialogObj.hide();
                        setTimeout(function () {
                            location.reload();
                        }, 200);                       
                    }
                }, 
            });
        }
        else {
            // document.getElementById('progressDialogModal').style.display = 'none';
            if (projectNameContainer.value === "") {
                errorprojectName.innerHTML = "Please enter project name";
                errorprojectName.style.display = "block";
            }
            if (descriptionContainer.value === "") {
                errorprojectDescription.innerHTML = "Please enter project description";
                errorprojectDescription.style.display = "block";
                document.getElementById("errorprojectDescription").style.visibility = "visible";
            }
            progressModel.style.cssText = "display : none";
        }
    }
    function cancelButtonClick() {
        newdialogObj.hide();
        clearButtonClick();
    }
    function addMilestonedialogOpen() {
        document.getElementsByTagName('body')[0].classList.add('mainscroller-hide');
    }
    function clearButtonClick() {
        $('#erroraddproject').css('display', 'none');
        var projectNameContainer = document.getElementById('projectnameinput');
        projectNameContainer.value = "";
        var errorprojectName = document.getElementById('errorprojectName');
        errorprojectName.value = "";
        errorprojectName.innerHTML = "";
        var errorprojectDescription = document.getElementById('errorprojectDescription');
        errorprojectDescription.value = "";
        errorprojectDescription.innerHTML = "";
        var descriptionContainer = document.getElementById('descriptioninput');
        descriptionContainer.value = "";
    } 
};


function loadProjectDialog() {
    document.getElementById('projectmodalDialog_title').innerHTML = "Add Project";
    projectIdstring = "";
    var projectNameContainer = document.getElementById('projectnameinput');
    projectNameContainer.value = "";
    var descriptionContainer = document.getElementById('descriptioninput');
    descriptionContainer.value = "";
    newdialogObj.show();
}

function shareClick(id) {
    $.ajax({
        type: "Get",
        url: 'project/shareproject',
        data: {
            'projectId': id
        },
        error: function (response) {
        },
        success: function (response) {
            $('#sharemodalDialog').html('');
            var sharedialogObj = new ej.popups.Dialog({
                width: '600px',
                height: '500px',
                header: 'Share Project',
                //created: dlgCreate,
                content: response,
                closeOnEscape: false,
                target: document.getElementById('targetbody'),
                isModal: true,
                showCloseIcon: true,
                animationSettings: { effect: 'None' },
                buttons: [{
                    click: cancelButtonClick,
                    buttonModel: { id: 'addprojectCancelButton', content: 'Cancel', cssClass: 'e-flat dlg-btn-secondary' },
                }],
            });
            sharedialogObj.appendTo('#sharemodalDialog');
            sharedialogObj.hide();
            document.getElementById('sharemodalDialog').style.maxHeight = '500px';
            function cancelButtonClick() {
                sharedialogObj.hide();
            }
            sharedialogObj.overlayClick = function () {
                sharedialogObj.hide();
            };
            document.getElementById('sharebtn').onclick = function () {
                var emailContainer = document.getElementById('shareEmail');
                var erroremail = document.getElementById('erroremail');
                if (emailContainer.value !== "") {
                    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

                    if (!re.test(String((emailContainer.value).toLowerCase()))){
                        erroremail.innerHTML = "You have entered an invalid email address";
                        erroremail.style.display = "block";
                        erroremail.style.fontWeight = "normal";
                    }
                    else{
                    $.ajax({
                        dataType: 'json',
                        type: "GET",
                        url: 'project/shareemail',
                        data: {
                            'projectId': sharedialogObj.id, 'email': emailContainer.value
                        },
                        error: function (response) {
                        },
                        success: function (response) {
                            if (response.status === true) {
                                sharedialogObj.hide();
                                toasts[1].content = "Permission added successfully. Share the project link to user.";
                                toastObj.show(toasts[1]); 
                            }
                        }, 
                    });
                }
                }
                else {
                    if (emailContainer.value === "") {
                        erroremail.innerHTML = "Required Field";
                        erroremail.style.display = "block";
                    }
                }
            }; 
            window.deleteShareProjectClick = function(id) {
                var confirm = window.confirm("Are you sure you want to delete project permission? Once deleted it cannot be recovered.");
                if (!confirm)
                    return false;
                $.ajax({
                    dataType: 'json',
                    type: "POST",
                    url: 'project/removepermission',
                    data: {
                        'permissionId': id
                    },
                    error: function (response) {
                    },
                    success: function (response) {
                        if (response.status === true) {
                            sharedialogObj.hide();
                            toasts[1].content = response.message;
                            toastObj.show(toasts[1]);
                        }
                    }, 
                });
            };
            sharedialogObj.id = id;
            var button = new ej.buttons.Button();
            button.appendTo('#sharebtn');
            var projectNameContainer = document.getElementById('shareEmail');
            projectNameContainer.value = ""; 
            sharedialogObj.show();
        },
       
    });
}
