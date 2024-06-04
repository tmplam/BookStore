// Header
$(() => {
    $('#toggle-btn').click(() => {
        $('#sidebar').toggleClass('hide');
    });

    $('#close-sidebar-btn').click(() => {
        $('#sidebar').addClass('hide');
    });
});


// Sidebar
$(() => {
    if (window.matchMedia('(max-width: 899.5px)').matches) {
        $('#sidebar').addClass('hide');

        document.addEventListener('click', function (event) {
            const sidebar = document.getElementById('sidebar');
            const toggle = document.getElementById('toggle-btn');

            if (!toggle.contains(event.target) && !sidebar.contains(event.target)) {
                $('#sidebar').addClass('hide');
            }
        });
    }

    $('#sidebar-nav li').click((e) => {
        $('#sidebar-nav li a.active').addClass('link-body-emphasis').removeClass('active');
    });
});
