
let movieCardMarkup = (movie) => {
    let cardMarkup = `
                                <div class="col-3">
                                    <div class="card">
                                        <img class="card-img-top" src="..." alt="Card image cap">
                                            <div class="card-body">
                                                <h5 class="card-title">${movie.Name}</h5>
                                                <p class="card-text">Movie description</p>
                                                <i class="fa fa-plus addToCart" aria-hidden="true"></i>
                                                <p hidden class="movieId">${movie.Id}</p>
                                        </div>
                                    </div>
                                </div>`;
    return cardMarkup;
}

function validateForm() {
    if(cart.length>0)
        return true;
    toastr.error("select at least one film");
    return false;
}

let addMovieCards = async () => {
    let movies = await getMovies();

    let veces = Math.trunc(movies.length / 4);
    let resto = movies.length % 4;

    let k = 0;

    for (let i = 0; i < veces; i++) {

        let row = $(`<div class="row">`);

        for (let j = 0; j < 4; j++) {
            row.append(movieCardMarkup(movies[k]));
            k++;
        }

        $("#container").append(row);
        $("#container").append(`</div>`);
    };

    if (resto) {

        let row = $(`<div class="row">`);

        for (let j = 0; j < resto; j++) {
            row.append(movieCardMarkup(movies[k]));
            k++;
        }

        $("#container").append(row);
        $("#container").append(`</div>`);
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

    $("#container").on("click",
        ".addToCart",
        function () {
            var addToCartButton = $(this);
            var newMovie = {
                Id: parseInt(addToCartButton.siblings(".movieId").text()),
                Name: addToCartButton.siblings(".card-title").text()
            };
            const index = cart.findIndex(cartMovie => {
                return cartMovie.Id == newMovie.Id;
            });

            if (index === -1) {
                cart.push(newMovie);
                localStorage.setItem("cart", JSON.stringify(cart));
                toastr.success("Movie added to Cart.");
            }
            else
                toastr.error("Movie already added to Cart.");
        });

});
