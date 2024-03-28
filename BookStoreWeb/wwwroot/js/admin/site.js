// Header
$('#darkMode').change(async (e) => {
    const darkMode = $('#darkMode').is(':checked');
    const response = await fetch('/utils/dark-mode', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ darkMode }),
    });
    const result = await response.json();
    if (result.success) {
        if (darkMode) {
            $('html').attr('data-bs-theme', 'dark');
            $('#dark-mode-icon').removeClass('fa-sun').addClass('fa-moon');
        } else {
            $('html').attr('data-bs-theme', 'light');
            $('#dark-mode-icon').removeClass('fa-moon').addClass('fa-sun');
        }
    }
});

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
