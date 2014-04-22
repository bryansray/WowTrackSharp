requirejs.config({
    baseUrl: '/Scripts/App',

    paths: {
        jquery: '../jquery-2.1.0',
        knockout: '../knockout-3.1.0.debug',
        mapping: '../knockout.mapping-latest',
        amplify: '../amplify',
        respond: '../respond',
        text: '../text',
        domReady: '../domReady',

        kendo: '../KendoUI/kendo.web.min',

        bootstrap: '../bootstrap'
    },


    deps: ['knockout', 'mapping'],

    callback: function (ko, mapping) {
        ko.mapping = mapping;
    },

    shim: {
        'bootstrap': {
            deps: ['jquery'],
            exports: 'bootstrap'
        },

        'amplify': {
            deps: ['jquery', 'respond'],
            exports: 'amplify'
        },
    }
});