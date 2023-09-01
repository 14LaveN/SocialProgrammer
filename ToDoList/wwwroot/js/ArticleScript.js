$('#CreateArticle').on('click', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'POST',
        url: '@Url.Action("CreateArticle", "Article")',
        data: $('#CreateArticleForm').serialize(),
        success: function (response) {
            Swal.fire({
                title: 'Информация',
                text: response.description,
                icon: 'success',
                confirmButtonText: 'Окей'
            })
        },
        error: function (response) {
            Swal.fire({
                title: 'Информация',
                text: response.responseJSON.description,
                icon: 'error',
                confirmButtonText: 'Окей'
            })
        }
    })
});