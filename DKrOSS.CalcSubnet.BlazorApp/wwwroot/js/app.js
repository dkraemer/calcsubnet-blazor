// Register PWA service worker
navigator.serviceWorker.register('../service-worker.js');

// Enable tooltips - Get's invoked in MainLayout.OnAfterRenderAsync()
window.enableToolTips = () => {
    $('[data-toggle="tooltip"]').tooltip();
}