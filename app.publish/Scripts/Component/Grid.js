// public variable collection of grid instance
let adsGrid = {};

// initOneGrid and add to adsGrid
let initGrid = function (gridId, controllerName, size, orderBy, usernameSession) {
    // public var
    let _g = {};

    _g.isForLookup = false;

    // add to grid collection
    if (!adsGrid[gridId]) {
        adsGrid[gridId] = _g;
    }

    let url = {
        gridList: controllerName + '/GridList',
        exportToExcel: controllerName + '/ExportToExcel',
        getAdaptiveFilter: controllerName + '/GetAdaptiveFilter',
        exportToExcel: controllerName + '/ExportToExcel',
        exportToPdf: controllerName + '/ExportToPDF',
    };


    let page = 1;

    // filter
    //let adaptiveFilter = createAdaptiveFilters(gridId, '/ExplorationStructure/GetAdaptiveFilter', usernameSession);
    //if (gridId == 'ExplorationStructureControllerReview_grid') {
    //    adaptiveFilter = createAdaptiveFilters(gridId, '/ExplorationStructure/GetAdaptiveFilterView', usernameSession);
    //}
    //else if (gridId == 'ExplorationStructureControllerReport_grid') {
    //    adaptiveFilter = createAdaptiveFilters(gridId, '/ExplorationStructure/GetAdaptiveFilterReport', usernameSession);
    //}
    //else if (gridId == 'ESDCController_grid') {
    //    adaptiveFilter = createAdaptiveFilters(gridId, '/ESDC/GetAdaptiveFilterESDC', usernameSession);
    //}
    //else {
    //    adaptiveFilter = createAdaptiveFilters(gridId, '/ExplorationStructure/GetAdaptiveFilter', usernameSession);
    //}

    let adaptiveFilter = createAdaptiveFilters(gridId, '/phe_xplore/ExplorationStructure/GetAdaptiveFilter', usernameSession);
    if (gridId == 'ExplorationStructureControllerReview_grid') {
        adaptiveFilter = createAdaptiveFilters(gridId, '/phe_xplore/ExplorationStructure/GetAdaptiveFilterView', usernameSession);
    }
    else if (gridId == 'ExplorationStructureControllerReport_grid') {
        adaptiveFilter = createAdaptiveFilters(gridId, '/phe_xplore/ExplorationStructure/GetAdaptiveFilterReport', usernameSession);
    }
    else if (gridId == 'ESDCController_grid') {
        adaptiveFilter = createAdaptiveFilters(gridId, '/phe_xplore/ESDC/GetAdaptiveFilterESDC', usernameSession);
    }
    else {
        adaptiveFilter = createAdaptiveFilters(gridId, '/phe_xplore/ExplorationStructure/GetAdaptiveFilter', usernameSession);
    }

    let textFilter = createTextFilters(gridId);
    let numberFilter = createNumberFilters(gridId);
    let quickFindFilter = null;
    let advanceSearchFilter = createAdvanceSearch(gridId);

    // grid parameter
    function buildGridParam() {
        let filters = [];
        let filterIncludes = adaptiveFilter.getFilters();
        let filterText = textFilter.getFilters();
        let filterNumber = numberFilter.getFilters();
        let filterAdvanceSearch = advanceSearchFilter.getFilters();

        filters = filters.concat(filterIncludes);
        filters = filters.concat(filterText);
        filters = filters.concat(filterNumber);

        filters = filters.concat(filterAdvanceSearch);

        if (quickFindFilter) {
            filters.push(quickFindFilter);
        }

        let param = {
            isForLookup: _g.isForLookup,
            gridId: gridId,
            page: page,
            size: size,
            orderBy: orderBy,
            filterItems: filters,
            usernameSession: usernameSession
        };


        return param;
    }

    // reload grid ke server
    function reload() {

        let gridParam = buildGridParam();
        console.log(gridParam);

        advanceSearchFilter.updateAdvanceSearchSectionState();

        //$.post(url.gridList, gridParam, function (newGridList) {
        //    $('#' + gridId).html($(newGridList));
        //    setTotalItemToTitle();
        //    advanceSearchFilter.setAdvanceSearchState();
        //})

        //$.post('/'+controllerName+'/GridList', gridParam, function (newGridList) {
        //    $('#' + gridId).html($(newGridList));
        //    setTotalItemToTitle();
        //    advanceSearchFilter.setAdvanceSearchState();
        //})

        $("#loadingMaskGrid").css('visibility', 'visible');

        $.post('/phe_xplore/' + controllerName + '/GridList', gridParam, function (newGridList) {
            $('#' + gridId).html($(newGridList));
            setTotalItemToTitle();
            advanceSearchFilter.setAdvanceSearchState();
        })
        .done(function () {
            $("#loadingMaskGrid").css('visibility', 'hidden'); // Hide loading spinner
        })
    }

    // download excel
    function downloadSpreadsheet() {
        let gridParam = buildGridParam();

        //$.ajax({
        //    url: '/ExplorationStructure/ExportToExcel',
        //    data: gridParam,
        //    method: 'POST',
        //    xhrFields: {
        //        responseType: 'blob'
        //    },
        //    success: function (data) {
        //        var a = document.createElement('a');
        //        var url = window.URL.createObjectURL(data);
        //        a.href = url;
        //        a.download = controllerName + ' Report.xlsx';
        //        document.body.append(a);
        //        a.click();
        //        a.remove();
        //        window.URL.revokeObjectURL(url);
        //    }
        //});

        $.ajax({
            url: '/phe_xplore/ExplorationStructure/ExportToExcel',
            data: gridParam,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            beforeSend: function () {
                $("#loadingMaskGrid").css('visibility', 'visible');
            },
            success: function (data) {
                $("#loadingMaskGrid").css('visibility', 'hidden');
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = 'ProspectiveResources_Report.xlsx';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
        });

    }
    function downloadSpreadsheetRJPP() {
        let gridParam = buildGridParam();

        //$.ajax({
        //    url: '/ExplorationStructure/ExportToExcelRJPP',
        //    data: gridParam,
        //    method: 'POST',
        //    xhrFields: {
        //        responseType: 'blob'
        //    },
        //    success: function (data) {
        //        var a = document.createElement('a');
        //        var url = window.URL.createObjectURL(data);
        //        a.href = url;
        //        a.download = 'ExportToExcelRJPP_Report.xlsx';
        //        document.body.append(a);
        //        a.click();
        //        a.remove();
        //        window.URL.revokeObjectURL(url);
        //    }
        //});

        $.ajax({
            url: '/phe_xplore/ExplorationStructure/ExportToExcelRJPP',
            data: gridParam,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            beforeSend: function () {
                $("#loadingMaskGrid").css('visibility', 'visible');
            },
            success: function (data) {
                $("#loadingMaskGrid").css('visibility', 'hidden');
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = 'ExportToExcelRJPP_Report.xlsx';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
        });

    }

    // download excel
    function downloadSpreadsheetESDC() {
        let gridParam = buildGridParam();

        //$.ajax({
        //    url: '/ESDC/ExportToExcelESDC',
        //    data: gridParam,
        //    method: 'POST',
        //    xhrFields: {
        //        responseType: 'blob'
        //    },
        //    success: function (data) {
        //        var a = document.createElement('a');
        //        var url = window.URL.createObjectURL(data);
        //        a.href = url;
        //        a.download = controllerName + ' Report.xlsx';
        //        document.body.append(a);
        //        a.click();
        //        a.remove();
        //        window.URL.revokeObjectURL(url);
        //    }
        //});

        $.ajax({
            url: '/phe_xplore/ESDC/ExportToExcelESDC',
            data: gridParam,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            beforeSend: function () {
                $("#loadingMaskGrid").css('visibility', 'visible');
            },
            success: function (data) {
                $("#loadingMaskGrid").css('visibility', 'hidden');
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = controllerName + ' Report.xlsx';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
        });

    }

    function downloadPdf() {
        let gridParam = buildGridParam();

        var timeOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        let param = {
            filterList: gridParam,
            headerText: new Date().toLocaleDateString('en-EN', timeOptions),
            tableHeaderSizes: [30, 0, 0, 0, 0, 0, 50]
        };
        $.ajax({
            url: url.exportToPdf,
            data: param,
            method: 'POST',
            xhrFields: {
                responseType: 'blob'
            },
            success: function (data) {
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = controllerName + ' Report.pdf';
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
        });
    }

    // quick find
    let quickFindModal;

    function showQuickFindDialog() {
        if (!quickFindModal) {
            let modalId = "#" + gridId + "_QuickFindModal";

            let modalElement = $(modalId);
            quickFindModal = new bootstrap.Modal(modalElement);
        }

        quickFindModal.show();
    }

    function quickFind(formElement) {
        let keyword = $(formElement).find("input").val();
        if (keyword) {
            quickFindFilter = [{
                Name: "AnyField",
                Value: keyword,
                FilterType: 'Contains'
            }];
        } else {
            quickFindFilter = null;
        }

        quickFindModal.hide();
        reload();
        return false;
    }

    // sorting
    function setOrderBy(columnId, order) {
        orderBy = columnId + " " + order;
        reload();
    }

    // clear all filter
    function clearAllFilter() {
        adaptiveFilter.clearAll();
        textFilter.clearAll();
        numberFilter.clearAll();
        advanceSearchFilter.clearAll();
        quickFindFilter = null;
        reload();
    }

    // clear one filter
    function clearFilter(columnId) {
        adaptiveFilter.clear(columnId);
        textFilter.clear(columnId);
        numberFilter.clear(columnId);
        reload();
    }

    // page change
    function onPageChange(newPage) {
        page = newPage;
        reload();
    };




    // display total item to table title
    function setTotalItemToTitle() {
        if (_g.isForLookup == false) {
            let total = $('#' + gridId).find('.total-label').attr('data-total');

            if (total) {
                $("#crud-grid-count").text("(" + total + ")");
            }

        }
    }
    setTotalItemToTitle();

    // public property and method
    _g.reload = reload;
    _g.downloadSpreadsheet = downloadSpreadsheet;
    _g.downloadSpreadsheetRJPP = downloadSpreadsheetRJPP;
    _g.downloadSpreadsheetESDC = downloadSpreadsheetESDC;
    _g.downloadPdf = downloadPdf;
    _g.showQuickFindDialog = showQuickFindDialog;
    _g.quickFind = quickFind;
    _g.setOrderBy = setOrderBy;
    _g.clearAllFilter = clearAllFilter;
    _g.clearFilter = clearFilter;
    _g.onPageChange = onPageChange;
    _g.adaptiveFilter = adaptiveFilter;
    _g.textFilter = textFilter;
    _g.numberFilter = numberFilter;
    _g.advanceSearchFilter = advanceSearchFilter;
}











