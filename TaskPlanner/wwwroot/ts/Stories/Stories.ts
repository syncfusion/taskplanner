import { enableRipple } from '@syncfusion/ej2-base';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { Filter, Grid, Page, Pager, QueryCellInfoEventArgs, RowDataBoundEventArgs, Sort, SortEventArgs, Toolbar, ExcelExport, Group, FilterType, Resize, ColumnChooser, Edit  } from '@syncfusion/ej2-grids';
import { Dialog } from '@syncfusion/ej2-popups';
import { ClickEventArgs } from '@syncfusion/ej2-navigations';
enableRipple(true);
Grid.Inject(Sort, Page, Filter, Toolbar, ExcelExport, Group, Resize, ColumnChooser, Edit );
let progressModel: HTMLInputElement = document.getElementById('progressDialogModal') as HTMLInputElement;

let projectId = $("#projectId").val();
let templatedata: DataManager = new DataManager({
    adaptor: new UrlAdaptor(),
    crossDomain: true,
    requestType: 'GET',
    url: '/storieslist/' + projectId,
});

let storiesList: Grid = new Grid({
    actionBegin: actionBegin,
    actionComplete: actionComplete, 
    allowExcelExport: true,
    allowPaging: false,
    allowGrouping: true, 
    allowSorting: true,
    allowFiltering: true,
    allowTextWrap: true,
    filterSettings: { type: 'checkbox' },  
    toolbar: ['excelexport', 'search', 'columnchooser', 'add', 'edit', 'delete', 'update', 'cancel'],
    showColumnChooser: true,
    //enablePersistence: true,
    editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'normal' },
    groupSettings: { showDropArea: true },
    columns: [
        { field: 'StoryId', headerText: 'Story Id', showInColumnChooser: false, isPrimaryKey: true, type: "number", visible: false },
        { field: 'TaskId', headerText: 'Task Id', type: "number" },
        { field: 'Title', headerText: 'Title', width: '150', type: "string", validationRules: { required: true } },
        { field: 'ThemeName', headerText: 'Theme', type: "string" },
        { field: 'EpicName', headerText: 'Epic', type: "string" },
        { field: 'Priority', headerText: 'Priority', type: "string" },
        { field: 'Benifit', headerText: 'Benefit', type: "number" },
        { field: 'Penalty', headerText: 'Penalty', type: "number" },
        { field: 'StoryPoints', headerText: 'Story Points', type: "number" },
        { field: 'SprintName', headerText: 'Sprint', type: "string" },
        { field: 'AssigneeName', headerText: 'Assignee', type: "string" },
        { field: 'Tag', headerText: 'Label', type: "string" }
    ],
    created: create,
    dataSource: templatedata,
    load: load,
    //pageSettings: { pageSize: 10 },

});
storiesList.appendTo('#storiesList');

storiesList.toolbarClick = (args: ClickEventArgs) => {
    if (args.item.id === 'storiesList_excelexport') {
        storiesList.excelExport();
    } 
};


function load(): void {
    progressModel.style.cssText = "display : block";
}

function create(): void {
    progressModel.style.cssText = "display : none";
}

function actionBegin(args): void {
    progressModel.style.cssText = "display : block";
    if (args.requestType == "save") {
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
            url: '/story/addupdate/',
            error: function (response) {
                progressModel.style.cssText = "display : none";
                toastr.error("Unexpected error occured");
            },
            success: function (response) {
                storiesList.refresh();
                progressModel.style.cssText = "display : none";
                if (response.status === true) {
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.message);
                }
            },
        });
    }
    else if (args.requestType == "delete") {
        var confirm = (<any>window).confirm("Are you sure you want to delete selected story? Once deleted it cannot be recovered.");
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
            url: '/story/delete/',
            error: function (response) {
                progressModel.style.cssText = "display : none";
                toastr.error("Unexpected error occured");
            },
            success: function (response) {
                storiesList.refresh();
                progressModel.style.cssText = "display : none";
                if (response.status === true) {
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.message);
                }
            },
        });
    }
}

function actionComplete(): void {
    progressModel.style.cssText = "display : none";
}