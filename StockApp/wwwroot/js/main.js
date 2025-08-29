let price = document.querySelector('#price');
let symbol = document.querySelector('#stock-symbol');
let changeColor = document.querySelector('#changeColor');

fetch('/get-token')
    .then(resp => {
        if (!resp.ok) throw new Error('Error while fetching the token');
        return resp.json();
    })
    .then(data => {
        const token = data.token;

        const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

        socket.addEventListener('open', (event) => {
            socket.send(
                JSON.stringify(
                    { 'type': 'subscribe', 'symbol': symbol.textContent.trim() }
                )
            );
        });

        socket.addEventListener('message', (event) => {
            let finnhubData = JSON.parse(event.data);

            if (!finnhubData || finnhubData.error) return;
            if (finnhubData.data) price.innerHTML = finnhubData.data[0].p;
        });
    });

changeColor.addEventListener('click', () => {
    document.querySelector('html').classList.toggle('dark');
});