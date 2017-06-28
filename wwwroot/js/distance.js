
    $().ready(function () {
        $("#Form_Distance").validate({
            rules: 
            {
                Route:{
                    required: true,
                    regex: "^([A-Z][-]+)*[A-Z]$"
                }
                
            },
            messages:
            {
                Route:{regex:"Wrong format, format should like 'A-B-C-E'"}
            },
            submitHandler: function(form) {      
                Submit();
            } 
        });
        $('#ButtonSubmit').click(function () { Submit(); });
    });
    //Add regex validate method
    jQuery.validator.addMethod(
        "regex",  
        function(value, element, params)
        {    
            var exp = new RegExp(params);    
            return exp.test(value);                 
        },
        //default error message
        "Wrong Format Input!"
    ); 
    
    function Submit() {
        if( $("#Form_Distance").valid() ){
            var postData = $('#Form_Distance').serialize();
            $.ajax({
                url: "/QueryRoute/Distance",
                type: "POST",
                data: postData,
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    if (data) {
                        $(".output").children().remove();
                        $("<li>The distance is : " + data + "</li>").appendTo(".output");
                    }
                    else {
                        $(".output").children().remove();
                        $("<li>Not a Through Road.</li>").appendTo(".output");
                    }
                }
            });
        }
    }
