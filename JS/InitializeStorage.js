"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var BrowserStorage_1 = require("./BrowserStorage");
var Storage;
(function (Storage) {
    var blazorExtensions = 'BlazorExtensions';
    // define what this extension adds to the window object inside BlazorExtensions
    var extensionObject = {
        Storage: new BrowserStorage_1.BrowserStorage()
    };
    function initialize() {
        if (typeof window !== 'undefined' && !window[blazorExtensions]) {
            // when the library is loaded in a browser via a <script> element, make the
            // following APIs available in global scope for invocation from JS
            window[blazorExtensions] = __assign({}, extensionObject);
        }
        else {
            window[blazorExtensions] = __assign(__assign({}, window[blazorExtensions]), extensionObject);
        }
    }
    Storage.initialize = initialize;
})(Storage || (Storage = {}));
Storage.initialize();
//# sourceMappingURL=InitializeStorage.js.map