let cart = [];

$("#closeCartButton").click(() => {
    $('#cartModal').modal('hide');
});

let movieListMarkup = (movie) => {
    let listElement = `
                            <li class="list-group-item" id="cartElem">
                                <span class="name">${movie.Name}</span>
                                <p hidden class="movieId">${movie.Id}</p>
                                <button class="btn btn-default btn-xs pull-right remove-item">
                                    <i class="icon-remove">x</i>
                                </button>
                            </li>`;
    return listElement;
}
$("#openCartButton").click(() => {
    let storage = localStorage.getItem("cart");

    cart = JSON.parse(storage);
    console.log(cart);
    $("#cartList").empty();
    if (cart.length != 0)
        cart.forEach(movie => $("#cartList").append(movieListMarkup(movie)));
    else
        $("#cartList").append("<a>No movies added yet<a>");
    $('#cartModal').modal('show');
});
$('#cartList').on('click',
    '.list-group-item',
    function () {
        let id = parseInt($(this).find('.movieId').text());


        const index = cart.findIndex(movie => {
            return movie.Id == id;
        });


        if (index > -1) {
            cart.splice(index, 1); // 2nd parameter means remove one item only
        }
        localStorage.setItem("cart", JSON.stringify(cart));
        $(this).remove();
    });