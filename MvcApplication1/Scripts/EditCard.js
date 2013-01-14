$(document).ready(function () {
    var buttonGroup;
    buttonGroup =$("<ul></ul>");
    buttonGroup.attr("class", "dropdown-menu");
    var buttonlist = $("<li></li>");
    var buttonAccept = $("<a></a>");
    buttonAccept.html("Accept");
    buttonlist.append(buttonAccept);
    buttonGroup.append(buttonlist);
    var buttonCancel = $("<a></a>");
    buttonCancel.html("Cancel");
    buttonlist = $("<li></li>");
    buttonlist.append(buttonCancel);
    buttonGroup.append(buttonlist);

    /* Button Group will be implemented later
    
    $(this).click(function () {
        if(buttonGroup != null)
            buttonGroup.remove();
    }
        )

    $(".cardDescription").click(function () {
        $(this).append(buttonGroup);
        buttonGroup.show();
    })*/
 
    $(".cardDescription").keyup(function (e) {
        
        if (e.which == 13) {
            $.post("/Ajax/ChangeDescription/", { description : $(this).html(), cardID : $(this).attr("id")}, function (data) {
                alert("Data Changed!");
            })
        }
    })
})