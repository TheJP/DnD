(function () {

    function showSummary() {
        var form = this;
        setTimeout(function () {
            var message = $(form).find('.validation-summary');
            if ($(form).find('.summary-content').text().trim()) {
                message.addClass('visible');
            } else {
                message.removeClass('visible');
            }
        }, 10);
    }

    $(document).ready(function () {
        $('form.form.summary')
            .each(showSummary)
            .submit(showSummary);
        $('form.form .checkbox').checkbox();
    });
})();