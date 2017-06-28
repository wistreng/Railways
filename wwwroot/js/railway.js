
    $(document).ready(function () {
        $('#ButtonSubmit').click(function () { SubmitEventHandler(); });
    });
    function SubmitEventHandler() {
        var start = $.trim($('#TextBox_Strat').val());
        var dest = $.trim($('#TextBox_Dest').val());
        if (start.length == 0 || dest.length == 0) {
            alert('輸入不齊全');
        }
        else {
            
            var postData = $('#Form_Login').serialize();
            $.ajax({
                url: "/QueryRoute/Possibilities",
                type: "POST",
                data: postData,
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    if (data) {
                        validate(data);
                    }
                    else {
                        alert('沒有回傳');
                    }
                }
            });
        }
    }
    function validate(result){
        $(".output").children().remove();
        $("<li>The possible routes including:</li>").appendTo(".output");
        $.each(result,function(name,value)
        {
            $("<li>The Route " + name + " distance is " + value + "</li>").appendTo(".output");
        });

    }
