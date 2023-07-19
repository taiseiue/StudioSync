

if ('wakeLock' in navigator) {

    // 非同期関数を作成して起動ロックをリクエスト
    try {
        navigator.wakeLock.request('screen');
        console.log("Screen WakeLocked.");
    } catch (err) {
        // 起動ロックのリクエストに失敗。ふつうはバッテリーなどのシステム関連
        console.log(`${err.name}, ${err.message}`);
    }
} else {
    console.log("WakeLockAPI not supported.");
}


function showModal(id) {
    var myModal = new bootstrap.Modal(document.getElementById(id), {
        keyboard: false
    });
    myModal.show();
}