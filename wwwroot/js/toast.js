var toastObj = new ej.notifications.Toast({
    position: {
        X: 'Right'
    }, target: document.body
});
toastObj.appendTo('#toast_type');
var toasts = [
    { title: 'Error!', cssClass: 'e-toast-danger', icon: 'e-error toast-icons' },
    { title: 'Success!', cssClass: 'e-toast-success', icon: 'e-success toast-icons' },
]; 


