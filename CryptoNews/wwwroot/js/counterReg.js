const ta = document.querySelector('#counter-input');
const counter = document.querySelector('.counter-text__current');
const total = document.querySelector('.counter-text__total');
const maxl = 200;
total.innerText = maxl;

ta.addEventListener('input', onInput);

function onInput(event) {
    event.target.value = event.target.value.substr(0, maxl);
    const length = event.target.value.length;
    counter.innerText = length;
}
