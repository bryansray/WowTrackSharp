define(['knockout', 'amplify', 'text!./Templates/confirmation.html', 'domReady!'], function (ko, amplify, template) {
    var confirmationViewModel = function() {
        var self = this;

        self.title = ko.observable('Are you sure you want to continue?');

        self.element = $("#confirmation-dialog");

        self.show = function () {
            self.element.removeClass("hide").show();
        };

        self.callback = function() {};

        $("#confirmation-yes").click(function () {
            self.element.hide();
            amplify.publish('confirmation-yes');

            self.callback();
        });

        $("#confirmation-no").click(function () {
            self.element.hide();
            amplify.publish('confirmation-no');
        });
    };

    $("body").append(template);

    var viewModel = new confirmationViewModel;

    ko.applyBindings(viewModel, document.getElementById("confirmation-dialog"));
    
    return viewModel;
});