//window.App = Ember.Application.create({
//    LOG_TRANSITIONS: true
//});
//
//App.ApplicationAdapter = DS.FixtureAdapter.extend();

define(['bootstrap', 'amplify'], function(bootstrap, amplify) {
    var initialize = function () {
        console.log("Initializing ...");

        amplify.request.define('characters', 'ajax', { url: "/api/characters", dataType: "json", type: "GET" });
        amplify.request.define('charactersUpdate', 'ajax', { url: "/api/characters/{id}", dataType: "json", type: "PUT" });
    };

    return { initialize: initialize };
});