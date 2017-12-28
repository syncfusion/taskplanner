import { KeyboardEventArgs } from '@syncfusion/ej2-base';
import { Button } from '@syncfusion/ej2-buttons';
import { Filter, Grid, Page, QueryCellInfoEventArgs, Sort, Toolbar } from '@syncfusion/ej2-grids';
Grid.Inject(Sort, Page, Filter, Toolbar);
declare let storiesList: any;

function tooltip(querycell: QueryCellInfoEventArgs) {
	if (querycell.data[querycell.column.field]) {
		querycell.cell.setAttribute('title', querycell.data[querycell.column.field]);
	}
}


let storyListGrid: Grid = new Grid({
	rowTemplate: '#rowtemplate',
	columns: [
		{ field: 'ProjectId' },
		{ field: 'ProjectName' },
		{ field: 'ProjectDescription' },
	],
	dataSource: storiesList,
	queryCellInfo: tooltip,
});
storyListGrid.appendTo('#projectGrid');

(<any>window).editClick = function (id) {
	alert(id);
};
(<any>window).deleteClick = function (id) {
	alert(id);
};
(<any>window).faviouriteClick = function (id) {
	alert(id);
};
(<any>window).shareClick = function (id) {
	alert(id);
};
(<any>window).permissionClick = function (id) {
	alert(id);
};

(<any>window).gridRowClick = function (row) {
	$.each($(".grid-row"), function (key, value) {	
		$(this).removeClass('active');
	});
	$(row).addClass('active');

	window.location.href = "/";
};
