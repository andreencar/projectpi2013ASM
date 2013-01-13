$(document).ready(function () {
    var SBoxList = $('#SearchBoxList');
    
    var SBox = $('#searchInput');
    $('#searchInput').keyup(function () {
        var input = SBox.val();
        SBoxList.children().remove();
        if (input.length >= 3) {

            SBoxList.hide();

            SBoxList.hide();
            $.getJSON("/Ajax/AjaxTest/" , { sbinput : input}, function(data) {
                $.each(data, function (i, item) {
                    var toAppear = document.createElement("li")
                    toAppear.setAttribute('href', item.link);
                    toAppear.innerText = item.name;
                    SBoxList.append(toAppear);
                    if (i == 4) {
                        return false;
                    }
                })
                SBoxList.slideDown("slow");
            } )
   
        }
    })
    //var suggestionList;



})