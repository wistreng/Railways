
    $(document).ready(function () {
        $("#Form_Shortest").validate({
            rules: 
            {
                Start:{required:true, maxlength: 1},
                Dest:{required:true, maxlength: 1},
            },
            submitHandler: function(form) {      
                SubmitEventHandler();     
            } 
        });
        $('#ButtonSubmit').click(function () { SubmitEventHandler(); });
    });
    function SubmitEventHandler() {
        if( $("#Form_Shortest").valid()){
            var postData = $('#Form_Shortest').serialize();
            $.ajax({
                url: "/QueryRoute/Shortest",
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
                        alert("No response!");
                    }
                }
            });
        }
    }
    function ResultHandle(result){
        $(".output").children().remove();
        if(JSONLength(result) > 0){
            $.each(result,function(name,value)
            {
                $("<li>The Shortest length is: <b>" + value + "</b></li>").appendTo(".output");
                $("<li>The Route is: " + name + "</li>").appendTo(".output");
                // count++;
            });
        }
        else{
            $("<li>The is no Through route</li>").appendTo(".output");
        }
    }
    function JSONLength(obj) {
        var size = 0, key;
        for (key in obj)
        {
            if (obj.hasOwnProperty(key)) size++;
        }
        return size;
    };