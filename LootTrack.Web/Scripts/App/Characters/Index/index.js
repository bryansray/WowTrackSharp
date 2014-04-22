require(['jquery', 'knockout', 'kendo'], function ($, ko, kendo) {
    var viewModel = function() {
        var self = this;

        self.selectedItem = ko.observable();

        $("#lootGrid").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/Loots"
                },
                pageSize: 10
            },
            filterable: true,
            selectable: true,
            sortable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },

            change: function (event) {
                var item = this.dataItem(this.select()[0]);
                self.selectedItem(item);
                console.log(item);
            },
            columns: [
                { field: "Name", title: "Name", width: 300, attributes: { class: "ItemLevel-#= ItemLevel # ItemQuality-#= Quality #" } },
                { field: "ItemLevel", title: "Level", width: 75, sortable: false },
                { field: "Character.Name", title: "Received By", width: 100 },
                { field: "ReceivedAt", title: "Received On", width: 100, sortable: false }]
        });
    };
    
    ko.applyBindings(viewModel);
});