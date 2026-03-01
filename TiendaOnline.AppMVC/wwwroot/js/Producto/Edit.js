(function () {
    const sw = document.getElementById('estatusSwitch');
    const hidden = document.getElementById('Estatus');
    const txt = document.getElementById('estatusText');

    function sync() {
        const on = sw.checked;
        hidden.value = on ? 1 : 0;
        if (txt) txt.textContent = on ? 'Activo' : 'Inactivo';
    }

    sw.addEventListener('change', sync);
    sync();
})();