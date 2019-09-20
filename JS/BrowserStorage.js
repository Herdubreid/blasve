"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
;
var BrowserStorage = /** @class */ (function () {
    function BrowserStorage() {
    }
    BrowserStorage.prototype.Length = function (storage) {
        return window[storage].length;
    };
    ;
    BrowserStorage.prototype.Key = function (storage, index) {
        return window[storage].key(index);
    };
    ;
    BrowserStorage.prototype.GetItem = function (storage, key) {
        var item = window[storage].getItem(key);
        if (item) {
            return JSON.parse(item);
        }
        return null;
    };
    ;
    BrowserStorage.prototype.SetItem = function (storage, key, keyValue) {
        window[storage].setItem(key, JSON.stringify(keyValue));
    };
    ;
    BrowserStorage.prototype.RemoveItem = function (storage, key) {
        window[storage].removeItem(key);
    };
    ;
    BrowserStorage.prototype.Clear = function (storage) {
        window[storage].clear();
    };
    ;
    return BrowserStorage;
}());
exports.BrowserStorage = BrowserStorage;
;
//# sourceMappingURL=BrowserStorage.js.map