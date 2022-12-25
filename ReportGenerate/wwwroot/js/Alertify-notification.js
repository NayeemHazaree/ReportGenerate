function Alertify(message, type, timeFactor=2500) {
    switch (type) {
        case 'success':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'danger':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'primary':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'default':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'info':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'dark':
            AlertifyBuilder(message, type, timeFactor);
            break;
        case 'light':
            AlertifyBuilder(message, type, timeFactor);
            break;
        default:
            AlertifyBuilder(message, 'alertify-default', timeFactor);
    }
}

function AlertifyBuilder(message, type, timeFactor) {
    var c = document.getElementsByClassName('main_container')[0];
    const body = c ? c : document.getElementsByClassName('body-content')[0];
    var alertDiv = document.createElement('div');
    alertDiv.setAttribute('class', `alert alert-${type} alertify-active alertify`);

    var alertDivInner = document.createElement('div');
    alertDivInner.setAttribute('class', 'alertifyInner');
    alertDivInner.innerHTML = `${message}`;
    alertDiv.appendChild(alertDivInner);

    body.appendChild(alertDiv);

    setTimeout(e => {
        body.removeChild(alertDiv);
    }, timeFactor);
}