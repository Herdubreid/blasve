export const removeClass = (el, className) => {
    if (el.classList) {
        el.classList.remove(className)
    } else {
        el.className = el.className.replace(new RegExp('(^|\\b)' + className.split(' ').join('|') + '(\\b|$', 'gi'), ' ');
    }
}
export const addClass = (el, className) => {
    if (el.classList) {
        el.classList.add(className);
    } else {
        el.className += ' ' + className;
    }
    return el;
}
export const debounce = (fn, time) => {
    let timeout;
    return (...args) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => fn.apply(this, args), time);
    };
}
