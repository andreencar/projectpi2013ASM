$(document).ready(function () {
    var SBoxList = $('#SearchBoxList');
    SBoxList.hide();
    var SBox = $('#searchInput');
    $('#searchInput').keyup(function () {
        var input = SBox.val();
        SBoxList.children().remove();
        SBoxList.hide();
        if (input.length >= 3) {

            //SBoxList.hide();
            $.getJSON("/Ajax/AjaxTest/", { sbinput: input }, function (data) {
                $.each(data, function (i, item) {
                    var toAppear = document.createElement("li");
                    var btn = document.createElement("a");
                    //btn.setAttribute("");
                    btn.setAttribute('href', item.link);
                    btn.innerText = item.name;
                    toAppear.appendChild(btn);
                    SBoxList.append(toAppear);
                    if (i == 4) {
                        return false;
                    }
                    
                })
                if (data.length != 0) {
                    SBoxList.show();
                    SBoxList.slideDown("slow");
                }
            } )
   
        }
    })
    //var suggestionList;



})