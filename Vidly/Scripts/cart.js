let cart = [];

$("#closeCartButton").click(() => {
    $('#cartModal').modal('hide');
});

let movieListMarkup = (movie) => {
    let listElement = `
                            <li class="list-group-item" id="cartElem">
                                <i class="fa fa-times" aria-hidden="true"></i>
                                <span class="name">${movie.Name}</span>
                                <span style="float:right;" class="price">${movie.Price}</span>
                                <p hidden class="movieId">${movie.Id}</p>
                            </li>`;
    return listElement;
}
$("#openCartButton").click(() => {
    let storage = localStorage.getItem("cart");
    cart = JSON.parse(storage);
    $("#cartList").empty();
    if (cart.length != 0) {
        cart.forEach(movie => $("#cartList").append(movieListMarkup(movie)));
            $("#total").text(cart.Total);
    }
    else
        $("#cartList").append("<a>No movies added yet<a>");
    $('#cartModal').modal('show');
});
$('#cartList').on('click',
    '.list-group-item .fa',
    function () {
        let id = parseInt($(this).siblings('.movieId').text());
        let price = parseInt($(this).siblings('.price').text());


        const index = cart.findIndex(movie => {
            return movie.Id == id;
        });


        if (index > -1) {
            cart.splice(index, 1);
            cart.Total -= price;
        }
        localStorage.setItem("cart", JSON.stringify(cart));
        $(this).parent().remove();
    });