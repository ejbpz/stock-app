let listItems = document.querySelectorAll('#dataList li');
let stockViewComponent = document.querySelector('#stockViewComponent');

listItems.forEach(item => item.addEventListener('click', (event) => {
    event.preventDefault();

    let value = item.getAttribute('data-symbol');

    fetch(`/stocks/stock-details/${value}`)
        .then(resp => resp.text())
        .then(html => {
            stockViewComponent.innerHTML = html;
        })
}));