ej.base.enableRipple(true);
var progressModel = document.getElementById('progressDialogModal');
var projectId = $("#projectId").val();
var templatedata = new ej.data.DataManager({
    adaptor: new ej.data.UrlAdaptor(),
    crossDomain: true,
    requestType: 'GET',
    url: '/showcase/aspnetcore/task-planner/storieslist/' + projectId,
});
var statusElem;
var statusObj;
var status = [
    { statusName: 'Open', statusId: '1' },
    { statusName: 'In Progress', statusId: '2' },
    { statusName: 'Hold', statusId: '3' },
    { statusName: 'Closed', statusId: '4' }
];  
function toolbarClick(args) {
    var gridObj = document.getElementById("storiesList").ej2_instances[0];
   
    if (args.item.id === 'storiesList_excelexport') {
        gridObj.excelExport();
    } 
} 
function databound()
{
    var grid = document.getElementById('storiesList').ej2_instances[0]
    if (grid.currentViewData.length === 0) {
        grid.toolbarModule.enableItems(['storiesList_excelexport'], false); // enable toolbar items.
    }
    else { 
            grid.toolbarModule.enableItems(['storiesList_excelexport'], true); // enable toolbar items.
        
    }
}
function load() {
    progressModel.style.cssText = "display : block";
    // create local data
    var statusVal = [
        { Id: '1', statusName: 'Open' },
        { Id: '2', statusName: 'In Progress' },
        { Id: '3', statusName: 'Hold' },
        { Id: '4', statusName: 'Closed' },
    ];
    document.getElementById("storiesList").ej2_instances[0].columns[12].edit = {
        params: {
            dataSource: statusVal.reverse(),
            query: new ej.data.Query(),
            fields: { value: "statusName", text: "statusName" },
            placeholder: 'Select status',
        }
    }
}
function create() {
    progressModel.style.cssText = "display : none";
}
function actionBegin(args) {
    var grid = document.getElementById('storiesList').ej2_instances[0]
    progressModel.style.cssText = "display : block";
    if (args.requestType === "save") {
        $.ajax({
            data: {
                'data': JSON.stringify(args.data),
                'projectId': projectId,
            },
            dataType: 'json',
            timeout: 180000,
            complete: function () {
                progressModel.style.cssText = "display : none";
            },
            type: "POST",
            url: '/showcase/aspnetcore/task-planner/story/addupdate/',
            error: function (response) {
                progressModel.style.cssText = "display : none";
                toasts[0].content = "Unexpected error occured";
                toastObj.show(toasts[0]); 
            },
            success: function (response) {
             grid.refresh();
                progressModel.style.cssText = "display : none";
                if (response.status === true) {
                    toasts[1].content = response.message;
                    toastObj.show(toasts[1]);
                }
                else {
                    toasts[0].content = response.message;
                    toastObj.show(toasts[0]); 
                }
            },
        });
    }
    else if (args.requestType === "delete") {
        var confirm = window.confirm("Are you sure you want to delete the selected story? Once deleted it cannot be recovered.");
        if (!confirm)
            return false;
        $.ajax({
            data: {
                'storyId': args.data[0].StoryId,
            },
            dataType: 'json',
            timeout: 180000,
            complete: function () {
                progressModel.style.cssText = "display : none";
            },
            type: "POST",
            url: '/showcase/aspnetcore/task-planner/story/delete/',
            error: function (response) {
                progressModel.style.cssText = "display : none"; 
                toasts[0].content = "Unexpected error occured";
                toastObj.show(toasts[0]); 
            },
            success: function (response) {
                grid.refresh();
                progressModel.style.cssText = "display : none";
                if (response.status === true) {
                    toasts[1].content = response.message;
                    toastObj.show(toasts[1]);
                }
                else {
                    toasts[0].content = response.message;
                    toastObj.show(toasts[0]); 
                }
            },
        });
    }
}
function actionComplete() {
    progressModel.style.cssText = "display : none"; 
}
