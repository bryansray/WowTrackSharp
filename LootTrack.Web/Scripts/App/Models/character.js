App.Character = DS.Model.extend({
    name: DS.attr('string'),
    isActive: DS.attr('boolean')
});

App.Character.FIXTURES = [
    { id: 1, name: 'Virtualize', isActive: true },
    { id: 2, name: 'Khatian', isActive: true }
];