(function () {

    function setLifeAndMana() {
        if ($(this).find('option[value=' + $(this).val() + ']').text() == 'Drache') {
            $('#initial-life').val(0);
            $('#initial-mana').val(0);
            $('.initial').hide();
        } else {
            $('.initial').show();
        }
    }
    
    $(document).ready(function () {
        $('#race')
            .each(setLifeAndMana)
            .change(setLifeAndMana);
    });

})();