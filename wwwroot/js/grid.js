let gridApi;

window.initializeAgGrid = function (elementId, gridOptions) {
    const gridDiv = document.querySelector('#' + elementId);
    
    // Process cell class rules
    if (gridOptions.columnDefs) {
        gridOptions.columnDefs.forEach(col => {
            if (col.cellClassRules) {
                const rules = {};
                for (const [className, ruleStr] of Object.entries(col.cellClassRules)) {
                    rules[className] = eval('(' + ruleStr + ')');
                }
                col.cellClassRules = rules;
            }
            if (col.valueFormatter && typeof col.valueFormatter === 'string') {
                col.valueFormatter = eval('(' + col.valueFormatter + ')');
            }
        });
    }
    
    // Process getRowId
    if (gridOptions.getRowId && typeof gridOptions.getRowId === 'string') {
        gridOptions.getRowId = eval('(' + gridOptions.getRowId + ')');
    }
    
    gridApi = agGrid.createGrid(gridDiv, gridOptions);
};

window.updateAgGridRow = function (rowData) {
    if (gridApi) {
        const rowNode = gridApi.getRowNode(rowData.symbol);
        if (rowNode) {
            // Flash cells that changed
            const flashCells = [];
            if (rowNode.data.price !== rowData.price) {
                flashCells.push('price');
            }
            if (rowNode.data.change !== rowData.change) {
                flashCells.push('change');
                flashCells.push('changePercent');
            }
            if (rowNode.data.volume !== rowData.volume) {
                flashCells.push('volume');
            }
            
            // Update the row data
            rowNode.setData(rowData);
            
            // Flash the changed cells
            if (flashCells.length > 0) {
                gridApi.flashCells({
                    rowNodes: [rowNode],
                    columns: flashCells,
                    flashDuration: 1000,
                    fadeOut: 500
                });
            }
        } else {
            // Add new row if it doesn't exist
            gridApi.applyTransaction({ add: [rowData] });
        }
    }
};