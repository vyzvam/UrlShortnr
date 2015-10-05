var UrlShortnrManager = {
    config: {
        form: '#url-form',
        message: '#login-message'
    },
    init: function () {
        this.setDefaults();
    },

    setDefaults: function () {

        var self = this;
        $(this.config.form).validate({
            ignore: "",
            rules: {
                Name: {
                    required: true,
                    url: true
                }
            },

            messages: { Name: "Please specify a valid url. make sure 'http:// or https:// is provided" },
            errorElement: 'div',
            errorLabelContainer: '.errorTxt'
        });
    }
};

jQuery(document).ready(function ($) {

    console.log('Document Ready...');

    UrlShortnrManager.init();  

});

