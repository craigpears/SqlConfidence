sqlConfidenceApp.service('jsonDeserializerService', function () {
    // From http://stackoverflow.com/questions/13782052/using-javascript-to-deserialize-references-in-a-complex-object-graph-from-signal
    this.retrocycle = function(obj) {
        var catalog = [];
        catalogObject(obj, catalog);
        resolveReferences(obj, catalog);
    }

    function catalogObject(obj, catalog) {
        var i;
        if (obj && typeof obj === 'object') {
            var id = obj.$id;
            if (typeof id === 'string') {
                catalog[id] = obj;
            }
            if (Object.prototype.toString.apply(obj) === '[object Array]') {
                for (i = 0; i < obj.length; i += 1) {
                    catalogObject(obj[i], catalog);
                }
            } else {
                for (name in obj) {
                    if (typeof obj[name] === 'object') {
                        catalogObject(obj[name], catalog);
                    }
                }
            }
        }
    }

    function resolveReferences(obj, catalog) {
        var i, item, name, id;
        if (obj && typeof obj === 'object') {
            if (Object.prototype.toString.apply(obj) === '[object Array]') {
                for (i = 0; i < obj.length; i += 1) {
                    item = obj[i];
                    if (item && typeof item === 'object') {
                        id = item.$ref;
                        if (typeof id === 'string') {
                            obj[i] = catalog[id];
                        } else {
                            resolveReferences(item, catalog);
                        }
                    }
                }
            } else {
                for (name in obj) {
                    if (typeof obj[name] === 'object') {
                        item = obj[name];
                        if (item) {
                            id = item.$ref;
                            if (typeof id === 'string') {
                                obj[name] = catalog[id];
                            } else {
                                resolveReferences(item, catalog);
                            }
                        }
                    }
                }
            }
        }
    }
});