window.createRating = (selector, maxValue, steep,readonly) => {
    //or for example
    var options = {
        max_value: maxValue,
        step_size: steep,
        readonly: readonly
    }
    $(selector).rate(options);
}

window.getRatingValue = (selector) => {
    return $(selector).rate("getValue");
}