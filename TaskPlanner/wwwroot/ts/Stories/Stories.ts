import { enableRipple } from '@syncfusion/ej2-base';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { Filter, Grid, Page, Pager, QueryCellInfoEventArgs, RowDataBoundEventArgs, Sort, SortEventArgs, Toolbar } from '@syncfusion/ej2-grids';
import { Dialog } from '@syncfusion/ej2-popups';
enableRipple(true);
Grid.Inject(Sort, Page, Filter, Toolbar);
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
    allowFiltering: false,
    allowPaging: true,
    allowSorting: true,
    columns: [
        { field: 'StoryId', headerText: 'Story Id' },
        { field: 'Title', headerText: 'Title' },
        { field: 'ThemeName', headerText: 'Theme Name' },
        { field: 'EpicName', headerText: 'Epic Name' },
        { field: 'Priority', headerText: 'Priority' },
        { field: 'Benifit', headerText: 'Benifit' },
        { field: 'Penalty', headerText: 'Penalty' },
        { field: 'StoryPoints', headerText: 'Story Points' },
        { field: 'Tag', headerText: 'Tag' }
    ],
    created: create,
    dataSource: templatedata,
    load: load,
    pageSettings: { pageSize: 10 },
    toolbar: ['search'],
});
storiesList.appendTo('#storiesList');


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