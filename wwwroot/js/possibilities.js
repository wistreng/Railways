
    $().ready(function () {
        $("#Form_Possibilities").validate({
            rules: 
            {
                Start:{required:true, maxlength: 1},
                Dest:{required:true, maxlength: 1},
                Max:{digits:true, max:99}
            },
            submitHandler: function(form) {      
                SubmitEventHandler();     
            } 
        });
        $('#ButtonSubmit').click(function () { SubmitEventHandler(); });
    });
    function SubmitEventHandler() {
        if( $("#Form_Possibilities").valid() ){
            var start = $.trim($('#TextBox_Start').val());
            // var dest = $.trim($('#TextBox_Dest').val());
            var postData = $('#Form_Possibilities').serialize();
            $.ajax({
                url: "/QueryRoute/Possibilities",
                type: "POST",
                data: postData,
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    if (data) {
                        ResultHandle(data);
                    }
                    else {
                        alert('No Response!');
                    }
                }
            });
        }
    };
    function ResultHandle(result){
        $(".output").children().remove();
        if(JSONLength(result) > 0)
        {
            $("<li>The are <b>" + JSONLength(result) + "</b> possible routes including: </li>").appendTo(".output");
            $.each(result,function(name,value)
            {
                $("<li>The Route is: " + name + ", The distance is " + value + "</li>").appendTo(".output");
            });
        }
        else
        {
            $("<li>The are <b>" + JSONLength(result) + "</b> possible routes</li>").appendTo(".output");
        }
    };
    function JSONLength(obj) {
        var size = 0, key;
        for (key in obj)
        {
            if (obj.hasOwnProperty(key)) size++;
        }
        return size;
    };
