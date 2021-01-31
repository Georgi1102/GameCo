let counter = document.querySelector('h1');
let count = 1;
var span = document.getElementById('currState');
var text = span.textContent;

if (text == "The selected file was successfully uploaded") {

    setInterval(() => {
        count++;
        counter.innerText = "Can proceed now";
        if (count == 5) {
            window.location.replace("https://localhost:44353");
        }

    },1000)
}

else {
    counter.innerText = "Can not proceed";
}
