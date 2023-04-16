let form = document.querySelector('form');

form.addEventListener('submit', (e) => {
    e.preventDefault();
    console.log(e);

    let el = document.getElementById("input-login-username");
    let url = (new URL('home/loginform' + '?userName=' + el.value, document.location)).href;
    console.log("newurl",url);
    fetch(url).then(function (response) {
        location.reload();
    });
});