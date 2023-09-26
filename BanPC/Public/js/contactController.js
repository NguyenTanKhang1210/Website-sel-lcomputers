var contact =
{
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $("#btnSend").off("click").on("click", function () {
            var name = $("#Name").val();
            var phone = $("#Phone").val();
            var email = $("#Email").val();

            $.ajax({
                url: "/Contact/Send",
                type: "POST",
                dataType: "json",
                data: {
                    name: name,
                    phone: phone,
                    email: email
                },
                success: function (res) {
                    if (res.status == true) {
                        alert("Gửi thành công");
                    }
                }
            })
        });
    }
}
contact.init();