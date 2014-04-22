define(['jquery', 'knockout', 'amplify', 'kendo'], function ($, ko, amplify, kendo) {
    var viewModel = function () {
        var self = this;

        self.selectedItem = ko.observable();

        $("#update").click(function (event) {
            amplify.request("charactersUpdate", { id: $("#character").data("characterid") }, function (data) {
                // TODO : Post a noty here ...
                console.log(data);
            });
        });

        var characterId = $("#character").data('characterid');

        $("#lootsGrid").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: "/api/characters/" + characterId + "/loots"
                },
                pageSize: 10
            },
            selectable: true,
            groupable: true,
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
                { field: "ItemLevel", title: "Level", width: 75 },
                { field: "ReceivedAt", title: "Received On", width: 150 }]
        });
    };

    ko.applyBindings(new viewModel());
});