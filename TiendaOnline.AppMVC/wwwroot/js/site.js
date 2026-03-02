(function () {
    const path = window.location.pathname.toLowerCase();
    const links = document.querySelectorAll('.navbar .nav-link');

    links.forEach(a => {
        const href = (a.getAttribute('href') || '').toLowerCase();
        if (!href || href === '#') return;

        if (path === href || (href !== '/' && path.startsWith(href))) {
            a.classList.add('active');
        }
    });
})();
