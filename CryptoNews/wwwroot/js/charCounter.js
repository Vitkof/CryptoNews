
//onblur = document.getElementById('count').value = this.value.length
//let counting;
function counting33(text) {
    let count = text.length;
    if (count > 900) {
        let commBlock = document.getElementById('mb-3');
        let p = document.createElement('p');

        if (count <= 1000) {
            let left = 1000 - count;
            p.innerHTML = left + ' characters left to enter';
            commBlock.append(p);
        }
        else {
            let right = count - 1000;
            p.innerHTML = right + ' characters over limit';
            commBlock.append(p);
        }
    }
    
}
