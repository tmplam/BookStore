// Handle darkmode

$('#darkMode').change(async (e) => {
    const darkMode = $('#darkMode').is(':checked');
    const response = await fetch('/Settings/ToggleDarkMode', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(darkMode),
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

// Handle pagination
function getRange(start, end) {
    let range = [];
    for (let i = start; i <= end; i++) {
        range.push(i);
    }
    return range;
}

function paginationRange(page, totalPage, siblings) {
    const totalPageNoInArray = 7 + siblings;
    if (totalPage <= totalPageNoInArray) {
        return getRange(1, totalPage);
    }

    const leftSiblingsIndex = Math.max(page - siblings, 1);
    const rightSiblingsIndex = Math.min(page + siblings, totalPage);

    const showLeftDots = leftSiblingsIndex > 2;
    const showRightDots = rightSiblingsIndex < totalPage - 2;

    if (!showLeftDots && showRightDots) {
        const leftItemsCount = 3 + 2 * siblings;
        const leftRange = getRange(1, leftItemsCount);
        return [...leftRange, '...', totalPage];
    } else if (showLeftDots && !showRightDots) {
        const rightItemsCount = 3 + 2 * siblings;
        const rightRange = getRange(totalPage - rightItemsCount + 1, totalPage);
        return [1, '...', ...rightRange];
    } else {
        const middleRange = getRange(leftSiblingsIndex, rightSiblingsIndex);
        return [1, '...', ...middleRange, '...', totalPage];
    }
}
