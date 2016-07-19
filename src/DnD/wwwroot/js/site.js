(function () {

    function errorLabelChange() {
        var label = $(this).siblings('.error-label');
        setTimeout(function () {
            if (label.text().trim()) {
                label.closest('.field').addClass('error');
            } else {
                label.closest('.field').removeClass('error');
            }
        }, 200);
    }

    function showSummary() {
        var form = this;
        setTimeout(function () {
            var message = $(form).find('.validation-summary');
            if ($(form).find('.summary-content').text().trim()) {
                message.addClass('visible');
            } else {
                message.removeClass('visible');
            }
            $(form).find('input, select, textarea').each(errorLabelChange);
        }, 10);
    }

    $(document).ready(function () {
        $('form.form.summary')
            .each(showSummary)
            .submit(showSummary);
        $('form.form.summary').find('input, select, textarea')
            .each(errorLabelChange)
            .change(errorLabelChange)
            .click(errorLabelChange)
            .keydown(errorLabelChange);

        $('form.form .checkbox').checkbox();
        $('.dropdown.create-default').dropdown();
        $('.dropdown.allow-add').dropdown({ allowAdditions: true });
    });

})();