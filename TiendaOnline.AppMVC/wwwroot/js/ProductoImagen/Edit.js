(function () {
    const input = document.getElementById('urlInput');
    const preview = document.getElementById('previewBox');

    function setPreview(url) {
        const clean = (url || '').trim();
        preview.innerHTML = '';

        if (!clean) {
            preview.innerHTML = '<span>PREVIEW</span>';
            return;
        }

        const img = document.createElement('img');
        img.alt = 'Vista previa';
        img.loading = 'lazy';
        img.referrerPolicy = 'no-referrer';

        img.onload = function () {
            preview.innerHTML = '';
            preview.appendChild(img);
        };

        img.onerror = function () {
            preview.innerHTML = `
          <div class="text-center px-3">
            <div class="text-danger fw-semibold">No se pudo cargar</div>
            <div class="text-muted small mt-1">
              Usa un enlace directo o una ruta local tipo /uploads/productos/1.webp
            </div>
          </div>`;
        };

        img.src = clean;
    }

    input?.addEventListener('input', () => setPreview(input.value));
    if (input?.value) setPreview(input.value);
})();
