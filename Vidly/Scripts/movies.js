
let movieCardMarkup = (movie) => {
    let cardMarkup = `
        <div class="card">
            <div style="width:100%; height:60%;">
                <img  style="width: 100%;height: 100%; max-height:100%;" src="../${movie.ImageUrl}" alt="Card image cap">
            </div>
            <div class="card-body">
                <h5 class="card-title">${movie.Name}</h5>
                <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content.</p>
            </div>
            <div class="card-footer">
                <small class="moviePrice">$${movie.Price}</small>
                <small hidden class="movieId">${movie.Id}</small>
                <i class="fa fa-plus addToCart grow" style="cursor: pointer;float:right; font-size: 1.73em;" aria-hidden="true"></i>
            </div>
        </div>`;
    return cardMarkup;
}

function validateForm() {
    if(cart.MovieList.length>0)
        return true;
    toastr.error("select at least one film");
    return false;
}

let addMovieCards = async () => {
    let movies = await getMovies();
    const COLS = 3;
    let times = Math.trunc(movies.length / COLS);
    let rest = movies.length % COLS;
    let fill = COLS - rest;

    let k = 0;

    for (let i = 0; i < times; i++) {

        let row = $(`<div class="row">`);
        let deck = $(`<div class="card-deck">`);

        for (let j = 0; j < COLS; j++) {
            deck.append(movieCardMarkup(movies[k]));
            k++;
        }

        deck.append(`</div>`);
        row.append(deck);
        row.append(`</div>`);
        $("#container").append(row);
    };

    if (rest) {

        let row = $(`<div class="row">`);
        let deck = $(`<div class="card-deck">`);

        for (let j = 0; j < rest; j++) {
            deck.append(movieCardMarkup(movies[k]));
            k++;
        }

        for (let j = 0; j < fill; j++) {
            deck.append(`<div class="card" style="visibility:hidden;"></div>`);
            k++;
        }

        deck.append(`</div>`);
        row.append(deck);
        row.append(`</div>`);
        $("#container").append(row);
    }
}

let getMovies = () => {
    return fetch('http://localhost:49333/api/movies')
        .then((res) => {
            return res.json();
        });
}

//toastr.options{ "positionClass": "toast-bottom-right"}

$(document).ready(function () {
    addMovieCards();
    toastr.options.timeOut = 15;
    let cart = getCart();
    $("#container").on("click",
        ".addToCart",
        function () {
            cart = JSON.parse(localStorage.getItem("cart"));
            console.log(cart);
            var addToCartButton = $(this);
            var newMovie = {
                Id: parseInt(addToCartButton.siblings(".movieId").text()),
                Name: addToCartButton.parent().siblings(".card-body").find(".card-title").text(),
                Price: parseInt(addToCartButton.parent().find('.moviePrice').text().substring(1))
            };
            const index = cart.MovieList.findIndex(cartMovie => {
                return cartMovie.Id == newMovie.Id;
            });

            if (index === -1) {
                cart.MovieList.push(newMovie);
                cart.Total += newMovie.Price;
                localStorage.setItem("cart", JSON.stringify(cart));
                toastr.success("Movie added to Cart.");
            }
            else
                toastr.error("Movie already added to Cart.");
        });

});
