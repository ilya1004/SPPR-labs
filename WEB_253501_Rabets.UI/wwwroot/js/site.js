
$(document).on('click', '.page-link', function (event) {
    event.preventDefault();

    const url = $(this).attr('href');
    console.log(url);
    $('#product-list').load(url);
});

