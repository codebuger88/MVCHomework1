(function ($) {
    $.validator.addMethod('overDate', function (value, element, param) {
        if (!value) {
            return true;
        }
        try {
            var dateValue = value.replace(/-/g, "/").replace(/\./g, "/");
        }
        catch (e) {
            return false;
        }

        return dateValue <= param.max;
    });

    $.validator.unobtrusive.adapters.add('overdate', ['max'], function (options) {
        var params = {
            max: options.params.max
        };

        options.rules['overDate'] = params;
        if (options.message) {
            options.messages['overDate'] = options.message;
        }
    });
}(jQuery));