$(document).ready(function () {
        let skip = 3;
    $("#loadMore").click(function () {

        $.ajax({
            url: `/blog/loadmore?offset=${skip}`,
            method: "get",
            success: function (datas) {
                $("#blogList").append(datas);
                skip += 3;
                if (skip >= $("#blogCount").val()) {
                    $("#loadMore").remove();
                }
            },
            error: function (error) {
                console.log(error)
            }
        })

    });

    $(document).on("keyup", "#input-search", function () {
        $("#searchList li").slice(1).remove();
        let value = $(this).val().trim();
        if (value) {
            $.ajax({
                url: "/blog/searchblog?text="+value,
                method: "get",
                success: function (datas) {
                    $("#searchList").append(datas);
                },
                error: function (error) {
                    console.log(error)
                }
            })
        }
       
    })







});
