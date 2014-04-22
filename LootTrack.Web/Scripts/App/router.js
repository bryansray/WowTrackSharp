App.Router.map(function () {
    this.resource('characters');
});

App.CharactersRoute = Ember.Route.extend({
    setupController: function(controller) {
        console.log(controller);
    },

    model: function() {
        return this.store.find('character');
    }
});

App.IndexRoute = Ember.Route.extend({
    setupController: function (controller) {
        controller.set('title', 'Loot Tracker');
    }
});