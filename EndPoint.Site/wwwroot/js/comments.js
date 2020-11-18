function SetReply(id, name) {

    document.getElementById('Reply').innerHTML = id;
    document.getElementById('Cancell').removeAttribute('hidden');

}

function CancellReply() {
    document.getElementById('Reply').innerHTML = '0';
    document.getElementById('Cancell').setAttribute('hidden','hidden');
}