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
        { field: 'StoryId', headerText: 'Story Id', showInColumnChooser: false, isPrimaryKey: true },
        { field: 'Title', headerText: 'Title',width:'150px' },
        { field: 'ThemeName', headerText: 'Theme Name' },
        { field: 'EpicName', headerText: 'Epic Name' },
        { field: 'Priority', headerText: 'Priority' },
        { field: 'Benifit', headerText: 'Benefit' },
        { field: 'Penalty', headerText: 'Penalty' },
        { field: 'StoryPoints', headerText: 'Story Points' },
        { field: 'Tag', headerText: 'Tag' }
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

function actionBegin(): void {
    progressModel.style.cssText = "display : block";
}

function actionComplete(): void {
    progressModel.style.cssText = "display : none";
}