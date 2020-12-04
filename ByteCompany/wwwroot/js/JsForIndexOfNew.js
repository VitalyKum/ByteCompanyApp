var currentElem = null;

jQuery("table").mouseover(function (event) {
    if (currentElem) return;

    var target = event.target.closest('tr');

    if (!target) return;

    currentElem = target;
    target.style.background = '#DCDCDC';
});

jQuery("table").mouseout(function (event) {
    if (!currentElem) return;

    let relatedTarget = event.relatedTarget;

    while (relatedTarget) {
        // поднимаемся по дереву элементов и проверяем – внутри ли мы currentElem или нет
        // если да, то это переход внутри элемента – игнорируем
        if (relatedTarget == currentElem) return;

        relatedTarget = relatedTarget.parentNode;
    }

    currentElem.style.background = '';
    currentElem = null;
});